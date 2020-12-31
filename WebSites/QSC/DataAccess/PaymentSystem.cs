using System; 
using System.Net;
using QSPFulfillment.DataAccess.PaymentSystem;

namespace QSPFulfillment
{
    public class PaymentSystemInterface
    {
        #region Fields
        private string userName;
        private string password;
        private int applicationID;
        private BatchPaymentSystemWebservice ps;
        #endregion

        #region Properties
        private int ApplicationID
        {
            get
            {
                if (applicationID == 0)
                    applicationID = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["PaymentSystemApplicationID"]);
                return applicationID;
            }
        }

        private string Password
        {
            get
            {
                if (password == null)
                    password = System.Configuration.ConfigurationSettings.AppSettings["PaymentSystemPassword"];
                return password;
            }
        }
        #endregion

        #region Methods
        public PaymentSystemInterface()
        {
            ps = new BatchPaymentSystemWebservice();
        }

        public BPPSTxResponse AuthDepositRealTime(string CustomerFirstName, string CustomerLastName, string CustomerStreet1, string CustomerStreet2,
            string CustomerCity, string CustomerState, string CustomerPostalCode, CountryCode CustomerCountryCode, CardType CreditCardType,
            string CreditCardNumber, int CreditCardExpirationMonth, int CreditCardExpirationYear, int TransactionAmount, string LineItemDescription,
            string Descriptor, string ClientTransactionID)
        {
            BPPSTxResponse bPPSTxResponse = ps.AuthDepositRealTime(ApplicationID, CustomerFirstName, CustomerLastName, CustomerStreet1,
                CustomerStreet2, CustomerCity, CustomerState, CustomerPostalCode, CustomerCountryCode, CreditCardType, CreditCardNumber,
                CreditCardExpirationMonth, CreditCardExpirationYear, TransactionAmount, LineItemDescription, Descriptor, ClientTransactionID, Password);

            return bPPSTxResponse;
        }


        public BPPSTxResponse RefundPrevious(int ReferringBPPSTxID, int TransactionAmount, string LineItemDescription, string Descriptor)
        {
            BPPSTxResponse bPPSTxResponse = ps.RefundPrevious(applicationID, password, ReferringBPPSTxID, TransactionAmount, LineItemDescription, Descriptor);

            return bPPSTxResponse;
        }

        public BPPSTxResponse Refund(string CustomerFirstName, string CustomerLastName, string CustomerStreet1, string CustomerStreet2,
            string CustomerCity, string CustomerState, string CustomerPostalCode, CountryCode CustomerCountryCode, CardType CreditCardType,
            string CreditCardNumber, int CreditCardExpirationMonth, int CreditCardExpirationYear, int TransactionAmount, string LineItemDescription,
            string Descriptor, string ClientTransactionID)
        {
            BPPSTxResponse bPPSTxResponse = ps.Refund(applicationID, password, CustomerFirstName, CustomerLastName, CustomerStreet1,
                CustomerStreet2, CustomerCity, CustomerState, CustomerPostalCode, CustomerCountryCode, CreditCardType, CreditCardNumber,
                CreditCardExpirationMonth, CreditCardExpirationYear, TransactionAmount, LineItemDescription, Descriptor, ClientTransactionID);

            return bPPSTxResponse;
        }
        #endregion
    }
}
