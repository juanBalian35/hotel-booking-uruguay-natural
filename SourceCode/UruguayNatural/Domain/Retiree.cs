using System;

namespace Domain
{
    public class Retiree : Guest
    {
        public override double CalculatePrice(int nights, double pricePerNight)
        {
            var individualPrice = nights * pricePerNight;
            var discount = Math.Floor(Quantity/2.0) * 0.3 * individualPrice;
            
            return Quantity * individualPrice - discount;
        }
    }
}