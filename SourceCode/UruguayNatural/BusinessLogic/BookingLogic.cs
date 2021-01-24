using System;
using System.Collections.Generic;
using BusinessLogicInterface.Exceptions;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System.Linq;

namespace BusinessLogic
{
    public class BookingLogic : IBookingLogic
    {
        private readonly IRepository<Booking> BookingRepository;
        private readonly ILodgingRepository LodgingRepository;
        
        public BookingLogic(IUnitOfWork unitOfWork)
        {
            BookingRepository = unitOfWork.GetBookingRepository();
            LodgingRepository = unitOfWork.GetLodgingRepository();
        }

        public Booking Create(Booking booking, ICollection<Guest> guests)
        {
            var lodging = LodgingRepository.GetFirst(x => x.Id == booking.Lodging.Id && !x.IsDeleted && !x.IsFull);
            if (lodging == null)
            {
                throw new NotFoundException("Lodging");
            }

            booking.Lodging = lodging;
            booking.TotalPrice = lodging.CalculatePrice((DateTime)booking.CheckIn, (DateTime)booking.CheckOut, guests);
            
            BookingRepository.Add(booking);
            BookingRepository.Save();
            
            return BookingRepository.GetFirst(x => x.Id == booking.Id);
        }

        public ICollection<BookingState> GetAllStates(int id)
        {
            var booking = BookingRepository.GetFirst(x => x.Id == id, "States");
            if (booking == null)
            {
                throw new NotFoundException("BookingId");
            }
            
            return booking.States.OrderByDescending(x => x.Id).ToList();
        }
    }
}
