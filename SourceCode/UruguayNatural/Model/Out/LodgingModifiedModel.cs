using Domain;

namespace Model.Out
{
    public class LodgingModifiedModel
    {
        public string Name { get; set; }
        public bool IsFull { get; set; }
        public string Address { get; set; }
        public TouristSpotNoAssociationsModel TouristSpot { get; set; }

        public LodgingModifiedModel(Lodging lodging)
        {
            Name = lodging.Name;
            TouristSpot = new TouristSpotNoAssociationsModel(lodging.TouristSpot);
            IsFull = lodging.IsFull;
            Address = lodging.Address;

        }

        public override bool Equals(object obj)
        {
            if (!(obj is LodgingModifiedModel))
            {
                return false;
            }

            var touristSpot = obj as LodgingModifiedModel;
            return Address == touristSpot.Address;
        }
    }
}
