using System;
using System.Collections.Generic;
using Domain;
using Domain.Validations;

namespace Model.In
{
    public class BookingModel : Validatable
    {
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Lodging { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Babies { get; set; }
        public int Retirees { get; set; }

        public BookingModel()
        {
            AddValidation(nameof(Adults), new MinNumberValidation(0));
            AddValidation(nameof(Children), new MinNumberValidation(0));
            AddValidation(nameof(Babies), new MinNumberValidation(0));
            AddValidation(nameof(Retiree), new MinNumberValidation(0));
        }
        
        public Booking ToEntity()
        {
            var booking = new Booking
            {
                CheckIn = this.CheckIn,
                CheckOut = this.CheckOut,
                Tourist = new Tourist { Name = this.Name, LastName = this.LastName, Email = this.Email },
                Lodging = Lodging == null ? null : new Lodging { Id = (int)Lodging },
                Guests = Babies + Adults + Children,
                States = new List<BookingState> {new BookingState { State = "Creada", Description = "Estado inicial" }}
            };

            return booking;
        }

        public bool HasErrors()
        {
            return Errors().HasErrors();
        }

        public INotification Errors()
        {
            var entityErrors = ToEntity().Validate();
            entityErrors.Merge(Validate());
            
            return entityErrors;
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
    }
}
