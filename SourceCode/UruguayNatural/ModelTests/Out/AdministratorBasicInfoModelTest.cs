using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;
using Domain;

namespace ModelTests.Out
{
    [TestClass]
    public class AdministratorBasicInfoModelTest
    {
        private AdministratorBasicInfoModel AdministratorBasicInfoModel;
        private Administrator Administrator;

        [TestInitialize]
        public void TestInitialize()
        {
            Administrator = new Administrator()
            {
                Id = 1,
                Name = "name",
                Email = "email@mail.com",
                Password = "changeme"
            };
            AdministratorBasicInfoModel = new AdministratorBasicInfoModel(Administrator);
        }

        [TestMethod]
        public void ConstructorCreatesAdministratorBasicInfoModelWithSameId()
        {
            Assert.AreEqual(Administrator.Id, AdministratorBasicInfoModel.Id);
        }

        [TestMethod]
        public void ConstructorCreatesAdministratorBasicInfoModelWithSameName()
        {
            Assert.AreEqual(Administrator.Name, AdministratorBasicInfoModel.Name);
        }

        [TestMethod]
        public void ConstructorCreatesAdministratorBasicInfoModelWithSameEmail()
        {
            Assert.AreEqual(Administrator.Email, AdministratorBasicInfoModel.Email);
        }

        [TestMethod]
        public void EqualsAnotherWithSameIdAndEmail()
        {
            var anotherAdministratorModel = new AdministratorBasicInfoModel(Administrator);

            Assert.IsTrue(AdministratorBasicInfoModel.Equals(anotherAdministratorModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentId()
        {
            var anotherAdministrator = Administrator;
            anotherAdministrator.Id = 2;
            var anotherAdministratorModel = new AdministratorBasicInfoModel(anotherAdministrator);

            Assert.IsFalse(AdministratorBasicInfoModel.Equals(anotherAdministratorModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentEmail()
        {
            var anotherAdministrator = Administrator;
            anotherAdministrator.Email = "different@email.com";
            var anotherAdministratorModel = new AdministratorBasicInfoModel(anotherAdministrator);

            Assert.IsFalse(AdministratorBasicInfoModel.Equals(anotherAdministratorModel));
        }

        [TestMethod]
        public void DoesNotEqualOtherType()
        {
            Assert.IsFalse(AdministratorBasicInfoModel.Equals("String"));
        }
    }
}
