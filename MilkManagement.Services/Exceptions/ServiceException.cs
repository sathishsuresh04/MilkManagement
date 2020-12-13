using System;

namespace MilkManagement.Services.Exceptions
{
   public class ServiceException:Exception
    {
        internal ServiceException(string businessMessage)
            : base(businessMessage)
        {
        }

        internal ServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
