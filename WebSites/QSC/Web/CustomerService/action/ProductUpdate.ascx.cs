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
	///		Summary description for NewSubToInvoice.
	/// </summary>
	public partial class ProductUpdate : NewItemControl
	{
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;

		protected System.Web.UI.WebControls.TextBox txtValidate;

		protected const string MSG_HEADER = "Update Product";

		private bool ActionCompleted = false;

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			this.ctrlControlerMagazineTerm.SelectMagazineClick +=new SelectMagazineEventHandler(ctrlControlerMagazineTerm_SelectMagazineClick);
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

		private void SetValueControlerMagazineTerm() 
		{
			this.lblPrice.Text = String.Format("{0:N2}", DataSource.Rows[0]["Price"]);
		}

		private void SetValue()
		{	
			this.lblTitleCode.Text = this.MagazineInfo.ProductCode;
			this.lblMagazineTitle.Text = this.MagazineInfo.Title;
			this.lblTerm.Text = this.MagazineInfo.Term.ToString();
			this.lblCatalogPrice.Text = String.Format("{0:N2}", this.MagazineInfo.Price);
		}


		protected override void SetValueElement()
		{
			this.Page.Header = MSG_HEADER;
		}
		protected override void DoAction()
		{
            this.Page.BusCustomerOrderDetail.ProductUpdate(this.Page.OrderInfo.CustomerOrderHeaderInstance, this.Page.OrderInfo.TransID, this.MagazineInfo.MagInstance);

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
			return Convert.ToSingle(DataSource.Rows[0]["Price"]);
		}
	}
}

