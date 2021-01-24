namespace Domain
{
    public abstract class Guest
    {
        public int Quantity { get; set; }
        public abstract double CalculatePrice(int nights, double pricePerNight);
    }
}
