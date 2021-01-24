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

namespace BusinessLogicTests
{
    [TestClass]
    public class LodgingLogicTest
    {
        private Lodging Lodging;
        private Mock<ITouristSpotRepository> TouristSpotsMock;
        private Mock<ILodgingRepository> LodgingMock;
        
        [TestInitialize]
        public void TestInitialize()
        {
            var touristSpot = new TouristSpot
            {
                Id = 1,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                Region = null,
                TouristSpotCategories = new List<TouristSpotCategory>()
            };

            Lodging = new Lodging
            {
                Id = 1,
                Name = "Name",
                Description = "Description",
                Rating = 3,
                IsFull = true,
                Images = new List<LodgingImage> { new LodgingImage() },
                PricePerNight = 100,
                Address = "Valid Address 123",
                Phone = "+598 98 303 040",
                ConfirmationMessage = "Your reservation has been confirmed!",
                TouristSpot = touristSpot
            };
            TouristSpotsMock = new Mock<ITouristSpotRepository>();
            LodgingMock = new Mock<ILodgingRepository>();
        }

        private LodgingLogic CreateLodgingLogic()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            unitOfWorkMock.Setup(m => m.GetTouristSpotRepository()).Returns(TouristSpotsMock.Object);
            unitOfWorkMock.Setup(m => m.GetLodgingRepository()).Returns(LodgingMock.Object);
            return new LodgingLogic(unitOfWorkMock.Object);
        }

        [TestMethod]
        public void CreateLodgingReturnsRepositoryValue()
        {
            LodgingMock.Setup(m => m.Add(Lodging));
            LodgingMock.Setup(m => m.Save());
            LodgingMock.Setup(m => m.Exists(It.IsAny<Expression<Func<Lodging, bool>>>())).Returns(false);
            LodgingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Lodging, bool>>>(), "TouristSpot,Images"))
                .Returns(Lodging);

            TouristSpotsMock.Setup(m => m.Get(Lodging.TouristSpot.Id)).Returns(Lodging.TouristSpot);

            var lodgingLogic = CreateLodgingLogic();
            
            Assert.AreEqual(lodgingLogic.Create(Lodging), Lodging);
            TouristSpotsMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CreateLodgingThrowsExceptionIfTouristSpotIsNotFound()
        {
            TouristSpotsMock.Setup(m => m.Get(Lodging.TouristSpot.Id)).Returns((TouristSpot)null);

            var lodgingLogic = CreateLodgingLogic();
            
            lodgingLogic.Create(Lodging);
        }

        [TestMethod]
        [ExpectedException(typeof(NotUniqueException))]
        public void CreateLodgingThrowsExceptionIfLodgingIsNotUnique()
        {
            TouristSpotsMock.Setup(m => m.Get(Lodging.TouristSpot.Id)).Returns(Lodging.TouristSpot);
            LodgingMock.Setup(m => m.Exists(It.IsAny<Expression<Func<Lodging, bool>>>())).Returns(true);

            var lodgingLogic = CreateLodgingLogic();
            
            lodgingLogic.Create(Lodging);
        }
        
        [TestMethod]
        public void UpdateLodgingReturnsRepositoryValue()
        {
            LodgingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Lodging, bool>>>(), "")).Returns(Lodging);
            LodgingMock.Setup(m => m.Update(Lodging));
            LodgingMock.Setup(m => m.Save());
            LodgingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Lodging, bool>>>(), "TouristSpot,Images"))
                .Returns(Lodging);

            var lodgingLogic = CreateLodgingLogic();

            Assert.AreEqual(lodgingLogic.Update(Lodging.Id), Lodging);
            LodgingMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void UpdateLodgingThrowsExceptionWith404StatusIfIdNotExists()
        {
            LodgingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Lodging, bool>>>(), "")).Returns((Lodging)null);
            
            var lodgingLogic = CreateLodgingLogic();
            
            lodgingLogic.Update(100);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void SearchLodgingThrowsExceptionIfTouristSpotIsNull()
        {
            var searchLodgingModel = new SearchLodgingModel()
            {
                Adults = 2,
                Children = 4,
                Babies = 1,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(3),
                TouristSpot = 3
            };
            
            TouristSpotsMock.Setup(m => m.Get(Lodging.TouristSpot.Id)).Returns((TouristSpot)null);
            var lodgingLogic = CreateLodgingLogic();
            
            lodgingLogic.Search(searchLodgingModel);
        }
        
        [TestMethod]
        public void SearchLodgingReturnsLodgingListIfValid()
        {
            var searchLodgingModel = new SearchLodgingModel()
            {
                Adults = 2,
                Children = 4,
                Babies = 1,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(3),
                TouristSpot = 3
            };

            var lodgings = new List<Lodging> { Lodging };
            
            TouristSpotsMock.Setup(m => m.Get(searchLodgingModel.TouristSpot)).Returns(Lodging.TouristSpot);
            LodgingMock.Setup(m => m.GetAllWithPagination(It.IsAny<Expression<Func<Lodging, bool>>>(),
                "Images", searchLodgingModel.Page, searchLodgingModel.ResultsPerPage)).Returns(lodgings);
            var lodgingLogic = CreateLodgingLogic();

            Assert.IsTrue(lodgingLogic.Search(searchLodgingModel).SequenceEqual(lodgings));
            TouristSpotsMock.VerifyAll();
            LodgingMock.VerifyAll();
        }
        
        [TestMethod]
        public void DeleteLodgingCallsRepositoryMethods()
        {
            LodgingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Lodging, bool>>>(),"")).Returns(Lodging);
            LodgingMock.Setup(m => m.Update(Lodging));
            LodgingMock.Setup(m => m.Save());
            
            var lodgingLogic = CreateLodgingLogic();

            lodgingLogic.Delete(Lodging.Id);
            
            LodgingMock.VerifyAll();
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void DeleteLodgingThrowsExceptionWith404StatusIfIdNotExists()
        {
            LodgingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Lodging, bool>>>(), "")).Returns((Lodging)null);
            
            var lodgingLogic = CreateLodgingLogic();
            
            lodgingLogic.Delete(100);
        }
    }
}
