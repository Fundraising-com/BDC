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
	/// </summary>
	public class AgingReportForFM :  QSPFulfillment.CommonWeb.QSPPage
	{
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label lblAccountId;
		protected System.Web.UI.WebControls.Label lblDateTo;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ucDateFrom,ucDateTo;
		protected System.Web.UI.WebControls.Button PrintButton;
		protected System.Web.UI.WebControls.Label lblErrorMessage;
		protected System.Web.UI.WebControls.TextBox tbAccountId;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationAgingReportByFM;
		protected System.Web.UI.WebControls.Label lblAccountFM;
		protected System.Web.UI.WebControls.Label lblLoggedFM;
		protected System.Web.UI.WebControls.Label lblAccountName;
		protected System.Web.UI.WebControls.TextBox tbAccount;
		//protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationAgingReportByFM;
		//protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationAgingReportByFMID;
		protected System.Web.UI.WebControls.Label lblAgingReportForFM;
		//protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationGroupSalesProfitReport;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// If a FM login disable DDL and show only the FMId
			if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM && 
				QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID != "9999")
			{
				this.lblLoggedFM.Text = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FullName;
			}
			else
			{this.lblLoggedFM.Text = "ANY FM";}


			if (!Page.IsPostBack)
			{
				//if(aUserProfile.HasRole("Finance"))
				{
				
					lblErrorMessage.Text =" ";
					//populate_DDList();
				}
				//else
				//{
					//user is not allowed to see this page
					//Response.Redirect("../Common/AccessDenied.aspx?p=Finance_Reports_GroupSalesProfitReport");
			
				//}
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
			lblErrorMessage.Text =" ";
			DateTime ReportDateTo;
			int	AccountId;

			if (this.tbAccountId.Text == "")
			{
				AccountId = 0;
			}
			else
			{
				AccountId = Convert.ToInt32(this.tbAccountId.Text);
				
			}
			
			// end Date can not be null												
			if (this.ucDateTo.Date  == System.DateTime.MinValue)
			{
				this.ucDateTo.Date = System.DateTime.Now;
				ReportDateTo = System.DateTime.Now;
				
			}
			else
			{
				ReportDateTo = this.ucDateTo.Date;
			
			}
			

			CallReport(AccountId,ReportDateTo,QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID  );
			
			}

			
	
		private void CallReport(int AccountId, DateTime ReportDateTo,  string FMID)

		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;

			parameterValue = new ParameterValue("AccountID", AccountId.ToString());
			parameterValues.Add(parameterValue);
					
			parameterValue = new ParameterValue("AsOfDate", ReportDateTo.ToShortDateString());
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue("FMID", FMID);
			parameterValues.Add(parameterValue);

			rsGenerationAgingReportByFM.Generate(parameterValues);
		}

		}
		}
	

