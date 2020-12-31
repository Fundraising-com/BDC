using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// DAL file for class AccountDataAccess to access CAccount info.
	/// </summary>
	public class AccountDataAccess : QDataAccess
	{
		public AccountDataAccess()
		{

		}

		///<summary>Insert an Account object</summary>
		/// <param name="Account_ID"></param>
		/// <param name="Name"></param>
		/// <param name="Country"></param>
		/// <param name="Lang"></param>
		/// <param name="CAccountCodeClass"></param>
		/// <param name="CAccountCodeGroup"></param>
		/// <param name="PhoneListID"></param>
		/// <param name="AddressListID"></param>
		/// <param name="StatusID"></param>
		/// <param name="Enrollment"></param>
		/// <param name="Comment"></param>
		/// <param name="EMail"></param>
		/// <param name="IsPrivateOrg"></param>
		/// <param name="IsAdultGroup"></param>
		/// <param name="Sponsor"></param>
		/// <param name="ParentID"></param>
		/// <param name="UserIDModified"></param>
		/// <returns>bool: success or failure</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_ins_CAccount")]
		public bool Insert(
			[SqlParameter(SqlDbType.Int		 ,ParameterDirection.Output)]out int Account_ID,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string Name,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string Country,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string Lang,
			[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string CAccountCodeClass,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string CAccountCodeGroup,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Output)]out int PhoneListID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Output)]out int AddressListID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int StatusID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int Enrollment,
			[SqlParameter(SqlDbType.VarChar	 ,1000,ParameterDirection.Input)]string Comment,
			[SqlParameter(SqlDbType.VarChar	 ,75,ParameterDirection.Input)]string EMail,
			[SqlParameter(SqlDbType.Bit		 ,ParameterDirection.Input)]bool IsPrivateOrg,
			[SqlParameter(SqlDbType.Bit		 ,ParameterDirection.Input)]bool IsAdultGroup,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string Sponsor,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int ParentID,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int UserIDModified,
			[SqlParameter(SqlDbType.VarChar	 ,30,ParameterDirection.Input)]string VendorNumber,
			[SqlParameter(SqlDbType.VarChar	 ,15,ParameterDirection.Input)]string VendorSiteName,
			[SqlParameter(SqlDbType.VarChar	 ,25,ParameterDirection.Input)]string VendorPayGroup

			)
		{
			PhoneListID		= -2;
			AddressListID	= -2;
			Account_ID		= -2;
			
			SqlCommand sqlCmd = CreateSqlParametersCommand(null,
			new object[]{Account_ID, Name,Country,Lang,CAccountCodeClass,CAccountCodeGroup,PhoneListID,AddressListID,StatusID,Enrollment,Comment,EMail,IsPrivateOrg,IsAdultGroup,Sponsor,ParentID,UserIDModified,VendorNumber,VendorSiteName,VendorPayGroup});

			int rows = -1;
			try
			{
				rows = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_ins_CAccount", sqlCmd);

				PhoneListID		= Convert.ToInt32(sqlCmd.Parameters["@PhoneListID"].Value);
				AddressListID	= Convert.ToInt32(sqlCmd.Parameters["@AddressListID"].Value);
				Account_ID		= Convert.ToInt32(sqlCmd.Parameters["@Account_ID"].Value);
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
			
 			if(rows != 3)//inserted a PhoneListID row, AddressListID row, and Account row
			{
				string msg = rows.ToString() + " rows were inserted, we needed exactly 3 to go in";
				throw new System.ApplicationException(msg);
			}
			else
			{				
				return true;
			}
		}

		///<summary>Update an Account object</summary>
		///<param name="Id"></param>
		///<param name="Name"></param>
		///<param name="Lang"></param>
		///<param name="CAccountCodeClass"></param>
		///<param name="CAccountCodeGroup"></param>
		///<param name="StatusID"></param>
		///<param name="Enrollment"></param>
		///<param name="Comment"></param>
		///<param name="EMail"></param>
		///<param name="IsPrivateOrg"></param>
		///<param name="IsAdultGroup"></param>
		///<param name="Sponsor"></param>
		///<param name="ParentID"></param>
		///<param name="UserIDUpdated"></param>
		///<returns>bool: success or failure</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_upd_CAccount")]
		public bool Update(	[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int Id,
							[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string Name,
							[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string Lang,
							[SqlParameter(SqlDbType.VarChar	 ,10,ParameterDirection.Input)]string CAccountCodeClass,
							[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string CAccountCodeGroup,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int StatusID,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int Enrollment,
							[SqlParameter(SqlDbType.VarChar	 ,1000,ParameterDirection.Input)]string Comment,
							[SqlParameter(SqlDbType.VarChar	 ,75,ParameterDirection.Input)]string EMail,
							[SqlParameter(SqlDbType.Bit		 ,ParameterDirection.Input)]bool IsPrivateOrg,
							[SqlParameter(SqlDbType.Bit		 ,ParameterDirection.Input)]bool IsAdultGroup,
							[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string Sponsor,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int ParentID,
							[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int UserIdModified
							)
		{

				int rows;
			
				try
				{
					SqlParameter[] aParams = CreateSqlParameters(null,
						new object[]{Id,Name,Lang,CAccountCodeClass,CAccountCodeGroup,StatusID,Enrollment,Comment,EMail,IsPrivateOrg,IsAdultGroup,Sponsor,ParentID, UserIdModified});

					rows = 	SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
								"QSPCanadaCommon.dbo.pr_upd_CAccount",aParams);
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
		
		///<summary>Select an Account object</summary>
		///<param name="Id">int: what account</param>
		///<returns>DataTable full of account info</returns>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_get_CAccount")]
		public DataTable GetCAccountByID([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int AccountID)
		{
			DataTable dt=null;
			try
			{

				SqlParameter[] aParams = CreateSqlParameters(null,new object[]{AccountID});
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, "QSPCanadaCommon.dbo.pr_get_CAccount",aParams);
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


		///<summary>Update the Vendor Information for a CAccount object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"pr_upd_CAccount_VendorInfo")]
		public bool UpdateVendorInfo(	
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int CAccountID,
			[SqlParameter(SqlDbType.VarChar	 ,30,ParameterDirection.Input)]string VendorNumber,
			[SqlParameter(SqlDbType.VarChar	 ,15,ParameterDirection.Input)]string VendorSiteName,
			[SqlParameter(SqlDbType.VarChar	 ,25,ParameterDirection.Input)]string VendorPayGroup,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int UserIDModified
			)
		{
			int rows;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
				new object[]{CAccountID,VendorNumber,VendorSiteName,VendorPayGroup,UserIDModified});

				rows = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_upd_CAccount_VendorInfo",aParams);
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

			if(rows == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}
 
