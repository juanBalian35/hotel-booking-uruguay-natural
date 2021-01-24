using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;
using Model.Out;
using Moq;
using WebApi.Controllers;

namespace WebApiTests
{
    [TestClass]
    public class LodgingControllerTest
    {
        private LodgingModel LodgingModel;
        private LodgingReviewModel LodgingReviewModel;
        private Mock<ILodgingReviewLogic> LodgingReviewLogicMock;
        private Mock<ILodgingLogic> LodgingLogicMock;
        private LodgingController LodgingController;

        [TestInitialize]
        public void TestInitialize()
        {
            var fileMock = new Mock<IFormFile>();
            LodgingModel = new LodgingModel()
            {
                Name = "Name",
                Description = "Description",
                Rating = 3,
                IsFull = true,
                Images = new List<IFormFile> { fileMock.Object },
                PricePerNight = 100,
                Address = "Valid Address 123",
                Phone = "+598 98 303 040",
                ConfirmationMessage = "Your reservation has been confirmed!",
                TouristSpot = 1
            };
            
            LodgingReviewModel = new LodgingReviewModel
            {
                BookingId = 1,
                Rating = 3,
                Commentary = "a Comment" 
            };
            
            LodgingLogicMock = new Mock<ILodgingLogic>(MockBehavior.Strict);
            LodgingReviewLogicMock = new Mock<ILodgingReviewLogic>(MockBehavior.Strict);

            LodgingController = new LodgingController(LodgingLogicMock.Object, LodgingReviewLogicMock.Object);
        }

        [TestMethod]
        public void PostLodgingReturnsValidModel()
        {
            var lodging = LodgingModel.ToEntity();
            var lodgingToReturn = LodgingModel.ToEntity();
            lodgingToReturn.TouristSpot.Region = new Region()
            {
                Id = 3,
                Name = "region"
            };
            lodgingToReturn.TouristSpot.TouristSpotCategories = new List<TouristSpotCategory>();

            LodgingLogicMock.Setup(m => m.Create(lodging)).Returns(lodgingToReturn);

            var result = LodgingController.Post(LodgingModel) as CreatedResult;
            var content = result.Value as LodgingBasicInfoModel;

            LodgingLogicMock.VerifyAll();
            Assert.IsTrue(content.Equals(new LodgingBasicInfoModel(lodgingToReturn)));
        }
        
        [TestMethod]
        public void PostLodgingHas201StatusCode()
        {
            var lodging = LodgingModel.ToEntity();
            var lodgingToReturn = LodgingModel.ToEntity();
            lodgingToReturn.TouristSpot.Region = new Region()
            {
                Id = 3,
                Name = "region"
            };
            lodgingToReturn.TouristSpot.TouristSpotCategories = new List<TouristSpotCategory>();

            LodgingLogicMock.Setup(m => m.Create(lodging)).Returns(lodgingToReturn);

            var result = LodgingController.Post(LodgingModel) as CreatedResult;

            LodgingLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 201);
        }

        [TestMethod]
        public void PostLodgingInvalidReturnsError400()
        {
            LodgingModel.PricePerNight = -300;

            var result = LodgingController.Post(LodgingModel) as BadRequestObjectResult;
            Assert.AreEqual(result.StatusCode, 400);
        }

        [TestMethod]
        public void ModifyLodgingCapacityReturnsValidModelWithCapacityModified()
        {
            var lodging = LodgingModel.ToEntity();
            var lodgingToReturn = LodgingModel.ToEntity();
            lodgingToReturn.IsFull = !lodging.IsFull;

            LodgingLogicMock.Setup(m => m.Update(lodging.Id)).Returns(lodgingToReturn);

            var result = LodgingController.Put(lodging.Id) as OkObjectResult;
            var content = result.Value as LodgingModifiedModel;

            LodgingLogicMock.VerifyAll();
            Assert.IsTrue(content.Equals(new LodgingModifiedModel(lodgingToReturn)));
        }

        [TestMethod]
        public void ModifyLodgingCapacityHas200StatusCode()
        {
            var lodging = LodgingModel.ToEntity();
            var lodgingToReturn = LodgingModel.ToEntity();
            lodgingToReturn.IsFull = !lodging.IsFull;

            LodgingLogicMock.Setup(m => m.Update(lodging.Id)).Returns(lodgingToReturn);

            var result = LodgingController.Put(lodging.Id) as OkObjectResult;

            LodgingLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 200);
        }
        
        [TestMethod]
        public void GetLodgingReturnsValidModel()
        {
            var lodgingSearchModel = new SearchLodgingModel()
            {
                Adults = 1,
                Babies = 3,
                Children = 2,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(3),
                TouristSpot = 2
            };

            var lodgins = new List<Lodging> { LodgingModel.ToEntity()};

            LodgingLogicMock.Setup(m => m.Search(lodgingSearchModel)).Returns(lodgins);
            
            var result = LodgingController.Get(lodgingSearchModel) as OkObjectResult;
            var content = result.Value as List<LodgingSearchBasicInfoModel> ;

            LodgingLogicMock.VerifyAll();
            Assert.IsTrue(content.SequenceEqual(lodgins.Select(x => new LodgingSearchBasicInfoModel(x))));
        }
        
        [TestMethod]
        public void GetLodgingHas200StatusCode()
        {
            var lodgingSearchModel = new SearchLodgingModel()
            {
                Adults = 1,
                Babies = 3,
                Children = 2,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(3),
                TouristSpot = 2
            };

            var lodgins = new List<Lodging> { LodgingModel.ToEntity()};

            LodgingLogicMock.Setup(m => m.Search(lodgingSearchModel)).Returns(lodgins);
            
            var result = LodgingController.Get(lodgingSearchModel) as OkObjectResult;

            LodgingLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 200);
        }
        
        [TestMethod]
        public void GetLodgingWithInvalidModelReturnsBadRequest()
        {
            var lodgingSearchModel = new SearchLodgingModel()
            {
                Adults = 1,
                Babies = 3,
                Children = 2,
                TouristSpot = null
            };
            
            var result = LodgingController.Get(lodgingSearchModel) as BadRequestObjectResult;

            LodgingLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 400);
        }
        
        [TestMethod]
        public void DeleteLodgingDeletesLodgingWithProvidedId()
        { 
            var lodging = LodgingModel.ToEntity();

            LodgingLogicMock.Setup(m => m.Delete(lodging.Id));
            
            var result = LodgingController.Delete(lodging.Id) as NoContentResult;
            
            LodgingLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 204);
        }
        
        [TestMethod]
        public void GetReviewsReturnsValidModel()
        {
            var lodgingReview = LodgingReviewModel.ToEntity();
            lodgingReview.Booking = new Booking()
            {
                Id = 1,
                Tourist = new Tourist()
                {
                    Name = "aName",
                    LastName = "aLastname"
                }
            };
            var lodging = LodgingModel.ToEntity();

            LodgingReviewLogicMock.Setup(m => m.GetAllReviews(lodging.Id, 1, 1))
                .Returns(new List<LodgingReview> {lodgingReview});
            
            var result = LodgingController.GetReviews(lodging.Id, 1, 1) as OkObjectResult;
            var content = result.Value as List<LodgingReviewBasicInfoModel>;
            
            LodgingReviewLogicMock.VerifyAll();
            Assert.IsTrue(content.SequenceEqual(
                new List<LodgingReviewBasicInfoModel> {new LodgingReviewBasicInfoModel(lodgingReview)}));
        }
    }
}
