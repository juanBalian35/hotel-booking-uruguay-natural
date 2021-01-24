using System;
using System.Collections.Generic;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;

namespace ModelTests.Out
{
    [TestClass]
    public class BookingStateBasicInfoModelTest
    {
        private BookingState BookingState;
        private BookingStateBasicInfoModel BookingStateBasicInfoModel;
        
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

            var booking = new Booking()
            {
                Id = 1,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5),
                Lodging = lodging,
                Guests = 3,
                Tourist = tourist,
            };
            
            BookingState = new BookingState
            {
                Id = 1,
                Booking = booking,
                BookingId = booking.Id,
                State = "Aceptada",
                Description = "Primer estado"
            };
            
            BookingStateBasicInfoModel = new BookingStateBasicInfoModel(BookingState);
        }

        [TestMethod]
        public void ConstructorCreatesModelWithSameIdAsEntity()
        {
            Assert.AreEqual(BookingState.Id, BookingStateBasicInfoModel.Id);
        }
        
        [TestMethod]
        public void ConstructorCreatesModelWithSameStateAsEntity()
        {
            Assert.AreEqual(BookingState.State, BookingStateBasicInfoModel.State);
        }
        
        [TestMethod]
        public void ConstructorCreatesModelWithSameDescriptionAsEntity()
        {
            Assert.AreEqual(BookingState.Description, BookingStateBasicInfoModel.Description);
        }

        [TestMethod]
        public void ModelWithSameIdEqualsAnotherModel()
        {
            var otherModel = new BookingStateBasicInfoModel(BookingState);
            Assert.AreEqual(BookingStateBasicInfoModel, otherModel);
        }

        [TestMethod]
        public void ModelWithSameIdDoesNotEqualAnotherModel()
        {
            var otherModel = new BookingStateBasicInfoModel(BookingState);
            otherModel.Id = 3;
            Assert.AreNotEqual(BookingStateBasicInfoModel, otherModel);
        }

        [TestMethod]
        public void ModelDoesNotEqualAnotherObject()
        {
            Assert.IsFalse(BookingStateBasicInfoModel.Equals("string"));
        }
    }
}
