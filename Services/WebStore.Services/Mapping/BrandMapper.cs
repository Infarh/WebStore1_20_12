using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public static class BrandMapper
    {
        public static BrandDTO ToDTO(this Brand Brand) => Brand is null
            ? null
            : new BrandDTO(
                Brand.Id, 
                Brand.Name, 
                Brand.Order
            );

        public static Brand FromDTO(this BrandDTO Brand) => Brand is null
            ? null
            : new Brand
            {
                Id = Brand.Id,
                Name = Brand.Name,
                Order = Brand.Order,
            };

        public static IEnumerable<BrandDTO> ToDTO(this IEnumerable<Brand> Brands) => Brands.Select(ToDTO);

        public static IEnumerable<Brand> FromDTO(this IEnumerable<BrandDTO> Brands) => Brands.Select(FromDTO);
    }
}
