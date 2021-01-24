namespace Domain.Validations
{
    public class StringNotWhitespaceValidation : IValidation
    {
        public string GetError() => "Should not be empty or whitespace";

        public bool Validate(object value)
        {
            if(!(value is string))
            {
                return value == null;
            }

            return !string.IsNullOrWhiteSpace(value as string);
        }
    }
}
