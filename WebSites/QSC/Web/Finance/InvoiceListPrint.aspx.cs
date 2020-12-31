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
using Business.Objects;
using Common.TableDef;
using QSPFulfillment.CommonWeb;
//using System.Text.RegularExpressions;
//using System.Configuration;
//using DAL;


namespace QSPFulfillment.Finance
{
	///<summary>InvoiceListPrint</summary>
	public class InvoiceListPrint : QSPFulfillment.CommonWeb.QSPPage
	{
		private const string PRINT_LIST_REPORT_PAGE = "InvoicePrintWholeList.aspx";

        private const int SELECTEDCHECKBOX_COLUMN = 0;
		private const int PRINTED_COLUMN = 13;

		public InvoiceListPrint()
		{
			aInvoicePrintDataAccess = new DAL.InvoiceListPrintDataAccess();
		}
		
		#region Item Declarations
		//private string RSInstallation = System.Configuration.ConfigurationSettings.AppSettings["RSInstallation"];
		protected DAL.InvoiceListPrintDataAccess aInvoicePrintDataAccess;
		protected System.Web.UI.WebControls.Button PrintItems;
		//protected System.Web.UI.WebControls.DropDownList ddlPrinters;
		protected System.Web.UI.WebControls.RequiredFieldValidator Req8;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegExp8;
		protected System.Web.UI.WebControls.RequiredFieldValidator Req9;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegExp9;

		//protected DataSet ds  = new DataSet();
		protected DataTable dt = new DataTable();
		protected DataView dv = new DataView();
		protected TextBox Search;
		protected DataGrid InvoiceListDG;
		protected Label lblInvoice;
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

        protected System.Web.UI.WebControls.DropDownList ddlShowInvoiceType;
        protected System.Web.UI.WebControls.DropDownList ddlFiscalYear;
		protected System.Web.UI.WebControls.DropDownList ddlInvStatus;
		protected System.Web.UI.WebControls.LinkButton BtnSearch;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnPrintList;
		protected System.Web.UI.WebControls.Button UnprintItems;
		protected System.Web.UI.WebControls.Button btnMarkWholeListPrinted;
		protected System.Web.UI.WebControls.Button btnMarkWholeListUnprinted;
		protected System.Web.UI.WebControls.Button btnPrintChecked;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdPrintItems;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdUnprintItems;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdPrintList;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdUnprintList;
        protected System.Web.UI.HtmlControls.HtmlTableCell tdShowInvoiceType;
        protected System.Web.UI.HtmlControls.HtmlTable tblPrintButtons;
		protected System.Web.UI.WebControls.Label lblPrinted;
        protected System.Web.UI.WebControls.CheckBox chkIncludeOEFUReport;
        protected System.Web.UI.WebControls.CheckBox chkShowNonPrinted;
        //Printer Name
		public string m_printerName;
		//Report Name
		//public string m_reportName			= "PrintInvoice";
		//public string m_reportNameFrench	= "PrintInvoiceFrench";
		//Param Name
		//public string m_paramName = "InvoiceID";
		#endregion Item Declarations

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Request.QueryString["Search"] != null)
			{
				strSearchField = Request.QueryString["Search"];
			}

			if (!IsPostBack)
			{
                this.BindFiscalYearDDL();

				SetFMView();

				Search.Text = strSearchField;

				SetPrintedValue();
                BindInvoiceGrid(strSearchField, this.CurrentFiscalYear, FMID);
			}

			AddJavaScript();
		}

		private void btnPrintChecked_Click(object sender, System.EventArgs e)
		{
			string script;
			FillCheckedItems();

			script  = "<script language=\"javascript\">\n";
			script += "  window.open(\"" + PRINT_LIST_REPORT_PAGE + "?IsNewWindow=true&PrintChecked=true\",'',\"toolbar = no,status=no,scrollbars=yes,resizable=yes, width=800, height=550\");\n";
			script += "</script>\n";

			this.RegisterStartupScript("PrintReport", script);
		}

		private InvoiceBatchReport InvoiceReport 
		{
			get 
			{
				if(Session["InvoiceReport"] == null) 
				{
                    Session["InvoiceReport"] = new InvoiceBatchReport(this.chkIncludeOEFUReport.Checked);
				}

				return (InvoiceBatchReport) Session["InvoiceReport"];
			}
			set 
			{
				Session["InvoiceReport"] = value;
			}
		}
        private int CurrentFiscalYear
        {
            get
            {
                Season seasonCurrentFY = new Season();
                seasonCurrentFY.GetOneByDate(DateTime.Now);

                int currentFiscalYear = 0;
                if (((SeasonDataSet)seasonCurrentFY.dataSet).Season.Rows.Count > 0)
                {
                    currentFiscalYear = ((SeasonDataSet.SeasonRow)((SeasonDataSet)seasonCurrentFY.dataSet).Season.Rows[0]).FiscalYear;
                }

                return currentFiscalYear;
            }
        }
        private int? SelectedFiscalYear
        {
            get
            {
                int? result = null;

                if (this.ddlFiscalYear.SelectedIndex > -1)
                {
                    if (this.ddlFiscalYear.SelectedValue.Trim().Length > 0)
                    {
                        result = Convert.ToInt32(this.ddlFiscalYear.SelectedValue);
                    }
                }

                return result;
            }
        }
        private bool ShowOnlyAccountsInOwing
        {
            get
            {
                // Values can be:
                // "All" to show all invoices
                // "Owing" to show only invoices that are in owing state

                bool result = false;

                if (QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999")
                {
                    result = false;
                }
                else
                {
                    if (this.ddlShowInvoiceType.SelectedValue == "Owing")
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }

                return result;
            }
        }
		private string FMID 
		{
			get 
			{
				string fmid = "9999";

				if (ViewState["FMID"] != null) 
				{
					fmid = ViewState["FMID"].ToString();
				}

				return fmid;
			}
			set 
			{
				ViewState["FMID"] = value;
			}
		}

        private void BindFiscalYearDDL()
        {
            int currentFiscalYear = this.CurrentFiscalYear;

            Season seasonAllFY = new Season();
            seasonAllFY.GetAllFiscalYears();

            SeasonDataSet sds = (SeasonDataSet)seasonAllFY.dataSet;
            SeasonDataSet.SeasonDataTable sdt = (SeasonDataSet.SeasonDataTable)sds.Season;

            this.ddlFiscalYear.Items.Clear();
            for (int i = 0; i < sdt.Rows.Count; i++)
            {
                SeasonDataSet.SeasonRow row = (SeasonDataSet.SeasonRow)sdt.Rows[i];

                ListItem newItem = new ListItem();

                newItem.Value = row.FiscalYear.ToString();
                newItem.Text = row.FiscalYear.ToString();
                if (row.FiscalYear == currentFiscalYear)
                {
                    newItem.Selected = true;
                }

                this.ddlFiscalYear.Items.Add(newItem);
            }
        }

		#region INVOICE
		public void BindInvoiceGrid(string strSearchField, int? fiscalYear, string FMID)
		{
			LabelMsg.Text = "";

         bool showOnlyAccountsInOwing = false;
         if (this.ddlShowInvoiceType.SelectedValue == "Owing")
         {
               showOnlyAccountsInOwing = true;
         }

         bool showNonPrinted = false;
         if (this.chkShowNonPrinted.Checked)
         {
            showNonPrinted = true;
         }

			//Get all invoices by date range, for FM's, only get their data
         dt = aInvoicePrintDataAccess.GetAllInvoicesToPrint(fiscalYear, FMID, showOnlyAccountsInOwing, showNonPrinted);
			dv = dt.DefaultView;

			if(strSearchField.Length > 0)
			{
				if(ddlStatus.SelectedItem.Value.Trim()=="Name")
				{
					//dv.RowFilter = "Group_Name LIKE '" + Regex.Replace(strSearchField,"'","''") + "%'" + " And IS_Printed = '" + ddlPrinted.SelectedItem.Value.Trim() + "'";
                    if (ddlPrinted.SelectedIndex == 0)
                    {
                        dv.RowFilter = "Group_Name LIKE '" + strSearchField.Replace("'", "''") + "%'";
                    }
                    else
                    {
                        dv.RowFilter = "Group_Name LIKE '" + strSearchField.Replace("'", "''") + "%'" + " And IS_Printed = '" + ddlPrinted.SelectedItem.Value.Trim() + "'";
                    }

                    if (Session["InvSortField"] == null)
                    {
                        Session["InvSortField"] = "CampaignBalance, Group_Name, Invoice_Date";
                    }
				}
				else if (ddlStatus.SelectedItem.Value.Trim()=="InvoiceID")
				{
					//dv.RowFilter = "Invoice_ID = " + Regex.Replace(strSearchField,"'","''")+ " And IS_Printed = '" + ddlPrinted.SelectedItem.Value.Trim() + "'";
                    if (ddlPrinted.SelectedIndex == 0)
                    {
                        dv.RowFilter = "Invoice_ID = " + strSearchField.Replace("'", "''");
                    }
                    else
                    {
                        dv.RowFilter = "Invoice_ID = " + strSearchField.Replace("'", "''") + " And IS_Printed = '" + ddlPrinted.SelectedItem.Value.Trim() + "'";
                    }

                    if (Session["InvSortField"] == null)
                    {
                        Session["InvSortField"] = "CampaignBalance, Invoice_ID, Group_Name";
                    }
				}
				else if (ddlStatus.SelectedItem.Value.Trim()=="AccountID")
				{
                    if (ddlPrinted.SelectedIndex == 0)
                    {
                        dv.RowFilter = "Account_ID = " + strSearchField.Replace("'", "''");
                    }
                    else
                    {
                        dv.RowFilter = "Account_ID = " + strSearchField.Replace("'", "''") + " And IS_Printed = '" + ddlPrinted.SelectedItem.Value.Trim() + "'";
                    }

                    if (Session["InvSortField"] == null)
                    {
                        Session["InvSortField"] = "CampaignBalance, Account_ID, Invoice_ID, Group_Name";
                    }
				}
				else if (ddlStatus.SelectedItem.Value.Trim()=="CampaignID")
				{
                    if (ddlPrinted.SelectedIndex == 0)
                    {
                        dv.RowFilter = "CampaignID = " + strSearchField.Replace("'", "''");
                    }
                    else
                    {
                        dv.RowFilter = "CampaignID = " + strSearchField.Replace("'", "''") + " And IS_Printed = '" + ddlPrinted.SelectedItem.Value.Trim() + "'";
                    }

                    if (Session["InvSortField"] == null)
                    {
                        Session["InvSortField"] = "CampaignBalance, Campaign_ID, Invoice_ID, Group_Name";
                    }
				}
			}
			else
			{
                if (ddlPrinted.SelectedIndex > 0)
                {
                    dv.RowFilter = "Is_Printed = '" + ddlPrinted.SelectedItem.Value.Trim() + "'";
                }

                if (Session["InvSortField"] == null)
                {
                    Session["InvSortField"] = "CampaignBalance, Group_Name, Invoice_Date";
                }
			}


			dv.Sort = (string)Session["InvSortField"];
			int totalInvoices = Convert.ToInt32(dv.Count);
			ShowInvoiceStats(totalInvoices);

			// bind to the Data
			InvoiceListDG.DataSource = dv;
			InvoiceListDG.DataBind();
		}

		public void ShowInvoiceStats(int nCount)
		{
			if (nCount == 0)
            {
                lblInvoice.Text = "No Records Found."; 
            }
			else
			{
				int nStartOfSet = (nCount > 0) ? (InvoiceListDG.CurrentPageIndex*InvoiceListDG.PageSize+1) : 0;
				int nEndPage = (InvoiceListDG.CurrentPageIndex+1)*(InvoiceListDG.PageSize);
				int nEndOfSet = (nEndPage > nCount) ? nCount : nEndPage;
				lblInvoice.Text = String.Format("Records: {0}-{1} of {2}", nStartOfSet, nEndOfSet ,nCount);
				//InvoiceListDG.PagerStyle.Visible = (nCount <= InvoiceListDG.PageSize) ? false : true;
			}
		}
		public void InvoiceListDG_Page(Object sender, DataGridPageChangedEventArgs e)
		{
			//used by built-in pager.  CurrentPageIndex already set
			InvoiceListDG.CurrentPageIndex = e.NewPageIndex;
			strSearchField = (string)Search.Text.Trim();

            BindInvoiceGrid(strSearchField, this.SelectedFiscalYear, FMID);
		}
		public void InvoiceListDG_Sort(Object sender, DataGridSortCommandEventArgs e)
		{
			Session["InvSortField"] = (string)e.SortExpression;
			strSearchField = (string)Search.Text.Trim();

            BindInvoiceGrid(strSearchField, this.SelectedFiscalYear, FMID);
		}

        protected void btnSearchGo_Click(object sender, EventArgs e)
        {
            InvoiceListDG.CurrentPageIndex = 0;
            strSearchField = (string)Search.Text.Trim();

            BindInvoiceGrid(strSearchField, this.SelectedFiscalYear, FMID);
        }

		public void DoPrint(Object sender, EventArgs e)
		{
			ChangeStatusChecked("Y");
		}
		public void DoUnprint(Object sender, EventArgs e)
		{
			ChangeStatusChecked("N");
		}
		private void btnMarkWholeListPrinted_Click(object sender, EventArgs e)
		{
			ChangeStatusList("Y");
		}
		private void btnMarkWholeListUnprinted_Click(object sender, EventArgs e)
		{
			ChangeStatusList("N");
		}
		private void ChangeStatusChecked(string isPrinted) 
		{
			bool isChecked = false;
			ArrayList array		= new ArrayList();

			foreach (DataGridItem i in InvoiceListDG.Items)
			{
				CheckBox printItem = (CheckBox)i.FindControl("PrintThis");
				if (printItem.Checked)
				{
					array.Add (((Literal)i.FindControl("InvoiceID")).Text.ToString());
					isChecked = true;
				}
			}

			if (isChecked == true)
			{
				ChangeStatus(array, isPrinted);
			}
			else
			{
				LabelMsg.Text = "No Records Selected";
			}
		}

		private void ChangeStatusList(string isPrinted) 
		{
         InvoiceBatchReport invoice = new InvoiceBatchReport(this.chkIncludeOEFUReport.Checked);
			ArrayList array	= new ArrayList();
			string accountName = String.Empty;
			int invoiceID = 0;
			int accountID = 0;
			int campaignID = 0;
			
			if(this.ddlStatus.SelectedValue == "Name") 
			{
				accountName = Search.Text;
			} 
			else if(this.ddlStatus.SelectedValue == "InvoiceID") 
			{
				invoiceID = Convert.ToInt32(Search.Text);
			}
			else if(this.ddlStatus.SelectedValue == "AccountID") 
			{
				accountID = Convert.ToInt32(Search.Text);
			}
			else if(this.ddlStatus.SelectedValue == "CampaignID") 
			{
				campaignID = Convert.ToInt32(Search.Text);
			}

            invoice.Search(accountName, invoiceID, accountID, campaignID, this.SelectedFiscalYear, this.ddlPrinted.SelectedValue, this.ShowOnlyAccountsInOwing, this.chkShowNonPrinted.Checked);
			
			if (invoice.dataSet.INVOICE.Count > 0) 
			{
				foreach(InvoiceDataSet.INVOICERow row in invoice.dataSet.INVOICE.Rows)
				{
					array.Add(row.Invoice_ID.ToString());
				}

				ChangeStatus(array, isPrinted);
			}
			else
			{
				LabelMsg.Text = "No Records Selected";
			}
		}
		private void ChangeStatus(ArrayList invoiceIDArray, string isPrinted) 
		{
			string strChangedBy = Convert.ToString(Session["Instance"]);

			try
			{
				//run the print routine; get the printer name
				bool bSuccess = true;
				for (int x = 0; x < invoiceIDArray.Count; x++)
				{
					if (bSuccess)
					{
						aInvoicePrintDataAccess.UpdateInvoicePrintedStatus(invoiceIDArray[x].ToString().Trim(), strChangedBy, isPrinted);
					}
				}
				if (bSuccess)
				{
					//Refresh data
					strSearchField  = Convert.ToString(Search.Text.Trim());
                    BindInvoiceGrid(strSearchField, this.SelectedFiscalYear, FMID);
					LabelMsg.Text = "Invoices Updated";
				}
			}
			catch (SqlException exc)
			{ throw exc;}
		}
		private void FillCheckedItems() 
		{
			InvoiceReport = new InvoiceBatchReport(this.chkIncludeOEFUReport.Checked);

			foreach(DataGridItem item in InvoiceListDG.Items) 
			{
				if (GetPrintThis(item).Checked) 
				{
					InvoiceReport.dataSet.INVOICE.AddINVOICERow(GetAccountType(item), GetGroupName(item), GetCampaignID(item), GetAccountID(item), GetOrderID(item), GetInvoiceDate(item), GetInvoiceDueDate(item), GetInvoiceAmount(item), GetIsPrinted(item), GetApprovedDate(item), GetLanguage(item), GetType(item)).Invoice_ID = GetInvoiceID(item);
				}
			}
		}
		private CheckBox GetPrintThis(DataGridItem item) 
		{
			return (CheckBox) item.FindControl("PrintThis");
		}
		private int GetInvoiceID(DataGridItem item) 
		{
			return Convert.ToInt32(((Literal) item.FindControl("InvoiceID")).Text);
		}
		private string GetAccountType(DataGridItem item) 
		{
			return ((Label) item.FindControl("lblAccountType")).Text;
		}
		private string GetGroupName(DataGridItem item) 
		{
			return ((Label) item.FindControl("lblGroupName")).Text;
		}
		private int GetCampaignID(DataGridItem item) 
		{
			return Convert.ToInt32(((Label) item.FindControl("lblCampaignID")).Text);
		}
		private int GetAccountID(DataGridItem item) 
		{
			return Convert.ToInt32(((Label) item.FindControl("lblAccountID")).Text);
		}

		private int GetOrderID(DataGridItem item) 
		{
			return Convert.ToInt32(((Label) item.FindControl("lblOrderID")).Text);
		}

		private DateTime GetInvoiceDate(DataGridItem item) 
		{
			return Convert.ToDateTime(((Label) item.FindControl("lblInvoiceDate")).Text);
		}

		private DateTime GetInvoiceDueDate(DataGridItem item) 
		{
			return Convert.ToDateTime(((Label) item.FindControl("lblInvoiceDueDate")).Text);
		}

		private double GetInvoiceAmount(DataGridItem item) 
		{
			return Convert.ToDouble(((Label) item.FindControl("lblInvoiceAmount")).Text.Replace("$", ""));
		}

		private string GetIsPrinted(DataGridItem item) 
		{
			return ((Label) item.FindControl("lblIsPrinted")).Text;
		}

		private DateTime GetApprovedDate(DataGridItem item) 
		{
			return Convert.ToDateTime(((Label) item.FindControl("lblApprovedDate")).Text);
		}

		private string GetLanguage(DataGridItem item) 
		{
			return ((Literal) item.FindControl("Lang")).Text;
		}

		private string GetType(DataGridItem item) 
		{
			return ((Literal) item.FindControl("Type")).Text;
		}

		public void changePrinted(Object sender, EventArgs e)
		{
			InvoiceListDG.CurrentPageIndex = 0;
			strSearchField  = (string)Search.Text.Trim();

			SetPrintedValue();

            BindInvoiceGrid(strSearchField, this.SelectedFiscalYear, FMID);
		}
        public void changeFY(Object sender, EventArgs e)
        {
            InvoiceListDG.CurrentPageIndex = 0;
            strSearchField = (string)Search.Text.Trim();

            BindInvoiceGrid(strSearchField, this.SelectedFiscalYear, FMID);
        }
        public void changeShowInvoiceType(Object sender, EventArgs e)
        {
            InvoiceListDG.CurrentPageIndex = 0;
            strSearchField = (string)Search.Text.Trim();

            BindInvoiceGrid(strSearchField, this.SelectedFiscalYear, FMID);
        }

		private void SetFMView()
		{
			//Check if user is FM
            if (QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999")
            {
                //Set FMID
                FMID = QSPPage.aUserProfile.FMID;

                //Set Printed Dropdown to All
                ddlPrinted.SelectedIndex = ddlPrinted.Items.IndexOf(ddlPrinted.Items.FindByValue(""));

                //Hide Dropdown
                ddlPrinted.Visible = false;

                //Hide Printed Label
                lblPrinted.Visible = false;

                //Hide Printed Column in DataGrid
                //InvoiceListDG.Columns[SELECTEDCHECKBOX_COLUMN].Visible = false;
                //InvoiceListDG.Columns[PRINTED_COLUMN].Visible = false;
                
                this.tblPrintButtons.Visible = true;
                this.tdShowInvoiceType.Visible = false;
            }
            else
            {
                //Set Printed Dropdown to All
                ddlPrinted.SelectedIndex = ddlPrinted.Items.IndexOf(ddlPrinted.Items.FindByValue("N"));

                this.tblPrintButtons.Visible = true;
                this.tdShowInvoiceType.Visible = true;
            }
		}

		private void SetPrintedValue() 
		{
			bool printedVisible = (this.ddlPrinted.SelectedValue == "Y");
			
			//always keep these buttons hidden to FM's
			if (QSPPage.aUserProfile.IsFM && FMID != "9999")
			{
				tdPrintItems.Visible = false;
				tdUnprintItems.Visible = false;
				tdPrintList.Visible = false;
				tdUnprintList.Visible = false;
			}
			else
			{
				tdPrintItems.Visible = !printedVisible;
				tdUnprintItems.Visible = printedVisible;
				tdPrintList.Visible = !printedVisible;
				tdUnprintList.Visible = printedVisible;
			}

			if (printedVisible) 
			{
				this.btnPrintChecked.Text = "Reprint checked";
				this.btnPrintList.Value = "Reprint whole list";
			} 
			else 
			{
				this.btnPrintChecked.Text = "Print checked";
				this.btnPrintList.Value = "Print whole list";
			}
		}

		public void InvoiceListDG_ItemDataBound(Object sender, DataGridItemEventArgs e)
		{
			ParameterValueCollection parameterValues;
			ParameterValue parameterValue;

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Literal InvoiceID	= (Literal)e.Item.FindControl("InvoiceID");
				Literal Lang		= (Literal)e.Item.FindControl("Lang");
				RSGenerationLinkButton rsGenerationPrintInvoice = (RSGenerationLinkButton) e.Item.FindControl("rsGenerationPrintInvoice");

				if(rsGenerationPrintInvoice != null) 
				{
					rsGenerationPrintInvoice.Mode = FilePageMode.PopUp;
					rsGenerationPrintInvoice.Text = InvoiceID.Text;

					if(Lang.Text.ToUpper().Trim() == "EN")
					{
						rsGenerationPrintInvoice.ReportName = "PrintInvoice";
                    }
                    else if (Lang.Text.ToUpper().Trim() == "FR")
                    {
                        rsGenerationPrintInvoice.ReportName = "PrintInvoice";
                        //rsGenerationPrintInvoice.ReportName = "PrintInvoiceFrench";
                    }

					parameterValues = new ParameterValueCollection();
					parameterValue = new ParameterValue();
					parameterValue.Name = "InvoiceID";
					parameterValue.Value = InvoiceID.Text;
					parameterValues.Add(parameterValue);

					rsGenerationPrintInvoice.ParameterValues = parameterValues;
				}
			}
		}
        protected void InvoiceListDG_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            this.InvoiceReport = new InvoiceBatchReport(this.chkIncludeOEFUReport.Checked);
            this.InvoiceReport.dataSet.INVOICE.AddINVOICERow(GetAccountType(e.Item), GetGroupName(e.Item), GetCampaignID(e.Item), GetAccountID(e.Item), GetOrderID(e.Item), GetInvoiceDate(e.Item), GetInvoiceDueDate(e.Item), GetInvoiceAmount(e.Item), GetIsPrinted(e.Item), GetApprovedDate(e.Item), GetLanguage(e.Item), GetType(e.Item)).Invoice_ID = GetInvoiceID(e.Item);

            string script;
            script = "<script language=\"javascript\">\n";
            script += "  window.open(\"" + PRINT_LIST_REPORT_PAGE + "?IsNewWindow=true&PrintChecked=true\",'',\"toolbar = no,status=no,scrollbars=yes,resizable=yes, width=800, height=550\");\n";
            script += "</script>\n";

            this.RegisterStartupScript("PrintReport", script);
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
			this.btnPrintChecked.Click += new System.EventHandler(this.btnPrintChecked_Click);
			this.PrintItems.Click += new System.EventHandler(this.DoPrint);
			this.UnprintItems.Click += new System.EventHandler(this.DoUnprint);
			this.btnMarkWholeListPrinted.Click += new System.EventHandler(this.btnMarkWholeListPrinted_Click);
			this.btnMarkWholeListUnprinted.Click += new System.EventHandler(this.btnMarkWholeListUnprinted_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		private void AddJavaScript() 
		{
			string script;

			script  = "<script language=\"javascript\">\n";
			script += "  function PrintWholeList() {\n";
			script += "    var accountName = \"\";\n";
			script += "    var invoiceID = 0;\n";

			script += "    if(document.getElementById(\"" + ddlStatus.ClientID + "\").options[document.getElementById(\"" + ddlStatus.ClientID + "\").selectedIndex].value == \"Name\") {\n";
			script += "      accountName = document.getElementById(\"" + Search.ClientID + "\").value;\n";
			script += "    } else if(document.getElementById(\"" + ddlStatus.ClientID + "\").options[document.getElementById(\"" + ddlStatus.ClientID + "\").selectedIndex].value == \"AccountID\") {\n";
			script += "      accountID = document.getElementById(\"" + Search.ClientID + "\").value;\n";
			script += "    } else if(document.getElementById(\"" + ddlStatus.ClientID + "\").options[document.getElementById(\"" + ddlStatus.ClientID + "\").selectedIndex].value == \"CampaignID\") {\n";
			script += "      campaignID = document.getElementById(\"" + Search.ClientID + "\").value;\n";
			script += "    } else if(document.getElementById(\"" + ddlStatus.ClientID + "\").options[document.getElementById(\"" + ddlStatus.ClientID + "\").selectedIndex].value == \"InvoiceID\") {\n";
			script += "      invoiceID = document.getElementById(\"" + Search.ClientID + "\").value;\n";
			script += "    }\n";
            script += "    var fiscalYear = document.getElementById(\"" + ddlFiscalYear.ClientID + "\").value;\n";
            script += "    var showInvoiceType = document.getElementById(\"" + ddlShowInvoiceType.ClientID + "\").value;\n";
			script += "    var isPrinted = document.getElementById(\"" + ddlPrinted.ClientID + "\").options[document.getElementById(\"" + ddlPrinted.ClientID + "\").selectedIndex].value;\n";
            script += "    var isIncludeOEFUReport = document.getElementById(\"" + chkIncludeOEFUReport.ClientID + "\").checked;\n";

            script += "    window.open(\"" + PRINT_LIST_REPORT_PAGE + "?IsNewWindow=true&PrintChecked=false&AccountName=\" + accountName + \"&InvoiceID=\" + invoiceID + \"&FiscalYear=\" + fiscalYear + \"&ShowInvoiceType=\" + showInvoiceType + \"&IsPrinted=\" + isPrinted + \"&IsIncludeOEFUReport=\" + isIncludeOEFUReport + \"\",'',\"toolbar = no,status=no,scrollbars=yes,resizable=yes, width=800, height=550\");\n";
			script += "  }\n";
			script += "</script>\n";
            
			this.Page.RegisterClientScriptBlock("PrintWholeList", script);

			this.btnPrintList.Attributes["onclick"] = "PrintWholeList()";

			PrintItems.Attributes.Add("onclick", "return confirmPrint(this.form, true);");
			btnMarkWholeListPrinted.Attributes.Add("onclick", "return confirmPrint(this.form, false);");
		}



        

	}
}
