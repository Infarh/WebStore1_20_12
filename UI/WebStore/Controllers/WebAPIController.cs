using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.TestAPI;

namespace WebStore.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValuesServices _ValueService;

        public WebAPIController(IValuesServices ValueService) => _ValueService = ValueService;

        public IActionResult Index()
        {
            var values = _ValueService.Get();

            return View(values);
        }
    }
}
