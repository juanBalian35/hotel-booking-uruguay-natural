namespace Domain
{
    public class Baby : Guest
    {
        public override double CalculatePrice(int nights, double pricePerNight)
        {
            return 0.25 * Quantity * nights * pricePerNight;
        }
    }
}
