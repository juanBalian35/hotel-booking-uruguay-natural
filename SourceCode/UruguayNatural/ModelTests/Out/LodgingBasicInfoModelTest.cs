using Domain;
using System.Collections.Generic;
using System.Linq;
using Model.Out;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests.Out
{
    [TestClass]
    public class LodgingBasicInfoModelTest
    {
        private LodgingBasicInfoModel LodgingBasicInfoModel;
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
            LodgingBasicInfoModel = new LodgingBasicInfoModel(Lodging);
        }

        [TestMethod]
        public void ConstructorSetsModelName()
        {
            Assert.AreEqual(Lodging.Name, LodgingBasicInfoModel.Name);
        }

        [TestMethod]
        public void ConstructorSetsModelDescription()
        {
            Assert.AreEqual(Lodging.Description, LodgingBasicInfoModel.Description);
        }

        [TestMethod]
        public void ConstructorSetsModelRating()
        {
            Assert.AreEqual(Lodging.Rating, LodgingBasicInfoModel.Rating);
        }

        [TestMethod]
        public void ConstructorSetsModelIsFull()
        {
            Assert.AreEqual(Lodging.IsFull, LodgingBasicInfoModel.IsFull);
        }

        [TestMethod]
        public void ConstructorSetsModelImages()
        {
            Assert.AreEqual(Lodging.Images.Count(), LodgingBasicInfoModel.Images.Count());
        }

        [TestMethod]
        public void ConstructorSetsModelPricePerNight()
        {
            Assert.AreEqual(Lodging.PricePerNight, LodgingBasicInfoModel.PricePerNight);
        }

        [TestMethod]
        public void ConstructorSetsModelAddress()
        {
            Assert.AreEqual(Lodging.Address, LodgingBasicInfoModel.Address);
        }

        [TestMethod]
        public void ConstructorSetsModelPhone()
        {
            Assert.AreEqual(Lodging.Phone, LodgingBasicInfoModel.Phone);
        }

        [TestMethod]
        public void ConstructorSetsModelConfirmationMessage()
        {
            Assert.AreEqual(Lodging.ConfirmationMessage, LodgingBasicInfoModel.ConfirmationMessage);
        }

        [TestMethod]
        public void ConstructorSetsModelTouristSpot()
        {
            Assert.AreEqual(Lodging.TouristSpot.Id, LodgingBasicInfoModel.TouristSpot.Id);
        }

        [TestMethod]
        public void EqualsIsTrueIfAddressesAreTheSame()
        {
            var other = new LodgingBasicInfoModel(Lodging);
            Assert.IsTrue(other.Equals(LodgingBasicInfoModel));
        }

        [TestMethod]
        public void EqualsIsFalseIfAddressesAreTheSame()
        {
            var other = new LodgingBasicInfoModel(Lodging);
            other.Address = "anotheraddress";
            Assert.IsFalse(other.Equals(LodgingBasicInfoModel));
        }

        [TestMethod]
        public void EqualsIsFalseWithAnotherType()
        {
            Assert.IsFalse(LodgingBasicInfoModel.Equals("string"));
        }
    }
}
