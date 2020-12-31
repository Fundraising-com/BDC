using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	///<summary>Gets a list of available programs</summary>
	public class ProfileDataAccess : QDataAccess
	{
		public ProfileDataAccess()
		{
		}


		[DAL.SqlCommandMethodAttribute(CommandType.StoredProcedure, "SelectFromUserProfileInstance")]
		public DataTable SelectProfile([SqlParameter(SqlDbType.Int, ParameterDirection.Input)] int instance)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null, new object[]{instance});

				return SqlHelper.ExecuteDataTable(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.SelectFromUserProfileInstance",aParams);
			}
			catch(SqlException sqlE)
			{
				//cleanup

				throw sqlE;
			}
		}

		[DAL.SqlCommandMethodAttribute(CommandType.StoredProcedure, "Update_CUserProfile")]
		public bool SaveProfile(
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_fmid,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_username,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Password,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_LastName,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_MakeChecks,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Email,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_SigOther,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_VoiceMail,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_HomePhone,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_WorkPhone,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Fax,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_TollFreePhone,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_MobilePhone,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Pager,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Maddress1,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Maddress2,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Mcity,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Mstate,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Mzip,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Saddress1,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Saddress2,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Scity,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Sstate,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Szip,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Iaddress1,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Iaddress2,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Icity,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Istate,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_Izip,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_IPhone,
			[SqlParameter(SqlDbType.Int,		ParameterDirection.Input)] int p_InvoiceTerm,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_DefaultTerm1,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_DefaultTerm2,
			[SqlParameter(SqlDbType.VarChar,	ParameterDirection.Input)] string p_DefaultTerm3,
			[SqlParameter(SqlDbType.SmallInt,	ParameterDirection.Input)] short p_TimeZone,
			[SqlParameter(SqlDbType.Bit,		ParameterDirection.Input)] bool p_DST,
			[SqlParameter(SqlDbType.Int,		ParameterDirection.Input)] int p_UserId,
			[SqlParameter(SqlDbType.Int,		ParameterDirection.Input)] int p_ModifiedBy
			)
		{
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null, 
					new object[]{
									p_fmid,p_username,p_Password,p_LastName,p_MakeChecks,p_Email,
									p_SigOther,p_VoiceMail,p_HomePhone,p_WorkPhone,p_Fax,p_TollFreePhone,
									p_MobilePhone,p_Pager,p_Maddress1,p_Maddress2,p_Mcity,p_Mstate,p_Mzip,
									p_Saddress1,p_Saddress2,p_Scity,p_Sstate,p_Szip,p_Iaddress1,p_Iaddress2,
									p_Icity,p_Istate,p_Izip,p_IPhone,p_InvoiceTerm,p_DefaultTerm1,p_DefaultTerm2,
									p_DefaultTerm3,p_TimeZone,p_DST,p_UserId,p_ModifiedBy
								});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, 
					"QSPCanadaCommon.dbo.Update_CUserProfile",aParams);
				return true;
			}
			catch(SqlException sqlE)
			{
				//cleanup

				throw sqlE;
			}
		}
	}
}
	
