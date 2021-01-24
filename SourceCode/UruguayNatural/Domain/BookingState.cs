using System.Linq;
using Domain.Validations;

namespace Domain
{
    public class BookingState : Validatable
    {
        private static readonly string[] ValidStates = 
            { "Creada", "Pendiente Pago", "Aceptada", "Rechazada", "Expirada" };
        
        public int Id { get; set; }
        public Booking Booking { get; set; }
        public int BookingId { get; set; }
        public string State { get; set; }
        public string Description { get; set; }

        public BookingState()
        {
            AddValidation(nameof(Booking), new NotNullValidation());
            
            AddValidation(nameof(State), new NotNullValidation());
            AddValidation(nameof(State), new StringNotWhitespaceValidation());
            AddValidation(nameof(State), new IncludedInListValidation<string>(ValidStates.ToList()));
            
            AddValidation(nameof(Description), new NotNullValidation());
            AddValidation(nameof(Description), new StringNotWhitespaceValidation());
        }
    }
}
