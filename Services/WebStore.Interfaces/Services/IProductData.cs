using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<SectionDTO> GetSections();

        SectionDTO GetSectionById(int id);

        IEnumerable<BrandDTO> GetBrands();

        BrandDTO GetBrandById(int id);

        PageProductsDTO GetProducts(ProductFilter Filter = null);

        ProductDTO GetProductById(int id);
    }
}
