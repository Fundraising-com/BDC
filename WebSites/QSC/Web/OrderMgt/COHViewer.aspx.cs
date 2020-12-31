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
	/// <summary>
	/// Summary description for COHViewer.
	/// </summary>
	public partial class COHViewer :  QSPFulfillment.CommonWeb.QSPPage
	{
		protected System.Web.UI.WebControls.Label lblPickDate;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			PopulateBatchOrder();
		}

		private void PopulateBatchOrder()
		{
			Business.Batch oBatch = new Business.Batch();
			DataTable oDT = oBatch.GetCOHByCOHId(Convert.ToInt32(Request.QueryString["COHId"].ToString()));

			lblBatchId.Text = oDT.Rows[0]["OrderBatchId"].ToString();
			lblBatchDate.Text = oDT.Rows[0]["OrderBatchDate"].ToString();
			lblAccountId.Text = oDT.Rows[0]["AccountId"].ToString() + " " + oDT.Rows[0]["AccountName"].ToString();
			lblChangeUserId.Text = oDT.Rows[0]["ChangeUserId"].ToString();
			if (oDT.Rows[0]["ChangeDate"] != System.DBNull.Value) 
			{
				lblChangeDate.Text = Convert.ToDateTime(oDT.Rows[0]["ChangeDate"].ToString()).ToString();
			}
			lblStudent.Text = oDT.Rows[0]["StudentInstance"].ToString() + " " + oDT.Rows[0]["StudentName"].ToString();
			lblCustomerBillToInstance.Text = oDT.Rows[0]["CustomerBillToInstance"].ToString();
			lblStatus.Text = oDT.Rows[0]["StatusInstance"].ToString() + " " + oDT.Rows[0]["StatusDesc"].ToString();
			lblFirstStatus.Text = oDT.Rows[0]["FirstStatusInstance"].ToString() + " " + oDT.Rows[0]["FirstStatusDesc"].ToString();
			lblTotalProcessingFee.Text = String.Format("{0:c}", oDT.Rows[0]["TotalProcessingFee"]);
			lblTotalProcessingFeeA.Text = String.Format("{0:c}", oDT.Rows[0]["TotalProcessingFeeA"]);
			lblProcessingFeeTax.Text = String.Format("{0:c}", oDT.Rows[0]["ProcessingFeeTax"]);
			lblProcessingFeeTaxA.Text = String.Format("{0:c}", oDT.Rows[0]["ProcessingFeeTaxA"]);
			lblOrderBatchSequence.Text = oDT.Rows[0]["OrderBatchSequence"].ToString();
			lblProcessingFeeTransId.Text = oDT.Rows[0]["ProcessingFeeTransId"].ToString();
			lblCreationDate.Text = oDT.Rows[0]["CreationDate"].ToString();
			lblLastSentInvoiceDate.Text = oDT.Rows[0]["LastSentInvoiceDate"].ToString();
			lblNumberInvoicesSent.Text = oDT.Rows[0]["NumberInvoicesSent"].ToString();
			lblForceInvoice.Text = oDT.Rows[0]["ForceInvoice"].ToString();
			lblType.Text = oDT.Rows[0]["Type"].ToString() + " " + oDT.Rows[0]["TypeDesc"].ToString();
			lblPaymentMethod.Text = oDT.Rows[0]["PaymentMethodInstance"].ToString() + " " + oDT.Rows[0]["PaymentMethodInstanceDesc"].ToString();
			
			
			DataGrid1.DataSource = oBatch.GetCODsByCOHId(Convert.ToInt32(Request.QueryString["COHId"].ToString()));
			DataGrid1.DataBind();
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

		}
		#endregion
	}
}
