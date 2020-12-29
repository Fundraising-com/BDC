using System;
using System.Runtime.Serialization;

namespace GA.BDC.Core.EmailVerification
{
    [Serializable]
    public abstract class ServiceException : Exception, ISerializable
    {
      protected ServiceException() : base()
      {
      }

      protected ServiceException(string message) : base(message)
      {
      }

      protected ServiceException(string message, Exception innerException) : base(message, innerException)
      {
      }

      protected ServiceException(SerializationInfo info, StreamingContext context)
          : base(info, context)
      {
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.GetObjectData(info, context);
      }
    }
}
