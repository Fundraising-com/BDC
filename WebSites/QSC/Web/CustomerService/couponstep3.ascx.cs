namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Common.ActionObject;

	/// <summary>
	///		Summary description for couponstep3.
	/// </summary>
	public partial class couponstep3 : CustomerServiceControlCoupon
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label lblPrice;
		protected System.Web.UI.WebControls.Label lblPriceOverrideReason;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			if(this.Page.Step2Completed)
			{
					SetValue();
			}
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

		}
		#endregion
		protected override void DoAction()
		{
			

			if(this.Page.CurrentStep ==3)
			{
				string TRANSACTIONNAME = "Coupon";
				QSPFulfillment.DataAccess.Data.ConnectionProvider connProvider = new QSPFulfillment.DataAccess.Data.ConnectionProvider();
				try
				{
					
						
					
					this.Page.BusCustomerOrderDetail.MainConnectionProvider = connProvider;
					this.Page.BusCoupon.MainConnectionProvider = connProvider;
					connProvider.OpenConnection();
					connProvider.BeginTransaction(TRANSACTIONNAME);

					NewSubcription newsub = new NewSubcription(this.Page.OrderInfo.CampaignID,this.Page.MagazineInfo.MagInstance,this.Page.MagazineInfo.NewRenew,this.Page.MagazineInfo.Price,45004,this.Page.CustomerInfo.CustomerInstance,this.Page.MagazineInfo.Price,this.Page.UserID, this.lblFirstName.Text, this.lblLastName.Text, this.lblStreet1.Text, this.lblStreet2.Text, this.lblCity.Text, this.Page.CustomerInfo.CustomerAddress.StateProvinceCode, this.lblPostalCode.Text);
					//Todo:have to be a transaction
					this.Page.BusCustomerOrderDetail.NewItem(newsub, this.Page.MagazineInfo.ProductType, this.Page.OrderQualifierID, this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);
					this.Page.BusCoupon.UpdateCouponStatus(this.Page.Coupon);
					connProvider.CommitTransaction();
					this.Page.Step3Completed = true;
					this.Page.ActionPerformed = true;
					this.Page.CurrentStep =0;
				}
				catch (Exception ex)
				{				
					
					if (connProvider.DBConnection.State != ConnectionState.Closed)
						connProvider.RollbackTransaction(TRANSACTIONNAME);
				
					throw ex;
						
					
				
				}
				finally
				{
					if (connProvider.DBConnection.State != ConnectionState.Closed)
						connProvider.CloseConnection(false);


					
				}

				
			
				
			}

		}
		private void SetValue()
		{
			try 
			{
				this.lblCatalogPrice.Text = this.Page.MagazineInfo.Price.ToString();
				this.lblCity.Text =this.Page.CustomerInfo.CustomerAddress.City;
				this.lblCoupon.Text =  this.Page.Coupon;
                this.lblInvoiceOrder.Text = this.Page.InvoiceOrder ? "YES" : "NO";
				this.lblFirstName.Text =this.Page.CustomerInfo.FirstName;
				this.lblLastName.Text =this.Page.CustomerInfo.LastName;
				this.lblMagazineTitle.Text =this.Page.MagazineInfo.Title;
				this.lblPostalCode.Text =this.Page.CustomerInfo.CustomerAddress.PostalCode;
				this.lblNewRenewal.Text = this.Page.MagazineInfo.NewRenew.ToString();
				this.lblStreet1.Text =this.Page.CustomerInfo.CustomerAddress.Street1;
				this.lblStreet2.Text = this.Page.CustomerInfo.CustomerAddress.Street2;
				this.lblTerm.Text =this.Page.MagazineInfo.Term.ToString();
				this.lblTitleCode.Text =this.Page.MagazineInfo.ProductCode;
				this.lblProvince.Text = this.Page.CustomerInfo.CustomerAddress.StateProvince;
				this.lblCountry.Text = this.Page.CustomerInfo.CustomerAddress.Country;
                this.lblEmail.Text = this.Page.CustomerInfo.Email;
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
		public override void SetValueElement()
		{
			
			
				this.Page.Message = "Please validate the following information and click on <i>Save</i>.";
				this.Page.Header ="Prepaid Subscription - Step 3";
			
				
		}
	}
}
