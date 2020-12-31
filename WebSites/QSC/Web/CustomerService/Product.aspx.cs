using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for Magazine.
	/// </summary>
	public partial class Product : CustomerServicePage
	{
		protected ControlerMagazine ctrlControlerMagazine;
		protected ControlerSearchMagazine ctrlControlerSearchMagazine;
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
			this.ctrlControlerSearchMagazine.SearchClicked +=new SearchEventHandler(_SearchClicked);
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void _SearchClicked(object sender, SearchClickedArgs e)
		{
			this.ctrlControlerMagazine.List = e.List;
			this.ctrlControlerMagazine.DataBind();
		}

	
	}
}
