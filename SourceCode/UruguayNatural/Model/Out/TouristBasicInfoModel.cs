using Domain;

namespace Model.Out
{
    public class TouristBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public TouristBasicInfoModel(Tourist tourist)
        {
            Id = tourist.Id;
            Name = tourist.Name;
            LastName = tourist.LastName;
            Email = tourist.Email;
        }
    }
}
