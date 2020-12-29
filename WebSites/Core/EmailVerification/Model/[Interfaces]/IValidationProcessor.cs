using System;

namespace GA.BDC.Core.EmailVerification.Model
{
    public interface IValidationProcessor
    {
        IValidationResponse ProcessRequest(IValidationRequest request);
    }
}
