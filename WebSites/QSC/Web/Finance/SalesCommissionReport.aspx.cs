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
using Microsoft.ReportingServices.Interfaces;

namespace QSPFulfillment.Finance.Rpt
{
	///<summary>Link up to SalesCommissionReport rdl</summary>
	public class SalesCommissionReport : FinanceReportPage
	{
		private const int REPORT_TIMEOUT = 540000;

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
		protected QSPFulfillment.CommonWeb.UC.DateEntry				ucStartDate;
		protected QSPFulfillment.CommonWeb.UC.DateEntry				ucEndDate;
		protected QSPFulfillment.CommonWeb.UC.FieldManagerDDL		ucFMddl;
		protected QSPFulfillment.CommonWeb.UC.DivisionManagerDDL	ucDMddl;
		protected System.Web.UI.WebControls.DropDownList			ddlSectionType;
        protected System.Web.UI.WebControls.Label                   lblFieldManager;
		//private string p_FmId;
		//private string p_DmId;
		//private DateTime p_StartDate;
		//private DateTime p_EndDate;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationSalesCommissionReport;
		//private int p_SectionType;
		#endregion Item Declarations

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
                this.ucFMddl.Bind(1);

                if (QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999")
                {
                    SetValueFieldManager();
                }

				/*if( aUserProfile.HasRole("Finance")  )
				{
					this.ucFMddl.Bind(1); //mode = 1
				}
				else
				{
					//user is not allowed to see this page
					Response.Redirect("../Common/AccessDenied.aspx?p=Finance_Reports_SalesCommissionReport");
				}*/
			}
			else
			{
				this.ValSummary_TOP.HeaderText = QSPFulfillment.DataAccess.Common.Message.VALMSG_HEADER_TEXT_VAR_0;
			}
		}

        private void SetValueFieldManager()
        {
            this.ucFMddl.SelectedValue = QSPPage.aUserProfile.FMID;
            this.lblFieldManager.Text = this.ucFMddl.SelectedValue;

            this.ucFMddl.Visible = false;
            this.lblFieldManager.Visible = true;
        }

		#endregion Page_Load

		#region Postback process

		private void btSubmit_Click(object s, EventArgs e)
		{
			Page.Validate();
			if(!Page.IsValid)
			{
				this.MessageManager.Add("The page did not validate properly");
				this.SetPageError();
				return;
			}

            if (QSPPage.aUserProfile.IsFM && QSPPage.aUserProfile.FMID != "9999")
            {
                SetValueFieldManager();
            }

			bool blError = false;

			string FmId = "";
			try   { if(this.ucFMddl.SelectedValue.Trim() != "") { FmId = this.ucFMddl.SelectedValue; } }
			catch { this.MessageManager.Add("The FMID value is of an un-recognized format."); blError = true; }

			string DmId = "";
			try   { if(this.ucDMddl.SelectedValue.Trim() != "") { DmId = this.ucDMddl.SelectedValue; } }
			catch { this.MessageManager.Add("The DMID value is of an un-recognized format."); blError = true; }

			DateTime StartDate = this.ucStartDate.Date;
			if(StartDate == System.DateTime.MinValue)
			{
				this.MessageManager.Add("A valid start date must be entered");
				blError = true;
			}

			DateTime EndDate = this.ucEndDate.Date;
			if(EndDate == System.DateTime.MinValue)
			{
				this.MessageManager.Add("A valid end date must be entered");
				blError = true;
			}

			int	SectionType = 0;
			try   { if(this.ddlSectionType.SelectedValue.Trim() != "") { SectionType = Convert.ToInt32(this.ddlSectionType.SelectedValue); } }
			catch { this.MessageManager.Add("The Section Type value must be numeric."); blError = true; }

			if (blError == false)
			{
				//use the web service
				CallReport(FmId, DmId, StartDate, EndDate, SectionType);
				

				//OR use the report queue
				/*this.p_FmId = FmId;
				this.p_DmId = DmId;
				this.p_StartDate = StartDate;
				this.p_EndDate = EndDate;
				this.p_SectionType = SectionType;*/
				
				//Response.Write("FMID:" + this.p_FmId.ToString() + "<br>");
				//Response.Write("DMID:" + this.p_DmId.ToString() + "<br>");
				//Response.Write("StartDate:" + this.p_StartDate.ToShortDateString() + "<br>");
				//Response.Write("EndDate:" + this.p_EndDate.ToShortDateString() + "<br>");
				
				// Do not Create Subscription MS May 23, 07
				//this.SubmitReport();
			}
			else
			{
				this.SetPageError();
				return;
			}
		}


		private void CallReport(
			  string FmId
			, string DmId
			, DateTime StartDate
			, DateTime EndDate
			, int SectionType)
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;

			if(FmId.Trim() != "") {
				parameterValue = new ParameterValue();
				parameterValue.Name = "FmId";
				parameterValue.Value = FmId.ToString();
				parameterValues.Add(parameterValue);
			}

			if(DmId.Trim() != "") {
				parameterValue = new ParameterValue();
				parameterValue.Name = "DmId";
				parameterValue.Value = DmId.ToString();
				parameterValues.Add(parameterValue);
			}

			parameterValue = new ParameterValue();
			parameterValue.Name = "StartDate";
			parameterValue.Value = StartDate.ToShortDateString();
			parameterValues.Add(parameterValue);
			
			parameterValue = new ParameterValue();
			parameterValue.Name = "EndDate";
			parameterValue.Value = EndDate.ToShortDateString();
			parameterValues.Add(parameterValue);
			
			if(SectionType != 0) {
				parameterValue = new ParameterValue();
				parameterValue.Name = "SectionType";
				parameterValue.Value = SectionType.ToString();
				parameterValues.Add(parameterValue);
			}

			rsGenerationSalesCommissionReport.Generate(parameterValues, REPORT_TIMEOUT, "EXCEL");
		}


		/*protected void ReportParamsAndSubmit()
		{
			//reports are setup around ordermgt batches
			//fake this out, use the inverse of this users instance
			//int lBatchOrderIdP = abs(aUserProfile.Instance * -1;
			int lReportTypeIdP = (int)Business.enum_ReportType.Sales_Commission_Report;
			string sSubscriptionId;
			string sUsername = aUserProfile.UserName;
			string descFooter = "";

			Business.Report oReport = new Business.Report();


			Int32 lReportRequestId;
			int lReportIdP = (int)Business.enum_Report.Sales_Commission_Report;

			lReportRequestId = oReport.InsertReportRequest(
				lBatchOrderIdP 	//int batch order id - (has to change ?)
				,lReportTypeIdP //int batch type - (has to change ? )
				, ""			//string subscription id
				, sUsername);	//string modified by

			oReport.InsertReportRequestDetail(
				  lReportRequestId	//int lReportRequestId
				, lReportIdP		//int reportid (has to change ?)
				, sUsername);		//string  modified by

			QSPFulfillment.RDReportServer.ParameterValue[] inputParams = new QSPFulfillment.RDReportServer.ParameterValue[5];

			QSPFulfillment.RDReportServer.ParameterValue oPV = new QSPFulfillment.RDReportServer.ParameterValue();
			oPV.Name  = "FmId";
			if(this.p_FmId != "")
			{
				oPV.Value = this.p_FmId.ToString();
				descFooter += "FmId : " + oPV.Value;
			}
			else
			{
				oPV.Value = null;
			}
			inputParams[0] = oPV;

			oPV = new QSPFulfillment.RDReportServer.ParameterValue();
			oPV.Name  = "DmId";
			if(this.p_DmId != "")
			{
				oPV.Value = this.p_DmId.ToString();
				descFooter += "DmId : " + oPV.Value;
			}
			else
			{
				oPV.Value = null;
			}
			inputParams[1] = oPV;

			oPV = new QSPFulfillment.RDReportServer.ParameterValue();
			oPV.Name  = "StartDate";
			oPV.Value = this.p_StartDate.ToShortDateString();
			descFooter += "StartDate : " + oPV.Value;
			inputParams[2] = oPV;

			oPV = new QSPFulfillment.RDReportServer.ParameterValue();
			oPV.Name  = "EndDate";
			oPV.Value = this.p_EndDate.ToShortDateString();
			descFooter += "EndDate : " + oPV.Value;
			inputParams[3] = oPV;

			oPV = new QSPFulfillment.RDReportServer.ParameterValue();
			oPV.Name  = "SectionType";
			if(this.p_SectionType != 0)
			{
				oPV.Value = this.p_SectionType.ToString();
				descFooter += "SectionType : " + oPV.Value;
			}
			else
			{
				oPV.Value = null;
			}
			inputParams[4] = oPV;

			//extensionParams[0].Name = "FILENAME";
			extensionParams[0].Value = Convert.ToString(lReportRequestId);


			string sDESCRIPTION = "Finance Request #" + Convert.ToString(lReportRequestId) + " submitted by " + sUsername + "." ;
			if (descFooter != "")  {sDESCRIPTION += "Params: " + descFooter;}
			//sSubscriptionId = oRS.CreateSubscription(
			//	string report,extension settings, string descm string eventtype, string MatchData, array params);
			sSubscriptionId = oRS.CreateSubscription(
				"SalesCommissionReport"
				, oExt
				, sDESCRIPTION
				, eventType
				, scheduleXml
				, inputParams);

			oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);
			//Response.Write("FILE:" + lReportRequestId.ToString() + "<br>");
		}

		private void SubmitReport()
		{
			//step 1
			this.LogOn_to_ReportingServices();

			//step 2
			this.SubscriptionSetup();

			//step 3 - params setup and submit
			this.ReportParamsAndSubmit();

			//step 4 - go to the report listing
			Response.Redirect("FinanceReportStatus.aspx", false);
		}
		*/
		#endregion Postback process
	}
}
