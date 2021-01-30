using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Controllers;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            // A-A-A = Arrange - Act - Assert = Подготовка данных - выполнения действия - проверка результатов

            var controller = new HomeController();

            var result = controller.Index();

            //Assert.IsInstanceOfType(result, typeof(ViewResult)); // MSTest
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void SecondAction_Returns_View()
        {
            var controller = new HomeController();
            var expected_content_string = "Second controller action";

            var result = controller.SecondAction();

            var content_result = Assert.IsType<ContentResult>(result);
            Assert.Equal(expected_content_string, content_result.Content);
        }

        [TestMethod]
        public void Blogs_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Blogs();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.BlogSingle();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ContactUs_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.ContactUs();

            Assert.IsType<ViewResult>(result);
        }
    }
}
