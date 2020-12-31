using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// Summary description for PersonnelDirectoryDataAccess
	/// </summary>
	public class PersonnelDirectoryDataAccess : QDataAccess
	{
		///<summary>DefaultConstructor</summary>
		public PersonnelDirectoryDataAccess(){}

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.pr_DirectoryPersonnel")]
		public DataTable GetPersonnelDirectoryData(
//			string mode,
			[SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)] string first,
			[SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)] string last)
//			string mode,
//			string first,
//			string last)
		{
//			string proc;
//			if(mode == "Personnel") { 
//				proc = "QSPCommon.dbo.pr_DirectoryPersonnel"; 
//			} else if(mode == "Meridian") { 
//				proc = "QSPCommon.dbo.pr_DirectoryMeridian"; }
//			else { 
//				string errorStr = "Unrecognized PersonnelDirectoryDataAccess mode";
//				throw new ArgumentException(errorStr, mode); 
//			}


			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{first, last});
			DataTable dt=null;

			try
			{
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
										"QSPCanadaCommon.dbo.pr_DirectoryPersonnel",aParams);
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
	

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon.dbo.pr_DirectoryMeridian")]
		public DataTable GetMeridianDirectoryData(
			[SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)] string first,
			[SqlParameter(SqlDbType.VarChar,20,ParameterDirection.Input)] string last)
		{
			SqlParameter[] aParams = CreateSqlParameters(null,new object[]{first, last});
			DataTable dt=null;

			try
			{
				dt = SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.pr_DirectoryMeridian",aParams);
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
	}
}
