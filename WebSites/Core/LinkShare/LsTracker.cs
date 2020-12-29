//
// 2005-07-15 - Stephen Lim - New class.
//


using System;
using System.Web;

namespace GA.BDC.Core.LinkShare
{
	/// <summary>
	/// LsTracker class is a container for LinkShare tracking services.
	/// </summary>
	public class LsTracker
	{
		#region Constants
		private const string MLP_KEY = "rURL";
		private const string SITE_ID_KEY = "extSID";
		private const string RETURN_DAYS_SITE_ID_KEY = "lstracker.siteid";
		private const string RETURN_DAYS_TIMESTAMP_KEY = "lstracker.time";
		private const int MAX_RETURN_DAYS = 1095;
		#endregion

		#region Fields
		private string _siteId = null;
		private string _redirectUrl = null;
		#endregion

		#region Constructors
		public LsTracker()
		{
			// Check for Return Days visits by inspecting cookie
			if (HttpContext.Current.Request.Cookies[RETURN_DAYS_SITE_ID_KEY] != null)
			{
				_siteId = HttpContext.Current.Request.Cookies[RETURN_DAYS_SITE_ID_KEY].Value;
			}

			// Check if this is a new LinkShare referral by inspecting
			// the querystring for the SiteID name.
			if (HttpContext.Current.Request.QueryString[SITE_ID_KEY] != null && 
				HttpContext.Current.Request.QueryString[SITE_ID_KEY].Length > 0)
			{
				_siteId = HttpContext.Current.Request.QueryString[SITE_ID_KEY];
			}
			
			// Get the redirect URL if any from the MLP (Multiple Landing Page)
			// parameter.
			if (HttpContext.Current.Request.QueryString[MLP_KEY] != null && 
				HttpContext.Current.Request.QueryString[MLP_KEY].Length > 0)
			{
				_redirectUrl = HttpContext.Current.Request.QueryString[MLP_KEY];
			}
		}
		#endregion

		#region Methods

		/// <summary>
		/// Set the persistent cookie for Return Days if SiteId exists.
		/// </summary>
		public void SetReturnDays()
		{
			if (_siteId != "" && _siteId != null)
			{
				// Set LinkShare Return Days cookie
				HttpCookie cookie = new HttpCookie(RETURN_DAYS_SITE_ID_KEY, _siteId);
				cookie.Expires = DateTime.Now.AddDays(MAX_RETURN_DAYS);
				HttpContext.Current.Response.Cookies.Add(cookie);

				cookie = new HttpCookie(RETURN_DAYS_TIMESTAMP_KEY, DateTime.Now.ToString("r"));
				cookie.Expires = DateTime.Now.AddDays(MAX_RETURN_DAYS);
				HttpContext.Current.Response.Cookies.Add(cookie);
			}
		}
		#endregion

		#region Properties
		public string RedirectUrl
		{
			get 
			{
				return _redirectUrl;
			}
		}

		public string SiteId
		{
			get 
			{
				return _siteId;
			}
		}
		#endregion


	}
}
