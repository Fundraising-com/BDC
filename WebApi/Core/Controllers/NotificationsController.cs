using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.WebApi.Fundraising.Core.Helpers;
using GA.BDC.WebApi.Fundraising.Core.Templates.Emails;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

using System.Threading.Tasks;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
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
            switch (notification.Type)
            {
                case NotificationType.Unknown:
                    break;
                case NotificationType.KitRequest:
                    SendKitRequestedNotification(notification);
                    break;
                case NotificationType.LeadEntry:
                    SendLeadCreatedNotification(notification);
                    break;
                case NotificationType.InformationRequested:
                    SendRepresentativeInformationRequestedNotification(notification);
                    break;
                case NotificationType.OrderConfirmation:
                    SendOrderConfirmationNotification(notification);
                    break;
                case NotificationType.OrderCreated:
                    SendOrderCreatedNotification(notification);
                    break;
                case NotificationType.SaleCreated:
                    SendSaleCreatedNotification(notification);
                    break;
                case NotificationType.DuplicatedLead:
                    SendLeadDuplicatedEmailNotification(notification);
                    break;
                case NotificationType.SaleCreationFailed:
                    SendSaleCreationFailedNotification(notification);
                    break;
                case NotificationType.SaleProcessFailed:
                    SendSaleProcessFailedNotification(notification);
                    break;
                case NotificationType.CreditCardChargeFailed:
                    SendCreditCardChargeFailedNotification(notification);
                    break;
                case NotificationType.PaypalPaymentStarted:
                    SendPaypalPaymentStartedNotification(notification);
                    break;
                case NotificationType.PotentialDuplicateLead:
                    SendPotentialDuplicateLeaNotification(notification);
                    break;
                //case NotificationType.OrderFollowUp:
                //    SendSaleFollowUp(notification);
                //    break;
                case NotificationType.SalePaid:
                    SendSalePaidNotification(notification);
                    break;
                case NotificationType.ProductReviewSubmitted:
                    SendProductReviewSubmittedNotification(notification);
                    break;
                //case NotificationType.SalesScreenOrderConfirmation:
                //    SendSalesScreenOrderConfirmation(notification);
                //    break;
                //case NotificationType.SportsApparelGetStarted:
                //    SendSportsApparelGetStarted(notification);
                //    break;
                //case NotificationType.SendFundPassPromoCode:
                //    SendFundPassPromoCode(notification);
                //    break;
                //case NotificationType.ApparelSalesScreenSale:
                //    ApparelSalesScreenSaleCreatedNotification(notification);
                //    break;
                //case NotificationType.SalesScreenSendClientShippingDetails:
                //    SalesScreenSendClientShippingDetails(notification);
                //    break;
                case NotificationType.SendBrochureOrder:
                    SendBrochureOrder(notification);
                    break;
                case NotificationType.SendInBlueTest:
                    SendInBlueTest(notification);
                    break;
                default:
                    throw new Exception("Notification Type doesn't exist");
            }
            return Ok();
        }


        private Task SendInBlueTest(Notification notification)
        {



            try
            {

                var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("api-key", "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");

                //request.AddParameter("application/json",
                //    "{\"sender\":{\"name\":\"jason farrell\",\"email\":\"jason.farrell@fundraising.com\"}," +
                //    "\"to\":[{\"email\":\"touchedbymusic@gmail.com\",\"name\":\"Jason A Farrell\"}]," +
                //    "\"replyTo\":{\"email\":\"jason.farrell@fundraising.com\",\"name\":\"JF\"}," +
                //    "\"subject\":\"test 6 lead\",\"templateId\":1}", ParameterType.RequestBody);
                //request.RequestFormat = DataFormat.Json;
                //request.AddBody(new { A = "foo", B = "bar" });

                
                var body = new { sender = new { name = "JWORK", email = "jason.farrell@fundraising.com" }, to = new[] { new { email = "touchedbymusic@gmail.com", name = "Jason" } }, replyTo = new { email = "jason.farrell@fundraising.com", name = "JWORK" }, templateId = 1 };
                request.AddBody(body);
                var response = client.Execute(request);
                return response;


                //var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                //var request = new RestRequest(Method.POST);
                //request.AddHeader("accept", "application/json");
                //request.AddHeader("content-type", "application/json");
                //request.AddHeader("api-key", "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");
                //request.AddParameter("application/json", "{\"sender\":{\"name\":\"JWORK\",\"email\":\"jason.farrell@fundraising.com\"},\"to\":[{\"email\":\"touchedbymusic@gmail.com\",\"name\":\"JHOME\"}],\"replyTo\":{\"email\":\"jason.farrell@fundraising.com\",\"name\":\"JWORK\"},\"templateId\":1}", ParameterType.RequestBody);
                //IRestResponse response = client.Execute(request);




            }


            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SendBrochureOrder(Notification notification)
        {
            const string subject = "Fundraising.com Order-Taker Request ";
            var orderInfo = notification;
            try
            {
                using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                {
                    //var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
                    //var product = productRepository.GetById(notification.ExternalId, true);
                    var saleOrderTakerTemplate = new OrderTakerEmailTemplate(subject, orderInfo);


                    try
                    {

                        var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = "Marc.Alcindor@fundraising.com", name = "Marc" }, new { email = "sadday.zivec@fundraising.com", name = "Saddaycita" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "support@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = saleOrderTakerTemplate.TransformText(), subject = "Fundraising.com Order-Taker Request " };
                        request.AddBody(body);
                        var response = clientBlue.Execute(request);

                    }


                    catch (Exception ex)
                    {
                        throw ex;
                    }




                    //    EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                    //       ConfigurationManager.AppSettings["BrochureSubmitted"].Split(',')
                    //          .ToDictionary(emailAddress => emailAddress),
                    //       new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                    //       saleOrderTakerTemplate.TransformText());

                    //    notification = new Notification
                    //    {
                    //        Type = NotificationType.SendBrochureOrder,
                    //        ExternalId = 999,
                    //        Email = "fake@fake.com",
                    //        ExtraData = null,
                    //        Created = DateTime.Now
                    //    };
                    //    var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
                    //    notificationRepository.Save(notification);
                    //    efundstoreUnitOfWork.Commit();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }





        /// <summary>
        /// Sends an EXTERNAL notification to the Client, saying that his Order was confirmed.
        /// </summary>
        /// <param name="notification"></param>
        //private void SalesScreenSendClientShippingDetails(Notification notification)
        //{
        //    using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
        //    {
        //        var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
        //        var clientsRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
        //        int delayMinutes; //Delay on minutes that this notification will take
        //        int.TryParse(notification.ExtraData, out delayMinutes);


        //        using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
        //        {

        //            var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
        //            //var salesShippingDetails = salesRepository.GetSaleShippingDetails();

        //            var client = clientsRepository.GetById(notification.ExternalId);
        //            var sales = salesRepository.GetByClientId(client.Id);
        //            //const string subject = "Fundraising.com Order Shipping Details";
        //            //foreach (var sale in salesShippingDetails)
        //            //{
        //            //    var clientShippingAddress = clientsRepository.GetShippingAddressById(sale.ClientId);


        //            //}

        //            //foreach (var sale in salesShippingDetails)
        //            //{
        //            //    sale.Client = client;
        //            //    foreach (var saleItem in sale.Items)
        //            //    {
        //            //        saleItem.Product = productRepository.GetByScratchbookId(saleItem.ScratchBookId);
        //            //        saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
        //            //           ? saleItem.Product.Profits.First(
        //            //              profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
        //            //           : saleItem.Product.Price;
        //            //    }
        //            //}
        //            //var orderShippingDetailsEmailTemplate = new OrderConfirmationEmailTemplate(subject, salesShippingDetails);
        //            //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
        //            //   new Dictionary<string, string> { { client.Email, client.FirstName + " " + client.LastName } },
        //            //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
        //            //   orderShippingDetailsEmailTemplate.TransformText(),
        //            //   ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsCC"].Split(',')
        //            //      .ToDictionary(emailAddress => emailAddress),
        //            //   ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsBCC"].Split(',')
        //            //      .ToDictionary(emailAddress => emailAddress), delayMinutes);



        //            //salesRepository.UpdateSaleIsDelivered();



        //            notification = new Notification
        //            {
        //                Type = NotificationType.SalesScreenSendClientShippingDetails,
        //                ExternalId = client.Id,
        //                Email = client.Email,
        //                ExtraData = null,
        //                Created = DateTime.Now
        //            };
        //            var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
        //            notificationRepository.Save(notification);
        //            efundstoreUnitOfWork.Commit();
        //        }
        //    }
        //}










        //private void SendFundPassPromoCode(Notification notification)
        //{

        //        using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
        //        {
        //            const string subject = "As a Thank You, We’ve Got Something for You";
        //            const string subject2 = "# FundPass Coupons Still Available";
        //            var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
        //            var fundpassRepository = efundraisingProdUnitOfWork.CreateRepository<IFundPassCouponRepositoryRepository>();
        //            var lead = leadsRepository.GetById(notification.ExternalId);
        //            var coupon = fundpassRepository.GetById(notification.ExternalId);

        //        var remaining = 0;
        //        //var fundPassRemainingEmailTemplate = new FundPassRemainingEmailTemplate(subject2, remaining);
        //        if (coupon == null)
        //        {
        //            var couponsStillRemaining = fundpassRepository.GetAllRemaining();
        //            remaining = couponsStillRemaining.Count();
        //            var fundPassRemainingEmailTemplate = new FundPassRemainingEmailTemplate(subject2, remaining);
        //            var alternateSubject = "No more FUND-PASS coupons to send out";
        //            EmailManager.Send(new KeyValuePair<string, string>("marketing@fundraising.com", "Fundraising.com"),
        //                ConfigurationManager.AppSettings["FundPassEmail"].Split(',')
        //                     .ToDictionary(emailAddress => emailAddress),
        //                 new KeyValuePair<string, string>("marketing@fundraising.com", "Fundraising.com"), alternateSubject, string.Empty,
        //                 fundPassRemainingEmailTemplate.TransformText());
        //        }
        //        else
        //        {
        //            var fundPassEmailTemplate = new FundPassEmailTemplate(subject, lead.FirstName, coupon.FundraisingCode);
        //            EmailManager.Send(new KeyValuePair<string, string>("marketing@fundraising.com", "Fundraising.com"),
        //                 new Dictionary<string, string> { { lead.Email, lead.FirstName } },
        //                 new KeyValuePair<string, string>("marketing@fundraising.com", "Fundraising.com"), subject, string.Empty, fundPassEmailTemplate.TransformText(),
        //                ConfigurationManager.AppSettings["FundPassEmail"].Split(',').ToDictionary(emailAddress => emailAddress));


        //            fundpassRepository.Update(notification.ExternalId, true);
        //            //get remainng coupons
        //            var couponsStillRemaining = fundpassRepository.GetAllRemaining();
        //            remaining = couponsStillRemaining.Count();

        //            var fundPassRemainingEmailTemplate = new FundPassRemainingEmailTemplate(subject2, remaining);
        //            EmailManager.Send(new KeyValuePair<string, string>("marketing@fundraising.com", "Fundraising.com"),
        //            ConfigurationManager.AppSettings["FundPassEmail"].Split(',')
        //                 .ToDictionary(emailAddress => emailAddress),
        //             new KeyValuePair<string, string>("marketing@fundraising.com", "Fundraising.com"), subject2, string.Empty,
        //             fundPassRemainingEmailTemplate.TransformText());



        //        }



        //        efundraisingProdUnitOfWork.Commit();



        //    }

        //    using (var eFundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
        //    {
        //        var notificationRepository = eFundStoreUnitOfWork.CreateRepository<INotificationRepository>();
        //        notification = new Notification
        //        {
        //            Type = NotificationType.SendFundPassPromoCode,
        //            ExternalId = notification.ExternalId,
        //            Email = notification.Email,
        //            ExtraData = "Fundpass promo email",
        //            Created = DateTime.Now
        //        };
        //        notificationRepository.Save(notification);
        //        eFundStoreUnitOfWork.Commit();
        //    }

        //}




        //private void SendSportsApparelGetStarted(Notification notification)
        //{

        //    var name = notification.Name;
        //    var phone = notification.Phone;
        //    var email = notification.Email;
        //    var members = notification.Members;
        //    var startdate = notification.Startdate;
        //    var group = notification.Group;
        //    var state = notification.State;
        //    var country = notification.Country;
        //    var imageFileName = notification.ImageFileName;


        //    try
        //    {

        //        using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
        //        {

        //            const string subject = "Sports Apparel Client Prospect Submitted";
        //            //var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
        //            //var lead = leadsRepository.GetById(notification.ExternalId);

        //            var easyApparelReceivedEmailTemplate = new EasyApparelEmailTemplate(subject, name, phone, email, members, startdate, group, state, country, imageFileName);
        //            EmailManager.Send(new KeyValuePair<string, string>("info@easyapparelfundraising.com", "EasyApparelFundraising.com"),
        //                  ConfigurationManager.AppSettings["EasyApparelSubmitted"].Split(',')
        //                     .ToDictionary(emailAddress => emailAddress),
        //                  new KeyValuePair<string, string>("info@easyapparelfundraising.com", "EasyApparelFundraising.com"), subject, string.Empty,
        //                  easyApparelReceivedEmailTemplate.TransformText());


        //        }
        //    }
        //    catch (Exception exception)
        //    {

        //        using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
        //        {
        //            notification = new Notification
        //            {
        //                Type = NotificationType.SportsApparelGetStarted,
        //                ExternalId = notification.ExternalId,
        //                Email = "fake@fake.com",
        //                ExtraData = exception.ToString(),
        //                Created = DateTime.Now
        //            };
        //            var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
        //            notificationRepository.Save(notification);
        //            efundstoreUnitOfWork.Commit();
        //        }



        //        //new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, exception);

        //    }
        //}




        private void SendProductReviewSubmittedNotification(Notification notification)
        {
            const string subject = "Fundraising.com Product's Review Submitted";

            using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
            {
                var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
                var product = productRepository.GetById(notification.ExternalId, true);
                var saleEmailTemplate = new ProductEmailTemplate(subject, product, false, "A User submitted a Review for this Product.");

                try
                {

                    var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("accept", "application/json");
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("api-key", "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");

                    var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = "marketing@fundraising.com", name = "Marketing" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "support@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = saleEmailTemplate.TransformText(), subject = "Fundraising.com Product's Review Submitted" };
                    request.AddBody(body);
                    var response = clientBlue.Execute(request);

                }


                catch (Exception ex)
                {
                    throw ex;
                }



                //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                //   ConfigurationManager.AppSettings["ProductReviewSubmitted"].Split(',')
                //      .ToDictionary(emailAddress => emailAddress),
                //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                //   saleEmailTemplate.TransformText());

                //notification = new Notification
                //{
                //    Type = NotificationType.ProductReviewSubmitted,
                //    ExternalId = notification.ExternalId,
                //    Email = "fake@fake.com",
                //    ExtraData = null,
                //    Created = DateTime.Now
                //};
                //var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
                //notificationRepository.Save(notification);
                //efundstoreUnitOfWork.Commit();
            }

        }
        /// <summary>
        /// Sends an INTERNAL notification when the Sale was Paid
        /// </summary>
        /// <param name="notification"></param>
        private void SendSalePaidNotification(Notification notification)
        {
            const string subject = "Fundraising.com Sales Paid";
            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
                var clientsRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
                var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
                var sales = salesRepository.GetByClientId(notification.ExternalId);
                var client = clientsRepository.GetById(notification.ExternalId);
                client.Lead = leadsRepository.GetById(client.LeadId);
                var consultant = consultantRepository.GetById(sales[0].ConsultantId);
                using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                {
                    var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
                    foreach (var sale in sales)
                    {
                        sale.Client = client;
                        sale.Consultant = consultant;
                        foreach (var saleItem in sale.Items)
                        {
                            saleItem.Product = productRepository.GetByScratchbookId(saleItem.ScratchBookId);
                            saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
                               ? saleItem.Product.Profits.First(
                                  profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
                               : saleItem.Product.Price;
                        }
                    }

                    var saleEmailTemplate = new SaleEmailTemplate(subject, sales, "The following Sales have being paid.");



                    try
                    {

                        var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = "marc.alcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "support@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = saleEmailTemplate.TransformText(), subject = "Fundraising.com Sales Paid" };
                        request.AddBody(body);
                        var response = clientBlue.Execute(request);

                    }


                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                    //   new Dictionary<string, string> { { consultant.Email, consultant.Name } },
                    //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                    //   saleEmailTemplate.TransformText(),
                    //   ConfigurationManager.AppSettings["SalePaidEmail"].Split(',')
                    //      .ToDictionary(emailAddress => emailAddress));

                    //notification = new Notification
                    //{
                    //    Type = NotificationType.SalePaid,
                    //    ExternalId = client.Id,
                    //    Email = consultant.Email,
                    //    ExtraData = null,
                    //    Created = DateTime.Now
                    //};
                    //var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
                    //notificationRepository.Save(notification);
                    //efundstoreUnitOfWork.Commit();
                }
            }
        }

        /// <summary>
        /// Sends an INTERNAL notification when a Sale was tried to be created but failed
        /// </summary>
        /// <param name="extraData">Data from the Failed Sale</param>
        /// <param name="leadId">Lead Id</param>
        private void SendSaleCreationFailedNotification(Notification notification)
        {
            const string subject = "Fundraising.com Sales Creation Failed";

            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                var lead = leadsRepository.GetById(notification.ExternalId);
                using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
                {
                    var promotionRepository = efrCommonUnitOfWork.CreateRepository<IPromotionRepository>();
                    var partnerRepository = efrCommonUnitOfWork.CreateRepository<IPartnerRepository>();
                    var promotion = promotionRepository.GetById(lead.PromotionId);
                    var partner = partnerRepository.GetById(promotion.PartnerId);
                    if (promotion.Name == null)
                    {
                        promotion.Name = "N/A";
                    }
                    lead.Partner = partner;
                    lead.Promotion = promotion;

                    if (lead.ConsultantId < 0)
                    {
                        lead.Consultant = new Consultant { Id = -1, Name = "NOT ASSIGNED" };
                    }
                    else
                    {
                        var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
                        lead.Consultant = consultantRepository.GetById(lead.ConsultantId);
                    }

                    var leadReceivedEmailTemplate = new LeadEmailTemplate(subject, lead,
                      extraMessage:
                         string.Concat(
                            "A Customer tried to place an Order at the Fundraising.com Store but failed. No charges were applied to him, but it will be a great idea to reach him and close the sale (in case he hasn't tried again later). Sale's Information: ",
                            notification.ExtraData));


                    try
                    {

                        var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = "marc.alcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "support@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = leadReceivedEmailTemplate.TransformText(), subject = "Fundraising.com Sales Creation Failed" };
                        request.AddBody(body);
                        var response = clientBlue.Execute(request);

                    }


                    catch (Exception ex)
                    {
                        throw ex;
                    }


                    //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                    //   ConfigurationManager.AppSettings["SaleFailedEmail"].Split(',')
                    //      .ToDictionary(emailAddress => emailAddress),
                    //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                    //   leadReceivedEmailTemplate.TransformText());

                    //using (var eFundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                    //{
                    //    var notificationRepository = eFundStoreUnitOfWork.CreateRepository<INotificationRepository>();
                    //    notification = new Notification
                    //    {
                    //        Type = NotificationType.SaleCreationFailed,
                    //        ExternalId = lead.Id,
                    //        Email = "fake@fake.com",
                    //        ExtraData = null,
                    //        Created = DateTime.Now
                    //    };
                    //    notificationRepository.Save(notification);
                    //    eFundStoreUnitOfWork.Commit();
                    //}
                }
            }
        }

        /// <summary>
        /// Sends an INTERNAL notification when a paypal purchase starts
        /// </summary>
        /// <param name="clientId">Client Id</param>
        private void SendPaypalPaymentStartedNotification(Notification notification)
        {
            const string subject = "Fundraising.com Paypal Sales Payment Started";

            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
                var clientsRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
                var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
                var sales = salesRepository.GetByClientId(notification.ExternalId);
                var client = clientsRepository.GetById(notification.ExternalId);
                client.Lead = leadsRepository.GetById(client.LeadId);
                var consultant = consultantRepository.GetById(sales[0].ConsultantId);
                using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                {
                    var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
                    foreach (var sale in sales)
                    {
                        sale.Client = client;
                        sale.Consultant = consultant;
                        foreach (var saleItem in sale.Items)
                        {
                            saleItem.Product = productRepository.GetByScratchbookId(saleItem.ScratchBookId);
                            saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
                               ? saleItem.Product.Profits.First(
                                  profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
                               : saleItem.Product.Price;
                        }
                    }

                    var saleEmailTemplate = new SaleEmailTemplate(subject, sales, "A Customer started his Paypal process.");


                    try
                    {

                        var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = "marc.alcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "support@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = saleEmailTemplate.TransformText(), subject = "Fundraising.com Paypal Sales Payment Started" };
                        request.AddBody(body);
                        var response = clientBlue.Execute(request);

                    }


                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                    //   ConfigurationManager.AppSettings["SaleFailedEmail"].Split(',')
                    //      .ToDictionary(emailAddress => emailAddress),
                    //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                    //   saleEmailTemplate.TransformText());

                    notification = new Notification
                    {
                        Type = NotificationType.PaypalPaymentStarted,
                        ExternalId = client.Id,
                        Email = "fake@fake.com",
                        ExtraData = null,
                        Created = DateTime.Now
                    };
                    var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
                    notificationRepository.Save(notification);
                    efundstoreUnitOfWork.Commit();
                }

            }

        }

        /// <summary>
        /// Sends an INTERNAL notification when a Sale failed during the procress
        /// </summary>
        /// <param name="clientId">Client Id</param>
        private void SendSaleProcessFailedNotification(Notification notification)
        {
            const string subject = "Fundraising.com Sales Process Failed";

            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
                var clientsRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
                var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
                var sales = salesRepository.GetByClientId(notification.ExternalId);
                var client = clientsRepository.GetById(notification.ExternalId);
                client.Lead = leadsRepository.GetById(client.LeadId);
                var consultant = consultantRepository.GetById(sales[0].ConsultantId);
                using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                {
                    var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
                    foreach (var sale in sales)
                    {
                        sale.Client = client;
                        sale.Consultant = consultant;
                        foreach (var saleItem in sale.Items)
                        {
                            saleItem.Product = productRepository.GetByScratchbookId(saleItem.ScratchBookId);
                            saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
                               ? saleItem.Product.Profits.First(
                                  profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
                               : saleItem.Product.Price;
                        }
                    }

                    var saleEmailTemplate = new SaleEmailTemplate(subject, sales,
                       "A Customer placed an Order at the Fundraising.com Store but at the middle of the process, it failed. Please review the Sale and contact him as soon as possible.");



                    try
                    {

                        var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = "marc.alcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "support@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = saleEmailTemplate.TransformText(), subject = "Fundraising.com Sales Process Failed" };
                        request.AddBody(body);
                        var response = clientBlue.Execute(request);

                    }


                    catch (Exception ex)
                    {
                        throw ex;
                    }



                    //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                    //   ConfigurationManager.AppSettings["SaleFailedEmail"].Split(',')
                    //      .ToDictionary(emailAddress => emailAddress),
                    //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                    //   saleEmailTemplate.TransformText());

                    //notification = new Notification
                    //{
                    //    Type = NotificationType.SaleProcessFailed,
                    //    ExternalId = client.Id,
                    //    Email = "fake@fake.com",
                    //    ExtraData = null,
                    //    Created = DateTime.Now
                    //};
                    //var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
                    //notificationRepository.Save(notification);
                    //efundstoreUnitOfWork.Commit();
                }
            }
        }

        /// <summary>
        /// Sends an INTERNAL notification when a Sale's credit card charge failed
        /// </summary>
        /// <param name="clientId">Client Id</param>
        private void SendCreditCardChargeFailedNotification(Notification notification)
        {
            const string subject = "Fundraising.com Sales Credit Card Charge Failed";
            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
                var clientsRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
                var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
                var sales = salesRepository.GetByClientId(notification.ExternalId);
                var client = clientsRepository.GetById(notification.ExternalId);
                client.Lead = leadsRepository.GetById(client.LeadId);
                var consultant = consultantRepository.GetById(sales[0].ConsultantId);
                using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                {
                    var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
                    foreach (var sale in sales)
                    {
                        sale.Client = client;
                        sale.Consultant = consultant;
                        foreach (var saleItem in sale.Items)
                        {
                            saleItem.Product = productRepository.GetByScratchbookId(saleItem.ScratchBookId);
                            saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
                               ? saleItem.Product.Profits.First(
                                  profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
                               : saleItem.Product.Price;
                        }
                    }

                    var saleEmailTemplate = new SaleEmailTemplate(subject, sales,
                       "A Customer placed an Order at the Fundraising.com Store using a Credit Card but the charge failed. The Customer may have continued or ran away, please follow the Sale.");


                    try
                    {

                        var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = "marc.alcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "support@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = saleEmailTemplate.TransformText(), subject = "Fundraising.com Sales Credit Card Charge Failed" };
                        request.AddBody(body);
                        var response = clientBlue.Execute(request);

                    }


                    catch (Exception ex)
                    {
                        throw ex;
                    }



                    //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                    //   ConfigurationManager.AppSettings["SaleFailedEmail"].Split(',')
                    //      .ToDictionary(emailAddress => emailAddress),
                    //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                    //   saleEmailTemplate.TransformText());

                    //notification = new Notification
                    //{
                    //    Type = NotificationType.CreditCardChargeFailed,
                    //    ExternalId = client.Id,
                    //    Email = "fake@fake.com",
                    //    ExtraData = null,
                    //    Created = DateTime.Now
                    //};
                    //var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
                    //notificationRepository.Save(notification);
                    //efundstoreUnitOfWork.Commit();
                }
            }
        }

        /// <summary>
        /// Sends an INTERNAL notification when a Sale has been created
        /// </summary>
        /// <param name="clientId">Client Id</param>
        private void SendSaleCreatedNotification(Notification notification)
        {
            const string subject = "Fundraising.com Sales Created";

            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
                var clientsRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
                var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
                var sales = salesRepository.GetByClientId(notification.ExternalId);
                var client = clientsRepository.GetById(notification.ExternalId);
                client.Lead = leadsRepository.GetById(client.LeadId);
                var consultant = consultantRepository.GetById(sales[0].ConsultantId);
                using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                {
                    var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
                    foreach (var sale in sales)
                    {
                        sale.Client = client;
                        sale.Consultant = consultant;
                        foreach (var saleItem in sale.Items)
                        {
                            saleItem.Product = productRepository.GetByScratchbookId(saleItem.ScratchBookId);
                            saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
                               ? saleItem.Product.Profits.First(
                                  profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
                               : saleItem.Product.Price;
                        }

                    }


                    var saleEmailTemplate = new SaleEmailTemplate(subject, sales);


                    try
                    {

                        var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = "marc.alcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "support@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = saleEmailTemplate.TransformText(), subject = "Fundraising.com Sales Created" };
                        request.AddBody(body);
                        var response = clientBlue.Execute(request);

                    }


                    catch (Exception ex)
                    {
                        throw ex;
                    }


                    //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                    //   ConfigurationManager.AppSettings["SaleCreatedEmails"].Split(',')
                    //      .ToDictionary(emailAddress => emailAddress),
                    //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                    //   saleEmailTemplate.TransformText(), string.IsNullOrEmpty(consultant.Email) ? null : new Dictionary<string, string>{{ consultant.Email, consultant.Name }});                     

                    //notification = new Notification
                    //{
                    //    Type = NotificationType.SaleCreated,
                    //    ExternalId = client.Id,
                    //    Email = "fake@fake.com",
                    //    ExtraData = null,
                    //    Created = DateTime.Now
                    //};
                    //var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
                    //notificationRepository.Save(notification);
                    //efundstoreUnitOfWork.Commit();
                }
            }
        }

        ///// <summary>
        ///// Sends an INTERNAL notification when a Sale has been created
        ///// </summary>
        ///// <param name="clientId">Client Id</param>
        //private void ApparelSalesScreenSaleCreatedNotification(Notification notification)
        //{
        //    using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
        //    {

        //        var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
        //        var clientsRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
        //        var extraData = notification.ExtraData.Split('|');
        //        var salesId = extraData[1].ToString();
        //        var sequenceCode = extraData[2].ToString();


        //        using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
        //        {
        //            const string subject = "Apparel Sale Created (sale placed On Hold)";

        //            var client = clientsRepository.GetByIdAndSequenceCode(notification.ExternalId, sequenceCode);
        //            var sales = salesRepository.GetById(Convert.ToInt32(salesId));
        //            var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
        //            sales.Client = client;
        //              foreach (var saleItem in sales.Items)
        //                {
        //                    saleItem.Product = productRepository.GetByScratchbookIdSalesScreen(saleItem.ScratchBookId);
        //                    saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
        //                       ? saleItem.Product.Profits.First(
        //                          profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
        //                       : saleItem.Product.Price;
        //                }

        //            var ApparelSaleEmailTemplate = new ApparelSaleEmailTemplate(subject, sales);

        //            EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
        //               ConfigurationManager.AppSettings["SaleCreatedEmails"].Split(',')
        //                  .ToDictionary(emailAddress => emailAddress),
        //               new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
        //               ApparelSaleEmailTemplate.TransformText());

        //            notification = new Notification
        //            {
        //                Type = NotificationType.SaleCreated,
        //                ExternalId = client.Id,
        //                Email = "fake@fake.com",
        //                ExtraData = null,
        //                Created = DateTime.Now
        //            };
        //            var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
        //            notificationRepository.Save(notification);
        //            efundstoreUnitOfWork.Commit();
        //        }
        //    }
        //}




        /// <summary>
        /// Sends an EXTERNAL notification to the Client, saying that his Order was confirmed.
        /// </summary>
        /// <param name="notification"></param>
        private void SendOrderConfirmationNotification(Notification notification)
        {
            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
                var clientsRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
                int delayMinutes; //Delay on minutes that this notification will take
                int.TryParse(notification.ExtraData, out delayMinutes);
                //const string subject = "Fundraising.com Order Confirmation";



                var client = clientsRepository.GetById(notification.ExternalId);
                var sales = salesRepository.GetByClientId(client.Id);

                //var saleItems = salesRepository.GetSaleItemsByScratchbookID(saleItems.sale)

                foreach (var sale in sales)
                {


                    foreach (var saleItem in sale.Items)
                    {


                        //var sItems = salesRepository.GetSaleItemsByScratchbookID(saleItem.ScratchBookId);

                        //var saleItem = salesRepository.GetSaleItemsByScratchbookID(saleItem.ScratchBookId);
                        //saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
                        //   ? saleItem.Product.Profits.First(
                        //      profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
                        //   : saleItem.Product.Price;
                    }
                }

                //var orderConfirmationEmailTemplate = new OrderConfirmationEmailTemplate(subject, sItems);
                //    EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                //       new Dictionary<string, string> { { client.Email, client.FirstName + " " + client.LastName } },
                //       new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                //       orderConfirmationEmailTemplate.TransformText(),
                //       ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsCC"].Split(',')
                //          .ToDictionary(emailAddress => emailAddress),
                //       ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsBCC"].Split(',')
                //          .ToDictionary(emailAddress => emailAddress), delayMinutes);



                //using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                //{
                //    var client = clientsRepository.GetById(notification.ExternalId);
                //    var sales = salesRepository.GetByClientId(client.Id);
                //    const string subject = "Fundraising.com Order Confirmation";

                //    var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();

                //    foreach (var sale in sales)
                //    {
                //        sale.Client = client;
                //        foreach (var saleItem in sale.Items)
                //        {


                //            saleItem.Product = salesRepository.GetByScratchbookIdSalesScreen(saleItem.ScratchBookId);
                //            //saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
                //            //   ? saleItem.Product.Profits.First(
                //            //      profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
                //            //   : saleItem.Product.Price;
                //        }
                //    }




                //    foreach (var sale in sales)
                //    {
                //        sale.Client = client;
                //        foreach (var saleItem in sale.Items)
                //        {
                //            saleItem.Product = productRepository.GetByScratchbookId(saleItem.ScratchBookId);
                //            saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
                //               ? saleItem.Product.Profits.First(
                //                  profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
                //               : saleItem.Product.Price;



                //        }
                //    }
                //    var orderConfirmationEmailTemplate = new OrderConfirmationEmailTemplate(subject, sales);
                //    EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                //       new Dictionary<string, string> { { client.Email, client.FirstName + " " + client.LastName } },
                //       new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                //       orderConfirmationEmailTemplate.TransformText(),
                //       ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsCC"].Split(',')
                //          .ToDictionary(emailAddress => emailAddress),
                //       ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsBCC"].Split(',')
                //          .ToDictionary(emailAddress => emailAddress), delayMinutes);


                using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                {
                    notification = new Notification
                    {
                        Type = NotificationType.OrderConfirmation,
                        ExternalId = client.Id,
                        Email = client.Email,
                        ExtraData = null,
                        Created = DateTime.Now
                    };
                    var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
                    notificationRepository.Save(notification);
                    efundstoreUnitOfWork.Commit();
                }
            }
        }


        ///// <summary>
        ///// Sends an Sales Screen EXTERNAL notification to the Client, saying that his Order was confirmed.
        ///// </summary>
        ///// <param name="notification"></param>
        //private void SendSalesScreenOrderConfirmation(Notification notification)
        //{
        //    using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
        //    {
        //        var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
        //        var clientsRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
        //        //int delayMinutes; //Delay on minutes that this notification will take

        //        //var extraData = notification.ExtraData.Split('|');
        //        //int.TryParse(extraData[0], out delayMinutes);
        //        //var sequenceCode = extraData[1].ToString();
        //        //var leadId = extraData[2].ToString();
        //        //var salesId = extraData[3].ToString();


        //        //using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
        //        //{

        //        //    var client = clientsRepository.GetByIdAndSequenceCode(notification.ExternalId, sequenceCode);
        //        //    var sales = salesRepository.GetById(Convert.ToInt32(salesId));

        //        //    const string subject = "Fundraising.com Payment Confirmation*";

        //        //    var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
        //        //    sales.Client = client;
        //        //        foreach (var saleItem in sales.Items)
        //        //        {
        //        //            saleItem.Product = productRepository.GetByScratchbookIdSalesScreen(saleItem.ScratchBookId);
        //        //            saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
        //        //               ? saleItem.Product.Profits.First(
        //        //                  profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
        //        //               : saleItem.Product.Price;
        //        //        }

        //        //    var orderConfirmationSalesScreenEmailTemplate = new OrderConfirmationSalesScreenEmailTemplate(subject, sales);
        //        //    EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
        //        //       new Dictionary<string, string> { { client.Email, client.FirstName + " " + client.LastName } },
        //        //       new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
        //        //       orderConfirmationSalesScreenEmailTemplate.TransformText(),
        //        //       ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsCC"].Split(',')
        //        //          .ToDictionary(emailAddress => emailAddress),
        //        //       ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsBCC"].Split(',')
        //        //          .ToDictionary(emailAddress => emailAddress), delayMinutes);

        //        //    notification = new Notification
        //        //    {
        //        //        Type = NotificationType.OrderConfirmation,
        //        //        ExternalId = client.Id,
        //        //        Email = client.Email,
        //        //        ExtraData = null,
        //        //        Created = DateTime.Now
        //        //    };
        //        //    var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
        //        //    notificationRepository.Save(notification);
        //        //    efundstoreUnitOfWork.Commit();
        //       // }
        //    }
        //}




        /// <summary>
        /// Sends an EXTERNAL notification to the Client, saying that his Order was created.
        /// </summary>
        /// <param name="clientId"></param>
        private void SendOrderCreatedNotification(Notification notification)
        {
            const string subject = "Fundraising.com Order Created";
            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
                var clientsRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();
                var client = clientsRepository.GetById(notification.ExternalId);
                var sales = salesRepository.GetByClientId(client.Id);
                using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                {
                    var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();
                    foreach (var sale in sales)
                    {
                        sale.Client = client;
                        foreach (var saleItem in sale.Items)
                        {
                            saleItem.Product = productRepository.GetByScratchbookId(saleItem.ScratchBookId);
                            saleItem.Product.CalculatedPrice = saleItem.Product.Profits.Any()
                               ? saleItem.Product.Profits.First(
                                  profit => profit.Min <= saleItem.Quantity && profit.Max >= saleItem.Quantity).Price
                               : saleItem.Product.Price;
                        }
                    }

                    var orderCreatedEmailTemplate = new OrderCreatedEmailTemplate(subject, sales);

                    try
                    {

                        var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = client.Email, name = client.FirstName + " " + client.LastName } }, bcc = new[] { new { email = "marc.alcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "support@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = orderCreatedEmailTemplate.TransformText(), subject = "Fundraising.com Order Created" };
                        request.AddBody(body);
                        var response = clientBlue.Execute(request);

                    }


                    catch (Exception ex)
                    {
                        throw ex;
                    }




                    //var orderCreatedEmailTemplate = new OrderCreatedEmailTemplate(subject, sales);
                    //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                    //   new Dictionary<string, string> { { client.Email, client.FirstName + " " + client.LastName } },
                    //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                    //   orderCreatedEmailTemplate.TransformText(),
                    //   ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsCC"].Split(',')
                    //      .ToDictionary(emailAddress => emailAddress),
                    //   ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsBCC"].Split(',')
                    //      .ToDictionary(emailAddress => emailAddress));
                    //notification = new Notification
                    //{
                    //    Type = NotificationType.OrderCreated,
                    //    ExternalId = client.Id,
                    //    Email = client.Email,
                    //    ExtraData = null,
                    //    Created = DateTime.Now
                    //};
                    //var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
                    //notificationRepository.Save(notification);
                    //efundstoreUnitOfWork.Commit();
                }
            }

        }


        ///// <summary>
        ///// Sends an EXTERNAL notification to the Client, saying that his Order was created.
        ///// </summary>
        ///// <param name="clientId"></param>
        //private void SendSaleFollowUp(Notification notification)
        //{
        //    using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
        //    {
        //        var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
        //        var clientsRepository = efundraisingProdUnitOfWork.CreateRepository<IClientsRepository>();

        //        var client = clientsRepository.GetById(notification.ExternalId);
        //        var sales = salesRepository.GetByClientId(client.Id);
        //        const string subject = "Fundraising.com  - How's your fundraiser doing?";
        //        var emailTemplate =
        //           File.ReadAllText(HttpContext.Current.Server.MapPath("~/templates/emails/SaleConfirmationFollowUp.html"));
        //        emailTemplate = emailTemplate.Replace("{{clientId}}", client.Id.ToString());
        //        emailTemplate = emailTemplate.Replace("{{leadId}}", client.LeadId.ToString());
        //        emailTemplate = emailTemplate.Replace("{{clientName}}", client.FirstName + " " + client.LastName);

        //        var metaProducts = new StringBuilder();
        //        var products = new StringBuilder();
        //        var featProducts = new StringBuilder();
        //        var incaseMoreProducts = "fundraising products";
        //        var clientSaleItem = "";
        //        var itemCount = 0;
        //        var amountOfSaleItems = 0;
        //        var saleProductClass = 0;

        //        using (var efundstoreUnitOfWork = new UnitOfWork(Database.EFundStore))
        //        {
        //            var notificationRepository = efundstoreUnitOfWork.CreateRepository<INotificationRepository>();
        //            var productRepository = efundstoreUnitOfWork.CreateRepository<IProductRepository>();




        //            foreach (var sale in sales)
        //            {
        //                foreach (var saleItem in sale.Items)
        //                {
        //                    saleItem.Product = productRepository.GetByScratchbookId(saleItem.ScratchBookId);
        //                    metaProducts.Append(
        //                       string.Format(
        //                          "<div itemprop=\"acceptedOffer\" itemscope itemtype=\"http://schema.org/Offer\"><div itemprop=\"itemOffered\" itemscope itemtype=\"http://schema.org/Product\"><meta itemprop=\"name\" content=\"{0}\" /></div><meta itemprop=\"price\" content=\"{1}\" /><meta itemprop=\"priceCurrency\" content=\"USD\" /><div itemprop=\"eligibleQuantity\" itemscope itemtype=\"http://schema.org/QuantitativeValue\"><meta itemprop=\"value\" content=\"{2}\" /></div></div>",
        //                          saleItem.Product.Name, saleItem.Product.CalculatedPrice, saleItem.Quantity));
        //                    products.Append(
        //                       string.Format(saleItem.Product.Name));

        //                    itemCount++;
        //                    amountOfSaleItems = sale.Items.Count;

        //                    if (itemCount == 1)
        //                    {
        //                        clientSaleItem = saleItem.Product.Name;

        //                        try
        //                        {
        //                            saleProductClass = saleItem.Product.CategoryId;
        //                            var featuredProducts = productRepository.GetByCategory(saleProductClass);

        //                            if (featuredProducts != null)

        //                            {
        //                                foreach (var prods in featuredProducts)
        //                                {
        //                                    featProducts.Append(
        //                                       string.Format(
        //                                          "<a href=\"{1}\" target=\"_blank\"><img src=\"{0}\" style=\"width: 130px; height: 130px; border-width: 0px; display: block; border-style:none; padding-right:15px\" width=\"130\" height=\"130\"></a>",
        //                                          "https://www.fundraising.com/Content/external/products/" + prods.Id +
        //                                          ".jpg"
        //                                          , "https://www.fundraising.com/products/" + prods.Url
        //                                          ));
        //                                }

        //                            }


        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            throw ex;
        //                        }

        //                    }



        //                }
        //            }
        //            emailTemplate = emailTemplate.Replace("{{featProducts}}", featProducts.ToString());
        //            emailTemplate = emailTemplate.Replace("{{metaProducts}}", metaProducts.ToString());
        //            emailTemplate = amountOfSaleItems > 1
        //               ? emailTemplate.Replace("{{products}}", incaseMoreProducts)
        //               : emailTemplate.Replace("{{products}}", clientSaleItem);

        //            EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
        //               new Dictionary<string, string> { { client.Email, client.FirstName + " " + client.LastName } },
        //               new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject,
        //               string.Empty,
        //               emailTemplate,
        //               ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsCC"].Split(',')
        //                  .ToDictionary(emailAddress => emailAddress),
        //               ConfigurationManager.AppSettings["OrderConfirmationCopyEmailsBCC"].Split(',')
        //                  .ToDictionary(emailAddress => emailAddress));


        //            notification = new Notification
        //            {
        //                Type = NotificationType.OrderFollowUp,
        //                ExternalId = client.Id,
        //                Email = client.Email,
        //                ExtraData = null,
        //                Created = DateTime.Now
        //            };
        //            notificationRepository.Save(notification);
        //            efundstoreUnitOfWork.Commit();


        //        }

        //    }


        //}

        /// <summary>
        /// Sends an INTERNAL notification that a Repeated Lead has been received.
        /// </summary>
        /// <param name="leadId"></param>
        private void SendLeadDuplicatedEmailNotification(Notification notification)
        {
            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                const string subject = "A duplicated lead has been added";
                var lead = leadsRepository.GetById(notification.ExternalId);
                using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
                {
                    var promotionRepository = efrCommonUnitOfWork.CreateRepository<IPromotionRepository>();
                    var partnerRepository = efrCommonUnitOfWork.CreateRepository<IPartnerRepository>();
                    var promotion = promotionRepository.GetById(lead.PromotionId);
                    var partner = partnerRepository.GetById(promotion.PartnerId);
                    if (promotion.Name == null)
                    {
                        promotion.Name = "N/A";
                    }
                    lead.Partner = partner;
                    lead.Promotion = promotion;

                    if (lead.ConsultantId < 0)
                    {
                        lead.Consultant = new Consultant { Id = -1, Name = "NOT ASSIGNED" };
                    }
                    else
                    {
                        var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
                        lead.Consultant = consultantRepository.GetById(lead.ConsultantId);
                    }
                    var leadReceivedEmailTemplate = new LeadEmailTemplate(subject, lead);



                    try
                    {

                        var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = "marc.alcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "support@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = leadReceivedEmailTemplate.TransformText(), subject = "A Duplicated Lead Form Attempt" };
                        request.AddBody(body);
                        var response = clientBlue.Execute(request);

                    }


                    catch (Exception ex)
                    {
                        throw ex;
                    }






                    //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                    //   ConfigurationManager.AppSettings["LeadReceivedReport"].Split(',')
                    //      .ToDictionary(emailAddress => emailAddress),
                    //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                    //   leadReceivedEmailTemplate.TransformText());
                    //using (var eFundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                    //{
                    //    var notificationRepository = eFundStoreUnitOfWork.CreateRepository<INotificationRepository>();
                    //    notification = new Notification
                    //    {
                    //        Type = NotificationType.DuplicatedLead,
                    //        ExternalId = lead.Id,
                    //        Email = lead.Email,
                    //        ExtraData = null,
                    //        Created = DateTime.Now
                    //    };
                    //    notificationRepository.Save(notification);
                    //    eFundStoreUnitOfWork.Commit();
                    //}
                }
            }
        }

        /// <summary>
        /// Sends an INTERNAL notification that a Potential Repeated Lead has been received.
        /// </summary>
        /// <param name="leadId"></param>
        private void SendPotentialDuplicateLeaNotification(Notification notification)
        {
            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                const string subject = "A POTENTIAL duplicated lead has been added";
                var lead = leadsRepository.GetById(notification.ExternalId);
                using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
                {
                    var promotionRepository = efrCommonUnitOfWork.CreateRepository<IPromotionRepository>();
                    var partnerRepository = efrCommonUnitOfWork.CreateRepository<IPartnerRepository>();
                    var promotion = promotionRepository.GetById(lead.PromotionId);
                    var partner = partnerRepository.GetById(promotion.PartnerId);
                    if (promotion.Name == null)
                    {
                        promotion.Name = "N/A";
                    }
                    lead.Partner = partner;
                    lead.Promotion = promotion;
                    if (lead.ConsultantId < 0)
                    {
                        lead.Consultant = new Consultant { Id = -1, Name = "NOT ASSIGNED" };
                    }
                    else
                    {
                        var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
                        lead.Consultant = consultantRepository.GetById(lead.ConsultantId);
                    }
                    var leadReceivedEmailTemplate = new LeadEmailTemplate(subject, lead);




                    try
                    {

                        var clientBlue = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = "marc.alcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = "support@fundraising.com", name = "Sales Support" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = leadReceivedEmailTemplate.TransformText(), subject = "A POTENTIAL Duplicated Lead Form Attempt" };
                        request.AddBody(body);
                        var response = clientBlue.Execute(request);

                    }


                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                    //   ConfigurationManager.AppSettings["LeadReceivedReport"].Split(',')
                    //      .ToDictionary(emailAddress => emailAddress),
                    //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                    //   leadReceivedEmailTemplate.TransformText());
                    //using (var eFundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                    //{
                    //    var notificationRepository = eFundStoreUnitOfWork.CreateRepository<INotificationRepository>();
                    //    notification = new Notification
                    //    {
                    //        Type = NotificationType.PotentialDuplicateLead,
                    //        ExternalId = lead.Id,
                    //        Email = lead.Email,
                    //        ExtraData = null,
                    //        Created = DateTime.Now
                    //    };
                    //    notificationRepository.Save(notification);
                    //    eFundStoreUnitOfWork.Commit();
                    //}
                }
            }
        }

        /// <summary>
        /// Sends an INTERNAL notification, saying that a new Lead has been received.
        /// </summary>
        /// <param name="leadId"></param>
        private void SendLeadCreatedNotification(Notification notification)
        {

            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                const string subject = "A new lead has been added";
                var lead = leadsRepository.GetById(notification.ExternalId);
                var leadsCreated = leadsRepository.GetAllByDateRange(DateTime.Today, DateTime.Today);
                try
                {
                    using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
                    {
                        var promotionRepository = efrCommonUnitOfWork.CreateRepository<IPromotionRepository>();
                        var partnerRepository = efrCommonUnitOfWork.CreateRepository<IPartnerRepository>();
                        var promotion = promotionRepository.GetById(lead.PromotionId);
                        var partner = partnerRepository.GetById(promotion.PartnerId);
                        if (promotion.Name == null)
                        {
                            promotion.Name = "N/A";
                        }
                        var notes = leadsCreated.Any()
                           ? string.Format("{1}/{0} - {2}", leadsCreated.Count,
                              leadsCreated.Count(p => p.PromotionId == lead.PromotionId),
                              (leadsCreated.Count(p => p.PromotionId == lead.PromotionId) / (double)leadsCreated.Count)
                                 .ToString(
                                    "P0"))
                           : string.Empty;
                        lead.Partner = partner;
                        lead.Promotion = promotion;

                        if (lead.ConsultantId < 0)
                        {
                            lead.Consultant = new Consultant { Id = -1, Name = "NOT ASSIGNED" };
                        }
                        else
                        {
                            var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
                            lead.Consultant = consultantRepository.GetById(lead.ConsultantId);
                        }
                        var leadReceivedEmailTemplate = new LeadEmailTemplate(subject, lead, notes);
                        var emailbody = leadReceivedEmailTemplate.TransformText();
                        var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = "marc.alcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" } }, replyTo = new { email = "online@fundraising.com", name = "Fundraising.com" }, htmlContent = emailbody.ToString(), subject = "A new lead has been added" };
                        request.AddBody(body);
                        var response = client.Execute(request);


                        //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                        //   ConfigurationManager.AppSettings["LeadReceivedReport"].Split(',')
                        //      .ToDictionary(emailAddress => emailAddress),
                        //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                        //   leadReceivedEmailTemplate.TransformText());
                        //using (var eFundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                        //{
                        //    var notificationRepository = eFundStoreUnitOfWork.CreateRepository<INotificationRepository>();
                        //    notification = new Notification
                        //    {
                        //        Type = NotificationType.LeadEntry,
                        //        ExternalId = lead.Id,
                        //        Email = lead.Email,
                        //        ExtraData = null,
                        //        Created = DateTime.Now
                        //    };
                        //    notificationRepository.Save(notification);
                        //    eFundStoreUnitOfWork.Commit();
                        //}
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Sends an EXTERNAL notification to the User who requested a Kit Request.
        /// </summary>
        /// <param name="leadId"></param>
        /// <param name="emailAddress"></param>
        private void SendKitRequestedNotification(Notification notification)
        {
            const string subject = "The Guide to Reach your Goals";

            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
                {
                    var promotionRepository = efrCommonUnitOfWork.CreateRepository<IPromotionRepository>();
                    var partnerRepository = efrCommonUnitOfWork.CreateRepository<IPartnerRepository>();
                    var lead = leadsRepository.GetById(notification.ExternalId);
                    var promotion = promotionRepository.GetById(lead.PromotionId);
                    var partner = partnerRepository.GetById(promotion.PartnerId);
                    lead.Partner = partner;
                    lead.Promotion = promotion;
                    var kitRequestedEmailTemplate = new KitRequestedEmailTemplate(subject, lead);
                    var emailbody = kitRequestedEmailTemplate.TransformText();

                    try
                    {

                        var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = lead.Email, name = lead.FirstName } }, replyTo = new { email = "online@fundraising.com", name = "fundraising.com" }, htmlContent = emailbody.ToString(), subject = "The Guide to Reach your Goals" };
                        request.AddBody(body);
                        var response = client.Execute(request);

                    }


                    catch (Exception ex)
                    {
                        throw ex;
                    }




                }
            }
        }

        /// <summary>
        /// Sends an INTERNAL notification to the Representative, saying that a User requested information to him.
        /// </summary>
        /// <param name="leadId"></param>
        /// <param name="representativeEmail"></param>
        private void SendRepresentativeInformationRequestedNotification(Notification notification)
        {
            const string subject = "You have received a request for information from your GA Rep Portal! See details below.";
            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var leadsRepository = efundraisingProdUnitOfWork.CreateRepository<ILeadRepository>();
                using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
                {
                    var promotionRepository = efrCommonUnitOfWork.CreateRepository<IPromotionRepository>();
                    var partnerRepository = efrCommonUnitOfWork.CreateRepository<IPartnerRepository>();
                    var lead = leadsRepository.GetById(notification.ExternalId);
                    var promotion = promotionRepository.GetById(lead.PromotionId);
                    var partner = partnerRepository.GetById(promotion.PartnerId);
                    lead.Partner = partner;
                    lead.Promotion = promotion;

                    if (lead.ConsultantId < 0)
                    {
                        lead.Consultant = new Consultant { Id = -1, Name = "NOT ASSIGNED" };
                    }
                    else
                    {
                        var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
                        lead.Consultant = consultantRepository.GetById(lead.ConsultantId);
                    }

                    var leadEmailTemplate = new LeadEmailTemplate(subject, lead, extraMessage: "You have received a request for information from your GA Rep Portal! See details below.");


                    try
                    {

                        var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("api-key", "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");

                        var body = new { sender = new { name = "Fundraising.com", email = "online@fundraising.com" }, to = new[] { new { email = "marc.alcindor@fundraising.com", name = "Marc" }, new { email = "jason.farrell@fundraising.com", name = "Jay" }, new { email = notification.Email, name = "FR Rep" } }, replyTo = new { email = "online@fundraising.com", name = "fundraising.com" }, htmlContent = leadEmailTemplate.TransformText(), subject = "You have received a request for information from your GA Rep Portal! See details below." };
                        request.AddBody(body);
                        var response = client.Execute(request);

                    }


                    catch (Exception ex)
                    {
                        throw ex;
                    }




                    //EmailManager.Send(new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"),
                    //   new Dictionary<string, string> { { notification.Email, notification.Email } },
                    //   new KeyValuePair<string, string>("online@fundraising.com", "Fundraising.com"), subject, string.Empty,
                    //   leadEmailTemplate.TransformText(),
                    //   ConfigurationManager.AppSettings["GALeadCopyEmailsBCC"].Split(',')
                    //      .ToDictionary(emailAddress => emailAddress));


                    //using (var eFundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
                    //{
                    //    var notificationRepository = eFundStoreUnitOfWork.CreateRepository<INotificationRepository>();
                    //    notification = new Notification
                    //    {
                    //        Type = NotificationType.InformationRequested,
                    //        ExternalId = lead.Id,
                    //        Email = notification.Email,
                    //        ExtraData = null,
                    //        Created = DateTime.Now
                    //    };
                    //    notificationRepository.Save(notification);
                    //    eFundStoreUnitOfWork.Commit();
                    //}

                }
            }
        }
    }
}
