using Domain;
using System;

namespace BusinessLogicInterface
{
    public interface ISessionLogic
    {
        bool IsValidToken(Guid token);

        Guid CreateSession(string email, string password);

        Administrator GetAdministrator(Guid token);
    }
}
