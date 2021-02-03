using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BrandsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke(string BrandId)
        {
            ViewBag.BrandId = int.TryParse(BrandId, out var id) ? id : (int?)null;
            return View(GetBrands());
        }

        private IEnumerable<BrandViewModel> GetBrands() =>
            _ProductData.GetBrands()
                .OrderBy(brand => brand.Order)
                .Select(brand => new BrandViewModel
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    ProductsCount = brand.ProductsCount
                });
    }
}
