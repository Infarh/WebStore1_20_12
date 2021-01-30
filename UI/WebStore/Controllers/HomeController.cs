using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Throw(string id) => throw new ApplicationException(id);

        public IActionResult SecondAction() => Content("Second controller action");

        public IActionResult Blogs() => View();
        
        public IActionResult BlogSingle() => View();
        
        public IActionResult ContactUs() => View();

        public IActionResult Error404() => View();

        public IActionResult ErrorStatus(string Code) => Code switch
        {
            "404" => RedirectToAction(nameof(Error404)),
            _ => Content($"Error code {Code}")
        };
    }
}
