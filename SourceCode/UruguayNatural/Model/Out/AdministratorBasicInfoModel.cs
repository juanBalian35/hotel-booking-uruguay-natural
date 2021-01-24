using Domain;

namespace Model.Out
{
    public class AdministratorBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public AdministratorBasicInfoModel(Administrator administrator)
        {
            Id = administrator.Id;
            Name = administrator.Name;
            Email = administrator.Email;
            Password = administrator.Password;
        }

        public override bool Equals(object obj)
        {
            if(!(obj is AdministratorBasicInfoModel))
            {
                return false;
            }

            var administrator = obj as AdministratorBasicInfoModel;
            return Id == administrator.Id && Email == administrator.Email;
        }
    }
}
