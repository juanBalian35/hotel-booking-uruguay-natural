using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Model.In;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ModelTests.In
{
    [TestClass]
    public class LodgingModelTest
    {
        private LodgingModel LodgingModel;

        [TestInitialize]
        public void TestInitialize()
        {
            var fileMock = new Mock<IFormFile>();
            LodgingModel = new LodgingModel()
            {
                Name = "Name",
                Description = "Description",
                Rating = 3,
                IsFull = true,
                Images = new List<IFormFile> { fileMock.Object },
                PricePerNight = 100,
                Address = "Valid Address 123",
                Phone = "+598 98 303 040",
                ConfirmationMessage = "Your reservation has been confirmed!",
                TouristSpot = 1
            };
        }

        [TestMethod]
        public void ToEntityCreatesLodgingWithSameNameAsModel()
        {
            var lodging = LodgingModel.ToEntity();
            Assert.AreEqual(lodging.Name, LodgingModel.Name);
        }

        [TestMethod]
        public void ToEntityCreatesLodgingWithSameDescriptionAsModel()
        {
            var lodging = LodgingModel.ToEntity();
            Assert.AreEqual(lodging.Description, LodgingModel.Description);
        }

        [TestMethod]
        public void ToEntityCreatesLodgingWithSameRatingAsModel()
        {
            var lodging = LodgingModel.ToEntity();
            Assert.AreEqual(lodging.Rating, LodgingModel.Rating);
        }

        [TestMethod]
        public void ToEntityCreatesLodgingWithSameIsFullAsModel()
        {
            var lodging = LodgingModel.ToEntity();
            Assert.AreEqual(lodging.IsFull, LodgingModel.IsFull);
        }

        [TestMethod]
        public void ToEntityCreatesLodgingWithSameImagesAsModel()
        {
            var lodging = LodgingModel.ToEntity();
            Assert.AreEqual(lodging.Images.Count, LodgingModel.Images.Count);
        }

        [TestMethod]
        public void ToEntityCreatesLodgingWithSamePricePerNightAsModel()
        {
            var lodging = LodgingModel.ToEntity();
            Assert.AreEqual(lodging.PricePerNight, LodgingModel.PricePerNight);
        }

        [TestMethod]
        public void ToEntityCreatesLodgingWithSameAddressAsModel()
        {
            var lodging = LodgingModel.ToEntity();
            Assert.AreEqual(lodging.Address, LodgingModel.Address);
        }

        [TestMethod]
        public void ToEntityCreatesLodgingWithSamePhoneAsModel()
        {
            var lodging = LodgingModel.ToEntity();
            Assert.AreEqual(lodging.Phone, LodgingModel.Phone);
        }

        [TestMethod]
        public void ToEntityCreatesLodgingWithSameConfirmationMessageAsModel()
        {
            var lodging = LodgingModel.ToEntity();
            Assert.AreEqual(lodging.ConfirmationMessage, LodgingModel.ConfirmationMessage);
        }

        [TestMethod]
        public void ToEntityCreatesLodgingWithSameTouristSpotAsModel()
        {
            var lodging = LodgingModel.ToEntity();
            Assert.AreEqual(lodging.TouristSpot.Id, LodgingModel.TouristSpot);
        }

        [TestMethod]
        public void IsValidReturnsTrueIfEntityIsNotValid()
        {
            var lodging = LodgingModel.ToEntity();
            Assert.AreEqual(lodging.Validate().HasErrors(), LodgingModel.HasErrors());
        }

        [TestMethod]
        public void IsValidReturnsFalseIfEntityIsNotValid()
        {
            LodgingModel.Rating = -3;
            var lodging = LodgingModel.ToEntity();
            Assert.AreEqual(lodging.Validate().HasErrors(), LodgingModel.HasErrors());
        }
    }
}
