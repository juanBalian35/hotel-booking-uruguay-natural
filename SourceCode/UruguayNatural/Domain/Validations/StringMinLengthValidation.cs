namespace Domain.Validations
{
    public class StringMinLengthValidation : IValidation
    {
        public string GetError() => "Minimum length is " + MinLength;

        private readonly int MinLength;

        public StringMinLengthValidation(int minLength)
        {
            MinLength = minLength;
        }

        public bool Validate(object value)
        {
            if(!(value is string))
            {
                return value == null;
            }

            return (value as string).Length >= MinLength;
        }
    }
}
