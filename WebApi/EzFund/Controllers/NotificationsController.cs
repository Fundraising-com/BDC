using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.Helpers;
using System.Configuration;
using GA.BDC.WebApi.EzFund.Templates.Emails;
using GA.BDC.WebApi.EzFund.Helpers;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace GA.BDC.WebApi.EzFund.Controllers
{
    public class NotificationsController : ApiController
    {
        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post(Notification notification)
        {
            var notificationTxt = "";
            switch (notification.Type)
            {
                case NotificationType.Unknown:
                    break;
                case NotificationType.LeadEntry:
                    SendLeadCreatedNotification(notification);
                    notificationTxt = "SendLeadCreatedNotification";
                    break;

                case NotificationType.OrderCreated:
                    SendOrderCreatedNotification(notification);
                    notificationTxt = "SendOrderCreatedNotification";
                    break;
                case NotificationType.SaleCreated:
                    SendSaleCreatedNotification(notification);
                    notificationTxt = "SendSaleCreatedNotification";
                    break;
                case NotificationType.CreditCardChargeFailed:
                    SendCreditCardChargeFailedNotification(notification);
                    notificationTxt = "SendCreditCardChargeFailedNotification";
                    break;
                default:
                    throw new Exception("Notification Type doesn't exist");
            }
            return Ok(notificationTxt);
        }



        /// <summary>
        /// Sends an INTERNAL notification when a Sale has been created
        /// </summary>
        /// <param name="notification"></param>
        private void SendCreditCardChargeFailedNotification(Notification notification)
        {
            const string subject = "Ezfund Sales Credit Card Charge FAILED! Please follow up (INTERNAL).";

            using (var ezmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var salesRepository = ezmainUnitOfWork.CreateRepository<ISalesRepository>();
                var productRepository = ezmainUnitOfWork.CreateRepository<IProductRepository>();

                var sales = salesRepository.GetEzFundSaleByOrderId(notification.ExternalId);

                foreach (var saleItem in sales.SubProducts)
                {
                    var product = productRepository.GetSubProductByCode(saleItem.ItemCode);
                    saleItem.Name = product.Name;

                }

                var saleEmailTemplate = new SaleEmailTemplate(subject, sales, "A Customer placed an Order at the Ezfund.com Store using a Credit Card but the charge failed. Please follow up.");



                try
                {

                    var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("accept", "application/json");
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("api-key", "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");

                    var body = new { sender = new { name = "EzFund.com", email = "online@ezfund.com" }, to = new[] { new { email = "malcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "SalesSupport.EFR@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@ezfund.com", name = "EzFund.com" }, htmlContent = saleEmailTemplate.TransformText(), subject = "Ezfund Sales Credit Card Charge FAILED! Please follow up (INTERNAL)." };
                    request.AddBody(body);
                    var response = clientBlue.Execute(request);

                }


                catch (Exception ex)
                {
                    throw ex;
                }


                //    EmailManager.Send(new KeyValuePair<string, string>("online@ezfund.com", "EzFund.com"),
                //       ConfigurationManager.AppSettings["SaleFailedEmail"].Split(',')
                //          .ToDictionary(emailAddress => emailAddress),
                //       new KeyValuePair<string, string>("online@ezfund.com", "EzFund.com"), subject, string.Empty,
                //       saleEmailTemplate.TransformText(),
                //ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsCC"].Split(',')
                //         .ToDictionary(emailAddress => emailAddress),
                //       ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsBCC"].Split(',')
                //          .ToDictionary(emailAddress => emailAddress));



            }



        }







        /// <summary>
        /// Sends an INTERNAL notification when a Sale has been created
        /// </summary>
        /// <param name="notification"></param>
        private void SendSaleCreatedNotification(Notification notification)
        {
            const string subject = "EZfund.com Sales Created (INTERNAL)";

            using (var ezmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var salesRepository = ezmainUnitOfWork.CreateRepository<ISalesRepository>();
                var productRepository = ezmainUnitOfWork.CreateRepository<IProductRepository>();

                var sales = salesRepository.GetEzFundSaleByOrderId(notification.ExternalId);

                foreach (var saleItem in sales.SubProducts)
                {
                    var product = productRepository.GetSubProductByCode(saleItem.ItemCode);
                    saleItem.Name = product.Name;

                }

                var saleEmailTemplate = new SaleEmailTemplate(subject, sales);


                try
                {

                    var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("accept", "application/json");
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("api-key", "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");

                    var body = new { sender = new { name = "EzFund.com", email = "online@ezfund.com" }, to = new[] { new { email = "malcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "SalesSupport.EFR@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@ezfund.com", name = "EzFund.com" }, htmlContent = saleEmailTemplate.TransformText(), subject = "EZfund.com Sales Created (INTERNAL)" };
                    request.AddBody(body);
                    var response = clientBlue.Execute(request);

                }


                catch (Exception ex)
                {
                    throw ex;
                }

                //EmailManager.Send(new KeyValuePair<string, string>("online@ezfund.com", "EzFund.com"),
                //       ConfigurationManager.AppSettings["SaleCreatedEmails"].Split(',')
                //          .ToDictionary(emailAddress => emailAddress),
                //       new KeyValuePair<string, string>("online@ezfund.com", "EzFund.com"), subject, string.Empty,
                //       saleEmailTemplate.TransformText(),
                //ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsCC"].Split(',')
                //         .ToDictionary(emailAddress => emailAddress),
                //       ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsBCC"].Split(',')
                //          .ToDictionary(emailAddress => emailAddress));



            }
        }


        /// <summary>
        /// Sends an EXTERNAL notification to the Client, saying that his Order was created.
        /// </summary>
        /// <param name="notification"></param>
        private void SendOrderCreatedNotification(Notification notification)
        {
            const string subject = "EzFund.com Order Created";
            using (var ezmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var salesRepository = ezmainUnitOfWork.CreateRepository<ISalesRepository>();
                var productRepository = ezmainUnitOfWork.CreateRepository<IProductRepository>();

                //var clientsRepository = EzmainUnitOfWork.CreateRepository<IClientsRepository>();
                var sales = salesRepository.GetEzFundSaleByOrderId(notification.ExternalId);

                foreach (var saleItem in sales.SubProducts)
                {
                    var product = productRepository.GetSubProductByCode(saleItem.ItemCode);
                    saleItem.Name = product.Name;

                }

                var orderCreatedEmailTemplate = new OrderCreatedEmailTemplate(subject, sales);
                try
                {

                    var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("accept", "application/json");
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("api-key", "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");

                    var body = new { sender = new { name = "EzFund.com", email = "online@ezfund.com" }, to = new[] { new { email = sales.Client.Email, name = sales.Client.FirstName + " " + sales.Client.LastName } }, bcc = new[] { new { email = "malcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "SalesSupport.EFR@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@ezfund.com", name = "EzFund.com" }, htmlContent = orderCreatedEmailTemplate.TransformText(), subject = "EzFund.com Order Created" };
                    request.AddBody(body);
                    var response = clientBlue.Execute(request);

                }


                catch (Exception ex)
                {
                    throw ex;
                }

                //EmailManager.Send(new KeyValuePair<string, string>("online@ezfund.com", "EzFund.com"),
                //   new Dictionary<string, string> { { sales.Client.Email, sales.Client.FirstName + " " + sales.Client.LastName } },
                //   new KeyValuePair<string, string>("online@ezfund.com", "EzFund.com"), subject, string.Empty,
                //   orderCreatedEmailTemplate.TransformText(),
                //   ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsCC"].Split(',')
                //      .ToDictionary(emailAddress => emailAddress),
                //   ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsBCC"].Split(',')
                //      .ToDictionary(emailAddress => emailAddress));


            }

        }


        /// <summary>
        /// Sends an INTERNAL notification, saying that a new Lead has been received.
        /// </summary>
        /// <param name="notification"></param>
        private void SendLeadCreatedNotification(Notification notification)
        {
            const string subject = "A EZFUND new lead has been added";

            using (var ezmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var prospectRepository = ezmainUnitOfWork.CreateRepository<IProspectRepository>();
                var lead = prospectRepository.GetProspectById(notification.ExternalId);

                var leadReceivedEmailTemplate = new LeadEmailTemplate(subject, lead);

                try
                {

                    var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("accept", "application/json");
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("api-key", "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");

                    var body = new { sender = new { name = "EzFund.com", email = "online@ezfund.com" }, to = new[] { new { email = "malcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "SalesSupport.EFR@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@ezfund.com", name = "EzFund.com" }, htmlContent = leadReceivedEmailTemplate.TransformText(), subject = "A EZFUND new lead has been added" };
                    request.AddBody(body);
                    var response = clientBlue.Execute(request);

                }


                catch (Exception ex)
                {
                    throw ex;
                }


                //EmailManager.Send(new KeyValuePair<string, string>("online@ezfund.com", "EzFund.com"),
                //   ConfigurationManager.AppSettings["LeadReceivedReport"].Split(',')
                //      .ToDictionary(emailAddress => emailAddress),
                //   new KeyValuePair<string, string>("online@ezfund.com", "EzFund.com"), subject, string.Empty,
                //   leadReceivedEmailTemplate.TransformText());
            }
        }
    }
}
