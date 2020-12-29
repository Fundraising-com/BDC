/* Title:	Culture
 * Author:	Jean-Francois Buist
 * Summary:	The code uses normal C# culture code like 'en-US' but we also use ISO in 
 *			the database.  This class has methods to work with both.
 * 
 * Create Date:	August 1, 2005
 * 
 */

#define _CANADA
using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using GA.BDC.Core.ESubsGlobal.DataAccess;
using System.Web;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// Summary description for Culture.
	/// </summary>
	[Serializable]
	public class Culture : EnvironmentBase {

		#region Fields
		private string _languageCode = null;
		private string _countryCode = null;
		private string _cultureCode = null;
        private Partner defaultPartner = null;
		#endregion

		#region Constructors

		public Culture() 
		{
            // if called from web application where HTTP Context exitst:
            if (HttpContext.Current != null)
            {
                SetPrivateVariables();
            }
            else
            {
                setCultureUS();
            }
            InitializeDefaultPartner();
        }

        private void SetPrivateVariables()
        {
            string RequestURL = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
            if (RequestURL.Contains(".ca") || RequestURL.Contains("/canada/"))
            {
                setCultureCA();
            }
            else
            {
                setCultureUS();
            }
        }

        private void InitializeDefaultPartner()
        {
            // if culture is not set yet, generate an error
            if ( _countryCode == string.Empty || _countryCode == null )
                throw new ESubsGlobalException( "EsubGlobal: Culture object was not initialized");

            // if country is canada
            if (_countryCode == ESubsGlobal.Culture.EN_CA.CountryCode)
                defaultPartner = Partner.LoadByID(741, Culture.EN_CA);
            else if (_countryCode == ESubsGlobal.Culture.EN_US.CountryCode)
                defaultPartner = Partner.LoadByID(0, Culture.EN_US);
        }

        private void setCultureUS()
        {
            _languageCode = "EN";
            _countryCode = "US";
            _cultureCode = "en-US";
        }
        private void setCultureCA()
        {
            _languageCode = "EN";
            _countryCode = "CA";
            _cultureCode = "en-CA";
        }
		private Culture(string cultureCode)
		{
			_cultureCode = cultureCode.Trim();
			string[] s = _cultureCode.Split("-".ToCharArray(), 2);
			if (s.Length == 2)
			{
				_languageCode = s[0].ToUpper();
				_countryCode = s[1].ToUpper();
			}
			_cultureCode = _languageCode.ToLower() + "-" + _countryCode.ToUpper();
		}

		private Culture(string languageCode, string countryCode) {
			_languageCode = languageCode.Trim().ToUpper();
			_countryCode = countryCode.Trim().ToUpper();
			_cultureCode = _languageCode.ToLower() + "-" + _countryCode.ToUpper();
		}
		#endregion

		#region Methods

		/// <summary>
		/// Check if culture is supported.
		/// </summary>
		/// <param name="cultureCode"></param>
		/// <returns></returns>
		public static bool IsSupported(string cultureCode)
		{
			try 
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				dbo.GetCulture(cultureCode);
				return true;
			}
			catch {}
			return false;
		}

		public static Culture Create(string cultureCode)
		{
			// NOTE: We only support a limited number of cultures. 
			// Unsupported cultures will fallback to en-US.
			if (IsSupported(cultureCode))
				return new Culture(cultureCode);
			else
				return new Culture(DEFAULT.CultureCode);
		}
		#endregion

		#region Properties

		public static Culture DEFAULT {
            get
            {
                string RequestURL;

                if (HttpContext.Current == null)
                {
                    return Culture.EN_US;
                }

                try
                {
                   RequestURL = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
                }
                catch(Exception ex)
                {
                    throw new ESubsGlobalException("No HTTP Context can be found".ToString());
                }
                if (RequestURL.Contains(".ca") || RequestURL.Contains("/canada/") || 
                    // For development only
                    // On logout from canadian campaign, RequestURL is http://localhost:.../welcome.aspx/...
                    // so, default culture will always be en-US
                    // the following condition tests for the page that called the signout. And this is only
                    // needed on development. On Production, we will be having .ca in RequestURL
                    (RequestURL.Contains("localhost") && (RequestURL.Contains("welcome.aspx") || (RequestURL.Contains("default.aspx"))) && (HttpContext.Current.Request.UrlReferrer != null && HttpContext.Current.Request.UrlReferrer.AbsoluteUri.ToLower().Contains("/canada"))))
                    return Culture.EN_CA;
                else
                    return Culture.EN_US;
            }
		}

		public static Culture EN_US 
		{
			get { return new Culture("en-US"); }
		}

		public static Culture EN_CA {
			get { return new Culture("en-CA"); }
		}

        public static Culture FR_CA
        {
            get { return new Culture("fr-CA"); }
        }

		public string LanguageCode
		{
			get { return _languageCode;}
			set {_languageCode = value.ToUpper();}
		}

		public string CountryCode {
			get { return _countryCode; }
			set { _countryCode = value.ToUpper();}
		}

		public string CultureCode
		{
			get { return _cultureCode; }
			set { _cultureCode = value.Trim();}
		}

        // get the default partner for a given culture
        // 0 for US, 741 for CA
        public Partner DefaultPartner
        {
            get
            {
                if (defaultPartner == null)
                    InitializeDefaultPartner();
                return defaultPartner;
            }
        }

        
        #endregion
	}
}
