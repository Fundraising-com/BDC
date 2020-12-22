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

using efundraising.eReport;
using efundraising.eReport.DataAccess;
using efundraising.Utilities.CookieHandler;

namespace efundraising.eReportWeb
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public partial class Login : eReportWebBasePage
	{


		#region Page Methods
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				if(userLogin != null)
				{
					// Show appropriate panel
					PanelAuthenticated.Visible = true;
					PanelNotAuthenticated.Visible = false;

					// Display Partner information
					LabelPartnerName.Text = string.Empty;//partner.PartnerName;
					LabelUsername.Text = userLogin.UserName;//partner.PartnerPath;

					// Check checkbox if cookie exists and is valid
					if(CookieHandler.CookieExists(Request, "USERNAME"))
						if(CookieHandler.CookieValue(Request, "USERNAME") == userLogin.UserName)//partner.PartnerPath)
							CheckBoxCookieAuthenticated.Checked = true;
				}

				// Set labels text
				LabelLoginError.Text = "Invalid Username and Password Combination";
				LabelLoginError.CssClass = "NormalTextBold ImportantMessage";
			}
		}
		#endregion

		#region Private Methods
		private void RedirectToPage()
		{
			// Redirect to report selection if no redirect url is set
			if(Request["RedirectUrl"] != null)
				Response.Redirect(Request["RedirectUrl"]);
			Response.Redirect("ReportSelection.aspx");
		}

		private void CheckCookies()
		{
			if(userLogin != null)
			{
				if(CheckBoxCookieAuthenticated.Checked)
				{
					// Create or override cookies
					CreateCookies();
				}
				else
				{
					if(CookieHandler.CookieExists(Request, "USERNAME"))
					{
						// Remove cookies if the cookie is related to the current partner
						if(CookieHandler.CookieValue(Request, "USERNAME") == userLogin.UserName)//partner.PartnerPath)
						{
							CookieHandler.SetCookie(Request, Response, "USERNAME", "", DateTime.Now);
							CookieHandler.SetCookie(Request, Response, "PASSWORD", "", DateTime.Now);
						}
					}
				}
			}
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}

		private void CreateCookies()
		{
			Utilities.Encryption.DES.TripleDES tripleDES = new Utilities.Encryption.DES.TripleDES();

			// Create username and encrypted cookies that last one year
			CookieHandler.SetCookie(Request, Response, "USERNAME", userLogin.UserName);//partner.PartnerPath);
			CookieHandler.SetCookie(Request, Response, "PASSWORD", tripleDES.Encrypt(userLogin.Password, "H4LV9TB")/*tripleDES.Encrypt(partner.PartnerPassword, "H4LV9TB")*/);
		}
		#endregion

		protected void ButtonLogin_Click(object sender, System.EventArgs e)
		{
			// Validate information from partner from the database
			userLogin = efundraising.eReport.User.GetUserByUserName(TextBoxUsername.Text);

			if(userLogin == null)
			{
				LabelLoginError.Visible = true;
				return;
			}
			//thePartner = efundraising.efundraisingCore.Partner.GetPartnerByPath(TextBoxUsername.Text);
			thePartner = efundraising.efundraisingCore.Partner.GetPartnerInfoByID(System.Convert.ToInt32(userLogin.ExternalId));
			if (thePartner == null)
			{
				LabelLoginError.Visible = true;
				efundraising.Diagnostics.Logger.LogError(string.Format("Partner object is null with externalID: {0}", userLogin.ExternalId));
				return;
			}
			//if(thePartner.PartnerPassword != TextBoxPassword.Text) // This is the old code: get Password from partner in DB efundweb
			// new DB structure get password from user of report_center_v2.
			// If password validation fails
			if (userLogin.Password  != TextBoxPassword.Text)
			{
				LabelLoginError.Visible = true;
				return;
			}

			// If Remember me option is set, create a cookie
			if(CheckBoxRememberMe.Checked == true && userLogin != null)
				CreateCookies();

			// Save the partner in the session
			userLogin.Save(Session);

			// Information log on partner
			efundraising.Diagnostics.Logger.LogInfo("Partner " + userLogin.UserName /*partner.PartnerName*/ + "has logged in eReport");

			// Check if page contains an url redirection
			RedirectToPage();
		}

		protected void ButtonUpdate_Click(object sender, System.EventArgs e)
		{
			CheckCookies();

			// Go to requested page
			// Normally there shouldn't be a redirectUrl here
			RedirectToPage();
		}

		protected void ButtonLogOut_Click(object sender, System.EventArgs e)
		{
			// Can't keep cookie if loggin out
			CheckBoxCookieAuthenticated.Checked = false;
			CheckCookies();

			// Old version from inherited eReport.Partner
			efundraising.efundraisingCore.Partner.LogOut(Session);
			userLogin = null;

			Response.Redirect("Login.aspx");
		}
	}
}
