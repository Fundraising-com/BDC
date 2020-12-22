using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Transactions;
using System.Configuration;
using GA.BDC.Data.MGP.EFREcommerce.Models;
using esubsProvider = GA.BDC.Data.MGP.esubs_global_v2.Models;
using GA.BDC.Web.MGP.Helpers;
using GA.BDC.Web.MGP.Helpers.Extensions;
using GA.BDC.Web.MGP.Helpers.Routes.Attributes;
using GA.BDC.Web.MGP.Helpers.Routes.Filters;
using GA.BDC.Web.MGP.Helpers.EmailTemplate;
using GA.BDC.Web.MGP.Models.Branding;
using GA.BDC.Web.MGP.Models.Views;

namespace GA.BDC.Web.MGP.Controllers
{
   [RequireHttps, RenderPartnerBranding]
   public class DonationController : BaseController
   {
      [RenderPublicEventBranding]
      public ActionResult CheckOut(int eventId, int participantId)
      {
         var eventBranding = ViewBag.PublicEvent as Models.Branding.PublicEvent;
         var partner = ViewBag.Partner as Partner;
         var partnerId = partner.Id;
         var model = new Donation
         {
            EventId = eventId,
            ParticipantId = participantId,
            CardExpiryMonth = 1,
            CardExpiryYear = System.DateTime.Now.Year,
            CardType = 5,
            RecurringType = 1,
            RecurringState = 2,
            State = "US-AA",
            Country = "US",
            Detail1 = partner.Name
         };
         using (var dataProvider = new esubsProvider.DataProvider())
         {
            var landingPage = (from l in dataProvider.landingPages
                               where l.partner_id == partnerId
                               select l).FirstOrDefault();

            if (landingPage == null)
               return RedirectToAction("Index", "Home");

            var groupId = GetGroupIdByEventId(eventId);
            if (groupId == landingPage.group_id)
               model.MemberNameToSupport = partner.Name;
            else
            {
               var user = GetUserByEventParticipationId(participantId);
               if (user.IsParticipant)
                  model.MemberNameToSupport = string.Concat(user.FirstName, " ", user.LastName).Trim();
               else if (user.IsSponsor)
                  model.MemberNameToSupport = eventBranding.Name;
               else
               {
                  user = GetParentUserByEventParticipationId(participantId);
                  if (user.IsParticipant)
                     model.MemberNameToSupport = string.Concat(user.FirstName, " ", user.LastName).Trim();
                  else if (user.IsSponsor)
                     model.MemberNameToSupport = eventBranding.Name;
               }
            }

            var paymentInfo = (from g in dataProvider.groups
                               from eg in dataProvider.event_group
                               from pi in dataProvider.payment_info
                               where g.group_id == landingPage.group_id
                                  && g.group_id == eg.group_id
                                  && eg.group_id == pi.group_id
                                  && eg.event_id == pi.event_id
                                  && pi.active
                               select pi).FirstOrDefault();
            if (paymentInfo != null)
            {
               var postalAddress = (from pa in dataProvider.postal_address
                                    where pa.postal_address_id == paymentInfo.postal_address_id
                                    select pa).SingleOrDefault();
               if (postalAddress != null)
               {
                  var city = postalAddress.city;
                  var state = postalAddress.subdivision_code.IsNotEmpty()
                              ? postalAddress.subdivision_code.Replace("US-", "").Replace("CA-", "")
                              : string.Empty;
                  model.Detail2 = string.Concat(city, " ", state).Trim();
               }
            }
         }
         return View(model);
      }

      public ActionResult Confirmation(string orId)
      {
         var partner = ViewBag.Partner as Partner;
         using (var dataProvider = new DataProvider())
         {
            int orderID = EnvironmentFilter.DecryptInteger(orId);
            if (orderID < 0)
               return RedirectToAction("Index", "Home");
            var @result = dataProvider.orders.Where(o => o.order_id == orderID).Select(o => o).SingleOrDefault();
            if (@result == null)
               return RedirectToAction("Index", "Home");

            var model = new Donation
            {
               ConfirmationNumber = orderID.ToString(),
               CardName = @result.credit_card.credit_card_name,
               CardTypeName = @result.credit_card.credit_card_type.credit_card_type_name,
               LastCreditCardDigits = @result.credit_card.last_cc_digits,
               Address1 = @result.postal_address.address1,
               Address2 = @result.postal_address.address2,
               City = @result.postal_address.city,
               State = @result.postal_address.subdivision_code,
               ZipCode = @result.postal_address.zip,
               Country = @result.postal_address.subdivision.country_code,
               Detail1 = partner.Name,
               DonationAmount = @result.order_detail.Sum(p => p.price)
            };
            return View(model);
         }
      }

      [HttpPost]
      public JsonResult Donate(Donation model)
      {
         var partnerBranding = ViewBag.Partner as Models.Branding.Partner;
         var user = ViewBag.User as Models.Branding.User;
         var eventBranding = ViewBag.Event as Models.Branding.Event;
         if (!ModelState.IsValid)
         {
            var errorMessage = ModelState.First(p => p.Value.Errors.Count > 0).Value.Errors[0].ErrorMessage;
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = errorMessage } };
         }
         if (model.CardExpiryYear == DateTime.Now.Year && model.CardExpiryMonth < DateTime.Now.Month)
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = "Invalid credit card expiration date." } };

         bool CCT = false;
         var orderId = 0;
         var leadId = GetLeadIdByEventParticipationId(model.ParticipantId);

         using (var dataProvider = new DataProvider())
         {
            using (var transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 0, 10, 0)))
            {
               // Insert User Comment
               var oc = new order_comment
               {
                  first_name = model.FirstName,
                  last_name = model.LastName,
                  comment = model.Comment,
                  visible = !model.HideName
               };
               dataProvider.order_comment.Add(oc);

               // Insert User Email
               var em = new email
               {
                  email_address = model.Email,
                  recipient_name = string.Concat(model.FirstName, " ", model.LastName),
                  create_date = DateTime.Now
               };
               dataProvider.emails.Add(em);

               // Insert User Postal Address
               var pa = new postal_address
               {
                  first_name = model.FirstName,
                  last_name = model.LastName,
                  address1 = model.Address1,
                  address2 = model.Address2,
                  city = model.City,
                  zip = model.ZipCode,
                  subdivision_code = model.State,
                  create_date = DateTime.Now
               };
               dataProvider.postal_address.Add(pa);
               dataProvider.SaveChanges();

               // Insert Credit Card
               var cc = new credit_card
               {
                  credit_card_number = null, // DO NOT SAVE THE NUMBER PLEASE
                  credit_card_name = model.CardName,
                  credit_card_type_id = model.CardType,
                  credit_card_expiration_month = model.CardExpiryMonth,
                  credit_card_expiration_year = model.CardExpiryYear,
                  create_date = DateTime.Now,
                  postal_address_id = pa.postal_address_id,
                  last_cc_digits = model.CardNumber.Trim().Length > 4 ? model.CardNumber.Substring(model.CardNumber.Trim().Length - 4, 4) : string.Empty,
                  GUID = Session.SessionID
               };
               dataProvider.credit_card.Add(cc);
               dataProvider.SaveChanges();

               // Insert Order
               var or = new order
               {
                  source_id = (from c in dataProvider.sources where c.source_name == "EFRO Donation" select c).Select(c => c.source_id).FirstOrDefault(),
                  billing_postal_address_id = pa.postal_address_id,
                  billing_email_id = em.email_id,
                  ext_participation_id = model.ParticipantId,
                  ext_lead_id = leadId,
                  order_date = DateTime.Now,
                  create_date = DateTime.Now,
                  order_comment_id = oc.order_comment_id,
                  GUID = Session.SessionID,
                  status_id = (from c in dataProvider.status where c.status_name == "New" select c).Select(c => c.status_id).FirstOrDefault(),
                  ext_order_tracking = string.Empty,
                  credit_card_id = cc.credit_card_id
               };
               dataProvider.orders.Add(or);
               dataProvider.SaveChanges();

               orderId = or.order_id;

               // Insert Order Detail
               var od = new order_detail
               {
                  price = Convert.ToDecimal(model.DonationAmount),
                  quantity = 1,
                  order_id = or.order_id,
                  product_id = (from c in dataProvider.products where c.product_name.ToLower() == "Donation".ToLower() select c).Select(c => c.product_id).FirstOrDefault(),
                  status_id = (from c in dataProvider.status where c.status_name == "New" select c).Select(c => c.status_id).FirstOrDefault(),
                  create_date = DateTime.Now
               };
               dataProvider.order_detail.Add(od);
               dataProvider.SaveChanges();

               String sResponse = string.Empty;
               int orderStatusID = (from c in dataProvider.status where c.status_name == "In Process" select c).Select(c => c.status_id).FirstOrDefault();

               if (model.CardNumber == ConfigurationManager.AppSettings["CCTest"])
                  CCT = true;

               var ccType = (from c in dataProvider.credit_card_type where c.credit_card_type_id == cc.credit_card_type_id select c.credit_card_type_name).SingleOrDefault().ToUpper();
               var country = pa.subdivision_code.ToUpper().Substring(0, 2);

               sResponse = chargeCreditCard(cc, pa, ccType, country, model.DonationAmount, model.CardNumber);
               if (sResponse.IsNotEmpty())
               {
                  string[] aResponse = sResponse.Split('|');
                  string sResponseCode = aResponse[0].ToString();
                  string sResponseAuthCode = aResponse[1].ToString();
                  string sResponseOrderId = aResponse[2].ToString();
                  string sResponsePaymentId = aResponse[3].ToString();
                  string sResponseMessage = aResponse[4].ToString();

                  string authority = Request.Url.Authority.ToLower();
                  bool testEnvironment = authority.Contains("localhost") || authority.Contains("test.");

                  if (sResponseCode == "100" || testEnvironment)
                  {
                     #region if (not credit or valid credit)
                     bool bDummyCC = false;

                     if (CCT)
                     {
                        orderStatusID = (from c in dataProvider.status where c.status_name == "Test" select c).Select(c => c.status_id).FirstOrDefault(); //Ensure order is set with Test Status 
                        bDummyCC = true;
                     }

                     if (!bDummyCC || testEnvironment) //We Need to add the CC Transaction
                     {
                        int lResponseOrderId = sResponseOrderId != string.Empty ? Convert.ToInt32(sResponseOrderId) : 0;
                        int lResponsePaymentId = sResponsePaymentId != string.Empty ? Convert.ToInt32(sResponsePaymentId) : 0;

                        var cca = new credit_card_authorization
                        {
                           credit_card_id = cc.credit_card_id,
                           transaction_type = "authorize",
                           amount = model.DonationAmount,
                           response_code = sResponseCode,
                           response_auth_code = sResponseAuthCode,
                           response_order_id = lResponseOrderId,
                           response_payment_id = lResponsePaymentId,
                           response_message = sResponseMessage,
                           deleted = false,
                           create_date = DateTime.Now
                        };
                        dataProvider.credit_card_authorization.Add(cca);

                        orderStatusID = (from c in dataProvider.status where c.status_name == "Paid" select c).Select(c => c.status_id).FirstOrDefault();
                     }

                      // Perform Post Update Order
                      (from o in dataProvider.orders where o.order_id == or.order_id select o).ToList<order>().ForEach(o => o.status_id = orderStatusID);
                     (from ord in dataProvider.order_detail where ord.order_id == or.order_id select ord).ToList().ForEach(ord => ord.status_id = orderStatusID);
                     dataProvider.SaveChanges();

                     #endregion If esubs or mvp insert efund tx
                  }
                  else
                  {
                     #region If credit card order with response code not 100
                     /*
                     // LOG THE RESPONSE CODES
                     Int32 lResponseOrderId2;
                     Int32 lResponsePaymentId2;

                     lResponseOrderId2 = (sResponseOrderId != string.Empty) ? Convert.ToInt32(sResponseOrderId) : 0;
                     lResponsePaymentId2 = (sResponsePaymentId != string.Empty) ? Convert.ToInt32(sResponsePaymentId) : 0;

                     //Check for Test or Dummy CC.  If we have one.  No need to insert CC record.\
                     orderStatusID = (from c in dataProvider.status where c.status_name == "Cancelled" select c).Select(c => c.status_id).FirstOrDefault(); //Default to this, will be overwritten in sproc 

                     // Perform Post Update Order
                     (from o in dataProvider.orders where o.order_id == or.order_id select o).ToList<order>().ForEach(o => o.status_id = orderStatusID);
                     (from ord in dataProvider.order_detail where ord.order_id == or.order_id select ord).ToList().ForEach(ord => ord.status_id = orderStatusID);
                     dataProvider.SaveChanges();
                      * */
                     transactionScope.Dispose();
                     return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = "Not a valid credit card. Please try again." } };
                     #endregion If credit card order with response code not 100
                  }
               }
               else
               {
                  transactionScope.Dispose();
                  return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { success = false, responseText = "Error processing credit card. Please try again." } };
               }
               transactionScope.Complete();
            }
         }

         TouchHelper.InsertEmail(496, 213, orderId, "en-US");

         string orId = EnvironmentFilter.Encrypt(orderId.ToString());
         var url = new UrlHelper(Request.RequestContext).Action("Confirmation", "Donation", new { orId });
         return new JsonResult { Data = new { success = true, url }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
      }

      private string chargeCreditCard(credit_card cc, postal_address ccPostalAddress, string ccType, string country, decimal chargeAmount, string ccNumTemp)
      {
         string sResponse = null;
         string clientIDTransaction = null;
         string firstName = null, lastName = null;

         QPayWS.CardType oCard;
         switch (ccType)
         {
            case "AMERICAN EXPRESS":
               oCard = QPayWS.CardType.AMEX;
               break;
            case "DISCOVER":
               oCard = QPayWS.CardType.DISCOVER;
               break;
            case "MASTERCARD":
               oCard = QPayWS.CardType.MASTERCARD;
               break;
            case "VISA":
               oCard = QPayWS.CardType.VISA;
               break;
            default:
               oCard = QPayWS.CardType.VISA;
               break;
         }
         QPayWS.CountryCode oCountry;
         switch (country)
         {
            case "CA":
               oCountry = QPayWS.CountryCode.CA;
               break;
            case "US":
               oCountry = QPayWS.CountryCode.US;
               break;
            default:
               oCountry = QPayWS.CountryCode.US;
               break;
         }

         clientIDTransaction = DateTime.Now.Ticks.ToString();

         if (cc.credit_card_name.LastIndexOf(" ") != -1)
         {
            firstName = cc.credit_card_name.Substring(0, cc.credit_card_name.LastIndexOf(" ")).Trim();
            lastName = cc.credit_card_name.Substring(cc.credit_card_name.LastIndexOf(" ")).Trim();
         }
         else
         {
            lastName = cc.credit_card_name;
         }

         if (ccPostalAddress != null)
         {
            QPayWS.BatchPaymentSystemWebservice psD = new QPayWS.BatchPaymentSystemWebservice();
            string sCustomerFirstName = firstName;
            string sCustomerLastName = lastName;
            string sCustomerStreet1 = ccPostalAddress.address1;
            string sCustomerStreet2 = ccPostalAddress.address2;
            string sCustomerCity = ccPostalAddress.city;

            if (sCustomerFirstName.IsNotEmpty() && sCustomerFirstName.Length > 15) { sCustomerFirstName = sCustomerFirstName.Substring(0, 15); };
            if (sCustomerLastName.Length > 15) { sCustomerLastName = sCustomerLastName.Substring(0, 15); };
            if (sCustomerStreet1.Length > 15) { sCustomerStreet1 = sCustomerStreet1.Substring(0, 15); };
            if (sCustomerStreet2.IsNotEmpty() && sCustomerStreet2.Length > 15) { sCustomerStreet2 = sCustomerStreet2.Substring(0, 15); };
            if (sCustomerCity.Length > 15) { sCustomerCity = sCustomerCity.Substring(0, 15); };

            QPayWS.BPPSTxResponse bPPSTxResponse =
                psD.AuthRealTime(Convert.ToInt32(ConfigurationManager.AppSettings["QPayApplicationId"]), sCustomerFirstName, sCustomerLastName,
                                 sCustomerStreet1, sCustomerStreet2, sCustomerCity, ccPostalAddress.subdivision_code.Substring(ccPostalAddress.subdivision_code.Length - 2, 2),
                                 ccPostalAddress.zip, oCountry, oCard, ccNumTemp, (int)cc.credit_card_expiration_month, (int)cc.credit_card_expiration_year,
                                 Convert.ToInt32(chargeAmount * 100), String.Empty, String.Empty, clientIDTransaction, ConfigurationManager.AppSettings["QPayGUID"]);

            sResponse = bPPSTxResponse.responseCode + "|" + bPPSTxResponse.authNumber + "|" + bPPSTxResponse.BPPS_Tx_Id + "|" + bPPSTxResponse.BPPS_Tx_Id + "|" + bPPSTxResponse.ErrorMessage;
         }
         else
         {
            throw new Exception("EFR Checkout: Failed to retrive the address for CC");
         }

         return sResponse;
      }

      private static int? GetGroupIdByEventId(int eventId)
      {
         using (var dataProvider = new esubsProvider.DataProvider())
         {
            return (from eg in dataProvider.event_group
                    where eg.event_id == eventId
                    select eg.group_id).SingleOrDefault();
         }
      }
      private static int? GetLeadIdByEventParticipationId(int eventParticipationId)
      {
         using (var dataProvider = new esubsProvider.DataProvider())
         {
            return (from ep in dataProvider.event_participation
                    from eg in dataProvider.event_group
                    from g in dataProvider.groups
                    where ep.event_participation_id == eventParticipationId
                        && ep.event_id == eg.event_id
                        && eg.group_id == g.group_id
                    select g.lead_id).SingleOrDefault();
         }
      }
      private static User GetUserByEventParticipationId(int eventParticipationId)
      {
         using (var dataProvider = new esubsProvider.DataProvider())
         {
            var user = (from ep in dataProvider.event_participation
                        from mh in dataProvider.member_hierarchy
                        from cc in dataProvider.creation_channel
                        from m in dataProvider.members
                        where ep.event_participation_id == eventParticipationId
                          && ep.member_hierarchy_id == mh.member_hierarchy_id
                          && mh.creation_channel_id == cc.creation_channel_id
                          && mh.member_id == m.member_id
                        select new User
                        {
                           Id = m.member_id,
                           MemberTypeId = cc.member_type_id,
                           FirstName = m.first_name,
                           LastName = m.last_name,
                           Email = m.email_address,
                           Password = m.password
                        }).SingleOrDefault();
            if (user != null)
            {
               var @results = (from u in dataProvider.users
                               from m in dataProvider.members
                               where u.user_id == m.user_id
                                  && m.member_id == user.Id
                               select u).FirstOrDefault();
               if (@results != null)
               {
                  user.Id = @results.user_id;
                  user.FirstName = @results.first_name;
                  user.LastName = @results.last_name;
                  user.Email = @results.email_address;
                  user.Password = @results.password;
               }
            }
            return user;
         }
      }
      private static User GetParentUserByEventParticipationId(int eventParticipationId)
      {
         using (var dataProvider = new esubsProvider.DataProvider())
         {
            var parentUser = (from ep in dataProvider.event_participation
                              from mh in dataProvider.member_hierarchy
                              from mhp in dataProvider.member_hierarchy
                              from cc in dataProvider.creation_channel
                              from mp in dataProvider.members
                              where ep.event_participation_id == eventParticipationId
                                 && ep.member_hierarchy_id == mh.member_hierarchy_id
                                 && mh.parent_member_hierarchy_id == mhp.member_hierarchy_id
                                 && mhp.creation_channel_id == cc.creation_channel_id
                                 && mhp.member_id == mp.member_id
                              select new User
                              {
                                 Id = mp.member_id,
                                 MemberTypeId = cc.member_type_id,
                                 FirstName = mp.first_name,
                                 LastName = mp.last_name,
                                 Email = mp.email_address,
                                 Password = mp.password
                              }).SingleOrDefault();
            if (parentUser != null)
            {
               var @results = (from u in dataProvider.users
                               from m in dataProvider.members
                               where u.user_id == m.user_id
                                  && m.member_id == parentUser.Id
                               select u).SingleOrDefault();
               if (@results != null)
               {
                  parentUser.Id = @results.user_id;
                  parentUser.FirstName = @results.first_name;
                  parentUser.LastName = @results.last_name;
                  parentUser.Email = @results.email_address;
                  parentUser.Password = @results.password;
               }
            }
            return parentUser;
         }
      }
   }
}