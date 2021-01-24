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
    public class BookingLogicTest
    {
        private BookingLogic BookingLogic;
        private Booking Booking;
        private Mock<ILodgingRepository> LodgingMock;
        private Mock<IRepository<Booking>> BookingMock;
        private ICollection<Guest> ExampleGuests;

        [TestInitialize]
        public void TestInitialize()
        {
            var lodging = new Lodging
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
                TouristSpot = null
            };
            var tourist = new Tourist
            {
                Name = "aName",
                LastName = "aLastName",
                Email = "an@email.com"
            };
            Booking = new Booking
            {
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5),
                Lodging = lodging,
                Guests = 3,
                Tourist = tourist,
                States = new List<BookingState> { new BookingState() }
            };
            BookingMock = new Mock<IRepository<Booking>>(MockBehavior.Strict);
            LodgingMock = new Mock<ILodgingRepository>(MockBehavior.Strict);
            ExampleGuests = new List<Guest> { new Adult() };
        }

        private BookingLogic CreateBookingLogic()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            unitOfWorkMock.Setup(m => m.GetBookingRepository()).Returns(BookingMock.Object);
            unitOfWorkMock.Setup(m => m.GetLodgingRepository()).Returns(LodgingMock.Object);
            return new BookingLogic(unitOfWorkMock.Object);
        }

        [TestMethod]
        public void CreateBookingCallReturnsRepositoryReturnValue()
        {
            BookingMock.Setup(m => m.Add(Booking));
            BookingMock.Setup(m => m.Save());
            BookingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Booking, bool>>>(),"")).Returns(Booking);

            LodgingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Lodging, bool>>>(),"")).Returns(Booking.Lodging);
            
            BookingLogic = CreateBookingLogic();

            Assert.AreEqual(BookingLogic.Create(Booking, ExampleGuests), Booking);
            BookingMock.VerifyAll();
            LodgingMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CreateBookingThrowsNotFoundExceptionIfLodgingDoesNotExist()
        {
            LodgingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Lodging, bool>>>(), "")).Returns((Lodging)null);
            
            BookingLogic = CreateBookingLogic();

            BookingLogic.Create(Booking, ExampleGuests);
        }

        [TestMethod]
        public void GetAllStatesReturnsRepositoryValue()
        {
            BookingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Booking, bool>>>(),"States")).Returns(Booking);
            
            BookingLogic = CreateBookingLogic();

            Assert.IsTrue(BookingLogic.GetAllStates(Booking.Id).SequenceEqual(Booking.States));
            BookingMock.VerifyAll();
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetAllStatesThrowsNotFoundExceptionIfBookingDoesNotExist()
        {
            BookingMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Booking, bool>>>(),"States"))
                .Returns((Booking)null);
            
            BookingLogic = CreateBookingLogic();

            BookingLogic.GetAllStates(Booking.Id);
        }
    }
}
