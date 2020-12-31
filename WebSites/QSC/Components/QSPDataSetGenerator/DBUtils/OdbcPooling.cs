using System;
using System.Runtime.InteropServices;

namespace ADONET.DbUtils
{
  /// <summary>
  /// A static only class to allow for enabling and disabling of 
  /// ODBC Connection Pooling.
  /// </summary>
  /// <example><code>
  /// // Turn on ODBC Connection Pooling
  /// ODBCPooling.Enable();
  /// </code></example>
  public class ODBCPooling
  {
    [System.Runtime.InteropServices.DllImport("odbc32.dll",
       CharSet=System.Runtime.InteropServices.CharSet.Auto)]
    private static extern int SQLSetEnvAttr(long Environment, 
      long EnvAttribute, 
      long ValuePtr, 
      long StringLength);
  
    const long SQL_ATTR_CONNECTION_POOLING = 201;
    const long SQL_CP_ONE_PER_DRIVER = 1;
    const long SQL_IS_INTEGER = -6;
    const long SQL_CP_OFF = 0;
    const long SQL_SUCCESS = 0;
    const long SQL_SUCCESS_WITH_INFO = 1;
  
    // Disable creation
    private ODBCPooling() {}

    /// <summary>
    /// Used to enable connection pooling in ODBC for this process.
    /// </summary>
    /// <returns>True if successfully enabled.</returns>
    static public bool Enable()
    {
      long result = SQLSetEnvAttr(0, 
                                  SQL_ATTR_CONNECTION_POOLING, 
                                  SQL_CP_ONE_PER_DRIVER,
                                  SQL_IS_INTEGER);
      return (result == SQL_SUCCESS || result == SQL_SUCCESS_WITH_INFO);
    }

    /// <summary>
    /// Used to disable connection pooling in ODBC for this process.
    /// </summary>
    /// <returns>True if successfully disabled.</returns>
    static public bool Disable()
    {
      long result = SQLSetEnvAttr(0, 
                                  SQL_ATTR_CONNECTION_POOLING, 
                                  SQL_CP_OFF,
                                  SQL_IS_INTEGER);
      return (result == SQL_SUCCESS || result == SQL_SUCCESS_WITH_INFO);
    }
  }
}
