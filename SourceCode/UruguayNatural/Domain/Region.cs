using System.Collections.Generic;

namespace Domain
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<TouristSpot> TouristSpots { get; set; }
        public string VideoPath { get; set; }
        public byte[] MapYellow { get; set; }
        public byte[] MapTransparent { get; set; }
    }
}
