using System.Collections.Generic;
using PayPal.Api;

namespace GA.BDC.WebApi.EzFund.Helpers
{
   public static class PaypalConfiguration
   {
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        static PaypalConfiguration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        public static Dictionary<string, string> GetConfig()
        {
            return ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetAPIContext(string accessToken = "")
        {

            var apiContext = new APIContext(string.IsNullOrEmpty(accessToken) ? GetAccessToken() : accessToken)
            {
                Config = GetConfig()
            };

            return apiContext;
        }
    }

   public class PaypalPaymentRequest
   {
      public string PaymentId { get; set; }
      public string PayerId { get; set; }
      public int ClientId { get; set; }
   }
}