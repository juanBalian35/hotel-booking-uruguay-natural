using System;
using Domain.Validations;

namespace BusinessLogicInterface.Exceptions
{
    public class EntityNotValidException : Exception
    {
        public INotification Notification { get; }

        public EntityNotValidException(INotification notification)
        {
            Notification = notification;
        }
    }
}
