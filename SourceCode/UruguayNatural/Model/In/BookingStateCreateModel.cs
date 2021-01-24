using Domain;
using Domain.Validations;

namespace Model.In
{
    public class BookingStateCreateModel
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string Description { get; set; }

        public BookingState ToEntity()
        {
            return new BookingState
            {
                Booking = new Booking { Id = this.Id },
                State = this.State,
                Description = this.Description
            };
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
