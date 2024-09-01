using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Andreed_IP11;
using Andreed_IP11.ViewModel;
using Andreed_IP11.Model;
using Andreed_IP11.View;
namespace TestAndreev
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AuthMethodExistingUser_TrueReturned()
        {
            string login = "admin";
            string password = "admin";
            bool result = UserVM.CheckAuth(login, password);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void AuthMethodNotExistingUser_Exception()
        {
            string login = "123";
            string password = "321";
            Assert.ThrowsException<Exception>(() => UserVM.CheckAuth(login, password));
        }
        [TestMethod]
        public void AuthMethodOnlyLogin_Exception()
        {
            string login = "admin";
            string password = "";
            Assert.ThrowsException<Exception>(() => UserVM.CheckAuth(login, password));
        }
        [TestMethod]
        public void AuthMethodOnlyPassword_Exception()
        {
            string login = "";
            string password = "admin";
            Assert.ThrowsException<Exception>(() => UserVM.CheckAuth(login, password));
        }
        [TestMethod]
        public void AuthMethodSpaceCheck_Exception()
        {
            string login = "admin";
            string password = " admin";
            Assert.ThrowsException<Exception>(() => UserVM.CheckAuth(login, password));
        }
        [TestMethod]
        public void AuthMethodSpaceCheck2_Exception()
        {
            string login = " admin";
            string password = "admin";
            Assert.ThrowsException<Exception>(() => UserVM.CheckAuth(login, password));
        }
    }
}
