namespace QSPFulfillment.OrderMgt
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for SearchModule.
	/// </summary>
	public partial class SearchModule : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblHeader;
		public event SearchEventHandler SearchClicked;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryFrom;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ctrlDateEntryTo;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				this.ctrlDateEntryFrom.Date =System.DateTime.Now.Add(new TimeSpan(-14,0,0,0)); 
				this.ctrlDateEntryTo.Date =  System.DateTime.Now;
			}
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

		protected void lbtnSearch_Click(object sender, System.EventArgs e)
		{
			FireEvent();
		}
		
		private void FireEvent()
		{
			if(SearchClicked != null)
				SearchClicked(this,new SearchClickedArgs(ctrlDateEntryFrom.Date,ctrlDateEntryTo.Date));
		}
	}
}
