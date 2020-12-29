//
// 2005-09-02 - Maxime Normand - New class.
// 2005-09-20 - Maxime Normand : add InsertNewsletter method
//

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using GA.BDC.Core.efundraisingCore;
using GA.BDC.Core.Data.Sql;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.BusinessBase;
using GA.BDC.Core.Database.Scratchcard;

namespace GA.BDC.Core.Database.Scratchcard.DataAccess 
{
	/// <summary>
	/// Summary description for Scratchcard.
	/// </summary>
	public class ScratchcardDatabase : GA.BDC.Core.Data.Sql.DatabaseObject 
	{

		public ScratchcardDatabase() 
		{
			if(Config.IsProduction) 
			{
				SetConnectionString(Config.ConnectionStringRelease);
				SetDataProvider(Config.DataProviderRelease);
			} 
			else 
			{
				SetConnectionString(Config.ConnectionStringDebug);
				SetDataProvider(Config.DataProviderDebug);
			}
		}

		public void InsertNewsletter(NewsletterSub n) 
		{
			bool useTransaction = false;
			string storedProcName = "sc_create_newsletter_subscription";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				// params for sc_create_newsletter_subscription StroredProc
				// @email varchar(100)
				// @fullname varchar(100)

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@fullname", DbType.String, DBValue.ToDBString(n.Name)));
				paramCol.Add(new SqlDataParameter("@email", DbType.String, DBValue.ToDBString(n.Email)));
				
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Fetch and store into database.
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
		
		public string getStory(int storyType, int groupTypeID) 
		{
			string story = "";
			string storedProcName;
			bool useTransaction = false;
			
			// if the storyType is 1 select a success story
			// otherwise select a "did you know" story
			if (storyType == 1)
			{
				storedProcName = "sc_get_successstory_by_group_type_id";
			}
			else
			{
				storedProcName = "sc_get_didyouknow_by_group_type_id";
			}
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@group_type_id", DbType.Int32, DBValue.ToDBInt32(groupTypeID)));
				
				
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
						story = DBValue.ToString(row["story_text"]);	
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
		
			return story;
		}
		

	}
}
