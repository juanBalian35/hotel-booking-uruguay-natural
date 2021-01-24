using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;

namespace ModelTests.In
{
    [TestClass]
    public class TouristSpotSearchModelTest
    {
        private TouristSpotSearchModel TouristSpotSearchModel;
       
        [TestInitialize]
        public void TestInitialize()
        {
            TouristSpotSearchModel = new TouristSpotSearchModel()
            {
                Region = 5,
                Categories = new[] {3}
            };
        }

        [TestMethod]
        public void SetRegionSetsRegion()
        {
            const int REGION = 1;
            TouristSpotSearchModel.Region = REGION;
            Assert.AreEqual(TouristSpotSearchModel.Region, REGION);
        }
        
        [TestMethod]
        public void SetCategoriesSetsCategories()
        {
            int[] categories = {1,2,3};
            TouristSpotSearchModel.Categories = categories;
            Assert.AreEqual(TouristSpotSearchModel.Categories, categories);
        }
        
        [TestMethod]
        public void SetPageSetsPage()
        {
            const int PAGE = 1;
            TouristSpotSearchModel.Page = PAGE;
            Assert.AreEqual(TouristSpotSearchModel.Page, PAGE);
        }
        
        [TestMethod]
        public void SetResultsPerPageSetsResultsPerPage()
        {
            const int RESULTS_PER_PAGE = 10;
            TouristSpotSearchModel.ResultsPerPage = RESULTS_PER_PAGE;
            Assert.AreEqual(TouristSpotSearchModel.ResultsPerPage, RESULTS_PER_PAGE);
        }

        [TestMethod]
        public void IsValidReturnsTrueIfTouristSpotSearchModelIsValid()
        {
            TouristSpotSearchModel.Region = 1;
            Assert.IsFalse(TouristSpotSearchModel.HasErrors());
        }
        
        [TestMethod]
        public void IsValidReturnsFalseIfTouristSpotSearchModelIsNotValid()
        {
            TouristSpotSearchModel.Region = null;
            Assert.IsTrue(TouristSpotSearchModel.HasErrors());
        }
    }
}
