using System;

namespace GA.BDC.Shared.Exceptions
{
    public class CreditCardChargeException : Exception
    {
        public CreditCardChargeException(string creditCardType, double amount, string responseCode, string responseMessage, string rawMessage, Exception innerException)
            : base(string.Format("Credit Card Type: {0}. Amount: {1}. Response Code: {2}. Response Message: {3}. Raw Message: {4}.", creditCardType, amount, responseCode, responseMessage, rawMessage), innerException)
        {

        }
    }
}
