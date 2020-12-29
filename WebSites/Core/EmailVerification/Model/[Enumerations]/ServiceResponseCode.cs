using System;

namespace GA.BDC.Core.EmailVerification.Model
{
    public enum ServiceResponseCode
    {
        Success = 0,
        Fail,
        Retry,
        UnknownError
    }
}
