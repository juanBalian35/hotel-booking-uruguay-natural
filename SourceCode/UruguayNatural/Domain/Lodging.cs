using System;
using Domain.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Lodging : Validatable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public bool IsFull { get; set; }
        public ICollection<LodgingImage> Images { get; set; }
        public double PricePerNight { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ConfirmationMessage { get; set; }
        public TouristSpot TouristSpot { get; set; }
        public double TotalPrice { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public double ReviewAverage { get; set; }
        public int ReviewsQuantity { get; set; }

        public Lodging()
        {
            AddValidation(nameof(Name), new NotNullValidation());
            AddValidation(nameof(Name), new StringNotWhitespaceValidation());

            AddValidation(nameof(Description), new NotNullValidation());
            AddValidation(nameof(Description), new StringNotWhitespaceValidation());

            AddValidation(nameof(Rating), new MinNumberValidation(1));
            AddValidation(nameof(Rating), new MaxNumberValidation(5));

            AddValidation(nameof(Images), new NotNullValidation());
            AddValidation(nameof(Images), new ListMinLengthValidation(1));

            AddValidation(nameof(PricePerNight), new MinNumberValidation(0));

            AddValidation(nameof(Address), new NotNullValidation());
            AddValidation(nameof(Address), new StringNotWhitespaceValidation());

            AddValidation(nameof(Phone), new NotNullValidation());
            AddValidation(nameof(Phone), new StringNotWhitespaceValidation());

            AddValidation(nameof(ConfirmationMessage), new NotNullValidation());
            AddValidation(nameof(ConfirmationMessage), new StringNotWhitespaceValidation());

            AddValidation(nameof(TouristSpot), new NotNullValidation());
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is Lodging))
            {
                return false;
            }

            Lodging lodging = obj as Lodging;
            return Id == lodging.Id && Address == lodging.Address;
        }

        public double CalculatePrice(DateTime checkIn, DateTime checkOut, ICollection<Guest> guests)
        {
            return guests.Sum(guest => guest.CalculatePrice((int) (checkOut - checkIn).TotalDays, PricePerNight));
        }
    }
}
