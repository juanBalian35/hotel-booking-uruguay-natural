using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;
using Moq;
using WebApi.Controllers;

namespace WebApiTests
{
    [TestClass]
    public class ImportControllerTest
    {
        private ImportLodgingModel ImportLodgingModel;
        private Mock<IImportLogic> ImportLogicMock;
        
        [TestInitialize]
        public void TestInitialize()
        {
            var fileMock = new Mock<IFormFile>();
            ImportLodgingModel = new ImportLodgingModel
            {
                Format = "Json",
                File = fileMock.Object
            };
            
            ImportLogicMock = new Mock<IImportLogic>();
        }
        
        [TestMethod]
        public void GetReturnsValueFromImportLogic()
        {
            var names = new List<string> { "Name1", "Name2" };
            ImportLogicMock.Setup(m => m.GetFormatNames()).Returns(names);
            var importController = new ImportController(ImportLogicMock.Object);

            var response = importController.Get() as OkObjectResult;
            var responseValue = response.Value as ICollection<string>;
            
            ImportLogicMock.VerifyAll();
            Assert.IsTrue(responseValue.SequenceEqual(names));
        }
        
        [TestMethod]
        public void GetReturns200IfValid()
        {
            var names = new List<string> { "Name1" };
            ImportLogicMock.Setup(m => m.GetFormatNames()).Returns(names);
            var importController = new ImportController(ImportLogicMock.Object);

            var response = importController.Get() as OkObjectResult;
            
            ImportLogicMock.VerifyAll();
            Assert.AreEqual(response.StatusCode, 200);
        }
        
        [TestMethod]
        public void PostReturns204IfValid()
        {
            ImportLogicMock.Setup(m => m.Import(ImportLodgingModel));
            var importController = new ImportController(ImportLogicMock.Object);

            var response = importController.Post(ImportLodgingModel) as NoContentResult;
            
            ImportLogicMock.VerifyAll();
            Assert.AreEqual(response.StatusCode, 204);
        }
    }
}