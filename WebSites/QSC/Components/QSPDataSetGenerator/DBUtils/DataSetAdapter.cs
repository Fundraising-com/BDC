using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ADONET.DbUtils
{
	/// <summary>
	/// Summary description for DataSetAdapter.
	/// </summary>
	public class DataSetAdapter
	{
    /// <summary>
    /// 
    /// </summary>
		public DataSetAdapter()
		{
		}

    #region Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataTableName"></param>
    /// <param name="dataAdapter"></param>
    public void AddDataAdapter(string dataTableName, DbDataAdapter dataAdapter)
    {
      _adapterList.Add(dataTableName, new DataAdapterTablePair(dataAdapter, dataTableName));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataAdapter"></param>
    public void AddDataAdapter(DbDataAdapter dataAdapter)
    {
      AddDataAdapter(dataAdapter.TableMappings[0].DataSetTable, dataAdapter);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="adapters"></param>
    public void AddDataAdapters(DbDataAdapter[] adapters)
    {
      foreach (DbDataAdapter adapter in adapters)
      {
        AddDataAdapter(adapter);
      }
    }

    public void AddDataAdapters(string[] tableNames, DbDataAdapter[] adapters)
    {
      if (tableNames.Length != adapters.Length || tableNames.GetLowerBound(0) != adapters.GetLowerBound(0)) throw new ArgumentException("tableNames and adapters must be the same length", "tableNames");

      for (int x = tableNames.GetLowerBound(0); x < tableNames.GetUpperBound(0); ++x)
      {
        AddDataAdapter(tableNames[x], adapters[x]);
      }

    }

    public void Fill(DataSet dataSet)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataSet"></param>
    public void Update(DataSet dataSet)
    {
      if (_adapterList.Count == 0)
      {
        throw new Exception("Can not Update the DataSet.  No DataAdapters have been added");
      }

      CalculateUpd

    }

    void CalculateUpdateTree()
    {
    }

    #endregion

    #region Implementation Fields
    ArrayList _updateTree = new ArrayList();
    Hashtable _adapterList = new Hashtable();
    #endregion

    #region DataAdapterDataTableTwople
    internal struct DataAdapterTablePair
    {
      public DataAdapterTablePair(DbDataAdapter dataAdapter, string tableName)
      {
        this.dataAdapter = dataAdapter;
        this.tableName = tableName;
      }

      public DbDataAdapter dataAdapter;
      public string tableName;
    }
    #endregion
	}
}
