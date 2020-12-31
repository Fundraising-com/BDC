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
	/// Summary description for CODViewer.
	/// </summary>
	public partial class CODViewer :  QSPFulfillment.CommonWeb.QSPPage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			PopulateBatchOrderDetail();
		}

		private void PopulateBatchOrderDetail()
		{
			Business.Batch oBatch = new Business.Batch();
			DataTable oDT = oBatch.GetCODByCOHIdTransId(Convert.ToInt32(Request.QueryString["COHId"].ToString()), Convert.ToInt32(Request.QueryString["TransId"].ToString()));

			lblCOHId.Text = oDT.Rows[0]["CustomerOrderHeaderInstance"].ToString();
			lblTransId.Text = oDT.Rows[0]["TransId"].ToString();
			lblChangeUserId.Text = oDT.Rows[0]["ChangeUserId"].ToString();
			if (oDT.Rows[0]["ChangeDate"] != System.DBNull.Value) 
			{
				lblChangeDate.Text = Convert.ToDateTime(oDT.Rows[0]["ChangeDate"].ToString()).ToString();
			}
			lblStatus.Text = oDT.Rows[0]["StatusInstance"].ToString() + " " + oDT.Rows[0]["StatusDesc"].ToString();
			lblCreationDate.Text = oDT.Rows[0]["CreationDate"].ToString();
			lblQuantity.Text = oDT.Rows[0]["Quantity"].ToString();
			lblInvoiceNumber.Text = oDT.Rows[0]["InvoiceNumber"].ToString();
			lblProductCode.Text = oDT.Rows[0]["ProductCode"].ToString();
			lblProductName.Text = oDT.Rows[0]["ProductName"].ToString();
			lblQuantityShipped.Text = oDT.Rows[0]["QuantityShipped"].ToString();
			lblQuantityReserved.Text = oDT.Rows[0]["QuantityReserved"].ToString();
			
			lblPrice.Text = String.Format("{0:c}", oDT.Rows[0]["Price"]);			
			lblCatalogPrice.Text = String.Format("{0:c}", oDT.Rows[0]["CatalogPrice"]);
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
