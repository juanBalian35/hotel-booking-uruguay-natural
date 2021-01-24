using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Domain;

namespace DomainTests
{
    [TestClass]
    public class BookingStateTest
    {
        private BookingState BookingState;
        private Booking Booking;
        
        [TestInitialize]
        public void TestInitialize()
        {
            Booking = new Booking()
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
                Booking = Booking,
                BookingId = Booking.Id,
                State = "Creada",
                Description = "Description"
            };
        }

        [TestMethod]
        public void SetBookingStateIdSetsId()
        {
            const int ID = 2;
            BookingState.Id = ID;
            
            Assert.AreEqual(BookingState.Id, ID);
        }
        
        [TestMethod]
        public void SetBookingStateBookingSetsBooking()
        {
            const Booking ANOTHER_BOOKING = null;
            BookingState.Booking = ANOTHER_BOOKING;
            
            Assert.AreEqual(BookingState.Booking, ANOTHER_BOOKING);
        }

        [TestMethod]
        public void SetBookingStateBookingIdSetsBookingId()
        {
            const int ID = 2;
            BookingState.BookingId = ID;
            
            Assert.AreEqual(BookingState.BookingId, ID);
        }

        [TestMethod]
        public void SetBookingStateStateSetsState()
        {
            const string STATE = "Creada";
            BookingState.State = STATE;
            
            Assert.AreEqual(BookingState.State, STATE);
        }

        [TestMethod]
        public void SetBookingStateDescriptionSetsDescription()
        {
            const string DESCRIPTION = "booking created";
            BookingState.Description = DESCRIPTION;
            
            Assert.AreEqual(BookingState.Description, DESCRIPTION);
        }

        [TestMethod]
        public void ValidBookingStateIsValid()
        {
            Assert.IsFalse(BookingState.Validate().HasErrors());
        }

        [TestMethod]
        public void BookingCannotBeNull()
        {
            BookingState.Booking = null;
            Assert.IsTrue(BookingState.Validate().HasErrors());
        }

        [TestMethod]
        public void BookingStateCannotBeNull()
        {
            BookingState.State = null;
            Assert.IsTrue(BookingState.Validate().HasErrors());
        }

        [TestMethod]
        public void BookingStateCannotBeEmpty()
        {
            BookingState.State = "";
            Assert.IsTrue(BookingState.Validate().HasErrors());
        }

        [TestMethod]
        public void BookingStateHasToBeInTheList()
        {
            BookingState.State = "NotValidState";
            Assert.IsTrue(BookingState.Validate().HasErrors());
        }

        [TestMethod]
        public void DescriptionCannotBeNull()
        {
            BookingState.Description = null;
            Assert.IsTrue(BookingState.Validate().HasErrors());
        }

        [TestMethod]
        public void DescriptionCannotBeEmpty()
        {
            BookingState.Description = "   ";
            Assert.IsTrue(BookingState.Validate().HasErrors());
        }
    }
}