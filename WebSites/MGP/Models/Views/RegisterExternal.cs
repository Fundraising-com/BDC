using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GA.BDC.Web.MGP.Helpers.Security;

namespace GA.BDC.Web.MGP.Models.Views
{
	[Serializable]
    public class RegisterExternal : Registration
    {
        [Required]
        public string ProviderName { get; set; }
        [Required]
        public string ProviderUserId { get; set; }
        
        public RegisterExternal(){}
        
        public RegisterExternal(MGPOAuthWebSecurityResult authenticationResult)
        {
            var extraData = GetExtraData(authenticationResult);
            Email = extraData["Email"];
            FirstName = extraData["FirstName"];
            LastName = extraData["LastName"];
            ProviderName = authenticationResult.Provider;
            ProviderUserId = authenticationResult.ProviderUserId;
            GroupType = 1;
        }

        private IDictionary<string, string> GetExtraData(MGPOAuthWebSecurityResult authenticationResult)
        {
            switch (authenticationResult.Provider.ToLower())
            {
                case "google":
                    return GetExtraDataFromGoogle(authenticationResult);
                case "facebook":
                    return GetExtraDataFromFacebook(authenticationResult);
                case "twitter":
                    return GetExtraDataFromTwitter(authenticationResult);
                default:
                    throw new Exception("Unknown OAuth Provider");
            }
        }

        private IDictionary<string, string> GetExtraDataFromGoogle(MGPOAuthWebSecurityResult authenticationResult)
        {
            var result = new Dictionary<string, string>(3)
            {
                {"Email", authenticationResult.UserName},
                {"FirstName", authenticationResult.ExtraData["FirstName"].ToString()},
                {"LastName", authenticationResult.ExtraData["LastName"].ToString()}
            };
            return result;
        }
        private IDictionary<string, string> GetExtraDataFromFacebook(MGPOAuthWebSecurityResult authenticationResult)
        {
            var result = new Dictionary<string, string>(3)
            {
                {"Email", authenticationResult.UserName},
                {"FirstName", authenticationResult.ExtraData["FirstName"].ToString()},
                {"LastName", authenticationResult.ExtraData["LastName"].ToString()}
            };
            return result;
        }
        private IDictionary<string, string> GetExtraDataFromTwitter(MGPOAuthWebSecurityResult authenticationResult)
        {
            var result = new Dictionary<string, string>(3)
            {
                {"Email", string.Empty},
                {"FirstName", authenticationResult.ExtraData["FirstName"].ToString()},
                {"LastName", authenticationResult.ExtraData["LastName"].ToString()}
            };
            return result;
        }
    }
}