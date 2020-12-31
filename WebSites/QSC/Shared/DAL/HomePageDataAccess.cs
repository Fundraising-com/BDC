using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// Summary description for HomePageDataAccess.
	/// </summary>
	public class HomePageDataAccess : QDataAccess
	{
		public HomePageDataAccess()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		[SqlCommandMethod(CommandType.StoredProcedure, "GetHomePageNewsAndDateItems")]
		public DataTable GetHomePageItems([SqlParameter(SqlDbType.Int, ParameterDirection.Input)] int Mode)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null, new object[]{Mode});

				return SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.GetHomePageNewsAndDateItems", aParams);
			}
			catch(SqlException sqlE)
			{
				throw sqlE;
			}
		}

		[SqlCommandMethod(CommandType.StoredProcedure, "GetHomePageNewsAndDateItems")]
		public DataTable GetHomePageItems(
			[SqlParameter(SqlDbType.Int, ParameterDirection.Input)] int Mode,
			[SqlParameter(SqlDbType.Bit, ParameterDirection.Input)] bool ShowAll
			)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null, new object[]{Mode, ShowAll});

				return SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.GetHomePageNewsAndDateItems", aParams);
			}
			catch(SqlException sqlE)
			{
				throw sqlE;
			}
		}
	}
}
