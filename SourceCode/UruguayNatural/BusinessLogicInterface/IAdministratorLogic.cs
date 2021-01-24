using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface
{
    public interface IAdministratorLogic
    {
        ICollection<Administrator> GetAll();
        
        Administrator Create(Administrator administrator);

        Administrator Update(int id, Administrator administrator);

        void Delete(int id);
    }
}
