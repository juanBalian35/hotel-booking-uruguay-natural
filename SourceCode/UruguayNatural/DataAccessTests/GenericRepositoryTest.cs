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
    public class GenericRepositoryTest
    {
        private DbContext Context;
        private DbContextOptions Options;
        private List<TouristSpot> TouristSpots;

        [TestInitialize]
        public void Setup()
        {
            TouristSpots = new List<TouristSpot>();
            
            Options = new DbContextOptionsBuilder<UruguayNaturalContext>()
                .UseInMemoryDatabase("UruguayNaturalDBTest").Options;
            Context = new UruguayNaturalContext(Options);

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
            var touristSpot = new TouristSpot
            {
                Id = id,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                Region = region,
                TouristSpotCategories = new List<TouristSpotCategory>()
            };
            
            var tsc = new TouristSpotCategory
            {
                CategoryId = category.Id, 
                Category = category, 
                TouristSpotId = touristSpot.Id,
                TouristSpot = touristSpot
            };
            touristSpot.TouristSpotCategories.Add(tsc);
            
            return touristSpot;
        }
        
        [TestCleanup]
        public void TestCleanup()
        {
            Context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetFirstReturnsFirstThatSatisfiesCondition()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            Context.SaveChanges();
            var repository = new GenericRepository<TouristSpot>(Context);

            Assert.AreEqual(repository.GetFirst(x => x.Id == TouristSpots.ElementAt(0).Id), TouristSpots.ElementAt(0));
        }  

        [TestMethod]
        public void GetFirstReturnsNullIfConditionIsNotSatisfied()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            Context.SaveChanges();
            var repository = new GenericRepository<TouristSpot>(Context);

            Assert.IsNull(repository.GetFirst(x => x.Id == 99999));
        }  
        
        [TestMethod]
        public void GetAllReturnsAll()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            Context.SaveChanges();
            var repository = new GenericRepository<TouristSpot>(Context);

            Assert.IsTrue(TouristSpots.SequenceEqual(repository.GetAll()));
        }  
        
        [TestMethod]
        public void GetAllWithInclude()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            Context.SaveChanges();
            var repository = new GenericRepository<TouristSpot>(Context);
            var actual = repository.GetAll(null, "TouristSpotCategories.Category");
            Assert.IsNotNull(actual.First().TouristSpotCategories.First().Category);
        }

        [TestMethod]
        public void GetAllWithFilterReturnsOnlyValid()
        {
            TouristSpots.ForEach(m => Context.Add(m));
            Context.SaveChanges();
            var repository = new GenericRepository<TouristSpot>(Context);

            var expected = TouristSpots.Where(a => a.Name == "name");
            var actual = repository.GetAll(a => a.Name == "name");

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void GetAllPaginatedReturnsCount()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            Context.SaveChanges();
            var repository = new GenericRepository<TouristSpot>(Context);

            Assert.AreEqual(repository.GetAllWithPagination(null, "", 1, 1).Count, 1);
        }  
        
        [TestMethod]
        public void GetAllPaginatedWithInclude()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            Context.SaveChanges();
            var repository = new GenericRepository<TouristSpot>(Context);
            var actual = repository.GetAllWithPagination(null, "TouristSpotCategories.Category");
            Assert.IsNotNull(actual.First().TouristSpotCategories.First().Category);
        }

        [TestMethod]
        public void GetAllPaginatedWithFilterReturnsOnlyValid()
        {
            TouristSpots.ForEach(m => Context.Add(m));
            Context.SaveChanges();
            var repository = new GenericRepository<TouristSpot>(Context);

            var expected = TouristSpots.Where(a => a.Name == "name");
            var actual = repository.GetAllWithPagination(a => a.Name == "name");

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void GetReturnsAdministrator()
        {
            var expected = TouristSpots[0];

            Context.Add(expected);
            Context.SaveChanges();
            var repository = new GenericRepository<TouristSpot>(Context);

            var actual = repository.Get(expected.Id);

            Assert.AreEqual(expected, actual);
        }

       [TestMethod] 
       public void AddIncrementsCount()
       {
            var repository = new GenericRepository<TouristSpot>(Context);

            repository.Add(TouristSpots[0]);
            repository.Save();

            Assert.AreEqual(1, repository.GetAll().Count());
        }

       [TestMethod] 
       public void UpdateChangesField()
       {
            Context.Add(TouristSpots[0]);
            Context.SaveChanges();

            var expectedName = "new name";
            TouristSpots[0].Name = expectedName;

            var repository = new GenericRepository<TouristSpot>(Context);
            repository.Update(TouristSpots[0]);
            repository.Save();

            Assert.AreEqual(expectedName, repository.Get(TouristSpots[0].Id).Name);
        }

        [TestMethod]
        public void RemoveDecreasesCount()
        {
            Context.Add(TouristSpots[0]);
            Context.SaveChanges();

            var repository = new GenericRepository<TouristSpot>(Context);

            repository.Remove(TouristSpots[0]);
            repository.Save();

            Assert.AreEqual(0, repository.GetAll().Count());
        }

        [TestMethod]
        public void ExistsReturnsTrueIfAtLeastOneElementExists()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            Context.SaveChanges();
            var repository = new GenericRepository<TouristSpot>(Context);

            Assert.IsTrue(repository.Exists(x => x.Name == "name"));
        }
        
        [TestMethod]
        public void ExistsReturnsFalseIfAtLeastNoElementExists()
        {
            TouristSpots.ForEach(ts => Context.Add(ts));
            Context.SaveChanges();
            var repository = new GenericRepository<TouristSpot>(Context);

            Assert.IsFalse(repository.Exists(x => x.Name == "not existing name"));
        }
    }
}
