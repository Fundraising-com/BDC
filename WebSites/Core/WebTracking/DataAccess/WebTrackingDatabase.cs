/* Title:	WebTracking Database
 * Author:	Jean-Francois Buist
 * Summary:	Data access layer object to retreive/update/insert values in the database.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using System.Data;
using GA.BDC.Core.BusinessBase;
using GA.BDC.Core.Data.Sql;
using GA.BDC.Core.Configuration;

namespace GA.BDC.Core.WebTracking.DataAccess
{
	/// <summary>
	/// Summary description for WebTrackingDatabase.
	/// </summary>
	public class WebTrackingDatabase : GA.BDC.Core.Data.Sql.DatabaseObject {
		public WebTrackingDatabase() {
            SetConnectionString(Config.ConnectionString);
            SetDataProvider(Config.DataProvider);
		}

		public void UpdateWebSiteFromPartnerID(Int32 partner_id, Int32 webproject_id, Int32 visitor_log_id) {
			bool useTransaction = false;
			string storedProcName = "wt_update_website_from_partner_id";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@partner_id", DbType.Int32, DBValue.ToDBInt32(partner_id)));
				paramCol.Add(new SqlDataParameter("@webproject_id", DbType.Int32, DBValue.ToDBInt32(webproject_id)));
				paramCol.Add(new SqlDataParameter("@visitor_log_id", DbType.Int32, DBValue.ToDBInt32(visitor_log_id)));
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);

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
		}

		public void InsertWebTrackingObject(Int32 trackable_obj_type_id, String trackable_obj_desc, String tracking_code, ref Int32 trackableObjectID) {
			bool useTransaction = false;
			string storedProcName = "wt_insert_trackable_obj";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@trackable_obj_type_id", DbType.Int32, DBValue.ToDBInt32(trackable_obj_type_id)));
				paramCol.Add(new SqlDataParameter("@trackable_obj_desc", DbType.String, DBValue.ToDBString(trackable_obj_desc)));
				paramCol.Add(new SqlDataParameter("@tracking_code", DbType.String, DBValue.ToDBString(tracking_code)));
				paramCol.Add(new SqlDataParameter("@trackingID", DbType.Int32, ParameterDirection.Output));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();

				// Fetch and store into database.
				si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);

				trackableObjectID = int.Parse(paramCol["@trackingID"].Value.ToString());

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
		}

		public int GetClickCount(String tracking_code) {
			int clickCount = int.MinValue;

			bool useTransaction = false;
			string storedProcName = "wt_get_click_count";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@tracking_code", DbType.String, DBValue.ToDBString(tracking_code)));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();

				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt.Rows.Count < 1) {
					return int.MinValue;
					//throw new SqlDataException("No records on " + storedProcName);
				}

				// fill our objects
				try {
					int click_count = DBValue.ToInt32(dt.Rows[0]["click_count"]);
					clickCount = click_count;
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
			return clickCount;
		}

		public TrackableObject GetTrackableObject(String tracking_code) {
			TrackableObject trackableObject = null;

			bool useTransaction = false;
			string storedProcName = "wt_get_trackable_obj_id";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@tracking_code", DbType.String, DBValue.ToDBString(tracking_code)));

				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, paramCol);

				if(dt.Rows.Count < 1) {
					return null;
					//throw new SqlDataException("No records on " + storedProcName);
				}

				// fill our objects
				try {
					foreach(DataRow row in dt.Rows) {
						int trackable_obj_id = DBValue.ToInt32(row["trackable_obj_id"]);
						int trackable_obj_type_id = DBValue.ToInt32(row["trackable_obj_type_id"]);

						trackableObject = new TrackableObject();
						trackableObject.TrackingCode = tracking_code;
						trackableObject.TrackableObjectID = trackable_obj_id;
						trackableObject.TrackableObjectTypeID = trackable_obj_type_id;
					}
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
			return trackableObject;
		}

		public string GetVisitorGUID() {
			string guid = null;

			bool useTransaction = false;
			string storedProcName = "wt_get_visitor_guid";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {

				si.Open();

				if(useTransaction)
					si.BeginTransaction();

		
				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable( storedProcName, CommandType.StoredProcedure, null);

				if(dt.Rows.Count < 1) {
					throw new SqlDataException("No records on " + storedProcName);
				}

				// fill our objects
				try {
					foreach(DataRow row in dt.Rows) {
						guid = DBValue.ToString(row["visitor_guid"]);
					}
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
			return guid;
		}

		public void InsertVisitorTrack(Int32 visitor_log_id, Int32 trackable_obj_id, Int32 increment, Int32 trackno, String track_dynamic) {
			bool useTransaction = false;
			string storedProcName = "wt_insert_visitor_track";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@visitor_log_id", DbType.Int32, DBValue.ToDBInt32(visitor_log_id)));
				paramCol.Add(new SqlDataParameter("@trackable_obj_id", DbType.Int32, DBValue.ToDBInt32(trackable_obj_id)));
				paramCol.Add(new SqlDataParameter("@increment", DbType.Int32, DBValue.ToDBInt32(increment)));
				paramCol.Add(new SqlDataParameter("@trackno", DbType.Int32, DBValue.ToDBInt32(trackno)));
				paramCol.Add(new SqlDataParameter("@track_dynamic", DbType.String, DBValue.ToDBString(track_dynamic)));

				si.Open();

				if(useTransaction)
					si.BeginTransaction();

		
				// Fetch and store into database.
				si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);

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
		}

		public void UpdateVisitorLog(Int32 visitorLogId, Int32 leadId) {
			bool useTransaction = false;
			string storedProcName = "wt_update_visitor_log_lead_id";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);
			int returnValue = -1;
			try {
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@visitor_log_id", DbType.Int32, DBValue.ToDBInt32(visitorLogId)));
				paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, DBValue.ToDBInt32(leadId)));
				si.Open();
				if(useTransaction)
					si.BeginTransaction();
				// Fetch and store into database.
				si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);
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
				si.Close();
			}
		}

		public int InsertVisitorLog(Int32 website_id, Int32 host_id, Int32 lead_id, String version, Int32 promotion_id, Int32 touch_id, string visitor_guid, int tellafriendid, string extSiteID) {
			bool useTransaction = false;
			string storedProcName = "wt_insert_visitor_log";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			int returnValue = -1;

			try {

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@website_id", DbType.Int32, DBValue.ToDBInt32(website_id)));
//				paramCol.Add(new SqlDataParameter("@host_id", DbType.Int32, DBValue.ToDBInt32(host_id)));
				paramCol.Add(new SqlDataParameter("@lead_id", DbType.Int32, DBValue.ToDBInt32(lead_id)));
				paramCol.Add(new SqlDataParameter("@version", DbType.String, DBValue.ToDBString(version)));
				paramCol.Add(new SqlDataParameter("@promotion_id", DbType.Int32, DBValue.ToDBInt32(promotion_id)));
				paramCol.Add(new SqlDataParameter("@touch_id", DbType.Int32, DBValue.ToDBInt32(touch_id)));
				paramCol.Add(new SqlDataParameter("@visitor_guid", DbType.String, DBValue.ToDBString(visitor_guid)));
				paramCol.Add(new SqlDataParameter("@tellafriend_id", DbType.Int32, DBValue.ToDBInt32(tellafriendid)));
				paramCol.Add(new SqlDataParameter("@ext_site_id", DbType.String, DBValue.ToDBString(extSiteID)));
				paramCol.Add(new SqlDataParameter("@return", DbType.Int32, ParameterDirection.ReturnValue));

				si.Open();

				if(useTransaction)
					si.BeginTransaction();

		
				// Fetch and store into database.
				si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);

				returnValue = int.Parse(paramCol["@return"].Value.ToString());

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
			return returnValue;
		}

		public void InsertVisitorInfo(Int32 visitor_log_id, Int32 avail_width, Int32 avail_height, string ip_address, String browser_name, String browser_version, String browser_language, String platform, String dns, String referrer, String country_code, String subdivision_code) {
			bool useTransaction = false;
			string storedProcName = "wt_insert_visitor_info";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@visitor_log_id", DbType.Int32, DBValue.ToDBInt32(visitor_log_id)));
				paramCol.Add(new SqlDataParameter("@avail_width", DbType.Int32, DBValue.ToDBInt32(avail_width)));
				paramCol.Add(new SqlDataParameter("@avail_height", DbType.Int32, DBValue.ToDBInt32(avail_height)));
				paramCol.Add(new SqlDataParameter("@ip_address", DbType.String, DBValue.ToDBString(ip_address)));
				paramCol.Add(new SqlDataParameter("@browser_name", DbType.String, DBValue.ToDBString(browser_name)));
				paramCol.Add(new SqlDataParameter("@browser_version", DbType.String, DBValue.ToDBString(browser_version)));
				paramCol.Add(new SqlDataParameter("@browser_language", DbType.String, DBValue.ToDBString(browser_language)));
				paramCol.Add(new SqlDataParameter("@platform", DbType.String, DBValue.ToDBString(platform)));
				paramCol.Add(new SqlDataParameter("@dns", DbType.String, DBValue.ToDBString(dns)));
				paramCol.Add(new SqlDataParameter("@referrer", DbType.String, DBValue.ToDBString(referrer)));
				paramCol.Add(new SqlDataParameter("@country_code", DbType.String, DBValue.ToDBString(country_code)));
				paramCol.Add(new SqlDataParameter("@subdivision_code", DbType.String, DBValue.ToDBString(subdivision_code)));
				paramCol.Add(new SqlDataParameter("@return", DbType.String, ParameterDirection.ReturnValue));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Fetch and store into database.
				si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);

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
		}

		public DataSet GetVisitorSteps(int visitorLogID, int memberHierarchyID) {
			bool useTransaction = false;
			string storedProcName = "wt_get_visitor_steps";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			DataSet data = null;

			try {

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@visitor_log_id", DbType.Int32, DBValue.ToDBInt32(visitorLogID)));
				paramCol.Add(new SqlDataParameter("@hierarchy_id", DbType.Int32, DBValue.ToDBInt32(memberHierarchyID)));
				paramCol.Add(new SqlDataParameter("@return", DbType.Int32, ParameterDirection.ReturnValue));

				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Fetch and store into database.
				data = si.ExecuteFetchDataSet( storedProcName, CommandType.StoredProcedure, paramCol);

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
			return data;
		}

		#region Visitor Identify
		public void InsertVisitorIdentity(Int32 visitor_log_id, Int32 member_hierarchy_id) {
			bool useTransaction = false;
			string storedProcName = "wt_insert_visitor_identity";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try {

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@visitor_log_id", DbType.Int32, DBValue.ToDBInt32(visitor_log_id)));
				paramCol.Add(new SqlDataParameter("@member_hierarchy_id", DbType.Int32, DBValue.ToDBInt32(member_hierarchy_id)));
				paramCol.Add(new SqlDataParameter("@return", DbType.Int32, ParameterDirection.ReturnValue));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Fetch and store into database.
				si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);

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
		}
		#endregion

	}
}

