using System;
using Interop.OLEDB32;
using Genghis.Windows.Forms;
using System.Windows.Forms;

namespace ADONET.DbUtils
{

  [Flags]
  public enum ManagedProviderType
  {
    OleDb,
    SqlClient,
    Oracle,
    Odbc,
    All = Odbc | OleDb | SqlClient | Oracle
  }

	/// <summary>
	/// Summary description for DBUtils.
	/// </summary>
	public class ConnectionStrings
	{
		private ConnectionStrings()
		{
		}

    /// <summary>
    /// Retrives
    /// </summary>
    /// <returns>A Valid OleDb Connection String or an empty string if the dialog was cancelled.</returns>
    public static string GetOleDbConnectionString()
    {
      DataLinksClass cls = new DataLinksClass();
      ADODB._Connection conn = (ADODB._Connection) cls.PromptNew();
      if (conn == null) return "";
      else return conn.ConnectionString;
    }

    public static string GetConnectionString()
    {
      return new ConnectionStringBuilder(ManagedProviderType.All).GetString();
    }

    public static string GetConnectionString(ManagedProviderType type)
    {
      return new ConnectionStringBuilder(type).GetString();
    }
  }
}
