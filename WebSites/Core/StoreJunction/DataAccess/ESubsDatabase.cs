using System;
using System.Data;

using GA.BDC.Core.Data.Sql;
using GA.BDC.Core.BusinessBase;

namespace GA.BDC.Core.StoreJunction.DataAccess
{
	/// <summary>
	/// Summary description for ESubsDatabase.
	/// </summary>
    public class ESubsDatabase : GA.BDC.Core.Data.Sql.DatabaseObject
    {
		public ESubsDatabase()
        {
            SetConnectionString(AppConfig.ConnectionString);
            SetDataProvider(AppConfig.DataProvider);
		}

		public StoreJunction.QSP.com.Store GetStore(Int32 event_participation_id)
        {
			StoreJunction.QSP.com.Store store = null;

			bool useTransaction = false;
			string storedProcName = "es_get_link_to_store";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try
            {

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@event_participation_id", DbType.Int32, DBValue.ToDBInt32(event_participation_id)));
				if(useTransaction)
					si.BeginTransaction();

		
				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt.Rows.Count < 1) {
					throw new SqlDataException("No records on " + storedProcName);
				}

				// fill our objects
				try {
					DataRow row = dt.Rows[0];

					int event_id = DBValue.ToInt32(row["event_id"]);
					int event_type_id = DBValue.ToInt32(row["event_type_id"]);
					string event_name = DBValue.ToString(row["event_name"]);
					DateTime start_date = DBValue.ToDateTime(row["start_date"]);
					DateTime end_date = DBValue.ToDateTime(row["end_date"]);
					int account_number = DBValue.ToInt32(row["account_number"]);
                    int opportunity_id = int.MinValue;
                    if (row.Table.Columns.Contains("opportunity_id"))
                        opportunity_id = DBValue.ToInt32(row["opportunity_id"]);
					int store_id = DBValue.ToInt32(row["store_id"]);
					string name = DBValue.ToString(row["name"]);
					string email_address = DBValue.ToString(row["email_address"]);
					int member_hierarchy_id = DBValue.ToInt32(row["member_hierarchy_id"]);
					int aggregator_id = DBValue.ToInt32(row["aggregator_id"]);
					int store_template_id = DBValue.ToInt32(row["store_template_id"]);
					string culture_code = DBValue.ToString(row["culture_code"]);
					string parent_name = DBValue.ToString(row["parent_name"]);
					string parent_email = DBValue.ToString(row["parent_email_address"]);

					store = new StoreJunction.QSP.com.Store();
					store.EventID = event_id;
					store.EventTypeID = event_type_id;
					store.EventName = event_name;
					store.StartDate = start_date;
					store.EndDate = end_date;
					store.AccountNumber = account_number;
                    store.OpportunityID = opportunity_id;
					store.StoreID = store_id;
					store.Name = name;
					store.EmailAddress = email_address;
					store.MemberHierarchyID = member_hierarchy_id;
					store.AggregatorID = aggregator_id;
					store.StoreTemplateID = store_template_id;
					store.CultureCode = culture_code;
					store.ParentName = parent_name;
					store.ParentEmail = parent_email;

				} catch(System.Exception ex) {
					throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
				}

				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} catch {
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} finally {
				// Always close connection.
				si.Close();
			}
			return store;
		}


        // Code added by Andre Showairy on July 30, 2008
        // Problem:
        //      using the StoreJunction.QSP.com.Store for the canadian store
        // Resolution:
        //      Temporarly create this canadian verion of the store structure.
        // Hack:
        //      merge the canadian version and the us version of this class into one general
        //      a unified structutre of the store should be created and used.
        //      Only the BuildStoreUrl and BuildStoreUrlQSP could be seperated for US and canadian versions.

        /// <summary>
        /// Construct the structure of the canadian store to which the user should be redirected 
        /// when purchasing magazines
        /// </summary>
        /// <param name="event_participation_id"></param>
        /// <returns>The canadian storestructure</returns>
        public StoreJunction.QSP.ca.Store GetCanadianStore(Int32 event_participation_id, string Province)
        {
            StoreJunction.QSP.ca.Store store = null;

            bool useTransaction = false;
            string storedProcName = "es_get_link_to_can_store";
                                     
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {

                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@event_participation_id", DbType.Int32, DBValue.ToDBInt32(event_participation_id)));
                paramCol.Add(new SqlDataParameter("@subdivision_code", DbType.String, DBValue.ToString(Province)));
                if (useTransaction)
                    si.BeginTransaction();


                // Fetch and store into database.
                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt.Rows.Count < 1)
                {
                    throw new SqlDataException("No records on " + storedProcName);
                }

                // fill our objects
                try
                {
                    DataRow row = dt.Rows[0];

                    int event_id = DBValue.ToInt32(row["event_id"]);
                    int event_type_id = DBValue.ToInt32(row["event_type_id"]);
                    string event_name = DBValue.ToString(row["event_name"]);
                    DateTime start_date = DBValue.ToDateTime(row["start_date"]);
                    DateTime end_date = DBValue.ToDateTime(row["end_date"]);
                    int account_number = DBValue.ToInt32(row["account_number"]);
                    int opportunity_id = int.MinValue;
                    if (row.Table.Columns.Contains("opportunity_id"))
                        opportunity_id = DBValue.ToInt32(row["opportunity_id"]);
                    int store_id = DBValue.ToInt32(row["store_id"]);
                    string name = DBValue.ToString(row["name"]);
                    string email_address = DBValue.ToString(row["email_address"]);
                    int member_hierarchy_id = DBValue.ToInt32(row["member_hierarchy_id"]);
                    int aggregator_id = DBValue.ToInt32(row["aggregator_id"]);
                    int store_template_id = DBValue.ToInt32(row["store_template_id"]);
                    string culture_code = DBValue.ToString(row["culture_code"]);
                    string parent_name = DBValue.ToString(row["parent_name"]);
                    string parent_email = DBValue.ToString(row["parent_email_address"]);

                    store = new StoreJunction.QSP.ca.Store();
                    store.EventID = event_id;
                    store.EventTypeID = event_type_id;
                    store.EventName = event_name;
                    store.StartDate = start_date;
                    store.EndDate = end_date;
                    store.AccountNumber = account_number;
                    store.OpportunityID = opportunity_id;
                    store.StoreID = store_id;
                    store.SupporterName = name;
                    store.EmailAddress = email_address;
                    store.MemberHierarchyID = member_hierarchy_id;
                    store.AggregatorID = aggregator_id;
                    store.StoreTemplateID = store_template_id;
                    store.CultureCode = culture_code;
                    store.ParentName = parent_name;
                    store.ParentEmail = parent_email;

                }
                catch (System.Exception ex)
                {
                    throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                }

                // Commit our transaction.
                if (useTransaction)
                    si.Commit();
            }
            catch(Exception ex)
            {
                // Rollback on error.
                if (useTransaction)
                    si.Rollback();

                // throw exception
                throw ex;
            }
            finally
            {
                // Always close connection.
                si.Close();
            }
            return store;
        }

        // end of code added

    }
}
