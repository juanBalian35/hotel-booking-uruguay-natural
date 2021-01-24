namespace Domain.Validations
{
    public class StringMaxLengthValidation : IValidation
    {
        public string GetError() => "Maximum length is " + MaxLength;

        private readonly int MaxLength;

        public StringMaxLengthValidation(int maxLength)
        {
            MaxLength = maxLength;
        }

        public bool Validate(object value)
        {
            if(!(value is string))
            {
                return value == null;
            }

            return (value as string).Length <= MaxLength;
        }
    }
}
