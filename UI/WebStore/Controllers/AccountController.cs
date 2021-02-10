using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(
            UserManager<User> UserManager, 
            SignInManager<User> SignInManager,
            ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        #region Регистрация нового пользователя

        [AllowAnonymous]
        public async Task<IActionResult> IsNameFree(string UserName)
        {
            _Logger.LogInformation($"Запрос проверки занятости имени пользвоателя {UserName}");

            var user = await _UserManager.FindByNameAsync(UserName);
            return Json(user is null ? "true" : "Пользователь с таким имененм уже существует");
        }

        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);

            _Logger.LogInformation("Регистрация нового пользователя {0}", Model.UserName);
            //_Logger.LogInformation($"Регистрация нового пользователя {Model.UserName}");

            using (_Logger.BeginScope("Регистрация пользователя {0}", Model.UserName))
            {
                var user = new User
                {
                    UserName = Model.UserName
                };

                var registration_result = await _UserManager.CreateAsync(user, Model.Password);
                if (registration_result.Succeeded)
                {
                    _Logger.LogInformation("Пользователь {0} зарегистрирован", user.UserName);

                    await _UserManager.AddToRoleAsync(user, Role.User);

                    _Logger.LogInformation("Пользователю {0} назначена роль {1}",
                        user.UserName, Role.User);


                    await _SignInManager.SignInAsync(user, false);
                    _Logger.LogInformation("Пользователь {0} автоматически вошёл в систему после регистрации", user.UserName);

                    return RedirectToAction("Index", "Home");
                }

                _Logger.LogWarning("Ошибка при регистрации нового пользователя {0}:{1}",
                    user.UserName,
                    string.Join(",", registration_result.Errors.Select(e => e.Description)));

                foreach (var error in registration_result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(Model);
        }

        #endregion

        #region Вход в систему

        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            _Logger.LogInformation("Вход пользователя {0} в систему", Model.UserName);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
#if DEBUG
                false
#else
                true
#endif
                );

            if (login_result.Succeeded)
            {
                _Logger.LogInformation("Вход пользователя {0} в систему - успешно выполнен", Model.UserName);

                if (Url.IsLocalUrl(Model.ReturnUrl))
                    return Redirect(Model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            _Logger.LogWarning("Вход пользователя {0} в систему - неверный пароль, или имя пользователя", Model.UserName);


            ModelState.AddModelError("", "Неверное имя пользователя, или пароль!");

            return View(Model);
        }

        #endregion

        public async Task<IActionResult> Logout()
        {
            var user_name = User.Identity!.Name;
            await _SignInManager.SignOutAsync();

            _Logger.LogInformation("{0} вышел из системы", user_name);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            _Logger.LogWarning("Отказано в доступе к {0}", Request.Path);

            return View();
        }
    }
}
