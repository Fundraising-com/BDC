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
	/// Summary description for AdjustmentDetailReport.
	/// </summary>
	public class AdjustmentDetailReport : QSPFulfillment.CommonWeb.QSPPage
	{
		protected System.Web.UI.WebControls.Label lblEffectiveDateFrom;
		protected System.Web.UI.WebControls.Label lblEffectiveDateTo;
		protected System.Web.UI.WebControls.Label lblAccountId;
		protected System.Web.UI.WebControls.Label lblOrderId;
		protected System.Web.UI.WebControls.Label lblAccountType;
		protected System.Web.UI.WebControls.Label lblAdjustmentType;
		protected System.Web.UI.WebControls.Label lblErrorMessage;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ucAdjustDateFrom,ucAdjustDateTo;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button PrintButton;
		protected System.Web.UI.WebControls.TextBox tbAccountId;
		protected System.Web.UI.WebControls.TextBox tbOrderId;
		protected System.Web.UI.WebControls.DropDownList ddlAdjustmentType;
		protected System.Web.UI.WebControls.DropDownList ddlAccountType;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationAdjustmentDetailReport;
		protected System.Web.UI.WebControls.Label lblAdjustmentDetailReport;
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if(aUserProfile.HasRole("Finance"))
				{
				
					lblErrorMessage.Text =" ";
					populate_DDList();
				}
				else
				{
					//user is not allowed to see this page
					Response.Redirect("../Common/AccessDenied.aspx?p=Finance_Reports_AdjustmentDetailReport");
			
				}
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

		private void populate_DDList()
		{
			int AcctTypeId = (int) Business.CodeHeader.CustomerType ;
			int AdjTypeId = (int)  Business.CodeHeader.AdjustmentType;

			DataSet AdjTypeds = new DataSet();
			DAL.CodeDetailDataAccess CDData =	new DAL.CodeDetailDataAccess();
			AdjTypeds = CDData.GetListFromHeader(AdjTypeId);
			ddlAdjustmentType.DataSource= AdjTypeds;
			ddlAdjustmentType.DataBind();
			ddlAdjustmentType.Items.Insert(0, new ListItem("All", String.Empty));
			
			
			DataSet AccTypeds = new DataSet();
			DAL.CodeDetailDataAccess CDData1 =	new DAL.CodeDetailDataAccess();
			AccTypeds =   CDData1.GetListFromHeader(AcctTypeId);			
			ddlAccountType.DataSource= AccTypeds;
			ddlAccountType.DataBind();
			ddlAccountType.Items.Insert(0, new ListItem("All", String.Empty));
	
		}


		private void PrintButton_Click(object sender, System.EventArgs e)
		{
			lblErrorMessage.Text =" ";
			DateTime AdjustmentDateFrom;
			DateTime AdjustmentDateTo;
			
			int OrderId;
			int	AccountId;
			int AccountTypeId;
			int AdjustmentTypeId;

			try
			{
				AccountTypeId = Convert.ToInt32(ddlAccountType.SelectedValue);
			}
			catch
			{
				// If user Select "ALL"
				
				{
					AccountTypeId = 0;
					
				}
				
			}

			
			//Account Id is optional

			if (this.tbAccountId.Text == "")
			{
				AccountId = 0;
			}
			else
			{
				AccountId = Convert.ToInt32(this.tbAccountId.Text);
				
			}

			// Adjustment from Date can not be null	
			if (this.ucAdjustDateFrom.Date == System.DateTime.MinValue )
			{
				lblErrorMessage.Text = "Invalid date, please correct";
				return;
			}
			else
			{
				AdjustmentDateFrom = this.ucAdjustDateFrom.Date;
			}

			// Adjustment end Date can not be null												
			if (this.ucAdjustDateTo.Date  == System.DateTime.MinValue)
			{
				this.ucAdjustDateTo.Date = System.DateTime.Now;
				AdjustmentDateTo = System.DateTime.Now;
				
			}
			else
			{
				AdjustmentDateTo = this.ucAdjustDateTo.Date;
			
			}
			if (this.ucAdjustDateFrom.Date > this.ucAdjustDateTo.Date)
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
			
			// Adjustment type select 			
			try
			{
				AdjustmentTypeId = Convert.ToInt32(ddlAdjustmentType.SelectedValue);
			}
			catch
			{
				// If user Select "ALL"
				
			{
				AdjustmentTypeId = 0;
					
			}
				
			}

			CallReport(AccountTypeId,AccountId,AdjustmentDateFrom,AdjustmentDateTo,OrderId,AdjustmentTypeId	);
	
		}

		private void CallReport(int AccountTypeId,int AccountId,
								DateTime AdjustmentDateFrom,
								DateTime AdjustmentDateTo,
								int OrderId,
								int  AdjustmentTypeId)
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;
			
			if(AccountTypeId != 0) 
			{
				parameterValue = new ParameterValue("AccountType", AccountTypeId.ToString());
				parameterValues.Add(parameterValue);
			}

			if(AccountId != 0) 
			{
				parameterValue = new ParameterValue("AccountId", AccountId.ToString());
				parameterValues.Add(parameterValue);
			}

			parameterValue = new ParameterValue("AdjEffectiveDateFrom", AdjustmentDateFrom.ToShortDateString());
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue("AdjEffectiveDateTo", AdjustmentDateTo.ToShortDateString());
			parameterValues.Add(parameterValue);

			if(OrderId != 0) 
			{
				parameterValue = new ParameterValue("OrderId", OrderId.ToString());
				parameterValues.Add(parameterValue);
			}
		
			
			if(AdjustmentTypeId != 0) 
			{
				parameterValue = new ParameterValue("AdjType", AdjustmentTypeId.ToString());
				parameterValues.Add(parameterValue);
			}

			rsGenerationAdjustmentDetailReport.Generate(parameterValues);
		}
	}
}
	

