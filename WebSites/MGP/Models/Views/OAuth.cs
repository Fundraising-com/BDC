using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using System.Xml.Linq;
using GA.BDC.Web.MGP.Views.Localization;
using GA.BDC.Web.MGP.Properties;
using GA.BDC.Web.MGP.Helpers;
using GA.BDC.Web.MGP.Helpers.Extensions;
using DotNetOpenAuth.ApplicationBlock.Consumers;
using Newtonsoft.Json;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OpenId.Extensions.OAuth;
using System.ServiceModel;
using System.Web;
using System.IO;
using System.Globalization;
using System.Xml;
using System.Web.UI;
using Google.GData.Client;
using Google.Contacts;
using Google.GData.Contacts;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class OAuth
    {
        public OAuth(string provider)
        {
            Provider = provider;
        }

        public enum OAuthTokenType
        {
            RequestToken = 0,
            AccessToken = 1,
            InvalidToken = 2
        }

        [Serializable]
        protected internal class OAuthToken
        {
            public string Token;
            public string Secret;
            public OAuthTokenType Type;
            public string ExtraData;
        }

        public string Provider { get; set; }
        public string ErrorMessage { get; private set; }
        public List<EmailContact> Contacts { get; private set; }
        public bool ContactsLoaded
        {
            get { return (Contacts != null && Contacts.Any()); }
        }
        public string ContactsJsonList
        {
            get
            {
                if (!ContactsLoaded)
                {
                    return null;
                }

                var contactList = new StringBuilder();
                contactList.Append("{ \"Contacts\" : [");
                foreach (var contact in Contacts)
                {
                    contactList.Append(string.Format("{{ \"FirstName\" : \"{0}\", \"LastName\": \"{1}\", \"Nickname\": \"{2}\", \"EmailAddress\": \"{3}\" }},", contact.FirstName.CleanupContactEntry(), contact.LastName.CleanupContactEntry(), contact.Nickname.CleanupContactEntry(), contact.EmailAddress));
                }
                if (contactList.Length > 1)
                {
                    contactList.Remove(contactList.Length - 1, 1);
                }
                contactList.Append("]}");
                return contactList.ToString();
            }
        }

        public void ProcessImport()
        {
            var currentProvider = (ImportAddressBookModel.ProviderType)Enum.Parse(typeof (ImportAddressBookModel.ProviderType), Provider, true);
            
            switch (currentProvider)
            {
                case ImportAddressBookModel.ProviderType.Google:

                    var googleClient = new GoogleClient { };
                    var ClientIdentifier = Settings.Default.GoogleClientIdentifier;
                    var ClientSecret = Settings.Default.GoogleClientSecret;
                    var CallBackUrlCampaign = Settings.Default.GoogleCallBackCampaign;

                    var url = HttpContext.Current.Request.Url.PathAndQuery;

                    if (url != "" && url.Contains("code"))
                    {

                        string queryString = url.ToString();
                        char[] delimiterChars = { '=' };
                        string[] words = queryString.Split(delimiterChars);
                        string code = words[2];

                        if (code != null)
                        {

                            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
                            webRequest.Method = "POST";
                            var Parameters = "code=" + code + "&client_id=" + ClientIdentifier + "&client_secret=" + ClientSecret + "&redirect_uri=" + CallBackUrlCampaign + "&grant_type=authorization_code";
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
                                string refreshToken = string.Empty;
                                accessToken = serStatus.access_token;
                                refreshToken = serStatus.refresh_token;
                                if (!string.IsNullOrEmpty(accessToken))
                                {
                                    
                                    var xdoc = GetContacts(accessToken, refreshToken, 500, 1);

                                    if (xdoc != null)
                                    {
                                        Contacts = GetGoogleContacts(xdoc);
                                   
                                }
                                }
                            }
                        }
                    }
                    else
                    {

                        string clientId = Settings.Default.GoogleClientIdentifier;
                        string redirectUrl = Settings.Default.GoogleCallBackCampaign;
                        //HttpContext.Current.Response.Redirect("https://accounts.google.com/o/oauth2/auth?redirect_uri="+redirectUrl+"&response_type=code&client_id=" + clientId + "&scope=https://www.googleapis.com/auth/userinfo.email/&access_type=offline&include_granted_scopes=true&prompt=consent");
                        HttpContext.Current.Response.Redirect("https://accounts.google.com/o/oauth2/v2/auth?redirect_uri=" + redirectUrl + "&response_type=code&client_id=" + clientId + "&scope=https://www.googleapis.com/auth/contacts.readonly&access_type=offline&include_granted_scopes=true");

                    }


                    break; 
                case ImportAddressBookModel.ProviderType.WindowsLive:
                    break;
                case ImportAddressBookModel.ProviderType.Yahoo:
                    if (System.Web.HttpContext.Current.Session[Provider + "Consumer"] == null)
                    {
                        System.Web.HttpContext.Current.Session[Provider + "Consumer"] = new OAuthTokenManager(currentProvider);
                    }

                    var tokenManager = (OAuthTokenManager)System.Web.HttpContext.Current.Session[Provider + "Consumer"];
                    var yahoo = new WebConsumer(YahooConsumer.ServiceDescription, tokenManager);

                    //Is Yahoo calling back with authorization?
                    var accessTokenResponse = yahoo.ProcessUserAuthorization();

                    if (accessTokenResponse != null)
                    {
                        tokenManager.CurrentToken.Token = accessTokenResponse.AccessToken;
                        tokenManager.ExpireRequestTokenAndStoreNewAccessToken(null, null, accessTokenResponse.AccessToken, tokenManager.GetTokenSecret(accessTokenResponse.AccessToken));
                        tokenManager.StoreExtraData("oauth_session_handle", accessTokenResponse.ExtraData["oauth_session_handle"]);
                        tokenManager.StoreExtraData("YahooGUID", accessTokenResponse.ExtraData["xoauth_yahoo_guid"]);
                        HttpStatusCode status;
                        var xdoc = YahooConsumer.GetContacts(yahoo, 500, 0, tokenManager.CurrentToken.Token, tokenManager.GetExtraData("YahooGUID"), out status);

                        if (xdoc != null && status == HttpStatusCode.OK)
                        {
                            Contacts = GetYahooContacts(xdoc);
                        }
                        else
                        {
                            string refreshedToken;
                            string refreshedTokenSecret;

                            if (YahooConsumer.RefreshAccessToken(tokenManager.ConsumerKey, tokenManager.ConsumerSecret,
                                                                 tokenManager.GetExtraData("oauth_session_handle"), tokenManager.CurrentToken.Token,
                                                                 tokenManager.CurrentToken.Secret, out refreshedToken, out refreshedTokenSecret))
                            {
                                tokenManager.CurrentToken.Token = refreshedToken;
                                tokenManager.CurrentToken.Secret = refreshedTokenSecret;

                                xdoc = YahooConsumer.GetContacts(yahoo, 500, 0, tokenManager.CurrentToken.Token, tokenManager.GetExtraData("YahooGUID"), out status);

                                if (xdoc != null && status == HttpStatusCode.OK)
                                {
                                    Contacts = GetYahooContacts(xdoc);
                                }
                            }
                        }
                    }
                    else
                    {
                       // If we don't yet have access, immediately request it.
                try
                        {
                            YahooConsumer.RequestAuthorization(yahoo);
                        }
                        catch (DotNetOpenAuth.Messaging.ProtocolException protocolEx)
                        {
                            ErrorMessage = protocolEx.InnerException is WebException && protocolEx.InnerException.Message == Resources.OAuthModel_Unauthorized_ErrorMessage
                                               ? Resources.OAuthModel_UnauthorizedAccess_ErrorMessage
                                               : Resources.OAuthModel_ErrorOccured_ErrorMessage;
                        }
                    }
                    break;
                case ImportAddressBookModel.ProviderType.Outlook:
                case ImportAddressBookModel.ProviderType.CSV:
                    break;
            }

           

    }

        public class GooglePlusAccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string id_token { get; set; }
            public string refresh_token { get; set; }
        }


        public XDocument GetContacts(string accessToken, string refreshToken,  int maxResults, int startIndex)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            if (maxResults < 1) maxResults = 25;
            if (startIndex < 1) startIndex = 1;

            string extraData = "?start-index=" + startIndex.ToString(CultureInfo.InvariantCulture) +
                               "&max-results=" + maxResults.ToString(CultureInfo.InvariantCulture);

            //var httpRequest = (HttpWebRequest)WebRequest.Create("http://www.google.com/m8/feeds/" + extraData);
            var httpRequest = (HttpWebRequest)WebRequest.Create("https://www.google.com/m8/feeds/contacts/default/full?access_token=" + accessToken + "&refresh_token=" + "&max-results=25");
            //var httpRequest = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/auth/user.emails.read/?personFields=emailAddresses&access_token=" + accessToken);
            ClientBase.AuthorizeRequest(httpRequest, accessToken);

            // Enable gzip compression.  Google only compresses the response for recognized user agent headers. - Mike Lim
            httpRequest.AutomaticDecompression = DecompressionMethods.GZip;
            httpRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.16 (KHTML, like Gecko) Chrome/10.0.648.151 Safari/534.16";
            

            using (var response = httpRequest.GetResponse())
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    //var body = sr.ReadToEnd();
                    XDocument xdoc = XDocument.Parse(sr.ReadToEnd());
                    xdoc.Declaration = null;
                    return XDocument.Parse(xdoc.ToString());
                }
            }
        }


        private static List<EmailContact> GetGoogleContacts(XDocument xdoc)
        {
            var contactList = new List<EmailContact>();

            // loop through all contacts found for this account
            var contacts = xdoc.Root.Elements(XName.Get("entry", "http://www.w3.org/2005/Atom"));

            foreach (var contact in contacts)
            {
                var title = contact.Element(XName.Get("title", "http://www.w3.org/2005/Atom"));
                var emails = contact.Elements(XName.Get("email", "http://schemas.google.com/g/2005"));

                foreach (var eachEmail in emails)
                {
                    var email = eachEmail.Attribute("address") != null
                                    ? eachEmail.Attribute("address").Value.Trim()
                                    : null;

                    if (email.IsEmpty() || !RegexHelper.IsValidEmail(email))
                    {
                        continue;
                    }

                    var parsedFirstName = string.Empty;
                    var parsedLastName = string.Empty;

                    if (title != null)
                    {
                        var fullName = title.Value.Trim();
                        var indexOfSpace = fullName.IndexOf(' ');

                        if (indexOfSpace > -1)
                        {
                            parsedFirstName = fullName.Substring(0, indexOfSpace);
                            parsedLastName = fullName.Substring(indexOfSpace + 1);
                        }
                        else
                        {
                            parsedFirstName = fullName;
                        }
                    }
                    if (parsedFirstName == string.Empty)
                    {
                        parsedFirstName = parsedLastName = "n/a";
                    }
                    if (parsedLastName == string.Empty)
                    {
                        parsedLastName = "n/a";
                    }
                    if (contactList.Count(c => c.EmailAddress.Trim().ToLower() == email.Trim().ToLower()) == 0)
                    {
                        contactList.Add(new EmailContact
                        {
                            //FirstName = parsedFirstName.HtmlEncodeDecode(),
                            //LastName = (parsedLastName != string.Empty
                            //                ? parsedLastName.HtmlEncodeDecode()
                            //                : string.Empty),
                            EmailAddress = email.HtmlEncodeDecode()
                        });
                    }
                }
            }
            return contactList;
        }

        private static List<EmailContact> GetYahooContacts(XDocument xdoc)
        {
            var contactList = new List<EmailContact>();

            //Parse through the XML returned by the Yahoo Contacts API
            var contacts = from contact in xdoc.Root.Elements(XName.Get("contact", "http://social.yahooapis.com/v1/schema.rng")) select contact;

            foreach (var contact in contacts)
            {
                var firstName = string.Empty;
                var lastName = string.Empty;
                var nickName = string.Empty;
                var emailAddress = string.Empty;
                var fields = contact.Elements(XName.Get("fields", "http://social.yahooapis.com/v1/schema.rng"));

                foreach (var field in fields)
                {
                    switch (field.Element(XName.Get("type", "http://social.yahooapis.com/v1/schema.rng")).Value)
                    {
                        case "name":
                            var valueNode = field.Element(XName.Get("value", "http://social.yahooapis.com/v1/schema.rng"));
                            firstName = valueNode.Element(XName.Get("givenName", "http://social.yahooapis.com/v1/schema.rng")).Value; //.XmlDecode();
                            lastName = valueNode.Element(XName.Get("familyName", "http://social.yahooapis.com/v1/schema.rng")).Value; //.XmlDecode();
                            break;
                        case "nickname":
                            nickName = field.Element(XName.Get("value", "http://social.yahooapis.com/v1/schema.rng")).Value; //.XmlDecode();
                            break;
                        case "email":
                            if (emailAddress.IsEmpty())
                            {
                                emailAddress = field.Element(XName.Get("value", "http://social.yahooapis.com/v1/schema.rng")).Value; //.XmlDecode();
                            }
                            break;
                    }
                }

                if (emailAddress.IsEmpty() || !RegexHelper.IsValidEmail(emailAddress))
                {
                    continue;
                }

                emailAddress = emailAddress.Trim();
                firstName = firstName.IsNotEmpty()
                                ? firstName.Trim()
                                : "n/a";
                lastName = lastName.IsNotEmpty()
                               ? lastName.Trim()
                               : "n/a";
                nickName = nickName.IsNotEmpty()
                               ? nickName.Trim()
                               : string.Empty;

                if (contactList.Count(c => c.EmailAddress.Trim().ToLower() == emailAddress.Trim().ToLower()) == 0)
                {
                    contactList.Add(new EmailContact
                    {
                        FirstName = firstName.HtmlEncodeDecode(),
                        LastName = (lastName != string.Empty
                                        ? lastName.HtmlEncodeDecode()
                                        : string.Empty),
                        Nickname = nickName.HtmlEncodeDecode(),
                        EmailAddress = emailAddress.HtmlEncodeDecode()
                    });
                }
            }
            return contactList;
        }
    }
    [Serializable]
    internal class OAuthTokenManager : DotNetOpenAuth.OAuth.ChannelElements.IConsumerTokenManager, IOpenIdOAuthTokenManager
    {
        public OAuthTokenManager(ImportAddressBookModel.ProviderType enumProvider)
        {
            switch (enumProvider)
            {
                case ImportAddressBookModel.ProviderType.Yahoo:
                    ConsumerKey = Settings.Default.YahooConsumerKey;
                    ConsumerSecret = Settings.Default.YahooConsumerSecret;
                    break;
                case ImportAddressBookModel.ProviderType.Google:
                case ImportAddressBookModel.ProviderType.WindowsLive:
                    throw new ArgumentOutOfRangeException("This Provider isn't implemented in OAuthTokenManager constructor yet!");
                default:
                    throw new ArgumentOutOfRangeException("This Provider isn't implemented in OAuthTokenManager constructor yet!");
            }
        }

        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }
        public OAuth.OAuthToken CurrentToken { get; set; }

        #region ITokenManager Members

        public string GetTokenSecret(string token)
        {
            return CurrentToken.Secret;
        }
        public void StoreNewRequestToken(UnauthorizedTokenRequest request, ITokenSecretContainingMessage response)
        {
            CurrentToken = new OAuth.OAuthToken
            {
                Token = response.Token,
                Secret = response.TokenSecret,
                Type = OAuth.OAuthTokenType.RequestToken
            };
        }
        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
            if (CurrentToken == null) //some oauth providers don't use request tokens, insert a new token if it doesn't already exist
            {
                CurrentToken = new OAuth.OAuthToken
                {
                    Token = accessToken,
                    Secret = accessTokenSecret,
                    Type = OAuth.OAuthTokenType.AccessToken
                };
            }
            else
            {
                CurrentToken.Token = accessToken;
                CurrentToken.Secret = accessTokenSecret;

                if (CurrentToken.Type == OAuth.OAuthTokenType.RequestToken)
                {
                    CurrentToken.Type = OAuth.OAuthTokenType.AccessToken;
                }
            }
        }
        public TokenType GetTokenType(string token)
        {
            throw new NotImplementedException();
        }
        public void StoreExtraData(string key, string value)
        {
            var qs = System.Web.HttpUtility.ParseQueryString(CurrentToken.ExtraData ?? string.Empty);

            qs.Set(key, value);

            CurrentToken.ExtraData = ToQueryString(qs);
        }
        public string GetExtraData(string key)
        {
            return System.Web.HttpUtility.ParseQueryString(CurrentToken.ExtraData ?? string.Empty)[key];
        }

        #endregion ITokenManager Members

        #region IOpenIdOAuthTokenManager Members

        public void StoreOpenIdAuthorizedRequestToken(string consumerKey, AuthorizationApprovedResponse authorization)
        {
            throw new NotImplementedException();
        }

        #endregion IOpenIdOAuthTokenManager Members

        #region Private Helper Methods

        private static string ToQueryString(System.Collections.Specialized.NameValueCollection nvc)
        {
            return string.Join("&", Array.ConvertAll(nvc.AllKeys, key => string.Format("{0}={1}", System.Web.HttpUtility.UrlEncode(key), System.Web.HttpUtility.UrlEncode(nvc[key]))));
        }

        #endregion Private Helper Methods
    }
}