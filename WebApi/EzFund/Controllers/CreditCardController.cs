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
using Stripe;

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

            //Documentation:
            //https://stackoverflow.com/questions/62749790/how-to-integrate-stripe-payment-gateway-to-asp-net-mvc
            //https://www.c-sharpcorner.com/blogs/implement-stripe-payment-gateway-in-asp-net-mvc
            //https://www.nuget.org/packages/Stripe.net/

            //reading credentials from configuration
            string stripePublishableKey = ConfigurationManager.AppSettings["StripePublishableKey"];
            string stripePrivateKey = ConfigurationManager.AppSettings["StripePrivateKey"];

                //Setting temporary variables
                long expiryMonth;
                long.TryParse(model.ExpirationDate.Substring(0, 2), out expiryMonth);
                long expiryYear;
                long.TryParse(model.ExpirationDate.Substring(2, 2), out expiryYear);

                //Set API key with below function,
                //Stripe.StripeConfiguration.SetApiKey(“pk_test_FyPZYPyqf8jU6IdG2DONgudS”); taken c-sharpcorner.com example
                //SetApiKey is depreciated. Using StripeConfiguration.ApiKey
                StripeConfiguration.ApiKey = stripePrivateKey;
          
            

                var tokenOptions = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = model.Number,
                        ExpMonth = expiryMonth,
                        ExpYear = expiryYear,
                        Cvc = model.CVV,
                        AddressLine1 = model.Address.Address1,
                        AddressLine2 = model.Address.Address2,
                        AddressCity = model.Address.City,
                        AddressState = model.Address.Region.Code,
                        AddressZip = model.Address.PostCode,
                        AddressCountry = model.Address.Country.Code
                    }
                };

            var tokenService = new TokenService();
            Token stripeToken = tokenService.Create(tokenOptions);

            Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions();
            myCustomer.Name = model.Holder;
            myCustomer.Email = model.Email;
            myCustomer.Source = stripeToken.Id;
            //myCustomer.Shipping = new ShippingOptions()
            //{
            //    Name = model.Holder,
            //    Address = new AddressOptions()
            //    {
            //        Line1 = model.Address.Address1,
            //        PostalCode = model.Address.PostCode,
            //        State = model.Address.Region.Code,
            //        City = model.Address.City,
            //        Country = model.Address.Country.Code
            //    }
            //};

            var customerService = new Stripe.CustomerService();
            Stripe.Customer stripeCustomer = customerService.Create(myCustomer);



            var chargeOptions = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt64(model.Amount * 100), //amount should be in cents
                    Currency = getPaymentCurrency(model.Address.Country.Code).ToString(),
                    Description = $"Invoice:{model.Reference}",
                    Customer = stripeCustomer.Id,
                    ReceiptEmail = model.Email
                    //Source = stripeToken.Id
                };

                var paymentService = new ChargeService();
                var responseDescription = string.Empty;
                try
                {
                    Charge charge = paymentService.Create(chargeOptions);

                    responseDescription = getStripeErrorDescription(charge); //get a charge response full description

                    if (charge.Paid)
                    {
                        SentrySdk.CaptureMessage(responseDescription, Sentry.Protocol.SentryLevel.Info);
                        return Ok(new { charge.AuthorizationCode });
                    }
                    else
                    {
                        SentrySdk.CaptureMessage(responseDescription, Sentry.Protocol.SentryLevel.Info);
                        throw new ArgumentOutOfRangeException(
                           nameof(charge), responseDescription,
                           $"Unhandled {nameof(charge)} '{responseDescription}'");
                    }


                }
                catch (Exception exception)
                {
                    SentrySdk.CaptureMessage(responseDescription, Sentry.Protocol.SentryLevel.Info);
                    return InternalServerError(new Exception("Error while processing Credit Card. No Credit Card charge was done.", exception));
                }
           



        }

        private string getStripeErrorDescription(Charge charge)
        {
            return string.Join(Environment.NewLine,
               $"Amount: {charge.Amount / 100.00}",
               $"AmountCaptured: {charge.AmountCaptured / 100.00}",
               $"ApplicationFeeAmount: {charge.ApplicationFeeAmount}",
               $"ApplicationFeeId: {charge.ApplicationFeeId}",
               $"AuthorizationCode: {charge.AuthorizationCode}",
               $"BalanceTransactionId: {charge.BalanceTransactionId}",
               $"BillingDetails.StripeResponse.Content: {charge.BillingDetails?.StripeResponse?.Content}",
               $"Currency: {charge.Currency}",
               $"Description: {charge.Description}",
               $"DisputeId: {charge.DisputeId}",
               $"FailureCode: {charge.FailureCode}",
               $"FailureMessage: {charge.FailureMessage}",
               $"FraudDetails.StripeReport: {charge.FraudDetails?.StripeReport}",
               $"FraudDetails.StripeResponse: {charge.FraudDetails?.StripeResponse}",
               $"FraudDetails.UserReport: {charge.FraudDetails?.UserReport}",
               $"Id: {charge.Id}",
               $"InvoiceId: {charge.InvoiceId}",
               $"Level3.StripeResponse: {charge.Level3?.StripeResponse}",
               $"LiveMode: {charge.Livemode}",
               $"Object: {charge.Object}",
               $"OrderId: {charge.OrderId}",
               $"Outcome.SellerMessage: {charge.Outcome?.SellerMessage}",
               $"Paid: {charge.Paid}",
               $"PaymentIntentId: {charge.PaymentIntentId}",
               $"PaymentMethod: {charge.PaymentMethod}",
               $"ReceiptNumber: {charge.ReceiptNumber}",
               $"ReviewId: {charge.ReviewId}",
               $"Status: {charge.Status}",
               $"StripeResponse: {charge.StripeResponse}",
               $": {charge.TransferId}"
               );

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
                case InternalPaymentMethod.DinersClub:
                    return PaymentCardType.DINERS;
                default:
                    throw new ArgumentOutOfRangeException("internalPaymentMethod");
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

