////////////////////////////////////////////////////////////////////////////////////////////
// Class generated by SqlCodeGen 1.1.0.0.
// Do not edit this file directly. Changes will be lost when this file is regenerated.
// Extensions should be added in a separate file using partial classes.
////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NHibernate;
using NHibernate.Expression;
using NHibernate.Mapping.Attributes;

namespace QSP.Business.Fulfillment
{
	[Serializable]
	[Class(Schema="`dbo`", Table="`Discount_Program_Discount_Class`")]
	public partial class DiscountProgramDiscountClass
	{
		#region Constants
        public const string DiscountProgramDiscountClassEntity = "DiscountProgramDiscountClass";
		public const string DiscountProgramDiscountClassIdProperty = "DiscountProgramDiscountClassId";
		public const string DiscountProgramIdProperty = "DiscountProgramId";
		public const string DiscountClassIdProperty = "DiscountClassId";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		public const string DeletedProperty = "Deleted";
		#endregion

		#region Fields
		protected int discountProgramDiscountClassId = 0;
		protected int discountProgramId = 0;
		protected int discountClassId = 0;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = DateTime.Now;
		protected int? updateUserId = null;
		protected bool deleted = false;
		#endregion

		#region Constructors
		public DiscountProgramDiscountClass() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""DiscountProgramDiscountClassId"" column=""`Discount_Program_Discount_Class_Id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int DiscountProgramDiscountClassId
		{
			get { return this.discountProgramDiscountClassId; }
			set { this.discountProgramDiscountClassId = value; }
		}

		[Property(Column="`Discount_Program_Id`")]
		public virtual int DiscountProgramId
		{
			get { return this.discountProgramId; }
			set { this.discountProgramId = value; }
		}

		[Property(Column="`Discount_Class_Id`")]
		public virtual int DiscountClassId
		{
			get { return this.discountClassId; }
			set { this.discountClassId = value; }
		}

		[Property(Column="`Create_Date`")]
		public virtual DateTime CreateDate
		{
			get { return this.createDate; }
			set { this.createDate = value; }
		}

		[Property(Column="`Create_User_Id`")]
		public virtual int CreateUserId
		{
			get { return this.createUserId; }
			set { this.createUserId = value; }
		}

		[Property(Column="`Update_Date`")]
		public virtual DateTime? UpdateDate
		{
			get { return this.updateDate; }
			set { this.updateDate = value; }
		}

		[Property(Column="`Update_User_Id`")]
		public virtual int? UpdateUserId
		{
			get { return this.updateUserId; }
			set { this.updateUserId = value; }
		}

		[Property(Column="`Deleted`")]
		public virtual bool Deleted
		{
			get { return this.deleted; }
			set { this.deleted = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(DiscountProgramDiscountClass));
                return c;
            }
        }

        public static List<DiscountProgramDiscountClass> GetDiscountProgramDiscountClassList(ICriteria criteria)
        {
            return (List<DiscountProgramDiscountClass>)criteria.List<DiscountProgramDiscountClass>();
        }

		public static DiscountProgramDiscountClass GetDiscountProgramDiscountClass(int discountProgramDiscountClassId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<DiscountProgramDiscountClass>(discountProgramDiscountClassId);
			}
		}

		public static List<DiscountProgramDiscountClass> GetDiscountProgramDiscountClassList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(DiscountProgramDiscountClass));
				return (List<DiscountProgramDiscountClass>)c.List<DiscountProgramDiscountClass>();
			}
		}

		public static List<DiscountProgramDiscountClass> GetDiscountProgramDiscountClassList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(DiscountProgramDiscountClass));
				if (sortExpression != null && sortExpression != "")
				{
					// Get ascending or descending order
					bool descending = sortExpression.Contains(" DESC");

					// Strip off ASC or DESC ordering
					sortExpression = sortExpression.Replace(" ASC", "");
					sortExpression = sortExpression.Replace(" DESC", "");
					sortExpression = sortExpression.Trim();

					// Get multi column sort from the comma delimited string
					List<String> expressions = new List<String>();
					if (sortExpression.Contains(","))
					{
						string[] tokens = sortExpression.Split(",".ToCharArray());
						for (int i = 0; i < tokens.Length; i++)
						{
							tokens[i] = tokens[i].Trim();
							if (tokens[i] != "")
								expressions.Add(tokens[i]);
						}
					}
					else if (sortExpression != "")
					{
						expressions.Add(sortExpression);
					}

					// Create the order
					for (int i = 0; i < expressions.Count; i++)
					{
						if (descending)
							c.AddOrder(NHibernate.Expression.Order.Desc(expressions[i]));
						else
							c.AddOrder(NHibernate.Expression.Order.Asc(expressions[i]));
					}
				}

				// Set offset and limit
				if (startRowIndex >= 0)
					c.SetFirstResult(startRowIndex);

				if (maximumRows >= 0)
					c.SetMaxResults(maximumRows);

				return (List<DiscountProgramDiscountClass>)c.List<DiscountProgramDiscountClass>();
			}
		}

		public static List<DiscountProgramDiscountClass> GetDiscountProgramDiscountClassList(string sortExpression)
		{
			return GetDiscountProgramDiscountClassList(sortExpression, -1, -1);
		}

		public static void InsertDiscountProgramDiscountClass(DiscountProgramDiscountClass obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateDiscountProgramDiscountClass(DiscountProgramDiscountClass obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteDiscountProgramDiscountClass(DiscountProgramDiscountClass obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static DiscountProgramDiscountClass PopulateDiscountProgramDiscountClass(IDataReader r)
		{
			DiscountProgramDiscountClass obj = new DiscountProgramDiscountClass();
			obj.DiscountProgramDiscountClassId = (int)r["Discount_Program_Discount_Class_Id"];
			obj.DiscountProgramId = (int)r["Discount_Program_Id"];
			obj.DiscountClassId = (int)r["Discount_Class_Id"];
			obj.CreateDate = (DateTime)r["Create_Date"];
			obj.CreateUserId = (int)r["Create_User_Id"];
			obj.UpdateDate = (r["Update_Date"] == DBNull.Value) ? null : (DateTime?)r["Update_Date"];
			obj.UpdateUserId = (r["Update_User_Id"] == DBNull.Value) ? null : (int?)r["Update_User_Id"];
			obj.Deleted = (bool)r["Deleted"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(DiscountProgramDiscountClass));
				c.SetProjection(Projections.RowCount());
				return (int) c.UniqueResult();
			}
		}

		/// <summary>
		/// Insert the entity to database.
		/// </summary>
		public virtual void Insert()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ITransaction trans = session.BeginTransaction();
				try
				{
					session.Save(this);
					trans.Commit();
				}
				catch
				{
					trans.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Update the entity to database.
		/// </summary>
		public virtual void Update()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ITransaction trans = session.BeginTransaction();
				try
				{
					session.Update(this);
					trans.Commit();
				}
				catch
				{
					trans.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Persist the entity back to database.
		/// </summary>
		public virtual void Save()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ITransaction trans = session.BeginTransaction();
				try
				{
					session.SaveOrUpdate(this);
					trans.Commit();
				}
				catch
				{
					trans.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Delete entity in database.
		/// </summary>
		public virtual void Delete()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ITransaction trans = session.BeginTransaction();
				try
				{
					session.Delete(this);
					trans.Commit();
				}
				catch
				{
					trans.Rollback();
					throw;
				}
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

			DiscountProgramDiscountClass castObj = (DiscountProgramDiscountClass)obj;
			return (castObj != null && this.discountProgramDiscountClassId == castObj.DiscountProgramDiscountClassId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.discountProgramDiscountClassId.GetHashCode());
		}
		#endregion
	}
}
