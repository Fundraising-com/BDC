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

namespace QSPFulfillment.Admin
{
	/// <summary>Personnel directory.</summary>
	public class pdir : QSPFulfillment.Admin.pdirPage
	{
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		
		private void Page_Load(object src, System.EventArgs e)
		{
			this.BASE_Page_Load("Personnel");
		}
	}
}

