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

namespace QSPFulfillment.OrderMgt
{
	///<summary>View a Batch record and related descriptive information</summary>
	public partial class BatchViewer : QSPFulfillment.CommonWeb.QSPPage
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
		#endregion Item Declarations
	
		protected void Page_Load(object s, System.EventArgs e)
		{
			PopulateBatch();
		}

		private void PopulateBatch()
		{
			Business.Batch oBatch = new Business.Batch();
			DataTable oDT = oBatch.GetBatchDetailsByOrderId(Convert.ToInt32(Request.QueryString["BatchOrderId"].ToString()));

			string sEnteredAmt = String.Format("{0:c}", oDT.Rows[0]["EnterredAmount"]);
			string sCalculatedAmt = String.Format("{0:c}", oDT.Rows[0]["CalculatedAmount"]);
			
			lblBatchId.Text = oDT.Rows[0]["Id"].ToString();
			lblBatchDate.Text = oDT.Rows[0]["Date"].ToString();
			lblStatus.Text = oDT.Rows[0]["StatusInstance"].ToString() + " " + oDT.Rows[0]["BatchStatus"].ToString();
			lblOrderId.Text = oDT.Rows[0]["OrderId"].ToString();
			lblAccountId.Text = oDT.Rows[0]["AccountId"].ToString() + " " + oDT.Rows[0]["AccountName"].ToString();
			lblEnterredAmount.Text = sEnteredAmt;
			lblCalculatedAmount.Text = sCalculatedAmt;
			lblEnterredCount.Text = oDT.Rows[0]["EnterredCount"].ToString();
			lblKE3.Text = oDT.Rows[0]["KE3Filename"].ToString();
			lblChangeUserId.Text = oDT.Rows[0]["ChangeUserId"].ToString();
			if (oDT.Rows[0]["ChangeDate"] != System.DBNull.Value) 
			{
				lblChangeDate.Text = Convert.ToDateTime(oDT.Rows[0]["ChangeDate"].ToString()).ToString();
			}
			lblStudentCount.Text = oDT.Rows[0]["StudentCount"].ToString();
			lblTeacherCount.Text = oDT.Rows[0]["TeacherCount"].ToString();
			lblOrderCount.Text = oDT.Rows[0]["OrderCount"].ToString();
			lblCustomerCount.Text = oDT.Rows[0]["CustomerCount"].ToString();
			lblOrderDetailCount.Text = oDT.Rows[0]["OrderDetailCount"].ToString();
			lblOrderCountAccept.Text = oDT.Rows[0]["OrderCountAccept"].ToString();
			lblStartImportTime.Text = oDT.Rows[0]["StartImportTime"].ToString();
			lblEndImportTime.Text = oDT.Rows[0]["EndImportTime"].ToString();
			lblImportTimeSeconds.Text = oDT.Rows[0]["ImportTimeSeconds"].ToString();
			lblClerk.Text = oDT.Rows[0]["Clerk"].ToString();
			lblCreateDate.Text = oDT.Rows[0]["DateCreated"].ToString();
			lblUserIdCreated.Text = oDT.Rows[0]["UserIdCreated"].ToString();
			lblDateKeyed.Text = oDT.Rows[0]["DateKeyed"].ToString();
			lblDateBatchCompleted.Text = oDT.Rows[0]["DateBatchCompleted"].ToString();
			lblOverridePct.Text = oDT.Rows[0]["OverridePctState"].ToString();
			lblPctState.Text = oDT.Rows[0]["PctState"].ToString();
			lblOriginalBatchStatus.Text = oDT.Rows[0]["OriginalStatusInstance"].ToString() + " " + oDT.Rows[0]["OriginalBatchStatus"].ToString();
			lblOrderType.Text = oDT.Rows[0]["OrderTypeCode"].ToString() + " " + oDT.Rows[0]["OrderTypeCodeDesc"].ToString();
			lblCampaignId.Text = oDT.Rows[0]["CampaignId"].ToString();
			lblShipToAddressId.Text = oDT.Rows[0]["ShipToAddressId"].ToString();
			lblBillToAddressId.Text = oDT.Rows[0]["BillToAddressId"].ToString();
			lblShipToAccountId.Text = oDT.Rows[0]["ShipToAccountId"].ToString();
			lblBillToFMId.Text = oDT.Rows[0]["BillToFMId"].ToString();
			lblShipToFMId.Text = oDT.Rows[0]["ShipToFMId"].ToString();			
			lblReportedEnvelopes.Text = oDT.Rows[0]["ReportedEnvelopes"].ToString();
			lblPaymentSend.Text = String.Format("{0:c}", oDT.Rows[0]["PaymentSend"]);
			lblSalesBeforeTax.Text = String.Format("{0:c}", oDT.Rows[0]["SalesBeforeTax"]);
			lblDateSent.Text = oDT.Rows[0]["DateSent"].ToString();
			lblDateReceived.Text = oDT.Rows[0]["DateReceived"].ToString();
			lblContactName.Text = oDT.Rows[0]["ContactFirstName"].ToString() + " " + oDT.Rows[0]["ContactLastName"].ToString();	
			lblContactEmail.Text = oDT.Rows[0]["ContactEmail"].ToString();
			lblContactPhone.Text = oDT.Rows[0]["ContactPhone"].ToString();
			lblComment.Text = oDT.Rows[0]["Comment"].ToString();
			lblIncentiveCalculationStatus.Text = oDT.Rows[0]["IncentiveCalculationStatusDesc"].ToString();
			lblMagnetBookletCount.Text = oDT.Rows[0]["MagnetBookletCount"].ToString();
			lblMagnetCardCount.Text = oDT.Rows[0]["MagnetCardCount"].ToString();
			lblMagnetGoodCardCount.Text = oDT.Rows[0]["MagnetGoodCardCount"].ToString();
			lblMagnetCardsMailed.Text = oDT.Rows[0]["MagnetCardsMailed"].ToString();
			lblMagnetMailDate.Text = oDT.Rows[0]["MagnetMailDate"].ToString();
			lblIsDMApproved.Text = oDT.Rows[0]["IsDMApproved"].ToString();
			lblCountryCode.Text = oDT.Rows[0]["CountryCode"].ToString();
			lblPickLine.Text = oDT.Rows[0]["PickLine"].ToString();
			lblOrderQualifier.Text = oDT.Rows[0]["OrderQualifierId"].ToString() + " " + oDT.Rows[0]["OrderQualifierDesc"].ToString();
			lblCheckPayableToQSPAmount.Text = String.Format("{0:c}", oDT.Rows[0]["CheckPayableToQSPAmount"]);
			lblIsIncentive.Text = oDT.Rows[0]["IsIncentive"].ToString();
			lblOrderDeliveryDate.Text = oDT.Rows[0]["OrderDeliveryDate"].ToString();
			lblRefNumber.Text = oDT.Rows[0]["RefNumber"].ToString();
			lblPaymentBatchDate.Text = oDT.Rows[0]["PaymentBatchDate"].ToString();
			lblPaymentBatchId.Text = oDT.Rows[0]["PaymentBatchId"].ToString();
			lblIsStaffOrder.Text = oDT.Rows[0]["IsStaffOrder"].ToString();
			lblInquireUponComplete.Text = oDT.Rows[0]["InquireUponComplete"].ToString();
			lblGroupProfit.Text = oDT.Rows[0]["GroupProfit"].ToString();
			lblOrderAmtDue.Text = oDT.Rows[0]["OrderAmntDue"].ToString();
			lblMagnetPostage.Text = oDT.Rows[0]["MagnetPostage"].ToString();
			lblOrderIdIncentive.Text = oDT.Rows[0]["OrderIdIncentive"].ToString();
			lblIsInvoiced.Text = oDT.Rows[0]["IsInvoiced"].ToString();
			lblCampaignNetTotal.Text = String.Format("{0:c}",oDT.Rows[0]["CampaignNetTotal"].ToString());
            lblProcessingFees.Text = String.Format("{0:c}", oDT.Rows[0]["ProcessingFees"].ToString());

			DataGrid1.DataSource = oBatch.GetCOHsByOrderId(Convert.ToInt32(Request.QueryString["BatchOrderId"].ToString()));
			DataGrid1.DataBind();
		}
	}
}
