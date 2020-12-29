using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace GA.BDC.Shared.Helpers
{
    public class TokenHelper
    {
        private string ClientId;
        private string Host;
        private string TokenRefreshUrl;
        const string ACCESS_TOKEN_KEY = "access_token";
        const string REFRESH_TOKEN_KEY = "refresh_token";
        const string EXPIRES_IN_KEY = "expires_in";

        public TokenHelper(string host, string clientId, string tokenRefreshUrl)
        {
            ClientId = clientId;
            Host = host;
            TokenRefreshUrl = tokenRefreshUrl;
        }
        /// <summary>
        /// Creates the Token Refresh Request and parse the result
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public IDictionary<string, string> RefreshAccessToken(string refreshToken)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    System.Net.ServicePointManager.Expect100Continue = false;
                    client.BaseAddress = new Uri(Host);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                    var request = new HttpRequestMessage(HttpMethod.Post, TokenRefreshUrl);

	                var keyValues = new List<KeyValuePair<string, string>>
	                {
		                new KeyValuePair<string, string>("refresh_token", refreshToken),
		                new KeyValuePair<string, string>("grant_type", "refresh_token"),
		                new KeyValuePair<string, string>("client_id", ClientId)
	                };
	                request.Content = new FormUrlEncodedContent(keyValues);

                    var response = client.SendAsync(request, HttpCompletionOption.ResponseContentRead).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);
	                    var result = new Dictionary<string, string>
	                    {
		                    {ACCESS_TOKEN_KEY, jsonObject[ACCESS_TOKEN_KEY].ToString()},
		                    {REFRESH_TOKEN_KEY, jsonObject[REFRESH_TOKEN_KEY].ToString()},
		                    {EXPIRES_IN_KEY, jsonObject[EXPIRES_IN_KEY].ToString()}
	                    };
	                    return result;
                    }
	                return null;
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Parses the string value into the Access and Refresh token
        /// </summary>
        /// <param name="unparsedToken"></param>
        /// <returns></returns>
        public IDictionary<string, string> ParseToken(string unparsedToken)
        {
            var cookieValues = HttpUtility.UrlDecode(unparsedToken).Split('&');
            var accessToken = cookieValues[0].Split('=')[1];
            var refreshToken = cookieValues[1].Split('=')[1];
	        var result = new Dictionary<string, string>
	        {
		        {ACCESS_TOKEN_KEY, accessToken},
		        {REFRESH_TOKEN_KEY, refreshToken}
	        };
	        return result;
        }
    }
}
