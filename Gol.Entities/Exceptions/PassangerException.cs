using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Gol.Entities.Exceptions
{
    public class PassengerException : Exception
    {
        public PassengerException()
        {
        }

        public PassengerException(string message) : base(message)
        {
        }

        public PassengerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PassengerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
