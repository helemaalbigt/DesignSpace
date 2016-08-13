using System;
using System.Runtime.Serialization;

namespace BlackBox.Exceptions
{
    [Serializable]
    internal class ObsoleteException : Exception
    {
        public ObsoleteException()
        {
        }

        public ObsoleteException(string message) : base(message)
        {
        }

        public ObsoleteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ObsoleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}