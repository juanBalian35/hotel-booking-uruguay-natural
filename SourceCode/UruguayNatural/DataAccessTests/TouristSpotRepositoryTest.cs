using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Context;
using Domain;
using System.Collections.Generic;
using System.Linq;
using DataAccess;

namespace DataAccessTests
{
    [TestClass]
    public class TouristSpotRepositoryTest
    {
        private DbContext Context;
        private DbContextOptions Options;
        private List<TouristSpot> TouristSpots;
        private TouristSpotRepository Repository;

        [TestInitialize]
        public void Setup()
        {
            TouristSpots = new List<TouristSpot>();

            Options = new DbContextOptionsBuilder<UruguayNaturalContext>()
                .UseInMemoryDatabase("UruguayNaturalDBTest").Options;
            Context = new UruguayNaturalContext(Options);
            Repository = new TouristSpotRepository(Context);
            
            var region = new Region()
            {
                Id = 1,
                Name = "region 1"
            };

            var category = new Category()
            {
                Id = 1,
                Name = "category 1"
            };

            var touristSpot1 = CreateTouristSpot(1, region, category);
            var touristSpot2 = CreateTouristSpot(2, region, category);
            TouristSpots.Add(touristSpot1);
            TouristSpots.Add(touristSpot2);
        }

        private static TouristSpot CreateTouristSpot(int id, Region region, Category category)
        {
            var touristSpot = new TouristSpot()
            {
                Id = id,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                Region = region,
                TouristSpotCategories = new List<TouristSpotCategory>()
            };

            var touristSpotCategory = new TouristSpotCategory()
            {
                CategoryId = category.Id,
                Category = category,
                TouristSpotId = touristSpot.Id,
                TouristSpot = touristSpot
            };

            touristSpot.TouristSpotCategories.Add(touristSpotCategory);

            return touristSpot;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void SearchWithRegionAndCategoriesReturnsAllSatisfyingThoseFilters()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            Context.SaveChanges();
            
            Assert.IsTrue(TouristSpots.SequenceEqual(Repository.Search(1, new List<int> {1})));
        }
        
        [TestMethod]
        public void SearchWithRegionAndNullCategoriesReturnsAllWithSpecifiedRegion()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            Context.SaveChanges();
            
            Assert.IsTrue(TouristSpots.SequenceEqual(Repository.Search(1, null)));
        }

        [TestMethod]
        public void HasAnyBookingReturnsTrueIfABookingExistsForTouristSpot()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            var lodging = new Lodging
            {
                Id = 1, Address = "Addr 1", TouristSpot = TouristSpots.ElementAt(0), IsDeleted = false
            };
            Context.Add(lodging);
            
            var booking = new Booking
            {
                Id = 1,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5),
                Lodging = lodging,
                Tourist = new Tourist { Name = "Example", Email = "exa@mpl.com" },
                States = new List<BookingState> { new BookingState { Description = "desc", State = "Aceptada" } }
            };
            Context.Add(booking);
            Context.SaveChanges();
            
            Assert.IsTrue(Repository.HasAnyBooking(TouristSpots.ElementAt(0).Id));
        }

        [TestMethod]
        public void HasAnyBookingReturnsFalseIfNoBookingsExistsForTouristSpot()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            var lodging = new Lodging
            {
                Id = 1, Address = "Addr 1", TouristSpot = TouristSpots.ElementAt(0), IsDeleted = false
            };
            Context.Add(lodging);
            Context.SaveChanges();
            
            Assert.IsFalse(Repository.HasAnyBooking(TouristSpots.ElementAt(0).Id));
        }
        
        [TestMethod]
        public void HasAnyBookingReturnsFalseIfABookingOnlyExistsForDeletedLodgingInTouristSpot()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            var lodging = new Lodging
            {
                Id = 1, Address = "Addr 1", TouristSpot = TouristSpots.ElementAt(0), IsDeleted = true
            };
            Context.Add(lodging);
            var booking = new Booking
            {
                Id = 1,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5),
                Lodging = lodging,
                Tourist = new Tourist { Name = "Example", Email = "exa@mpl.com" },
                States = new List<BookingState> { new BookingState { Description = "desc", State = "Aceptada" } }
            };
            Context.Add(booking);
            Context.SaveChanges();
            
            Assert.IsFalse(Repository.HasAnyBooking(TouristSpots.ElementAt(0).Id));
        }
    }
}
