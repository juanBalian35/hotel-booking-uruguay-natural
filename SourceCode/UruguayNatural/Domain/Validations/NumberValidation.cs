using System;
using System.Globalization;

namespace Domain.Validations
{
    public abstract class NumberValidation : IValidation
    {
        public abstract string GetError();
        public abstract bool Validate(object value);

        protected double AsNumber(object value)
        {
            double number;
            double.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture),
                NumberStyles.Any,
                NumberFormatInfo.InvariantInfo,
                out number);

            return number;
        }
    }
}
