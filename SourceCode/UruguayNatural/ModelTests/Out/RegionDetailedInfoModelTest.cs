using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;
using Domain;

namespace ModelTests.Out
{
    [TestClass]
    public class RegionDetailedInfoModelTest
    {
        private RegionDetailedInfoModel RegionDetailedInfoModel;
        private Region Region;

        [TestInitialize]
        public void TestInitialize()
        {
            Region = new Region
            {
                Id = 1,
                Name = "region 1",
                Description = "aDescription",
                VideoPath = "aVideoPath",
                MapYellow = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                MapTransparent = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20}
            };
            RegionDetailedInfoModel = new RegionDetailedInfoModel(Region);
        }

        [TestMethod]
        public void ConstructorCreatesRegionDetailedInfoModelWithSameId()
        {
            Assert.AreEqual(Region.Id, RegionDetailedInfoModel.Id);
        }

        [TestMethod]
        public void ConstructorCreatesRegionDetailedInfoModelWithSameName()
        {
            Assert.AreEqual(Region.Name, RegionDetailedInfoModel.Name);
        }
        
        [TestMethod]
        public void ConstructorCreatesRegionDetailedInfoModelWithSameDescription()
        {
            Assert.AreEqual(Region.Description, RegionDetailedInfoModel.Description);
        }
        
        [TestMethod]
        public void ConstructorCreatesRegionDetailedInfoModelWithSameVideoPath()
        {
            Assert.AreEqual(Region.VideoPath, RegionDetailedInfoModel.VideoPath);
        }
        
        [TestMethod]
        public void ConstructorCreatesRegionDetailedInfoModelWithSameMapYellow()
        {
            Assert.AreEqual(Region.MapYellow, RegionDetailedInfoModel.MapYellow);
        }
        [TestMethod]
        public void ConstructorCreatesRegionDetailedInfoModelWithSameMapTransparent()
        {
            Assert.AreEqual(Region.MapTransparent, RegionDetailedInfoModel.MapTransparent);
        }
        
        [TestMethod]
        public void EqualsAnotherWithSameIdAndName()
        {
            var anotherRegionModel = new RegionDetailedInfoModel(Region);

            Assert.IsTrue(RegionDetailedInfoModel.Equals(anotherRegionModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentId()
        {
            var anotherRegion = Region;
            anotherRegion.Id = 2;
            var anotherRegionModel = new RegionDetailedInfoModel(anotherRegion);

            Assert.IsFalse(RegionDetailedInfoModel.Equals(anotherRegionModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentName()
        {
            var anotherRegion = Region;
            anotherRegion.Name = "differentName";
            var anotherRegionModel = new RegionDetailedInfoModel(anotherRegion);

            Assert.IsFalse(RegionDetailedInfoModel.Equals(anotherRegionModel));
        }

        [TestMethod]
        public void DoesNotEqualOtherType()
        {
            Assert.IsFalse(RegionDetailedInfoModel.Equals("String"));
        }
    }
}
