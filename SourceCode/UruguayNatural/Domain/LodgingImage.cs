namespace Domain
{
    public class LodgingImage
    {
        public int Id { get; set; }
        public int LodgingId { get; set; }
        public Lodging Lodging { get; set; }
        public byte[] ImageData { get; set; }
    }
}
