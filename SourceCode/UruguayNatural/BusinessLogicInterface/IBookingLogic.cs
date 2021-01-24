using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface
{
    public interface IBookingLogic
    {
        Booking Create(Booking booking, ICollection<Guest> guests);

        ICollection<BookingState> GetAllStates(int id);
    }
}
