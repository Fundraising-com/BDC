//
// 2005-08-01 - Stephen Lim - New class.
//


using System;
using System.Data;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.BusinessBase;
using GA.BDC.Core.Data.Sql;

namespace GA.BDC.Core.LinkShare.DataAccess
{
	/// <summary>
	/// LinkShareDataInterface.
	/// </summary>
	public class LinkShareDatabase : DatabaseObject
	{
		public LinkShareDatabase()
		{
			Config cfg = new Config();			

			if(cfg.IsProduction) 
			{
				SetConnectionString(cfg.ConnectionStringRelease);
				SetDataProvider(cfg.DataProviderRelease);
			} 
			else 
			{
				SetConnectionString(cfg.ConnectionStringDebug);
				SetDataProvider(cfg.DataProviderDebug);
			}
		}


		public LsTransEntryCollection GetReport()
		{

			LsTransEntryCollection lstransCol = new LsTransEntryCollection();

			bool useTransaction = false;
			string storedProcName = "get_linkshare_data";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@return", DbType.Int32, ParameterDirection.ReturnValue));

				if(useTransaction)
					si.BeginTransaction();

				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

				// Fill our objects
				try 
				{
					int ret = (int) paramCol["@return"].Value;
					if (ret == 0)
					{
						foreach (DataRow row in dt.Rows) 
						{
							LsTransEntry entry = new LsTransEntry();
							entry.VisitorLogId = DBValue.ToInt32(row["visitor_log_id"]);
							entry.VisitorId = DBValue.ToString(row["visitor_guid"]);
							entry.PromotionId = DBValue.ToInt32(row["promotion_id"]);
							entry.SiteId = DBValue.ToString(row["ext_site_id"]);

							entry.OrderId = DBValue.ToInt32(row["order_id"]);
							if (entry.OrderId == DBValue.NullInt32)
								entry.OrderId = 0;

							entry.Sku = DBValue.ToString(row["sku_number"]);
							if (entry.Sku == DBValue.NullString)
								entry.Sku = "0";

							entry.TimeEntered = DBValue.ToDateTime(row["time_entered"]);
							entry.TimeCompleted = DBValue.ToDateTime(row["time_completed"]);

							entry.TotalOrder = DBValue.ToDouble(row["order_total"]);
							if (entry.TotalOrder == DBValue.NullDecimal)
								entry.TotalOrder = 0;

							entry.NetAmount = DBValue.ToDouble(row["net_amount"]);
							if (entry.NetAmount == DBValue.NullDecimal)
								entry.NetAmount = 0;

							entry.TotalQuantity = DBValue.ToInt32(row["total_quantity"]);
							if (entry.TotalQuantity == DBValue.NullInt32)
								entry.TotalQuantity = 0;

							entry.DateStamp = DBValue.ToDateTime(row["datestamp"]);

							lstransCol.Add(entry);
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

			return lstransCol;
		}
	}
}
