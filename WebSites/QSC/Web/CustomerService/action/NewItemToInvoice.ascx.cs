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
	///		Summary description for NewItemToInvoice.
	/// </summary>
	public partial class NewItemToInvoice : NewItemControl
	{
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;

		protected QSPFulfillment.CustomerService.ControlerAddress ctrlControlerAddress;
		protected QSPFulfillment.CustomerService.ControlerMagazineTerm ctrlControlerMagazineTerm;
		protected System.Web.UI.WebControls.TextBox txtValidate;

		protected const string MSG_HEADER = "New item to invoice";

		private bool ActionCompleted = false;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlControlerMagazineTerm.SelectMagazineClick += new SelectMagazineEventHandler(ctrlControlerMagazineTerm_SelectMagazineClick);
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
			if(this.Step1Completed && !this.ActionCompleted)
			{
				this.ctrlControlerMagazineTerm.Visible = false;
				this.ctrlControlerAddress.Visible = true;
				this.ctrlControlerAddress.DataBind(AddressType.ShipTo);
				this.divStep2.Visible = true;
				this.SetValue();
				this.Page.ConfirmButton.Visible = true;
				this.Page.ConfirmButton.Enabled = true;
				this.Page.CommentTextBox.Visible = true;
				this.Page.CommentsLabel.Visible = true;
				this.Page.ConfirmButton.CausesValidation = false;
				this.btnBack.Visible = true;
			}
			else if(!this.ActionCompleted)
			{
				this.DataBind();
				this.ctrlControlerMagazineTerm.Visible = true;
				this.ctrlControlerMagazineTerm.DataBind();
				this.ctrlControlerAddress.Visible = false;
				this.divStep2.Visible = false;
				this.Page.ConfirmButton.Visible = false;
				this.Page.CommentTextBox.Visible = false;
				this.Page.CommentsLabel.Visible = false;
				this.btnBack.Visible = false;
			}
		}

		private void ctrlControlerMagazineTerm_SelectMagazineClick(object sender, SelectMagazineClickedArgs e)
		{
			this.Step1Completed = true;
			this.MagazineInfo = e.MagazineInfo;
		}

		protected void btnBack_Click(object sender, System.EventArgs e)
		{
			this.Step1Completed = false;
		}

		private QSPFulfillment.DataAccess.Common.ActionObject.Magazine MagazineInfo
		{
			get 
			{
				if(ViewState["MagazineInfo"] == null)
					ViewState["MagazineInfo"] = new QSPFulfillment.DataAccess.Common.ActionObject.Magazine();

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

			if(!Step1Completed) 
			{
				SetValueControlerMagazineTerm();
			} 
		}

		private void LoadData() 
		{
			DataSource = new DataTable("CustomerOrderDetail");
			this.Page.BusCustomerOrderDetail.SelectOne(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID, 1);
		}

		private void DataBindNewItem() 
		{
			DataSource = new DataTable("CustomerOrderDetail");
			this.Page.BusCustomerOrderDetail.SelectOne(DataSource,this.Page.OrderInfo.CustomerOrderHeaderInstance,this.Page.OrderInfo.TransID, 0);
		}

		private void SetValueControlerMagazineTerm() 
		{
			this.ctrlControlerMagazineTerm.ProductCode = GetProductCode();
			this.ctrlControlerMagazineTerm.ProductType = GetProductType();
			this.tbxPrice.Text = String.Format("{0:N2}", Convert.ToSingle(DataSource.Rows[0]["Price"]) * Convert.ToSingle(DataSource.Rows[0]["Quantity"]));
		}

		private void SetValue()
		{	
			this.lblProductCode.Text = this.MagazineInfo.ProductCode;
			this.lblProductName.Text = this.MagazineInfo.Title;
			this.lblQuantity.Text = this.MagazineInfo.Term.ToString();
			this.lblCatalogPrice.Text = String.Format("{0:N2}", this.MagazineInfo.Price);
		}


		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}
		protected override void DoAction()
		{
			DataBindNewItem();
			this.ctrlControlerAddress.DataBind(AddressType.ShipTo);

			NewSubcription newitem = new NewSubcription(this.Page.OrderInfo.CampaignID, this.MagazineInfo.MagInstance, GetNewRenewal(), GetPrice(), GetOverrideCode(), this.Page.CustomerInfo.CustomerInstance, this.MagazineInfo.Price, this.Page.UserID, GetFirstName(), GetLastName(), GetAddress1(), GetAddress2(), GetCity(), GetProvince(), GetPostalCode());
		
			this.Page.BusCustomerOrderDetail.NewItem(newitem, GetProductType(), 39020, this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID);

			this.ActionCompleted = true;
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
		private float GetPrice()
		{
			float price = Convert.ToSingle(this.tbxPrice.Text);

			//MS May 01, 07 No more staff discount 
			//if(GetIsStaffCampaign()) 
			//{
			//	price *= 2.00f;
			//}

			return price;
		}
		private int GetProductType() 
		{
			return Convert.ToInt32(DataSource.Rows[0]["ProductType"]);
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

