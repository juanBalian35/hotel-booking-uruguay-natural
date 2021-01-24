using Domain;

namespace Model.Out
{
    public class BookingBasicInfoModel
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string ConfirmationMessage { get; set; }

        public BookingBasicInfoModel(Booking booking)
        {
            Id = booking.Id;
            Phone = booking.Lodging.Phone;
            ConfirmationMessage = booking.Lodging.ConfirmationMessage;
        }

        public override bool Equals(object obj)
        {
            if(!(obj is BookingBasicInfoModel))
            {
                return false;
            }

            var booking = obj as BookingBasicInfoModel;
            return Id == booking.Id;
        }
    }
}
