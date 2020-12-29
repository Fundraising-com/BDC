using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.CustomPartner
{
	/// <summary>
	/// Summary description for Partner.
	/// </summary>
	public class Partner
	{

		//private ArrayList emailtemplateslist = new ArrayList();

		private string id;
		private string name;
		private string templatename;
		private string prize;
		private string showparentname;
		private string showperiodicemail;
		private string showpaymentinformation;
		private string insertaslead;
		private string showDemo;
		private string showListOfSupporters;
		private string showAmountRaised;
		private string showStatesInFindGroup;
		private string showFundraisingGoal;
		private string showShopMagazineBanner;
		private string showAddressInFindGroup;
		private string showFeaturedGroup;
		private string homeIsClickable;
        private string logoIsClickable;
		private string showCheckReport;
		private string showSalesRepQuestion;
        private string showGroupPageLanguage;
        private string redirectToLandingPage;
        private string showChangePassword;
        private string chargeProcessingFee;
        private string showImageMotivation;
        private string showPopularItems;
        private string showDirectMail;
        private string showPartnerOnlyLogo;
        private string showDonation;
        private string showGroupTitleHeader;
        private string css;
        private string showCanadienLink;
        private string showUSALink;
        private string showTopMenuLinks;
        private string showDefaultGroup;
        private string showReceiptLink;
        private string showCheckoutRecurringBloc;
        private string showCreateFundraisingStore;
        private string showPowerByLogo;
        private string showFilterByInFindGroup;
        private string languageLinkSuppressionList;
        private string showSchoolDisclaimer;
        private string showFindMyGroup;
        private string storeType;
        private string eventType;

		public Partner() 
		{

		}

		/*
		#region Sponsor Wizard
		public int SponsorWizardNewParticipant1 {
			get {
				return GetEmailTemplateByID("Sponsor-Wizard-NewParticipant1");
			}
		}

		public int SponsorWizardNewParticipant2 {
			get {
				return GetEmailTemplateByID("Sponsor-Wizard-NewParticipant2");
			}
		}

		public int SponsorWizardNewParticipant3 {
			get {
				return GetEmailTemplateByID("Sponsor-Wizard-NewParticipant3");
			}
		}

		public int BusinessRuleSponsorWizardNewParticipant1 {
			get {
				return GetBusinessRuleByID("Sponsor-Wizard-NewParticipant1");
			}
		}

		public int BusinessRuleSponsorWizardNewParticipant2 {
			get {
				return GetBusinessRuleByID("Sponsor-Wizard-NewParticipant2");
			}
		}

		public int BusinessRuleSponsorWizardNewParticipant3 {
			get {
				return GetBusinessRuleByID("Sponsor-Wizard-NewParticipant3");
			}
		}

		#endregion

		#region Sponsor Zone
		public int SponsorZoneNewParticipant1 {
			get {
				return GetEmailTemplateByID("Sponsor-Zone-NewParticipant1");
			}
		}

		public int SponsorZoneNewParticipant2 {
			get {
				return GetEmailTemplateByID("Sponsor-Zone-NewParticipant2");
			}
		}

		public int SponsorZoneNewParticipant3 {
			get {
				return GetEmailTemplateByID("Sponsor-Zone-NewParticipant3");
			}
		}

		public int BusinessRuleSponsorZoneNewParticipant1 {
			get {
				return GetBusinessRuleByID("Sponsor-Zone-NewParticipant1");
			}
		}

		public int BusinessRuleSponsorZoneNewParticipant2 {
			get {
				return GetBusinessRuleByID("Sponsor-Zone-NewParticipant2");
			}
		}

		public int BusinessRuleSponsorZoneNewParticipant3 {
			get {
				return GetBusinessRuleByID("Sponsor-Zone-NewParticipant3");
			}
		}

		public int SponsorZoneReminderEmail {
			get {
				return GetEmailTemplateByID("Sponsor-Zone-ReminderEmail");
			}
		}

		public int BusinessRuleSponsorZoneReminderEmail {
			get {
				return GetBusinessRuleByID("Sponsor-Zone-ReminderEmail");
			}
		}

		public int SponsorZonePersonalNote {
			get {
				return GetEmailTemplateByID("Sponsor-Zone-PersonalNote");
			}
		}

		public int BusinessRuleSponsorZonePersonalNote {
			get {
				return GetBusinessRuleByID("Sponsor-Zone-PersonalNote");
			}
		}

		public int SponsorZoneInviteSupporter1 {
			get {
				return GetEmailTemplateByID("Sponsor-Zone-InviteSupporter1");
			}
		}

		public int BusinessRuleSponsorZoneInviteSupporter1 {
			get {
				return GetBusinessRuleByID("Sponsor-Zone-InviteSupporter1");
			}
		}

		public int SponsorZoneInviteSupporter2 {
			get {
				return GetEmailTemplateByID("Sponsor-Zone-InviteSupporter2");
			}
		}

		public int BusinessRuleSponsorZoneInviteSupporter2 {
			get {
				return GetBusinessRuleByID("Sponsor-Zone-InviteSupporter2");
			}
		}

		public int SponsorZoneInviteSupporter3 {
			get {
				return GetEmailTemplateByID("Sponsor-Zone-InviteSupporter3");
			}
		}

		public int BusinessRuleSponsorZoneInviteSupporter3 {
			get {
				return GetBusinessRuleByID("Sponsor-Zone-InviteSupporter3");
			}
		}

		#endregion

		#region Participant Wizard
		public int ParticipantWizardNewSupporter1 {
			get {
				return GetEmailTemplateByID("Participant-Wizard-NewSupporter1");
			}
		}

		public int BusinessRuleParticipantWizardNewSupporter1 {
			get {
				return GetBusinessRuleByID("Participant-Wizard-NewSupporter1");
			}
		}

		public int ParticipantWizardNewSupporter2 {
			get {
				return GetEmailTemplateByID("Participant-Wizard-NewSupporter2");
			}
		}

		public int BusinessRuleParticipantWizardNewSupporter2 {
			get {
				return GetBusinessRuleByID("Participant-Wizard-NewSupporter2");
			}
		}

		public int ParticipantWizardNewSupporter3 {
			get {
				return GetEmailTemplateByID("Participant-Wizard-NewSupporter3");
			}
		}

		public int BusinessRuleParticipantWizardNewSupporter3 {
			get {
				return GetBusinessRuleByID("Participant-Wizard-NewSupporter3");
			}
		}

		#endregion

		#region Participant Zone

		public int ParticipantZoneNewSupporter1 {
			get {
				return GetEmailTemplateByID("Participant-Zone-NewSupporter1");
			}
		}

		public int BusinessRuleParticipantZoneNewSupporter1 {
			get {
				return GetBusinessRuleByID("Participant-Zone-NewSupporter1");
			}
		}

		public int ParticipantZoneNewSupporter2 {
			get {
				return GetEmailTemplateByID("Participant-Zone-NewSupporter2");
			}
		}

		public int BusinessRuleParticipantZoneNewSupporter2 {
			get {
				return GetBusinessRuleByID("Participant-Zone-NewSupporter2");
			}
		}

		public int ParticipantZoneNewSupporter3 {
			get {
				return GetEmailTemplateByID("Participant-Zone-NewSupporter3");
			}
		}

		public int BusinessRuleParticipantZoneNewSupporter3 {
			get {
				return GetBusinessRuleByID("Participant-Zone-NewSupporter3");
			}
		}

		public int ParticipantZoneReminderEmail {
			get {
				return GetEmailTemplateByID("Participant-Zone-ReminderEmail");
			}
		}

		public int BusinessRuleParticipantZoneReminderEmail {
			get {
				return GetBusinessRuleByID("Participant-Zone-ReminderEmail");
			}
		}

		public int ParticipantZonePersonalNote {
			get {
				return GetEmailTemplateByID("Participant-Zone-PersonalNote");
			}
		}

		public int BusinessRuleParticipantZonePersonalNote {
			get {
				return GetBusinessRuleByID("Participant-Zone-PersonalNote");
			}
		}

		#endregion

		private int GetEmailTemplateByID(string id) {
			EmailTemplates emailTemplates = GetEmailTemplatesByID();
			foreach(EmailTemplate t in emailTemplates.EmailTemplateList) {
				if(t.ID == id) {
					return int.Parse(t.EmailTemplateID);
				}
			}
			return -1;
		}

		private int GetBusinessRuleByID(string id) {
			EmailTemplates emailTemplates = GetEmailTemplatesByID();
			foreach(EmailTemplate t in emailTemplates.EmailTemplateList) {
				if(t.ID == id) {
					return int.Parse(t.BusinessRule);
				}
			}
			return -1;
		}

		private EmailTemplates GetEmailTemplatesByID() {
			foreach(EmailTemplates emailtemplates in emailtemplateslist) {
				return emailtemplates;
			}
			return null;
		}


		private EmailTemplates GetEmailTemplatesByName() {
			foreach(EmailTemplates emailtemplates in emailtemplateslist) {
				return emailtemplates;
			}
			return null;
		}
		*/

		public void LoadPartner(XmlNode node) 
		{			
			foreach(XmlNode child in node) 
			{
				if(child.Name.ToLower() == "ID".ToLower()) 
				{
					ID = child.InnerText;
				}
				else if(child.Name.ToLower() == "Name".ToLower()) 
				{
					Name = child.InnerText;
				}
				else if(child.Name.ToLower() == "TemplateName".ToLower()) 
				{
					TemplateName = child.InnerText;
				}
				else if(child.Name.ToLower() == "Prize".ToLower()) 
				{
					Prize = child.InnerText;
				}
				else if(child.Name.ToLower() == "ShowParentName".ToLower()) 
				{
					ShowParentName = child.InnerText;
				}
				else if(child.Name.ToLower() == "ShowPeriodicEmail".ToLower()) 
				{
					ShowPeriodicEmail = child.InnerText;
				}
				else if(child.Name.ToLower() == "ShowPaymentInformation".ToLower()) 
				{
					ShowPaymentInformation = child.InnerText;
				}
				else if(child.Name.ToLower() == "InsertAsLead".ToLower()) 
				{
					InsertAsLead = child.InnerText;
				}
				else if(child.Name.ToLower() == "ShowDemo".ToLower()) 
				{
					ShowDemo = child.InnerText;
				}
				else if(child.Name.ToLower() == "ShowListOfSupporters".ToLower()) 
				{
					ShowListOfSupporters = child.InnerText;
				}
				else if(child.Name.ToLower() == "ShowAmountRaised".ToLower()) 
				{
					ShowAmountRaised = child.InnerText;
				}
				else if(child.Name.ToLower() == "ShowStatesInFindGroup".ToLower()) 
				{
					ShowStatesInFindGroup = child.InnerText;
				}
				else if(child.Name.ToLower() == "ShowFundraisingGoal".ToLower()) 
				{
					ShowFundraisingGoal = child.InnerText;
				}
				else if(child.Name.ToLower() == "ShowShopMagazineBanner".ToLower()) 
				{
					showShopMagazineBanner = child.InnerText;
				}
				else if(child.Name.ToLower() == "ShowAddressInFindGroup".ToLower()) 
				{
					showAddressInFindGroup = child.InnerText;
				} 
				else if(child.Name.ToLower() == "showFeaturedGroup".ToLower()) 
				{
					showFeaturedGroup = child.InnerText;
				} 
				else if(child.Name.ToLower() == "homeIsClickable".ToLower()) 
				{
					homeIsClickable = child.InnerText;
				}
                else if (child.Name.ToLower() == "LogoIsClickable".ToLower())
                {
                    logoIsClickable = child.InnerText;
                }
				else if(child.Name.ToLower() == "ShowCheckReport".ToLower()) 
				{
					showCheckReport = child.InnerText;
				}
				else if(child.Name.ToLower() == "ShowSalesRepQuestion".ToLower()) 
				{
					showSalesRepQuestion = child.InnerText;
				}
                else if (child.Name.ToLower() == "ShowGroupPageLanguage".ToLower())
                {
                    showGroupPageLanguage = child.InnerText;
                }
                else if (child.Name.ToLower() == "RedirectToLandingPage".ToLower())
                {
                    redirectToLandingPage = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowChangePassword".ToLower())
                {
                    showChangePassword = child.InnerText;
                }
                else if (child.Name.ToLower() == "ChargeProcessingFee".ToLower())
                {
                    chargeProcessingFee = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowImageMotivation".ToLower())
                {
                    showImageMotivation = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowPopularItems".ToLower())
                {
                    showPopularItems = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowDirectMail".ToLower())
                {
                    showDirectMail = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowPartnerOnlyLogo".ToLower())
                {
                    showPartnerOnlyLogo = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowDonation".ToLower())
                {
                    showDonation = child.InnerText;
                } 
                else if (child.Name.ToLower() == "ShowGroupTitleHeader".ToLower())
                {
                    showGroupTitleHeader = child.InnerText;
                }
                else if (child.Name.ToLower() == "css".ToLower())
                {
                    css= child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowCanadienLink".ToLower())
                {
                    showCanadienLink = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowUSALink".ToLower())
                {
                    showUSALink = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowTopMenuLinks".ToLower())
                {
                    showTopMenuLinks = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowDefaultGroup".ToLower())
                {
                    showDefaultGroup = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowReceiptLink".ToLower())
                {
                    showReceiptLink = child.InnerText;
                }
                else if (child.Name.ToLower() == "showCheckoutRecurringBloc".ToLower())
                {
                    showCheckoutRecurringBloc = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowCreateFundraisingStore".ToLower())
                {
                    showCreateFundraisingStore = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowPowerByLogo".ToLower())
                {
                    showPowerByLogo = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowFilterByInFindGroup".ToLower())
                {
                    showFilterByInFindGroup = child.InnerText;
                }
                else if (child.Name.ToLower() == "LanguageLinkSuppressionList".ToLower())
                {
                    languageLinkSuppressionList = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowSchoolDisclaimer".ToLower())
                {
                    showSchoolDisclaimer = child.InnerText;
                }
                else if (child.Name.ToLower() == "ShowFindMyGroup".ToLower())
                {
                    showFindMyGroup = child.InnerText;
                }
                else if (child.Name.ToLower() == "StoreType".ToLower())
                {
                    storeType = child.InnerText;
                }
				else
				{
					switch (child.Name.ToLower())
					{
						case "showcreatecampaign" :
							ShowCreateCampaign = child.InnerText;
							break;
						default:
							break;
					}
				}
				/*else if(child.Name.ToLower() == "EmailTemplates".ToLower()) {
					EmailTemplates emailtemplates = new EmailTemplates();
					emailtemplates.LoadEmailTemplates(child);
					AddEmailTemplates(emailtemplates);
				}*/
			}
		}

		/*
		public void AddEmailTemplates(EmailTemplates emailtemplates) {
			EmailTemplatesList.Add(emailtemplates);
		}*/

		private string _showCreateCampaign = bool.TrueString;

		#region Properties


		public string ShowCreateCampaign
		{
			get
			{
				return _showCreateCampaign;
			}
			set
			{
				_showCreateCampaign = value;
			}
		}

		public string ID 
		{
			set { id = value; }
			get { return id; }
		}

		public string Name 
		{
			set { name = value; }
			get { return name; }
		}

		public string TemplateName 
		{
			set { templatename = value; }
			get { return templatename; }
		}

		public string Prize 
		{
			set { prize = value; }
			get { return prize; }
		}

		public string ShowParentName 
		{
			set { showparentname = value; }
            get { if (string.IsNullOrEmpty(showparentname)) return "false"; else return showparentname; }
		}

		public string ShowPeriodicEmail 
		{
			set { showperiodicemail = value; }
            get { if (string.IsNullOrEmpty(showperiodicemail)) return "false"; else return showperiodicemail; }
		}

		public string ShowPaymentInformation 
		{
			set { showpaymentinformation = value; }
            get { if (string.IsNullOrEmpty(showpaymentinformation)) return "false"; else return showpaymentinformation; }
		}

		public string InsertAsLead 
		{
			set { insertaslead = value; }
            get { if (string.IsNullOrEmpty(insertaslead)) return "false"; else return insertaslead; }
		}

		public string ShowDemo 
		{
			set { showDemo = value; }
            get { if (string.IsNullOrEmpty(showDemo)) return "false"; else return showDemo; }
		}

		public string ShowListOfSupporters 
		{
			set { showListOfSupporters = value; }
            get { if (string.IsNullOrEmpty(showListOfSupporters)) return "false"; else return showListOfSupporters; }
		}

		public string ShowAmountRaised 
		{
			set { showAmountRaised = value; }
            get { if (string.IsNullOrEmpty(showAmountRaised)) return "false"; else return showAmountRaised; }
		}

		public string ShowStatesInFindGroup 
		{
			set { showStatesInFindGroup = value; }
            get { if (string.IsNullOrEmpty(showStatesInFindGroup)) return "false"; else return showStatesInFindGroup; }
		}

		public string ShowFundraisingGoal 
		{
			set { showFundraisingGoal = value; }
            get { if (string.IsNullOrEmpty(showFundraisingGoal)) return "false"; else return showFundraisingGoal; }
		}

		public string ShowShopMagazineBanner 
		{
			set { showShopMagazineBanner = value; }
            get { if (string.IsNullOrEmpty(showShopMagazineBanner)) return "false"; else return showShopMagazineBanner; }
		}

		public string ShowAddressInFindGroup 
		{
			set { showAddressInFindGroup = value; }
            get { if (string.IsNullOrEmpty(showAddressInFindGroup)) return "false"; else return showAddressInFindGroup; }
		}

		public string ShowFeaturedGroup 
		{
			set { showFeaturedGroup = value; }
            get { if (string.IsNullOrEmpty(showFeaturedGroup)) return "false"; else return showFeaturedGroup; }
		}

		public string HomeIsClickable 
		{
			set { homeIsClickable = value; }
            get { if (string.IsNullOrEmpty(homeIsClickable)) return "false"; else return homeIsClickable; }
		}

        public string LogoIsClickable
        {
            set { logoIsClickable = value; }
            get { if (string.IsNullOrEmpty(logoIsClickable)) return "true"; else return logoIsClickable; }
        }

		public string ShowCheckReport {
			set { showCheckReport = value; }
            get { if (string.IsNullOrEmpty(showCheckReport)) return "false"; else return showCheckReport; }
		}

		public string ShowSalesRepQuestion 
		{
			set { showSalesRepQuestion = value; }
            get { if (string.IsNullOrEmpty(showSalesRepQuestion)) return "false"; else return showSalesRepQuestion; }
		}

        public string ShowGroupPageLanguage
        {
            set { showGroupPageLanguage = value; }
            get { if (string.IsNullOrEmpty(showGroupPageLanguage)) return "false"; else return showGroupPageLanguage; }
        }

        public string RedirectToLandingPage
        {
            set { redirectToLandingPage = value; }
            get { if (string.IsNullOrEmpty(redirectToLandingPage)) return "false"; else return redirectToLandingPage; }
        }

        public string ShowChangePassword
        {
            set { showChangePassword = value; }
            get { if (string.IsNullOrEmpty(showChangePassword)) return "false"; else return showChangePassword; }
        }

        public string ChargeProcessingFee
        {
            set { chargeProcessingFee = value; }
            get { if (string.IsNullOrEmpty(chargeProcessingFee)) return "false"; else return chargeProcessingFee; }
        }

        public string ShowImageMotivation
        {
            set { showImageMotivation = value; }
            get { if (string.IsNullOrEmpty(showImageMotivation)) return "false"; else return showImageMotivation; }
        }

        public string ShowPopularItems
        {
            set { showPopularItems = value; }
            get { if (string.IsNullOrEmpty(showPopularItems)) return "true"; else return showPopularItems; }
        }


        // it return true ( if not there)
        public string ShowDirectMail
        {
            set { showDirectMail = value; }
            get { if (string.IsNullOrEmpty(showDirectMail)) return "true"; else return showDirectMail; }
        }

        public string ShowPartnerOnlyLogo
        {
            set { showPartnerOnlyLogo = value; }
            get { if (string.IsNullOrEmpty(showPartnerOnlyLogo)) return "false"; else return showPartnerOnlyLogo; }
        }

        public string ShowDonation
        {
            set { showDonation = value; }
            get { if (string.IsNullOrEmpty(showDonation)) return "true"; else return showDonation; }
        }

        public string ShowGroupTitleHeader
        {
            set { showGroupTitleHeader = value; }
            get { if (string.IsNullOrEmpty(showGroupTitleHeader)) return "true"; else return showGroupTitleHeader; }
        }

        public string Css
        {
            set { css= value; }
            get { if (string.IsNullOrEmpty(css)) return ""; else return css; }
        }

        public string ShowCanadienLink
        {
            set { showCanadienLink = value; }
            get { if (string.IsNullOrEmpty(showCanadienLink)) return "true"; else return showCanadienLink; }
        }

        public string ShowUSALink
        {
            set { showUSALink = value; }
            get { if (string.IsNullOrEmpty(showUSALink)) return "true"; else return showUSALink; }
        }

        public string ShowTopMenuLinks
        {
            set { showTopMenuLinks = value; }
            get { if (string.IsNullOrEmpty(showTopMenuLinks)) return "true"; else return showTopMenuLinks; }
        }

        public string ShowDefaultGroup
        {
            set { showDefaultGroup = value; }
            get { if (string.IsNullOrEmpty(showDefaultGroup)) return "true"; else return showDefaultGroup; }
        }

        public string ShowReceiptLink
        {
            set { showReceiptLink = value; }
            get { if (string.IsNullOrEmpty(showReceiptLink)) return "true"; else return showReceiptLink; }
        }

        public string ShowCheckoutRecurringBloc
        {
            set { showCheckoutRecurringBloc = value; }
            get { if (string.IsNullOrEmpty(showCheckoutRecurringBloc)) return "true"; else return showCheckoutRecurringBloc; }
        }

        public string ShowCreateFundraisingStore
        {
            set { showCreateFundraisingStore = value; }
            get { if (string.IsNullOrEmpty(showCreateFundraisingStore)) return "true"; else return showCreateFundraisingStore; }
        }

        public string ShowPowerByLogo
        {
            set { showPowerByLogo = value; }
            get { if (string.IsNullOrEmpty(showPowerByLogo)) return "true"; else return showPowerByLogo; }
        }

        public string ShowFilterByInFindGroup
        {
            set { showFilterByInFindGroup = value; }
            get { if (string.IsNullOrEmpty(showFilterByInFindGroup)) return "true"; else return showFilterByInFindGroup; }
        }

        public string LanguageLinkSuppressionList
        {
            set { languageLinkSuppressionList = value; }
            get { if (string.IsNullOrEmpty(languageLinkSuppressionList)) return string.Empty; else return languageLinkSuppressionList; }
        }

        public string ShowSchoolDisclaimer
        {
            set { showSchoolDisclaimer = value; }
            get { if (string.IsNullOrEmpty(showSchoolDisclaimer)) return "false"; else return showSchoolDisclaimer; }
        }

        public string ShowFindMyGroup
        {
            set { showFindMyGroup = value; }
            get { if (string.IsNullOrEmpty(showFindMyGroup)) return "false"; else return showFindMyGroup; }
        }

        public string StoreType
        {
            set { storeType = value; }
            get { if (string.IsNullOrEmpty(storeType)) return string.Empty; else return storeType; }
        }

        public string EventType
        {
            set { eventType = value; }
            get { if (string.IsNullOrEmpty(eventType)) return string.Empty; else return eventType; }
        }
		#endregion

	}
}
