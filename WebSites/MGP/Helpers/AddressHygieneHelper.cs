using System;
using System.Configuration;
using System.Net;
using GA.BDC.Web.MGP.com.qsp.wsi;

namespace GA.BDC.Web.MGP.Helpers
{
    public class AddressHygieneHelper
    {

        private readonly AddressHygieneContract _addressHygieneContract;
        public AddressHygieneHelper()
        {
            _addressHygieneContract = new AddressHygieneContract
            {
                Url = ConfigurationManager.AppSettings["AddressHygieneURL"],
                CookieContainer = new CookieContainer()
            };
            Login();
        }

        private void Login()
        {
            var loginMessageRequest = new LoginMessageRequest {UserName = ConfigurationManager.AppSettings["AddressHygieneUser"], Password = ConfigurationManager.AppSettings["AddressHygienePassword"]};
            if (!_addressHygieneContract.Login(loginMessageRequest))
            {
                throw new Exception("Address Hygiene Login Request Failed");
            }
        }

        public Address SendAddress(Address address, bool enableSuggestions = false)
        {
            var serviceAddress = new com.qsp.wsi.Address
            {
                Address1 = address.Address1,
                Address2 = address.Address2,
                City = address.City,
                Country = address.Country,
                County = address.County,
                PostCode = address.PostCode,
                PostCode2 = address.PostCode2,
                Region = address.Region
            };
            var addressHygieneSingleMessageRequest = new AddressHygieneSingleMessageRequest { Address = serviceAddress, EnableSuggestionList = enableSuggestions };
            var response = _addressHygieneContract.HygieneAddress(addressHygieneSingleMessageRequest);
            return new Address
            {
                Address1 = response.OutputAddress.Address1,
                Address2 = response.OutputAddress.Address2,
                City = response.OutputAddress.City,
                Country = response.OutputAddress.Country,
                County = response.OutputAddress.County,
                PostCode = response.OutputAddress.PostCode,
                PostCode2 = response.OutputAddress.PostCode2,
                Region = response.OutputAddress.Region                
            };
        }
    }

    public class Address
    {
        public string City { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string PostCode2 { get; set; }
        public string Region { get; set; }
    }
}