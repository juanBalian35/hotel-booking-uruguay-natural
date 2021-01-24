using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using Model.In;
using Model.Out;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApiTests
{
    [TestClass]
    public class AdministratorControllerTest
    {
        private AdministratorModel AdministratorModel;

        [TestInitialize]
        public void TestInitialize()
        {
            AdministratorModel = new AdministratorModel()
            {
                Name = "name",
                Email = "email@mail.com",
                Password = "changeme"
            };
        }

        [TestMethod]
        public void PostAdministratorHas201StatusCode()
        {
            var administrator = AdministratorModel.ToEntity();

            var administratorLogicMock = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            administratorLogicMock.Setup(m => m.Create(administrator)).Returns(administrator);
            var administratorController = new AdministratorController(administratorLogicMock.Object);

            var result = administratorController.Post(AdministratorModel) as CreatedResult;

            administratorLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 201);
        }

        [TestMethod]
        public void PostAdministratorReturnsValidModel()
        {
            var administrator = AdministratorModel.ToEntity();

            var administratorLogicMock = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            administratorLogicMock.Setup(m => m.Create(administrator)).Returns(administrator);
            var administratorController = new AdministratorController(administratorLogicMock.Object);

            var result = administratorController.Post(AdministratorModel) as CreatedResult;
            var content = result.Value as AdministratorBasicInfoModel;

            administratorLogicMock.VerifyAll();
            Assert.IsTrue(content.Equals(new AdministratorBasicInfoModel(administrator)));
        }

        [TestMethod]
        public void PostAdministratorInvalidReturnsError400()
        {
            AdministratorModel.Email = "bad email";
            var administrator = AdministratorModel.ToEntity();

            var administratorLogicMock = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            administratorLogicMock.Setup(m => m.Create(administrator)).Returns(administrator);
            var administratorController = new AdministratorController(administratorLogicMock.Object);

            var result = administratorController.Post(AdministratorModel) as BadRequestObjectResult;

            Assert.AreEqual(result.StatusCode, 400);
        }

        [TestMethod]
        public void PutAdministratorReturnsValidModel()
        {
            var administrator = AdministratorModel.ToEntity();
            var id = 5;

            var mock = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Update(id, administrator)).Returns(administrator);
            var administratorController = new AdministratorController(mock.Object);

            var result = administratorController.Put(id, AdministratorModel) as OkObjectResult;
            var content = result.Value as AdministratorBasicInfoModel;

            mock.VerifyAll();
            Assert.IsTrue(content.Equals(new AdministratorBasicInfoModel(administrator)));
        }

        [TestMethod]
        public void PutAdministratorHas200StatusCode()
        {
            var administrator = AdministratorModel.ToEntity();
            var id = 5;

            var administratorLogicMock = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            administratorLogicMock.Setup(m => m.Update(id, administrator)).Returns(administrator);
            var administratorController = new AdministratorController(administratorLogicMock.Object);

            var result = administratorController.Put(id, AdministratorModel) as OkObjectResult;

            administratorLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 200);
        }

        [TestMethod]
        public void DeleteAdministratorDeletesAdministratorWithProvidedId()
        { 
            var administrator = AdministratorModel.ToEntity();

            var administratorLogicMock = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            administratorLogicMock.Setup(m => m.Delete(administrator.Id));
            
            var administratorController = new AdministratorController(administratorLogicMock.Object);
            var result = administratorController.Delete(administrator.Id) as NoContentResult;
            
            administratorLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 204);
        }

        [TestMethod]
        public void GetAllAdministratorReturnsAdminsInLogic()
        { 
            var administrators = new List<Administrator>{ AdministratorModel.ToEntity() };
            var administratorsModel = administrators.Select(adm => new AdministratorBasicInfoModel(adm));

            var administratorLogicMock = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            administratorLogicMock.Setup(m => m.GetAll()).Returns(administrators);
            
            var administratorController = new AdministratorController(administratorLogicMock.Object);
            var result = administratorController.Get() as OkObjectResult;
            var content = result.Value as List<AdministratorBasicInfoModel>;
            
            Assert.IsTrue(content.SequenceEqual(administratorsModel));
            administratorLogicMock.VerifyAll();
        }
    }
}
