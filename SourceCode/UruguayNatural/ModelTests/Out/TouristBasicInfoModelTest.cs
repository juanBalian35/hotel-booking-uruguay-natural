using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;
using Domain;

namespace ModelTests.Out
{
    [TestClass]
    public class TouristBasicInfoModelTest
    {
        private TouristBasicInfoModel TouristBasicInfoModel;
        private Tourist Tourist;

        [TestInitialize]
        public void TestInitialize()
        {
            Tourist = new Tourist()
            {
                Id = 1,
                Name = "aName",
                LastName = "lastName",
                Email = "em@ail.com"
            };
            TouristBasicInfoModel = new TouristBasicInfoModel(Tourist);
        }

        [TestMethod]
        public void ConstructorCreatesTouristBasicInfoModelWithSameId()
        {
            Assert.AreEqual(Tourist.Id, TouristBasicInfoModel.Id);
        }

        [TestMethod]
        public void ConstructorCreatesTouristBasicInfoModelWithSameName()
        {
            Assert.AreEqual(Tourist.Name, TouristBasicInfoModel.Name);
        }
        
        [TestMethod]
        public void ConstructorCreatesTouristBasicInfoModelWithSameLastName()
        {
            Assert.AreEqual(Tourist.LastName, TouristBasicInfoModel.LastName);
        }
        
        [TestMethod]
        public void ConstructorCreatesTouristBasicInfoModelWithSameEMail()
        {
            Assert.AreEqual(Tourist.Email, TouristBasicInfoModel.Email);
        }
    }
}