using System.Collections.Generic;
using System.Net.Mime;
using Domain;

namespace Model.Out
{
    public class TouristSpotDetailedInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? RegionId { get; set; }
        public List<CategoryDetailedInfoModel> Categories { get; set; }
        public byte[] ImageData { get; set; }
        
        public TouristSpotDetailedInfoModel(TouristSpot touristSpot)
        {
            Id = touristSpot.Id;
            Name = touristSpot.Name;
            Description = touristSpot.Description;
            RegionId = touristSpot.RegionId;
            Categories = new List<CategoryDetailedInfoModel>();
            ImageData = touristSpot.Image;
            foreach (var tsc in touristSpot.TouristSpotCategories)
            {
                var model = new CategoryDetailedInfoModel(tsc.Category);
                Categories.Add(model);
            }    
        }

        public override bool Equals(object obj)
        {
            if(!(obj is TouristSpotDetailedInfoModel))
            {
                return false;
            }

            var touristSpot = obj as TouristSpotDetailedInfoModel;
            return Id == touristSpot.Id;
        }
    }
}
