using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Lead: EFundraisingCRMDataObject {

		private int leadId;
		private int leadStatusId;
		private int leadQualificationTypeId;
		private int leadPriorityId;
		private int tempLeadId;
		private short divisionId;
		private int promotionId;
		private string channelCode;
		private int consultantId;
		private short groupTypeId;
		private short organizationTypeId;
		private short hearId;
		private int fkKitTypeId;
		private int oldLeadId;
		private int assignerId;
		private int refereeId;
		private short titleId;
		private short campaignReasonId;
		private int webSiteId;
		private int promotionCodeId;
		private short activityClosingReasonId;
		private int extConsultantId;
		private string salutation;
		private string firstName;
		private string lastName;
		private string organization;
		private string streetAddress;
		private string city;
		private string stateCode;
		private string countryCode;
		private string zipCode;
		private string dayPhone;
		private string dayTimeCall;
		private string eveningPhone;
		private string eveningTimeCall;
		private string fax;
		private string email;
		private DateTime leadEntryDate;
		private int memberCount;
		private int participantCount;
		private int fundRaisingGoal;
		private DateTime decisionDate;
		private bool decisionMaker;
		private bool committeeMeetingRequired;
		private DateTime committeeMeetingDate;
		private DateTime fundRaiserStartDate;
		private bool onemaillist;
		private bool faxkit;
		private bool emailkit;
		private string comments;
		private bool kitToSend;
		private bool kitSent;
		private DateTime kitSentDate;
		private DateTime leadAssignmentDate;
		private string interests;
		private bool hasBeenContacted;
		private string dayPhoneExt;
		private string eveningPhoneExt;
		private string otherPhone;
		private string groupWebSite;
		private int nbQueries;
		private DateTime submitDate;
		private string cookieContent;
		private DateTime firstContactDate;
		private bool dayPhoneIsGood;
		private bool eveningPhoneIsGood;
		private int accountNumber;
		private bool validEmail;
		private string otherClosingActivityReason;
		private DateTime transferedDate;
		private string matchingCode;
		private string vif;
		private int phoneNumberTrackingId;
		private int customerStatusId;
		private int clientStatusId;
		private int addressZoneId;
		private byte fundraisersPerYear = byte.MinValue;
        //By PT varibales below not part of the efudraisingCRM.Lead table
        private int partnerId;
         private bool isConsultantActive;


		public Lead() : this(int.MinValue) { }
		public Lead(int leadId) : this(leadId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId) : this(leadId, leadStatusId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId) : this(leadId, leadStatusId, leadQualificationTypeId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, short.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, short.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, short.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, short.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, short.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, short.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, short.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, DateTime.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, DateTime.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, false) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, false) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, DateTime.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, DateTime.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, false) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, false) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, false) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, false) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, false) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, DateTime.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, DateTime.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, false) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, otherPhone, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, otherPhone, groupWebSite, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, otherPhone, groupWebSite, 
			nbQueries, DateTime.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries, DateTime submitDate) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, 
			otherPhone, groupWebSite, nbQueries, submitDate, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries, DateTime submitDate, string cookieContent) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, 
			eveningPhoneExt, otherPhone, groupWebSite, nbQueries, submitDate, cookieContent, DateTime.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries, DateTime submitDate, string cookieContent, DateTime firstContactDate) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, 
			hasBeenContacted, dayPhoneExt, eveningPhoneExt, otherPhone, groupWebSite, nbQueries, submitDate, cookieContent, firstContactDate, false) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries, DateTime submitDate, string cookieContent, DateTime firstContactDate, bool dayPhoneIsGood) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, 
			interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, otherPhone, groupWebSite, nbQueries, submitDate, cookieContent, firstContactDate, dayPhoneIsGood, false) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries, DateTime submitDate, string cookieContent, DateTime firstContactDate, bool dayPhoneIsGood, bool eveningPhoneIsGood) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, 
			kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, otherPhone, groupWebSite, nbQueries, submitDate, cookieContent, firstContactDate, dayPhoneIsGood, eveningPhoneIsGood, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries, DateTime submitDate, string cookieContent, DateTime firstContactDate, bool dayPhoneIsGood, bool eveningPhoneIsGood, int accountNumber) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, 
			kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, otherPhone, groupWebSite, nbQueries, submitDate, cookieContent, firstContactDate, dayPhoneIsGood, eveningPhoneIsGood, accountNumber, false) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries, DateTime submitDate, string cookieContent, DateTime firstContactDate, bool dayPhoneIsGood, bool eveningPhoneIsGood, int accountNumber, bool validEmail) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, 
			comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, otherPhone, groupWebSite, nbQueries, submitDate, cookieContent, firstContactDate, dayPhoneIsGood, eveningPhoneIsGood, accountNumber, validEmail, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries, DateTime submitDate, string cookieContent, DateTime firstContactDate, bool dayPhoneIsGood, bool eveningPhoneIsGood, int accountNumber, bool validEmail, string otherClosingActivityReason) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, 
			onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, otherPhone, groupWebSite, nbQueries, submitDate, cookieContent, firstContactDate, dayPhoneIsGood, eveningPhoneIsGood, accountNumber, validEmail, otherClosingActivityReason, DateTime.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries, DateTime submitDate, string cookieContent, DateTime firstContactDate, bool dayPhoneIsGood, bool eveningPhoneIsGood, int accountNumber, bool validEmail, string otherClosingActivityReason, DateTime transferedDate) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, 
			committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, otherPhone, groupWebSite, nbQueries, submitDate, cookieContent, firstContactDate, dayPhoneIsGood, eveningPhoneIsGood, accountNumber, validEmail, otherClosingActivityReason, transferedDate, null) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries, DateTime submitDate, string cookieContent, DateTime firstContactDate, bool dayPhoneIsGood, bool eveningPhoneIsGood, int accountNumber, bool validEmail, string otherClosingActivityReason, DateTime transferedDate, string matchingCode) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, 
			committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, otherPhone, groupWebSite, nbQueries, submitDate, cookieContent, firstContactDate, dayPhoneIsGood, eveningPhoneIsGood, accountNumber, validEmail, otherClosingActivityReason, transferedDate, matchingCode, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries, DateTime submitDate, string cookieContent, DateTime firstContactDate, bool dayPhoneIsGood, bool eveningPhoneIsGood, int accountNumber, bool validEmail, string otherClosingActivityReason, DateTime transferedDate, string matchingCode, int phoneNumberTrackingId) : this(leadId, leadStatusId, leadQualificationTypeId, leadPriorityId, tempLeadId, divisionId, promotionId, channelCode, consultantId, groupTypeId, organizationTypeId, hearId, fkKitTypeId, oldLeadId, assignerId, refereeId, titleId, campaignReasonId, webSiteId, promotionCodeId, activityClosingReasonId, extConsultantId, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, leadEntryDate, memberCount, participantCount, fundRaisingGoal, 
			decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onemaillist, faxkit, emailkit, comments, kitToSend, kitSent, kitSentDate, leadAssignmentDate, interests, hasBeenContacted, dayPhoneExt, eveningPhoneExt, otherPhone, groupWebSite, nbQueries, submitDate, cookieContent, firstContactDate, dayPhoneIsGood, eveningPhoneIsGood, accountNumber, validEmail, otherClosingActivityReason, transferedDate, matchingCode, phoneNumberTrackingId, int.MinValue) { }
		public Lead(int leadId, int leadStatusId, int leadQualificationTypeId, int leadPriorityId, int tempLeadId, short divisionId, int promotionId, string channelCode, int consultantId, short groupTypeId, short organizationTypeId, short hearId, int fkKitTypeId, int oldLeadId, int assignerId, int refereeId, short titleId, short campaignReasonId, int webSiteId, int promotionCodeId, short activityClosingReasonId, int extConsultantId, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, DateTime leadEntryDate, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, bool decisionMaker, bool committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, bool onemaillist, bool faxkit, bool emailkit, string comments, bool kitToSend, bool kitSent, DateTime kitSentDate, DateTime leadAssignmentDate, string interests, bool hasBeenContacted, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string groupWebSite, int nbQueries, DateTime submitDate, string cookieContent, DateTime firstContactDate, bool dayPhoneIsGood, bool eveningPhoneIsGood, int accountNumber, bool validEmail, string otherClosingActivityReason, DateTime transferedDate, string matchingCode, int phoneNumberTrackingId, int customerStatusId) {
			this.leadId = leadId;
			this.leadStatusId = leadStatusId;
			this.leadQualificationTypeId = leadQualificationTypeId;
			this.leadPriorityId = leadPriorityId;
			this.tempLeadId = tempLeadId;
			this.divisionId = divisionId;
			this.promotionId = promotionId;
			this.channelCode = channelCode;
			this.consultantId = consultantId;
			this.groupTypeId = groupTypeId;
			this.organizationTypeId = organizationTypeId;
			this.hearId = hearId;
			this.fkKitTypeId = fkKitTypeId;
			this.oldLeadId = oldLeadId;
			this.assignerId = assignerId;
			this.refereeId = refereeId;
			this.titleId = titleId;
			this.campaignReasonId = campaignReasonId;
			this.webSiteId = webSiteId;
			this.promotionCodeId = promotionCodeId;
			this.activityClosingReasonId = activityClosingReasonId;
			this.extConsultantId = extConsultantId;
			this.salutation = salutation;
			this.firstName = firstName;
			this.lastName = lastName;
			this.organization = organization;
			this.streetAddress = streetAddress;
			this.city = city;
			this.stateCode = stateCode;
			this.countryCode = countryCode;
			this.zipCode = zipCode;
			this.dayPhone = dayPhone;
			this.dayTimeCall = dayTimeCall;
			this.eveningPhone = eveningPhone;
			this.eveningTimeCall = eveningTimeCall;
			this.fax = fax;
			this.email = email;
			this.leadEntryDate = leadEntryDate;
			this.memberCount = memberCount;
			this.participantCount = participantCount;
			this.fundRaisingGoal = fundRaisingGoal;
			this.decisionDate = decisionDate;
			this.decisionMaker = decisionMaker;
			this.committeeMeetingRequired = committeeMeetingRequired;
			this.committeeMeetingDate = committeeMeetingDate;
			this.fundRaiserStartDate = fundRaiserStartDate;
			this.onemaillist = onemaillist;
			this.faxkit = faxkit;
			this.emailkit = emailkit;
			this.comments = comments;
			this.kitToSend = kitToSend;
			this.kitSent = kitSent;
			this.kitSentDate = kitSentDate;
			this.leadAssignmentDate = leadAssignmentDate;
			this.interests = interests;
			this.hasBeenContacted = hasBeenContacted;
			this.dayPhoneExt = dayPhoneExt;
			this.eveningPhoneExt = eveningPhoneExt;
			this.otherPhone = otherPhone;
			this.groupWebSite = groupWebSite;
			this.nbQueries = nbQueries;
			this.submitDate = submitDate;
			this.cookieContent = cookieContent;
			this.firstContactDate = firstContactDate;
			this.dayPhoneIsGood = dayPhoneIsGood;
			this.eveningPhoneIsGood = eveningPhoneIsGood;
			this.accountNumber = accountNumber;
			this.validEmail = validEmail;
			this.otherClosingActivityReason = otherClosingActivityReason;
			this.transferedDate = transferedDate;
			this.matchingCode = matchingCode;
			this.phoneNumberTrackingId = phoneNumberTrackingId;
			this.customerStatusId = customerStatusId;
			this.addressZoneId = int.MinValue;
		}

		#region Method

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Lead>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"	<LeadStatusId>" + leadStatusId + "</LeadStatusId>\r\n" +
			"	<LeadQualificationTypeId>" + leadQualificationTypeId + "</LeadQualificationTypeId>\r\n" +
			"	<LeadPriorityId>" + leadPriorityId + "</LeadPriorityId>\r\n" +
			"	<TempLeadId>" + tempLeadId + "</TempLeadId>\r\n" +
			"	<DivisionId>" + divisionId + "</DivisionId>\r\n" +
			"	<PromotionId>" + promotionId + "</PromotionId>\r\n" +
			"	<ChannelCode>" + System.Web.HttpUtility.HtmlEncode(channelCode) + "</ChannelCode>\r\n" +
			"	<ConsultantId>" + consultantId + "</ConsultantId>\r\n" +
			"	<GroupTypeId>" + groupTypeId + "</GroupTypeId>\r\n" +
			"	<OrganizationTypeId>" + organizationTypeId + "</OrganizationTypeId>\r\n" +
			"	<HearId>" + hearId + "</HearId>\r\n" +
			"	<FkKitTypeId>" + fkKitTypeId + "</FkKitTypeId>\r\n" +
			"	<OldLeadId>" + oldLeadId + "</OldLeadId>\r\n" +
			"	<AssignerId>" + assignerId + "</AssignerId>\r\n" +
			"	<RefereeId>" + refereeId + "</RefereeId>\r\n" +
			"	<TitleId>" + titleId + "</TitleId>\r\n" +
			"	<CampaignReasonId>" + campaignReasonId + "</CampaignReasonId>\r\n" +
			"	<WebSiteId>" + webSiteId + "</WebSiteId>\r\n" +
			"	<PromotionCodeId>" + promotionCodeId + "</PromotionCodeId>\r\n" +
			"	<ActivityClosingReasonId>" + activityClosingReasonId + "</ActivityClosingReasonId>\r\n" +
			"	<ExtConsultantId>" + extConsultantId + "</ExtConsultantId>\r\n" +
			"	<Salutation>" + System.Web.HttpUtility.HtmlEncode(salutation) + "</Salutation>\r\n" +
			"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
			"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
			"	<Organization>" + System.Web.HttpUtility.HtmlEncode(organization) + "</Organization>\r\n" +
			"	<StreetAddress>" + System.Web.HttpUtility.HtmlEncode(streetAddress) + "</StreetAddress>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<DayPhone>" + System.Web.HttpUtility.HtmlEncode(dayPhone) + "</DayPhone>\r\n" +
			"	<DayTimeCall>" + System.Web.HttpUtility.HtmlEncode(dayTimeCall) + "</DayTimeCall>\r\n" +
			"	<EveningPhone>" + System.Web.HttpUtility.HtmlEncode(eveningPhone) + "</EveningPhone>\r\n" +
			"	<EveningTimeCall>" + System.Web.HttpUtility.HtmlEncode(eveningTimeCall) + "</EveningTimeCall>\r\n" +
			"	<Fax>" + System.Web.HttpUtility.HtmlEncode(fax) + "</Fax>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<LeadEntryDate>" + leadEntryDate + "</LeadEntryDate>\r\n" +
			"	<MemberCount>" + memberCount + "</MemberCount>\r\n" +
			"	<ParticipantCount>" + participantCount + "</ParticipantCount>\r\n" +
			"	<FundRaisingGoal>" + fundRaisingGoal + "</FundRaisingGoal>\r\n" +
			"	<DecisionDate>" + decisionDate + "</DecisionDate>\r\n" +
			"	<DecisionMaker>" + decisionMaker + "</DecisionMaker>\r\n" +
			"	<CommitteeMeetingRequired>" + committeeMeetingRequired + "</CommitteeMeetingRequired>\r\n" +
			"	<CommitteeMeetingDate>" + committeeMeetingDate + "</CommitteeMeetingDate>\r\n" +
			"	<FundRaiserStartDate>" + fundRaiserStartDate + "</FundRaiserStartDate>\r\n" +
			"	<Onemaillist>" + onemaillist + "</Onemaillist>\r\n" +
			"	<Faxkit>" + faxkit + "</Faxkit>\r\n" +
			"	<Emailkit>" + emailkit + "</Emailkit>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"	<KitToSend>" + kitToSend + "</KitToSend>\r\n" +
			"	<KitSent>" + kitSent + "</KitSent>\r\n" +
			"	<KitSentDate>" + kitSentDate + "</KitSentDate>\r\n" +
			"	<LeadAssignmentDate>" + leadAssignmentDate + "</LeadAssignmentDate>\r\n" +
			"	<Interests>" + System.Web.HttpUtility.HtmlEncode(interests) + "</Interests>\r\n" +
			"	<HasBeenContacted>" + hasBeenContacted + "</HasBeenContacted>\r\n" +
			"	<DayPhoneExt>" + System.Web.HttpUtility.HtmlEncode(dayPhoneExt) + "</DayPhoneExt>\r\n" +
			"	<EveningPhoneExt>" + System.Web.HttpUtility.HtmlEncode(eveningPhoneExt) + "</EveningPhoneExt>\r\n" +
			"	<OtherPhone>" + System.Web.HttpUtility.HtmlEncode(otherPhone) + "</OtherPhone>\r\n" +
			"	<GroupWebSite>" + System.Web.HttpUtility.HtmlEncode(groupWebSite) + "</GroupWebSite>\r\n" +
			"	<NbQueries>" + nbQueries + "</NbQueries>\r\n" +
			"	<SubmitDate>" + submitDate + "</SubmitDate>\r\n" +
			"	<CookieContent>" + System.Web.HttpUtility.HtmlEncode(cookieContent) + "</CookieContent>\r\n" +
			"	<FirstContactDate>" + firstContactDate + "</FirstContactDate>\r\n" +
			"	<DayPhoneIsGood>" + dayPhoneIsGood + "</DayPhoneIsGood>\r\n" +
			"	<EveningPhoneIsGood>" + eveningPhoneIsGood + "</EveningPhoneIsGood>\r\n" +
			"	<AccountNumber>" + accountNumber + "</AccountNumber>\r\n" +
			"	<ValidEmail>" + validEmail + "</ValidEmail>\r\n" +
			"	<OtherClosingActivityReason>" + System.Web.HttpUtility.HtmlEncode(otherClosingActivityReason) + "</OtherClosingActivityReason>\r\n" +
			"	<TransferedDate>" + transferedDate + "</TransferedDate>\r\n" +
			"	<MatchingCode>" + System.Web.HttpUtility.HtmlEncode(matchingCode) + "</MatchingCode>\r\n" +
			"	<PhoneNumberTrackingId>" + phoneNumberTrackingId + "</PhoneNumberTrackingId>\r\n" +
			"	<CustomerStatusId>" + customerStatusId + "</CustomerStatusId>\r\n" +
			"	<AddressZoneId>" + addressZoneId + "</AddressZoneId>\r\n" +
			"</Lead>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadStatusId")) {
					SetXmlValue(ref leadStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadQualificationTypeId")) {
					SetXmlValue(ref leadQualificationTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadPriorityId")) {
					SetXmlValue(ref leadPriorityId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("tempLeadId")) {
					SetXmlValue(ref tempLeadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("divisionId")) {
					SetXmlValue(ref divisionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionId")) {
					SetXmlValue(ref promotionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("channelCode")) {
					SetXmlValue(ref channelCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("groupTypeId")) {
					SetXmlValue(ref groupTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organizationTypeId")) {
					SetXmlValue(ref organizationTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("hearId")) {
					SetXmlValue(ref hearId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fkKitTypeId")) {
					SetXmlValue(ref fkKitTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("oldLeadId")) {
					SetXmlValue(ref oldLeadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("assignerId")) {
					SetXmlValue(ref assignerId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("refereeId")) {
					SetXmlValue(ref refereeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("titleId")) {
					SetXmlValue(ref titleId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("campaignReasonId")) {
					SetXmlValue(ref campaignReasonId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("webSiteId")) {
					SetXmlValue(ref webSiteId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionCodeId")) {
					SetXmlValue(ref promotionCodeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("activityClosingReasonId")) {
					SetXmlValue(ref activityClosingReasonId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("extConsultantId")) {
					SetXmlValue(ref extConsultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salutation")) {
					SetXmlValue(ref salutation, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("firstName")) {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("lastName")) {
					SetXmlValue(ref lastName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organization")) {
					SetXmlValue(ref organization, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("streetAddress")) {
					SetXmlValue(ref streetAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("city")) {
					SetXmlValue(ref city, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zipCode")) {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayPhone")) {
					SetXmlValue(ref dayPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayTimeCall")) {
					SetXmlValue(ref dayTimeCall, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("eveningPhone")) {
					SetXmlValue(ref eveningPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("eveningTimeCall")) {
					SetXmlValue(ref eveningTimeCall, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fax")) {
					SetXmlValue(ref fax, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadEntryDate")) {
					SetXmlValue(ref leadEntryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("memberCount")) {
					SetXmlValue(ref memberCount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("participantCount")) {
					SetXmlValue(ref participantCount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fundRaisingGoal")) {
					SetXmlValue(ref fundRaisingGoal, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("decisionDate")) {
					SetXmlValue(ref decisionDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("decisionMaker")) {
					SetXmlValue(ref decisionMaker, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("committeeMeetingRequired")) {
					SetXmlValue(ref committeeMeetingRequired, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("committeeMeetingDate")) {
					SetXmlValue(ref committeeMeetingDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fundRaiserStartDate")) {
					SetXmlValue(ref fundRaiserStartDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("onemaillist")) {
					SetXmlValue(ref onemaillist, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("faxkit")) {
					SetXmlValue(ref faxkit, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("emailkit")) {
					SetXmlValue(ref emailkit, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("kitToSend")) {
					SetXmlValue(ref kitToSend, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("kitSent")) {
					SetXmlValue(ref kitSent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("kitSentDate")) {
					SetXmlValue(ref kitSentDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadAssignmentDate")) {
					SetXmlValue(ref leadAssignmentDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("interests")) {
					SetXmlValue(ref interests, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("hasBeenContacted")) {
					SetXmlValue(ref hasBeenContacted, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayPhoneExt")) {
					SetXmlValue(ref dayPhoneExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("eveningPhoneExt")) {
					SetXmlValue(ref eveningPhoneExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("otherPhone")) {
					SetXmlValue(ref otherPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("groupWebSite")) {
					SetXmlValue(ref groupWebSite, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("nbQueries")) {
					SetXmlValue(ref nbQueries, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("submitDate")) {
					SetXmlValue(ref submitDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cookieContent")) {
					SetXmlValue(ref cookieContent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("firstContactDate")) {
					SetXmlValue(ref firstContactDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayPhoneIsGood")) {
					SetXmlValue(ref dayPhoneIsGood, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("eveningPhoneIsGood")) {
					SetXmlValue(ref eveningPhoneIsGood, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("accountNumber")) {
					SetXmlValue(ref accountNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("validEmail")) {
					SetXmlValue(ref validEmail, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("otherClosingActivityReason")) {
					SetXmlValue(ref otherClosingActivityReason, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("transferedDate")) {
					SetXmlValue(ref transferedDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("matchingCode")) {
					SetXmlValue(ref matchingCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phoneNumberTrackingId")) {
					SetXmlValue(ref phoneNumberTrackingId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("customerStatusId")) {
					SetXmlValue(ref customerStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("addressZoneId")) {
					SetXmlValue(ref addressZoneId, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Lead[] GetLeads() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeads();
		}
		
		public static Lead[] GetLeadsWithoutLeadVisit()
		{
			DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadsWithoutLeadVisits();
		}

		public static Lead GetLeadByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadByID(id);
		}
		

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLead(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLead(this);
		}

		public static LeadCollection GetLeadByDates(DateTime lowerDate, DateTime upperDate)
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadByDates(lowerDate, upperDate);
		}
		#endregion

		#region Properties
		public int LeadId {
			set { leadId = value; }
			get { return leadId; }
		}

		public int LeadStatusId {
			set { leadStatusId = value; }
			get { return leadStatusId; }
		}

		public int LeadQualificationTypeId {
			set { leadQualificationTypeId = value; }
			get { return leadQualificationTypeId; }
		}

		public int LeadPriorityId {
			set { leadPriorityId = value; }
			get { return leadPriorityId; }
		}

		public int TempLeadId {
			set { tempLeadId = value; }
			get { return tempLeadId; }
		}

		public short DivisionId {
			set { divisionId = value; }
			get { return divisionId; }
		}

		public int PromotionId {
			set { promotionId = value; }
			get { return promotionId; }
		}

		public string ChannelCode {
			set { channelCode = value; }
			get { return channelCode; }
		}

		public int ConsultantId {
			set { consultantId = value; }
			get { return consultantId; }
		}

		public short GroupTypeId {
			set { groupTypeId = value; }
			get { return groupTypeId; }
		}

		public short OrganizationTypeId {
			set { organizationTypeId = value; }
			get { return organizationTypeId; }
		}

		public short HearId {
			set { hearId = value; }
			get { return hearId; }
		}

		public int FkKitTypeId {
			set { fkKitTypeId = value; }
			get { return fkKitTypeId; }
		}

		public int OldLeadId {
			set { oldLeadId = value; }
			get { return oldLeadId; }
		}

		public int AssignerId {
			set { assignerId = value; }
			get { return assignerId; }
		}

		public int RefereeId {
			set { refereeId = value; }
			get { return refereeId; }
		}

		public short TitleId {
			set { titleId = value; }
			get { return titleId; }
		}

		public short CampaignReasonId {
			set { campaignReasonId = value; }
			get { return campaignReasonId; }
		}

		public int WebSiteId {
			set { webSiteId = value; }
			get { return webSiteId; }
		}

		public int PromotionCodeId {
			set { promotionCodeId = value; }
			get { return promotionCodeId; }
		}

		public short ActivityClosingReasonId {
			set { activityClosingReasonId = value; }
			get { return activityClosingReasonId; }
		}

		public int ExtConsultantId {
			set { extConsultantId = value; }
			get { return extConsultantId; }
		}

		public string Vif
		{
			set { vif = value; }
			get { return vif; }
		}

		public string Salutation {
			set { salutation = value; }
			get { return salutation; }
		}

		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

		public string Organization {
			set { organization = value; }
			get { return organization; }
		}

		public string StreetAddress {
			set { streetAddress = value; }
			get { return streetAddress; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public string DayPhone {
			set { dayPhone = value; }
			get { return dayPhone; }
		}

		public string DayTimeCall {
			set { dayTimeCall = value; }
			get { return dayTimeCall; }
		}

		public string EveningPhone {
			set { eveningPhone = value; }
			get { return eveningPhone; }
		}

		public string EveningTimeCall {
			set { eveningTimeCall = value; }
			get { return eveningTimeCall; }
		}

		public string Fax {
			set { fax = value; }
			get { return fax; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public DateTime LeadEntryDate {
			set { leadEntryDate = value; }
			get { return leadEntryDate; }
		}

		public int MemberCount {
			set { memberCount = value; }
			get { return memberCount; }
		}

		public int ParticipantCount {
			set { participantCount = value; }
			get { return participantCount; }
		}

		public int FundRaisingGoal {
			set { fundRaisingGoal = value; }
			get { return fundRaisingGoal; }
		}

		public DateTime DecisionDate {
			set { decisionDate = value; }
			get { return decisionDate; }
		}

		public bool DecisionMaker {
			set { decisionMaker = value; }
			get { return decisionMaker; }
		}

		public bool CommitteeMeetingRequired {
			set { committeeMeetingRequired = value; }
			get { return committeeMeetingRequired; }
		}

		public DateTime CommitteeMeetingDate {
			set { committeeMeetingDate = value; }
			get { return committeeMeetingDate; }
		}

		public DateTime FundRaiserStartDate {
			set { fundRaiserStartDate = value; }
			get { return fundRaiserStartDate; }
		}

		public bool Onemaillist {
			set { onemaillist = value; }
			get { return onemaillist; }
		}

		public bool Faxkit {
			set { faxkit = value; }
			get { return faxkit; }
		}

		public bool Emailkit {
			set { emailkit = value; }
			get { return emailkit; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		public bool KitToSend {
			set { kitToSend = value; }
			get { return kitToSend; }
		}

		public bool KitSent {
			set { kitSent = value; }
			get { return kitSent; }
		}

		public DateTime KitSentDate {
			set { kitSentDate = value; }
			get { return kitSentDate; }
		}

		public DateTime LeadAssignmentDate {
			set { leadAssignmentDate = value; }
			get { return leadAssignmentDate; }
		}

		public string Interests {
			set { interests = value; }
			get { return interests; }
		}

		public bool HasBeenContacted {
			set { hasBeenContacted = value; }
			get { return hasBeenContacted; }
		}

		public string DayPhoneExt {
			set { dayPhoneExt = value; }
			get { return dayPhoneExt; }
		}

		public string EveningPhoneExt {
			set { eveningPhoneExt = value; }
			get { return eveningPhoneExt; }
		}

		public string OtherPhone {
			set { otherPhone = value; }
			get { return otherPhone; }
		}

		public string GroupWebSite {
			set { groupWebSite = value; }
			get { return groupWebSite; }
		}

		public int NbQueries {
			set { nbQueries = value; }
			get { return nbQueries; }
		}

		public DateTime SubmitDate {
			set { submitDate = value; }
			get { return submitDate; }
		}

		public string CookieContent {
			set { cookieContent = value; }
			get { return cookieContent; }
		}

		public DateTime FirstContactDate {
			set { firstContactDate = value; }
			get { return firstContactDate; }
		}

		public bool DayPhoneIsGood {
			set { dayPhoneIsGood = value; }
			get { return dayPhoneIsGood; }
		}

		public bool EveningPhoneIsGood {
			set { eveningPhoneIsGood = value; }
			get { return eveningPhoneIsGood; }
		}

		public int AccountNumber {
			set { accountNumber = value; }
			get { return accountNumber; }
		}

		public bool ValidEmail {
			set { validEmail = value; }
			get { return validEmail; }
		}

		public string OtherClosingActivityReason {
			set { otherClosingActivityReason = value; }
			get { return otherClosingActivityReason; }
		}

		public DateTime TransferedDate {
			set { transferedDate = value; }
			get { return transferedDate; }
		}

		public string MatchingCode {
			set { matchingCode = value; }
			get { return matchingCode; }
		}

		public int PhoneNumberTrackingId {
			set { phoneNumberTrackingId = value; }
			get { return phoneNumberTrackingId; }
		}

		public int CustomerStatusId {
			set { customerStatusId = value; }
			get { return customerStatusId; }
		}

		public int ClientStatusId 
		{
			set { clientStatusId = value; }
			get { return clientStatusId; }
		}

		public int AddressZoneId {
			get { return addressZoneId; }
			set { addressZoneId = value; }
		}

		public byte FundraisersPerYear 
		{
			get { return fundraisersPerYear; }
			set { fundraisersPerYear = value; }
		}

        public int PartnerID
        {
            get { return partnerId; }
            set { partnerId = value; }
        }

        public bool IsConsultantActive
        {
            get { return isConsultantActive; }
            set { isConsultantActive = value; }
        }
		#endregion
	}

	public class LeadCollection : efundraising.Collections.BusinessCollectionBase
	{
		public LeadCollection()
		{
		}

		
		/// <summary>Get or set the object at specified index.</summary>
		/// 
		public Lead this[int index]
		{
			get { return (Lead) List[index]; }
			set { List[index] = value; }
		}

		/// <summary>Add object to collection.</summary>
		/// <param name="obj">Lead object.</param>
		/// <returns>Index of the newly added object.</returns>
		/// 
		public int Add(Lead obj)
		{
			return List.Add(obj);
		}

		/// <summary>Add collection to collection of objects.</summary>
		/// <param name="obj">LeadCollection object.</param>
		/// 
		public void Add(LeadCollection obj)
		{
			if (obj != null)
			{
				for (int i = 0; i < obj.Count; i++)
				{
					List.Add(obj[i]);
				}
			}
		}

		/// <summary>Remove object from collection.</summary>
		/// <param name="obj">Lead object.</param>
		/// 
		public void Remove(Lead obj)
		{
			List.Remove(obj);
		}

		/// <summary>Check if object is in collection.</summary>
		/// <param name="obj">Lead object</param>
		/// <returns>True if object is in collection, else false.</returns>
		/// 
		public bool Contains(Lead obj)
		{
			return List.Contains(obj);
		}

		/// <summary>Get the index associated with the object in collection.</summary>
		/// <param name="obj">Lead object.</param>
		/// <returns>The index of the object.</returns>
		/// 
		public int IndexOf(Lead obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>Insert object into collection at the specified index.</summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">Lead object.</param>
		/// 
		public void Insert(int index, Lead obj) 
		{
			List.Insert(index, obj);
		}

		
	}
}
