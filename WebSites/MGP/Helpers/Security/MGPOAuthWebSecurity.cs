using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using DotNetOpenAuth.ApplicationBlock;
using DotNetOpenAuth.ApplicationBlock.Consumers;
using GA.BDC.Web.MGP.Properties;


using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;

using System.Web.UI;
using System.Web.UI.WebControls;
using Google.GData.Client;
using Google.Contacts;
using Google.GData.Contacts;
using Google.GData.Extensions;
using System.Text;





namespace GA.BDC.Web.MGP.Helpers.Security
{
   // ReSharper disable once InconsistentNaming
   public class MGPOAuthWebSecurity
   {
      private IList<string> Providers { get; set; }
      public MGPOAuthWebSecurity()
      {
         Providers = new List<string> { "google", "facebook", "twitter" };
      }

      public string GetOAuthClientData(string provider)
      {
         var providerFound = Providers.FirstOrDefault(p => p == provider.ToLowerInvariant());
         return providerFound;
      }

      public void RequestAuthentication(string provider, string returnUrl)
      {

         var providerType = Providers.FirstOrDefault(p => p == provider.ToLowerInvariant());
         switch (providerType)
         {
            case "google":
               var googleClient = new GoogleClient
               {
                  ClientIdentifier = Settings.Default.GoogleClientIdentifier,
                  ClientSecret = Settings.Default.GoogleClientSecret
               };
               var googleClientAuthorization = googleClient.ProcessUserAuthorization();
               if (googleClientAuthorization == null)
               {
						googleClient.RequestAuthorization(
							new List<GoogleClient.Applications> { GoogleClient.Applications.Email, GoogleClient.Applications.Profile }, 
							new Uri(returnUrl));
					}
               break;
            case "facebook":
               var facebookClient = new FacebookClient();
					var facebookClientAuthorization = facebookClient.ProcessUserAuthorization();
					if (facebookClientAuthorization == null)
					{
						facebookClient.RequestUserAuthorization(new List<string> { "email", "public_profile" }, new Uri(returnUrl));
					}
					break;
            case "twitter":
               var response = TwitterConsumer.StartSignInWithTwitter(false, new Uri(returnUrl));
               response.Send();
               break;
         }
      }

      public MGPOAuthWebSecurityResult VerifyAuthentication(string provider)
      {
             

            if (string.IsNullOrEmpty(provider))
         {
            return new MGPOAuthWebSecurityResult { IsSuccesful = false };
         }
        // //try
         //{
            var providerType = Providers.FirstOrDefault(p => p == provider.ToLowerInvariant());
            switch (providerType)
            {
               case "google":
                    var googleClient = new GoogleClient
                    {
                        ClientIdentifier = Settings.Default.GoogleClientIdentifier,
                        ClientSecret = Settings.Default.GoogleClientSecret

                    };

                    var googleplus_client_id = Settings.Default.GoogleClientIdentifier;    // Replace this with your Client ID
                    var googleplus_client_secret =Settings.Default.GoogleClientSecret;      // Replace this with your Client Secret
                    var googleplus_redirect_url = Settings.Default.GoogleCallBackLogin;

                    var url = HttpContext.Current.Request.Url.PathAndQuery;
                     if (url != "")
                    {

                        string queryString = url.ToString();
                        char[] delimiterChars = { '=' };
                        string[] words = queryString.Split(delimiterChars);
                        string code = words[2];

                        if (code != null)
                        {

                            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");

                            webRequest.Method = "POST";
                            var Parameters = "code=" + code + "&client_id=" + googleplus_client_id + "&client_secret=" + googleplus_client_secret + "&redirect_uri=" + googleplus_redirect_url + "&grant_type=authorization_code";

                            //Parameters = "&client_id=" + Settings.Default.GoogleClientIdentifier + "&client_secret=" + Settings.Default.GoogleClientSecret + "&redirect_uri=http://local.efundraising.com/Security/LoginExternalCallback?providerName=google";
                            byte[] byteArray = Encoding.ASCII.GetBytes(Parameters);
                            webRequest.ContentType = "application/x-www-form-urlencoded";
                            webRequest.ContentLength = byteArray.Length;
                            Stream postStream = webRequest.GetRequestStream();
                            // Add the post data to the web request
                            postStream.Write(byteArray, 0, byteArray.Length);
                            postStream.Close();

                            WebResponse response = webRequest.GetResponse();
                            postStream = response.GetResponseStream();
                            StreamReader reader = new StreamReader(postStream);
                            string responseFromServer = reader.ReadToEnd();


                            GooglePlusAccessToken serStatus = JsonConvert.DeserializeObject<GooglePlusAccessToken>(responseFromServer);
                            if (serStatus != null)
                            {
                                string accessToken = string.Empty;
                                accessToken = serStatus.access_token;

                                if (!string.IsNullOrEmpty(accessToken))
                                {
                                    var profile = googleClient.GetProfile(accessToken);
                                    var data = System.Web.Helpers.Json.Decode(profile);
                                    var result = new MGPOAuthWebSecurityResult { IsSuccesful = true, Provider = providerType, ProviderUserId = data.id };
                                    result.ExtraData.Add("LastName", data.family_name);
                                    result.ExtraData.Add("FirstName", data.given_name);
                                    result.ExtraData.Add("Gender", data.gender);
                                    result.ExtraData.Add("Email", data.email);
                                    result.UserName = data.email;
                                    return result;
                                }
                            }
                        }
                        }








      //              var googleClientAuthorization = googleClient.ProcessUserAuthorization();
                       

      //                  if (googleClientAuthorization == null)
      //            {
						//	googleClient.RequestAuthorization(new List<GoogleClient.Applications> { GoogleClient.Applications.Email, GoogleClient.Applications.Profile });
						//}
      //            else
      //            {
      //               var profile = googleClient.GetProfile(googleClientAuthorization.AccessToken);
      //               var data = System.Web.Helpers.Json.Decode(profile);
      //               var result = new MGPOAuthWebSecurityResult { IsSuccesful = true, Provider = providerType, ProviderUserId = data.id };
      //               result.ExtraData.Add("LastName", data.family_name);
      //               result.ExtraData.Add("FirstName", data.given_name);
      //               result.ExtraData.Add("Gender", data.gender);
      //               result.ExtraData.Add("Email", data.email);
      //               result.UserName = data.email;
      //               return result;
      //            }
                  break;
               case "facebook":
                  var facebookClient = new FacebookClient();
                  var facebookClientAuthorization = facebookClient.ProcessUserAuthorization();
                  if (facebookClientAuthorization == null)
                  {
                     facebookClient.RequestUserAuthorization(new List<string> { "email", " public_profile" });
                  }
                  else
                  {
							string userInfo = "fields=id,first_name,last_name,gender,email";
							var profile = facebookClient.GetCustomUserInfo(userInfo, facebookClientAuthorization.AccessToken);

							var data = System.Web.Helpers.Json.Decode(profile);

							var result = new MGPOAuthWebSecurityResult { IsSuccesful = true, Provider = providerType, ProviderUserId = data.id };
                     result.ExtraData.Add("LastName", data.last_name);
                     result.ExtraData.Add("FirstName", data.first_name);
                     result.ExtraData.Add("Gender", data.gender);
                     result.ExtraData.Add("Email", data.email);
                     result.UserName = data.email;
                     return result;
                  }
                  break;
               case "twitter":
                  var profileTwitter = TwitterConsumer.GetProfile();
                  var dataTwitter = System.Web.Helpers.Json.Decode(profileTwitter);
                  var resultTwitter = new MGPOAuthWebSecurityResult { IsSuccesful = true, Provider = providerType, ProviderUserId = dataTwitter.id.ToString() };
                  var names = ((string)Convert.ToString(dataTwitter.name)).Split(' ');
                  resultTwitter.ExtraData.Add("LastName", names.Length > 1 ? names[names.Length - 1] : string.Empty);
                  resultTwitter.ExtraData.Add("FirstName", names[0]);
                  resultTwitter.ExtraData.Add("Gender", string.Empty);
                  resultTwitter.ExtraData.Add("Email", string.Empty);
                  resultTwitter.UserName = string.Empty;
                  return resultTwitter;
            }
        // }
        // catch (Exception ex)
        // {
          //  return new MGPOAuthWebSecurityResult { IsSuccesful = false };
        // }
        return new MGPOAuthWebSecurityResult { IsSuccesful = false };
      }

        public class GooglePlusAccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string id_token { get; set; }
            public string refresh_token { get; set; }
        }
    }

   // ReSharper disable once InconsistentNaming
   public class MGPOAuthWebSecurityResult
   {
      public MGPOAuthWebSecurityResult()
      {
         ExtraData = new Dictionary<string, object>();
      }

      public bool IsSuccesful { get; set; }
      public string UserName { get; set; }
      public string Provider { get; set; }
      public string ProviderUserId { get; set; }
      public Dictionary<string, object> ExtraData { get; set; }
   }
}