using System;
using System.Collections.Specialized;

namespace GA.BDC.Core.Utilities.CookieHandler
{
	/*
	 * Created by:	Jean-Francois Buist.
	 * Date:		Novembre 2004. 
     * 
     * 
     * Updated by : Jiro Hidaka.
     * Date:        February 2013.
     * Description: Added support for SubKey collection
     * 
	 */

	/// <summary>
	/// Handles the cookies.
	/// </summary>
	/// <example>
	/// <code>
	/// using efundraising.Utilities.CookieHandler;
	/// 
	///	if(CookieHandler.IsCookieEnable(Request)) {
	///		if(CookieHandler.CookieExists(Request, "user_id")) {
	///			int user_id = int.Parse(CookieHandler.CookieValue(Request, "user_id"));
	///		} else {
	///			CookieHandler.SetCookie(Request, Response, "user_id", "10");
	///		}
	///	}
	/// </code>
	/// </example>
	public class CookieHandler {

		/// <summary>
		/// Cookie hander constructor
		/// </summary>
		public CookieHandler() {

		}

		/// <summary>
		/// Checks if the client browser accepts cookies.
		/// </summary>
		/// <param name="request">Request Object</param>
		/// <returns>TRUE if client accept cookies</returns>
		public static bool IsCookieEnable(System.Web.HttpRequest request) {
			return (request == null? false: request.Browser.Cookies);
		}

		/// <summary>
		/// Check if a cookie entry exists
		/// </summary>
		/// <param name="request">Request Object</param>
		/// <param name="cookieName">The key name of the cookie value</param>
		/// <returns>TRUE if the cookie key has value</returns>
		public static bool CookieExists(System.Web.HttpRequest request, string cookieName) {
			return (request.Cookies[cookieName] == null? false: true);
		}

		/// <summary>
		/// Retreive the cookie value
		/// </summary>
		/// <param name="request">Request Object</param>
		/// <param name="cookieName">Cookie key name</param>
		/// <returns>The value of the cookie</returns>
		/// <remarks>Make sure to check if the cookie key exists by using CookieExists() first</remarks>
		public static string CookieValue(System.Web.HttpRequest request, string cookieName) {
			return request.Cookies[cookieName].Value;
		}

        /// <summary>
        /// Retreive the specific subkey cookie value 
        /// </summary>
        /// <param name="request">Request Object</param>
        /// <param name="cookieName">Cookie key name</param>
        /// <param name="subKeyName">Sub key name</param>
        /// <returns>The value of the cookie</returns>
        /// <remarks>Make sure to check if the cookie key exists by using CookieExists() first</remarks>
        public static string CookieValue(System.Web.HttpRequest request, string cookieName, string subKeyName)
        {
            return request.Cookies[cookieName][subKeyName];
        }

		/// <summary>
		/// Set a cookie value.
		/// </summary>
		/// <param name="request">Request Object</param>
		/// <param name="response">Response Object</param>
		/// <param name="cookieName">Cookie key name</param>
		/// <param name="cookieValue">Cookie value</param>
		/// <remarks>Default expiration date is 1 year</remarks>
		public static void SetCookie(System.Web.HttpRequest request, System.Web.HttpResponse response, string cookieName, string cookieValue) {
			SetCookie(request, response, cookieName, cookieValue, DateTime.Now.AddYears(1));
		}

		/// <summary>
		/// Set a cookie value
		/// </summary>
		/// <param name="request">Request Object</param>
		/// <param name="response">Response Object</param>
		/// <param name="cookieName">Cookie Key Name</param>
		/// <param name="cookieValue">Cookie Value</param>
		/// <param name="cookieExpires">Cookie expires datetime</param>
		public static void SetCookie(System.Web.HttpRequest request, System.Web.HttpResponse response, string cookieName, string cookieValue, DateTime cookieExpires) {
			System.Web.HttpCookie cookie = new System.Web.HttpCookie(cookieName, cookieValue);
			cookie.Expires = 	cookieExpires;
			response.Cookies.Add(cookie);
		}

        /// <summary>
        /// Set a cookie value
        /// </summary>
        /// <param name="request">Request Object</param>
        /// <param name="response">Response Object</param>
        /// <param name="cookieName">Cookie Key Name</param>
        /// <param name="cookieValue">Cookie Value</param>
        /// <param name="cookieExpires">Cookie expires datetime</param>
        /// <param name="domain">Cookie Domain Scope</param>
        public static void SetCookie(System.Web.HttpRequest request, System.Web.HttpResponse response, string cookieName, string cookieValue, DateTime cookieExpires, string domain)
        {
            System.Web.HttpCookie cookie = new System.Web.HttpCookie(cookieName, cookieValue);
            if (cookieExpires != DateTime.MinValue)
                cookie.Expires = cookieExpires;
            if (!string.IsNullOrEmpty(domain))
                cookie.Domain = domain;
            response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Set a cookie value with subkeys
        /// </summary>
        /// <param name="request">Request Object</param>
        /// <param name="response">Response Object</param>
        /// <param name="cookieName">Cookie Key Name</param>
        /// <param name="subKeyValues">Collection of subkeys</param>
        /// <param name="cookieExpires">Cookie expires datetime</param>
        /// <param name="domain">Cookie Domain Scope</param>
        public static void SetCookie(System.Web.HttpRequest request, System.Web.HttpResponse response, string cookieName, NameValueCollection subKeyValues, DateTime cookieExpires, string domain)
        {
            string subkeyName, subkeyValue;
            System.Web.HttpCookie cookie = new System.Web.HttpCookie(cookieName);
            for (int i = 0; i < subKeyValues.Count; i++)
            {
                subkeyName = subKeyValues.AllKeys[i];
                subkeyValue = subKeyValues[i];
                cookie.Values[subkeyName] = subkeyValue;
            }
            if (cookieExpires != DateTime.MinValue)
                cookie.Expires = cookieExpires;
            if (!string.IsNullOrEmpty(domain))
                cookie.Domain = domain;
            response.Cookies.Add(cookie);
        }
	}
}
