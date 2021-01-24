using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace DomainTests
{
    [TestClass]
    public class LodgingReviewTests
    {
        private LodgingReview LodgingReview;

        [TestInitialize]
        public void TestInitialize()
        {
            var booking = new Booking
            {
                Id = 1,
            }; 
            LodgingReview = new LodgingReview()
            {
                Id = 1,
                Rating = 4,
                Commentary = "a comment",
                Booking = booking,
                BookingId =  booking.Id
            };
        }

        [TestMethod]
        public void ValidLodgingReviewIsValid()
        {
            Assert.IsFalse(LodgingReview.Validate().HasErrors());
        }

        [TestMethod]
        public void CommentaryCannotBeNull()
        {
            LodgingReview.Commentary = null;
            Assert.IsTrue(LodgingReview.Validate().HasErrors());
        }

        [TestMethod]
        public void CommentaryCannotBeEmpty()
        {
            LodgingReview.Commentary = "  ";
            Assert.IsTrue(LodgingReview.Validate().HasErrors());
        }

        [TestMethod]
        public void RatingCannotBeLessThan1()
        {
            LodgingReview.Rating = 0;
            Assert.IsTrue(LodgingReview.Validate().HasErrors());
        }

        [TestMethod]
        public void RatingCannotBeMoreThan5()
        {
            LodgingReview.Rating = 6;
            Assert.IsTrue(LodgingReview.Validate().HasErrors());
        }

    }
}
