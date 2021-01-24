using Domain;
using System.Collections.Generic;
using Model.Out;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests.Out
{
    [TestClass]
    public class LodgingModifiedModelTest
    {
        private LodgingModifiedModel LodgingModifiedModel;
        private Lodging Lodging;

        [TestInitialize]
        public void TestInitialize()
        {
            var region = new Region()
            {
                Id = 1,
                Name = "RegionName"
            };

            var touristSpot = new TouristSpot()
            {
                Id = 1,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                Region = region,
                TouristSpotCategories = new List<TouristSpotCategory>()
            };
            
            var lodgingImage = new LodgingImage
            {
                Id = 1,
                LodgingId = 1,
                ImageData = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20}
            };
            
            Lodging = new Lodging()
            {
                Id = 1,
                Name = "Name",
                Description = "Description",
                Rating = 3,
                IsFull = true,
                Images = new List<LodgingImage>() { lodgingImage },
                PricePerNight = 100,
                Address = "Valid Address 123",
                Phone = "+598 98 303 040",
                ConfirmationMessage = "Your reservation has been confirmed!",
                TouristSpot = touristSpot
            };
            LodgingModifiedModel = new LodgingModifiedModel(Lodging);
        }

        [TestMethod]
        public void ConstructorSetsModelName()
        {
            Assert.AreEqual(Lodging.Name, LodgingModifiedModel.Name);
        }

        [TestMethod]
        public void ConstructorSetsModelIsFull()
        {
            Assert.AreEqual(Lodging.IsFull, LodgingModifiedModel.IsFull);
        }
        

        [TestMethod]
        public void ConstructorSetsModelAddress()
        {
            Assert.AreEqual(Lodging.Address, LodgingModifiedModel.Address);
        }

        [TestMethod]
        public void ConstructorSetsModelTouristSpot()
        {
            Assert.AreEqual(Lodging.TouristSpot.Id, LodgingModifiedModel.TouristSpot.Id);
        }

        [TestMethod]
        public void EqualsIsTrueIfAddressesAreTheSame()
        {
            var other = new LodgingModifiedModel(Lodging);
            Assert.IsTrue(other.Equals(LodgingModifiedModel));
        }

        [TestMethod]
        public void EqualsIsFalseIfAddressesAreNotTheSame()
        {
            var other = new LodgingModifiedModel(Lodging);
            other.Address = "anotheraddress";
            Assert.IsFalse(other.Equals(LodgingModifiedModel));
        }

        [TestMethod]
        public void EqualsIsFalseWithAnotherType()
        {
            Assert.IsFalse(LodgingModifiedModel.Equals("string"));
        }
    }
}
