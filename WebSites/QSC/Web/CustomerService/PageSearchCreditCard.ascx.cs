namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess;

	/// <summary>
	///		Summary description for searchsubscriber.
	/// </summary>
	public partial class PageSearchCreditCard : ControlSearch
	{
		protected System.Web.UI.WebControls.Button btnRes;
		protected InfoSearchCreditCard ctrlInfoSearchCreditCard;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

		}
		#endregion


		protected override ParameterValueList GetValueToSearch()
		{
			return ctrlInfoSearchCreditCard.GetParameterValue("");
		}
		
		
		public override SearchMultiPage ResultType
		{
			get
			{				
				return SearchMultiPage.CreditCard;
			}
		}
		
		
	}
}
