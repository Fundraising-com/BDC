namespace CRMWeb.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for MyCalendar.
	/// </summary>
	public partial class MyCalendar : System.Web.UI.UserControl
	{
		public System.Web.UI.WebControls.Calendar Cal;
		public string textBox;
        protected TextBox myTxt;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		public void setDate(DateTime dt ){
			Cal.TodaysDate = dt;
			Cal.SelectedDate = dt;
			
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

	

	
		
	}
}
