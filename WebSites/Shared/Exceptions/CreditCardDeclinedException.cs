using System;

namespace GA.BDC.Shared.Exceptions
{
    public class CreditCardDeclinedException : Exception
    {
        public CreditCardDeclinedException(string creditCardType, double amount, string responseCode, string rawMessage, Exception innerException)
            : base(string.Format("Credit Card Type: {0}. Amount: {1}. Response Code: {2}. Raw Message: {3}.", creditCardType, amount, responseCode, rawMessage), innerException)
        {

        }
    }
}
