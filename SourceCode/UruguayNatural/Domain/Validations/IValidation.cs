namespace Domain.Validations
{
    public interface IValidation
    {
        string GetError();
        bool Validate(object value);
    }
}
