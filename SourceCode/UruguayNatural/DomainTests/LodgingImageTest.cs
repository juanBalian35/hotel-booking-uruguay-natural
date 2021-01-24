using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace DomainTests
{
    [TestClass]
    public class LodgingImageTest
    {
        private Lodging Lodging;
        private LodgingImage LodgingImage;
        
        [TestInitialize]
        public void TestInitialize()
        {
            var touristSpot = new TouristSpot()
            {
                Id = 1,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                Region = null,
                TouristSpotCategories = new List<TouristSpotCategory>()
            };

            Lodging = new Lodging()
            {
                Id = 1,
                Name = "Name",
                Description = "Description",
                Rating = 3,
                IsFull = true,
                PricePerNight = 100,
                Address = "Valid Address 123",
                Phone = "+598 98 303 040",
                ConfirmationMessage = "Your reservation has been confirmed!",
                TouristSpot = touristSpot
            };

            LodgingImage = new LodgingImage
            {
                Id = 1,
                LodgingId = Lodging.Id,
                Lodging = Lodging,
                ImageData = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20}
            };
        }

        [TestMethod]
        public void SetLodgingIdSetsLodgingId()
        {
            const int ID = 10;
            LodgingImage.LodgingId = ID;
            
            Assert.AreEqual(LodgingImage.LodgingId, ID);
        }
        
        [TestMethod]
        public void SetLodgingSetsLodging()
        {
            LodgingImage.Lodging = Lodging;
            
            Assert.AreEqual(LodgingImage.Lodging, Lodging);
        }
        
        [TestMethod]
        public void SetImageDataSetsImageData()
        {
            var newImageData = new byte[] {0x20, 0x20, 0x20, 0x20, 0x30, 0x20, 0x20};
            LodgingImage.ImageData = newImageData;
            
            Assert.IsTrue(LodgingImage.ImageData.SequenceEqual(newImageData));
        }
    }
}
