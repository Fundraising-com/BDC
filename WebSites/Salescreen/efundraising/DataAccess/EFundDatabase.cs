using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using efundraising.Core;
using efundraising.Data.Sql;
using efundraising.Configuration;
using efundraising.efundraisingCore;


namespace efundraising.efundraisingCore.DataAccess
{
	/// <summary>
	/// Summary description for EFundDatabase.
	/// </summary>
	public class EFundDatabase : efundraising.Data.Sql.DatabaseObject
	{
		public EFundDatabase()
		{
			if(Config.IsEFundWebProduction) 
			{
				SetConnectionString(Config.EFundWebConnectionStringRelease);
				SetDataProvider(Config.EFundWebDataProviderRelease);
			} 
			else 
			{
				SetConnectionString(Config.EFundWebConnectionStringDebug);
				SetDataProvider(Config.EFundWebDataProviderDebug);
			}
		}
		
		#region Partner


		/// <summary>
		/// Loads a partner with every field in the database including password
		/// </summary
		private Partner LoadPartner(DataRow row) {

			Partner partner = new Partner();

			partner.PartnerID				= DBValue.ToInt32(row["partner_id"]);
			partner.PartnerGroupTypeID		= DBValue.ToInt32(row["partner_group_type_id"]);
			partner.PartnerSubGroupTypeID	= DBValue.ToInt32(row["partner_subgroup_type_id"]);
			partner.CountryID				= DBValue.ToInt32(row["country_id"]);
			partner.PartnerName				= DBValue.ToString(row["partner_name"]);
			partner.PartnerPassword			= DBValue.ToString(row["partner_password"]);
			partner.PartnerPath				= DBValue.ToString(row["partner_path"]);
			partner.ESubsUrl				= DBValue.ToString(row["esubs_url"]);
			partner.EStoreUrl				= DBValue.ToString(row["estore_url"]);
			partner.FreeKitUrl				= DBValue.ToString(row["free_kit_url"]);
			partner.Logo					= DBValue.ToString(row["logo"]);
			partner.PhoneNumber				= DBValue.ToString(row["phone_number"]);
			partner.EmailExt				= DBValue.ToString(row["email_ext"]);
			partner.Url						= DBValue.ToString(row["url"]);
			partner.GUID					= DBValue.ToString(row["guid"]);
			partner.PrizeEligible			= DBValue.ToBoolean(row["prize_eligible"]);
			partner.HasCollectionSite		= DBValue.ToBoolean(row["has_collection_site"]);
			partner.PartnerFolder			= DBValue.ToString(row["partner_folder"]);

			return partner;
		}


		// It is assumed that two partners won't have the same partner path
		// even though it is not a primary key and the function will
		// always return only one partner, the first selected
		public Partner GetPartnerByPath(string partnerPath) {
			Partner partner = null;

			string storedProcName = "efr_get_partner_by_path";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@partner_path", DbType.String, DBValue.ToDBString(partnerPath)));
				
				si.Open();

				DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					try {
						partner = LoadPartner(dt.Rows[0]);
					}
					catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}
			} 
			finally {
				si.Close();
			}
			return partner;
		}

		public Partner[] GetPartners() {
			Partner[] partners = null;

			string storedProcName = "efr_get_partners";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				
				si.Open();

				DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt != null && dt.Rows.Count > 0) {
					try {
						partners = new Partner[dt.Rows.Count];

						for(int i=0; i<dt.Rows.Count; i++) {
							partners[i] = LoadPartner(dt.Rows[i]);
						}
					} 
					catch(Exception ex) {
						throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
					}
				}
			} 
			finally {
				si.Close();
			}
			return partners;
		}

		public Partner GetPartnerInfoByID(int partnerID) {
			Partner partner = new Partner();
			partner.PartnerID = partnerID;
			string storedProcName = "efr_get_partner_by_partner_id";
			bool useTransaction = false;

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@partnerID", DbType.Int32, DBValue.ToDBInt32(partnerID)));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Fetch the database.
				DataTable dt1 = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
				
				// read the result
				if (dt1.Rows.Count > 0) {
					foreach (DataRow row in dt1.Rows) {
						partner.PartnerName = DBValue.ToString(row["partner_name"]);
						partner.PhoneNumber = DBValue.ToString(row["phone_number"]);
						partner.GUID =  DBValue.ToString(row["guid"]);
						partner.ESubsUrl = DBValue.ToString(row["esubs_url"]);
						partner.HasCollectionSite = DBValue.ToInt32(row["has_collection_site"]) == 1 ? true : false;
						partner.PartnerFolder = DBValue.ToString(row["partner_folder"]);
						partner.Url = DBValue.ToString(row["url"]);
					}
				}
				
				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch {
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally {
				// Always close connection.
				si.Close();
			}
		
			return partner;
		}
		
		public Partner GetPartnerInfoByURL(string url) {
			
			Partner partner = new Partner();
			partner.Url = url;
			
			string storedProcName = "efr_get_partner_by_url";
			bool useTransaction = false;

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@url", DbType.String, DBValue.ToDBString(url)));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Fetch the database.
				DataTable dt1 = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
				
				// read the result
				if (dt1.Rows.Count > 0) {
					foreach (DataRow row in dt1.Rows) {
						partner.PartnerID = DBValue.ToInt32(row["partner_id"]);
						partner.PartnerName = DBValue.ToString(row["partner_name"]);
						partner.PhoneNumber = DBValue.ToString(row["phone_number"]);
						partner.GUID =  DBValue.ToString(row["guid"]);
						partner.HasCollectionSite = DBValue.ToInt32(row["has_collection_site"]) == 1 ? true : false;
						partner.PartnerFolder = DBValue.ToString(row["partner_folder"]);
					}
				}
				else {
					partner = null;
					partner = this.GetPartnerInfoByID(0);
					partner.Url = url;
				}
				
				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch {
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally {
				// Always close connection.
				si.Close();
			}
		
			return partner;
		}
		
		public Partner GetPartnerInfoByFolder(string folder) {
			
			Partner partner = new Partner();
			partner.PartnerFolder = folder;
			
			string storedProcName = "efr_get_partner_by_partner_folder";
			bool useTransaction = false;

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@partner_folder", DbType.String, DBValue.ToDBString(folder)));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Fetch the database.
				DataTable dt1 = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
				
				// read the result
				if (dt1.Rows.Count > 0) {
					foreach (DataRow row in dt1.Rows) {
						partner.PartnerID = DBValue.ToInt32(row["partner_id"]);
						partner.PartnerName = DBValue.ToString(row["partner_name"]);
						partner.PhoneNumber = DBValue.ToString(row["phone_number"]);
						partner.GUID =  DBValue.ToString(row["guid"]);
						partner.HasCollectionSite = DBValue.ToInt32(row["has_collection_site"]) == 1 ? true : false;
						partner.Url = DBValue.ToString(row["url"]);
					}
				}
				else {
					partner = null;
					partner = this.GetPartnerInfoByID(0);
				}
				
				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch {
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally {
				// Always close connection.
				si.Close();
			}
		
			return partner;
		}

		#endregion
		
		#region WebSite
		public int GetWebSiteID(int promotionID, int webprojectID) 
		{
			int webSiteID = 16;
			
			string storedProcName = "web_tracking_2.dbo.wt_get_website_from_promotion_id";
			bool useTransaction = false;

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@promotion_id", DbType.Int32, DBValue.ToDBInt32(promotionID)));
				paramCol.Add(new SqlDataParameter("@webproject_id", DbType.Int32, DBValue.ToDBInt32(webprojectID)));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Fetch the database.
				DataTable dt1 = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
				
				// read the result
				if (dt1.Rows.Count > 0)
				{
					foreach (DataRow row in dt1.Rows)
					{
						webSiteID = DBValue.ToInt32(row["website_id"]);
					}
				}
				
				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
		
			return webSiteID;
		}
		
		#endregion

		#region Newsletters
		public void UnSubscribeNewsletter(string email, int partnerID) 
		{
			
			string storedProcName = "sp_UnsubscribeEmail";
			bool useTransaction = false;

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@strEmail", DbType.String, DBValue.ToDBString(email)));
				paramCol.Add(new SqlDataParameter("@intPartnerID", DbType.Int32, DBValue.ToDBInt32(partnerID)));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Fetch the database.
				si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);
								
				
				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
		
		}
		
		public int SubscribeNewsletter(string name, string email, int partnerID)
		{
			int newId = 0;
			string storedProcName = "efr_InsertNewsLetter";
			bool useTransaction = false;

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@newsletter_id", DbType.Int32, ParameterDirection.Output));
				paramCol.Add(new SqlDataParameter("@strReferrer", DbType.String, DBValue.ToDBString("")));
				paramCol.Add(new SqlDataParameter("@strFullName", DbType.String, DBValue.ToDBString(name)));
				paramCol.Add(new SqlDataParameter("@strEmail", DbType.String, DBValue.ToDBString(email)));
				paramCol.Add(new SqlDataParameter("@intPartnerID", DbType.Int32, DBValue.ToDBInt32(partnerID)));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Fetch the database.
				si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);
				
				newId = DBValue.ToInt32(paramCol["@newsletter_id"].Value);				
				
				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch (Exception ex)
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw ex;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
			
			return newId;
		}
		
		public void SubscribeNewsletter(Newsletter newsletter)
		{
			string storedProcName = "efr_InsertNewsLetter";
			bool useTransaction = false;

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@strReferrer", DbType.String, DBValue.ToDBString("")));
				paramCol.Add(new SqlDataParameter("@strFullName", DbType.String, DBValue.ToDBString(newsletter.Fullname)));
				paramCol.Add(new SqlDataParameter("@strEmail", DbType.String, DBValue.ToDBString(newsletter.Email)));
				paramCol.Add(new SqlDataParameter("@intPartnerID", DbType.Int32, DBValue.ToDBInt32(newsletter.PartnerId)));
				paramCol.Add(new SqlDataParameter("@newsletter_id", DbType.Int32, ParameterDirection.Output));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Fetch the database.
				si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);
				
				newsletter.NewsletterId = DBValue.ToInt32(paramCol["@newsletter_id"].Value);
				
				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch (Exception ex)
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw ex;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
		}
		
		#endregion

		#region Destination
		public Destination GetDestination(int destinationId)
		{
			Destination d = null;

			bool useTransaction = false;
			string storedProcName = "efr_get_destination";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				si.Open();

				if(useTransaction)
					si.BeginTransaction();

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@destination_id", DbType.Int32, DBValue.ToDBInt32(destinationId)));

				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				// fill our objects
				try 
				{
					if (dt.Rows.Count > 0) 
					{
						d = new Destination();						
						d.DestinationId = DBValue.ToInt32(dt.Rows[0]["destination_id"]);
						d.WebSiteId = DBValue.ToInt32(dt.Rows[0]["web_site_id"]);
						d.Url = DBValue.ToString(dt.Rows[0]["url"]);
					}
						
				} 
				catch(System.Exception ex) 
				{
					throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
				}

				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}

			return d;
		}
		#endregion

		#region Promotion
		public Promotion LoadPromotion(DataRow row)
		{
			Promotion p = new Promotion();
			p.PromotionId = DBValue.ToInt32(row["promotion_id"]);
			p.PromotionTypeCode = DBValue.ToString(row["promotion_type_code"]);
			p.TargetedMarketId = DBValue.ToInt32(row["targeted_market_id"]);
			p.AdvertisingSupportId = DBValue.ToInt32(row["advertising_support_id"]);
			p.AdvertisementId = DBValue.ToInt32(row["advertisement_id"]);
			p.PartnerId = DBValue.ToInt32(row["partner_id"]);
			p.AdvertiserId = DBValue.ToInt32(row["advertiser_id"]);
			p.AdvertismentTypeId = DBValue.ToInt32(row["advertisment_type_id"]);
			p.DestinationId = DBValue.ToInt32(row["destination_id"]);
			p.AdvertiserPartnerId = DBValue.ToInt32(row["advertiser_partner_id"]);
			p.GrabberId = DBValue.ToInt32(row["grabber_id"]);
			p.Description = DBValue.ToString(row["description"]);
			p.ScriptName = DBValue.ToString(row["script_name"]);
			p.ContactName = DBValue.ToString(row["contact_name"]);
			p.Visibility = DBValue.ToString(row["visibility"]);
			p.TrackingSerial = DBValue.ToString(row["tracking_serial"]);
			p.NbImpressionBought = DBValue.ToInt32(row["nb_impression_bought"]);
			p.IsActive = DBValue.ToBoolean(row["is_active"]);
			p.CookieContent = DBValue.ToString(row["cookie_content"]);
			p.IsPredictive = DBValue.ToBoolean(row["is_predictive"]);
			p.Keyword = DBValue.ToString(row["keyword"]);

			return p;
		}
		
		public Promotion GetPromotion(int promotionId)
		{
			Promotion p = null;

			bool useTransaction = false;
			string storedProcName = "efr_get_promotion";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				si.Open();

				if(useTransaction)
					si.BeginTransaction();

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@promotion_id", DbType.Int32, DBValue.ToDBInt32(promotionId)));

				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				// fill our objects
				try 
				{
					if (dt.Rows.Count > 0) 
					{
						p = LoadPromotion(dt.Rows[0]);
					}
						
				} 
				catch(System.Exception ex) 
				{
					throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
				}

				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}

			return p;
		}


		public Promotion GetPromotion(string scriptName)
		{
			Promotion p = null;

			bool useTransaction = false;
			string storedProcName = "efr_get_promotion_by_script_name";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				si.Open();

				if(useTransaction)
					si.BeginTransaction();

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@script_name", DbType.String, DBValue.ToDBString(scriptName)));

				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				// fill our objects
				try 
				{
					if (dt.Rows.Count > 0) 
					{
						p = LoadPromotion(dt.Rows[0]);
					}
						
				} 
				catch(System.Exception ex) 
				{
					throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
				}

				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}

			return p;
		}

		#endregion

		#region Leads
		public void UnassignLead(Lead lead, int userId)
		{
			string storedProcName = "efr_call_crm_unassign_lead";
			bool useTransaction = false;

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, DBValue.ToDBInt32(lead.LeadID)));
				paramCol.Add(new SqlDataParameter("@consultant_id", DbType.Int32, DBValue.ToDBInt32(lead.ConsultantID)));
				paramCol.Add(new SqlDataParameter("@user_id", DbType.Int32, DBValue.ToDBInt32(userId)));

				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Execute
				si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);
				
				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
		}



        /// <summary>
        /// Update newly entered lead with additional infomational
        /// </summary>
       

		/// <summary>
		/// Insert a new lead.
		/// </summary>
		public void InsertNewLead(Lead lead) 
		{
			string storedProcName = "efr_call_efr_insert_new_lead";
			bool useTransaction = false;

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@first_name", DbType.String, DBValue.ToDBString(lead.FirstName)));
				paramCol.Add(new SqlDataParameter("@last_name", DbType.String, DBValue.ToDBString(lead.LastName)));
				paramCol.Add(new SqlDataParameter("@email", DbType.String, DBValue.ToDBString(lead.Email)));
				paramCol.Add(new SqlDataParameter("@street_address", DbType.String, DBValue.ToDBString(lead.StreetAddress)));
				paramCol.Add(new SqlDataParameter("@city", DbType.String, DBValue.ToDBString(lead.City)));
				paramCol.Add(new SqlDataParameter("@state_code", DbType.String, DBValue.ToDBString(lead.State)));
				paramCol.Add(new SqlDataParameter("@zip_code", DbType.String, DBValue.ToDBString(lead.ZipCode)));
				paramCol.Add(new SqlDataParameter("@country_code", DbType.String, DBValue.ToDBString(lead.Country)));
				paramCol.Add(new SqlDataParameter("@day_phone", DbType.String, DBValue.ToDBString(lead.DayPhone)));
				paramCol.Add(new SqlDataParameter("@evening_phone", DbType.String, DBValue.ToDBString(lead.EveningPhone)));
				paramCol.Add(new SqlDataParameter("@participant_count", DbType.Int32, DBValue.ToDBInt32(lead.ParticipantCount)));
				paramCol.Add(new SqlDataParameter("@organization", DbType.String, DBValue.ToDBString(lead.GroupName)));

				if (lead.PromotionID == -1)
					paramCol.Add(new SqlDataParameter("@promotion_id", DbType.Int32, DBNull.Value));
				else
					paramCol.Add(new SqlDataParameter("@promotion_id", DbType.Int32, DBValue.ToDBInt32(lead.PromotionID)));

				paramCol.Add(new SqlDataParameter("@title", DbType.String, DBValue.ToDBString(lead.Title)));
				paramCol.Add(new SqlDataParameter("@evening_phone_ext", DbType.String, DBValue.ToDBString(lead.EveningPhoneExt)));
				paramCol.Add(new SqlDataParameter("@day_phone_ext", DbType.String, DBValue.ToDBString(lead.DayPhoneExt)));
				paramCol.Add(new SqlDataParameter("@best_time_to_call", DbType.String, DBValue.ToDBString(lead.BestTimeToCall)));
				paramCol.Add(new SqlDataParameter("@organization_type_id", DbType.Int32, DBValue.ToDBInt32(lead.OrganizationTypeID)));
				paramCol.Add(new SqlDataParameter("@group_type_id", DbType.Byte, DBValue.ToDBByte(lead.GroupTypeID)));
				try 
				{
					// Make sure FundraisingDate is valid DateTime because internally the database uses a datetime field
					// and will throw exception if FundraisingDate is invalid.
					DateTime realDate = DateTime.Parse(lead.FundraisingDate);
					paramCol.Add(new SqlDataParameter("@Fundraising_Date", DbType.DateTime, DBValue.ToDBDateTime(realDate)));
				}
				catch 
				{
					paramCol.Add(new SqlDataParameter("@Fundraising_Date", DbType.DateTime, DBValue.ToDBDateTime(DateTime.Now)));
				}
				paramCol.Add(new SqlDataParameter("@decision_maker", DbType.Boolean, DBValue.ToDBBoolean(lead.DecisionMaker)));
				paramCol.Add(new SqlDataParameter("@products_interest_in", DbType.String, DBValue.ToDBString(lead.ProductsInterest)));
				paramCol.Add(new SqlDataParameter("@on_email_list", DbType.Boolean, DBValue.ToDBBoolean(lead.OnEmailList)));
				paramCol.Add(new SqlDataParameter("@comments", DbType.String, DBValue.ToDBString(lead.Comments)));
				paramCol.Add(new SqlDataParameter("@lead_status_id", DbType.Int32, DBValue.ToDBInt32(lead.LeadStatusID)));
				paramCol.Add(new SqlDataParameter("@temp_lead_id", DbType.Int32, DBValue.ToDBInt32(lead.TempLeadID)));
				paramCol.Add(new SqlDataParameter("@consultant_id", DbType.Int32, DBValue.ToDBInt32(lead.ConsultantID)));
				paramCol.Add(new SqlDataParameter("@is_postal_address_validated", DbType.Int32, DBValue.ToDBInt32(lead.IsPostalAddressValidated)));
				paramCol.Add(new SqlDataParameter("@fundraisers_per_year", DbType.Byte, DBValue.ToDBByte(lead.FundraisersPerYear)));
				paramCol.Add(new SqlDataParameter("@address_zone_id", DbType.Int32, DBValue.ToDBInt32(lead.AddressZoneId)));
				paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, ParameterDirection.ReturnValue));
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Execute
				si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				lead.LeadID = DBValue.ToInt32(paramCol["@lead_id"].Value);
				
				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch (Exception ex)
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
		}


		public Lead LoadLead(DataRow dr)
		{
			Lead lead = new Lead();
			lead.LeadID = DBValue.ToInt32(dr["lead_id"]);
			lead.PartnerID = DBValue.ToInt32(dr["partner_id"]);
			lead.FirstName = DBValue.ToString(dr["first_name"]);
			lead.LastName = DBValue.ToString(dr["last_name"]);
			lead.GroupName = DBValue.ToString(dr["organization"]);
			lead.StreetAddress = DBValue.ToString(dr["street_address"]);
			lead.City = DBValue.ToString(dr["city"]);
			lead.DayPhone = DBValue.ToString(dr["day_phone"]);
			lead.EveningPhone = DBValue.ToString(dr["evening_phone"]);
			lead.ParticipantCount = DBValue.ToInt32(dr["participant_count"]);
			lead.ConsultantID = DBValue.ToInt32(dr["consultant_id"]);
			lead.IsConsultantActive = DBValue.ToBoolean(dr["consultant_active"]);
			lead.PromotionID = DBValue.ToInt32(dr["promotion_id"]);
			lead.Email = DBValue.ToString(dr["email_address"]);
			lead.Country = DBValue.ToString(dr["country_code"]);
			lead.ZipCode = DBValue.ToString(dr["zip_code"]);
			lead.LeadStatusID = DBValue.ToInt32(dr["lead_status_id"]);
			return lead;
		}

		public LeadCollection GetMatchingLeads(string firstName, string lastName, string street, string zipCode, string dayPhone, string eveningPhone, string email)
		{
			LeadCollection leads = new LeadCollection();

			bool useTransaction = false;
			string storedProcName = "efr_call_es_get_lead_doubles";
			
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				si.Open();

				if(useTransaction)
					si.BeginTransaction();

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@first_name", DbType.String, DBValue.ToDBString(firstName)));
				paramCol.Add(new SqlDataParameter("@last_name", DbType.String, DBValue.ToDBString(lastName)));
				paramCol.Add(new SqlDataParameter("@street_address", DbType.String, DBValue.ToDBString(street)));
				paramCol.Add(new SqlDataParameter("@zip_code", DbType.String, DBValue.ToDBString(zipCode)));
				paramCol.Add(new SqlDataParameter("@day_phone", DbType.String, DBValue.ToDBString(dayPhone)));
				paramCol.Add(new SqlDataParameter("@evening_phone", DbType.String, DBValue.ToDBString(eveningPhone)));
				paramCol.Add(new SqlDataParameter("@email", DbType.String, DBValue.ToDBString(email)));

				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				// fill our objects
				try 
				{
					if (dt != null)
					{
						for (int i = 0; i < dt.Rows.Count; i++)
						{
					
							leads.Add(LoadLead(dt.Rows[i]));
						}
					}
						
				} 
				catch(System.Exception ex) 
				{
					throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
				}

				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch (Exception ex)
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}

			return leads;

		}
        // function is depreciated due to Link Server retirement
        // use EFundraingCRM.DataAccess.EfundraisingCRMDatabase.
        //public void InsertLeadVisit(Lead lead) 
        //{
        //    string storedProcName = "efr_call_es_insert_lead_visit";
        //    bool useTransaction = false;

        //    SqlInterface si = new SqlInterface(dataProvider, connectionString);

        //    try 
        //    {

        //        SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
        //        paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, DBValue.ToDBInt32(lead.LeadID)));
        //        paramCol.Add(new SqlDataParameter("@promotion_id", DbType.Int32, DBValue.ToDBInt32(lead.PromotionID)));
        //        paramCol.Add(new SqlDataParameter("@temp_lead_id", DbType.Int32, DBValue.ToDBInt32(lead.TempLeadID)));
        //        paramCol.Add(new SqlDataParameter("@lead_visit_id", DbType.Int32, ParameterDirection.ReturnValue));
				
        //        si.Open();

        //        if(useTransaction)
        //            si.BeginTransaction();
		
        //        // Execute
        //        si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);
				
        //        lead.LeadVisitID = DBValue.ToInt32(paramCol["@lead_visit_id"].Value);
				
        //        // Commit our transaction.
        //        if(useTransaction) 
        //            si.Commit();
        //    } 
        //    catch 
        //    {
        //        // Rollback on error.
        //        if(useTransaction)
        //            si.Rollback(); 

        //        // throw exception
        //        throw;
        //    } 
        //    finally 
        //    {
        //        // Always close connection.
        //        si.Close();
        //    }
        //}


		public void InsertLeadActivity(Lead lead, LeadActivityType activityType) 
		{
			string storedProcName = "efr_call_es_insert_lead_activity";
			bool useTransaction = false;

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, DBValue.ToDBInt32(lead.LeadID)));
				paramCol.Add(new SqlDataParameter("@lead_activity_type_id", DbType.Int32, DBValue.ToDBInt32((int) activityType)));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Execute
				int ret = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (ret <= 0)
					throw new SqlDataException("Insert lead activity failed.");
				
				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
		}
		
		public LeadActivity GetLeadActivity(int leadActivityID)
		{
			LeadActivity leadActity = null;
			string storedProcName = "efr_call_efr_get_lead_activity";

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@lead_activity_id", DbType.Int32, DBValue.ToDBInt32(leadActivityID)));
				
				si.Open();
				
				// Fetch the database.
				DataTable dt1 = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
				
				// read the result
				if (dt1.Rows.Count > 0)
				{
					leadActity = this.LoadLeadActivity(dt1.Rows[0]);
				}
				
			}
			finally 
			{
				// Always close connection.
				si.Close();
			}
		
			return leadActity;	
		}
		
		private LeadActivity LoadLeadActivity(DataRow row)
		{
			LeadActivity leadActivity = new LeadActivity();
			leadActivity.LeadActivityID = DBValue.ToInt32(row["lead_activity_id"]);
			leadActivity.LeadID = DBValue.ToInt32(row["lead_id"]);
			leadActivity.LeadActivityTypeID = DBValue.ToInt32(row["lead_activity_type_id"]);
			leadActivity.LeadActivityDate = DBValue.ToDateTime(row["lead_activity_date"]);
			leadActivity.CompletedDate = DBValue.ToDateTime(row["completed_date"]);
			leadActivity.Comments = DBValue.ToString(row["comments"]);
			return leadActivity;
		}
		
		public void InsertLeadActivity(LeadActivity leadActivity) 
		{
			string storedProcName = "efr_call_efr_insert_lead_activity";
			
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, DBValue.ToDBInt32(leadActivity.LeadID)));
				paramCol.Add(new SqlDataParameter("@lead_activity_type_id", DbType.Int32, DBValue.ToDBInt32(leadActivity.LeadActivityTypeID)));
				paramCol.Add(new SqlDataParameter("@comments", DbType.String, DBValue.ToDBString(leadActivity.Comments)));
				
				si.Open();

				// Execute
				int ret = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (ret <= 0)
					throw new SqlDataException("Insert lead activity failed.");
				
				
			} 
			catch 
			{
				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
		}
		
		public void UpdatetLeadActivity(LeadActivity leadActivity) 
		{
			string storedProcName = "efr_call_efr_update_lead_activity";
			
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@lead_activity_id", DbType.Int32, DBValue.ToDBInt32(leadActivity.LeadActivityID)));
				paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, DBValue.ToDBInt32(leadActivity.LeadID)));
				paramCol.Add(new SqlDataParameter("@lead_activity_type_id", DbType.Int32, DBValue.ToDBInt32(leadActivity.LeadActivityTypeID)));
				paramCol.Add(new SqlDataParameter("@lead_activity_date", DbType.DateTime, DBValue.ToDBDateTime(leadActivity.LeadActivityDate)));
				paramCol.Add(new SqlDataParameter("@completed_date", DbType.DateTime, DBValue.ToDBDateTime(leadActivity.CompletedDate)));
				paramCol.Add(new SqlDataParameter("@comments", DbType.String, DBValue.ToDBString(leadActivity.Comments)));
				
				si.Open();

				// Execute
				int ret = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				if (ret <= 0)
					throw new SqlDataException("Update lead activity failed.");
				
				
			} 
			catch 
			{
				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
		}
		
		#endregion

		#region Temp Lead
		
		private TempLead LoadTempLead(DataRow row)
		{
			TempLead tempLead = new TempLead();
			tempLead.DivisionID = DBValue.ToInt32(row["Division_ID"]);
			tempLead.PromotionID = DBValue.ToInt32(row["Promotion_ID"]);
			tempLead.TempLeadID = DBValue.ToInt32(row["Temp_Lead_ID"]);
			tempLead.ChannelCode = DBValue.ToString(row["Channel_Code"]);
			tempLead.LeadStatusID = DBValue.ToInt32(row["Lead_Status_ID"]);
			tempLead.ConsultantID = DBValue.ToInt32(row["Consultant_ID"]);
			tempLead.LeadEntryDate = DBValue.ToDateTime(row["Lead_Entry_Date"]);
			tempLead.Salutation = DBValue.ToString(row["Salutation"]);
			tempLead.FirstName = DBValue.ToString(row["First_Name"]);
			tempLead.LastName = DBValue.ToString(row["Last_Name"]);
			tempLead.Organization = DBValue.ToString(row["Organization"]);
			tempLead.StreetAddress = DBValue.ToString(row["Street_Address"]);
			tempLead.City = DBValue.ToString(row["City"]);
			tempLead.StateCode = DBValue.ToString(row["State_Code"]);
			tempLead.CountryCode = DBValue.ToString(row["Country_Code"]);
			tempLead.ZipCode = DBValue.ToString(row["Zip_Code"]);
			tempLead.DayPhone = DBValue.ToString(row["Day_Phone"]);
			tempLead.DayTimeCall = DBValue.ToString(row["Day_Time_Call"]);
			tempLead.EveningPhone = DBValue.ToString(row["Evening_Phone"]);
			tempLead.Fax = DBValue.ToString(row["Fax"]);
			tempLead.Email = DBValue.ToString(row["Email"]);
			tempLead.GroupTypeID = DBValue.ToInt32(row["Group_Type_ID"]);
			tempLead.ParticipantCount = DBValue.ToInt32(row["Participant_Count"]);
			tempLead.FundRaisingGoal = DBValue.ToInt32(row["Fund_Raising_Goal"]);
			tempLead.DecisionDate = DBValue.ToDateTime(row["Decision_Date"]);
			tempLead.DecisionMaker = DBValue.ToBoolean(row["Decision_Maker"]);
			tempLead.FundRaiserStartDate = DBValue.ToDateTime(row["Fund_Raiser_Start_Date"]);
			tempLead.OnEmailList = DBValue.ToBoolean(row["OnEmailList"]);
			tempLead.Comments = DBValue.ToString(row["Comments"]);
			tempLead.HearID = DBValue.ToInt32(row["Hear_ID"]);
			tempLead.KitToSend = DBValue.ToBoolean(row["kit_to_send"]);
			tempLead.KitSent = DBValue.ToBoolean(row["kit_sent"]);
			tempLead.KitSentDate = DBValue.ToDateTime(row["kit_sent_date"]);
			tempLead.DayPhoneExt = DBValue.ToString(row["Day_Phone_Ext"]);
			tempLead.EveningPhoneExt = DBValue.ToString(row["Evening_Phone_Ext"]);
			tempLead.RejectionReason = DBValue.ToString(row["Rejection_reason"]);
			tempLead.OtherPhone = DBValue.ToString(row["Other_Phone"]);
			tempLead.CookieContent = DBValue.ToString(row["Cookie_Content"]);
			tempLead.GroupWebSite = DBValue.ToString(row["Group_Web_Site"]);
			tempLead.OrganizationTypeID = DBValue.ToInt32(row["Organization_Type_ID"]);
			tempLead.TitleID = DBValue.ToInt32(row["Title_ID"]);
			tempLead.CampaignReasonID = DBValue.ToInt32(row["Campaign_Reason_ID"]);
			tempLead.WebSiteID = DBValue.ToInt32(row["Web_Site_ID"]);
			tempLead.OtherPhoneExt = DBValue.ToString(row["Other_Phone_Ext"]);
			tempLead.IsNew = DBValue.ToBoolean(row["IsNew"]);
			tempLead.OptInSweepstakes = DBValue.ToBoolean(row["Opt_In_Sweepstakes"]);
			tempLead.GroupID = DBValue.ToInt32(row["Group_ID"]);
			return tempLead;
		}
		
		public TempLead GetTempLead(int tempLeadID)
		{
			TempLead tempLead = null;
			string storedProcName = "efr_get_temp_lead";

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@temp_lead_id", DbType.Int32, DBValue.ToDBInt32(tempLeadID)));
				
				si.Open();
				
				// Fetch the database.
				DataTable dt1 = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);
				
				// read the result
				if (dt1.Rows.Count > 0)
				{
					tempLead = this.LoadTempLead(dt1.Rows[0]);
				}
				
			}
			finally 
			{
				// Always close connection.
				si.Close();
			}
		
			return tempLead;	
		}
		
		public TempLeadCollection GetNewTempLeads()
		{
			TempLeadCollection tempLeads = new TempLeadCollection();

			string storedProcName = "efr_get_new_temp_leads";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				si.Open();

				
				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, null);

				// fill our objects
				try 
				{
					if (dt != null)
					{
						for (int i = 0; i < dt.Rows.Count; i++)
						{
					
							tempLeads.Add(LoadTempLead(dt.Rows[i]));
						}
					}
						
				} 
				catch(System.Exception ex) 
				{
					throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
				}
			}
			finally 
			{
				// Always close connection.
				si.Close();
			}

			return tempLeads;	
		}
		
		public void InsertTempLead(TempLead tempLead) 
		{
			SqlInterface si = new SqlInterface(dataProvider, connectionString);
			string storedProcName = "efr_insert_new_temp_lead";
			
			try 
			{	
				// open the connection
				si.Open();
				
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Division_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.DivisionID)));
				paramCol.Add(new SqlDataParameter("@Promotion_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.PromotionID)));
				paramCol.Add(new SqlDataParameter("@Channel_Code", DbType.String, DBValue.ToDBString(tempLead.ChannelCode)));
				paramCol.Add(new SqlDataParameter("@Lead_Status_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.LeadStatusID)));
				paramCol.Add(new SqlDataParameter("@Consultant_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.ConsultantID)));
				paramCol.Add(new SqlDataParameter("@Lead_Entry_Date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.LeadEntryDate)));
				paramCol.Add(new SqlDataParameter("@Salutation", DbType.String, DBValue.ToDBString(tempLead.Salutation)));
				paramCol.Add(new SqlDataParameter("@First_Name", DbType.String, DBValue.ToDBString(tempLead.FirstName)));
				paramCol.Add(new SqlDataParameter("@Last_Name", DbType.String, DBValue.ToDBString(tempLead.LastName)));
				paramCol.Add(new SqlDataParameter("@Organization", DbType.String, DBValue.ToDBString(tempLead.Organization)));
				paramCol.Add(new SqlDataParameter("@Street_Address", DbType.String, DBValue.ToDBString(tempLead.StreetAddress)));
				paramCol.Add(new SqlDataParameter("@City", DbType.String, DBValue.ToDBString(tempLead.City)));
				paramCol.Add(new SqlDataParameter("@State_Code", DbType.String, DBValue.ToDBString(tempLead.StateCode)));
				paramCol.Add(new SqlDataParameter("@Country_Code", DbType.String, DBValue.ToDBString(tempLead.CountryCode)));
				paramCol.Add(new SqlDataParameter("@Zip_Code", DbType.String, DBValue.ToDBString(tempLead.ZipCode)));
				paramCol.Add(new SqlDataParameter("@Day_Phone", DbType.String, DBValue.ToDBString(tempLead.DayPhone)));
				paramCol.Add(new SqlDataParameter("@Day_Time_Call", DbType.String, DBValue.ToDBString(tempLead.DayTimeCall)));
				paramCol.Add(new SqlDataParameter("@Evening_Phone", DbType.String, DBValue.ToDBString(tempLead.EveningPhone)));
				paramCol.Add(new SqlDataParameter("@Fax", DbType.String, DBValue.ToDBString(tempLead.Fax)));
				paramCol.Add(new SqlDataParameter("@Email", DbType.String, DBValue.ToDBString(tempLead.Email)));
				paramCol.Add(new SqlDataParameter("@Group_Type_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.GroupTypeID)));
				paramCol.Add(new SqlDataParameter("@Participant_Count", DbType.Int32, DBValue.ToDBInt32(tempLead.ParticipantCount)));
				paramCol.Add(new SqlDataParameter("@Fund_Raising_Goal", DbType.Int32, DBValue.ToDBInt32(tempLead.FundRaisingGoal)));
				paramCol.Add(new SqlDataParameter("@Decision_Date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.DecisionDate)));
				paramCol.Add(new SqlDataParameter("@Decision_Maker", DbType.Boolean, DBValue.ToDBBoolean(tempLead.DecisionMaker)));
				paramCol.Add(new SqlDataParameter("@Fund_Raiser_Start_Date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.FundRaiserStartDate)));
				paramCol.Add(new SqlDataParameter("@OnEmailList", DbType.Boolean, DBValue.ToDBBoolean(tempLead.OnEmailList)));
				paramCol.Add(new SqlDataParameter("@Comments", DbType.String, DBValue.ToDBString(tempLead.Comments)));
				paramCol.Add(new SqlDataParameter("@Hear_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.HearID)));
				paramCol.Add(new SqlDataParameter("@Kit_to_send", DbType.Boolean, DBValue.ToDBBoolean(tempLead.KitToSend)));
				paramCol.Add(new SqlDataParameter("@Kit_sent", DbType.Boolean, DBValue.ToDBBoolean(tempLead.KitSent)));
				paramCol.Add(new SqlDataParameter("@Kit_sent_date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.KitSentDate)));
				paramCol.Add(new SqlDataParameter("@Day_Phone_Ext", DbType.String, DBValue.ToDBString(tempLead.DayPhoneExt)));
				paramCol.Add(new SqlDataParameter("@Evening_Phone_Ext", DbType.String, DBValue.ToDBString(tempLead.EveningPhoneExt)));
				paramCol.Add(new SqlDataParameter("@Rejection_reason", DbType.String, DBValue.ToDBString(tempLead.RejectionReason)));
				paramCol.Add(new SqlDataParameter("@Other_Phone", DbType.String, DBValue.ToDBString(tempLead.OtherPhone)));
				paramCol.Add(new SqlDataParameter("@Cookie_Content", DbType.String, DBValue.ToDBString(tempLead.CookieContent)));
				paramCol.Add(new SqlDataParameter("@Group_Web_Site", DbType.String, DBValue.ToDBString(tempLead.GroupWebSite)));
				paramCol.Add(new SqlDataParameter("@Organization_Type_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.OrganizationTypeID)));
				paramCol.Add(new SqlDataParameter("@Title_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.TitleID)));
				paramCol.Add(new SqlDataParameter("@Campaign_Reason_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.CampaignReasonID)));
				paramCol.Add(new SqlDataParameter("@Web_Site_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.WebSiteID)));
				paramCol.Add(new SqlDataParameter("@Other_Phone_Ext", DbType.String, DBValue.ToDBString(tempLead.OtherPhoneExt)));
				paramCol.Add(new SqlDataParameter("@IsNew", DbType.Boolean, DBValue.ToDBBoolean(tempLead.IsNew)));
				paramCol.Add(new SqlDataParameter("@Opt_In_Sweepstakes", DbType.Boolean, DBValue.ToDBBoolean(tempLead.OptInSweepstakes)));
				paramCol.Add(new SqlDataParameter("@Group_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.GroupID)));
		
				si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);
			}
			catch (Exception ex)
			{
				throw new SqlDataException("Error inserting into database calling " + storedProcName);	
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
		}

		
		
		/// <summary>
		/// DEPRECATED!!! - Use InsertTempLead(TempLead tempLead) instead
		/// Insert temp lead normally used when InsertNewLead fails
		/// </summary>
		public void InsertTempLead(Lead lead) 
		{
			string storedProcName = "efr_insert_temp_lead";
			bool useTransaction = false;

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@first_name", DbType.String, DBValue.ToDBString(lead.FirstName)));
				paramCol.Add(new SqlDataParameter("@last_name", DbType.String, DBValue.ToDBString(lead.LastName)));
				paramCol.Add(new SqlDataParameter("@email", DbType.String, DBValue.ToDBString(lead.Email)));
				paramCol.Add(new SqlDataParameter("@street_address", DbType.String, DBValue.ToDBString(lead.StreetAddress)));
				paramCol.Add(new SqlDataParameter("@city", DbType.String, DBValue.ToDBString(lead.City)));
				paramCol.Add(new SqlDataParameter("@state_code", DbType.String, DBValue.ToDBString(lead.State)));
				paramCol.Add(new SqlDataParameter("@zip_code", DbType.String, DBValue.ToDBString(lead.ZipCode)));
				paramCol.Add(new SqlDataParameter("@country_code", DbType.String, DBValue.ToDBString(lead.Country)));
				paramCol.Add(new SqlDataParameter("@day_phone", DbType.String, DBValue.ToDBString(lead.DayPhone)));
				paramCol.Add(new SqlDataParameter("@evening_phone", DbType.String, DBValue.ToDBString(lead.EveningPhone)));
				paramCol.Add(new SqlDataParameter("@participant_count", DbType.Int32, DBValue.ToDBInt32(lead.ParticipantCount)));
				paramCol.Add(new SqlDataParameter("@organization", DbType.String, DBValue.ToDBString(lead.GroupName)));

				if (lead.PromotionID == -1)
					paramCol.Add(new SqlDataParameter("@promotion_id", DbType.Int32, DBNull.Value));
				else
					paramCol.Add(new SqlDataParameter("@promotion_id", DbType.Int32, DBValue.ToDBInt32(lead.PromotionID)));

				paramCol.Add(new SqlDataParameter("@title", DbType.String, DBValue.ToDBString(lead.Title)));
				paramCol.Add(new SqlDataParameter("@evening_phone_ext", DbType.String, DBValue.ToDBString(lead.EveningPhoneExt)));
				paramCol.Add(new SqlDataParameter("@day_phone_ext", DbType.String, DBValue.ToDBString(lead.DayPhoneExt)));
				paramCol.Add(new SqlDataParameter("@best_time_to_call", DbType.String, DBValue.ToDBString(lead.BestTimeToCall)));
				paramCol.Add(new SqlDataParameter("@organization_type_id", DbType.Int32, DBValue.ToDBInt32(lead.OrganizationTypeID)));
				paramCol.Add(new SqlDataParameter("@group_type_id", DbType.Int32, DBValue.ToDBInt32(lead.GroupTypeID)));
				paramCol.Add(new SqlDataParameter("@fundraising_date", DbType.String, DBValue.ToDBString(lead.FundraisingDate)));
				paramCol.Add(new SqlDataParameter("@decision_maker", DbType.Boolean, DBValue.ToDBBoolean(lead.DecisionMaker)));
				paramCol.Add(new SqlDataParameter("@products_interest_in", DbType.String, DBValue.ToDBString(lead.ProductsInterest)));
				paramCol.Add(new SqlDataParameter("@on_email_list", DbType.Boolean, DBValue.ToDBBoolean(lead.OnEmailList)));
				paramCol.Add(new SqlDataParameter("@comments", DbType.String, DBValue.ToDBString(lead.Comments)));
				paramCol.Add(new SqlDataParameter("@lead_status_id", DbType.Int32, DBValue.ToDBInt32(lead.LeadStatusID)));
				paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, ParameterDirection.ReturnValue));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Execute
				si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

				lead.LeadID = DBValue.ToInt32(paramCol["@lead_id"].Value);
				
				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
		}

		
		public void UpdateTempLead(TempLead tempLead)
		{
			string storedProcName = "efr_update_temp_lead";
			
			SqlInterface si = new SqlInterface(dataProvider, connectionString);
			
			try 
			{
			
				// open the connection
				si.Open();
				
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@Division_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.DivisionID)));
				paramCol.Add(new SqlDataParameter("@Promotion_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.PromotionID)));
				paramCol.Add(new SqlDataParameter("@Temp_Lead_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.TempLeadID)));
				paramCol.Add(new SqlDataParameter("@Channel_Code", DbType.String, DBValue.ToDBString(tempLead.ChannelCode)));
				paramCol.Add(new SqlDataParameter("@Lead_Status_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.LeadStatusID)));
				paramCol.Add(new SqlDataParameter("@Consultant_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.ConsultantID)));
				paramCol.Add(new SqlDataParameter("@Lead_Entry_Date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.LeadEntryDate)));
				paramCol.Add(new SqlDataParameter("@Salutation", DbType.String, DBValue.ToDBString(tempLead.Salutation)));
				paramCol.Add(new SqlDataParameter("@First_Name", DbType.String, DBValue.ToDBString(tempLead.FirstName)));
				paramCol.Add(new SqlDataParameter("@Last_Name", DbType.String, DBValue.ToDBString(tempLead.LastName)));
				paramCol.Add(new SqlDataParameter("@Organization", DbType.String, DBValue.ToDBString(tempLead.Organization)));
				paramCol.Add(new SqlDataParameter("@Street_Address", DbType.String, DBValue.ToDBString(tempLead.StreetAddress)));
				paramCol.Add(new SqlDataParameter("@City", DbType.String, DBValue.ToDBString(tempLead.City)));
				paramCol.Add(new SqlDataParameter("@State_Code", DbType.String, DBValue.ToDBString(tempLead.StateCode)));
				paramCol.Add(new SqlDataParameter("@Country_Code", DbType.String, DBValue.ToDBString(tempLead.CountryCode)));
				paramCol.Add(new SqlDataParameter("@Zip_Code", DbType.String, DBValue.ToDBString(tempLead.ZipCode)));
				paramCol.Add(new SqlDataParameter("@Day_Phone", DbType.String, DBValue.ToDBString(tempLead.DayPhone)));
				paramCol.Add(new SqlDataParameter("@Day_Time_Call", DbType.String, DBValue.ToDBString(tempLead.DayTimeCall)));
				paramCol.Add(new SqlDataParameter("@Evening_Phone", DbType.String, DBValue.ToDBString(tempLead.EveningPhone)));
				paramCol.Add(new SqlDataParameter("@Fax", DbType.String, DBValue.ToDBString(tempLead.Fax)));
				paramCol.Add(new SqlDataParameter("@Email", DbType.String, DBValue.ToDBString(tempLead.Email)));
				paramCol.Add(new SqlDataParameter("@Group_Type_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.GroupTypeID)));
				paramCol.Add(new SqlDataParameter("@Participant_Count", DbType.Int32, DBValue.ToDBInt32(tempLead.ParticipantCount)));
				paramCol.Add(new SqlDataParameter("@Fund_Raising_Goal", DbType.Int32, DBValue.ToDBInt32(tempLead.FundRaisingGoal)));
				paramCol.Add(new SqlDataParameter("@Decision_Date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.DecisionDate)));
				paramCol.Add(new SqlDataParameter("@Decision_Maker", DbType.Boolean, DBValue.ToDBBoolean(tempLead.DecisionMaker)));
				paramCol.Add(new SqlDataParameter("@Fund_Raiser_Start_Date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.FundRaiserStartDate)));
				paramCol.Add(new SqlDataParameter("@OnEmailList", DbType.Boolean, DBValue.ToDBBoolean(tempLead.OnEmailList)));
				paramCol.Add(new SqlDataParameter("@Comments", DbType.String, DBValue.ToDBString(tempLead.Comments)));
				paramCol.Add(new SqlDataParameter("@Hear_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.HearID)));
				paramCol.Add(new SqlDataParameter("@Kit_to_send", DbType.Boolean, DBValue.ToDBBoolean(tempLead.KitToSend)));
				paramCol.Add(new SqlDataParameter("@Kit_sent", DbType.Boolean, DBValue.ToDBBoolean(tempLead.KitSent)));
				paramCol.Add(new SqlDataParameter("@Kit_sent_date", DbType.DateTime, DBValue.ToDBDateTime(tempLead.KitSentDate)));
				paramCol.Add(new SqlDataParameter("@Day_Phone_Ext", DbType.String, DBValue.ToDBString(tempLead.DayPhoneExt)));
				paramCol.Add(new SqlDataParameter("@Evening_Phone_Ext", DbType.String, DBValue.ToDBString(tempLead.EveningPhoneExt)));
				paramCol.Add(new SqlDataParameter("@Rejection_reason", DbType.String, DBValue.ToDBString(tempLead.RejectionReason)));
				paramCol.Add(new SqlDataParameter("@Other_Phone", DbType.String, DBValue.ToDBString(tempLead.OtherPhone)));
				paramCol.Add(new SqlDataParameter("@Cookie_Content", DbType.String, DBValue.ToDBString(tempLead.CookieContent)));
				paramCol.Add(new SqlDataParameter("@Group_Web_Site", DbType.String, DBValue.ToDBString(tempLead.GroupWebSite)));
				paramCol.Add(new SqlDataParameter("@Organization_Type_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.OrganizationTypeID)));
				paramCol.Add(new SqlDataParameter("@Title_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.TitleID)));
				paramCol.Add(new SqlDataParameter("@Campaign_Reason_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.CampaignReasonID)));
				paramCol.Add(new SqlDataParameter("@Web_Site_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.WebSiteID)));
				paramCol.Add(new SqlDataParameter("@Other_Phone_Ext", DbType.String, DBValue.ToDBString(tempLead.OtherPhoneExt)));
				paramCol.Add(new SqlDataParameter("@IsNew", DbType.Boolean, DBValue.ToDBBoolean(tempLead.IsNew)));
				paramCol.Add(new SqlDataParameter("@Opt_In_Sweepstakes", DbType.Boolean, DBValue.ToDBBoolean(tempLead.OptInSweepstakes)));
				paramCol.Add(new SqlDataParameter("@Group_ID", DbType.Int32, DBValue.ToDBInt32(tempLead.GroupID)));
		
				si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

			}
			catch (Exception ex)
			{
				throw new SqlDataException("Error updating database calling " + storedProcName);
			}
			finally 
			{
				// Always close connection.
				si.Close();
			}	
		}
		
		#endregion

		#region Consultants
		public Consultant GetConsultant(int consultantId)
		{
			Consultant c = null;

			bool useTransaction = false;
			string storedProcName = "efundraisingprod.dbo.es_get_consultant";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				si.Open();

				if(useTransaction)
					si.BeginTransaction();

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@consultant_id", DbType.Int32, DBValue.ToDBInt32(consultantId)));

				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				// fill our objects
				try 
				{
					if (dt.Rows.Count > 0) 
					{
						c = new Consultant();
						c.ConsultantId = DBValue.ToInt32(dt.Rows[0]["consultant_id"]);
						c.DivisionId = DBValue.ToByte(dt.Rows[0]["division_id"]);
						c.ClientId = DBValue.ToInt32(dt.Rows[0]["client_id"]);
						c.ClientSequenceCode = DBValue.ToString(dt.Rows[0]["client_sequence_code"]);
						c.DepartmentId = DBValue.ToInt32(dt.Rows[0]["department_id"]);
						c.PartnerId = DBValue.ToInt32(dt.Rows[0]["partner_id"]);
						c.ConsultantTransferStatusId = DBValue.ToByte(dt.Rows[0]["consultant_transfer_status_id"]);
						c.TerritoryId = DBValue.ToInt16(dt.Rows[0]["territory_id"]);
						c.ExtConsultantId = DBValue.ToInt32(dt.Rows[0]["ext_consultant_id"]);
						c.Name = DBValue.ToString(dt.Rows[0]["name"]);
						c.IsAgent = DBValue.ToBoolean(dt.Rows[0]["is_agent"]);
						c.IsActive = DBValue.ToBoolean(dt.Rows[0]["is_active"]);
						c.NtLogin = DBValue.ToString(dt.Rows[0]["nt_login"]);
						c.PhoneExtension = DBValue.ToString(dt.Rows[0]["phone_extension"]);
						c.EmailAddress = DBValue.ToString(dt.Rows[0]["email_address"]);
						c.HomePhone = DBValue.ToString(dt.Rows[0]["home_phone"]);
						c.WorkPhone = DBValue.ToString(dt.Rows[0]["work_phone"]);
						c.FaxNumber = DBValue.ToString(dt.Rows[0]["fax_number"]);
						c.TollFreePhone = DBValue.ToString(dt.Rows[0]["toll_free_phone"]);
						c.MobilePhone = DBValue.ToString(dt.Rows[0]["mobile_phone"]);
						c.PagerPhone = DBValue.ToString(dt.Rows[0]["pager_phone"]);
						c.DefaultProposalText = DBValue.ToString(dt.Rows[0]["default_proposal_text"]);
						c.CsrConsultant = DBValue.ToBoolean(dt.Rows[0]["csr_consultant"]);
						c.Objectives = DBValue.ToDouble(dt.Rows[0]["objectives"]);
						c.IsAvailable = DBValue.ToBoolean(dt.Rows[0]["is_available"]);
						c.Password = DBValue.ToString(dt.Rows[0]["password"]);
						c.KitPaid = DBValue.ToBoolean(dt.Rows[0]["kit_paid"]);
						c.IsFm = DBValue.ToBoolean(dt.Rows[0]["is_fm"]);
					}
						
				} 
				catch(System.Exception ex) 
				{
					throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
				}

				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}

			return c;
		}
		#endregion

		#region Tell a friend

		#region Insert
		public void InsertTellAFriend(TellFriendCollection tfc) 
		{
			string storedProcName = "ef_insert_tellfriend";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {
				// Open connection
				si.Open();

				// Begin transaction
				si.BeginTransaction();

				foreach(TellFriend tf in tfc.Friends) {
					// declare stored procedure parameters
					SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
					
					paramCol.Add(new SqlDataParameter("@cultureCode", DbType.String, DBValue.ToDBString(tf.CultureCode)));
					paramCol.Add(new SqlDataParameter("@fromName", DbType.String, DBValue.ToDBString(tf.FromName)));
					paramCol.Add(new SqlDataParameter("@fromEmail", DbType.String, DBValue.ToDBString(tf.FromEmail)));
					paramCol.Add(new SqlDataParameter("@toName", DbType.String, DBValue.ToDBString(tf.ToName)));
					paramCol.Add(new SqlDataParameter("@toEmail", DbType.String, DBValue.ToDBString(tf.ToEmail)));
					paramCol.Add(new SqlDataParameter("@subject", DbType.String, DBValue.ToDBString(tf.Subject)));
					paramCol.Add(new SqlDataParameter("@message", DbType.String, DBValue.ToDBString(tf.Message)));
					paramCol.Add(new SqlDataParameter("@dateSent", DbType.DateTime, DBValue.ToDBDateTime(tf.DateSent)));

					// Fetch and store into database.
					int ret = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

					if (ret <= 0) {
						throw new SqlDataException("Error inserting into database calling " + storedProcName);
					}
				}

				// Commit transaction
				si.Commit();
			} catch {
				si.Rollback();
				throw;
			}
			finally {
				si.Close();
			}
		}
		#endregion

		#endregion
		
		#region QSPLead

		private QSPLead LoadQSPLead(DataRow row) 
		{
			QSPLead lead = new QSPLead();

			// Store database values into our business object
			lead.LeadGenId = DBValue.ToInt32(row["LeadGen_Id"]);
			lead.FirstName = DBValue.ToString(row["First_Name"]);
			lead.LastName = DBValue.ToString(row["Last_Name"]);
			lead.Title = DBValue.ToString(row["Title"]);
			lead.Organization = DBValue.ToString(row["Organization"]);
			lead.Address1 = DBValue.ToString(row["Address1"]);
			lead.Address2 = DBValue.ToString(row["Address2"]);
			lead.City = DBValue.ToString(row["City"]);
			lead.State = DBValue.ToString(row["State"]);
            lead.Country = DBValue.ToString(row["Country"]);
			lead.Zip = DBValue.ToString(row["Zip"]);
			lead.DayPhone = DBValue.ToString(row["Phone_Day"]);
			lead.EveningPhone = DBValue.ToString(row["Phone_Evening"]);
			lead.Fax = DBValue.ToString(row["Fax"]);
			lead.Email = DBValue.ToString(row["EMail"]);
			lead.GoalOfFundraisers = DBValue.ToString(row["Goal_Of_Fundraisers"]);
			lead.NoOfFundraisers = DBValue.ToInt32(row["No_Of_Fundraisers"]);
			lead.NoOfYears = DBValue.ToInt32(row["No_Of_Years"]);
			lead.TimePeriod = DBValue.ToString(row["Time_Period"]);
			lead.MessageToRep = DBValue.ToString(row["Message_To_Rep"]);
			lead.Comment = DBValue.ToString(row["Comment"]);
			lead.Status = DBValue.ToByte(row["Status"]);
			lead.DateEntered = DBValue.ToDateTime(row["Date_Entered"]);
			lead.Origin = DBValue.ToString(row["Origin"]);
			lead.InternalTrackingId = DBValue.ToString(row["InternalTrackingId"]);
			lead.ExternalTrackingId = DBValue.ToString(row["ExternalTrackingId"]);
			lead.CreateDate = DBValue.ToDateTime(row["Create_Date"]);
			lead.ModifyDate = DBValue.ToDateTime(row["Modify_Date"]);
			lead.ModifiedBy = DBValue.ToString(row["Modified_By"]);
			lead.DeletedTF = DBValue.ToBoolean(row["Deleted_TF"]);

			return lead;
		}

		public QSPLead[] GetQSPLeadsToTransfer() 
		{
			return GetQSPLeadsToTransfer(null);
		}

		private QSPLead[] GetQSPLeadsToTransfer(SqlInterface si) 
		{
			QSPLead[] leads = null;

			string storedProcName = "efr_get_lead_from_qsp";

			// if the SqlInterface is passed as argument it means that 
			// this call should be applied to an already open connection
			// and the method which call this method is using transaction
			bool newConnection = true;
			if (si == null) 
			{
				si = new SqlInterface(dataProvider, connectionString);
			} 
			else 
			{
				newConnection = false;
			}

			try 
			{
				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
		
		
				if (newConnection) 
				{
					// open the connection
					si.Open();
				}

				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if (dt != null) 
				{
					leads = new QSPLead[dt.Rows.Count];

					for (int i = 0; i < dt.Rows.Count; i++) 
					{
						// fill our objects
						try 
						{
							leads[i] = LoadQSPLead(dt.Rows[i]);
						} 
						catch(Exception ex) 
						{
							throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
						}
					}
				}


			} 
			finally 
			{
				if(newConnection) 
				{
					// Always close connection.
					si.Close();
				}
			}
			return leads;
		}


		public void InsertLeadGen_Lead(int leadGenId, int leadId, DateTime createDate) 
		{
			string storedProcName = "efr_insert_leadgen_lead";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				// Open connection
				si.Open();

				// declare stored procedure parameters
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
					
				paramCol.Add(new SqlDataParameter("@LeadGen_id", DbType.Int32, DBValue.ToDBInt32(leadGenId)));
				paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, DBValue.ToDBInt32(leadId)));
				paramCol.Add(new SqlDataParameter("@create_date", DbType.DateTime, DBValue.ToDBDateTime(createDate)));

				// Fetch and store into database.
				si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

			} 
			catch(Exception ex) 
			{
				throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
			}
			finally 
			{
				si.Close();
			}
		}
		
		

		#endregion

	}
}
