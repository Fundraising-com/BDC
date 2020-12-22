using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GA.BDC.Web.Custcare.Controllers
{
    public class AccountController : ApiController
    {
        const string ___ACCOUNT_USA_COUNTRY_CODE = "US";
        const string ___ACCOUNT_CANADA_COUNTRY_CODE = "CAN";

        GA.BDC.Web.Custcare.Repository.ICampaignRepository _repo;

        public AccountController(GA.BDC.Web.Custcare.Repository.ICampaignRepository repo)
        {
            _repo = repo;
        }

        public HttpResponseMessage Get(string param)
        {
            int id;
            Models.Account _account = null;

            if (int.TryParse(param, out id))
            {
                _account = new Models.Account();

                // Load Payment Info
                var payment_info = _repo.GetPaymentInfoById(id);

                if (payment_info != null)
                {
                    _account.PaymentInfo = payment_info;
                }
                else
                {
                    var result = _repo.InitializePaymentInfoById(id);

                    if (!result)
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    else
                        _account.PaymentInfo = _repo.GetPaymentInfoById(id);
                }

                // Load Shipping Address & States
                _account.ShippingAddress = _repo.GetShippingAddressById(id);
                if (_account.ShippingAddress != null)
                {
                    _account.ShippingStates = _repo.GetStatesByCountryCode(_account.ShippingAddress.CountryCode).ToList();
                }
                else
                {
                    _account.ShippingStates = _repo.GetStatesByCountryCode(___ACCOUNT_USA_COUNTRY_CODE).ToList();
                }
                

                // Load Group & Partner Info
                var group = _repo.GetGroupById(id);

                if (group != null)
                {
                    _account.Group = group;

                    var partner_data = _repo.GetPartnerById(group.PartnerId);
                    if (partner_data != null)
                        _account.Partner = partner_data;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, _account);
        }

        public HttpResponseMessage Post(string externalGroupOnly, [FromBody]Models.Account account)
        {
            var message = string.Empty;
            var result = true;
            var newLinkToExtGrpId = account.Group.ExternalGroupId;

            if (externalGroupOnly == "1")
            {
                result = _repo.UpdateExternalGroupId(account.Group, account.LinkToEventId, out message);

                // Get the new linked external group id
                if (result)
                    newLinkToExtGrpId = _repo.GetExternalGroupId(account.Group.GroupId);
            }
            else
            {
                var _pi = _repo.GetPaymentInfoById(account.PaymentInfo.EventId.Value);

                if (_pi.PostalAddressId == null)
                {
                    var postal_address_id = _repo.InsertShipping(account.ShippingAddress);
                    account.PaymentInfo.PostalAddressId = postal_address_id;
                }
                else
                {
                    _repo.UpdateShipping(account.ShippingAddress);
                }

                _repo.UpdatePaymentInfo(account.PaymentInfo);
            }

            if (result)
            {
                if (_repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, newLinkToExtGrpId);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Error saving external group id.");
                }
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, message);
        }
    }
}
