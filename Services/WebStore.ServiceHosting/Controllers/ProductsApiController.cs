using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;

        public ProductsApiController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("sections")] // http://localhost:5000/api/products/sections
        public IEnumerable<Section> GetSections() => _ProductData.GetSections();

        [HttpGet("sections/{id}")] // http://localhost:5000/api/products/sections/5
        public Section GetSectionById(int id) => _ProductData.GetSectionById(id);

        [HttpGet("brands")] // http://localhost:5000/api/products/brands
        public IEnumerable<Brand> GetBrands() => _ProductData.GetBrands();

        [HttpGet("brands/{id}")] // http://localhost:5000/api/products/brands/5
        public Brand GetBrandById(int id) => _ProductData.GetBrandById(id);

        [HttpPost]
        public IEnumerable<Product> GetProducts([FromBody] ProductFilter Filter) => _ProductData.GetProducts(Filter);

        [HttpGet("{id}")] // http://localhost:5000/api/products/5
        public Product GetProductById(int id) => _ProductData.GetProductById(id);
    }
}
