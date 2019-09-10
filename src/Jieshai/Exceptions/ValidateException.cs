using System;
using System.Runtime.Serialization;

namespace Jieshai.Exceptions
{
    [Serializable]
    public class ValidateException : EIMException
    {
        public ValidateException(string message)
        {
            this.ExceptionMessage = message;
        }


        public ValidateException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {

        }
    }
}
