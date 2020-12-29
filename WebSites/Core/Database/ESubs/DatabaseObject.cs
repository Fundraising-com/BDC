//
// 2004-12-01	-	Jean-Francois Buist	- Version 0.0
// 2005-01-06	-	Louis Turmel		- Ajout de la function GetTotalRaised
// 2005-01-08	-	Stephen Lim			- Added new method GetLinkToStore().
// 2005-02-18	-	Stephen Lim			- Added FindGroup overload for suggestion group names.
// 2005-02-28	-	Louis Turmel		- Added new Method to Creating Campaign, Taken Creation Channel Parameters
// 2005-03-11	-	Louis Turmel		- Added es_get_parter_top10 Method 
// 2005-03-14	-	Louis Turmel		- Added GetTop10Members method
// 2005-03-31	-	Louis Turmel		- Added GetParticipantInvitedByCampaignID function
// 2005-04-19	-	Louis Turmel		- Added GetCampaignIDFromEmail function
// 2005-05-03	-	Louis Turmel		- Added CreateCampaign functions
// 2005-05-19	-	Louis Turmel		- Added GetCampaignIDFromOrgID
// 2005-05-30	-	Stephen Lim			- Add check for column to_update before reading row in GetCampaignIDFromOrgID.
// 2005-05-30	-	Stephen Lim			- Add UpdateAccountInfo() method.
// 2005-06-15	-	Stephen Lim			- Add support for IsActive in InsertParticipant().

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Specialized;

using GA.BDC.Core.EnterpriseComponents;

namespace GA.BDC.Core.Database.eSubs
{

	/// <summary>
	/// This class encapsulate all requests used by any eSubs project.
	/// </summary>
	/// <remarks>
	/// This class will use the connection string provided by your configuration.
	/// Make sure it matches and you have the correct rights.
	/// </remarks>
    public class DatabaseObject : GA.BDC.Core.Database.DatabaseObjects
    {

		public DatabaseObject()	{

		}

		protected override string GetConnectionStringConfigKey() {
			return "eSubs.ConnectionString";
		}

		protected override string GetDataProviderStringConfigKey() {
			return "eSubs.DataProvider";
		}

		#region eSubs Reporting

		/// <summary>
		/// Get email activity by participant	
		/// </summary>
		/// <param name="partnerId"></param>
		/// <param name="campaignId"></param>
		/// <param name="beginDate"></param>
		/// <param name="endDate"></param>
		/// <param name="count">pass -1 to retrieve all</param>
		/// <param name="orderByType"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public DataTable GetParticipantSaleInfo(int campaignId) {

			DataParameters[] parameters = new DataParameters[2];
			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intCampaignId";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = campaignId;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@blnFiltered";
			parameters[1].DataType = DbType.Byte;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = 1;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());

			return dbi.ExecuteFetchDataTable("es_rpt_get_part_sales_info_by_camp", parameters);
		}

		public DataTable es_rpt_get_sales_summary_by_campaign(int campaignId) {

			DataParameters[] parameters = new DataParameters[1];
			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intCampaignId";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = campaignId;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());

			return dbi.ExecuteFetchDataTable("es_rpt_get_sales_summary_by_campaign", parameters);
			
		}


		// March 31, 2005	-	Louis Turmel	-	New Function
		public DataTable GetParticipantInvitedByCampaignID(int pCampaignID) {
			DataParameters[] parameters = new DataParameters[1];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intCampaignID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = pCampaignID;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_rpt_get_participants_invited_stats_by_campaign", parameters);
			} catch {
				// todo throw an error
			}
			return dt;
		}

		public DataTable GetParticipantsByStatsByCampaignID(int intCampaignID) {
			DataParameters[] parameters = new DataParameters[1];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intCampaignID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = intCampaignID;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_rpt_get_participants_stats_by_campaign", parameters);
			} catch {
				
			}
			return dt;
		}

		#endregion eSubs Reporting

		#region Generate DataSet/DataTables from xml file queries

        public DataSet GetDataSetFromQueryByName(GA.BDC.Core.Database.DatabaseQueries dq, string queryName, params string[] list)
        {
            GA.BDC.Core.Database.Query query = dq.GetQueryByName(queryName);
			string queryString = "";
			if(query == null) {
				throw new Exception("Unable to retreive the query from xml queries collection file!");
			} else {
				queryString = query.QueryString;
			}
			return GetDataSetFromQuery(queryString, list);
		}

        public DataTable GetDataTableFromQueryByName(GA.BDC.Core.Database.DatabaseQueries dq, string queryName, params string[] list)
        {
			DataSet ds = GetDataSetFromQueryByName(dq, queryName, list);
			return ds.Tables[0];
		}

		public DataTable GetDataTableFromQuery(string query, params string[] list) {
			return GetDataTableFromQuery(query, GetConnectionStringConfigKey(), GetDataProviderStringConfigKey(), list);
		}

		public DataSet GetDataSetFromQuery(string query, params string[] list) {
			return GetDataSetFromQuery(query, GetConnectionStringConfigKey(), GetDataProviderStringConfigKey(), list);
		}
		#endregion

		#region Create Campaign
		/// <summary>
		/// Create Campaing using a Lead ID
		/// </summary>
		/// <param name="campaignID"></param>
		/// <param name="leadID"></param>
		/// <param name="creationChannel"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public DataTable CreateCampaign(ref int campaignID, int leadID, int creationChannel, string culture) {
			return CreateCampaign(ref campaignID, -1, -1, "", "", "", "", "", "", "", "", "", "", "", "", "",
				-1, -1, leadID, creationChannel, "", -1, -1, -1, "", culture);
		}

		public void CreateCampaignByExternalOrgID(ref int pCampaignID, int pPartnerID,  string pSponsorName, string pSponsorGroupName, 
			string pSponsorEmail, string pExternalOrgUrl, int pExternalOrgID, int pExternalGroupID,
			string pCultureCode, int pCreationChannel) {
			DataParameters[] parameters = new DataParameters[26];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intPartnerID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = pPartnerID;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@intOrganizationID";
			parameters[1].DataType = DbType.Int32;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = DBNull.Value;			

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@strCampaignName";
			parameters[2].DataType = DbType.String;
			parameters[2].ParamDirection = ParameterDirection.Input;
			parameters[2].Value = pSponsorGroupName;

			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@strAddress";
			parameters[3].DataType = DbType.String;
			parameters[3].ParamDirection = ParameterDirection.Input;
			parameters[3].Value = DBNull.Value;			

			parameters[4] = new DataParameters();
			parameters[4].ParameterName = "@strCity";
			parameters[4].DataType = DbType.String;
			parameters[4].ParamDirection = ParameterDirection.Input;
			parameters[4].Value = DBNull.Value;
		
			parameters[5] = new DataParameters();
			parameters[5].ParameterName = "@strZip";
			parameters[5].DataType = DbType.String;
			parameters[5].ParamDirection = ParameterDirection.Input;
			parameters[5].Value = DBNull.Value;			

			parameters[6] = new DataParameters();
			parameters[6].ParameterName = "@strStateCode";
			parameters[6].DataType = DbType.String;
			parameters[6].ParamDirection = ParameterDirection.Input;
			parameters[6].Value = DBNull.Value;		

			parameters[7] = new DataParameters();
			parameters[7].ParameterName = "@strOrganizerName";
			parameters[7].DataType = DbType.String;
			parameters[7].ParamDirection = ParameterDirection.Input;
			parameters[7].Value = pSponsorName;

			parameters[8] = new DataParameters();
			parameters[8].ParameterName = "@strDayPhone";
			parameters[8].DataType = DbType.String;
			parameters[8].ParamDirection = ParameterDirection.Input;
			parameters[8].Value = DBNull.Value;
			

			parameters[9] = new DataParameters();
			parameters[9].ParameterName = "@strEveningPhone";
			parameters[9].DataType = DbType.String;
			parameters[9].ParamDirection = ParameterDirection.Input;
			parameters[9].Value = DBNull.Value;
			

			parameters[10] = new DataParameters();
			parameters[10].ParameterName = "@strFaxPhone";
			parameters[10].DataType = DbType.String;
			parameters[10].ParamDirection = ParameterDirection.Input;
			parameters[10].Value = DBNull.Value;
			

			parameters[11] = new DataParameters();
			parameters[11].ParameterName = "@strTitle";
			parameters[11].DataType = DbType.String;
			parameters[11].ParamDirection = ParameterDirection.Input;
			parameters[11].Value = DBNull.Value;			

			parameters[12] = new DataParameters();
			parameters[12].ParameterName = "@strPayableName";
			parameters[12].DataType = DbType.String;
			parameters[12].ParamDirection = ParameterDirection.Input;
			if(pSponsorName == "") {
				parameters[12].Value = DBNull.Value;
			} else {
				parameters[12].Value = pSponsorName;
			}

			parameters[13] = new DataParameters();
			parameters[13].ParameterName = "@strOrganizerEmail";
			parameters[13].DataType = DbType.String;
			parameters[13].ParamDirection = ParameterDirection.Input;
			parameters[13].Value = pSponsorEmail;

			parameters[14] = new DataParameters();
			parameters[14].ParameterName = "@intExternalOrgURL";
			parameters[14].DataType = DbType.String;
			parameters[14].ParamDirection = ParameterDirection.Input;
			if(pExternalOrgUrl == "") {
				parameters[14].Value = DBNull.Value;
			} else {
				parameters[14].Value = pExternalOrgUrl;
			}

			parameters[15] = new DataParameters();
			parameters[15].ParameterName = "@intExternalOrgID";
			parameters[15].DataType = DbType.Int32;
			parameters[15].ParamDirection = ParameterDirection.Input;
			if(pExternalOrgID == -1) {
				parameters[15].Value = DBNull.Value;
			} else {
				parameters[15].Value = pExternalOrgID;
			}

			parameters[16] = new DataParameters();
			parameters[16].ParameterName = "@intExternalGroupID";
			parameters[16].DataType = DbType.Int32;
			parameters[16].ParamDirection = ParameterDirection.Input;
			if(pExternalGroupID == -1) {
				parameters[16].Value = DBNull.Value;
			} else {
				parameters[16].Value = pExternalGroupID;
			}

			parameters[17] = new DataParameters();
			parameters[17].ParameterName = "@intLeadID";
			parameters[17].DataType = DbType.Int32;
			parameters[17].ParamDirection = ParameterDirection.Input;
			parameters[17].Value = DBNull.Value;

			parameters[18] = new DataParameters();
			parameters[18].ParameterName = "@intOrgCreationChannelID";
			parameters[18].DataType = DbType.Int32;
			parameters[18].ParamDirection = ParameterDirection.Input;
			if(pCreationChannel == -1)
				parameters[18].Value = DBNull.Value;
			else
				parameters[18].Value = pCreationChannel;
			
			parameters[19] = new DataParameters();
			parameters[19].ParameterName = "@strPassword";
			parameters[19].DataType = DbType.String;
			parameters[19].ParamDirection = ParameterDirection.Input;
			parameters[19].Value = DBNull.Value;			

			parameters[20] = new DataParameters();
			parameters[20].ParameterName = "@intNumberParticipants";
			parameters[20].DataType = DbType.Int32;
			parameters[20].ParamDirection = ParameterDirection.Input;
			parameters[20].Value = 1;			

			parameters[21] = new DataParameters();
			parameters[21].ParameterName = "@intFinancialGoal";
			parameters[21].DataType = DbType.Int32;
			parameters[21].ParamDirection = ParameterDirection.Input;
			parameters[21].Value = 1;			

			parameters[22] = new DataParameters();
			parameters[22].ParameterName = "@blnWillingToPurchase";
			parameters[22].DataType = DbType.Byte;
			parameters[22].ParamDirection = ParameterDirection.Input;
			parameters[22].Value = 1;			

			parameters[23] = new DataParameters();
			parameters[23].ParameterName = "@strFundraisingReason";
			parameters[23].DataType = DbType.String;
			parameters[23].ParamDirection = ParameterDirection.Input;
			parameters[23].Value = DBNull.Value;			

			parameters[24] = new DataParameters();
			parameters[24].ParameterName = "@strCultureCode";
			parameters[24].DataType = DbType.String;
			parameters[24].ParamDirection = ParameterDirection.Input;
			parameters[24].Value = "en-US";

			parameters[25] = new DataParameters();
			parameters[25].DataType = DbType.Int32;
			parameters[25].ParamDirection = ParameterDirection.ReturnValue;

			DatabaseInterface dbi = new DatabaseInterface();
			try {
				dbi.ExecuteNonQuery("es_create_campaign", parameters);
				pCampaignID = (int)parameters[25].Value;
			} catch(System.Exception ex) {
				throw ex;
			}			
		}

		/// <summary>
		/// Create Campaign using custom information
		/// </summary>
		/// <param name="campaignID"></param>
		/// <param name="intPartnerID"></param>
		/// <param name="intOrganizationID"></param>
		/// <param name="strCampaignName"></param>
		/// <param name="strAddress"></param>
		/// <param name="strCity"></param>
		/// <param name="strZip"></param>
		/// <param name="strStateCode"></param>
		/// <param name="strOrganizerName"></param>
		/// <param name="strDayPhone"></param>
		/// <param name="strEveningPhone"></param>
		/// <param name="strFaxPhone"></param>
		/// <param name="strTitle"></param>
		/// <param name="strPayableName"></param>
		/// <param name="strOrganizerEmail"></param>
		/// <param name="strExternalOrgURL"></param>
		/// <param name="intExternalOrgID"></param>
		/// <param name="intExternalGroupID"></param>
		/// <param name="intLeadID"></param>
		/// <param name="intOrgCreationChannelID"></param>
		/// <param name="strPassword"></param>
		/// <param name="intNumberParticipants"></param>
		/// <param name="intFinancialGoal"></param>
		/// <param name="blnWillingToPurchase"></param>
		/// <param name="strFundraisingReason"></param>
		/// <param name="strCultureCode"></param>
		/// <returns>
		/// DataTable
		/// </returns>
		/// <remarks>
		/// campaign id > 0 if OK
		/// -1 = Organizer already exists 
		/// -3 = Partner id is not null
		/// -4 = Internal Error
		/// -5 = Error inserting into organization
		/// -6 = Error inserting into organizer
		/// -7 = Error inserting into campaign
		/// -8 = Error inserting into participant
		/// -9 = Error inserting into supporter
		/// </remarks>
		public DataTable CreateCampaign(ref int campaignID, int intPartnerID, int intOrganizationID, string strCampaignName, 
				string strAddress, string strCity, string strZip, string strStateCode, 
				string strOrganizerName, string strDayPhone, string strEveningPhone, 
				string strFaxPhone, string strTitle, string strPayableName, 
				string strOrganizerEmail, string strExternalOrgURL, int intExternalOrgID, 
				int intExternalGroupID, int intLeadID, int intOrgCreationChannelID, 
				string strPassword, int intNumberParticipants, int intFinancialGoal, 
				int blnWillingToPurchase, string strFundraisingReason, 
				string strCultureCode) {

			DataParameters[] parameters = new DataParameters[26];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intPartnerID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = intPartnerID;			
			
			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@intOrganizationID";
			parameters[1].DataType = DbType.Int32;
			parameters[1].ParamDirection = ParameterDirection.Input;
			if(intOrganizationID == -1) {
				parameters[1].Value = DBNull.Value;
			} else {
				parameters[1].Value = intOrganizationID;
			}

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@strCampaignName";
			parameters[2].DataType = DbType.String;
			parameters[2].ParamDirection = ParameterDirection.Input;
			parameters[2].Value = strCampaignName;

			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@strAddress";
			parameters[3].DataType = DbType.String;
			parameters[3].ParamDirection = ParameterDirection.Input;
			if(strAddress == "") {
				parameters[3].Value = DBNull.Value;
			} else {
				parameters[3].Value = strAddress;
			}

			parameters[4] = new DataParameters();
			parameters[4].ParameterName = "@strCity";
			parameters[4].DataType = DbType.String;
			parameters[4].ParamDirection = ParameterDirection.Input;
			if(strCity == "") {
				parameters[4].Value = DBNull.Value;
			} else {
				parameters[4].Value = strCity;
			}

			parameters[5] = new DataParameters();
			parameters[5].ParameterName = "@strZip";
			parameters[5].DataType = DbType.String;
			parameters[5].ParamDirection = ParameterDirection.Input;
			if(strZip == "") {
				parameters[5].Value = DBNull.Value;
			} else {
				parameters[5].Value = strZip;
			}

			parameters[6] = new DataParameters();
			parameters[6].ParameterName = "@strStateCode";
			parameters[6].DataType = DbType.String;
			parameters[6].ParamDirection = ParameterDirection.Input;
			if(strStateCode == "") {
				parameters[6].Value = DBNull.Value;
			} else {
				parameters[6].Value = strStateCode;
			}

			parameters[7] = new DataParameters();
			parameters[7].ParameterName = "@strOrganizerName";
			parameters[7].DataType = DbType.String;
			parameters[7].ParamDirection = ParameterDirection.Input;
			parameters[7].Value = strOrganizerName;

			parameters[8] = new DataParameters();
			parameters[8].ParameterName = "@strDayPhone";
			parameters[8].DataType = DbType.String;
			parameters[8].ParamDirection = ParameterDirection.Input;
			if(strDayPhone == "") {
				parameters[8].Value = DBNull.Value;
			} else { 
				parameters[8].Value = strDayPhone;
			}

			parameters[9] = new DataParameters();
			parameters[9].ParameterName = "@strEveningPhone";
			parameters[9].DataType = DbType.String;
			parameters[9].ParamDirection = ParameterDirection.Input;
			if(strEveningPhone == "") {
				parameters[9].Value = DBNull.Value;
			} else {
				parameters[9].Value = strEveningPhone;
			}

			parameters[10] = new DataParameters();
			parameters[10].ParameterName = "@strFaxPhone";
			parameters[10].DataType = DbType.String;
			parameters[10].ParamDirection = ParameterDirection.Input;
			if(strFaxPhone == "") {
				parameters[10].Value = DBNull.Value;
			} else {
				parameters[10].Value = strFaxPhone;
			}

			parameters[11] = new DataParameters();
			parameters[11].ParameterName = "@strTitle";
			parameters[11].DataType = DbType.String;
			parameters[11].ParamDirection = ParameterDirection.Input;
			if(strTitle == "") {
				parameters[11].Value = DBNull.Value;
			} else {
				parameters[11].Value = strTitle;
			}

			parameters[12] = new DataParameters();
			parameters[12].ParameterName = "@strPayableName";
			parameters[12].DataType = DbType.String;
			parameters[12].ParamDirection = ParameterDirection.Input;
			if(strPayableName == "") {
				parameters[12].Value = DBNull.Value;
			} else {
				parameters[12].Value = strPayableName;
			}

			parameters[13] = new DataParameters();
			parameters[13].ParameterName = "@strOrganizerEmail";
			parameters[13].DataType = DbType.String;
			parameters[13].ParamDirection = ParameterDirection.Input;
			parameters[13].Value = strOrganizerEmail;

			parameters[14] = new DataParameters();
			parameters[14].ParameterName = "@intExternalOrgURL";
			parameters[14].DataType = DbType.String;
			parameters[14].ParamDirection = ParameterDirection.Input;
			if(strExternalOrgURL == "") {
				parameters[14].Value = DBNull.Value;
			} else {
				parameters[14].Value = strExternalOrgURL;
			}

			parameters[15] = new DataParameters();
			parameters[15].ParameterName = "@intExternalOrgID";
			parameters[15].DataType = DbType.Int32;
			parameters[15].ParamDirection = ParameterDirection.Input;
			if(intExternalOrgID == -1) {
				parameters[15].Value = DBNull.Value;
			} else {
				parameters[15].Value = intExternalOrgID;
			}

			parameters[16] = new DataParameters();
			parameters[16].ParameterName = "@intExternalGroupID";
			parameters[16].DataType = DbType.Int32;
			parameters[16].ParamDirection = ParameterDirection.Input;
			if(intExternalGroupID == -1) {
				parameters[16].Value = DBNull.Value;
			} else {
				parameters[16].Value = intExternalGroupID;
			}

			parameters[17] = new DataParameters();
			parameters[17].ParameterName = "@intLeadID";
			parameters[17].DataType = DbType.Int32;
			parameters[17].ParamDirection = ParameterDirection.Input;
//			if(intLeadID == -1) {
//				parameters[17].Value = DBNull.Value;
//			} else {
				parameters[17].Value = intLeadID;
//			}

			parameters[18] = new DataParameters();
			parameters[18].ParameterName = "@intOrgCreationChannelID";
			parameters[18].DataType = DbType.Int32;
			parameters[18].ParamDirection = ParameterDirection.Input;
			if(intOrgCreationChannelID == -1) {
				parameters[18].Value = DBNull.Value;
			} else {
				parameters[18].Value = intOrgCreationChannelID;
			}

			parameters[19] = new DataParameters();
			parameters[19].ParameterName = "@strPassword";
			parameters[19].DataType = DbType.String;
			parameters[19].ParamDirection = ParameterDirection.Input;
			if(strPassword == "") {
				parameters[19].Value = DBNull.Value;
			} else {
				parameters[19].Value = strPassword;
			}

			parameters[20] = new DataParameters();
			parameters[20].ParameterName = "@intNumberParticipants";
			parameters[20].DataType = DbType.Int32;
			parameters[20].ParamDirection = ParameterDirection.Input;
			if(intNumberParticipants == -1) {
				parameters[20].Value = 1;
			} else {
				parameters[20].Value = intNumberParticipants;
			}

			parameters[21] = new DataParameters();
			parameters[21].ParameterName = "@intFinancialGoal";
			parameters[21].DataType = DbType.Int32;
			parameters[21].ParamDirection = ParameterDirection.Input;
			if(intFinancialGoal == -1) {
				parameters[21].Value = 1;
			} else {
				parameters[21].Value = intFinancialGoal;
			}

			parameters[22] = new DataParameters();
			parameters[22].ParameterName = "@blnWillingToPurchase";
			parameters[22].DataType = DbType.Byte;
			parameters[22].ParamDirection = ParameterDirection.Input;
			if(blnWillingToPurchase == -1) {
				parameters[22].Value = 1;
			} else {
				parameters[22].Value = blnWillingToPurchase;
			}

			parameters[23] = new DataParameters();
			parameters[23].ParameterName = "@strFundraisingReason";
			parameters[23].DataType = DbType.String;
			parameters[23].ParamDirection = ParameterDirection.Input;
			if(strFundraisingReason == "") {
				parameters[23].Value = DBNull.Value;
			} else {
				parameters[23].Value = strFundraisingReason;
			}

			parameters[24] = new DataParameters();
			parameters[24].ParameterName = "@strCultureCode";
			parameters[24].DataType = DbType.String;
			parameters[24].ParamDirection = ParameterDirection.Input;
			parameters[24].Value = strCultureCode;

			parameters[25] = new DataParameters();
			parameters[25].DataType = DbType.Int32;
			parameters[25].ParamDirection = ParameterDirection.ReturnValue;

			DatabaseInterface dbi = new DatabaseInterface();
			DataTable tbl = null;
			try {
				tbl = dbi.ExecuteFetchDataTable("es_create_campaign", parameters);
				campaignID = (int)parameters[25].Value;
			} catch(System.Exception ex) {
				throw ex;
			}
			return tbl;	// 
		}

		#endregion

		public DataTable GetParticipantListToLaunch(int campaign_id) {
			DataParameters[] parameters = new DataParameters[1];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@campaign_id";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = campaign_id;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_get_participant_list_to_launch", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
			return dt;
		}

		public void UpdateParticipantActive(int participant_id) {
			DataParameters[] parameters = new DataParameters[1];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@participant_id";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = participant_id;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			try {
				dbi.ExecuteNonQuery("es_update_participant_active", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
		}

		//method that gets the partnerID, the partnerName and the campaignType
		public DataSet GetPartnerInfoByGUID(string guid) {

			guid = guid.Replace("{","");
			guid = guid.Replace("}",""); 
			guid = guid.Trim();

			DataSet ds = new DataSet();
			try {
				//Mode -->	emgSecurityMode
				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@strGUID";
				parameters[0].DataType = DbType.String;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = guid;
				
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
				
				ds = dbi.ExecuteFetchDataSet(CommandType.StoredProcedure, "es_get_partner_info_from_guid", 
					parameters);
				
			}
			catch(Exception e) {
				throw e;
			}
			
			return ds;
			
		}

		//method that gets the partnerID, the partnerName and the campaignType
		public DataSet GetPartnerInfo(int supporterID) {

			DataSet ds = new DataSet();
			try {
				//Mode -->	emgSecurityMode
				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intSupporterID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = supporterID;
				
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
				
				ds = dbi.ExecuteFetchDataSet(CommandType.StoredProcedure, "es_get_partner_info_from_supporter_id", 
					parameters);
				

			}
			catch(Exception e) {
				throw e;
			}
			
			return ds;
			
		}

		//method that will get the campaignID for a supporter
		public int GetCampaignIDForSupporter(int ID) {
			int campaignID = 0;
			try {
				//Mode -->	emgSecurityMode
				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intSupporterID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = ID;
				
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
				
				campaignID = Convert.ToInt32(dbi.ExecuteScalar(CommandType.StoredProcedure, "es_get_campaign_id_for_supporter", 
					parameters));
			

			}
			catch(Exception e) {
				throw e;
			}
			
			return campaignID;
			
		}

		//get the emails corrresponding to a campaign
		//if emailTypeID is specified, one email is returned
		//if no emailTypeID is specified, we return all emails (3)
		public DataTable GetEmailByType(int EmailTypeID, int culture) {
			DataTable dt = new DataTable();
			try {
				DataParameters[] parameters = new DataParameters[2];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intEmailTypeID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = EmailTypeID;

				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@culture_id";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = culture;

				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
				
				dt = dbi.ExecuteFetchDataTable ("es_get_emails_by_type", parameters);
			} catch(Exception e) {
				throw e;
			}			
			return dt;
		}

		//insert a new supporter for a partcipant (new way)
		public int InsertSupporter(int participantID, string name, string email, string stateCode,
			string relationDesc,bool isDefault,bool isParent, int channelCode, int identification) {

			int supporterID = 0;

			try {
				DataParameters[] parameters = new DataParameters[9];

				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intParticipantID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = participantID;
				
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@strFullName";
				parameters[1].DataType = DbType.String;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = name;
				
				parameters[2] = new DataParameters();
				parameters[2].ParameterName = "@strEmail";
				parameters[2].DataType = DbType.String;
				parameters[2].ParamDirection = ParameterDirection.Input;
				parameters[2].Value = email;
				
//				parameters[3] = new DataParameters();
//				parameters[3].ParameterName = "@strStateCode";
//				parameters[3].DataType = DbType.String;
//				parameters[3].ParamDirection = ParameterDirection.Input;
//				parameters[3].Value = stateCode;

				parameters[3] = new DataParameters();
				parameters[3].ParameterName = "@strRelationDesc";
				parameters[3].DataType = DbType.String;
				parameters[3].ParamDirection = ParameterDirection.Input;
				parameters[3].Value = relationDesc;
				
				parameters[4] = new DataParameters();
				parameters[4].ParameterName = "@bitIsDefault";
				parameters[4].DataType = DbType.Boolean;
				parameters[4].ParamDirection = ParameterDirection.Input;
				parameters[4].Value = isDefault;

				parameters[5] = new DataParameters();
				parameters[5].ParameterName = "@bitIsParent";
				parameters[5].DataType = DbType.Boolean;
				parameters[5].ParamDirection = ParameterDirection.Input;
				parameters[5].Value = isParent;

				parameters[6] = new DataParameters();
				parameters[6].ParameterName = "@intCreationChannelID";
				parameters[6].DataType = DbType.Int32;
				parameters[6].ParamDirection = ParameterDirection.Input;
				parameters[6].Value = channelCode;

				parameters[7] = new DataParameters();
				parameters[7].ParameterName = "@intIdentification";
				parameters[7].DataType = DbType.Int32;
				parameters[7].ParamDirection = ParameterDirection.Input;
				if (identification == 0){
					parameters[7].Value =  DBNull.Value;
				}else{
					parameters[7].Value = identification;
				}
				
				parameters[8] = new DataParameters();
				parameters[8].ParamDirection = ParameterDirection.ReturnValue;
				
				

				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
				
				dbi.ExecuteNonQuery(CommandType.StoredProcedure, "es_insert_supporter", parameters);
				
				supporterID = Convert.ToInt32(parameters[8].Value);

			}
			catch(Exception e) {
				throw e;
			}
			
			return supporterID;
			
		}

		public DataTable GetCampaignDetails(int campaignID) {
			DataTable dt = new DataTable();
			try {
				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@Campaign_ID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = campaignID;
				
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
				dt = dbi.ExecuteFetchDataTable ("sp_GetCampaignDetail", parameters);
			}
			catch(Exception e) {
				throw e;
			}
			return dt;
		}
			
		//fill in a table with all the tags that an email can contain with its corresponding
		//value depending on the supporter
		public DataTable GetTags(int supporterID) {
			DataTable dt = new DataTable();
			try {
				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@supporter_id";
				parameters[0].DataType = DbType.String;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = supporterID;
				
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
				
				dt = dbi.ExecuteFetchDataTable("sp_getTagsToReplace", parameters);
			} catch(Exception e) {
				throw e;
			}
			return dt;
		}

		public bool InsertEmail(int SupporterID, string SupporterName, string Subject, string Body, string Email, string PartName, string PartEmail, DateTime DateToLaunch ) { 
			return this.InsertEmailMassMailer(SupporterID, SupporterName, Subject, Body, Email, PartName, PartEmail, DateToLaunch);
			//return obj.InsertEmailMassMailer(SupporterID, SupporterName, Subject, Body, Email, PartName, PartEmail, DateToLaunch);
		} 
		
		public bool InsertEmailMassMailer(int SupporterID, string SupporterName, string Subject, string Body, string Email, string PartName, string PartEmail, DateTime DateToLaunch) { 
			DataParameters[] parameters = new DataParameters[10];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@strBounceEmail";
			parameters[0].DataType = DbType.String;
			parameters[0].ParamDirection = ParameterDirection.Input;
            parameters[0].Value = "online@fundraising.com";

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@strSubject";
			parameters[1].DataType = DbType.String;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = Subject;

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@strBody";
			parameters[2].DataType = DbType.String;
			parameters[2].ParamDirection = ParameterDirection.Input;
			parameters[2].Value = Body;

			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@strName";
			parameters[3].DataType = DbType.String;
			parameters[3].ParamDirection = ParameterDirection.Input;
			parameters[3].Value = SupporterName;

			parameters[4] = new DataParameters();
			parameters[4].ParameterName = "@strEmail";
			parameters[4].DataType = DbType.String;
			parameters[4].ParamDirection = ParameterDirection.Input;
			parameters[4].Value = Email;

			parameters[5] = new DataParameters();
			parameters[5].ParameterName = "@intSupporterID";
			parameters[5].DataType = DbType.Int32;
			parameters[5].ParamDirection = ParameterDirection.Input;
			parameters[5].Value = SupporterID;

			parameters[6] = new DataParameters();
			parameters[6].ParameterName = "@strBodyFormat";
			parameters[6].DataType = DbType.String;
			parameters[6].ParamDirection = ParameterDirection.Input;
			parameters[6].Value = "text";

			parameters[7] = new DataParameters();
			parameters[7].ParameterName = "@strFromEmail";
			parameters[7].DataType = DbType.String;
			parameters[7].ParamDirection = ParameterDirection.Input;
			parameters[7].Value = PartEmail;

			parameters[8] = new DataParameters();
			parameters[8].ParameterName = "@dteDateToLaunch";
			parameters[8].DataType = DbType.DateTime;
			parameters[8].ParamDirection = ParameterDirection.Input;
			parameters[8].Value = DateToLaunch;

			parameters[9] = new DataParameters();
			parameters[9].ParameterName = "@strFromName";
			parameters[9].DataType = DbType.String;
			parameters[9].ParamDirection = ParameterDirection.Input;
			parameters[9].Value = PartName;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			
			try {
				dbi.ExecuteScalar("MassMailer.dbo.es_insert_email", parameters);
			} catch(System.Exception ex) {
				LoggingSystem.LogError(ex.Message);
				return false;
			}
			return true;
		}

		public int GetDefaultParticipantFromCampaignID(int campaignID) {
			int participantID = 0;
			try {
				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intCampaignID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = campaignID;
				
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
				
				participantID = Convert.ToInt32(dbi.ExecuteScalar(CommandType.StoredProcedure,"es_get_default_participant", parameters));
				
			}catch (Exception ex) {
				throw ex;
			}
			return participantID;
			
		}
		
		/// <summary>
		/// Gest the states
		/// </summary>
		/// <param name="country">Country code</param>
		/// <returns></returns>
		public DataSet GetStates(int partnerId, string country) {
			DataSet ds = null;
			try {
				DataParameters[] parameters = new DataParameters[2];

				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intPartnerID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = partnerId;

				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@strCountryCode";
				parameters[1].DataType = DbType.String;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = country;

				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());

				ds = dbi.ExecuteFetchDataSet(CommandType.StoredProcedure, 
					"es_get_states_by_partner_id", parameters); 
			}
			catch(Exception e) {
				throw e;
			}

			return ds;
		}

		public DataSet GetStatesByCountry(string strCountryCode) {
			DataParameters[] parameters = new DataParameters[1];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@strCountryCode";
			parameters[0].DataType = DbType.String;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = strCountryCode;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataSet dt = null;
			try {
				dt = dbi.ExecuteFetchDataSet("get_states_provinces_by_country", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
			return dt;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partnerID"></param>
		/// <param name="extOrgID"></param>
		/// <returns></returns>
		public int GetCampaignIDFromOrgID(int partnerID, int extOrgID) 
		{
			DataSet ds = null;
			int campaignID = 0;
			try 
			{
				DataParameters[] parameters = new DataParameters[3];

				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intPartnerID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = partnerID;

				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@intExternalOrgID";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = extOrgID;

				parameters[2] = new DataParameters();
				parameters[2].ParameterName = "@intExternalGroupID";
				parameters[2].DataType = DbType.Int32;
				parameters[2].ParamDirection = ParameterDirection.Input;
				parameters[2].Value = DBNull.Value;

				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());

				ds = dbi.ExecuteFetchDataSet(CommandType.StoredProcedure, "es_get_campaign_id_from_partner_info", parameters); 

				if (ds.Tables[0].Rows.Count > 0 )
				{
					campaignID = Convert.ToInt32(ds.Tables[0].Rows[0]["campaign_ID"]);
				}

				// ds = dbi.ExecuteFetchDataSet(CommandType.StoredProcedure, 
				//	"get_states_provinces_by_country", parameters); 
			}
			catch(Exception e) 
			{
				throw e;
			}

			return campaignID;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="partnerID"></param>
		/// <param name="extOrgID"></param>
		/// <returns></returns>
		public int GetCampaignIDFromOrgID(int partnerID, int extOrgID, out bool pIsUpdatable) {
			DataSet ds = null;
			pIsUpdatable = false;
			int campaignID = 0;
			try {
				DataParameters[] parameters = new DataParameters[3];

				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intPartnerID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = partnerID;

				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@intExternalOrgID";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = extOrgID;

				parameters[2] = new DataParameters();
				parameters[2].ParameterName = "@intExternalGroupID";
				parameters[2].DataType = DbType.Int32;
				parameters[2].ParamDirection = ParameterDirection.Input;
				parameters[2].Value = DBNull.Value;

				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());

				ds = dbi.ExecuteFetchDataSet(CommandType.StoredProcedure, "es_get_campaign_id_from_partner_info", parameters); 

				if (ds.Tables[0].Rows.Count > 0 ){
					campaignID = Convert.ToInt32(ds.Tables[0].Rows[0]["campaign_ID"]);
					if(ds.Tables[0].Columns.Contains("to_update") &&
						int.Parse(ds.Tables[0].Rows[0]["to_update"].ToString()) == 1)
						pIsUpdatable = true;
				}

				// ds = dbi.ExecuteFetchDataSet(CommandType.StoredProcedure, 
				//	"get_states_provinces_by_country", parameters); 
			}
			catch(Exception e) {
				throw e;
			}

			return campaignID;
		}

		/// <summary>
		/// Search groups by group name.
		/// </summary>
		/// <param name="groupName">Group Name</param>
		/// <param name="partnerID">Partner ID</param>
		/// <returns></returns>
		public DataSet FindGroup(string groupName, int partnerID) 
		{
			DataSet ds = new DataSet();
			try 
			{
				DataParameters[] parameters = new DataParameters[2];
			
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@search_value";
				parameters[0].DataType = DbType.String;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = groupName;
			
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@partner_id";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = partnerID;

//				ds = dbi.ExecuteFetchDataSet(CommandType.StoredProcedure, 
//					"emagnet.dbo.es_get_group_names", parameters);

				ds = dbi.ExecuteFetchDataSet(CommandType.StoredProcedure, 
					"es_get_group_names", parameters);
			}
			catch(Exception e) 
			{
				throw e;
			}

			return ds;
		}


		/// <summary>
		/// Finds the group
		/// </summary>
		/// <param name="partnerID">Partner ID</param>
		/// <param name="countryCode">Country Code</param>
		/// <param name="campaignName">Campaign Name</param>
		/// <param name="stateCode">State Code</param>
		/// <param name="campaignType">Campaign Type</param>
		/// <returns></returns>
		public DataSet FindGroup(int partnerID, string countryCode, 
			string campaignName, string stateCode, int campaignType) {
			DataSet ds = null;
			try {
				DataParameters[] parameters = new DataParameters[5];
			
				//  P A R T N E R  I D
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intPartnerID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = partnerID;
			
				//  C O U N T R Y  C O D E
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@strCountryCode";
				parameters[1].DataType = DbType.String;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = countryCode;

				//  C A M P A I G N  N A M E
				parameters[2] = new DataParameters();
				parameters[2].ParameterName = "@strCampaignName";
				parameters[2].DataType = DbType.String;
				parameters[2].ParamDirection = ParameterDirection.Input;
			
				if (campaignName == "") {
					parameters[2].Value = DBNull.Value;
				} else {
					parameters[2].Value = campaignName;
				}

				//  S T A T E  C O D E
				parameters[3] = new DataParameters();
				parameters[3].ParameterName = "@strStateCode";
				parameters[3].DataType = DbType.String;
				parameters[3].ParamDirection = ParameterDirection.Input;

				if (stateCode == "ALL") {
					parameters[3].Value = DBNull.Value;
				}
				else {
					parameters[3].Value = stateCode;
				}
			
				//  C A M P A I G N  T Y P E
				parameters[4] = new DataParameters();
				parameters[4].ParameterName = "@intCampaignType";
				parameters[4].DataType = DbType.Int16;
				parameters[4].ParamDirection = ParameterDirection.Input;
				parameters[4].Value = campaignType;

				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			
				ds = dbi.ExecuteFetchDataSet(CommandType.StoredProcedure, 
					"dbo.es_search_campaigns", parameters);
			} catch(Exception e) {
				throw e;
			}

			return ds;
		}

		public void IdentifyVisitor(int visitorLogID, int identTypeID, int supporterID) {
			try {
				DataParameters[] parameters = new DataParameters[3];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intVisitorsLogID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = visitorLogID;
				
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@intIdentTypeID";
				parameters[1].DataType = DbType.Int32;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = identTypeID;
				
				parameters[2] = new DataParameters();
				parameters[2].ParameterName = "@intIdentification";
				parameters[2].DataType = DbType.Int32;
				parameters[2].ParamDirection = ParameterDirection.Input;
				parameters[2].Value = supporterID;
				
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
				
				Convert.ToInt32(dbi.ExecuteNonQuery(CommandType.StoredProcedure,"web_tracking.dbo.identify_visitor", parameters));
				
			} catch (Exception ex) {
				throw ex;
			}
		
		} 

		public string GetFreeKitURL(int partherID) {
			string freeKitUrl;
			try {
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());

				string query = "select free_kit_url from partner where partner_id = " + partherID.ToString();

				DataSet ds = dbi.ExecuteFetchDataSet(CommandType.Text, query, null);

				freeKitUrl = ds.Tables[0].Rows[0]["free_kit_url"].ToString();

			} catch(Exception ex) {
				throw ex;
			}
			
			return freeKitUrl;
		}

		public DataTable GeteSubsInfoFromIdent(int intIdentification, int intIdentTypeID) {
			DataParameters[] parameters = new DataParameters[2];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intIdentification";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = intIdentification;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@intIdentTypeID";
			parameters[1].DataType = DbType.Int16;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = intIdentTypeID;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_get_esubs_info_from_ident", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
			return dt;
		}

		public DataTable GetOrganizerInfoFromCampaignID(int intCampaignID) {
			DataParameters[] parameters = new DataParameters[1];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intCampaignID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = intCampaignID;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_get_campaign_info", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
			return dt;
		}

		public DataTable GetPartnerInfoFromCampaignID(int intCampaignID) {
			DataParameters[] parameters = new DataParameters[1];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intCampaignID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = intCampaignID;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_get_partner_info_from_campaign_id", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
			return dt;
		}

		public DataTable GetSupporterInfo(int intSupporterID) {
			DataParameters[] parameters = new DataParameters[1];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intSupporterID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = intSupporterID;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_get_supporter_info", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
			return dt;
		}

		public DataTable GetSupporterSalesInfoByCampaignIDandParticipantID(int intCampaignID, int intParticipantID) {
			DataParameters[] parameters = new DataParameters[2];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intCampaignID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = intCampaignID;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@intParticipantID";
			parameters[1].DataType = DbType.Int32;
			parameters[1].ParamDirection = ParameterDirection.Input;
			if(intParticipantID == -1) {
				parameters[1].Value = DBNull.Value;
			} else {
				parameters[1].Value = intParticipantID;
			}

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_rpt_get_supporter_sales_info_by_camp", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
			return dt;
		}

		public bool UpdateCampaignInfo(int intCampaignID, string strOrganizerName, 
			string strGroupName, string strAddress, string strCity, string strStateCode, 
			string strZip, /*string strCountryCode,*/ string strOrganizerEmail, string strPhoneNumber, 
			int intNumberParticipants, int intFundraisingGoal, string strPassword,
			bool blnWillingToPurchase, string strEveningPhoneNumber, 
			string pPayableName, string pFundraisingReason) {
			DataParameters[] parameters = new DataParameters[17];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intCampaignID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = intCampaignID;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@strOrganizerName";
			parameters[1].DataType = DbType.String;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = strOrganizerName;

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@strGroupName";
			parameters[2].DataType = DbType.String;
			parameters[2].ParamDirection = ParameterDirection.Input;
			parameters[2].Value = strGroupName;

			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@strAddress";
			parameters[3].DataType = DbType.String;
			parameters[3].ParamDirection = ParameterDirection.Input;
			parameters[3].Value = strAddress;

			parameters[4] = new DataParameters();
			parameters[4].ParameterName = "@strCity";
			parameters[4].DataType = DbType.String;
			parameters[4].ParamDirection = ParameterDirection.Input;
			parameters[4].Value = strCity;

			parameters[5] = new DataParameters();
			parameters[5].ParameterName = "@strStateCode";
			parameters[5].DataType = DbType.String;
			parameters[5].ParamDirection = ParameterDirection.Input;
			parameters[5].Value = strStateCode;

			parameters[6] = new DataParameters();
			parameters[6].ParameterName = "@strZip";
			parameters[6].DataType = DbType.String;
			parameters[6].ParamDirection = ParameterDirection.Input;
			parameters[6].Value = strZip;

			/*
			parameters[7] = new DataParameters();
			parameters[7].ParameterName = "@strCountryCode";
			parameters[7].DataType = DbType.String;
			parameters[7].ParamDirection = ParameterDirection.Input;
			parameters[7].Value = strCountryCode; */

			parameters[7] = new DataParameters();
			parameters[7].ParameterName = "@strOrganizerEmail";
			parameters[7].DataType = DbType.String;
			parameters[7].ParamDirection = ParameterDirection.Input;
			parameters[7].Value = strOrganizerEmail;

			parameters[8] = new DataParameters();
			parameters[8].ParameterName = "@strPhoneNumber";
			parameters[8].DataType = DbType.String;
			parameters[8].ParamDirection = ParameterDirection.Input;
			parameters[8].Value = strPhoneNumber;

			parameters[9] = new DataParameters();
			parameters[9].ParameterName = "@intNumberParticipants";
			parameters[9].DataType = DbType.Int32;
			parameters[9].ParamDirection = ParameterDirection.Input;
			parameters[9].Value = intNumberParticipants;

			parameters[10] = new DataParameters();
			parameters[10].ParameterName = "@intFundraisingGoal";
			parameters[10].DataType = DbType.Int32;
			parameters[10].ParamDirection = ParameterDirection.Input;
			parameters[10].Value = intFundraisingGoal;

			parameters[11] = new DataParameters();
			parameters[11].ParameterName = "@strPassword";
			parameters[11].DataType = DbType.String;
			parameters[11].ParamDirection = ParameterDirection.Input;
			parameters[11].Value = strPassword;

			parameters[12] = new DataParameters();
			parameters[12].ParameterName = "@blnWillingToPurchase";
			parameters[12].DataType = DbType.Byte;
			parameters[12].ParamDirection = ParameterDirection.Input;
			if(blnWillingToPurchase) {
				parameters[12].Value = 1;
			} else {
				parameters[12].Value = 0;
			}

			parameters[13] = new DataParameters();
			parameters[13].ParameterName = "@strEveningPhone";
			parameters[13].DataType = DbType.String;
			parameters[13].ParamDirection = ParameterDirection.Input;
			parameters[13].Value = strEveningPhoneNumber;

			parameters[14] = new DataParameters();
			parameters[14].ParameterName = "@strPayableName";
			parameters[14].DataType = DbType.String;
			parameters[14].ParamDirection = ParameterDirection.Input;
			parameters[14].Value = pPayableName;

			parameters[15] = new DataParameters();
			parameters[15].ParameterName = "@strFundraisingReason";
			parameters[15].DataType = DbType.String;
			parameters[15].ParamDirection = ParameterDirection.Input;
			parameters[15].Value = pFundraisingReason;

			parameters[16] = new DataParameters();
			parameters[16].DataType = DbType.Int32;
			parameters[16].ParamDirection = ParameterDirection.ReturnValue;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			try {
				dbi.ExecuteScalar("es_update_campaign_info", parameters);
				int errorCode = int.Parse(parameters[16].Value.ToString());
				if(errorCode != 0) {
					return false;
				}
			} catch(System.Exception ex) {
				throw ex;
			}
			return true;
		}

		/// <summary>
		/// Update account info. Use this method to terminate campaign.
		/// </summary>
		/// <param name="intCampaignID">Campaign Id.</param>
		/// <param name="isOver">Is over flag.</param>
		/// <param name="strPayableName">Payable name.</param>
		/// <param name="strGroupName">Group name.</param>
		/// <returns>True if update succeeded, else false.</returns>
		public bool UpdateAccountInfo(int intCampaignID, bool isOver, 
			string strPayableName, string strGroupName) 
		{
			DataParameters[] parameters = new DataParameters[5];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intCampaignID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = intCampaignID;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@bitIsOver";
			parameters[1].DataType = DbType.Boolean;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = isOver;

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@strPayableName";
			parameters[2].DataType = DbType.String;
			parameters[2].ParamDirection = ParameterDirection.Input;
			parameters[2].Value = strPayableName;

			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@strGroupName";
			parameters[3].DataType = DbType.String;
			parameters[3].ParamDirection = ParameterDirection.Input;
			parameters[3].Value = strGroupName;

			parameters[4] = new DataParameters();
			parameters[4].ParamDirection = ParameterDirection.ReturnValue;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			try 
			{
				dbi.ExecuteNonQuery("es_update_account_info", parameters);

				int errorCode = (int) parameters[4].Value;
				if(errorCode != 0) 
				{
					return false;
				}
			} 
			catch(System.Exception ex) 
			{
				throw ex;
			}
			return true;
		}

		public int UpdateCampaignName(int campaignId, string campaignName) {
			try {
				DataParameters[] parameters = new DataParameters[3];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intCampaignId";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = campaignId;
				
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@strCampaignName ";
				parameters[1].DataType = DbType.String;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = campaignName;

				parameters[2] = new DataParameters();
				parameters[2].ParamDirection = ParameterDirection.ReturnValue;
				
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
				
				int result = dbi.ExecuteNonQuery(CommandType.StoredProcedure, "es_update_campaign_name", 
					parameters);

				int error = Convert.ToInt32(parameters[2].Value);

				return result;
			}
			catch(Exception e) {
				return -1;
			}
		}


		public DataTable GetLostPassword(string strUsername) {
			DataParameters[] parameters = new DataParameters[1];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@strUsername";
			parameters[0].DataType = DbType.String;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = strUsername;
			/*
						parameters[1].DataType = DbType.String;
						parameters[1].ParamDirection = ParameterDirection.ReturnValue;
			*/
			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable tb = null;
			try {
				tb = dbi.ExecuteFetchDataTable("es_get_lost_password", parameters); 
			} catch(System.Exception ex) {
				throw ex;
			}
			return tb;
		}



		public int AuthenticateCampaignManager(string strUsername, string strPassword) {
			DataParameters[] parameters = new DataParameters[3];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@strUsername";
			parameters[0].DataType = DbType.String;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = strUsername;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@strPassword";
			parameters[1].DataType = DbType.String;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = strPassword;

			parameters[2].DataType = DbType.Int32;
			parameters[2].ParamDirection = ParameterDirection.ReturnValue;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			try {
				dbi.ExecuteScalar("es_authenticate_campaign_manager", parameters); 
				return (int)parameters[2].Value;
			} catch(System.Exception ex) {
				throw ex;
			}
		}

		/// <summary>
		/// Method to get the eSubs Campaign ID from an Lead ID reference Number
		/// </summary>
		/// <param name="pPartnerID">Partner ID reference number</param>
		/// <param name="pExternalOrgID">Lead ID reference number must be pass here</param>
		/// <param name="pExternalGroupID"></param>
		/// <returns>Campaign ID reference number for the Lead ID passed</returns>
		/// <remarks>Louis Turmel - 21 June, 2005</remarks>
		public int GetCampaignIDFromLeadID(int pPartnerID, int pExternalOrgID, int pExternalGroupID) {
			int oCampaignID = -1;
			#region DataParameters
			
			DataParameters[] oParams = new DataParameters[3];
			
			oParams[0] = new DataParameters();
			oParams[0].DataType = DbType.Int32;
			oParams[0].ParamDirection = ParameterDirection.Input;
			oParams[0].ParameterName = "@intPartnerID";
			oParams[0].Value = pPartnerID;

			oParams[1] = new DataParameters();
			oParams[1].DataType = DbType.Int32;
			oParams[1].ParamDirection = ParameterDirection.Input;
			oParams[1].ParameterName = "@intExternalOrgID";
			if(pExternalOrgID == -1)
				oParams[1].Value = DBNull.Value;
			else
				oParams[1].Value = pExternalOrgID;

			oParams[2] = new DataParameters();
			oParams[2].DataType = DbType.Int32;
			oParams[2].ParamDirection = ParameterDirection.Input;
			oParams[2].ParameterName = "@intExternalGroupID";
			if(pExternalGroupID == -1)
				oParams[2].Value = DBNull.Value;
			else
				oParams[2].Value = pExternalGroupID;
			
			#endregion
			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			try {
				DataTable oDt = dbi.ExecuteFetchDataTable("es_get_campaign_id_from_lead_id", oParams);
				if(oDt.Rows.Count > 0)
					oCampaignID = int.Parse(oDt.Rows[0]["campaign_ID"].ToString());
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return oCampaignID;
		}

		public int GetCampaignIDFromEmail(string pEmail, int pPartnerID) {
			//es_get_campaign_from_email
 			//@strUsername VARCHAR(75) , @intPartner_ID INTEGER
			
			DataParameters[] parameters = new DataParameters[3];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@strUsername";
			parameters[0].DataType = DbType.String;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value	= pEmail;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@intPartner_ID";
			parameters[1].DataType = DbType.Int32;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = pPartnerID;

			parameters[2].DataType = DbType.Int32;
			parameters[2].ParamDirection = ParameterDirection.ReturnValue;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			try {
				dbi.ExecuteScalar("es_get_campaign_from_email", parameters);
				return (int)parameters[2].Value;
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				this.Close(); 
			}
		}

		public DataSet GetMainCampaignSectionInfo(int intCampaignID) {
			DataParameters[] parameters = new DataParameters[1];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intCampaignID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = intCampaignID;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataSet dt = null;
			try {
				dt = dbi.ExecuteFetchDataSet("es_get_main_camp_section_info", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
			return dt;
		}

		public DataTable GetBouncedParticipantEmails(int intCampaignID) {
			DataParameters[] parameters = new DataParameters[1];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intCampaignID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = intCampaignID;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_get_bounced_participant_emails", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
			return dt;
		}

		public int UnsubscribeEmail(int intIdentTypeID, int intIdentification, string strEmail, int intEmailTypeID) {
			DataParameters[] parameters = new DataParameters[5];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intIdentTypeID";
			parameters[0].DataType = DbType.Int16;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = intIdentTypeID;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@intIdentification";
			parameters[1].DataType = DbType.Int32;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = intIdentification;

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@strEmail";
			parameters[2].DataType = DbType.String;
			parameters[2].ParamDirection = ParameterDirection.Input;
			parameters[2].Value = strEmail;

			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@intEmailTypeID";
			parameters[3].DataType = DbType.Int32;
			parameters[3].ParamDirection = ParameterDirection.Input;
			if(intEmailTypeID == 0) {
				parameters[3].Value = DBNull.Value;
			} else {
				parameters[3].Value = intEmailTypeID;
			}

			parameters[4] = new DataParameters();
			parameters[4].DataType = DbType.Int32;
			parameters[4].ParamDirection = ParameterDirection.ReturnValue;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			int rowAffected = -1;

			try {
				dbi.ExecuteScalar("es_insert_unsubscribe_emails", parameters);
				rowAffected = Convert.ToInt32(parameters[4].Value); 
			} catch (System.Exception ex) {
				throw ex;
			}

			return rowAffected;
		}

		public DataTable UnsubscribeFromMailQueue(int identity_id, int ident_type_id) {
			DataParameters[] parameters = new DataParameters[2];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@identity_id";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = identity_id;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@ident_type_id";
			parameters[1].DataType = DbType.Int16;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = ident_type_id;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("unsubscribe_queue", parameters);
			} catch (System.Exception ex) {
				throw ex;
			}
			return dt;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Campaign_ID"></param>
		/// <param name="ident_type_id"></param>
		/// <returns></returns>
		public DataTable GetEmailTemplate(int Campaign_ID, int ident_type_id) {
			DataParameters[] parameters = new DataParameters[2];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@Campaign_ID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = Campaign_ID;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@ident_type_id";
			parameters[1].DataType = DbType.Int16;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = ident_type_id;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_email_preview", parameters);
				//dt = dbi.ExecuteFetchDataTable("es_email_template", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
			return dt;
		}

		public bool InsertEmail(byte identity_type_id, int identity_id, string from_name, string from_email, string to_name, string to_email, string bounce_email, string subject,  string body, DateTime date_to_launch) {
			DataParameters[] parameters = new DataParameters[10];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@identity_id";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = identity_id;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@identity_type_id";
			parameters[1].DataType = DbType.Int16;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = identity_type_id;

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@from_name";
			parameters[2].DataType = DbType.String;
			parameters[2].ParamDirection = ParameterDirection.Input;
			parameters[2].Value = from_name;

			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@from_email";
			parameters[3].DataType = DbType.String;
			parameters[3].ParamDirection = ParameterDirection.Input;
			parameters[3].Value = from_email;

			parameters[4] = new DataParameters();
			parameters[4].ParameterName = "@to_name";
			parameters[4].DataType = DbType.String;
			parameters[4].ParamDirection = ParameterDirection.Input;
			parameters[4].Value = to_name;

			parameters[5] = new DataParameters();
			parameters[5].ParameterName = "@to_email";
			parameters[5].DataType = DbType.String;
			parameters[5].ParamDirection = ParameterDirection.Input;
			parameters[5].Value = to_email;

			parameters[6] = new DataParameters();
			parameters[6].ParameterName = "@bounce_email";
			parameters[6].DataType = DbType.String;
			parameters[6].ParamDirection = ParameterDirection.Input;
			parameters[6].Value = bounce_email;

			parameters[7] = new DataParameters();
			parameters[7].ParameterName = "@subject";
			parameters[7].DataType = DbType.String;
			parameters[7].ParamDirection = ParameterDirection.Input;
			parameters[7].Value = subject;

			parameters[8] = new DataParameters();
			parameters[8].ParameterName = "@body";
			parameters[8].DataType = DbType.String;
			parameters[8].ParamDirection = ParameterDirection.Input;
			parameters[8].Value = body;

			parameters[9] = new DataParameters();
			parameters[9].ParameterName = "@date_to_launch";
			parameters[9].DataType = DbType.DateTime;
			parameters[9].ParamDirection = ParameterDirection.Input;
			parameters[9].Value = date_to_launch;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			try {
				dbi.ExecuteScalar("es_insert_email", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
			return true;
		}

		#region Web Tracking 
		/// <summary>
		/// Adds a new visitor
		/// </summary>
		/// <returns>Visitor Id</returns>
		public int AddVisitor() {
			DataParameters[] parameters = new DataParameters[1];
			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intVisitorID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = null;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());

			int visitor_id = -1;
			try {
				visitor_id = (int) dbi.ExecuteScalar("web_tracking.dbo.insert_visitor", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}

			return visitor_id;
		}

		/// <summary>
		/// updates the number of visits for that particular visitor
		/// Returns a visitor id
		/// </summary>
		/// <param name="visitorId"></param>
		/// <returns>Visitor Id</returns>
		public int AddVisitor(int visitorId) {
			DataParameters[] parameters = new DataParameters[1];
			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intVisitorID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = visitorId;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			int visitor_id = -1;
			
			try {
				visitor_id = (int) dbi.ExecuteScalar("web_tracking.dbo.insert_visitor", parameters);
			} catch(System.Exception ex) {
				throw ex;
			}
			return visitor_id;
		}

		/// <summary>
		/// Adds a visitor click
		/// </summary>
		/// <param name="visitorLogId"></param>
		/// <param name="clickableObjectId"></param>
		/// <param name="currentPageID"></param>
		/// <returns>status</returns>
		public int AddVisitorClick(int visitorLogId, int clickableObjectId, int currentPageID) {
			DataParameters[] parameters = new DataParameters[3];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intVisitorsLogID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = visitorLogId;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@intClickableObjID";
			parameters[1].DataType = DbType.Byte;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = clickableObjectId;

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@intCurrentWebpageID";
			parameters[2].DataType = DbType.Int32;
			parameters[2].ParamDirection = ParameterDirection.Input;
			parameters[2].Value = currentPageID;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			int i = 0;
			try {
				i = dbi.ExecuteNonQuery("web_tracking.dbo.insert_visitors_clicks", parameters);
			} catch(System.Exception ex) { 
				EnterpriseComponents.LoggingSystem.LogError("Unable to insert click: " + ex.Message + " VLOGID" + visitorLogId + " CLICKABLEOBJ=" + clickableObjectId + " PAGEID" + currentPageID);
				return -1;
			}
			return i;
		}

		#endregion

		public DataTable GetParticipantInfo(int participantID) {
			DataTable dt = new DataTable();
			try {
				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@Participant_ID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = participantID;
				
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(),
					GetDataProviderStringConfigKey());
				
				dt = dbi.ExecuteFetchDataTable("sp_GetParticipantDetail", parameters);			

			}
			catch(Exception e) {
				throw e;
			}
			
			return dt;
		}

		//insert a new participant for a campaign (new way)
		public int InsertParticipant(int campaignID, string name, string email, bool isDefault, string participantGroupName, int channelCode, int identification, bool isActive) {

			int partID = 0;

			try {
				DataParameters[] parameters = new DataParameters[9];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intCampaignID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = campaignID;
				
				parameters[1] = new DataParameters();
				parameters[1].ParameterName = "@strName";
				parameters[1].DataType = DbType.String;
				parameters[1].ParamDirection = ParameterDirection.Input;
				parameters[1].Value = name;
				
				parameters[2] = new DataParameters();
				parameters[2].ParameterName = "@strEmail";
				parameters[2].DataType = DbType.String;
				parameters[2].ParamDirection = ParameterDirection.Input;
				parameters[2].Value = email;
				
				parameters[3] = new DataParameters();
				parameters[3].ParameterName = "@bitIsDefault";
				parameters[3].DataType = DbType.Boolean;
				parameters[3].ParamDirection = ParameterDirection.Input;
				parameters[3].Value = isDefault;
				
				parameters[4] = new DataParameters();
				parameters[4].ParameterName = "@intCreationChannelID";
				parameters[4].DataType = DbType.Int32;
				parameters[4].ParamDirection = ParameterDirection.Input;
				parameters[4].Value = channelCode;
				
				parameters[5] = new DataParameters();
				parameters[5].ParameterName = "@intIdentification";
				parameters[5].DataType = DbType.Int32;
				parameters[5].ParamDirection = ParameterDirection.Input;
				if(identification == 0) {
					parameters[5].Value = DBNull.Value;
				} else {
					parameters[5].Value = identification;
				}
				
				parameters[6] = new DataParameters();
				parameters[6].ParameterName = "@strSubgroupName";
				parameters[6].DataType = DbType.String;
				parameters[6].ParamDirection = ParameterDirection.Input;
				parameters[6].Value = participantGroupName;

				parameters[7] = new DataParameters();
				parameters[7].ParameterName = "@bitIsActive";
				parameters[7].DataType = DbType.Boolean;
				parameters[7].ParamDirection = ParameterDirection.Input;
				parameters[7].Value = isActive;

				parameters[8] = new DataParameters();
				parameters[8].ParamDirection = ParameterDirection.ReturnValue;

				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(),
					GetDataProviderStringConfigKey());
				
				dbi.ExecuteNonQuery(CommandType.StoredProcedure, "es_insert_participant", parameters);
				
				partID = Convert.ToInt32( parameters[8].Value);

			}
			catch(Exception e) {
				throw e;
			}
			
			return partID;
			
		}

		public int GetCountEmailSent(int identification, int ident_type_id) {
			DataParameters[] parameters = new DataParameters[3];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@identification";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = identification;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@ident_type_id";
			parameters[1].DataType = DbType.Int16;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = ident_type_id;

			parameters[2] = new DataParameters();
			parameters[2].DataType = DbType.Int32;
			parameters[2].ParamDirection = ParameterDirection.ReturnValue;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(),
				GetDataProviderStringConfigKey());

			int emailCount = -1;
			try {
				dbi.ExecuteNonQuery("es_get_count_emails_sent", parameters);
				emailCount = int.Parse(parameters[2].Value.ToString());
			} catch(System.Exception ex) {
				throw ex;
			}

			return emailCount;
		}

		/// <summary>
		/// Insert lead visit when lead already exists (LeadIntegrator)
		/// </summary>
		/// <param name="lead_id">Lead ID</param>
		/// <param name="promotion_id">Promotion ID</param>
		/// <returns></returns>
		public int InsertLeadVisit(int lead_id, int promotion_id) {
			DataParameters[] parameters = new DataParameters[2];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@lead_id";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = lead_id;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@promotion_id";
			parameters[1].DataType = DbType.Int32;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = promotion_id;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataSet ds = null;
			int leadVisitId = -1;
			try {
				ds = dbi.ExecuteFetchDataSet("es_call_es_insert_lead_visit", parameters);
				if(ds.Tables[1] != null) {
					if(ds.Tables[1].Rows.Count > 0) {
						leadVisitId = int.Parse(ds.Tables[1].Rows[0]["lead_visit_id"].ToString());
					}
				}
			} catch(System.Exception ex) {
				throw new EnterpriseComponents.EnterpriseException(ex.Message, ex);
			}
			return leadVisitId;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pCampaignID"></param>
		/// <returns></returns>
		public int GetParticipantCountByCampaignID(int pCampaignID) {
			DataParameters[] parameters = new DataParameters[1];
			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@campaignID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = pCampaignID;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			int oParticipantCount = 0;
			try {
				oParticipantCount = Convert.ToInt32(dbi.ExecuteScalar(CommandType.Text, "SELECT dbo.fct_participant_count_by_camp(@campaignID)",parameters));
			}
			catch(System.Exception ex) {
				throw new EnterpriseComponents.EnterpriseException(ex.Message, ex);
			}
			return oParticipantCount;
		}

		public DataSet GetPartnerInfoByPartnerID(int pPartnerID) {
			//Mode -->	emgSecurityMode
			DataParameters[] parameters = new DataParameters[1];
			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intPartnerID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = pPartnerID;				
			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(),GetDataProviderStringConfigKey());
			DataSet ds = new DataSet();
			try {
				ds = dbi.ExecuteFetchDataSet("es_get_partner_info_from_partner_id", parameters);				
			} catch(Exception ex) {
				throw new EnterpriseComponents.EnterpriseException(ex.Message,ex);
			}
			finally {
				this.Close();				
			}	
			return ds;
		}

		/// <summary>
		/// Get link to store information.
		/// </summary>
		/// <param name="supporterID">Supporter ID</param>
		/// <returns>A DataTable containing Participant_Name, Supporter_Name, Account_Number, Gender, StoreID,
		/// Template_Desc_ID, Group_Name, Campaign_Type_ID,	Participant_Is_Default, Supporter_Is_Default,
		/// Supporter_Email</returns>
		public DataTable GetLinkToStore(int supporterID) 
		{
			DataTable dt = new DataTable();
			try 
			{
				DataParameters[] parameters = new DataParameters[1];
				parameters[0] = new DataParameters();
				parameters[0].ParameterName = "@intSupporterID";
				parameters[0].DataType = DbType.Int32;
				parameters[0].ParamDirection = ParameterDirection.Input;
				parameters[0].Value = supporterID;

				if (enableTransaction) 
				{
				
//					DataSet ds = dbi.ExecuteFetchDataSet(currentConnection, CommandType.StoredProcedure, 
//						ref currentTransaction, "emagnet.dbo.es_get_link_to_store", 
//						parameters);
					
					DataSet ds = dbi.ExecuteFetchDataSet(currentConnection, CommandType.StoredProcedure, 
						ref currentTransaction, "es_get_link_to_store", 
						parameters);

					return ds.Tables[0];
				}
				else 
				{
					// DataSet ds = dbi.ExecuteFetchDataSet("emagnet.dbo.es_get_link_to_store", parameters);
					DataSet ds = dbi.ExecuteFetchDataSet("es_get_link_to_store", parameters);
					return ds.Tables[0];
				}
			}
			catch(Exception e) 
			{
				throw new EnterpriseException(e.Message, e);
			}
			
			return dt;
		}

		
		public double GetTotalRaised() {
			double oTotal = 0;
			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataParameters[] parameters = new DataParameters[1];
			parameters[0].ParameterName = "@ValueName";
			parameters[0].DataType = DbType.String;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = "total_sales";
			try {
				oTotal = double.Parse(dbi.ExecuteScalar(CommandType.Text,"SELECT value FROM eSubs_Summary_Values WHERE value_Name = @ValueName", parameters).ToString());
			}
			catch(Exception ex) {
				throw new EnterpriseComponents.EnterpriseException(ex.Message,ex);
			}
			finally {
				this.Close();
			}
			return oTotal;
		}


		#region LeadIntegrator
		public int GetPromotionID(int partner_id) {
			DataParameters[] parameters = new DataParameters[1];
			int promotionID = -1;

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@partner_id";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = partner_id;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_get_promotion_self_registered", parameters);
				foreach(DataRow row in dt.Rows) {
					promotionID = (int)row["promotion_id"];
				}
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return promotionID;
		}

		public DataTable GetLeadFromOrganizer(string street, string day_phone, string evening_phone, string email) {
			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			
			DataParameters[] parameters = new DataParameters[4];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@street";
			parameters[0].DataType = DbType.String;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = street;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@day_phone";
			parameters[1].DataType = DbType.String;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = day_phone;

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@evening_phone";
			parameters[2].DataType = DbType.String;
			parameters[2].ParamDirection = ParameterDirection.Input;
			if(evening_phone != "" && evening_phone != "--") {

				parameters[2].Value = evening_phone;
			} else {
				parameters[2].Value = DBNull.Value;
			}

			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@email";
			parameters[3].DataType = DbType.String;
			parameters[3].ParamDirection = ParameterDirection.Input;
			parameters[3].Value = email;

			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_call_es_get_lead_doubles", parameters);
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return dt;
		}

		public int InsertNewLead(int promotion_id, string first_name, string last_name, string organization, string street_address, string city, string state_code, string country_code, string zip_code, string day_phone, string email, int numberOfParticipant) {
			DataParameters[] parameters = new DataParameters[12];
			int leadID = -1;

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@promotion_id";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = promotion_id;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@first_name";
			parameters[1].DataType = DbType.String;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = first_name;

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@last_name";
			parameters[2].DataType = DbType.String;
			parameters[2].ParamDirection = ParameterDirection.Input;
			parameters[2].Value = last_name;

			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@organization";
			parameters[3].DataType = DbType.String;
			parameters[3].ParamDirection = ParameterDirection.Input;
			parameters[3].Value = organization;

			parameters[4] = new DataParameters();
			parameters[4].ParameterName = "@street_address";
			parameters[4].DataType = DbType.String;
			parameters[4].ParamDirection = ParameterDirection.Input;
			parameters[4].Value = street_address;

			parameters[5] = new DataParameters();
			parameters[5].ParameterName = "@city";
			parameters[5].DataType = DbType.String;
			parameters[5].ParamDirection = ParameterDirection.Input;
			parameters[5].Value = city;

			parameters[6] = new DataParameters();
			parameters[6].ParameterName = "@state_code";
			parameters[6].DataType = DbType.String;
			parameters[6].ParamDirection = ParameterDirection.Input;
			parameters[6].Value = state_code;

			parameters[7] = new DataParameters();
			parameters[7].ParameterName = "@country_code";
			parameters[7].DataType = DbType.String;
			parameters[7].ParamDirection = ParameterDirection.Input;
			parameters[7].Value = country_code;

			parameters[8] = new DataParameters();
			parameters[8].ParameterName = "@zip_code";
			parameters[8].DataType = DbType.String;
			parameters[8].ParamDirection = ParameterDirection.Input;
			parameters[8].Value = zip_code;

			parameters[9] = new DataParameters();
			parameters[9].ParameterName = "@day_phone";
			parameters[9].DataType = DbType.String;
			parameters[9].ParamDirection = ParameterDirection.Input;
			parameters[9].Value = day_phone;

			parameters[10] = new DataParameters();
			parameters[10].ParameterName = "@email";
			parameters[10].DataType = DbType.String;
			parameters[10].ParamDirection = ParameterDirection.Input;
			parameters[10].Value = email;

			parameters[11] = new DataParameters();
			parameters[11].ParameterName = "@participant_count";
			parameters[11].DataType = DbType.Int32;
			parameters[11].ParamDirection = ParameterDirection.Input;
			parameters[11].Value = numberOfParticipant;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable leads = null;
			try {
				leads = dbi.ExecuteFetchDataTable("es_call_es_insert_new_lead", parameters);
				foreach(DataRow rLead in leads.Rows) {
					leadID = int.Parse(rLead[0].ToString());
				}
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return leadID;
		}

		public bool UpdateBounceEmail(int pIdentTypeID, string pNewName, string pNewEmail, int pIdentification) {
			
			DataParameters[] oParams = new DataParameters[5];
			oParams[0] = new DataParameters();
			oParams[0].ParameterName = "@intIdentTypeID";
			oParams[0].DataType = DbType.Int32;
			oParams[0].ParamDirection = ParameterDirection.Input;
			oParams[0].Value = pIdentTypeID;
			oParams[1] = new DataParameters();
			oParams[1].ParameterName = "@strNewName";
			oParams[1].DataType = DbType.String;
			oParams[1].ParamDirection = ParameterDirection.Input;
			oParams[1].Value = pNewName;
			oParams[2] = new DataParameters();
			oParams[2].ParameterName = "@strNewEmail";
			oParams[2].ParamDirection = ParameterDirection.Input;
			oParams[2].DataType = DbType.String;
			oParams[2].Value = pNewEmail;
			oParams[3] = new DataParameters();
			oParams[3].ParameterName = "@intIdentification";
			oParams[3].DataType = DbType.Int32;
			oParams[3].ParamDirection = ParameterDirection.Input;
			oParams[3].Value = pIdentification;
			oParams[4] = new DataParameters();
			oParams[4].DataType = DbType.Int32;
			oParams[4].ParamDirection = ParameterDirection.ReturnValue;

			DatabaseInterface dbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			bool oOk = false;
			try {
				dbi.ExecuteScalar("es_update_bounce_email", oParams);
				if(int.Parse(oParams[4].Value.ToString()) == 0)
					oOk = true;			 
			} catch(Exception ex) {
				throw new Exception("es_update_bounce_email return " + oParams[4].Value.ToString(), ex);
			} finally {
				this.Close();
			}
			return oOk;
		}

		public bool UpdateOrganizationLead(int lead_id, int organization_id) {
			DataParameters[] parameters = new DataParameters[3];
			bool ok = false;

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@lead_id";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = lead_id;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@organization_id";
			parameters[1].DataType = DbType.Int32;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = organization_id;

			parameters[2] = new DataParameters();
			parameters[2].DataType = DbType.Int32;
			parameters[2].ParamDirection = ParameterDirection.ReturnValue;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			try {
				dbi.ExecuteScalar("es_update_organization_lead", parameters);
				if(0 == (int)parameters[2].Value) {
					ok = true;
				}
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return ok;
		}

		public int InsertLeadActivity(int lead_id, int lead_activity_type_id) {
			DataParameters[] parameters = new DataParameters[2];
			int leadActivityID = -1;

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@lead_id";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = lead_id;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@lead_activity_type_id";
			parameters[1].DataType = DbType.Int32;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = lead_activity_type_id;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable leadActivitiesID = null;
			try {
				leadActivitiesID = dbi.ExecuteFetchDataTable("es_call_es_insert_lead_activity", parameters);
				foreach(DataRow rLeadAct in leadActivitiesID.Rows) {
					leadActivityID = int.Parse(rLeadAct[0].ToString());
				}
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return leadActivityID;
		}

		public DataTable GetConsultantInfo(int consultant_id) {
			DataParameters[] parameters = new DataParameters[1];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@consultant_id";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = consultant_id;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable dt = null;
			try {
				dt = dbi.ExecuteFetchDataTable("es_get_consultant_info", parameters);
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return dt;
		}
		#endregion

		#region Debug Creation Channels
		/// <summary>
		/// Returns "creation_channel_id" param.
		/// </summary>
		/// <param name="organizerID"></param>
		/// <returns></returns>
		public DataSet GetOrganizerCreationChannelFromOrganizerID(int organizerID) {
			DataSet ds = new DataSet();
			try {
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());

				string query = 
					"SELECT     creation_channel_id\r\n" +
					"FROM         dbo.efo_organizer\r\n" +
					"where organizer_id = " + organizerID.ToString() + "\r\n";
				ds = dbi.ExecuteFetchDataSet(CommandType.Text, query, null);
			} catch(Exception ex) {
				throw ex;
			}
			return ds;
		}

		/// <summary>
		/// Returns "participant_id, creation_channel_id" param.
		/// </summary>
		/// <param name="campaigID"></param>
		/// <returns></returns>
		public DataSet GetParticipantsIDFromCampaignID(int campaigID) {
			DataSet ds = new DataSet();
			try {
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());

				string query = 
					"		SELECT     participant_id, creation_channel_id\r\n" +
					"			FROM         dbo.efo_participant\r\n" +
					"								 WHERE     (campaign_id = " + campaigID.ToString() + ")\r\n" +
					"\r\n";
				ds = dbi.ExecuteFetchDataSet(CommandType.Text, query, null);
			} catch(Exception ex) {
				throw ex;
			}
			return ds;
		}

		/// <summary>
		/// Returns "supporter_id, creation_channel_id" param.
		/// </summary>
		/// <param name="participantID"></param>
		/// <returns></returns>
		public DataSet GetSupporterIDFromParticipantID(int participantID) {
			DataSet ds = new DataSet();
			try {
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());

				string query = 
					"SELECT     supporter_id, creation_channel_id\r\n" +
					"FROM         dbo.efo_supporter\r\n" +
					"where participant_id = " + participantID.ToString() + "\r\n";
				ds = dbi.ExecuteFetchDataSet(CommandType.Text, query, null);
			} catch(Exception ex) {
				throw ex;
			}
			return ds;
		}

		/// <summary>
		/// Returns "ident_type_id, creation_channel_desc" param.
		/// </summary>
		/// <param name="creationChannel"></param>
		/// <returns></returns>
		public DataSet GetCreationChannelDescription(int creationChannel) {
			DataSet ds = new DataSet();
			try {
				DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());

				string query = 
					"SELECT     ident_type_id, creation_channel_desc\r\n" +
					"FROM         dbo.creation_channels\r\n" +
					"WHERE creation_channel_id = " + creationChannel.ToString() + "\r\n";
				ds = dbi.ExecuteFetchDataSet(CommandType.Text, query, null);
			} catch(Exception ex) {
				throw ex;
			}
			return ds;
		}
		#endregion

		/// <summary>
		/// Function to get the Top 10 Campaign from a specific Partner
		/// </summary>
		/// <param name="pPartnerID">PartnerID to get the top 10 list</param>
		/// <returns></returns>
		public DataTable GetTop10(int pPartnerID) {
			//es_get_parter_top10
			DataParameters[] parameters = new DataParameters[1];
			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@partner_id";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = pPartnerID;
			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable oTop10Table = null;
			try {
				oTop10Table = dbi.ExecuteFetchDataTable("es_get_parter_top10", parameters);
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return oTop10Table;
		}	
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pCampaignId"></param>
		/// <param name="pExtOrgID"></param>
		/// <param name="pExtCampId"></param>
		/// <param name="pPartnerId"></param>
		/// <returns></returns>
		public DataTable GetTop10Members(int pCampaignId,int pExtOrgID, int pExtCampId,int pPartnerId) {
			DataParameters[] parameters = new DataParameters[4];
			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@campaign_id";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			if(pCampaignId == -1)
				parameters[0].Value = System.DBNull.Value;
			else
				parameters[0].Value = pCampaignId;
			
			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@ext_org_id";
			parameters[1].DataType = DbType.Int32;
			parameters[1].ParamDirection = ParameterDirection.Input;
			if(pExtOrgID == -1)
				parameters[1].Value = System.DBNull.Value;
			else
				parameters[1].Value = pExtOrgID;
			
			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@ext_camp_id";
			parameters[2].DataType = DbType.Int32;
			parameters[2].ParamDirection = ParameterDirection.Input;
			if(pExtCampId == -1)
				parameters[2].Value = System.DBNull.Value;
			else
				parameters[2].Value = pExtCampId;
			
			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@partner_id";
			parameters[3].DataType = DbType.Int32;
			parameters[3].ParamDirection = ParameterDirection.Input;
			parameters[3].Value = pPartnerId;

			DatabaseInterface dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			DataTable oTop10MembersTable = null;
			try {
				oTop10MembersTable = dbi.ExecuteFetchDataTable("es_get_group_top10", parameters);
			} catch(System.Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return oTop10MembersTable;
		}
	}
}
