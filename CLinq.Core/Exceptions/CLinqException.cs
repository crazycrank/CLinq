using System;

namespace CLinq.Core.Exceptions
{
    public class CLinqException : Exception
    {
        public CLinqException()
        { }

        public CLinqException(string message)
            : base(message)
        { }

        public CLinqException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
