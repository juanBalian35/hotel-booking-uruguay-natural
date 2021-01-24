using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessInterface;
using Moq;
using Domain;
using System.Linq.Expressions;
using System;
using BusinessLogic;
using BusinessLogicInterface.Exceptions;

namespace BusinessLogicTests
{
    [TestClass]
    public class SessionLogicTest
    {
        private Administrator Administrator;
        private Session Session;
        private Mock<IRepository<Session>> SessionsMock;
        private Mock<IRepository<Administrator>> AdministratorsMock;

        [TestInitialize]
        public void TestInitialize()
        {
            Administrator = new Administrator
            {
                Name = "Name",
                Email = "email@mail.com",
                Password = "123123"
            };
            Session = new Session
            {
                Token = Guid.NewGuid(),
                Administrator = Administrator
            };
            SessionsMock = new Mock<IRepository<Session>>(MockBehavior.Strict);
            AdministratorsMock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
        }

        private SessionLogic CreateSessionLogic()
        {
            var repositoryFactoryMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            repositoryFactoryMock.Setup(m => m.GetSessionRepository()).Returns(SessionsMock.Object);
            repositoryFactoryMock.Setup(m => m.GetAdminRepository()).Returns(AdministratorsMock.Object);
            return new SessionLogic(repositoryFactoryMock.Object);
        }

        [TestMethod]
        public void CreateSessionReturnsTokenIfValid()
        {
            SessionsMock.Setup(m => m.Add(It.IsAny<Session>()));
            SessionsMock.Setup(m => m.Save());

            AdministratorsMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Administrator, bool>>>(),"")).
                Returns(Administrator);

            var sessionLogic = CreateSessionLogic();
            var token = sessionLogic.CreateSession(Administrator.Email, Administrator.Password);

            Assert.IsInstanceOfType(token, typeof(Guid));
            SessionsMock.VerifyAll();
            AdministratorsMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCredentialsException))]
        public void CreateSessionWithInvalidPasswordThrowsException()
        {
            AdministratorsMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Administrator, bool>>>(),"")).
                Returns(Administrator);

            var sessionLogic = CreateSessionLogic();

            sessionLogic.CreateSession(Administrator.Email, "badpass");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCredentialsException))]
        public void CreateSessionWithEmailNotInRepositoryThrowsException()
        {
            AdministratorsMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Administrator, bool>>>(),"")).
                Returns((Administrator)null);

            var sessionLogic = CreateSessionLogic();

            sessionLogic.CreateSession("bademail", "123123");
        }

        [TestMethod]
        public void IsValidTokenReturnsTrueIfTokenInSessions()
        {
            SessionsMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Session);

            var sessionLogic = CreateSessionLogic();

            Assert.IsTrue(sessionLogic.IsValidToken(Session.Token));
            SessionsMock.VerifyAll();
        }

        [TestMethod]
        public void IsValidTokenReturnsFalseIfTokenNotInSessions()
        {
            SessionsMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns((Session)null);

            var sessionLogic = CreateSessionLogic();

            Assert.IsFalse(sessionLogic.IsValidToken(Guid.NewGuid()));
            SessionsMock.VerifyAll();
        }

        [TestMethod]
        public void GetAdministratorReturnsAdministratorForToken()
        {
            SessionsMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Session, bool>>>(),"")).Returns(Session);

            var sessionLogic = CreateSessionLogic();

            Assert.AreEqual(Administrator, sessionLogic.GetAdministrator(Session.Token));
            SessionsMock.VerifyAll();
        }

        [TestMethod]
        public void GetAdministratorReturnsNullIfThereIsNoAdministratorForToken()
        {
            SessionsMock.Setup(m => m.GetFirst(It.IsAny<Expression<Func<Session, bool>>>(),"")).Returns((Session) null);

            var sessionLogic = CreateSessionLogic();

            Assert.IsNull(sessionLogic.GetAdministrator(Guid.NewGuid()));
            AdministratorsMock.VerifyAll();
        }
    }
}
