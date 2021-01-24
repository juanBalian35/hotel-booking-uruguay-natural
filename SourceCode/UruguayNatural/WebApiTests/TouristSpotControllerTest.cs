using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using Domain;
using Model.In;
using Model.Out;
using BusinessLogicInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace WebApiTests
{
    [TestClass]
    public class TouristSpotControllerTest
    {
        private TouristSpotModel TouristSpotModel;
        private TouristSpotSearchModel TouristSpotSearchModel;
        private Category Category;
        private Mock<ITouristSpotLogic> TouristSpotLogicMock;

        [TestInitialize]
        public void TestInitialize()
        {
            Category = new Category()
            {
                Id = 1,
                Name = "Category1"
            };
            
            var fileMock = new Mock<IFormFile>();
            TouristSpotModel = new TouristSpotModel()
            {
                Name = "name",
                Description = "description",
                Image = fileMock.Object,
                Categories = new List<int> { Category.Id },
                RegionId = 1
            };
            
            TouristSpotSearchModel = new TouristSpotSearchModel()
            {
                Region = 1, 
                Categories = new[] {Category.Id}
            };
            
            TouristSpotLogicMock = new Mock<ITouristSpotLogic>(MockBehavior.Strict);

        }

        [TestMethod]
        public void TestPostTouristSpotReturnsValidModel()
        {
            var touristSpot = TouristSpotModel.ToEntity();
            touristSpot.Region = new Region { Id = touristSpot.RegionId.Value };
            
            touristSpot.TouristSpotCategories = new List<TouristSpotCategory>(){
                new TouristSpotCategory()
                {
                    CategoryId = Category.Id,
                    Category =  Category,
                    TouristSpot = touristSpot,
                    TouristSpotId = touristSpot.Id
                }};
            
            var touristSpotLogicMock = new Mock<ITouristSpotLogic>(MockBehavior.Strict);
            touristSpotLogicMock.Setup(m => m.Create(TouristSpotModel.ToEntity())).Returns(touristSpot);
            var touristSpotController = new TouristSpotController(touristSpotLogicMock.Object);
            var result = touristSpotController.Post(TouristSpotModel) as CreatedResult;
            var content = result.Value as TouristSpotBasicInfoModel;

            touristSpotLogicMock.VerifyAll();
            Assert.IsTrue(content.Equals(new TouristSpotBasicInfoModel(touristSpot)));
        }

        [TestMethod]
        public void TestPostTouristSpotHas201StatusCode()
        {
            var touristSpot = TouristSpotModel.ToEntity();
            touristSpot.Region = new Region { Id = touristSpot.RegionId.Value };
            
            touristSpot.TouristSpotCategories = new List<TouristSpotCategory>(){
                new TouristSpotCategory
                {
                    CategoryId = Category.Id,
                    Category =  Category,
                    TouristSpot = touristSpot,
                    TouristSpotId = touristSpot.Id
                }};
            
            var touristSpotLogicMock = new Mock<ITouristSpotLogic>(MockBehavior.Strict);
            touristSpotLogicMock.Setup(m => m.Create(TouristSpotModel.ToEntity())).Returns(touristSpot);
            var touristSpotController = new TouristSpotController(touristSpotLogicMock.Object);
            var result = touristSpotController.Post(TouristSpotModel) as CreatedResult;

            touristSpotLogicMock.VerifyAll();

            Assert.AreEqual(result.StatusCode, 201);
        }

        [TestMethod]
        public void TestPostTouristSpotInvalidReturnsError400()
        {
            TouristSpotModel.Name = "";
            var touristSpot = TouristSpotModel.ToEntity();

            var touristSpotLogicMock = new Mock<ITouristSpotLogic>(MockBehavior.Strict);
            touristSpotLogicMock.Setup(m => m.Create(touristSpot)).Returns(touristSpot);
            var touristSpotController = new TouristSpotController(touristSpotLogicMock.Object);

            var result = touristSpotController.Post(TouristSpotModel) as BadRequestObjectResult;

            Assert.AreEqual(result.StatusCode, 400);
        }
        
        [TestMethod]
        public void TestGetTouristSpotReturnsValidModel()
        {
            var touristSpot = TouristSpotModel.ToEntity();
            touristSpot.Region = new Region { Id = touristSpot.RegionId.Value };
            var touristSpotCategories = new TouristSpotCategory()
            {
                Category = Category,
                CategoryId = Category.Id,
                TouristSpot = touristSpot,
                TouristSpotId = touristSpot.Id
            };
            
            touristSpot.TouristSpotCategories =  new List<TouristSpotCategory>() { touristSpotCategories };
            
            var touristSpots = new List<TouristSpot> {touristSpot};

            var touristSpotLogicMock = new Mock<ITouristSpotLogic>(MockBehavior.Strict);
            touristSpotLogicMock.Setup(m => m.Search(TouristSpotSearchModel)).Returns(touristSpots);
            
            var touristSpotController = new TouristSpotController(touristSpotLogicMock.Object);
            var result = touristSpotController.Get(TouristSpotSearchModel) as OkObjectResult;
            var content = result.Value as List<TouristSpotBasicInfoModel>;

            touristSpotLogicMock.VerifyAll();
            Assert.IsTrue(content.SequenceEqual(touristSpots.Select(x => new TouristSpotBasicInfoModel(x))));
        }
        
        [TestMethod]
        public void TestGetTouristSpotHas200StatusCode()
        {
            var touristSpot = TouristSpotModel.ToEntity();
            touristSpot.Region = new Region { Id = touristSpot.RegionId.Value };
            var touristSpotCategories = new TouristSpotCategory()
            {
                Category = Category,
                CategoryId = Category.Id,
                TouristSpot = touristSpot,
                TouristSpotId = touristSpot.Id
            };
            
            touristSpot.TouristSpotCategories =  new List<TouristSpotCategory>() { touristSpotCategories };
            
            var touristSpots = new List<TouristSpot> {touristSpot};

            var touristSpotLogicMock = new Mock<ITouristSpotLogic>(MockBehavior.Strict);
            touristSpotLogicMock.Setup(m => m.Search(TouristSpotSearchModel)).Returns(touristSpots);
            
            var touristSpotController = new TouristSpotController(touristSpotLogicMock.Object);
            var result = touristSpotController.Get(TouristSpotSearchModel) as OkObjectResult;

            touristSpotLogicMock.VerifyAll();
            
            Assert.AreEqual(result.StatusCode, 200);
        }
        
        [TestMethod]
        public void TestGetTouristSpotIsBadRequestIfRegionIsNull()
        {
            TouristSpotSearchModel.Region = null;
            
            var touristSpotLogicMock = new Mock<ITouristSpotLogic>(MockBehavior.Strict);
            var touristSpotController = new TouristSpotController(touristSpotLogicMock.Object);
            var result = touristSpotController.Get(TouristSpotSearchModel) as BadRequestObjectResult;

            Assert.AreEqual(result.StatusCode, 400);
        }
        
        [TestMethod]
        public void TestGetTouristSpotByIdReturnsValidModel()
        {
            var touristSpot = TouristSpotModel.ToEntity();
            touristSpot.Region = new Region { Id = touristSpot.RegionId.Value };
            var touristSpotCategories = new TouristSpotCategory()
            {
                Category = Category,
                CategoryId = Category.Id,
                TouristSpot = touristSpot,
                TouristSpotId = touristSpot.Id
            };
            
            touristSpot.TouristSpotCategories =  new List<TouristSpotCategory>() { touristSpotCategories };

            TouristSpotLogicMock.Setup(m => m.Get(touristSpot.Id)).Returns(touristSpot);
            
            var touristSpotController = new TouristSpotController(TouristSpotLogicMock.Object);
            var result = touristSpotController.GetById(touristSpot.Id) as OkObjectResult;
            var content = result.Value as TouristSpotDetailedInfoModel;

            TouristSpotLogicMock.VerifyAll();
            Assert.AreEqual(content, new TouristSpotDetailedInfoModel(touristSpot));
        }
    }
}
