using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InMemory
{
    [Obsolete("Класс устарел потому что не надо размещать данные в памяти. Пользуйтесь классом SqlProductData", true)]
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;
        public Section GetSectionById(int id) => throw new NotSupportedException();

        public IEnumerable<Brand> GetBrands() => TestData.Brands;
        public Brand GetBrandById(int id) => throw new NotSupportedException();

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            var query = TestData.Products;

            if (Filter?.SectionId is { } section_id) // сопоставление с образцом
                query = query.Where(product => product.SectionId == section_id);

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            return query;
        }

        public Product GetProductById(int id) => throw new NotSupportedException();
    }
}
