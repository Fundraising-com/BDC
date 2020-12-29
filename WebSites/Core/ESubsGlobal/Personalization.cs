//
// 2005-08-18 - Stephen Lim - New class.
//


using System;
using System.IO;
using System.Xml;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.ESubsGlobal.DataAccess;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal
{
	using GA.BDC.Core.ESubsGlobal.Stats;

	/// <summary>
	/// Abstract class for personalization.
	/// </summary>
	[Serializable]
	public class Personalization : EnvironmentBase
	{
		protected int _personalizationId = int.MinValue;
		protected int _eventParticipationID = int.MinValue;
		protected string _headerTitle1 = null;
		protected string _headerTitle2 = null;
		protected string _body = null;
		protected decimal _fundraisingGoal = decimal.MinValue;
		protected string _siteBackgroundColor = null;
		protected string _headerBackgroundColor = null;
		protected string _headerTextColor = null;
		protected string _groupUrl = null;
		protected string _imageUrl = null;
        protected byte _imageMotivator = 0;
        protected string _redirect = null;
        protected byte _displayGroupMessage = 0;
        protected byte _skip = 0;
        protected byte _remind_later = 0;

		private static string xmlLocation = "http://my.fundraising.com/resources/xml/defaultpersonalization.xml";
        private static readonly object OBJECT_CACHE_LOCK = new object();
		private static Hashtable PersonalizationPartnerList = Hashtable.Synchronized(new Hashtable());

		public Personalization() 
		{

		}

        public Personalization(int eventParticipationID, string headerTitle1, string headerTitle2, string body,
			decimal fundraisingGoal, string siteBackgroundColor, string headerBackgroundColor, string headerTextColor,
            string groupUrl, string imageUrl, byte imageMotivator, string redirect, byte displayGroupMessage)
        {
			_eventParticipationID = eventParticipationID;
			_headerTitle1 = headerTitle1;
			_headerTitle2 = headerTitle2;
			_body = body;
			_fundraisingGoal = fundraisingGoal;
			_siteBackgroundColor = siteBackgroundColor;
			_headerBackgroundColor = headerBackgroundColor;
			_headerTextColor = headerTextColor;
			_groupUrl = groupUrl;
			_imageUrl = imageUrl;
            _imageMotivator = imageMotivator;
            _redirect = redirect;
            _displayGroupMessage = displayGroupMessage;
		}

		public void UpdateIntoDatabase(EventParticipation ev) {
			this.EventParticipationID = ev.EventParticipationID;
			UpdateIntoDatabase();
		}

		public void UpdateIntoDatabase() {
			try 
			{
				DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
				dbo.UpdatePersonalization(this);
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException(ex.Message, ex, this);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>This method is kept for backward compatibility and should not be used.</remarks>
		/// <param name="eventParticipationID"></param>
		public void InsertIntoDatabase(int eventParticipationID) 
		{
			_eventParticipationID = eventParticipationID;
			InsertIntoDatabase();
		}
		
		public void InsertIntoDatabase() {//int eventParticipationID) {
			try 
			{
				DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
				//_eventParticipationID = eventParticipationID;
				dbo.InsertPersonalization(_eventParticipationID, HeaderTitle1, HeaderTitle2, Body, FundraisingGoal,
					SiteBackgroundColor, HeaderBackgroundColor, HeaderTextColor,
					GroupUrl, ImageUrl, ImageMotivator, ref _personalizationId, this.DisplayGroupMessage, Redirect, Skip);
			}
			catch (Exception ex)
			{
				throw new ESubsGlobalException(ex.Message, ex, this);
			}
		}

		/// <summary>
		/// Get parent personalization settings.
		/// </summary>
		/// <param name="ev"></param>
		public static Personalization CreateByParentEventParticipation(EventParticipation ev)
		{
			if (ev != null)
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				return dbo.GetParentPersonalization(ev.EventParticipationID);
			}

			return null;
		}

        /// <summary>
        /// Get parent personalization settings.
        /// </summary>
        /// <param name="ev"></param>
        public static Personalization CreateByParentEventParticipationID(int ev_id)
        {
            if (ev_id != int.MinValue)
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                return dbo.GetParentPersonalization(ev_id);
            }

            return null;
        }

		/// <summary>
		/// Get parent personalization settings.
		/// </summary>
		/// <param name="ev"></param>
		public static Personalization CreateByEventParticipation(EventParticipation ev)
		{
			if (ev != null)
			{
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				return dbo.GetPersonalization(ev.EventParticipationID);
			}

			return null;
		}

        /// <summary>
        /// Get personalization settings.
        /// </summary>
        /// <param name="ev"></param>
        public static Personalization CreateByEventParticipationID(int ev_id)
        {
            if (ev_id != int.MinValue)
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                return dbo.GetPersonalization(ev_id);
            }

            return null;
        }

		public static Personalization GetCurrentPersonalization(EventParticipation ev) {
			if(ev != null) {
				ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
				return dbo.GetCurrentPersonalization(ev.EventParticipationID);
			} 

			return null;
		}

        public static List<Personalization> GetPersonalizationByRedirect(string redirect)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetPersonalizationByRedirect(redirect);
        }

        public static Personalization GetCurrentPersonalizationByID(int ev_id)
        {
            if (ev_id != int.MinValue)
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                return dbo.GetCurrentPersonalization(ev_id);
            }

            return null;
        }

		public static void ClearPersonalizationPartnerList()
		{
			PersonalizationPartnerList.Clear();
		}

		public static DefaultPersonalization.PersonalizationSection 
			GetPersonalizationSection(Partner partnerValue, Culture culture, string personalizationSectionID)
		{
			if (PersonalizationPartnerList.Count < 1)
			{
				// Lock to avoid two processes access to the list.
				lock (OBJECT_CACHE_LOCK)
				{
					if (PersonalizationPartnerList.Count < 1)
					{
						DefaultPersonalization.Personalizations pers = new GA.BDC.Core.ESubsGlobal.DefaultPersonalization.Personalizations();
						try
						{
							pers.LoadXML(xmlLocation);
						}
						catch (Exception)
						{
							return null;
						}
						for (int i=0; i < pers.PartnerList.Count; i++)
						{
							DefaultPersonalization.Partner partner = (DefaultPersonalization.Partner)pers.PartnerList[i];
							if (partner != null)
							{
								// Store personalization in the hastable with its partner ID.
								PersonalizationPartnerList[partner.ID] = partner; 
							}
						}
					}
				}
			}

			DefaultPersonalization.Partner p = PersonalizationPartnerList[partnerValue.PartnerID.ToString()] as  DefaultPersonalization.Partner;
			if (p == null) // If the partner does not have its personalization, get the default personalization.
				p = PersonalizationPartnerList["Default"] as  DefaultPersonalization.Partner;
			if (p != null)
			{
				for (int i=0; i < p.CulturesList.Count; i++)
				{
					DefaultPersonalization.Cultures culs = p.CulturesList[i] as DefaultPersonalization.Cultures;
					if (culs != null)
					{
						DefaultPersonalization.Culture cul = culs.GetCultureByID(culture.CultureCode);
						if (cul != null)
						{
							for (int k=0 ; k < cul.PersonalizationSectionsList.Count; k++)
							{
								DefaultPersonalization.PersonalizationSections perSections = cul.PersonalizationSectionsList[k] as DefaultPersonalization.PersonalizationSections;
								DefaultPersonalization.PersonalizationSection foundSection = perSections.GetPersonalizationSectionByID(personalizationSectionID);
								return foundSection;
							}
						}
					}
				}
			}
			return null;
		}



		public static Personalization CreateDefaultParticipantPersonalization(EventParticipation evp, Partner partner, Culture culture, 
			string groupName, string userName)
		{
            string redirect = string.Empty;
            Event e = Event.GetEventByEventID(evp.EventID);

            if (e != null)
                redirect = e.Redirect;

			try
			{
				DefaultPersonalization.PersonalizationSection perSection = GetPersonalizationSection(partner, culture, "Participant"); 
				if (perSection != null)
				{
					string headerTitle2 = perSection.HeaderTitle2.Replace("[Participant Name]", userName); 
					Decimal theDec = Decimal.Zero;
					try
					{
						theDec = Decimal.Parse(perSection.FundraisingGoal);
					}
					catch (Exception)
					{
						theDec = Decimal.Zero;
					}

                    Personalization personalization = new Personalization(evp.EventParticipationID, perSection.HeaderTitle1, headerTitle2,
						perSection.Body, theDec, perSection.SiteBGColor, perSection.HeaderBGColor,
						perSection.HeaderColor, perSection.GroupURL, perSection.ImageURL, (byte)0, redirect, (byte)1);

					return personalization;
				}
                return ExCreateDefaultParticipantPersonalization(evp, partner, culture, groupName, userName, redirect);
			}
			catch (Exception)
			{
                return ExCreateDefaultParticipantPersonalization(evp, partner, culture, groupName, userName, redirect);
			}

		}

        public static string GetSponsorRedirectPage(int eventID)
        {
            EventParticipation ep = null;
            List<Personalization> lstperso = Personalization.GetPersonalizationByEventID(eventID);

            if (lstperso != null)
            {
                if (lstperso.Count == 1)
                {
                    return lstperso[0].Redirect;
                }
                else if (lstperso.Count > 1)
                {
                    foreach (var p in lstperso)
                    {
                        if (p.EventParticipationID != int.MinValue)
                        {
                            ep = EventParticipation.GetEventParticipationByEventParticipationID(p.EventParticipationID);
                            if (ep != null)
                            {
                                if (ep.ParticipationChannel.ParticipationChannelID == ParticipationChannel.SponsorCreated.ParticipationChannelID)
                                    return p.Redirect;
                            }
                        }
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public static string GetParticipantRedirectPage(int eventID, int personalization_id)
        {
            //List<Personalization> lstperso = Personalization.GetPersonalizationByEventID(eventID);
            //if (lstperso != null && lstperso.Count > 0)
            //{

            //    return lstperso[0].Redirect;
            //}
            return "";
        }

        public static List<Personalization> GetPersonalizationByEventID(int eventID)
        { 
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetPersonalizationByEventID(eventID);
        }

		private static Personalization ExCreateDefaultParticipantPersonalization(EventParticipation evp, Partner partner, Culture culture, 
			string groupName, string userName, string redirect)
		{	
			// Default lookup
			string partnerId = "";
			string cultureCode = "en-US";

			// Overwrite lookup from custom parameters
			if (partner != null)
				partnerId = partner.PartnerID.ToString();

			if (culture != null)
				cultureCode = culture.CultureCode;

			// Find the best matching personalization
			int persIndex = -1;
			for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("ESubsGlobal.Personalization.Participant"); i++)
			{
				// Locate default personalization
				if (ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", i, "partnerId"] == "" &&
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", i, "culture"] == "en-US")
				{
					persIndex = i;
				}

				// Locate partner personalization. If partner personalization found, 
				// setting the index here will overwrite the default index.
				if (ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", i, "partnerId"] == partnerId &&
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", i, "culture"] == cultureCode)
				{
					persIndex = i;
					break;
				}
			}

			// If default or partner index is -1, this means personalization does not exists. We should quietly return null.
			if (persIndex == -1)
			{
				return null;
			}
			else
			{
				// Build our personalization object
				return new Personalization(evp.EventParticipationID, 
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", persIndex, "headerTitle1"].Replace("[Group Name]", groupName),
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", persIndex, "headerTitle2"],
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", persIndex, "body"],
					decimal.Parse(ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", persIndex, "fundraisingGoal"]),
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", persIndex, "siteBgColor"],
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", persIndex, "headerBgColor"],
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", persIndex, "headerColor"],
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", persIndex, "groupUrl"],
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Participant", persIndex, "imageUrl"],
                    (byte)0, redirect, (byte)1);
			}
		}

		#region Sponsors

        public static Personalization GetSponsorPersonalizationByRedirect(string redirect)
        {
            ESubsGlobal.DataAccess.ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetSponsorPersonalizationByRedirect(redirect);
        }

		public static Personalization CreateDefaultSponsorPersonalization(EventParticipation evp, Partner partner, Culture culture, string groupName, string userName)
		{
            string redirect = string.Empty;
            Event e = Event.GetEventByEventID(evp.EventID);

            if (e != null)
                redirect = e.Redirect;

			//return ExCreateDefaultSponsorPersonalization(evp, partner, culture, groupName, userName);
			try
			{
				DefaultPersonalization.PersonalizationSection perSection = GetPersonalizationSection(partner, culture, "Sponsor"); 
				if (perSection != null)
				{
					string headerTitle1 = perSection.HeaderTitle1.Replace("[Group Name]", groupName); 
                    string body = perSection.Body.Replace("[Group Name]", groupName).Replace("[Sponsor Name]", userName); ; 
					Decimal theDec = Decimal.Zero;
					try
					{
						theDec = Decimal.Parse(perSection.FundraisingGoal);
					}
					catch (Exception)
					{
						theDec = Decimal.Zero;
					}
					Personalization personalization = new Personalization(evp.EventParticipationID, headerTitle1, perSection.HeaderTitle2,
                        body, theDec, perSection.SiteBGColor, perSection.HeaderBGColor,
						perSection.HeaderColor, perSection.GroupURL, perSection.ImageURL, (byte)1, redirect, (byte)0);

					return personalization;
				}
                return ExCreateDefaultSponsorPersonalization(evp, partner, culture, groupName, userName, redirect);
			}
			catch (Exception ex)
			{
                GA.BDC.Core.Diagnostics.Logger.LogError("error in 'CreateDefaultSponsorPersonalization'", ex);
                return ExCreateDefaultSponsorPersonalization(evp, partner, culture, groupName, userName, redirect);
			}
		}

        private static Personalization ExCreateDefaultSponsorPersonalization(EventParticipation evp, Partner partner, Culture culture, string groupName, string userName, string redirect)
		{	
			// Default lookup
			string partnerId = "";
			string cultureCode = "en-US";

			// Overwrite lookup from custom parameters
			if (partner != null)
				partnerId = partner.PartnerID.ToString();

			if (culture != null)
				cultureCode = culture.CultureCode;

			// Find the best matching personalization
			int persIndex = -1;
			for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("ESubsGlobal.Personalization.Sponsor"); i++)
			{
				// Locate default personalization
				if (ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", i, "partnerId"] == "" &&
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", i, "culture"] == "en-US")
				{
					persIndex = i;
				}
				
				// Locate partner personalization. If partner personalization found, 
				// setting the index here will overwrite the default index.
				if (ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", i, "partnerId"] == partnerId &&
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", i, "culture"] == cultureCode)
				{
					persIndex = i;
					break;
				}
			}


			// If default or partner index is -1, this means personalization does not exists. We should quietly return null.
			if (persIndex == -1)
			{
				return null;
			}
			else
			{
				// Build our personalization object
				return new Personalization(evp.EventParticipationID, 
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", persIndex, "headerTitle1"].Replace("[Group Name]", groupName),
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", persIndex, "headerTitle2"],
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", persIndex, "body"],
					decimal.Parse(ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", persIndex, "fundraisingGoal"]),
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", persIndex, "siteBgColor"],
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", persIndex, "headerBgColor"],
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", persIndex, "headerColor"],
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", persIndex, "groupUrl"],
					ApplicationSettings.GetConfig()["ESubsGlobal.Personalization.Sponsor", persIndex, "imageUrl"],
                    (byte)1, redirect, (byte)0);
			}
		}
		
		#region Static Methods		

		public static string FormatDifference(Personalization p1, Personalization p2, string header)
		{
			if (p1 == null || p2 == null)
				return string.Empty;

			string result = string.Empty;
			if (p1.HeaderTitle1 != p2.HeaderTitle1)
				result += "," + string.Format("{0}_{1}", header, "HeaderTitle1");
			
			if (p1.HeaderTitle2 != p2.HeaderTitle2)
				result += "," + string.Format("{0}_{1}", header, "HeaderTitle2");
			
			if (p1.Body != p2.Body)
				result += "," + string.Format("{0}_{1}", header, "Body");

			if (p1.FundraisingGoal != p2.FundraisingGoal)
				result += "," + string.Format("{0}_{1}", header, "FundraisingGoal");

			if (p1.HeaderBackgroundColor != p2.HeaderBackgroundColor)
				result += "," + string.Format("{0}_{1}", header, "HeaderBackgroundColor");

			if (p1.HeaderTextColor != p2.HeaderTextColor)
				result += "," + string.Format("{0}_{1}", header, "HeaderTextColor");

			if (p1.SiteBackgroundColor != p2.SiteBackgroundColor)
				result += "," + string.Format("{0}_{1}", header, "SiteBackgroundColor");

			if (p1.GroupUrl != p2.GroupUrl)
				result += "," + string.Format("{0}_{1}", header, "GroupUrl");

			if (p1.ImageUrl != p2.ImageUrl)
				result += "," + string.Format("{0}_{1}", header, "ImageUrl");


			return result;
		}


		private static string removeChar(string strValue, string charRemoved)
		{
			if (strValue.Length > 0 && strValue.Substring(0,1) == charRemoved)
				return strValue.Substring(1);

			return strValue;
		}

		public static StatsPersonalization[] CreateStatsPersonalization(Personalization original, 
			Personalization p2, StatsPersonalizationSection section)
		{
			if (original == null || p2 == null || section == null)
				return null;

			
			ArrayList arList = new ArrayList();
			StatsPersonalization result = null;

			if (original.HeaderTitle1 != p2.HeaderTitle1)
			{
				result = new StatsPersonalization();
				result.StatsPersonalizationSectionId = section.StatsPersonalizationSectionId;
				result.StatsPersonalizationItemId = StatsPersonalizationItem.HeaderTitle1.StatsPersonalizationItemId;
				arList.Add(result);
			}
			
			if (original.HeaderTitle2 != p2.HeaderTitle2)
			{
				result = new StatsPersonalization();
				result.StatsPersonalizationSectionId = section.StatsPersonalizationSectionId;
				result.StatsPersonalizationItemId = StatsPersonalizationItem.HeaderTitle2.StatsPersonalizationItemId;
				arList.Add(result);
			}
			
			if (original.Body != p2.Body)
			{
				result = new StatsPersonalization();
				result.StatsPersonalizationSectionId = section.StatsPersonalizationSectionId;
				result.StatsPersonalizationItemId = StatsPersonalizationItem.MessageBody.StatsPersonalizationItemId;
				arList.Add(result);
			}

			if (original.FundraisingGoal != p2.FundraisingGoal)
			{
				result = new StatsPersonalization();
				result.StatsPersonalizationSectionId = section.StatsPersonalizationSectionId;
				result.StatsPersonalizationItemId = StatsPersonalizationItem.Goal.StatsPersonalizationItemId;
				arList.Add(result);
			}

			//string orig = removeChar(original.HeaderBackgroundColor, "#");
			//string newValue = removeChar(p2.HeaderBackgroundColor, "#");
            string orig = "";
            string newValue = "";

			if (string.Compare(orig,newValue, true) != 0)
			{
				result = new StatsPersonalization();
				result.StatsPersonalizationSectionId = section.StatsPersonalizationSectionId;
				result.StatsPersonalizationItemId = StatsPersonalizationItem.BackgroundColor.StatsPersonalizationItemId;
				arList.Add(result);
			}

			//orig = removeChar(original.HeaderTextColor, "#");
			//newValue = removeChar(p2.HeaderTextColor, "#");

			if (string.Compare(orig,newValue, true) != 0)
			{
				result = new StatsPersonalization();
				result.StatsPersonalizationSectionId = section.StatsPersonalizationSectionId;
				result.StatsPersonalizationItemId = StatsPersonalizationItem.ForegroundColor.StatsPersonalizationItemId;
				arList.Add(result);
			}
			
			

//			if (original.SiteBackgroundColor != p2.SiteBackgroundColor)
//			{
//				IsDifferent = true;
//				result.StatsPersonalizationItemId = 1;
//			}

//			if (original.GroupUrl != p2.GroupUrl)
//			{
//				IsDifferent = true;
//				result.StatsPersonalizationItemId = 1;
//			}


			//orig = removeAllRelativePath(original.ImageUrl);
			//newValue = removeAllRelativePath(p2.ImageUrl);
            /*
			if (string.Compare(orig, newValue, true) !=0)
			{
				if (p2.ImageUrl.ToUpper().IndexOf("DUMPTEMP") > -1)
				{
					result = new StatsPersonalization();
					result.StatsPersonalizationSectionId = section.StatsPersonalizationSectionId;
					result.StatsPersonalizationItemId = StatsPersonalizationItem.ImageUploaded.StatsPersonalizationItemId;
				}
				else
				{
					result = new StatsPersonalization();
					result.StatsPersonalizationSectionId = section.StatsPersonalizationSectionId;
					result.StatsPersonalizationItemId = StatsPersonalizationItem.ImageFromLibrary.StatsPersonalizationItemId;
				}
				arList.Add(result);
			}
            */

			return (StatsPersonalization[])arList.ToArray(typeof(StatsPersonalization));
		}

		private static string removeAllRelativePath(string sFileName)
		{
			string sLocal = sFileName;
			while (sLocal.Length > 3 && 
				sLocal.Substring(0, 3) == "../")
			{
				sLocal = sLocal.Substring(3, sLocal.Length-3);
			}
			return sLocal;
		}

		#endregion

		#endregion
        
		#region Properties

		public int PersonalizationId
		{
			get { return _personalizationId; }
			set { _personalizationId = value; }
		}

		public int EventParticipationID {
			get { return _eventParticipationID; }
			set { _eventParticipationID = value; }
		}

		public string HeaderTitle1
		{
			get { return _headerTitle1; }
			set { _headerTitle1 = value; }
		}

		public string HeaderTitle2
		{
			get { return _headerTitle2; }
			set { _headerTitle2 = value; }
		}

		public string Body
		{
			get { return _body; }
			set { _body = value; }
		}

		public decimal FundraisingGoal
		{
			get { return _fundraisingGoal; }
			set { _fundraisingGoal = value; }
		}

		public string SiteBackgroundColor
		{
			get { return _siteBackgroundColor; }
			set { _siteBackgroundColor = value; }
		}

		public string HeaderBackgroundColor
		{
			get { return _headerBackgroundColor; }
			set { _headerBackgroundColor = value; }
		}

		public string HeaderTextColor
		{
			get { return _headerTextColor; }
			set { _headerTextColor = value; }
		}

		public string GroupUrl
		{
			get { return _groupUrl; }
			set { _groupUrl = value; }
		}

		public string ImageUrl
		{
			get { return _imageUrl; }
			set { _imageUrl = value; }
		}

        public byte ImageMotivator
        {
            get { return _imageMotivator; }
            set { _imageMotivator = value; }
        }

        public string Redirect
        {
            set { _redirect = value; }
            get { return _redirect; }
        }

        public byte DisplayGroupMessage
        {
            get { return _displayGroupMessage; }
            set { _displayGroupMessage = value; }
        }

        public byte Skip
        {
            get { return this._skip; }
            set { this._skip = value; }
        }

        public byte RemindLater
        {
            get { return this._remind_later; }
            set { this._remind_later = value; }
        }

		#endregion
	}
}
