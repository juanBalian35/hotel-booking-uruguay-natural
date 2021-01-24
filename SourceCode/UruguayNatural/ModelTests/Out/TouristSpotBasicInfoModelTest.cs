using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace ModelTests.Out
{
    [TestClass]
    public class TouristSpotBasicInfoModelTest
    {
        private TouristSpotBasicInfoModel TouristSpotBasicInfoModel;
        private TouristSpot TouristSpot;

        [TestInitialize]
        public void TestInitialize()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "category 1"
            };

            var region = new Region()
            {
                Id = 1,
                Name = "region 1"
            };
            var tcs = new List<TouristSpotCategory>(){new TouristSpotCategory(){CategoryId = category.Id, Category = category}};
            TouristSpot = new TouristSpot()
            {
                Id = 1,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                Region = region,
                TouristSpotCategories = tcs
            };
            TouristSpotBasicInfoModel = new TouristSpotBasicInfoModel(TouristSpot);
        }

        [TestMethod]
        public void ConstructorCreatesTouristSpotBasicInfoModelWithSameId()
        {
            Assert.AreEqual(TouristSpot.Id, TouristSpotBasicInfoModel.Id);
        }

        [TestMethod]
        public void ConstructorCreatesTouristSpotBasicInfoModelWithSameName()
        {
            Assert.AreEqual(TouristSpot.Name, TouristSpotBasicInfoModel.Name);
        }
        
        
        [TestMethod]
        public void ConstructorCreatesTouristSpotBasicInfoModelWithSameCategories()
        {
            Assert.IsTrue(TouristSpot.TouristSpotCategories
                .All(x => TouristSpotBasicInfoModel.Categories
                    .Any(y => y.Id == x.CategoryId)));
        }
        
        [TestMethod]
        public void EqualsAnotherWithSameIdAndName()
        {
            var anotherTouristSpotModel = new TouristSpotBasicInfoModel(TouristSpot);

            Assert.IsTrue(TouristSpotBasicInfoModel.Equals(anotherTouristSpotModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentId()
        {
            var anotherTouristSpot = TouristSpot;
            anotherTouristSpot.Id = 2;
            var anotherTouristSpotModel = new TouristSpotBasicInfoModel(anotherTouristSpot);

            Assert.IsFalse(TouristSpotBasicInfoModel.Equals(anotherTouristSpotModel));
        }

        [TestMethod]
        public void DoesNotEqualOtherType()
        {
            Assert.IsFalse(TouristSpotBasicInfoModel.Equals("String"));
        }
    }
}
