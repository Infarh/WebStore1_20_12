using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        public CatalogController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Index() => View(_ProductData.GetProducts().FromDTO());

        public IActionResult Edit(int id)
        {
            var product = _ProductData.GetProductById(id);
            if (product is null) return NotFound();
            return View(product.FromDTO());
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid) return View(product);
            
            // Логика редактирования
            // Логика вызова метода из IProductData

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var product = _ProductData.GetProductById(id);
            if (product is null) return NotFound();
            return View(product.FromDTO());
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            // Логика удаления

            return RedirectToAction(nameof(Index));
        }
    }
}
