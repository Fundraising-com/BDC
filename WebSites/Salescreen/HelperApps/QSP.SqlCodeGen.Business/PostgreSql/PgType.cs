using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.Attributes;

namespace QSP.SqlCodeGen.Business.PostgreSql
{
	[Serializable]
	[Class(Schema = "pg_catalog", Table = "pg_type")]
    public partial class PgType
	{
		#region Fields
		protected int oid = 0;
		protected string typName = "";
		protected short typLen = 0;
		#endregion

		#region Constructors
		public PgType()
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

		[Property(Column = "typname", Type = "String", NotNull = true)]
		public virtual string TypName
		{
			get { return this.typName; }
			set { this.typName = value; }
		}

		[Property(Column = "typlen", Type="Int16", NotNull = true)]
		public virtual short TypLen
		{
			get { return this.typLen; }
			set { this.typLen = value; }
		}

		#endregion

		#region Methods
		public static PgType GetPgType(int id)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<PgType>(id);
			}
		}

		public static List<PgType> GetPgTypeList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				IQuery q = session.CreateQuery("FROM PgType p");
				return (List<PgType>)q.List<PgType>();
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

			PgType castObj = (PgType)obj;
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
