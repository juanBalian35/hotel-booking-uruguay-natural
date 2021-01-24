using System.Collections.Generic;

namespace Domain.Validations
{
    public class IncludedInListValidation<T> : IValidation
    {
        public string GetError() => "The valid values are: " + string.Join(", ", List);

        private readonly ICollection<T> List;

        public IncludedInListValidation(ICollection<T> list)
        {
            List = list;
        }

        public bool Validate(object value)
        {
            if(!(value is T))
            {
                return value == null;
            }

            return List.Contains((T)value);
        }
    }
}
