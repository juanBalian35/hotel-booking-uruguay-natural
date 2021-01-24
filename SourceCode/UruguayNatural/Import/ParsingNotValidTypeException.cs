using System;

namespace Import
{
    public class ParsingNotValidTypeException : Exception
    {
        public string ParserName { get; }

        public ParsingNotValidTypeException(string parserName, string errorMessage) : base(errorMessage)
        {
            ParserName = parserName;
        }
    }
}
