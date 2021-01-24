using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Domain;

namespace Model.Out
{
    public class TouristSpotBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryBasicInfoModel> Categories { get; set; }
        public byte[] ImageData { get; set; }
        
        public TouristSpotBasicInfoModel(TouristSpot touristSpot)
        {
            Id = touristSpot.Id;
            Name = touristSpot.Name;
            Categories = new List<CategoryBasicInfoModel>();
            ImageData = touristSpot.Image;
            Categories = 
                touristSpot.TouristSpotCategories.Select(tsc => new CategoryBasicInfoModel(tsc.Category)).ToList();
        }

        public override bool Equals(object obj)
        {
            if(!(obj is TouristSpotBasicInfoModel))
            {
                return false;
            }

            var touristSpot = obj as TouristSpotBasicInfoModel;
            return Id == touristSpot.Id;
        }
    }
}
