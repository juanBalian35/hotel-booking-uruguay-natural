using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessInterface;
using Moq;
using Domain;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;
using BusinessLogic;
using BusinessLogicInterface.Exceptions;
using Model.In;
using Model.Out;

namespace BusinessLogicTests
{
    [TestClass]
    public class ReportLogicTest
    {
        private Mock<ITouristSpotRepository> TouristSpotRepositoryMock;
        private Mock<ILodgingRepository> LodgingRepositoryMock;
        
        [TestInitialize]
        public void TestInitialize()
        { 
            LodgingRepositoryMock = new Mock<ILodgingRepository>();
            TouristSpotRepositoryMock = new Mock<ITouristSpotRepository>();
        }

        private ICollection<Lodging> CreateLodgingsForTouristSpot(TouristSpot touristSpot)
        {
            return new List<Lodging>
            {
                new Lodging { Id = 1, Address = "Address 1", TouristSpot = touristSpot,  IsDeleted = false },
                new Lodging { Id = 2, Address = "Address 2", TouristSpot = touristSpot,  IsDeleted = false }
            };
        }

        private ReportLogic CreateReportLogic()
        {
            var repositoryFactoryMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            repositoryFactoryMock.Setup(m => m.GetTouristSpotRepository()).Returns(TouristSpotRepositoryMock.Object);
            repositoryFactoryMock.Setup(m => m.GetLodgingRepository()).Returns(LodgingRepositoryMock.Object);
            return new ReportLogic(repositoryFactoryMock.Object);
        }

        [TestMethod]
        public void SearchReturnsRepositoryValue()
        {
            var touristSpot = new TouristSpot { Id = 1, Name = "name", Description = "description" };
            
            TouristSpotRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<TouristSpot, bool>>>(), ""))
                .Returns(touristSpot);
            TouristSpotRepositoryMock.Setup(m => m.HasAnyBooking(1)).Returns(true);

            var reportModel = new ReportModel()
            {
                TouristSpot = touristSpot.Id,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5)
            };

            var reports = CreateLodgingsForTouristSpot(touristSpot).Select(l => new ReportBasicInfoModel(l)).ToList();
            LodgingRepositoryMock.Setup(m => m.Search(reportModel))
                .Returns(reports);
            
            var reportLogic = CreateReportLogic();
            Assert.IsTrue(reportLogic.Search(reportModel).SequenceEqual(reports));
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void SearchThrowsExceptionIfNoTouristSpotFound()
        {
            TouristSpotRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<TouristSpot, bool>>>(), ""))
                .Returns((TouristSpot)null);

            var reportModel = new ReportModel()
            {
                TouristSpot = 1,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5)
            };

            var reportLogic = CreateReportLogic();
            reportLogic.Search(reportModel);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotValidException))]
        public void SearchThrowsExceptionIfNoBookingsForTouristSpots()
        {
            var touristSpot = new TouristSpot { Id = 1, Name = "name", Description = "description" };
            
            TouristSpotRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<TouristSpot, bool>>>(), ""))
                .Returns(touristSpot);
            TouristSpotRepositoryMock.Setup(m => m.HasAnyBooking(1)).Returns(false);

            var reportModel = new ReportModel()
            {
                TouristSpot = touristSpot.Id,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5)
            };

            var reportLogic = CreateReportLogic();
            reportLogic.Search(reportModel);
        }
    }
}