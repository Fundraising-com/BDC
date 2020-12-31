namespace QSPFulfillment.CustomerService.action
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;

	/// <summary>
	///		Summary description for NewSubStep1.
	/// </summary>
	public partial class NewSubStep1 : NewItemStepControl
	{
		
		protected ControlerMagazineTerm ctrlControlerMagazineTerm;
		
		public event SelectMagazineEventHandler SelectMagazineClicked;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			ctrlControlerMagazineTerm.SelectMagazineClick +=new SelectMagazineEventHandler(ctrlControlerMagazineTerm_SelectMagazineClick);
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += new System.EventHandler(this.NewSubStep1_PreRender);

		}
		#endregion

		protected void NewSubStep1_PreRender(object sender, EventArgs e)
		{
			this.Page.Message = "Please select product on the list";
		}

		private void ctrlControlerMagazineTerm_SelectMagazineClick(object sender, SelectMagazineClickedArgs e)
		{
			if(SelectMagazineClicked != null) 
			{
				SelectMagazineClicked(sender, e);
			}
		}

		protected override bool RaisesDoAction
		{
			get
			{
				return false;
			}
		}

	}
}
