using BusinessLogicInterface.Exceptions;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;

namespace BusinessLogic 
{
    public class SessionLogic : ISessionLogic
    {
        private readonly IRepository<Session> SessionRepository;
        private readonly IRepository<Administrator> AdministratorRepository;

        public SessionLogic(IUnitOfWork unitOfWork)
        {
            SessionRepository = unitOfWork.GetSessionRepository();
            AdministratorRepository = unitOfWork.GetAdminRepository();
        }

        public bool IsValidToken(Guid token)
        {
            return SessionRepository.Get(token) != null;
        }

        public Guid CreateSession(string email, string password)
        {
            var admin = AdministratorRepository.GetFirst(a => a.Email == email);
            if (admin == null || admin.Password != password)
            {
                throw new InvalidCredentialsException();
            }

            var session = new Session()
            {
                Token = Guid.NewGuid(),
                Administrator = admin
            };

            SessionRepository.Add(session);
            SessionRepository.Save();

            return session.Token;
        }

        public Administrator GetAdministrator(Guid token)
        {
            var session = SessionRepository.GetFirst(a => a.Token == token);

            return session?.Administrator;
        }
    }
}
