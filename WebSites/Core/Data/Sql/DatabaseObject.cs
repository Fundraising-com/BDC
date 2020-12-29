using System;

namespace GA.BDC.Core.Data.Sql {
	/// <summary>
	/// DatabaseObject.
	/// </summary>
	public abstract class DatabaseObject {
		protected string connectionString;
		protected string dataProvider;

		public DatabaseObject() {

		}

		#region Methods

		protected void SetConnectionString(string _connectionString) 
		{
			connectionString = _connectionString;
		}

		protected void SetDataProvider(string _dataProvider) 
		{
			dataProvider = _dataProvider;
		}
		#endregion

	}
}
