using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class TempLead: EFundraisingCRMDataObject {

		private int divisionID;
		private int promotionID;
		private int tempLeadId;
		private string channelCode;
		private int leadStatusID;
		private int consultantID;
		private DateTime leadEntryDate;
		private string salutation;
		private string firstName;
		private string lastName;
		private string title;
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
		private int groupTypeID;
		private int memberCount;
		private int participantCount;
		private int fundRaisingGoal;
		private DateTime decisionDate;
		private int decisionMaker;
		private int committeeMeetingRequired;
		private DateTime committeeMeetingDate;
		private DateTime fundRaiserStartDate;
		private int onEmailList;
		private int faxKit;
		private int emailKit;
		private string comments;
		private int hearId;
		private int kitToSend;
		private int kitSent;
		private DateTime kitSentDate;
		private int oldLeadId;
		private DateTime leadAssignmentDate;
		private string interests;
		private int hasBeenContacted;
		private int fkKitTypeID;
		private int leadPriorityId;
		private string dayPhoneExt;
		private string eveningPhoneExt;
		private string rejectionReason;
		private string otherPhone;
		private string otherPhoneExt;
		private string groupWebSite;
		private int organizationTypeId;
		private int campaignReasonId;
		private int titleId;
		private string cookieContent;
		private string campaignReason;
		private int webSiteId;
		private int isNew;


		public TempLead() : this(int.MinValue) { }
		public TempLead(int divisionID) : this(divisionID, int.MinValue) { }
		public TempLead(int divisionID, int promotionID) : this(divisionID, promotionID, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId) : this(divisionID, promotionID, tempLeadId, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode) : this(divisionID, promotionID, tempLeadId, channelCode, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, DateTime.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, DateTime.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, DateTime.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, DateTime.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, DateTime.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, DateTime.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, dayPhoneExt, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt, string eveningPhoneExt) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, dayPhoneExt, eveningPhoneExt, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt, string eveningPhoneExt, string rejectionReason) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, dayPhoneExt, eveningPhoneExt, rejectionReason, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string otherPhoneExt) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, otherPhoneExt, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string otherPhoneExt, string groupWebSite) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, otherPhoneExt, groupWebSite, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string otherPhoneExt, string groupWebSite, int organizationTypeId) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, otherPhoneExt, groupWebSite, organizationTypeId, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string otherPhoneExt, string groupWebSite, int organizationTypeId, int campaignReasonId) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, otherPhoneExt, groupWebSite, organizationTypeId, campaignReasonId, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string otherPhoneExt, string groupWebSite, int organizationTypeId, int campaignReasonId, int titleId) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, otherPhoneExt, groupWebSite, organizationTypeId, campaignReasonId, titleId, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string otherPhoneExt, string groupWebSite, int organizationTypeId, int campaignReasonId, int titleId, string cookieContent) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, otherPhoneExt, groupWebSite, organizationTypeId, campaignReasonId, titleId, cookieContent, null) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string otherPhoneExt, string groupWebSite, int organizationTypeId, int campaignReasonId, int titleId, string cookieContent, string campaignReason) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, otherPhoneExt, groupWebSite, organizationTypeId, campaignReasonId, titleId, cookieContent, campaignReason, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string otherPhoneExt, string groupWebSite, int organizationTypeId, int campaignReasonId, int titleId, string cookieContent, string campaignReason, int webSiteId) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, title, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, eveningTimeCall, fax, email, groupTypeID, memberCount, participantCount, fundRaisingGoal, decisionDate, decisionMaker, committeeMeetingRequired, committeeMeetingDate, fundRaiserStartDate, onEmailList, faxKit, emailKit, comments, hearId, kitToSend, kitSent, kitSentDate, oldLeadId, leadAssignmentDate, interests, hasBeenContacted, fkKitTypeID, leadPriorityId, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, otherPhoneExt, groupWebSite, organizationTypeId, campaignReasonId, titleId, cookieContent, campaignReason, webSiteId, int.MinValue) { }
		public TempLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string title, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string eveningTimeCall, string fax, string email, int groupTypeID, int memberCount, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, int committeeMeetingRequired, DateTime committeeMeetingDate, DateTime fundRaiserStartDate, int onEmailList, int faxKit, int emailKit, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, int oldLeadId, DateTime leadAssignmentDate, string interests, int hasBeenContacted, int fkKitTypeID, int leadPriorityId, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string otherPhoneExt, string groupWebSite, int organizationTypeId, int campaignReasonId, int titleId, string cookieContent, string campaignReason, int webSiteId, int isNew) {
			this.divisionID = divisionID;
			this.promotionID = promotionID;
			this.tempLeadId = tempLeadId;
			this.channelCode = channelCode;
			this.leadStatusID = leadStatusID;
			this.consultantID = consultantID;
			this.leadEntryDate = leadEntryDate;
			this.salutation = salutation;
			this.firstName = firstName;
			this.lastName = lastName;
			this.title = title;
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
			this.groupTypeID = groupTypeID;
			this.memberCount = memberCount;
			this.participantCount = participantCount;
			this.fundRaisingGoal = fundRaisingGoal;
			this.decisionDate = decisionDate;
			this.decisionMaker = decisionMaker;
			this.committeeMeetingRequired = committeeMeetingRequired;
			this.committeeMeetingDate = committeeMeetingDate;
			this.fundRaiserStartDate = fundRaiserStartDate;
			this.onEmailList = onEmailList;
			this.faxKit = faxKit;
			this.emailKit = emailKit;
			this.comments = comments;
			this.hearId = hearId;
			this.kitToSend = kitToSend;
			this.kitSent = kitSent;
			this.kitSentDate = kitSentDate;
			this.oldLeadId = oldLeadId;
			this.leadAssignmentDate = leadAssignmentDate;
			this.interests = interests;
			this.hasBeenContacted = hasBeenContacted;
			this.fkKitTypeID = fkKitTypeID;
			this.leadPriorityId = leadPriorityId;
			this.dayPhoneExt = dayPhoneExt;
			this.eveningPhoneExt = eveningPhoneExt;
			this.rejectionReason = rejectionReason;
			this.otherPhone = otherPhone;
			this.otherPhoneExt = otherPhoneExt;
			this.groupWebSite = groupWebSite;
			this.organizationTypeId = organizationTypeId;
			this.campaignReasonId = campaignReasonId;
			this.titleId = titleId;
			this.cookieContent = cookieContent;
			this.campaignReason = campaignReason;
			this.webSiteId = webSiteId;
			this.isNew = isNew;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TempLead>\r\n" +
			"	<DivisionID>" + divisionID + "</DivisionID>\r\n" +
			"	<PromotionID>" + promotionID + "</PromotionID>\r\n" +
			"	<TempLeadId>" + tempLeadId + "</TempLeadId>\r\n" +
			"	<ChannelCode>" + System.Web.HttpUtility.HtmlEncode(channelCode) + "</ChannelCode>\r\n" +
			"	<LeadStatusID>" + leadStatusID + "</LeadStatusID>\r\n" +
			"	<ConsultantID>" + consultantID + "</ConsultantID>\r\n" +
			"	<LeadEntryDate>" + leadEntryDate + "</LeadEntryDate>\r\n" +
			"	<Salutation>" + System.Web.HttpUtility.HtmlEncode(salutation) + "</Salutation>\r\n" +
			"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
			"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
			"	<Title>" + System.Web.HttpUtility.HtmlEncode(title) + "</Title>\r\n" +
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
			"	<GroupTypeID>" + groupTypeID + "</GroupTypeID>\r\n" +
			"	<MemberCount>" + memberCount + "</MemberCount>\r\n" +
			"	<ParticipantCount>" + participantCount + "</ParticipantCount>\r\n" +
			"	<FundRaisingGoal>" + fundRaisingGoal + "</FundRaisingGoal>\r\n" +
			"	<DecisionDate>" + decisionDate + "</DecisionDate>\r\n" +
			"	<DecisionMaker>" + decisionMaker + "</DecisionMaker>\r\n" +
			"	<CommitteeMeetingRequired>" + committeeMeetingRequired + "</CommitteeMeetingRequired>\r\n" +
			"	<CommitteeMeetingDate>" + committeeMeetingDate + "</CommitteeMeetingDate>\r\n" +
			"	<FundRaiserStartDate>" + fundRaiserStartDate + "</FundRaiserStartDate>\r\n" +
			"	<OnEmailList>" + onEmailList + "</OnEmailList>\r\n" +
			"	<FaxKit>" + faxKit + "</FaxKit>\r\n" +
			"	<EmailKit>" + emailKit + "</EmailKit>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"	<HearId>" + hearId + "</HearId>\r\n" +
			"	<KitToSend>" + kitToSend + "</KitToSend>\r\n" +
			"	<KitSent>" + kitSent + "</KitSent>\r\n" +
			"	<KitSentDate>" + kitSentDate + "</KitSentDate>\r\n" +
			"	<OldLeadId>" + oldLeadId + "</OldLeadId>\r\n" +
			"	<LeadAssignmentDate>" + leadAssignmentDate + "</LeadAssignmentDate>\r\n" +
			"	<Interests>" + System.Web.HttpUtility.HtmlEncode(interests) + "</Interests>\r\n" +
			"	<HasBeenContacted>" + hasBeenContacted + "</HasBeenContacted>\r\n" +
			"	<FkKitTypeID>" + fkKitTypeID + "</FkKitTypeID>\r\n" +
			"	<LeadPriorityId>" + leadPriorityId + "</LeadPriorityId>\r\n" +
			"	<DayPhoneExt>" + System.Web.HttpUtility.HtmlEncode(dayPhoneExt) + "</DayPhoneExt>\r\n" +
			"	<EveningPhoneExt>" + System.Web.HttpUtility.HtmlEncode(eveningPhoneExt) + "</EveningPhoneExt>\r\n" +
			"	<RejectionReason>" + System.Web.HttpUtility.HtmlEncode(rejectionReason) + "</RejectionReason>\r\n" +
			"	<OtherPhone>" + System.Web.HttpUtility.HtmlEncode(otherPhone) + "</OtherPhone>\r\n" +
			"	<OtherPhoneExt>" + System.Web.HttpUtility.HtmlEncode(otherPhoneExt) + "</OtherPhoneExt>\r\n" +
			"	<GroupWebSite>" + System.Web.HttpUtility.HtmlEncode(groupWebSite) + "</GroupWebSite>\r\n" +
			"	<OrganizationTypeId>" + organizationTypeId + "</OrganizationTypeId>\r\n" +
			"	<CampaignReasonId>" + campaignReasonId + "</CampaignReasonId>\r\n" +
			"	<TitleId>" + titleId + "</TitleId>\r\n" +
			"	<CookieContent>" + System.Web.HttpUtility.HtmlEncode(cookieContent) + "</CookieContent>\r\n" +
			"	<CampaignReason>" + System.Web.HttpUtility.HtmlEncode(campaignReason) + "</CampaignReason>\r\n" +
			"	<WebSiteId>" + webSiteId + "</WebSiteId>\r\n" +
			"	<IsNew>" + isNew + "</IsNew>\r\n" +
			"</TempLead>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("divisionId")) {
					SetXmlValue(ref divisionID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promotionId")) {
					SetXmlValue(ref promotionID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("tempLeadId")) {
					SetXmlValue(ref tempLeadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("channelCode")) {
					SetXmlValue(ref channelCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadStatusId")) {
					SetXmlValue(ref leadStatusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadEntryDate")) {
					SetXmlValue(ref leadEntryDate, node.InnerText);
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
				if(ToLowerCase(node.Name) == ToLowerCase("title")) {
					SetXmlValue(ref title, node.InnerText);
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
				if(ToLowerCase(node.Name) == ToLowerCase("groupTypeId")) {
					SetXmlValue(ref groupTypeID, node.InnerText);
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
					SetXmlValue(ref onEmailList, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("faxkit")) {
					SetXmlValue(ref faxKit, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("emailkit")) {
					SetXmlValue(ref emailKit, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("hearId")) {
					SetXmlValue(ref hearId, node.InnerText);
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
				if(ToLowerCase(node.Name) == ToLowerCase("oldLeadId")) {
					SetXmlValue(ref oldLeadId, node.InnerText);
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
				if(ToLowerCase(node.Name) == ToLowerCase("fkKitTypeId")) {
					SetXmlValue(ref fkKitTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadPriorityId")) {
					SetXmlValue(ref leadPriorityId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayPhoneExt")) {
					SetXmlValue(ref dayPhoneExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("eveningPhoneExt")) {
					SetXmlValue(ref eveningPhoneExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("rejectionReason")) {
					SetXmlValue(ref rejectionReason, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("otherPhone")) {
					SetXmlValue(ref otherPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("otherPhoneExt")) {
					SetXmlValue(ref otherPhoneExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("groupWebSite")) {
					SetXmlValue(ref groupWebSite, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organizationTypeId")) {
					SetXmlValue(ref organizationTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("campaignReasonId")) {
					SetXmlValue(ref campaignReasonId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("titleId")) {
					SetXmlValue(ref titleId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cookieContent")) {
					SetXmlValue(ref cookieContent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("campaignReason")) {
					SetXmlValue(ref campaignReason, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("webSiteId")) {
					SetXmlValue(ref webSiteId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isnew")) {
					SetXmlValue(ref isNew, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static TempLead[] GetTempLeads() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTempLeads();
		}

		public static TempLead GetTempLeadByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTempLeadByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTempLead(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTempLead(this);
		}
		#endregion

		#region Properties
		public int DivisionID {
			set { divisionID = value; }
			get { return divisionID; }
		}

		public int PromotionID {
			set { promotionID = value; }
			get { return promotionID; }
		}

		public int TempLeadId {
			set { tempLeadId = value; }
			get { return tempLeadId; }
		}

		public string ChannelCode {
			set { channelCode = value; }
			get { return channelCode; }
		}

		public int LeadStatusID {
			set { leadStatusID = value; }
			get { return leadStatusID; }
		}

		public int ConsultantID {
			set { consultantID = value; }
			get { return consultantID; }
		}

		public DateTime LeadEntryDate {
			set { leadEntryDate = value; }
			get { return leadEntryDate; }
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

		public string Title {
			set { title = value; }
			get { return title; }
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

		public int GroupTypeID {
			set { groupTypeID = value; }
			get { return groupTypeID; }
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

		public int DecisionMaker {
			set { decisionMaker = value; }
			get { return decisionMaker; }
		}

		public int CommitteeMeetingRequired {
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

		public int OnEmailList {
			set { onEmailList = value; }
			get { return onEmailList; }
		}

		public int FaxKit {
			set { faxKit = value; }
			get { return faxKit; }
		}

		public int EmailKit {
			set { emailKit = value; }
			get { return emailKit; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		public int HearId {
			set { hearId = value; }
			get { return hearId; }
		}

		public int KitToSend {
			set { kitToSend = value; }
			get { return kitToSend; }
		}

		public int KitSent {
			set { kitSent = value; }
			get { return kitSent; }
		}

		public DateTime KitSentDate {
			set { kitSentDate = value; }
			get { return kitSentDate; }
		}

		public int OldLeadId {
			set { oldLeadId = value; }
			get { return oldLeadId; }
		}

		public DateTime LeadAssignmentDate {
			set { leadAssignmentDate = value; }
			get { return leadAssignmentDate; }
		}

		public string Interests {
			set { interests = value; }
			get { return interests; }
		}

		public int HasBeenContacted {
			set { hasBeenContacted = value; }
			get { return hasBeenContacted; }
		}

		public int FkKitTypeID {
			set { fkKitTypeID = value; }
			get { return fkKitTypeID; }
		}

		public int LeadPriorityId {
			set { leadPriorityId = value; }
			get { return leadPriorityId; }
		}

		public string DayPhoneExt {
			set { dayPhoneExt = value; }
			get { return dayPhoneExt; }
		}

		public string EveningPhoneExt {
			set { eveningPhoneExt = value; }
			get { return eveningPhoneExt; }
		}

		public string RejectionReason {
			set { rejectionReason = value; }
			get { return rejectionReason; }
		}

		public string OtherPhone {
			set { otherPhone = value; }
			get { return otherPhone; }
		}

		public string OtherPhoneExt {
			set { otherPhoneExt = value; }
			get { return otherPhoneExt; }
		}

		public string GroupWebSite {
			set { groupWebSite = value; }
			get { return groupWebSite; }
		}

		public int OrganizationTypeId {
			set { organizationTypeId = value; }
			get { return organizationTypeId; }
		}

		public int CampaignReasonId {
			set { campaignReasonId = value; }
			get { return campaignReasonId; }
		}

		public int TitleId {
			set { titleId = value; }
			get { return titleId; }
		}

		public string CookieContent {
			set { cookieContent = value; }
			get { return cookieContent; }
		}

		public string CampaignReason {
			set { campaignReason = value; }
			get { return campaignReason; }
		}

		public int WebSiteId {
			set { webSiteId = value; }
			get { return webSiteId; }
		}

		public int IsNew {
			set { isNew = value; }
			get { return isNew; }
		}

		#endregion
	}
}
