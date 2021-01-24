using Context;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessTests
{
    [TestClass]
    public class UnitOfWorkTest
    {
        /*
        ITouristSpotRepository GetTouristSpotRepository();
        ILodgingRepository GetLodgingRepository();
        IRepository<Administrator> GetAdminRepository();
        IRepository<Session> GetSessionRepository();
        IRepository<Region> GetRegionRepository();
        IRepository<Category> GetCategoryRepository();
        IRepository<Booking> GetBookingRepository();
        IRepository<BookingState> GetBookingStateRepository();
        IRepository<LodgingReview> GetLodgingReviewRepository();*/
        private UnitOfWork UnitOfWork;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<UruguayNaturalContext>()
                .UseInMemoryDatabase("UruguayNaturalDBTest").Options;
            UnitOfWork = new UnitOfWork(new UruguayNaturalContext(options));
        }

        [TestMethod]
        public void GetTouristSpotRepositoryCreatesNewIfFirstTime()
        {
            Assert.IsNotNull(UnitOfWork.GetTouristSpotRepository());
        }
        
        [TestMethod]
        public void GetTouristSpotRepositoryReturnsAlreadyExistingRepository()
        {
            var previousRepository = UnitOfWork.GetTouristSpotRepository();
            Assert.AreEqual(previousRepository, UnitOfWork.GetTouristSpotRepository());
        }

        [TestMethod]
        public void GetLodgingRepositoryCreatesNewIfFirstTime()
        {
            Assert.IsNotNull(UnitOfWork.GetLodgingRepository());
        }
        
        [TestMethod]
        public void GetLodgingRepositoryReturnsAlreadyExistingRepository()
        {
            var previousRepository = UnitOfWork.GetLodgingRepository();
            Assert.AreEqual(previousRepository, UnitOfWork.GetLodgingRepository());
        }

        [TestMethod]
        public void GetAdminRepositoryCreatesNewIfFirstTime()
        {
            Assert.IsNotNull(UnitOfWork.GetAdminRepository());
        }
        
        [TestMethod]
        public void GetAdminRepositoryReturnsAlreadyExistingRepository()
        {
            var previousRepository = UnitOfWork.GetAdminRepository();
            Assert.AreEqual(previousRepository, UnitOfWork.GetAdminRepository());
        }

        [TestMethod]
        public void GetSessionRepositoryCreatesNewIfFirstTime()
        {
            Assert.IsNotNull(UnitOfWork.GetSessionRepository());
        }
        
        [TestMethod]
        public void GetSessionRepositoryReturnsAlreadyExistingRepository()
        {
            var previousRepository = UnitOfWork.GetSessionRepository();
            Assert.AreEqual(previousRepository, UnitOfWork.GetSessionRepository());
        }

        [TestMethod]
        public void GetRegionRepositoryCreatesNewIfFirstTime()
        {
            Assert.IsNotNull(UnitOfWork.GetRegionRepository());
        }
        
        [TestMethod]
        public void GetRegionRepositoryReturnsAlreadyExistingRepository()
        {
            var previousRepository = UnitOfWork.GetRegionRepository();
            Assert.AreEqual(previousRepository, UnitOfWork.GetRegionRepository());
        }
        
        [TestMethod]
        public void GetCategoryRepositoryCreatesNewIfFirstTime()
        {
            Assert.IsNotNull(UnitOfWork.GetCategoryRepository());
        }
        
        [TestMethod]
        public void GetCategoryRepositoryReturnsAlreadyExistingRepository()
        {
            var previousRepository = UnitOfWork.GetCategoryRepository();
            Assert.AreEqual(previousRepository, UnitOfWork.GetCategoryRepository());
        }
        
        [TestMethod]
        public void GetBookingRepositoryCreatesNewIfFirstTime()
        {
            Assert.IsNotNull(UnitOfWork.GetBookingRepository());
        }
        
        [TestMethod]
        public void GetBookingRepositoryReturnsAlreadyExistingRepository()
        {
            var previousRepository = UnitOfWork.GetBookingRepository();
            Assert.AreEqual(previousRepository, UnitOfWork.GetBookingRepository());
        }
        
        [TestMethod]
        public void GetBookingStateRepositoryCreatesNewIfFirstTime()
        {
            Assert.IsNotNull(UnitOfWork.GetBookingStateRepository());
        }
        
        [TestMethod]
        public void GetBookingStateRepositoryReturnsAlreadyExistingRepository()
        {
            var previousRepository = UnitOfWork.GetBookingStateRepository();
            Assert.AreEqual(previousRepository, UnitOfWork.GetBookingStateRepository());
        }
        
        [TestMethod]
        public void GetLodgingReviewRepositoryCreatesNewIfFirstTime()
        {
            Assert.IsNotNull(UnitOfWork.GetLodgingReviewRepository());
        }
        
        [TestMethod]
        public void GetLodgingReviewRepositoryReturnsAlreadyExistingRepository()
        {
            var previousRepository = UnitOfWork.GetLodgingReviewRepository();
            Assert.AreEqual(previousRepository, UnitOfWork.GetLodgingReviewRepository());
        }
    }
}