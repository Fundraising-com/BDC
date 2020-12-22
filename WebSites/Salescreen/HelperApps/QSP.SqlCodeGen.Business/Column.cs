using System;
using System.Collections.Generic;
using System.Text;

namespace QSP.SqlCodeGen.Business
{
	public class Column
	{
		#region Constructors
		public Column()
		{

		} 
		#endregion

		#region Fields
		protected string columnType = "";
		protected int columnTypeSize = 0;
		protected bool map = true;
		protected string entityName = "";
		protected string entityType = "";
		protected string name = "";
		protected int ordinalPosition = 0;
		protected string entityDefaultValue = "";
		protected string columnDefaultValue = "";
		protected bool nullable = true;
		protected bool primaryKey = false;
		protected bool identity = false;
		#endregion

		#region Properties
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string ColumnType
		{
			get { return columnType; }
			set { columnType = value; }
		}

		public int ColumnTypeSize
		{
			get { return columnTypeSize; }
			set { columnTypeSize = value; }
		}

		public bool Nullable
		{
			get { return nullable; }
			set { nullable = value; }
		}

		public string EntityDefaultValue
		{
			get { return entityDefaultValue; }
			set { entityDefaultValue = value; }
		}

		public string ColumnDefaultValue
		{
			get { return columnDefaultValue; }
			set { columnDefaultValue = value; }
		}

		public bool Map
		{
			get { return map; }
			set { map = value; }
		}

		public string EntityName
		{
			get { return entityName; }
			set { entityName = value; }
		}


		public string EntityType
		{
			get { return entityType; }
			set { entityType = value; }
		}

		public int OrdinalPosition
		{
			get { return ordinalPosition; }
			set { ordinalPosition = value; }
		}

		public bool PrimaryKey
		{
			get { return  primaryKey; }
			set { primaryKey = value; }
		}

		public bool Identity
		{
			get { return identity; }
			set { identity = value; }
		} 
		#endregion
	}
}
