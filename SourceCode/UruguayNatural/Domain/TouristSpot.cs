using System.Collections.Generic;
using Domain.Validations;

namespace Domain
{
    public class TouristSpot : Validatable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public ICollection<TouristSpotCategory> TouristSpotCategories { get; set; }
        public ICollection<Lodging> Lodgings { get; set; }

        public Region Region { get; set; }
        public int? RegionId { get; set; }

        public TouristSpot()
        {
            AddValidation(nameof(Name), new NotNullValidation());
            AddValidation(nameof(Name), new StringNotWhitespaceValidation());

            AddValidation(nameof(Description), new NotNullValidation());
            AddValidation(nameof(Description), new StringNotWhitespaceValidation());
            AddValidation(nameof(Description), new StringMaxLengthValidation(2000));

            AddValidation(nameof(Image), new NotNullValidation());
            
            AddValidation(nameof(RegionId), new NotNullValidation());
            
            AddValidation(nameof(TouristSpotCategories), new ListMinLengthValidation(1));
        }

        public override bool Equals(object obj)
        {
            if(!(obj is TouristSpot))
            {
                return false;
            }

            TouristSpot touristSpot = obj as TouristSpot;
            return this.Id == touristSpot.Id;
        }
    }
}
