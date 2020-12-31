

using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using QCommon;


namespace DAL
{
	/// <summary>
	/// Provides an implementation for IQError
	/// </summary>
	public class QDataTable : DataTable//, IQError
	{
        protected int nErrorCode;
        protected string zErrorCode;
        

        public int GetCode( ){return nErrorCode;}
        public void SetCode(int nCodeP) { nErrorCode = nCodeP;}

        
		public QDataTable()
		{
			
		}

	}
}
