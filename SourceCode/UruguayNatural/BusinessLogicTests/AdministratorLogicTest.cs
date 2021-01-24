using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessInterface;
using Moq;
using Domain;
using System.Linq.Expressions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using BusinessLogic;
using BusinessLogicInterface.Exceptions;

namespace BusinessLogicTests
{
    [TestClass]
    public class AdministratorLogicTest
    {
        private Mock<IRepository<Administrator>> AdministratorRepositoryMock;
        private Administrator Administrator;

        [TestInitialize]
        public void TestInitialize()
        {
            Administrator = new Administrator()
            {
                Name = "Name",
                Email = "admin@email.com",
                Password = "123123"
            };
            AdministratorRepositoryMock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
        }

        private AdministratorLogic CreateAdministratorLogic()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            unitOfWorkMock.Setup(m => m.GetAdminRepository()).Returns(AdministratorRepositoryMock.Object);
            return new AdministratorLogic(unitOfWorkMock.Object);
        }

        [TestMethod]
        public void CreateAdministratorReturnsRepositoryAdministrator()
        {
            AdministratorRepositoryMock.Setup(m => m.Add(Administrator));
            AdministratorRepositoryMock.Setup(m => m.Save());
            AdministratorRepositoryMock.Setup(m => m.Exists(It.IsAny<Expression<Func<Administrator, bool>>>())).Returns(false);

            var administratorLogic = CreateAdministratorLogic();

            Assert.AreEqual(administratorLogic.Create(Administrator), Administrator);
            AdministratorRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotUniqueException))]
        public void CreateAdministratorThrowsExceptionIfEmailAlreadyExists()
        {
            AdministratorRepositoryMock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            AdministratorRepositoryMock.Setup(m => m.Exists(It.IsAny<Expression<Func<Administrator, bool>>>())).Returns(true);

            var administratorLogic = CreateAdministratorLogic();
            administratorLogic.Create(Administrator);
        }

        [TestMethod]
        public void UpdateAdministratorCallsRepositoryMethods()
        {
            Administrator.Id = 5;
            AdministratorRepositoryMock.Setup(m => m.Get(Administrator.Id)).Returns(Administrator);
            AdministratorRepositoryMock.Setup(m => m.Update(Administrator));
            AdministratorRepositoryMock.Setup(m => m.Save());
            AdministratorRepositoryMock.Setup(m => m.Exists(It.IsAny<Expression<Func<Administrator, bool>>>()))
                .Returns(false);

            var administratorLogic = CreateAdministratorLogic();

            administratorLogic.Update(Administrator.Id, Administrator);

            AdministratorRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotUniqueException))]
        public void UpdateAdministratorThrowsExceptionIfEmailNotUnique()
        {
            Administrator.Id = 5;
            AdministratorRepositoryMock.Setup(m => m.Get(Administrator.Id)).Returns(Administrator);
            AdministratorRepositoryMock.Setup(m => m.Exists(It.IsAny<Expression<Func<Administrator, bool>>>())).Returns(true);

            var administratorLogic = CreateAdministratorLogic();
            
            administratorLogic.Update(Administrator.Id, Administrator);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void UpdateAdministratorThrowsExceptionIfIdNotExists()
        {
            Administrator.Id = 5;
            const int DIFFERENT_ID = 999;
            AdministratorRepositoryMock.Setup(m => m.Get(DIFFERENT_ID)).Returns((Administrator) null);
            AdministratorRepositoryMock.Setup(m => m.Exists(It.IsAny<Expression<Func<Administrator, bool>>>()))
                .Returns(false);

            var administratorLogic = CreateAdministratorLogic();

            administratorLogic.Update(DIFFERENT_ID, Administrator);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotValidException))]
        public void UpdateAdministratorThrowsExceptionIfAdminIsInvalid()
        {
            Administrator.Id = 5;
            AdministratorRepositoryMock.Setup(m => m.Get(Administrator.Id)).Returns(Administrator);
            AdministratorRepositoryMock.Setup(m => m.Exists(It.IsAny<Expression<Func<Administrator, bool>>>())).Returns(false);

            var newAdministrator = Administrator;
            newAdministrator.Name = null;
            newAdministrator.Email = null;
            newAdministrator.Password = null;

            var administratorLogic = CreateAdministratorLogic();
                
            administratorLogic.Update(Administrator.Id, newAdministrator);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void DeleteAdministratorThrowsExceptionIfIdNotExists()
        {
            Administrator.Id = 5;
            AdministratorRepositoryMock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            AdministratorRepositoryMock.Setup(m => m.Get(Administrator.Id)).Returns((Administrator) null);

            var administratorLogic = CreateAdministratorLogic();

            administratorLogic.Delete(Administrator.Id);
        }
        
        [TestMethod]
        public void DeleteAdministratorCallsRepositoryMethods()
        {
            Administrator.Id = 5;
            AdministratorRepositoryMock.Setup(m => m.Get(Administrator.Id)).Returns(Administrator);
            AdministratorRepositoryMock.Setup(m => m.Remove(Administrator));
            AdministratorRepositoryMock.Setup(m => m.Save());

            var administratorLogic = CreateAdministratorLogic();
            
            administratorLogic.Delete(Administrator.Id);
            AdministratorRepositoryMock.VerifyAll();
        }
        
        [TestMethod]
        public void GetAllAdministratorsReturns()
        {
            var list = new Collection<Administrator> { Administrator };
            AdministratorRepositoryMock.Setup(m => m.GetAll(It.IsAny<Expression<Func<Administrator, bool>>>(),""))
                .Returns(list);

            var administratorLogic = CreateAdministratorLogic();
            
            Assert.IsTrue(administratorLogic.GetAll().SequenceEqual(list));
            AdministratorRepositoryMock.VerifyAll();
        }
    }
}
