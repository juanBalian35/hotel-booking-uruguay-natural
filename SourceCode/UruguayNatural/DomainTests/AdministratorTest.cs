using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Domain;

namespace DomainTests
{
    [TestClass]
    public class AdministratorTest
    {
        private Administrator Administrator;

        [TestInitialize]
        public void TestInitialize()
        {
            Administrator = new Administrator()
            {
                Id = 1,
                Name = "name",
                Email = "email@mail.com",
                Password = "changeme"
            };
        }

        [TestMethod]
        public void ValidAdministratorIsValid()
        {
            Assert.IsFalse(Administrator.Validate().HasErrors());
        }

        [TestMethod]
        public void NameCannotBeNull()
        {
            Administrator.Name = null;
            Assert.IsTrue(Administrator.Validate().HasErrors());
        }

        [TestMethod]
        public void NameCannotBeEmpty()
        {
            Administrator.Name = "  ";
            Assert.IsTrue(Administrator.Validate().HasErrors());
        }

        [TestMethod]
        public void EmailCannotBeNull()
        {
            Administrator.Email = null;
            Assert.IsTrue(Administrator.Validate().HasErrors());
        }

        [TestMethod]
        public void EmailHasToBeValidEmail()
        {
            Administrator.Email = "name@email";
            Assert.IsTrue(Administrator.Validate().HasErrors());
        }

        [TestMethod]
        public void PasswordCannotBeNull()
        {
            Administrator.Password = null;
            Assert.IsTrue(Administrator.Validate().HasErrors());
        }

        [TestMethod]
        public void PasswordHasToHave6CharactersOrMore()
        {
            Administrator.Password = "1234";
            Assert.IsTrue(Administrator.Validate().HasErrors());
        }

        [TestMethod]
        public void EqualsAnotherWithSameIdAndEmail()
        {
            var anotherAdministrator = new Administrator
            {
                Id = Administrator.Id,
                Email = Administrator.Email
            };

            Assert.IsTrue(Administrator.Equals(anotherAdministrator));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentEmail()
        {
            var anotherAdministrator = new Administrator
            {
                Id = Administrator.Id,
                Email = "different@email.com"
            };

            Assert.IsFalse(Administrator.Equals(anotherAdministrator));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentId()
        {
            var anotherAdministrator = new Administrator
            {
                Id = 2,
                Email = Administrator.Email
            };

            Assert.IsFalse(Administrator.Equals(anotherAdministrator));
        }

        [TestMethod]
        public void DoesNotEqualOtherTpye()
        {
            Assert.IsFalse(Administrator.Equals("String"));
        }

        [TestMethod]
        public void SetSessionsSetsSessions()
        {
            Administrator.Sessions = new List<Session>();   
            Assert.IsTrue(Administrator.Sessions != null);
        }
    }
}
