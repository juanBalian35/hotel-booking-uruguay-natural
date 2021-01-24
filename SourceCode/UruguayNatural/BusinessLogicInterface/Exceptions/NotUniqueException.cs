using System;
using BusinessLogicInterface;

namespace BusinessLogicInterface.Exceptions
{
    public class NotUniqueException : Exception
    {
        public string Field { get; }

        public NotUniqueException(string field)
        {
            Field = field;
        }
    }
}
