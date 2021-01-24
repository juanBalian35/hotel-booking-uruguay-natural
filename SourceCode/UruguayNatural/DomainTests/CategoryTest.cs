using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Domain;

namespace DomainTests
{
    [TestClass]
    public class CategoryTest
    {
        private Category Category;

        [TestInitialize]
        public void TestInitialize()
        {
            Category = new Category();
        }

        [TestMethod]
        public void SetIdSetsId()
        {
            const int ID = 1;
            Category.Id = ID;
            
            Assert.AreEqual(Category.Id, ID);

        }
        [TestMethod]
        public void SetNameSetsName()
        {
            const string NAME = "name";
            Category.Name = NAME;
            
            Assert.AreEqual(Category.Name, NAME);
        } 
        
        [TestMethod]
        public void SetFaIconNameSetsFaIconName()
        {
            const string FA_ICON_NAME = "fas-close";
            Category.FaIconName = FA_ICON_NAME;
            
            Assert.AreEqual(Category.FaIconName, FA_ICON_NAME);
        }
        
        [TestMethod]
        public void SetTouristSpotCategorySetsTouristSpotCategory()
        {
            var touristSpotCategories = new List<TouristSpotCategory>
            {
                new TouristSpotCategory()
            };
            Category.TouristSpotCateogries = touristSpotCategories;
            
            Assert.AreEqual(Category.TouristSpotCateogries, touristSpotCategories);
        }
    }
}
