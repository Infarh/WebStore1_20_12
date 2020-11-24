using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _Configuration;

        public HomeController(IConfiguration Configuration) => _Configuration = Configuration;

        public IActionResult Index()
        {
            // Обработка данных

            return Content(_Configuration["ControllerActionText"]);
        }

        public IActionResult SecondAction()
        {
            return Content("Second controller action");
        }
    }
}
