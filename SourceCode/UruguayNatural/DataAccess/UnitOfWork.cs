using System;
using System.Data.Common;
using DataAccess;
using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext Context;
        private TouristSpotRepository TouristSpotRepository;
        private LodgingRepository LodgingRepository;
        private GenericRepository<Administrator> AdministratorRepository;
        private GenericRepository<Session> SessionRepository;
        private GenericRepository<Region> RegionRepository;
        private GenericRepository<Category> CategoryRepository;
        private GenericRepository<Booking> BookingRepository;
        private GenericRepository<BookingState> BookingStateRepository;
        private GenericRepository<LodgingReview> LodgingReviewRepository;

        public UnitOfWork(DbContext dbContext)
        {
            Context = dbContext;
        }

        public ITouristSpotRepository GetTouristSpotRepository()
        {
            return TouristSpotRepository ?? (TouristSpotRepository = new TouristSpotRepository(Context));
        }

        public ILodgingRepository GetLodgingRepository()
        {
            return LodgingRepository ?? (LodgingRepository = new LodgingRepository(Context));
        }

        public IRepository<Administrator> GetAdminRepository()
        {
            return AdministratorRepository ?? (AdministratorRepository = new GenericRepository<Administrator>(Context));
        }

        public IRepository<Session> GetSessionRepository()
        {
            return SessionRepository ?? (SessionRepository = new GenericRepository<Session>(Context));
        }

        public IRepository<Region> GetRegionRepository()
        {
            return RegionRepository ?? (RegionRepository = new GenericRepository<Region>(Context));
        }

        public IRepository<Category> GetCategoryRepository()
        {
            return CategoryRepository ?? (CategoryRepository = new GenericRepository<Category>(Context));
        }

        public IRepository<Booking> GetBookingRepository()
        {
            return BookingRepository ?? (BookingRepository = new GenericRepository<Booking>(Context));
        }

        public IRepository<BookingState> GetBookingStateRepository()
        {
            return BookingStateRepository ?? (BookingStateRepository = new GenericRepository<BookingState>(Context));
        }

        public IRepository<LodgingReview> GetLodgingReviewRepository()
        {
            return LodgingReviewRepository ?? (LodgingReviewRepository = new GenericRepository<LodgingReview>(Context));
        }
    }
}
