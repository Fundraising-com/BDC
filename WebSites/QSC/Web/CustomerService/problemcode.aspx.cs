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
	/// Summary description for problemcode.
	/// </summary>
	public partial class problemcode : CustomerServicePage
	{
		private const string SELECT_TITLE = "Search Problem Code";
		private const string MAINTENANCE_TITLE = "Problem Code Maintenance";

		protected ControlerSearchProblemCode ctrlControlerSearchProblemCode;
		protected ControlerProblemCode ctrlControlerProblemCode;

		protected void Page_Load(object sender, System.EventArgs e)
		{
	
			if(!GetIsSelectOnly())
			{
				imgTitle.Visible=false;
				lblTitle.Visible=true;			
			}
			else
			{
				Menu1.Visible = false;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.ctrlControlerSearchProblemCode.SearchClicked += new SearchEventHandler(ctrlControlerSearchProblemCode_SearchClicked);
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

		private void ctrlControlerSearchProblemCode_SearchClicked(object sender, SearchClickedArgs e)
		{
			this.ctrlControlerProblemCode.List = e.List;
			this.NewSearch = true;
			this.ctrlControlerProblemCode.DataBind();
		}

		protected string PageTitle 
		{
			get 
			{
				string pageTitle;

				if(GetIsSelectOnly()) 
				{
					pageTitle = SELECT_TITLE;
				} 
				else 
				{
					pageTitle = MAINTENANCE_TITLE;
				}

				return pageTitle;
			}
		}

		private bool GetIsSelectOnly()
		{
			if(Request.QueryString["ID"] != null)
				return Convert.ToBoolean(Request.QueryString["ID"]);
			else
				return true;
		}
	}
}
