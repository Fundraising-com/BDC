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
using QSPFulfillment.DataAccess.Common;
using QSPFulfillment.DataAccess.Business;
using Business.Objects;
using Common.TableDef;

namespace QSPFulfillment.Finance
{
	///<summary>StatementReportPrintByCampaign</summary>
	public class PrintGroupStatements : QSPFulfillment.CommonWeb.QSPPage
	{
		private const string PRINT_LIST_REPORT_PAGE = "StatementPrintList.aspx";
		//protected System.Web.UI.WebControls.Label lblPrinters;

		//protected string RSInstallation = System.Configuration.ConfigurationSettings.AppSettings["RSInstallation"];
		//MSprotected DAL.InvoiceListPrintDataAccess aInvoicePrintDataAccess;
		protected DAL.StatementReportByCampaignPrintDataAccess aStatementReportByCampaignPrintDataAccess;
		//protected DAL.StatementData aStatementReportByCampaignPrintDataAccess;
		protected System.Web.UI.WebControls.Button PrintItems;
		protected System.Web.UI.WebControls.DropDownList ddlPrinters;
		protected System.Web.UI.WebControls.RequiredFieldValidator Req8;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegExp8;
		protected System.Web.UI.WebControls.RequiredFieldValidator Req9;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegExp9;
		protected PrintCampaignStatements aPrintCampaignStatement;
		protected System.Web.UI.WebControls.DropDownList ddlFM;

		//protected DataSet ds  = new DataSet();
		protected DataView dv = new DataView();
		protected TextBox Search;
		protected TextBox FromDate;
		protected TextBox ToDate;
		protected DataGrid CampaignStatementReportDG;
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
		//public string m_reportName  = "/QSPCanadaFinance/StatementReportByCampaign";
		public string m_reportName  = "/QSP CA Systems/QSPCanadaFinance/StatementReportByCampaign";
		//Param Name
		public string m_paramName = "AccountID";
		public string m_paramName2= "StartDate";
		public string m_paramName3= "EndDate";
		public string m_paramName4= "CampaignID";

		private void Page_Load(object sender, System.EventArgs e)
		{
			AddJavaScript();

			if (!IsPostBack)
			{
				LoadDataDDLSearchBy();
				LoadDataDDLFieldManager() ;
				//ddlPrinters.DataBind();
				

				SetFMSearch();
			
				if(Request.QueryString["Search"] != null)
				{
					strSearchField = Request.QueryString["Search"];
				}

				strSearchField=Search.Text;
				// go back 60 days
				TimeSpan ts = new System.TimeSpan(-60, 0, 0, 0);
				DateTime aToDate = DateTime.Now.Add(ts);
				strStartDate = aToDate.ToString("dd-MMM-yyyy").Trim();
				strEndDate   = DateTime.Now.ToString("dd-MMM-yyyy").Trim();

				//Textboxes
				FromDate.Text = strStartDate;
				ToDate.Text   = strEndDate;

				//Printers
				//aPrintCampaignStatement.GetPrinterList(ddl);

				//Bind the invoice datagrid
				// in pre render
				BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
			}
			/*else
			{
				strStartDate = this.FromDate.Text;
				strEndDate   = this.ToDate.Text;

				SetFMSearch();
			
				if(Request.QueryString["Search"] != null)
				{
					strSearchField = Request.QueryString["Search"];
				}

				strSearchField=Search.Text;

				BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
			}*/
		}

		/*private void PrintGroupStatements_PreRender(object sender, EventArgs e)
		{
			strStartDate = this.FromDate.Text;
			strEndDate   = this.ToDate.Text;

			SetFMSearch();
			
			if(Request.QueryString["Search"] != null)
			{
				strSearchField = Request.QueryString["Search"];
			}

			Search.Text = strSearchField;

			BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
			//if(!IsPostBack) 
			//{
			//	this.ctrlCampaignMaintenanceControl.CampaignID = this.CampaignID;
			//	this.ctrlCampaignMaintenanceControl.AccountID = this.AccountID;
			//	this.ctrlCampaignMaintenanceControl.DataBind();
			//} 
			//else if(this.hidDataBind.Value == "1") 
			//{
			//	this.ctrlCampaignMaintenanceControl.DataBind();
			//	this.hidDataBind.Value = "0";
			//}

			//this.onload_script += "; window_onunload();";
		}*/

		public void SetFMSearch()
		{
			
			if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM && 
				QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID == "9999" && this.ddlStatus.SelectedValue=="FM"	)
					{
						this.Search.Visible = false;
						this.ddlFM.Visible=true;		 
					}
			else if(QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM && 
						QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID == "9999" &&	this.ddlStatus.SelectedValue !="FM"	)
					{
						this.Search.Visible = true;
						this.ddlFM.Visible=false;		 
					}
			else
					{this.ddlFM.Visible=false;}
 
					
		}

		private void LoadDataDDLFieldManager() 
		{
			string country="CA";
			FieldManager fm = new FieldManager();
			fm.GetAllByCountryCode(country);

			this.ddlFM.DataSource = fm.dataSet;
			this.ddlFM.DataMember = fm.dataSet.FieldManager.TableName;
			this.ddlFM.DataTextField = fm.dataSet.FieldManager.ListNameColumn.ColumnName;
			this.ddlFM.DataValueField = fm.dataSet.FieldManager.FMIDColumn.ColumnName;

			this.ddlFM.DataBind();
			
		}

		private void LoadDataDDLSearchBy()
		{
			if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM && 
				QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID == "9999" )

			{ddlStatus.Items.Clear();
			
			ddlStatus.Items.Insert(0, new ListItem("Acct ID", "AcctID"));
			ddlStatus.Items.Insert(1, new ListItem("Camp ID", "CampID"));
            ddlStatus.Items.Insert(2, new ListItem("Acct Name", "AcctName"));
			ddlStatus.Items.Insert(3, new ListItem("FM", "FM"));}
			else
			{
			ddlStatus.Items.Clear();
			ddlStatus.Items.Insert(0, new ListItem("Acct ID", "AcctID"));
			ddlStatus.Items.Insert(1, new ListItem("Camp ID", "CampID"));
			ddlStatus.Items.Insert(2, new ListItem("Acct Name", "AcctName"));}

		}

		//NN
		//private DAL.InvoiceListPrintDataAccess InvoicePrintDataAccess 
		//{
		//	get 
		//	{
		//		if(aInvoicePrintDataAccess == null) 
		//		{
		//			aInvoicePrintDataAccess = new DAL.InvoiceListPrintDataAccess();
		//		}

		//		return aInvoicePrintDataAccess;
		//	}
		//}

		//private DAL.StatementData StatementReportByCampaignPrintDataAccess
		private DAL.StatementReportByCampaignPrintDataAccess StatementReportByCampaignPrintDataAccess
		{
			get 
			{
				if(aStatementReportByCampaignPrintDataAccess == null) 
				{
					//aStatementReportByCampaignPrintDataAccess = new DAL.StatementData();
					aStatementReportByCampaignPrintDataAccess = new DAL.StatementReportByCampaignPrintDataAccess();
				}

				return aStatementReportByCampaignPrintDataAccess;
			}
		}

		private PrintCampaignStatements PrintCampaignStatement
		{
			get 
			{
				if(aPrintCampaignStatement == null) 
				{
					aPrintCampaignStatement = new PrintCampaignStatements();
				}

				return aPrintCampaignStatement;
			}
		}

		#region GetPrinterList
			public DataTable GetPrinterList()
			{
				return null;
				//return InvoicePrintDataAccess.GetPrinterNames();
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

		private void AddJavaScript() 
		{
			PrintItems.Attributes.Add("onclick", "return confirmPrint (this.form);");
		}

		#region STATEMENT
		public void BindStatementReportGrid(string strSearchField, string strFromDate, string strToDate)
		{
			
			LabelMsg.Text = "";
			//Get all statements by date range
			//ds = aStatementReportByCampaignPrintDataAccess.GetAllStatementsByCampaignToPrint(strFromDate, strToDate);
			//ds = aStatementReportByCampaignPrintDataAccess.SelectALLCAStatementToPrint(ds,dt,Convert.ToDateTime(strFromDate), Convert.ToDateTime(strToDate));
			//dv = ds.Tables[0].DefaultView;
			dv = ( (DataTable)
					StatementReportByCampaignPrintDataAccess.GetAllStatementsByCampaignToPrint(
						Convert.ToDateTime(strFromDate), 
						Convert.ToDateTime(strToDate))
				).DefaultView;

			if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM && 
				QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID != "9999" )
			{
				dv.RowFilter = "FMID ="	+QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID;	 
			}
			else
			{
				if(ddlStatus.SelectedItem.Value.Trim()=="FM")
				{
					strSearchField= this.ddlFM.SelectedValue;
				}

			}
			
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
				else if(ddlStatus.SelectedItem.Value.Trim()=="CampID")
				{
					//dv.RowFilter = "CampaignID = " + Regex.Replace(strSearchField,"'","''");
					dv.RowFilter = "CampaignID = " + strSearchField.Replace("'","''");
					Session["StatementSortField"] = "CampaignID, Name";
				}
				else if(ddlStatus.SelectedItem.Value.Trim()=="FM")
				{
					//dv.RowFilter = "FMID LIKE '" + Regex.Replace(strSearchField,"'","''") + "%'";
					//dv.RowFilter = "FMID LIKE '" + strSearchField.Replace("'","''") + "%'";
					dv.RowFilter = "FMID ="	+this.ddlFM.SelectedValue;	
					Session["StatementSortField"] = "FMID, Name";
				}
				/*else if(ddlStatus.SelectedItem.Value.Trim()=="LastName")
				{
					//dv.RowFilter = "LastName LIKE '" + Regex.Replace(strSearchField,"'","''") + "%'";
					dv.RowFilter = "LastName LIKE '" + strSearchField.Replace("'","''") + "%'";
					Session["StatementSortField"] = "LastName, FirstName";
				}*/
			}
			else
			{
				if (Session["StatementSortField"] == null) Session["StatementSortField"] = "Name, AccountID";
			}
			
			dv.Sort = (string)Session["StatementSortField"];
			int totalStatements = Convert.ToInt32(dv.Count);
			ShowStatementsStats(totalStatements);

			// bind to the Data
			CampaignStatementReportDG.DataSource = dv;
			CampaignStatementReportDG.DataBind();
		}

		public void ShowStatementsStats(int nCount)
		{
			if (nCount == 0){lblStatement.Text = "No Records Found."; }
			else
			{
				int nStartOfSet = (nCount > 0) ? (CampaignStatementReportDG.CurrentPageIndex*CampaignStatementReportDG.PageSize+1) : 0;
				int nEndPage = (CampaignStatementReportDG.CurrentPageIndex+1)*(CampaignStatementReportDG.PageSize);
				int nEndOfSet = (nEndPage > nCount) ? nCount : nEndPage;
				lblStatement.Text = String.Format("Records: {0}-{1} of {2}", nStartOfSet, nEndOfSet ,nCount);
			}
		}

		public void CampaignStatementReportDG_Page(Object sender, DataGridPageChangedEventArgs e)
		{
			//used by built-in pager.  CurrentPageIndex already set
			CampaignStatementReportDG.CurrentPageIndex = e.NewPageIndex;
			strSearchField = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void CampaignStatementReportDG_Sort(Object sender, DataGridSortCommandEventArgs e)
		{
			Session["StatementSortField"] = (string)e.SortExpression;
			strSearchField = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void SearchButtonClick(Object sender, EventArgs e)
		{
			CampaignStatementReportDG.CurrentPageIndex = 0;
			strSearchField  = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
		}

		public void CampaignStatementReportDG_ItemDataBound(Object sender, DataGridItemEventArgs e)
		{
			ParameterValueCollection parameterValues;
			ParameterValue parameterValue;

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label lblAccountID	= (Label)e.Item.FindControl("AccountID");
				Label lblCampaignID	= (Label)e.Item.FindControl("lblCampaignID");
				Label lblLang		= (Label)e.Item.FindControl("Lang");
				RSGenerationLinkButton rsGenerationStatementReportByCampaign = (RSGenerationLinkButton) e.Item.FindControl("rsGenerationStatementReportByCampaign");

				if(rsGenerationStatementReportByCampaign != null) 
				{
					rsGenerationStatementReportByCampaign.Mode = FilePageMode.PopUp;
					rsGenerationStatementReportByCampaign.Text = lblAccountID.Text;

					if(lblLang.Text.ToUpper().Trim() == "EN") 
					{
						rsGenerationStatementReportByCampaign.ReportName = "StatementReportByCampaign";
					}
					else if(lblLang.Text.ToUpper().Trim() == "FR")
					{
						rsGenerationStatementReportByCampaign.ReportName = "StatementReportByCampaignFrench";
					}

					parameterValues = new ParameterValueCollection();

					parameterValue = new ParameterValue();
					parameterValue.Name = "AccountID";
					parameterValue.Value = lblAccountID.Text.Trim();
					parameterValues.Add(parameterValue);

					parameterValue = new ParameterValue();
					parameterValue.Name = "CampaignID";
					parameterValue.Value = lblCampaignID.Text.Trim();
					parameterValues.Add(parameterValue);

					parameterValue = new ParameterValue();
					parameterValue.Name = "StartDate";
					parameterValue.Value = strStartDate;
					parameterValues.Add(parameterValue);

					parameterValue = new ParameterValue();
					parameterValue.Name = "EndDate";
					parameterValue.Value = strEndDate;
					parameterValues.Add(parameterValue);

					rsGenerationStatementReportByCampaign.ParameterValues = parameterValues;
				}
			}
		}

		public void DoPrint(Object sender, EventArgs e)
		{
			/*bool isChecked = false;
			ArrayList array = new ArrayList();
			ArrayList array2 = new ArrayList();
			int intCounter = 0;
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();

			foreach (DataGridItem i in CampaignStatementReportDG.Items)
			{
				CheckBox printItem = (CheckBox)i.FindControl("PrintThis");
				if (printItem.Checked)
				{
					array.Add (((Label)i.FindControl("AccountID")).Text.ToString());
					array2.Add (((Label)i.FindControl("CampaignID")).Text.ToString());
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
						PrintCampaignStatement.PrintCampaignStatement(m_printerName, m_reportName, m_paramName, array[x].ToString(),
							m_paramName2, strStartDate,
							m_paramName3, strEndDate,
							m_paramName4, array2[x].ToString());
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
			}*/
		}

		public void changePrinted(Object sender, EventArgs e)
		{
			CampaignStatementReportDG.CurrentPageIndex = 0;
			strSearchField  = (string)Search.Text.Trim();
			strStartDate    = (string)FromDate.Text.Trim();
			strEndDate      = (string)ToDate.Text.Trim();
			BindStatementReportGrid(strSearchField, strStartDate, strEndDate);
		}

		#endregion STATEMENT


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
			this.ddlStatus.SelectedIndexChanged += new System.EventHandler(this.ddlStatus_SelectedIndexChanged);
			this.PrintItems.Click += new System.EventHandler(this.PrintItems_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ddlStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetFMSearch();
		}

				
		public CheckBox GetThisPrint(DataGridItem item) 
		{
			return (CheckBox) item.FindControl("PrintThis");
		}

		public int GetCAID(DataGridItem item) 
		{
			return Convert.ToInt32(((Label) item.FindControl("lblCampaignID")).Text);
		}

		public int GetAcctID(DataGridItem item) 
		{
			return Convert.ToInt32(( (Label) item.FindControl("AccountID") ).Text  );
		}

		/*public string GetFManagerID(DataGridItem item) 
		{
			return Convert.ToString(( (Label) item.FindControl("FMID") ).Text  );
			
		}*/
		
		public void PrintItems_Click(object sender, System.EventArgs e)
		{
			string script;
			bool isChecked = false;
			int intCounter = 0;

			GroupStatementBatchReport = new StatementBatchReport();
			
			foreach(DataGridItem item in this.CampaignStatementReportDG.Items)
			{	
				if(GetThisPrint(item).Checked) 
				{
					GroupStatementBatchReport.dataSet.Campaign.AddCampaignRow( 37002,false,"CA","","01/01/1995", 
						"EN",Convert.ToDateTime(this.ToDate.Text),Convert.ToDateTime(this.FromDate.Text),0,
						GetAcctID(item),0,0,Convert.ToDecimal(0),0,0,0,0,0,0,Convert.ToDateTime("01/01/1995"),
						null,false,Convert.ToDecimal(0),false,Convert.ToDateTime("01/01/1995"),0,false,0,false,
						false,false,Convert.ToDateTime("01/01/1995"),Convert.ToDateTime("01/01/1995"),false,false,
						false,null,0,0,Convert.ToDateTime("01/01/1995"),0,0,false).ID = this.GetCAID(item);

					intCounter++;
					isChecked = true;
		        
				}
			}

			script  = "<script language=\"javascript\">\n";
			script += "  window.open(\"" + PRINT_LIST_REPORT_PAGE + "?IsNewWindow=true&PrintChecked=true\",'',\"toolbar = no,status=no,scrollbars=yes,resizable=yes, width=800, height=550\");\n";
			script += "</script>\n";

			this.RegisterStartupScript("PrintReport", script);


				//Refresh data
				strSearchField  = (string)Search.Text.Trim();
				strStartDate    = (string)FromDate.Text.Trim();
				strEndDate      = (string)ToDate.Text.Trim();
				BindStatementReportGrid(strSearchField, strStartDate, strEndDate);

				//if(intCounter > 1)
				//	LabelMsg.Text = "<u>" + intCounter + "</u><font color=green> Statements Printed</font>";
				//else
				//{	
					LabelMsg.Text = "<u>" +  "</u><font color=green> Statements Printed</font>";
				//}
				
			
			}

		private StatementBatchReport GroupStatementBatchReport 
		{
			get 
			{
				if(Session["GroupStatementBatchReport"] == null) 
				{
					Session["GroupStatementBatchReport"] = new StatementBatchReport();
				}

				return (StatementBatchReport) Session["GroupStatementBatchReport"];
			}
			set 
			{
				Session["GroupStatementBatchReport"] = value;
			}
		}

		
	}
}
