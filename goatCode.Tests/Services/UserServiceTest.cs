using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using goatCode.Services;

namespace goatCode.Tests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange:
            const string email = "drasl@drasl.com";
            var uservice = new UserService();

            //Act:
            var res = uservice.DoesUserExist(email);
            //Assert:
            Assert.AreEqual(email, res);
        }
    }
}
