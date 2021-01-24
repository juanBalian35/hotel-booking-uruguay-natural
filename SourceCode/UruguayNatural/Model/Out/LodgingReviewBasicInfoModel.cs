using Domain;

namespace Model.Out
{
    public class LodgingReviewBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Commentary { get; set; }

        public LodgingReviewBasicInfoModel(LodgingReview lodgingReview)
        {
            Id = lodgingReview.Id;
            Name = lodgingReview.Booking.Tourist.Name + " " + lodgingReview.Booking.Tourist.LastName;
            Rating = lodgingReview.Rating;
            Commentary = lodgingReview.Commentary;
        }

        public override bool Equals(object obj)
        {
            if(!(obj is LodgingReviewBasicInfoModel))
            {
                return false;
            }

            var lodgingReview = obj as LodgingReviewBasicInfoModel;
            return Id == lodgingReview.Id;
        }
    }
}
