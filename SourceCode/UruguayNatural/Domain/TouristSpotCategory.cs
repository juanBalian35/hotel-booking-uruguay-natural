namespace Domain
{
    public class TouristSpotCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int TouristSpotId { get; set; }
        public TouristSpot TouristSpot { get; set; }
    }
}
