using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Security;

using efundraising.eFundData.com.rd.epipe;
//using efundraising.eFundData.com.rdigest.us.epipetest1;
using efundraising.Diagnostics;
using efundraising.EFundraisingCRM;

// for certificate name mismatch workaround
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace efundraising.eFundData
{
	/// <summary>
	/// Summary description for PaymenTech.
	/// </summary>
	[WebService(Namespace="http://webservices.efundraising.com/PaymenTech", Description="Web Service for credit card transactions through PaymenTech")]
	public class PaymenTech : System.Web.Services.WebService
	{
		// constants for index keys
		private const string LAST_ERROR_KEY = "LastError";
		
		// constants for status of the transaction
		private const int ACTION_SUCCEEDED 			=  0;
		private const int GENERAL_FAILURE 			= -1;
		private const int AUTHENTICATION_FAILED 	= -2;
		private const int ACTION_DISALLOWED 		= -3;
		private const int MALFORMED_REQUEST			= -4;
		private const int ACTION_FAILED 			= -5;

		private const string userId                 = "efrus";
		private const string password               = "3fru5gfjhgvghm";

		// US country id = 1
		private const int countryId					= 1;
		private const int paymentechDivision		= 154999; // <FILL IN WITH DIVISION ID> - from QSP - US : 152603, CA: 80449

		private OrderServiceRequest orderServiceRequest;
		
		public PaymenTech()
		{
            
			InitializeComponent();
			orderServiceRequest = new OrderServiceRequest();
			orderServiceRequest.password = password;
			orderServiceRequest.userId = userId;
			orderServiceRequest.fulfillmentChannelId = 20200;
			orderServiceRequest.divisionId = paymentechDivision;
			orderServiceRequest.countryId = countryId;

			// force the server's certificate to be accepted
			System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		#region Web Methods

		/// <summary>
		/// Authorizes a customer's credit card for the specified amount
		/// Save the paymentId to use in the "Settle" transaction (see below)
		/// </summary>
		[WebMethod(EnableSession=true,MessageName="Authorize", Description="Authorizes a customer's credit card for the specified amount.")]
		public string Authorize(double amt, string ccNumber, int ccExpMonth, int ccExpYear, string ccCardSecurityCode, string fullName, string address1, string address2, string city, string state, string postalCode, string comments, bool isSecurityCheck) 
		{
			Init("authorize", amt, ccNumber, ccExpMonth, ccExpYear, ccCardSecurityCode, fullName, address1, address2, city, state, postalCode, comments, isSecurityCheck);

			OrderServiceService orderServiceService = new OrderServiceService();
			OrderServiceResponse oResponse = orderServiceService.processOrder(orderServiceRequest);

			return oResponse.resultCode + "|" + oResponse.authCode + "|" + oResponse.orderId + "|" + oResponse.paymentId + "|" + oResponse.message;
		}

		/// <summary>
		/// Settles a previously-authorized card
		/// pass the paymentId (from the authorize response) in the orderId field
		/// </summary>
		[WebMethod(EnableSession=true,MessageName="Settle", Description="Settles a previously-authorized card.")]
		public string Settle(int paymentId) 
		{
			orderServiceRequest.action = "chargeonly";
			orderServiceRequest.orderId = paymentId;

			OrderServiceService orderServiceService = new OrderServiceService();
			OrderServiceResponse oResponse = orderServiceService.processOrder(orderServiceRequest);
																									
			return oResponse.resultCode + "|" + oResponse.authCode + "|" + oResponse.orderId + "|" + oResponse.paymentId + "|" + oResponse.message;
		}

		/// <summary>
		/// Credits an arbitrary amount to a card
		/// Needs to be settled w/ the Settle method after success
		/// </summary>
		[WebMethod(EnableSession=true,MessageName="Credit", Description="Credits an arbitrary amount to a card.")]
		public string Credit(double amt, string ccNumber, int ccExpMonth, int ccExpYear, string ccCardSecurityCode, string fullName, string address1, string address2, string city, string state, string postalCode, string comments, bool isSecurityCheck)
		{
			Init("authorize", amt, ccNumber, ccExpMonth, ccExpYear, ccCardSecurityCode, fullName, address1, address2, city, state, postalCode, comments, isSecurityCheck);
			orderServiceRequest.payStatusId = 11;
			orderServiceRequest.paymentTypeId = 5;

			OrderServiceService orderServiceService = new OrderServiceService();
			OrderServiceResponse oResponse = orderServiceService.processOrder(orderServiceRequest);

			return oResponse.resultCode + "|" + oResponse.authCode + "|" + oResponse.orderId + "|" + oResponse.paymentId + "|" + oResponse.message;
		}

		/// <summary>
		/// Cancels or voids a previous transaction -- pass the paymentId in the orderId field
		/// </summary>
		[WebMethod(EnableSession=true,MessageName="Refund", Description="Cancels or voids a previous transaction.")]
		public string Refund(int paymentId) 
		{
			orderServiceRequest.action = "cancelcharge";
			orderServiceRequest.orderId = paymentId;

			OrderServiceService orderServiceService = new OrderServiceService();
			OrderServiceResponse oResponse = orderServiceService.processOrder(orderServiceRequest);

			return oResponse.resultCode + "|" + oResponse.authCode + "|" + oResponse.orderId + "|" + oResponse.paymentId + "|" + oResponse.message;
		}
		
		
		
		#endregion 
		
		#region Methods

		/// <summary>
		/// Used by several methods to initialize the request object
		/// <summary>
		private void Init(string sAction, double amt, string ccNumber, int ccExpMonth, int ccExpYear, string ccCardSecurityCode, string fullName, string address1, string address2, string city, string state, string postalCode, string comments, bool isSecurityCheck) 
		{

			orderServiceRequest.amount = amt;
			orderServiceRequest.cardNumber = ccNumber;
			orderServiceRequest.expMonth = ccExpMonth;
			orderServiceRequest.expYear = ccExpYear;
			orderServiceRequest.securityCode = ccCardSecurityCode;
			orderServiceRequest.nameOnCard = fullName;
			orderServiceRequest.address1 = address1;
			orderServiceRequest.address2 = address2;
			orderServiceRequest.state = state;
			orderServiceRequest.city = city;
			orderServiceRequest.postalCode = postalCode;
			orderServiceRequest.comments = comments;

			orderServiceRequest.orderAmount = amt;
			orderServiceRequest.submittedPrice = amt;

			orderServiceRequest.orderStatus = -1;
			orderServiceRequest.payStatusId = 1;
			orderServiceRequest.paymentTypeId = 2;
			orderServiceRequest.orderType = 2;
			orderServiceRequest.promoKey = "Dummy Item";
			orderServiceRequest.quantity = 1;			
			orderServiceRequest.billingIsShipping = false;
			orderServiceRequest.orderCalculationMethodId = 2;

			orderServiceRequest.isSecurityCheck = isSecurityCheck; 

			switch ( new String(ccNumber[0],1) ) 
			{
				case "3" : orderServiceRequest.creditCardTypeId = 4; break; // Amex
				case "4" : orderServiceRequest.creditCardTypeId = 2; break; // Visa
				case "5" : orderServiceRequest.creditCardTypeId = 1; break; // Mastercard
				case "6" : orderServiceRequest.creditCardTypeId = 3; break; // Discover
				default : orderServiceRequest.creditCardTypeId = -1; break; // Unknown - Error
			}

			orderServiceRequest.action = sAction;
		}
		
		#endregion
		
		#region Login Methods
		
		/// <summary>
		/// Login method for the user of web service
		/// </summary>
		/// <param name="pUsername">Username to login in</param>
		/// <param name="pPassword">Password of the username</param>
		/// <returns></returns>
		[WebMethod(EnableSession=true,MessageName="Login", Description="Provide your Username and Password to Login on WebService.efundraising.com")]
		public bool Login(string pUsername, string pPassword) 
		{
			ClearError();

			string call = MethodCall("Login", pUsername, pPassword);
			Logger.LogInfo(call);

			try 
			{
				if (FormsAuthentication.Authenticate(pUsername, pPassword))
				{
					FormsAuthentication.SetAuthCookie(pUsername, false);
					return true;
				}
				else
				{
					PublishError("Login failed.", call);
					return false;
				}
			}
			catch (Exception ex)
			{
				PublishError("Login failed.", call, ex);
			}

			return false;
		}

		/// <summary>
		/// Function for Logout the user of web service
		/// </summary>
		/// <returns></returns>
		[WebMethod(EnableSession=true,Description="Provide the way to Logout of webservice.efundraising.com")]
		public bool Logout() 
		{
			ClearError();

			string call = MethodCall("Logout");
			Logger.LogInfo(call);

			if(User.Identity.IsAuthenticated) 
			{
				FormsAuthentication.SignOut();

				// Clear session
				Session.Clear();

				return true;
			} 
			else
				return false;
		}

		/// <summary>
		/// Function returning the value about if the current Web Service user is properly Authenticated
		/// </summary>
		/// <returns></returns>
		[WebMethod(EnableSession=true, Description="Get if the current user is authenticate")]
		public bool IsLoggedIn() 
		{
			ClearError();

			string call = MethodCall("IsLoggedIn");
			Logger.LogInfo(call);

			return User.Identity.IsAuthenticated;
		}		
		
		#endregion
		
		#region Error Methods

		/// <summary>
		/// Clear the Current Error object
		/// </summary>
		public void ClearError() 
		{
			Session[LAST_ERROR_KEY] = "";
		}

		/// <summary>
		/// Function returning the last Error encounted by the current Web Service User
		/// </summary>
		/// <returns></returns>
		/// <remarks>Each Web Service Users have their own Error variable</remarks>
		[WebMethod(EnableSession=true, Description="Get the Lastest errors encounted on your Web Service session")]
		public string GetLastError() 
		{
			if (Session[LAST_ERROR_KEY] == null)
				return "";
			else
			{
				return (string) Session[LAST_ERROR_KEY];
			}
		}

		/// <summary>
		/// Set last error.
		/// </summary>
		/// <param name="error"></param>
		public void SetError(string error) 
		{
			Session[LAST_ERROR_KEY] = error;
		}

		#endregion
		
		#region Helper Methods
		/// <summary>
		/// Dump string representation of method call.
		/// </summary>
		/// <param name="methodName"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private string MethodCall(string methodName, params object[] parameters)
		{
			string call = methodName + "(";
			
			// Write out parameters
			if (parameters != null)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					if (i > 0)
						call += ", ";

					try 
					{
						if (parameters[i] == null)
							call += "null";
						else if (parameters[i].GetType().FullName == "System.String")
							call += "\"" + parameters[i] + "\"";
						else
							call += parameters[i].ToString();
					}
					catch 
					{
						call += "?";
					}
				}
			}

			call += ")";

			return call;
		}
		
		private void PublishError(string message)
		{
			PublishError(message, "");
		}

		private void PublishError(string message, string methodCall)
		{
			PublishError(message, methodCall, null);
		}

		private void PublishError(string message, string methodCall, Exception ex)
		{
			SetError(message);
			Logger.LogError(message + " " + methodCall, ex);
		}
		#endregion
		
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
