using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Domain;

namespace DomainTests
{
    [TestClass]
    public class TouristSpotCategoryTest
    {
        private TouristSpotCategory TouristSpotCategory;
        private TouristSpot TouristSpot;
        private Category Category;
        
        [TestInitialize]
        public void TestInitialize()
        {
            TouristSpotCategory = new TouristSpotCategory();
            
            var category = new Category()
            {
                Id = 1,
                Name = "category 1"
            };
            
            Category = category;
            
            var region = new Region()
            {
                Id = 1,
                Name = "region 1"
            };

            TouristSpot = new TouristSpot()
            {
                Id = 1,
                Name = "name",
                Description = "description",
                Image = new byte[] {0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20},
                Region = region,
                TouristSpotCategories = new List<TouristSpotCategory>()
            };
            
            TouristSpot.TouristSpotCategories.Add(TouristSpotCategory);
        }

        [TestMethod]
        public void SetTouristSpotIdSetsTouristSpotId()
        {
            const int ID = 1;
            TouristSpotCategory.TouristSpotId = ID;
            
            Assert.AreEqual(TouristSpotCategory.TouristSpotId, ID);

        }
        [TestMethod]
        public void SetTouristSpotSetsTouristSpot()
        {
            TouristSpotCategory.TouristSpot = TouristSpot;
            
            Assert.AreEqual(TouristSpotCategory.TouristSpot, TouristSpot);
        }
        
        [TestMethod]
        public void SetCategoryIdSetsCategoryId()
        {
            const int ID = 1;
            TouristSpotCategory.CategoryId = ID;
            
            Assert.AreEqual(TouristSpotCategory.CategoryId, ID);

        }
        
        [TestMethod]
        public void SetCategorySetsCategory()
        {
            TouristSpotCategory.Category = Category;
            
            Assert.AreEqual(TouristSpotCategory.Category, Category);
        }
    }
}
