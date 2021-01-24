namespace Domain
{
    public class Adult : Guest
    {
        public override double CalculatePrice(int nights, double pricePerNight)
        {
            return Quantity * nights * pricePerNight;
        }
    }
}
