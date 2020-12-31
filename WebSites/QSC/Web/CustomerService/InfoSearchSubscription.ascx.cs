namespace QSPFulfillment.CustomerService
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess;
	using Business.Objects;
	using Common;

	/// <summary>
	///		Summary description for SubscriberInfoSearch.
	/// </summary>
	public partial class InfoSearchSubscription : CustomerServiceControl,ISearch
	{
		protected QSP.WebControl.TextBoxSearch tbxTitle;
		protected QSP.WebControl.TextBoxSearch tbxTitleCode;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.CustomValidator cvFromTo;
		protected System.Web.UI.WebControls.HyperLink hypFindProblemCode;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryFrom;
		protected System.Web.UI.WebControls.CompareValidator cpvFMID;
		protected System.Web.UI.WebControls.CompareValidator CompareValidator1;
		protected System.Web.UI.WebControls.CompareValidator CompareValidator3;
		protected System.Web.UI.WebControls.CompareValidator CompareValidator2;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryTo;
		protected InfoSearchMagazine ctrlInfoSearchMagazine;
		protected QSPFulfillment.CommonWeb.UC.FiscalYearSelectControl ctrlFiscalYearSelect;
		protected ControlerSearchCategory ctrlControlerSearchCategory;

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

			
			ParameterValueList List = ctrlInfoSearchMagazine.GetParameterValue("");
			AddParameterValue(this.Controls,List,StartParameterName);
			if(List.Count > 0) 
			{
				AddParameterValue(ctrlControlerSearchCategory.Controls, List, StartParameterName);
			}

			return List;

		}

		public override bool Validate()
		{	
			return ValidFromTo(ctrlDateEntryFrom.Date,ctrlDateEntryTo.Date);
		}		
	}
}

