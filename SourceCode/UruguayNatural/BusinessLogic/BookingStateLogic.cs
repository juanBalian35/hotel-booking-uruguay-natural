using BusinessLogicInterface.Exceptions;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic
{
    public class BookingStateLogic : IBookingStateLogic
    {
        private readonly IRepository<Booking> BookingRepository;
        private readonly IRepository<BookingState> BookingStateRepository;

        public BookingStateLogic(IUnitOfWork unitOfWork)
        {
            BookingStateRepository = unitOfWork.GetBookingStateRepository();
            BookingRepository = unitOfWork.GetBookingRepository();
        }
        
        public BookingState Create(BookingState bookingState)
        {
            var booking = BookingRepository.Get(bookingState.Booking.Id);
            if (booking == null)
            {
                throw new NotFoundException("BookingId");
            }

            bookingState.Booking = booking;
            BookingStateRepository.Add(bookingState);
            BookingStateRepository.Save();
            return bookingState;
        }
    }
}
