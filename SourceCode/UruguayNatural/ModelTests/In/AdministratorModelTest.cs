using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;

namespace ModelTests.In
{
    [TestClass]
    public class AdministratorTest
    {
        private AdministratorModel AdministratorModel;

        [TestInitialize]
        public void TestInitialize()
        {
            AdministratorModel = new AdministratorModel()
            {
                Name = "name",
                Email = "email@mail.com",
                Password = "changeme"
            };
        }

        [TestMethod]
        public void ToEntityCreatesAdministratorWithSameName()
        {
            var administrator = AdministratorModel.ToEntity();
            Assert.AreEqual(AdministratorModel.Name, administrator.Name);
        }

        [TestMethod]
        public void ToEntityCreatesAdministratorWithSameEmail()
        {
            var administrator = AdministratorModel.ToEntity();
            Assert.AreEqual(AdministratorModel.Email, administrator.Email);
        }

        [TestMethod]
        public void ToEntityCreatesAdministratorWithSamePassword()
        {
            var administrator = AdministratorModel.ToEntity();
            Assert.AreEqual(AdministratorModel.Password, administrator.Password);
        }

        [TestMethod]
        public void IsValidReturnsTrueIfAdministratorIsValid()
        {
            Assert.IsFalse(AdministratorModel.HasErrors());
        }

        [TestMethod]
        public void IsValidReturnsFalseIfAdministratorIsInvalid()
        {
            AdministratorModel.Email = "invalid email";
            Assert.IsTrue(AdministratorModel.HasErrors());
        }
    }
}
