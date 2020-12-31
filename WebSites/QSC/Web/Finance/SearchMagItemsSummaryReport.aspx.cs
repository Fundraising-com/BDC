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
	///<summary>SearchMagItemsSummaryReport</summary>
	public class SearchMagItemsSummaryReport : QSPFulfillment.CommonWeb.QSPPage
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
			//this.MagItemsSummaryReportDG.SelectedIndexChanged += new System.EventHandler(this.MagItemsSummaryReportDG_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);
		}

		//public void MagItemsSummaryReportDG_SelectedIndexChanged(object sender, System.EventArgs e){}
		#endregion auto-generated code
		
		#region constructor
		public SearchMagItemsSummaryReport()
		{
			aMagItemsSumDAL = new DAL.MagItemsSummaryReportDataAccess();
		}
		#endregion constructor
		
		#region Item Declarations
		//private string RSInstallation = System.Configuration.ConfigurationSettings.AppSettings["RSInstallation"];
		protected DAL.InvoiceListPrintDataAccess aInvoicePrintDataAccess;
		protected DAL.MagItemsSummaryReportDataAccess aMagItemsSumDAL;
		protected System.Web.UI.WebControls.Button PrintItems;
		public System.Web.UI.WebControls.DropDownList ddlPrinters;

		//protected DataSet ds  = new DataSet();
		protected DataView dv = new DataView();
		protected TextBox Search;
		protected TextBox FromDate;
		protected TextBox ToDate;
		protected DataGrid MagItemsSummaryReportDG;
		protected Label lblSummary;
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

		protected System.Web.UI.WebControls.DropDownList ddlInvStatus;
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
			dv = ( (DataTable)
					aMagItemsSumDAL.GetMagItemsSummaryReportToPrint(
						Convert.ToDateTime(strFromDate), 
						Convert.ToDateTime(strToDate))
				).DefaultView;
			//dv = dt.DefaultView;

			if(strSearchField.Length > 0)
			{
				if(ddlStatus.SelectedItem.Value.Trim()=="OrderID")
				{
					//dv.RowFilter = "OrderID = " + Regex.Replace(strSearchField,"'","''");
					dv.RowFilter = "OrderID = " + strSearchField.Replace("'","''");
					Session["MagItemSummaryReportSortField"] = "Name, OrderID";
				}
				else if(ddlStatus.SelectedItem.Value.Trim()=="CampaignID")
				{
					//dv.RowFilter = "CampaignID = " + Regex.Replace(strSearchField,"'","''");
					dv.RowFilter = "CampaignID = " + strSearchField.Replace("'","''");
					Session["MagItemSummaryReportSortField"] = "Name, CampaignID";
				}
			}
			else
			{
				if (Session["MagItemSummaryReportSortField"] == null) Session["MagItemSummaryReportSortField"] = "Name, OrderID";
			}

			dv.Sort = (string)Session["MagItemSummaryReportSortField"];
			int totalItems = Convert.ToInt32(dv.Count);
			ShowSummaryStats(totalItems);

			// bind to the Data
			MagItemsSummaryReportDG.DataSource = dv;
			MagItemsSummaryReportDG.DataBind();
		}

		public void ShowSummaryStats(int nCount)
		{
			if (nCount == 0){lblSummary.Text = "No Records Found."; }
			else
			{
				int nStartOfSet = (nCount > 0) ? (MagItemsSummaryReportDG.CurrentPageIndex*MagItemsSummaryReportDG.PageSize+1) : 0;
				int nEndPage = (MagItemsSummaryReportDG.CurrentPageIndex+1)*(MagItemsSummaryReportDG.PageSize);
				int nEndOfSet = (nEndPage > nCount) ? nCount : nEndPage;
				lblSummary.Text = String.Format("Records: {0}-{1} of {2}", nStartOfSet, nEndOfSet ,nCount);
				//MagItemsSummaryReportDG.PagerStyle.Visible = (nCount <= MagItemsSummaryReportDG.PageSize) ? false : true;
			}
		}

		public void MagItemsSummaryReportDG_Page(Object sender, DataGridPageChangedEventArgs e)
		{
			//used by built-in pager.  CurrentPageIndex already set
			MagItemsSummaryReportDG.CurrentPageIndex = e.NewPageIndex;
			strSearchField = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindSummaryReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void MagItemsSummaryReportDG_Sort(Object sender, DataGridSortCommandEventArgs e)
		{
			Session["MagItemSummaryReportSortField"] = (string)e.SortExpression;
			strSearchField = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindSummaryReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void SearchButtonClick(Object sender, EventArgs e)
		{
			MagItemsSummaryReportDG.CurrentPageIndex = 0;
			strSearchField  = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindSummaryReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void MagItemsSummaryReportDG_ItemDataBound(Object sender, DataGridItemEventArgs e)
		{
			ParameterValueCollection parameterValues;
			ParameterValue parameterValue;

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Literal OrderID		= (Literal)e.Item.FindControl("ltOrderID");
				Literal CampaignID	= (Literal)e.Item.FindControl("ltCampaignID");
				Literal Lang		= (Literal)e.Item.FindControl("ltLang");
				RSGenerationLinkButton rsGenerationMagazineItemsSummary = (RSGenerationLinkButton) e.Item.FindControl("rsGenerationMagazineItemsSummary");
				
				if(rsGenerationMagazineItemsSummary != null)
				{
					rsGenerationMagazineItemsSummary.Mode = FilePageMode.PopUp;
					rsGenerationMagazineItemsSummary.Text = OrderID.Text;

					if(Lang.Text.ToUpper().Trim() == "EN") 
					{
						rsGenerationMagazineItemsSummary.ReportName = "MagazineItemsSummary";
					} 
					else if(Lang.Text.ToUpper().Trim() == "FR")
					{
						rsGenerationMagazineItemsSummary.ReportName = "MagazineItemsSummaryFrench";
					}

					parameterValues = new ParameterValueCollection();
					
					parameterValue = new ParameterValue();
					parameterValue.Name = "OrderID";
					parameterValue.Value = OrderID.Text;
					parameterValues.Add(parameterValue);

					parameterValue = new ParameterValue();
					parameterValue.Name = "CampaignID";
					parameterValue.Value = CampaignID.Text;
					parameterValues.Add(parameterValue);
					
					rsGenerationMagazineItemsSummary.ParameterValues = parameterValues;
				}
			}
		}
		#endregion Grid
	}
}
