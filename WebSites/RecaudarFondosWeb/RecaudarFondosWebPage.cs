using System;
using System.Text;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using efundraising.RecaudarFondosWeb;
using GA.BDC.Core.Web.UI.Tracking.Omniture;
using GA.BDC.Core.efundraisingCore;


namespace efundraising.RecaudarFondosWeb
{
	
	/// <summary>
	/// Summary description for RecaudarFondosWebPage.
	/// </summary>
	public class RecaudarFondosWebPage : System.Web.UI.Page
	{
		private string addPartner = "?partner=";
		private string addPromotion ="&promotion_id=";
		public RecaudarFondosWebPage()
		{

		}

		#region Omniture Tracking Methods
		
		private Components.Server.Omniture.RecaudarFondosOmniture _recaudarFondosOmnitureTracking = null;

		protected Components.Server.Omniture.RecaudarFondosOmniture recaudarFondosOmnitureTracking 
		{
			get 
			{
				if (_recaudarFondosOmnitureTracking == null)
					_recaudarFondosOmnitureTracking = new Components.Server.Omniture.RecaudarFondosOmniture (GetOmnitureJSFileName(this.Page.Request));
				return _recaudarFondosOmnitureTracking;
			}
		}
		
		protected override void OnPreRender(EventArgs e) 
		{
			if (_recaudarFondosOmnitureTracking != null && _recaudarFondosOmnitureTracking.GetPageName().Trim() != string.Empty && _recaudarFondosOmnitureTracking.JSFileName.Trim() != string.Empty )
				RegisterStartupScript ("OmnitureTracking", _recaudarFondosOmnitureTracking.FetchScriptBlock ());
			base.OnPreRender (e);	
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

		#endregion

		#region Added to introduce "Partner" Concept

		protected override void OnInit(EventArgs e) 
		{
			
					
			if(!IsPostBack) 
			{
				
				if(Session["partner"] == null)
				{
					if (Request.QueryString["partner"] != null && Request.QueryString["partner"] != "")
					{
						StringBuilder token = new StringBuilder();
						Session["partner"] = Request.QueryString["partner"];
						token.Append(Request.Url.AbsolutePath);
						/*if(Request.Url.AbsolutePath.LastIndexOf("?") != -1)
						{
							addPartner = addPartner.Replace("?", "&");
						}*/
						token.Append(addPartner);
						token.Append(Session["partner"].ToString());
						if(Session["promotion_id"]== null)
						{
							if(Request.QueryString["promotion_id"] != null && Request.QueryString["promotion_id"] != "")
							{
								token.Append(addPromotion);
								Session["promotion_id"] = Request.QueryString["promotion_id"];
								token.Append(Request.QueryString["promotion_id"]);
							}
						}
						Response.Redirect(token.ToString());
					}
				}
				else
				{
					if (Request.QueryString["partner"] == null )
					{
						StringBuilder token = new StringBuilder();
						token.Append(Request.Url.AbsolutePath);
						/*if(Request.Url.AbsolutePath.LastIndexOf("?") != -1)
						{
							addPartner = addPartner.Replace("?", "&");
						}*/
						token.Append(addPartner);
						token.Append(Session["partner"].ToString());
						if(Session["promotion_id"] != null)
						{
							token.Append(addPromotion);
							token.Append(Session["promotion_id"].ToString());
						}
						else if(Request.QueryString["promotion_id"] != null && Request.QueryString["promotion_id"] != "")
						{
							token.Append(addPromotion);
							Session["promotion_id"] = Request.QueryString["promotion_id"];
							token.Append(addPromotion);
							token.Append(Request.QueryString["promotion_id"]);
						}
						Response.Redirect(token.ToString());
					}
				}
				
			}
				
			base.OnInit(e);
		}

		#endregion
	}
}
