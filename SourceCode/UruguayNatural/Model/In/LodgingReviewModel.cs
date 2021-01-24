using Domain;
using Domain.Validations;

namespace Model.In
{
    public class LodgingReviewModel 
    {
        public int BookingId { get; set; }
        public int Rating { get; set; }
        public string Commentary { get; set; }

        public LodgingReview ToEntity()
        {
            var lodgingReview = new LodgingReview
            {
                BookingId = BookingId,
                Rating = Rating ,
                Commentary = Commentary
            };

            return lodgingReview;
        }

        public bool HasErrors()
        {
            return !ToEntity().IsValid();
        }

        public INotification Errors()
        {
            return ToEntity().Validate();
        }
    }
}
