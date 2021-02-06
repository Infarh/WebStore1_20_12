using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration Configuration) : base(Configuration, WebAPI.Products) { }

        public IEnumerable<SectionDTO> GetSections() => Get<IEnumerable<SectionDTO>>($"{Address}/sections");

        public SectionDTO GetSectionById(int id) => Get<SectionDTO>($"{Address}/sections/{id}");

        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{Address}/brands");

        public BrandDTO GetBrandById(int id) => Get<BrandDTO>($"{Address}/brands/{id}");

        public PageProductsDTO GetProducts(ProductFilter Filter = null)
        {
            var response = Post(Address, Filter ?? new ProductFilter());
            var result = response.Content
               .ReadAsAsync<PageProductsDTO>()
               .Result;
            return result;
        }

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{Address}/{id}");
    }
}
