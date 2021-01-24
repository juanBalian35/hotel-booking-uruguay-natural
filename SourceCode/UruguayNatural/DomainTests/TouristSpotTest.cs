using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Domain;

namespace DomainTests
{
    [TestClass]
    public class TouristSpotTest
    {
        private TouristSpot TouristSpot;

        [TestInitialize]
        public void TestInitialize()
        {
            var category = new Category()
            {
                Id = 1,
                Name = "category 1"
            };
            var region = new Region()
            {
                Id = 1,
                Name = "region 1"
            };
            var tcs = new List<TouristSpotCategory>(){new TouristSpotCategory(){CategoryId = category.Id, Category = category}};
            TouristSpot = new TouristSpot()
            {
                Id = 1,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                RegionId = region.Id,
                Region = region,
                TouristSpotCategories = tcs
            };
        }

        [TestMethod]
        public void ValidTouristSpotIsValid()
        {
            Assert.IsFalse(TouristSpot.Validate().HasErrors());
        }

        [TestMethod]
        public void NameCannotBeNull()
        {
            TouristSpot.Name = null;
            Assert.IsTrue(TouristSpot.Validate().HasErrors());
        }

        [TestMethod]
        public void NameCannotBeEmpty()
        {
            TouristSpot.Name = "  ";
            Assert.IsTrue(TouristSpot.Validate().HasErrors());
        }

        [TestMethod]
        public void ImageCannotBeNull()
        {
            TouristSpot.Image = null;
            Assert.IsTrue(TouristSpot.Validate().HasErrors());
        }

        [TestMethod]
        public void DescriptionCannotBeNull()
        {
            TouristSpot.Description = null;
            Assert.IsTrue(TouristSpot.Validate().HasErrors());
        }

        [TestMethod]
        public void DescriptionCannotBeEmpty()
        {
            TouristSpot.Description = "  ";
            Assert.IsTrue(TouristSpot.Validate().HasErrors());
        }

        [TestMethod]
        public void DescriptionHasToHaveLessThan2000Characters()
        {
            var longString = new string('a', 2001);

            TouristSpot.Description = longString;
            Assert.IsTrue(TouristSpot.Validate().HasErrors());
        }
        
        [TestMethod]
        public void RegionIdCannotBeNull()
        {
            TouristSpot.RegionId = null;
            Assert.IsTrue(TouristSpot.Validate().HasErrors());
        }

        [TestMethod]
        public void TouristSpotCategoryHasToHaveAtLeast1Category()
        {

            TouristSpot.TouristSpotCategories = new List<TouristSpotCategory>();
            Assert.IsTrue(TouristSpot.Validate().HasErrors());
        }
        
        [TestMethod]
        public void TouristSpotCategoryIsNull()
        {
            TouristSpot.TouristSpotCategories = null; 
            Assert.IsFalse(TouristSpot.Validate().HasErrors());
        }
        
        [TestMethod]
        public void DoesNotEqualWithDifferentId()
        {
            var anotherTouristSpot = new TouristSpot
            {
                Id = 2,
                Name = TouristSpot.Name,
                Description = TouristSpot.Description,
                Image = TouristSpot.Image,
               TouristSpotCategories = TouristSpot.TouristSpotCategories,
                Region = TouristSpot.Region
            };

            Assert.IsFalse(TouristSpot.Equals(anotherTouristSpot));
        }

        [TestMethod]
        public void DoesNotEqualOtherTpye()
        {
            Assert.IsFalse(TouristSpot.Equals("String"));
        }
    }
}
