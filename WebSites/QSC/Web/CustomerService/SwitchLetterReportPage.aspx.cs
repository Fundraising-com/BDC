using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPFulfillment.DataAccess.Business;
using System.Net;
using System.IO;
using System.Web.Services.Protocols;
using Business.ReportExecution;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using Business.Objects;


namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for SwitchLetterReportPage.
	/// </summary>
	/// 

	
	public partial class SwitchLetterReportPage : CustomerServicePage
	{
		RSClient rs = new RSClient();
        private System.Text.StringBuilder sb = new System.Text.StringBuilder();
		private const int SREPORT_POSITION = 0;
		private const int STITLE_CODE_POSITION = 1;
		private const int IREMITBATCHID_POSITION = 2;
		private const int DFROM_POSITION = 3;
		private const int DTO_POSITION =4;
		private const int ISWITCHLETTERBATCHBATCHID_POSITION = 5;


		/*private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsSessionNull)
			{

				try
				{
					/*NetworkCredential creds = new NetworkCredential(QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ReportUserID,QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ReportPassword);
					rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
				
					rs.CookieContainer = new CookieContainer();
					rs.LogonUser(creds.UserName, creds.Password, null);
					//rs.CookieContainer.GetCookies(System.Uri.
					rs.AllowAutoRedirect = true;
					//Response.Cookies.Add(rs.CookieContainer.GetCookies();

					rs.PreAuthenticate= true;
					Response.Cookies.Add((HttpCookie)rs.CookieContainer.GetCookies(new Uri("http://uspvl2k20-dev"))[0]);*/
/*
					sb.Append(QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ReportDefaultURL+"/SwitchLetter&rs:Command=Render&rc:parameters=false&rc:toolbar=true&rs:Format=PDF");
					if(IsPreview)
					{
						if(GetTitleCode() != "" && GetRemitBatchID()==0)
						{
							sb.Append("&sTitleCode="+GetTitleCode());
							sb.Append("&sReport=pr_SwitchLetterSelectReportPreview");
							sb.Append("&dTo="+GetTo().ToString());
							sb.Append("&dFrom="+GetFrom().ToString());
						}
						else if(GetCustomerOrderHeader() !=0)
						{
							sb.Append("&sReport=pr_SwitchLetterSelectReportPreviewSub");
							sb.Append("&iCustomerOrderHeaderInstance="+ GetCustomerOrderHeader().ToString());
							sb.Append("&iTransID=" + GetTransID().ToString());
							//set default value because it dont work
							//TODO: dont supposed to always provide this value
							sb.Append("&dTo=" + new DateTime(1754,1,1).ToString());
							sb.Append("&dFrom=" + new DateTime(1754,1,1).ToString());
						}
						else
						{
				
							sb.Append("&sReport=pr_SwitchLetterSelectReportPreview");
							sb.Append("&iRemitBatchID="+ GetRemitBatchID().ToString());
							sb.Append("&sTitleCode="+GetTitleCode());
							//set default value because it dont work
							//TODO: dont supposed to always provide this value
							sb.Append("&dTo="+ new DateTime(1754,1,1).ToString());
							sb.Append("&dFrom="+new DateTime(1754,1,1).ToString());
						}
					}
					else
					{
						sb.Append("&sReport=pr_SwitchLetterSelectReport");
						sb.Append("&iSwitchLetterBatchID="+GetSwitchLetterBatchID().ToString());
						//set default value because it dont work
						//TODO: dont supposed to always provide this value
						sb.Append("&dTo=" + new DateTime(1754,1,1).ToString());
						sb.Append("&dFrom="+ new DateTime(1754,1,1).ToString());


					}
					
					Response.Redirect(sb.ToString());
				}
				catch
				{
				
				}
			}
			else if(IsSessionNull && IsNewWindow)
			{
				this.AddScriptRelaodClose();
			}
			
			
		
		}*/
		
			private void Render()
			{
				if(!IsSessionNull)
				{

					Byte[] b;
					b = SetReportDocument(GetTitleCode(),GetFrom(),GetTo(),GetRemitBatchID(),IsPreview,GetSwitchLetterBatchID());
					Response.Clear();
					//Response.ContentType = "application/pdf";
				/*	Response.AddHeader("Content-Type", "application/pdf");
					Response.AppendHeader("content-length", b.Length.ToString());
					Response.AppendHeader("content-disposition", "inline:filename=blah.pdf");
					Response.AppendHeader("Title","Fulfillment Report Viewer (PDF)");
					Response.Clear();*/
					Response.AddHeader("Content-Type", "application/pdf");
					Response.AddHeader("content-length", b.Length.ToString());
					Response.AddHeader("Title","Fulfillment Report Viewer (PDF)");
		
					Response.BinaryWrite(b);
					Response.Flush();
					Response.Close();
				
				
		

					
				}
				else if(IsSessionNull && IsNewWindow)
				{
					this.AddScriptReloadClose();
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
			//this.Load +=new EventHandler(Page_Load);
			  this.PreRender +=new EventHandler(SwitchLetterReportPage_PreRender);

		}
		#endregion
		
	
		private bool IsPreview
		{
			get
			{
				return Convert.ToBoolean(Context.Request.QueryString["Preview"]);
			}
		}
		private int GetSwitchLetterBatchID()
		{			
			if(	Context.Request.QueryString["SLBID"] != null)
			return Convert.ToInt32(Context.Request.QueryString["SLBID"]);

			return 0;
		}
		private string GetTitleCode()
		{
			if(	Context.Request.QueryString["TitleCode"] != null)
				return Context.Request.QueryString["TitleCode"];

			return "";	
		}
		private int GetRemitBatchID()
		{
			if(	Context.Request.QueryString["RemitBatchID"] != null)
				return Convert.ToInt32(Context.Request.QueryString["RemitBatchID"]);

			return 0;	
		}
		/*private int GetCustomerOrderHeader()
		{
			if(	Context.Request.QueryString["COHI"] != null)
				return Convert.ToInt32(Context.Request.QueryString["COHI"]);

			return 0;
		}*/
		/*private int GetTransID()
		{
			if(	Context.Request.QueryString["TransID"] != null)
				return Convert.ToInt32(Context.Request.QueryString["TransID"]);

			return 0;
		}*/
		private DateTime GetFrom()
		{
			if(	Context.Request.QueryString["From"] != null)
				return Convert.ToDateTime(Context.Request.QueryString["From"]);

			return new DateTime(1754,1,1);
		}
		private DateTime GetTo()
		{
			if(	Context.Request.QueryString["To"] != null)
				return Convert.ToDateTime(Context.Request.QueryString["To"]);

			return new DateTime(1754,1,1);
		}

	private void SwitchLetterReportPage_PreRender(object sender, EventArgs e)
		{
			Render();
		}
		public Byte[] SetReportDocument(string TitleCode,DateTime From,DateTime TO,int RemitBatchID,bool IsPreview,int SwitchLetterBatchID)
		{
            /*
			// Render arguments
			byte[] results = null;
		    NetworkCredential creds = new NetworkCredential(QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ReportUserID, QSPFulfillment.DataAccess.Common.ApplicationConfiguration.ReportPassword);
			
			try
			{

				DataSourceCredentials[] dtscre = null;
				string PathReport = "SwitchLetter";
				string format = "PDF";
				string historyID = null;


				// Make the second call to Render.

				string devInfo = @"<DeviceInfo><Toolbar>false</Toolbar></DeviceInfo>";
				ReportParameter[] parameters = null;
				ParameterValue[] parametervalues = null;
				DataSourceCredentials[] credentials = null;
				string showHideToggle = null;
				string encoding;
				string mimeType;
				Warning[] warnings = null;
				ParameterValue[] reportHistoryParameters = null;
				string[] streamIDs = null;

				rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
				rs.CookieContainer = new CookieContainer();
				rs.LogonUser(creds.UserName, creds.Password, null);
				
				
				parameters = rs.GetReportParameters(PathReport,historyID,false,parametervalues,dtscre);
				parametervalues =	GetInitParameterValue(parameters);
				
				
				SetDefaultValue(parametervalues);

				if(IsPreview)
				{

					

					if(TitleCode != "" && RemitBatchID==0)
					{

						parametervalues[STITLE_CODE_POSITION].Value	= TitleCode;
						parametervalues[SREPORT_POSITION].Value	= "pr_SwitchLetterSelectReportPreview";
						parametervalues[DFROM_POSITION].Value	= From.ToString();
						parametervalues[DTO_POSITION].Value	= TO.ToString();

					}
					else
					{
				
									
					
						parametervalues[SREPORT_POSITION].Value	= "pr_SwitchLetterSelectReportPreview";
						parametervalues[IREMITBATCHID_POSITION].Value	= RemitBatchID.ToString();
						parametervalues[STITLE_CODE_POSITION].Value	= TitleCode;
						
					}
				}
				else
				{

					parametervalues[ISWITCHLETTERBATCHBATCHID_POSITION].Value	= SwitchLetterBatchID.ToString();
					parametervalues[SREPORT_POSITION].Value	= "pr_SwitchLetterSelectReport";
					



				}
						
				SessionHeader sh = new SessionHeader();
				rs.SessionHeaderValue = sh;

				results = rs.Render(PathReport, format, historyID, devInfo, parametervalues,
					credentials,
					showHideToggle, out encoding, out mimeType, out reportHistoryParameters,
					out warnings,
					out streamIDs);

				

				sh.SessionId = rs.SessionHeaderValue.SessionId;
							
			}
			catch (Exception ex)
			{
				throw ex;
			}
            */
            byte[] results = null;
			return results;
			
		}
		private void SetDefaultValue(ParameterValue[] Values)
		{
			Values[STITLE_CODE_POSITION].Value	= "0";
			//Values[SREPORT_POSITION].Value	= "pr_SwitchLetterSelectReportPreview";
			Values[DFROM_POSITION].Value	= new DateTime(1754,1,1).ToString();
			Values[DTO_POSITION].Value	= new DateTime(1754,1,1).ToString();
			Values[ISWITCHLETTERBATCHBATCHID_POSITION].Value = "0";
			Values[IREMITBATCHID_POSITION].Value	= "0";
			
		}
		

	
	}
}
