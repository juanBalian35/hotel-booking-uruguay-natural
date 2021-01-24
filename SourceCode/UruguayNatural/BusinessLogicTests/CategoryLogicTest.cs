using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessInterface;
using Moq;
using Domain;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic;


namespace BusinessLogicTests
{
    [TestClass]
    public class CategoryLogicTest
    {
        private Mock<IRepository<Category>> CategoriesMock;
        private Category Category;

        [TestInitialize]
        public void TestInitialize()
        { 
            CategoriesMock = new Mock<IRepository<Category>>(MockBehavior.Strict);

            Category = new Category
            {
                Id = 1,
                Name = "Category 1",
                FaIconName = "fas-icon"
            };
            
        }

        private CategoryLogic CreateCategoryLogic()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            unitOfWorkMock.Setup(m => m.GetCategoryRepository()).Returns(CategoriesMock.Object);
            return new CategoryLogic(unitOfWorkMock.Object);
        }

        [TestMethod]
        public void GetAllCategoriesReturnsRepositoryValues()
        {
            var categories = new List<Category> { Category };
            CategoriesMock.Setup(m => m.GetAll(null,"")).Returns(categories);

            var categoryLogic = CreateCategoryLogic();

            Assert.IsTrue(categoryLogic.GetAll().SequenceEqual(categories));
            CategoriesMock.VerifyAll();
        }
    }
}
