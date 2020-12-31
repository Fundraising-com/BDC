using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business;
using QSPFulfillment.CommonWeb;


namespace QSPFulfillment.Reports
{
	/// <summary>
	/// Summary description for CAManagementReport.
	/// </summary>
	public class CAManagementReport : QSPFulfillment.CommonWeb.QSPPage
	{
		private const int REPORT_TIMEOUT = 180000;

		protected System.Web.UI.WebControls.Label lblCAStartDate;
		protected System.Web.UI.WebControls.Label lblFM;
		protected System.Web.UI.WebControls.Label lblProgram;
		protected System.Web.UI.WebControls.Label lblCAApprovedFrom;
		protected System.Web.UI.WebControls.Label lblCAApprovedTo;
		protected System.Web.UI.WebControls.Label lblCAStatus;
		protected System.Web.UI.WebControls.Label lblSortBy;
		protected System.Web.UI.WebControls.Label lblCAStartDateTo;	
		protected System.Web.UI.WebControls.Button PrintButton;
		protected QSPFulfillment.CommonWeb.UC.DateEntry CAStartDateFrom,CAStartDateTo;
		protected QSPFulfillment.CommonWeb.UC.DateEntry CAApproveDateFrom,CAApproveDateTo;
		protected QSPFulfillment.CommonWeb.UC.FieldManagerDDL ucFMddl1;
		protected System.Web.UI.WebControls.DropDownList ddlCAStatus;
		protected System.Web.UI.WebControls.DropDownList ddlSortBy;
		protected QSPFulfillment.CommonWeb.UC.ProgramsDDL ucProgramsDDL1;

        private string connStringCommon = "server=gasqlp02; uid=qspcafulfillment; pwd=fi11m3nt; database=QSPCanadaCommon";
		protected System.Web.UI.WebControls.Label lblErrorMessage;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblDeadOrderReport;
		protected System.Web.UI.WebControls.Label lblLoggedFMId;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationCAManagementReport;
		CodeHeader CAStatusCodeHeader = CodeHeader.CampaignStatus;


		private void Page_Load(object sender, System.EventArgs e)
		{
			// If a FM login disable DDL and show only the FMId
			if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM && 
				QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID != "9999")
			{
				ucFMddl1.Visible=false;
				this.lblLoggedFMId.Text = QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID;
			}			
			if (!Page.IsPostBack)
			{
				//this.CAApproveDateTo.Date = System.DateTime.Now;
				this.CAStartDateTo.Date	 = System.DateTime.Now;
				lblErrorMessage.Text="";
				populate_DDList();
			}
		}

		private void populate_DDList()
		{

			CAStatusCodeHeader = CodeHeader.CampaignStatus; 
			SqlConnection  Commonconn = new SqlConnection(connStringCommon);

			string CAStatusSelect = "select instance,Substring(description,(charindex('-',description,1)+1) ,20)description from codedetail where codeheaderinstance='"+(int)CAStatusCodeHeader+"' and instance in ( 37001,37002,37003,37004,37005,37006,37007) order by 2";
			SqlCommand cmdCAStatusSelect = new SqlCommand(CAStatusSelect, Commonconn);

			Commonconn.Open();
			SqlDataReader readerCAStatus = cmdCAStatusSelect.ExecuteReader();
			ddlCAStatus.DataSource = readerCAStatus;
			ddlCAStatus.DataBind();
			readerCAStatus.Close();
			Commonconn.Close();
			//ddlCAStatus.Items.Insert(0, new ListItem("", String.Empty));

			// populate field Manager and program dropdown

			this.ucFMddl1.Bind(1); //mode = 1

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
			this.ddlCAStatus.SelectedIndexChanged += new System.EventHandler(this.ddlCAStatus_SelectedIndexChanged);
			this.ddlSortBy.SelectedIndexChanged += new System.EventHandler(this.ddlSortBy_SelectedIndexChanged);
			this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void PrintButton_Click(object sender, System.EventArgs e)
		{
			lblErrorMessage.Text=" ";

			DateTime CADateFrom;
			DateTime CADateTo;
			DateTime CAApproveFrom;
			DateTime CAApproveTo;
			int FMId;
			int	ProgramId;
			int     CAStatusId;
			string  sortby;

			// CA Start Date can not be null	
			if (this.CAStartDateFrom.Date == System.DateTime.MinValue )
			{
				lblErrorMessage.Text = "Invalid CA date please correct";
				return;
			}
			else
			{
				CADateFrom = this.CAStartDateFrom.Date;
			}

			// CA End Date can not be null												
			if (this.CAStartDateTo.Date  == System.DateTime.MinValue)
			{
				lblErrorMessage.Text = "Invalid CA date please correct";
				return;
			}
			else
			{
				 CADateTo = this.CAStartDateTo.Date;
			
			}
			if (this.CAStartDateFrom.Date > this.CAStartDateTo.Date)
			{
				lblErrorMessage.Text = "Invalid CA Date, please correct ";
				return;
			}

			// Approve date from  is null 									
			if	(this.CAApproveDateFrom.Date  == System.DateTime.MinValue)
			{
				 CAApproveFrom = Convert.ToDateTime("01/01/1995");
			}
			else
			{
				 CAApproveFrom = this.CAApproveDateFrom.Date;
			
			}
			//Approve date to is null
			if (this.CAApproveDateTo.Date == System.DateTime.MinValue)
			{
				//DateTime CAApproveTo = Convert.ToDateTime("01/01/1995");
				//this.CAApproveDateTo.Date = System.DateTime.Now;
				//CAApproveTo = System.DateTime.Now;
				CAApproveTo = Convert.ToDateTime("01/01/1995");
			}
			else
			{
				 CAApproveTo   = this.CAApproveDateTo.Date;
			}
			// CA approve from date should be greater than approve to date
			if (this.CAApproveDateFrom.Date > this.CAApproveDateTo.Date)
			{
				lblErrorMessage.Text = "Invalid Approve date, please correct";
				return;
			}

			// FM id is optional parameter
			if (QSPFulfillment.CommonWeb.QSPPage.aUserProfile.IsFM && 
				QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID != "9999") 
			{
				FMId = Convert.ToInt32(QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID);
			}
			else
			// if not FM
			{
				if (ucFMddl1.SelectedValue == "")
				{
					FMId = 0;
				}
				else
				{
					FMId = Convert.ToInt32(this.ucFMddl1.SelectedValue);
				
				}
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
			
			//TextBox1.Text = Convert.ToString(ucProgramsDDL1.SelectedValue);
			CAStatusId	   =  Convert.ToInt32(this.ddlCAStatus.SelectedValue);
			sortby		   =  this.ddlSortBy.SelectedValue; 
   

			CallReport(CADateFrom,	CADateTo,CAApproveFrom, CAApproveTo, FMId,ProgramId, CAStatusId, sortby	);


		}

		private void CallReport(DateTime CADateFrom,
								DateTime CADateTo,
								DateTime CAApproveFrom,
								DateTime CAApproveTo,
								int FMId,
								int	ProgramId,
								int CAStatusId,
								string sortby	)
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;
			
			if(FMId != 0) 
			{
				parameterValue = new ParameterValue();
				parameterValue.Name = "FMID";
				parameterValue.Value = FMId.ToString();
				parameterValues.Add(parameterValue);
			}
			
			parameterValue = new ParameterValue();
			parameterValue.Name = "CAStartDate";
			parameterValue.Value = CADateFrom.ToShortDateString();
			parameterValues.Add(parameterValue);
			
			parameterValue = new ParameterValue();
			parameterValue.Name = "CAEndDate";
			parameterValue.Value = CADateTo.ToShortDateString();
			parameterValues.Add(parameterValue);
			
			if(CAApproveFrom.ToShortDateString() != "01/01/1995") 
			{
				parameterValue = new ParameterValue();
				parameterValue.Name = "DateApprovedFrom";
				parameterValue.Value = CAApproveFrom.ToShortDateString();
				parameterValues.Add(parameterValue);
			}

			if(CAApproveTo.ToShortDateString() != "01/01/1995") 
			{
				parameterValue = new ParameterValue();
				parameterValue.Name = "DateApprovedTo";
				parameterValue.Value = CAApproveTo.ToShortDateString();
				parameterValues.Add(parameterValue);
			}

			parameterValue = new ParameterValue();
			parameterValue.Name = "CAStatus";
			parameterValue.Value = CAStatusId.ToString();
			parameterValues.Add(parameterValue);

			if(ProgramId != 0) 
			{
				parameterValue = new ParameterValue();
				parameterValue.Name = "CAProgram";
				parameterValue.Value = ProgramId.ToString();
				parameterValues.Add(parameterValue);
			}

			parameterValue = new ParameterValue();
			parameterValue.Name = "SortBy";
			parameterValue.Value = sortby;
			parameterValues.Add(parameterValue);

			rsGenerationCAManagementReport.Generate(parameterValues, REPORT_TIMEOUT);
		}

		private void ddlCAStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		lblErrorMessage.Text = "";
		}

		private void ddlSortBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		lblErrorMessage.Text = "";
		}

		

	}
}
