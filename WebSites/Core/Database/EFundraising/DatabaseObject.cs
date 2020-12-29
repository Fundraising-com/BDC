//
//	April 20, 2005	-	Louis Turmel	-	New Functions added InsertNewLead
//	April 21, 2005	-	Louis Turmel	-	New Functions and Methods added
//	April 27, 2005	-	Louis Turmel	-	New Functions efr_rpt_partner_leads
//	June  27, 2005	-	Louis Turmel	-	Code Comments added
//

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Specialized;

using GA.BDC.Core.EnterpriseComponents;

namespace GA.BDC.Core.Database.efundraising {

	/// <summary>
	/// This class encapsulate all requests used by any efundraising PHP Projects
	/// </summary>
	/// <remarks>
	/// This class will use the connection string provided by your configuration.
	/// Make sure it matches and you have the correct rights.
	/// </remarks>
	public sealed class DatabaseObject : Database.DatabaseObjects	{

		#region constructor

		/// <summary>
		/// default class constructor
		/// </summary>
		public DatabaseObject() {
			
		}

		#endregion

		#region protected override function

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override string GetConnectionStringConfigKey() {
			return "efundraising.ConnectionString";
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override string GetDataProviderStringConfigKey() {
			return "efundraising.DataProvider";
		}

		#endregion

		/// <summary>
		/// Method used for Insert a new lead on www.efundrasing.com Web Site
		/// </summary>
		/// <param name="pFirstName">First Name of new Lead</param>
		/// <param name="pLastName">Last Name of new Lead</param>
		/// <param name="pEmail">Email address of new Lead</param>
		/// <param name="pAddress">Street Adreess</param>
		/// <param name="pCity">City</param>
		/// <param name="pState">State Code</param>
		/// <param name="pZip">Zip Code</param>
		/// <param name="pCountry">Country</param>
		/// <param name="pDayPhone">Day Phone Number</param>
		/// <param name="pEveningPhone">Evening Phone Number</param>
		/// <param name="pGroupSize">Number of Participant</param>
		/// <param name="pOrganization">Organization Name</param>
		/// <param name="pPromotionID">Promotion ID</param>
		/// <param name="pTitle">Title of the Lead</param>
		/// <param name="pEveningPhoneExt">Evening Phone Extention call</param>
		/// <param name="pDayPhoneExt">Day Phone Extention Call</param>
		/// <param name="pBestTimeToCall">Best time to call the lead person</param>
		/// <param name="pOrganizationTypeID">Organzation Type ID</param>
		/// <param name="pGroupTypeID">Group type ID</param>
		/// <param name="pFundraisingDate">Fundraising Date</param>
		/// <param name="pDecisionMaker"></param>
		/// <param name="pProductsInterestIn"></param>
		/// <param name="pOnEmailList"></param>
		/// <param name="pComments"></param>
		/// <remarks>efr_call_efr_insert_new_lead</remarks>
		public int InsertNewLead(string pFirstName, string pLastName, string pEmail, string pAddress, 
			string pCity, string pState, string pZip, string pCountry, string pDayPhone,  
			string pEveningPhone, int pGroupSize, string pOrganization, int pPromotionID, string pTitle, 
			string pEveningPhoneExt, string pDayPhoneExt, string pBestTimeToCall, int pOrganizationTypeID, 
			int pGroupTypeID, string pFundraisingDate, int pDecisionMaker, string pProductsInterestIn,
			bool pOnEmailList, string pComments, int leadStatusID) {
			
			#region DataParameters[] Definition

			//ok
			DataParameters[] oParameters = new DataParameters[26];

			oParameters[0] = new DataParameters();
			oParameters[0].ParameterName = "@first_name";
			oParameters[0].DataType = DbType.String;
			oParameters[0].ParamDirection = ParameterDirection.Input;
			oParameters[0].Value = pFirstName;

			//ok
			oParameters[1] = new DataParameters();
			oParameters[1].ParameterName = "@last_name";
			oParameters[1].DataType = DbType.String;
			oParameters[1].ParamDirection = ParameterDirection.Input;
			oParameters[1].Value = pLastName;

			//ok
			oParameters[2] = new DataParameters();
			oParameters[2].ParameterName = "@email";
			oParameters[2].DataType = DbType.String;
			oParameters[2].ParamDirection = ParameterDirection.Input;
			oParameters[2].Value = pEmail;

			//ok
			oParameters[3] = new DataParameters();
			oParameters[3].ParameterName = "@street_address";
			oParameters[3].DataType = DbType.String;
			oParameters[3].ParamDirection = ParameterDirection.Input;
			oParameters[3].Value = pAddress;

			//ok
			oParameters[4] = new DataParameters();
			oParameters[4].ParameterName = "@city";
			oParameters[4].DataType = DbType.String;
			oParameters[4].ParamDirection = ParameterDirection.Input;
			oParameters[4].Value = pCity;

			//ok
			oParameters[5] = new DataParameters();
			oParameters[5].ParameterName = "@state_code";
			oParameters[5].DataType = DbType.String;
			oParameters[5].ParamDirection = ParameterDirection.Input;
			oParameters[5].Value = pState;

			//ok
			oParameters[6] = new DataParameters();
			oParameters[6].ParameterName = "@zip_code";
			oParameters[6].DataType = DbType.String;
			oParameters[6].ParamDirection = ParameterDirection.Input;
			oParameters[6].Value = pZip;

			//ok
			oParameters[7] = new DataParameters();
			oParameters[7].ParameterName = "@country_code";
			oParameters[7].DataType = DbType.String;
			oParameters[7].ParamDirection = ParameterDirection.Input;
			oParameters[7].Value = pCountry;

			//ok
			oParameters[8] = new DataParameters();
			oParameters[8].ParameterName = "@day_phone";
			oParameters[8].DataType = DbType.String;
			oParameters[8].ParamDirection = ParameterDirection.Input;
			oParameters[8].Value = pDayPhone;

			//ok
			oParameters[9] = new DataParameters();
			oParameters[9].ParameterName = "@evening_phone";
			oParameters[9].DataType = DbType.String;
			oParameters[9].ParamDirection = ParameterDirection.Input;
			oParameters[9].Value = pEveningPhone;

			//ok
			oParameters[10] = new DataParameters();
			oParameters[10].ParameterName = "@participant_count";
			oParameters[10].DataType = DbType.Int32;
			oParameters[10].ParamDirection = ParameterDirection.Input;
			oParameters[10].Value = pGroupSize;

			//ok
			oParameters[11] = new DataParameters();
			oParameters[11].ParameterName = "@organization";
			oParameters[11].DataType = DbType.String;
			oParameters[11].ParamDirection = ParameterDirection.Input;
			oParameters[11].Value = pOrganization;

			//ok
			oParameters[12] = new DataParameters();
			oParameters[12].ParameterName = "@promotion_id";
			oParameters[12].DataType = DbType.Int32;
			oParameters[12].ParamDirection = ParameterDirection.Input;
			if(pPromotionID == -1)
				oParameters[12].Value = System.DBNull.Value;
			else
				oParameters[12].Value = pPromotionID;

			//ok
			oParameters[13] = new DataParameters();
			oParameters[13].ParameterName = "@title";
			oParameters[13].DataType = DbType.String;
			oParameters[13].ParamDirection = ParameterDirection.Input;
			oParameters[13].Value = pTitle;

			//ok
			oParameters[14] = new DataParameters();
			oParameters[14].ParameterName = "@evening_phone_ext";
			oParameters[14].DataType = DbType.String;
			oParameters[14].ParamDirection = ParameterDirection.Input;
			oParameters[14].Value = pEveningPhoneExt;

			//ok
			oParameters[15] = new DataParameters();
			oParameters[15].ParameterName = "@day_phone_ext";
			oParameters[15].DataType = DbType.String;
			oParameters[15].ParamDirection = ParameterDirection.Input;
			oParameters[15].Value = pDayPhoneExt;

			//ok
			oParameters[16] = new DataParameters();
			oParameters[16].ParameterName = "@best_time_to_call";
			oParameters[16].DataType = DbType.String;
			oParameters[16].ParamDirection = ParameterDirection.Input;
			oParameters[16].Value = pBestTimeToCall;

			//ok
			oParameters[17] = new DataParameters();
			oParameters[17].ParameterName = "@organization_type_id";
			oParameters[17].DataType = DbType.Int32;
			oParameters[17].ParamDirection = ParameterDirection.Input;
			oParameters[17].Value = pOrganizationTypeID;
            
			//ok
			oParameters[18] = new DataParameters();
			oParameters[18].ParameterName = "@group_type_id";
			oParameters[18].DataType = DbType.Int32;
			oParameters[18].ParamDirection = ParameterDirection.Input;
			oParameters[18].Value = pGroupTypeID;

			//ok
			oParameters[19] = new DataParameters();
			oParameters[19].ParameterName = "@fundraising_date";
			oParameters[19].DataType = DbType.String;
			oParameters[19].ParamDirection = ParameterDirection.Input;
			oParameters[19].Value = pFundraisingDate;

			//ok
			oParameters[20] = new DataParameters();
			oParameters[20].ParameterName = "@decision_maker";
			oParameters[20].DataType = DbType.Int32;
			oParameters[20].ParamDirection = ParameterDirection.Input;
			oParameters[20].Value = pDecisionMaker;

			//ok
			oParameters[21] = new DataParameters();
			oParameters[21].ParameterName = "@products_interest_in";
			oParameters[21].DataType = DbType.String;
			oParameters[21].ParamDirection = ParameterDirection.Input;
			oParameters[21].Value = pProductsInterestIn;

			//ok
			oParameters[22] = new DataParameters();
			oParameters[22].ParameterName = "@on_email_list";
			oParameters[22].DataType = DbType.Boolean;
			oParameters[22].ParamDirection = ParameterDirection.Input;
			oParameters[22].Value = pOnEmailList;
			
			//ok
			oParameters[23] = new DataParameters();
			oParameters[23].ParameterName = "@comments";
			oParameters[23].DataType = DbType.String;
			oParameters[23].ParamDirection = ParameterDirection.Input;
			oParameters[23].Value = pComments;

			oParameters[24] = new DataParameters();
			oParameters[24].ParameterName = "@lead_status_id";
			oParameters[24].DataType = DbType.Int32;
			oParameters[24].ParamDirection = ParameterDirection.Input;
			oParameters[24].Value = leadStatusID;

			oParameters[25] = new DataParameters();
			oParameters[25].ParamDirection = ParameterDirection.ReturnValue;
			oParameters[25].DataType = DbType.Int32;
				
			#endregion
			
			//efr_insert_new_lead			
			DatabaseInterface dbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			int oLeadId = -1;
			try {
				dbi.ExecuteNonQuery(CommandType.StoredProcedure,"efr_call_efr_insert_new_lead", oParameters);
				oLeadId = (int)oParameters[25].Value;
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return oLeadId;
		}

		public int InsertNewTempLead(string pFirstName, string pLastName, string pEmail, string pAddress, 
			string pCity, string pState, string pZip, string pCountry, string pDayPhone,  
			string pEveningPhone, int pGroupSize, string pOrganization, int pPromotionID, string pTitle, 
			string pEveningPhoneExt, string pDayPhoneExt, string pBestTimeToCall, int pOrganizationTypeID, 
			int pGroupTypeID, string pFundraisingDate, int pDecisionMaker, string pProductsInterestIn,
			bool pOnEmailList, string pComments, int leadStatusID) {
			//efr_insert_temp_lead
			#region DataParameters[] Definition

			//ok
			DataParameters[] oParameters = new DataParameters[26];

			oParameters[0] = new DataParameters();
			oParameters[0].ParameterName = "@first_name";
			oParameters[0].DataType = DbType.String;
			oParameters[0].ParamDirection = ParameterDirection.Input;
			oParameters[0].Value = pFirstName;

			//ok
			oParameters[1] = new DataParameters();
			oParameters[1].ParameterName = "@last_name";
			oParameters[1].DataType = DbType.String;
			oParameters[1].ParamDirection = ParameterDirection.Input;
			oParameters[1].Value = pLastName;

			//ok
			oParameters[2] = new DataParameters();
			oParameters[2].ParameterName = "@email";
			oParameters[2].DataType = DbType.String;
			oParameters[2].ParamDirection = ParameterDirection.Input;
			oParameters[2].Value = pEmail;

			//ok
			oParameters[3] = new DataParameters();
			oParameters[3].ParameterName = "@street_address";
			oParameters[3].DataType = DbType.String;
			oParameters[3].ParamDirection = ParameterDirection.Input;
			oParameters[3].Value = pAddress;

			//ok
			oParameters[4] = new DataParameters();
			oParameters[4].ParameterName = "@city";
			oParameters[4].DataType = DbType.String;
			oParameters[4].ParamDirection = ParameterDirection.Input;
			oParameters[4].Value = pCity;

			//ok
			oParameters[5] = new DataParameters();
			oParameters[5].ParameterName = "@state_code";
			oParameters[5].DataType = DbType.String;
			oParameters[5].ParamDirection = ParameterDirection.Input;
			oParameters[5].Value = pState;

			//ok
			oParameters[6] = new DataParameters();
			oParameters[6].ParameterName = "@zip_code";
			oParameters[6].DataType = DbType.String;
			oParameters[6].ParamDirection = ParameterDirection.Input;
			oParameters[6].Value = pZip;

			//ok
			oParameters[7] = new DataParameters();
			oParameters[7].ParameterName = "@country_code";
			oParameters[7].DataType = DbType.String;
			oParameters[7].ParamDirection = ParameterDirection.Input;
			oParameters[7].Value = pCountry;

			//ok
			oParameters[8] = new DataParameters();
			oParameters[8].ParameterName = "@day_phone";
			oParameters[8].DataType = DbType.String;
			oParameters[8].ParamDirection = ParameterDirection.Input;
			oParameters[8].Value = pDayPhone;

			//ok
			oParameters[9] = new DataParameters();
			oParameters[9].ParameterName = "@evening_phone";
			oParameters[9].DataType = DbType.String;
			oParameters[9].ParamDirection = ParameterDirection.Input;
			oParameters[9].Value = pEveningPhone;

			//ok
			oParameters[10] = new DataParameters();
			oParameters[10].ParameterName = "@participant_count";
			oParameters[10].DataType = DbType.Int32;
			oParameters[10].ParamDirection = ParameterDirection.Input;
			oParameters[10].Value = pGroupSize;

			//ok
			oParameters[11] = new DataParameters();
			oParameters[11].ParameterName = "@organization";
			oParameters[11].DataType = DbType.String;
			oParameters[11].ParamDirection = ParameterDirection.Input;
			oParameters[11].Value = pOrganization;

			//ok
			oParameters[12] = new DataParameters();
			oParameters[12].ParameterName = "@promotion_id";
			oParameters[12].DataType = DbType.Int32;
			oParameters[12].ParamDirection = ParameterDirection.Input;
			if(pPromotionID == -1)
				oParameters[12].Value = System.DBNull.Value;
			else
				oParameters[12].Value = pPromotionID;

			//ok
			oParameters[13] = new DataParameters();
			oParameters[13].ParameterName = "@title";
			oParameters[13].DataType = DbType.String;
			oParameters[13].ParamDirection = ParameterDirection.Input;
			oParameters[13].Value = pTitle;

			//ok
			oParameters[14] = new DataParameters();
			oParameters[14].ParameterName = "@evening_phone_ext";
			oParameters[14].DataType = DbType.String;
			oParameters[14].ParamDirection = ParameterDirection.Input;
			oParameters[14].Value = pEveningPhoneExt;

			//ok
			oParameters[15] = new DataParameters();
			oParameters[15].ParameterName = "@day_phone_ext";
			oParameters[15].DataType = DbType.String;
			oParameters[15].ParamDirection = ParameterDirection.Input;
			oParameters[15].Value = pDayPhoneExt;

			//ok
			oParameters[16] = new DataParameters();
			oParameters[16].ParameterName = "@best_time_to_call";
			oParameters[16].DataType = DbType.String;
			oParameters[16].ParamDirection = ParameterDirection.Input;
			oParameters[16].Value = pBestTimeToCall;

			//ok
			oParameters[17] = new DataParameters();
			oParameters[17].ParameterName = "@organization_type_id";
			oParameters[17].DataType = DbType.Int32;
			oParameters[17].ParamDirection = ParameterDirection.Input;
			oParameters[17].Value = pOrganizationTypeID;
            
			//ok
			oParameters[18] = new DataParameters();
			oParameters[18].ParameterName = "@group_type_id";
			oParameters[18].DataType = DbType.Int32;
			oParameters[18].ParamDirection = ParameterDirection.Input;
			oParameters[18].Value = pGroupTypeID;

			//ok
			oParameters[19] = new DataParameters();
			oParameters[19].ParameterName = "@fundraising_date";
			oParameters[19].DataType = DbType.String;
			oParameters[19].ParamDirection = ParameterDirection.Input;
			oParameters[19].Value = pFundraisingDate;

			//ok
			oParameters[20] = new DataParameters();
			oParameters[20].ParameterName = "@decision_maker";
			oParameters[20].DataType = DbType.Int32;
			oParameters[20].ParamDirection = ParameterDirection.Input;
			oParameters[20].Value = pDecisionMaker;

			//ok
			oParameters[21] = new DataParameters();
			oParameters[21].ParameterName = "@products_interest_in";
			oParameters[21].DataType = DbType.String;
			oParameters[21].ParamDirection = ParameterDirection.Input;
			oParameters[21].Value = pProductsInterestIn;

			//ok
			oParameters[22] = new DataParameters();
			oParameters[22].ParameterName = "@on_email_list";
			oParameters[22].DataType = DbType.Boolean;
			oParameters[22].ParamDirection = ParameterDirection.Input;
			oParameters[22].Value = pOnEmailList;
			
			//ok
			oParameters[23] = new DataParameters();
			oParameters[23].ParameterName = "@comments";
			oParameters[23].DataType = DbType.String;
			oParameters[23].ParamDirection = ParameterDirection.Input;
			oParameters[23].Value = pComments;

			oParameters[24] = new DataParameters();
			oParameters[24].ParameterName = "@lead_status_id";
			oParameters[24].DataType = DbType.Int32;
			oParameters[24].ParamDirection = ParameterDirection.Input;
			oParameters[24].Value = leadStatusID;

			oParameters[25] = new DataParameters();
			oParameters[25].ParamDirection = ParameterDirection.ReturnValue;
			oParameters[25].DataType = DbType.Int32;
				
			#endregion
			
			//efr_insert_new_lead			
			DatabaseInterface dbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			int oLeadId = -1;
			try {
				dbi.ExecuteNonQuery(CommandType.StoredProcedure,"efr_insert_temp_lead", oParameters);
				oLeadId = (int)oParameters[25].Value;
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return oLeadId;
		}

		/// <summary>
		/// Function returning the informations about the lead
		/// </summary>
		/// <param name="pStreetAddress">Street Address</param>
		/// <param name="pDayPhone">Day Phone Number</param>
		/// <param name="pEveningPhone">Evening Phone Number</param>
		/// <param name="pEmail">Email Address</param>
		/// <returns>DataTable containing the informations of the current lead</returns>
		/// <remarks>efr_call_es_get_lead_doubles</remarks>
		public DataTable GetLeadDoubles(string pStreetAddress, string pDayPhone, string pEveningPhone, string pEmail) {
		
			#region DataParameters definition

			DataParameters[] oDataParameters = new DataParameters[4];
			oDataParameters[0] = new DataParameters();
			oDataParameters[0].ParameterName = "@street";
			oDataParameters[0].DataType = DbType.String;
			oDataParameters[0].ParamDirection = ParameterDirection.Input;
			if(pStreetAddress.Length == 0)
				oDataParameters[0].Value = System.DBNull.Value;
			else
				oDataParameters[0].Value = pStreetAddress;

			oDataParameters[1] = new DataParameters();
			oDataParameters[1].ParameterName = "@day_phone";
			oDataParameters[1].DataType = DbType.String;
			oDataParameters[1].ParamDirection = ParameterDirection.Input;
			oDataParameters[1].Value = pDayPhone;

			oDataParameters[2] = new DataParameters();
			oDataParameters[2].ParameterName = "@evening_phone";
			oDataParameters[2].DataType = DbType.String;
			oDataParameters[2].ParamDirection = ParameterDirection.Input;
			if(pEveningPhone.Length == 0)
				oDataParameters[2].Value = System.DBNull.Value;
			else
				oDataParameters[2].Value = pEveningPhone;

			oDataParameters[3] = new DataParameters();
			oDataParameters[3].ParameterName = "@email";
			oDataParameters[3].DataType = DbType.String;
			oDataParameters[3].ParamDirection = ParameterDirection.Input;
			oDataParameters[3].Value = pEmail;
			
			#endregion
			
			DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			DataTable oLeadTbl = null;
			try {
				oLeadTbl = oDbi.ExecuteFetchDataTable("efr_call_es_get_lead_doubles",oDataParameters);
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return oLeadTbl;
		}

		/// <summary>
		/// Method creating an Activity for the lead
		/// </summary>
		/// <param name="pLeadID">Lead ID</param>
		/// <param name="pLeadActivityTypeId">Lead Activity ID</param>
		/// <remarks>efr_call_es_insert_lead_activity</remarks>
		public void CreateLeadActivity(int pLeadID, int pLeadActivityTypeId) {
			
			#region DataParameters Definition

			DataParameters[] oDataParam = new DataParameters[2];

			oDataParam[0] = new DataParameters();
			oDataParam[0].ParameterName = "@lead_id";
			oDataParam[0].DataType = DbType.Int32;
			oDataParam[0].ParamDirection = ParameterDirection.Input;
			oDataParam[0].Value = pLeadID;

			oDataParam[1] = new DataParameters();
			oDataParam[1].ParameterName = "@lead_activity_type_id";
			oDataParam[1].DataType = DbType.Int32;
			oDataParam[1].ParamDirection = ParameterDirection.Input;
			oDataParam[1].Value = pLeadActivityTypeId;

			#endregion
			
			DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(),this.GetDataProviderStringConfigKey());
			try {
				oDbi.ExecuteNonQuery("efr_call_es_insert_lead_activity", oDataParam);
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
		}

		/// <summary>
		/// Method adding a new Visit for an specific Lead
		/// </summary>
		/// <param name="pLeadID">Lead ID</param>
		/// <param name="pPromotionID">Promotion ID</param>
		/// <remarks>efr_call_es_insert_lead_visit</remarks>
		public void InsertLeadVisit(int pLeadID, int pPromotionID) {
			
			#region DataParameters Definition
			
			DataParameters[] oDataParam = new DataParameters[2];
			oDataParam[0] = new DataParameters();
			oDataParam[0].ParameterName = "@lead_id";
			oDataParam[0].DataType = DbType.Int32;
			oDataParam[0].ParamDirection = ParameterDirection.Input;
			oDataParam[0].Value = pLeadID;

			oDataParam[1] = new DataParameters();
			oDataParam[1].ParameterName = "@promotion_id";
			oDataParam[1].DataType = DbType.Int32;
			oDataParam[1].ParamDirection = ParameterDirection.Input;
			oDataParam[1].Value	= pPromotionID;

			#endregion
			
			DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			try {
				oDbi.ExecuteNonQuery("efr_call_es_insert_lead_visit", oDataParam);
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}	
		}

		/// <summary>
		/// Function returning a report about the PartnerSale
		/// </summary>
		/// <param name="pPartnerID">PartnerID</param>
		/// <param name="pStartDate">Start Date of the Sales Report</param>
		/// <param name="pEndDate">End Date of the Sales Report</param>
		/// <returns></returns>
		public DataTable GetRptPartnerSales(int pPartnerID, DateTime pStartDate, DateTime pEndDate) {
			
			#region DataParameters[] Definition

			DataParameters[] oParameters = new DataParameters[3];
			oParameters[0] = new DataParameters();
			oParameters[0].ParameterName = "@partner_id";
			oParameters[0].DataType = DbType.Int32;
			oParameters[0].ParamDirection = ParameterDirection.Input;
			oParameters[0].Value = pPartnerID;

			oParameters[1] = new DataParameters();
			oParameters[1].ParameterName = "@date_from";
			oParameters[1].DataType = DbType.DateTime;
			oParameters[1].ParamDirection = ParameterDirection.Input;
			oParameters[1].Value = pStartDate;

			oParameters[2] = new DataParameters();
			oParameters[2].ParameterName = "@date_to";
			oParameters[2].DataType = DbType.DateTime;
			oParameters[2].ParamDirection = ParameterDirection.Input;
			oParameters[2].Value = pEndDate;
			
			#endregion

			DatabaseInterface dbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			DataTable oMemberTable = null;
			try {
				oMemberTable = dbi.ExecuteFetchDataTable("efundraisingprod.dbo.efr_rpt_partner_sales", oParameters);
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return oMemberTable;	
		}

		/// <summary>
		/// Get a DataTable of Partner Lead's Report
		/// </summary>
		/// <param name="pPartnerID">Partner ID Reference number</param>
		/// <param name="pStartDate">Report Start Date</param>
		/// <param name="pEndDate">Report End Date</param>
		/// <returns></returns>
		public DataTable GetRptPartnerLeads(int pPartnerID, DateTime pStartDate, DateTime pEndDate) {
			
			#region DataParameters[] Definition
			
			DataParameters[] oParameters = new DataParameters[3];
			oParameters[0] = new DataParameters();
			oParameters[0].ParameterName = "@partner_id";
			oParameters[0].DataType = DbType.Int32;
			oParameters[0].ParamDirection = ParameterDirection.Input;
			oParameters[0].Value = pPartnerID;

			oParameters[1] = new DataParameters();
			oParameters[1].ParameterName = "@date_from";
			oParameters[1].DataType = DbType.DateTime;
			oParameters[1].ParamDirection = ParameterDirection.Input;
			oParameters[1].Value = pStartDate;

			oParameters[2] = new DataParameters();
			oParameters[2].ParameterName = "@date_to";
			oParameters[2].DataType = DbType.DateTime;
			oParameters[2].ParamDirection = ParameterDirection.Input;
			oParameters[2].Value = pEndDate;

			#endregion

			DatabaseInterface dbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			DataTable oLeadsTable = null;
			try {
				oLeadsTable = dbi.ExecuteFetchDataTable("efundraisingprod.dbo.efr_rpt_partner_leads", oParameters);
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return oLeadsTable;
		}

		/// <summary>
		/// Function returning the informations about a consultant
		/// from his ConsultantID Number
		/// </summary>
		/// <param name="pConsultantID">Consultant ID Number</param>
		/// <returns></returns>
		/// <remarks>efr_get_consultant_info</remarks>
		public DataTable GetConsultantInfo(int pConsultantID) {
		
			#region DataParameters Definition

			DataParameters[] oDataParam = new DataParameters[1];
			
			oDataParam[0] = new DataParameters();
			oDataParam[0].ParameterName = "@consultant_id";
			oDataParam[0].DataType = DbType.Int32;
			oDataParam[0].ParamDirection = ParameterDirection.Input;
			oDataParam[0].Value = pConsultantID;

			#endregion
			
			DataTable dt = null;
			DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			try {
				dt = oDbi.ExecuteFetchDataTable("efr_get_consultant_info",oDataParam);
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return dt;		
		}

		/// <summary>
		/// Get DataTable with GroupType
		/// </summary>
		/// <returns></returns>
		public DataTable GetGroupType() {
			DataTable oGroupTable = null;
			DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			try {
				oGroupTable = oDbi.ExecuteFetchDataTable("SELECT group_type_id, Description FROM group_type ORDER BY Description", CommandType.Text, null);
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return oGroupTable;
		}

		/// <summary>
		/// Get DataTable of Organization Type
		/// </summary>
		/// <returns></returns>
		public DataTable GetOrganizationType() {
			DataTable oOrgTable = null;
			DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			try {
				oOrgTable = oDbi.ExecuteFetchDataTable("SELECT organization_type_id, organization_type_desc FROM organization_type ORDER BY organization_type_desc", CommandType.Text,null);
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return oOrgTable;
		}

		/// <summary>
		/// Get DataTable of BestTimeToCall
		/// </summary>
		/// <returns></returns>
		public DataTable GetBestTimeToCall() {
			DataTable oBestTable = null;
			DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			try {
				oBestTable = oDbi.ExecuteFetchDataTable("SELECT best_time_call, best_time_call_desc FROM best_time_call", CommandType.Text,null);
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close(); 
			}
			return oBestTable;			
		}

		/// <summary>
		/// Get DataTable of State from specific country code
		/// </summary>
		/// <param name="pCountryCode"></param>
		/// <returns></returns>
		public DataTable GetStateByCountryCode(string pCountryCode) {
			DataTable oDtState = null;
			DataParameters[] oParams = new DataParameters[1];
			oParams[0] = new DataParameters();
			oParams[0].ParameterName = "@strCountryCode";
			oParams[0].DataType = DbType.String;
			oParams[0].ParamDirection = ParameterDirection.Input;
			oParams[0].Value = pCountryCode;

			DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			try {
				oDtState = oDbi.ExecuteFetchDataTable("get_states_provinces_by_country", oParams);
			} catch(Exception ex){
				throw ex;
			} finally {
				this.Close();
			}
			return oDtState;
		}

		/// <summary>
		/// Get DataTable of Coutry
		/// </summary>
		/// <returns></returns>
		public DataTable GetCountry() {
			DataTable oDtCnt = null;
			DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			try {
				oDtCnt = oDbi.ExecuteFetchDataTable("SELECT country_code, country_name FROM country ORDER BY country_name",CommandType.Text, null);
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return oDtCnt;
		}

        #region New Country method
        public DataTable GetCountrys()
        {
            DataTable oDtCnt = null;
            DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
            try
            {
                oDtCnt = oDbi.ExecuteFetchDataTable("efr_get_country", CommandType.StoredProcedure, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Close();
            }
            return oDtCnt;
        }
        #endregion

		/// <summary>
		/// Get DataTable of Title
		/// </summary>
		/// <returns></returns>
		public DataTable GetTitle() {
			DataTable dtTitle = null;
			DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			try {
				dtTitle = oDbi.ExecuteFetchDataTable("SELECT Title_ID, Title_Desc FROM title ORDER BY Title_Desc ASC",CommandType.Text,null);
			} catch(Exception ex){
				throw ex;
			} finally {
				this.Close(); 
			}
			return dtTitle;
		}

		/// <summary>
		/// Method to get the Partner Info from Lead ID Reference Number
		/// </summary>
		/// <param name="pLeadID">Lead ID reference number</param>
		/// <returns>DataTable with Partner Informations</returns>
		[Obsolete("Use GetLeadInfoByLeadID",true)]
		public DataTable GetPartnerInfoByLeadID(int pLeadID) {
			DataTable oDtPartnerInfo = new DataTable();
			
			#region DataParameters

			DataParameters[] oParams = new DataParameters[1];
			oParams[0] = new DataParameters();
			oParams[0].ParameterName = "@intLeadID";
			oParams[0].ParamDirection = ParameterDirection.Input;
			oParams[0].DataType = DbType.Int32;
			oParams[0].Value = pLeadID;

			#endregion
			DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			try {
				//efr_call_sp_GetPartnerInfoFromLeadID
				oDtPartnerInfo = oDbi.ExecuteFetchDataTable("efr_GetPartnerInfoFromLeadID", oParams);
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close(); 
			}
			return oDtPartnerInfo;
		}

		/// <summary>
		/// Method to get the Partner Information from Partner ID Reference Number
		/// </summary>
		/// <param name="pPartnerID">Partner ID Reference Number</param>
		/// <returns>DataTable with Partner Informations</returns>
		public DataTable GetPartnerInfo(int pPartnerID) {
			DataTable oDtPartnerInfo = null;
			DataParameters[] oParams = new DataParameters[1];
			oParams[0] = new DataParameters();
			oParams[0].DataType = DbType.Int32;
			oParams[0].ParamDirection = ParameterDirection.Input;
			oParams[0].ParameterName = "@PartnerID";
			oParams[0].Value = pPartnerID;
			DatabaseInterface oDbi = new DatabaseInterface(this.GetConnectionStringConfigKey(), this.GetDataProviderStringConfigKey());
			try {
			 	oDtPartnerInfo = oDbi.ExecuteFetchDataTable("SELECT partner_id, " +
																"partner_group_type_id, " +
																"country_id, " + 
																"partner_name, " + 
																"partner_path, " +
																"eSubs_url, eStore_url," +
																"free_kit_url, " +
																"phone_number, " +
																"url, " +
																"guid, " +
																"prize_eligible, " +
																"has_collection_site FROM Partner WHERE Partner_id = @PartnerID", CommandType.Text, oParams);
			} catch(Exception ex) {
				throw ex;
			} finally {
				this.Close();
			}
			return oDtPartnerInfo;
		}

		public void UnsubscribedEmail(string emailAddress, string fullName, int partnerId) {
			#region DataParameters
			DataParameters[] oParams = new DataParameters[2];
			oParams[0] = new DataParameters();
			oParams[0].ParameterName = "@strEmail";
			oParams[0].ParamDirection = ParameterDirection.Input;
			oParams[0].DataType = DbType.String;
			oParams[0].Value = emailAddress;
			
			oParams[1] = new DataParameters();
			oParams[1].ParameterName = "@intPartnerID";
			oParams[1].ParamDirection = ParameterDirection.Input;
			oParams[1].DataType = DbType.Int32;
			oParams[1].Value = partnerId;

			#endregion

			DatabaseInterface oDbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			try 
			{
				oDbi.ExecuteNonQuery("sp_UnsubscribeEmail", oParams);
			} catch {
			 	throw;
			} finally {
				Close();
			}
		}

		public void InsertToENewsLetter(string Referrer, string FullName, string EmailAddress, int PartnerId) {

			#region DataParameters
			DataParameters[] oParams = new DataParameters[5];
			oParams[0] = new DataParameters();
			oParams[0].ParameterName = "@newsletter_id";
			oParams[0].ParamDirection = ParameterDirection.Output;
			oParams[0].DataType = DbType.Int32;
			oParams[0].Value = int.MinValue;
			
			oParams[1] = new DataParameters();
			oParams[1].ParameterName = "@strReferrer";
			oParams[1].ParamDirection = ParameterDirection.Input;
			oParams[1].DataType = DbType.String;
			oParams[1].Value = Referrer;
			
			oParams[2] = new DataParameters();
			oParams[2].ParameterName = "@strFullName";
			oParams[2].ParamDirection = ParameterDirection.Input;
			oParams[2].DataType = DbType.String;
			oParams[2].Value = FullName;

			oParams[3] = new DataParameters();
			oParams[3].ParameterName = "@strEmail";
			oParams[3].ParamDirection = ParameterDirection.Input;
			oParams[3].DataType = DbType.String;
			oParams[3].Value = EmailAddress;

			oParams[4] = new DataParameters();
			oParams[4].ParameterName = "@intPartnerID";
			oParams[4].ParamDirection = ParameterDirection.Input;
			oParams[4].DataType = DbType.Int32;
			oParams[4].Value = PartnerId;
			#endregion

			DatabaseInterface oDbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
			try {
				oDbi.ExecuteNonQuery("efr_InsertNewsLetter",oParams);
			} catch (Exception ex) {
				throw;
			} finally {
				Close();
			}
		}
		
	}	
}