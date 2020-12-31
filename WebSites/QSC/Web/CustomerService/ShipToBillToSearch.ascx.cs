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
	///		Summary description for ShipToBillTo.
	/// </summary>
	public partial class ShipToBillToSearch : CustomerServiceControl,ISearch
	{


		

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
			
			ParameterValueList List = new ParameterValueList();

			AddParameterValue(this.Controls,List,StartParameterName);
			return List;
		}
		
	}
}
