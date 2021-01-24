using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessInterface;
using Moq;
using Domain;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;
using BusinessLogic;
using BusinessLogicInterface.Exceptions;
using Model.In;

namespace BusinessLogicTests
{
    [TestClass]
    public class TouristSpotLogicTest
    {
        private TouristSpot TouristSpot;
        private Mock<IRepository<Region>> RegionsMock;
        private Mock<ITouristSpotRepository> TouristSpotsMock;
        private Mock<IRepository<Category>> CategoriesMock;

        [TestInitialize]
        public void TestInitialize()
        { 
            TouristSpotsMock = new Mock<ITouristSpotRepository>(MockBehavior.Strict);
            RegionsMock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            CategoriesMock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            
            var category = new Category
            {
                Id = 1,
                Name = "category 1"
            };

            var region = new Region
            {
                Id = 1,
                Name = "region 1"
            };
            
            TouristSpot = new TouristSpot()
            {
                Id = 1,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                Region = region,
                RegionId = region.Id,
                TouristSpotCategories = new List<TouristSpotCategory>()
            };

            var touristSpotCategory = new TouristSpotCategory
            {
                CategoryId = category.Id,
                Category = category,
                TouristSpotId = TouristSpot.Id,
                TouristSpot = TouristSpot
            };
        
            TouristSpot.TouristSpotCategories.Add(touristSpotCategory);
        }

        private TouristSpotLogic CreateTouristSpotLogic()
        {
            var repositoryFactoryMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            repositoryFactoryMock.Setup(m => m.GetTouristSpotRepository()).Returns(TouristSpotsMock.Object);
            repositoryFactoryMock.Setup(m => m.GetRegionRepository()).Returns(RegionsMock.Object);
            repositoryFactoryMock.Setup(m => m.GetCategoryRepository()).Returns(CategoriesMock.Object);
            return new TouristSpotLogic(repositoryFactoryMock.Object);
        }

        [TestMethod]
        public void CreateTouristSpotReturnsRepositoryValues()
        {
            TouristSpotsMock.Setup(m => m.Add(TouristSpot));
            TouristSpotsMock.Setup(m => m.Save());
            TouristSpotsMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<TouristSpot, bool>>>(),
                    "TouristSpotCategories.Category"))
                .Returns(TouristSpot);
           
            RegionsMock.Setup(m => m.Get(TouristSpot.Region.Id)).Returns(TouristSpot.Region);

            CategoriesMock.Setup(m => m.Get(TouristSpot.TouristSpotCategories.First().CategoryId))
                .Returns(TouristSpot.TouristSpotCategories.First().Category);

            var touristSpotLogic = CreateTouristSpotLogic();

            Assert.AreEqual(touristSpotLogic.Create(TouristSpot), TouristSpot);
            TouristSpotsMock.VerifyAll();
            CategoriesMock.VerifyAll();
            RegionsMock.VerifyAll(); 
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CreateTouristSpotThrowsExceptionIfRegionIsNotFound()
        {
            RegionsMock.Setup(m => m.Get(TouristSpot.Region.Id)).Returns((Region) null);

            var touristSpotLogic = CreateTouristSpotLogic();
            
            touristSpotLogic.Create(TouristSpot);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CreateTouristSpotThrowsExceptionIfCategoryIsNotFound()
        {
            RegionsMock.Setup(m => m.Get(TouristSpot.Region.Id)).Returns(TouristSpot.Region);
            CategoriesMock.Setup(m => m.Get(TouristSpot.TouristSpotCategories.First().CategoryId))
                .Returns((Category) null);        
            
            var touristSpotLogic = CreateTouristSpotLogic();
            
            touristSpotLogic.Create(TouristSpot);
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void SearchTouristSpotThrowsExceptionIfRegionIsNull()
        {
            TouristSpot.Region = null;
            RegionsMock.Setup(m => m.Get(1)).Returns(TouristSpot.Region);
            
            var touristSpotLogic = CreateTouristSpotLogic();

            var touristSpotSearchModel = new TouristSpotSearchModel
            {
                Region = 1,
                Categories = new[]{1}
            };
            
            touristSpotLogic.Search(touristSpotSearchModel);
        } 
        
        [TestMethod]
        public void SearchTouristSpotsReturnsTouristSpots()
        {
            var touristSpotSearchModel = new TouristSpotSearchModel
            {
                Region = 1,
                Categories = new[]{1}
            };
            
            RegionsMock.Setup(m => m.Get(touristSpotSearchModel.Region)).Returns(TouristSpot.Region);
            
            var touristSpots = new List<TouristSpot> { TouristSpot };
            
            TouristSpotsMock.Setup(m => m.Search((int) touristSpotSearchModel.Region,
                It.IsAny<List<int>>(), touristSpotSearchModel.Page,
                touristSpotSearchModel.ResultsPerPage)).Returns(touristSpots);
            
            var touristSpotLogic = CreateTouristSpotLogic();
            
            var result = touristSpotLogic.Search(touristSpotSearchModel);
            
            TouristSpotsMock.VerifyAll(); 
            RegionsMock.VerifyAll();

            Assert.IsTrue(result.SequenceEqual(touristSpots));
        }
        [TestMethod]
        public void GetTouristSpotCallsRepositoryMethods()
        {
            
            TouristSpotsMock.Setup(m => m.GetFirst(
                    It.IsAny<Expression<Func<TouristSpot, bool>>>(),"TouristSpotCategories.Category"))
                .Returns(TouristSpot);

            var touristSpotLogic = CreateTouristSpotLogic();

            touristSpotLogic.Get(TouristSpot.Id);

            TouristSpotsMock.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetTouristSpotThrowsExceptionWith404StatusIfNotFound()
        { 
            TouristSpotsMock.Setup(m => m.GetFirst(
                    It.IsAny<Expression<Func<TouristSpot, bool>>>(),"TouristSpotCategories.Category"))
                .Returns((TouristSpot) null);

            var touristSpotLogic = CreateTouristSpotLogic();

            touristSpotLogic.Get(TouristSpot.Id);
        }
    }
}