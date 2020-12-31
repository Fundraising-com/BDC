using System;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Genghis.Windows.Forms;

namespace ADONET.DbUtils
{
  /// <summary>
  /// Summary description for ConnectionStringBuillder.
  /// </summary>
  internal class ConnectionStringBuilder
  {
    #region Construction
    internal ConnectionStringBuilder(ManagedProviderType type)
    {
      // Cache the Type
      _type = type;
      
      // Create the default rows in the DataSet (for binding)
      _csds.SqlClient.AddSqlClientRow(_csds.SqlClient.NewSqlClientRow());
      _csds.SqlClient.AcceptChanges();

      // Cache the Pages
      _providerPage       = new ConnStringProviderPage(_type);
      _sqlLocationPage    = new SqlClientLocation(_csds.SqlClient);
      _sqlSecurityPage    = new SqlClientSecurity(_csds.SqlClient);
      _oledbLocationPage  = new OleDbLocation();
    }
    #endregion

    #region internal interface
    internal string GetString()
    {
      // Add Pages to the Sheet
      _sheet.AddPage(_providerPage, new string[] { "SqlClient", "OleDb", "Odbc", "Oracle"} );
      _sheet.AddPage(_sqlLocationPage, "SqlClient");
      _sheet.AddPage(_sqlSecurityPage, "SqlClient");
      _sheet.AddPage(_oledbLocationPage, "OleDb");

      // Show the wizard
      if (_sheet.ShowDialog() == DialogResult.OK)
      {
        if (_sheet.CurrentGroup == "SqlClient")
        {
          return CalculateSqlString();
        }
        else if (_sheet.CurrentGroup == "OleDb")
        {
          return CalculateOleDbString();
        }
        else if (_sheet.CurrentGroup == "Oracle")
        {
          return CalculateOracleString();
        }
        else if (_sheet.CurrentGroup == "Odbc")
        {
          return CalculateOdbcString();
        }
      }

      return "";
    }
    #endregion

    #region private data
    ManagedProviderType     _type;
    ConnStringDataSet       _csds = new ConnStringDataSet();
    WizardSheet             _sheet = new WizardSheet("Connection String Builder");
    ConnStringProviderPage  _providerPage;
    SqlClientLocation       _sqlLocationPage;
    SqlClientSecurity       _sqlSecurityPage;
    OleDbLocation           _oledbLocationPage;
    #endregion

    #region private methods
    private string CalculateSqlString()
    {
      StringBuilder returnString = new StringBuilder();
      DataRow row = _csds.SqlClient[0];
      for (int x = 0; x < row.ItemArray.Length; ++x)
      {
#if DEBUG
        object original = row[x,DataRowVersion.Original];
        object proposed = row[x,DataRowVersion.Proposed];
        System.Diagnostics.Trace.WriteLine(string.Format("{0} : {1} == {2}", original.Equals(proposed), original, proposed));
#endif
        if (!row[x,DataRowVersion.Proposed].Equals(row[x,DataRowVersion.Original]))
        {
          returnString.AppendFormat("{0}={1};", _csds.SqlClient.Columns[x].ColumnName, row[x]);
        }
      }
      System.Diagnostics.Trace.WriteLine(returnString.ToString());
      return returnString.ToString();
    }

    private string CalculateOleDbString()
    {
      return "";
    }
    
    private string CalculateOracleString()
    {
      return "";
    }
  
    private string CalculateOdbcString()
    {
      return "";
    }
    #endregion
  }
}
