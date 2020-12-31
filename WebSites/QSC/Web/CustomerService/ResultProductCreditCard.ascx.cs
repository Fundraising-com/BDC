namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess;
	using QSPFulfillment.DataAccess.Business;

	/// <summary>
	///		Summary description for ResultSubscriber.
	/// </summary>
	public class ResultProductCreditCard : ControlerResult
	{
		protected DataGridObject dtgMain;
		int CustomerOrderHeaderInstance;
		int TransID;
		protected Label lblMessage;

		private void Page_Load(object sender, System.EventArgs e)
		{

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
			this.dtgMain.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgMain_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);
			this.DataBinding += new System.EventHandler(this.List_DataBinding);

		}
		#endregion
		public override void DataBind()
		{
			this.dtgMain.CurrentPageIndex = this.Page.GetPageIndexSubNested(this.ClientID);
			base.DataBind();
		}

		private void List_DataBinding(object sender, System.EventArgs e)
		{
			DataGridItem dgi = (DataGridItem) this.BindingContainer;
			DataSet ds = (DataSet) dgi.DataItem;
			dtgMain.DataSource = ds;
			dtgMain.DataMember = "CreditCardDetail";
			if(this.Page.ResultSelected)
			{
				this.CustomerOrderHeaderInstance = this.Page.OrderInfo.CustomerOrderHeaderInstance;
				this.TransID = this.Page.OrderInfo.TransID;
			
			}
			else
			{
				this.dtgMain.SelectedIndex	=-1;
			}

			dtgMain.DataBind();
		}
	

		private void dtgMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dtgMain.CurrentPageIndex = e.NewPageIndex;
			this.Page.NewSearch = false;
			this.Page.AddPageIndexSubNested(this.ClientID, e.NewPageIndex);
		}
	}
}
