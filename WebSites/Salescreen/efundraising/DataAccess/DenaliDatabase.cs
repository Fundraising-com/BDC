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
	public class DenaliDatabase : efundraising.Data.Sql.DatabaseObject
	{
		public DenaliDatabase()
		{
			if(Config.IsDenaliProduction) 
			{
				SetConnectionString(Config.DenaliConnectionStringRelease);
				SetDataProvider(Config.DenaliDataProviderRelease);
			} 
			else 
			{
				SetConnectionString(Config.DenaliConnectionStringDebug);
				SetDataProvider(Config.DenaliDataProviderDebug);
			}
		}
		
		public void InsertDonationForm(string language_id, string first_name, string last_name, string company_name, string address, string city, string province, string postal_code, string phone_number, string email, float contribution, float donate_onreach, string credit_card, string credit_card_number, string credit_card_owner, string credit_card_expiration)
		{
			string storedProcName = "denali_insert_application_form";
			bool useTransaction = false;

			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{

				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@language_id", DbType.String, DBValue.ToDBString(language_id)));
				paramCol.Add(new SqlDataParameter("@first_name", DbType.String, DBValue.ToDBString(first_name)));
				paramCol.Add(new SqlDataParameter("@last_name", DbType.String, DBValue.ToDBString(last_name)));
				paramCol.Add(new SqlDataParameter("@company_name", DbType.String, DBValue.ToDBString(company_name)));
				paramCol.Add(new SqlDataParameter("@address", DbType.String, DBValue.ToDBString(address)));
				paramCol.Add(new SqlDataParameter("@city", DbType.String, DBValue.ToDBString(city)));
				paramCol.Add(new SqlDataParameter("@province", DbType.String, DBValue.ToDBString(province)));
				paramCol.Add(new SqlDataParameter("@postal_code", DbType.String, DBValue.ToDBString(postal_code)));
				paramCol.Add(new SqlDataParameter("@phone_number", DbType.String, DBValue.ToDBString(phone_number)));
				paramCol.Add(new SqlDataParameter("@email", DbType.String, DBValue.ToDBString(email)));
				paramCol.Add(new SqlDataParameter("@contribution", DbType.Double, DBValue.ToDBFloat(contribution)));
				paramCol.Add(new SqlDataParameter("@donate_onreach", DbType.Double, DBValue.ToDBFloat(donate_onreach)));
				paramCol.Add(new SqlDataParameter("@credit_card", DbType.String, DBValue.ToDBString(credit_card)));
				paramCol.Add(new SqlDataParameter("@credit_card_number", DbType.String, DBValue.ToDBString(credit_card_number)));
				paramCol.Add(new SqlDataParameter("@credit_card_owner", DbType.String, DBValue.ToDBString(credit_card_owner)));
				paramCol.Add(new SqlDataParameter("@credit_card_expiration", DbType.String, DBValue.ToDBString(credit_card_expiration)));
				
				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
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
		
	}
}
