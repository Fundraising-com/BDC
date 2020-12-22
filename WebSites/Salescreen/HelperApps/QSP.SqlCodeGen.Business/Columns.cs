using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.Attributes;

namespace QSP.SqlCodeGen.Business
{
	[Serializable]
	[Class(Schema = "information_schema", Table = "columns")]
    public partial class Columns
	{
		#region Fields
		protected string tableCatalog = "";
		protected string tableSchema = "";
		protected string tableName = "";
		protected string columnName = "";
		protected int ordinalPosition = 0;
		protected string columnDefault = "";
		protected string isNullable = "";
		protected string dataType = "";
		protected int characterMaximumLength = 0;
		protected int characterOctetLength = 0;
		protected int numericPrecision = 0;
		protected int numericScale = 0;
		protected string characterSetName = "";
		protected string collationName = "";

		#endregion

		#region Constructors
		public Columns()
		{

		}
		#endregion

		#region Properties
		[RawXml(Content = @"
		<composite-id>
			<key-property name=""TableSchema"" column=""table_schema"" />
			<key-property name=""TableName"" column=""table_name"" />
			<key-property name=""ColumnName"" column=""column_name"" />
		</composite-id>")]
		[Property(Column="table_catalog")]
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

		[Property(Column = "column_default")]
		public virtual string ColumnDefault
		{
			get { return this.columnDefault; }
			set { this.columnDefault = value; }
		}

		[Property(Column = "is_nullable")]
		public virtual string IsNullable
		{
			get { return this.isNullable; }
			set { this.isNullable = value; }
		}

		[Property(Column = "data_type")]
		public virtual string DataType
		{
			get { return this.dataType; }
			set { this.dataType = value; }
		}

		[Property(Column = "character_maximum_length")]
		public virtual int CharacterMaximumLength
		{
			get { return this.characterMaximumLength; }
			set { this.characterMaximumLength = value; }
		}

		[Property(Column = "character_octet_length")]
		public virtual int CharacterOctetLength
		{
			get { return this.characterOctetLength; }
			set { this.characterOctetLength = value; }
		}

		[Property(Column = "numeric_precision")]
		public virtual int NumericPrecision
		{
			get { return this.numericPrecision; }
			set { this.numericPrecision = value; }
		}

		[Property(Column = "numeric_scale")]
		public virtual int NumericScale
		{
			get { return this.numericScale; }
			set { this.numericScale = value; }
		}

		[Property(Column = "character_set_name")]
		public virtual string CharacterSetName
		{
			get { return this.characterSetName; }
			set { this.characterSetName = value; }
		}

		[Property(Column = "collation_name")]
		public virtual string CollationName
		{
			get { return this.collationName; }
			set { this.collationName = value; }
		}

		#endregion

		#region Methods
		public static Columns GetColumns(string tableCatalog)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<Columns>(tableCatalog);
			}
		}

		public static List<Columns> GetColumnsList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Columns));
				return (List<Columns>)c.List<Columns>();
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

			Columns castObj = (Columns)obj;
			return (castObj != null) && (this.tableSchema == castObj.TableSchema
				&& this.tableName == castObj.TableName && this.columnName == castObj.ColumnName);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * this.tableSchema.GetHashCode() +
				this.tableName.GetHashCode() + this.columnName.GetHashCode();
		}
		#endregion
	}
}
