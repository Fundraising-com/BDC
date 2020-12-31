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
	///		Summary description for couponstep2.
	/// </summary>
	public partial class couponstep2 : CustomerServiceControlCoupon
	{
		protected System.Web.UI.WebControls.Label lblTitle2;
		protected System.Web.UI.WebControls.Label Label1s;
		protected System.Web.UI.WebControls.TextBox tbxTitleCode;
		protected System.Web.UI.WebControls.Label Label3s;
		protected System.Web.UI.WebControls.TextBox tbxTitle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox tbxTerm;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator5;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected QSPFulfillment.CustomerService.DataGridObject dtgMain;
		protected ControlerMagazineTerm ctrlControlerMagazineTerm;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			if(this.Page.CurrentStep ==2)
			{
				if(this.Page.CustomerInfo.Type == CustomerType.NewOrderForNonExisting) 
				{
					this.ctrlControlerMagazineTerm.ProductType = 0;
				}
				else 
				{
					this.ctrlControlerMagazineTerm.ProductType = 46001;
				}

				this.ctrlControlerMagazineTerm.ShowNewRenew = true;
				this.ctrlControlerMagazineTerm.CouponSetID = this.Page.CouponSetID;
				this.ctrlControlerMagazineTerm.DataBind();
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.ctrlControlerMagazineTerm.SelectMagazineClick +=new SelectMagazineEventHandler(ctrlControlerMagazineTerm_SelectMagazineClick);
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender +=new EventHandler(Page_PreRender);

		}
		#endregion

		
		protected override void DoAction()
		{
			
			if(this.Page.CurrentStep ==2)
			{
				this.Page.Step2Completed = true;
				this.Page.ActionPerformed = true;
				this.Page.CurrentStep ++;
				
			}

		}

		private void ctrlControlerMagazineTerm_SelectMagazineClick(object sender, SelectMagazineClickedArgs e)
		{
			this.Page.MagazineInfo = e.MagazineInfo;
			this.DoAction();
		}
		public override void SetValueElement()
		{
			
				this.Page.Message = "Please select a magazine from the following list.";
				this.Page.Header ="Prepaid Subscription - Step 2";
			
				
		}
	}
}
