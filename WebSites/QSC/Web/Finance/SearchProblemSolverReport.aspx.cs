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
	///<summary>SearchProblemSolverReport</summary>
	public class SearchProblemSolverReport : QSPFulfillment.CommonWeb.QSPPage
	{
		#region constructor
		public SearchProblemSolverReport()
		{
			aProblemSolverReportDataAccess = new DAL.ProblemSolverReportDataAccess();
		}
		#endregion constructor
		
		#region Item Declarations
		//private string RSInstallation = System.Configuration.ConfigurationSettings.AppSettings["RSInstallation"];
		protected DAL.ProblemSolverReportDataAccess aProblemSolverReportDataAccess;
		//protected DataSet ds  = new DataSet();
		protected DataView dv = new DataView();
		protected TextBox Search;
		protected TextBox FromDate;
		protected TextBox ToDate;
		protected DataGrid ProblemSolverReportDG;
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
				BindProblemSolverReportGrid(strSearchField, strStartDate, strEndDate);
			}
		}
		#endregion Page Load

		#region Grid
		public void BindProblemSolverReportGrid(string strSearchField, string strFromDate, string strToDate)
		{
			LabelMsg.Text = "";
			//Get all OrderID's by date range
			//ds = aProblemSolverReportDataAccess.GetProblemSolverReportToPrint(Convert.ToDateTime(strFromDate), Convert.ToDateTime(strToDate));
			//dv = ds.Tables[0].DefaultView;
			dv = ( (DataTable)
					aProblemSolverReportDataAccess.GetProblemSolverReportToPrint(
						Convert.ToDateTime(strFromDate), 
						Convert.ToDateTime(strToDate))
				).DefaultView;

			if(strSearchField.Length > 0)
			{
				if(ddlStatus.SelectedItem.Value.Trim()=="OrderID")
				{
					//dv.RowFilter = "OrderID = " + Regex.Replace(strSearchField,"'","''");
					dv.RowFilter = "OrderID = " + strSearchField.Replace("'","''");
					Session["ProblemSolverReportSortField"] = "Name, OrderID";
				}
				else if(ddlStatus.SelectedItem.Value.Trim()=="CampaignID")
				{
					//dv.RowFilter = "CampaignID = " + Regex.Replace(strSearchField,"'","''");
					dv.RowFilter = "CampaignID = " + strSearchField.Replace("'","''");
					Session["ProblemSolverReportSortField"] = "Name, CampaignID";
				}
			}
			else
			{
				if (Session["ProblemSolverReportSortField"] == null) Session["ProblemSolverReportSortField"] = "Name, OrderID";
			}

			dv.Sort = (string)Session["ProblemSolverReportSortField"];
			int totalItems = Convert.ToInt32(dv.Count);
			ShowProblemSolverStats(totalItems);

			// bind to the Data
			ProblemSolverReportDG.DataSource = dv;
			ProblemSolverReportDG.DataBind();
		}

		public void ShowProblemSolverStats(int nCount)
		{
			if (nCount == 0){lblListing.Text = "No Records Found."; }
			else
			{
				int nStartOfSet = (nCount > 0) ? (ProblemSolverReportDG.CurrentPageIndex*ProblemSolverReportDG.PageSize+1) : 0;
				int nEndPage = (ProblemSolverReportDG.CurrentPageIndex+1)*(ProblemSolverReportDG.PageSize);
				int nEndOfSet = (nEndPage > nCount) ? nCount : nEndPage;
				lblListing.Text = String.Format("Records: {0}-{1} of {2}", nStartOfSet, nEndOfSet ,nCount);
				//ProblemSolverReportDG.PagerStyle.Visible = (nCount <= ProblemSolverReportDG.PageSize) ? false : true;
			}
		}

		public void ProblemSolverReportDG_Page(Object sender, DataGridPageChangedEventArgs e)
		{
			//used by built-in pager.  CurrentPageIndex already set
			ProblemSolverReportDG.CurrentPageIndex = e.NewPageIndex;
			strSearchField = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindProblemSolverReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void ProblemSolverReportDG_Sort(Object sender, DataGridSortCommandEventArgs e)
		{
			Session["ProblemSolverReportSortField"] = (string)e.SortExpression;
			strSearchField = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindProblemSolverReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void SearchButtonClick(Object sender, EventArgs e)
		{
			ProblemSolverReportDG.CurrentPageIndex = 0;
			strSearchField  = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindProblemSolverReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void ProblemSolverReportDG_ItemDataBound(Object sender, DataGridItemEventArgs e)
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
			//this.ProblemSolverReportDG.SelectedIndexChanged += new System.EventHandler(this.ProblemSolverReportDG_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);
		}

		//public void ProblemSolverReportDG_SelectedIndexChanged(object sender, System.EventArgs e){}
		#endregion auto-generated code
	}
}
