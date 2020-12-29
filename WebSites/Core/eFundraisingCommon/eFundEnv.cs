using System;
using System.Web;
using GA.BDC.Core.Database.efundraising;
using GA.BDC.Core.eFundraisingCommon.DataAccess;
using GA.BDC.Core.eFundraisingCommon;
using GA.BDC.Core.BusinessBase;

namespace GA.BDC.Core.eFundraisingCommon
{
    [Serializable()]
    public sealed class eFundEnv
    {
        private const string SESSION_KEY = "eFundCommonEnv";


        #region Fields

        private Partner _partnerInfo = new Partner();
        private int _PartnerID = int.MinValue;

        private string _PartnerName = "";
        private string _PartnerUrl = "";
        private string _eSubsUrl = "";
        private string _CultureName = "";
        private string _Guid = "";


        private string _MailConfigFile = "";
        private string _ApplicationPath = "";

        private string _previousUrl = "www.efundraising.com";
        private string _partnerDefaultPhoneNumber = "1-866-313-8867";

        private int _defaultPromotionID = 10475;
        private int _PromotionID = -1;
        private string externalGroupID = string.Empty;


        private Lead _Lead = new Lead();

        #endregion
        
        
        
        
        public eFundEnv()
        {

        }


        public static eFundEnv Create()
        {
            // Web based
            if (System.Web.HttpContext.Current != null)
            {
                if (System.Web.HttpContext.Current.Session == null ||
                    System.Web.HttpContext.Current.Session[SESSION_KEY] == null)
                {
                    return new eFundEnv();
                }
                else
                {
                    return (eFundEnv)System.Web.HttpContext.Current.Session[SESSION_KEY];
                }
            }
            return null;

        }


        public void Save()
        {
            // Web based
            if (System.Web.HttpContext.Current != null)
            {
                System.Web.HttpContext.Current.Session[SESSION_KEY] = this;
            }
        }


        public void FindPromotion(System.Web.HttpRequestBase request)
        {
            string scriptname = "";

            // check if the visitor comes from a standard promotion id
            if (request.QueryString["pr_id"] != null)
            {
                this.PromotionID = int.Parse(request.QueryString["pr_id"]);
                return;
            }
            else if (request.QueryString["promotion_id"] != null)
            {
                this.PromotionID = int.Parse(request.QueryString["promotion_id"]);
                return;
            }
            else if (request.QueryString["promotion"] != null)
            {
                this.PromotionID = int.Parse(request.QueryString["promotion"]);
                return;
            }

             else if (request.QueryString["a_bid"] != null && request.QueryString["a_aid"] != null)
            {

                string abid = request.QueryString["a_bid"].ToString();
                string aaid = request.QueryString["a_aid"].ToString();
                Promotion env2 = new Promotion();
                Partner partner2 = new Partner();
                Promotion promo = eFundraisingCommon.Promotion.GetPromotionBYABID(abid, aaid);
                if (promo != null)
                {
                    //Promotion.Promotion env2 = new Promotion();
                    env2.PromotionId = promo.PromotionId;
                }
                else
                {
                    int partner = partner2.PartnerID;
                    string promoType = "PAP";
                    string promoName = partner2.PartnerName + "PAP";
                    int displayable = 0;

                    Promotion insert = new Promotion();
                    env2.PromotionId = insert.InsertNewPromo(partner, promoType, promoName, displayable, abid);


                }

}




            // if there's no promotion id, check for other types of promotions

            // GoToast
            else if (request.QueryString["GTSE"] != null)
            {
                scriptname = request.QueryString.GetValues("GTSE").GetValue(0).ToString();
                scriptname = scriptname.Replace(" ", "+");
                scriptname = "GTSE=" + scriptname;
            }

            // CJ
            else if (request.QueryString["PID"] != null)
            {
                scriptname = "CJ_Efundraising";
            }

            //// Google paid keywords vs natural keywords Search
            //else if (referrer.IndexOf("google.") != -1)
            //{
            //    if (request.QueryString["trkid"] != null)
            //    {
            //        scriptname = request.QueryString["trkid"].ToString(); //"CJ_Efundraising";
            //    }
            //}


            // else check if the visitor has a referrer
            if (request.UrlReferrer != null)
            {
                string referrer = request.UrlReferrer.AbsoluteUri;

                // linkshare
                if (referrer.IndexOf("ukfundraising") > 0 || referrer.IndexOf("fundraising.co.uk") > 0)
                {
                    scriptname = "UKFUNDRAISING";
                }


                // search engines
                else
                {
                    referrer = referrer.ToLower();

                    bool isSearchEngine = false;

                    // google
                    if (referrer.IndexOf("google.") != -1)
                    {
                        if (request.QueryString["trkid"] != null)
                        {
                            scriptname = request.QueryString["trkid"].ToString();
                            isSearchEngine = true;
                        }
                        else
                        {
                            scriptname = "SEO_GOOGLE";
                            isSearchEngine = true;
                        }

                    }
                    // yahoo
                    else if (referrer.IndexOf("yahoo.") != -1)
                    {
                        scriptname = "SEO_YAHOO";
                        isSearchEngine = true;
                    }
                    // altavista
                    else if (referrer.IndexOf("altavista.") != -1)
                    {
                        scriptname = "SEO_ALTAVISTA";
                        isSearchEngine = true;
                    }
                    // All The Web
                    else if (referrer.IndexOf("alltheweb.") != -1)
                    {
                        scriptname = "SEO_All_THE_WEB";
                        isSearchEngine = true;
                    }
                    // MSN
                    else if (referrer.IndexOf("msn.") != -1)
                    {
                        scriptname = "SEO_MSN";
                        isSearchEngine = true;
                    }
                    // Lycos
                    else if (referrer.IndexOf("lycos.") != -1)
                    {
                        scriptname = "SEO_LYCOS";
                        isSearchEngine = true;
                    }
                    // excite
                    else if (referrer.IndexOf("excite.") != -1)
                    {
                        scriptname = "SEO_EXITE";
                        isSearchEngine = true;
                    }
                    // ask	
                    else if (referrer.IndexOf("ask.") != -1)
                    {
                        scriptname = "SEO_ASK_JEEVES";
                        isSearchEngine = true;
                    }
                    // aol
                    else if (referrer.IndexOf("aol.") != -1)
                    {
                        scriptname = "SEO_AOL";
                        isSearchEngine = true;
                    }
                    // dogpile
                    else if (referrer.IndexOf("dogpile.") != -1)
                    {
                        scriptname = "SEO_DOGPILE";
                        isSearchEngine = true;
                    }
                    // earthlink
                    else if (referrer.IndexOf("earthlink.") != -1)
                    {
                        scriptname = "SEO_EARTHLINK";
                        isSearchEngine = true;
                    }
                    // netscape
                    else if (referrer.IndexOf("netscape.") != -1)
                    {
                        scriptname = "SEO_NETSCAPE";
                        isSearchEngine = true;
                    }
                    // antibot
                    else if (referrer.IndexOf("antibot.") != -1)
                    {
                        scriptname = "SEO_ANTIBOT";
                        isSearchEngine = true;
                    }
                    // webcrawler
                    else if (referrer.IndexOf("webcrawler.") != -1)
                    {
                        scriptname = "SEO_WEBCRAWLER";
                        isSearchEngine = true;
                    }
                    // mamma
                    else if (referrer.IndexOf("mamma.") != -1)
                    {
                        scriptname = "SEO_MAMMA";
                        isSearchEngine = true;
                    }
                    // ixquick
                    else if (referrer.IndexOf("ixquick.") != -1)
                    {
                        scriptname = "SEO_IXQUICK";
                        isSearchEngine = true;
                    }
                    // snap
                    else if (referrer.IndexOf("snap.") != -1)
                    {
                        scriptname = "SEO_SNAP";
                        isSearchEngine = true;
                    }

                    if (isSearchEngine == true)
                    {
                        if (this.PartnerInfo.PartnerID != 0)
                        {
                            scriptname = "SEARCH_ENGINE-" + this.PartnerInfo.PartnerID;
                        }
                    }
                    else
                    {
                        scriptname = "HTTP_REFERER-" + this.PartnerInfo.PartnerID;
                    }
                }

            }

            // direct link
            else
            {

                if (request.QueryString["trkid"] != null)
                {
                    scriptname = request.QueryString["trkid"].ToString();
                }
                else
                {
                    scriptname = "URL-" + this.PartnerInfo.PartnerID;
                }


            }

            EFRCommonDatabase dbi = new EFRCommonDatabase();
            
            Promotion promotion = Promotion.GetPromotion(scriptname);
            if (promotion != null)
            {
                this.PromotionID = promotion.PromotionId;
            }
            else
            {
                this.PromotionID = 10475;
            }

        }


        public void FindPromotion(System.Web.HttpRequest request)
        {
            string scriptname = "";

            // check if the visitor comes from a standard promotion id
            if (request.QueryString["pr_id"] != null)
            {
                this.PromotionID = int.Parse(request.QueryString["pr_id"]);
                return;
            }
            else if (request.QueryString["promotion_id"] != null)
            {
                this.PromotionID = int.Parse(request.QueryString["promotion_id"]);
                return;
            }
            else if (request.QueryString["promotion"] != null)
            {
                this.PromotionID = int.Parse(request.QueryString["promotion"]);
                return;
            }

            else if (request.QueryString["a_bid"] != null && request.QueryString["a_aid"] != null)
            {

                string abid = request.QueryString["a_bid"].ToString();
                string aaid = request.QueryString["a_aid"].ToString();
                Promotion env2 = new Promotion();
                Partner partner2 = new Partner();
                Promotion promo = eFundraisingCommon.Promotion.GetPromotionBYABID(abid, aaid);
                if (promo != null)
                {
                    //Promotion.Promotion env2 = new Promotion();
                    env2.PromotionId = promo.PromotionId;
                }
                else
                {
                    int partner = partner2.PartnerID;
                    string promoType = "PAP";
                    string promoName = partner2.PartnerName + "PAP";
                    int displayable = 0;

                    Promotion insert = new Promotion();
                    env2.PromotionId = insert.InsertNewPromo(partner, promoType, promoName, displayable, abid);


                }


            }





            // if there's no promotion id, check for other types of promotions

            // GoToast
            else if (request.QueryString["GTSE"] != null)
            {
                scriptname = request.QueryString.GetValues("GTSE").GetValue(0).ToString();
                scriptname = scriptname.Replace(" ", "+");
                scriptname = "GTSE=" + scriptname;
            }

            // CJ
            else if (request.QueryString["PID"] != null)
            {
                scriptname = "CJ_Efundraising";
            }

            //// Google paid keywords vs natural keywords Search
            //else if (referrer.IndexOf("google.") != -1)
            //{
            //    if (request.QueryString["trkid"] != null)
            //    {
            //        scriptname = request.QueryString["trkid"].ToString(); //"CJ_Efundraising";
            //    }
            //}


            // else check if the visitor has a referrer
            if (request.UrlReferrer != null)
            {
                string referrer = request.UrlReferrer.AbsoluteUri;

                // linkshare
                if (referrer.IndexOf("ukfundraising") > 0 || referrer.IndexOf("fundraising.co.uk") > 0)
                {
                    scriptname = "UKFUNDRAISING";
                }


                // search engines
                else
                {
                    referrer = referrer.ToLower();

                    bool isSearchEngine = false;

                    // google
                    if (referrer.IndexOf("google.") != -1)
                    {
                        if (request.QueryString["trkid"] != null)
                        {
                            scriptname = request.QueryString["trkid"].ToString();
                            isSearchEngine = true;
                        }
                        else
                        {
                            scriptname = "SEO_GOOGLE";
                            isSearchEngine = true;
                        }

                    }
                    // yahoo
                    else if (referrer.IndexOf("yahoo.") != -1)
                    {
                        scriptname = "SEO_YAHOO";
                        isSearchEngine = true;
                    }
                    // altavista
                    else if (referrer.IndexOf("altavista.") != -1)
                    {
                        scriptname = "SEO_ALTAVISTA";
                        isSearchEngine = true;
                    }
                    // All The Web
                    else if (referrer.IndexOf("alltheweb.") != -1)
                    {
                        scriptname = "SEO_All_THE_WEB";
                        isSearchEngine = true;
                    }
                    // MSN
                    else if (referrer.IndexOf("msn.") != -1)
                    {
                        scriptname = "SEO_MSN";
                        isSearchEngine = true;
                    }
                    // Lycos
                    else if (referrer.IndexOf("lycos.") != -1)
                    {
                        scriptname = "SEO_LYCOS";
                        isSearchEngine = true;
                    }
                    // excite
                    else if (referrer.IndexOf("excite.") != -1)
                    {
                        scriptname = "SEO_EXITE";
                        isSearchEngine = true;
                    }
                    // ask	
                    else if (referrer.IndexOf("ask.") != -1)
                    {
                        scriptname = "SEO_ASK_JEEVES";
                        isSearchEngine = true;
                    }
                    // aol
                    else if (referrer.IndexOf("aol.") != -1)
                    {
                        scriptname = "SEO_AOL";
                        isSearchEngine = true;
                    }
                    // dogpile
                    else if (referrer.IndexOf("dogpile.") != -1)
                    {
                        scriptname = "SEO_DOGPILE";
                        isSearchEngine = true;
                    }
                    // earthlink
                    else if (referrer.IndexOf("earthlink.") != -1)
                    {
                        scriptname = "SEO_EARTHLINK";
                        isSearchEngine = true;
                    }
                    // netscape
                    else if (referrer.IndexOf("netscape.") != -1)
                    {
                        scriptname = "SEO_NETSCAPE";
                        isSearchEngine = true;
                    }
                    // antibot
                    else if (referrer.IndexOf("antibot.") != -1)
                    {
                        scriptname = "SEO_ANTIBOT";
                        isSearchEngine = true;
                    }
                    // webcrawler
                    else if (referrer.IndexOf("webcrawler.") != -1)
                    {
                        scriptname = "SEO_WEBCRAWLER";
                        isSearchEngine = true;
                    }
                    // mamma
                    else if (referrer.IndexOf("mamma.") != -1)
                    {
                        scriptname = "SEO_MAMMA";
                        isSearchEngine = true;
                    }
                    // ixquick
                    else if (referrer.IndexOf("ixquick.") != -1)
                    {
                        scriptname = "SEO_IXQUICK";
                        isSearchEngine = true;
                    }
                    // snap
                    else if (referrer.IndexOf("snap.") != -1)
                    {
                        scriptname = "SEO_SNAP";
                        isSearchEngine = true;
                    }

                    if (isSearchEngine == true)
                    {
                        if (this.PartnerInfo.PartnerID != 0 && this.PartnerInfo.PartnerID == 814)
                        {
                            scriptname = request.QueryString["trkid"].ToString();
                        }
                        else
                        {
                            scriptname = "SEARCH_ENGINE-" + this.PartnerInfo.PartnerID;
                        }
                    }
                    else
                    {
                        scriptname = "HTTP_REFERER-" + this.PartnerInfo.PartnerID;
                    }
                }

            }

            // direct link
            else
            {

                if (request.QueryString["trkid"] != null)
                {
                    scriptname = request.QueryString["trkid"].ToString();
                }
                else
                {
                    scriptname = "URL-" + this.PartnerInfo.PartnerID;
                }


            }

            EFRCommonDatabase dbi = new EFRCommonDatabase();

            Promotion promotion = Promotion.GetPromotion(scriptname);
            if (promotion != null)
            {
                this.PromotionID = promotion.PromotionId;
            }
            else
            {
                this.PromotionID = 10475;
            }

        }




        #region Properties

        /// <summary>
        /// Get - Set the WebTrackingInfo object
        /// </summary>
        /// <remarks><see cref="WebTrackingInfo"/></remarks>
        //public WebTrackingInfo WebTrackingInfo {
        //	get{ return this._WebTrackInfo; }
        //	set{ this._WebTrackInfo = value; }
        //}

        /// <summary>
        /// Get - Set the MailConfigFile Name
        /// </summary>
        public string MailConfigFile
        {
            get { return this._MailConfigFile; }
            set { this._MailConfigFile = value; }
        }

        /// <summary>
        /// Get - Set the ApplicationPath 
        /// </summary>
        public string ApplicationPath
        {
            get { return this._ApplicationPath; }
            set { this._ApplicationPath = value; }
        }

        /// <summary>
        /// Get - Set PartnerID reference number
        /// </summary>
        public int PartnerID
        {
            get { return this._PartnerID; }
            set { this._PartnerID = value; }
        }

        /// <summary>
        /// Get - Set the PartnerName
        /// </summary>
        public string PartnerName
        {
            get { return this._PartnerName; }
            set { this._PartnerName = value; }
        }

        /// <summary>
        /// Get - Set the PartnerUrl
        /// </summary>
        public string PartnerUrl
        {
            get { return this._PartnerUrl; }
            set { this._PartnerUrl = value; }
        }

        /// <summary>
        /// Get - Set eSubsUrl
        /// </summary>
        public string eSubsUrl
        {
            get { return this._eSubsUrl; }
            set { this._eSubsUrl = value; }
        }

        /// <summary>
        /// Get - Set PreviousUrl
        /// </summary>
        public string PreviousUrl
        {
            get { return this._previousUrl; }
            set { this._previousUrl = value; }
        }


        /// <summary>
        /// Get - Set PartnerPhoneNumber
        /// </summary>
        public string PartnerDefaultPhoneNumber
        {
            get { return this._partnerDefaultPhoneNumber; }
            set { this._partnerDefaultPhoneNumber = value; }
        }

        /// <summary>
        /// Get - Set CultureName
        /// </summary>
        public string CultureName
        {
            get { return this._CultureName; }
            set { this._CultureName = value; }
        }

        /// <summary>
        /// Get - Set the Lead object
        /// </summary>
        /// <remarks><see cref="Lead"/></remarks>
        public Lead LeadObject
        {
            get { return this._Lead; }
            set { this._Lead = value; }
        }

        /// <summary>
        /// Get - Set the PromotionID reference ID
        /// </summary>
        public int PromotionID
        {
            get { return this._PromotionID; }
            set { this._PromotionID = value; }
        }

        /// <summary>
        /// Get - Set the default PromotionID reference ID
        /// </summary>
        public int DefaultPromotionID
        {
            get { return this._defaultPromotionID; }
            set { this._defaultPromotionID = value; }
        }

        public string GUID
        {
            get { return this._Guid; }
            set { this._Guid = value; }
        }

        public Partner PartnerInfo
        {
            get { return _partnerInfo; }
            set { _partnerInfo = value; }
        }

        public string ExternalGroupID
        {
            get { return this.externalGroupID; }
            set { this.externalGroupID = value; }
        }


        //public Lead LeadObject
        //{
        //    get { return this._Lead; }
        //    set { this._Lead = value; }
        //}

        #endregion
    
    
    }



}
