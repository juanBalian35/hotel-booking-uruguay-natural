using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;

namespace ModelTests.In
{
    [TestClass]
    public class BookingStateCreateodelTest
    {
        private BookingStateCreateModel BookingStateCreateModel;
        
        [TestInitialize]
        public void TestInitialize()
        {
            BookingStateCreateModel = new BookingStateCreateModel()
            {
                Id = 1,
                State = "Creada",
                Description = "A description",
            };
        }

        [TestMethod]
        public void ToEntityCreatesBookingWithSameCheckInAsModel()
        {
            Assert.AreEqual(BookingStateCreateModel.ToEntity().Booking.Id, BookingStateCreateModel.Id);
        }

        [TestMethod]
        public void ToEntityCreatesBookingWithSameStateAsModel()
        {
            Assert.AreEqual(BookingStateCreateModel.ToEntity().State, BookingStateCreateModel.State);
        }
        
        [TestMethod]
        public void ToEntityCreatesBookingWithSameDescriptionAsModel()
        {
            Assert.AreEqual(BookingStateCreateModel.ToEntity().Description, BookingStateCreateModel.Description);
        }

        [TestMethod]
        public void ValidBookingStateModelIsValid()
        {
            Assert.IsFalse(BookingStateCreateModel.HasErrors());
        }
        
        [TestMethod]
        public void InvalidBookingStateModelIsNotValid()
        {
            BookingStateCreateModel.State = null;
            Assert.IsTrue(BookingStateCreateModel.HasErrors());
        }
    }
}