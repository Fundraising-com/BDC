using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// Summary description for CodeDetailDataAccess.
	/// </summary>
	public class CodeDetailDataAccess : QDataAccess
	{
		public CodeDetailDataAccess()
		{
			
		}

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon..GetListFromHeader")]
		public DataSet GetListFromHeader([SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int CodeHeaderInstance )
		{
			DataSet ds=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{CodeHeaderInstance});

				
				ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon..GetListFromHeader" , 
					aParams);

			}
			catch(InvalidOperationException io)
			{              			
				string x = io.Message;
			}
			catch( SqlException)
			{
			}
			catch( Exception)
			{
			}
			return ds;
		}

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon..pr_CodeDetail_SelectAllByCodeHeaderInstance")]
		public DataSet GetCodeDesc([SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int iCodeHeaderInstance )
		{
			DataSet ds=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{iCodeHeaderInstance});

				
				ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon..pr_CodeDetail_SelectAllByCodeHeaderInstance" , 
					aParams);

			}
			catch(InvalidOperationException io)
			{              			
				string x = io.Message;
			}
			catch( SqlException)
			{
			}
			catch( Exception)
			{
			}
			return ds;
		}

			
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"QSPCanadaCommon..pr_CodeDetail_SelectOne")]
		public DataSet GetCodeDescSelectone([SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int iInstance )
		{
			DataSet ds=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{iInstance});

				
				ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon..pr_CodeDetail_SelectOne" , 
					aParams);

			}
			catch(InvalidOperationException io)
			{              			
				string x = io.Message;
			}
			catch( SqlException)
			{
			}
			catch( Exception)
			{
			}
			return ds;
		}








	}
}
