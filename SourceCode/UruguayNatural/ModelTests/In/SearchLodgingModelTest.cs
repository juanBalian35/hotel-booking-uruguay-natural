using System;
using System.Linq;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;

namespace ModelTests.In
{
    [TestClass]
    public class SearchLodgingModelTest
    {
        private SearchLodgingModel SearchLodgingModel;
        
        [TestInitialize]
        public void Initialize()
        {
            SearchLodgingModel = new SearchLodgingModel()
            {
                Adults = 2,
                Children = 4,
                Retirees = 5,
                Babies = 1,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(3),
                TouristSpot = 3
            };
        }

        [TestMethod]
        public void ValidSearchLodgingModelIsValid()
        {
            Assert.IsFalse(SearchLodgingModel.Validate().HasErrors());
        }

        [TestMethod]
        public void ModelWithNullTouristSpotIsNotValid()
        {
            SearchLodgingModel.TouristSpot = null;
            Assert.IsTrue(SearchLodgingModel.Validate().HasErrors());
        }

        [TestMethod]
        public void ModelWithNullCheckInDateIsNotValid()
        {
            SearchLodgingModel.CheckIn = null;
            Assert.IsTrue(SearchLodgingModel.Validate().HasErrors());
        }

        [TestMethod]
        public void ModelWithNullCheckOutDateIsNotValid()
        {
            SearchLodgingModel.CheckOut = null;
            Assert.IsTrue(SearchLodgingModel.Validate().HasErrors());
        }

        [TestMethod]
        public void ModelWithLessCheckInDateThanCheckOutIsInvalid()
        {
            SearchLodgingModel.CheckOut = DateTime.Now;
            SearchLodgingModel.CheckIn = DateTime.Now.AddDays(3);
            Assert.IsTrue(SearchLodgingModel.Validate().HasErrors());
        }

        [TestMethod]
        public void ModelWithLessThan0BabiesIsInvalid()
        {
            SearchLodgingModel.Babies = -1;
            Assert.IsTrue(SearchLodgingModel.Validate().HasErrors());
        }

        [TestMethod]
        public void ModelWithLessThan0ChildrenIsInvalid()
        {
            SearchLodgingModel.Children = -5;
            Assert.IsTrue(SearchLodgingModel.Validate().HasErrors());
        }

        [TestMethod]
        public void ModelWithLessThan0AdultsIsInvalid()
        {
            SearchLodgingModel.Adults = -3;
            Assert.IsTrue(SearchLodgingModel.Validate().HasErrors());
        }
        
        [TestMethod]
        public void SetPageSetsPage()
        {
            const int PAGE = 3;
            SearchLodgingModel.Page = PAGE;
            Assert.AreEqual(SearchLodgingModel.Page, PAGE);
        }
        
        [TestMethod]
        public void SetResultsPerPageSetsResultsPerPage()
        {
            const int RESULTS_PER_PAGE = 3;
            SearchLodgingModel.ResultsPerPage = RESULTS_PER_PAGE;
            Assert.AreEqual(SearchLodgingModel.ResultsPerPage, RESULTS_PER_PAGE);
        }

        [TestMethod]
        public void GetGuestsReturnsAsManyAdminsAsSpecified()
        {
            var numChildren = SearchLodgingModel.GetGuests().Sum(a => a is Adult ? a.Quantity : 0);
            Assert.AreEqual(SearchLodgingModel.Adults, numChildren);
        }

        [TestMethod]
        public void GetGuestsReturnsAsManyChildrenAsSpecified()
        {
            var numChildren = SearchLodgingModel.GetGuests().Sum(c => c is Child ? c.Quantity : 0);
            Assert.AreEqual(SearchLodgingModel.Children, numChildren);
        }

        [TestMethod]
        public void GetGuestsReturnsAsManyBabiesAsSpecified()
        {
            var numBabies = SearchLodgingModel.GetGuests().Sum(b => b is Baby ? b.Quantity : 0);
            Assert.AreEqual(SearchLodgingModel.Babies, numBabies);
        }

        [TestMethod]
        public void GetGuestsReturnsAsManyRetireesAsSpecified()
        {
            var numRetirees = SearchLodgingModel.GetGuests().Sum(r => r is Retiree ? r.Quantity : 0);
            Assert.AreEqual(SearchLodgingModel.Retirees, numRetirees);
        }
    }
}
