using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Gol.Entities.Exceptions
{
    public class AirplaneException : Exception
    {
        public AirplaneException()
        {
        }

        public AirplaneException(string message) : base(message)
        {
        }

        public AirplaneException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AirplaneException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
