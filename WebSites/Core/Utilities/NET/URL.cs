using System;
using System.Net;
using System.IO;
using System.Web;

namespace GA.BDC.Core.Utilities.Net
{
	/// <summary>
	/// Sets of utilities to manage urls
	/// </summary>
	public class URL {

		/// <summary>
		/// URL Constructor
		/// </summary>
		public URL() {
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Get the web page content
		/// </summary>
		/// <param name="url">URL</param>
		/// <param name="userAgent">User agent</param>
		/// <returns>Page content</returns>
		public static string GetPageContent(string url, string userAgent) {
			WebClient client = new WebClient ();
			string s = "";

			try {
				client.Headers.Add ("user-agent", userAgent);

				Stream data = client.OpenRead (url);
                StreamReader reader = new StreamReader(data);
                s = reader.ReadToEnd();
                data.Close();
                reader.Close();
            }
            catch (System.Exception ex)
            {
                throw ex;
			}
			return s;
		}

		/// <summary>
		/// Get the web page content
		/// </summary>
		/// <param name="url">URL</param>
		/// <returns>Page Content</returns>
		/// <remarks>Default user agent is mozilla under windows</remarks>
		public static string GetPageContent(string url) 
        {
             
            

            string userAgent = HttpContext.Current.Request.UserAgent == null ? string.Empty : System.Web.HttpContext.Current.Request.UserAgent.ToString();

            if (string.IsNullOrEmpty(userAgent))
                userAgent = "Mozilla/4.0 (compatible; MSIE 6.0; MSIE 7.0; MSIE 8.0; Windows NT 5.1; Trident/4.0; GTB6.5; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";

            return GetPageContent(url, userAgent);		
		}
	}
}
