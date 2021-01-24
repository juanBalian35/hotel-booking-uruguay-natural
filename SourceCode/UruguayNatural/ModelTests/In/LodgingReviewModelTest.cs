using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;

namespace ModelTests.In
{
    [TestClass]
    public class LodgingReviewModelTest
    {
        private LodgingReviewModel LodgingReviewModel;

        [TestInitialize]
        public void TestInitialize()
        {
            LodgingReviewModel = new LodgingReviewModel
            {
                BookingId = 1 ,
                Rating = 3,
                Commentary = "a comment"
            };
        }

        [TestMethod]
        public void ToEntityCreatesLodgingReviewWithSameBookingId()
        {
            var lodgingReview = LodgingReviewModel.ToEntity();
            Assert.AreEqual(LodgingReviewModel.BookingId, lodgingReview.BookingId);
        }

        [TestMethod]
        public void ToEntityCreatesLodgingReviewWithSameRating()
        {
            var lodgingReview = LodgingReviewModel.ToEntity();
            Assert.AreEqual(LodgingReviewModel.Rating, lodgingReview.Rating);
        }

        [TestMethod]
        public void ToEntityCreatesLodgingReviewWithCommentary()
        {
            var lodgingReview = LodgingReviewModel.ToEntity();
            Assert.AreEqual(LodgingReviewModel.Commentary, lodgingReview.Commentary);
        }

        [TestMethod]
        public void IsValidReturnsTrueIfLodgingReviewIsValid()
        {
            Assert.IsFalse(LodgingReviewModel.HasErrors());
        }

        [TestMethod]
        public void IsValidReturnsFalseIfAdministratorIsInvalid()
        {
            LodgingReviewModel.Commentary = "";
            Assert.IsTrue(LodgingReviewModel.HasErrors());
        }
    }
}
