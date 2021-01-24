using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BusinessLogic;
using BusinessLogicInterface;
using BusinessLogicInterface.Exceptions;
using DataAccessInterface;
using Domain;
using Import;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;
using Moq;

namespace BusinessLogicTests
{
    // In order to test this class we are using a JSON Parser dummy implementation.
    [TestClass]
    public class ImportLogicTest
    {
        private Mock<ILodgingLogic> LodgingLogicMock;
        private Mock<ITouristSpotRepository> TouristSpotRepositoryMock;
        private string ValidJson;
        
        [TestInitialize]
        public void TestInitialize()
        { 
            LodgingLogicMock = new Mock<ILodgingLogic>();
            TouristSpotRepositoryMock = new Mock<ITouristSpotRepository>();
            ValidJson = @"
                [
                  {
                    ""Name"": ""aName"",
                    ""Description"": ""description"",
                    ""Rating"": 3,
                    ""PricePerNight"": 120.0,
                    ""Address"": ""Address 12345"",
                    ""Phone"": ""+598 98 399 344"",
                    ""ConfirmationMessage"": ""Successful"",
                    ""TouristSpot"": {
                      ""Name"": ""touristSpot"",
                      ""Description"": ""aDescription"",
                      ""RegionId"": 2
                    }
                  },
                  {
                    ""Name"": ""anotherName"",
                    ""Description"": ""description"",
                    ""Rating"": 5,
                    ""PricePerNight"": 90.5,
                    ""Address"": ""Address 1234567"",
                    ""Phone"": ""+598 98 129 934"",
                    ""ConfirmationMessage"": ""Successful"",
                    ""TouristSpot"": {
                      ""Name"": ""anotherTouristSpot"",
                      ""Description"": ""anotherDescription"",
                      ""RegionId"": 2
                    }
                  }
                ]
            ";
        }

        private static FormFile CreateFormFile(string content)
        {
            var contentBytes = Encoding.UTF8.GetBytes(content);
            return new FormFile(new MemoryStream(contentBytes), 0, contentBytes.Length, "placeholder", "placeholder");
        }

        private ImportLogic CreateImportLogic()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            unitOfWorkMock.Setup(m => m.GetTouristSpotRepository()).Returns(TouristSpotRepositoryMock.Object);
            return new ImportLogic(LodgingLogicMock.Object, unitOfWorkMock.Object);
        }

        [TestMethod]
        public void GetNameReturnsValidNames()
        {
            var importLogic = CreateImportLogic();
            Assert.IsTrue(new List<string> { "Json" }.SequenceEqual(importLogic.GetFormatNames()));
        }

        [TestMethod]
        public void ImportValidCreatesSpecifiedEntitiesInDatabase()
        {
            TouristSpotRepositoryMock.Setup(m => m.Add(It.IsAny<TouristSpot>()));
            TouristSpotRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<TouristSpot, bool>>>(),""))
                .Returns((TouristSpot)null);
            LodgingLogicMock.Setup(m => m.Create(It.IsAny<Lodging>()));
            
            var importLogic = CreateImportLogic();
            var input = new ImportLodgingModel
            {
                Format = "Json",
                File = CreateFormFile(ValidJson)
            };
            importLogic.Import(input);

            TouristSpotRepositoryMock.VerifyAll();
            LodgingLogicMock.VerifyAll();
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void ImportWithInvalidNameNotFoundException()
        {
            var importLogic = CreateImportLogic();
            var input = new ImportLodgingModel
            {
                Format = "Invalid",
                File = CreateFormFile(ValidJson)
            };
            importLogic.Import(input);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ParsingNotValidTypeException))]
        public void ImportFileWithInvalidFormatThrowsException()
        {
            var importLogic = CreateImportLogic();
            var input = new ImportLodgingModel
            {
                Format = "Json",
                File = CreateFormFile("invalid json")
            };
            importLogic.Import(input);
        }
        
        [TestMethod]
        [ExpectedException(typeof(EntityNotValidException))]
        public void ImportWithInvalidTouristSpotThrowsEntityNotValidException()
        {
            TouristSpotRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<TouristSpot, bool>>>(),""))
                .Returns((TouristSpot)null);
            
            var importLogic = CreateImportLogic();
            const string JSON = "[{\"Name\": \"aLodgingName\",{\"TouristSpot\": {\"Name\": \"invalidTouristSpot\"}}]";
            var input = new ImportLodgingModel
            {
                Format = "Json",
                File = CreateFormFile(JSON)
            };
            importLogic.Import(input);
        }
        
        [TestMethod]
        [ExpectedException(typeof(EntityNotValidException))]
        public void ImportWithInvalidLodgingThrowsEntityNotValidException()
        {
            TouristSpotRepositoryMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<TouristSpot, bool>>>(),""))
                .Returns((TouristSpot)null);
            
            var importLogic = CreateImportLogic();
            const string JSON =
                "[{\"Name\": \"aLodgingName\", \"TouristSpot\": " + 
                "{\"Name\": \"touristSpot\",\"Description\": \"aDescription\",\"RegionId\": 2}}]";
            var input = new ImportLodgingModel
            {
                Format = "Json",
                File = CreateFormFile(JSON)
            };
            importLogic.Import(input);
        }
    }
}