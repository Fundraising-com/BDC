using System;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers.Exceptions;
using GA.BDC.Shared.Entities;
using GA.BDC.WebApi.EzFund.GAPayNETTCP;
using GA.BDC.Data.EzFund.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using Moneris;
using Sentry;
using Sentry.EntityFramework;

namespace GA.BDC.WebApi.EzFund.Controllers
{
    public class CreditCardController : ApiController
    {
        private IDisposable _sentry;
        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post(CreditCard model)
        {

            //sentry error handling
            SentryDatabaseLogging.UseBreadcrumbs();
            _sentry = SentrySdk.Init(o =>
            {
                // We store the DSN inside Web.config; make sure to use your own DSN!
                o.Dsn = new Dsn(ConfigurationManager.AppSettings["SentryDSN"]);

                // Get Entity Framework integration
                o.AddEntityFramework();
                o.SendDefaultPii = true;

            });


            bool IsMonerisPaymentServiceUsed = false;
            bool.TryParse(ConfigurationManager.AppSettings["IsMonerisPaymentServiceUsed"], out IsMonerisPaymentServiceUsed);
            if (IsMonerisPaymentServiceUsed)
            {
                return monerisClient(model);
            }
            else
            {
                return paymentProcessingService(model);
            }
        }

            private IHttpActionResult paymentProcessingService(CreditCard model)
            {

                try
                {
                    var client = new PaymentProcessingServiceClient(ConfigurationManager.AppSettings["GAPay.BindingName"]);

                    var amount = ((int)(model.Amount * 100)).ToString(CultureInfo.InvariantCulture);// we multiply * 100 because they are cents
                    var creditCardPaymentResult =
                        client.ProcessPaymentTransaction(
                            TranRequestType: PaymentRequestType.AUTHDEP,
                            PresenterId: "EzFund Store",
                            ClientId: PaymentClient.EZFUND,
                            RefId: model.Reference,
                            ReferenceSourceId: "",
                            ReferenceAuthNumber: "",
                            FirstName: model.Holder, //it has the full name
                            LastName: "",
                            Address1: model.Address.Address1,
                            Address2: "",
                            City: model.Address.City,
                            State: model.Address.Region.Code,
                            PostalCode: model.Address.PostCode,
                            Plus4Zip: "",
                            CountryCode: getPaymentCountry(model.Address.Country.Code),
                            EpvType: getCreditCardType(model.InternalPaymentMethod),
                            AccountNumber: model.Number,
                            CcExp: model.ExpirationDate,
                            CcSecurityCode: model.CVV,
                            BankRoutingNumber: "",
                            BankAccountType: "",
                            CurrencyCode: getPaymentCurrency(model.Address.Country.Code),
                            TranAmount: amount,
                            SalesTax: "0",
                            Descriptor: "",
                            DescriptorCityPhone: "",
                            ClientPassThrough: "",
                            ClientTranId: model.Reference,
                            RvrslTCSTranId: "",
                            RvrslTranDate: "",
                            RvrslAuthCode: "",
                            EncryptionFlag: "");

                    switch (creditCardPaymentResult.TranResponseType)
                    {
                        case PaymentResponseType.A: // Approved
                                                    /*Add the AuthNumber to the Order*/
                            using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
                            {
                                /* 
                                   * Adding AuthNumber to Order
                                   * 
                                   */
                                var saleRepository = eZFundProdUnitOfWork.CreateRepository<ISalesRepository>();
                                int orderId;
                                if (Int32.TryParse(model.Reference, out orderId))
                                {
                                    var status = saleRepository.UpdateFundPaymentReferenceByOrderId(orderId, creditCardPaymentResult.AuthNumber);
                                }
                            }
                            return Ok(new { creditCardPaymentResult.AuthNumber });

                        case PaymentResponseType.H: // Hard decline
                        case PaymentResponseType.S: // Soft decline
                        var ccError = new CreditCardDeclinedException(model.InternalPaymentMethod.ToString(), model.Amount, creditCardPaymentResult.ResponseCode, creditCardPaymentResult.ResponseRaw, null);
                        SentrySdk.CaptureException(ccError);
                        //new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, new CreditCardDeclinedException(model.InternalPaymentMethod.ToString(), model.Amount, creditCardPaymentResult.ResponseCode, creditCardPaymentResult.ResponseRaw, null));
                        return BadRequest($"{creditCardPaymentResult.ResponseMessage}. No Credit Card charge was done.");

                    case PaymentResponseType.D: // Duplicate Transaction
                        case PaymentResponseType.E: // Communication Error
                        var ccError2 = new CreditCardChargeException(model.InternalPaymentMethod.ToString(), model.Amount, "Error while processing Credit Card. No Credit Card charge was done.", string.Empty, string.Empty, null);
                        SentrySdk.CaptureException(ccError2);
                        return BadRequest("Error while processing Credit Card. No Credit Card charge was done.");

                        default:
                        var ccErrorSysErr = new ArgumentOutOfRangeException(nameof(creditCardPaymentResult.TranResponseType), (int)creditCardPaymentResult.TranResponseType,$"Unhandled {nameof(creditCardPaymentResult.TranResponseType)} '{creditCardPaymentResult.TranResponseType}");
                        SentrySdk.CaptureException(ccErrorSysErr);
                        throw new ArgumentOutOfRangeException(
                               nameof(creditCardPaymentResult.TranResponseType), (int)creditCardPaymentResult.TranResponseType,
                               $"Unhandled {nameof(creditCardPaymentResult.TranResponseType)} '{creditCardPaymentResult.TranResponseType}");
                    }
                }
                catch (Exception exception)
                {
                        var ccError = new CreditCardChargeException(model.InternalPaymentMethod.ToString(), model.Amount, "Error while processing Credit Card. No Credit Card charge was done.", string.Empty, string.Empty, exception);
                        SentrySdk.CaptureException(ccError);
                        SentrySdk.CaptureException(exception);
                        //new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, new CreditCardChargeException(model.InternalPaymentMethod.ToString(), model.Amount, string.Empty, string.Empty, string.Empty, exception));
                        return InternalServerError(new Exception("Error while processing Credit Card. No Credit Card charge was done.", exception));
                 }
            }
        
        private PaymentCountry getPaymentCountry(string countryCode)
        {
            switch (countryCode)
            {
                case "US":
                    return PaymentCountry.US;
                case "CA":
                    return PaymentCountry.CA;
                default:
                    return PaymentCountry.US;
            }
        }

        private PaymentCurrency getPaymentCurrency(string countryCode)
        {
            switch (countryCode)
            {
                case "US":
                    return PaymentCurrency.USD;
                case "CA":
                    return PaymentCurrency.CAD;
                default:
                    return PaymentCurrency.USD;
            }
        }

        private PaymentCardType getCreditCardType(InternalPaymentMethod internalPaymentMethod)
        {
            switch (internalPaymentMethod)
            {
                case InternalPaymentMethod.VISA:
                    return PaymentCardType.VISA;
                case InternalPaymentMethod.MASTERCARD:
                    return PaymentCardType.MASTERCARD;
                case InternalPaymentMethod.AMEX:
                    return PaymentCardType.AMEX;
                case InternalPaymentMethod.DISCOVER:
                    return PaymentCardType.DISCOVER;
                default:
                    throw new ArgumentOutOfRangeException("internalPaymentMethod");
            }
        }


        private IHttpActionResult monerisClient(CreditCard model)
        {
            //reading credentials from configuration depending on card address.
            string storeId = ConfigurationManager.AppSettings[$"Moneris{model.Address.Country.Code}StoreID"];
            string apiToken = ConfigurationManager.AppSettings[$"Moneris{model.Address.Country.Code}APIToken"];
            string hostName = ConfigurationManager.AppSettings[$"MonerisHostURL"];

            string order_id = model.Reference;
            string amount = model.Amount.ToString("#.00");
            string pan = model.Number; ; // "4242424242424242" for testing
            string expiryDate = model.ExpirationDate.Substring(2, 2) + model.ExpirationDate.Substring(0, 2); // from MMYY to YYMM format
            string crypt = "7";
            bool IsMonerisTestMode = false;
            bool.TryParse(ConfigurationManager.AppSettings["IsMonerisTestMode"], out IsMonerisTestMode);

            var s = model.Address.Address1;
            var firstSpaceIndex = s.IndexOf(" ");
            var streetNumber = s.Substring(0, firstSpaceIndex);
            var streetName = s.Substring(firstSpaceIndex + 1);

            bool status_check = false;

            AvsInfo avsCheck = new AvsInfo();
            avsCheck.SetAvsStreetNumber(streetNumber);
            avsCheck.SetAvsStreetName(streetName);
            avsCheck.SetAvsZipCode(model.Address.PostCode);
            avsCheck.SetAvsHostname(hostName);

            //avsCheck.SetAvsEmail("test@host.com");
            //avsCheck.SetAvsBrowser("Mozilla");
            //avsCheck.SetAvsShipToCountry(model.Address.Country.Name);
            //avsCheck.SetAvsShipMethod("G");
            //avsCheck.SetAvsMerchProdSku("123456");
            //avsCheck.SetAvsCustIp("192.168.0.1");
            //avsCheck.SetAvsCustPhone("5556667777");

            CvdInfo cvdCheck = new CvdInfo();
            cvdCheck.SetCvdIndicator("1");
            cvdCheck.SetCvdValue(model.CVV);

            Purchase purchase = new Purchase();
            purchase.SetOrderId(order_id);
            purchase.SetAmount(amount);
            purchase.SetPan(pan);
            purchase.SetExpDate(expiryDate);
            purchase.SetCryptType(crypt);
            purchase.SetAvsInfo(avsCheck);
            purchase.SetCvdInfo(cvdCheck);

            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetProcCountryCode("CA");
            mpgReq.SetTestMode(IsMonerisTestMode); //false or comment out this line for production transactions
            mpgReq.SetStoreId(storeId);
            mpgReq.SetApiToken(apiToken);
            mpgReq.SetTransaction(purchase);
            mpgReq.SetStatusCheck(status_check);
            mpgReq.Send();

            try
            {
                //To debug the response:
                //https://developer.moneris.com/Documentation/NA/E-Commerce%20Solutions/API/Purchase?lang=dotnet
                //https://developer.moneris.com/en/More/Testing/Testing%20a%20Solution
                //https://developer.moneris.com/More/Testing/Response%20Codes
                //https://developer.moneris.com/Documentation/NA/E-Commerce%20Solutions/API/Response%20Fields
                //https://developer.moneris.com/Documentation/NA/E-Commerce%20Solutions/API/Response%20Fields?lang=dotnet
                //https://developer.moneris.com/More/Testing/Penny%20Value%20Simulator
                //https://developer.moneris.com/en/More/Testing/AVS%20Result%20Codes
                Receipt receipt = mpgReq.GetReceipt();

                #region Completed and timedout
                bool completed = false;
                bool.TryParse(receipt.GetComplete(), out completed);
                string timedoutInString = receipt.GetTimedOut();
                bool? timedout = null;
                if (!(string.IsNullOrWhiteSpace(timedoutInString)) && timedoutInString != "null")
                {
                    bool timedoutInBool = false;
                    bool.TryParse(timedoutInString, out timedoutInBool);
                    timedout = timedoutInBool;
                }
                #endregion

                #region returned response, and message
                int tempInt = -1;
                int? responseCode = int.TryParse(receipt.GetResponseCode(), out tempInt) ? (int?)tempInt : null;
                string message = receipt.GetMessage();
                #endregion

                #region result codes
                string authorizationCode = receipt.GetAuthCode();
                string avsResultCode = receipt.GetAvsResultCode();
                string cvdResultCode = receipt.GetCvdResultCode();
                #endregion

                string t = getErrorDescription(receipt);

                if (completed && !(timedout ?? true) && responseCode.HasValue)
                {
                    if (responseCode < 50) //Approved
                    {
                        string goodDescription = getErrorDescription(receipt);
                        //SentrySdk.CaptureException(ccErrorExcept);
                        SentrySdk.CaptureMessage(goodDescription, Sentry.Protocol.SentryLevel.Info);
                        return Ok(new { authorizationCode });
                    }
                    else if (responseCode == 78 || responseCode == 84) // Duplicate Transaction
                    {
                        string errorDescription = getErrorDescription(receipt);
                        var ccErrorExcept = new CreditCardChargeException(model.InternalPaymentMethod.ToString(), model.Amount, string.Empty, string.Empty, errorDescription, null);
                        SentrySdk.CaptureException(ccErrorExcept);
                        //new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, new CreditCardChargeException(model.InternalPaymentMethod.ToString(), model.Amount, string.Empty, string.Empty, errorDescription, null));
                        return BadRequest("Error while processing Credit Card. No Credit Card charge was done.");
                    }
                    else if ((responseCode >= 50 && responseCode < 149) || (responseCode >= 400 && responseCode < 499) || (responseCode >= 800 && responseCode < 1000)) //Declined / Referral / 
                    {
                        string errorDescription = getErrorDescription(receipt);
                        var ccErrorDecline = new CreditCardDeclinedException(model.InternalPaymentMethod.ToString(), model.Amount, responseCode.ToString(), errorDescription, null);
                        SentrySdk.CaptureException(ccErrorDecline);
                        //new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, new CreditCardDeclinedException(model.InternalPaymentMethod.ToString(), model.Amount, responseCode.ToString(), errorDescription, null));
                        return BadRequest($"{message}. No Credit Card charge was done.");
                    }
                    else if (responseCode >= 150 && responseCode < 299) // System Error
                    {
                        string errorDescription = getErrorDescription(receipt);
                        var ccErrorSysErr = new CreditCardChargeException(model.InternalPaymentMethod.ToString(), model.Amount, string.Empty, string.Empty, errorDescription, null);
                        SentrySdk.CaptureException(ccErrorSysErr);
                        //new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, new CreditCardChargeException(model.InternalPaymentMethod.ToString(), model.Amount, string.Empty, string.Empty, errorDescription, null));
                        return BadRequest("Error while processing Credit Card. No Credit Card charge was done.");
                    }
                    else
                    {
                        string errorDescription = getErrorDescription(receipt);
                        var ccErrorSysErr = new ArgumentOutOfRangeException(nameof(receipt), errorDescription, $"Unhandled {nameof(receipt)} '{errorDescription}'");
                        SentrySdk.CaptureException(ccErrorSysErr);
                        throw new ArgumentOutOfRangeException(
                           nameof(receipt), errorDescription,
                           $"Unhandled {nameof(receipt)} '{errorDescription}'");
                    }
                }
                else
                {
                    string errorDescription = getErrorDescription(receipt);
                    var ccErrorSysErr = new ArgumentOutOfRangeException(nameof(receipt), errorDescription, $"Unhandled {nameof(receipt)} '{errorDescription}'");
                    SentrySdk.CaptureException(ccErrorSysErr);
                    throw new ArgumentOutOfRangeException(
                  nameof(receipt), errorDescription,
                  $"Unhandled {nameof(receipt)} '{errorDescription}");

                }
            }
            catch (Exception exception)
            {
                //new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, new CreditCardChargeException(model.InternalPaymentMethod.ToString(), model.Amount, string.Empty, string.Empty, string.Empty, exception));
                var ccErrorSysErr = new CreditCardChargeException(model.InternalPaymentMethod.ToString(), model.Amount, string.Empty, string.Empty, string.Empty, exception);
                SentrySdk.CaptureException(ccErrorSysErr);
                return InternalServerError(new Exception("Error while processing Credit Card. No Credit Card charge was done.", exception));
            }
        }

        private string getErrorDescription(Receipt receipt)
        {
            string errorDescription = string.Empty;

            errorDescription = "CardType = " + receipt.GetCardType() + Environment.NewLine;
            errorDescription += "TransAmount = " + receipt.GetTransAmount() + Environment.NewLine;
            errorDescription += "TxnNumber = " + receipt.GetTxnNumber() + Environment.NewLine;
            errorDescription += "ReceiptId = " + receipt.GetReceiptId() + Environment.NewLine;
            errorDescription += "TransType = " + receipt.GetTransType() + Environment.NewLine;
            errorDescription += "ReferenceNum = " + receipt.GetReferenceNum() + Environment.NewLine;
            errorDescription += "ResponseCode = " + receipt.GetResponseCode() + Environment.NewLine;
            errorDescription += "ISO = " + receipt.GetISO() + Environment.NewLine;
            errorDescription += "BankTotals = " + receipt.GetBankTotals() + Environment.NewLine;
            errorDescription += "Message = " + receipt.GetMessage() + Environment.NewLine;
            errorDescription += "AuthCode = " + receipt.GetAuthCode() + Environment.NewLine;
            errorDescription += "Complete = " + receipt.GetComplete() + Environment.NewLine;
            errorDescription += "TransDate = " + receipt.GetTransDate() + Environment.NewLine;
            errorDescription += "TransTime = " + receipt.GetTransTime() + Environment.NewLine;
            errorDescription += "Ticket = " + receipt.GetTicket() + Environment.NewLine;
            errorDescription += "TimedOut = " + receipt.GetTimedOut() + Environment.NewLine;
            errorDescription += "IsVisaDebit = " + receipt.GetIsVisaDebit() + Environment.NewLine;
            errorDescription += "HostId = " + receipt.GetHostId() + Environment.NewLine;
            errorDescription += "IssuerId = " + receipt.GetIssuerId() + Environment.NewLine;
            errorDescription += "WEBSITE - EZFUND = ";
            return errorDescription;
        }
    }



}

