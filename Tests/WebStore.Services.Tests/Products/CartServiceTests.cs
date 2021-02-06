using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Products;
using Assert = Xunit.Assert;

namespace WebStore.Services.Tests.Products
{
    [TestClass]
    public class CartServiceTests
    {
        private Cart _Cart;
        private Mock<IProductData> _ProductDataMock;
        private Mock<ICartStore> _CartStoreMock;

        private ICartService _CartService;

        [TestInitialize]
        public void Initialize()
        {
            _Cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new() { ProductId = 1, Quantity = 1 },
                    new() { ProductId = 2, Quantity = 3 },
                }
            };

            _ProductDataMock = new Mock<IProductData>();
            _ProductDataMock
               .Setup(c => c.GetProducts(It.IsAny<ProductFilter>()))
               .Returns(new PageProductsDTO(new ProductDTO[]
                {
                    new(1, "Product 1", 0, 1.1m, "Product1.png", new BrandDTO(1, "Brand 1", 1, 1), new SectionDTO(1, "Section 1", 1, null, 1)),
                    new(2, "Product 2", 1, 2.2m, "Product2.png", new BrandDTO(2, "Brand 2", 2, 1), new SectionDTO(2, "Section 2", 2, null, 1)),
                }, 2));

            _CartStoreMock = new Mock<ICartStore>();
            _CartStoreMock.Setup(c => c.Cart).Returns(_Cart);

            _CartService = new CartService(_ProductDataMock.Object, _CartStoreMock.Object);
        }

        [TestMethod]
        public void Cart_Class_ItemsCount_returns_Correct_Quantity()
        {
            var cart = _Cart;
            const int expected_count = 4;

            var actual_count = cart.ItemsCount;

            Assert.Equal(expected_count, actual_count);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_ItemsCount()
        {
            var cart_view_model = new CartViewModel
            {
                Items = new[]
                {
                    ( new ProductViewModel { Id = 1, Name = "Product 1", Price = 0.5m }, 1 ),
                    ( new ProductViewModel { Id = 2, Name = "Product 2", Price = 1.5m }, 3 ),
                }
            };
            const int expected_count = 4;

            var actual_count = cart_view_model.ItemsCount;

            Assert.Equal(expected_count, actual_count);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_TotalPrice()
        {
            var cart_view_model = new CartViewModel
            {
                Items = new[]
                {
                    ( new ProductViewModel { Id = 1, Name = "Product 1", Price = 0.5m }, 1 ),
                    ( new ProductViewModel { Id = 2, Name = "Product 2", Price = 1.5m }, 3 ),
                }
            };
            var expected_total_price = cart_view_model.Items.Sum(item => item.Quantity * item.Product.Price);

            var actual_total_price = cart_view_model.TotalPrice;

            Assert.Equal(expected_total_price, actual_total_price);
        }

        [TestMethod]
        public void CartService_AddToCart_WorkCorrect()
        {
            _Cart.Items.Clear();

            const int expected_id = 5;
            const int expected_items_count = 1;

            _CartService.AddToCart(expected_id);

            Assert.Equal(expected_items_count, _Cart.ItemsCount);

            Assert.Single(_Cart.Items);

            Assert.Equal(expected_id, _Cart.Items.First().ProductId);
        }

        [TestMethod]
        public void CartService_RemoveFromCart_Remove_Correct_Item()
        {
            const int item_id = 1;
            const int expected_product_id = 2;

            _CartService.RemoveFromCart(item_id);

            Assert.Single(_Cart.Items);

            Assert.Equal(expected_product_id, _Cart.Items.Single().ProductId);
        }

        [TestMethod]
        public void CartService_Clear_ClearCart()
        {
            _CartService.Clear();

            Assert.Empty(_Cart.Items);
        }

        [TestMethod]
        public void CartService_Decrement_Correct()
        {
            const int item_id = 2;
            const int expected_quantity = 2;
            const int expectes_items_count = 3;
            const int expected_products_count = 2;

            _CartService.DecrementFromCart(item_id);

            Assert.Equal(expectes_items_count, _Cart.ItemsCount);
            Assert.Equal(expected_products_count, _Cart.Items.Count);
            var items = _Cart.Items.ToArray();
            Assert.Equal(item_id, items[1].ProductId);
            Assert.Equal(expected_quantity, items[1].Quantity);
        }

        [TestMethod]
        public void CartService_Remove_Item_When_Decrement_to_0()
        {
            const int item_id = 1;
            const int expected_items_count = 3;

            _CartService.DecrementFromCart(item_id);

            Assert.Equal(expected_items_count, _Cart.ItemsCount);
            Assert.Single(_Cart.Items);
        }

        [TestMethod]
        public void CartService_TransformFromCart_WorkCorrect()
        {
            const int expected_items_count = 4;
            const decimal expected_first_product_price = 1.1m;

            var result = _CartService.TransformFromCart();

            Assert.Equal(expected_items_count, result.ItemsCount);
            Assert.Equal(expected_first_product_price, result.Items.First().Product.Price);
        }
    }
}
