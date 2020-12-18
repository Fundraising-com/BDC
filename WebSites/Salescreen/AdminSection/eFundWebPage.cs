using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using efundraising;
using efundraising.Collections;
using efundraising.Configuration;
using efundraising.Diagnostics;
using efundraising.efundraisingCore;
using efundraising.Utilities.CookieHandler;
using efundraising.WebTracking;
using efundraising.WebTracking.DataAccess;


namespace AdminSection
{

	public class eFundWebPage : efundraising.Web.UI.UIControls.GlobalizerBasePage 
	{
		
		#region Fields
		protected string PartnerPath = "";
		#endregion

		#region Helper Methods

		public Literal LineBreak(int numberOfLines)
		{
			Literal literal = new Literal();
			for (int i = 0; i < numberOfLines; i++)
				literal.Text += "<br>";
			return literal;
		}

		public Literal Separator()
		{
			Literal literal = new Literal();
            literal.Text = "<table border=0 width=100%><tr><td class='Separator'></td></tr></table>";
			return literal;
		}

		public string SeparatorHTML()
		{
			return "<table border=0 width=100%><tr><td class='Separator'></td></tr></table>";
		}


		#endregion

		#region Omniture Tracking Methods
		
		
		//private Components.Server.Omniture.EFundOmniture _eFundOmnitureTracking = null;

		private string campaignName
		{
            
			get
			{
				if (Session["campaignName"] == null)
					return string.Empty;

				return (string)Session["campaignName"];
			}

			set
			{
				Session["campaignName"] = value;
			}
		}

	/*	public Components.Server.Omniture.EFundOmniture eFundOmnitureTracking 
		{
			get 
			{
				if (_eFundOmnitureTracking == null)
					_eFundOmnitureTracking = new Components.Server.Omniture.EFundOmniture (GetOmnitureJSFileName(this.Page.Request),
						GetOmnitureAdditionalJSFileName(this.Page.Request));
				return _eFundOmnitureTracking;
			}
		}*/
		
		protected override void OnPreRender(EventArgs e) 
		{
			/*if (this is PackageProductBase)
			{
				Components.Server.Omniture.WebpageTracking webpageTracking = new Components.Server.Omniture.WebpageTracking(GetCurrentPageName(Request), Server.MapPath("~/Resources/xml/OmnitureSettings.xml"));
				eFundOmnitureTracking.SetPageNameAndCategory(webpageTracking.CategoryName, webpageTracking.PageName);
			}
			if (eFundOmnitureTracking != null && eFundOmnitureTracking.GetPageName().Trim() != string.Empty && eFundOmnitureTracking.JSFileName.Trim() != string.Empty )
			{
				Components.Server.UrlParameters urlParameters = new Components.Server.UrlParameters(Request);
//				if (urlParameters.ExistMarketingTRKID != string.Empty &&
//					urlParameters.MarketingCampaignName != string.Empty)
//				{
//					if (urlParameters.MarketingCampaignName != campaignName)
//					{
//						eFundOmnitureTracking.CampaignNameTrackingStatus = 1;
//						campaignName = urlParameters.MarketingCampaignName;
//						eFundOmnitureTracking.Campaign = campaignName;
//						eFundOmnitureTracking.AddEVar_Custom(2, urlParameters.ExistMarketingTRKID);
//					}
//					else
//					{
//						eFundOmnitureTracking.CampaignNameTrackingStatus = 0;
//						eFundOmnitureTracking.Campaign = campaignName;
//						eFundOmnitureTracking.AddEVar_Custom(2, urlParameters.ExistMarketingTRKID);
//					}
//				}
//				else
//				{
//					if (campaignName.Trim() != string.Empty)
//					{
//						eFundOmnitureTracking.CampaignNameTrackingStatus = 0;
//						eFundOmnitureTracking.Campaign = campaignName;
//						eFundOmnitureTracking.AddEVar_Custom(2, urlParameters.ExistMarketingTRKID);
//					}
//					else
//					{
//						eFundOmnitureTracking.CampaignNameTrackingStatus = -1;
//						eFundOmnitureTracking.Campaign = "";
//						eFundOmnitureTracking.AddEVar_Custom(2, urlParameters.ExistMarketingTRKID);
//					}
//				}

				// Tracking external campaigns
				if (urlParameters.ExistMarketingTRKID != string.Empty)
				{
					eFundOmnitureTracking.CampaignNameTrackingStatus = true;
					eFundOmnitureTracking.Campaign = urlParameters.ExistMarketingTRKID;
				}
				else
				{
					eFundOmnitureTracking.CampaignNameTrackingStatus = false;
					eFundOmnitureTracking.Campaign = "";
				}
				// Tracking internal Campaigns
				if (urlParameters.InternalMarketingTrkId != string.Empty)
				{
					eFundOmnitureTracking.InternalCampaignTrackingStatus = true;
					eFundOmnitureTracking.InternalCampaignName = urlParameters.InternalMarketingTrkId;
				}
				else
				{
					eFundOmnitureTracking.InternalCampaignTrackingStatus = false;
					eFundOmnitureTracking.InternalCampaignName = string.Empty;
				}
				RegisterStartupScript ("OmnitureTracking", eFundOmnitureTracking.FetchScriptBlock ());
			}
			base.OnPreRender (e);	*/
		}

		public static string GetOmnitureJSFileName(System.Web.HttpRequest request)
		{
			string resourcePath = "Resources/Javascript/";
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
		
		public static string GetOmnitureAdditionalJSFileName(System.Web.HttpRequest request)
		{
			string resourcePath = "Resources/Javascript/";
			string result = string.Empty;
			string sAppPath= request.ApplicationPath;
			if (!sAppPath.EndsWith("/"))
				sAppPath += "/";
			if (ApplicationSettings.GetConfig()["WebTracking.OmnitureAddition", "isProduction"].ToLower () == "true")
			{
				string sTmp = ApplicationSettings.GetConfig()["WebTracking.OmnitureAddition", "jsFileNameInProduction"];
				if (sTmp.Trim() != string.Empty)
					result = sAppPath + resourcePath + sTmp;
			}
			else
			{
				string sTmp = ApplicationSettings.GetConfig()["WebTracking.OmnitureAddition", "jsFileNameInDevelopment"];
				if (sTmp.Trim() != string.Empty)
					result = sAppPath + resourcePath + sTmp;
			}

			return result;
		}

		#endregion

		#region Web Tracking Methods
		
		private void InsertVisitorLog(int promotionID, int touchID, int leadID, int memberHierarchyID) 
		{
					
			/*eFundEnv env = eFundEnv.Create();
			
			VisitorLog visitorLog = VisitorLog.Create(Session);

			if (!IsPostBack && (promotionID > 0 || touchID > 0)) 
			{
				if(visitorLog.VisitorLogID < 0) 
				{
					string visitorGUID = visitorLog.VisitorGUID;
					visitorLog = new VisitorLog();
					visitorLog.VisitorGUID = visitorGUID;
					visitorLog.LeadID = leadID;
					visitorLog.PromotionID = promotionID;
					visitorLog.TouchID = touchID;
					visitorLog.Version = Components.Server.AppConfig.Version.ToString() + 
						"." + Components.Server.AppConfig.SubVersion.ToString();

					try 
					{
						visitorLog.InsertIntoDatabase();
					} 
					catch 
					{
						// reset the object
						visitorLog = new VisitorLog();	
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
					info.InsertIntoDatabase();

				} 
				else if(env.LeadObject.LeadID > -1) 
				{
					visitorLog.UpdateVisitorLog(env.LeadObject.LeadID);
				} 
			}*/
		}

		private void InsertVisitorPageTrack() 
		{
			VisitorLog visitorLog = VisitorLog.Create(Session);
			if(visitorLog.VisitorLogID > 0)
			{
				VisitorTrack visitorTrack = new VisitorTrack(visitorLog);
				string pagename = GetCurrentPageName(Request);
				visitorTrack.InsertIntoDatabase("efr_" + pagename);
			}
		}

		public static void SetWebTrackingAttributes(System.Web.HttpRequest request,
			System.Web.SessionState.HttpSessionState session) 
		{
			
			if(session["TrackingButton.ConnectionString"] == null) 
			{
				// set the tracking buttons controls attributes
				if(efundraising.EnterpriseComponents.Helper.GetWebConfigValue("TrackingButton.ConnectionString") != "") 
				{
					session["TrackingButton.ConnectionString"] = efundraising.EnterpriseComponents.Helper.GetWebConfigValue("TrackingButton.ConnectionString");
				}

				if(efundraising.EnterpriseComponents.Helper.GetWebConfigValue("TrackingButton.SaveClicks").ToLower() == "true") 
				{
					session["TrackingButton.SaveClicks"] = "True";
				} 
				else 
				{
					session["TrackingButton.SaveClicks"] = "False";
				}

				session["TrackingButton.PageID"] = 1;
				// session["TrackingButton.PageID"] = eSubsEnv.WebTrackingInfo.PageID.ToString();

				if(efundraising.EnterpriseComponents.Helper.GetWebConfigValue("TrackingButton.ShowClicks").ToLower() == "true") 
				{
					session["TrackingButton.ShowClicks"] = "True";
				} 
				else 
				{
					session["TrackingButton.ShowClicks"] = "False";
				}

				if(efundraising.EnterpriseComponents.Helper.GetWebConfigValue("TrackingButton.ConfigErrors").ToLower() == "true") 
				{
					session["TrackingButton.ConfigErrors"] = "True";
				} 
				else 
				{
					session["TrackingButton.ConfigErrors"] = "False";
				}

				// set the panel controls attributes
				if(efundraising.EnterpriseComponents.Helper.GetWebConfigValue("PanelConfig.RevealYourself").ToLower() == "true") 
				{
					session["PanelConfig.RevealYourself"] = "True";
				} 
				else 
				{
					session["PanelConfig.RevealYourself"] = "False";
				}

				// set the panel controls attributes
				if(efundraising.EnterpriseComponents.Helper.GetWebConfigValue("PanelConfig.Edit").ToLower() == "true") {
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

		private int GetTrackableObjectTypeID(string s) 
		{
			if (s.ToLower().StartsWith("img")) 
			{
				return 2;
			} 
			else if (s.ToLower().StartsWith("lnk")) 
			{
				return 1;
			} 
			else if (s.ToLower().StartsWith("btn")) 
			{
				return 5;
			}
			return -1;
		}

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
				url = "Default.aspx";
			}
			return url;
		}

		#endregion

		#region Init Methods

		protected override void OnInit(EventArgs e) 
		{
			eFundEnv env = eFundEnv.Create();
			/*		
			if(!IsPostBack) 
			{
			
				// get the host from the url
				string partnerUrl = Request.Url.Host;
				
				// create the partner object and check if the object already existed,
				// check if the partner has changed
				Partner partner = Partner.Create(Session);

				if (env.PreviousUrl != partnerUrl)
				{
					partner = Partner.GetPartnerInfoByURL(partnerUrl);
					partner.Save(Session);
					env.PartnerInfo = partner;
					env.MailConfigFile = Server.MapPath("EmailTemplate/EmailTemplate.xml");
					env.FindPromotion(Request);
					env.PreviousUrl = partnerUrl;
					env.PartnerID = env.PartnerInfo.PartnerID;
					Session[Global.SessionKeys.Environment] = env;
					env.Save();
				}
				else if (Request.QueryString["partner"] != null && Request.QueryString["partner"] != "")
				{
					if (partner.PartnerFolder != Request.QueryString["partner"])
					{
						partner = Partner.GetPartnerInfoByFolder(Request.QueryString["partner"]);
						partner.Save(Session);
						env.PartnerInfo = partner;
						env.MailConfigFile = Server.MapPath("EmailTemplate/EmailTemplate.xml");
						env.FindPromotion(Request);
						env.PartnerID = env.PartnerInfo.PartnerID;
						Session[Global.SessionKeys.Environment] = env;
						env.Save();
					}
				}
				else
				{
					env.PartnerInfo = partner;
					partner.Save(Session);
					if (env.PromotionID == -1)
					{
						env.MailConfigFile = Server.MapPath("EmailTemplate/EmailTemplate.xml");
						env.FindPromotion(Request);
						env.PartnerID = env.PartnerInfo.PartnerID;
						Session[Global.SessionKeys.Environment] = env;
					}
					env.Save();
				}
				
				// if the promotion is changed via the query 
				// during an active session (not likely to happen...)
				if (Request.QueryString[Global.QueryStringKeys.PromotionId2] != null)
				{
					try
					{
						if (int.Parse(Request.QueryString[Global.QueryStringKeys.PromotionId2]) != env.PromotionID)
						{
							env.FindPromotion(Request);
							env.Save();
						}
					}
					catch{}
				}
				else if (Request.QueryString[Global.QueryStringKeys.PromotionId] != null)
				{
					try
					{
						if (int.Parse(Request.QueryString[Global.QueryStringKeys.PromotionId]) != env.PromotionID)
						{
							env.FindPromotion(Request);
							env.Save();
						}
					}
					catch{}
				}
				
				
				// set the web tracking attributes from the appconfig/request/session
				SetWebTrackingAttributes(Request, Session);

				// check the cookies
				if(CookieHandler.IsCookieEnable(Request)) 
				{
					// if the user guid id exists
					if(Components.Server.AppConfig.IsProduction) 
					{
						if(CookieHandler.CookieExists(Request, "eFundUserGUID")) 
						{
							string userGUID = CookieHandler.CookieValue(Request, "eFundUserGUID");
							VisitorLog visitorLog = VisitorLog.Create(Session);
							visitorLog.VisitorGUID = userGUID;
							visitorLog.Save(Session);
						}
					} 
					else 
					{
						if(CookieHandler.CookieExists(Request, "eFundUserGUID_DEBUG")) 
						{
							string userGUID = CookieHandler.CookieValue(Request, "eFundUserGUID_DEBUG");
							VisitorLog visitorLog = VisitorLog.Create(Session);
							visitorLog.VisitorGUID = userGUID;
							visitorLog.Save(Session);
						}
					}
					// if the user already had a cookie for a PAID search engine promo
					if (CookieHandler.CookieExists(Request, "SEPromo")) 
					{
						string paidSEPRomo = CookieHandler.CookieValue(Request, "SEPromo");
						env.PromotionID = int.Parse(paidSEPRomo);
						env.Save();
					}
				}
				
				
				// insert the visitor log id for this user
				InsertVisitorLog(env.PromotionID , int.MinValue, int.MinValue, int.MinValue);
				
				// if the button does not have a post back, we get the name of the button
				// by the url query
				if(Request["tc"] != null) 
				{
					// insert the tc tracking if not exists 
					if(Session["TrackingButton.ConfigErrors"] != null) 
					{
						if(Session["TrackingButton.ConfigErrors"].ToString().ToLower() == "true") 
						{
							TrackableObject trackableObject =
								new TrackableObject();
							trackableObject.TrackingCode = Request["tc"];
							trackableObject.TrackableObjectTypeID = GetTrackableObjectTypeID(Request["tc"]);
							trackableObject.TrackableObjectDesc = Request["tc"];
							if(TrackableObject.GetTrackableObject(Request["tc"]) == null) 
							{
								trackableObject.InsertIntoDatabase();
							}
						}
					}

					VisitorLog visitorLog = VisitorLog.Create(Session);
					string codeName = Request["tc"];
					VisitorTrack visitorTrack = 
						new VisitorTrack(visitorLog);
					visitorTrack.InsertIntoDatabase(codeName);
				}

				// insert the page tracking
				if(Session["TrackingButton.ConfigErrors"] != null) 
				{
					if(Session["TrackingButton.ConfigErrors"].ToString().ToLower() == "true") 
					{
						TrackableObject trackableObject = new TrackableObject();
						if (Request.Url.AbsoluteUri.ToLower().IndexOf("/alaska") > 0)
						{
							trackableObject.TrackingCode = "alaska_" + GetCurrentPageName(Request);
							trackableObject.TrackableObjectTypeID = 0;
							trackableObject.TrackableObjectDesc = "alaska_" + GetCurrentPageName(Request);
							if(TrackableObject.GetTrackableObject("alaska_" + GetCurrentPageName(Request)) == null) 
							{
								trackableObject.InsertIntoDatabase();
							}	
						}
						else 
						{
							trackableObject.TrackingCode = "efr_" + GetCurrentPageName(Request);
							trackableObject.TrackableObjectTypeID = 0;
							trackableObject.TrackableObjectDesc = "efr_" + GetCurrentPageName(Request);
							if(TrackableObject.GetTrackableObject("efr_" + GetCurrentPageName(Request)) == null) 
							{
								trackableObject.InsertIntoDatabase();
							}
						}
					}
				}

				// insert the page tracking
				InsertVisitorPageTrack();

				// save the cookies
				if(CookieHandler.IsCookieEnable(Request)) 
				{
					// save the visitor log guid
					VisitorLog visitorLog = VisitorLog.Create(Session);
					if(visitorLog.VisitorLogID > 0) 
					{
						if(Components.Server.AppConfig.IsProduction) 
						{
							if(!CookieHandler.CookieExists(Request, "eFundUserGUID")) 
							{
								string userGUID =  visitorLog.VisitorGUID;
								CookieHandler.SetCookie(Request, Response, "eFundUserGUID", userGUID, DateTime.Now.AddYears(4));
							}
						} 
						else 
						{
							if(!CookieHandler.CookieExists(Request, "eFundUserGUID_DEBUG")) 
							{
								string userGUID =  visitorLog.VisitorGUID;
								CookieHandler.SetCookie(Request, Response, "eFundUserGUID_DEBUG", userGUID, DateTime.Now.AddYears(4));
							}
						}
					}
					// if the user came from a PAID seach engine, save his promotionID
					if (partner.PartnerID == 649 || partner.PartnerID == 650) 
					{
						CookieHandler.SetCookie(Request, Response, "SEPromo", env.PromotionID.ToString(), DateTime.Now.AddDays(30));
					}
				}
			}

			GlobalizerSettings(Cache, Session, Request, env);

			#region Globalizer URL Modification Settings
			// this variable is used mainly for the css tag
			PartnerPath = "" + DynTags["TemplatePath"] + "/" + DynTags["Theme"];
			#endregion
			
			base.OnInit(e);*/
		}

		#endregion

		#region Confirmation Email Methods
		
		/// <summary>
		/// 
		/// </summary>
		/// <remarks>This methods create a Thread to launch the confirmation email</remarks>
		protected void SendConfirmationEmail() 
		{
			/*eFundEnv env = eFundEnv.Create();
			Lead lead = env.LeadObject;

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(Server.MapPath(@"EmailTemplate\EmailTemplate.xml"));
			
			if (lead.LeadID > 0) 
			{	
				EmailTemplate.EmailPreview email = EmailTemplate.EmailBodyBuilder.GetEmailBodyTemplate(env, "FreeKit", xmlDoc);
				try
				{
					efundraising.Email.SendMail.Send(efundraising.Configuration.ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"], 
						"\"" + env.PartnerInfo.PartnerName + "" + "<services@efundraising.com>", 
						env.LeadObject.Email,"","", "","", email.Subject, "", email.Body);
														
				} 
				catch(Exception ex) 
				{
					Logger.LogError(ex);
				} 
			}*/
		}

		protected void SendLeadConfirmationEmail() 
		{	/*		
			try 
			{
				Session[Global.SessionKeys.FormClicked] = null;
				eFundEnv env = eFundEnv.Create();
				Lead lead = env.LeadObject;
					
				// Send the email
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(Server.MapPath(@"~\EmailTemplate\EmailTemplate.xml"));

				EmailTemplate.EmailPreview email = 
					EmailTemplate.EmailBodyBuilder.GetEmailBodyTemplate(env, "AddNewLeadReport", xmlDoc);

				// Send email to all people interested to receive reports
				for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("EFundraisingWeb.Leads.Report"); i++)
				{	
					efundraising.Email.SendMail.AsyncSend(efundraising.Configuration.ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"],
						"services@efundraising.com", ApplicationSettings.GetConfig()["EFundraisingWeb.Leads.Report", i, "email"],
						null, null, null, "",  email.Subject, "", email.Body);
				}
			} 
			catch(Exception ex) 
			{
				Logger.LogError(ex);
			}	*/
		}		

		#endregion

		#region Globalizer Methods
        
		public void GlobalizerSettings(System.Web.Caching.Cache cache, System.Web.SessionState.HttpSessionState session, System.Web.HttpRequest request, efundraising.efundraisingCore.eFundEnv pEFundEnv) 
		{/*
			// in the aspx files, the ASPXFile tags contains a path to the page
			// configuration.  This path in production might differ from the one
			// in dev.  So the 2 paths are set in the c:\GlobalizerConfig\GlobalizerConfig.xml
			// configuration file.
			efundraising.Web.UI.UIControls.BaseConfig.GlobalizerConfigs gcs = new efundraising.Web.UI.UIControls.BaseConfig.GlobalizerConfigs();
			try 
			{
				gcs.LoadXML();
				if(gcs != null) 
				{
					efundraising.Web.UI.UIControls.BaseConfig.GlobalizerConfig gc = gcs.GetGlobalizerConfigByName("efundraising");
					if(gc != null) 
					{
						SetCurrentBaseProjectPath(gc.BaseProjectFileName);
						SetCurrentProductionBaseProjectPath(gc.ProductionBaseProjectFilename);
					}
				}
			} 
			catch (Exception ex) 
			{
				Logger.LogError("Unable to load c:\\GlobalizerConfig\\GlobalizerConfig.xml", ex);
			}
			
			
			#region Set Default Culture and Partner ID

			if (Request.QueryString["culture"] != null)
			{
				pEFundEnv.CultureName = Request.QueryString["culture"];
				pEFundEnv.Save();
				SetCurrentCulture(Request.QueryString["culture"]);
				DynTags["Culture"] = Request.QueryString["culture"];
			}
			else
			{
				if (GetCurrentCulture() == "")
				{
					pEFundEnv.CultureName = "en-US";
					pEFundEnv.Save();
					SetCurrentCulture("en-US");
					DynTags["Culture"] = "en-US";
				}
				// to set fr-CA as the default culture when first landing on denali/alaska page
				if (Request.Url.AbsoluteUri.ToLower().IndexOf("denali") > 0 || Request.Url.AbsoluteUri.ToLower().IndexOf("alaska") > 0)
				{
					if (Session["_DENALI_NOTFIRSTVIEW_"] == null)
					{
						Session["_DENALI_NOTFIRSTVIEW_"] = "true";
						if (Request.QueryString["culture"] != null)
						{
							if (Request.QueryString["culture"] == "en-US")
							{
								pEFundEnv.CultureName = "en-US";
								SetCurrentCulture("en-US");
								DynTags["Culture"] = "en-US";
								pEFundEnv.Save();
							}
							else
							{
								pEFundEnv.CultureName = "fr-CA";
								SetCurrentCulture("fr-CA");
								DynTags["Culture"] = "fr-CA";
								pEFundEnv.Save();
							}
						}
						else
						{
							pEFundEnv.CultureName = "fr-CA";
							SetCurrentCulture("fr-CA");
							DynTags["Culture"] = "fr-CA";
							pEFundEnv.Save();
						}
						
						// For Alaska counter
#if !DEBUG
						
						if (File.Exists(@"C:\log\alaska\counter.txt"))
						{
							TextReader tr = null;
							TextWriter tw = null;
							try
							{
								tr = new StreamReader(@"C:\log\alaska\counter.txt");
								int count = int.Parse(tr.ReadLine());
								tr.Close();
								tw = new StreamWriter(@"C:\log\alaska\counter.txt");
								tw.Write(++count);
								tw.Close();
							}
							catch (Exception ex)
							{
							
							}
						}
						
#endif
					}
				}
			}

			if(GetCurrentPartnerID() == "")
				SetCurrentPartnerID(pEFundEnv.PartnerInfo.PartnerID.ToString());


			#region DYNAMIC TAGS
			// UI setting
			// the dynamic tags are used only for displaying
			// images, text, etc.  they are compatible with
			// the globalizer components and the asp tags.
			if (session["DynTags"] == null) 
			{

				// template path is contained into an xml configuration file
				// eg. (images/<template_path>/<theme>/...)
				if (cache["eFundPartnerPath"] != null)
					DynTags["TemplatePath"] = "_efund_";
				else 
					DynTags["TemplatePath"] = "_efund_";
				
				try 
				{
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.Load(Server.MapPath(@"~\Xml\PartnerTheme.xml"));
					if (Request.QueryString[Global.QueryStringKeys.PartnerId] == null)
						DynTags["Theme"] = XML.PartnerTheme.GetThemeName(0, xmlDoc);
					else
						DynTags["Theme"] = XML.PartnerTheme.GetThemeName(int.Parse(Request.QueryString[Global.QueryStringKeys.PartnerId]), xmlDoc);

				} 
				catch 
				{
					DynTags["Theme"] = "_classic_";
				}
				
				DynTags["PartnerName"] = pEFundEnv.PartnerInfo.PartnerName;
				DynTags["PhoneNumber"] = pEFundEnv.PartnerInfo.PhoneNumber;
				DynTags["_PARTNER_FOLDER_"] = pEFundEnv.PartnerInfo.PartnerFolder;

				session["DynTags"] = DynTags;
			} 
			else
				DynTags = (efundraising.Web.UI.UIControls.DynTag)Session["DynTags"];

			// these can be modified through the url
			if(request["theme"] != null) 
			{
				DynTags["Theme"] = request["theme"];
				session["DynTags"] = DynTags;
			}
			// end of url dynamic tags modification
			
			// tag for adding the partner folder name in the href's in html code
			session["_PARTNER_FOLDER_"] = pEFundEnv.PartnerInfo.PartnerFolder;
			// tag for adding the partner folder name in globalizer controls
			DynTags["_PARTNER_FOLDER_"] = pEFundEnv.PartnerInfo.PartnerFolder;
			session["DynTags"] = DynTags;

			#endregion

			#endregion*/

		}


		#region Change page content at Render time		
		// HTML written in aspx files are contained into LiteralControl components,
		// we loop through the Controls to get the LiteralControls to change the 
		// text dynamically.  ** IMPORTANT ** The use of asp tags (<%= blah %>) will
		// make this method failed to work properly, if you use these tags, please make
		// sure you set the paths in the code behind of the page. (DynTags["TemplatePath"] & DynTags["Theme"])
		private void ParseLiteralControls(Control c) 
		{
			if(c is LiteralControl) 
			{
				
				LiteralControl lit = (LiteralControl)c;

				// insert the partner folder of the current partner at the
				// end of all the hrefs containing _PARTNER_FOLDER_
				if(lit.Text.IndexOf("_PARTNER_FOLDER_") > 0)
				{
					if (Session["_PARTNER_FOLDER_"] != null)
						lit.Text = lit.Text.Replace("_PARTNER_FOLDER_", Session["_PARTNER_FOLDER_"].ToString());
					else
						lit.Text = lit.Text.Replace("_PARTNER_FOLDER_", "efundraising");
					
				}
				
				// insert the correct path in the href when a
				// control is used in a subfolder
				if(lit.Text.IndexOf("_PRE_PATH_") > 0)
				{
					if (Session["_PRE_PATH_"] != null)
						lit.Text = lit.Text.Replace("_PRE_PATH_", Session["_PRE_PATH_"].ToString());					
					else
						lit.Text = lit.Text.Replace("_PRE_PATH_", "");					
				}
								
			}
			else if(c is HyperLink)
			{
				HyperLink lit = (HyperLink)c;
				
				// insert the partner folder of the current partner at the
				// end of all the hrefs containing _PARTNER_FOLDER_
				if(lit.NavigateUrl.IndexOf("_PARTNER_FOLDER_") > 0)
				{
					if (Session["_PARTNER_FOLDER_"] != null)
						lit.NavigateUrl = lit.NavigateUrl.Replace("_PARTNER_FOLDER_", Session["_PARTNER_FOLDER_"].ToString());
					else
						lit.NavigateUrl = lit.NavigateUrl.Replace("_PARTNER_FOLDER_", "efundraising");
					
				}
			}

			foreach(Control cc in c.Controls) 
			{
				if(cc is efundraising.Web.UI.UIControls.ButtonPanelControl) 
				{
					foreach(System.Web.UI.Control uc in cc.Controls) 
					{
						if(uc is ImageButton) 
						{
							ImageButton ib = (ImageButton)uc;
							ib.Attributes.Add("OnClick", "if(ShowFeedBackForm) ShowFeedBackForm(); ");
						}
					}
				}
				ParseLiteralControls(cc);
			}
		}

		// If a partner doesn't have the same resources as the efundraising partner,
		// this method will change the resources path from _mentor_ to <templatepath>
		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")] 
		protected override void Render(HtmlTextWriter writer) 
		{
			if(HasControls()) 
			{
				foreach(Control c in Controls) 
				{
					ParseLiteralControls(c);
				}
			}
			base.Render(writer);			
		}
		#endregion

		#endregion
		
	}
}
