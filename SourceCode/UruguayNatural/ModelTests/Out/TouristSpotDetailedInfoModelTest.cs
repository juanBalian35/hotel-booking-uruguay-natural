using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace ModelTests.Out
{
    [TestClass]
    public class TouristSpotDetailedInfoModelTest
    {
        private TouristSpotDetailedInfoModel TouristSpotDetailedInfoModel;
        private TouristSpot TouristSpot;

        [TestInitialize]
        public void TestInitialize()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "category 1"
            };

           
            var tcs = new List<TouristSpotCategory>(){new TouristSpotCategory(){CategoryId = category.Id, Category = category}};
            TouristSpot = new TouristSpot()
            {
                Id = 1,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                RegionId = 1,
                TouristSpotCategories = tcs
            };
            TouristSpotDetailedInfoModel = new TouristSpotDetailedInfoModel(TouristSpot);
        }

        [TestMethod]
        public void ConstructorCreatesTouristSpotBasicInfoModelWithSameId()
        {
            Assert.AreEqual(TouristSpot.Id, TouristSpotDetailedInfoModel.Id);
        }

        [TestMethod]
        public void ConstructorCreatesTouristSpotBasicInfoModelWithSameName()
        {
            Assert.AreEqual(TouristSpot.Name, TouristSpotDetailedInfoModel.Name);
        }
        
        [TestMethod]
        public void ConstructorCreatesTouristSpotBasicInfoModelWithSameDescription()
        {
            Assert.AreEqual(TouristSpot.Description, TouristSpotDetailedInfoModel.Description);
        }
        
        [TestMethod]
        public void ConstructorCreatesTouristSpotBasicInfoModelWithSameRegionId()
        {
            Assert.AreEqual(TouristSpot.RegionId, TouristSpotDetailedInfoModel.RegionId);
        }
        
        [TestMethod]
        public void ConstructorCreatesTouristSpotBasicInfoModelWithSameCategories()
        {
            Assert.IsTrue(TouristSpot.TouristSpotCategories
                .All(x => TouristSpotDetailedInfoModel.Categories
                    .Any(y => y.Id == x.CategoryId)));
        }
        
        [TestMethod]
        public void EqualsAnotherWithSameIdAndName()
        {
            var anotherTouristSpotModel = new TouristSpotDetailedInfoModel(TouristSpot);

            Assert.IsTrue(TouristSpotDetailedInfoModel.Equals(anotherTouristSpotModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentId()
        {
            var anotherTouristSpot = TouristSpot;
            anotherTouristSpot.Id = 2;
            var anotherTouristSpotModel = new TouristSpotDetailedInfoModel(anotherTouristSpot);

            Assert.IsFalse(TouristSpotDetailedInfoModel.Equals(anotherTouristSpotModel));
        }

        [TestMethod]
        public void DoesNotEqualOtherType()
        {
            Assert.IsFalse(TouristSpotDetailedInfoModel.Equals("String"));
        }
    }
}
