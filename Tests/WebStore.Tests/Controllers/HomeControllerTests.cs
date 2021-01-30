using System;
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

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Throw_thrown_ApplicationException()
        {
            var controller = new HomeController();
            const string expected_exception_message = "Test";

            var result = controller.Throw(expected_exception_message);
        }

        [TestMethod]
        public void Throw_thrown_ApplicationException2()
        {
            var controller = new HomeController();
            const string expected_exception_message = "Test";

            Exception actual_exception = null;
            try
            {
                controller.Throw(expected_exception_message);
            }
            catch (Exception error)
            {
                actual_exception = error;
            }

            var app_exception = Assert.IsType<ApplicationException>(actual_exception);
            Assert.Equal(expected_exception_message, app_exception.Message);
        }

        [TestMethod]
        public void Throw_thrown_ApplicationException3()
        {
            var controller = new HomeController();
            const string expected_exception_message = "Test";

            var actual_exception = Assert.Throws<ApplicationException>(() => controller.Throw(expected_exception_message));
            Assert.Equal(expected_exception_message, actual_exception.Message);
        }

        [TestMethod]
        public void Error404_Returns_View()
        {
            var controller = new HomeController();

            var result = controller.Error404();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ErrorStatus_404_RedirectTo_Error404()
        {
            var controller = new HomeController();
            const string error_status_code = "404";
            const string expected_action_name = nameof(HomeController.Error404);

            var result = controller.ErrorStatus(error_status_code);

            //Assert.NotNull(result);

            var redirect_to_action = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(expected_action_name, redirect_to_action.ActionName);
            Assert.Null(redirect_to_action.ControllerName);
        }
    }
}
