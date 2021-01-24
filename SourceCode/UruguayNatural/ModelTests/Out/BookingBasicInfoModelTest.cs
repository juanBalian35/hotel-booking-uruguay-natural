using System;
using System.Collections.Generic;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;

namespace ModelTests.Out
{
    [TestClass]
    public class BookingBasicInfoModelTest
    {
        private Booking Booking;
        private BookingBasicInfoModel BookingBasicInfoModel;
        
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
                Tourist = tourist
            };
            BookingBasicInfoModel = new BookingBasicInfoModel(Booking);
        }

        [TestMethod]
        public void ConstructorCreatesModelWithSameIdAsEntity()
        {
            Assert.AreEqual(Booking.Id, BookingBasicInfoModel.Id);
        }
        
        [TestMethod]
        public void ConstructorCreatesModelWithSamePhoneAsEntity()
        {
            Assert.AreEqual(Booking.Lodging.Phone, BookingBasicInfoModel.Phone);
        }
        
        [TestMethod]
        public void ConstructorCreatesModelWithSameConfirmationMessageAsEntity()
        {
            Assert.AreEqual(Booking.Lodging.ConfirmationMessage, BookingBasicInfoModel.ConfirmationMessage);
        }

        [TestMethod]
        public void ModelWithSameIdEqualsANotherModel()
        {
            var otherModel = new BookingBasicInfoModel(Booking);
            Assert.AreEqual(BookingBasicInfoModel, otherModel);
        }

        [TestMethod]
        public void ModelWithSameIdDoesNotEqualAnotherModel()
        {
            var otherModel = new BookingBasicInfoModel(Booking);
            otherModel.Id = 3;
            Assert.AreNotEqual(BookingBasicInfoModel, otherModel);
        }

        [TestMethod]
        public void ModelDoesNotEqualAnotherObject()
        {
            Assert.IsFalse(BookingBasicInfoModel.Equals("string"));
        }
    }
}
