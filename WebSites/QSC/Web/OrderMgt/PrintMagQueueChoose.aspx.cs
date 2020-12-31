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
	///<summary>PrintMagQueueChoose:</summary>
	public partial class PrintMagQueueChoose : QSPFulfillment.CommonWeb.QSPPage
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

		#region Item Declarations

		RSClient oRS = new RSClient();

		#endregion Item Declarations


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//cbSubReport1.Attributes.Add("onClick", "javascript: if (Form1." + cbSubReport1.ClientID + ".checked == false) { var bAnswer; bAnswer = confirm('This is a required report. Are you sure you want to remove it?'); if (bAnswer == false){Form1." + cbSubReport1.ClientID + ".checked = true;}}");
			//cbSubReport2.Attributes.Add("onClick", "javascript: if (Form1." + cbSubReport2.ClientID + ".checked == false) { var bAnswer; bAnswer = confirm('This is a required report. Are you sure you want to remove it?'); if (bAnswer == false){Form1." + cbSubReport2.ClientID + ".checked = true;}}");
			//cbSubReport3.Attributes.Add("onClick", "javascript: if (Form1." + cbSubReport3.ClientID + ".checked == false) { var bAnswer; bAnswer = confirm('This is a required report. Are you sure you want to remove it?'); if (bAnswer == false){Form1." + cbSubReport3.ClientID + ".checked = true;}}");
			//cbSubReport4.Attributes.Add("onClick", "javascript: if (Form1." + cbSubReport4.ClientID + ".checked == false) { var bAnswer; bAnswer = confirm('This is a required report. Are you sure you want to remove it?'); if (bAnswer == false){Form1." + cbSubReport4.ClientID + ".checked = true;}}");
			Server.ScriptTimeout = 60;

			if(!IsPostBack) 
			{
				PopulateDG();
			}
						
		}

		private void PopulateDG()
		{
				
			Business.PickList oPick = new Business.PickList();

			DataGrid1.DataSource = oPick.GetMagQueueOrdersByList(Session["QSPF_OrderList"].ToString());
			DataGrid1.DataBind();
	
		}

		private bool IsInList(string pString) 
		{
			bool bFound = false;
			string sString = (string)Session["QSPF_OrderList"];
			
			string[] aString = sString.Split(new char[] {','});

			foreach (string string1 in aString)
			{
				if (string1.Equals(pString)) 
				{
					bFound = true;
				}
			}
			
			return bFound;
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			/*if ((string)Session["QSPF_OrderList"] == "") 
			{
				Response.Redirect("Login.aspx", false);
			}

			int lUserId = Convert.ToInt32(aUserProfile.Instance);
			string sUsername = aUserProfile.UserName;

			string sString = (string)Session["QSPF_OrderList"];			
			string[] aString = sString.Split(new char[] {','});
			Business.PickList oPick = new Business.PickList();
			Business.Report oReport = new Business.Report();
			Business.Batch oBatch = new Business.Batch();

			Int32 lReportRequestId;
			Int32 lCombinedReportRequestId;
			Int32 lBatchId;
			string sBatchDate;
			Int32 lCampaignId;
			string sLanguage;
				
			foreach (string string1 in aString)
			{

				DataTable oTable = oBatch.GetBatchByOrderId(Convert.ToInt32(string1),QSPFulfillment.CommonWeb.QSPPage.aUserProfile.FMID);
				lBatchId = Convert.ToInt32(oTable.Rows[0]["BatchID"]);
				sBatchDate = oTable.Rows[0]["BatchDate"].ToString();
				lCampaignId = Convert.ToInt32(oTable.Rows[0]["CampaignId"]);
				sLanguage = Convert.ToString(oTable.Rows[0]["Language"]);
				oTable.Dispose();

				// Print Reports
				lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 13, "", sUsername);
				lCombinedReportRequestId = lReportRequestId;
				
				if (cbSubReport1.Checked) 
				{
					oReport.InsertReportRequestDetail(lReportRequestId, 1, sUsername);
				}

				if (cbSubReport2.Checked) 
				{
					oReport.InsertReportRequestDetail(lReportRequestId, 2, sUsername);
				}

				if (cbSubReport3.Checked) 
				{
					oReport.InsertReportRequestDetail(lReportRequestId, 3, sUsername);
				}

				if (cbSubReport5.Checked) 
				{
					oReport.InsertReportRequestDetail(lReportRequestId, 5, sUsername);
				}

				if (cbSubReport6.Checked) 
				{
					oReport.InsertReportRequestDetail(lReportRequestId, 6, sUsername);
					oReport.InsertReportRequestDetail(lReportRequestId, 6, sUsername);
				}

				if (cbSubReport7.Checked) 
				{
					oReport.InsertReportRequestDetail(lReportRequestId, 7, sUsername);
					oReport.InsertReportRequestDetail(lReportRequestId, 7, sUsername);
				}

				if (cbSubReport8.Checked) 
				{
					oReport.InsertReportRequestDetail(lReportRequestId, 8, sUsername);
				}

				if (cbSubReport9.Checked) 
				{
					oReport.InsertReportRequestDetail(lReportRequestId, 9, sUsername);
				}

				if (cbSubReport11.Checked) 
				{
					oReport.InsertReportRequestDetail(lReportRequestId, 11, sUsername);
				}

				if (cbSubReport12.Checked) 
				{
					oReport.InsertReportRequestDetail(lReportRequestId, 12, sUsername);
				}


				

				ParameterValue[] extensionParams = new ParameterValue[7];
				for(int i = 0; i < extensionParams.Length; i++)
					extensionParams[i] = new ParameterValue();
			
				extensionParams[0].Name = "FILENAME";
				extensionParams[0].Value = Convert.ToString(lReportRequestId);

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

				DateTime oPlus5 = System.DateTime.Now.AddMinutes(5);
				string sDate = oPlus5.Year.ToString() + "-" + oPlus5.Month.ToString() + "-" + oPlus5.Day.ToString();
				string sTime = oPlus5.TimeOfDay.ToString();

				string eventType = "TimedSubscription";
				string scheduleXml = @"<ScheduleDefinition>";
				scheduleXml += @"<StartDateTime>" + sDate + "T" + sTime + "-05:00</StartDateTime></ScheduleDefinition>";

				string sSubscriptionId;
				//string sSubscriptionId = oRS.CreateSubscription("PickMain", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams);

				//oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);


				// PickList Reports
				if (cbSubReport2.Checked) 
				{
					
					lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 1, "", sUsername);
						
					oReport.InsertReportRequestDetail(lReportRequestId, 2, sUsername);
					oReport.InsertReportCombination(lReportRequestId, lCombinedReportRequestId);

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
					oB4.Value = "1";
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

					sSubscriptionId = oRS.CreateSubscription("PrintPickList", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams2);
			
					oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);


				}


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

					sSubscriptionId = oRS.CreateSubscription("BHEShippingLabelsReport", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams4);
			
					oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);


				}

				// Participant Listing
				if (cbSubReport5.Checked) 
				{
					lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 5, "", sUsername);
						
					oReport.InsertReportRequestDetail(lReportRequestId, 5, sUsername);
					oReport.InsertReportCombination(lReportRequestId, lCombinedReportRequestId);

					ParameterValue[] inputParams5 = new ParameterValue[1];
					
					ParameterValue oE1 = new ParameterValue();
					oE1.Name = "OrderID";
					oE1.Value = Convert.ToString(string1);
					inputParams5[0] = oE1;
				
					extensionParams[0].Value = Convert.ToString(lReportRequestId);

					if (sLanguage == "FR") 
					{
						sSubscriptionId = oRS.CreateSubscription("ParticipantListingFrench_Online", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams5);
					}
					else
					{
						sSubscriptionId = oRS.CreateSubscription("ParticipantListing_Online", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams5);
					}
					oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);

					
				}

				// Homeroom Summary Report
				if (cbSubReport6.Checked) 
				{
					
					lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 6, "", sUsername);
						
					oReport.InsertReportRequestDetail(lReportRequestId, 6, sUsername);
					oReport.InsertReportCombination(lReportRequestId, lCombinedReportRequestId);

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

					sSubscriptionId = oRS.CreateSubscription("HomeroomSummaryReport", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams6);
			
					oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);

					//Print Second Copy
					lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 6, "", sUsername);
						
					oReport.InsertReportRequestDetail(lReportRequestId, 6, sUsername);
					oReport.InsertReportCombination(lReportRequestId, lCombinedReportRequestId);
					
					//extensionParams[0].Name = "FILENAME";
					extensionParams[0].Value = Convert.ToString(lReportRequestId);

					sSubscriptionId = oRS.CreateSubscription("HomeroomSummaryReport", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams6);
			
					oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);

				}

				// Group Summary Report
				if (cbSubReport7.Checked) 
				{
					
					lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 7, "", sUsername);
						
					oReport.InsertReportRequestDetail(lReportRequestId, 7, sUsername);
					oReport.InsertReportCombination(lReportRequestId, lCombinedReportRequestId);

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

					sSubscriptionId = oRS.CreateSubscription("GroupSummaryReport", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams7);
			
					oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);

					// second print

					lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 7, "", sUsername);
						
					oReport.InsertReportRequestDetail(lReportRequestId, 7, sUsername);
					oReport.InsertReportCombination(lReportRequestId, lCombinedReportRequestId);

					//extensionParams[0].Name = "FILENAME";
					extensionParams[0].Value = Convert.ToString(lReportRequestId);

					sSubscriptionId = oRS.CreateSubscription("GroupSummaryReport", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams7);
			
					oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);



				}

				// Magazine Item Summary Report
				if (cbSubReport8.Checked) 
				{
					
					lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 8, "", sUsername);
						
					oReport.InsertReportRequestDetail(lReportRequestId, 8, sUsername);
					oReport.InsertReportCombination(lReportRequestId, lCombinedReportRequestId);

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
						sSubscriptionId = oRS.CreateSubscription("MagazineItemsSummaryFrench", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams8);
					}
					else
					{
						sSubscriptionId = oRS.CreateSubscription("MagazineItemsSummary", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams8);
					}
					oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);


				}

				// Problem Summary Report
				if (cbSubReport9.Checked) 
				{
					
					lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 9, "", sUsername);
						
					oReport.InsertReportRequestDetail(lReportRequestId, 9, sUsername);
					oReport.InsertReportCombination(lReportRequestId, lCombinedReportRequestId);

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
						sSubscriptionId = oRS.CreateSubscription("ProblemSolverReportFrench", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams9);
					}
					else
					{
						sSubscriptionId = oRS.CreateSubscription("ProblemSolverReport", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams9);
					}
					oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);


				}

				// Classroom Box Labels
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

					sSubscriptionId = oRS.CreateSubscription("TeacherBoxLabelsReport", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams10);
			
					oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);


				}

				// Order Entry Followup Report
				if (cbSubReport11.Checked) 
				{
					
					lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 11, "", sUsername);
						
					oReport.InsertReportRequestDetail(lReportRequestId, 11, sUsername);
					oReport.InsertReportCombination(lReportRequestId, lCombinedReportRequestId);

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

					sSubscriptionId = oRS.CreateSubscription("OrderEntryFollowupReport", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams11);
			
					oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);


				}

				// Price Discrepancy Report
				if (cbSubReport12.Checked) 
				{
					
					lReportRequestId = oReport.InsertReportRequest(Convert.ToInt32(string1), 12, "", sUsername);
						
					oReport.InsertReportRequestDetail(lReportRequestId, 12, sUsername);
					oReport.InsertReportCombination(lReportRequestId, lCombinedReportRequestId);

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

					sSubscriptionId = oRS.CreateSubscription("PriceDiscrepancyReport", oExt, "Request #" + Convert.ToString(lReportRequestId), eventType, scheduleXml, inputParams12);
			
					oReport.UpdateSubscriptionId(lReportRequestId, sSubscriptionId);


				}

				oPick.ProcessMagQueue(Convert.ToInt32(Convert.ToInt32(string1)));
				
			}
			
			
			Response.Redirect("PrintMagQueueProcessed.aspx", false);
            */
		}
	}
}
