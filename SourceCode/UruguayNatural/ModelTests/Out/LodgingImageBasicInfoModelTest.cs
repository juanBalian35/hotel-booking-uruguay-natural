using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;

namespace ModelTests.Out
{
    [TestClass]
    public class LodgingImageBasicInfoModelTest
    {
        private LodgingImageBasicInfoModel LodgingImageBasicInfoModel;
        private LodgingImage LodgingImage;

        [TestInitialize]
        public void TestInitialize()
        {
            var touristSpot = new TouristSpot
            {
                Id = 1,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                Region = null,
                TouristSpotCategories = new List<TouristSpotCategory>()
            };

            var lodging = new Lodging
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

            LodgingImage = new LodgingImage
            {
                Id = 1,
                LodgingId = lodging.Id,
                Lodging = lodging,
                ImageData = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20}
            };
            LodgingImageBasicInfoModel = new LodgingImageBasicInfoModel(LodgingImage);
        }

        [TestMethod]
        public void ConstructorCreatesLodgingImageBasicInfoModelWithSameId()
        {
            Assert.AreEqual(LodgingImage.Id, LodgingImageBasicInfoModel.Id);
        }

        [TestMethod]
        public void ConstructorCreatesLodgingImageBasicInfoModelWithSameImageData()
        {
            Assert.IsTrue(LodgingImage.ImageData.SequenceEqual(LodgingImageBasicInfoModel.ImageData));
        }

        [TestMethod]
        public void EqualsAnotherWithSameIdAndName()
        {
            var anotherModel = new LodgingImageBasicInfoModel(LodgingImage);

            Assert.IsTrue(LodgingImageBasicInfoModel.Equals(anotherModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentId()
        {
            var anotherLodgingImage = LodgingImage;
            anotherLodgingImage.Id = 3;
            
            var anotherModel = new LodgingImageBasicInfoModel(anotherLodgingImage);

            Assert.IsFalse(LodgingImageBasicInfoModel.Equals(anotherModel));
        }

        [TestMethod]
        public void DoesNotEqualOtherType()
        {
            Assert.IsFalse(LodgingImageBasicInfoModel.Equals("string"));
        }
    }
}