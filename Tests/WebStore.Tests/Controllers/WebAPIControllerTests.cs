using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces.TestAPI;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class WebAPIControllerTests
    {
        [TestMethod]
        public void Index_Returns_View_with_Values()
        {
            var expected_values = new[] { "1", "2", "3" };

            var values_service_mock = new Mock<IValuesServices>();
            values_service_mock
               .Setup(service => service.Get())
               .Returns(expected_values);

            var controller = new WebAPIController(values_service_mock.Object); // "Стаб"

            var result = controller.Index();

            var view_result = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<string>>(view_result.Model);

            Assert.Equal(expected_values.Length, model.Count());

            //"Мок"

            values_service_mock.Verify(service => service.Get());
            values_service_mock.VerifyNoOtherCalls();
        }
    }
}
