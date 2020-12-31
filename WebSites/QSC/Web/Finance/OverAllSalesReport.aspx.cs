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

//using System.Web.Services.Protocols;
using Business.ReportExecution;
//using System.Runtime.InteropServices;
using Microsoft.ReportingServices.Interfaces;
//using Microsoft.Samples.ReportingServices.CustomSecurity;

namespace QSPFulfillment.Finance.Rpt
{
	///<summary>Link up to OverAllSalesReport rdl</summary>
	public class OverAllSalesReport : FinanceReportPage
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
		protected QSPFulfillment.CommonWeb.UC.DateEntry				ucStartDate;
		protected QSPFulfillment.CommonWeb.UC.DateEntry				ucEndDate;
		protected QSPFulfillment.CommonWeb.UC.FieldManagerDDL		ucFMddl;
		protected QSPFulfillment.CommonWeb.UC.DivisionManagerDDL	ucDMddl;
		//private int p_FmId;
		//private int p_DmId;
		//private DateTime p_StartDate;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationOverAllSalesReport;
		//private DateTime p_EndDate;
		#endregion Item Declarations

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				Server.ScriptTimeout = 90;
				//Finance Dept and Rosa Bavaro
				if( aUserProfile.HasRole("Finance") || aUserProfile.Instance == 333)
				{
					this.ucFMddl.Bind(1); //mode = 1
				}
				else
				{
					//user is not allowed to see this page
					Response.Redirect("../Common/AccessDenied.aspx?p=Finance_Reports_OverAllSalesReport");
				}
			}
			else
			{
				this.ValSummary_TOP.HeaderText = QSPFulfillment.DataAccess.Common.Message.VALMSG_HEADER_TEXT_VAR_0;
			}
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

			bool blError = false;

			int FmId = 0;
			try   { if(this.ucFMddl.SelectedValue.Trim() != "") { FmId = Convert.ToInt32(this.ucFMddl.SelectedValue); } }
			catch { this.MessageManager.Add("The FMID value is of an un-recognized format."); blError = true; }

			int DmId = 0;
			try   { if(this.ucDMddl.SelectedValue.Trim() != "") { DmId = Convert.ToInt32(this.ucDMddl.SelectedValue); } }
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

			if (blError == false)
			{
				//use the web service
				CallReport(FmId, DmId, StartDate, EndDate);


				//OR use the report queue
				//this.p_FmId = FmId;
				//this.p_DmId = DmId;
				//this.p_StartDate = StartDate;
				//this.p_EndDate = EndDate;
				//Response.Write("FMID:" + this.p_FmId.ToString() + "<br>");
				//Response.Write("DMID:" + this.p_DmId.ToString() + "<br>");
				//Response.Write("StartDate:" + this.p_StartDate.ToShortDateString() + "<br>");
				//Response.Write("EndDate:" + this.p_EndDate.ToShortDateString() + "<br>");
				//this.SubmitReport();
				
			}
			else
			{
				this.SetPageError();
				return;
			}
		}


		private void CallReport(
			  int FmId
			, int DmId
			, DateTime StartDate
			, DateTime EndDate)
		{
			QSPFulfillment.CommonWeb.ParameterValueCollection parameterValues = new QSPFulfillment.CommonWeb.ParameterValueCollection();
			QSPFulfillment.CommonWeb.ParameterValue parameterValue;

			if(FmId != 0) {
				parameterValue = new QSPFulfillment.CommonWeb.ParameterValue("FmId", FmId.ToString());
				parameterValues.Add(parameterValue);
			}

			if(DmId != 0) {
				parameterValue = new QSPFulfillment.CommonWeb.ParameterValue("DmId", DmId.ToString());
				parameterValues.Add(parameterValue);
			}

			parameterValue = new QSPFulfillment.CommonWeb.ParameterValue("StartDate", StartDate.ToShortDateString());
			parameterValues.Add(parameterValue);
			
			parameterValue = new QSPFulfillment.CommonWeb.ParameterValue("EndDate", EndDate.ToShortDateString());
			parameterValues.Add(parameterValue);
			
			rsGenerationOverAllSalesReport.Generate(parameterValues, 540000);
		}


		/*protected void ReportParamsAndSubmit()
		{
			//reports are setup around ordermgt batches
			//fake this out, use the inverse of this users instance
			//int lBatchOrderIdP = abs(aUserProfile.Instance * -1;
			int lReportTypeIdP = (int)Business.enum_ReportType.Over_All_Sales_Report;
			string sSubscriptionId;
			string sUsername = aUserProfile.UserName;
			string descFooter = "";

			Business.Report oReport = new Business.Report();


			Int32 lReportRequestId;
			int lReportIdP = (int)Business.enum_Report.Over_All_Sales_Report;

			lReportRequestId = oReport.InsertReportRequest(
				lBatchOrderIdP 	//int batch order id - (has to change ?)
				,lReportTypeIdP //int batch type - (has to change ? )
				, ""			//string subscription id
				, sUsername);	//string modified by

			oReport.InsertReportRequestDetail(
				  lReportRequestId	//int lReportRequestId
				, lReportIdP		//int reportid (has to change ?)
				, sUsername);		//string  modified by

			ParameterValue[] inputParams = new ParameterValue[4];

			ParameterValue oPV = new ParameterValue();
			oPV.Name  = "FmId";
			if(this.p_FmId != 0)
			{
				oPV.Value = this.p_FmId.ToString();
				descFooter += "FmId : " + oPV.Value;
			}
			else
			{
				oPV.Value = null;
			}
			inputParams[0] = oPV;

			oPV = new ParameterValue();
			oPV.Name  = "DmId";
			if(this.p_DmId != 0)
			{
				oPV.Value = this.p_DmId.ToString();
				descFooter += "DmId : " + oPV.Value;
			}
			else
			{
				oPV.Value = null;
			}
			inputParams[1] = oPV;

			oPV = new ParameterValue();
			oPV.Name  = "StartDate";
			oPV.Value = this.p_StartDate.ToShortDateString();
			descFooter += "StartDate : " + oPV.Value;
			inputParams[2] = oPV;

			oPV = new ParameterValue();
			oPV.Name  = "EndDate";
			oPV.Value = this.p_EndDate.ToShortDateString();
			descFooter += "EndDate : " + oPV.Value;
			inputParams[3] = oPV;

			//extensionParams[0].Name = "FILENAME";
			extensionParams[0].Value = Convert.ToString(lReportRequestId);

			string sDESCRIPTION = "Finance Request #" + Convert.ToString(lReportRequestId) + " submitted by " + sUsername + "." ;
			if (descFooter != "")  {sDESCRIPTION += "Params: " + descFooter;}
			//sSubscriptionId = oRS.CreateSubscription(
			//	string report,extension settings, string descm string eventtype, string MatchData, array params);
			sSubscriptionId = oRS.CreateSubscription(
				"OverAllSalesReport"
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