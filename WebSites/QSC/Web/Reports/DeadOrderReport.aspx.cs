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

namespace QSPFulfillment.Reports
{
	/// <summary>
	/// Summary description for DeadOrderReport.
	/// </summary>
	public class DeadOrderReport : QSPFulfillment.CommonWeb.QSPPage
	{
		protected System.Web.UI.WebControls.Label lblStartDate;
		protected System.Web.UI.WebControls.Label lblOrderDateTo;
		protected System.Web.UI.WebControls.Label lblOrderId;
		protected System.Web.UI.WebControls.Label lblCampaignId;
		protected System.Web.UI.WebControls.Label lblAccountId;
		protected System.Web.UI.WebControls.Label lblErrorType;
		protected System.Web.UI.WebControls.Button PrintButton;
		protected System.Web.UI.WebControls.TextBox tbOrderId;
		protected System.Web.UI.WebControls.TextBox tbAccountId;
		protected System.Web.UI.WebControls.TextBox tbCampaignId;
		protected System.Web.UI.WebControls.DropDownList ddlErrorType;
		protected System.Web.UI.WebControls.Label lblDeadOrderReport;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ucOrderDateFrom,ucOrderDateTo;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationDeadOrderReport;
		protected System.Web.UI.WebControls.Label lblErrorMessage;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				this.ucOrderDateTo.Date = System.DateTime.Now;
				lblErrorMessage.Text="";
				
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

		private void PrintButton_Click(object sender, System.EventArgs e)
		{
			lblErrorMessage.Text="";

			DateTime OrderDateFrom;
			DateTime OrderDateTo;
			int OrderId;
			int	AccountId;
			int CampaignId;
			string  ErrorType;

			// Order from Date can not be null	
			if (this.ucOrderDateFrom.Date == System.DateTime.MinValue )
			{
				lblErrorMessage.Text = "Invalid date, please correct";
				return;
			}
			else
			{
				OrderDateFrom = this.ucOrderDateFrom.Date;
			}

			// order end Date can not be null												
			if (this.ucOrderDateTo.Date  == System.DateTime.MinValue)
			{
				lblErrorMessage.Text = "Invalid date, please correct";
				return;
			}
			else
			{
				OrderDateTo = this.ucOrderDateTo.Date;
			
			}
			if (this.ucOrderDateFrom.Date > this.ucOrderDateTo.Date)
			{
				lblErrorMessage.Text = "Invalid date, please correct";
				return;
			}

			// Order id is optional parameter
			if (this.tbOrderId.Text == "")
			{
				OrderId = 0;
			}
			else
			{
				OrderId = Convert.ToInt32(this.tbOrderId.Text);
				
			}
			// Account Id is optional
			if (this.tbAccountId.Text == "")
			{
				AccountId	   = 0;
			}
			else
			{
				AccountId = Convert.ToInt32(this.tbAccountId.Text);
			}
			if (this.tbCampaignId.Text == "")
			{
				CampaignId	   = 0;
			}
			else
			{
				CampaignId = Convert.ToInt32(this.tbCampaignId.Text);
			}

			ErrorType		   =  this.ddlErrorType.SelectedValue; 
   
			CallReport(OrderDateFrom,OrderDateTo,OrderId,AccountId,CampaignId,ErrorType	);
		}

		private void CallReport(DateTime OrderDateFrom,
								DateTime OrderDateTo,
								int OrderId,
								int	AccountId,
								int CampaignId,
								string  ErrorType	)
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;

			parameterValue = new ParameterValue();
			parameterValue.Name = "StartDate";
			parameterValue.Value = OrderDateFrom.ToShortDateString();
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue();
			parameterValue.Name = "EndDate";
			parameterValue.Value = OrderDateTo.ToShortDateString();
			parameterValues.Add(parameterValue);
			
			if(OrderId != 0) 
			{
				parameterValue = new ParameterValue();
				parameterValue.Name = "OrderId";
				parameterValue.Value = OrderId.ToString();
				parameterValues.Add(parameterValue);
			}
		
			if(AccountId != 0) 
			{
				parameterValue = new ParameterValue();
				parameterValue.Name = "AccountId";
				parameterValue.Value = AccountId.ToString();
				parameterValues.Add(parameterValue);
			}

			if(CampaignId != 0) 
			{
				parameterValue = new ParameterValue();
				parameterValue.Name = "CampaignId";
				parameterValue.Value = CampaignId.ToString();
				parameterValues.Add(parameterValue);
			}

			parameterValue = new ParameterValue();
			parameterValue.Name = "ErrorType";
			parameterValue.Value = ErrorType;
			parameterValues.Add(parameterValue);

			rsGenerationDeadOrderReport.Generate(parameterValues);
		}
	}
}
