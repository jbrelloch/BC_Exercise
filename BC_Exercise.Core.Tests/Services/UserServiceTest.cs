using System.Collections.Generic;
using BC_Exercise.Core.Services;
using BC_Exercise.Models.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client;
using Raven.Client.Embedded;

namespace BC_Exercise.Core.Tests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private IDocumentSession DBSetup()
        {
            var _store = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };

            _store.Initialize();

            return _store.OpenSession();
        }

        [TestMethod]
        public void GetUser()
        {
            // Arrange
            ExternalService exService = new ExternalService();
            UserService service = new UserService(exService);

            using (var _session = DBSetup())
            {
                var userModel = new User()
                {
                    FirstName = "TestFN",
                    LastName = "TestLN",
                    Email = "test@test.com",
                    StreetAddress = "TestSA",
                    City = "TestCity",
                    State = "TestState",
                    Zip = "99999"
                };
                _session.Store(userModel);
                _session.SaveChanges();
                
                // Act
                User result = service.GetUser(_session, "users/1");

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Id, "users/1");
                Assert.AreEqual(userModel.Id, "users/1");
            }
        }

        [TestMethod]
        public void GetUserFail()
        {
            // Arrange
            ExternalService exService = new ExternalService();
            UserService service = new UserService(exService);

            using (var _session = DBSetup())
            {
                // Act
                User result = service.GetUser(_session, "users/1");

                // Assert
                Assert.IsNull(result);
            }
        }
        
        [TestMethod]
        public void GetAllUsers()
        {
            // Arrange
            ExternalService exService = new ExternalService();
            UserService service = new UserService(exService);

            using (var _session = DBSetup())
            {
                var userModel1 = new User()
                {
                    FirstName = "TestFN1",
                    LastName = "TestLN1",
                    Email = "test1@test1.com",
                    StreetAddress = "TestSA1",
                    City = "TestCity1",
                    State = "TestState1",
                    Zip = "99998"
                };
                _session.Store(userModel1);

                var userModel2 = new User()
                {
                    FirstName = "TestFN2",
                    LastName = "TestLN2",
                    Email = "test2@test2.com",
                    StreetAddress = "TestSA2",
                    City = "TestCity2",
                    State = "TestState2",
                    Zip = "99999"
                };
                _session.Store(userModel2);
                _session.SaveChanges();

                // Act
                List<User> result = service.GetAllUsers(_session);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Count, 2);
            }
        }

        [TestMethod]
        public void AddUser()
        {
            // Arrange
            ExternalService exService = new ExternalService();
            UserService service = new UserService(exService);

            using (var _session = DBSetup())
            {
                var userModel = new User()
                {
                    FirstName = "TestFN",
                    LastName = "TestLN",
                    Email = "test@test.com",
                    StreetAddress = "TestSA",
                    City = "TestCity",
                    State = "TestState",
                    Zip = "99999"
                };

                // Act
                string result = service.AddUser(_session, userModel);

                // Assert
                Assert.IsNotNull(result);

                var user = _session.Load<User>(result);

                Assert.IsNotNull(user);
                Assert.IsNotNull(user.Id);
            }
        }

        [TestMethod]
        public void UpdateUser()
        {
            // Arrange
            ExternalService exService = new ExternalService();
            UserService service = new UserService(exService);

            using (var _session = DBSetup())
            {
                var userModel = new User()
                {
                    FirstName = "TestFN",
                    LastName = "TestLN",
                    Email = "test@test.com",
                    StreetAddress = "TestSA",
                    City = "TestCity",
                    State = "TestState",
                    Zip = "99999"
                };
                _session.Store(userModel);
                _session.SaveChanges();

                var changeUserModel = new User()
                {
                    Id = userModel.Id,
                    FirstName = "TestFN",
                    LastName = "TestLN",
                    Email = "test@test.com",
                    StreetAddress = "NEWTestSA",
                    City = "TestCity",
                    State = "TestState",
                    Zip = "99999"
                };

                // Act
                User result = service.UpdateUser(_session, changeUserModel);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Id, "users/1");
                Assert.AreEqual(result.StreetAddress, "NEWTestSA");
            }
        }

        [TestMethod]
        public void UpdateUserFail()
        {
            // Arrange
            ExternalService exService = new ExternalService();
            UserService service = new UserService(exService);

            using (var _session = DBSetup())
            {
                var changeUserModel = new User()
                {
                    Id = "users/1",
                    FirstName = "TestFN",
                    LastName = "TestLN",
                    Email = "test@test.com",
                    StreetAddress = "NEWTestSA",
                    City = "TestCity",
                    State = "TestState",
                    Zip = "99999"
                };

                // Act
                User result = service.UpdateUser(_session, changeUserModel);

                // Assert
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void DeleteUser()
        {
            // Arrange
            ExternalService exService = new ExternalService();
            UserService service = new UserService(exService);

            using (var _session = DBSetup())
            {
                var userModel = new User()
                {
                    FirstName = "TestFN",
                    LastName = "TestLN",
                    Email = "test@test.com",
                    StreetAddress = "TestSA",
                    City = "TestCity",
                    State = "TestState",
                    Zip = "99999"
                };
                _session.Store(userModel);
                _session.SaveChanges();

                // Act
                string result = service.DeleteUser(_session, "users/1");

                // Assert
                Assert.IsNotNull(result);

                var user = _session.Load<User>(result);

                Assert.IsNull(user);
            }
        }

        [TestMethod]
        public void DeleteUserFail()
        {
            // Arrange
            ExternalService exService = new ExternalService();
            UserService service = new UserService(exService);

            using (var _session = DBSetup())
            {
                // Act
                string result = service.DeleteUser(_session, "users/1");

                // Assert
                Assert.IsNull(result);
            }
        }
    }
}
