using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product p) => new ProductViewModel()
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            Brand = p.Brand?.Name
        };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> p) => p.Select(ToView);

        public static ProductDTO ToDTO(this Product Product) => Product is null
            ? null
            : new ProductDTO(
                Product.Id,
                Product.Name,
                Product.Order,
                Product.Price,
                Product.ImageUrl,
                Product.Brand.ToDTO(),
                Product.Section.ToDTO());

        public static Product FromDTO(this ProductDTO Product) => Product is null
            ? null
            : new Product
            {
                Id = Product.Id,
                Name = Product.Name,
                Order = Product.Order,
                Price = Product.Price,
                ImageUrl = Product.ImageUrl,
                BrandId = Product.Brand?.Id,
                Brand = Product.Brand.FromDTO(),
                SectionId = Product.Section.Id,
                Section = Product.Section.FromDTO(),
            };

        public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product> Products) => Products.Select(ToDTO);

        public static IEnumerable<Product> FromDTO(this IEnumerable<ProductDTO> Products) => Products.Select(FromDTO);
    }
}
