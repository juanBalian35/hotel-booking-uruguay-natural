using Domain;
using System.Collections.Generic;
using Model.Out;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelTests.Out
{
    [TestClass]
    public class LodgingSearchBasicInfoModelTest
    {
        private LodgingSearchBasicInfoModel LodgingSearchBasicInfoModel;
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
                TouristSpot = touristSpot,
                ReviewAverage = 5,
                ReviewsQuantity = 1
            };
            LodgingSearchBasicInfoModel = new LodgingSearchBasicInfoModel(Lodging);
        }

        [TestMethod]
        public void ConstructorSetsModelName()
        {
            Assert.AreEqual(Lodging.Name, LodgingSearchBasicInfoModel.Name);
        }
        
        [TestMethod]
        public void ConstructorSetsModelRating()
        {
            Assert.AreEqual(Lodging.Rating, LodgingSearchBasicInfoModel.Rating);
        }

        [TestMethod]
        public void ConstructorSetsModelAddress()
        {
            Assert.AreEqual(Lodging.Address, LodgingSearchBasicInfoModel.Address);
        }
        
        [TestMethod]
        public void ConstructorSetsModelDescription()
        {
            Assert.AreEqual(Lodging.Description, LodgingSearchBasicInfoModel.Description);
        }
        
        [TestMethod]
        public void ConstructorSetsModelImages()
        {
            Assert.AreEqual(Lodging.Images.Count, LodgingSearchBasicInfoModel.Images.Count);
        }
        
        [TestMethod]
        public void ConstructorSetsModelPricePerNight()
        {
            Assert.AreEqual(Lodging.PricePerNight, LodgingSearchBasicInfoModel.PricePerNight);
        }
        
        [TestMethod]
        public void ConstructorSetsModelTotalPrice()
        {
            Assert.AreEqual(Lodging.TotalPrice, LodgingSearchBasicInfoModel.TotalPrice);
        }
        
        [TestMethod]
        public void ConstructorSetsModelReviewsAverage()
        {
            Assert.AreEqual(Lodging.ReviewAverage, LodgingSearchBasicInfoModel.ReviewsAverage);
        }
       
        [TestMethod]
        public void ConstructorSetsModelReviewsQuantity()
        {
            Assert.AreEqual(Lodging.ReviewsQuantity, LodgingSearchBasicInfoModel.ReviewsQuantity);
        }

        [TestMethod]
        public void EqualsIsTrueIfAddressesAreTheSame()
        {
            var other = new LodgingSearchBasicInfoModel(Lodging);
            Assert.IsTrue(other.Equals(LodgingSearchBasicInfoModel));
        }

        [TestMethod]
        public void EqualsIsFalseIfAddressesAreNotTheSame()
        {
            var other = new LodgingSearchBasicInfoModel(Lodging);
            other.Address = "anotheraddress";
            Assert.IsFalse(other.Equals(LodgingSearchBasicInfoModel));
        }

        [TestMethod]
        public void EqualsIsFalseWithAnotherType()
        {
            Assert.IsFalse(LodgingSearchBasicInfoModel.Equals("string"));
        }
    }
}
