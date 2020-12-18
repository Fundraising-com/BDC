using System;

namespace AdminSection
{
	/// <summary>
	/// Summary description for AdministrationBasePage.
	/// </summary>
	public class AdministrationBasePage : PackageProductBase {
		public AdministrationBasePage() {
			//
			// TODO: Add constructor logic here
			//
		}

		protected override void OnInit(EventArgs e) {
			/*if(Components.Server.AdminUser.AdmUser.Create(Session) == null) {
				Response.Redirect("Administration.aspx");
			};*/
            base.OnInit(e);
		}

	}
}
