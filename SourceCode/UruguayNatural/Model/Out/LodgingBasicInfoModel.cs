using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Model.Out
{
    public class LodgingBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public bool IsFull { get; set; }
        public ICollection<LodgingImageBasicInfoModel> Images { get; set; }
        public double PricePerNight { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ConfirmationMessage { get; set; }
        public TouristSpotNoAssociationsModel TouristSpot { get; set; }

        public LodgingBasicInfoModel(Lodging lodging)
        {
            Id = lodging.Id;
            Name = lodging.Name;
            Description = lodging.Description;
            Rating = lodging.Rating;
            IsFull = lodging.IsFull;
            Images = lodging.Images.Select(x => new LodgingImageBasicInfoModel(x)).ToList();
            PricePerNight = lodging.PricePerNight;
            Address = lodging.Address;
            Phone = lodging.Phone;
            ConfirmationMessage = lodging.ConfirmationMessage;
            TouristSpot = new TouristSpotNoAssociationsModel(lodging.TouristSpot);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LodgingBasicInfoModel))
            {
                return false;
            }

            var touristSpot = obj as LodgingBasicInfoModel;
            return Address == touristSpot.Address;
        }
    }
}
