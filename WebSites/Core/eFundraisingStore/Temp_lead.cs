using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class TempLead: eFundraisingStoreDataObject {

		private int tempLeadId;
		private int divisionId;
		private int promotionId;
		private string channelCode;
		private int leadStatusId;
		private short organizationTypeId;
		private short campaignReasonId;
		private short webSiteId;
		private short groupTypeId;
		private string salutation;
		private short titleId;
		private short hearId;
		private DateTime leadEntryDate;
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
		private int participantCount;
		private int fundRaisingGoal;
		private DateTime decisionDate;
		private short decisionMaker;
		private DateTime fundRaiserStartDate;
		private short onemaillist;
		private string comments;
		private string dayPhoneExt;
		private string eveningPhoneExt;
		private string otherPhone;
		private string cookieContent;
		private string groupWebSite;
		private string otherPhoneExt;
		private short isnew;
		private short optInSweepstakes;


		public TempLead() : this(int.MinValue) { }
		public TempLead(int tempLeadId) : this(tempLeadId, int.MinValue) { }
		public TempLead(int tempLeadId, int divisionId) : this(tempLeadId, divisionId, int.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId) : this(tempLeadId, divisionId, promotionId, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode) : this(tempLeadId, divisionId, promotionId, channelCode, int.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, short.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, short.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, short.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, short.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, short.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, short.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, DateTime.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, int.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, int.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, DateTime.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, decisionDate, short.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate, short decisionMaker) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, decisionDate, decisionMaker, DateTime.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate, short decisionMaker, DateTime fundRaiserStartDate) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, short.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate, short decisionMaker, DateTime fundRaiserStartDate, short onemaillist) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onemaillist, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate, short decisionMaker, DateTime fundRaiserStartDate, short onemaillist, string comments) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onemaillist, comments, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate, short decisionMaker, DateTime fundRaiserStartDate, short onemaillist, string comments, string dayPhoneExt) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onemaillist, comments, dayPhoneExt, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate, short decisionMaker, DateTime fundRaiserStartDate, short onemaillist, string comments, string dayPhoneExt, string eveningPhoneExt) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onemaillist, comments, dayPhoneExt, eveningPhoneExt, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate, short decisionMaker, DateTime fundRaiserStartDate, short onemaillist, string comments, string dayPhoneExt, string eveningPhoneExt, string otherPhone) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onemaillist, comments, dayPhoneExt, eveningPhoneExt, otherPhone, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate, short decisionMaker, DateTime fundRaiserStartDate, short onemaillist, string comments, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string cookieContent) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onemaillist, comments, dayPhoneExt, eveningPhoneExt, otherPhone, cookieContent, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate, short decisionMaker, DateTime fundRaiserStartDate, short onemaillist, string comments, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string cookieContent, string groupWebSite) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onemaillist, comments, dayPhoneExt, eveningPhoneExt, otherPhone, cookieContent, groupWebSite, null) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate, short decisionMaker, DateTime fundRaiserStartDate, short onemaillist, string comments, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string cookieContent, string groupWebSite, string otherPhoneExt) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onemaillist, comments, dayPhoneExt, eveningPhoneExt, otherPhone, cookieContent, groupWebSite, otherPhoneExt, short.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate, short decisionMaker, DateTime fundRaiserStartDate, short onemaillist, string comments, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string cookieContent, string groupWebSite, string otherPhoneExt, short isnew) : this(tempLeadId, divisionId, promotionId, channelCode, leadStatusId, organizationTypeId, campaignReasonId, webSiteId, groupTypeId, salutation, titleId, hearId, leadEntryDate, firstName, lastName, organization, streetAddress, city, stateCode, countryCode, zipCode, dayPhone, dayTimeCall, eveningPhone, fax, email, participantCount, fundRaisingGoal, decisionDate, decisionMaker, fundRaiserStartDate, onemaillist, comments, dayPhoneExt, eveningPhoneExt, otherPhone, cookieContent, groupWebSite, otherPhoneExt, isnew, short.MinValue) { }
		public TempLead(int tempLeadId, int divisionId, int promotionId, string channelCode, int leadStatusId, short organizationTypeId, short campaignReasonId, short webSiteId, short groupTypeId, string salutation, short titleId, short hearId, DateTime leadEntryDate, string firstName, string lastName, string organization, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string dayPhone, string dayTimeCall, string eveningPhone, string fax, string email, int participantCount, int fundRaisingGoal, DateTime decisionDate, short decisionMaker, DateTime fundRaiserStartDate, short onemaillist, string comments, string dayPhoneExt, string eveningPhoneExt, string otherPhone, string cookieContent, string groupWebSite, string otherPhoneExt, short isnew, short optInSweepstakes) {
			this.tempLeadId = tempLeadId;
			this.divisionId = divisionId;
			this.promotionId = promotionId;
			this.channelCode = channelCode;
			this.leadStatusId = leadStatusId;
			this.organizationTypeId = organizationTypeId;
			this.campaignReasonId = campaignReasonId;
			this.webSiteId = webSiteId;
			this.groupTypeId = groupTypeId;
			this.salutation = salutation;
			this.titleId = titleId;
			this.hearId = hearId;
			this.leadEntryDate = leadEntryDate;
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
			this.participantCount = participantCount;
			this.fundRaisingGoal = fundRaisingGoal;
			this.decisionDate = decisionDate;
			this.decisionMaker = decisionMaker;
			this.fundRaiserStartDate = fundRaiserStartDate;
			this.onemaillist = onemaillist;
			this.comments = comments;
			this.dayPhoneExt = dayPhoneExt;
			this.eveningPhoneExt = eveningPhoneExt;
			this.otherPhone = otherPhone;
			this.cookieContent = cookieContent;
			this.groupWebSite = groupWebSite;
			this.otherPhoneExt = otherPhoneExt;
			this.isnew = isnew;
			this.optInSweepstakes = optInSweepstakes;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TempLead>\r\n" +
			"	<TempLeadId>" + tempLeadId + "</TempLeadId>\r\n" +
			"	<DivisionId>" + divisionId + "</DivisionId>\r\n" +
			"	<PromotionId>" + promotionId + "</PromotionId>\r\n" +
			"	<ChannelCode>" + System.Web.HttpUtility.HtmlEncode(channelCode) + "</ChannelCode>\r\n" +
			"	<LeadStatusId>" + leadStatusId + "</LeadStatusId>\r\n" +
			"	<OrganizationTypeId>" + organizationTypeId + "</OrganizationTypeId>\r\n" +
			"	<CampaignReasonId>" + campaignReasonId + "</CampaignReasonId>\r\n" +
			"	<WebSiteId>" + webSiteId + "</WebSiteId>\r\n" +
			"	<GroupTypeId>" + groupTypeId + "</GroupTypeId>\r\n" +
			"	<Salutation>" + System.Web.HttpUtility.HtmlEncode(salutation) + "</Salutation>\r\n" +
			"	<TitleId>" + titleId + "</TitleId>\r\n" +
			"	<HearId>" + hearId + "</HearId>\r\n" +
			"	<LeadEntryDate>" + leadEntryDate + "</LeadEntryDate>\r\n" +
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
			"	<ParticipantCount>" + participantCount + "</ParticipantCount>\r\n" +
			"	<FundRaisingGoal>" + fundRaisingGoal + "</FundRaisingGoal>\r\n" +
			"	<DecisionDate>" + decisionDate + "</DecisionDate>\r\n" +
			"	<DecisionMaker>" + decisionMaker + "</DecisionMaker>\r\n" +
			"	<FundRaiserStartDate>" + fundRaiserStartDate + "</FundRaiserStartDate>\r\n" +
			"	<Onemaillist>" + onemaillist + "</Onemaillist>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"	<DayPhoneExt>" + System.Web.HttpUtility.HtmlEncode(dayPhoneExt) + "</DayPhoneExt>\r\n" +
			"	<EveningPhoneExt>" + System.Web.HttpUtility.HtmlEncode(eveningPhoneExt) + "</EveningPhoneExt>\r\n" +
			"	<OtherPhone>" + System.Web.HttpUtility.HtmlEncode(otherPhone) + "</OtherPhone>\r\n" +
			"	<CookieContent>" + System.Web.HttpUtility.HtmlEncode(cookieContent) + "</CookieContent>\r\n" +
			"	<GroupWebSite>" + System.Web.HttpUtility.HtmlEncode(groupWebSite) + "</GroupWebSite>\r\n" +
			"	<OtherPhoneExt>" + System.Web.HttpUtility.HtmlEncode(otherPhoneExt) + "</OtherPhoneExt>\r\n" +
			"	<Isnew>" + isnew + "</Isnew>\r\n" +
			"	<OptInSweepstakes>" + optInSweepstakes + "</OptInSweepstakes>\r\n" +
			"</TempLead>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "tempLeadId") {
					SetXmlValue(ref tempLeadId, node.InnerText);
				}
				if(node.Name.ToLower() == "divisionId") {
					SetXmlValue(ref divisionId, node.InnerText);
				}
				if(node.Name.ToLower() == "promotionId") {
					SetXmlValue(ref promotionId, node.InnerText);
				}
				if(node.Name.ToLower() == "channelCode") {
					SetXmlValue(ref channelCode, node.InnerText);
				}
				if(node.Name.ToLower() == "leadStatusId") {
					SetXmlValue(ref leadStatusId, node.InnerText);
				}
				if(node.Name.ToLower() == "organizationTypeId") {
					SetXmlValue(ref organizationTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "campaignReasonId") {
					SetXmlValue(ref campaignReasonId, node.InnerText);
				}
				if(node.Name.ToLower() == "webSiteId") {
					SetXmlValue(ref webSiteId, node.InnerText);
				}
				if(node.Name.ToLower() == "groupTypeId") {
					SetXmlValue(ref groupTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "salutation") {
					SetXmlValue(ref salutation, node.InnerText);
				}
				if(node.Name.ToLower() == "titleId") {
					SetXmlValue(ref titleId, node.InnerText);
				}
				if(node.Name.ToLower() == "hearId") {
					SetXmlValue(ref hearId, node.InnerText);
				}
				if(node.Name.ToLower() == "leadEntryDate") {
					SetXmlValue(ref leadEntryDate, node.InnerText);
				}
				if(node.Name.ToLower() == "firstName") {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(node.Name.ToLower() == "lastName") {
					SetXmlValue(ref lastName, node.InnerText);
				}
				if(node.Name.ToLower() == "organization") {
					SetXmlValue(ref organization, node.InnerText);
				}
				if(node.Name.ToLower() == "streetAddress") {
					SetXmlValue(ref streetAddress, node.InnerText);
				}
				if(node.Name.ToLower() == "city") {
					SetXmlValue(ref city, node.InnerText);
				}
				if(node.Name.ToLower() == "stateCode") {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(node.Name.ToLower() == "countryCode") {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(node.Name.ToLower() == "zipCode") {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(node.Name.ToLower() == "dayPhone") {
					SetXmlValue(ref dayPhone, node.InnerText);
				}
				if(node.Name.ToLower() == "dayTimeCall") {
					SetXmlValue(ref dayTimeCall, node.InnerText);
				}
				if(node.Name.ToLower() == "eveningPhone") {
					SetXmlValue(ref eveningPhone, node.InnerText);
				}
				if(node.Name.ToLower() == "fax") {
					SetXmlValue(ref fax, node.InnerText);
				}
				if(node.Name.ToLower() == "email") {
					SetXmlValue(ref email, node.InnerText);
				}
				if(node.Name.ToLower() == "participantCount") {
					SetXmlValue(ref participantCount, node.InnerText);
				}
				if(node.Name.ToLower() == "fundRaisingGoal") {
					SetXmlValue(ref fundRaisingGoal, node.InnerText);
				}
				if(node.Name.ToLower() == "decisionDate") {
					SetXmlValue(ref decisionDate, node.InnerText);
				}
				if(node.Name.ToLower() == "decisionMaker") {
					SetXmlValue(ref decisionMaker, node.InnerText);
				}
				if(node.Name.ToLower() == "fundRaiserStartDate") {
					SetXmlValue(ref fundRaiserStartDate, node.InnerText);
				}
				if(node.Name.ToLower() == "onemaillist") {
					SetXmlValue(ref onemaillist, node.InnerText);
				}
				if(node.Name.ToLower() == "comments") {
					SetXmlValue(ref comments, node.InnerText);
				}
				if(node.Name.ToLower() == "dayPhoneExt") {
					SetXmlValue(ref dayPhoneExt, node.InnerText);
				}
				if(node.Name.ToLower() == "eveningPhoneExt") {
					SetXmlValue(ref eveningPhoneExt, node.InnerText);
				}
				if(node.Name.ToLower() == "otherPhone") {
					SetXmlValue(ref otherPhone, node.InnerText);
				}
				if(node.Name.ToLower() == "cookieContent") {
					SetXmlValue(ref cookieContent, node.InnerText);
				}
				if(node.Name.ToLower() == "groupWebSite") {
					SetXmlValue(ref groupWebSite, node.InnerText);
				}
				if(node.Name.ToLower() == "otherPhoneExt") {
					SetXmlValue(ref otherPhoneExt, node.InnerText);
				}
				if(node.Name.ToLower() == "isnew") {
					SetXmlValue(ref isnew, node.InnerText);
				}
				if(node.Name.ToLower() == "optInSweepstakes") {
					SetXmlValue(ref optInSweepstakes, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static TempLead[] GetTempLeads() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetTempLeads();
		}

		public static TempLead GetTempLeadByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetTempLeadByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertTempLead(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateTempLead(this);
		}
		#endregion

		#region Properties
		public int TempLeadId {
			set { tempLeadId = value; }
			get { return tempLeadId; }
		}

		public int DivisionId {
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

		public int LeadStatusId {
			set { leadStatusId = value; }
			get { return leadStatusId; }
		}

		public short OrganizationTypeId {
			set { organizationTypeId = value; }
			get { return organizationTypeId; }
		}

		public short CampaignReasonId {
			set { campaignReasonId = value; }
			get { return campaignReasonId; }
		}

		public short WebSiteId {
			set { webSiteId = value; }
			get { return webSiteId; }
		}

		public short GroupTypeId {
			set { groupTypeId = value; }
			get { return groupTypeId; }
		}

		public string Salutation {
			set { salutation = value; }
			get { return salutation; }
		}

		public short TitleId {
			set { titleId = value; }
			get { return titleId; }
		}

		public short HearId {
			set { hearId = value; }
			get { return hearId; }
		}

		public DateTime LeadEntryDate {
			set { leadEntryDate = value; }
			get { return leadEntryDate; }
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

		public short DecisionMaker {
			set { decisionMaker = value; }
			get { return decisionMaker; }
		}

		public DateTime FundRaiserStartDate {
			set { fundRaiserStartDate = value; }
			get { return fundRaiserStartDate; }
		}

		public short Onemaillist {
			set { onemaillist = value; }
			get { return onemaillist; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
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

		public string CookieContent {
			set { cookieContent = value; }
			get { return cookieContent; }
		}

		public string GroupWebSite {
			set { groupWebSite = value; }
			get { return groupWebSite; }
		}

		public string OtherPhoneExt {
			set { otherPhoneExt = value; }
			get { return otherPhoneExt; }
		}

		public short Isnew {
			set { isnew = value; }
			get { return isnew; }
		}

		public short OptInSweepstakes {
			set { optInSweepstakes = value; }
			get { return optInSweepstakes; }
		}

		#endregion
	}
}
