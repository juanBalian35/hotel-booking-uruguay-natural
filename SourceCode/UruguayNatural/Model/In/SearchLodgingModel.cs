using System;
using System.Collections.Generic;
using Domain;
using Domain.Validations;

namespace Model.In
{
    public class SearchLodgingModel : Validatable
    {
        public DateTime? CheckIn { get; set; }
        
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
        
        public int Retirees { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Babies { get; set; }
        public int? TouristSpot { get; set; }
        public int Page { get; set; } = 1;
        public int ResultsPerPage { get; set; } = 10;
        public bool? IsFull { get; set; }

        public SearchLodgingModel()
        {
            AddValidation(nameof(CheckIn), new NotNullValidation());
            AddValidation(nameof(CheckOut), new NotNullValidation());
            
            AddValidation(nameof(TouristSpot), new NotNullValidation());
            
            AddValidation(nameof(Retirees), new MinNumberValidation(0));
            AddValidation(nameof(Adults), new MinNumberValidation(0));
            AddValidation(nameof(Children), new MinNumberValidation(0));
            AddValidation(nameof(Babies), new MinNumberValidation(0));
        }
        
        public ICollection<Guest> GetGuests()
        {
            return new List<Guest>
            {
                new Adult { Quantity = Adults },
                new Child { Quantity = Children },
                new Baby { Quantity = Babies },
                new Retiree { Quantity = Retirees }
            };
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
