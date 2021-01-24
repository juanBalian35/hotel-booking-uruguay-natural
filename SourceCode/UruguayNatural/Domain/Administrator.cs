using System.Collections.Generic;
using Domain.Validations;

namespace Domain
{
    public class Administrator : Validatable
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public List<Session> Sessions { get; set; }

        public Administrator()
        {
            AddValidation(nameof(Name), new NotNullValidation());
            AddValidation(nameof(Name), new StringNotWhitespaceValidation());

            AddValidation(nameof(Email), new NotNullValidation());
            AddValidation(nameof(Email), new EmailValidation());

            AddValidation(nameof(Password), new NotNullValidation());
            AddValidation(nameof(Password), new StringMinLengthValidation(6));
        }

        public override bool Equals(object obj)
        {
            if(!(obj is Administrator))
            {
                return false;
            }

            var administrator = obj as Administrator;
            return this.Id == administrator.Id && this.Email == administrator.Email;
        }
    }
}
