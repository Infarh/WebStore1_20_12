using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db) => _db = db;

        public IEnumerable<SectionDTO> GetSections() => _db.Sections
           .Include(section => section.Products)
           .AsEnumerable()
           .ToDTO();

        //public Section GetSectionById(int id) => _db.Sections.Find(id);
        public SectionDTO GetSectionById(int id) => _db.Sections
           .Include(section => section.Products)
           .FirstOrDefault(s => s.Id == id)
           .ToDTO();

        public IEnumerable<BrandDTO> GetBrands() => _db.Brands
           .Include(brand => brand.Products)
           .AsEnumerable()
           .ToDTO();

        public BrandDTO GetBrandById(int id) => _db.Brands
           .Include(b => b.Products)
           .FirstOrDefault(b => b.Id == id)
           .ToDTO();

        public PageProductsDTO GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products
                .Include(p => p.Brand)
                .Include(p => p.Section);

            if (Filter?.Ids?.Length > 0)
                query = query.Where(product => Filter.Ids.Contains(product.Id));
            else
            {
                if (Filter?.BrandId != null)
                    query = query.Where(product => product.BrandId == Filter.BrandId);

                if (Filter?.SectionId != null)
                    query = query.Where(product => product.SectionId == Filter.SectionId);
            }

            var total_count = query.Count();

            if (Filter?.PageSize > 0)
                query = query
                   .Skip((Filter.Page - 1) * (int) Filter.PageSize)
                   .Take((int) Filter.PageSize);


            return new PageProductsDTO(query.AsEnumerable().ToDTO(), total_count);
        }

        public ProductDTO GetProductById(int id) => _db.Products
           .Include(p => p.Brand)
           .Include(p => p.Section)
           .FirstOrDefault(p => p.Id == id)
           .ToDTO();
    }
}
