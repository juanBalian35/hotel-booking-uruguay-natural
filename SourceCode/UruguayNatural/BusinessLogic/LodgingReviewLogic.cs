using System.Collections.Generic;
using BusinessLogicInterface.Exceptions;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System.Linq;

namespace BusinessLogic
{
    public class LodgingReviewLogic : ILodgingReviewLogic
    {
        private readonly IRepository<Booking> BookingRepository;
        private readonly IRepository<LodgingReview> LodgingReviewRepository;
        private readonly ILodgingRepository LodgingRepository;

        public LodgingReviewLogic(IUnitOfWork unitOfWork)
        {
            BookingRepository = unitOfWork.GetBookingRepository();
            LodgingReviewRepository = unitOfWork.GetLodgingReviewRepository();
            LodgingRepository = unitOfWork.GetLodgingRepository();
        }

        public LodgingReview Create(LodgingReview lodgingReview)
        {
            if (LodgingReviewRepository.Exists(x => x.BookingId == lodgingReview.BookingId))
            {
                throw new NotUniqueException("BookingId");
            }

            var actualBooking = BookingRepository.GetFirst(x => x.Id == lodgingReview.BookingId, "Lodging");
            if (actualBooking == null)
            {
                throw new NotFoundException("BookingId");
            }

            lodgingReview.Booking = actualBooking;
            
            
            var lodging = lodgingReview.Booking.Lodging;
            lodging.ReviewAverage = ((lodging.ReviewAverage * lodging.ReviewsQuantity) + lodgingReview.Rating) /
                                    ++lodging.ReviewsQuantity; 
            
            
            LodgingReviewRepository.Add(lodgingReview);
            LodgingReviewRepository.Save();
            
            return LodgingReviewRepository.GetAll(x => x.Id == lodgingReview.Id, "Booking,Booking.Tourist").First();
        }
        
        public ICollection<LodgingReview> GetAllReviews(int id, int page, int resultsPerPage)
        {
            if (!LodgingRepository.Exists(l => l.Id == id))
            {
                throw new NotFoundException("Lodging");
            }
            
            var reviews = LodgingReviewRepository.GetAllWithPagination(
                x => x.Booking.Lodging.Id == id,
                "Booking,Booking.Tourist", page, resultsPerPage);
            
            return reviews;
        }
    }
}
