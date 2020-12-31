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

namespace QSPFulfillment.OrderMgt
{
	///<summary>ReportStatus</summary>
	public class BatchReportStatus : QSPFulfillment.CommonWeb.QSPPage
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
		protected System.Web.UI.WebControls.Label Label3;

		#endregion item declarations

		#region page load
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack) 
			{
				Label1.Text = Request.QueryString["BatchOrderId"].ToString();

				Business.Report oReport = new Business.Report();

                int? shipmentGroupID;
                if (Request.QueryString["ShipmentGroupID"] == "")
                    shipmentGroupID = null;
                else
                    shipmentGroupID = Convert.ToInt32(Request.QueryString["ShipmentGroupID"].ToString());

                DataGrid1.DataSource = oReport.GetBatchReportsInfo(Convert.ToInt32(Request.QueryString["BatchOrderId"].ToString()), shipmentGroupID, QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID);
				
				DataGrid1.DataBind();
			}
		}
		#endregion page load

		#region data grid
		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{

				QSPFulfillment.CommonWeb.WebFileStreamerLinkButton oLink;
				oLink = (QSPFulfillment.CommonWeb.WebFileStreamerLinkButton)e.Item.FindControl("lnkWebFileStreamerLinkButtonBatchReports");

				string RepType = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ReportTypeID"));
				string RepStatus = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "LastStatus"));
					   
				if (RepType == "1") //pick list
				{
					oLink.NavigateUrl = ConfigurationSettings.AppSettings["BatchFilesURL"].ToString() + "PickList/"+Convert.ToString(DataBinder.Eval(e.Item.DataItem, "FileName"));
				}

				else if (RepType == "2")//packing slip
				{
					oLink.NavigateUrl = ConfigurationSettings.AppSettings["BatchFilesURL"].ToString() +"PackingSlip/"+ Convert.ToString(DataBinder.Eval(e.Item.DataItem, "FileName"));
				}

				else if (RepType == "3")//bhe
				{
					oLink.NavigateUrl = ConfigurationSettings.AppSettings["BatchFilesURL"].ToString() +"BHELabels/"+ Convert.ToString(DataBinder.Eval(e.Item.DataItem, "FileName"));
				}
				else if (RepType == "4") //teacher
				{
					oLink.NavigateUrl = ConfigurationSettings.AppSettings["BatchFilesURL"].ToString() +"TeacherBoxLabels/"+ Convert.ToString(DataBinder.Eval(e.Item.DataItem, "FileName"));
				}

				else if (RepType == "5") //participant listing
				{
					oLink.NavigateUrl = ConfigurationSettings.AppSettings["BatchFilesURL"].ToString() +"ParticipantListing/"+ Convert.ToString(DataBinder.Eval(e.Item.DataItem, "FileName"));
				}

				else if (RepType == "6") //hoemroom
				{
					oLink.NavigateUrl = ConfigurationSettings.AppSettings["BatchFilesURL"].ToString() +"HomeRoomSummary/"+ Convert.ToString(DataBinder.Eval(e.Item.DataItem, "FileName"));
				}
				else if (RepType == "7")//group room
				{
					oLink.NavigateUrl = ConfigurationSettings.AppSettings["BatchFilesURL"].ToString() +"GroupSummary/"+ Convert.ToString(DataBinder.Eval(e.Item.DataItem, "FileName"));
				}
				else if (RepType == "8") //mag item
				{
					oLink.NavigateUrl = ConfigurationSettings.AppSettings["BatchFilesURL"].ToString() +"MagazineItemsSummary/"+ Convert.ToString(DataBinder.Eval(e.Item.DataItem, "FileName"));
				}
				else if (RepType == "9") //problem solver
				{
					oLink.NavigateUrl = ConfigurationSettings.AppSettings["BatchFilesURL"].ToString() +"ProblemSolver/"+ Convert.ToString(DataBinder.Eval(e.Item.DataItem, "FileName"));
				}
				else if (RepType == "10") // OE
				{
					oLink.NavigateUrl = ConfigurationSettings.AppSettings["BatchFilesURL"].ToString() +"OrderEntryFollowup/"+ Convert.ToString(DataBinder.Eval(e.Item.DataItem, "FileName"));
				}
				else if (RepType == "11") // price desc
				{
					oLink.NavigateUrl = ConfigurationSettings.AppSettings["BatchFilesURL"].ToString() +"PriceDiscrepancy/"+ Convert.ToString(DataBinder.Eval(e.Item.DataItem, "FileName"));
				}
					
				if (RepStatus == "Completed")
				{
					oLink.Visible=true;
				}


						LinkButton oLB = new LinkButton();
						oLB = (LinkButton)e.Item.FindControl("btnResubmit");
						oLB.Visible = true;
			}

			}
		

		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "Resubmit") 
			{
				
				Label2.Text = "UNDER CONSTRUCTION";

			}
	     }
		#endregion data grid
	}
}
