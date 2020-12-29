using System;
using System.Collections;
using System.Data;
using System.Data.SqlTypes;
using System.Web;

using GA.BDC.Core.Database.eSubs;

namespace GA.BDC.Core.StoreJunction.QSP.com
{
	/// <summary>
	/// This class gets the url string needed to go the the http://qsp.com store.
	/// </summary>
	/// <example>
	/// 
	/// efundraising.StoreJunction.QSP.com.Store store = 
	///		new efundraising.StoreJunction.QSP.com.Store();
	///	
	///	Response.Redirect(store.BuildStoreUrl("1", 1, "1", "1", "1", "1", "1", "1"));
	/// 
	/// </example>
	public class Store : GoToStore {
		private int _eventID;
		private int _eventTypeID;
		private string _eventName;
		private DateTime _startDate;
		private DateTime _endDate;
		private int _accountNumber;
        private int _opportunityId;
		private int _storeID;
		private string _name;
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

        private int _touchID;
        private bool _payProcessingFee;

        private string _catalogId;
        private string _categoryId;

        private string _qxtrack = string.Empty;
		public Store() {

		}

        public static Store Create(int eventParticipationID, string emailTypeID, int supporterID, string sweepsID,
            string sweeps, string payLater, string bweb,
            string salesChannel, string visitorLogID, string catalogItemID, string magStatus,
            bool isUnknownSupporter)
        {
            return Create(eventParticipationID, emailTypeID, supporterID, sweepsID, sweeps, payLater, bweb,
                salesChannel, visitorLogID, catalogItemID, magStatus, isUnknownSupporter, int.MinValue, false, string.Empty);
        }

        public static Store Create(int eventParticipationID, string emailTypeID, int supporterID, string sweepsID,
            string sweeps, string payLater, string bweb,
            string salesChannel, string visitorLogID, string catalogItemID, string magStatus,
            bool isUnknownSupporter, int touchID, bool payProcessingFee, string qxtrack)
        {
            return Create(eventParticipationID, emailTypeID, supporterID, sweepsID, sweeps, payLater, bweb,
                salesChannel, visitorLogID, catalogItemID, magStatus, isUnknownSupporter, int.MinValue, false, string.Empty, string.Empty, qxtrack);
        }

		public static Store Create(int eventParticipationID, string emailTypeID, int supporterID, string sweepsID, 
			string sweeps, string payLater, string bweb, 
			string salesChannel, string visitorLogID, string catalogItemID, string magStatus,
            bool isUnknownSupporter, int touchID, bool payProcessingFee, string catalogId, string categoryId, string qxtrack)
        {
			DataAccess.ESubsDatabase dbo = new DataAccess.ESubsDatabase();
			Store store = dbo.GetStore(eventParticipationID);
			store.SetParameters(emailTypeID, eventParticipationID, sweepsID, sweeps, payLater, bweb, salesChannel,
				visitorLogID, catalogItemID, magStatus, isUnknownSupporter, touchID, payProcessingFee, catalogId, categoryId, qxtrack);
			return store;
		}

		private void SetParameters(string emailTypeID, int supporterID, string sweepsID, 
			string sweeps, string payLater, string bweb, 
			string salesChannel, string visitorLogID, string catalogItemID, string magStatus,
			bool isUnknownSupporter, int touchID, bool payProcessingFee, string catalogId, string categoryId, string qxtrack) {
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
            _touchID = touchID;
            _payProcessingFee = payProcessingFee;
            _catalogId = catalogId;
            _categoryId = categoryId;
            _qxtrack = qxtrack;
           
		}

		public override string BuildStoreUrl()  {
			return BuildStoreUrl(_emailTypeID, _supporterID, _sweepsID, _sweeps, _payLater,
                _bweb, _salesChannel, _visitorLogID, _qxtrack);
		}

		#region OLD
		/// <summary> 
		/// Build the redirection URL to go to QSP Store.
		/// </summary>
		/// <param name="emailTypeID">Email Type Id</param>
		/// <param name="supporterID">Supporter Id</param>
		/// <param name="sweepsID">Sweeps Id.</param>
		/// <param name="sweeps">Sweeps</param>
		/// <param name="payLater">Pay later</param>
		/// <param name="bweb">bweb</param>
		/// <param name="salesChannel">Sales channel</param>
		/// <param name="visitorLogID">Visitor log Id.</param>
		/// <returns>The redirection URL.</returns>
		private string TBDBuildStoreUrl2(string emailTypeID, int supporterID, string sweepsID, 
			string sweeps, string payLater, string bweb, 
			string salesChannel, string visitorLogID) {
			// Initialize default host redirect.
			string urlPath = "http://www.qsp.com/gift";

			// Initialize default values in query string table
			// used for redirection to store.
			Hashtable queryStrRedirTable = new Hashtable();
			queryStrRedirTable["aid"] = "";			// Affiliate Id
			queryStrRedirTable["bweb"] = "1";		// From web. 0 = email, 1 = web
			queryStrRedirTable["email"] = "bin" + DateTime.Now.Ticks + "@efundraising.com";	// Supporter email.
			queryStrRedirTable["Gender"] = "";		// Supporter gender. M or F.
			queryStrRedirTable["ON"] = "";			// Organization/Group name.
			queryStrRedirTable["PartId"] = "9999";	// Attached to the sale used by eFundraising.
			queryStrRedirTable["PL"] = "0";			// Pay later flag. 0 - No paylater, 1 - paylater, Null - paylater.
			queryStrRedirTable["qxtrak"] = "";		// QSP external tracking Id.
			queryStrRedirTable["sid"] = "";			// School or Account Id.
			queryStrRedirTable["SN"] = "";			// Student full name
			queryStrRedirTable["sName"] = "";		// Supporter name
			queryStrRedirTable["SuppId"] = "";		// Supporter Id.
			queryStrRedirTable["sweeps"] = "";		// Flag if user will be entered into sweepstakes. 1 - yes, 0 - redirected to "No Purchase", Null - No sweepstakes.
			queryStrRedirTable["SweepsId"] = "";	// Sweepstakes Id
			queryStrRedirTable["TI"] = "1";			// Template Id.
			queryStrRedirTable["TopTenId"] = "";	// Used to set the top ten items to display at store
			queryStrRedirTable["u1"] = "0";			// Not used for emagnet.

			
			// Get params from database
			DatabaseObject dbo = new DatabaseObject();
			DataTable dt = new DataTable();

			try {
				dbo.Open();
				dt = dbo.GetLinkToStore(supporterID);
			}
			catch {
				return "http://mygrouppage2.efundraising.com";
			}
			finally {
				dbo.Close();
			}

			//
			// Read data from database
			//
			if (dt.Rows.Count > 0) {

				SqlString participantName = SqlString.Null;
				if (! dt.Rows[0].IsNull("Participant_Name"))
					participantName = dt.Rows[0]["Participant_Name"].ToString().Trim();

				SqlString supporterName = SqlString.Null;
				if (! dt.Rows[0].IsNull("Supporter_Name"))
					supporterName = dt.Rows[0]["Supporter_Name"].ToString().Trim();

				SqlString supporterEmail = SqlString.Null;
				if (! dt.Rows[0].IsNull("Supporter_Email"))
					supporterEmail = dt.Rows[0]["Supporter_Email"].ToString().Trim();

				SqlInt32 account = SqlInt32.Null;
				if (! dt.Rows[0].IsNull("Account_Number"))
					account = Convert.ToInt32(dt.Rows[0]["Account_Number"]);

				SqlInt32 genderId = SqlInt32.Null;
				if (! dt.Rows[0].IsNull("Gender"))
					genderId = Convert.ToInt32(dt.Rows[0]["Gender"]);
				
		//-----------------------------------------------------------------------------------------		
		//** IMPORTANT **//
		// NOTE: 13 Juillet 2005 - 
		//		Selon les propos de Fred, le champ StoreID sera remplacer par
		//		le champ Store_ID dans la prochaine version de bd
		//		- Louis Turmel -
				SqlInt32 storeId = SqlInt32.Null;
				/*
				if(!dt.Rows[0].IsNull("Store_Id"))
					storeId = Convert.ToInt32(dt.Rows[0]["Store_Id"]);
				*/
				if (! dt.Rows[0].IsNull("StoreID"))
					storeId = Convert.ToInt32(dt.Rows[0]["StoreID"]);

		//------------------------------------------------------------------------------------------
		

		//------------------------------------------------------------------------------------------
		// NOTE: Dans la store procedure le Template_Desc_ID est un ALIAS au champ store_template_id		
				SqlInt32 templateId = SqlInt32.Null;
				if (! dt.Rows[0].IsNull("Template_Desc_ID"))
					templateId = Convert.ToInt32(dt.Rows[0]["Template_Desc_ID"]);
		//------------------------------------------------------------------------------------------

				SqlString groupName = SqlString.Null;
				if (! dt.Rows[0].IsNull("Group_Name"))
					groupName = dt.Rows[0]["Group_Name"].ToString();

				SqlInt32 campaignType = SqlInt32.Null;
				if (! dt.Rows[0].IsNull("Campaign_Type_ID"))
					campaignType = Convert.ToInt32(dt.Rows[0]["Campaign_Type_ID"]);

				SqlBoolean participantIsDefault = SqlBoolean.Null;
				if (! dt.Rows[0].IsNull("Participant_Is_Default"))
					participantIsDefault = Convert.ToBoolean(dt.Rows[0]["Participant_Is_Default"]);

				SqlBoolean supporterIsDefault = SqlBoolean.Null;
				if (! dt.Rows[0].IsNull("Supporter_Is_Default"))
					supporterIsDefault = Convert.ToBoolean(dt.Rows[0]["Supporter_Is_Default"]);

				SqlInt32 aggregatorId = SqlInt32.Null;
				if (! dt.Rows[0].IsNull("aggregator_id"))
					aggregatorId = Convert.ToInt32(dt.Rows[0]["aggregator_id"]);


				//
				// Prepare the redirection querystring.
				//

				// Map affiliate id
				if (! aggregatorId.IsNull)
					queryStrRedirTable["aid"] = aggregatorId.Value.ToString();

				// Map email
				if (! supporterEmail.IsNull)
					queryStrRedirTable["email"] = supporterEmail.Value.Trim();

				// Map gender from database to querystring.
				if (! genderId.IsNull) {
					if (genderId.Value == 1)
						queryStrRedirTable["Gender"] = "M";
					else if (genderId.Value == 2)
						queryStrRedirTable["Gender"] = "F";
				}

				// Map Organizer Name
				if (! groupName.IsNull)
					queryStrRedirTable["ON"] = groupName.Value;

				// Map paylater
				// NOTE: For eSubs, PayLater will never be true
//				if (payLater != "" || (IsPayLater(campaignType.Value))) {
//					queryStrRedirTable["PL"] = "1";
//				}

				// Map qxtrak
				queryStrRedirTable["qxtrak"] = "eMagE" + emailTypeID + "|eMagSC" + salesChannel + "|VLID" + visitorLogID;

				// Map sid
				if (! account.IsNull)
					queryStrRedirTable["sid"] = account.Value.ToString();

				// Map student name
				if (! participantName.IsNull)
					queryStrRedirTable["SN"] = participantName.Value;

				// Map supporter name
				if (! supporterName.IsNull)
					queryStrRedirTable["sName"] = supporterName.Value;

				// Map supporter Id
				if (supporterID != -1)
					queryStrRedirTable["SuppId"] = supporterID.ToString();

				// Map sweeps and SweepsId
				queryStrRedirTable["sweeps"] = sweeps;
				queryStrRedirTable["SweepsId"] = sweepsID;

				// Map TemplateId
				if (! templateId.IsNull) {
					queryStrRedirTable["TI"] = templateId.Value.ToString();

					// Set TopTenId = 10051 if the templateid = 110.
					// This is specific for efundraising.com used to 
					// generate the top ten items to display.
					if (templateId.Value == 110) {
						queryStrRedirTable["TopTenId"] = "10051";
					}
				}

				// Map top ten id
				if (! storeId.IsNull)
					queryStrRedirTable["TopTenId"] = storeId.ToString();

				// Build host part
				if (storeId.IsNull || storeId.Value != 1)
					urlPath = "http://www.qsp.com/store/efund/efund.asp";

				if (! campaignType.IsNull && campaignType.Value == 13) {
					if (! aggregatorId.IsNull && aggregatorId.Value == 13)
						urlPath = "http://www.magazinefundraising.com/store/mvp/efund.asp";
					else
						urlPath = "http://www.magazinefundraising.com/store/esubs/efund.asp";
				}

				//
				// Build final redirection URL
				//
				string redirectUrl = urlPath + "?";
				foreach (string name in queryStrRedirTable.Keys) {
					if ((string) queryStrRedirTable[name] != "")
						redirectUrl += HttpUtility.UrlEncode(name) + "=" + HttpUtility.UrlEncode((string) queryStrRedirTable[name]) + "&";
				}			

				return redirectUrl;
			}
			else {
				return "http://mygrouppage2.efundraising.com";
			}
		}
		#endregion

		private string BuildStoreUrl(string emailTypeID, int supporterID, string sweepsID, 
			string sweeps, string payLater, string bweb, 
			string salesChannel, string visitorLogID, string qxtrack) {
			// Initialize default host redirect.
			string urlPath = "http://www.qsp.com/gift";

			// Initialize default values in query string table
			// used for redirection to store.
			Hashtable queryStrRedirTable = new Hashtable();
			//queryStrRedirTable["aid"] = "";			// Affiliate Id, UPDATE JULY 21: Removed as per Shawn Upton
            //queryStrRedirTable["bweb"] = "1";		// From web. 0 = email, 1 = web, UPDATE JULY 21: Removed as per Shawn Upton
            //queryStrRedirTable["email"] = "bin" + DateTime.Now.Ticks + "@efundraising.com";	// Supporter email, UPDATE JULY 21: Removed as per Shawn Upton
			queryStrRedirTable["Gender"] = "";		// Supporter gender. M or F.
			queryStrRedirTable["ON"] = "";			// Organization/Group name.
			queryStrRedirTable["PartId"] = "9999";	// Attached to the sale used by eFundraising.
            //queryStrRedirTable["PL"] = "0";			// Pay later flag. 0 - No paylater, 1 - paylater, Null - paylater., UPDATE JULY 21: Removed as per Shawn Upton
			queryStrRedirTable["qxtrak"] = "";		// QSP external tracking Id.
			queryStrRedirTable["sid"] = "";			// School or Account Id.
			queryStrRedirTable["SN"] = "";			// Student full name
			queryStrRedirTable["sName"] = "";		// Supporter name
            queryStrRedirTable["SuppId"] = "";		// Supporter Id., UPDATE JULY 21: Removed as per Shawn Upton
            //queryStrRedirTable["sweeps"] = "";		// Flag if user will be entered into sweepstakes. 1 - yes, 0 - redirected to "No Purchase", Null - No sweepstakes., UPDATE JULY 21: Removed as per Shawn Upton
            //queryStrRedirTable["SweepsId"] = "";	// Sweepstakes Id, UPDATE JULY 21: Removed as per Shawn Upton
            //queryStrRedirTable["TI"] = "1";			// Template Id., UPDATE JULY 21: Removed as per Shawn Upton
            //queryStrRedirTable["TopTenId"] = "";	// Used to set the top ten items to display at store, UPDATE JULY 21: Removed as per Shawn Upton
			queryStrRedirTable["u1"] = "0";			// Not used for emagnet.
            queryStrRedirTable["SiteId"] = "2";     // UPDATE March 24, 2010: Site ID
            queryStrRedirTable["PF"] = "0";         // UPDATE March 25, 2010: Processing Fee.  0 = OFF, 1 = ON

			// participant name
			SqlString participantName = SqlString.Null;
			if(_parentName != null) {
				participantName = _parentName;
			}

			SqlString supporterName = SqlString.Null;
			if(_name != null) {
				supporterName = _name;
			}

			SqlString supporterEmail = SqlString.Null;
			if(_emailAddress != null) {
				supporterEmail = _emailAddress;
			}

            // UPDATE MAY 30 2011: Conversion to ERP
            //      Transfer order fulfillment from EDS to TCS => Need to use the new "opportunity_id" instead of "account_number" (if exists)
			SqlInt32 account = SqlInt32.Null;
            if (_opportunityId != int.MinValue)
            {
                account = _opportunityId;
            }
            else if (_accountNumber != int.MinValue)
            {
                account = _accountNumber;
            }

			// always null
			SqlInt32 genderId = SqlInt32.Null;
				
			SqlInt32 storeId = SqlInt32.Null;
			if(_storeID != int.MinValue) {
				storeId = _storeID;
			}

			SqlInt32 templateId = SqlInt32.Null;
			if(_storeTemplateID != int.MinValue) {
				templateId = _storeTemplateID;
			}

			SqlString groupName = SqlString.Null;
			if(_eventName != null) {
				groupName = _eventName;
			}

			SqlInt32 campaignType = SqlInt32.Null;
			if(_eventTypeID != int.MinValue) {
				campaignType = _eventTypeID;
			}

			// no more default user
			SqlBoolean participantIsDefault = SqlBoolean.Null;
			SqlBoolean supporterIsDefault = SqlBoolean.Null;
			

			SqlInt32 aggregatorId = SqlInt32.Null;
			if(_aggregatorID != int.MinValue) {
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

			//
			// Prepare the redirection querystring.
			//

			// Map affiliate id
            //if (! aggregatorId.IsNull) 
            //    queryStrRedirTable["aid"] = aggregatorId.Value.ToString();

			// Map email
            //if (! supporterEmail.IsNull)
            //    queryStrRedirTable["email"] = supporterEmail.Value.Trim();

			// Map gender from database to querystring.
			if (! genderId.IsNull) {
				if (genderId.Value == 1)
					queryStrRedirTable["Gender"] = "M";
				else if (genderId.Value == 2)
					queryStrRedirTable["Gender"] = "F";
			}

			// Map Organizer Name
			if (! groupName.IsNull)
				queryStrRedirTable["ON"] = groupName.Value;

			// Map paylater
			// NOTE: For eSubs, PayLater will never be true
			//				if (payLater != "" || (IsPayLater(campaignType.Value))) {
			//					queryStrRedirTable["PL"] = "1";
			//				}

			// Map qxtrak
            if (qxtrack != string.Empty)
            {
                queryStrRedirTable["qxtrak"] = qxtrack;
            }
            else
            {
			queryStrRedirTable["qxtrak"] = "eMagE" + emailTypeID + "|eMagSC" + salesChannel + "|VLID" + visitorLogID;
            }

			// Map sid
            if (!account.IsNull)
            {
                queryStrRedirTable["sid"] = account.Value.ToString();
            }

			// Map student name
			if(_isUnknownSupporter) {
				if (! participantName.IsNull)
					queryStrRedirTable["SN"] = ""; //groupName.Value;
			} else {
				if (! participantName.IsNull)
					queryStrRedirTable["SN"] = participantName.Value;
			}

			// Map supporter name
			if (! supporterName.IsNull)
				queryStrRedirTable["sName"] = supporterName.Value;

			// Map supporter Id
			if (supporterID != -1)
				queryStrRedirTable["SuppId"] = supporterID.ToString();

			// Map sweeps and SweepsId
            //queryStrRedirTable["sweeps"] = sweeps;
            //queryStrRedirTable["SweepsId"] = sweepsID;

			// Map TemplateId
            //if (! templateId.IsNull) {
            //    queryStrRedirTable["TI"] = templateId.Value.ToString();

				// Set TopTenId = 10051 if the templateid = 110.
				// This is specific for efundraising.com used to 
				// generate the top ten items to display.
            //    if (templateId.Value == 110) {
            //        queryStrRedirTable["TopTenId"] = "10051";
            //    }
            //}

            //// Map top ten id
            //if (! storeId.IsNull)
            //    queryStrRedirTable["TopTenId"] = storeId.ToString();

            //UPDATE MARCH 24, 2010:
            //     In order to encrypt the store querystring, we needed to add "SiteId" paramter to fix the issues below:
            //      1) MVP partner does not link to MVP template    =>  add SiteId=3
            //      2) View all selections links to an error page   =>  add SiteId=2
            //     All other links will default to SiteId = 2
            if (!aggregatorId.IsNull && aggregatorId.Value == 13)
                queryStrRedirTable["SiteId"] = "3";

            // Map processing Fee
            if (!payProcessingFee.IsNull)
                queryStrRedirTable["PF"] = payProcessingFee.ToString();
            
            //
            // Build optional parameters
            //
            
            // Check if we need to include TouchID 
            if (!touchID.IsNull)
                queryStrRedirTable["TId"] = touchID.ToString();
                
            
            //
			// Build final redirection URL
			//

            // Build host part
            //     UPDATE MARCH 23, 2010: Store redirect encryption will need to work on a .NET page so "efund.asp" is no longer used
            urlPath = "http://www.magazinefundraising.com/store/setsession.aspx";

            #region Old host part
            //if (storeId.IsNull || storeId.Value != 1)
            //    urlPath = "http://www.magazinefundraising.com/store/esubs/efund.asp";

            //if (! aggregatorId.IsNull && aggregatorId.Value == 13)
            //        urlPath = "http://www.magazinefundraising.com/store/mvp/efund.asp";
            //    else
            //        urlPath = "http://www.magazinefundraising.com/store/esubs/efund.asp";
            #endregion

			string redirectUrl = urlPath + "?";
			foreach (string name in queryStrRedirTable.Keys) {
				if ((string) queryStrRedirTable[name] != "")
					redirectUrl += HttpUtility.UrlEncode(name) + "=" + HttpUtility.UrlEncode((string) queryStrRedirTable[name]) + "&";
			}

            if (_catalogItemID != "")
            {
                if (redirectUrl.EndsWith("&"))
                {
                    redirectUrl += "catalogitemid=" + _catalogItemID + "&magstatus=" + _magStatus;
                }
                else
                {
                    redirectUrl += "&catalogitemid=" + _catalogItemID + "&magstatus=" + _magStatus;
                }
            }

            //UPDATE AUGUST 16, 2010: Sepcific catalog view
            if (_catalogId != string.Empty && _categoryId != string.Empty)
            {
                if (redirectUrl.EndsWith("&"))
                {
                    redirectUrl += "CatalogId=" + _catalogId + "&CategoryId=" + _categoryId;
                }
                else
                {
                    redirectUrl += "&CatalogId=" + _catalogId + "&CategoryId=" + _categoryId;
                }
            }

            if (redirectUrl.EndsWith("&"))
                redirectUrl = redirectUrl.Remove(redirectUrl.Length - 1, 1);

			return redirectUrl;
			//return "http://mygrouppage2.efundraising.com";

		}


		public string BuildStoreUrlQSP(string accountId, string orgName, string studentName, 
			string supporterName, string eventParticipationId, string catalogItemId, string magStatus, 
			string supporterQSP, string onlineParticipantQSP, string visitorLogID, string touch_id, string payProcessingFee) 
		{
			// Initialize default host redirect.
			string urlPath = "";

			// Initialize default values in query string table
			// used for redirection to store.
			Hashtable queryStrRedirTable = new Hashtable();
			queryStrRedirTable["SiteId"] = "17";
			queryStrRedirTable["AccountId"] = accountId;
			queryStrRedirTable["LanguageId"] = "1";
			queryStrRedirTable["OrgName"] = orgName;
			if (studentName != null)
				queryStrRedirTable["StudentName"] = studentName;
			if (supporterName != null)
				queryStrRedirTable["SupporterName"] = supporterName;
			queryStrRedirTable["SuppId"] = eventParticipationId;
			queryStrRedirTable["PartId"] = "9999";
			queryStrRedirTable["U1"] = "0";
			if (catalogItemId != "")
				queryStrRedirTable["catalogitemid"] = catalogItemId;
			if (magStatus != "")
				queryStrRedirTable["magstatus"] = magStatus;
			queryStrRedirTable["sourceid"] = "2";
			/* if (supporterQSP != null)
				queryStrRedirTable["supporterid"] = supporterQSP;*/
			if (onlineParticipantQSP != null)
				queryStrRedirTable["participantid"] = onlineParticipantQSP;

            // UPDATE March 26, 2010: Check if we need to include TouchID to track sales per email, and processing fee
            if (!string.IsNullOrEmpty(touch_id))
                queryStrRedirTable["TId"] = touch_id;
            if (!string.IsNullOrEmpty(payProcessingFee))
                queryStrRedirTable["PF"] = payProcessingFee;


			// Map qxtrak
			queryStrRedirTable["qxtrak"] = "eMagE138|eMagSC1|VLID" + visitorLogID;


			urlPath = "http://www.qsp.com/Store/SetSession.aspx";


			//
			// Build final redirection URL
			//
			string redirectUrl = urlPath + "?";
			foreach (string name in queryStrRedirTable.Keys) 
			{
				if ((string) queryStrRedirTable[name] != "")
					redirectUrl += HttpUtility.UrlEncode(name) + "=" + HttpUtility.UrlEncode((string) queryStrRedirTable[name]) + "&";
			}			

			if (redirectUrl.EndsWith("&"))
				redirectUrl = redirectUrl.Remove(redirectUrl.Length - 1, 1);

			redirectUrl = redirectUrl + "&extTrackID=eLaunch";

			return redirectUrl;
			//return "http://mygrouppage2.efundraising.com";

		}


		/// <summary>
		/// Determine if a campaign should be paylater.
		/// </summary>
		/// <param name="campaignType">CampaignType Id.</param>
		/// <returns>True if the campaign should be paylater, else false.</returns>
		public bool IsPayLater(int campaignType) {
			switch (campaignType) {
				case 1:
				case 2:
				case 3:
				case 4:
					return true;
				case 5:
					return false;
				case 6:
				case 7:
					return true;
				case 8:
				case 9:
				case 10:
				case 11:
				case 12:
				case 13:
					return false;
				case 14:
					return true;
				default:
					return false;
			}
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

		public string Name {
			set { _name = value; }
			get { return _name; }
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
        
        public string CatalogId {
			set { _catalogId = value; }
			get { return _catalogId; }
		}

        public string CategoryId
        {
			set { _categoryId = value; }
            get { return _categoryId; }
		}
		#endregion
	}
}
