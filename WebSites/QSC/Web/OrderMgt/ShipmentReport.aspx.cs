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
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.OrderMgt
{
	/// <summary>
	/// Summary description for GroupRefundReport.
	/// </summary>
	public class ShipmentReport : OrderMgtPage
	{
		protected System.Web.UI.WebControls.Label lblAccountId;
		protected System.Web.UI.WebControls.Label lblCampaignId;
        protected System.Web.UI.WebControls.Label lblOrderId;
        protected System.Web.UI.WebControls.Button PrintButton;
		protected System.Web.UI.WebControls.Label lblErrorMessage;
		protected System.Web.UI.WebControls.TextBox tbCampaignId;
		protected System.Web.UI.WebControls.TextBox tbAccountId;
        protected System.Web.UI.WebControls.TextBox tbOrderId;
        protected System.Web.UI.WebControls.CheckBox ShowBHECheckBox;
        protected QSP.WebControl.DropDownListSearch ddlFieldManager;
        protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationShipmentReport;
        protected System.Web.UI.WebControls.Label lblFieldManager;
        private QSPFulfillment.DataAccess.Business.AccountBusiness bAccountBusiness;
        protected QSPFulfillment.CommonWeb.UC.FiscalYearSelectControl ctrlFiscalYearSelect;
        protected QSPFulfillment.CommonWeb.UC.StatementRunSelectControl ctrlStatementRunSelect;

		private void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack)
            {
                this.SetValueDropDownList();

                if (QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999")
                {
                    SetValueFieldManager();
                }
            }

			lblErrorMessage.Visible =false;
	     }

        private int? FiscalYear
        {
            get
            {
                if (ctrlFiscalYearSelect.Value == "")
                    return null;
                return Convert.ToInt32(ctrlFiscalYearSelect.Value);
            }
        }

        private string FMID
        {
            get
            {
                return this.ddlFieldManager.Value;
            }
        }

        private int? CampaignID
        {
            get
            {
                if (this.tbCampaignId.Text == "")
                    return null;
                else
                    return Convert.ToInt32(this.tbCampaignId.Text);
            }
        }

        private int? AccountID
        {
            get
            {
                if (this.tbAccountId.Text == "")
                    return null;
                else
                    return Convert.ToInt32(this.tbAccountId.Text);
            }
        }

        private int? OrderID
        {
            get
            {
                if (this.tbOrderId.Text == "")
                    return null;
                else
                    return Convert.ToInt32(this.tbOrderId.Text);
            }
        }

        private bool ShowBHE
        {
            get
            {
                return this.ShowBHECheckBox.Checked;
            }
        }

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
			this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

        public AccountBusiness BusAccount
        {
            get
            {
                if (bAccountBusiness == null)
                    bAccountBusiness = new AccountBusiness(false);

                return bAccountBusiness;
            }

        }

        private void SetValueFieldManager()
        {
            this.ddlFieldManager.SelectedIndex = this.ddlFieldManager.Items.IndexOf(this.ddlFieldManager.Items.FindByValue(QSPPage.aUserProfile.FMID));
            this.lblFieldManager.Text = this.ddlFieldManager.SelectedItem.Text;

            this.ddlFieldManager.Visible = false;
            this.lblFieldManager.Visible = true;
        }

        private void SetValueDropDownList()
        {
            SetValueDDLFieldManager();
        }

        private void SetValueDDLFieldManager()
        {
            DataTable dtbFieldManager = new DataTable();
            ListItem sel = new ListItem("", "");

            sel.Selected = true;
            this.ddlFieldManager.Items.Add(sel);

            BusAccount.SelectAllFieldManager(dtbFieldManager);

            foreach (DataRow dtrFieldManager in dtbFieldManager.Rows)
            {
                this.ddlFieldManager.Items.Add(new ListItem(dtrFieldManager["LastName"].ToString() + ", " + dtrFieldManager["FirstName"].ToString() + " (" + dtrFieldManager["FMID"].ToString() + ")", dtrFieldManager["FMID"].ToString()));
            }
        }

		private void PrintButton_Click(object sender, System.EventArgs e)
		{
			lblErrorMessage.Text =" ";
			   
			CallReport(CampaignID, AccountID, OrderID, FiscalYear, FMID, ShowBHE);
		}

        private void CallReport(int? CampaignID, int? AccountID, int? OrderID, int? FiscalYear, string FMID, bool ShowBHE)
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;

			if(CampaignID > 0) 
			{
				parameterValue = new ParameterValue("CampaignID", CampaignID.ToString());
				parameterValues.Add(parameterValue);
			}

			if(AccountID > 0) 
			{
				parameterValue = new ParameterValue("AccountID", AccountID.ToString());
				parameterValues.Add(parameterValue);
			}

            if (OrderID > 0)
            {
                parameterValue = new ParameterValue("OrderID", OrderID.ToString());
                parameterValues.Add(parameterValue);
            }

            if (FiscalYear > 0)
            {
                parameterValue = new ParameterValue("FiscalYear", FiscalYear.ToString());
                parameterValues.Add(parameterValue);
            }

            if (FMID.Length > 0)
            {
                parameterValue = new ParameterValue("FMID", FMID);
                parameterValues.Add(parameterValue);
            }

            parameterValue = new ParameterValue("ShowBHE", ShowBHE.ToString());
            parameterValues.Add(parameterValue);

			rsGenerationShipmentReport.Generate(parameterValues);
		}
	}
}
