using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {
        [TestMethod]
        public void Details_Returns_with_Correct_View()
        {
            #region Arrange

            const int expected_product_id = 1;
            const decimal expected_price = 10m;

            var expected_name = $"Product id {expected_product_id}";
            var expected_brand_name = $"Brand of product {expected_product_id}";

            var product_data_mock = new Mock<IProductData>();
            product_data_mock.Setup(p => p.GetProductById(It.IsAny<int>()))
               .Returns<int>(id => new ProductDTO(
                    id,
                    $"Product id {id}",
                    1,
                    expected_price,
                    $"img{id}.png",
                    new BrandDTO(1, $"Brand of product {id}", 1, 1),
                    new SectionDTO(1, $"Section of product {id}", 1, null, 1)
                    ));

            var controller = new CatalogController(product_data_mock.Object);

            #endregion

            #region Act

            var result = controller.Details(expected_product_id);

            #endregion

            #region Assert

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(view_result.Model);

            Assert.Equal(expected_product_id, model.Id);
            Assert.Equal(expected_name, model.Name);
            Assert.Equal(expected_price, model.Price);
            Assert.Equal(expected_brand_name, model.Brand);

            #endregion

        }

        [TestMethod]
        public void Shop_Returns_CorrectView()
        {
            var products = new[]
            {
                new ProductDTO(1, "Product1", 1, 10m, "Image1.png", new BrandDTO(1, "Brand1", 1, 1), new SectionDTO(1, "Section1", 1, null, 1)),
                new ProductDTO(2, "Product2", 2, 20m, "Image2.png", new BrandDTO(2, "Brand2", 1, 1), new SectionDTO(2, "Section2", 2, null, 1)),
                new ProductDTO(3, "Product3", 3, 30m, "Image3.png", new BrandDTO(5, "Brand5", 1, 1), new SectionDTO(3, "Section3", 3, null, 1)),
            };

            const int expected_section_id = 1;
            const int expected_brand_id = 5;

            var product_data_mock = new Mock<IProductData>();
            product_data_mock
               .Setup(p => p.GetProducts(It.IsAny<ProductFilter>()))
               .Returns(products);

            var controller = new CatalogController(product_data_mock.Object);

            var result = controller.Shop(expected_brand_id, expected_section_id);

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<CatalogViewModel>(view_result.ViewData.Model);

            Assert.Equal(products.Length, model.Products.Count());
            Assert.Equal(expected_brand_id, model.BrandId);
            Assert.Equal(expected_section_id, model.SectionId);
        }
    }
}
