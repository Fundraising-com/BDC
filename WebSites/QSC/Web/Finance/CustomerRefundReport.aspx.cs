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

namespace QSPFulfillment.Finance
{
	/// <summary>
	/// Summary description for CustomerRefundReport.
	/// </summary>
	public class CustomerRefundReport : QSPFulfillment.CommonWeb.QSPPage
	{
		protected System.Web.UI.WebControls.Label lblReportDetail;
		protected System.Web.UI.WebControls.Label lblErrorMessage;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ucCreateDateFrom, ucCreateDateTo;
		protected QSPFulfillment.CommonWeb.UC.StateProvince ucProvinceddl;
		protected System.Web.UI.WebControls.Button PrintButton;
		protected System.Web.UI.WebControls.TextBox tbAmountFrom;
		protected System.Web.UI.WebControls.TextBox tbAmountTo;
		protected System.Web.UI.WebControls.DropDownList ddlSortBy;
		protected System.Web.UI.WebControls.Label lblRefundDateFrom;
		protected System.Web.UI.WebControls.Label lblRefundTo;
		protected System.Web.UI.WebControls.Label lblAmountFrom;
		protected System.Web.UI.WebControls.Label lblAmountTo;
		protected System.Web.UI.WebControls.Label lblProvince;
		protected System.Web.UI.WebControls.Label lblSortBy;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationCustomerRefundReport;
		protected System.Web.UI.WebControls.Label lblCustomerRefundReport;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if(aUserProfile.HasRole("Finance"))
				{
				   lblErrorMessage.Visible =false;
				
				}
				else
				{
					//user is not allowed to see this page
					Response.Redirect("../Common/AccessDenied.aspx?p=Finance_Reports_CustomerRefundReport");
			
				}
			}
			lblErrorMessage.Visible =false;
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
			lblErrorMessage.Visible =false;

			DateTime CreateDateFrom;
			DateTime CreateDateTo;
			decimal  RefundAmountFrom;
			decimal  RefundAmountTo;
			string  SortBy;
			string Province;

			// Refund Amount from is optional parameter
			if (this.tbAmountFrom.Text == "")
			{
				RefundAmountFrom = 0.00M;
			}
			else
			{
				RefundAmountFrom = Convert.ToDecimal(this.tbAmountFrom.Text);
				
			}
			// Refund Amount to is optional parameter
			if (this.tbAmountTo.Text == "")
			{
				RefundAmountTo = 0.00M;
			}
			else
			{
				RefundAmountTo = Convert.ToDecimal(this.tbAmountTo.Text);
				
			}

			Province = this.ucProvinceddl.Value;
			

			CreateDateFrom = this.ucCreateDateFrom.Date;

			// CreateDate to is null											
			if (this.ucCreateDateTo.Date  == System.DateTime.MinValue) 
				
			{
				this.ucCreateDateTo.Date = System.DateTime.Now;
				CreateDateTo = System.DateTime.Now;
				
			}
			else
			{
				CreateDateTo = this.ucCreateDateTo.Date;
			}

			if  (this.ucCreateDateFrom.Date > this.ucCreateDateTo.Date)
				
			{
				lblErrorMessage.Visible =true;
				lblErrorMessage.Text = "Invalid Create From/To combination date, please correct";
				return;
			}

			SortBy =  this.ddlSortBy.SelectedValue; 
   
			CallReport(RefundAmountFrom, RefundAmountTo, CreateDateFrom, CreateDateTo, Province, SortBy);
	
		}

		private void CallReport(decimal  RefundAmountFrom, decimal RefundAmountTo, DateTime CreateDateFrom,	DateTime CreateDateTo, string Province, string SortBy)
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;

			parameterValue = new ParameterValue("DateCreatedFrom", CreateDateFrom.ToShortDateString());
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue("DateCreatedTo", CreateDateTo.ToShortDateString());
			parameterValues.Add(parameterValue);

			if(RefundAmountFrom.ToString() != "") 
			{
				parameterValue = new ParameterValue("RefundAmountFrom", RefundAmountFrom.ToString());
				parameterValues.Add(parameterValue);
			}

			if(RefundAmountTo.ToString() != "") 
			{
				parameterValue = new ParameterValue("RefundAmountTo", RefundAmountTo.ToString());
				parameterValues.Add(parameterValue);
			}

			if(Province != "") 
			{
				parameterValue = new ParameterValue("Province", Province);
				parameterValues.Add(parameterValue);
			}
			
			parameterValue = new ParameterValue("SortBy", SortBy);
			parameterValues.Add(parameterValue);
			
			rsGenerationCustomerRefundReport.Generate(parameterValues);
		}
	}
}
