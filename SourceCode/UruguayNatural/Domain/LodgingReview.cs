using Domain.Validations;

namespace Domain
{
    public class LodgingReview : Validatable
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public int Rating { get; set; }
        public string Commentary { get; set; }

        public LodgingReview()
        {
            AddValidation(nameof(Rating), new MinNumberValidation(1));
            AddValidation(nameof(Rating), new MaxNumberValidation(5));
            
            AddValidation(nameof(Commentary), new NotNullValidation());
            AddValidation(nameof(Commentary), new StringNotWhitespaceValidation());
        }
    }
    
}