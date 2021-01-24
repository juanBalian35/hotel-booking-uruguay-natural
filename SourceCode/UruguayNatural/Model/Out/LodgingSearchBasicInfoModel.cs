using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Model.Out
{
    public class LodgingSearchBasicInfoModel
    {
        public int Id { get; set; }
        public bool IsFull { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double PricePerNight { get; set; }
        public double TotalPrice { get; set; }
        public double ReviewsAverage { get; set; }
        public double ReviewsQuantity { get; set; }
        public ICollection<LodgingImageBasicInfoModel> Images { get; set; }

        public LodgingSearchBasicInfoModel(Lodging lodging)
        {
            Id = lodging.Id;
            Name = lodging.Name;
            Rating = lodging.Rating;
            Address = lodging.Address;
            Description = lodging.Description;
            Images = lodging.Images.Select(x => new LodgingImageBasicInfoModel(x)).ToList();
            PricePerNight = lodging.PricePerNight;
            TotalPrice = lodging.TotalPrice;
            ReviewsAverage = lodging.ReviewAverage;
            ReviewsQuantity = lodging.ReviewsQuantity;
            IsFull = lodging.IsFull;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LodgingSearchBasicInfoModel))
            {
                return false;
            }
            
            var touristSpot = obj as LodgingSearchBasicInfoModel;
            return Address == touristSpot.Address;
        }
    }
}
