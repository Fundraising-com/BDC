//
// 2005-05-15 - Stephen Lim - New class.
//
using System;
using System.Collections.Specialized;
using System.Web;

namespace GA.BDC.Core.Diagnostics
{
	/// <summary>
	/// Summary description for WebEnvironment.
	/// </summary>
	public class WebEnvironment
	{
		#region Fields
		private string _url = "";
		private string _remoteIp = "";
		private string _httpMethod = "";
		private bool _newSession = true;
		private string _query = "";
		private NameValueCollection _headers = new NameValueCollection();
		private NameValueCollection _form = new NameValueCollection();
		#endregion

		#region Constructors
		private WebEnvironment()
		{
		}
		#endregion

		#region Methods
		public static WebEnvironment NewWebEnvironment()
		{
			HttpContext ctxt = HttpContext.Current;
			if (ctxt != null)
			{
				HttpRequest req = HttpContext.Current.Request;
				WebEnvironment webEnv = new WebEnvironment();
				webEnv.Url = req.Url.ToString();
				webEnv.RemoteIp = req.UserHostAddress;
				webEnv.HttpMethod = req.HttpMethod;
				webEnv.NewSession = ctxt.Session.IsNewSession;
				webEnv.Query = req.QueryString.ToString();
				webEnv.Headers = req.Headers;
				webEnv.Form = req.Form;
				return webEnv;
			}
			return null;
		}
		#endregion

		#region Methods
		public string GeneratePostData() {
			string postedData = "<PostedData>\r\n";
			for(int i=0;i<Form.Count;i++) {
				postedData += "<" + Form.AllKeys[i] + ">" + Form[i] + "</" + Form.AllKeys[i] + ">\r\n";
			}
			return postedData + "</PostedData>";
		}
		#endregion

		#region Properties
		public string Url
		{
			get { return _url; }
			set { _url = value; }
		}

		public string RemoteIp
		{
			get { return _remoteIp; }
			set { _remoteIp = value; }
		}

		public string HttpMethod
		{
			get { return _httpMethod; }
			set { _httpMethod = value; }
		}

		public bool NewSession
		{
			get { return _newSession; }
			set { _newSession = value; }
		}

		public string Query
		{
			get { return _query; }
			set { _query = value; }
		}

		public NameValueCollection Headers
		{
			get { return _headers; }
			set { _headers = value; }
		}

		public NameValueCollection Form
		{
			get { return _form; }
			set { _form = value; }
		}
		#endregion
	}
}
