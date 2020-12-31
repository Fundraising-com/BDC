namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.TableDef;

	/// <summary>
	///		Summary description for MainSubscriptionInfo.
	/// </summary>
	public partial class MainSubscriptionInfo : CustomerServiceControl
	{
		protected System.Web.UI.WebControls.Label Label34;
		protected System.Web.UI.WebControls.Label lblOrderItemID;
		protected System.Web.UI.WebControls.Label lblSubID;
		
		protected System.Web.UI.WebControls.Label lblOriginalAdditional;
		protected ControlerSubscriptionStatusHistory  ctrlControlerSubscriptionStatusHistory;
		private const string NOT_AVAILABLE = "N/A";
		private CustomerOrderDetailRemitHistoryTable Table;
		private DataTable ShipmentTable;
		private double RefundAmount;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
		
		public override void DataBind()
		{
			LoadData();
			SetValueElementLastTransaction();

			ctrlControlerSubscriptionStatusHistory.DataBind();
				
			
		}
		private void SetValueElementLastTransaction()
		{
            string OrderFormImageBaseURL = System.Configuration.ConfigurationSettings.AppSettings["OrderFormImageBaseURL"];

			if(DataSource.Rows.Count!=0)
			{
                
				DataRow row=DataSource.Rows[0];
				lblTitleCode.Text = row[CustomerOrderDetailRemitHistoryTable.FLD_TITLECODE].ToString();
				lblGiftOrderType.Text= row[CustomerOrderDetailRemitHistoryTable.FLD_GIFTORDERTYPE].ToString();
				lblOrderItemStatus.Text= row["StatusDescription"].ToString();
				lblCOHInstance.Text= row[CustomerOrderDetailRemitHistoryTable.FLD_CUSTOMERORDERHEADERINSTANCE].ToString();
				lblTransID.Text= row[CustomerOrderDetailRemitHistoryTable.FLD_TRANSID].ToString();
				lblRemitBatchID.Text = row[CustomerOrderDetailRemitHistoryTable.FLD_REMITBATCHID].ToString();
				lblRemitBatchDate.Text = row["RemitBatchDate"].ToString();
				
				if(Convert.ToInt32(row["RunID"]) != 0) 
				{
					lblRunID.Text = row["RunID"].ToString();
				} 
				else 
				{
					lblRunID.Text = String.Empty;
				}

				lblRemitBatchCount.Text = row["RemitBatchCount"].ToString();
				lblOrderKeyedDate.Text = row["OrderKeyedDate"].ToString();

				if(ShipmentTable.Rows.Count > 0) 
				{
					lblShipmentID.Text = ShipmentTable.Rows[0]["ID"].ToString();
					lblShipmentDate.Text = ShipmentTable.Rows[0]["ShipmentDate"].ToString();
					lblShipmentID.Visible = true;
					lblShipmentIDTitle.Visible = true;
					lblShipmentDate.Visible = true;
					lblShipmentDateTitle.Visible = true;
				} 
				else 
				{
					lblShipmentID.Visible = false;
					lblShipmentIDTitle.Visible = false;
					lblShipmentDate.Visible = false;
					lblShipmentDateTitle.Visible = false;
				}

				lblMagazineTitle.Text= row[CustomerOrderDetailRemitHistoryTable.FLD_MAGAZINETITLE].ToString();
				lblMagazineStatus.Text= row["MagazineStatus"].ToString();
				lblFulfillmentHouseName.Text= row["Ful_Name"].ToString();
				lblFullContact.Text= row["Ful_Tel"].ToString();
				lblPriceEntered.Text= row["PriceEntered"].ToString();
				lblCatalogPrice.Text= row[CustomerOrderDetailRemitHistoryTable.FLD_CATALOGPRICE].ToString();
				lblPrice.Text= row[CustomerOrderDetailRemitHistoryTable.FLD_BASEPRICE].ToString();
				
				if(RefundAmount > 0)
				{
					lblRefundAmount.Text = RefundAmount.ToString();
					lblRefundAmount.Visible = true;
					lblRefundAmountTitle.Visible = true;
				} 
				else 
				{
					lblRefundAmount.Visible = false;
					lblRefundAmountTitle.Visible = false;
				}
				
				lblNewRenew.Text= row[CustomerOrderDetailRemitHistoryTable.FLD_RENEWAL].ToString();
				
				lblNumberofIssues.Text= row[CustomerOrderDetailRemitHistoryTable.FLD_NUMBEROFISSUES].ToString();
				lblCustomerName.Text = row["RecipientLastName"].ToString()+", "+ row["RecipientFirstName"].ToString();
                lblCustomerOrderID.Text = row["CustomerOrderID"].ToString();
                lblInvoiceID.Text = row["InvoiceNumber"].ToString() == "0" ? "" : row["InvoiceNumber"].ToString();
                hypLnkOrderForm.HRef = OrderFormImageBaseURL + row["ToteID"].ToString().PadLeft(10, '0')+"/" +row["CustomerOrderID"].ToString().PadLeft(10, '0') +".jpg";
				SetGiftCardValue(row);
			}
	
		}

		private void SetGiftCardValue(DataRow row)
		{
			if(row["DateCardSent"].ToString() != String.Empty)
			{
				lblDateGiftCardSent.Text= row["DateCardSent"].ToString();
				lblIsGiftCardSent.Text = "YES";
			}
			else
			{
				lblIsGiftCardSent.Text = "NO";
				lblDateGiftCardSent.Text= NOT_AVAILABLE;
			}

			lblDonorName.Text = row["DonorName"].ToString();
			lblGiftOrderType.Text = row[CustomerOrderDetailRemitHistoryTable.FLD_GIFTORDERTYPE].ToString();
		}
		private void LoadData()
		{
			try
			{
				Table = new CustomerOrderDetailRemitHistoryTable();
				this.DataSource = Table;
				this.Page.BusCustomerOrderDetailRemitHistory.SelectOneLastTransaction(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);

				ShipmentTable = new DataTable("Shipment");
				this.Page.BusShipmentOrder.SelectShipmentByCOHInstance(ShipmentTable, this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);

                RefundAmount = this.Page.BusPayment.SelectRefundTotalAmountByCOD(this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);
			}
			catch(NullReferenceException ex) 
			{
				bool hasKey = false;

				foreach(string key in Session.Keys) 
				{
					if(key == "CurrentInfoSession") 
					{
						hasKey = true;
					}
				}

				if(hasKey) 
				{
					ex.Source += " Has the session key.";
				} 
				else 
				{
					ex.Source += " Does not have the session key.";
				}

				throw ex;
			}
		}


		
	}
}

