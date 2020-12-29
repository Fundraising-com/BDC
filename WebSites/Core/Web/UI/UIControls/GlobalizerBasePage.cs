using System;
using System.Web;
using System.Web.UI;

namespace GA.BDC.Core.Web.UI.UIControls {
	/// <summary>
	/// Summary description for GlobalizerBasePage.
	/// </summary>
	public class GlobalizerBasePage : System.Web.UI.Page	{
		public DynTag DynTags = new DynTag();
		public string PageTitle;
		public string MetaContent;
		public static string PageTitleSessionKey = "PageTitleSessionKey";
		public static string MetaContentSessionKey = "MetaContentSessionKey";
		public static string CurrentPartnerIDSessionKey = "CurrentPartnerIDSessionKey";
		public static string CurrentCultureSessionKey = "CurrentCultureSessionKey";
		public static string CurrentPartnerTypeSessionKey = "CurrentPartnerTypeSessionKey";
		public static string CurrentBaseProjectPath = "CurrentProductionProjectPath";
		public static string CurrentProductionBaseProjectPath = "CurrentProductionBaseProjectPath";

		// get the current partner id
		public string GetCurrentPartnerID() {
			return GetCurrentPartnerID(Session);
		}

		public string GetCurrentPartnerID(System.Web.SessionState.HttpSessionState session) {
			if(session[CurrentPartnerIDSessionKey] != null) {
				return session[CurrentPartnerIDSessionKey].ToString();
			}
			return "";
		}

		// get the current culture
		public string GetCurrentCulture() {
			return GetCurrentCulture(Session);
		}

		public string GetCurrentCulture(System.Web.SessionState.HttpSessionState session) {
			if(session[CurrentCultureSessionKey] != null) {
				return session[CurrentCultureSessionKey].ToString();
			}
			return "";
		}

        // get the current culture without creating an instance of this class
        public static string GetCurrentCultureCode()
        {
            return GetCurrentCultureCode(HttpContext.Current.Session);
        }

        public static string GetCurrentCultureCode(System.Web.SessionState.HttpSessionState session)
        {
            if (session[CurrentCultureSessionKey] != null)
            {
                return session[CurrentCultureSessionKey].ToString();
            }
            return "";
        }

        // set current culture id
        public static void SetCurrentCultureCode(string culture)
        {
            SetCurrentCultureCode(HttpContext.Current.Session, culture);
        }

        public static void SetCurrentCultureCode(System.Web.SessionState.HttpSessionState session, string culture)
        {
            session[CurrentCultureSessionKey] = culture;
        }

        // get the current partner type
		public string GetCurrentPartnerType() {
			return GetCurrentPartnerType(Session);
		}

		public string GetCurrentPartnerType(System.Web.SessionState.HttpSessionState session) {
			if(session[CurrentPartnerTypeSessionKey] != null) {
				return session[CurrentPartnerTypeSessionKey].ToString();
			}
			return "";
		}

		// get current production base project
		public string GetCurrentProductionBaseProjectPath() {
			return GetCurrentProductionBaseProjectPath(Session);
		}

		public string GetCurrentProductionBaseProjectPath(System.Web.SessionState.HttpSessionState session) {
			if(session[CurrentProductionBaseProjectPath] != null) {
				return session[CurrentProductionBaseProjectPath].ToString().ToLower();
			}
			return "";
		}

		// get current base project path
		public string GetCurrentBaseProjectPath() {
			return GetCurrentBaseProjectPath(Session);
		}

		public string GetCurrentBaseProjectPath(System.Web.SessionState.HttpSessionState session) {
			if(session[CurrentBaseProjectPath] != null) {
				return session[CurrentBaseProjectPath].ToString().ToLower();
			}
			return "";
		}

		// set current partner id
		public void SetCurrentPartnerID(string partnerID) {
			SetCurrentPartnerID(Session, partnerID);
		}

		public void SetCurrentPartnerID(System.Web.SessionState.HttpSessionState session, string partnerID) {
			session[CurrentPartnerIDSessionKey] = partnerID;
		}

		// set current culture id
		public void SetCurrentCulture(string culture) {
			SetCurrentCulture(Session, culture);
		}

		public void SetCurrentCulture(System.Web.SessionState.HttpSessionState session, string culture) {
			session[CurrentCultureSessionKey] = culture;
		}

		// set current partner type 
		public void SetCurrentPartnerType(string partnerType) {
			SetCurrentPartnerType(Session, partnerType);
		}

		public void SetCurrentPartnerType(System.Web.SessionState.HttpSessionState session, string partnerType) {
			session[CurrentPartnerTypeSessionKey] = partnerType;
		}

		// set current production project path
		public void SetCurrentProductionBaseProjectPath(string productionBaseProjectPath) {
			SetCurrentProductionBaseProjectPath(Session, productionBaseProjectPath);
		}

		public void SetCurrentProductionBaseProjectPath(System.Web.SessionState.HttpSessionState session, string productionBaseProjectPath) {
			session[CurrentProductionBaseProjectPath] = productionBaseProjectPath.ToLower(); 
		}

		// set debug production project path
		public void SetCurrentBaseProjectPath(string baseProjectPath) {
			SetCurrentBaseProjectPath(Session, baseProjectPath);
		}

		public void SetCurrentBaseProjectPath(System.Web.SessionState.HttpSessionState session, string baseProjectPath) {
			session[CurrentBaseProjectPath] = baseProjectPath.ToLower();
		}

		// this method loads the configuration file, get the controls and assign
		// their proper value to these controls
		public void Globalize(GA.BDC.Core.Web.UI.UIControls.PagePanelControl p) {
			Globalize(p, null);
		}

		// this method loads the configuration file, get the controls and assign
		// their proper value to these controls (for user controls)
		public void Globalize(GA.BDC.Core.Web.UI.UIControls.PagePanelControl p, System.Web.UI.UserControl userControl) {
			try {
                p.ASPXFileName = p.ASPXFileName.ToLower().Replace(GetCurrentBaseProjectPath(), GetCurrentProductionBaseProjectPath());
                if (Session["TemporaryGlobalizerPath"] != null)
                {
                    string temporaryGlobalizerPath = Session["TemporaryGlobalizerPath"].ToString();
                    p.ASPXFileName = p.ASPXFileName.ToLower().Replace(GetCurrentProductionBaseProjectPath(), GetCurrentProductionBaseProjectPath() + temporaryGlobalizerPath + "\\");
                }

                p.Refresh(DynTags, userControl);

                if (Session[PageTitleSessionKey] != null)
                {
                    PageTitle = Session[PageTitleSessionKey].ToString();
                }

                if (Session[MetaContentSessionKey] != null)
                {
                    MetaContent = Session[MetaContentSessionKey].ToString();
                }

                System.Threading.Thread.CurrentThread.CurrentCulture =
                    new System.Globalization.CultureInfo(Session[CurrentCultureSessionKey].ToString());
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    new System.Globalization.CultureInfo(Session[CurrentCultureSessionKey].ToString());

                Session["CurrentPartnerID"] = Session[CurrentPartnerIDSessionKey].ToString();
            } catch(System.Exception ex) {
				// todo 
				throw ex;
			}
		}        

		// The globalizer can have hidden values inserted.
		// This is the way we can retreive them by using unique key.
		public string GlobalizerGetValue(GA.BDC.Core.Web.UI.UIControls.PagePanelControl p, string key) {
			string r = "";
			try {
//				if(Session["TemporaryGlobalizerPath"] != null) {
//					string temporaryGlobalizerPath = Session["TemporaryGlobalizerPath"].ToString();
//					if(p.ASPXFileName.IndexOf(GetCurrentProductionBaseProjectPath() + temporaryGlobalizerPath + "\\") <= 0) {
//						p.ASPXFileName = p.ASPXFileName.ToLower().Replace(GetCurrentBaseProjectPath(), GetCurrentProductionBaseProjectPath() + temporaryGlobalizerPath + "\\");
//					}
//				} else {
					p.ASPXFileName = p.ASPXFileName.ToLower().Replace(GetCurrentBaseProjectPath(), GetCurrentProductionBaseProjectPath());
//				}

//				if(Session["TemporaryGlobalizerPath"] != null) {
//					string temporaryGlobalizerPath = Session["TemporaryGlobalizerPath"].ToString();
//					p.ASPXFileName = p.ASPXFileName.ToLower().Replace(GetCurrentProductionBaseProjectPath(), GetCurrentProductionBaseProjectPath() + temporaryGlobalizerPath + "\\");
//				}

				r = p.GetValue(DynTags, key);
			} catch(System.Exception ex) {
				// throw new Exception("Can't retreive key=" + key + " from config file: " + p.ASPXFileName, ex);
			}
			return r;
		}
	}
}
