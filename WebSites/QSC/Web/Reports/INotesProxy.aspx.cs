using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QSPFulfillment.CommonWeb;
using Business.Objects;

namespace QSPFulfillment.Reports
{
	public class iNotesProxy : QSPPage
	{
		private iNotes iNotes;
		private string html;

		protected override void Render(HtmlTextWriter writer)
		{
			writer.InnerWriter.Write(html);
		    base.Render(writer);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
            if (Request.QueryString["Document"] == null)
                iNotes = new iNotes(QSPPage.aUserProfile.FullName, QSPPage.aUserProfile.Password);
            else
                iNotes = new iNotes(QSPPage.aUserProfile.FullName, QSPPage.aUserProfile.Password, Request.QueryString["Document"].ToString());
			html = iNotes.CreateClientHTML();
		}

		#region Web Form Designer generated code
		protected override void OnInit(EventArgs e)
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}