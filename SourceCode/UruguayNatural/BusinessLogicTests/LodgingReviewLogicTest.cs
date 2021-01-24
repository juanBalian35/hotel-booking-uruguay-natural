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

namespace BusinessLogicTests
{
    [TestClass]
    public class LodgingReviewLogicTest
    {
        private LodgingReview LodgingReview;
        private Booking Booking;
        private Mock<IRepository<LodgingReview>> LodgingReviewMock;
        private Mock<ILodgingRepository> LodgingMock;
        private Mock<IRepository<Booking>> BookingMock;

        [TestInitialize]
        public void TestInitialize()
        {
            Booking = new Booking
            {
                Lodging = new Lodging { Id = 1 }
            };

            LodgingReview = new LodgingReview
            {
                BookingId = Booking.Id,
                Booking = Booking,
                Rating = 5,
                Commentary = "a comment" 
            };
            
            BookingMock = new Mock<IRepository<Booking>>(MockBehavior.Strict);
            LodgingReviewMock = new Mock<IRepository<LodgingReview>>(MockBehavior.Strict);
            LodgingMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
        }
        
        private LodgingReviewLogic CreateLodgingReviewLogic()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            unitOfWorkMock.Setup(m => m.GetLodgingReviewRepository()).Returns(LodgingReviewMock.Object);
            unitOfWorkMock.Setup(m => m.GetBookingRepository()).Returns(BookingMock.Object);
            unitOfWorkMock.Setup(m => m.GetLodgingRepository()).Returns(LodgingMock.Object);
            return new LodgingReviewLogic(unitOfWorkMock.Object);
        }


        [TestMethod]
        public void CreateLodgingReviewReturnsRepositoryValues()
        {
            LodgingReviewMock.Setup(m => m.Exists(It.IsAny<Expression<Func<LodgingReview, bool>>>())).Returns(false);
            
            BookingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Booking, bool>>>(),"Lodging")).Returns(Booking);

            LodgingReviewMock.Setup(m => m.Add(LodgingReview));
            LodgingReviewMock.Setup(m => m.Save());
            LodgingReviewMock
                .Setup(m => m.GetAll(It.IsAny<Expression<Func<LodgingReview, bool>>>(), "Booking,Booking.Tourist"))
                .Returns(new List<LodgingReview> {LodgingReview});

            var lodgingReviewLogic = CreateLodgingReviewLogic();
            
            Assert.AreEqual(lodgingReviewLogic.Create(LodgingReview), LodgingReview);
            BookingMock.VerifyAll();
            LodgingReviewMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CreateLodgingReviewThrowsNotFoundExceptionIfBookingDoesNotExist()
        {
            LodgingReviewMock.Setup(m => m.Exists(It.IsAny<Expression<Func<LodgingReview, bool>>>())).Returns(false);
            BookingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Booking, bool>>>(),"Lodging")).Returns((Booking)null);
            
            var lodgingReviewLogic = CreateLodgingReviewLogic();
            lodgingReviewLogic.Create(LodgingReview);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotUniqueException))]
        public void CreateLodgingReviewThrowsExceptionWith400StatusIfBookingIdIsNotUnique()
        {
            LodgingReviewMock.Setup(m => m.Exists(It.IsAny<Expression<Func<LodgingReview, bool>>>())).Returns(true);
            
            var lodgingReviewLogic = CreateLodgingReviewLogic();
            lodgingReviewLogic.Create(LodgingReview);
        }
        
        
        [TestMethod]
        public void GetAllReviewsReturnsRepositoryValues()
        {
            var lodgingReviewList = new List<LodgingReview>() {LodgingReview};
            LodgingMock.Setup(m => m.Exists(It.IsAny<Expression<Func<Lodging, bool>>>())).Returns(true);
            LodgingReviewMock.Setup(m => m.GetAllWithPagination(It.IsAny<Expression<Func<LodgingReview, bool>>>(),
                "Booking,Booking.Tourist", 1, 1)).Returns(lodgingReviewList);

            var lodgingReviewLogic = CreateLodgingReviewLogic();
            Assert.IsTrue(lodgingReviewLogic.GetAllReviews(Booking.Lodging.Id, 1, 1).SequenceEqual(lodgingReviewList));
            LodgingMock.VerifyAll();
            LodgingReviewMock.VerifyAll();
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetAllReviewsThrowsNotFoundExceptionIfLodgingDoesNotExist()
        {
            LodgingMock.Setup(m => m.Exists(It.IsAny<Expression<Func<Lodging, bool>>>())).Returns(false);
            
            var lodgingReviewLogic = CreateLodgingReviewLogic();
            lodgingReviewLogic.GetAllReviews(Booking.Lodging.Id, 1, 1);
        }
    }
}
