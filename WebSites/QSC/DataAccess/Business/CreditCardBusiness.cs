namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;
    using QSPFulfillment.DataAccess.PaymentSystem;

	public class CreditCardBusiness
	{
		private QSPFulfillment.PaymentSystemInterface psi = new PaymentSystemInterface();
		internal DataAccess.Common.Message messageManager;

        private static CardType ConvertToPaymentSystemCreditCardType(PaymentMethod PaymentMethod)
        {
            CardType cardType;

 	        switch (PaymentMethod)
            {
                case PaymentMethod.Visa: cardType = CardType.VISA;
                    break;
                case PaymentMethod.MasterCard: cardType = CardType.MASTERCARD;
                    break;
                default: cardType = CardType.VISA;
                    break;
            }

            return cardType;
        }

        private static QSPFulfillment.DataAccess.PaymentSystem.CountryCode ConvertToPaymentSystemCountryCode(string CountryCode)
        {
            QSPFulfillment.DataAccess.PaymentSystem.CountryCode countryCode;

            countryCode = QSPFulfillment.DataAccess.PaymentSystem.CountryCode.CA;

            return countryCode;
        }

		public CreditCardBusiness(Message MessageManager)
		{
			if(messageManager == null)
				messageManager = new DataAccess.Common.Message(true);
		}
        
        public string AuthDepositRealTime(CreditCard cc, int ChargeAmount, QSPFulfillment.DataAccess.Common.ActionObject.Address address, int incidentID) 
		{
            BPPSTxResponse bPPSTxResponse;

			try 
			{
                bPPSTxResponse = psi.AuthDepositRealTime(
                    cc.CardHolderName.Substring(0, cc.CardHolderName.IndexOf(" ")),
                    cc.CardHolderName.Substring(cc.CardHolderName.LastIndexOf(" ")),
					address.Street1,
					address.Street2,
                    address.City,
                    address.StateProvinceCode,
                    address.PostalCode,
                    ConvertToPaymentSystemCountryCode(address.CountryCode),
                    ConvertToPaymentSystemCreditCardType(cc.PaymentMethodID),
                    cc.CreditCardNumber,
                    Convert.ToInt32(cc.ExpirationMonth),
                    Convert.ToInt32(cc.ExpirationYear),
                    ChargeAmount,
                    "",
                    "",
                    Convert.ToString(incidentID));
            }
			catch (Exception e)
			{
				messageManager.Add(Message.ERRMSG_SYSTEM_VAR_0);
				messageManager.PrepareErrorMessage();
				throw new ExceptionFulf(messageManager);
			}

         //if (bPPSTxResponse.ErrorMessage.Length > 0)
         if (bPPSTxResponse.responseCode != "100") 
         {
            string errorMessage;
            switch (bPPSTxResponse.responseCode)
            {
               case "GA-8": errorMessage = "The credit card has expired."; break;
               case "GA-11": errorMessage = "A duplicate transaction has been submitted."; break;
               case "GA-37": errorMessage = "The credit card number is invalid."; break;
               default: errorMessage = "Code " + bPPSTxResponse.responseCode; break;
            }
                     
            messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_CREDIT_CARD_REJECTED_1, errorMessage));
            messageManager.PrepareErrorMessage();
            throw new ExceptionFulf(messageManager);
         }

         return bPPSTxResponse.authNumber;
		}

        public bool Refund(RefundCustomer rcRefund, CreditCard cc, int incidentID) 
		{
			bool bSuccess = false;
            BPPSTxResponse bPPSTxResponse;

            try
            {
                bPPSTxResponse = psi.Refund(
                    cc.CardHolderName.Substring(0, cc.CardHolderName.IndexOf(" ")),
                    cc.CardHolderName.Substring(cc.CardHolderName.LastIndexOf(" ")),
                    rcRefund.CustomerInfo.CustomerAddress.Street1,
					rcRefund.CustomerInfo.CustomerAddress.Street2,
                    rcRefund.CustomerInfo.CustomerAddress.City,
                    rcRefund.CustomerInfo.CustomerAddress.StateProvinceCode,
                    rcRefund.CustomerInfo.CustomerAddress.PostalCode,
                    ConvertToPaymentSystemCountryCode(rcRefund.CustomerInfo.CustomerAddress.CountryCode),
                    ConvertToPaymentSystemCreditCardType(cc.PaymentMethodID),
                    cc.CreditCardNumber,
                    Convert.ToInt32(cc.ExpirationMonth),
                    Convert.ToInt32(cc.ExpirationYear),
                    Convert.ToInt32(rcRefund.RefundAmount*100),
                    "",
                    "",
                    Convert.ToString(incidentID));
            }
            catch
            {
                messageManager.Add(Message.ERRMSG_SYSTEM_VAR_0);
                messageManager.PrepareErrorMessage();
                throw new ExceptionFulf(messageManager);
            }

            if (bPPSTxResponse.ErrorMessage.Length > 0)
            {
                bSuccess = false;
                messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_CREDIT_CARD_REJECTED_1, bPPSTxResponse.ErrorMessage));
                messageManager.PrepareErrorMessage();
                throw new ExceptionFulf(messageManager);
            }
            else
            {
                bSuccess = true;
            }

            return bSuccess;
		}
	}
}