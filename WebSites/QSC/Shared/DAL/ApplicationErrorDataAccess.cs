using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>Access to the error logging DB</summary>
	public class ApplicationErrorDataAccess : QDataAccess
	{
		///<summary>default constructor</summary>
		public ApplicationErrorDataAccess(){}

		///<summary>Insert an ASP.NET error</summary>
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaError.dbo.pr_Insert_Error_ASPNET")]
		public bool InsertASPNET(	
			[SqlParameter(SqlDbType.VarChar	 ,1000,ParameterDirection.Input)]string Stack,
			[SqlParameter(SqlDbType.VarChar	 ,100,ParameterDirection.Input)]string Project,
			[SqlParameter(SqlDbType.VarChar	 ,100,ParameterDirection.Input)]string Namespace,
			[SqlParameter(SqlDbType.VarChar	 ,100,ParameterDirection.Input)]string Class,
			[SqlParameter(SqlDbType.VarChar	 ,500,ParameterDirection.Input)]string Func,
			[SqlParameter(SqlDbType.VarChar	 ,255,ParameterDirection.Input)]string Ffile,
			[SqlParameter(SqlDbType.VarChar	 ,1000,ParameterDirection.Input)]string HelpLink,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string ServerName,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string ServerIP,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string Port,
			[SqlParameter(SqlDbType.VarChar	 ,1000,ParameterDirection.Input)]string PageURL,
			[SqlParameter(SqlDbType.VarChar	 ,500,ParameterDirection.Input)]string BrowserInfo,
			[SqlParameter(SqlDbType.Text     ,    ParameterDirection.Input)]string ASPErrorDescription,
			[SqlParameter(SqlDbType.VarChar	 ,255,ParameterDirection.Input)]string ASPErrorCategory,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string ASPErrorNumber,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string IPAddress,
			[SqlParameter(SqlDbType.VarChar	 ,1000,ParameterDirection.Input)]string FormValues,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string FormMethod,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string LineNumber,
			[SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string ColumnNumber,
			[SqlParameter(SqlDbType.VarChar	 ,1000,ParameterDirection.Input)]string ReferingURL,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int SiteId,
			[SqlParameter(SqlDbType.Text     ,    ParameterDirection.Input)]string SessionVariables,
			[SqlParameter(SqlDbType.Text     ,    ParameterDirection.Input)]string AdditionalComments,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Output)]out int ErrorId
							)
		{
			ErrorId = -1;
			try
			{
				SqlCommand sqlCmd = CreateSqlParametersCommand(null,
				new object[]{Stack,Project,Namespace,Class,Func,Ffile,HelpLink,ServerName,ServerIP,Port,PageURL,BrowserInfo,ASPErrorDescription,ASPErrorCategory,ASPErrorNumber,IPAddress,FormValues,FormMethod,LineNumber,ColumnNumber,ReferingURL,SiteId,SessionVariables,AdditionalComments,ErrorId});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "QSPCanadaError.dbo.pr_Insert_Error_ASPNET",sqlCmd);
				ErrorId = Convert.ToInt32(sqlCmd.Parameters["@ErrorId"].Value);
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
			return true;
		}
	}
}
