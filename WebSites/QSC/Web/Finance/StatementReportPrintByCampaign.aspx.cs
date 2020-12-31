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


namespace QSPFulfillment.Finance
{
	///<summary>StatementReportPrintByCampaign</summary>
	public class StatementReportPrintByCampaign : QSPFulfillment.CommonWeb.QSPPage
	{
		protected DAL.InvoiceListPrintDataAccess aInvoicePrintDataAccess;
		protected DAL.StatementReportByCampaignPrintDataAccess aStatementReportByCampaignPrintDataAccess;
		protected System.Web.UI.WebControls.RequiredFieldValidator Req8;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegExp8;
		protected System.Web.UI.WebControls.RequiredFieldValidator Req9;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegExp9;
        protected QSPFulfillment.CommonWeb.UC.FiscalYearSelectControl ctrlFiscalYearSelect;
        protected QSPFulfillment.CommonWeb.UC.StatementRunSelectControl ctrlStatementRunSelect;
        protected QSPFulfillment.CommonWeb.UC.DateEntry dteFromDeliveryDate;
        protected RadioButtonList ctrlStatementType;
        
		protected DataView dv = new DataView();
		protected TextBox Search;
		protected DataGrid CampaignStatementReportDG;
		protected Label lblStatement;
      protected Panel pnlShowTransactionsDateFrom;
		//Number of Records to show
		protected DropDownList ddlStatus;

		protected string strSearchField = "";


		protected System.Web.UI.WebControls.DropDownList ddlInvStatus;
		protected System.Web.UI.WebControls.LinkButton BtnSearch;

        //Report Name
		public string m_reportName  = "StatementReportByCampaign";

        private int? FiscalYear
        {
            get
            {
                if (ctrlFiscalYearSelect.Value == "")
                    return null;
                return Convert.ToInt32(ctrlFiscalYearSelect.Value);
            }
        }
        private bool Realtime
        {
            get
            {
                return (ctrlStatementType.SelectedValue == "Realtime");
            }
        }
        private bool StatementTypeVisible
        {
            get
            {
                return ctrlStatementType.Visible;
            }
            set
            {
                ctrlStatementType.Visible = value;
            }
        }
        private string DateFrom
        {
            get
            {
                if (ctrlStatementType.SelectedValue == "Realtime" && !dteFromDeliveryDate.Date.Equals(DateTime.MinValue))
                {
                    return dteFromDeliveryDate.Value;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
                SetFMView();

                BindStatementReportGrid(this, EventArgs.Empty);
			}
		}
        protected void ctrlStatementType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.ctrlStatementType.SelectedIndex;

            if (selectedIndex == 0)
            {
                // Real time statement
                this.ctrlStatementRunSelect.Visible = false;
                this.pnlShowTransactionsDateFrom.Visible = true;
            }
            else
            {
                // Existing statement
                this.ctrlStatementRunSelect.Visible = true;
                this.pnlShowTransactionsDateFrom.Visible = false;
            }
        }

        private void BindStatementReportGrid(object sender, System.EventArgs e)
        {
            if (Request.QueryString["Search"] != null)
            {
                strSearchField = Request.QueryString["Search"];
            }

            Search.Text = strSearchField;

            int? statementRunId = null;
            if (!Realtime)
            {
                int temp;
                bool isStatementRunIdParsedSuccessfully =
                    int.TryParse(this.ctrlStatementRunSelect.Value, out temp);

                if (isStatementRunIdParsedSuccessfully)
                {
                    statementRunId = temp;
                }
            }

            BindStatementReportGrid(strSearchField, FiscalYear, Realtime, statementRunId);
        }
        private void LoadDataDDLSearchBy()
        {
            if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM &&
                QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID == "9999")
            {
                ddlStatus.Items.Clear();

                ddlStatus.Items.Insert(0, new ListItem("Acct ID", "AcctID"));
                ddlStatus.Items.Insert(1, new ListItem("Camp ID", "CampID"));
                ddlStatus.Items.Insert(2, new ListItem("Acct Name", "AcctName"));
                ddlStatus.Items.Insert(3, new ListItem("FM", "FM"));
            }
            else
            {
                ddlStatus.Items.Clear();
                ddlStatus.Items.Insert(0, new ListItem("Acct ID", "AcctID"));
                ddlStatus.Items.Insert(1, new ListItem("Camp ID", "CampID"));
                ddlStatus.Items.Insert(2, new ListItem("Acct Name", "AcctName"));
            }

        }
        private void SetFMView()
        {
            bool IsFM = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM &&
                QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID != "9999";

            /*if (IsFM)
            {
                ctrlStatementType.SelectedValue = "LastOfficial";
                StatementTypeVisible = false;
            }*/
        }

		private DAL.StatementReportByCampaignPrintDataAccess StatementReportByCampaignPrintDataAccess
		{
			get 
			{
				if(aStatementReportByCampaignPrintDataAccess == null) 
				{
					aStatementReportByCampaignPrintDataAccess = new DAL.StatementReportByCampaignPrintDataAccess();
				}

				return aStatementReportByCampaignPrintDataAccess;
			}
		}

		#region Statement
		public void BindStatementReportGrid(string strSearchField, int? fiscalYear, bool realtime, int? statementRunId)
		{
            string accountName = "";
            int? accountID = null;
            int? campaignID = null;
            string fmid = "";
            string fmLastName = "";

            if (strSearchField.Length > 0)
            {
                if (ddlStatus.SelectedItem.Value.Trim() == "AcctName")
                {
                    accountName = strSearchField.Replace("'", "''");
                    Session["StatementSortField"] = "Name, AccountID";
                }
                else if (ddlStatus.SelectedItem.Value.Trim() == "AcctID")
                {
                    accountID = Convert.ToInt32(strSearchField.Replace("'", "''"));
                    Session["StatementSortField"] = "AccountID, Name";
                }
                else if (ddlStatus.SelectedItem.Value.Trim() == "CampID")
                {
                    campaignID = Convert.ToInt32(strSearchField.Replace("'", "''"));
                    Session["StatementSortField"] = "CampaignID, Name";
                }
                else if (ddlStatus.SelectedItem.Value.Trim() == "FMID")
                {
                    fmid = strSearchField.Replace("'", "''");
                    Session["StatementSortField"] = "FMID, Name";
                }
                else if (ddlStatus.SelectedItem.Value.Trim() == "LastName")
                {
                    fmLastName = strSearchField.Replace("'", "''");
                    Session["StatementSortField"] = "LastName, FirstName";
                }
            }
            else
            {
                if (Session["StatementSortField"] == null) Session["StatementSortField"] = "Name, AccountID";
            }

            if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM &&
                QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID != "9999")
            {
                fmid = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID;
            }

            dv = ( (DataTable)
					StatementReportByCampaignPrintDataAccess.GetAllStatementsByCampaignToPrint(
						fiscalYear, realtime, campaignID, accountID, accountName, fmid, fmLastName, statementRunId)
				).DefaultView;

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
			CampaignStatementReportDG.CurrentPageIndex = e.NewPageIndex;
			strSearchField = (string)Search.Text.Trim();

            int? statementRunId = null;
            if (!Realtime)
            {
                int temp;
                bool isStatementRunIdParsedSuccessfully =
                    int.TryParse(this.ctrlStatementRunSelect.Value, out temp);

                if (isStatementRunIdParsedSuccessfully)
                {
                    statementRunId = temp;
                }
            }

            BindStatementReportGrid(strSearchField, FiscalYear, Realtime, statementRunId);
		}

		public void CampaignStatementReportDG_Sort(Object sender, DataGridSortCommandEventArgs e)
		{
			Session["StatementSortField"] = (string)e.SortExpression;
			strSearchField = (string)Search.Text.Trim();

            int? statementRunId = null;
            if (!Realtime)
            {
                int temp;
                bool isStatementRunIdParsedSuccessfully =
                    int.TryParse(this.ctrlStatementRunSelect.Value, out temp);

                if (isStatementRunIdParsedSuccessfully)
                {
                    statementRunId = temp;
                }
            }

            BindStatementReportGrid(strSearchField, FiscalYear, Realtime, statementRunId);
		}

		public void SearchButtonClick(Object sender, EventArgs e)
		{
			CampaignStatementReportDG.CurrentPageIndex = 0;
			strSearchField  = (string)Search.Text.Trim();

            int? statementRunId = null;
            if (!Realtime)
            {
                int temp;
                bool isStatementRunIdParsedSuccessfully =
                    int.TryParse(this.ctrlStatementRunSelect.Value, out temp);

                if (isStatementRunIdParsedSuccessfully)
                {
                    statementRunId = temp;                    
                }
            }

            BindStatementReportGrid(strSearchField, FiscalYear, Realtime, statementRunId);
		}

		public void CampaignStatementReportDG_ItemDataBound(Object sender, DataGridItemEventArgs e)
		{
			ParameterValueCollection parameterValues;
			ParameterValue parameterValue;

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label lblAccountID	    = (Label)e.Item.FindControl("AccountID");
				Label lblCampaignID	    = (Label)e.Item.FindControl("lblCampaignID");
				Label lblLang		    = (Label)e.Item.FindControl("Lang");
                Label lblStatementId    = (Label)e.Item.FindControl("lblStatementId");
                
				RSGenerationLinkButton rsGenerationStatementReportByCampaign = (RSGenerationLinkButton) e.Item.FindControl("rsGenerationStatementReportByCampaign");

				if (rsGenerationStatementReportByCampaign != null) 
				{
					rsGenerationStatementReportByCampaign.Mode = FilePageMode.PopUp;
					rsGenerationStatementReportByCampaign.Text = lblAccountID.Text;

					if(lblLang.Text.ToUpper().Trim() == "EN") 
					{
						rsGenerationStatementReportByCampaign.ReportName = "StatementReportByCampaign";
					}
					else if(lblLang.Text.ToUpper().Trim() == "FR")
					{
						rsGenerationStatementReportByCampaign.ReportName = "StatementReportByCampaign";
					}

					parameterValues = new ParameterValueCollection();

					parameterValue = new ParameterValue();
					parameterValue.Name = "CampaignID";
					parameterValue.Value = lblCampaignID.Text.Trim();
					parameterValues.Add(parameterValue);

                    parameterValue = new ParameterValue();
                    parameterValue.Name = "Realtime";
                    parameterValue.Value = Convert.ToString(Realtime);
                    parameterValues.Add(parameterValue);
                    if (!string.IsNullOrWhiteSpace(DateFrom))
                    {
                        parameterValue = new ParameterValue();
                        parameterValue.Name = "DateFrom";
                        parameterValue.Value = DateFrom;
                        parameterValues.Add(parameterValue);
                    }
                    int statementIdValue = 0;
                    bool parseResult = int.TryParse(lblStatementId.Text.Trim(), out statementIdValue);

                    parameterValue = new ParameterValue();
                    parameterValue.Name = "StatementId";
                    parameterValue.Value = statementIdValue.ToString();
                    parameterValues.Add(parameterValue);

					rsGenerationStatementReportByCampaign.ParameterValues = parameterValues;
				}
			}
		}

		#endregion Statement

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
			this.Load += new System.EventHandler(this.Page_Load);
            this.ctrlStatementType.SelectedIndexChanged += new EventHandler(this.BindStatementReportGrid);
            this.dteFromDeliveryDate.tb_DATE_TextChanged_Event += new System.EventHandler(this.BindStatementReportGrid);
		}
		#endregion

        
	}
}
