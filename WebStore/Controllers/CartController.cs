using System;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _CartService;

        public CartController(ICartService CartService) => _CartService = CartService;

        public IActionResult Index() => View(_CartService.TransformFromCart());

        public IActionResult AddToCart(int id)
        {
            _CartService.AddToCart(id);
            //var referer = Request.Headers["Referer"].ToString();
            //var ref_url = new Uri(referer);
            //referer = referer.Substring(ref_url.Host.Length + ref_url.Scheme.Length + 3);
            //if (Url.IsLocalUrl(referer))
            //    return Redirect(referer);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            _CartService.RemoveFromCart(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DecrementFromCart(int id)
        {
            _CartService.DecrementFromCart(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            _CartService.Clear();
            return RedirectToAction(nameof(Index));
        }
    }
}
