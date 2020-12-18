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

namespace CRMWeb
{
	/// <summary>
	/// Summary description for WebForm.
	/// </summary>
	public partial class WebForm : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			DataTable dt = DatabaseObjects.GetConsultantList(1);
			DataGrid1.DataSource = dt;
			DataGrid1.DataBind();
	


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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);

		}
		#endregion


	

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
	    
		
		    ListItemType itemType = e.Item.ItemType;
			if ((itemType != ListItemType.Pager) &&
				(itemType != ListItemType.Header) && 
				(itemType != ListItemType.Footer))
			{

				LinkButton button = (LinkButton)e.Item.Cells[0].Controls[0];
				e.Item.Attributes["onclick"] = 
					Page.GetPostBackClientHyperlink(button, "");


				e.Item.Attributes.Add("onmouseover", 
					"this.style.cursor='default'");
			}


		}
	}
}
