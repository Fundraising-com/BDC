//
// 2005-07-11 - Stephen Lim - New class.
//


using System;
using System.Data;

namespace GA.BDC.Core.Data.Sql
{
	[Serializable]
	public class SqlDataParameter
	{
		#region Fields
		private string _parameterName = "";
		private DbType _dbType = DbType.String;
		private object _value = null;
		private ParameterDirection _direction = ParameterDirection.Input;
		private string _sourceColumn = "";
		#endregion

		#region Constructors
		public SqlDataParameter()
		{

		}

		public SqlDataParameter(string paramName) : this(paramName, DbType.String)
		{

		}

		public SqlDataParameter(string paramName, DbType paramType) : this(paramName, paramType, ParameterDirection.Input)
		{

		}

		public SqlDataParameter(string paramName, DbType paramType, ParameterDirection paramDirection) :
			this(paramName, paramType, paramDirection, null)
		{

		}

		public SqlDataParameter(string paramName, DbType paramType, object paramValue) :
			this(paramName, paramType, ParameterDirection.Input, paramValue, "")
		{

		}

		public SqlDataParameter(string paramName, DbType paramType, ParameterDirection paramDirection, object paramValue) :
			this(paramName, paramType, paramDirection, paramValue, "")
		{

		}

		public SqlDataParameter(string paramName, DbType paramType, ParameterDirection paramDirection, object paramValue, string sourceColumn)
		{
			_parameterName = paramName;
			_dbType = paramType;
			_direction = paramDirection;
			_value = paramValue;
			_sourceColumn = sourceColumn;
		}
		#endregion

		#region Properties

		/// <summary>
		/// Get or set the parameter name.
		/// </summary>
		public string ParameterName
		{
			get { return _parameterName; }
			set { _parameterName = value; }
		}

		/// <summary>
		/// Get or set the data type.
		/// </summary>
		public DbType DbType
		{
			get { return _dbType; }
			set { _dbType = value; }
		}

		/// <summary>
		/// Get or set the value.
		/// </summary>
		public object Value
		{
			get { return _value; }
			set { _value = value; }
		}

		/// <summary>
		/// Get or set the parameter direction.
		/// </summary>
		public ParameterDirection Direction
		{
			get { return _direction; }
			set { _direction = value; }
		}

		/// <summary>
		/// Get or set the source column.
		/// </summary>
		public string SourceColumn
		{
			get { return _sourceColumn; }
			set { _sourceColumn = value; }
		}
		#endregion

	}
}
