using Domain.Validations;

namespace Model.In
{
    public class TouristSpotSearchModel : Validatable
    {
        public int? Region { get; set; }

        public int[] Categories { get; set; }

        public int Page { get; set; } = 1;

        public int ResultsPerPage { get; set; } = 10;

        public TouristSpotSearchModel()
        {
            AddValidation(nameof(Region), new NotNullValidation());
            if (Categories == null)
            {
                Categories = new int[]{};
            }
        }

        public bool HasErrors()
        {
            return Validate().HasErrors();
        }
        
        public INotification Errors()
        {
            return Validate();
        }
    }
}
