using System.Collections;

namespace Domain.Validations
{
    public class ListMinLengthValidation : IValidation
    {
        public string GetError() => "Minimum length should be " + MinLength;

        private readonly int MinLength;

        public ListMinLengthValidation(int minLength)
        {
            MinLength = minLength;
        }

        public bool Validate(object value)
        {
            if(!(value is ICollection))
            {
                return value == null;
            }

            return (value as ICollection).Count >= MinLength;
        }
    }
}
