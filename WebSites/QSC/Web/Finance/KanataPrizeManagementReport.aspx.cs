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
	/// Summary description for KanataPrizeManagementReport.
	/// </summary>
	public class KanataPrizeManagementReport :  QSPFulfillment.CommonWeb.QSPPage
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.DropDownList ddlSortBy;
		protected QSPFulfillment.CommonWeb.UC.FieldManagerDDL ucFMddl2;
		protected QSPFulfillment.CommonWeb.UC.ProgramsDDL ucProgramsDDL1;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ucOrdDateCreatedFrom,ucOrdDateCreatedTo;
	
		protected System.Web.UI.WebControls.Label lblErrorMessage;
		protected System.Web.UI.WebControls.TextBox tbAccountId;
		protected System.Web.UI.WebControls.TextBox tbCampaignId;
		protected System.Web.UI.WebControls.Button  PrintButton;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationKanataPrizeManagementReport;
		
		protected System.Web.UI.WebControls.Label Label1;
	
	
		private void Page_Load(object sender, System.EventArgs e)
		{
					
			if (!Page.IsPostBack)
			{
				lblErrorMessage.Text="";
				populate_DDList();
			}
		
		}
		private void populate_DDList()
		{

			// populate field Manager and program dropdown

			this.ucFMddl2.Bind(1); //mode = 1

			// populate program dropdown set required attribute to false
			this.ucProgramsDDL1.Mode = 1;
			this.ucProgramsDDL1.Required=false;
			this.ucProgramsDDL1.Bind();
		
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
			lblErrorMessage.Text=" ";

			DateTime OrderDateFrom;
			DateTime OrderDateTo;
			int FMId;
			int	ProgramId;
			int AccountId;
			int CAId;
			string  sortby;

			
			if (this.tbAccountId.Text == "")
			{
				AccountId = 0;
				}
			else
			{
				AccountId = Convert.ToInt32(this.tbAccountId.Text);
			}

			if (this.tbCampaignId.Text == "")
			{
				CAId = 0;
			}
			else
			{
				CAId = Convert.ToInt32(this.tbCampaignId.Text);
			}

			/////////////////If OrdercreatedDateFrom is Null 
			if (this.ucOrdDateCreatedFrom.Date  == System.DateTime.MinValue) 
			{
				lblErrorMessage.Text = "Order From date is required, please correct";
				return;
			}
			else
			{
				OrderDateFrom = this.ucOrdDateCreatedFrom.Date;
			}

			if (this.ucOrdDateCreatedTo.Date == System.DateTime.MinValue)
			{
				//if orderCreated date is null set it to current date
				this.ucOrdDateCreatedTo.Date = System.DateTime.Now;
				OrderDateTo = System.DateTime.Now;
				//OrderDateTo = Convert.ToDateTime("01/01/1995");
			}
			else
			{
				OrderDateTo   = this.ucOrdDateCreatedTo.Date;
			}
			//
			//if	(this.ucOrdDateCreatedFrom.Date  == System.DateTime.MinValue)
			//{
			//	OrderDateFrom = Convert.ToDateTime("01/01/1995");
			//}
			//else
			//{
			//	OrderDateFrom = this.ucOrdDateCreatedFrom.Date;
			
			//}

			if (this.ucOrdDateCreatedFrom.Date > this.ucOrdDateCreatedTo.Date)
			{
				lblErrorMessage.Text = "Invalid Order From/To Date, please correct ";
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
			
			// Program Id is optional
			if (ucProgramsDDL1.SelectedValue == -5)
			{
				ProgramId	   = 0;
			}
			else
			{
				ProgramId = this.ucProgramsDDL1.SelectedValue;
			}
			
			sortby		   =  this.ddlSortBy.SelectedValue; 
   

			CallReport(OrderDateFrom, OrderDateTo, FMId,ProgramId,AccountId,CAId,  sortby);
			


		}

		private void CallReport(DateTime OrderDateFrom,DateTime OrderDateTo,
			int FMId,
			int	ProgramId,
			int AccountId,
			int CAId,
			string sortby	)
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;

			if(FMId != 0) 
			{
				parameterValue = new ParameterValue("FMId", FMId.ToString());
				parameterValues.Add(parameterValue);
			}

			if(AccountId != 0) 
			{
				parameterValue = new ParameterValue("AccountId", AccountId.ToString());
				parameterValues.Add(parameterValue);
			}

			if(CAId != 0) 
			{
				parameterValue = new ParameterValue("CampaignId", CAId.ToString());
				parameterValues.Add(parameterValue);
			}

			if(OrderDateFrom.ToShortDateString() != "01/01/1995") 
			{
				parameterValue = new ParameterValue("OrderDateFrom", OrderDateFrom.ToShortDateString());
				parameterValues.Add(parameterValue);
			}

			if(OrderDateTo.ToShortDateString() != "01/01/1995") 
			{
				parameterValue = new ParameterValue("OrderDateTo", OrderDateTo.ToShortDateString());
				parameterValues.Add(parameterValue);
			}

			if(ProgramId != 0) 
			{
				parameterValue = new ParameterValue("CAProgram", ProgramId.ToString());
				parameterValues.Add(parameterValue);
			}

			parameterValue = new ParameterValue("SortBy", sortby);
			parameterValues.Add(parameterValue);

			rsGenerationKanataPrizeManagementReport.Generate(parameterValues);
		}

		
		private void ddlSortBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			lblErrorMessage.Text = "";
		}

		

		

		

	}
}
