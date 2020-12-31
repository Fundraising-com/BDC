using System;
using System.Collections;
using System.ComponentModel;
//using System.Configuration;
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

namespace QSPFulfillment.Finance
{
	///<summary>FinanceReportStatus</summary>
	public class FinanceReportStatus : QSPFulfillment.CommonWeb.QSPPage
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
		protected System.Web.UI.WebControls.Label lbReportsFor;
		protected System.Web.UI.WebControls.Label lbPageStatus;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.WebControls.Button btnResubmit;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
	    RSClient oRS = new RSClient();
		private string sRSUsername = System.Configuration.ConfigurationSettings.AppSettings["RSPowerUsername"];
		private string sRSPassword = System.Configuration.ConfigurationSettings.AppSettings["RSPowerPassword"];
		private string sRSExportPath = System.Configuration.ConfigurationSettings.AppSettings["RSExportPath"];
		private string sRSExportUsername = System.Configuration.ConfigurationSettings.AppSettings["RSExportUsername"];
		private string sRSReportQueueURL = System.Configuration.ConfigurationSettings.AppSettings["RSReportQueueURL"].ToString();
		private string sRSExportPassword = System.Configuration.ConfigurationSettings.AppSettings["RSExportPassword"];
		protected skmMenu.Menu menuFinanceLinks;
		#endregion item declarations

		#region page load
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				if(menuFinanceLinks != null && !IsSessionNull)
				{
					menuFinanceLinks.DataSource = Server.MapPath("/QSPFulfillment/Finance/FinanceReportStatusMenu.xml");
					menuFinanceLinks.DataBind();
				}
				//lbReportsFor.Text = Request.QueryString["BatchOrderId"].ToString();
				string sFakeBatchID = "";
				try
				{
					sFakeBatchID += Request.QueryString["UserInstance"].ToString();
				}
				catch(System.NullReferenceException){}

				int iFakeBatchID = 0;
				bool blLookUpReports = true;
				if( ! aUserProfile.HasRole("Finance")  )
				{
					sFakeBatchID = " User does not have permission to view Finance Reports";
					blLookUpReports = false;
				}
				if(sFakeBatchID == "")
				{
					iFakeBatchID = aUserProfile.Instance * -1;
					sFakeBatchID = " " + aUserProfile.UserName;
				}
				else
				{
					try
					{
						iFakeBatchID = Convert.ToInt32(sFakeBatchID) * -1;
						sFakeBatchID = " ID: " + sFakeBatchID ;
					}
					catch
					{
						sFakeBatchID = " Error determining desired user.";
						blLookUpReports = false;
					}
				}
				lbReportsFor.Text = sFakeBatchID;

				if(blLookUpReports)
				{
					Business.Report oReport = new Business.Report();
					DataGrid1.DataSource = oReport.GetBatchReportStatus(iFakeBatchID,"9999");
					DataGrid1.DataBind();
				}
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
						oLink = (QSPFulfillment.CommonWeb.WebFileStreamerLinkButton) e.Item.FindControl("lnkWebFileStreamerLinkButtonFinance");
						oLink.NavigateUrl = sRSReportQueueURL + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Id")) + ".pdf";
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
						oLink = (QSPFulfillment.CommonWeb.WebFileStreamerLinkButton) e.Item.FindControl("lnkWebFileStreamerLinkButtonFinance");
						oLink.NavigateUrl = sRSReportQueueURL + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Id")) + ".pdf";
						oLink.Visible=true;
					}

					if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ArchiveTF")) + "" == "1")
					{
						QSPFulfillment.CommonWeb.WebFileStreamerLinkButton oLink;
						oLink = (QSPFulfillment.CommonWeb.WebFileStreamerLinkButton) e.Item.FindControl("lnkWebFileStreamerLinkButtonFinance");
						oLink.NavigateUrl = sRSReportQueueURL + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Id")) + ".pdf";
						oLink.Visible=true;

						e.Item.Cells[2].Text = "Report #" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Id")) + ".pdf has been archived. You can still print this report.";
					}
				}



			}


		}

		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "Resubmit")
			{

			}
	}
		#endregion data grid
	}
}
