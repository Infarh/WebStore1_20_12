using Microsoft.AspNetCore.Mvc;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatalogController : Controller
    {
        public IActionResult Index() => View();
    }
}
