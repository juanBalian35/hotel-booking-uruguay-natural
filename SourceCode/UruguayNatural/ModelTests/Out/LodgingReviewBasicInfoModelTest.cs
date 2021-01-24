using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;
using Domain;

namespace ModelTests.Out
{
    [TestClass]
    public class LodgingReviewBasicInfoModelTest
    {
        private LodgingReviewBasicInfoModel LodgingReviewBasicInfoModel;
        private LodgingReview LodgingReview;

        [TestInitialize]
        public void TestInitialize()
        {
            LodgingReview = new LodgingReview
            {
                Id = 1,
                BookingId = 1,
                Booking = new Booking
                {
                    Id = 1,
                    Tourist = new Tourist
                    {
                        Name = "aName",
                        LastName = "aLastname",
                    },
                },
                Rating = 3,
                Commentary = "a comment" 
            };
            LodgingReviewBasicInfoModel = new LodgingReviewBasicInfoModel(LodgingReview);
        }

        [TestMethod]
        public void ConstructorCreatesLodgingReviewBasicInfoModelWithSameId()
        {
            Assert.AreEqual(LodgingReview.Id, LodgingReviewBasicInfoModel.Id);
        } 
        
        [TestMethod]
        public void ConstructorCreatesLodgingReviewBasicInfoModelWithNameEqualToNamePlusLastname()
        {
            Assert.AreEqual(LodgingReview.Booking.Tourist.Name + " " + LodgingReview.Booking.Tourist.LastName,
                LodgingReviewBasicInfoModel.Name);
        }

        [TestMethod]
        public void ConstructorCreatesLodgingReviewBasicInfoModelWithSameRating()
        {
            Assert.AreEqual(LodgingReview.Rating, LodgingReviewBasicInfoModel.Rating);
        }

        [TestMethod]
        public void ConstructorCreatesLodgingReviewBasicInfoModelWithSameCommentary()
        {
            Assert.AreEqual(LodgingReview.Commentary, LodgingReviewBasicInfoModel.Commentary);
        }

        [TestMethod]
        public void EqualsAnotherWithSameId()
        {
            var anotherLodgingReviewModel = new LodgingReviewBasicInfoModel(LodgingReview);

            Assert.IsTrue(LodgingReviewBasicInfoModel.Equals(anotherLodgingReviewModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentId()
        {
            var anotherLodgingReview = LodgingReview;
            anotherLodgingReview.Id = 2;
            var anotherLodgingReviewModel = new LodgingReviewBasicInfoModel(anotherLodgingReview);

            Assert.IsFalse(LodgingReviewBasicInfoModel.Equals(anotherLodgingReviewModel));
        }
        

        [TestMethod]
        public void DoesNotEqualOtherType()
        {
            Assert.IsFalse(LodgingReviewBasicInfoModel.Equals("String"));
        }
    }
}
