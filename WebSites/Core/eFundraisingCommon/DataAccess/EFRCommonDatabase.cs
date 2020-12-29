using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using GA.BDC.Core.BusinessBase;
using GA.BDC.Core.Data.Sql;
using System.Data.SqlClient;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.eFundraisingCommon;

namespace GA.BDC.Core.eFundraisingCommon.DataAccess
{
    /// <summary>
    /// Summary description for EFRCommonDatabase.
    /// </summary>
    public class EFRCommonDatabase : GA.BDC.Core.Data.Sql.DatabaseObject
    {
        public EFRCommonDatabase()
        {
            SetConnectionString(Config.EFRCommonConnectionString);
            SetDataProvider(Config.EFRCommonDataProvider);
        }

        
        #region Promotion
        public Promotion LoadPromotion(DataRow row)
        {
            Promotion p = new Promotion();
            p.PromotionId = DBValue.ToInt32(row["promotion_id"]);
            p.PromotionTypeCode = DBValue.ToString(row["promotion_type_code"]);
            //p.TargetedMarketId = DBValue.ToInt32(row["targeted_market_id"]);
            //p.AdvertisingSupportId = DBValue.ToInt32(row["advertising_support_id"]);
            //p.AdvertisementId = DBValue.ToInt32(row["advertisement_id"]);
            //p.PartnerId = DBValue.ToInt32(row["partner_id"]);
            //p.AdvertiserId = DBValue.ToInt32(row["advertiser_id"]);
            //p.AdvertismentTypeId = DBValue.ToInt32(row["advertisment_type_id"]);
            //p.DestinationId = DBValue.ToInt32(row["destination_id"]);
            //p.AdvertiserPartnerId = DBValue.ToInt32(row["advertiser_partner_id"]);
            //p.GrabberId = DBValue.ToInt32(row["grabber_id"]);
            //p.Description = DBValue.ToString(row["description"]);
            p.ScriptName = DBValue.ToString(row["script_name"]);
            //p.ContactName = DBValue.ToString(row["contact_name"]);
            //p.Visibility = DBValue.ToString(row["visibility"]);
            //p.TrackingSerial = DBValue.ToString(row["tracking_serial"]);
           // p.NbImpressionBought = DBValue.ToInt32(row["nb_impression_bought"]);
            p.IsActive = DBValue.ToBoolean(row["active"]);
            p.CookieContent = DBValue.ToString(row["cookie_content"]);
            //p.IsPredictive = DBValue.ToBoolean(row["is_predictive"]);
            p.Keyword = DBValue.ToString(row["keyword"]);

            return p;
        }



        public Promotion GetPromotionBYABID(string abid, string aaid)
        {
            Promotion p = null;

            bool useTransaction = false;
            string storedProcName = "efrc_get_promotion_by_aaid_abid";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@a_aid", DbType.String, DBValue.ToDBString(aaid)));
                paramCol.Add(new SqlDataParameter("@a_bid", DbType.String, DBValue.ToDBString(abid)));

                // Fetch and store into database.
                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                // fill our objects
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        p = LoadPromotion(dt.Rows[0]);
                    }

                }
                catch (System.Exception ex)
                {
                    throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                }

                // Commit our transaction.
                if (useTransaction)
                    si.Commit();
            }
            catch
            {
                // Rollback on error.
                if (useTransaction)
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
        
        
        
        public Promotion GetPromotion(int promotionId)
        {
            Promotion p = null;

            bool useTransaction = false;
            string storedProcName = "efr_get_promotion";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@promotion_id", DbType.Int32, DBValue.ToDBInt32(promotionId)));

                // Fetch and store into database.
                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                // fill our objects
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        p = LoadPromotion(dt.Rows[0]);
                    }

                }
                catch (System.Exception ex)
                {
                    throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                }

                // Commit our transaction.
                if (useTransaction)
                    si.Commit();
            }
            catch
            {
                // Rollback on error.
                if (useTransaction)
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

        

        public int InsertNewPromo(int partner, string promoType, string promoName, int displayable, string abid)
        {
            int outputPromoID = int.MinValue;
            string storedProcName = "efrc_insert_promotion";
            bool useTransaction = false;

            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try{
                    
                     SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                     paramCol.Add(new SqlDataParameter("@script_name", DbType.String, DBValue.ToDBString(abid)));
                    paramCol.Add(new SqlDataParameter("@partner_id", DbType.Int32, DBValue.ToInt32(partner)));
                    paramCol.Add(new SqlDataParameter("@promotion_type_code", DbType.String, DBValue.ToDBString(promoType)));
                    paramCol.Add(new SqlDataParameter("@promotion_name", DbType.String, DBValue.ToDBString(promoName)));
                    paramCol.Add(new SqlDataParameter("@is_displayable", DbType.Int32, DBValue.ToInt32(displayable)));
                    paramCol.Add(new SqlDataParameter("@promotion_id", DbType.Int32, ParameterDirection.Output));
                    si.Open();

                    if (useTransaction)
                        si.BeginTransaction();

                    // Execute
                    si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

                    outputPromoID = DBValue.ToInt32(paramCol["@promotion_id"].Value);

                    // Commit our transaction.
                    if (useTransaction)
                        si.Commit();

                    return outputPromoID;
            }
            catch (Exception ex)
            {
                // Rollback on error.
                if (useTransaction)
                    si.Rollback();


                throw ex;
            }
            finally
            {
                // Always close connection.
                si.Close();
            }
        }

        public Promotion GetPromotion(string scriptName)
        {
            Promotion p = null;

            bool useTransaction = false;
            string storedProcName = "efrc_get_default_promotion_by_aaid";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@a_aid", DbType.String, DBValue.ToDBString(scriptName)));

                // Fetch and store into database.
                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                // fill our objects
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        p = LoadPromotion(dt.Rows[0]);
                    }

                }
                catch (System.Exception ex)
                {
                    throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                }

                // Commit our transaction.
                if (useTransaction)
                    si.Commit();
            }
            catch
            {
                // Rollback on error.
                if (useTransaction)
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

        


        #region Partner Section Methods

        //
        public int InsertPartner(string papid, string name, string path, string folder, string esubsurl, string tempdesc)
        {

            Partner partner = new Partner();
            string storedProcName = "efrc_create_partner";
            

            bool useTransaction = false;

            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try{
                    
                    SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                    paramCol.Add(new SqlDataParameter("@pap_id", DbType.String, DBValue.ToDBString(papid)));
                    paramCol.Add(new SqlDataParameter("@partner_name", DbType.String, DBValue.ToDBString(name)));
                    paramCol.Add(new SqlDataParameter("@partner_path", DbType.String, DBValue.ToDBString(path)));
                    paramCol.Add(new SqlDataParameter("@partner_folder", DbType.String, DBValue.ToDBString(folder)));
                    paramCol.Add(new SqlDataParameter("@esubs_url",DbType.String, DBValue.ToDBString(esubsurl)));
                    paramCol.Add(new SqlDataParameter("@template_description", DbType.String, DBValue.ToDBString(tempdesc)));
                    paramCol.Add(new SqlDataParameter("@partner_id", DbType.Int32, ParameterDirection.Output));
                    paramCol.Add(new SqlDataParameter("@guid", DbType.Guid, ParameterDirection.Output));
       
                     
                    si.Open();

                    if (useTransaction)
                        si.BeginTransaction();

                    // Execute
                    si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

                    partner.PartnerID = DBValue.ToInt32(paramCol["@partner_id"].Value);
                   

                    // Commit our transaction.
                    if (useTransaction)
                        si.Commit();

                    return partner.PartnerID;
            }
            catch (Exception ex)
            {
                // Rollback on error.
                if (useTransaction)
                    si.Rollback();


                throw ex;
            }
            finally
            {
                // Always close connection.
                si.Close();
            }
       
        }
        
        
        public Partner GetPAPPartnerInfo(string a_aid)
        {

            Partner partner = new Partner();

            string storedProcName = "efrc_get_partner_by_aaid";
            bool useTransaction = false;

            SqlInterface si = new SqlInterface(dataProvider, connectionString);


            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@a_aid", DbType.String, DBValue.ToDBString(a_aid)));

                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                // Fetch the database.
                DataTable dt1 = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                // read the result
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow row in dt1.Rows)
                    {
                        partner.PartnerID = DBValue.ToInt32(row["partner_id"]);
                        partner.PartnerName = DBValue.ToString(row["partner_name"]);
                        //partner.PhoneNumber = DBValue.ToString(row["phone_number"]);
                        partner.GUID = DBValue.ToString(row["guid"]);
                        partner.HasCollectionSite = DBValue.ToInt32(row["has_collection_site"]) == 1 ? true : false;
                        //partner.PartnerFolder = DBValue.ToString(row["partner_folder"]);
                        partner.PartnerTypeID = DBValue.ToInt32(row["partner_type_id"]);
                        partner.PartnerTypeName = DBValue.ToString(row["partner_type_name"]);
                    }

                }
                

                // Commit our transaction.
                if (useTransaction)
                    si.Commit();
            }
            catch
            {
                // Rollback on error.
                if (useTransaction)
                    si.Rollback();

                // throw exception
                throw;
            }
            finally
            {
                // Always close connection.
                si.Close();
            }

            return partner;
        
        }
        
        
        
        
        
        
        public Partner GetPartnerInfoByURL(string url)
        {

            Partner partner = new Partner();
            partner.Url = url;

            string storedProcName = "efr_get_partner_by_url";
            bool useTransaction = false;

            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {

                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@url", DbType.String, DBValue.ToDBString(url)));

                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                // Fetch the database.
                DataTable dt1 = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                // read the result
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow row in dt1.Rows)
                    {
                        partner.PartnerID = DBValue.ToInt32(row["partner_id"]);
                        partner.PartnerName = DBValue.ToString(row["partner_name"]);
                        partner.PhoneNumber = DBValue.ToString(row["phone_number"]);
                        partner.GUID = DBValue.ToString(row["guid"]);
                        partner.HasCollectionSite = DBValue.ToInt32(row["has_collection_site"]) == 1 ? true : false;
                        partner.PartnerFolder = DBValue.ToString(row["partner_folder"]);
                    }
                }
                else
                {
                    partner = null;
                    partner = this.GetPartnerInfoByID(0);
                    partner.Url = url;
                }

                // Commit our transaction.
                if (useTransaction)
                    si.Commit();
            }
            catch
            {
                // Rollback on error.
                if (useTransaction)
                    si.Rollback();

                // throw exception
                throw;
            }
            finally
            {
                // Always close connection.
                si.Close();
            }

            return partner;
        }
        
        
        public Partner GetPartnerInfoByID(int partnerID)
        {
            Partner partner = new Partner();
            partner.PartnerID = partnerID;
            string storedProcName = "efrc_get_partner_by_partner_id";
            bool useTransaction = false;

            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {

                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@partnerID", DbType.Int32, DBValue.ToDBInt32(partnerID)));

                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                // Fetch the database.
                DataTable dt1 = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                // read the result
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow row in dt1.Rows)
                    {
                        partner.PartnerName = DBValue.ToString(row["partner_name"]);
                        //partner.PhoneNumber = DBValue.ToString(row["phone_number"]);
                        partner.GUID = DBValue.ToString(row["guid"]);
                        //partner.ESubsUrl = DBValue.ToString(row["esubs_url"]);
                        partner.HasCollectionSite = DBValue.ToInt32(row["has_collection_site"]) == 1 ? true : false;
                        //partner.PartnerFolder = DBValue.ToString(row["partner_folder"]);
                        //partner.Url = DBValue.ToString(row["url"]);
                    }
                }

                // Commit our transaction.
                if (useTransaction)
                    si.Commit();
            }
            catch
            {
                // Rollback on error.
                if (useTransaction)
                    si.Rollback();

                // throw exception
                throw;
            }
            finally
            {
                // Always close connection.
                si.Close();
            }

            return partner;
        }
        
        
        
        
        public Partner GetPartnerInfoByFolder(string folder)
        {

            Partner partner = new Partner();
            partner.PartnerFolder = folder;

            string storedProcName = "efr_get_partner_by_partner_folder";
            bool useTransaction = false;

            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {

                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@partner_folder", DbType.String, DBValue.ToDBString(folder)));

                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                // Fetch the database.
                DataTable dt1 = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                // read the result
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow row in dt1.Rows)
                    {
                        partner.PartnerID = DBValue.ToInt32(row["partner_id"]);
                        partner.PartnerName = DBValue.ToString(row["partner_name"]);
                        partner.PhoneNumber = DBValue.ToString(row["phone_number"]);
                        partner.GUID = DBValue.ToString(row["guid"]);
                        partner.HasCollectionSite = DBValue.ToInt32(row["has_collection_site"]) == 1 ? true : false;
                        partner.Url = DBValue.ToString(row["url"]);
                    }
                }
                else
                {
                    partner = null;
                    partner = this.GetPartnerInfoByID(0);
                }

                // Commit our transaction.
                if (useTransaction)
                    si.Commit();
            }
            catch
            {
                // Rollback on error.
                if (useTransaction)
                    si.Rollback();

                // throw exception
                throw;
            }
            finally
            {
                // Always close connection.
                si.Close();
            }

            return partner;
        }
        
        
        
        
        
        
        
        
        
        
        private Partner LoadPartner(DataRow row)
        {
            Partner partner = new Partner();
            partner.PartnerID = DBValue.ToInt32(row["partner_id"]);
            partner.PartnerTypeID = DBValue.ToInt32(row["partner_type_id"]);
            partner.Name = DBValue.ToString(row["description"]);
            partner.HasCollectionSite = DBValue.ToBoolean(row["has_collection_site"]);
            partner.GUID = DBValue.ToString(row["guid"]);
            return partner;
        }

        public List<Partner> GetPartners()
        {
            return GetPartners(null);
        }
        
        private List<Partner> GetPartners(SqlInterface si)
        {
            List<Partner> partners = new List<Partner>();

            string storedProcName = "efrc_get_partners";

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

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            partners.Add(LoadPartner(dt.Rows[i]));
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return partners; 
        }

        public Partner GetPartnerByID( int id)
        {
            return GetPartnerByID(id, null);
        }
        private Partner GetPartnerByID(int id, SqlInterface si)
        {
            Partner partner = null;

            string storedProcName = "efrc_get_partner_by_id";

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
                paramCol.Add(new SqlDataParameter("@partner_id", DbType.Int32, DBValue.ToDBInt32(id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            partner = LoadPartner(dt.Rows[i]);
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return partner;
        }

        #endregion

        #region ProfitGroup

     
        private ProfitGroup LoadProfitGroup(DataRow row)
        {

            ProfitGroup pg = new ProfitGroup();

            // Store database values into our business object
           
            pg.Description = DBValue.ToString(row["description"]);
            pg.Disclaimer = DBValue.ToString(row["disclaimer"]);
            pg.AltDisclaimer = DBValue.ToString(row["alt_disclaimer"]);
            
            // return the filled object
            return pg;
        }


        public ProfitGroup GetProfitGroupByID(int id, SqlInterface si)
        {
            ProfitGroup profitGroup = null;

            string storedProcName = "efrc_get_profit_group_by_id";

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
                paramCol.Add(new SqlDataParameter("@profit_group_id", DbType.Int32, DBValue.ToDBInt32(id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            profitGroup = LoadProfitGroup(dt.Rows[i]);
                            profitGroup.ProfitGroup_id = id;
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            catch
            {
                // throw exception
                throw;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return profitGroup;
        }

        #endregion

        #region PartnerProfit Section

        private PartnerProfit LoadPartnerProfit(DataRow row)
        {
            PartnerProfit partnerProfit = new PartnerProfit();

            // Store database values into our business object
            partnerProfit.PartnerProfitId = DBValue.ToInt32(row["partner_profit_id"]);
            partnerProfit.PartnerId = DBValue.ToInt32(row["partner_id"]);            
            partnerProfit.StartDate = DBValue.ToDateTime(row["start_date"]);
            partnerProfit.EndDate = DBValue.ToDateTime(row["end_date"]);
            partnerProfit.ProfitGroupID = DBValue.ToInt32(row["profit_group_id"]);

            // return the filled object
            return partnerProfit;
        }

        public List<PartnerProfit> GetPartnerProfits()
        {
            return GetPartnerProfits(null);
        }

        private List<PartnerProfit> GetPartnerProfits(SqlInterface si)
        {
            List<PartnerProfit> partnerProfits = new List<PartnerProfit>();

            string storedProcName = "efrc_get_partner_profits";

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

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            partnerProfits.Add(LoadPartnerProfit(dt.Rows[i]));
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            catch
            {
                // throw exception
                throw;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return partnerProfits;
        }

        public List<PartnerProfit> GetPartnerProfitsByID(int id)
        {
            return GetPartnerProfitsByID(id, null);
        }

        private List<PartnerProfit> GetPartnerProfitsByID(int id, SqlInterface si)
        {
            List<PartnerProfit> partnerProfits = new List<PartnerProfit>();

            string storedProcName = "efrc_get_partner_profit_by_id";

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
                paramCol.Add(new SqlDataParameter("@partner_id", DbType.Int32, DBValue.ToDBInt32(id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            partnerProfits.Add(LoadPartnerProfit(dt.Rows[i]));
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            catch
            {
                // throw exception
                throw;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return partnerProfits;
        }

        public PartnerProfit GetCurrentPartnerProfitByID(int id)
        {
            return GetCurrentPartnerProfitByID(id, null);
        }

        private PartnerProfit GetCurrentPartnerProfitByID(int id, SqlInterface si)
        {
            PartnerProfit partnerProfit = null;

            string storedProcName = "efrc_get_current_partner_profit_by_id";

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
                paramCol.Add(new SqlDataParameter("@partner_id", DbType.Int32, DBValue.ToDBInt32(id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            partnerProfit = LoadPartnerProfit(dt.Rows[i]);
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            catch
            {
                // throw exception
                throw;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return partnerProfit;
        }

        public List<PartnerProfit> GetCurrentPartnerProfits()
        {
            return GetCurrentPartnerProfits(null);
        }

        private List<PartnerProfit> GetCurrentPartnerProfits(SqlInterface si)
        {
            List<PartnerProfit> partnerProfits = new List<PartnerProfit>();

            string storedProcName = "efrc_get_current_partner_profits";

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

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            partnerProfits.Add(LoadPartnerProfit(dt.Rows[i]));
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            catch
            {
                // throw exception
                throw;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return partnerProfits;
        }
        #endregion

        #region Profit Section Methods
        
        private Profit LoadProfit(DataRow row)
        {
            Profit profit = new Profit();

            // Store database values into our business object
            profit.ProfitId = DBValue.ToInt32(row["profit_id"]);
            profit.ProfitPercentage = DBValue.ToDouble(row["profit_percentage"]);
            profit.Description = DBValue.ToString(row["description"]);
            profit.Disclaimer = DBValue.ToString(row["disclaimer"]);
            profit.AltDisclaimer = DBValue.ToString(row["alt_disclaimer"]);
            profit.ProfitGroupID = DBValue.ToInt32(row["profit_group_id"]);
            profit.QspCatalogTypeID = DBValue.ToInt32(row["qsp_catalog_type_id"]);

            // return the filled object
            return profit;

        }

        public List<Profit> GetProfits()
        {
            return GetProfits(null);
        }

        private List<Profit> GetProfits(SqlInterface si)
        {
            List<Profit> profits = new List<Profit>();

            string storedProcName = "efrc_get_profits";

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

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            profits.Add(LoadProfit(dt.Rows[i]));
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            catch
            {
                // throw exception
                throw;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return profits;
        }

        public Profit GetProfitByID(int id)
        {
            return GetProfitByID(id, null);
        }

        private Profit GetProfitByID(int id, SqlInterface si)
        {
            Profit profit = null;

            string storedProcName = "efrc_get_profit_by_id";

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
                paramCol.Add(new SqlDataParameter("@profit_id", DbType.Int32, DBValue.ToDBInt32(id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            profit = LoadProfit(dt.Rows[i]);
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            catch
            {
                // throw exception
                throw;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return profit;
        }

        public List<Profit> GetProfitByProfitGroupID(int id)
        {
            return GetProfitByProfitGroupID(id, null);
        }

        private List<Profit> GetProfitByProfitGroupID(int id, SqlInterface si)
        {
            List<Profit> profits = new List<Profit>();

            string storedProcName = "efrc_get_profit_by_profit_group_id";

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
                paramCol.Add(new SqlDataParameter("@profit_group_id", DbType.Int32, DBValue.ToDBInt32(id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            profits.Add(LoadProfit(dt.Rows[i]));
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            catch
            {
                // throw exception
                throw;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return profits;
        }

        #endregion

        #region Profit Range Section
     
        private ProfitRange LoadProfitRange(DataRow row) 
        {
            ProfitRange profit_range = new  ProfitRange();

	        // Store database values into our business object
	        profit_range.ProfitRangeID = DBValue.ToInt32(row["profit_range_id"]);
	        profit_range.ProfitID = DBValue.ToInt32(row["profit_id"]);
	        profit_range.ProfitRangePercentage = DBValue.ToDouble(row["profit_range_percentage"]);
	        profit_range.MinSub = DBValue.ToInt32(row["min_sub"]);
	        profit_range.MinAmount= DBValue.ToInt32(row["min_amount"]);
	        profit_range.Operator = DBValue.ToString(row["operator"]);
	        profit_range.Disclaimer = DBValue.ToString(row["disclaimer"]);

	        // return the filled object
	        return profit_range;
        }

        public List<ProfitRange> GetProfitRanges()
        {
           return GetProfitRanges(null);
        }

        private List<ProfitRange> GetProfitRanges(SqlInterface si)
        {
            List<ProfitRange> profitRanges = new List<ProfitRange>();

            string storedProcName = "efrc_get_profit_ranges";

	        // if the SqlInterface is passed as argument it means that 
	        // this call should be applied to an already open connection
	        // and the method which call this method is using transaction
	        bool newConnection = true;
	        if (si == null)
            {
	            si = new SqlInterface(dataProvider, connectionString);
	        } else
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
	                   for (int i = 0; i < dt.Rows.Count; i++)	
                       {
			                // fill our objects
			                    try
                                {
                                    profitRanges.Add(LoadProfitRange(dt.Rows[i]));
			                    } 
                                catch(Exception ex) 
                                {
				                    throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
			                    }
		                 }
	             }

                return profitRanges;
	            }
                finally 
                {
                   if(newConnection)
                   {
		                // Always close connection.
		                si.Close();
                   }
	            }
	    
        }

   
        public List<ProfitRange> GetProfitRangeByProfitID(int id)
        {
            return GetProfitRangeByProfitID(id, null);
        }

        private List<ProfitRange> GetProfitRangeByProfitID(int id, SqlInterface si)
        {
            List<ProfitRange> profitRanges = new List<ProfitRange>();

	        string storedProcName = "efrc_get_profit_ranges_by_profit_id";

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
		        paramCol.Add(new SqlDataParameter("@profit_id", DbType.Int32, DBValue.ToDBInt32(id)));
        		
               if (newConnection)
               {
                   // open the connection
                   si.Open();
               }

		        DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

						if(dt != null && dt.Rows.Count > 0) 
				        {
					        for (int i = 0; i < dt.Rows.Count; i++)	
                             {
			                    // fill our objects
			                        try
                                    {
                                        profitRanges.Add(LoadProfitRange(dt.Rows[i]));
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
            return profitRanges;
        }
   

      #endregion

        #region Advertising Section Methods
        public int InsertNewAdvertising(int lead_id, int org_promotion_id, int advertsing_type_id, string firstName, string lastName, string phone, string email, string compagnie_name, string compagnie_url, string display_url, string listing_text, string is_visible, string image_type, DateTime start_date, DateTime end_date)
        {

            int advertisingID = int.MinValue;
            string storedProcName = "efrc_insert_new_advertising_client";
            bool useTransaction = false;

            SqlInterface si = new SqlInterface(dataProvider, connectionString);
            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@advertising_id", DbType.Int32, ParameterDirection.Output));
                paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, DBValue.ToDBInt32(lead_id)));
                paramCol.Add(new SqlDataParameter("@org_promotion_id", DbType.Int32, DBValue.ToDBInt32(org_promotion_id)));
                paramCol.Add(new SqlDataParameter("@advertising_type_id", DbType.Int32, DBValue.ToDBInt32(advertsing_type_id)));
                paramCol.Add(new SqlDataParameter("@first_name", DbType.String, DBValue.ToDBString(firstName)));
                paramCol.Add(new SqlDataParameter("@last_name", DbType.String, DBValue.ToDBString(lastName)));
                paramCol.Add(new SqlDataParameter("@phone", DbType.String, DBValue.ToDBString(phone)));
                paramCol.Add(new SqlDataParameter("@email", DbType.String, DBValue.ToDBString(email)));
                paramCol.Add(new SqlDataParameter("@compagnie_name", DbType.String, DBValue.ToDBString(compagnie_name)));
                paramCol.Add(new SqlDataParameter("@compagnie_url", DbType.String, DBValue.ToDBString(compagnie_url)));
                paramCol.Add(new SqlDataParameter("@display_url", DbType.String, DBValue.ToDBString(display_url)));
                paramCol.Add(new SqlDataParameter("@listing_text", DbType.String, DBValue.ToDBString(listing_text)));
                paramCol.Add(new SqlDataParameter("@is_visible", DbType.String, DBValue.ToDBString(is_visible)));
                paramCol.Add(new SqlDataParameter("@image_type", DbType.String, DBValue.ToDBString(image_type)));
                paramCol.Add(new SqlDataParameter("@start_date", DbType.DateTime, DBValue.ToDateTime(start_date)));
                paramCol.Add(new SqlDataParameter("@end_date", DbType.DateTime, DBValue.ToDateTime(end_date)));
                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                // Execute
                si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);
                
                advertisingID = DBValue.ToInt32(paramCol["@advertising_id"].Value);

                // Commit our transaction.
                if (useTransaction)
                    si.Commit();

            }
            catch (Exception ex)
            {
                // Rollback on error.
                if (useTransaction)
                    si.Rollback();
                
                throw ex;
            }
            finally
            {
                // Always close connection.
                si.Close();
            }
            return advertisingID;
        }

        public void UpdateClientAdvertisingInfo(int lead_id, int advertsing_type_id, string first_name, string last_name, string phone, string email, string compagnie_name, string compagnie_url, string display_url, string listing_text, string is_visible, DateTime start_date, DateTime end_date)
        {

            string storedProcName = "efrc_update_advertising_client_info";
            bool useTransaction = false;

            SqlInterface si = new SqlInterface(dataProvider, connectionString);
            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, DBValue.ToDBInt32(lead_id)));
                paramCol.Add(new SqlDataParameter("@advertising_type_id", DbType.Int32, DBValue.ToDBInt32(advertsing_type_id)));
                paramCol.Add(new SqlDataParameter("@first_name", DbType.String, DBValue.ToDBString(first_name)));
                paramCol.Add(new SqlDataParameter("@last_name", DbType.String, DBValue.ToDBString(last_name)));
                paramCol.Add(new SqlDataParameter("@phone", DbType.String, DBValue.ToDBString(phone)));
                paramCol.Add(new SqlDataParameter("@email", DbType.String, DBValue.ToDBString(email)));
                paramCol.Add(new SqlDataParameter("@compagnie_name", DbType.String, DBValue.ToDBString(compagnie_name)));
                paramCol.Add(new SqlDataParameter("@compagnie_url", DbType.String, DBValue.ToDBString(compagnie_url)));
                paramCol.Add(new SqlDataParameter("@display_url", DbType.String, DBValue.ToDBString(display_url)));
                paramCol.Add(new SqlDataParameter("@listing_text", DbType.String, DBValue.ToDBString(listing_text)));
                paramCol.Add(new SqlDataParameter("@is_visible", DbType.String, DBValue.ToDBString(is_visible)));
                paramCol.Add(new SqlDataParameter("@start_date", DbType.DateTime, DBValue.ToDateTime(start_date)));
                paramCol.Add(new SqlDataParameter("@end_date", DbType.DateTime, DBValue.ToDateTime(end_date)));
                
                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                // Execute
                si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);


                // Commit our transaction.
                if (useTransaction)
                    si.Commit();

            }
            catch (Exception ex)
            {
                // Rollback on error.
                if (useTransaction)
                    si.Rollback();

                throw ex;
            }
            finally
            {
                // Always close connection.
                si.Close();
            }
        }
        
        private Advertising LoadClient(DataRow row)
        {
            Advertising advlist = new Advertising();
            advlist.advertising_id = DBValue.ToInt32(row["advertising_id"]);
            advlist.first_name = DBValue.ToString(row["first_name"]);
            advlist.last_name = DBValue.ToString(row["last_name"]);
            advlist.compagnie_name = DBValue.ToString(row["compagnie_name"]);
            advlist.phone = DBValue.ToString(row["phone"]);
            advlist.email = DBValue.ToString(row["email"]);
            advlist.listing_text = DBValue.ToString(row["listing_text"]);
            advlist.compagnie_url = DBValue.ToString(row["compagnie_url"]);
            advlist.display_url = DBValue.ToString(row["display_url"]);
            advlist.advertsing_type_id = DBValue.ToInt32(row["advertising_type_id"]);
            advlist.picture_url = (byte[])row["picture_url"];
            advlist.start_date = DBValue.ToDateTime(row["start_date"]);
            advlist.end_date = DBValue.ToDateTime(row["end_date"]);
            advlist.image_type = DBValue.ToString(row["image_type"]);
            return advlist;
        }

        public Advertising GetClientInformation(int leadID)
        {
            return GetClientInformation(leadID, null);
        }
    
        public Advertising GetClientInformation(int leadID, SqlInterface si)
        {
            Advertising advlist = null;
            string storedProcName = "efrc_get_advertising_client_info_by_lead_id";

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
                paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, DBValue.ToInt32(leadID)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            advlist = LoadClient(dt.Rows[i]);
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return advlist;
       
        }
        #endregion


        public int InsertImage(int advertID, byte[] picture_url, string image_type)
        {
            return InsertImage(advertID, picture_url, image_type, null);
        }

        private int InsertImage(int advertID, byte[] picture_url, string image_type, SqlInterface si)
        {
            int results;
            string storedProcName = "dbo.efrc_insert_image_for_new_client";


            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;

            if (si == null)
            {
                newConnection = true;
            }
            else
            {
                newConnection = false;
            }


            System.Data.SqlClient.SqlConnection myConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            System.Data.SqlClient.SqlCommand storeimage = new System.Data.SqlClient.SqlCommand(storedProcName, myConnection);
            storeimage.CommandType = CommandType.StoredProcedure;
            storeimage.Parameters.Add("@advertising_id", SqlDbType.Int).Value = advertID;
            storeimage.Parameters.Add("@picture_url", SqlDbType.Image, picture_url.Length).Value = picture_url;
            storeimage.Parameters.Add("@image_type", SqlDbType.VarChar, 100).Value = image_type;

            try
            {
                if (newConnection)
                {
                    // open the connection
                    myConnection.Open();
                }

                results = storeimage.ExecuteNonQuery();
                                
            }
            catch (Exception ex)
            {
                throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    myConnection.Close();
                }
            }
            return results;
        }

        public void UpdateClientImage(int lead_id, byte[] image, string imageType)
        {
            SqlInterface si = new SqlInterface(dataProvider, connectionString);
            string storedProcName = "dbo.efrc_update_image_for_client";


            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;

            if (si == null)
            {
                newConnection = false;
            }
            else
            {
                newConnection = true;
            }

            System.Data.SqlClient.SqlConnection myConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            System.Data.SqlClient.SqlCommand storeimage = new System.Data.SqlClient.SqlCommand(storedProcName, myConnection);
            storeimage.CommandType = CommandType.StoredProcedure;
            storeimage.Parameters.Add("@lead_id", SqlDbType.Int).Value = lead_id;
            storeimage.Parameters.Add("@picture_url", SqlDbType.Image, image.Length).Value = image;
            storeimage.Parameters.Add("@image_type", SqlDbType.VarChar, 100).Value = imageType;

            try
            {
                if (newConnection)
                {
                    // open the connection
                    myConnection.Open();
                }

                // Execute
                storeimage.ExecuteNonQuery();

                
            }
            catch (Exception ex)
            {
                // throw exception
                throw ex;
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    myConnection.Close();
                }
            }

        }

        public void InsertAdvertisingListing(int listing_id, int advertID, DateTime start_date, DateTime end_date)
        {
            string storedProcName = "efrc_insert_advertising_listing";
            bool useTransaction = false;

            SqlInterface si = new SqlInterface(dataProvider, connectionString);
            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@listing_id", DbType.Int32, DBValue.ToDBInt32(listing_id)));
                paramCol.Add(new SqlDataParameter("@advertising_id", DbType.Int32, DBValue.ToDBInt32(advertID)));
                paramCol.Add(new SqlDataParameter("@start_date", DbType.DateTime, DBValue.ToDateTime(start_date)));
                paramCol.Add(new SqlDataParameter("@end_date", DbType.DateTime, DBValue.ToDateTime(end_date)));

                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                // Execute
                si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);


                // Commit our transaction.
                if (useTransaction)
                    si.Commit();

            }
            catch (Exception ex)
            {
                // Rollback on error.
                if (useTransaction)
                    si.Rollback();

                throw ex;
            }
            finally
            {
                // Always close connection.
                si.Close();
            }
          
        }

        private Advertising LoadAdvertising(DataRow row)
        {
            Advertising advertInfo = new Advertising();
            advertInfo.lead_id = DBValue.ToInt32(row["lead_id"]);
            advertInfo.compagnie_name = DBValue.ToString(row["compagnie_name"]);
            advertInfo.compagnie_url = DBValue.ToString(row["compagnie_url"]);
            advertInfo.display_url = DBValue.ToString(row["display_url"]);
            advertInfo.listing_text = DBValue.ToString(row["listing_text"]);
            advertInfo.picture_url = (byte[])row["picture_url"];
            advertInfo.image_type = DBValue.ToString(row["image_type"]);
            return advertInfo;
        }

        public List<Advertising> GetAdvertisingInfo(int advertID)
        {
            return GetAdvertisingInfo(advertID, null);
        }

        private List<Advertising> GetAdvertisingInfo(int advertID, SqlInterface si)
        {
            List<Advertising> advertising = new List<Advertising>();

            string storedProcName = "efrc_get_advertising_information_for_webpage";

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
                paramCol.Add(new SqlDataParameter("@advertising_type_id", DbType.Int32, DBValue.ToDBInt32(advertID)));
                
                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            advertising.Add(LoadAdvertising(dt.Rows[i]));
                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return advertising;
        }
    }
}
