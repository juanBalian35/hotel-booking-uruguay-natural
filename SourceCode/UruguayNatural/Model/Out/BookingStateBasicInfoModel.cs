using Domain;

namespace Model.Out
{
    public class BookingStateBasicInfoModel
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string Description { get; set; }

        public BookingStateBasicInfoModel(BookingState bookingState)
        {
            Id = bookingState.Id;
            State = bookingState.State;
            Description = bookingState.Description;
        }
        
        public override bool Equals(object obj)
        {
            if(!(obj is BookingStateBasicInfoModel))
            {
                return false;
            }

            var bookingState = obj as BookingStateBasicInfoModel;
            return Id == bookingState.Id;
        }
    }
}
