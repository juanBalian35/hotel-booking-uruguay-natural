using Domain;

namespace DataAccessInterface
{
    public interface IUnitOfWork
    {
        ITouristSpotRepository GetTouristSpotRepository();
        ILodgingRepository GetLodgingRepository();
        IRepository<Administrator> GetAdminRepository();
        IRepository<Session> GetSessionRepository();
        IRepository<Region> GetRegionRepository();
        IRepository<Category> GetCategoryRepository();
        IRepository<Booking> GetBookingRepository();
        IRepository<BookingState> GetBookingStateRepository();
        IRepository<LodgingReview> GetLodgingReviewRepository();
    }
}