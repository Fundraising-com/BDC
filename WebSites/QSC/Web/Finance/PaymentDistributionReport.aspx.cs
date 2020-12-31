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

namespace QSPFulfillment.Finance.Reports
{
	///<summary>Link up to PaymentDistributionReport rdl</summary>
	public class PaymentDistributionReport : QSPFulfillment.AcctMgt.AcctMgtPage
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
			this.btSubmit.Click += new System.EventHandler(this.btSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion auto-generated code

		#region Item Declarations
		protected System.Web.UI.WebControls.ValidationSummary		ValSummary_TOP;
		protected System.Web.UI.WebControls.Button					btSubmit;
		protected System.Web.UI.WebControls.DropDownList			ddlAccountType;
		//protected System.Web.UI.WebControls.RequiredFieldValidator	rqAccountType;
		protected QSPFulfillment.CommonWeb.UC.AccountLookUp			ucAccountNumber;
		protected QSPFulfillment.CommonWeb.UC.DateEntry				ucPaymentStartDate;
		protected QSPFulfillment.CommonWeb.UC.DateEntry				ucPaymentEndDate;
		protected System.Web.UI.WebControls.TextBox					tbOrderID;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationPaymentDistributionReport;
		//protected System.Web.UI.WebControls.RequiredFieldValidator	rqOrderID;
		protected System.Web.UI.WebControls.DropDownList			ddlPaymentMethod;
		//protected System.Web.UI.WebControls.RequiredFieldValidator	rqPaymentMethod;
		#endregion Item Declarations

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if(! aUserProfile.HasRole("Finance"))
				{
					//user is not allowed to see this page
					Response.Redirect("../Common/AccessDenied.aspx?p=Finance_Reports_PaymentDistributionReport");
				}
			}
			else
			{
				this.ValSummary_TOP.HeaderText = QSPFulfillment.DataAccess.Common.Message.VALMSG_HEADER_TEXT_VAR_0;
			}
		}

		private void Page_Load_NOT_PostBack_Handler(){}
		private void Page_Load_PostBack_Handler(){}
		#endregion Page_Load

		#region Postback process
		
		private void btSubmit_Click(object s, EventArgs e)
		{
			Page.Validate();
			if(!Page.IsValid)
			{
				this.CurrentMessageManager.Add("The page did not validate properly"); 
				this.SetPageError();
				return;
			}

			bool blError = false;

			int AccountType = 0;
			try   { if(this.ddlAccountType.SelectedValue.Trim() != "") { AccountType = Convert.ToInt32(this.ddlAccountType.SelectedValue); } }
			catch { this.CurrentMessageManager.Add("The Account Type value must be numeric."); blError = true; }

			int AccountId = 0;
			try   { if(this.ucAccountNumber.Text.Trim() != "") { AccountId = Convert.ToInt32(this.ucAccountNumber.Text); } }
			catch { this.CurrentMessageManager.Add("The Group # value must be numeric."); blError = true; }

			DateTime PaymentDateFrom = this.ucPaymentStartDate.Date;
			if(PaymentDateFrom == System.DateTime.MinValue) 
			{
				this.CurrentMessageManager.Add("A valid Payment start date must be entered"); 
				blError = true; 
			}

			DateTime PaymentDateTo = this.ucPaymentEndDate.Date;
			if(PaymentDateTo == System.DateTime.MinValue) 
			{
				this.CurrentMessageManager.Add("A valid Payment end date must be entered"); 
				blError = true; 
			}

			int	OrderId = 0;
			try   {  if(this.tbOrderID.Text.Trim() != "") { OrderId = Convert.ToInt32(this.tbOrderID.Text); } }
			catch { this.CurrentMessageManager.Add("The Order ID value must be numeric."); blError = true; }

			int	PaymentMethodId = 0;
			try   { if(this.ddlPaymentMethod.SelectedValue.Trim() != "") { PaymentMethodId = Convert.ToInt32(this.ddlPaymentMethod.SelectedValue); } } 
			catch { this.CurrentMessageManager.Add("The Payment Method value must be numeric."); blError = true; }

			if (blError == false)
			{
				CallReport(AccountType, AccountId, PaymentDateFrom, PaymentDateTo, OrderId, PaymentMethodId);
			}
			else
			{
				this.SetPageError();
				return;
			}
		}
		

		private void CallReport(
			int AccountType
			, int AccountId
			, DateTime PaymentDateFrom
			, DateTime PaymentDateTo
			, int OrderId
			, int PaymentMethodId)
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;

			if(AccountType != 0) {
				parameterValue = new ParameterValue("AccountType", AccountType.ToString());
				parameterValues.Add(parameterValue);
			}

			if(AccountId != 0) {
				parameterValue = new ParameterValue("AccountId", AccountId.ToString());
				parameterValues.Add(parameterValue);
			}

			parameterValue = new ParameterValue("PaymentDateFrom", PaymentDateFrom.ToShortDateString());
			parameterValues.Add(parameterValue);
			
			parameterValue = new ParameterValue("PaymentDateTo", PaymentDateTo.ToShortDateString());
			parameterValues.Add(parameterValue);
			
			if(OrderId != 0) {
				parameterValue = new ParameterValue("OrderId", OrderId.ToString());
				parameterValues.Add(parameterValue);
			}

			if(PaymentMethodId != 0) {
				parameterValue = new ParameterValue("PaymentMethodId", PaymentMethodId.ToString());
				parameterValues.Add(parameterValue);
			}

			rsGenerationPaymentDistributionReport.Generate(parameterValues, 540000);
		}

		#endregion Postback process


	}
}
