using Domain;

namespace Model.Out
{
    public class TouristSpotNoAssociationsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public TouristSpotNoAssociationsModel(TouristSpot touristSpot)
        {
            Id = touristSpot.Id;
            Name = touristSpot.Name;
            Description = touristSpot.Description;
        }

        public override bool Equals(object obj)
        {
            if(!(obj is TouristSpotNoAssociationsModel))
            {
                return false;
            }

            var touristSpot = obj as TouristSpotNoAssociationsModel;
            return Id == touristSpot.Id;
        }
    }
}
