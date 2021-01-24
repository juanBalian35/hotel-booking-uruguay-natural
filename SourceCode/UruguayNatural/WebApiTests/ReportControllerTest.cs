using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;
using Model.Out;
using Moq;
using WebApi.Controllers;

namespace WebApiTests
{
    [TestClass]
    public class ReportControllerTest
    {
        private ReportModel ReportModel;

        [TestInitialize]
        public void TestInitialize()
        {
            ReportModel = new ReportModel
            {
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5),
                TouristSpot = 1,
                Page = 3,
                ResultsPerPage = 12
            };
        }

        [TestMethod]
        public void GetReportReturnsValuesFromLogic()
        {
            var reportsToReturn = new List<Lodging>
            {
                new Lodging { Id = 1, Address = "Address 1", TouristSpot = new TouristSpot(),  IsDeleted = false }
            }.Select(l => new ReportBasicInfoModel(l)).ToList();
            
            var reportLogicMock = new Mock<IReportLogic>(MockBehavior.Strict);
            reportLogicMock.Setup(m => m.Search(ReportModel)).Returns(reportsToReturn);
            var reportController = new ReportController(reportLogicMock.Object);

            var result = reportController.Get(ReportModel) as OkObjectResult;
            var content = result.Value as List<ReportBasicInfoModel>;

            reportLogicMock.VerifyAll();
            Assert.IsTrue(content.SequenceEqual(reportsToReturn));
        }

        [TestMethod]
        public void ValidGetReportReturnHasStatusCode200()
        {
            var reportsToReturn = new List<Lodging>
            {
                new Lodging { Id = 1, Address = "Address 1", TouristSpot = new TouristSpot(),  IsDeleted = false }
            }.Select(l => new ReportBasicInfoModel(l)).ToList();
            
            var reportLogicMock = new Mock<IReportLogic>(MockBehavior.Strict);
            reportLogicMock.Setup(m => m.Search(ReportModel)).Returns(reportsToReturn);
            var reportController = new ReportController(reportLogicMock.Object);

            var result = reportController.Get(ReportModel) as OkObjectResult;

            reportLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 200);
        }

        [TestMethod]
        public void InvalidGetReportReturnHasStatusCode400()
        {
            ReportModel.CheckOut = null;
            
            var reportLogicMock = new Mock<IReportLogic>(MockBehavior.Strict);
            var reportController = new ReportController(reportLogicMock.Object);
            var result = reportController.Get(ReportModel) as BadRequestObjectResult;

            reportLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 400);
        }
    }
}
