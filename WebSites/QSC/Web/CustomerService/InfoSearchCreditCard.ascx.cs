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
	///		Summary description for SubscriberInfoSearch.
	/// </summary>
	public partial class InfoSearchCreditCard : CustomerServiceControl,ISearch
	{
		protected QSP.WebControl.TextBoxSearch tbxFirstName;
		protected QSP.WebControl.TextBoxSearch tbxLastName;
		protected QSP.WebControl.TextBoxSearch tbxPostalCode;
		protected QSPFulfillment.CommonWeb.UC.FiscalYearSelectControl ctrlFiscalYearSelect;

		

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

