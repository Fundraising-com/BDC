using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration; //For Web.Config
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services.Protocols;
using Business.ReportExecution;
using System.Runtime.InteropServices;
using Microsoft.ReportingServices.Interfaces;
using Business.Objects;
//using Microsoft.Samples.ReportingServices.CustomSecurity;

namespace QSPFulfillment.OrderMgt
{
	///<summary>PrintReports: Let user pick "one-off" reports to run</summary>
	public partial class PrintReports : QSPFulfillment.CommonWeb.QSPPage
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
		}
		#endregion auto-generated code

		#region	Item Declarations

		RSClient oRS = new RSClient();
		#endregion Item Declarations

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Server.ScriptTimeout = 60;
			if (!IsPostBack)
			{
				lblBatchOrderId.Text = Request.QueryString["BatchOrderId"].ToString();
			}

            if(aUserProfile.HasRole("HomeOffice")==false)
            {
                cbSubReport1.Enabled=false;
                cbSubReport2.Enabled=false;
                cbSubReport3.Enabled=false;
                cbSubReport4.Enabled=false;
                cbSubReport10.Enabled=false;
                cbSubReport13.Enabled=false;
            }

		}


		protected void Button2_Click(object sender, System.EventArgs e)
		{
/*			int lUserId = Convert.ToInt32(aUserProfile.Instance);
			string sUsername = aUserProfile.UserName;

			Business.PickList oPick = new Business.PickList();
			Business.Report oReport = new Business.Report();
			Business.Batch oBatch = new Business.Batch();

			Int32 lReportRequestId;
			Int32 lBatchId;
			string sBatchDate;
			Int32 lCampaignId;
			string sLanguage;

			string string1 = lblBatchOrderId.Text;
			string sBatchOrderId = lblBatchOrderId.Text;

			//get batch info
			DataTable oTable = oBatch.GetBatchByOrderId(Convert.ToInt32(string1),QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID);
			lBatchId = Convert.ToInt32(oTable.Rows[0]["BatchID"]);
			sBatchDate = oTable.Rows[0]["BatchDate"].ToString();
			lCampaignId = Convert.ToInt32(oTable.Rows[0]["CampaignId"]);
			sLanguage = Convert.ToString(oTable.Rows[0]["Language"]);

			#region Extension Parameters
			ParameterValue[] extensionParams = new ParameterValue[7];
			for(int i = 0; i < extensionParams.Length; i++)
			{
				extensionParams[i] = new ParameterValue();
			}

			extensionParams[0].Name = "FILENAME";
			extensionParams[0].Value = "";

			extensionParams[1].Name = "FILEEXTN";
			extensionParams[1].Value = "true";

			extensionParams[2].Name = "PATH";
			extensionParams[2].Value = sRSExportPath;

			extensionParams[3].Name = "RENDER_FORMAT";
			extensionParams[3].Value = "PDF";

			extensionParams[4].Name = "WRITEMODE";
			extensionParams[4].Value = "Overwrite";

			extensionParams[5].Name = "USERNAME";
			extensionParams[5].Value = sRSExportUsername;

			extensionParams[6].Name = "PASSWORD";
			extensionParams[6].Value = sRSExportPassword;


			ExtensionSettings oExt = new ExtensionSettings();
			oExt.Extension = "Report Server FileShare";
			oExt.ParameterValues = extensionParams;
			#endregion Extension Parameters

			DateTime oPlus5 = System.DateTime.Now.AddMinutes(5);
			string sDate = oPlus5.Year.ToString() + "-" + oPlus5.Month.ToString() + "-" + oPlus5.Day.ToString();
			string sTime = oPlus5.TimeOfDay.ToString();

			string eventType = "TimedSubscription";
			string scheduleXml = @"<ScheduleDefinition>";
			scheduleXml += @"<StartDateTime>" + sDate + "T" + sTime + "-05:00</StartDateTime></ScheduleDefinition>";

			string sSubscriptionId;

			#region PrintPickList
			//this item uses the same RDL as the Packing Slips
			//with a different ReportType parameter
			//and a different InsertReportRequestDetail parameter
			if (cbSubReport2.Checked)
			{
				lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 1, "", sUsername);

				oReport.InsertReportRequestDetail(lReportRequestId, 2, sUsername);

				ParameterValue[] inputParams2 = new ParameterValue[6];

				ParameterValue oB1 = new ParameterValue();
				oB1.Name = "OrderId";
				oB1.Value = Convert.ToString(string1);
				inputParams2[0] = oB1;

				ParameterValue oB2 = new ParameterValue();
				oB2.Name = "BatchId";
				oB2.Value = Convert.ToString(lBatchId);
				inputParams2[1] = oB2;

				ParameterValue oB3 = new ParameterValue();
				oB3.Name = "BatchDate";
				oB3.Value = Convert.ToString(sBatchDate);
				inputParams2[2] = oB3;

				ParameterValue oB4 = new ParameterValue();
				oB4.Name = "ReportType";
				oB4.Value = "1";//PickList(2 is Packing Slip)
				inputParams2[3] = oB4;

				ParameterValue oB5 = new ParameterValue();
				oB5.Name = "ShipDateFrom";
				oB5.Value = null;
				inputParams2[4] = oB5;

				ParameterValue oB6 = new ParameterValue();
				oB6.Name = "ShipDateTo";
				oB6.Value = null;
				inputParams2[5] = oB6;

				//extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = Convert.ToString(lReportRequestId);

				sSubscriptionId = oRS.CreateSubscription("PrintPickList", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams2);

				oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);
			}
			#endregion PrintPickList

			#region BHEShippingLabelsReport
			// BHE Labels
			if (cbSubReport4.Checked)
			{

				lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 3, "", sUsername);

				oReport.InsertReportRequestDetail(lReportRequestId, 4, sUsername);

				ParameterValue[] inputParams4 = new ParameterValue[1];

				ParameterValue oD1 = new ParameterValue();
				oD1.Name = "OrderID";
				oD1.Value = Convert.ToString(string1);
				inputParams4[0] = oD1;

				//extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = Convert.ToString(lReportRequestId);

				sSubscriptionId = oRS.CreateSubscription("BHEShippingLabelsReport", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams4);

				oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);


			}
			#endregion BHEShippingLabelsReport

			#region Participant Listing
			if (cbSubReport5.Checked)
			{

				lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 5, "", sUsername);

				oReport.InsertReportRequestDetail(lReportRequestId, 5, sUsername);

				ParameterValue[] inputParams5 = new ParameterValue[1];

				ParameterValue oE1 = new ParameterValue();
				oE1.Name = "OrderID";
				oE1.Value = Convert.ToString(string1);
				inputParams5[0] = oE1;

				//ParameterValue oE2 = new ParameterValue();
				//oE2.Name = "CampaignID";
				//oE2.Value = Convert.ToString(lCampaignId);
				//inputParams5[1] = oE2;

				//extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = Convert.ToString(lReportRequestId);

				if (sLanguage == "FR")
				{
					sSubscriptionId = oRS.CreateSubscription("ParticipantListingFrench_Online", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams5);
				}
				else
				{
					sSubscriptionId = oRS.CreateSubscription("ParticipantListing_Online", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams5);
				}
				oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);

			}
			#endregion Participant Listing

			#region Homeroom Summary Report
			if (cbSubReport6.Checked)
			{

				lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 6, "", sUsername);

				oReport.InsertReportRequestDetail(lReportRequestId, 6, sUsername);

				ParameterValue[] inputParams6 = new ParameterValue[3];

				ParameterValue oF1 = new ParameterValue();
				oF1.Name = "OrderId";
				oF1.Value = Convert.ToString(string1);
				inputParams6[0] = oF1;

				ParameterValue oF2 = new ParameterValue();
				oF2.Name = "BatchId";
				oF2.Value = Convert.ToString(lBatchId);
				inputParams6[1] = oF2;

				ParameterValue oF3 = new ParameterValue();
				oF3.Name = "BatchDate";
				oF3.Value = Convert.ToString(sBatchDate);
				inputParams6[2] = oF3;

				//extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = Convert.ToString(lReportRequestId);

				sSubscriptionId = oRS.CreateSubscription("HomeroomSummaryReport", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams6);

				oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);
			}
			#endregion Homeroom Summary Report

			#region Group Summary Report
			if (cbSubReport7.Checked)
			{

				lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 7, "", sUsername);

				oReport.InsertReportRequestDetail(lReportRequestId, 7, sUsername);

				ParameterValue[] inputParams7 = new ParameterValue[3];

				ParameterValue oG1 = new ParameterValue();
				oG1.Name = "OrderId";
				oG1.Value = Convert.ToString(string1);
				inputParams7[0] = oG1;

				ParameterValue oG2 = new ParameterValue();
				oG2.Name = "BatchId";
				oG2.Value = Convert.ToString(lBatchId);
				inputParams7[1] = oG2;

				ParameterValue oG3 = new ParameterValue();
				oG3.Name = "BatchDate";
				oG3.Value = Convert.ToString(sBatchDate);
				inputParams7[2] = oG3;

				//extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = Convert.ToString(lReportRequestId);

				sSubscriptionId = oRS.CreateSubscription("GroupSummaryReport", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams7);

				oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);
			}
			#endregion Group Summary Report

			#region Magazine Item Summary Report
			if (cbSubReport8.Checked)
			{

				lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 8, "", sUsername);

				oReport.InsertReportRequestDetail(lReportRequestId, 8, sUsername);

				ParameterValue[] inputParams8 = new ParameterValue[2];

				ParameterValue oH1 = new ParameterValue();
				oH1.Name = "OrderID";
				oH1.Value = Convert.ToString(string1);
				inputParams8[0] = oH1;

				ParameterValue oH2 = new ParameterValue();
				oH2.Name = "CampaignID";
				oH2.Value = Convert.ToString(lCampaignId);
				inputParams8[1] = oH2;

				//extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = Convert.ToString(lReportRequestId);

				if (sLanguage == "FR")
				{
					sSubscriptionId = oRS.CreateSubscription("MagazineItemsSummaryFrench", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams8);
				}
				else
				{
					sSubscriptionId = oRS.CreateSubscription("MagazineItemsSummary", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams8);
				}
				oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);
			}
			#endregion Magazine Item Summary Report

			#region ProblemSolverReport
			if (cbSubReport9.Checked)
			{

				lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 9, "", sUsername);

				oReport.InsertReportRequestDetail(lReportRequestId, 9, sUsername);

				ParameterValue[] inputParams9 = new ParameterValue[2];

				ParameterValue oI1 = new ParameterValue();
				oI1.Name = "OrderID";
				oI1.Value = Convert.ToString(string1);
				inputParams9[0] = oI1;

				ParameterValue oI2 = new ParameterValue();
				oI2.Name = "CampaignID";
				oI2.Value = Convert.ToString(lCampaignId);
				inputParams9[1] = oI2;

				//extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = Convert.ToString(lReportRequestId);

				if (sLanguage == "FR")
				{
					sSubscriptionId = oRS.CreateSubscription("ProblemSolverReportFrench", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams9);
				}
				else
				{
					sSubscriptionId = oRS.CreateSubscription("ProblemSolverReport", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams9);
				}
				oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);
			}
			#endregion ProblemSolverReport

			#region Classroom Box Labels
			if (cbSubReport10.Checked)
			{

				lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 4, "", sUsername);

				oReport.InsertReportRequestDetail(lReportRequestId, 10, sUsername);

				ParameterValue[] inputParams10 = new ParameterValue[3];

				ParameterValue oJ1 = new ParameterValue();
				oJ1.Name = "OrderID";
				oJ1.Value = Convert.ToString(string1);
				inputParams10[0] = oJ1;

				ParameterValue oJ2 = new ParameterValue();
				oJ2.Name = "TeacherID";
				oJ2.Value = null;
				inputParams10[1] = oJ2;

				ParameterValue oJ3 = new ParameterValue();
				oJ3.Name = "TotalLabels";
				oJ3.Value = null;
				inputParams10[2] = oJ3;

				//extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = Convert.ToString(lReportRequestId);

				sSubscriptionId = oRS.CreateSubscription("TeacherBoxLabelsReport", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams10);

				oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);
			}
			#endregion Classroom Box Labels

			#region Order Entry Followup Report
			if (cbSubReport11.Checked)
			{

				lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 11, "", sUsername);

				oReport.InsertReportRequestDetail(lReportRequestId, 11, sUsername);

				ParameterValue[] inputParams11 = new ParameterValue[3];

				ParameterValue oK1 = new ParameterValue();
				oK1.Name = "OrderID";
				oK1.Value = Convert.ToString(string1);
				inputParams11[0] = oK1;

				ParameterValue oK2 = new ParameterValue();
				oK2.Name = "AccountID";
				oK2.Value = null;
				inputParams11[1] = oK2;

				ParameterValue oK3 = new ParameterValue();
				oK3.Name = "CampaignID";
				oK3.Value = null;
				inputParams11[2] = oK3;

				//extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = Convert.ToString(lReportRequestId);

				sSubscriptionId = oRS.CreateSubscription("OrderEntryFollowupReport", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams11);

				oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);
			}
			#endregion Order Entry Followup Report

			#region Price Discrepancy Report
			if (cbSubReport12.Checked)
			{

				lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 12, "", sUsername);

				oReport.InsertReportRequestDetail(lReportRequestId, 12, sUsername);

				ParameterValue[] inputParams12 = new ParameterValue[5];

				ParameterValue oL1 = new ParameterValue();
				oL1.Name = "OrderID";
				oL1.Value = Convert.ToString(string1);
				inputParams12[0] = oL1;

				ParameterValue oL2 = new ParameterValue();
				oL2.Name = "AccountID";
				oL2.Value = null;
				inputParams12[1] = oL2;

				ParameterValue oL3 = new ParameterValue();
				oL3.Name = "CampaignID";
				oL3.Value = null;
				inputParams12[2] = oL3;

				ParameterValue oL4 = new ParameterValue();
				oL4.Name = "FMID";
				oL4.Value = null;
				inputParams12[3] = oL4;

				ParameterValue oL5 = new ParameterValue();
				oL5.Name = "InvoiceID";
				oL5.Value = null;
				inputParams12[4] = oL5;


				//extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = Convert.ToString(lReportRequestId);

				sSubscriptionId = oRS.CreateSubscription("PriceDiscrepancyReport", oExt, "One-off Request #" + Convert.ToString(lReportRequestId) + " submitted by " +sUsername + ".", eventType, scheduleXml, inputParams12);

				oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);
			}
			#endregion Price Discrepancy Report

			#region Packing Slip
			//this item uses the same RDL as the PickList
			//with a different ReportType parameter
			//and a different InsertReportRequestDetail parameter
			if (cbSubReport13.Checked)
			{
				lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 14, "", sUsername);

				oReport.InsertReportRequestDetail(lReportRequestId, 13, sUsername);

				ParameterValue[] inputParams13 = new ParameterValue[6];

				ParameterValue oB1 = new ParameterValue();
				oB1.Name = "OrderId";
				oB1.Value = Convert.ToString(string1);
				inputParams13[0] = oB1;

				ParameterValue oB2 = new ParameterValue();
				oB2.Name = "BatchId";
				oB2.Value = Convert.ToString(lBatchId);
				inputParams13[1] = oB2;

				ParameterValue oB3 = new ParameterValue();
				oB3.Name = "BatchDate";
				oB3.Value = Convert.ToString(sBatchDate);
				inputParams13[2] = oB3;

				ParameterValue oB4 = new ParameterValue();
				oB4.Name = "ReportType";
				oB4.Value = "2";//Packing Slip (1 is PickList)
				inputParams13[3] = oB4;

				ParameterValue oB5 = new ParameterValue();
				oB5.Name = "ShipDateFrom";
				oB5.Value = null;
				inputParams13[4] = oB5;

				ParameterValue oB6 = new ParameterValue();
				oB6.Name = "ShipDateTo";
				oB6.Value = null;
				inputParams13[5] = oB6;

				//extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = Convert.ToString(lReportRequestId);

				sSubscriptionId = oRS.CreateSubscription("PrintPickList", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams13);

				oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);
			}
			#endregion Packing Slip

			Response.Redirect("ReportStatus.aspx?BatchOrderId=" + sBatchOrderId, false);*/
		}
	}
}
