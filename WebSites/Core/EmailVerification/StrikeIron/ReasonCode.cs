using System;

namespace GA.BDC.Core.EmailVerification.StrikeIron
{
    /// <summary>
    ///   Reason Code to further describe the response of the StrikeIron Email Verification web service request
    /// </summary>
    public enum ReasonCode
    {
        MAILBOX_CONFIRMED = 201,
        USER_ACCOUNT_CONFIRMED = 202,
        MAILBOX_CONFIRMED_BUT_FULL = 203,
        SERVER_WILL_ACCEPT = 211,
        UNABLE_TO_VERIFY_AT_THIS_TIME = 212,
        TIMEOUT_TOO_SHORT = 221,
        SERVER_NOT_RESPONDING = 222,
        SERVER_DEFERRED = 223,
        SERVER_THROTTLED = 224,
        SERVER_ACTIVELY_REJECTED = 225,
        SERVER_CONNECTION_LOST = 226,
        MISSING_AT_SYMBOL = 301,
        BAD_DOMAIN_IN_SYNTAX = 302,
        BAD_LOCAL_PART_SYNTAX = 303,
        DOMAIN_NOT_FOUND = 304,
        NOT_A_VALID_MAIL_DOMAIN = 305,
        MAIL_DOMAIN_IS_NON_RESPONSIVE = 306,
        MAILBOX_REJECTED = 307,
        EMAIL_IS_REQUIRED = 401,
        TIMEOUT_MUST_BE_GREATER_THAN_0 = 402
    }
}
