using System;
using System.Web;

namespace efundraising.efundraisingCore {

	public class xCookieHandler {

		public xCookieHandler() {

		}

		public static bool IsCookieEnable(System.Web.HttpRequest request) {
			return (request == null? false: request.Browser.Cookies);
		}

		public static bool CookieExists(System.Web.HttpRequest request, string cookieName) {
			return (request.Cookies[cookieName] == null? false: true);
		}

		public static string CookieValue(System.Web.HttpRequest request, string cookieName) {
			return request.Cookies[cookieName].Value;
		}

		public static System.Collections.Specialized.NameValueCollection 
			CookieNameValueCollection(System.Web.HttpRequest request, string cookieName) {
			return request.Cookies[cookieName].Values;
		}

		public static void SetCookie(System.Web.HttpRequest request, System.Web.HttpResponse response, string cookieName, string cookieValue) {
			SetCookie(request, response, cookieName, cookieValue, DateTime.Now.AddYears(1));
		}

		public static void SetCookie(System.Web.HttpRequest request, System.Web.HttpResponse response, string cookieName, string cookieValue, DateTime cookieExpires) {
			System.Web.HttpCookie cookie = new System.Web.HttpCookie(cookieName, cookieValue);
			cookie.Expires = 	cookieExpires;
			response.Cookies.Add(cookie);
		}
	}
}
