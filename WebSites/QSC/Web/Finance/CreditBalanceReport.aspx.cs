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
	/// Summary description for CreditBalanceReport.
	/// </summary>
	public class CreditBalanceReport : QSPFulfillment.CommonWeb.QSPPage
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected QSPFulfillment.CommonWeb.UC.DateEntry AsOf;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label lblSortBy;
		protected System.Web.UI.WebControls.Label lblAsOfDate;
		protected System.Web.UI.WebControls.Label lblAccount;
		protected System.Web.UI.WebControls.Label lblErrorMessage;
		protected System.Web.UI.WebControls.DropDownList ddlSortBy;
		protected System.Web.UI.WebControls.TextBox tbAccountId;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationCreditBalanceReport;
		protected System.Web.UI.WebControls.Label lblCreditBalanceReport;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//lblErrorMessage.Text=" ";

			DateTime AsOfDate;
			int Account;
			string  Orderby;

			if (this.AsOf.Date == System.DateTime.MinValue )
			{
				lblErrorMessage.Text = "Invalid date please correct";
				return;
			}
			else
			{
				AsOfDate = this.AsOf.Date;
			}

			if (this.tbAccountId.Text == "")
			{
				Account = 0;
			}
			else
			{
				Account = Convert.ToInt32(this.tbAccountId.Text);
				
			}

			Orderby =  this.ddlSortBy.SelectedValue; 

			CallReport(AsOfDate,Account,Orderby);
		}

		private void CallReport(DateTime AsOfDate,
								int AccountId,
								string sortby	)
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;

			parameterValue = new ParameterValue();
			parameterValue.Name = "AsOfDate";
			parameterValue.Value = AsOfDate.ToShortDateString();
			parameterValues.Add(parameterValue);

			if(AccountId != 0) 
			{
				parameterValue = new ParameterValue();
				parameterValue.Name = "AccountId";
				parameterValue.Value = AccountId.ToString();
				parameterValues.Add(parameterValue);
			}

			parameterValue = new ParameterValue();
			parameterValue.Name = "SortBy";
			parameterValue.Value = sortby;
			parameterValues.Add(parameterValue);

			rsGenerationCreditBalanceReport.Generate(parameterValues);
		}
	}
}
