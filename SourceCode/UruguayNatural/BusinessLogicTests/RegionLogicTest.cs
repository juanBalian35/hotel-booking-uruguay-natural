using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessInterface;
using Moq;
using Domain;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic;


namespace BusinessLogicTests
{
    [TestClass]
    public class RegionLogicTest
    {
        private RegionLogic RegionLogic;
        private Mock<IRepository<Region>> RegionsMock;
        private Region Region;

        [TestInitialize]
        public void TestInitialize()
        { 
            RegionsMock = new Mock<IRepository<Region>>(MockBehavior.Strict);

            Region = new Region
            {
                Id = 1,
                Name = "region 1",
                Description = "aDescription",
                VideoPath = "aVideoPath",
                MapYellow = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                MapTransparent = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20}
            };
        }

        private RegionLogic CreateRegionLogic()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            unitOfWorkMock.Setup(m => m.GetRegionRepository()).Returns(RegionsMock.Object);
            return new RegionLogic(unitOfWorkMock.Object);
        }
        
        [TestMethod]
        public void GetAllRegionsReturnsRepositoryValues()
        {
            var regions = new List<Region> {Region};
            RegionsMock.Setup(m => m.GetAll(null,"")).Returns(regions);

            RegionLogic = CreateRegionLogic();

            Assert.IsTrue(RegionLogic.GetAll().SequenceEqual(regions));
            RegionsMock.VerifyAll(); 
        }
        
    }
}