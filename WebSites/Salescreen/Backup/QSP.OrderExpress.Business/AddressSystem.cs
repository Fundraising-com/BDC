using System;
using System.Collections.Generic;
using System.Text;
using QSPForm.Business.Properties;
using QSPForm.Business.com.ses.ws.AddressHygieneService;

namespace QSPForm.Business
{
    /// <summary>
    /// Business Object for Handling Addresses
    /// </summary>
    /// <author>Benoit Nadon</author>
    /// <creationdate>08/23/2007</creationdate>
    public class AddressSystem
    {
        private AddressHygieneContract addressHygiene = null;

        public AddressSystem() { }

        /// <summary>
        /// Address Hygiene Web Service Proxy.
        /// Note that it handles authentication.
        /// </summary>
        /// 
        private AddressHygieneContract AddressHygiene
        {
            get
            {
                LoginMessageRequest loginMessageRequest;

                if (addressHygiene == null)
                {
                    addressHygiene = new AddressHygieneContract();
                    loginMessageRequest = new LoginMessageRequest();

                    addressHygiene.CookieContainer = new System.Net.CookieContainer();

                    loginMessageRequest.UserName = Settings.Default.AddressHygieneWSUserName;
                    loginMessageRequest.Password = Settings.Default.AddressHygieneWSPassword;

                    addressHygiene.Login(loginMessageRequest);
                }

                return addressHygiene;
            }
        }

        /// <summary>
        /// Returns an hygiened address from the Address Hygiene Web Service.
        /// </summary>
        /// <param name="address">Address to hygiene</param>
        /// <param name="enableSuggestionList">Enables the suggestion list feature</param>
        /// <returns>Hygiened Address</returns>
        public OutputAddress GetHygienedAddress(Address address, bool enableSuggestionList)
        {
            if (Settings.Default.AddressHygieneEnabled) {
                AddressHygieneSingleMessageRequest addressHygieneSingleMessageRequest = new AddressHygieneSingleMessageRequest();
                OutputAddress outputAddress = null;

                addressHygieneSingleMessageRequest.Address = address;
                addressHygieneSingleMessageRequest.EnableSuggestionList = enableSuggestionList;
                outputAddress = AddressHygiene.HygieneAddress(addressHygieneSingleMessageRequest).OutputAddress;

                AddressHygiene.Logout();

                return outputAddress;
            }
            else
                return OutputAddress.CreateCleanOutputAddress(address);
        }
    }
}
