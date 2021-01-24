namespace Domain
{
    public class Child : Guest
    {
        public override double CalculatePrice(int nights, double pricePerNight)
        {
            return 0.5 * Quantity * nights * pricePerNight;
        }
    }
}
