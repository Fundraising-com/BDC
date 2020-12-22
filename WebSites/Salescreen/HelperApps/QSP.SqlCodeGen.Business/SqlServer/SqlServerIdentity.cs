using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.Attributes;

namespace QSP.SqlCodeGen.Business.SqlServer
{
	[Serializable]
	[Class]
    public partial class SqlServerIdentity
	{
		#region Constructors
		public SqlServerIdentity()
		{

		} 
		#endregion

		#region Fields
		private string tableCatalog = ""; 
		private string tableSchema = ""; 
		private string tableName = "";
		private string columnName = "";
		private bool isIdentity = false;
		#endregion

		#region Properties

		[RawXml(Content = @"
		<composite-id>
			<key-property name=""TableCatalog"" column=""table_catalog"" />
			<key-property name=""TableSchema"" column=""table_schema"" />
			<key-property name=""TableName"" column=""table_name"" />
			<key-property name=""ColumnName"" column=""column_name"" />
		</composite-id>")]
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

		[Property(Column = "is_identity")]
		public virtual bool IsIdentity
		{
			get { return this.isIdentity; }
			set { this.isIdentity = value; }
		}

		public static List<SqlServerIdentity> GetSqlServerIdentityList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ISQLQuery q = session.CreateSQLQuery(@"SELECT TABLE_CATALOG as table_catalog, 
															TABLE_SCHEMA as table_schema, 
															TABLE_NAME as table_name, 
															COLUMN_NAME as column_name, 
															COLUMNPROPERTY( OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') As is_identity
														FROM INFORMATION_SCHEMA.COLUMNS");
				q.AddEntity(typeof(SqlServerIdentity));
				return (List<SqlServerIdentity>)q.List<SqlServerIdentity>();
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
			return (castObj != null) && (this.tableCatalog == castObj.TableCatalog && this.tableSchema == castObj.TableSchema && this.tableName == castObj.TableName);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * this.tableCatalog.GetHashCode() + this.tableSchema.GetHashCode() + this.tableName.GetHashCode();
		}
		#endregion



	}
}
