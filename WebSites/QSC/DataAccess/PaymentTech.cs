using System; 
using System.Runtime.InteropServices;
using QSPFulfillment.DataAccess.com.rd.epipe;

// for certificate name mismatch workaround
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace QSPFulfillment
{ 
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class PaymentTechInterface
	{ 
		private const int ACTION_SUCCEEDED 			=  0;
		private const int GENERAL_FAILURE 			= -1;
		private const int AUTHENTICATION_FAILED 	= -2;
		private const int ACTION_DISALLOWED 		= -3;
		private const int MALFORMED_REQUEST			= -4;
		private const int ACTION_FAILED 			= -5;

		private const string userId					= "QSP";
		private const string password				= "jGTy76hkiFGdfc9rjk";

		private const int usaCountryId					= 1;
		private const int usaPaymentTechDivision		= 152603;
		private const int canadaCountryId				= 2;
		private const int canadaPaymentTechDivision		= 080449;

		public enum ResponseCode 
		{
			ACTION_SUCCEEDED 			=  0,
			GENERAL_FAILURE 			= -1,
			AUTHENTICATION_FAILED 		= -2,
			ACTION_DISALLOWED 			= -3,
			MALFORMED_REQUEST			= -4,
			ACTION_FAILED 				= -5,
		}

		private OrderServiceRequest orderServiceRequest;

		public PaymentTechInterface() 
		{
			orderServiceRequest = new OrderServiceRequest();
			orderServiceRequest.password = password;
			orderServiceRequest.userId = userId;
			orderServiceRequest.fulfillmentChannelId = 20000;
			orderServiceRequest.divisionId = usaPaymentTechDivision;
			orderServiceRequest.countryId = usaCountryId;

			// force the server's certificate to be accepted
			System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();
		} 

		/// <summary>
		/// Transactions will be marked as US currency
		/// Funds will be directed into the US PaymentTech division
		/// </summary>
		public void setUSFlag()
		{
			orderServiceRequest.divisionId = usaPaymentTechDivision;
			orderServiceRequest.countryId = usaCountryId;
		}

		/// <summary>
		/// Transactions will be marked as CA currency
		/// Funds will be directed into the CA PaymentTech division
		/// </summary>
		public void setCanadaFlag()
		{
			orderServiceRequest.divisionId = canadaPaymentTechDivision;
			orderServiceRequest.countryId = canadaCountryId;
		}

		/// <summary>
		/// Authorizes a customer's credit card for the specified amount
		/// Save the paymentId to use in the "Settle" transaction (see below)
		/// </summary>
		public string Authorize(double dAmt, string sCCNumber, int iCCExpMonth, int iCCExpYear, string sFirstName, string sLastName, string sAddress1, string sAddress2, string sCity, string sPostalCode) 
		{
			Init("authorize", dAmt, sCCNumber, iCCExpMonth, iCCExpYear, sFirstName, sLastName, sAddress1, sAddress2, sCity, sPostalCode);

			OrderServiceService orderServiceService = new OrderServiceService();
			orderServiceService.Timeout = 300000;
			OrderServiceResponse oResponse = orderServiceService.processOrder(orderServiceRequest);

			return oResponse.resultCode + "|" + oResponse.authCode + "|" + oResponse.orderId + "|" + oResponse.paymentId + "|" + oResponse.message;
		}

		/// <summary>
		/// Settles a previously-authorized card
		/// pass the paymentId (from the authorize response) in the orderId field
		/// </summary>
		public string Settle(int paymentId) 
		{
			orderServiceRequest.action = "chargeonly";
			orderServiceRequest.orderId = paymentId;

			OrderServiceService orderServiceService = new OrderServiceService();
			orderServiceService.Timeout = 300000;
			OrderServiceResponse oResponse = orderServiceService.processOrder(orderServiceRequest);

			return oResponse.resultCode + "|" + oResponse.authCode + "|" + oResponse.orderId + "|" + oResponse.paymentId + "|" + oResponse.message;
		}

		/// <summary>
		/// Charges a credit card for the specified amount immediately
		/// </summary>
		public string ImmediateSale(double dAmt, string sCCNumber, int iCCExpMonth, int iCCExpYear, string sFirstName, string sLastName, string sAddress1, string sAddress2, string sCity, string sPostalCode) 
		{
			Init("charge", dAmt, sCCNumber, iCCExpMonth, iCCExpYear, sFirstName, sLastName, sAddress1, sAddress2, sCity, sPostalCode);

			OrderServiceService orderServiceService = new OrderServiceService();
			OrderServiceResponse oResponse = orderServiceService.processOrder(orderServiceRequest);

			return oResponse.resultCode + "|" + oResponse.authCode + "|" + oResponse.orderId + "|" + oResponse.paymentId + "|" + oResponse.message;
		}

		/// <summary>
		/// Credits an arbitrary amount to a card
		/// Needs to be settled w/ the Settle method after success
		/// </summary>
		public string Credit(double dAmt, string sCCNumber, int iCCExpMonth, int iCCExpYear, string sFirstName, string sLastName, string sAddress1, string sAddress2, string sCity, string sPostalCode) 
		{
			Init("authorize", dAmt, sCCNumber, iCCExpMonth, iCCExpYear, sFirstName, sLastName, sAddress1, sAddress2, sCity, sPostalCode);
			orderServiceRequest.payStatusId = 11;
			orderServiceRequest.paymentTypeId = 5;

			OrderServiceService orderServiceService = new OrderServiceService();
			orderServiceService.Timeout = 300000;
			OrderServiceResponse oResponse = orderServiceService.processOrder(orderServiceRequest);

			return oResponse.resultCode + "|" + oResponse.authCode + "|" + oResponse.orderId + "|" + oResponse.paymentId + "|" + oResponse.message;
		}

		/// <summary>
		/// Cancels or voids a previous transaction -- pass the paymentId in the orderId field
		/// NOT IMPLEMENTED ON RD.COM'S END
		/// </summary>
		public string Refund(int paymentId) 
		{
			orderServiceRequest.action = "cancelcharge";
			orderServiceRequest.orderId = paymentId;

			OrderServiceService orderServiceService = new OrderServiceService();
			OrderServiceResponse oResponse = orderServiceService.processOrder(orderServiceRequest);

			return oResponse.resultCode + "|" + oResponse.authCode + "|" + oResponse.orderId + "|" + oResponse.paymentId + "|" + oResponse.message;
		}

		/// <summary>
		/// Tests the connection to the webservice
		/// </summary>
		public string TestConnection() 
		{
			Init("authenticate", 1, "4111111111111111", 12, 2010, "TestFirst", "TestLast", "Test Address 1", "Test Address 2", "Test City", "12345");

			OrderServiceService orderServiceService = new OrderServiceService();
			OrderServiceResponse oResponse = orderServiceService.processOrder(orderServiceRequest);

			return oResponse.resultCode + "|" + oResponse.authCode + "|" + oResponse.orderId + "|" + oResponse.paymentId + "|" + oResponse.message;
		}

		/// <summary>
		/// Used by several methods to initialize the request object
		/// <summary>
		private void Init(string sAction, double dAmt, string sCCNumber, int iCCExpMonth, int iCCExpYear, string sFirstName, string sLastName, string sAddress1, string sAddress2, string sCity, string sPostalCode) 
		{

			orderServiceRequest.amount = dAmt;
			orderServiceRequest.cardNumber = sCCNumber;
			orderServiceRequest.expMonth = iCCExpMonth;
			orderServiceRequest.expYear = iCCExpYear;
			orderServiceRequest.nameOnCard = sFirstName + " " + sLastName;
			orderServiceRequest.address1 = sAddress1;
			orderServiceRequest.address2 = sAddress2;
			orderServiceRequest.city = sCity;
			orderServiceRequest.postalCode = sPostalCode;

			orderServiceRequest.firstName = sFirstName;
			orderServiceRequest.lastName = sLastName;
			orderServiceRequest.orderAmount = dAmt;
			orderServiceRequest.submittedPrice = dAmt;

			orderServiceRequest.orderStatus = -1;
			orderServiceRequest.payStatusId = 1;
			orderServiceRequest.paymentTypeId = 2;
			orderServiceRequest.orderType = 2;
			orderServiceRequest.promoKey = "Dummy Item";
			orderServiceRequest.quantity = 1;
			orderServiceRequest.orderStatus = -1;
			orderServiceRequest.billingIsShipping = false;
			orderServiceRequest.orderCalculationMethodId = 2;

			if (new String(sCCNumber[0],1) == "4") 
			{
				orderServiceRequest.creditCardTypeId = 2;
			} 
			else 
			{
				orderServiceRequest.creditCardTypeId = 1;
			}

			orderServiceRequest.action = sAction;
		}
	}  

	// workaround to force the server's certificate to be accepted
	// needed since we're connecting to rd.com's server via backend IP
	// (certificate names won't match in this case)
	public class MyPolicy : ICertificatePolicy 
	{
		public bool CheckValidationResult(
			ServicePoint srvPoint
			, X509Certificate certificate
			, WebRequest request
			, int certificateProblem) 
		{

			return true;

		}
	}

}
