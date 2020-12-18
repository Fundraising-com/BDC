using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.Attributes;
using NHibernate.Expression;

namespace QSP.SqlCodeGen.Business
{
	[Serializable]
	[Class(Schema = "information_schema", Table = "tables")]
    public partial class Tables
	{
		#region Fields
		protected string tableCatalog = "";
		protected string tableSchema = "";
		protected string tableName = "";
		protected string tableType = "";
		#endregion

		#region Constructors
		public Tables()
		{

		}
		#endregion

		#region Properties

		[RawXml(Content = @"
		<composite-id>
			<key-property name=""TableSchema"" column=""table_schema"" />
			<key-property name=""TableName"" column=""table_name"" />
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

		[Property(Column = "table_type")]
		public virtual string TableType
		{
			get { return this.tableType; }
			set { this.tableType = value; }
		}

		#endregion

		#region Methods
		public static Tables GetTables(int id)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<Tables>(id);
			}
		}

		public static List<Tables> GetTablesList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Tables));
				return (List<Tables>)c.List<Tables>();
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

			Tables castObj = (Tables)obj;
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
