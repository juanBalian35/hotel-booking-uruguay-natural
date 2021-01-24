using System;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;
using System.Linq;

namespace ModelTests.In
{
    [TestClass]
    public class BookingModelTest
    {
        private BookingModel BookingModel;
        
        [TestInitialize]
        public void TestInitialize()
        {
            BookingModel = new BookingModel()
            {
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5),
                Adults = 1,
                Babies = 2,
                Children = 3,
                Retirees = 2,
                Lodging = 1,
                Name = "aName",
                LastName = "lastName",
                Email = "valid@email.com"
            };
        }

        [TestMethod]
        public void ToEntityCreatesBookingWithSameCheckInAsModel()
        {
            Assert.AreEqual(BookingModel.ToEntity().CheckIn, BookingModel.CheckIn);
        }

        [TestMethod]
        public void ToEntityCreatesBookingWithSameCheckOutAsModel()
        {
            Assert.AreEqual(BookingModel.ToEntity().CheckOut, BookingModel.CheckOut);
        }

        [TestMethod]
        public void ToEntityCreatesBookingWithSameGuestsAsModelSummed()
        {
            Assert.AreEqual(BookingModel.ToEntity().Guests, 
                BookingModel.Adults + BookingModel.Children + BookingModel.Babies);
        }
        
        [TestMethod]
        public void ToEntityCreatesBookingWithLodgingWithSameIdAsModel()
        {
            Assert.AreEqual(BookingModel.ToEntity().Lodging.Id, BookingModel.Lodging);
        }
        
        [TestMethod]
        public void ToEntityCreatesBookingWithTouristWithSameNameAsModel()
        {
            Assert.AreEqual(BookingModel.ToEntity().Tourist.Name, BookingModel.Name);
        }
        
        [TestMethod]
        public void ToEntityCreatesBookingWithTouristWithSameLastNameAsModel()
        {
            Assert.AreEqual(BookingModel.ToEntity().Tourist.LastName, BookingModel.LastName);
        }
        
        [TestMethod]
        public void ToEntityCreatesBookingWithTouristWithSameEmailAsModel()
        {
            Assert.AreEqual(BookingModel.ToEntity().Tourist.Email, BookingModel.Email);
        }
        
        [TestMethod]
        public void ToEntityAddsInitialBookingState()
        {
            Assert.IsTrue(BookingModel.ToEntity().States.Count == 1);
        }

        [TestMethod]
        public void ValidBookingModelIsValid()
        {
            Assert.IsFalse(BookingModel.HasErrors());
        }
        
        [TestMethod]
        public void InvalidBookingModelIsNotValid()
        {
            BookingModel.Email = "";
            Assert.IsTrue(BookingModel.HasErrors());
        }
        
        [TestMethod]
        public void InvalidNumberOfAdultsIsNotValid()
        {
            BookingModel.Adults = -3;
            Assert.IsTrue(BookingModel.HasErrors());
        }

        [TestMethod]
        public void GetGuestsReturnsAsManyAdminsAsSpecified()
        {
            Assert.AreEqual(BookingModel.Adults, BookingModel.GetGuests().Sum(a => a is Adult ? a.Quantity : 0));
        }

        [TestMethod]
        public void GetGuestsReturnsAsManyChildrenAsSpecified()
        {
            Assert.AreEqual(BookingModel.Children, BookingModel.GetGuests().Sum(c => c is Child ? c.Quantity : 0));
        }

        [TestMethod]
        public void GetGuestsReturnsAsManyBabiesAsSpecified()
        {
            Assert.AreEqual(BookingModel.Babies, BookingModel.GetGuests().Sum(b => b is Baby ? b.Quantity : 0));
        }

        [TestMethod]
        public void GetGuestsReturnsAsManEldersAsSpecified()
        {
            Assert.AreEqual(BookingModel.Retirees, BookingModel.GetGuests().Sum(r => r is Retiree ? r.Quantity : 0));
        }
    }
}
