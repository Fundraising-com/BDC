/* Title:	Environment
 * Author:	Jean-Francois Buist
 * Summary:	This object handles all environment objects.  This make the code easier to read
 *			and work with.  In order to fill this object you must use eSubsGlobalEnvironmentParameters. 
 *			DO NOT change private methods to public methods.  The reason is that if you have the member
 *			hierarchy id and the partner id.  This obj gets the partner info from the hierarchy id
 *			so we don't want to reload everything when it's not needed.
 * 
 * Create Date:	August 1, 2005
 * 
 */

#define _CANADA
using System;
using System.Web;
using System.Collections.Generic;
using GA.BDC.Core.ESubsGlobal.Users;
using GA.BDC.Core.eFundraisingCommon;

namespace GA.BDC.Core.ESubsGlobal {

	public enum ReloadEnvironmentAndUserInformationOnInactiveEventStatus {
		ENVIRONMENT_AND_USER_HAS_BEEN_RELOADED,
		CAMPAIGNS_HAVE_BEEN_CLOSED,
		INTERNAL_ERROR
	}

	/// <summary>
	/// This class handle the user environment.
	/// </summary>
	[Serializable]	
	public class eSubsGlobalEnvironment : EnvironmentBase {
		
		#region Constants
		private const string SESSION_KEY = "_ESUBS_ENVIRONMENT_";
		#endregion

		#region Fields
		private Partner _partner = null;
		private Group _group = null;
		private Event _event = null;
		private EventParticipation _eventParticipation = null;
		private Culture _culture = null;
		private Personalization _personalization = null;
		private PrizeCollection _prizeCollection = null;
		private StrongAuthentication strongAuthentication = null;
        private int _touch_id = int.MinValue; //We need to store the touch id in the environment so we can now associate an Order to an Email.
                                              //The touch_id will be included in the store redirect URL.
        private PartnerProfit _partnerProfit = null;
        private Profit _profit = null;
        private List<Profit> _profitCatalogs = null; //This collection will be used to support profit per product but is not implemented yet (1/5/2011)
        private ProfitGroup _profitGroup = null;
		#endregion

		#region Constructors
		public eSubsGlobalEnvironment() 
		{
		}
		#endregion

		#region Methods

		#region Relaunch 

		public ReloadEnvironmentAndUserInformationOnInactiveEventStatus ReloadEnvironmentAndUserInformationOnInactiveEvent(ref eSubsGlobalUser user) {
			// if the user has been identified, and the event is not active,
			// there is a series of rules to follow.
			if(user.HierarchyID > 0) {
				if(!Event.Active) {
					/* If the user is not a sponsor, and get to the web site from a touch
					 * or a URL to an Inactive Event, we call the stored proc that will
					 * place this user under another active event.
					 *  */

					NewParticipationEventOnInactiveCampaign newEventParticipation = 
						eSubsGlobalUser.GetNewParticipantEvent(EventParticipation, user);

					switch(newEventParticipation.status) {
						case NewParticipationEventOnInactiveCampaignStatus.OK:
							eSubsGlobalEnvironmentParameters param 
								= new eSubsGlobalEnvironmentParameters();
							param.EventParticipationID = newEventParticipation.newEventParticipationID;
							Personalization = null;
							Clear();
							Load(param);
							Save();
							user = Users.eSubsGlobalUser.LoadByHierarchyID(EventParticipation.MemberHierarchyID);
							user.Save();
							return ReloadEnvironmentAndUserInformationOnInactiveEventStatus.ENVIRONMENT_AND_USER_HAS_BEEN_RELOADED;
						case NewParticipationEventOnInactiveCampaignStatus.CLOSED:
							Save();
							user.Save();
							return ReloadEnvironmentAndUserInformationOnInactiveEventStatus.CAMPAIGNS_HAVE_BEEN_CLOSED;
						case NewParticipationEventOnInactiveCampaignStatus.INTERNAL_ERROR:
							return ReloadEnvironmentAndUserInformationOnInactiveEventStatus.INTERNAL_ERROR;
					}
				}
			}
			return ReloadEnvironmentAndUserInformationOnInactiveEventStatus.INTERNAL_ERROR;
		}

		#endregion

		#region Wizards Methods

		// return the step 1 (MaxValue means that it goes directly to the zone)
		public int GetSponsorWizardCurrentStep(eSubsGlobalUser user, int eventID) {
			int currentStep = 1;

			bool created = false;
			if(user.IsSponsor) 
				//&& user.IsStrongLogged) 
			{
				if(user.Password != null && user.Password != "") {
					// the user has been created, so bypass step1 and go directly to step 2
					created = true;
				}
			} else {
				currentStep = 1;	// create user
				return currentStep;
			}

			if(Group != null) {
				if(Group.SponsorID == user.HierarchyID) {
					if(created) {
						// the group has been created, so bypass step2 and go directly to step 3
						currentStep = 2;
					}
				} else {
					currentStep = 1;	// create group
					return currentStep;
				}
			}

			if(EventParticipation != null) {	
				if(Personalization.GetCurrentPersonalization(EventParticipation) != null) {
					// if the personalization already exists we bypass step3 and go directly to step 4
					currentStep = 3;
				}
			}

			if(user.IsSponsor) {
				if(eSubsGlobalUser.GetChildMember(user.HierarchyID, eventID) != null && currentStep == 3) {
					currentStep = int.MaxValue;
				}
			}

			return currentStep;
		}
		#endregion

		#region Load Methods
		private void LoadPartnerPrizeCollection() 
		{
			_prizeCollection = PrizeCollection.GetPrizeByPartnerID(_partner.ProgramCollection.GetPrograms()[0].ID, _partner.PartnerID);
		}

		private void LoadDefaults()
		{
            int defaultPartnerID = 0; //Default set to partnerID of USA
			// Get default culture by Sponsor, domain name suffix (.ca or .com ), browser and finally default to en-US if non exists.
			if (_culture == null)
			{
                if (_group != null)
                {
                    // Copy culture from sponsor
                    Users.eSubsGlobalUser user = Users.Sponsor.LoadByHierarchyID(_group.SponsorID);
                    _culture = user.Culture;
                    defaultPartnerID = _group.PartnerID;
                }
                /* The following will be used in prod */
                else
                {
                    _culture = ESubsGlobal.Culture.DEFAULT;
                    defaultPartnerID = _culture.DefaultPartner.PartnerID;
                }
                //else if (HttpContext.Current != null &&
                //    HttpContext.Current.Request.UserLanguages != null &&
                //    HttpContext.Current.Request.UserLanguages.Length > 0)
                //{
                //    // Load member's culture from browser
                //    _culture = Culture.Create(HttpContext.Current.Request.UserLanguages[0]);
                //    if (_culture.CultureCode.ToLower() == "en-ca" || _culture.CultureCode.ToLower() == "fr-ca")
                //        defaultPartnerID = _culture.DefaultPartner.PartnerID;
                //}
                //else
                //{
                //    // Default culture to en-US
                //    _culture = Culture.EN_US;
                //}
			}

			if (_partner == null)
			{
				// Load default efundraising partner
                _partner = Partner.LoadByID(defaultPartnerID, _culture);
			}
		}

		private void LoadCulture(Culture culture)
		{
			if (_culture == null || (culture != null && _culture.CultureCode != culture.CultureCode))
			{
				// Get Culture
				_culture = culture;
			}
		}

		private void LoadByPartner(int partnerID) 
		{
			if (_partner == null || _partner.PartnerID != partnerID)
			{
				// Get partner
				_partner = Partner.LoadByID(partnerID, _culture);

				// Get Prize
				LoadPartnerPrizeCollection();
			}
		}

		private void LoadByPartnerHost(string host) {

			if (_partner == null || _partner.Host != host)
			{
				// Get Partner
				_partner = Partner.LoadByHost(host, _culture);

				// Get Prize
				LoadPartnerPrizeCollection();
			}
		}

		private void LoadByPartnerGUID(string guid) {

			if (_partner == null || _partner.GUID != guid)
			{
				// Get Partner
				try {
					_partner = Partner.LoadByGUID(guid, _culture);
				} catch {
					// if the partner guid is not valid, do not throw an 
					// error, just load the default partner id which is
					// efundraising (0)
					_partner = Partner.LoadByID(0, _culture);
				}

				// Get Prize
				LoadPartnerPrizeCollection();
			}
		}

		private void LoadEventByExternalGroupID(int partnerID, string externalOrganizationID) 
		{
			if (_group == null || _group.ExternalGroupID != externalOrganizationID)
			{
				bool hasChanged = false;
				if(_group != null)
					hasChanged = (_group.ExternalGroupID != externalOrganizationID);

				// Get Group
				_group = Group.LoadByExternalGroupID(partnerID, externalOrganizationID);
				if (_group == null)
				{
                    // Added by Jiro HIdaka (January 20, 2008)
                    Partner p = Partner.LoadByID(partnerID, _culture);
                    if (p != null && p.PartnerLinkCollection != null)
                    {
                        PartnerLink link = p.PartnerLinkCollection.GetPartnerLinkByCountryCode(_culture.CountryCode);
                        if (link != null)
                        {
                            int correct_partnerid = link.LinkedPartnerID;
                            if (partnerID != correct_partnerid)
                            {
                                partnerID = correct_partnerid;
                                _group = Group.LoadByExternalGroupID(partnerID, externalOrganizationID);
                                if (_group == null) return;
                            }
                            else
                                return;
                        }
                        else
                            return;
                    }
                    else
                        return;//throw new ESubsGlobalException ("Invalid external group ID or partner ID", null, this);
				}

				// If at this point, if this member is not a sponsor and 
				// we still don't have the personalization,
				// we need to get the sponsor's personalization which should
				// always exist. This may be last chance to load personalization.

				_event = ESubsGlobal.Event.GetEventByGroupID (_group.GroupID );

				if (_event == null)
					throw new ESubsGlobalException (string.Format ("Can not load event by group ID: {0}", _group.GroupID.ToString ()), null, this);
				if ((_personalization == null && _group != null))
				{
					if(_personalization == null) 
					{
						try 
						{
							// Borrow the eventparticipation from sponsor
							EventParticipation evp = EventParticipation.GetEventParticipationByMemberHierarchyIDandCheckEventID(_group.SponsorID, _event.EventID);
							_personalization = Personalization.GetCurrentPersonalization(evp); //CreateByParentEventParticipation(evp);						
						}
						catch {}
					}
				}

				LoadByPartner(_group.PartnerID);
			}			
		}

		private void LoadByGroup(int groupID) {
			LoadByGroup(groupID, int.MinValue);
		}

		private void LoadByGroup(int groupID, int eventID) {

			if (_group == null || _group.GroupID != groupID)
			{
				bool hasChanged = false;
				if(_group != null)
					hasChanged = (_group.GroupID != groupID);

                if (_eventParticipation != null && eventID != int.MinValue)
                    hasChanged = hasChanged || (_eventParticipation.EventID != eventID);

				// Get Group
				_group = Group.LoadByGroupID(groupID);

                //UPDATE March 30, 2010: When user is unknown supporter, erase the eventparticipation object to resolve the grouppage permanent issue
                bool IsUnknownSupporter = false;
                if (_eventParticipation != null)
                {
                    eSubsGlobalUser user = eSubsGlobalUser.LoadByHierarchyID(_eventParticipation.MemberHierarchyID);
                    if (user.IsUnknownSupporter)
                        IsUnknownSupporter = true;
                }

				// If at this point, if this member is not a sponsor and 
				// we still don't have the personalization,
				// we need to get the sponsor's personalization which should
				// always exist. This may be last chance to load personalization.
				if ((_personalization == null && _group != null) || hasChanged)
				{
					try {
                        //UPDATE March 30, 2010: To resolve the unknown supporters being permanently stuck on a grouppage
                        if (IsUnknownSupporter && hasChanged)
                        {
                            _personalization = null;
                            _eventParticipation = null;
                        }
                        else
                            _personalization = Personalization.CreateByParentEventParticipation(_eventParticipation);	
					} catch {
                        
					}                    

                    if (_personalization == null)
                    {
                        try
                        {
                            _personalization = Personalization.CreateByEventParticipation(_eventParticipation);	
                        }
                        catch { }
                    }

					if(_personalization == null) {
						try {                            
							// Borrow the eventparticipation from sponsor
							EventParticipation evp = EventParticipation.GetEventParticipationByMemberHierarchyIDandCheckEventID(_group.SponsorID, eventID);
							_personalization = Personalization.GetCurrentPersonalization(evp); //CreateByParentEventParticipation(evp);		
                        }
						catch {}
					}
				}

				LoadByPartner(_group.PartnerID);
			}
		}

		private void LoadByEvent(int eventID) {

			if (_event == null || _event.EventID != eventID)
			{
				// Get event
				_event = Event.GetEventByEventID(eventID);

				LoadByGroup(_event.GroupID, eventID);
			}
		}

		public void LoadByGroupRedirect(string redirect) 
		{
			if(_group == null || _event == null) {
				_event = ESubsGlobal.Event.GetEventByGroupRedirect(redirect);

				LoadByGroup(_event.GroupID);
			} else if(_event != null) {
				if(_event.EventID != Event.EventID) {
					LoadByGroup(_event.GroupID);
				}
			}
		}

		public void LoadByMemberHierarchyID(int memberHierarchyID) 
		{
			if (_eventParticipation == null || _eventParticipation.MemberHierarchyID != memberHierarchyID) 
			{
				_eventParticipation = ESubsGlobal.EventParticipation.GetEventParticipationByMemberHierarchyID(memberHierarchyID);
				LoadByEvent(_eventParticipation.EventID);
			}
		}

		private void LoadByEventAndMember(int eventID, int memberHierarchyID)
		{
			if (_event == null || _event.EventID != eventID)
			{
				bool hasChanged = false;
				if(_event != null)
					hasChanged = (_event.EventID != eventID);

				// Get event
				_event = Event.GetEventByEventID(eventID);

				// Get culture
				eSubsGlobalUser user = eSubsGlobalUser.LoadByHierarchyID(memberHierarchyID);
				_culture = user.Culture;

				// Get Personalization
				if(!user.IsSponsor || hasChanged) {
					if((_personalization == null && _eventParticipation != null) || hasChanged) {
						try {
                            _personalization = Personalization.CreateByParentEventParticipation(_eventParticipation);	
						} catch {}
						
						if(_personalization == null) {
							_personalization = Personalization.CreateByEventParticipation(EventParticipation);
						}
					}
				}
			
				LoadByGroup(_event.GroupID, _event.EventID);
			}
		}

		private void LoadByEventParticipation(int eventParticipationID) {

			// Get EventParticipation
			if (_eventParticipation == null || _eventParticipation.EventParticipationID != eventParticipationID)
			{
				_eventParticipation = EventParticipation.GetEventParticipationByEventParticipationID(eventParticipationID);	

				LoadByEventAndMember(_eventParticipation.EventID, _eventParticipation.MemberHierarchyID);
			}
		}

		private void LoadByInvitation(int touchID) {

			// Get EventParticipation
			if (_eventParticipation == null)
			{
				_eventParticipation = EventParticipation.GetEventParticipationByTouchID(touchID);

				LoadByEventAndMember(_eventParticipation.EventID, _eventParticipation.MemberHierarchyID);
			}
		}        
		

		public void Load(eSubsGlobalEnvironmentParameters param) {
			Load(param, true);
		}

		public void Load(eSubsGlobalEnvironmentParameters param, bool processAllParam) {

			// Load important defaults that need to be there.
			LoadDefaults();

			// Always load culture first because it is needed by LoadPartner
            if (param.Culture != null)
                LoadCulture(param.Culture);

			// NOTE: Always load from most specific to least detail
			// so we get the most accurate info possible.
            if (param.TouchID != int.MinValue && processAllParam)
            {
                LoadByInvitation(param.TouchID);
                TouchID = param.TouchID;
            }
            else if (param.EventParticipationID != int.MinValue && processAllParam)
                LoadByEventParticipation(param.EventParticipationID);
            else if (param.EventID != int.MinValue && param.MemberHierarchyID != int.MinValue && processAllParam)
                LoadByEventAndMember(param.EventID, param.MemberHierarchyID);
            else if (param.MemberHierarchyID != int.MinValue && processAllParam)
                LoadByMemberHierarchyID(param.MemberHierarchyID);
            else if (param.Redirect != null)
            {
                EventParticipation = null;
                //Users.eSubsGlobalUser.Create().Clear();
                LoadByGroupRedirect(param.Redirect);
            }
            else if (param.FacebookID != int.MinValue && param.EventID != int.MinValue)
            {
                eSubsGlobalUser[] users = eSubsGlobalUser.LoadByFacebookID(param.FacebookID, param.EventID);
                eSubsGlobalUser user = users[0];
                foreach (eSubsGlobalUser u in users)
                {
                    if (u.IsSponsor)
                    {
                        user = u;
                        break;
                    }
                    else if (u.IsParticipant)
                    {
                        user = u;
                    }
                }
                LoadByEventAndMember(param.EventID, user.HierarchyID);
                user.Save();
            }
            else if (param.EventID != int.MinValue)
            {
                //EventParticipation = null;
                //Personalization = null;
                //Users.eSubsGlobalUser.Create().Clear();
                LoadByEvent(param.EventID);
            }
            else if (param.GroupID != int.MinValue)
            {
                //Users.eSubsGlobalUser.Create().Clear();
                EventParticipation = null;
                Event = null;
                LoadByGroup(param.GroupID);
            }
            else if (param.PartnerID != int.MinValue && param.ExternalGroupID != null && param.ExternalGroupID.Trim() != string.Empty)
            {
                //EventParticipation = null;
                //Event = null;
                LoadEventByExternalGroupID(param.PartnerID, param.ExternalGroupID);
            }
            else if (param.PartnerID != int.MinValue)
            {
                //Users.eSubsGlobalUser.Create().Clear();
                EventParticipation = null;
                Event = null;
                Group = null;
                Personalization = null;
                LoadByPartner(param.PartnerID);
            }
            else if (param.PartnerGUID != null && param.PartnerGUID != "" && param.ExternalGroupID != null && param.ExternalGroupID.Trim() != string.Empty)
            {
                //Users.eSubsGlobalUser.Create().Clear();
                EventParticipation = null;
                Event = null;
                Group = null;
                Personalization = null;
                LoadByPartnerGUID(param.PartnerGUID);
                if (this.Partner != null && this.Partner.PartnerID != int.MinValue)
                    LoadEventByExternalGroupID(this.Partner.PartnerID, param.ExternalGroupID);
            }
            else if (param.PartnerGUID != null && param.PartnerGUID != "")
            {
                //Users.eSubsGlobalUser.Create().Clear();
                EventParticipation = null;
                Event = null;
                Group = null;
                Personalization = null;
                LoadByPartnerGUID(param.PartnerGUID);
            }
            else if (param.PartnerHost != null && param.PartnerHost != "")
            {
                //Users.eSubsGlobalUser.Create().Clear();
                EventParticipation = null;
                Event = null;
                Group = null;
                Personalization = null;
                LoadByPartnerHost(param.PartnerHost);
            }
            else
            {
                LoadPartnerPrizeCollection();
            }            
            
            if (Event != null && _culture != null && Event.CultureCode != _culture.CultureCode)
                _culture = Culture.Create(Event.CultureCode);

            // Added by Jiro Hidaka (November 12, 2008)
            if (_partner.PartnerLinkCollection != null && _culture != null )
            {
                PartnerLink link = _partner.PartnerLinkCollection.GetPartnerLinkByCountryCode(_culture.CountryCode);
                if (link != null)
                {
                    if (_partner.PartnerID != link.LinkedPartnerID)
                        LoadByPartner(link.LinkedPartnerID); // The culturecode did not match the culture of the partner so reload
                                                             // correct partner id matching the culturecode.
                }
                else
                {
                    /* It should not reach this section unless on exceptional cases, so if it does, notify the programmer */
                    //efundraising.Diagnostics.Logger.LogError("LOADING THE DEFAULT PARTNER DUE TO PARTNER-CULTURE MISMATCH: Culture="+_culture.CultureCode+", PartnerID="+_partner.PartnerID.ToString());
                    LoadByPartner(ESubsGlobal.Culture.DEFAULT.DefaultPartner.PartnerID);
                }
            }

            //March 30, 2010: When there is an event mismatch between EventParticipation object & Event object, 
            //                resove using the EventParticipation object by reloading the Event and Group
            if (_eventParticipation != null && _event != null & _eventParticipation.EventID != _event.EventID)
            {
                LoadByEvent(_eventParticipation.EventID);
            }

            // Load the Partner Profit information from EFRCommon
            if (_partnerProfit == null || _partnerProfit.PartnerId != _partner.PartnerID)
            {
                _partnerProfit = eFundraisingCommon.PartnerProfit.GetCurrentPartnerProfitByID(_partner.PartnerID);
                if (_partnerProfit == null)
                    throw new ESubsGlobalException (string.Format ("Can not load current partner profit for partner ID: {0}", _partner.PartnerID.ToString ()), null, this);
                _profitGroup = eFundraisingCommon.ProfitGroup.GetProfitGroupByID(_partnerProfit.ProfitGroupID);
                if (_profitGroup == null)
                    throw new ESubsGlobalException(string.Format("Can not load profit group for profit group ID: {0}", _partnerProfit.ProfitGroupID.ToString()), null, this);
                _profitCatalogs = eFundraisingCommon.Profit.GetProfitByProfitGroupID(_partnerProfit.ProfitGroupID);
                if (_profitCatalogs == null)
                    throw new ESubsGlobalException(string.Format("Can not load profit list for profit group ID: {0}", _partnerProfit.ProfitGroupID.ToString()), null, this);
            }
        }
		#endregion

		#region Create Methods
		public static eSubsGlobalEnvironment Create()
		{
			// Web based
			if(System.Web.HttpContext.Current != null)
			{
				if (System.Web.HttpContext.Current.Session == null ||
					System.Web.HttpContext.Current.Session[SESSION_KEY] == null) 
				{
					return new eSubsGlobalEnvironment();
				} 
				else 
				{
					return (eSubsGlobalEnvironment)System.Web.HttpContext.Current.Session[SESSION_KEY];
				}
			}
			return null;
		}

		public static eSubsGlobalEnvironment Create(eSubsGlobalEnvironmentParameters param, bool readAllParam) 
		{
			eSubsGlobalEnvironment env = eSubsGlobalEnvironment.Create();
			if (param != null) 
			{
				// Web based
				if(System.Web.HttpContext.Current != null)
				{
					env.Load(param, readAllParam);
				}

				return env;
			}

			return null;
		}

		/*
		public void InsertMember(Users.eSubsGlobalUser parent, Users.eSubsGlobalUser child, 
									Invitation[] invitation, ParticipationChannel participationChannel) {
			// event object is mandatory to insert members
			if(_event.EventID < 1) {
				throw new ESubsGlobalException("Unable to create member, missing Event", null, this, parent);
			}

			// the parent has to exists in order to create a member
			if(parent.ID < 0) {
				throw new ESubsGlobalException("Unable to create member, parent has not been identified correctly", null, this, parent);
			}

			// set the child parent hierarchy id
			child.HierarchyParentID = parent.HierarchyID;
			child.InsertIntoDatabase();

			EventParticipation ep = new EventParticipation();
			ep.EventID = _event.EventID;
			ep.MemberHierarchyID = parent.HierarchyID;
			ep.ParticipationChannel = participationChannel;
			ep.InsertIntoDatabase();

//			foreach(Invitation inv in invitation) {
//				inv.InsertIntoDatabase(ep.EventParticipationID);
//			}
		}*/

		#endregion

		#region Save Methods

		public void Save() 
		{
			// Web based
			if(System.Web.HttpContext.Current != null) 
			{
				System.Web.HttpContext.Current.Session[SESSION_KEY] = this;
			}
		}
		#endregion

		#region Clear session value
	
		public void Reset() {
			int partnerID = Partner.PartnerID;

			eSubsGlobalEnvironmentParameters param = new eSubsGlobalEnvironmentParameters();
			param.PartnerID = partnerID;
			param.Culture = Culture.DEFAULT;
			Clear();
			Load(param);
			StrongAuthentication = null;
			Save();
		}

		public void Clear() {
			if(System.Web.HttpContext.Current != null) {	// Web based
				System.Web.HttpContext.Current.Session.Remove(SESSION_KEY);
			}

            //UPDATE March 29, 2010: I added touch id in the environment object so when clearing it, I also need to remove touch id
            if (TouchID != int.MinValue)
                TouchID = int.MinValue;
		}

		#endregion

		#endregion

		#region Properties

		public Partner Partner 
		{
			set { _partner = value; }
			get { return _partner; }
		}

		public Group Group {
			set { _group = value; }
			get { return _group; }
		}

		public Event Event {
			set { _event = value; }
			get { return _event; }
		}

		public EventParticipation EventParticipation  {
			set { _eventParticipation = value; }
			get { return _eventParticipation; }
		}

		public Culture CurrentCulture {
			set { _culture = value; }
			get { return _culture; }
		}

		public PrizeCollection PrizeCollection {
			set { _prizeCollection = value; }
			get { return _prizeCollection; }
		}


		public Personalization Personalization
		{
			set { _personalization = value; }
			get { return _personalization; }
		}

		public StrongAuthentication StrongAuthentication {
			get { return strongAuthentication; }
			set { strongAuthentication = value; }
		}

        public int TouchID
        {
            get { return _touch_id; }
            set { _touch_id = value; }
        }

        public Profit Profit
        {
            get {
                foreach (Profit p in _profitCatalogs)
                {
                    if (p.QspCatalogTypeID == int.MinValue)
                        return p;
                }
                return null; 
            }
        }

        public List<Profit> ProfitCatalogs
        {
            get { return _profitCatalogs; }
            set { _profitCatalogs = value; }
        }

        public ProfitGroup ProfitGroup
        {
            get { return _profitGroup; }
            set { _profitGroup = value; }
        }

		#endregion
	}
}
