using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BC_Exercise.Web;
using BC_Exercise.Web.Controllers;

namespace BC_Exercise.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Welcome()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Welcome() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
