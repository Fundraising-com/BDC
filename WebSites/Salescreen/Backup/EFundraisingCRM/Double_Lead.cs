using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class DoubleLead: EFundraisingCRMDataObject {

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
		private string organization;
		private string streetAddress;
		private string city;
		private string stateCode;
		private string countryCode;
		private string zipCode;
		private string dayPhone;
		private string dayTimeCall;
		private string eveningPhone;
		private string fax;
		private string email;
		private int groupTypeID;
		private int participantCount;
		private int fundRaisingGoal;
		private DateTime decisionDate;
		private int decisionMaker;
		private DateTime fundRaiserStartDate;
		private int onEmailList;
		private string comments;
		private int hearId;
		private int kitToSend;
		private int kitSent;
		private DateTime kitSentDate;
		private string dayPhoneExt;
		private string eveningPhoneExt;
		private string rejectionReason;
		private string otherPhone;
		private string cookieContent;
		private string groupWebSite;
		private int organizationTypeId;
		private int titleId;
		private string otherPhoneExt;
		private int campaignReasonId;
		private int webSiteId;


		public DoubleLead() : this(int.MinValue) { }
		public DoubleLead(int divisionID) : this(divisionID, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID) : this(divisionID, promotionID, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId) : this(divisionID, promotionID, tempLeadId, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode) : this(divisionID, promotionID, tempLeadId, channelCode, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, DateTime.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, DateTime.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, DateTime.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, kitSent, DateTime.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, kitSent, kitSentDate, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, string dayPhoneExt) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, kitSent, kitSentDate, dayPhoneExt, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, string dayPhoneExt, string eveningPhoneExt) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, kitSent, kitSentDate, dayPhoneExt, eveningPhoneExt, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, string dayPhoneExt, string eveningPhoneExt, string rejectionReason) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, kitSent, kitSentDate, dayPhoneExt, eveningPhoneExt, rejectionReason, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, kitSent, kitSentDate, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string cookieContent) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, kitSent, kitSentDate, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, cookieContent, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string cookieContent, string groupWebSite) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, kitSent, kitSentDate, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, cookieContent, groupWebSite, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string cookieContent, string groupWebSite, int organizationTypeId) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, kitSent, kitSentDate, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, cookieContent, groupWebSite, organizationTypeId, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string cookieContent, string groupWebSite, int organizationTypeId, int titleId) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, kitSent, kitSentDate, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, cookieContent, groupWebSite, organizationTypeId, titleId, null) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string cookieContent, string groupWebSite, int organizationTypeId, int titleId, string otherPhoneExt) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, kitSent, kitSentDate, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, cookieContent, groupWebSite, organizationTypeId, titleId, otherPhoneExt, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string cookieContent, string groupWebSite, int organizationTypeId, int titleId, string otherPhoneExt, int campaignReasonId) : this(divisionID, promotionID, tempLeadId, channelCode, leadStatusID, consultantID, leadEntryDate, salutation, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, groupTypeID, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onEmailList, comments, hearId, kitToSend, kitSent, kitSentDate, dayPhoneExt, eveningPhoneExt, rejectionReason, otherPhone, cookieContent, groupWebSite, organizationTypeId, titleId, otherPhoneExt, campaignReasonId, int.MinValue) { }
		public DoubleLead(int divisionID, int promotionID, int tempLeadId, string channelCode, int leadStatusID, int consultantID, DateTime leadEntryDate, string salutation, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int groupTypeID, int participantCount, int fundRaisingGoal, DateTime decisionDate, int decisionMaker, DateTime fundRaiserStartDate, int onEmailList, string comments, int hearId, int kitToSend, int kitSent, DateTime kitSentDate, string dayPhoneExt, string eveningPhoneExt, string rejectionReason, string otherPhone, string cookieContent, string groupWebSite, int organizationTypeId, int titleId, string otherPhoneExt, int campaignReasonId, int webSiteId) {
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
			this.organization = organization;
			this.streetAddress = streetAddress;
			this.city = city;
			this.stateCode = stateCode;
			this.countryCode = countryCode;
			this.zipCode = zipCode;
			this.dayPhone = dayPhone;
			this.dayTimeCall = dayTimeCall;
			this.eveningPhone = eveningPhone;
			this.fax = fax;
			this.email = email;
			this.groupTypeID = groupTypeID;
			this.participantCount = participantCount;
			this.fundRaisingGoal = fundRaisingGoal;
			this.decisionDate = decisionDate;
			this.decisionMaker = decisionMaker;
			this.fundRaiserStartDate = fundRaiserStartDate;
			this.onEmailList = onEmailList;
			this.comments = comments;
			this.hearId = hearId;
			this.kitToSend = kitToSend;
			this.kitSent = kitSent;
			this.kitSentDate = kitSentDate;
			this.dayPhoneExt = dayPhoneExt;
			this.eveningPhoneExt = eveningPhoneExt;
			this.rejectionReason = rejectionReason;
			this.otherPhone = otherPhone;
			this.cookieContent = cookieContent;
			this.groupWebSite = groupWebSite;
			this.organizationTypeId = organizationTypeId;
			this.titleId = titleId;
			this.otherPhoneExt = otherPhoneExt;
			this.campaignReasonId = campaignReasonId;
			this.webSiteId = webSiteId;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<DoubleLead>\r\n" +
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
			"	<Organization>" + System.Web.HttpUtility.HtmlEncode(organization) + "</Organization>\r\n" +
			"	<StreetAddress>" + System.Web.HttpUtility.HtmlEncode(streetAddress) + "</StreetAddress>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<DayPhone>" + System.Web.HttpUtility.HtmlEncode(dayPhone) + "</DayPhone>\r\n" +
			"	<DayTimeCall>" + System.Web.HttpUtility.HtmlEncode(dayTimeCall) + "</DayTimeCall>\r\n" +
			"	<EveningPhone>" + System.Web.HttpUtility.HtmlEncode(eveningPhone) + "</EveningPhone>\r\n" +
			"	<Fax>" + System.Web.HttpUtility.HtmlEncode(fax) + "</Fax>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<GroupTypeID>" + groupTypeID + "</GroupTypeID>\r\n" +
			"	<ParticipantCount>" + participantCount + "</ParticipantCount>\r\n" +
			"	<FundRaisingGoal>" + fundRaisingGoal + "</FundRaisingGoal>\r\n" +
			"	<DecisionDate>" + decisionDate + "</DecisionDate>\r\n" +
			"	<DecisionMaker>" + decisionMaker + "</DecisionMaker>\r\n" +
			"	<FundRaiserStartDate>" + fundRaiserStartDate + "</FundRaiserStartDate>\r\n" +
			"	<OnEmailList>" + onEmailList + "</OnEmailList>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"	<HearId>" + hearId + "</HearId>\r\n" +
			"	<KitToSend>" + kitToSend + "</KitToSend>\r\n" +
			"	<KitSent>" + kitSent + "</KitSent>\r\n" +
			"	<KitSentDate>" + kitSentDate + "</KitSentDate>\r\n" +
			"	<DayPhoneExt>" + System.Web.HttpUtility.HtmlEncode(dayPhoneExt) + "</DayPhoneExt>\r\n" +
			"	<EveningPhoneExt>" + System.Web.HttpUtility.HtmlEncode(eveningPhoneExt) + "</EveningPhoneExt>\r\n" +
			"	<RejectionReason>" + System.Web.HttpUtility.HtmlEncode(rejectionReason) + "</RejectionReason>\r\n" +
			"	<OtherPhone>" + System.Web.HttpUtility.HtmlEncode(otherPhone) + "</OtherPhone>\r\n" +
			"	<CookieContent>" + System.Web.HttpUtility.HtmlEncode(cookieContent) + "</CookieContent>\r\n" +
			"	<GroupWebSite>" + System.Web.HttpUtility.HtmlEncode(groupWebSite) + "</GroupWebSite>\r\n" +
			"	<OrganizationTypeId>" + organizationTypeId + "</OrganizationTypeId>\r\n" +
			"	<TitleId>" + titleId + "</TitleId>\r\n" +
			"	<OtherPhoneExt>" + System.Web.HttpUtility.HtmlEncode(otherPhoneExt) + "</OtherPhoneExt>\r\n" +
			"	<CampaignReasonId>" + campaignReasonId + "</CampaignReasonId>\r\n" +
			"	<WebSiteId>" + webSiteId + "</WebSiteId>\r\n" +
			"</DoubleLead>\r\n";
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
				if(ToLowerCase(node.Name) == ToLowerCase("fax")) {
					SetXmlValue(ref fax, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("groupTypeId")) {
					SetXmlValue(ref groupTypeID, node.InnerText);
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
				if(ToLowerCase(node.Name) == ToLowerCase("fundRaiserStartDate")) {
					SetXmlValue(ref fundRaiserStartDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("onemaillist")) {
					SetXmlValue(ref onEmailList, node.InnerText);
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
				if(ToLowerCase(node.Name) == ToLowerCase("cookieContent")) {
					SetXmlValue(ref cookieContent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("groupWebSite")) {
					SetXmlValue(ref groupWebSite, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organizationTypeId")) {
					SetXmlValue(ref organizationTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("titleId")) {
					SetXmlValue(ref titleId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("otherPhoneExt")) {
					SetXmlValue(ref otherPhoneExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("campaignReasonId")) {
					SetXmlValue(ref campaignReasonId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("webSiteId")) {
					SetXmlValue(ref webSiteId, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static DoubleLead[] GetDoubleLeads() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDoubleLeads();
		}

		public static DoubleLead GetDoubleLeadByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDoubleLeadByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertDoubleLead(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateDoubleLead(this);
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

		public DateTime FundRaiserStartDate {
			set { fundRaiserStartDate = value; }
			get { return fundRaiserStartDate; }
		}

		public int OnEmailList {
			set { onEmailList = value; }
			get { return onEmailList; }
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

		public string CookieContent {
			set { cookieContent = value; }
			get { return cookieContent; }
		}

		public string GroupWebSite {
			set { groupWebSite = value; }
			get { return groupWebSite; }
		}

		public int OrganizationTypeId {
			set { organizationTypeId = value; }
			get { return organizationTypeId; }
		}

		public int TitleId {
			set { titleId = value; }
			get { return titleId; }
		}

		public string OtherPhoneExt {
			set { otherPhoneExt = value; }
			get { return otherPhoneExt; }
		}

		public int CampaignReasonId {
			set { campaignReasonId = value; }
			get { return campaignReasonId; }
		}

		public int WebSiteId {
			set { webSiteId = value; }
			get { return webSiteId; }
		}

		#endregion
	}
}
