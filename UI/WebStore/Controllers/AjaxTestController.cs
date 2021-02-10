using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class AjaxTestController : Controller
    {
        public IActionResult Index() => View();

        public async Task<IActionResult> GetJSON(int? id, string msg, int Delay = 2000)
        {
            await Task.Delay(Delay);

            return Json(new
            {
                Message = $"Response (id:{id ?? -1}): {msg ?? "<null>"}",
                ServerTime = DateTime.Now
            });
        }

        public async Task<IActionResult> GetHTML(int? id, string msg, int Delay = 2000)
        {
            await Task.Delay(Delay);
            return PartialView("Partial/_DataView", new AjaxTestDataViewModel(id, msg, DateTime.Now));
        }

        public IActionResult SignalRTest() => View();
    }
}
