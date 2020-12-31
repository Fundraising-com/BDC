using System;
using System.Reflection;
using System.EnterpriseServices;
using System.Data;
using System.Data.SqlClient;
using Debug = System.Diagnostics.Debug;

namespace DAL
{
	/// <summary>
	/// ProgramMasterDataAccess
	/// </summary>	 

	public class ProgramMasterDataAccess : QDataAccess
	{
      
        public ProgramMasterDataAccess()
		{
        
		}

        #region CRUD Commands
        /// <summary>
        /// Insert a Program Master object
        /// </summary>	 
        [DAL.SqlCommandMethod(CommandType.StoredProcedure,"AddProgramMaster")]
        public bool Insert( [SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)] string Program_Type ,
                            [SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int SubType,
							[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int Season,
                            [SqlParameter(SqlDbType.VarChar ,50,ParameterDirection.Input)] string Alpha,
                            [SqlParameter(SqlDbType.VarChar ,10,ParameterDirection.Input)]string Code,
                            [SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int Status,
                            [SqlParameter(SqlDbType.VarChar ,10,ParameterDirection.Input)]string Country,
                            [SqlParameter(SqlDbType.VarChar ,10,ParameterDirection.Input)]string Lang,
                            [SqlParameter(SqlDbType.VarChar ,1,ParameterDirection.Input)]string IsReplacement,
                            [SqlParameter(SqlDbType.VarChar ,1,ParameterDirection.Input)]string IsNational,
                            [SqlParameter(SqlDbType.VarChar ,4,ParameterDirection.Input)]string UserIDCreated,
							[SqlParameter(SqlDbType.Int,ParameterDirection.Output)]out int Program_ID
                            )
        {
            Program_ID=-1;
            try
            {                
                SqlCommand aParams = CreateSqlParametersCommand(null,
					new object[]{Program_Type,SubType,Season,Alpha,Code,Status,Country,Lang,IsReplacement,IsNational,UserIDCreated, Program_ID});

	            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "AddProgramMaster",aParams);

               
                Program_ID = Convert.ToInt32(aParams.Parameters["Program_ID"].Value);
            }
            catch(InvalidOperationException)
            {              			
               
            }
            catch(SqlException e)
            {
                Debug.Assert(false, e.Message);
            }
            catch(Exception)
            {
                
            }
            return true;
        }
		/// <summary>
		/// Update a Program Master object
		/// </summary>	 
		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"EditProgramMaster")]
		public bool Update( [SqlParameter(SqlDbType.Int,ParameterDirection.Input)] int Program_ID,
			[SqlParameter(SqlDbType.VarChar,50,ParameterDirection.Input)] string Program_Type ,
			[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int SubType,
			[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int Season,
			[SqlParameter(SqlDbType.VarChar ,50,ParameterDirection.Input)] string Alpha,
			[SqlParameter(SqlDbType.VarChar ,10,ParameterDirection.Input)]string Code,
			[SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int Status,
			[SqlParameter(SqlDbType.VarChar ,10,ParameterDirection.Input)]string Country,
			[SqlParameter(SqlDbType.VarChar ,10,ParameterDirection.Input)]string Lang,
			[SqlParameter(SqlDbType.VarChar ,1,ParameterDirection.Input)]string IsReplacement,
			[SqlParameter(SqlDbType.VarChar ,1,ParameterDirection.Input)]string IsNational,
			[SqlParameter(SqlDbType.VarChar ,4,ParameterDirection.Input)]string UserIDChanged)
		{
			try
			{                
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{Program_ID,Program_Type,SubType,Alpha,Code,Status,Country,Lang,IsReplacement,IsNational,UserIDChanged});

				SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "EditProgramMaster",aParams);

               
			}
			catch(InvalidOperationException)
			{              			
               
			}
			catch(SqlException e)
			{
				Debug.Assert(false, e.Message);
			}
			catch(Exception)
			{
                
			}
			return true;
		}


        #endregion

		[DAL.SqlCommandMethod(CommandType.StoredProcedure,"GetProgramMaster")]
		public DataSet Exists([SqlParameter(SqlDbType.Int,ParameterDirection.Input)]int Program_ID )
        {
			DataSet ds=null;
			try
			{
				SqlParameter[] aParams = CreateSqlParameters(null,
					new object[]{Program_ID});

				
				ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, 
					"GetProgramMaster" , 
					aParams);

			}
			catch(InvalidOperationException io)
			{              			
				string x = io.Message;
			}
			catch(SqlException)
			{
			}
			catch(Exception)
			{
			}
           return ds;
        }
		public bool Delete(int ID)
		{
			bool bOk = true;
			return bOk;
		}
	}
}
