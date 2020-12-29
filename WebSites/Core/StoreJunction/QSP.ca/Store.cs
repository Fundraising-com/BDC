using System;
using System.Collections;
using System.Data;
using System.Data.SqlTypes;
using System.Web;

using GA.BDC.Core.Database;
using GA.BDC.Core.Database.eSubs;

namespace GA.BDC.Core.StoreJunction.QSP.ca
{
	/// <summary>
	/// This class gets the url string needed to go the the http://qsp.ca store.
	/// </summary>
	/// <example>
	/// 
	/// efundraising.StoreJunction.QSP.ca.Store store = 
	///		new efundraising.StoreJunction.QSP.ca.Store();
	///	
	///	Response.Redirect(store.BuildStoreUrl("1", 1, "1", "1", "1", "1", "1", "1"));
    /// http://uspvl3k06-dev:8060/Store/SetSession.aspx?sourceid=2&supporterid=1890929&PartId=9999&LanguageId=1&U1=0&qxtrak=eMagE138%7ceMagSC1%7cVLID14589511&FAccountId=17517&SiteId=22&OrgName=elaunchCATest&SupporterName=+&extTrkID=eLaunchCA
	/// 
	/// </example>
    // Hack:
    //      This class with the US version class need re-structuring.
    //      They should have their common data and methods in a base class
    //      from which they are both derived.
    //      Different properties and methods should stay seperated in different classes.
	public class Store : GoToStore
    {
        #region Private Variables
        
        private int _eventID;
		private int _eventTypeID;
		private string _eventName;
		private DateTime _startDate;
		private DateTime _endDate;
		private int _accountNumber;
        private int _opportunityId;
		private int _storeID;
		private string _supporterName;
		private string _emailAddress;
		private int _memberHierarchyID;
		private int _aggregatorID;
		private int _storeTemplateID;
		private string _cultureCode;

		private string _emailTypeID;
		private int _supporterID;
		private string _sweepsID;
		private string _sweeps;
		private string _payLater;
		private string _bweb;
		private string _salesChannel;
		private string _visitorLogID;

		private string _parentName;
		private string _parentEmail;

		private string _catalogItemID;
		private string _magStatus;

		private bool _isUnknownSupporter;

        private string _catalogGroupID;
        private string _catalogID;

        // needed for default values
        private string sourceID = "8";
        private string partID = "9999";
        private string languageID = "1";
        private string u1 = "0";
        private string siteID = "7"; //17 for US version

        // needed for Canada version
        private string exttrkid = "eLaunchCA";
        private string urlPath;

        private int _touchID;
        private bool _payProcessingFee;

        # endregion

        public Store()
        {

        }

        public Store(string turlPath)
        {
            urlPath = turlPath;
        }

        public static Store Create(int eventParticipationID, string emailTypeID, int supporterID, string sweepsID,
            string sweeps, string payLater, string bweb,
            string salesChannel, string visitorLogID, string catalogItemID, string magStatus,
            bool isUnknownSupporter, string turlPath)
        {
            DataAccess.ESubsDatabase dbo = new DataAccess.ESubsDatabase();
            Store store = dbo.GetCanadianStore(eventParticipationID, "");
            store.SetParameters(emailTypeID, eventParticipationID, sweepsID, sweeps, payLater, bweb, salesChannel,
                visitorLogID, catalogItemID, magStatus, isUnknownSupporter, turlPath);
            return store;
        }

        public static Store Create(string Province, int eventParticipationID, string emailTypeID, int supporterID, string sweepsID,
            string sweeps, string payLater, string bweb,
            string salesChannel, string visitorLogID, string catalogItemID, string magStatus,
            bool isUnknownSupporter, string turlPath, string eventName, string supporterName, string catalogGroupID, string catalogID)
        {
            return Create(Province, eventParticipationID, emailTypeID, supporterID, sweepsID, sweeps, payLater, bweb,
                        salesChannel, visitorLogID, catalogItemID, magStatus, isUnknownSupporter, turlPath, eventName, 
                        supporterName, catalogGroupID, catalogID, int.MinValue, false);
        }

        public static Store Create(string Province, int eventParticipationID, string emailTypeID, int supporterID, string sweepsID,
            string sweeps, string payLater, string bweb,
            string salesChannel, string visitorLogID, string catalogItemID, string magStatus,
            bool isUnknownSupporter, string turlPath, string eventName, string supporterName, string catalogGroupID, string catalogID,
            int touchID, bool payProcessingFee)
        {
            DataAccess.ESubsDatabase dbo = new DataAccess.ESubsDatabase();
            Store store = dbo.GetCanadianStore(eventParticipationID, Province);
            store.SetParameters(store.AccountNumber.ToString(), emailTypeID, eventParticipationID, sweepsID, sweeps, payLater, bweb, salesChannel,
                visitorLogID, catalogItemID, magStatus, isUnknownSupporter, turlPath, eventName, supporterName, catalogGroupID, catalogID,
                touchID, payProcessingFee);
            return store;
        }

        private void SetParameters(string emailTypeID, int supporterID, string sweepsID,
            string sweeps, string payLater, string bweb,
            string salesChannel, string visitorLogID, string catalogItemID, string magStatus,
            bool isUnknownSupporter, string turlPath)
        {
            _emailTypeID = emailTypeID;
            _supporterID = supporterID;
            _sweepsID = sweepsID;
            _sweeps = sweeps;
            _payLater = payLater;
            _bweb = bweb;
            _salesChannel = salesChannel;
            _visitorLogID = visitorLogID;
            _catalogItemID = catalogItemID;
            _magStatus = magStatus;
            _isUnknownSupporter = isUnknownSupporter;
            urlPath = turlPath;
        }

        private void SetParameters(string accountID, string emailTypeID, int supporterID, string sweepsID,
            string sweeps, string payLater, string bweb,
            string salesChannel, string visitorLogID, string catalogItemID, string magStatus,
            bool isUnknownSupporter, string turlPath, string eventName, string supporterName, string catalogGroupID, string catalogID,
            int touchID, bool payProcessingFee)
        {
            _accountNumber = Int32.Parse(accountID);
            _emailTypeID = emailTypeID;
            _supporterID = supporterID;
            _sweepsID = sweepsID;
            _sweeps = sweeps;
            _payLater = payLater;
            _bweb = bweb;
            _salesChannel = salesChannel;
            _visitorLogID = visitorLogID;
            _catalogItemID = catalogItemID;
            _magStatus = magStatus;
            _isUnknownSupporter = isUnknownSupporter;
            EventName = eventName;
            SupporterName = supporterName;
            urlPath = turlPath;
            CatalogGroupID = catalogGroupID;
            CatalogID = catalogID;
            _touchID = touchID;
            _payProcessingFee = payProcessingFee;
        }

        public override string BuildStoreUrl()
        {
            return BuildStoreUrl(_accountNumber.ToString(), _emailTypeID, SupporterName, _supporterID, _sweepsID, _sweeps, _payLater,
				_bweb, _salesChannel, _visitorLogID);
		}

        private string BuildStoreUrl(string accountId, string emailTypeID, string orgName, int supporterID, string sweepsID, 
			string sweeps, string payLater, string bweb, 
			string salesChannel, string visitorLogID)
        {
            // participant name
            SqlString participantName = SqlString.Null;
            if (_parentName != null)
            {
                participantName = _parentName;
            }

            SqlString supporterEmail = SqlString.Null;
            if (_emailAddress != null)
            {
                supporterEmail = _emailAddress;
            }

            SqlInt32 account = SqlInt32.Null;
            if (_accountNumber != int.MinValue)
            {
                account = _accountNumber;
            }

            // always null
            SqlInt32 genderId = SqlInt32.Null;

            SqlInt32 storeId = SqlInt32.Null;
            if (_storeID != int.MinValue)
            {
                storeId = _storeID;
            }

            SqlInt32 templateId = SqlInt32.Null;
            if (_storeTemplateID != int.MinValue)
            {
                templateId = _storeTemplateID;
            }

            SqlInt32 campaignType = SqlInt32.Null;
            if (_eventTypeID != int.MinValue)
            {
                campaignType = _eventTypeID;
            }

            // no more default user
            SqlBoolean participantIsDefault = SqlBoolean.Null;
            SqlBoolean supporterIsDefault = SqlBoolean.Null;


            SqlInt32 aggregatorId = SqlInt32.Null;
            if (_aggregatorID != int.MinValue)
            {
                aggregatorId = _aggregatorID;
            }

            SqlInt32 touchID = SqlInt32.Null;
            if (_touchID != int.MinValue)
            {
                touchID = _touchID;
            }

            SqlString payProcessingFee = SqlString.Null;
            if (_payProcessingFee != false)
            {
                payProcessingFee = "1";
            }
            
            
            // Initialize default values in query string table
			// used for redirection to store.
			Hashtable queryStrRedirTable = new Hashtable();
            queryStrRedirTable["sourceid"] = sourceID.ToString();
            queryStrRedirTable["SuppID"] = "";		// Supporter Id.
            queryStrRedirTable["PartId"] = partID.ToString();	// Attached to the sale used by eFundraising.
            queryStrRedirTable["LanguageId"] = languageID;
            queryStrRedirTable["u1"] = u1;			// Not used for emagnet.
            queryStrRedirTable["FAccountId"] = accountId; // set according to the province from web.config
            queryStrRedirTable["SiteId"] = siteID;
            queryStrRedirTable["OrgName"] = EventName;
//            if (!supporterName.IsNull)
            queryStrRedirTable["supporterName"] = SupporterName;  // Map supporter name
            queryStrRedirTable["exttrkid"] = exttrkid;
//            queryStrRedirTable["catalog_group_id"] = CatalogGroupID; // not needed because the account id shows which catalog group to show the user
//            queryStrRedirTable["catalog_id"] = CatalogID; // not needed because the account id shows which catalog group to show the user

            //queryStrRedirTable["PF"] = "0";         // UPDATE March 25, 2010: Processing Fee.  0 = OFF, 1 = ON (NOT CURRENTLY ACTIVATED ON CANADA)
            
			//
			// Prepare the redirection querystring.
			//

			// Map affiliate id
			//if (! aggregatorId.IsNull)
			//	queryStrRedirTable["aid"] = aggregatorId.Value.ToString();

			// Map email
			//if (! supporterEmail.IsNull)
			//	queryStrRedirTable["email"] = supporterEmail.Value.Trim();

			// Map gender from database to querystring.
			/*if (! genderId.IsNull) {
				if (genderId.Value == 1)
					queryStrRedirTable["Gender"] = "M";
				else if (genderId.Value == 2)
					queryStrRedirTable["Gender"] = "F";
			}*/

			// Map Organizer Name
			//if (! groupName.IsNull)
			//	queryStrRedirTable["ON"] = groupName.Value;

			// Map qxtrak
			queryStrRedirTable["qxtrak"] = "eMagE" + emailTypeID + "|eMagSC" + salesChannel + "|VLID" + visitorLogID;

			// Map sid
			//if (! account.IsNull)
			//	queryStrRedirTable["sid"] = account.Value.ToString();

			// Map student name
            //if(_isUnknownSupporter) {
            //    if (! participantName.IsNull)
            //        queryStrRedirTable["SN"] = ""; //groupName.Value;
            //} else {
            //    if (! participantName.IsNull)
            //        queryStrRedirTable["SN"] = participantName.Value;
            //}


			// Map supporter Id
			if (supporterID != -1)
				queryStrRedirTable["SuppID"] = supporterID.ToString();

			// Map sweeps and SweepsId
            //queryStrRedirTable["sweeps"] = sweeps;
            //queryStrRedirTable["SweepsId"] = sweepsID;

			// Map TemplateId
            //if (! templateId.IsNull) {
            //    queryStrRedirTable["TI"] = templateId.Value.ToString();

            //    // Set TopTenId = 10051 if the templateid = 110.
            //    // This is specific for efundraising.ca used to 
            //    // generate the top ten items to display.
            //    if (templateId.Value == 110) {
            //        queryStrRedirTable["TopTenId"] = "10051";
            //    }
            //}

			// Map top ten id
            //if (! storeId.IsNull)
            //    queryStrRedirTable["TopTenId"] = storeId.ToString();

            //
            // Build optional parameters
            //

            // Check if we need to include TouchID 
            if (!touchID.IsNull)
                queryStrRedirTable["TId"] = touchID.ToString();


			//
			// Build final redirection URL
			//
			string redirectUrl = urlPath + "?";
			foreach (string name in queryStrRedirTable.Keys) {
//				if ((string) queryStrRedirTable[name] != "")
					redirectUrl += HttpUtility.UrlEncode(name) + "=" + HttpUtility.UrlEncode((string) queryStrRedirTable[name]) + "&";
			}			

			if(_catalogItemID != "") {
				if(redirectUrl.EndsWith("&")) {
					redirectUrl += "catalogitemid=" + _catalogItemID + "&magstatus=" + _magStatus;
				} else {
					redirectUrl += "&catalogitemid=" + _catalogItemID + "&magstatus=" + _magStatus;
				}
			}

            if (redirectUrl.EndsWith("&"))
                redirectUrl = redirectUrl.Remove(redirectUrl.Length - 1, 1);

			return redirectUrl;
			//return "http://mygrouppage2.efundraising.com";

		}

		public string BuildStoreUrlQSP(string accountId, string orgName, 
			string supporterName, string eventParticipationId, string catalogItemId, string magStatus, 
			 string visitorLogID) 
		{
			// Initialize default values in query string table
			// used for redirection to store.
			Hashtable queryStrRedirTable = new Hashtable();
            queryStrRedirTable["sourceid"] = sourceID.ToString();
			queryStrRedirTable["SupporterID"] = eventParticipationId;
            queryStrRedirTable["PartId"] = "9999";
            queryStrRedirTable["LanguageId"] = languageID;
            queryStrRedirTable["U1"] = u1;
            queryStrRedirTable["qxtrak"] = "eMagE138|eMagSC1|VLID" + visitorLogID; // Map qxtrak
            queryStrRedirTable["AccountId"] = accountId;
            queryStrRedirTable["SiteId"] = siteID;
			queryStrRedirTable["OrgName"] = orgName; // this.Name

            queryStrRedirTable["SupporterName"] = supporterName;
            queryStrRedirTable["extTrkID"] = exttrkid;

			//
			// Build final redirection URL
			//
			string redirectUrl = urlPath + "?";
			foreach (string name in queryStrRedirTable.Keys) 
			{
//				if ((string) queryStrRedirTable[name] != "")
					redirectUrl += HttpUtility.UrlEncode(name) + "=" + HttpUtility.UrlEncode((string) queryStrRedirTable[name]) + "&";
			}			

			if (redirectUrl.EndsWith("&"))
				redirectUrl = redirectUrl.Remove(redirectUrl.Length - 1, 1);

			return redirectUrl;
	

		}

		#region Properties
		public int EventID {
			set { _eventID = value; }
			get { return _eventID; }
		}

		public int EventTypeID {
			set { _eventTypeID = value; }
			get { return _eventTypeID; }
		}

		public string EventName {
			set { _eventName = value; }
			get { return _eventName; }
		}

		public DateTime StartDate {
			set { _startDate = value; }
			get { return _startDate; }
		}

		public DateTime EndDate {
			set { _endDate = value; }
			get { return _endDate; }
		}

		public int AccountNumber {
			set { _accountNumber = value; }
			get { return _accountNumber; }
		}

        public int OpportunityID
        {
            set { _opportunityId = value; }
            get { return _opportunityId; }
        }

		public int StoreID {
			set { _storeID = value; }
			get { return _storeID; }
		}

		public string SupporterName {
			set { _supporterName = value; }
            get { return _supporterName; }
		}

		public string EmailAddress {
			set { _emailAddress = value; }
			get { return _emailAddress; }
		}

		public int MemberHierarchyID {
			set { _memberHierarchyID = value; }
			get { return _memberHierarchyID; }
		}

		public int AggregatorID {
			set { _aggregatorID = value; }
			get { return _aggregatorID; }
		}

		public int StoreTemplateID {
			set { _storeTemplateID = value; }
			get { return _storeTemplateID; }
		}

		public string CultureCode {
			set { _cultureCode = value; }
			get { return _cultureCode; }
		}

		public string EmailTypeID {
			set { _emailTypeID = value; }
			get { return _emailTypeID; }
		}

		public int SupporterID {
			set { _supporterID = value; }
			get { return _supporterID; }
		}

		public string SweepsID {
			set { _sweepsID = value; }
			get { return _sweepsID; }
		}

		public string Sweeps {
			set { _sweeps = value; }
			get { return _sweeps; }
		}

		public string PayLater {
			set { _payLater = value; }
			get { return _payLater; }
		}

		public string Bweb {
			set { _bweb = value; }
			get { return _bweb; }
		}

		public string SalesChannel {
			set { _salesChannel = value; }
			get { return _salesChannel; }
		}

		public string VisitorLogID {
			set { _visitorLogID = value; }
			get { return _visitorLogID; }
		}

		public string ParentName {
			set { _parentName = value; }
			get { return _parentName; }
		}

		public string ParentEmail {
			set { _parentEmail = value; }
			get { return _parentEmail; }
		}

        public string CatalogGroupID
        {
            set { _catalogGroupID = value; }
            get { return _catalogGroupID; }
        }

        public string CatalogID
        {
            set { _catalogID = value; }
            get { return _catalogID; }
        }

        public int TouchID
        {
            set { _touchID = value; }
            get { return _touchID; }
        }

        public bool PayProcessingFee
        {
            set { _payProcessingFee = value; }
            get { return _payProcessingFee; }
        }

		#endregion
	}
}
