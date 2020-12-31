using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services.Protocols;
using Business.ReportExecution;
using System.Runtime.InteropServices; 
using Microsoft.ReportingServices.Interfaces;
using Business.Objects;
//using Microsoft.Samples.ReportingServices.CustomSecurity;

namespace QSPFulfillment.OrderMgt
{
	///<summary>ReportStatus</summary>
	public class ReportStatus : QSPFulfillment.CommonWeb.QSPPage
	{
		#region auto-generated code
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
			this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion auto-generated code
		
		#region item declarations
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.WebControls.Button btnResubmit;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		RSClient oRS = new RSClient();
		private string sRSUsername = ConfigurationSettings.AppSettings["RSPowerUsername"];
		private string sRSPassword = ConfigurationSettings.AppSettings["RSPowerPassword"];
		private string sRSExportPath = ConfigurationSettings.AppSettings["RSExportPath"];
		private string sRSExportUsername = ConfigurationSettings.AppSettings["RSExportUsername"];
		protected System.Web.UI.WebControls.Label Label3;

		private string sRSExportPassword = ConfigurationSettings.AppSettings["RSExportPassword"];
		#endregion item declarations

		#region page load
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack) 
			{
				Label1.Text = Request.QueryString["BatchOrderId"].ToString();

				Business.Report oReport = new Business.Report();

				DataGrid1.DataSource = oReport.GetBatchReportStatus(Convert.ToInt32(Request.QueryString["BatchOrderId"].ToString()),QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID);
				DataGrid1.DataBind();
			}
		}
		#endregion page load

		#region data grid
        private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string sStatus = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "LastStatus"));
				string sId = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "RSSubscriptionId"));

				if (sStatus + "" != "") 
				{
					if (sStatus.IndexOf("was written", 1) > 0) 
					{
						QSPFulfillment.CommonWeb.WebFileStreamerLinkButton oLink;
						oLink = (QSPFulfillment.CommonWeb.WebFileStreamerLinkButton)e.Item.FindControl("lnkWebFileStreamerLinkButtonReports");
						oLink.NavigateUrl = ConfigurationSettings.AppSettings["RSReportQueueURL"].ToString() + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Id")) + ".pdf";
						oLink.Visible=true;

						LinkButton oLB = new LinkButton();
						oLB = (LinkButton)e.Item.FindControl("btnResubmit");
						oLB.Visible = true;
					}

				}
				else
				{
					if (sId == "COMPLETE") 
					{
						QSPFulfillment.CommonWeb.WebFileStreamerLinkButton oLink;
						oLink = (QSPFulfillment.CommonWeb.WebFileStreamerLinkButton)e.Item.FindControl("lnkWebFileStreamerLinkButtonReports");
						oLink.NavigateUrl = ConfigurationSettings.AppSettings["RSReportQueueURL"].ToString() + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Id")) + ".pdf";
						oLink.Visible=true;
					}

					if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ArchiveTF")) + "" == "1") 
					{
						QSPFulfillment.CommonWeb.WebFileStreamerLinkButton oLink;
						oLink = (QSPFulfillment.CommonWeb.WebFileStreamerLinkButton)e.Item.FindControl("lnkWebFileStreamerLinkButtonReports");
						oLink.NavigateUrl = ConfigurationSettings.AppSettings["RSReportQueueURL"].ToString() + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Id")) + ".pdf";
						oLink.Visible=true;

						e.Item.Cells[2].Text = "Report #" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Id")) + ".pdf has been archived. You can still print this report."; 
					}
				}
				


			}


		}

		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			/*if (e.CommandName == "Resubmit") 
			{

				string sRSSubscriptionId = e.CommandArgument.ToString();

				oRS.LogonUser(sRSUsername, sRSPassword, null);

				ParameterValue[] oExtParameters;
				ExtensionSettings oExtSettings;
				ExtensionSettings oExtSettings3 = new ExtensionSettings();
				string sDescription;
				ActiveState oActiveState;
				string sStatus;
				string sEventType;
				string sMatchData;
				
				oRS.GetSubscriptionProperties(sRSSubscriptionId, out oExtSettings, out sDescription, out oActiveState, out sStatus, out sEventType, out sMatchData, out oExtParameters);
	
				string sReportRequestId = "";
				
				for(int i = 0; i < oExtSettings.ParameterValues.Length; i++)
				{
					ParameterValue oParam = (ParameterValue)oExtSettings.ParameterValues[i];
					if (oParam.Name == "FILENAME") 
					{
						sReportRequestId = oParam.Value;
					}
				}

				DateTime oPlus5 = System.DateTime.Now.AddMinutes(5);
				string sDate = oPlus5.Year.ToString() + "-" + oPlus5.Month.ToString() + "-" + oPlus5.Day.ToString();
				string sTime = oPlus5.TimeOfDay.ToString();

				//preventing "warning CS0219: The variable 'eventType' is assigned but its value is never used"
				//string eventType = "TimedSubscription";
				string scheduleXml = @"<ScheduleDefinition>";
				scheduleXml += @"<StartDateTime>" + sDate + "T" + sTime + "-05:00</StartDateTime></ScheduleDefinition>";
			
				ParameterValue[] extensionParams = new ParameterValue[7];
				for(int i = 0; i < extensionParams.Length; i++)
					extensionParams[i] = new ParameterValue();
			
				extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = sReportRequestId;

				extensionParams[1].Name = "FILEEXTN";
				extensionParams[1].Value = "true";

				extensionParams[2].Name = "PATH";
				extensionParams[2].Value = sRSExportPath;

				extensionParams[3].Name = "RENDER_FORMAT";
				extensionParams[3].Value = "PDF";

				extensionParams[4].Name = "WRITEMODE";
				extensionParams[4].Value = "Overwrite";

				extensionParams[5].Name = "USERNAME";
				extensionParams[5].Value = sRSExportUsername;

				extensionParams[6].Name = "PASSWORD";
				extensionParams[6].Value = sRSExportPassword;

				
				ExtensionSettings oExt = new ExtensionSettings();
				oExt.Extension = "Report Server FileShare";
				oExt.ParameterValues = extensionParams;

				oRS.SetSubscriptionProperties(sRSSubscriptionId, oExt, sDescription, sEventType, scheduleXml, oExtParameters);

				
				Label2.Text = "Report will be rerun in approximately 5 minutes.  Continue to check the file after 5 minutes to see if it has been rerun.";

			}*/
	    }
		#endregion data grid
	}
}
