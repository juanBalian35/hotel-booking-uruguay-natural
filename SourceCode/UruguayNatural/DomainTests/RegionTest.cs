using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests
{
    [TestClass]
    public class RegionTest
    {
        private Region Region;
        private byte[] ImageData;  
        
        [TestInitialize]
        public void TestInitialize()
        {
            Region = new Region();
            ImageData = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20};
        }

        [TestMethod]
        public void SetIdSetsId()
        {
            const int ID = 1;
            Region.Id = ID;
            
            Assert.AreEqual(Region.Id, ID);
        }
        
        [TestMethod]
        public void SetNameSetsName()
        {
            const string NAME = "name";
            Region.Name = NAME;
            
            Assert.AreEqual(Region.Name, NAME);
        }
        
        [TestMethod]
        public void SetDescriptionSetsDescription()
        {
            const string DESCRIPTION = "aDescription";
            Region.Description = DESCRIPTION;
            
            Assert.AreEqual(Region.Description, DESCRIPTION);
        }
        
        [TestMethod]
        public void SetVideoPAthSetsVideoPath()
        {
            const string PATH = "aPath";
            Region.VideoPath = PATH;
            
            Assert.AreEqual(Region.VideoPath, PATH);
        }
        
        [TestMethod]
        public void SetMapYellowSetsMapYellow()
        {
            Region.MapYellow = ImageData;
            
            Assert.AreEqual(Region.MapYellow, ImageData);
        }
        
        [TestMethod]
        public void SetMapTransparentSetsMapTransparent()
        {
            Region.MapTransparent = ImageData;
            
            Assert.AreEqual(Region.MapTransparent, ImageData);
        }
        
        [TestMethod]
        public void SetListOfTouristSpots()
        {
            var touristSpotList = new List<TouristSpot>
            {
                new TouristSpot
                {
                    Id = 1,
                    Name = "name",
                    Description = "description",
                    Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                    RegionId = 1
                }
            };
            Region.TouristSpots = touristSpotList;
            
            Assert.IsTrue(Region.TouristSpots.SequenceEqual(touristSpotList));
        }
    }
}
