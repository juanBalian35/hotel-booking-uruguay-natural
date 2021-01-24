using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using Model.In;
using Model.Out;
using BusinessLogicInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;

namespace WebApiTests
{
    [TestClass]
    public class SessionControllerTest
    {
        private LogInModel LogInModel;

        [TestInitialize]
        public void TestInitialize()
        {
            LogInModel = new LogInModel
            {
                Email = "email@mail.com",
                Password = "changeme"
            };
        }

        [TestMethod]
        public void TestPostSessionReturnsValidGuid()
        {
            var sessionLogicMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            sessionLogicMock.Setup(m => m.CreateSession(LogInModel.Email, LogInModel.Password)).Returns(Guid.NewGuid());
            var sessionController = new SessionController(sessionLogicMock.Object);

            var result = sessionController.Post(LogInModel) as OkObjectResult;
            var content = result.Value as TokenModel;
            
            sessionLogicMock.VerifyAll();
            Assert.IsInstanceOfType(content.Token, typeof(Guid));
        }

        [TestMethod]
        public void TestPostSessionHas200StatusCode()
        {
            var sessionLogicMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            sessionLogicMock.Setup(m => m.CreateSession(LogInModel.Email, LogInModel.Password)).Returns(Guid.NewGuid());
            var sessionController = new SessionController(sessionLogicMock.Object);

            var result = sessionController.Post(LogInModel) as OkObjectResult;
            
            sessionLogicMock.VerifyAll();
            Assert.AreEqual(result.StatusCode, 200);
        }
    }
}
