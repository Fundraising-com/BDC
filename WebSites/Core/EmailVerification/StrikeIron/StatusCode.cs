using System;

namespace GA.BDC.Core.EmailVerification.StrikeIron
{
    /// <summary>
    ///   Status Code of the StrikeIron Email Verification web service request
    /// </summary>
    public enum StatusCode
    {
        EMAIL_VALID = 200,
        DOMAIN_CONFIRMED = 210,
        ANALYTICS_IN_PROGRESS = 220,
        EMAIL_VALID_POTENTIALLY_DANGEROUS = 250,
        DOMAIN_CONFIRMED_POTENTIALLY_DANGEROUS = 260,
        ANALYTICS_IN_PROGRESS_POTENTIALLY_DANGEROUS = 270,
        EMAIL_NOT_VALID = 300,
        INVALID_INPUT = 400,
        INTERNAL_ERROR = 500
    }
}
