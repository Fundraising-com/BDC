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
using Business;
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment.Finance
{
	/// <summary>
	/// Summary description for InvoiceRegisterReport.
	/// </summary>
	public class InvoiceRegisterReport : QSPFulfillment.CommonWeb.QSPPage
	{
		private static int REPORT_TIMEOUT = 180000;

		protected System.Web.UI.WebControls.Label lblErrorMessage;
		protected System.Web.UI.WebControls.Button PrintButton;
		protected System.Web.UI.WebControls.Label lblInvDateFrom;
		protected System.Web.UI.WebControls.Label lblFM;
		protected System.Web.UI.WebControls.Label lblSortBy;
		protected System.Web.UI.WebControls.Label lblInvDateTo;
		protected System.Web.UI.WebControls.Label lblInvType;
		protected System.Web.UI.WebControls.DropDownList ddlSortBy;
		protected System.Web.UI.WebControls.DropDownList ddlInvoiceType;
		protected QSPFulfillment.CommonWeb.UC.FieldManagerDDL ucFMddl2;
		protected System.Web.UI.WebControls.Label lblReportDetail;
		protected System.Web.UI.WebControls.Label lblInvoiceRegisterReport;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ucInvDateFrom,ucInvDateTo;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationInvoiceRegisterReport;
	
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
					Response.Redirect("../Common/AccessDenied.aspx?p=Finance_Reports_InvoiceRegisterReport");
			
				}
			}
		}

		private void populate_DDList()
		{
			int InvoiceTypeId = (int) Business.CodeHeader.BatchType;
			
			DataSet InvTypeds = new DataSet();
			DAL.CodeDetailDataAccess CDData =	new DAL.CodeDetailDataAccess();
			InvTypeds = CDData.GetListFromHeader(InvoiceTypeId);
			ddlInvoiceType.DataSource= InvTypeds;
			ddlInvoiceType.DataBind();
			ddlInvoiceType.Items.Insert(0, new ListItem("All", String.Empty));
	
			// populate field Manager and program dropdown

			this.ucFMddl2.Bind(1); //mode = 1
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
			lblErrorMessage.Text = "";
			DateTime InvEffDateFrom;
			DateTime InvEffDateTo;
			
			int FMId;
			int InvTypeId;
			string SortBy;


			// Invoice effective from Date can not be null	
			if (this.ucInvDateFrom.Date == System.DateTime.MinValue )
			{
				lblErrorMessage.Text = "Invalid date, please correct";
				return;
			}
			else
			{
				InvEffDateFrom = this.ucInvDateFrom.Date;
			}

			// Invoice end Date can not be null												
			if (this.ucInvDateTo.Date  == System.DateTime.MinValue)
			{
				this.ucInvDateTo.Date = System.DateTime.Now;
				InvEffDateTo = System.DateTime.Now;
				
			}
			else
			{
				InvEffDateTo = this.ucInvDateTo.Date;
			
			}
			if (this.ucInvDateFrom.Date > this.ucInvDateTo.Date)
			{
				lblErrorMessage.Text = "Invalid date, please correct";
				return;
			}
			
			
			// FM id is optional parameter
			if (ucFMddl2.SelectedValue == "")
			{
				FMId = 0;
			}
			else
			{
				FMId = Convert.ToInt32(this.ucFMddl2.SelectedValue);
				
			}		

			//Invoice Type Debit Memo Credit Memo etc
			try
			{
				InvTypeId = Convert.ToInt32(ddlInvoiceType.SelectedValue);
			}
			catch
			{
				// If user Select "ALL"
				
			{
				InvTypeId = 0;
					
			}
			}
			
				SortBy		   =  this.ddlSortBy.SelectedValue; 
			
				CallReport(InvEffDateFrom,InvEffDateTo,FMId,InvTypeId,SortBy);
			}
		
			private void CallReport(DateTime InvEffDateFrom,
									DateTime InvEffDateTo,
									int FMId,
									int	InvTypeId,
									string sortby	)
			{
				ParameterValueCollection parameterValues = new ParameterValueCollection();
				ParameterValue parameterValue;

				parameterValue = new ParameterValue("InvoiceEffectivedateFrom", InvEffDateFrom.ToShortDateString());
				parameterValues.Add(parameterValue);

				parameterValue = new ParameterValue("InvoiceEffectivedateTo", InvEffDateTo.ToShortDateString());
				parameterValues.Add(parameterValue);

				if(FMId != 0) 
				{
					parameterValue = new ParameterValue("FMId", FMId.ToString());
					parameterValues.Add(parameterValue);
				}

				if(InvTypeId != 0) 
				{
					parameterValue = new ParameterValue("InvoiceType", InvTypeId.ToString());
					parameterValues.Add(parameterValue);
				}

				parameterValue = new ParameterValue("SortBy", sortby);

				rsGenerationInvoiceRegisterReport.Generate(parameterValues, REPORT_TIMEOUT);
			}
				
	}

	
}
	



