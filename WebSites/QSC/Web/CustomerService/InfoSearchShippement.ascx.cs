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
	///		Summary description for ShippementInfoSearch.
	/// </summary>
	public partial class InfoSearchShippement : CustomerServiceControl,ISearch
	{
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryShipFrom;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryShipTo;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryCreatedFrom;
		protected System.Web.UI.WebControls.CustomValidator cvDateEntryShip;
		protected System.Web.UI.WebControls.CustomValidator cvDateEntryCreated;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator4;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryCreatedTo;
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

		public override bool Validate()
		{	
			bool IsValid =  true;

			IsValid &=  ValidFromTo(ctrlDateEntryCreatedFrom.Date,ctrlDateEntryCreatedTo.Date);
			//IsValid &= ValidFromTo(ctrlDateEntryReceivedFrom.Date,ctrlDateEntryReceivedTo.Date);
			IsValid &= ValidFromTo(ctrlDateEntryShipFrom.Date,ctrlDateEntryShipTo.Date);
			return IsValid;
		}
		public ParameterValueList GetParameterValue(string StartParameterName)
		{

			
			ParameterValueList List = new ParameterValueList();
			
			AddParameterValue(this.Controls,List,StartParameterName);

			return List;

		}
	}
}
