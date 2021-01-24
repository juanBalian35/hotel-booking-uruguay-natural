using System;
using System.Collections.Generic;
using BusinessLogic;
using BusinessLogicInterface.Exceptions;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogicTests
{
    [TestClass]
    public class BookingStateLogicTest
    {
        private BookingState BookingState;
        private Mock<IRepository<Booking>> BookingRepositoryMock;
        private Mock<IRepository<BookingState>> BookingStateRepositoryMock;

        [TestInitialize]
        public void TestInitialize()
        {
            var booking = new Booking
            {
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5),
                Lodging = null,
                Guests = 3,
                Tourist = null,
                States = new List<BookingState>()
            };
            BookingState = new BookingState
            {
                Id = 1,
                Booking = booking,
                BookingId = booking.Id,
                State = "Creada",
                Description = "Description"
            };
            BookingRepositoryMock = new Mock<IRepository<Booking>>(MockBehavior.Strict);
            BookingStateRepositoryMock = new Mock<IRepository<BookingState>>(MockBehavior.Strict);
        }

        private BookingStateLogic CreateBookingStateLogic()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            unitOfWorkMock.Setup(m => m.GetBookingStateRepository()).Returns(BookingStateRepositoryMock.Object);
            unitOfWorkMock.Setup(m => m.GetBookingRepository()).Returns(BookingRepositoryMock.Object);
            return new BookingStateLogic(unitOfWorkMock.Object);
        }

        [TestMethod]
        public void CreateBookingStateCreatesReturnsRepositoryValues()
        {
            BookingRepositoryMock.Setup(m => m.Get(BookingState.Booking.Id)).Returns(BookingState.Booking);
            BookingStateRepositoryMock.Setup(m => m.Add(It.IsAny<BookingState>()));
            BookingStateRepositoryMock.Setup(m => m.Save());

            var bookingStateLogic = CreateBookingStateLogic();
            var createdBookingState = bookingStateLogic.Create(BookingState);

            Assert.AreEqual(createdBookingState, BookingState);
            BookingStateRepositoryMock.VerifyAll();
            BookingRepositoryMock.VerifyAll();
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CreateBookingStateThrowsExceptionIfBookingIsNotFound()
        {
            BookingRepositoryMock.Setup(m => m.Get(BookingState.Booking.Id)).Returns((Booking)null);
            
            var bookingStateLogic = CreateBookingStateLogic();

            bookingStateLogic.Create(BookingState);
            Assert.Fail("Should throw exception");
        }
    }
}
