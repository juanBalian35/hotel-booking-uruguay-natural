using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Context;
using Domain;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Model.In;

namespace DataAccessTests
{
    [TestClass]
    public class LodgingRepositoryTest
    {
        private DbContext Context;
        private DbContextOptions Options;
        private LodgingRepository Repository;
        private ICollection<TouristSpot> TouristSpots;
        private ReportModel ReportModel;

        [TestInitialize]
        public void Setup()
        {
            Options = new DbContextOptionsBuilder<UruguayNaturalContext>()
                .UseInMemoryDatabase("UruguayNaturalDBTest")
                .Options;
            Context = new UruguayNaturalContext(Options);

            Repository = new LodgingRepository(Context);
            
            TouristSpots = new List<TouristSpot> { CreateTouristSpot(1), CreateTouristSpot(2) };
            Context.AddRange(TouristSpots);
            ReportModel = new ReportModel
            {
                TouristSpot = 1,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5)
            };
        }

        private TouristSpot CreateTouristSpot(int id)
        {
            return new TouristSpot { Id = id, Name = "name" };
        }

        private Lodging CreateLodging(int id, TouristSpot touristSpot)
        {
            return new Lodging { Id = id, Address = "Address " + id, TouristSpot = touristSpot, IsDeleted = false };
        }

        private static Booking CreateBooking(int id, Lodging lodging, bool wasRejected)
        {
            var booking = new Booking
            {
                Id = id,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(5),
                Lodging = lodging,
                Tourist = new Tourist { Name = "Example", Email = "exa@mpl.com" },
                States = new List<BookingState> { new BookingState { Description = "desc", State = "Aceptada" } }
            };
            
            if (wasRejected)
            {
                booking.States.Add(new BookingState { Description = "desc", State = "Rechazada" });
            }

            return booking;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void ValidSearchReturnsOnlyResultsFromTheSpecifiedTouristSpot()
        {
            var lodgings = new List<Lodging>
            {
                CreateLodging(1, TouristSpots.ElementAt(0)), CreateLodging(2, TouristSpots.ElementAt(0))
            };
            Context.AddRange(lodgings);
            var bookings = new List<Booking>
            {
                CreateBooking(1, lodgings.ElementAt(0), false), CreateBooking(2, lodgings.ElementAt(1), false)
            };
            Context.AddRange(bookings);
            Context.SaveChanges();
                
            var returnedValue = Repository.Search(ReportModel);
            Assert.IsTrue(returnedValue.All(report => report.TouristSpotId == TouristSpots.ElementAt(0).Id));
        }

        [TestMethod]
        public void ValidSearchIgnoresLodgingsWithOnlyInvalidBookings()
        {
            var lodgings = new List<Lodging>
            {
                CreateLodging(1, TouristSpots.ElementAt(0)), CreateLodging(2, TouristSpots.ElementAt(1))
            };
            Context.AddRange(lodgings);
            var bookings = new List<Booking>
            {
                CreateBooking(1, lodgings.ElementAt(0), true), CreateBooking(2, lodgings.ElementAt(1), false)
            };
            Context.AddRange(bookings);
            Context.SaveChanges();
                
            var returnedValue = Repository.Search(ReportModel);

            Assert.IsFalse(returnedValue.Any());
        }

        [TestMethod]
        public void ValidSearchIgnoresLodgingsWithoutBookings()
        {
            var lodgings = new List<Lodging>
            {
                CreateLodging(1, TouristSpots.ElementAt(0)), CreateLodging(2, TouristSpots.ElementAt(0))
            };
            Context.AddRange(lodgings);
            var bookings = new List<Booking>
            {
                CreateBooking(1, lodgings.ElementAt(0), false), CreateBooking(2, lodgings.ElementAt(1), true)
            };
            Context.AddRange(bookings);
            Context.SaveChanges();
                
            var returnedValue = Repository.Search(ReportModel);

            Assert.AreEqual(returnedValue.Count, 1);
        }

        [TestMethod]
        public void ValidSearchSortsByValidBookings()
        {
            var lodgings = new List<Lodging>
            {
                CreateLodging(1, TouristSpots.ElementAt(0)), CreateLodging(2, TouristSpots.ElementAt(0)),
                CreateLodging(3, TouristSpots.ElementAt(0)), CreateLodging(4, TouristSpots.ElementAt(1))
            };
            Context.AddRange(lodgings);
            var bookings = new List<Booking>
            {
                CreateBooking(1, lodgings.ElementAt(0), true), CreateBooking(2, lodgings.ElementAt(1), false),
                CreateBooking(3, lodgings.ElementAt(1), false), CreateBooking(4, lodgings.ElementAt(2), false),
                CreateBooking(5, lodgings.ElementAt(2), true)
            };
            Context.AddRange(bookings);
            Context.SaveChanges();
            
            var returnedValue = Repository.Search(ReportModel);

            var expectedList = new List<int> {2, 3};
            Assert.IsTrue(expectedList.SequenceEqual(returnedValue.Select(x => x.Id)));
        }

        [TestMethod]
        public void ValidSearchInDateRangeWithoutBookingsIsEmpty()
        {
            var lodgings = new List<Lodging>
            {
                CreateLodging(1, TouristSpots.ElementAt(0)), CreateLodging(2, TouristSpots.ElementAt(0)),
            };
            Context.AddRange(lodgings);
            var bookings = new List<Booking>
            {
                CreateBooking(1, lodgings.ElementAt(0), false), CreateBooking(2, lodgings.ElementAt(1), false),
            };
            Context.AddRange(bookings);
            Context.SaveChanges();
            
            ReportModel = new ReportModel
            {
                TouristSpot = 1,
                CheckIn = DateTime.Now.AddDays(10),
                CheckOut = DateTime.Now.AddDays(15)
            };
            var returnedValue = Repository.Search(ReportModel);
            
            Assert.IsFalse(returnedValue.Any());
        }

        [TestMethod]
        public void ValidSearchInDateRangeWithOnlyOneDayInCommonHasAllLodgings()
        {
            var lodgings = new List<Lodging>
            {
                CreateLodging(1, TouristSpots.ElementAt(0)), CreateLodging(2, TouristSpots.ElementAt(0)),
            };
            Context.AddRange(lodgings);
            var bookings = new List<Booking>
            {
                CreateBooking(1, lodgings.ElementAt(0), false), CreateBooking(2, lodgings.ElementAt(1), false),
            };
            Context.AddRange(bookings);
            Context.SaveChanges();
            
            ReportModel = new ReportModel
            {
                TouristSpot = 1,
                CheckIn = DateTime.Now.AddDays(4),
                CheckOut = DateTime.Now.AddDays(5)
            };
            var returnedValue = Repository.Search(ReportModel);

            Assert.AreEqual(returnedValue.Count, lodgings.Count);
        }
    }
}
