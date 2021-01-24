using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;
using Domain;

namespace ModelTests.Out
{
    [TestClass]
    public class CategoryDetailedInfoModelTest
    {
        private CategoryDetailedInfoModel CategoryDetailedInfoModel;
        private Category Category;

        [TestInitialize]
        public void TestInitialize()
        {
            Category = new Category()
            {
                Id = 1,
                Name = "Category1",
                FaIconName = "fa-icon"
            };
            CategoryDetailedInfoModel = new CategoryDetailedInfoModel(Category);
        }

        [TestMethod]
        public void ConstructorCreatesCategoryDetailedInfoModelWithSameId()
        {
            Assert.AreEqual(Category.Id, CategoryDetailedInfoModel.Id);
        }

        [TestMethod]
        public void ConstructorCreatesCategoryDetailedInfoModelWithSameName()
        {
            Assert.AreEqual(Category.Name, CategoryDetailedInfoModel.Name);
        } 
        [TestMethod]
        public void ConstructorCreatesCategoryDetailedInfoModelWithSameFaIconName()
        {
            Assert.AreEqual(Category.FaIconName, CategoryDetailedInfoModel.FaIconName);
        }

        

        [TestMethod]
        public void EqualsAnotherWithSameIdAndName()
        {
            var anotherCategoryModel = new CategoryDetailedInfoModel(Category);

            Assert.IsTrue(CategoryDetailedInfoModel.Equals(anotherCategoryModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentId()
        {
            var anotherCategory = Category;
            anotherCategory.Id = 2;
            var anotherCategoryModel = new CategoryDetailedInfoModel(anotherCategory);

            Assert.IsFalse(CategoryDetailedInfoModel.Equals(anotherCategoryModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentName()
        {
            var anotherCategory = Category;
            anotherCategory.Name = "differentName";
            var anotherCategoryModel = new CategoryDetailedInfoModel(anotherCategory);

            Assert.IsFalse(CategoryDetailedInfoModel.Equals(anotherCategoryModel));
        }

        [TestMethod]
        public void DoesNotEqualOtherType()
        {
            Assert.IsFalse(CategoryDetailedInfoModel.Equals("String"));
        }
    }
}
