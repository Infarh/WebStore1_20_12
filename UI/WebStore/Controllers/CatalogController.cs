using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private const string __PageSize = "CatalogPageSize";

        private readonly IProductData _ProductData;
        private readonly IConfiguration _Configuration;

        public CatalogController(IProductData ProductData, IConfiguration Configuration)
        {
            _ProductData = ProductData;
            _Configuration = Configuration;
        }

        public IActionResult Shop(int? BrandId, int? SectionId, int Page = 1, int? PageSize = null)
        {
            var page_size = PageSize
                ?? (int.TryParse(_Configuration[__PageSize], out var value) ? value : null);

            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Page = Page,
                PageSize = page_size
            };

            var (products, total_count) = _ProductData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products.OrderBy(p => p.Order).FromDTO().ToView(),
                PageViewModel = new PageViewModel
                {
                    Page = Page,
                    PageSize = page_size ?? 0,
                    TotalItems = total_count,
                },
            });
        }

        public IActionResult Shop2(ProductFilter filter)
        {
            var products = _ProductData.GetProducts(filter);

            return View("Shop", new CatalogViewModel
            {
                SectionId = filter.SectionId,
                BrandId = filter.BrandId,
                Products = products.Products
                   .OrderBy(p => p.Order)
                   .Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl
                    })
            });
        }

        public IActionResult Details(int id) =>
            _ProductData.GetProductById(id) is { } product
                ? View(product.FromDTO().ToView())
                : NotFound();

        #region WebAPI

        public IActionResult GetFeaturedItems(int? BrandId, int? SectionId, int Page = 1, int? PageSize = null)
            => PartialView("Partial/_FeaturesItems", GetProducts(BrandId, SectionId, Page, PageSize));

        private IEnumerable<ProductViewModel> GetProducts(int? BrandId, int? SectionId, int Page, int? PageSize) =>
            _ProductData.GetProducts(
                    new ProductFilter
                    {
                        SectionId = SectionId,
                        BrandId = BrandId,
                        Page = Page,
                        PageSize = PageSize
                            ?? (int.TryParse(_Configuration[__PageSize], out var size) ? size : null)
                    })
               .Products.OrderBy(p => p.Order)
               .FromDTO()
               .ToView();


        #endregion
    }
}
