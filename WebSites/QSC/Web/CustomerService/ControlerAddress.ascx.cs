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
	using QSPFulfillment.DataAccess.Common.ActionObject;


	/// <summary>
	///	Summary description for ControlerAddress.
	/// </summary>
	public partial class ControlerAddress :  CustomerServiceControl
	{
		protected QSPFulfillment.CommonWeb.UC.PostalAddressDisabled ctrlPostalAddress;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.CustomerInfo.CustomerAddress = this.AddressInfo;
		}

		private void ControlerAddress_PreRender(object sender, EventArgs e)
		{
			this.AddressInfo = this.CustomerInfo.CustomerAddress;
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += new EventHandler(ControlerAddress_PreRender);
		}
		#endregion
		
		public Customer CustomerInfo 
		{
			get 
			{
				if(ViewState["CustomerInfo"] == null)
					ViewState["CustomerInfo"] = new Customer();

				return (Customer) ViewState["CustomerInfo"];
			}
		}

		private Address AddressInfo 
		{
			get 
			{
				return (Address) ViewState["AddressInfo"];
			}
			set 
			{
				ViewState["AddressInfo"] = value;
			}
		}

		public void DataBind(int CampaignID,QSPFulfillment.DataAccess.Business.AddressType Type)
		{
			try
			{
				DataSource = new DataTable();

				
	
				this.Page.BusCustomerOrderHeader.SelectAddressInfo(DataSource,CampaignID,Type);
				if(DataSource.Rows.Count != 0)
				{
					SetPostalAddressInfo();
					this.lblName.Text  = DataSource.Rows[0]["Name"].ToString();
					this.rowPhone.Visible = false;
				}
				else 
				{
					SetPostalAddressInfo();
					this.lblName.Text = "";
					this.lblPhone.Text = "";
				}
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}


		public void DataBind(AddressType Type)
		{
			try
			{
				if(Type == AddressType.BillTo)
				{
					DataBindBillTo(this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);
				}
				else if(Type == AddressType.CustomerRefund)
				{
					DataBindRefund(this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);
				}
				else if(Type == AddressType.ShipTo)
				{
					DataBindShipTo(this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);
				}
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		
		private void DataBindBillTo(int CustomerOrderHeaderInstance,int TransID)
		{
			
			try
			{
				DataSource = new DataTable("Address");
				this.Page.BusCustomerOrderHeader.SelectBillToAddress(DataSource,CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID);
				
				SetInfo();
				SetValueAddressInfo();
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		private void DataBindShipTo(int CustomerOrderHeaderInstance,int TransID)
		{
			
			try
			{
				DataSource = new DataTable("Address");
				this.Page.BusCustomerOrderHeader.SelectShipToAddress(DataSource,CustomerOrderHeaderInstance,TransID);
				
				SetInfo();
				SetValueAddressInfo();
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		private void DataBindRefund(int CustomerOrderHeaderInstance,int TransID)
		{
			
			try
			{
				DataSource = new DataTable("Address");
				this.Page.BusCustomerOrderHeader.SelectCustomerRefundAddress(DataSource,CustomerOrderHeaderInstance,TransID);
				
				SetInfo();
			
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		private void SetInfo()
		{
			if(DataSource.Rows.Count != 0)
			{
				SetPostalAddressInfo();
				this.lblName.Text =  (DataSource.Rows[0]["LastName"].ToString()) + (DataSource.Rows[0]["LastName"].ToString()==""?" ":", ") +  (DataSource.Rows[0]["FirstName"].ToString());
				if(DataSource.Columns.Contains("Phone")) 
				{
					this.lblPhone.Text = DataSource.Rows[0]["Phone"].ToString();
				} 
				else 
				{
					rowPhone.Visible = false;
				}
			}
			else 
			{
				SetPostalAddressInfo();
				this.lblName.Text = "";
				this.lblPhone.Text = "";
			}
		}

		private void SetPostalAddressInfo() 
		{
			DataRow row;

			if(DataSource.Rows.Count != 0) 
			{
				row = DataSource.Rows[0];

				this.ctrlPostalAddress.pPostalCode = row["Zip"].ToString();
				this.ctrlPostalAddress.pStreet1 = row["Address1"].ToString();
				this.ctrlPostalAddress.pStreet2 = row["Address2"].ToString();
				this.ctrlPostalAddress.pCountry = row["Country"].ToString();
				this.ctrlPostalAddress.pCity = row["City"].ToString();
				this.ctrlPostalAddress.pStateProvince = row["State"].ToString();
				this.ctrlPostalAddress.Visible = true;
                if(DataSource.Columns.Contains("Email"))
                {
                    this.ctrlPostalAddress.pEmail = row["Email"].ToString();
                }
                
			}
			else 
			{
				this.ctrlPostalAddress.pPostalCode = "";
				this.ctrlPostalAddress.pStreet1 = "";
				this.ctrlPostalAddress.pStreet2 = "";
				this.ctrlPostalAddress.pCountry = "";
				this.ctrlPostalAddress.pCity = "";
				this.ctrlPostalAddress.pStateProvince = "";
				this.ctrlPostalAddress.Visible = false;
                this.ctrlPostalAddress.pEmail = "";
			}
		}
		
		private void SetValueAddressInfo() 
		{
			DataRow row;

			if(DataSource.Rows.Count != 0) 
			{
				row = DataSource.Rows[0];

				this.CustomerInfo.CustomerAddress = new Address(row["Address1"].ToString(), row["Address2"].ToString(), row["City"].ToString(), row["State"].ToString(), row["Zip"].ToString(), row["Country"].ToString());
				this.CustomerInfo.FirstName = row["firstname"].ToString();
				this.CustomerInfo.LastName = row["lastname"].ToString();
                if (DataSource.Columns.Contains("Email"))
                {
                    this.CustomerInfo.Email =  row["Email"].ToString();
                }
				if(DataSource.Columns.Contains("phone"))
				{
					this.CustomerInfo.PhoneNumber = row["phone"].ToString();
				}
			}
			else 
			{
				this.CustomerInfo.CustomerAddress = null;
				this.CustomerInfo.FirstName = "";
				this.CustomerInfo.LastName = "";
				this.CustomerInfo.PhoneNumber = "";
                this.CustomerInfo.Email = "";
			}
		}
	}
}
