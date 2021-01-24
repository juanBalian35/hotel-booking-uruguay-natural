using System;

namespace Domain.Validations
{
    public class DateIsLessThanOtherValidation : IValidation
    {
        public string GetError() => "Should be less than " + FieldNameToCompare;

        private readonly string FieldNameToCompare;
        private readonly DateTime? CompareTo;

        public DateIsLessThanOtherValidation(string fieldNameToCompare, DateTime? compareTo)
        {
            FieldNameToCompare = fieldNameToCompare;
            CompareTo = compareTo;
        }

        public bool Validate(object value)
        {
            if (!(value is DateTime) || CompareTo == null)
            {
                return false;
            }

            return ((DateTime)value).Date < ((DateTime)CompareTo).Date;
        }
    }
}
