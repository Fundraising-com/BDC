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
	///		Summary description for GroupInfoSearch.
	/// </summary>
	public partial class InfoSearchGroup : CustomerServiceControl,ISearch
	{		
		
		protected ShipToBillToSearch ctrlShipToSearch;
		protected ShipToBillToSearch ctrlBillToSearch;
		protected QSPFulfillment.CommonWeb.UC.FiscalYearSelectControl ctrlFiscalYearSelect;

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

			ParameterValueList List = this.ctrlBillToSearch.GetParameterValue("BillTo");
			ParameterValueList List2 = this.ctrlShipToSearch.GetParameterValue("ShipTo");
			List.Merge(List2);

			List.Add(new ParameterValue(ctrlFiscalYearSelect.ParameterName, ctrlFiscalYearSelect.Value));
			
			return List;

		}


		
	}
}
