using Domain;

namespace Model.Out
{
    public class CategoryBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryBasicInfoModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CategoryBasicInfoModel))
            {
                return false;
            }

            var category = obj as CategoryBasicInfoModel;
            return Id == category.Id && Name == category.Name;
        }
    }
}
