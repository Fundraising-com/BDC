using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.Attributes;

namespace QSP.SqlCodeGen.Business.PostgreSql
{
	[Serializable]
	[Class(Schema = "pg_catalog", Table = "pg_namespace")]
    public partial class PgNamespace
	{
		#region Fields
		protected int oid = 0;
		protected string nspName = "";
		#endregion

		#region Constructors
		public PgNamespace()
		{

		}
		#endregion

		#region Properties

		[Id(Column = "oid", Name = "Oid", Type = "Int32")]
		[Generator(1, Class = "native")]
		public virtual int Oid
		{
			get { return this.oid; }
			set { this.oid = value; }
		}

		[Property(Column = "nspname", Type = "String", NotNull = true)]
		public virtual string NspName
		{
			get { return this.nspName; }
			set { this.nspName = value; }
		}

		#endregion

		#region Methods
		public static PgNamespace GetPgNamespace(int id)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<PgNamespace>(id);
			}
		}

		public static List<PgNamespace> GetPgNamespaceList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				IQuery q = session.CreateQuery("FROM PgNamespace p");
				return (List<PgNamespace>)q.List<PgNamespace>();
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

			PgNamespace castObj = (PgNamespace)obj;
			return (castObj != null) &&	(this.oid == castObj.Oid);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * this.oid.GetHashCode();
		}
		#endregion
	}
}
