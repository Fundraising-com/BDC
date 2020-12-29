using System;

namespace GA.BDC.Core.EmailVerification.Model
{
    public interface IValidationRequest
    {
        string EmailAddress { get; }
    }
}
