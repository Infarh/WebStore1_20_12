using Microsoft.AspNetCore.Mvc;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
