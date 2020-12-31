using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>Data layer for Contact Info</summary>
	public class ContactDataAccess : QDataAccess
	{
		public ContactDataAccess()
		{
		}

		///<summary>Insert a Contact object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_ins_Contact")]
		public bool Insert(	
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int ContactListID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int CAccountID,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string Title,
			[SqlParameter(SqlDbType.VarChar	 ,20,ParameterDirection.Input)]string FirstName,
			[SqlParameter(SqlDbType.VarChar	 ,30,ParameterDirection.Input)]string LastName,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string MiddleInitial,
			//[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int TypeId,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string Function,
			[SqlParameter(SqlDbType.VarChar	 ,60,ParameterDirection.Input)]string Email,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Output)]out int AddressID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Output)]out int PhoneListID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Output)]out int ContactID
							)
		{
			AddressID = -1;
			PhoneListID = -1;
			ContactID = -1;


			int rows = -1;
			try
			{
				SqlCommand sqlCmd = CreateSqlParametersCommand(null,
				new object[]{ContactListID,CAccountID,Title,FirstName,LastName,MiddleInitial,Function,Email,AddressID,PhoneListID,ContactID});

				rows = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
						"QSPCanadaCommon.dbo.pr_ins_Contact", sqlCmd);

				
				AddressID		= Convert.ToInt32(sqlCmd.Parameters["@AddressID"].Value);
				PhoneListID		= Convert.ToInt32(sqlCmd.Parameters["@PhoneListID"].Value);
				ContactID		= Convert.ToInt32(sqlCmd.Parameters["@ContactID"].Value);

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
			
			if(rows != 3)//inserted a PhoneListID row, AddressListID row, and a Contact row
			{
				string msg = rows.ToString() + " rows were inserted, we needed exactly 3 to go in";
				throw new System.ApplicationException(msg);
			}
			else
			{				
				return true;
			}
		}


		///<summary>Update a Contact object</summary>
		///<returns>bool: success or failure</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_upd_Contact")]
		public bool Update(
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int ContactID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int ContactListID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int CAccountID,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string Title,
			[SqlParameter(SqlDbType.VarChar	 ,20,ParameterDirection.Input)]string FirstName,
			[SqlParameter(SqlDbType.VarChar	 ,30,ParameterDirection.Input)]string LastName,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string MiddleInitial,
			//[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int TypeId,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string Function,
			[SqlParameter(SqlDbType.VarChar	 ,60,ParameterDirection.Input)]string Email
							)
		{

			int rows = -1;
			try
			{
			
				SqlParameter[] aParams = CreateSqlParameters(null,
				new object[]{ContactID,ContactListID,CAccountID,Title,FirstName,LastName,MiddleInitial,Function,Email});
				
				rows = 	SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
						"QSPCanadaCommon.dbo.pr_upd_Contact",aParams);
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
				
			if(rows != 1)
			{
				string msg = rows.ToString() + " rows were updated, when only 1 was expected";
				throw new System.ApplicationException(msg);
			}
			else
			{
				return true;
			}
		}
		

		///<summary>Select a Contact object</summary>
		///<param name="Id">int: what Contact</param>
		///<returns>DataTable full of Contact info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_Contact")]
		public DataTable GetContact([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ContactID)
		{
			DataTable dt=null;
			try
			{

				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ContactID});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_get_Contact",aParams);
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
			return dt;
		}

		///<summary>Get a collection of Contacts</summary>
		///<param name="Id">int: what Account</param>
		///<returns>DataTable full of Contact info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_Contacts_byAccount")]
		public DataTable GetContactsByAccountID([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountID)
		{
			DataTable dt=null;
			try
			{

				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{AccountID});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_get_Contacts_byAccount",aParams);
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
			return dt;
		}


		///<summary>Get a collection of Contacts</summary>
		///<param name="Id">int: what ContactList</param>
		///<returns>DataTable full of Contact info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_Contacts_byAccount")]
		public DataTable GetContactsByContactList([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ContactListID)
		{
			DataTable dt=null;
			try
			{

				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ContactListID});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_get_Contacts_byContactList",aParams);
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
			return dt;
		}



		///<summary>Delete a Contact object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_del_Contact")]
		public bool Delete([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ContactID)
		{
			int rows = -1;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{ContactID});

				rows = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
						"QSPCanadaCommon.dbo.pr_del_Contact", aParams);
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
			
			if(rows != 1)//a Contact row
			{
				string msg = rows.ToString() + " rows were deleted, we needed exactly 1 to be removed";
				throw new System.ApplicationException(msg);
			}
			else
			{				
				return true;
			}
		}

	}
}
