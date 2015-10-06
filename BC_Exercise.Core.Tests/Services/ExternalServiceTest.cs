using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Exercise.Core.Services;
using BC_Exercise.Models.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BC_Exercise.Core.Tests.Services
{
    [TestClass]
    public class ExternalServiceTest
    {
        [TestMethod]
        public void GetAddressStrFromObject()
        {
            // Arrange
            ExternalService service = new ExternalService();
            
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
            var result = service.GetAddressStrFromObject(userModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "TestSA, TestCity, TestState 99999");
        }

        [TestMethod]
        public void GetCoordinatesFromAddress()
        {
            // Arrange
            ExternalService service = new ExternalService();

            var addrStr = "3405 Piedmont Rd NE, Atlanta, GA 30305";

            // Act
            var result = service.GetCoordinatesFromAddress(addrStr);

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(result, "TestSA, TestCity, TestState 99999");
        }
    }
}
