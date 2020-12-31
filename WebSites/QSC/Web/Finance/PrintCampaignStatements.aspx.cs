using System;
using System.Net; //for Cookie
using System.Configuration; //For Web.Config
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services.Protocols;
using QSPFulfillment.RDReportServer;
using System.Runtime.InteropServices;
using Microsoft.ReportingServices.Interfaces;
//using Microsoft.Samples.ReportingServices.CustomSecurity;

namespace QSPFulfillment.Finance
{
	public partial class PrintCampaignStatements : QSPFulfillment.CommonWeb.QSPPage
	{
		RSClient rs = new RSClient();
		byte[][] m_renderedReport;
		Graphics.EnumerateMetafileProc m_delegate = null;
		MemoryStream m_currentPageStream;
		Metafile m_metafile = null;
		int	m_numberOfPages;
		int	m_currentPrintingPage;
		int	m_lastPrintingPage;
		public string m_reportName;

		#region PrintCampaignStatements Constructor
		public PrintCampaignStatements()
		{
			NetworkCredential creds = rs.GetCredentials();
			rs.LogonUser(creds.UserName, creds.Password, null);

			try
			{
				if (rs.CheckAuthorized())
				{
					ItemTypeEnum type = rs.GetItemType("/");
				}
			}
			catch(Exception ex)
			{
				Context.Response.Write("<br>" + ex.Message + "<br>" + ex.InnerException + "<br>" + ex.Source + "<br>" + ex.StackTrace);
				return;
			}
		}
		#endregion

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{}
		#endregion

		#region RenderReport
		public byte[][] RenderReport(string reportPath, string strParamName, string strParamValue,
			string strParamName2, string strParamValue2,
			string strParamName3, string strParamValue3,
			string strParamName4, string strParamValue4)
		{
			// Variables for rendering
			string deviceInfo = null;
			string format = "IMAGE";
			//set the params
			ParameterValue[] parameters = new ParameterValue[4];
			parameters[0] = new ParameterValue();
			parameters[0].Name  = strParamName.Trim();
			parameters[0].Value = strParamValue.Trim();
			parameters[1] = new ParameterValue();
			parameters[1].Name  = strParamName2.Trim();
			parameters[1].Value = strParamValue2.Trim();
			parameters[2] = new ParameterValue();
			parameters[2].Name  = strParamName3.Trim();
			parameters[2].Value = strParamValue3.Trim();
			parameters[3] = new ParameterValue();
			parameters[3].Name  = strParamName4.Trim();
			parameters[3].Value = strParamValue4.Trim();
			Byte[] firstPage = null;
			string encoding;
			string mimeType;
			Warning[] warnings = null;
			ParameterValue[] reportHistoryParameters = null;
			string[] streamIDs = null;
			Byte[][] pages = null;

			// Build device info based on the start page
			deviceInfo = String.Format(@"<DeviceInfo><OutputFormat>{0}</OutputFormat></DeviceInfo>", "emf");

			//Exectute the report and get page count.
			try
			{
				// Renders the first page of the report and returns streamIDs for subsequent pages
				firstPage = rs.Render(
					reportPath,
					format,
					null,
					deviceInfo,
					parameters,
					null,
					null,
					out encoding,
					out mimeType,
					out reportHistoryParameters,
					out warnings,
					out streamIDs);

				// The total number of pages of the report is 1 + the streamIDs
				m_numberOfPages = streamIDs.Length + 1;
				pages = new Byte[m_numberOfPages][];

				// The first page was already rendered
				pages[0] = firstPage;

				for (int pageIndex = 1; pageIndex < m_numberOfPages; pageIndex++)
				{
					// Build device info based on start page
					deviceInfo =
						String.Format(@"<DeviceInfo><OutputFormat>{0}</OutputFormat><StartPage>{1}</StartPage></DeviceInfo>",
						"emf", pageIndex+1);
					pages[pageIndex] = rs.Render(
						reportPath,
						format,
						null,
						deviceInfo,
						parameters,
						null,
						null,
						out encoding,
						out mimeType,
						out reportHistoryParameters,
						out warnings,
						out streamIDs);
				}
			}
			catch (SoapException ex)
			{
				Context.Response.Write(ex.Detail.InnerXml);
			}
			catch (Exception ex)
			{
				Context.Response.Write("<br>" + ex.Message + "<br>" + ex.InnerException + "<br>" + ex.Source + "<br>" + ex.StackTrace);
			}
			return pages;
		}
		#endregion

		#region PrintStatement
		public bool PrintCampaignStatement(string printerName, string reportName, string strParamName, string strParamValue,
			string strParamName2, string strParamValue2,
			string strParamName3, string strParamValue3,
			string strParamName4, string strParamValue4)
		{
			this.RenderedReport = this.RenderReport(reportName, strParamName, strParamValue,
				strParamName2, strParamValue2,
				strParamName3, strParamValue3,
				strParamName4, strParamValue4);

			try
			{
				// Wait for the report to completely render.
				if(m_numberOfPages < 1)
					return false;
				// Print Document
				PrintDocument pd				= new PrintDocument();
				m_currentPrintingPage			= 1;
				m_lastPrintingPage				= m_numberOfPages;
				// Printer Settings
				PrinterSettings ps				= new PrinterSettings();
				ps.MaximumPage					= m_numberOfPages;
				ps.MinimumPage					= 1;
				ps.PrintRange					= PrintRange.SomePages;
				ps.FromPage						= 1;
				ps.ToPage						= m_numberOfPages;
				ps.PrinterName					= printerName.Trim();
				pd.PrinterSettings				= ps;
				pd.OriginAtMargins				= true;
				pd.DefaultPageSettings.Margins	= new Margins(0, 0, 0, 0);

				// Print report
				pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
				pd.Print();
			}
			catch(Exception ex)
			{
				Context.Response.Write("<br>" + ex.Message + "<br>" + ex.InnerException + "<br>" + ex.Source + "<br>" + ex.StackTrace);
			}
			return true;
		}
		#endregion

		#region pd_PrintPage
		private void pd_PrintPage(object sender, PrintPageEventArgs ev)
		{
			ev.HasMorePages = false;
			if (m_currentPrintingPage <= m_lastPrintingPage && MoveToPage(m_currentPrintingPage))
			{
				// Draw the page
				ReportDrawPage(ev.Graphics);
				// If the next page is less than or equal to the last page, print another page.
				if (++m_currentPrintingPage <= m_lastPrintingPage)
					ev.HasMorePages = true;
			}
		}
		#endregion

		#region ReportDrawPage
		// Method to draw the current emf memory stream
		private void ReportDrawPage(Graphics g)
		{
			if(null == m_currentPageStream || 0 == m_currentPageStream.Length || null ==m_metafile)
				return;
			lock(this)
			{
				// Set the metafile delegate.
				int width = m_metafile.Width;
				int height= m_metafile.Height;
				m_delegate = new Graphics.EnumerateMetafileProc(MetafileCallback);
				// Draw in the rectangle
				Point[] points		= new Point[3];
				Point destPoint		= new Point(0, 0);
				Point destPoint1	= new Point(width, 0);
				Point destPoint2	= new Point(0, height);
				points[0]=destPoint;
				points[1]=destPoint1;
				points[2]=destPoint2;
				g.EnumerateMetafile(m_metafile,points, m_delegate);
				// Clean up
				m_delegate = null;
			}
		}
		#endregion

		#region MoveToPage
		private bool MoveToPage(Int32 page)
		{
			// Check to make sure that the current page exists in the array list
			if(null == this.RenderedReport[m_currentPrintingPage-1])
				return false;
			// Set current page stream equal to the rendered page
			m_currentPageStream = new MemoryStream(this.RenderedReport[m_currentPrintingPage-1]);
			// Set its postion to start.
			m_currentPageStream.Position = 0;
			// Initialize the metafile
			if(null != m_metafile)
			{
				m_metafile.Dispose();
				m_metafile = null;
			}
			// Load the metafile image for this page
			m_metafile =  new Metafile((Stream)m_currentPageStream);
			return true;
		}
		#endregion

		#region MetafileCallback
		private bool MetafileCallback(
			EmfPlusRecordType recordType,
			int flags,
			int dataSize,
			IntPtr data,
			PlayRecordCallback callbackData)
		{
			byte[] dataArray = null;
			if (data != IntPtr.Zero)
			{
				// Copy the unmanaged record to a managed byte buffer that can be used by PlayRecord.
				dataArray = new byte[dataSize];
				Marshal.Copy(data, dataArray, 0, dataSize);
			}
			m_metafile.PlayRecord(recordType, flags, dataSize, dataArray);
	
			return true;
		}
		#endregion

		#region RenderedReport
		public byte[][] RenderedReport
		{
			get
			{return m_renderedReport;}
			set
			{m_renderedReport = value;}
		}
		#endregion

		#region GetPrinterList
		public void GetPrinterList(DropDownList ddl)
		{
			PrintDocument pd = new PrintDocument();
			string strDefaultPrinter = pd.PrinterSettings.PrinterName;
			int i = 0;

			foreach(string strPrinter in PrinterSettings.InstalledPrinters)
			{
				ddl.Items.Add(strPrinter);
				if (strPrinter.ToString() == strDefaultPrinter.ToString())
				{
					ddl.SelectedIndex = i;
				}
				i++;
			}
		}
		#endregion

		public class RSClient : ReportingService
		{
			public bool m_needLogon = false;
			private string m_authCookieName;
			private Cookie m_authCookie;

			public RSClient()
			{
				// Set the server URL
				base.Url = ConfigurationSettings.AppSettings["RSServer"];
				// Set default credentials to integrated.
				Credentials = System.Net.CredentialCache.DefaultCredentials;
			}

			/// <summary>
			/// Gets the type of the item on the report server. Use the
			/// new modifier to hide the base implementation.
			/// </summary>
			public new ItemTypeEnum GetItemType(string item)
			{
				ItemTypeEnum type = ItemTypeEnum.Unknown;
				try
				{
					type = base.GetItemType(item);
				}

				catch(SoapException)
				{
					return ItemTypeEnum.Unknown;
				}

				return type;
			}

			/// <summary>
			/// Get whether the given credentials can connect to the report server.
			/// Returns false if not authorized. Other errors throw an exception.
			/// </summary>
			public bool CheckAuthorized()
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
					// This condition could be caused by a redirect to a forms logon page
					Console.WriteLine("InvalidOperationException");
					if (m_needLogon)
					{
						NetworkCredential creds = Credentials as NetworkCredential;
						if (creds != null && creds.UserName != null)
						{
							try
							{
								base.CookieContainer = new CookieContainer();
								base.LogonUser(creds.UserName, creds.Password, null);
								return true;
							}
							catch (Exception)
							{
								return false;
							}
						}
					}
					else
					{
						throw;
					}
				}
				return true;
			}
			//Should get this from the web.config.
			public NetworkCredential GetCredentials()
			{
				//return new NetworkCredential("id", "pwd");
				return new NetworkCredential(ConfigurationSettings.AppSettings["RSUID"], ConfigurationSettings.AppSettings["RSPwd"]);
			}

			protected override WebRequest GetWebRequest(Uri uri)
			{
				HttpWebRequest request;
				request = (HttpWebRequest)HttpWebRequest.Create(uri);
				request.Credentials = base.Credentials;
				request.CookieContainer = new CookieContainer();
				if (m_authCookie != null)
				{
					request.CookieContainer.Add(m_authCookie);
				}
				return request;
			}

			protected override WebResponse GetWebResponse(WebRequest request)
			{
				WebResponse response = base.GetWebResponse(request);
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
			}
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}

