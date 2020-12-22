using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.Attributes;

namespace QSP.SqlCodeGen.Business.PostgreSql
{
	[Serializable]
	[Class(Schema = "pg_catalog", Table = "pg_class")]
    public partial class PgClass
	{
		#region Fields
		protected int oid = 0;
		protected string relName = "";
		protected int relNameSpace = 0;
		protected int relType = 0;
		protected string relKind = "";
		protected bool relHasPKey = false;
		#endregion

		#region Constructors
		public PgClass()
		{

		}
		#endregion

		#region Properties
		[Id(Column = "oid", Name = "Oid", Type = "Int32")]
		[Generator(1, Class = "native")]
		public virtual int Oid
		{
			get { return this.oid;  }
			set { this.oid = value; }
		}

		[Property(Column = "relname", Type = "String", NotNull = true)]
		public virtual string RelName
		{
			get { return this.relName; }
			set { this.relName = value; }
		}

		[Property(Column = "relnamespace", Type="Int32", NotNull = true)]
		public virtual int RelNameSpace
		{
			get { return this.relNameSpace; }
			set { this.relNameSpace = value; }
		}

		[Property(Column = "reltype", Type = "Int32", NotNull = true)]
		public virtual int RelType
		{
			get { return relType; }
			set { relType = value; }
		}

		[Property(Column = "relkind", Type = "String", NotNull = true)]
		public virtual string RelKind
		{
			get { return this.relKind; }
			set { this.relKind = value; }
		}

		[Property(Column = "relhaspkey", Type = "Boolean", NotNull = true)]
		public virtual bool RelHasPKey
		{
			get { return relHasPKey; }
			set { relHasPKey = value; }
		}
		#endregion

		#region Methods
		public static PgClass GetPgClass(int id)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<PgClass>(id);
			}
		}

		public static List<PgClass> GetPgClassList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				IQuery q = session.CreateQuery("FROM PgClass p");
				return (List<PgClass>)q.List<PgClass>();
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

			PgClass castObj = (PgClass)obj;
			return (castObj != null) &&
				(this.oid == castObj.Oid);
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
