using System;
using Domain.Validations;

namespace Model.In
{
    public class ReportModel : Validatable
    {
        public int? TouristSpot { get; set; }
        public DateTime? CheckIn { get; set; }
        
        public int Page { get; set; } = 1;
        public int ResultsPerPage { get; set; } = 10;
        
        private DateTime? _CheckOut;
        public DateTime? CheckOut
        {
            get { return _CheckOut; }
            set
            {
                _CheckOut = value;
                
                RemoveFieldValidations(nameof(CheckIn));
                AddValidation(nameof(CheckIn), new NotNullValidation());
                AddValidation(nameof(CheckIn), new DateIsLessThanOtherValidation(nameof(CheckOut), value));
            }
        }

        public ReportModel()
        {
            AddValidation(nameof(CheckIn), new NotNullValidation());
            AddValidation(nameof(CheckOut), new NotNullValidation());
            
            AddValidation(nameof(TouristSpot), new NotNullValidation());
        }
        
        public bool HasErrors()
        {
            return Validate().HasErrors();
        }
        
        public INotification Errors()
        {
            return Validate();
        }
    }
}
