using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;

namespace ModelTests.In
{
    [TestClass]
    public class LogInModelTest
    {
        [TestMethod]
        public void SetEmailSetsEmail()
        {
            const string EMAIL = "email@mail.com";

            var logInModel = new LogInModel();
            logInModel.Email = EMAIL;

            Assert.AreEqual(logInModel.Email, EMAIL);
        }

        [TestMethod]
        public void SetPasswordSetsPassword()
        {
            const string PASSWORD = "123123";

            var logInModel = new LogInModel();
            logInModel.Password = PASSWORD;

            Assert.AreEqual(logInModel.Password, PASSWORD);
        }
    }
}
