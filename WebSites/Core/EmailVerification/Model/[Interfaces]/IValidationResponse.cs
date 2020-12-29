using System;

namespace GA.BDC.Core.EmailVerification.Model
{
    public interface IValidationResponse
    {
        string EmailAddress { get; }

        int ResponseCode { get; }
        string ResponseMessage { get; }
        string ResponseRaw { get; }

        bool IsValid { get; }
        bool RetryRequest { get; }
    }
}
