﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _CartService;

        public CartController(ICartService CartService) => _CartService = CartService;

        public IActionResult Index() => View(new CartOrderViewModel
        {
            Cart = _CartService.TransformFromCart()
        });

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

        [Authorize]
        public async Task<IActionResult> CheckOut(OrderViewModel OrderModel, [FromServices] IOrderService OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), new CartOrderViewModel
                {
                    Cart = _CartService.TransformFromCart(),
                    Order = OrderModel
                });

            var create_order_model = new CreateOrderModel(
                OrderModel,
                _CartService.TransformFromCart().Items
                   .Select(item => new OrderItemDTO(
                        item.Product.Id,
                        item.Product.Price, 
                        item.Quantity))
                   .ToList());

            var order = await OrderService.CreateOrder(User.Identity!.Name, create_order_model);

            //var order = await OrderService.CreateOrder(
            //    User.Identity!.Name, 
            //    _CartService.TransformFromCart(), 
            //    OrderModel);

            _CartService.Clear();
            
            return RedirectToAction("OrderConfirmed", new { order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        #region WebAPI

        public IActionResult GetCartView() => ViewComponent("Cart");

        public IActionResult AddToCartAPI(int id)
        {
            _CartService.AddToCart(id);
            return Json(new { id, message = $"Товар с id:{id} был добавлен в корзину" });
        }

        public IActionResult RemoveFromCartAPI(int id)
        {
            _CartService.RemoveFromCart(id);
            return Ok();
        }

        public IActionResult DecrementFromCartAPI(int id)
        {
            _CartService.DecrementFromCart(id);
            return Ok(new { id, message = $"Количество товара с id:{id} уменьшено на 1" });
        }

        public IActionResult ClearAPI()
        {
            _CartService.Clear();
            return Ok();
        }

        #endregion
    }
}
