using System;
using System.Web;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.Utilities.CookieHandler;
using GA.BDC.Core.Web.UI.UIControls;
using GA.BDC.Core.WebTracking;
using GA.BDC.Core.Diagnostics;
using efundraising.ScratchcardWeb;
using GA.BDC.Core.efundraisingCore;
using GA.BDC.Core.Database.Scratchcard;



namespace GA.BDC.WEB.ScratchcardWeb
{
	/// <summary>
	/// Summary description for ScratchcardWebBase.
	/// </summary>
	public class ScratchcardWebBase : GlobalizerBasePage
	{
		protected override void OnInit(EventArgs e) 
		{
			
			if(!IsPostBack || Session.IsNewSession) {
				Initialize();
			}
			base.OnInit (e);
			if(GetCurrentCulture() == "") 
			{
				SetCurrentCulture("en-US");
				DynTags["Culture"] = "en-US";
			}
			DynTags["TemplatePath"] = "_ScratchcardWeb_";					
			DynTags["Theme"] = "_classic_";

			Session["DynTags"] = DynTags;
		}
	
		private void Initialize()
		{
			try
			{
                GA.BDC.Core.efundraisingCore.eFundEnv env = GA.BDC.Core.efundraisingCore.eFundEnv.Create();
                GA.BDC.Core.efundraisingCore.Partner partner = GA.BDC.Core.efundraisingCore.Partner.Create(Session);
				int websiteID = 17;
			
				// get the host from the url
				string partnerUrl = Request.Url.Host;
				if (partnerUrl.Substring(0, 3) != "www")
				{
					partnerUrl = "www." + partnerUrl;					
				}
			
				if (env.PreviousUrl == "www.efundraising.com")
				{
					env.PreviousUrl = "www.scratchcard.com";
				}
			
				if (env.PreviousUrl != partnerUrl)
				{
                    partner = GA.BDC.Core.efundraisingCore.Partner.GetPartnerInfoByURL(partnerUrl);
					partner.PartnerID = 129;
					partner.Save(Session);
					env.PartnerInfo = partner;
					env.PartnerID = partner.PartnerID;
					env.Save();
				}
				else if (partner.PartnerID != 651 && partner.PartnerID != 652)
				{
                    partner = GA.BDC.Core.efundraisingCore.Partner.GetPartnerInfoByURL(partnerUrl);
					partner.Save(Session);
					env.PartnerInfo = partner;
					env.Save();
				}
			
				if (partner.PartnerID == 98)
					websiteID = 165;	
				else if (partner.PartnerID == 129)
				{
					websiteID = 200;
					SetCurrentCulture("en-CA");
					DynTags["Culture"] = "en-CA";
					env.CultureName = "en-CA";
				}
							
				// Extract the promotionID
				if(Request.QueryString["pr_id"] != null)
				{
					env.PromotionID = int.Parse(Request.QueryString["pr_id"].ToString());
				}
				else if(Request.QueryString["promotion_id"] != null)
				{
					env.PromotionID = int.Parse(Request.QueryString["promotion_id"].ToString());
				}
					// if there is no promotionID, check if the referrer is known
					// and has a promotion
				else
				{
				
					if(Request.UrlReferrer != null)
					{

						string referrer = Request.UrlReferrer.AbsoluteUri.ToLower();
					
						// google
						if(referrer.IndexOf("google.") != -1)
						{
							env.PromotionID = 10555;
						}
							// yahoo
						else if(referrer.IndexOf("yahoo.") != -1)
						{
							env.PromotionID = 10556;
						}
							// altavista
						else if(referrer.IndexOf("altavista.") != -1)
						{
							env.PromotionID = 10557;
						}
							// All The Web
						else if(referrer.IndexOf("alltheweb.") != -1)
						{
							env.PromotionID = 10558;
						}
							// MSN
						else if(referrer.IndexOf("msn.") != -1)
						{
							env.PromotionID = 10559;
						}
							// Lycos
						else if(referrer.IndexOf("lycos.") != -1)
						{
							env.PromotionID = 10560;
						}
							// excite
						else if(referrer.IndexOf("excite.") != -1)
						{
							env.PromotionID = 10561;
						}		
							// ask	
						else if(referrer.IndexOf("ask.") != -1)
						{
							env.PromotionID = 10562;
						}
							// aol
						else if(referrer.IndexOf("aol.") != -1)
						{
							env.PromotionID = 10563;
						}
							// dogpile
						else if(referrer.IndexOf("dogpile.") != -1)
						{
							env.PromotionID = 10564;
						}
							// earthlink
						else if(referrer.IndexOf("earthlink.") != -1)
						{
							env.PromotionID = 10565;
						}
							// netscape
						else if(referrer.IndexOf("netscape.") != -1)
						{
							env.PromotionID = 10567;
						}
							// antibot
						else if(referrer.IndexOf("antibot.") != -1)
						{
							env.PromotionID = 10568;
						}
							// webcrawler
						else if(referrer.IndexOf("webcrawler.") != -1)
						{
							env.PromotionID = 10633;
						}
							// mamma
						else if(referrer.IndexOf("mamma.") != -1)
						{
							env.PromotionID = 10634;
						}
							// ixquick
						else if(referrer.IndexOf("ixquick.") != -1)
						{
							env.PromotionID = 10636;
						}
							// snap
						else if(referrer.IndexOf("snap.") != -1)
						{
							env.PromotionID = 10637;
						}

					}
				}
				if(env.PromotionID == -1)
				{
					if (env.PartnerInfo.PartnerID != 500)
					{
						env.FindPromotion(Request);
					}
					else
					{
						env.PromotionID = env.DefaultPromotionID;
					}
				}
						
				env.Save();
				//-------------------------------------------------------------------------------------

				// add page break for all pages
				// add robot meta tags

				// set the web tracking attributes from the appconfig/request/session
				SetWebTrackingAttributes(Request, Session);

				// check the cookies if the user guid id exists
				if(CookieHandler.IsCookieEnable(Request)) 
				{
					if(GA.BDC.Core.Database.Scratchcard.DataAccess.Config.IsProduction) 
					{
						if(CookieHandler.CookieExists(Request, "ScratchUserGUID")) 
						{
							string userGUID = CookieHandler.CookieValue(Request, "ScratchUserGUID");
                            GA.BDC.Core.WebTracking.VisitorLog visitorLog = GA.BDC.Core.WebTracking.VisitorLog.Create(Session);
							visitorLog.VisitorGUID = userGUID;
							visitorLog.Save(Session);
						}
					} 
					else 
					{
						if(CookieHandler.CookieExists(Request, "ScratchUserGUID_DEBUG")) 
						{
							string userGUID = CookieHandler.CookieValue(Request, "ScratchUserGUID_DEBUG");
                            GA.BDC.Core.WebTracking.VisitorLog visitorLog = GA.BDC.Core.WebTracking.VisitorLog.Create(Session);
							visitorLog.VisitorGUID = userGUID;
							visitorLog.Save(Session);
						}
					}
				}

				// insert the visitor log id for this user
				// InsertVisitorLog(int.MinValue, int.MinValue, int.MinValue, int.MinValue);
				InsertVisitorLog(env.PromotionID, int.MinValue,
					int.MinValue, int.MinValue, websiteID);


				// if the button does not have a post back, we get the name of the button
				// by the url query
				if(Request["tc"] != null) 
				{
					// insert the tc tracking if not exists 
					if(Session["TrackingButton.ConfigErrors"] != null) 
					{
						if(Session["TrackingButton.ConfigErrors"].ToString().ToLower() == "true") 
						{
                            GA.BDC.Core.WebTracking.TrackableObject trackableObject =
                                new GA.BDC.Core.WebTracking.TrackableObject();
							trackableObject.TrackingCode = Request["tc"];
							trackableObject.TrackableObjectTypeID = GetTrackableObjectTypeID(Request["tc"]);
							trackableObject.TrackableObjectDesc = Request["tc"];
                            if (GA.BDC.Core.WebTracking.TrackableObject.GetTrackableObject(Request["tc"]) == null) 
							{
								trackableObject.InsertIntoDatabase();
							}
						}
					}

                    GA.BDC.Core.WebTracking.VisitorLog visitorLog = GA.BDC.Core.WebTracking.VisitorLog.Create(Session);
					string codeName = Request["tc"];
                    GA.BDC.Core.WebTracking.VisitorTrack visitorTrack =
                        new GA.BDC.Core.WebTracking.VisitorTrack(visitorLog);
					visitorTrack.InsertIntoDatabase(codeName);
				}
			
				// insert the page tracking if not exists 
				if(Session["TrackingButton.ConfigErrors"] != null) 
				{
					if(Session["TrackingButton.ConfigErrors"].ToString().ToLower() == "true") 
					{
                        GA.BDC.Core.WebTracking.TrackableObject trackableObject =
                            new GA.BDC.Core.WebTracking.TrackableObject();
						trackableObject.TrackingCode = "sc_" + GetCurrentPageName(Request);
						trackableObject.TrackableObjectTypeID = 0;
						trackableObject.TrackableObjectDesc = "sc_" + GetCurrentPageName(Request);
                        if (GA.BDC.Core.WebTracking.TrackableObject.GetTrackableObject("sc_" + GetCurrentPageName(Request)) == null) 
						{
							trackableObject.InsertIntoDatabase();
						}
					}
				}

				// insert the page tracking
				InsertVisitorPageTrack();

				// save the visitor log guid into the user cookie
				if(CookieHandler.IsCookieEnable(Request)) 
				{
					VisitorLog visitorLog = VisitorLog.Create(Session);
					if(visitorLog.VisitorLogID > 0) 
					{
						if(GA.BDC.Core.Database.Scratchcard.DataAccess.Config.IsProduction) 
						{
							if(!CookieHandler.CookieExists(Request, "ScratchUserGUID")) 
							{
								string userGUID =  visitorLog.VisitorGUID;
								CookieHandler.SetCookie(Request, Response, "ScratchUserGUID", userGUID, DateTime.Now.AddYears(4));
							}
						} 
						else 
						{
							if(!CookieHandler.CookieExists(Request, "ScratchUserGUID_DEBUG")) 
							{
								string userGUID =  visitorLog.VisitorGUID;
								CookieHandler.SetCookie(Request, Response, "ScratchUserGUID_DEBUG", userGUID, DateTime.Now.AddYears(4));
							}
						}
					}
				}
				GlobalizerSettings(Cache, Session, Request, env);
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in Initialize of ScratchcardWebBase",ex);
			}

		}

		private int GetTrackableObjectTypeID(string s) {
			if(s.ToLower().StartsWith("img")) {
				return 2;
			} else if(s.ToLower().StartsWith("lnk")) {
				return 1;
			} else if(s.ToLower().StartsWith("btn")) {
				return 5;
			}
			return -1;
		}

		#region Globalizer Settings
		private void GlobalizerSettings(System.Web.Caching.Cache cache, System.Web.SessionState.HttpSessionState session, System.Web.HttpRequest request, GA.BDC.Core.efundraisingCore.eFundEnv env) 
		{
			// in the aspx files, the ASPXFile tags contains a path to the page
			// configuration.  This path in production might differ from the one
			// in dev.  So the 2 paths are set in the c:\GlobalizerConfig\GlobalizerConfig.xml
			// configuration file.
			GA.BDC.Core.Web.UI.UIControls.BaseConfig.GlobalizerConfigs gcs =
				new GA.BDC.Core.Web.UI.UIControls.BaseConfig.GlobalizerConfigs();
			try 
			{
				gcs.LoadXML();
				if(gcs != null) 
				{
					GA.BDC.Core.Web.UI.UIControls.BaseConfig.GlobalizerConfig gc =
						gcs.GetGlobalizerConfigByName("ScratchcardWeb");
					if(gc != null) 
					{
						SetCurrentBaseProjectPath(gc.BaseProjectFileName);
						SetCurrentProductionBaseProjectPath(gc.ProductionBaseProjectFilename);
					}
				}
			} 
			catch(System.Exception ex) 
			{
				throw new Exception("Unable to load c:\\GlobalizerConfig\\GlobalizerConfig.xml: " + ex.Message);
			}

			
			
			SetCurrentPartnerID(env.PartnerInfo.PartnerID.ToString());

			SetCurrentPartnerType("");

			#endregion
		}
	
		
		#region Web Tracking
		
		private Components.Server.Omniture.ScratchcardOmniture _scratchcardOmnitureTracking = null;

		protected Components.Server.Omniture.ScratchcardOmniture ScratchcardOmnitureTracking 
		{
			get 
			{
				if (_scratchcardOmnitureTracking == null)
					_scratchcardOmnitureTracking = new Components.Server.Omniture.ScratchcardOmniture (GetOmnitureJSFileName(this.Page.Request));
				return _scratchcardOmnitureTracking;
			}
		}
		
		protected override void OnPreRender(EventArgs e) 
		{
			if (ScratchcardOmnitureTracking  != null && ScratchcardOmnitureTracking.GetPageName().Trim() != string.Empty && ScratchcardOmnitureTracking.JSFileName.Trim() != string.Empty )
				RegisterStartupScript ("OmnitureTracking", ScratchcardOmnitureTracking.FetchScriptBlock ());
			base.OnPreRender (e);	
		}
		
		public static void SetWebTrackingAttributes(System.Web.HttpRequest request,
			System.Web.SessionState.HttpSessionState session) 
		{
			
			if(session["TrackingButton.ConnectionString"] == null) 
			{
				// set the tracking buttons controls attributes
                if (GA.BDC.Core.EnterpriseComponents.Helper.GetWebConfigValue("TrackingButton.ConnectionString") != "") 
				{
                    session["TrackingButton.ConnectionString"] = GA.BDC.Core.EnterpriseComponents.Helper.GetWebConfigValue("TrackingButton.ConnectionString");
				}

                if (GA.BDC.Core.EnterpriseComponents.Helper.GetWebConfigValue("TrackingButton.SaveClicks").ToLower() == "true") 
				{
					session["TrackingButton.SaveClicks"] = "True";
				} 
				else 
				{
					session["TrackingButton.SaveClicks"] = "False";
				}

				session["TrackingButton.PageID"] = 1;

                if (GA.BDC.Core.EnterpriseComponents.Helper.GetWebConfigValue("TrackingButton.ShowClicks").ToLower() == "true") 
				{
					session["TrackingButton.ShowClicks"] = "True";
				} 
				else 
				{
					session["TrackingButton.ShowClicks"] = "False";
				}

                if (GA.BDC.Core.EnterpriseComponents.Helper.GetWebConfigValue("TrackingButton.ConfigErrors").ToLower() == "true") 
				{
					session["TrackingButton.ConfigErrors"] = "True";
				} 
				else 
				{
					session["TrackingButton.ConfigErrors"] = "False";
				}

				// set the panel controls attributes
                if (GA.BDC.Core.EnterpriseComponents.Helper.GetWebConfigValue("PanelConfig.RevealYourself").ToLower() == "true") 
				{
					session["PanelConfig.RevealYourself"] = "True";
				} 
				else 
				{
					session["PanelConfig.RevealYourself"] = "False";
				}

				// set the panel controls attributes
                if (GA.BDC.Core.EnterpriseComponents.Helper.GetWebConfigValue("PanelConfig.Edit").ToLower() == "true")
                {
					session["PanelConfig.Edit"] = "True";
				} else {
					session["PanelConfig.Edit"] = "False";
				}
			}

			if(request["tracking"] != null) 
			{
				if(request["tracking"].ToLower() == "on") 
				{
					session["TrackingButton.ShowClicks"] = "True";
				} 
				else if(request["tracking"].ToLower() == "off") 
				{
					session["TrackingButton.ShowClicks"] = "False";
					session["TrackingButton.ConfigErrors"] = "False";
					session["PanelConfig.RevealYourself"] = "False";
				} 
				else if(request["tracking"].ToLower() == "errors") 
				{
					session["TrackingButton.ConfigErrors"] = "True";
				} 
				else if(request["tracking"].ToLower() == "showme") 
				{
					session["PanelConfig.RevealYourself"] = "True";
				} else if(request["tracking"].ToLower() == "edit") {
					if(request["whoami"].ToLower() == "bobafett") {
						session["PanelConfig.Edit"] = "True";
					}
				}
			}
		}
		
		public static string GetOmnitureJSFileName(System.Web.HttpRequest request)
		{
			string resourcePath = "Resources/jsScript/";
			string result = string.Empty;
			string sAppPath= request.ApplicationPath;
			if (!sAppPath.EndsWith("/"))
				sAppPath += "/";
			if (ApplicationSettings.GetConfig()["WebTracking.Omniture", "isProduction"].ToLower () == "true")
			{
				result = sAppPath + resourcePath + ApplicationSettings.GetConfig()["WebTracking.Omniture", "jsFileNameInProduction"];
			}
			else
			{
				result = sAppPath + resourcePath + ApplicationSettings.GetConfig()["WebTracking.Omniture", "jsFileNameInDevelopment"];
			}
			return result;
		}
		
		private void InsertVisitorLog(int promotionID, int touchID, int leadID, int memberHierarchyID, int websiteID) 
		{
			VisitorLog visitorLog = VisitorLog.CreateFromWebSiteID(Session, websiteID);

			if(!IsPostBack && (promotionID > 0 || touchID > 0)) 
			{
				if(visitorLog.VisitorLogID < 0) 
				{
					string visitorGUID = visitorLog.VisitorGUID;
					visitorLog = new VisitorLog();
					visitorLog.VisitorGUID = visitorGUID;
					visitorLog.LeadID = leadID;
					visitorLog.PromotionID = promotionID;
					visitorLog.TouchID = touchID;
                    visitorLog.Version = GA.BDC.Core.Database.Scratchcard.DataAccess.Config.Version.ToString() +
                        "." + GA.BDC.Core.Database.Scratchcard.DataAccess.Config.SubVersion.ToString();

					try 
					{
						visitorLog.InsertIntoDatabase();
					} 
					catch(System.Exception ex) 
					{
						// todo: throw ex
						visitorLog = new VisitorLog();	// reset the object
					}

					// save the visitor log object into the session
					visitorLog.Save(Session);

					WebUserInfo webUserInfo = WebUserInfo.GetWebUserPackage(Request, Session);

					VisitorInfo info = new VisitorInfo(visitorLog);

					info.AvailableHeight = int.MinValue;
					info.AvailableWidth = int.MinValue;
					info.BrowserLanguage = null;
					info.BrowserName = webUserInfo.BrowserName;
					info.BrowserVersion = webUserInfo.BrowserVersion;
					info.CountryCode = null;
					info.Dns = webUserInfo.IpAddress;
					info.Ip = webUserInfo.IpAddress;
					info.Platform = webUserInfo.Platform;
					info.Referrer = webUserInfo.Referrer;
					info.SubDivisionCode = null;

					try 
					{
						info.InsertIntoDatabase();
					} 
					catch(System.Exception ex) 
					{
						// todo: throw ex
					}

					if(memberHierarchyID != int.MinValue) 
					{
						VisitorIdentity visitorIdentity = new VisitorIdentity(visitorLog);
						visitorIdentity.MemberHierarchyID = 1;
						visitorIdentity.InsertIntoDatabase();
					}
				}
			}
		}

		private void InsertVisitorPageTrack() 
		{
			VisitorLog visitorLog = VisitorLog.Create(Session);
			if(visitorLog.VisitorLogID > 0) 
			{
				VisitorTrack visitorTrack = new VisitorTrack(visitorLog);
				try 
				{
					string pagename = GetCurrentPageName(Request);
					visitorTrack.InsertIntoDatabase("sc_" + pagename);
				} 
				catch(System.Exception ex) 
				{
					// todo: throw exception
				}
			}
		}

		#endregion
		
		#region Utils

		public static string GetCurrentPageName(System.Web.HttpRequest request) 
		{
			string url = request.Url.AbsolutePath;
			return GetCurrentPageName(url);
		}

		public static string GetCurrentPageName(string url) 
		{
			url = System.IO.Path.GetFileName(url);

			if(url == "") 
			{
				// default web page
				url = "default.aspx";
			}
			return url;
		}

		#endregion
	}
}
