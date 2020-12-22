using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Mapping.Attributes;

namespace QSP.SqlCodeGen.Business.PostgreSql
{
	[Serializable]
	[Class(Schema = "pg_catalog", Table = "pg_attribute")]
	public partial class PgAttribute
	{
		#region Fields
		protected int attRelId = 0;
		protected string attName = "";
		protected short attNum = 0;
		protected int attTypMod = 0;
		protected int attTypId = 0;
		protected bool attNotNull = false;
		protected bool attHasDef = false;
		protected bool attIsDropped = false;
		#endregion

		#region Constructors
		public PgAttribute()
		{

		}
		#endregion

		#region Properties

		[RawXml(Content = @"
		<composite-id>
			<key-property name=""AttRelId"" column=""attrelid"" />
			<key-property name=""AttName"" column=""attname"" />
		</composite-id>")]
		[Property(Column = "attrelid", Type = "Int32", NotNull = true)]
		public virtual int AttRelId
		{
			get { return this.attRelId;  }
			set { this.attRelId = value; }
		}

		[Property(Column = "attname", Type = "String", NotNull = true)]
		public virtual string AttName
		{
			get { return this.attName; }
			set { this.attName = value; }
		}

		[Property(Column = "attNum", Type="Int16", NotNull = true)]
		public virtual short AttNum
		{
			get { return this.attNum; }
			set { this.attNum = value; }
		}

		[Property(Column = "atttypmod", Type = "Int32", NotNull = true)]
		public virtual int AttTypMod
		{
			get { return this.attTypMod; }
			set { this.attTypMod = value; }
		}

		[Property(Column = "atttypid", Type = "Int32", NotNull = true)]
		public virtual int AttTypId
		{
			get { return this.attTypId; }
			set { this.attTypId = value; }
		}

		[Property(Column = "attnotnull", Type = "Boolean", NotNull = true)]
		public virtual bool AttNotNull
		{
			get { return this.attNotNull; }
			set { this.attNotNull = value; }
		}

		[Property(Column = "atthasdef", Type = "Boolean", NotNull = true)]
		public virtual bool AttHasDef
		{
			get { return this.attHasDef; }
			set { this.attHasDef = value; }
		}

		[Property(Column = "attisdropped", Type = "Boolean", NotNull = true)]
		public virtual bool AttIsDropped
		{
			get { return this.attIsDropped; }
			set { this.attIsDropped = value; }
		}
		#endregion

		#region Methods
		public static PgAttribute GetPgAttribute(int id)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<PgAttribute>(id);
			}
		}

		public static List<PgAttribute> GetPgAttributeList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				IQuery q = session.CreateQuery("FROM PgAttribute p");
				return (List<PgAttribute>)q.List<PgAttribute>();
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

			PgAttribute castObj = (PgAttribute)obj;
			return (castObj != null) &&
				(this.attRelId == castObj.AttRelId && this.attName == castObj.AttName);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * this.attRelId.GetHashCode() + this.attName.GetHashCode();
		}
		#endregion
	}
}
