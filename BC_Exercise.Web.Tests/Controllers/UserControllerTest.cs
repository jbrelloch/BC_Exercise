using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BC_Exercise.Web;
using BC_Exercise.Web.Controllers;

namespace BC_Exercise.Web.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void Users()
        {
            // Arrange
            UserController controller = new UserController();

            // Act
            ViewResult result = controller.Users() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Add()
        {
            // Arrange
            UserController controller = new UserController();

            // Act
            ViewResult result = controller.Add() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            UserController controller = new UserController();

            // Act
            ViewResult result = controller.Edit() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
