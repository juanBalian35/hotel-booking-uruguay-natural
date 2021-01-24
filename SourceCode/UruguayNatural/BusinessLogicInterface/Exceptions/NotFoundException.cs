using System;
using BusinessLogicInterface;

namespace BusinessLogicInterface.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Field { get; }

        public NotFoundException(string field)
        {
            Field = field;
        }
    }
}
