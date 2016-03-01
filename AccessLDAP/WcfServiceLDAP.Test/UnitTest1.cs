using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfServiceLDAP.Test.ServiceReferenceLDAP;

namespace WcfServiceLDAP.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateUser()
        {
             SSOClient client = new SSOClient();
            bool result = client.CreateUser("user", "123456", "esurmay@hotmail.com", true);
            Assert.IsNotNull(result, "Hello World");
        }

        [TestMethod]
        public void TestSignIn()
        {
            SSOClient client = new SSOClient();
            bool result = client.SignIn("user", "123456");
            Assert.IsTrue(result, "Custom Message: Error Authentication");
        }

       
        [TestMethod]
        public void TestIsAuthenticated()
        {
            SSOClient client = new SSOClient();
            bool result = client.IsAuthenticated("user");
            Assert.IsNotNull(result, "Custom Message: User not Authenticated");

        }

        [TestMethod]
        public void TestChangePassword()
        {
            SSOClient client = new SSOClient();
            bool result = client.ChangePassword("user", "123456", "nuevoPass");
            Assert.IsTrue(result, "successful");
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            SSOClient client = new SSOClient();
            bool result = client.UpdateUser("user", "udaptePassword","esurmay@gmail.com", true);
            Assert.IsTrue(result, "successful");
        }

        [TestMethod]
        public void TestSignOut()
        {
            SSOClient client = new SSOClient();
            bool result = client.SignOut("user");
            Assert.IsTrue(result, "Custom Message: Error Authentication");
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            SSOClient client = new SSOClient();
            bool result = client.DeleteUser("user");
            Assert.IsTrue(result, "Custom Message: Error Authentication");
        }
    }
}
