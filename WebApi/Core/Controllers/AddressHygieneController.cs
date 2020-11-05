using System.Configuration;
using System.Globalization;
using System.Net;
using System.Web.Http;
using GA.BDC.Shared.Entities;
using GA.BDC.WebApi.Fundraising.Core.com.qsp.wsi;
using Address = GA.BDC.Shared.Entities.Address;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
    public class AddressHygieneController : ApiController
    {
        private readonly AddressHygieneContract _addressHygieneContract;

        public AddressHygieneController()
        {
            _addressHygieneContract = new AddressHygieneContract
            {
                Url = ConfigurationManager.AppSettings["AddressHygieneURL"],
                CookieContainer = new CookieContainer()
            };
            var loginMessageRequest = new LoginMessageRequest { UserName = ConfigurationManager.AppSettings["AddressHygieneUser"], Password = ConfigurationManager.AppSettings["AddressHygienePassword"] };
            _addressHygieneContract.Login(loginMessageRequest);
        }

      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpPost]
        public IHttpActionResult Post(Address model)
        {            
           
            var serviceAddress = new com.qsp.wsi.Address
            {
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                Country = model.Country.Code,
                County = model.County,
                PostCode = model.PostCode,
                PostCode2 = model.PostCode2,
                Region = model.Region.Code
            };
            var addressHygieneSingleMessageRequest = new AddressHygieneSingleMessageRequest { Address = serviceAddress, EnableSuggestionList = true };
            var response = _addressHygieneContract.HygieneAddress(addressHygieneSingleMessageRequest);
            if (response.OutputAddress.Fault != Fault.NoError)
            {
                return Ok(model);
            }
            var addressProposed = response.OutputAddress;
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            var result = new Address
            {
                Address1 = string.IsNullOrEmpty(addressProposed.Address1) ? model.Address1 : textInfo.ToTitleCase(addressProposed.Address1.ToLowerInvariant()),
                Address2 = string.IsNullOrEmpty(addressProposed.Address2) ? model.Address2 : textInfo.ToTitleCase(addressProposed.Address2.ToLowerInvariant()),
                City = string.IsNullOrEmpty(addressProposed.City) ? model.City : textInfo.ToTitleCase(addressProposed.City.ToLowerInvariant()),
                Country = string.IsNullOrEmpty(addressProposed.Country) ? model.Country : new Country { Name = textInfo.ToUpper(model.Country.Name), Code = textInfo.ToUpper(addressProposed.Country) },
                County = string.IsNullOrEmpty(addressProposed.County) ? model.County : textInfo.ToTitleCase(addressProposed.County.ToLowerInvariant()),
                PostCode = string.IsNullOrEmpty(addressProposed.PostCode) ? model.PostCode : textInfo.ToUpper(addressProposed.PostCode),
                PostCode2 = string.IsNullOrEmpty(addressProposed.PostCode2) ? model.PostCode2 : textInfo.ToUpper(addressProposed.PostCode2),
                Region = string.IsNullOrEmpty(addressProposed.Region) ? model.Region : new Region { Code = textInfo.ToUpper(addressProposed.Region) }
            };
            //Country update
            result.Country.Code = result.Country.Code.ToUpper() == "CANADA" ? "CA" : "US";
            return Ok(result);
        }
    }
}
