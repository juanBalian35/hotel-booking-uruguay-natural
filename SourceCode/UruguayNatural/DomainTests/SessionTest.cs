using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain;

namespace DomainTests
{
    [TestClass]
    public class SessionTest
    {
        private Session Session;

        [TestInitialize]
        public void TestInitialize()
        {
            var administrator= new Administrator()
            {
                Id = 1,
                Name = "name",
                Email = "email@mail.com",
                Password = "changeme"
            };
            Session = new Session()
            {
                Token = Guid.NewGuid(),
                Administrator = administrator
            };
        }

        [TestMethod]
        public void EqualsAnotherWithToken()
        {
            var anotherSession = new Session
            {
                Token = Session.Token,
                Administrator = Session.Administrator
            };

            Assert.IsTrue(Session.Equals(anotherSession));
        }

        [TestMethod]
        public void DoesNotEqualWithAnotherToken()
        {

            var anotherSession = new Session
            {
                Token = Guid.NewGuid(),
                Administrator = Session.Administrator
            };

            Assert.IsFalse(Session.Equals(anotherSession));
        }

        [TestMethod]
        public void DoesNotEqualOtherTpye()
        {
            Assert.IsFalse(Session.Equals("string"));
        }
    }
}
