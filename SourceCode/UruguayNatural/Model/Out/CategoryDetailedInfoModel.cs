using Domain;

namespace Model.Out
{
    public class CategoryDetailedInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FaIconName { get; set; }

        public CategoryDetailedInfoModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            FaIconName = category.FaIconName;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CategoryDetailedInfoModel))
            {
                return false;
            }

            var category = obj as CategoryDetailedInfoModel;
            return Id == category.Id && Name == category.Name;
        }
    }
}
