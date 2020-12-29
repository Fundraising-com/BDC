/* Jean-Francois Buist - March 1, 2005
 * Base class of all usable controls for Globalization.
 * 
 */

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GA.BDC.Core.Web.UI.UIControls {
	/// <summary>
	/// Summary description for PanelControl.
	/// </summary>
	public abstract class PanelControl : System.Web.UI.WebControls.WebControl, INamingContainer {
		protected bool revealYourself = false;
		protected bool editControls = false;

		public PanelControl() {

		}

		// this retreive the common properties from the site session
		public void InitPanelControl() {
			if(Page.Session["PanelConfig.RevealYourself"] != null) {
				if(Page.Session["PanelConfig.RevealYourself"].ToString().ToLower() ==
					"true") {
					revealYourself = true;
				}
			}

			if(Page.Session["PanelConfig.Edit"] != null) {
				if(Page.Session["PanelConfig.Edit"].ToString().ToLower() ==
					"true") {
					editControls = true;
				}
			}
		}

		public static bool HasWebForm(System.Web.UI.Page page) {
			for(int i=0;i<page.Controls.Count;i++) {
				if(page.Controls[i] is WebControl) {
					return true;
				}
			}
			return false;
		}

		// sets the property of the displayed control the same attributes of the usercontrol 
		// that is designed
		protected void SetCommontProperties(ref System.Web.UI.WebControls.WebControl wc) {
			wc.CssClass = CssClass;
			
			wc.Font.Bold = Font.Bold;
			wc.Font.Italic = Font.Italic;
			wc.Font.Name = Font.Name;
			wc.Font.Names = Font.Names;
			wc.Font.Overline = Font.Overline;
			wc.Font.Size = Font.Size;
			wc.Font.Strikeout = Font.Strikeout;
			wc.Font.Underline = Font.Underline;

			wc.Width = Width;
			wc.Height = Height;
		}

		// Get the data from many cultures
		protected string GetData(UIControls.Config.Cultures cultures, string culturesName) {
			UIControls.Config.Culture defaultCulture = null;
			foreach(UIControls.Config.Culture c in cultures.CultureList) {
				if(defaultCulture == null) {
					defaultCulture = c;
				}

				if(c.ID == culturesName) {
					string data = "";
					try {
						data = c.Data.Parameters.Parameter[0].ToString();
					} catch(System.Exception ex) {
						throw new Exception("Unable to get data from " + c.ID, ex);
					}
					return System.Web.HttpUtility.HtmlDecode(data);
				}
			}

			string ndata = "";
			try {
				ndata = defaultCulture.Data.Parameters.Parameter[0].ToString();
			} catch(System.Exception ex) {
				throw new Exception("Unable to get data from " + defaultCulture.ID, ex);
			}

			return System.Web.HttpUtility.HtmlDecode(ndata);;
		}

		// Get the data from many partners
		protected string GetData(UIControls.Config.PartnersID partnersID, string partnerID, string culture, string partnerType) {
			UIControls.Config.PartnerID defaultPartner = null;

			foreach(UIControls.Config.PartnerID p in partnersID.PartnerIdList) {
				string pid = p.ID.ToLower();
				if(pid == "default") {
					defaultPartner = p;
				}else if(pid == partnerType.ToLower()) {
					defaultPartner = p;
				} else if(pid == partnerID) {
					return GetData(p.Cultures, culture);
				}
			}
			if(defaultPartner != null) {
				return GetData(defaultPartner.Cultures, culture);
			}
			return "NOT APPLICABLE: P";
		}
	}
}
