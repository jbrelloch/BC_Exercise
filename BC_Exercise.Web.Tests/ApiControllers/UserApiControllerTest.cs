using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using BC_Exercise.Core.Infrastructure;
using BC_Exercise.Core.Interfaces;
using BC_Exercise.Models.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BC_Exercise.Web;
using BC_Exercise.Web.ApiControllers;
using BC_Exercise.Web.Controllers;
using BC_Exercise.Web.Models.BindingModels;
using BC_Exercise.Web.Models.ViewModels;
using Raven.Client;
using Raven.Client.Embedded;
using Moq;

namespace BC_Exercise.Web.Tests.ApiControllers
{
    [TestClass]
    public class UserApiControllerTest
    {
        private void DBSetup()
        {
            var _store = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };

            _store.Initialize();

            DataDocumentStore.Instance = _store;
        }

        [TestMethod]
        public void GetUser()
        {
            // Arrange
            DBSetup();

            var userModel = new User()
            {
                Id = "users/1",
                FirstName = "TestFN",
                LastName = "TestLN",
                Email = "test@test.com",
                StreetAddress = "TestSA",
                City = "TestCity",
                State = "TestState",
                Zip = "99999"
            };

            Mock<IUserService> userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUser(It.IsAny<IDocumentSession>(), It.IsAny<string>())).Returns(userModel);
            
            UserApiController controller = new UserApiController(userService.Object);

            // Act
            IHttpActionResult result = controller.GetUser("1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(OkNegotiatedContentResult<User>), result.GetType());
        }

        [TestMethod]
        public void GetUserFail()
        {
            // Arrange
            DBSetup();

            Mock<IUserService> userService = new Mock<IUserService>();
            userService.Setup(x => x.GetUser(It.IsAny<IDocumentSession>(), It.IsAny<string>())).Returns<User>(null);

            UserApiController controller = new UserApiController(userService.Object);

            // Act
            IHttpActionResult result = controller.GetUser("1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual((result as BadRequestErrorMessageResult).Message, "User not found!");
        }

        [TestMethod]
        public void GetAllUsers()
        {
            // Arrange
            DBSetup();

            var userModel1 = new User()
            {
                Id = "users/1",
                FirstName = "TestFN",
                LastName = "TestLN",
                Email = "test@test.com",
                StreetAddress = "TestSA",
                City = "TestCity",
                State = "TestState",
                Zip = "99999"
            };

            var userModel2 = new User()
            {
                Id = "users/2",
                FirstName = "TestFN",
                LastName = "TestLN",
                Email = "test@test.com",
                StreetAddress = "TestSA",
                City = "TestCity",
                State = "TestState",
                Zip = "99999"
            };

            Mock<IUserService> userService = new Mock<IUserService>();
            userService.Setup(x => x.GetAllUsers(It.IsAny<IDocumentSession>())).Returns(new List<User>() { userModel1, userModel2 });

            UserApiController controller = new UserApiController(userService.Object);

            // Act
            IHttpActionResult result = controller.GetAllUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(OkNegotiatedContentResult<List<User>>), result.GetType());
        }

        [TestMethod]
        public void AddUser()
        {
            // Arrange
            DBSetup();

            Mock<IUserService> userService = new Mock<IUserService>();
            userService.Setup(x => x.AddUser(It.IsAny<IDocumentSession>(), It.IsAny<User>())).Returns("users/1");

            UserApiController controller = new UserApiController(userService.Object);

            // Act
            var bindingModel = new AddUserBindingModel()
            {
                FirstName = "TestFN",
                LastName = "TestLN",
                Email = "test@test.com",
                StreetAddress = "TestSA",
                City = "TestCity",
                State = "TestState",
                Zip = "99999"
            };

            IHttpActionResult result = controller.AddUser(bindingModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }

        [TestMethod]
        public void UpdateUser()
        {
            // Arrange
            DBSetup();

            var userModel = new User()
            {
                Id = "users/1",
                FirstName = "TestFN",
                LastName = "TestLN",
                Email = "test@test.com",
                StreetAddress = "ChangeTestSA",
                City = "TestCity",
                State = "TestState",
                Zip = "99999"
            };

            Mock<IUserService> userService = new Mock<IUserService>();
            userService.Setup(x => x.UpdateUser(It.IsAny<IDocumentSession>(), It.IsAny<User>())).Returns(userModel);

            UserApiController controller = new UserApiController(userService.Object);

            // Act
            var userViewModel = new UserViewModel()
            {
                Id = "users/1",
                FirstName = "TestFN",
                LastName = "TestLN",
                Email = "test@test.com",
                StreetAddress = "ChangeTestSA",
                City = "TestCity",
                State = "TestState",
                Zip = "99999"
            };

            IHttpActionResult result = controller.UpdateUser(userViewModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(OkNegotiatedContentResult<User>), result.GetType());
        }

        [TestMethod]
        public void UpdateUserFail()
        {
            // Arrange
            DBSetup();

            Mock<IUserService> userService = new Mock<IUserService>();
            userService.Setup(x => x.UpdateUser(It.IsAny<IDocumentSession>(), It.IsAny<User>())).Returns<User>(null);

            UserApiController controller = new UserApiController(userService.Object);

            // Act
            var userViewModel = new UserViewModel()
            {
                Id = "users/1",
                FirstName = "TestFN",
                LastName = "TestLN",
                Email = "test@test.com",
                StreetAddress = "ChangeTestSA",
                City = "TestCity",
                State = "TestState",
                Zip = "99999"
            };

            IHttpActionResult result = controller.UpdateUser(userViewModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual((result as BadRequestErrorMessageResult).Message, "User not found!");
        }

        [TestMethod]
        public void DeleteUser()
        {
            // Arrange
            DBSetup();
            
            Mock<IUserService> userService = new Mock<IUserService>();
            userService.Setup(x => x.DeleteUser(It.IsAny<IDocumentSession>(), It.IsAny<string>())).Returns("users/1");

            UserApiController controller = new UserApiController(userService.Object);

            // Act
            IHttpActionResult result = controller.DeleteUser("1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }

        [TestMethod]
        public void DeleteUserFail()
        {
            // Arrange
            DBSetup();

            Mock<IUserService> userService = new Mock<IUserService>();
            userService.Setup(x => x.UpdateUser(It.IsAny<IDocumentSession>(), It.IsAny<User>())).Returns<User>(null);

            UserApiController controller = new UserApiController(userService.Object);

            // Act
            IHttpActionResult result = controller.DeleteUser("1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual((result as BadRequestErrorMessageResult).Message, "User not found!");
        }
    }
}
