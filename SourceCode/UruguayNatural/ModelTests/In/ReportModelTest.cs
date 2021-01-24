using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;

namespace ModelTests.In
{
    [TestClass]
    public class ReportModelTest
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
        public void ValidReportModelIsValid()
        {
            Assert.IsFalse(ReportModel.HasErrors());
        }

        [TestMethod]
        public void CheckOutSmallerThanCheckInIsNotValid()
        {
            ReportModel.CheckOut = ReportModel.CheckIn.Value.AddDays(-5);
            Assert.IsTrue(ReportModel.HasErrors());
        }

        [TestMethod]
        public void EmptyTouristSpotIsNotValid()
        {
            ReportModel.TouristSpot = null;
            Assert.IsTrue(ReportModel.HasErrors());
        }
    }
}
