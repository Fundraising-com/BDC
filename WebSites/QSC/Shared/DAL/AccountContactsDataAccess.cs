using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// Summary description for AccountContactsDataAccess.
	/// </summary>
	public class AccountContactsDataAccess : QDataAccess
	{
		public AccountContactsDataAccess(){}

		///<summary>Grabs a list of contact types.</summary>
		///<returns>a DataTable of results</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.GetCAccountContactInfo")]
		public DataTable GetContactTypes()
		{
			try
			{
				return SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.GetCAccountContactInfo");
			}
			catch (SqlException SqlE)
			{
				//cleanup stuff
				
				//re-throw 
				throw SqlE;
			}
		}

		///<summary>Gets a list of contacts for an account</summary>
		///<param name="AccountID">int: Which Account to look for.</param>
		///<returns>a DataTable of results</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.GetCAccountContactInfo")]
		public DataTable GetAccountContacts([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountId)
		{
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{AccountId});
			try
			{
				return SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.GetCAccountContactInfo",aParams);
			}
			catch (SqlException SqlE)
			{
				//cleanup stuff
				
				//re-throw 
				throw SqlE;
			}
		}

		///<summary>Grabs a list of campaigns for a given account</summary>
		///<param name="ContactId">int: Which contact to get info on.</param>
		///<returns>a DataTable of the contacts info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.GetCAccountContactInfo")]
		public object[] GetContactInfo(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ContactId
			)
		{
			
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ContactId});
			aParams[0].ParameterName = "@ContactId";
			try
			{
				SqlDataReader reader = SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.GetCAccountContactInfo",aParams);
				reader.Read();
				object []result = new object[reader.FieldCount];
				reader.GetValues(result);
				return result;
			}
			catch (SqlException SqlE)
			{
				//cleanup stuff
				
				//re-throw 
				throw SqlE;
			}
		}

		///<summary>Saves basic contact information to the database</summary>
		///<param name="Mode">int: Should be 0 indicating basic information</param>
		///<param name="ContactId">int: Which contact to save.</param>
		///<param name="AccountID">int: Which account it belongs to</param>
		///<param name="Title">string: Title (Mr, Mrs, Ms)</param>
		///<param name="TypeId">int: Type of contact</param>
		///<returns>Boolean indicating success.</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.UpdateCAccountContactInfo")]
		public bool SaveContact(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int Mode,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ContactId,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountId,
			[SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)]string Title,
			[SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)]string FirstName,
			[SqlParameter(SqlDbType.VarChar,30,ParameterDirection.Input)]string LastName,
			[SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)]string MiddleInitial,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int TypeId,
			[SqlParameter(SqlDbType.VarChar,60,ParameterDirection.Input)]string Email
			)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{
																				Mode,
																				ContactId,
																				AccountId,
																				Title,
																				FirstName,
																				LastName,
																				MiddleInitial,
																				TypeId,
																				Email
																			});
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.UpdateCAccountContactInfo",aParams);
				return true;
			}
			catch(InvalidOperationException eIO)
			{
				//cleanup stuff

				//re-throw
				throw eIO;
			}
			catch (SqlException eSQL)
			{
				//cleanup stuff

				//re-throw
				throw eSQL;
			}
			catch(Exception eGeneric)
			{
				//cleanup stuff

				//re-throw
				throw eGeneric;
			}
		}

		///<summary>Saves detailed contact information to the database</summary>
		///<param name="Mode">int: Should be 1 indicating detailed information</param>
		///<param name="ContactId">int: Which contact to save.</param>
		///<param name="AccountId">int: Which account it belongs to.</param>
		///<returns>Boolean indicating success.</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.UpdateCAccountContactInfo")]
		public bool SaveContact(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int Mode,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ContactId,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountId,
			[SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string Address1,
			[SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)]string Address2,
			[SqlParameter(SqlDbType.VarChar,30,ParameterDirection.Input)]string City,
			[SqlParameter(SqlDbType.VarChar,2,ParameterDirection.Input)]string State,
			[SqlParameter(SqlDbType.VarChar,10,ParameterDirection.Input)]string Zip,
			[SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)]string HomePhone,
			[SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)]string WorkPhone,
			[SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)]string FaxPhone,
			[SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)]string MobilePhone
			)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{
																				Mode,
																				ContactId,
																				AccountId,
																				Address1,
																				Address2,
																				City,
																				State,
																				Zip,
																				HomePhone,
																				WorkPhone,
																				FaxPhone,
																				MobilePhone
																			});
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.UpdateCAccountContactInfo",aParams);
				return true;
			}
			catch(InvalidOperationException eIO)
			{
				//cleanup stuff

				//re-throw
				throw eIO;
			}
			catch (SqlException eSQL)
			{
				//cleanup stuff

				//re-throw
				throw eSQL;
			}
			catch(Exception eGeneric)
			{
				//cleanup stuff

				//re-throw
				throw eGeneric;
			}
		}

		///<summary>Deletes a contact from the database</summary>
		///<param name="ContactId">int: Which contact to delete.</param>
		///<param name="AccountId">int: Which account it belongs to.</param>
		///<param name="AccountId">bool: Delete the record (usually true).</param>
		///<returns>Boolean indicating success.</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.UpdateCAccountContactInfo")]
		public bool DeleteContact(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ContactId,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountId,
			[SqlParameter(SqlDbType.Bit,1,ParameterDirection.Input)]int Delete_TF
			)
		{
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{
																			  ContactId,
																			  AccountId,
																			  Delete_TF
																		  });
			try
			{
				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.UpdateCAccountContactInfo",aParams);
				return true;
			}
			catch (SqlException SqlE)
			{
				//cleanup stuff
				
				//re-throw 
				throw SqlE;
			}
		}
	}
}
