using System;
using System.Reflection;
using System.EnterpriseServices;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
	/// <summary>
	/// CatalogSectionDataTable
	/// </summary>	 

	public class CatalogSectionDataTable : QDAServicedComponent
	{
        //DataTable dataTable; //isnt used

        public CatalogSectionDataTable()
		{
            //dataTable = null;
		}

        #region CRUD Commands
        [DAL.SqlCommandMethod(CommandType.StoredProcedure,"_insertCatalogSection")]
        public bool Insert(
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int typeM ,
			[SqlParameter(SqlDbType.Int		 ,4,ParameterDirection.Input)]int catalogCodeM,
            [SqlParameter(SqlDbType.VarChar	 ,50,ParameterDirection.Input)]string nameM,
			[SqlParameter(SqlDbType.Int		 ,ParameterDirection.Output)]out int ID)
        {
            ID=-1;
            try
            {
                SqlCommand command = SqlCommandGenerator.GenerateCommand(connection,null,
                    new object[]{typeM, catalogCodeM, nameM, ID});

                command.ExecuteNonQuery();
                ID = (int)command.Parameters["@ID"].Value;
            }
            catch(InvalidOperationException )
            {              			
                SetCode(1);
            }
            catch( SqlException )
            {
                SetCode(1);
            }
            catch( Exception )
            {
                SetCode(1);
            }
            return true;
        }

        #endregion
        [DAL.SqlCommandMethod(CommandType.StoredProcedure,"_getCatalogSection")]
        public DataTable Exists([SqlParameter(SqlDbType.Int,4,ParameterDirection.Input)]int ID)
        {
            int nCount=0;
            DataTable dataTable = new DataTable();
            try
            {
                
                SqlCommand command = SqlCommandGenerator.GenerateCommand(connection,null,
                    new object[]{ID});

                 nCount = command.ExecuteNonQuery();
                
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                dataAdapter.SelectCommand = command;
                dataAdapter.Fill( dataTable);    
            }
            catch(InvalidOperationException)
            {              			
                SetCode(1);
            }
            catch( SqlException)
            {
                SetCode(1);
            }
            catch( Exception)
            {
                SetCode(1);
            }
           return dataTable;
        }
	}
}
