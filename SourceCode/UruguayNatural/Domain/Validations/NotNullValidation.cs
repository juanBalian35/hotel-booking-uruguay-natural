namespace Domain.Validations
{
    public class NotNullValidation : IValidation
    {
        public string GetError() => "Should not be null";

        public bool Validate(object value)
        {
            return value != null;
        }
    }
}
