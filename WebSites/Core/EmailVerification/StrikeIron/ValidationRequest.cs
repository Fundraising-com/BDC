using System;

using GA.BDC.Core.EmailVerification.Model;

namespace GA.BDC.Core.EmailVerification.StrikeIron
{
    public class ValidationRequest : IValidationRequest
    {
        public ValidationRequest(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; private set; }
    }
}
