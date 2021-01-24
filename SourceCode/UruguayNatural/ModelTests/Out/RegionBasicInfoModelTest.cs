using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;
using Domain;

namespace ModelTests.Out
{
    [TestClass]
    public class RegionBasicInfoModelTest
    {
        private RegionBasicInfoModel RegionBasicInfoModel;
        private Region Region;

        [TestInitialize]
        public void TestInitialize()
        {
            Region = new Region()
            {
                Id = 1,
                Name = "Region1"
            };
            RegionBasicInfoModel = new RegionBasicInfoModel(Region);
        }

        [TestMethod]
        public void ConstructorCreatesRegionBasicInfoModelWithSameId()
        {
            Assert.AreEqual(Region.Id, RegionBasicInfoModel.Id);
        }

        [TestMethod]
        public void ConstructorCreatesRegionBasicInfoModelWithSameName()
        {
            Assert.AreEqual(Region.Name, RegionBasicInfoModel.Name);
        }
        
        [TestMethod]
        public void EqualsAnotherWithSameIdAndName()
        {
            var anotherRegionModel = new RegionBasicInfoModel(Region);

            Assert.IsTrue(RegionBasicInfoModel.Equals(anotherRegionModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentId()
        {
            var anotherRegion = Region;
            anotherRegion.Id = 2;
            var anotherRegionModel = new RegionBasicInfoModel(anotherRegion);

            Assert.IsFalse(RegionBasicInfoModel.Equals(anotherRegionModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentName()
        {
            var anotherRegion = Region;
            anotherRegion.Name = "differentName";
            var anotherRegionModel = new RegionBasicInfoModel(anotherRegion);

            Assert.IsFalse(RegionBasicInfoModel.Equals(anotherRegionModel));
        }

        [TestMethod]
        public void DoesNotEqualOtherType()
        {
            Assert.IsFalse(RegionBasicInfoModel.Equals("String"));
        }
    }
}
