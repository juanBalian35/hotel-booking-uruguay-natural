using System.Collections.Generic;
using Domain;
using BusinessLogicInterface;
using DataAccessInterface;
using BusinessLogicInterface.Exceptions;

namespace BusinessLogic
{
    public class AdministratorLogic : IAdministratorLogic
    {
        private readonly IRepository<Administrator> AdministratorRepository;
        public AdministratorLogic(IUnitOfWork unitOfWork)
        {
            AdministratorRepository = unitOfWork.GetAdminRepository();
        }

        public Administrator Create(Administrator administrator)
        {
            if (AdministratorRepository.Exists(x => x.Email == administrator.Email))
            {
                throw new NotUniqueException("Email");
            }

            AdministratorRepository.Add(administrator);
            AdministratorRepository.Save();
            return administrator;
        }

        public Administrator Update(int id, Administrator administrator)
        {
            var actualAdmin = AdministratorRepository.Get(id);
            if (actualAdmin == null)
            {
                throw new NotFoundException("Id");
            }

            if (AdministratorRepository.Exists(x => x.Email == administrator.Email && x.Id != id))
            {
                throw new NotUniqueException("Email");
            }

            actualAdmin.Name = administrator.Name ?? actualAdmin.Name;
            actualAdmin.Email = administrator.Email ?? actualAdmin.Email;
            actualAdmin.Password = administrator.Password ?? actualAdmin.Password;

            if (!actualAdmin.IsValid())
            {
                throw new EntityNotValidException(actualAdmin.Validate());
            }

            AdministratorRepository.Update(actualAdmin);
            AdministratorRepository.Save();

            return actualAdmin;
        }

        public void Delete(int id)
        {
            var administrator = AdministratorRepository.Get(id);
            if (administrator == null)
            {
                throw new NotFoundException("Id");
            }
            
            AdministratorRepository.Remove(administrator);
            AdministratorRepository.Save();
        }

        public ICollection<Administrator> GetAll()
        {
            return AdministratorRepository.GetAll();
        }
    }
}
