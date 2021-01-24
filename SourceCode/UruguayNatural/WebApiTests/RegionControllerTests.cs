using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using Model.In;
using Model.Out;
using BusinessLogicInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace WebApiTests
{
    [TestClass]
    public class RegionControllerTest
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
            
        }

        [TestMethod]
        public void GetRegionsReturnsValuesFromLogic()
        {
            var regionsToReturn = new List<Region>
            {
                Region
            };
            
            var regionLogicMock = new Mock<IRegionLogic>(MockBehavior.Strict);
            regionLogicMock.Setup(m => m.GetAll()).Returns(new List<Region>(){Region});
            var regionController = new RegionController(regionLogicMock.Object);

            var result = regionController.GetAll() as OkObjectResult;
            var content = result.Value as List<RegionDetailedInfoModel>;

            regionLogicMock.VerifyAll();
            Assert.IsTrue(content.SequenceEqual(regionsToReturn.Select(x=> new RegionDetailedInfoModel(x))));
        }
        
        [TestMethod]
        public void ValidGetReportReturnHasStatusCode200()
        {
            var regionsToReturn = new List<Region>
            {
                Region
            };
            
            var regionLogicMock = new Mock<IRegionLogic>(MockBehavior.Strict);
            regionLogicMock.Setup(m => m.GetAll()).Returns(new List<Region>(){Region});
            var regionController = new RegionController(regionLogicMock.Object);

            var result = regionController.GetAll() as OkObjectResult;
            
            regionLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 200);
        }

        
    }
}
