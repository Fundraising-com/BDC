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
using System.Data.SqlClient;
using QSPFulfillment.CommonWeb;
//using System.Text.RegularExpressions;
//using System.Configuration;
//using DAL;


namespace QSPFulfillment.Finance
{
	///<summary>StatementReportPrint</summary>

	public class StatementReportPrint : QSPFulfillment.CommonWeb.QSPPage
	{
		//protected string RSInstallation = System.Configuration.ConfigurationSettings.AppSettings["RSInstallation"];
		protected DAL.InvoiceListPrintDataAccess aInvoicePrintDataAccess;
		protected DAL.StatementReportPrintDataAccess aStatementReportPrintDataAccess;
		protected System.Web.UI.WebControls.Button PrintItems;
		protected System.Web.UI.WebControls.DropDownList ddlPrinters;
		protected System.Web.UI.WebControls.RequiredFieldValidator Req8;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegExp8;
		protected System.Web.UI.WebControls.RequiredFieldValidator Req9;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegExp9;
		protected PrintStatements aPrintStatement;

		public StatementReportPrint()
		{
			aInvoicePrintDataAccess = new DAL.InvoiceListPrintDataAccess();
			aStatementReportPrintDataAccess = new DAL.StatementReportPrintDataAccess();
			aPrintStatement 	= new  PrintStatements();
		}

		//protected DataSet ds  = new DataSet();
		protected DataTable dt = new DataTable();
		protected DataView dv = new DataView();
		protected TextBox Search;
		protected TextBox FromDate;
		protected TextBox ToDate;
		protected DataGrid StatementReportDG;
		protected Label lblStatement;
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
		//Printer Name
		public string m_printerName;
		//Report Name
		public string m_reportName  = "/QSPCanadaFinance/StatementReport";
		//Param Name
		public string m_paramName = "AccountID";
		public string m_paramName2= "StartDate";
		public string m_paramName3= "EndDate";

		private void Page_Load(object sender, System.EventArgs e)
		{
			WebControl button = (WebControl) Page.FindControl("PrintItems");
			button.Attributes.Add ("onclick", "return confirmPrint (this.form);");
			ddlPrinters.DataBind();

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

				//Printers
				//aPrintStatement.GetPrinterList(ddl);

				//Bind the invoice datagrid
				BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
			}
		}

		#region GetPrinterList
		public DataTable GetPrinterList()
		{
			return aInvoicePrintDataAccess.GetPrinterNames();
			//DataSet ds = null;
			//ds = aInvoicePrintDataAccess.GetPrinterNames();
			//return ds.Tables[0];

			/*
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
				*/
		}
		#endregion

		#region INVOICE
		public void BindStatementReportGrid(string strSearchField, string strFromDate, string strToDate)
		{
			LabelMsg.Text = "";
			//Get all statements by date range
			//ds = aStatementReportPrintDataAccess.GetAllStatementsToPrint(strFromDate, strToDate);
			//dv = ds.Tables[0].DefaultView;
			dv = ( (DataTable)
					aStatementReportPrintDataAccess.GetAllStatementsToPrint(
						Convert.ToDateTime(strFromDate), 
						Convert.ToDateTime(strToDate))
				).DefaultView;



			if(strSearchField.Length > 0)
			{
				if(ddlStatus.SelectedItem.Value.Trim()=="AcctName")
				{
					//dv.RowFilter = "Name LIKE '" + Regex.Replace(strSearchField,"'","''") + "%'";
					dv.RowFilter = "Name LIKE '" + strSearchField.Replace("'","''") + "%'";
					Session["StatementSortField"] = "Name, AccountID";
				}
				else if(ddlStatus.SelectedItem.Value.Trim()=="AcctID")
				{
					//dv.RowFilter = "AccountID = " + Regex.Replace(strSearchField,"'","''");
					dv.RowFilter = "AccountID = " + strSearchField.Replace("'","''");
					Session["StatementSortField"] = "AccountID, Name";
				}
				else if(ddlStatus.SelectedItem.Value.Trim()=="FMID")
				{
					//dv.RowFilter = "FMID LIKE '" + Regex.Replace(strSearchField,"'","''") + "%'";
					dv.RowFilter = "FMID LIKE '" + strSearchField.Replace("'","''") + "%'";
					Session["StatementSortField"] = "FMID, Name";
				}
				else if(ddlStatus.SelectedItem.Value.Trim()=="LastName")
				{
					//dv.RowFilter = "LastName LIKE '" + Regex.Replace(strSearchField,"'","''") + "%'";
					dv.RowFilter = "LastName LIKE '" + strSearchField.Replace("'","''") + "%'";
					Session["StatementSortField"] = "LastName, FirstName";
				}
			}
			else
			{
				if (Session["StatementSortField"] == null) Session["StatementSortField"] = "Name, AccountID";
			}

			dv.Sort = (string)Session["StatementSortField"];
			int totalStatements = Convert.ToInt32(dv.Count);
			ShowStatementsStats(totalStatements);

			// bind to the Data
			StatementReportDG.DataSource = dv;
			StatementReportDG.DataBind();
		}

		public void ShowStatementsStats(int nCount)
		{
			if (nCount == 0){lblStatement.Text = "No Records Found."; }
			else
			{
				int nStartOfSet = (nCount > 0) ? (StatementReportDG.CurrentPageIndex*StatementReportDG.PageSize+1) : 0;
				int nEndPage = (StatementReportDG.CurrentPageIndex+1)*(StatementReportDG.PageSize);
				int nEndOfSet = (nEndPage > nCount) ? nCount : nEndPage;
				lblStatement.Text = String.Format("Records: {0}-{1} of {2}", nStartOfSet, nEndOfSet ,nCount);
				//StatementReportDG.PagerStyle.Visible = (nCount <= StatementReportDG.PageSize) ? false : true;
			}
		}

		public void StatementReportDG_Page(Object sender, DataGridPageChangedEventArgs e)
		{
			//used by built-in pager.  CurrentPageIndex already set
			StatementReportDG.CurrentPageIndex = e.NewPageIndex;
			strSearchField = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void StatementReportDG_Sort(Object sender, DataGridSortCommandEventArgs e)
		{
			Session["StatementSortField"] = (string)e.SortExpression;
			strSearchField = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void SearchButtonClick(Object sender, EventArgs e)
		{
			StatementReportDG.CurrentPageIndex = 0;
			strSearchField  = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void StatementReportDG_ItemDataBound(Object sender, DataGridItemEventArgs e)
		{
			ParameterValueCollection parameterValues;
			ParameterValue parameterValue;

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label lblAccountID	= (Label)e.Item.FindControl("AccountID");
				Label lblLang		= (Label)e.Item.FindControl("Lang");
				RSGenerationLinkButton rsGenerationStatementReport = (RSGenerationLinkButton) e.Item.FindControl("rsGenerationStatementReport");

				if(rsGenerationStatementReport != null) 
				{
					rsGenerationStatementReport.Mode = FilePageMode.PopUp;
					rsGenerationStatementReport.Text = lblAccountID.Text;

					if(lblLang.Text.ToUpper().Trim() == "EN") 
					{
						rsGenerationStatementReport.ReportName = "StatementReport";
					}
					else if(lblLang.Text.ToUpper().Trim() == "FR")
					{
						rsGenerationStatementReport.ReportName = "StatementReportFrench";
					}

					parameterValues = new ParameterValueCollection();
					
					parameterValue = new ParameterValue();
					parameterValue.Name = "AccountID";
					parameterValue.Value = lblAccountID.Text.Trim();
					parameterValues.Add(parameterValue);

					parameterValue = new ParameterValue();
					parameterValue.Name = "StartDate";
					parameterValue.Value = strStartDate;
					parameterValues.Add(parameterValue);

					parameterValue = new ParameterValue();
					parameterValue.Name = "EndDate";
					parameterValue.Value = strEndDate;
					parameterValues.Add(parameterValue);

					rsGenerationStatementReport.ParameterValues = parameterValues;
				}
			}
		}
		public void DoPrint(Object sender, EventArgs e)
		{
			bool isChecked = false;
			ArrayList array = new ArrayList();
			int intCounter = 0;
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();

			foreach (DataGridItem i in StatementReportDG.Items)
			{
				CheckBox printItem = (CheckBox)i.FindControl("PrintThis");
				if (printItem.Checked)
				{
					array.Add (((Label)i.FindControl("AccountID")).Text.ToString());
					intCounter++;
					isChecked = true;
				}
			}
			if (isChecked == true)
			{
				try
				{
					//run the print routine; get the printer name
					m_printerName = ddlPrinters.SelectedItem.Text.ToString();
					for (int x=0; x<array.Count; x++)
					{
						aPrintStatement.PrintStatement(m_printerName, m_reportName, m_paramName, array[x].ToString(),
														m_paramName2, strStartDate,
														m_paramName3, strEndDate);
					}
				}
				catch (SqlException exc)
				{ throw exc;}

				//Refresh data
				strSearchField  = (string)Search.Text.Trim();
				strStartDate    = (string)FromDate.Text.Trim();
				strEndDate      = (string)ToDate.Text.Trim();
				BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
				if(intCounter > 1)
					LabelMsg.Text = "<u>" + intCounter + "</u><font color=green> Statements Printed</font>";
				else
					LabelMsg.Text = "<u>" + intCounter + "</u><font color=green> Statement Printed</font>";

			}
			else
			{
				LabelMsg.Text = "No Records Selected";
			}
		}

		public void changePrinted(Object sender, EventArgs e)
		{
			StatementReportDG.CurrentPageIndex = 0;
			strSearchField  = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
		}

		#endregion INVOICE


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
			this.StatementReportDG.SelectedIndexChanged += new System.EventHandler(this.StatementReportDG_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		public void StatementReportDG_SelectedIndexChanged(object sender, System.EventArgs e)
		{

		}
	}
}
