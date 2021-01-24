using System;
using System.Collections.Generic;
using Domain.Validations;

namespace Domain
{
    public class Booking : Validatable
    {
        public int Id { get; set; }
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
        public int Guests { get; set; }
        public double TotalPrice { get; set; }
        public Lodging Lodging { get; set; }
        public Tourist Tourist { get; set; }
        public ICollection<BookingState> States { get; set; }

        public Booking()
        {
            AddValidation(nameof(Lodging), new NotNullValidation());
            
            AddValidation(nameof(CheckOut), new NotNullValidation());
            
            AddValidation(nameof(Tourist) + "." + nameof(Tourist.Name), new NotNullValidation());
            AddValidation(nameof(Tourist) + "." + nameof(Tourist.Name), new StringNotWhitespaceValidation());
            
            AddValidation(nameof(Tourist) + "." + nameof(Tourist.LastName), new NotNullValidation());
            AddValidation(nameof(Tourist) + "." + nameof(Tourist.LastName), new StringNotWhitespaceValidation());
            
            AddValidation(nameof(Tourist) + "." + nameof(Tourist.Email), new NotNullValidation());
            AddValidation(nameof(Tourist) + "." + nameof(Tourist.Email), new EmailValidation());
            
            AddValidation(nameof(Guests), new MinNumberValidation(1));
            
            AddValidation(nameof(States), new NotNullValidation());
            AddValidation(nameof(States), new ListMinLengthValidation(1));
        }
    }
}
