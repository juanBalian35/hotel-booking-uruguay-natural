using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Out;
using Domain;

namespace ModelTests.Out
{
    [TestClass]
    public class CategoryBasicInfoModelTest
    {
        private CategoryBasicInfoModel CategoryBasicInfoModel;
        private Category Category;

        [TestInitialize]
        public void TestInitialize()
        {
            Category = new Category()
            {
                Id = 1,
                Name = "Category1"
            };
            CategoryBasicInfoModel = new CategoryBasicInfoModel(Category);
        }

        [TestMethod]
        public void ConstructorCreatesCategoryBasicInfoModelWithSameId()
        {
            Assert.AreEqual(Category.Id, CategoryBasicInfoModel.Id);
        }

        [TestMethod]
        public void ConstructorCreatesCategoryBasicInfoModelWithSameName()
        {
            Assert.AreEqual(Category.Name, CategoryBasicInfoModel.Name);
        }

        

        [TestMethod]
        public void EqualsAnotherWithSameIdAndName()
        {
            var anotherCategoryModel = new CategoryBasicInfoModel(Category);

            Assert.IsTrue(CategoryBasicInfoModel.Equals(anotherCategoryModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentId()
        {
            var anotherCategory = Category;
            anotherCategory.Id = 2;
            var anotherCategoryModel = new CategoryBasicInfoModel(anotherCategory);

            Assert.IsFalse(CategoryBasicInfoModel.Equals(anotherCategoryModel));
        }

        [TestMethod]
        public void DoesNotEqualWithDifferentName()
        {
            var anotherCategory = Category;
            anotherCategory.Name = "differentName";
            var anotherCategoryModel = new CategoryBasicInfoModel(anotherCategory);

            Assert.IsFalse(CategoryBasicInfoModel.Equals(anotherCategoryModel));
        }

        [TestMethod]
        public void DoesNotEqualOtherType()
        {
            Assert.IsFalse(CategoryBasicInfoModel.Equals("String"));
        }
    }
}
