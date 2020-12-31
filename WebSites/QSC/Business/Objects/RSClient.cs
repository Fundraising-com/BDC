using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration; //For Web.Config
using System.Data;
using System.Drawing;
using System.Net; //for Cookie
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services.Protocols;
using System.Runtime.InteropServices; 
using Microsoft.ReportingServices.Interfaces;
using FileStore;
using Business.ReportExecution;
using Business.ReportService;

namespace Business.Objects
{
	/// <summary>
	/// Summary description for RSClient.
	/// </summary>
    public class RSClient
	{
        private const string DEFAULT_REPORT_FOLDER = "";

		public bool m_needLogon = false;
		private string m_authCookieName;
		private Cookie m_authCookie;
        ReportExecutionService rsExec = new ReportExecutionService();
        ReportingService2005 rs = new ReportingService2005();
        private ParameterFieldReference[] reportParameters = null;

		public RSClient()
		{
            rsExec.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rsExec.Url = ConfigurationSettings.AppSettings["RSServerExec"];
        
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Url = ConfigurationSettings.AppSettings["RSServer"];
        }

        public ParameterFieldReference[] ReportParameters
        {
            get
            {
                return reportParameters;
            }
            set
            {
                reportParameters = value;
            }
        }

		/// <summary>
		/// Gets the type of the item on the report server. Use the 
		/// new modifier to hide the base implementation.
		/// </summary>
		/*public ItemTypeEnum GetItemType(string item)
		{
			ItemTypeEnum type = ItemTypeEnum.Unknown;
			try
			{
				type = GetItemType(item);
			}

			catch(SoapException)
			{
				return ItemTypeEnum.Unknown;
			}

			return type;
		}*/

		/// <summary>
		/// Get whether the given credentials can connect to the report server.
		/// Returns false if not authorized. Other errors throw an exception.
		/// </summary>
		/*public bool CheckAuthorized()
		{
			try
			{
				GetItemType("/");
			}
			catch (WebException e)
			{
				if (! (e.Response is HttpWebResponse) ||
					((HttpWebResponse)e.Response).StatusCode != HttpStatusCode.Unauthorized)
				{
					throw;
				}
				return false;
			}
			catch(InvalidOperationException)
			{
				if (m_needLogon)
				{
					try
					{
						rs.CookieContainer = new CookieContainer();
						rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
						return true;
					}
					catch (Exception)
					{
						return false;
					}
				}
				else
				{
					throw;
				}
			}
			return true;
		}*/

		/*protected override WebRequest GetWebRequest(Uri uri)
		{
			HttpWebRequest request;
			request = (HttpWebRequest)HttpWebRequest.Create(uri);
			request.Credentials = Credential;
			request.CookieContainer = new CookieContainer();
			if (m_authCookie != null)
			{
				request.CookieContainer.Add(m_authCookie);
			}
			return request;
		}

		protected override WebResponse GetWebResponse(WebRequest request)
		{
			WebResponse response = GetWebResponse(request);
			string cookieName = response.Headers["RSAuthenticationHeader"];
			if (cookieName != null)
			{
				m_authCookieName = cookieName;
				HttpWebResponse webResponse = (HttpWebResponse)response;
				Cookie authCookie = webResponse.Cookies[cookieName];
				// save it away 
				m_authCookie = authCookie;
			}
			// need to call logon
			if (response.Headers["RSNotAuthenticated"] != null)
			{
				m_needLogon = true;
			}
			return response;
		}*/

        private string RSBaseDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RSBaseDirectory"];
            }
        }

        private string ProcessReportName(string reportName)
        {
            string processedReportName = reportName;

            if (reportName[0] != '/')
            {
                processedReportName = DEFAULT_REPORT_FOLDER + reportName;
            }

            return processedReportName;
        }

        public void SaveReport(string filePath, byte[] reportStream)
        {
            Store store = new Store();
            store.Add(filePath, reportStream);
        }

        public byte[] GenerateReportStream(string reportName, string reportFormat, ReportExecution.ParameterValue[] parameterValues, int timeOut)
        {
            reportName = ProcessReportName(reportName);

            return CallReportServicesDirect(reportName, reportFormat, parameterValues, timeOut);
        }

        private byte[] CallReportServicesDirect(string reportName, string reportFormat, ReportExecution.ParameterValue[] parameterValues, int timeOut)
        {
            Byte[] result;

            string extension;
            string encoding;
            string mimetype;
            ReportExecution.Warning[] warnings;
            string[] streamids;

            rs.Timeout = timeOut;

            ExecutionHeader execHeader = new ExecutionHeader();

            rsExec.ExecutionHeaderValue = execHeader;
            rsExec.LoadReport(RSBaseDirectory + reportName, null);
            rsExec.SetExecutionParameters(parameterValues, "en-us");

            result = rsExec.Render(
                reportFormat,
                null,
                out extension,
                out encoding,
                out mimetype,
                out warnings,
                out streamids);

            return result;
        }
	}
}
