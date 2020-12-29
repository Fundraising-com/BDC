using System;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.XFactor
{	
	
	using System.Text.RegularExpressions;
	using GA.BDC.Core.ESubsGlobal.Payment;
	using MyESubsGlobal=GA.BDC.Core.ESubsGlobal;
	using GA.BDC.Core.ESubsGlobal.Common;
	using GA.BDC.Core.ESubsGlobal.Users;
	/// <summary>
	/// Summary description for PartnershipService.
	/// </summary>
	public class PartnershipService: PartnershipBaseService
	{
		private const int DefaultNoOfGroupMembers = 15;
		public PartnershipService()
		{
			
		}

		// get the countries
		private CountryCollections _countryColl = null;
		private CountryCollections countryColl
		{
			get
			{
				if (_countryColl == null)
					_countryColl = CountryCollections.Create ();
				return _countryColl;
			}
		}

		#region Validate Method

		virtual protected string ValidateRedirect(string groupUrl)
		{
            if (string.IsNullOrEmpty(groupUrl))
                return string.Empty;

			string redirect = groupUrl.Trim ();

			GA.BDC.Core.ESubsGlobal.Event theEvent = GA.BDC.Core.ESubsGlobal.Event.GetEventByGroupRedirect(redirect);
			int i = 0;
			while (theEvent != null && i < 100000)
			{
				i++;
				if (i < 100000)
					redirect = groupUrl.Trim () + i.ToString();
				theEvent = GA.BDC.Core.ESubsGlobal.Event.GetEventByGroupRedirect(redirect);
			}
			return redirect;
		}

		virtual protected int ValidateNumberOfGroupMembers(int grpNumber)
		{
			int noGroup = grpNumber;
			if (grpNumber < 1) noGroup = DefaultNoOfGroupMembers;
			return noGroup;
		}
				
		virtual protected string ValidateCountry(string sValue)
		{
            if (sValue.ToLower() == "canada")
                sValue = "CA";
			string result = "US";
			if (!IsValidString (sValue))
				return result;
			for(int i=0;i<countryColl.Count;i++) 
			{
				if (sValue.ToUpper() == ((string)countryColl[i].GetCountryCode).ToUpper () )
					return (string)countryColl[i].GetCountryCode;
			}
			return result;
		}

		
		virtual protected string ValidateState(string sValue, string Country)
		{
            string result = "US-NY";

            // Fix, LLU sends as subdivision code for Nefoundland CA-NF, as opposed to CA-NL
            if (sValue == "CA-NF")
                sValue = "CA-NL";

			if (!IsValidString (sValue))
				return result;
			SubDivisionCollections subDivColl = countryColl[Country].GetSubDivision;
			for(int i=0;i<subDivColl.Count;i++) 
			{
				if (sValue.ToUpper () == ((string)subDivColl[i].GetSubDivisionCode.Code).ToUpper() )
					return (string)subDivColl[i].GetSubDivisionCode.Code;
			}
			return result;
		}

		virtual protected bool IsValidPhoneNumber(string sValue)
		{
			return true;
		}

		virtual protected string ValidatePassword(string sValue)
		{
			if (!IsValidString (sValue))
			{
				GA.BDC.Core.Utilities.PasswordGenerator.PasswordGenerator passGenerator = new GA.BDC.Core.Utilities.PasswordGenerator.PasswordGenerator ();
				return passGenerator.Generate ();
			}
			return sValue;
		}

		private OptInStatus ValidateOptionStatus(int optInStatus)
		{
			return (optInStatus == 0 ? OptInStatus.OPTOUT : OptInStatus.OPTIN);
		}

		#endregion

		#region Sponsor


		private PaymentInfo CreatePaymentInfo(ESubsGlobal.Group grp,
			string address ,
			string city ,
			string country ,
			string name ,
			string phoneNumberStr ,
			string state ,
			string zip
			)
		{
			PaymentInfo paymentInfo = null;
			
            ESubsGlobal.Common.PhoneNumber phoneNumber = new ESubsGlobal.Common.PhoneNumber();
            phoneNumber.SetPhoneNumber(string.IsNullOrEmpty(phoneNumberStr) ? string.Empty : phoneNumberStr);
            phoneNumber.IsActive = true;
            phoneNumber.PhoneNumberTypeID = ESubsGlobal.Common.PhoneNumberType.DAY_PHONE;
			
            // Create the PostalAddress (always recreate these information)
            ESubsGlobal.Common.PostalAddress postalAddress = new ESubsGlobal.Common.PostalAddress();
            postalAddress.Active = true;
            postalAddress.Address1 = address;
            postalAddress.City = city;
            postalAddress.PostalAddressTypeID = ESubsGlobal.Common.PostalAddressType.HOME_ADDRESS;
            postalAddress.CountryCode = ESubsGlobal.Common.CountryCode.Create(country.Trim());
            postalAddress.SubDivisionCode = state;
            postalAddress.ZipCode = zip;
            paymentInfo = new ESubsGlobal.Payment.PaymentInfo();

			if (grp != null)
				paymentInfo.GroupID = grp.GroupID;
            if (phoneNumber != null)
			    paymentInfo.PhoneNumber  = phoneNumber;
			paymentInfo.PostalAddress = postalAddress;
			paymentInfo.PaymentName = name;
			paymentInfo.OnBehalfOfName = null;
			paymentInfo.ShipToName = null;
			paymentInfo.Ssn = null;
			paymentInfo.Active = true;
			paymentInfo.CreateDate = DateTime.Now;
			//
			return paymentInfo;
		}


		override public ErrorCodes CreateGroup(
			string externalGroupID,  // mandatory
			int partnerID,   // mandatory
			string sponsorFirstName,  // mandatory
			string sponsorLastName,  // mandatory
			string email,    // mandatory
			string password,  // optional (autogenerated)
			int optInStatus,   // optional (Opt-In by default)
			string groupName,   // mandatory
			int numberOfGroupMembers,  // optional (15 by default)
			string groupUrl,   // optional (will be based on group name)
			string paymentFirstName,  // optional (default value sponsor name) 
			string paymentLastName,  // optional
			string paymentAddress,   // mandatory
			string paymentCity,   // mandatory
			string paymentState,   // mandatory
			string paymentZip,   // mandatory
			string paymentCountry,  // mandatory
			string paymentPhone,   // optional
			string groupCulture,  // optional (en-US by default)			
			BaseServiceStatus serviceStatus
			)
		{
			
			if (serviceStatus == null)
				serviceStatus = new ServiceStatus ();

			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder ();
			// Validate input values
			if (!IsValidString(externalGroupID))
			{
				strBuilder.Append("Invalid external group ID\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}
			if (partnerID < -1)
			{
				strBuilder.Append("Invalid partner ID\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}
			if (!IsValidString(sponsorFirstName))
			{
				strBuilder.Append("Sponsor first name can not be empty\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}
        
            if (!IsValidString(sponsorLastName))
            {
                strBuilder.Append("Sponsor last name can not be empty\n");
                serviceStatus.errorMessage = strBuilder.ToString();
                return ErrorCodes.InvalidParameter;
            }
            if (!Regex.Match(email, @"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]{1,6}$", RegexOptions.Compiled).Success)
            {
                strBuilder.Append("Invalid sponsor email address\n");
                serviceStatus.errorMessage = strBuilder.ToString();
                return ErrorCodes.InvalidParameter;
            }
            if (!IsValidString(groupName))
            {
                strBuilder.Append("Group Name can not be empty\n");
                serviceStatus.errorMessage = strBuilder.ToString();
                return ErrorCodes.InvalidParameter;
            }

			if (!IsValidString (paymentAddress))
			{
				strBuilder.Append("Payment Address can not be empty\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}

			if (!IsValidString (paymentCity))
			{
				strBuilder.Append("Payment City can not be empty\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}

			if (!IsValidString (paymentZip))
			{
				strBuilder.Append("Payment Zip can not be empty\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}

            //if (!IsValidString (paymentPhone))
            //{
            //    strBuilder.Append("Payment Phone can not be empty\n");
            //    serviceStatus.errorMessage = strBuilder.ToString ();
            //    return ErrorCodes.InvalidParameter;
            //}

			if (serviceStatus.IsTestUser)
				return ErrorCodes.Successful;

			// Create an environment.
			eSubsGlobalEnvironment env = eSubsGlobalEnvironment.Create ();
			if(env == null) {
				env = new eSubsGlobalEnvironment();
			}
			
			try
			{
				env.Partner = new Partner ();
				Partner partner = env.Partner ;
				if (partnerID == -1)
					partner.PartnerID = int.MinValue;
				else
					partner.PartnerID = partnerID;                

				// Create the culture.
				string cultureCode = (!IsValidString(groupCulture) ? "en-US" : groupCulture);
				Culture esubCul = Culture.Create (groupCulture);

                // Sanitize input
                if (cultureCode == "en-US")
                {
                    if (paymentCountry.TrimStart().TrimEnd().ToUpper() != "US")
                        paymentCountry = "US";
                    if (!paymentState.TrimStart().StartsWith("US-", StringComparison.CurrentCultureIgnoreCase))
                        paymentState = "US-" + paymentState;
                }
                else if (cultureCode == "en-CA")
                {
                    if (paymentCountry.TrimStart().TrimEnd().ToUpper() != "CA")
                        paymentCountry = "CA";
                    if (!paymentState.TrimStart().StartsWith("CA-", StringComparison.CurrentCultureIgnoreCase))
                        paymentState = "CA-" + paymentState;
                }

                string country = ValidateCountry(paymentCountry.TrimStart().TrimEnd());
                string state = ValidateState(paymentState.TrimStart().TrimEnd(), country);

				// Verify if the group is existing.
				MyESubsGlobal.Group grp = MyESubsGlobal.Group.LoadByExternalGroupID(partner.PartnerID, externalGroupID);
				if (grp != null)
				{
					serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage (ErrorCodes.GroupExisted);
					return ErrorCodes.GroupExisted;
				}

                // Create sponsor
                ESubsGlobal.Users.Sponsor sponsor = null;
                sponsor = new ESubsGlobal.Users.Sponsor(sponsorFirstName + " " + sponsorLastName,
                    email,
                    ValidatePassword(password), "",
                    ESubsGlobal.Users.CreationChannel.SPONSOR_CREATED_FROM_ROSTER_21,
                    esubCul, null, null, null, partner.PartnerID);
                sponsor.OptInStatusID = ValidateOptionStatus(optInStatus);
                // Create group (using constructor 11th).			
                grp = new MyESubsGlobal.Group(int.MinValue, groupName, int.MinValue, sponsor.HierarchyID, int.MinValue, int.MinValue,
                    externalGroupID, !ESubsGlobal.DataAccess.AppConfig.IsProduction, ValidateNumberOfGroupMembers(numberOfGroupMembers), null, null);
                grp.PartnerID = partner.PartnerID;
                // if groupUrl is empty use the Group Name as group URL.
                string theGrpURL = groupUrl;
                if (!IsValidString(groupUrl))
                    theGrpURL = groupName;
                // Verify if Group URL already existing ?
                theGrpURL = ValidateRedirect(theGrpURL.Trim().Replace(" ", "_"));
                // Create the event
                Event ev = null;
                ev = new Event(int.MinValue, Event.EventStatusInfo.FUNDRAISING_CAMPAIGN, groupName, grp.GroupID, sponsor.Culture.CultureCode,
                    DateTime.Now, DateTime.MinValue, !ESubsGlobal.DataAccess.AppConfig.IsProduction, null, DateTime.Now,
                    partner.PartnerID,int.MinValue, false, theGrpURL);

                // Get current partner profit from EFRCommon
                GA.BDC.Core.eFundraisingCommon.PartnerProfit pp = GA.BDC.Core.eFundraisingCommon.PartnerProfit.GetCurrentPartnerProfitByID(partner.PartnerID);
                if (pp != null)
                {
                    List<GA.BDC.Core.eFundraisingCommon.Profit> profits = GA.BDC.Core.eFundraisingCommon.Profit.GetProfitByProfitGroupID(pp.ProfitGroupID);
                    if (profits != null)
                    {
                        GA.BDC.Core.eFundraisingCommon.Profit currentProfit = null;
                        foreach (GA.BDC.Core.eFundraisingCommon.Profit p in profits)
                        {
                            if (p.QspCatalogTypeID == int.MinValue)
                                currentProfit = p;
                        }
                        if (currentProfit != null)
                        {
                            ev.ProfitGroupID = currentProfit.ProfitGroupID;
                            ev.ProfitCalculated = currentProfit.ProfitPercentage;
                        }
                    }
                }                

				PaymentInfo pi = null;
				// Create payment info for sponsor. 
				string paymentFullName = string.Empty;
				if (!IsValidString (paymentFirstName) && !IsValidString(paymentLastName))
					paymentFullName = sponsor.CompleteName;
				else
					paymentFullName = paymentFirstName + " " + paymentLastName;
				pi = CreatePaymentInfo(grp, paymentAddress, paymentCity, country, paymentFullName, paymentPhone, state, paymentZip);
				//

				// declare the object that contains the insert group/event/payment
				// processes
				// use the transaction controller to insert the campaign
				TransactionController grc =	new TransactionController(env);
				GroupEventResponse groupEventResponse = grc.InsertGroupEvent(sponsor, grp, ev, pi);

                int i = 0;
                while ((groupEventResponse.status == GroupEventStatus.RedirectAlreadyExists) && i < 2)
                {
                    grc = null;
                    ev.Redirect = ValidateRedirect(ev.Redirect);
                    grc = new TransactionController(env);
                    groupEventResponse = grc.InsertGroupEvent(sponsor, grp, ev, pi);
                    i++;
                    grc = null;
                }
                switch (groupEventResponse.status)
                {
                    case GroupEventStatus.Ok:
                        EventParticipation evp = null;
                        // Create Event Participant
                        evp = new EventParticipation(ev, sponsor, ParticipationChannel.SponsorCreated);
                        evp.Salutation = sponsor.CompleteName;
                        // Create default personalization
                        evp.EventParticipationID = groupEventResponse.eventParticipationID;
                        Personalization personal = null;
                        personal = Personalization.CreateDefaultSponsorPersonalization(evp, partner,
                            esubCul, grp.Name, sponsor.CompleteName);
                        if (personal != null)
                        {
                            personal.InsertIntoDatabase();

                            if (personal.PersonalizationId != int.MinValue)
                            {
                                // Create default personalization image
                                PersonalizationImage persImg = new PersonalizationImage();
                                persImg.ImageUrl = personal.ImageUrl;
                                persImg.Deleted = (byte)0;
                                persImg.ImageApprovalStatusId = 3;
                                persImg.IsCoverAlbum = (byte)1;
                                persImg.PersonalizationID = personal.PersonalizationId;
                                persImg.InsertIntoDatabase();
                            }
                        }
							
						env.Clear();
                        env = eSubsGlobalEnvironment.Create();
						// reload and save the environment with the new information
						ESubsGlobal.eSubsGlobalEnvironmentParameters param =
							new ESubsGlobal.eSubsGlobalEnvironmentParameters();
						param.EventParticipationID = evp.EventParticipationID;
						env.Load(param);
						env.Save();

						SetUser(sponsor);
						SetESubsGlobalEnvironment(env);
						return ErrorCodes.Successful;
					case GroupEventStatus.ExternalOrganizationIDAlreadyExists:
						serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage(ErrorCodes.ExternalGroupIDExisted); 
						return ErrorCodes.ExternalGroupIDExisted;
					case GroupEventStatus.RedirectAlreadyExists:
						serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage(ErrorCodes.GroupExisted); 
						return ErrorCodes.GroupExisted;
					default:
						serviceStatus.errorMessage = string.Format (
						"Unknown error in creation group when inserting a new group from external ID: {0}, partner ID: {1}.", externalGroupID, partnerID.ToString ());
						return ErrorCodes.Unknown;
				}
			}
			catch (Exception ex)
			{
				serviceStatus.errorMessage = ex.ToString ();
				throw new ESubsGlobalException(ex.Message, ex, env);
			}

		}


		override public ErrorCodes UpdateGroup(string externalGroupID, 
			int partnerID, 
			string sponsorFirstName, 
			string sponsorLastName, 
			string email, 
			string password, 
			int optInStatus, 
			string groupName, 
			int numberOfGroupMembers, 
			string groupUrl, 
			string paymentFirstName,
			string paymentLastName,
			string paymentAddress, 
			string paymentCity, 
			string paymentState, 
			string paymentZip, 
			string paymentCountry,   
			string paymentPhone, 
			string groupCulture,					
			BaseServiceStatus serviceStatus
			)
		{
			if (serviceStatus == null)
				serviceStatus = new ServiceStatus ();

			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder ();
			// Validate input values
			if (!IsValidString(externalGroupID))
			{
				strBuilder.Append("Invalid external group ID\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}
			if (partnerID < 1)
			{
				strBuilder.Append("Invalid partner ID\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}

			// Verify if the group is existing.
			MyESubsGlobal.Group grp = MyESubsGlobal.Group.LoadByExternalGroupID(partnerID, externalGroupID);
			if (grp == null)
			{
                // UPDATE DECEMBER 28, 2010: If group does not exist, create it
                ErrorCodes result = CreateGroup(externalGroupID, partnerID, sponsorFirstName, sponsorLastName, email, password, optInStatus, groupName,
                                                numberOfGroupMembers, groupUrl, paymentFirstName, paymentLastName, paymentAddress, paymentCity, paymentState,
                                                paymentZip, paymentCountry, paymentPhone, groupCulture, serviceStatus);
                if (result != 0)
                {
                    serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage(result);
                    return result;
                }

                grp = MyESubsGlobal.Group.LoadByExternalGroupID(partnerID, externalGroupID);
                if (grp == null)
                {
                    serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage(ErrorCodes.GroupNotExisted);
                    return ErrorCodes.GroupNotExisted;
                }
                else
                {
                    serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage(ErrorCodes.Successful);
                    return ErrorCodes.Successful;
                }
			}

			if (serviceStatus.IsTestUser)
				return ErrorCodes.Successful;
	
			// Load an environment.
			eSubsGlobalEnvironmentParameters param = new eSubsGlobalEnvironmentParameters ();
			param.PartnerID = partnerID;
			param.ExternalGroupID = externalGroupID;
			eSubsGlobalEnvironment env = null;
			try
			{
				env = eSubsGlobalEnvironment.Create (param, true);
			}
			catch (Exception ex)
			{
				NullReferenceException nullExcep = ex as NullReferenceException;
				if (nullExcep != null)
				{
					serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage(ErrorCodes.InvalidExternalGroupID);					
					return ErrorCodes.InvalidExternalGroupID;
				}
				else
				{
					serviceStatus.errorMessage = ex.ToString ();
					return ErrorCodes.Unknown;
				}
			}

			if (env == null)
			{
				serviceStatus.errorMessage = string.Format ("Can not load environment whose externalID: {0}, partner ID: {1}.", externalGroupID, partnerID.ToString ());
				return ErrorCodes.Others;
			}

            if (env == null)
            {
                serviceStatus.errorMessage = string.Format("Can not load environment whose externalID: {0}, partner ID: {1}.", externalGroupID, partnerID.ToString());
                return ErrorCodes.Others;
            }

            try
            {
                // Create the culture.
                string cultureCode = (!IsValidString(groupCulture) ? "en-US" : groupCulture);
                Culture esubCul = Culture.Create(groupCulture);

                // Sanitize input
                if (cultureCode == "en-US")
                {
                    if (paymentCountry.TrimStart().TrimEnd().ToUpper() != "US")
                        paymentCountry = "US";
                    if (!paymentState.TrimStart().StartsWith("US-", StringComparison.CurrentCultureIgnoreCase))
                        paymentState = "US-" + paymentState;
                }
                else if (cultureCode == "en-CA")
                {
                    if (paymentCountry.TrimStart().TrimEnd().ToUpper() != "CA")
                        paymentCountry = "CA";
                    if (!paymentState.TrimStart().StartsWith("CA-", StringComparison.CurrentCultureIgnoreCase))
                        paymentState = "CA-" + paymentState;
                }

                string country = ValidateCountry(paymentCountry.TrimStart().TrimEnd());
                string state = ValidateState(paymentState.TrimStart().TrimEnd(), country);				

				// Start update sponsor information
				eSubsGlobalUser user = eSubsGlobalUser.LoadByHierarchyID(env.Group.SponsorID);
				
				// If sponsorname is invalid there is no update for sponsorname.
				if (!IsValidString(sponsorFirstName))
				{
					sponsorFirstName = user.FirstName;
				}
				if (!IsValidString(sponsorLastName))
				{
					sponsorLastName = user.LastName;
				}
				user.ConvertName(sponsorFirstName  + " " + sponsorLastName);

				// Update only valid email address.
				if (IsValidString(email) && Regex.Match (email, @"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]{1,6}$",RegexOptions.Compiled).Success)
				{
					user.EmailAddress = GA.BDC.Core.EnterpriseStandards.EmailAddress.CreateEmailAddress(email);
				}

				user.OptInStatusID = ValidateOptionStatus(optInStatus);
				if (IsValidString (password))
					user.Password = password;
				InsertMemberIntoDatabaseReturnValue updateStatus = user.UpdateInDatabase();

                if (IsValidString(password))
                {
                    updateStatus = user.UpdateUserInDatabase();
                }
				// Update only valid group name.
				if (IsValidString (groupName))
				{
					env.Event.Name = groupName;
				}

                if (!string.IsNullOrEmpty(groupUrl) && groupUrl.Trim() == "''")
                {
                    groupUrl = null;
                }

                // if groupUrl is empty use the Group Name as group URL, only if redirect does not yet exist.
                bool modifyRedirect = false;
                if (string.IsNullOrEmpty(env.Event.Redirect))
                    modifyRedirect = true;
                else if (!string.IsNullOrEmpty(groupUrl) && env.Event.Redirect.ToUpper().Trim() != groupUrl.ToUpper().Trim())
                    modifyRedirect = true; 

				env.Group.ExpectedMembership = ValidateNumberOfGroupMembers(numberOfGroupMembers);

                if (modifyRedirect)
                {
                    string theGrpURL = groupUrl;
                    if (!IsValidString(groupUrl))
                        theGrpURL = groupName;

                    // Verify if Group URL already existing ?
                    env.Event.Redirect = ValidateRedirect(theGrpURL.Trim().Replace(" ", "_"));
                }

				// update the group information in datasource
				InsertGroupIntoDatabaseStatus groupUpdateStatus = env.Group.UpdateInDatabase();

				PaymentInfo pi = null;
				// Create payment info for sponsor. 			
				pi = PaymentInfo.LoadPaymentInfoBySponsorID (env.Group.SponsorID );
				//pi.PostalAddress.Active = true;
				// Validate Payment Info. 
				string paymentFullName = string.Empty;
				if (!IsValidString (paymentFirstName) && !IsValidString(paymentLastName))
					paymentFullName = sponsorFirstName + " " + sponsorLastName ;
				else
					paymentFullName = paymentFirstName + " " + paymentLastName;

                if (pi != null)
                {
                    // Payment Name.			
                    pi.PaymentName = paymentFullName;

                    if (pi.PostalAddress != null)
                    {
                        // Update only valid paymentAddress.
                        if (IsValidString(paymentAddress))
                        {
                            pi.PostalAddress.Address1 = paymentAddress;
                        }
                        // Update only valid city.
                        if (IsValidString(paymentCity))
                        {
                            pi.PostalAddress.City = paymentCity;
                        }
                        pi.PostalAddress.PostalAddressTypeID = ESubsGlobal.Common.PostalAddressType.HOME_ADDRESS;
                        pi.PostalAddress.CountryCode = ESubsGlobal.Common.CountryCode.Create(country.Trim());
                        pi.PostalAddress.SubDivisionCode = state;
                        // Update only valid Zip.
                        if (IsValidString(paymentZip))
                        {
                            pi.PostalAddress.ZipCode = paymentZip;
                        }                   
                    }
                    else
                    {
                        // Create the PostalAddress (always recreate these information)
                        ESubsGlobal.Common.PostalAddress postalAddress = new ESubsGlobal.Common.PostalAddress();
                        postalAddress.Active = true;
                        if (IsValidString(paymentAddress))
                        {
                            postalAddress.Address1 = paymentAddress;
                        }
                        if (IsValidString(paymentCity))
                        {
                            postalAddress.City = paymentCity;
                        }
                        postalAddress.PostalAddressTypeID = ESubsGlobal.Common.PostalAddressType.HOME_ADDRESS;
                        postalAddress.CountryCode = ESubsGlobal.Common.CountryCode.Create(country.Trim());
                        postalAddress.SubDivisionCode = state;
                        if (IsValidString(paymentZip))
                        {
                            postalAddress.ZipCode = paymentZip;
                        }

                        pi.PostalAddress = postalAddress;
                    }

                    if (pi.PhoneNumber != null)
                    {
                        // Payment Phone.
                        if (IsValidString(paymentPhone))
                        {
                            // Payment Phone.
                            pi.PhoneNumber.SetPhoneNumber(paymentPhone);
                        }
                        else
                        {
                            pi.PhoneNumber.SetPhoneNumber(" ");
                        }
                    }
                    else
                    {
                        // create the payment phone (always recreate these information)
                        ESubsGlobal.Common.PhoneNumber phoneNumber = new ESubsGlobal.Common.PhoneNumber();
                        if (IsValidString(paymentPhone))
                        {
                            // Payment Phone.
                            phoneNumber.SetPhoneNumber(paymentPhone);
                        }
                        else
                        {
                            phoneNumber.SetPhoneNumber(" ");
                        }
                        phoneNumber.IsActive = true;
                        phoneNumber.PhoneNumberTypeID = ESubsGlobal.Common.PhoneNumberType.DAY_PHONE;

                        pi.PhoneNumber = phoneNumber;
                    }

                    pi.UpdateIntoDatabase();
                }
                else
                {
                    if (!IsValidString(paymentPhone))
                    {
                        paymentPhone = " ";
                    }
                    pi = CreatePaymentInfo(grp, paymentAddress, paymentCity, country, paymentFullName, paymentPhone, state, paymentZip);
                    pi.InsertIntoDatabase(env.Event.EventID);
                }

				// Update event.		
				InsertUpdateStatus eventUpdateSatus = env.Event.UpdateInDatabase();
				switch(eventUpdateSatus) 
				{
					case InsertUpdateStatus.OK:
						SetUser(user);
						SetESubsGlobalEnvironment(env);
						return ErrorCodes.Successful;
					default:
					{
						serviceStatus.errorMessage = string.Format (
                            "Unknown error in Update group whose external ID: {0}, partner ID: {1}, sponsorFirstName: {2}, sponsorLastName: {3}, email: {4}, paymentFirstName: {5}, paymentAddress: {6}, paymentCity: {7}, paymentState: {8}, paymentZip: {9}, paymentPhone: {10}, groupUrl: {11}, groupName: {12}, password: {13}",
                                                      externalGroupID, partnerID.ToString(), sponsorFirstName, sponsorLastName, email, paymentFirstName, paymentAddress, paymentCity, paymentState, paymentZip, paymentPhone, groupUrl, groupName, password);
						return ErrorCodes.Others; 
					}
				}
			}
			catch (Exception ex)
			{
				serviceStatus.errorMessage = ex.ToString ();
				throw new ESubsGlobalException(ex.Message, ex, env);
			}
		}

		public ErrorCodes RemoveOrganizer(
			string externalGroupID,  // mandatory
			int partnerID,   // mandatory
			BaseServiceStatus serviceStatus
			)
		{

			// Load an environment.
			eSubsGlobalEnvironmentParameters param = new eSubsGlobalEnvironmentParameters ();
			param.PartnerID = param.PartnerID = partnerID;
			param.ExternalGroupID = externalGroupID;
			eSubsGlobalEnvironment env = null;
			try
			{
				env = eSubsGlobalEnvironment.Create (param, true);
			}
			catch (Exception ex)
			{
				NullReferenceException nullExcep = ex as NullReferenceException;
				if (nullExcep != null)
				{
					serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage(ErrorCodes.InvalidExternalGroupID);					
					return ErrorCodes.InvalidExternalGroupID;
				}
				else
				{
					serviceStatus.errorMessage = ex.ToString ();
					return ErrorCodes.Unknown;
				}
			}

            // Added by Jiro Hidaka (January 13, 2008)
            if (partnerID != env.Partner.PartnerID)
                partnerID = env.Partner.PartnerID;

			try
			{
				if (serviceStatus == null)
					serviceStatus = new ServiceStatus ();

				MyESubsGlobal.Group grp = MyESubsGlobal.Group.LoadByExternalGroupID(partnerID, externalGroupID);
				if (grp != null)
				{
					if (!grp.TerminateEvent())
					{
						serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage(ErrorCodes.TerminateEventFailed);
						return ErrorCodes.TerminateEventFailed;
					}
				}
				else
				{
					serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage(ErrorCodes.GroupNotExisted);
					return ErrorCodes.GroupNotExisted;
				}
			}
			catch (Exception ex)
			{
				serviceStatus.errorMessage = ex.ToString ();
				throw new ESubsGlobalException(ex.Message, ex, env);
			}
			return ErrorCodes.Successful;
		}

		#endregion

		#region Participant

		public ErrorCodes CreateTemporaryParticipant(
			string externalGroupID,// mandatory
			int partnerID,   // mandatory
			string externalMemberID,  // mandatory
			string cultureCode,   // optional (en-US by default)
			int optInStatus,   // optional (Opt-In by default)
			string firstName,   // mandatory
			string lastName,   // mandatory
			string email,    // mandatory
			string password,  // optional (autogenerated)
			string comments, //optional					
			BaseServiceStatus serviceStatus 
			)
		{
			if (serviceStatus == null)
				serviceStatus = new ServiceStatus ();
 
			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder ();
			// Validate input values
			if (!IsValidString(externalGroupID))
			{
				strBuilder.Append("Invalid external group ID\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}
			if (partnerID < 0)
			{
				strBuilder.Append("Invalid partner ID\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}
			
			if (!IsValidString(firstName))
			{
				strBuilder.Append("Participant first name can not be empty\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}

			if (!IsValidString(lastName))
			{
				strBuilder.Append("Participant last name can not be empty\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}
			if (!Regex.Match (email, @"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]{1,6}$",RegexOptions.Compiled).Success)
			{
				strBuilder.Append("Invalid email address\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}

			if (!IsValidString (externalMemberID))
			{
				serviceStatus.errorMessage = "ExternalMemberID can not be empty";
				return ErrorCodes.InvalidParameter;
			}


			// Verify if the group is existing.
			MyESubsGlobal.Group grp = MyESubsGlobal.Group.LoadByExternalGroupID( partnerID, externalGroupID);
			if (grp == null)
			{
				serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage (ErrorCodes.GroupNotExisted);
				return ErrorCodes.GroupNotExisted;
			}

			if (serviceStatus.IsTestUser)
			{
				return ErrorCodes.Successful;
			}


			password = ValidatePassword (password);
			// Insert into temporary table.
			
			string theCultureCode = (!IsValidString(cultureCode) ? "en-US" : cultureCode);
			Culture esubCul = Culture.Create (theCultureCode);
			ESubsGlobal.Users.CreationChannel cc = ESubsGlobal.Users.CreationChannel.CreateFromID ((int)CreationChannelType.PARTICIPANT_CREATED_BY_ROSTER_20);

			ESubsGlobal.Users.Participant parUser = 
				new ESubsGlobal.Users.Participant(firstName + " " + lastName, email, password, comments, cc,
					esubCul, null, null, null, partnerID);
			parUser._externalGroupID = externalGroupID;
			parUser.ExternalMemberID = externalMemberID; 
			InsertMemberIntoDatabaseReturnValue retValue = ESubsGlobal.Users.Participant.InsertIntoTemporaryTable (parUser);
			if (retValue == InsertMemberIntoDatabaseReturnValue.OK) {
				SetUser(parUser);
				SetESubsGlobalEnvironment(null);
				return ErrorCodes.Successful;
			}else {
				if (retValue == InsertMemberIntoDatabaseReturnValue.INSERT_INTO_MEMBER_FAILED)
					serviceStatus.errorMessage = "Insert into member into the temporary table failed";
				return ErrorCodes.Unknown;
			}
		}

		override public ErrorCodes CreateParticipant(
			string externalGroupID,// mandatory
			int partnerID,   // mandatory
			string externalMemberID,  // mandatory
			string cultureCode,   // optional (en-US by default)
			int optInStatus,   // optional (Opt-In by default)
			string firstName,   // mandatory
			string lastName,   // mandatory
			string email,    // mandatory
			string password,  // optional (autogenerated)
			string comments, //optional					
			BaseServiceStatus serviceStatus 
			)
		{
			if (serviceStatus == null)
				serviceStatus = new ServiceStatus ();
 
			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder ();
			// Validate input values
			if (!IsValidString(externalGroupID))
			{
				strBuilder.Append("Invalid external group ID\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}
			if (partnerID < 0)
			{
				strBuilder.Append("Invalid partner ID\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}
			
			if (!IsValidString(firstName))
			{
				strBuilder.Append("Participant first name can not be empty\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}

			if (!IsValidString(lastName))
			{
				strBuilder.Append("Participant last name can not be empty\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}
			if (!Regex.Match (email, @"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]{1,6}$",RegexOptions.Compiled).Success)
			{
				strBuilder.Append("Invalid email address\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}

			if (!IsValidString (externalMemberID))
			{
				serviceStatus.errorMessage = "ExternalMemberID can not be empty";
				return ErrorCodes.InvalidParameter;
			}
			
			if (serviceStatus.IsTestUser)
			{
				return ErrorCodes.Successful;
			}


			// Verify if the group is existing.
			MyESubsGlobal.Group grp = MyESubsGlobal.Group.LoadByExternalGroupID( partnerID, externalGroupID);
			if (grp == null)
			{
				serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage (ErrorCodes.GroupNotExisted);
				return ErrorCodes.GroupNotExisted;
			}

			password = ValidatePassword (password);


			eSubsGlobalEnvironmentParameters param = new eSubsGlobalEnvironmentParameters ();
			param.PartnerID = param.PartnerID = partnerID;
			param.ExternalGroupID = externalGroupID;
			eSubsGlobalEnvironment env = eSubsGlobalEnvironment.Create (param, true);

			if (env == null)
			{
				serviceStatus.errorMessage = "Failed to retrieve the environment";
				return ErrorCodes.Others ;
			}

			if (env.Event == null) {
				serviceStatus.errorMessage = "Failed to retrieve the event";
				return ErrorCodes.Others;
			}

			if (env.Group == null)
			{
				serviceStatus.errorMessage = "Failed to retrieve the group";
				return ErrorCodes.Others ;
			}
			eSubsGlobalUser user = eSubsGlobalUser.LoadByHierarchyID(env.Group.SponsorID);
			//
			string theCultureCode = (!IsValidString(cultureCode) ? "en-US" : cultureCode);
			Culture esubCul = Culture.Create (theCultureCode);
			ESubsGlobal.Users.CreationChannel cc = ESubsGlobal.Users.CreationChannel.CreateFromID ((int)CreationChannelType.PARTICIPANT_CREATED_BY_ROSTER_20);
			// Load external member
			ESubsGlobal.Users.eSubsGlobalUser parUser = eSubsGlobalUser.LoadByExternalMemberID (partnerID, externalMemberID ); 
			if (parUser == null) // If the external member does not exist 
				parUser = new ESubsGlobal.Users.Participant(firstName + " " + lastName, email, password, comments, cc,
							esubCul, user, null, null, env.Partner.PartnerID);


			parUser.ExternalMemberID = externalMemberID;
			parUser.OptInStatusID = ValidateOptionStatus(optInStatus);
            ESubsGlobal.Users.InsertMemberIntoDatabaseReturnValue retValue = parUser.InsertIntoDatabase(false);

			switch (retValue)
			{
				case ESubsGlobal.Users.InsertMemberIntoDatabaseReturnValue.OK:
				{
					// insert an event participation.
					EventParticipation eventParticipation = 
						new EventParticipation(env.Event, parUser, ParticipationChannel.InvitedBySponsor);
					eventParticipation.Salutation = parUser.CompleteName;
					eventParticipation.InsertIntoDatabase();
					SetUser(parUser);
					SetESubsGlobalEnvironment(env);
					return ErrorCodes.Successful;
				}
				default:
				{
					serviceStatus.errorMessage = string.Format("Failed to insert a new participant InsertMemberIntoDatabaseReturnValue: {0}", retValue.ToString ());
					return ErrorCodes.Others;
				}
			}

		}


		override public ErrorCodes UpdateParticipant(			
			string externalGroupID,// mandatory
			int partnerID,   // mandatory
			string externalMemberID,  // mandatory
			string cultureCode,// optional (en-US by default)
			int optInStatus, // optional (Opt-In by default)
			string firstName,// mandatory
			string lastName, // mandatory
			string email,    // mandatory
			string password, // optional (autogenerated)
			string comments, // optional		
			BaseServiceStatus serviceStatus
			)
		{
			if (serviceStatus == null)
				serviceStatus = new ServiceStatus ();

			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder ();
			// Validate input values
			if (!IsValidString(externalGroupID))
			{
				strBuilder.Append("Invalid external group ID\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}
			if (partnerID < 0)
			{
				strBuilder.Append("Invalid partner ID\n");
				serviceStatus.errorMessage = strBuilder.ToString ();
				return ErrorCodes.InvalidParameter;
			}
			

			
			if (!IsValidString (externalMemberID))
			{
				serviceStatus.errorMessage = "ExternalMemberID can not be empty";
				return ErrorCodes.InvalidParameter;
			}
			
			if (serviceStatus.IsTestUser)
				return ErrorCodes.Successful;


			UnknownUser participantUser = eSubsGlobalUser.LoadByExternalMemberID (partnerID, externalMemberID ); 
			if (participantUser == null)
			{
				serviceStatus.errorMessage = XFactor.ErrorMessage.GetErrorMessage(ErrorCodes.ParticipantDoesNotExists);
				return ErrorCodes.ParticipantDoesNotExists ;
			}


			if (IsValidString(firstName))
			{
				participantUser.FirstName = firstName;
			}
			else
				firstName = participantUser.FirstName;

			if (IsValidString(lastName))
			{
				participantUser.LastName = lastName;
			}
			else
			{
				lastName = participantUser.LastName;
			}

			
			if (IsValidString(email)
				&& string.Compare(email.Trim(), participantUser.Email.Trim(), true) != 0  
				&& Regex.Match (email, @"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]{1,6}$",RegexOptions.Compiled).Success)
				
			{
				 participantUser.EmailAddress = GA.BDC.Core.EnterpriseStandards.EmailAddress.CreateEmailAddress (email); 
			}
				
			
			if (IsValidString (password))
				participantUser.Password = password;

			participantUser.Comments = comments;
			participantUser.OptInStatusID = ValidateOptionStatus(optInStatus);



			ESubsGlobal.Users.InsertMemberIntoDatabaseReturnValue retValue = participantUser.UpdateInDatabase ();
			switch (retValue)
			{
				case ESubsGlobal.Users.InsertMemberIntoDatabaseReturnValue.OK:
				{
                        if (IsValidString(password))
                        {
                            retValue = participantUser.UpdateUserInDatabase();
                        }
					return ErrorCodes.Successful;
				}
				default:
				{
					serviceStatus.errorMessage = string.Format("Failed to update a participant InsertMemberIntoDatabaseReturnValue: {0}", retValue.ToString ());
					return ErrorCodes.Others;
				}
			}
		}
		#endregion

		
	}



	public class PartnershipFactory
	{
		//A simple Factory to create a service for partnership. 
		public static PartnershipService CreatePartnershipService(PartnershipNames partnerName)
		{
			switch (partnerName)
			{
				// League Line Up partner. PartnerID= 58.
				case PartnershipNames.LLUSERVICE:
					return new LLUService();
				default:
					return new PartnershipService();
			}
			
		}
	}
}
