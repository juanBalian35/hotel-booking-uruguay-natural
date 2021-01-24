using System;
using System.Collections.Generic;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests
{
    [TestClass]
    public class BookingTest
    {
        private Booking Booking;

        [TestInitialize]
        public void TestInitialize()
        {
            var lodging = new Lodging()
            {
                Id = 1,
                Name = "Name",
                Description = "Description",
                Rating = 3,
                IsFull = true,
                Images = new List<LodgingImage>() { new LodgingImage() },
                PricePerNight = 100,
                Address = "Valid Address 123",
                Phone = "+598 98 303 040",
                ConfirmationMessage = "Your reservation has been confirmed!",
                TouristSpot = null
            };
            
            var tourist = new Tourist()
            {
                Name = "aName",
                LastName = "aLastName",
                Email = "an@email.com"
            };
            
            Booking = new Booking()
            {
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5),
                Lodging = lodging,
                Guests = 3,
                Tourist = tourist,
                States = new List<BookingState> { new BookingState() }
            };
        }

        [TestMethod]
        public void ValidBookingIsValid()
        {
            Assert.IsFalse(Booking.Validate().HasErrors());
        }
        
        [TestMethod]
        public void CheckInCannotBeNull()
        {
            Booking.CheckIn = null;
            Assert.IsTrue(Booking.Validate().HasErrors());
        }

        [TestMethod]
        public void CheckOutCannotBeNull()
        {
            Booking.CheckOut = null;
            Assert.IsTrue(Booking.Validate().HasErrors());
        }
        
        [TestMethod]
        public void CheckInCannotBeGreaterOrEqualThanCheckIn()
        {
            Booking.CheckIn = DateTime.Now.AddDays(5);
            Booking.CheckOut = DateTime.Now;
            Assert.IsTrue(Booking.Validate().HasErrors());
        }

        [TestMethod]
        public void LodgingCannotBeNull()
        {
            Booking.Lodging = null;
            Assert.IsTrue(Booking.Validate().HasErrors());
        }

        [TestMethod]
        public void GuestsCannotBeNegativeOrZero()
        {
            Booking.Guests = 0;
            Assert.IsTrue(Booking.Validate().HasErrors());
        }

        [TestMethod]
        public void NameCannotBeNull()
        {
            Booking.Tourist.Name = null;
            Assert.IsTrue(Booking.Validate().HasErrors());
        }
        
        [TestMethod]
        public void NameCannotBeEmpty()
        {
            Booking.Tourist.Name = "   ";
            Assert.IsTrue(Booking.Validate().HasErrors());
        }
        
        [TestMethod]
        public void LastNameCannotBeNull()
        {
            Booking.Tourist.LastName = "   ";
            Assert.IsTrue(Booking.Validate().HasErrors());
        }
        
        [TestMethod]
        public void LastNameCannotBeEmpty()
        {
            Booking.Tourist.LastName = "   ";
            Assert.IsTrue(Booking.Validate().HasErrors());
        }
        
        [TestMethod]
        public void StatesCannotBeNull()
        {
            Booking.States = null;
            Assert.IsTrue(Booking.Validate().HasErrors());
        }
        
        [TestMethod]
        public void StatesCannotBeEmpty()
        {
            Booking.States.Clear();
            Assert.IsTrue(Booking.Validate().HasErrors());
        }
        
        [TestMethod]
        public void EmailCannotBeNull()
        {
            Booking.Tourist.Email = null;
            Assert.IsTrue(Booking.Validate().HasErrors());
        }
        
        [TestMethod]
        public void EmailHasToHaveValidFormat()
        {
            Booking.Tourist.Email = "invalid email.com";
            Assert.IsTrue(Booking.Validate().HasErrors());
        }

        [TestMethod]
        public void CheckInSameDateAsCheckOutIsNotValid()
        {
            Booking.CheckIn = DateTime.Today.AddHours(2);
            Booking.CheckOut = DateTime.Today.AddHours(5);
            Assert.IsTrue(Booking.Validate().HasErrors());
        }
        
        [TestMethod]
        public void TouristHasToBeValid()
        {
            Booking.Tourist = new Tourist()
            {
                Name = null,
                LastName = "lastname",
                Email = "invalidEmail"
            };
            
            Assert.IsTrue(Booking.Validate().HasErrors());
        }
    }
}