using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;
using Domain;

namespace ModelTests.Out
{
    [TestClass]
    public class TouristSpotNoAssociationsModelTest
    {
        private TouristSpotNoAssociationsModel TouristSpotNoAssociationsModel;
        private TouristSpot TouristSpot;

        [TestInitialize]
        public void TestInitialize()
        {
            TouristSpot = new TouristSpot()
            {
                Id = 1,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20}
            };
            TouristSpotNoAssociationsModel = new TouristSpotNoAssociationsModel(TouristSpot);
        }

        [TestMethod]
        public void ConstructorCreatesTouristSpotBasicInfoModelWithSameId()
        {
            Assert.AreEqual(TouristSpot.Id, TouristSpotNoAssociationsModel.Id);
        }

        [TestMethod]
        public void ConstructorCreatesTouristSpotBasicInfoModelWithSameName()
        {
            Assert.AreEqual(TouristSpot.Name, TouristSpotNoAssociationsModel.Name);
        }
        
        [TestMethod]
        public void ConstructorCreatesTouristSpotBasicInfoModelWithSameDescription()
        {
            Assert.AreEqual(TouristSpot.Description, TouristSpotNoAssociationsModel.Description);
        }

        [TestMethod]
        public void EqualsAnotherWithSameIdAndName()
        {
            var anotherTouristSpotModel = new TouristSpotNoAssociationsModel(TouristSpot);

            Assert.IsTrue(TouristSpotNoAssociationsModel.Equals(anotherTouristSpotModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentId()
        {
            var anotherTouristSpot = TouristSpot;
            anotherTouristSpot.Id = 2;
            var anotherTouristSpotModel = new TouristSpotNoAssociationsModel(anotherTouristSpot);

            Assert.IsFalse(TouristSpotNoAssociationsModel.Equals(anotherTouristSpotModel));
        }

        [TestMethod]
        public void DoesNotEqualOtherType()
        {
            Assert.IsFalse(TouristSpotNoAssociationsModel.Equals("String"));
        }
    }
}
