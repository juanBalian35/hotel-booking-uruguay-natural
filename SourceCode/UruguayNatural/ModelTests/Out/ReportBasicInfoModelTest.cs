using Domain;
using System.Collections.Generic;
using Model.Out;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests.Out
{
    [TestClass]
    public class ReportBasicInfoModelTest
    {
        private ReportBasicInfoModel ReportBasicInfoModel;
        private Lodging Lodging;

        [TestInitialize]
        public void TestInitialize()
        {
            var touristSpot = new TouristSpot { Id = 1, Name = "name", Description = "description" };
            
            Lodging = new Lodging
            {
                Id = 1,
                Name = "Name",
                Description = "Description",
                Rating = 3,
                IsFull = true,
                PricePerNight = 100,
                Address = "Valid Address 123",
                Phone = "+598 98 303 040",
                ConfirmationMessage = "Your reservation has been confirmed!",
                TouristSpot = touristSpot
            };
            ReportBasicInfoModel = new ReportBasicInfoModel(Lodging);
        }

        [TestMethod]
        public void ConstructorsSetsModelId()
        {
            Assert.AreEqual(Lodging.Id, ReportBasicInfoModel.Id);
        }

        [TestMethod]
        public void ConstructorSetsModelName()
        {
            Assert.AreEqual(Lodging.Name, ReportBasicInfoModel.Name);
        }

        [TestMethod]
        public void ConstructorSetsModelAddress()
        {
            Assert.AreEqual(Lodging.Address, ReportBasicInfoModel.Address);
        }

        [TestMethod]
        public void EqualsIsTrueIfAddressesAreTheSame()
        {
            var other = new ReportBasicInfoModel(Lodging);
            Assert.IsTrue(other.Equals(ReportBasicInfoModel));
        }

        [TestMethod]
        public void EqualsIsFalseIfAddressesAreNotTheSame()
        {
            var other = new ReportBasicInfoModel(Lodging) { Address = "anotheraddress" };
            Assert.IsFalse(other.Equals(ReportBasicInfoModel));
        }

        [TestMethod]
        public void EqualsIsFalseWithAnotherType()
        {
            Assert.IsFalse(ReportBasicInfoModel.Equals("string"));
        }
    }
}
