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
	///		Summary description for searchorder.
	/// </summary>
	public partial class PageSearchOrder : CustomerServiceControl,ISearch
	{
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Label lblHeader;
		protected InfoSearchOrder ctrlInfoSearchOrder;
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

		}
		#endregion

		public ParameterValueList GetParameterValue(string StartParameterName)
		{

			return this.ctrlInfoSearchOrder.GetParameterValue("");

		}
		public ParameterValueList GetParameterValueFilter(string StartParameterName)
		{

			return this.ctrlInfoSearchOrder.GetParameterFilter("");

		}
		public override bool Validate()
		{
			return this.ctrlInfoSearchOrder.Validate();
		}
		public int ItemType
		{
			get
			{
				return ctrlInfoSearchOrder.ItemType;
			}
			set
			{
				ctrlInfoSearchOrder.ItemType = value;
			}
		}
		
		
	}
}
