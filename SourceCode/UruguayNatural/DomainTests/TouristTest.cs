using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests
{
    [TestClass]
    public class TouristTest
    {
        private Tourist Tourist;

        [TestInitialize]
        public void TestInitialize()
        {
            Tourist = new Tourist()
            {
                Id = 3,
                Name = "aName",
                LastName = "aLastName",
                Email = "an@email.com"
            };
        }

        [TestMethod]
        public void SetNameSetsName()
        {
            const string NAME = "newName";
            Tourist.Name = NAME;
            Assert.AreEqual(Tourist.Name, NAME);
        }

        [TestMethod]
        public void SetLastNameSetsLastName()
        {
            const string LAST_NAME = "newLastName";
            Tourist.LastName = LAST_NAME;
            Assert.AreEqual(Tourist.LastName, LAST_NAME);
        }

        [TestMethod]
        public void SetEmailSetsEmail()
        {
            const string EMAIL = "new@email.com";
            Tourist.Email = EMAIL;
            Assert.AreEqual(Tourist.Email, EMAIL);
        }
    }
}