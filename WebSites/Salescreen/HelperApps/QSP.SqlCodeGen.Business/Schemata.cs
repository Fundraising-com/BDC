using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Mapping.Attributes;

namespace QSP.SqlCodeGen.Business
{
	[Serializable]
	[Class(Schema = "information_schema", Table = "schemata")]
    public partial class Schemata
	{
		#region Fields
		protected string catalogName = "";
		protected string schemaName = "";
		protected string defaultCharacterSetName = "";

		#endregion

		#region Constructors
		public Schemata()
		{

		}
		#endregion

		#region Properties

		[RawXml(Content = @"
		<composite-id>
			<key-property name=""CatalogName"" column=""catalog_name"" />
			<key-property name=""SchemaName"" column=""schema_name"" />
		</composite-id>")]

		[Property(Column = "catalog_name")]
		public virtual string CatalogName
		{
			get { return this.catalogName; }
			set { this.catalogName = value; }
		}

		[Property(Column = "schema_name")]
		public virtual string SchemaName
		{
			get { return this.schemaName; }
			set { this.schemaName = value; }
		}

		[Property(Column = "default_character_set_name")]
		public virtual string DefaultCharacterSetName
		{
			get { return this.defaultCharacterSetName; }
			set { this.defaultCharacterSetName = value; }
		}

		#endregion

		#region Methods
		public static Schemata GetSchemata(int id)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<Schemata>(id);
			}
		}

		public static List<Schemata> GetSchemataList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Schemata));
				return (List<Schemata>)c.List<Schemata>();
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

			Schemata castObj = (Schemata)obj;
			return (castObj != null) &&	(this.schemaName == castObj.SchemaName);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * this.schemaName.GetHashCode();
		}
		#endregion
	}
}
