using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using Model.In;
using Model.Out;
using BusinessLogicInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace WebApiTests
{
    [TestClass]
    public class CategoryControllerTest
    {
        private CategoryDetailedInfoModel CategoryDetailedInfoModel;
        private Category Category;

        [TestInitialize]
        public void TestInitialize()
        {
            Category = new Category
            {
                Id = 1,
                Name = "region 1",
                FaIconName = "fa-icon"
            };
            
        }

        [TestMethod]
        public void GetCategorysReturnsValuesFromLogic()
        {
            var regionsToReturn = new List<Category>
            {
                Category
            };
            
            var regionLogicMock = new Mock<ICategoryLogic>(MockBehavior.Strict);
            regionLogicMock.Setup(m => m.GetAll()).Returns(new List<Category>(){Category});
            var regionController = new CategoryController(regionLogicMock.Object);

            var result = regionController.GetAll() as OkObjectResult;
            var content = result.Value as List<CategoryDetailedInfoModel>;

            regionLogicMock.VerifyAll();
            Assert.IsTrue(content.SequenceEqual(regionsToReturn.Select(x=> new CategoryDetailedInfoModel(x))));
        }
        
        [TestMethod]
        public void ValidGetReportReturnHasStatusCode200()
        {
            var regionsToReturn = new List<Category>
            {
                Category
            };
            
            var regionLogicMock = new Mock<ICategoryLogic>(MockBehavior.Strict);
            regionLogicMock.Setup(m => m.GetAll()).Returns(new List<Category>(){Category});
            var regionController = new CategoryController(regionLogicMock.Object);

            var result = regionController.GetAll() as OkObjectResult;
            
            regionLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 200);
        }

        
    }
}
