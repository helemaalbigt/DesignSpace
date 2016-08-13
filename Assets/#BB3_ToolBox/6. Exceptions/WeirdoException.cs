using System;
using System.Runtime.Serialization;

namespace BlackBox.Exceptions
{
    [Serializable]
    internal class WeirdoException : Exception
    {
        public WeirdoException()
        {
        }

        public WeirdoException(string message) : base(message)
        {
        }

        public WeirdoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WeirdoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}