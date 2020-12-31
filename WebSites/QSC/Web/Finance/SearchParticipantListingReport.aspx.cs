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
using QSPFulfillment.CommonWeb;
//using System.Data.SqlClient;
//using System.Text.RegularExpressions;
//using System.Configuration;
//using DAL;

namespace QSPFulfillment.Finance
{
	public class SearchParticipantListingReport : QSPFulfillment.CommonWeb.QSPPage
	{
		#region Constructor
		public SearchParticipantListingReport()
		{
			aParticipantListingReportDataAccess = new DAL.ParticipantListingReportDataAccess();
		}
		#endregion Constructor

		#region Item Declarations
		//private string RSInstallation = System.Configuration.ConfigurationSettings.AppSettings["RSInstallation"];
		protected DAL.ParticipantListingReportDataAccess aParticipantListingReportDataAccess;
		//protected DataSet ds  = new DataSet();
		protected DataView dv = new DataView();
		protected TextBox Search;
		protected TextBox FromDate;
		protected TextBox ToDate;
		protected DataGrid ParticipantListingReportDG;
		protected Label lblListing;
		protected Label LabelMsg;

		//Number of Records to show
		protected DropDownList ddlStatus;
		//Show Printed ones or not
		protected DropDownList ddlPrinted;

		protected string strSearchField = "";

		protected DateTime dtStartParam, dtEndParam, dtStartDate;
		protected string strStartDate, strEndDate;

		//last week
		protected string strDayLW;
		protected string strMonthLW;

		protected System.Web.UI.WebControls.LinkButton BtnSearch;
		#endregion Item Declarations

		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{

			if(Request.QueryString["Search"] != null)
			{
				strSearchField = Request.QueryString["Search"];
			}

			if (!IsPostBack)
			{
				Search.Text = strSearchField;
				// go back 60 days
				TimeSpan ts = new System.TimeSpan(-60, 0, 0, 0);
				DateTime aToDate = DateTime.Now.Add(ts);
				strStartDate = aToDate.ToString("dd-MMM-yyyy").Trim();
				strEndDate   = DateTime.Now.ToString("dd-MMM-yyyy").Trim();

				//Textboxes
				FromDate.Text = strStartDate;
				ToDate.Text   = strEndDate;

				//Bind the invoice datagrid
				BindSummaryReportGrid(strSearchField, strStartDate, strEndDate);
			}
		}

		#endregion Page Load

		#region Grid
		public void BindSummaryReportGrid(string strSearchField, string strFromDate, string strToDate)
		{
			LabelMsg.Text = "";
			//Get all OrderID's by date range
			//ds = aParticipantListingReportDataAccess.GetParticipantListingReportToPrint(strFromDate, strToDate);
			//dv = ds.Tables[0].DefaultView;
			dv = ( (DataTable)
					aParticipantListingReportDataAccess.GetParticipantListingReportToPrint(
						Convert.ToDateTime(strFromDate), 
						Convert.ToDateTime(strToDate))
				).DefaultView;

			if(strSearchField.Length > 0)
			{
				if(ddlStatus.SelectedItem.Value.Trim()=="OrderID")
				{
					//dv.RowFilter = "OrderID = " + Regex.Replace(strSearchField,"'","''");
					dv.RowFilter = "OrderID = " + strSearchField.Replace("'","''");
					Session["ParticipantListingReportSortField"] = "Name, OrderID";
				}
				else if(ddlStatus.SelectedItem.Value.Trim()=="CampaignID")
				{
					//dv.RowFilter = "CampaignID = " + Regex.Replace(strSearchField,"'","''");
					dv.RowFilter = "CampaignID = " + strSearchField.Replace("'","''");
					Session["ParticipantListingReportSortField"] = "Name, CampaignID";
				}
			}
			else
			{
				if (Session["ParticipantListingReportSortField"] == null) Session["ParticipantListingReportSortField"] = "Name, OrderID";
			}

			dv.Sort = (string)Session["ParticipantListingReportSortField"];
			int totalItems = Convert.ToInt32(dv.Count);
			ShowParticipantStats(totalItems);

			// bind to the Data
			ParticipantListingReportDG.DataSource = dv;
			ParticipantListingReportDG.DataBind();
		}

		public void ShowParticipantStats(int nCount)
		{
			if (nCount == 0){lblListing.Text = "No Records Found."; }
			else
			{
				int nStartOfSet = (nCount > 0) ? (ParticipantListingReportDG.CurrentPageIndex*ParticipantListingReportDG.PageSize+1) : 0;
				int nEndPage = (ParticipantListingReportDG.CurrentPageIndex+1)*(ParticipantListingReportDG.PageSize);
				int nEndOfSet = (nEndPage > nCount) ? nCount : nEndPage;
				lblListing.Text = String.Format("Records: {0}-{1} of {2}", nStartOfSet, nEndOfSet ,nCount);
				//ParticipantListingReportDG.PagerStyle.Visible = (nCount <= ParticipantListingReportDG.PageSize) ? false : true;
			}
		}

		public void ParticipantListingReportDG_Page(Object sender, DataGridPageChangedEventArgs e)
		{
			//used by built-in pager.  CurrentPageIndex already set
			ParticipantListingReportDG.CurrentPageIndex = e.NewPageIndex;
			strSearchField = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindSummaryReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void ParticipantListingReportDG_Sort(Object sender, DataGridSortCommandEventArgs e)
		{
			Session["ParticipantListingReportSortField"] = (string)e.SortExpression;
			strSearchField = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindSummaryReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void SearchButtonClick(Object sender, EventArgs e)
		{
			ParticipantListingReportDG.CurrentPageIndex = 0;
			strSearchField  = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindSummaryReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void ParticipantListingReportDG_ItemDataBound(Object sender, DataGridItemEventArgs e)
		{
			ParameterValueCollection parameterValues;
			ParameterValue parameterValue;

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Literal OrderID		= (Literal)e.Item.FindControl("ltOrderID");
				Literal CampaignID	= (Literal)e.Item.FindControl("ltCampaignID");
				Literal Lang		= (Literal)e.Item.FindControl("ltLang");
				RSGenerationLinkButton rsGenerationParticipantListing = (RSGenerationLinkButton) e.Item.FindControl("rsGenerationParticipantListing");
				
				if(rsGenerationParticipantListing != null)
				{
					rsGenerationParticipantListing.Mode = FilePageMode.PopUp;
					rsGenerationParticipantListing.Text = OrderID.Text;

					if(Lang.Text.ToUpper().Trim() == "EN") 
					{
						rsGenerationParticipantListing.ReportName = "ParticipantListing";
					}
					else if(Lang.Text.ToUpper().Trim() == "FR")
					{
						rsGenerationParticipantListing.ReportName = "ParticipantListingFrench";
					}

					parameterValues = new ParameterValueCollection();

					parameterValue = new ParameterValue();
					parameterValue.Name = "OrderID";
					parameterValue.Value = OrderID.Text;
					parameterValues.Add(parameterValue);

					/*parameterValue = new ParameterValue();
					parameterValue.Name = "CampaignID";
					parameterValue.Value = CampaignID.Text;
					parameterValues.Add(parameterValue);*/

					rsGenerationParticipantListing.ParameterValues = parameterValues;
				}	
			}
		}
		#endregion Grid

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
			//this.ParticipantListingReportDG.SelectedIndexChanged += new System.EventHandler(this.ParticipantListingReportDG_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);
		}

		//public void ParticipantListingReportDG_SelectedIndexChanged(object sender, System.EventArgs e){}
		#endregion auto-generated code		
	}
}
