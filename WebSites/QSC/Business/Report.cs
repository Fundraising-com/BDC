using System;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
//using DAL;

namespace Business
{
	///<summary>MS Reporting Services</summary>
	public class Report : QBusinessObject
	{
		///<summary>default constructor</summary>
		public Report(){}
  
		///<summary>Insert A Report Request Detail </summary>
		///<param name="lReportRequestIdP">int: what ReportRequestId</param>
		///<param name="lReportIdP">int: what Report Id</param>
		///<param name="sModifiedByP">string: whose doing the insert</param>  
		///<returns>Nothing</returns>
		public void InsertReportRequestDetail(
			int lReportRequestIdP, 
			int lReportIdP,
			string sModifiedByP)
		{
			DAL.ReportDataAccess oReport = new DAL.ReportDataAccess();
			oReport.InsertReportRequestDetail(
				lReportRequestIdP
				, lReportIdP
				, sModifiedByP
				);
		}

		///<summary>Insert A Report Combination </summary>
		///<param name="lReportRequestIdP">int: what ReportRequestId</param>
		///<param name="lCombinedReportRequestIdP">int: what Report The Source Reports are being combined into</param>
		///<returns>Nothing</returns>
		public void InsertReportCombination(
			int lReportRequestIdP, 
			int lCombinedReportRequestIdP
			)
		{
			DAL.ReportDataAccess oReport = new DAL.ReportDataAccess();
			oReport.InsertReportCombination(
				lReportRequestIdP
				, lCombinedReportRequestIdP
				);
		}

		///<summary>Insert A Report Request</summary>
		///<param name="lBatchOrderIdP">int: The Order Id of the Batch</param>
		///<param name="lReportTypeIdP">int: what Report Type Id</param>
		///<param name="sRSSubscriptionIdP">int: The RS Subscripiton Id.  Just enter 0.  This will be updated after the call to RS.</param>
		///<param name="sModifiedByP">string: who is doing the insert</param>
		///<returns>Int with lReportRequestIdP</returns>
		public int InsertReportRequest(
			int lBatchOrderIdP,
			int lReportTypeIdP,
			string sRSSubscriptionIdP,
			string sModifiedByP
			)
		{
			DAL.ReportDataAccess oReport = new DAL.ReportDataAccess();
   
			return oReport.InsertReportRequest(
				lBatchOrderIdP
				, lReportTypeIdP
				, sRSSubscriptionIdP
				, sModifiedByP
				, 0
				);

   
		}

		///<summary>Batch Order Report Status</summary>
		///<param name="lBatchOrderIdP">int: The Order Id of the Batch</param>
		///<returns>DT full of Report History for given order</returns>
		public DataTable GetBatchReportStatus(
			int lBatchOrderIdP,
			string zFMIDP
			)
		{
			DAL.ReportDataAccess oReport = new DAL.ReportDataAccess();
   
			return oReport.BatchOrderReportStatus(
				lBatchOrderIdP, zFMIDP
				);

   
		}
  
		///<summary>Batch Order Report info</summary>
		///<param name="lBatchOrderIdP">int: The Order Id of the Batch</param>
		///<returns>DT full of Report History for given order</returns>
		public DataTable GetBatchReportsInfo(
			int lBatchOrderIdP,
            int? lShipmentGroupIDP,
			string zFMIDP
			)
		{
			DAL.ReportDataAccess oReport = new DAL.ReportDataAccess();
   
			return oReport.BatchOrderReportsInfo(
				lBatchOrderIdP, lShipmentGroupIDP, zFMIDP

				);

   
		}


        
		///<summary>Get QSP CA Report</summary>
		///<param name="lAccountIDP">int: AccountID</param>
		///<param name="lAccountIDP">string: FMID</param>
		///<returns>DT full of Report History for given order</returns>
		public DataTable GetQSPCAReportByAccountID(
			int lAccountIDP, string zFMIDP
			)
		{
			DAL.ReportDataAccess oReport = new DAL.ReportDataAccess();
   
			return oReport.GetQSPCAReportByAccountID(
				lAccountIDP, zFMIDP
				);

   
		}
  
		///<summary>Get QSP CA Report</summary>
		///<param name="lAccountIDP">int: CampaignID</param>
		///<param name="lAccountIDP">string: FMID</param>
		///<returns>DT full of Report History for given order</returns>
		public DataTable GetQSPCAReportByCampaignID(
			int lCampaignIDP, string zFMIDP
			)
		{
			DAL.ReportDataAccess oReport = new DAL.ReportDataAccess();
   
			return oReport.GetQSPCAReportByCampaignID(
				lCampaignIDP, zFMIDP
				);

   
		}
  
        
		///<summary>Get QSP CA Report</summary>
		///<param name="lAccountIDP">int: CampaignID</param>
		///<param name="lAccountIDP">string: FMID</param>
		///<returns>DT full of Report History for given order</returns>
		public DataTable GetQSPCAReportByAccountName(
			string zAccountNameP, string zFMIDP
			)
		{
			DAL.ReportDataAccess oReport = new DAL.ReportDataAccess();
   
			return oReport.GetQSPCAReportByAccountName(
				zAccountNameP, zFMIDP
				);

   
		}

		///<summary>Update SubscriptionId for Report Request</summary>
		///<param name="lReportRequestIdP">int: what ReportRequestId</param>
		///<param name="sRSSubscriptionIdP">string: what RS Subscription Id</param>
		///<returns>Nothing</returns>
		public void UpdateSubscriptionId(
			int lReportRequestIdP, 
			string sRSSubscriptionIdP)
		{
			DAL.ReportDataAccess oReport = new DAL.ReportDataAccess();
			oReport.UpdateSubscriptionId(
				lReportRequestIdP
				, sRSSubscriptionIdP
				);
		}

  
		///<summary>check it then submit it</summary>
		///<returns>bool: Was validation then saving successful ? </returns>
		override public bool ValidateAndSave()
		{
			if(this.Validate() == true)
			{
				return this.Save();
			}
			else
			{
				return false;
			}
		}

   
		///<summary>Check for compliance with biz rules</summary>
		///<returns>bool: Was validation successful ? </returns>
		public bool Validate()
		{
			/* setup variables to track validation */
			bool blValid = true;

			return blValid;
		}


		///<summary>Save to the db</summary>
		///<returns>bool: Was saving successful ? </returns>
		public bool Save()
		{
			bool blSave = false;
   
			return blSave;
		}
	}


}
