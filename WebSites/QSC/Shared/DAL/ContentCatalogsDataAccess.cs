using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>Gets a list of available catalogs for a Campaign+Program</summary>
	public class ContentCatalogsDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public ContentCatalogsDataAccess(){}

		///<summary>Select an AccountListDataAccess object</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"GetContentCatalogsByProgram")]
		public SqlDataReader GetContentCatalogsByProgram(
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int CampaignID,
			[SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ProgramID)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null, new object[]{CampaignID,ProgramID});

				return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, 
					"QSPCanadaOrderManagement.dbo.GetContentCatalogsByProgram", aParams);
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
	}
}