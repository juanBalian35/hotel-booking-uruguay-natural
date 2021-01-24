using Domain;
using Domain.Validations;

namespace Model.In
{
    public class AdministratorModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public Administrator ToEntity()
        {
            return new Administrator
            {
                Name = this.Name,
                Email = this.Email,
                Password = this.Password
            };
        }

        public bool HasErrors()
        {
            return !ToEntity().IsValid();
        }

        public INotification Errors()
        {
            return ToEntity().Validate();
        }
    }
}
