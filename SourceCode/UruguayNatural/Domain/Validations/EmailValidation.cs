using System.Text.RegularExpressions;

namespace Domain.Validations
{
    public class EmailValidation : IValidation
    {
        public string GetError() => "Email should have valid format.";

        public bool Validate(object value)
        {
            if(!(value is string))
            {
                return false;
            }

            const string PATTERN = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            return Regex.Match(value as string, PATTERN, RegexOptions.IgnoreCase).Success;
        }
    }
}
