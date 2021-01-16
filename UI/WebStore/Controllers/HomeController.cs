using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Data;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _Configuration;

        public HomeController(IConfiguration Configuration) => _Configuration = Configuration;

        public IActionResult Index() => View();

        public IActionResult Throw(string id) => throw new ApplicationException(id);

        public IActionResult SecondAction()
        {
            return Content("Second controller action");
        }

        public IActionResult Blogs() => View();
        
        public IActionResult BlogSingle() => View();
        
        public IActionResult Cart() => View();
        
        public IActionResult Checkout() => View();
        
        public IActionResult ContactUs() => View();
        
        public IActionResult ProductDetails() => View();
        
        public IActionResult Shop() => View();
        
    }
}
