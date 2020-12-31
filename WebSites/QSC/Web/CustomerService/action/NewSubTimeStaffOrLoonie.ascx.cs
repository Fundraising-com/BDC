using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPFulfillment.DataAccess.Common.ActionObject;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess.Common.TableDef;

namespace QSPFulfillment.CustomerService.action
{
	/// <summary>
	///		Summary description for NewSubTimeStaffOrLoonie.
	/// </summary>
	public partial class NewSubTimeStaffOrLoonie : NewItemControl
	{
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;

		protected QSPFulfillment.CustomerService.ControlerAddress ctrlControlerAddress;
		protected System.Web.UI.WebControls.TextBox txtValidate;

		protected const string MSG_HEADER = "New subscription for Time Staff or Loonie";

		private bool ActionCompleted = false;

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

		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			if(!this.ActionCompleted)
			{
				this.DataBind();
				this.ctrlControlerAddress.Visible = true;
				this.ctrlControlerAddress.DataBind(AddressType.ShipTo);
				this.SetValue();
				this.Page.ConfirmButton.Visible = true;
				this.Page.ConfirmButton.Enabled = true;
				this.Page.CommentTextBox.Visible = true;
				this.Page.CommentsLabel.Visible = true;
				this.Page.ConfirmButton.CausesValidation = false;
			}
		}

		private QSPFulfillment.DataAccess.Common.ActionObject.Magazine MagazineInfo
		{
			get 
			{
				if(ViewState["MagazineInfo"] == null)
					ViewState["MagazineInfo"] = new QSPFulfillment.DataAccess.Common.ActionObject.Magazine
						(GetTitle(), GetMagInstance(), GetProductCode(), GetPrice(), GetTerm(), GetNewRenewal(), GetProductType());

				return (QSPFulfillment.DataAccess.Common.ActionObject.Magazine) ViewState["MagazineInfo"];
			}
			set 
			{
				ViewState["MagazineInfo"] = value;
			}
		}

		public override void DataBind() 
		{
			LoadData();
		}

		private void LoadData() 
		{
			DataSource = new DataTable("CustomerOrderDetail");
			this.Page.BusCustomerOrderDetail.SelectOne(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID, 1);
		}

		private void DataBindNewSub() 
		{
			DataSource = new DataTable("CustomerOrderDetail");
			this.Page.BusCustomerOrderDetail.SelectOne(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID, 0);
		}

		private void SetValue()
		{	
			this.lblTitleCode.Text = this.MagazineInfo.ProductCode;
			this.lblMagazineTitle.Text = this.MagazineInfo.Title;
			this.lblTerm.Text = this.MagazineInfo.Term.ToString();
			this.lblCatalogPrice.Text = String.Format("{0:N2}", this.MagazineInfo.Price);
			this.lblPrice.Text = String.Format("{0:N2}", this.MagazineInfo.Price);
		}

		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}
		protected override void DoAction()
		{
			DataBindNewSub();
			this.ctrlControlerAddress.DataBind(AddressType.ShipTo);

			NewSubcription newsub = new NewSubcription(ActionBusiness.CAMPAIGNID, this.MagazineInfo.MagInstance, this.MagazineInfo.NewRenew, this.MagazineInfo.Price, GetOverrideCode(), this.Page.CustomerInfo.CustomerInstance, this.MagazineInfo.Price, this.Page.UserID, GetFirstName(), GetLastName(), GetAddress1(), GetAddress2(), GetCity(), GetProvince(), GetPostalCode());

			this.Page.BusCustomerOrderDetail.NewItem(newsub, 46001, 39021, this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);

			this.ActionCompleted = true;
		}

		private int GetMagInstance()
		{
			return Convert.ToInt32(DataSource.Rows[0]["PricingDetailsID"]);
		}

		private string GetTitle()
		{
			return DataSource.Rows[0]["ProductName"].ToString();
		}

		private int GetTerm()
		{
			return Convert.ToInt32(DataSource.Rows[0]["Quantity"]);
		}

		private int GetOverrideCode()
		{
			return Convert.ToInt32(DataSource.Rows[0]["PriceOverrideID"]);
		}

		private string GetNewRenewal()
		{
			return DataSource.Rows[0]["Renewal"].ToString();
		}

		private string GetProductCode() 
		{
			return DataSource.Rows[0]["ProductCode"].ToString();
		}

		private int GetProductType()
		{
			return Convert.ToInt32(DataSource.Rows[0]["ProductType"]);
		}

		private float GetPrice()
		{
			return Convert.ToSingle(DataSource.Rows[0]["Price"]) * 2;
		}

		private bool GetIsStaffCampaign() 
		{
			bool result;

			if(Convert.ToInt32(DataSource.Rows[0]["IsStaffOrder"]) == 0) 
			{
				result = false;
			} 
			else 
			{
				result = true;
			}

			return result;
		}

		private string GetFirstName() 
		{
			return ctrlControlerAddress.CustomerInfo.FirstName;
		}

		private string GetLastName() 
		{
			return ctrlControlerAddress.CustomerInfo.LastName;
		}

		private string GetAddress1() 
		{
			return ctrlControlerAddress.CustomerInfo.CustomerAddress.Street1;
		}

		private string GetAddress2() 
		{
			return ctrlControlerAddress.CustomerInfo.CustomerAddress.Street2;
		}

		private string GetCity() 
		{
			return ctrlControlerAddress.CustomerInfo.CustomerAddress.City;
		}

		private string GetPostalCode() 
		{
			return ctrlControlerAddress.CustomerInfo.CustomerAddress.PostalCode;
		}

		private string GetProvince() 
		{
			return ctrlControlerAddress.CustomerInfo.CustomerAddress.StateProvinceCode;
		}
	}
}

