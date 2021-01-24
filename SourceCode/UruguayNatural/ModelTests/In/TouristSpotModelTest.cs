using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;
using Moq;

namespace ModelTests.In
{
    [TestClass]
    public class TouristSpotModelTest
    {
        private TouristSpotModel TouristSpotModel;

        [TestInitialize]
        public void TestInitialize()
        {
            var fileMock = new Mock<IFormFile>();
            TouristSpotModel = new TouristSpotModel()
            {
                Name = "name",
                Description = "description",
                Image = fileMock.Object,
                Categories = new List<int>() {1},
                RegionId = 1
            };
        }

        [TestMethod]
        public void ToEntityCreatesTouristSpotWithSameName()
        {
            var touristSpot = TouristSpotModel.ToEntity();
            Assert.AreEqual(TouristSpotModel.Name, touristSpot.Name);
        }
    
        [TestMethod]
        public void ToEntityCreatesTouristSpotWithSameDescription()
        {
            var touristSpot = TouristSpotModel.ToEntity();
            Assert.AreEqual(TouristSpotModel.Description, touristSpot.Description);
        }
        
        [TestMethod]
        public void ToEntityCreatesTouristSpotWithImage()
        {
            var touristSpot = TouristSpotModel.ToEntity();
            Assert.IsNotNull(touristSpot.Image);
        }
        
        [TestMethod]
        public void ToEntityCreatesTouristSpotWithRegion()
        {
            var touristSpot = TouristSpotModel.ToEntity();
            Assert.AreEqual(TouristSpotModel.RegionId, touristSpot.RegionId);
        }
        
        [TestMethod]
        public void ToEntityCreatesTouristSpotWithCategories()
        {
            var touristSpot = TouristSpotModel.ToEntity();
            Assert.IsTrue(touristSpot.TouristSpotCategories
                .All(x => TouristSpotModel.Categories.Contains(x.CategoryId)));
        }

        [TestMethod]
        public void IsValidReturnsTrueIfTouristSpotIsValid()
        {
            Assert.IsFalse(TouristSpotModel.HasErrors());
        }

        [TestMethod]
        public void IsValidReturnsFalseIfTouristSpotIsInvalid()
        {
            TouristSpotModel.Name = "";
            Assert.IsTrue(TouristSpotModel.HasErrors());
        }

        [TestMethod]
        public void ToEntityWithNullRegionCreatesTouristSpotWithNullRegion()
        {
            TouristSpotModel.RegionId = null;
            var touristSpot = TouristSpotModel.ToEntity();
            Assert.AreEqual(TouristSpotModel.RegionId, touristSpot.Region);

        }
    }
}
