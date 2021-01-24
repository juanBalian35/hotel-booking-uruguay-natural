using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;
using Model.Out;
using Moq;
using WebApi.Controllers;

namespace WebApiTests
{
    [TestClass]
    public class BookingControllerTest
    {
        private BookingModel BookingModel;
        private Mock<IBookingLogic> BookingLogicMock;
        private Mock<ILodgingReviewLogic> LodgingReviewLogicMock;
        private Mock<IBookingStateLogic> BookingStateLogicMock;
        

        [TestInitialize]
        public void TestInitialize()
        {
            BookingModel = new BookingModel
            {
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5),
                Adults = 3,
                Lodging = 1,
                Name = "aName",
                LastName = "lastName",
                Email = "valid@email.com"
            };
            BookingLogicMock = new Mock<IBookingLogic>(MockBehavior.Strict);
            BookingStateLogicMock = new Mock<IBookingStateLogic>(MockBehavior.Strict);
            LodgingReviewLogicMock = new Mock<ILodgingReviewLogic>(MockBehavior.Strict);
        }

        [TestMethod]
        public void PostBookingReturnsValidModel()
        {
            var bookingToReturn = BookingModel.ToEntity();
            bookingToReturn.Lodging = new Lodging()
            {
                Id = 1,
                Name = "Name",
                Description = "Description",
                Rating = 3,
                IsFull = true,
                Images = new List<LodgingImage>() {new LodgingImage()},
                PricePerNight = 100,
                Address = "Valid Address 123",
                Phone = "+598 98 303 040",
                ConfirmationMessage = "Your reservation has been confirmed!",
                TouristSpot = new TouristSpot()
            };

            BookingLogicMock.Setup(m => m.Create(It.IsAny<Booking>(), It.IsAny<List<Guest>>()))
                .Returns(bookingToReturn);
            var bookingController = new BookingController(BookingLogicMock.Object, BookingStateLogicMock.Object,
                LodgingReviewLogicMock.Object);

            var result = bookingController.Post(BookingModel) as CreatedResult;
            var content = result.Value as BookingBasicInfoModel;

            BookingLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 201);
            Assert.IsTrue(content.Equals(new BookingBasicInfoModel(bookingToReturn)));
        }

        [TestMethod]
        public void PostBookingInvalidReturnsError400()
        {
            BookingModel.CheckIn = null;

            var bookingController = new BookingController(BookingLogicMock.Object, BookingStateLogicMock.Object,
                LodgingReviewLogicMock.Object);

            var result = bookingController.Post(BookingModel) as BadRequestObjectResult;

            Assert.AreEqual(result.StatusCode, 400);
        }

        [TestMethod]
        public void GetStatesReturnsValidModel()
        {
            var booking = BookingModel.ToEntity();

            BookingLogicMock.Setup(m => m.GetAllStates(booking.Id)).Returns(booking.States);

            var bookingController = new BookingController(BookingLogicMock.Object, BookingStateLogicMock.Object,
                LodgingReviewLogicMock.Object);

            var result = bookingController.GetStates(booking.Id) as OkObjectResult;
            var content = result.Value as List<BookingStateBasicInfoModel>;
            
            BookingLogicMock.VerifyAll();
            Assert.IsTrue(content.SequenceEqual(booking.States.Select(x => new BookingStateBasicInfoModel(x)).ToList()));
        }
        
        [TestMethod]
        public void PostStateReturnsValidBookingStateBasicInfoModel()
        {
            const int BOOKING_ID = 2;
            var bookingStateCreateModel = new BookingStateCreateModel
            {
                Id = BOOKING_ID,
                State = "Creada",
                Description = "Description"
            };
            var bookingState = bookingStateCreateModel.ToEntity();

            BookingStateLogicMock.Setup(m => m.Create(It.IsAny<BookingState>())).Returns(bookingState);

            var bookingController = new BookingController(BookingLogicMock.Object, BookingStateLogicMock.Object,
                LodgingReviewLogicMock.Object);
            var result = bookingController.PostState(BOOKING_ID, bookingStateCreateModel) as CreatedResult;
            var content = result.Value as BookingStateBasicInfoModel;
            
            BookingStateLogicMock.VerifyAll();
            Assert.AreEqual(content, new BookingStateBasicInfoModel(bookingState));
        }
        
        [TestMethod]
        public void PostStateReturnsValidBookingHas201StatusCode()
        {
            const int BOOKING_ID = 2;
            var bookingStateCreateModel = new BookingStateCreateModel()
            {
                Id = BOOKING_ID,
                State = "Creada",
                Description = "Description"
            };
            var bookingState = bookingStateCreateModel.ToEntity();

            BookingStateLogicMock.Setup(m => m.Create(It.IsAny<BookingState>())).Returns(bookingState);
            
            var bookingController = new BookingController(BookingLogicMock.Object, 
                BookingStateLogicMock.Object,
                LodgingReviewLogicMock.Object);
            var result = bookingController.PostState(BOOKING_ID, bookingStateCreateModel) as CreatedResult;
            
            BookingStateLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 201);
        }
        
        [TestMethod]
        public void PostBookingStateInvalidModelReturnsError400()
        {
            const int BOOKING_ID = 2;
            var bookingStateCreateModel = new BookingStateCreateModel
            {
                Id = BOOKING_ID,
                State = null,
                Description = "Description"
            };
            var bookingController = new BookingController(BookingLogicMock.Object, BookingStateLogicMock.Object,LodgingReviewLogicMock.Object);
            var result = bookingController.PostState(BOOKING_ID, bookingStateCreateModel) as BadRequestObjectResult;
            
            Assert.AreEqual(result.StatusCode, 400);
        }
        
        [TestMethod]
        public void PostLodgingReviewReturnsValidLodgingReviewBasicInfoModel()
        {
            const int BOOKING_ID = 2;
            var lodgingReviewModel = new LodgingReviewModel
            {
                BookingId = BOOKING_ID,
                Rating = 2,
                Commentary = "aComment" 
            };
            
            var lodgingReview = lodgingReviewModel.ToEntity();
            
            lodgingReview.Booking = new Booking
            {
                Tourist = new Tourist
                {
                    Name = "aName",
                    LastName =  "aLastName"
                },
            };
            LodgingReviewLogicMock.Setup(m => m.Create(It.IsAny<LodgingReview>())).Returns(lodgingReview);

            var bookingController = new BookingController(BookingLogicMock.Object, BookingStateLogicMock.Object,
                LodgingReviewLogicMock.Object);
            var result = bookingController.PostReview(BOOKING_ID, lodgingReviewModel) as CreatedResult;
            var content = result.Value as LodgingReviewBasicInfoModel;
            
            LodgingReviewLogicMock.VerifyAll();
            Assert.AreEqual(content, new LodgingReviewBasicInfoModel(lodgingReview));
        }
        
         
        [TestMethod]
        public void PostLodgingReviewInvalidModelReturnsError400()
        {
            const int BOOKING_ID = 2;
            var lodgingReviewModel = new LodgingReviewModel
            {
                BookingId = BOOKING_ID,
                Rating = 2,
                Commentary = null 
            };
            
            var bookingController = new BookingController(BookingLogicMock.Object, 
                BookingStateLogicMock.Object,
                LodgingReviewLogicMock.Object);
            var result = bookingController.PostReview(BOOKING_ID, lodgingReviewModel) as BadRequestObjectResult;
            
            Assert.AreEqual(result.StatusCode, 400);
        }
    }
}
