namespace Import
{
    public class LodgingParsed
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public double PricePerNight { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ConfirmationMessage { get; set; }
        public TouristSpotParsed TouristSpot { get; set; }
    }
}
