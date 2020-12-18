using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.Attributes;

namespace QSP.SqlCodeGen.Business
{
	[Serializable]
	[Class(Schema = "information_schema", Table = "key_column_usage")]
    public partial class KeyColumnUsage
	{
		#region Fields
		protected string constraintCatalog = "";
		protected string constraintSchema = "";
		protected string constraintName = "";
		protected string tableCatalog = "";
		protected string tableSchema = "";
		protected string tableName = "";
		protected string columnName = "";
		protected int ordinalPosition = 1;
		
		#endregion

		#region Constructors
		public KeyColumnUsage()
		{

		}
		#endregion

		#region Properties
		[RawXml(Content = @"
		<composite-id>
			<key-property name=""ConstraintName"" column=""constraint_name"" />
			<key-property name=""TableSchema"" column=""table_schema"" />
			<key-property name=""TableName"" column=""table_name"" />
			<key-property name=""ColumnName"" column=""column_name"" />
		</composite-id>")]

		[Property(Column = "constraint_catalog")]
		public virtual string ConstraintCatalog
		{
			get { return this.constraintCatalog; }
			set { this.constraintCatalog = value; }
		}

		[Property(Column = "constraint_schema")]
		public virtual string ConstraintSchema
		{
			get { return this.constraintSchema; }
			set { this.constraintSchema = value; }
		}

		[Property(Column = "constraint_name")]
		public virtual string ConstraintName
		{
			get { return this.constraintName; }
			set { this.constraintName = value; }
		}

		[Property(Column = "table_catalog")]
		public virtual string TableCatalog
		{
			get { return this.tableCatalog; }
			set { this.tableCatalog = value; }
		}

		[Property(Column = "table_schema")]
		public virtual string TableSchema
		{
			get { return this.tableSchema; }
			set { this.tableSchema = value; }
		}

		[Property(Column = "table_name")]
		public virtual string TableName
		{
			get { return this.tableName; }
			set { this.tableName = value; }
		}

		[Property(Column = "column_name")]
		public virtual string ColumnName
		{
			get { return this.columnName; }
			set { this.columnName = value; }
		}

		[Property(Column = "ordinal_position")]
		public virtual int OrdinalPosition
		{
			get { return this.ordinalPosition; }
			set { this.ordinalPosition = value; }
		}

		#endregion

		#region Methods
		public static KeyColumnUsage GetKeyColumnUsage(int id)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<KeyColumnUsage>(id);
			}
		}

		public static List<KeyColumnUsage> GetKeyColumnUsageList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(KeyColumnUsage));
				return (List<KeyColumnUsage>)c.List<KeyColumnUsage>();
			}
		}

		/// <summary>
		/// Local implementation of Equals based on unique value members
		/// </summary>
		public override bool Equals(object obj)
		{
			if (this == obj)
				return true;

			if ((obj == null) || (obj.GetType() != this.GetType())) 
				return false;

			KeyColumnUsage castObj = (KeyColumnUsage)obj;
			return (castObj != null) &&	(this.tableSchema == castObj.TableSchema && this.tableName == castObj.TableName);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * this.tableSchema.GetHashCode() + this.tableName.GetHashCode();
		}
		#endregion
	}
}
