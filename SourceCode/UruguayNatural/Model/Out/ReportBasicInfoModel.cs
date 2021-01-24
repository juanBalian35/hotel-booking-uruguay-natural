using Domain;

namespace Model.Out
{
    public class ReportBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int BookingCount { get; set; }
        public int TouristSpotId { get; set; }


        public ReportBasicInfoModel(Lodging lodging)
        {
            Id = lodging.Id;
            Name = lodging.Name;
            Address = lodging.Address;
            TouristSpotId = lodging.TouristSpot.Id;

        }

        public override bool Equals(object obj)
        {
            if (!(obj is ReportBasicInfoModel))
            {
                return false;
            }
            
            var reportModel = obj as ReportBasicInfoModel;
            return Address == reportModel.Address;
        }
    }
}