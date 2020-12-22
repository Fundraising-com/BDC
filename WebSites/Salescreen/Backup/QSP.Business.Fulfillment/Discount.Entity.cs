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
	[Class(Schema="`dbo`", Table="`Discount`")]
	public partial class Discount
	{
		#region Constants
        public const string DiscountEntity = "Discount";
		public const string DiscountIdProperty = "DiscountId";
		public const string DiscountProgramDiscountClassIdProperty = "DiscountProgramDiscountClassId";
		public const string CodeProperty = "Code";
		public const string StartDateProperty = "StartDate";
		public const string EndDateProperty = "EndDate";
		public const string ActiveTFProperty = "ActiveTF";
		public const string RedeemedTFProperty = "RedeemedTF";
		public const string AccountIdProperty = "AccountId";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		public const string DeletedProperty = "Deleted";
		#endregion

		#region Fields
		protected int discountId = 0;
		protected int discountProgramDiscountClassId = 0;
		protected string code = "";
		protected DateTime? startDate = null;
		protected DateTime? endDate = null;
		protected bool activeTF = true;
		protected bool redeemedTF = false;
		protected int? accountId = null;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = null;
		protected int? updateUserId = null;
		protected bool deleted = false;
		#endregion

		#region Constructors
		public Discount() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""DiscountId"" column=""`Discount_Id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int DiscountId
		{
			get { return this.discountId; }
			set { this.discountId = value; }
		}

		[Property(Column="`Discount_Program_Discount_Class_Id`")]
		public virtual int DiscountProgramDiscountClassId
		{
			get { return this.discountProgramDiscountClassId; }
			set { this.discountProgramDiscountClassId = value; }
		}

		[Property(Column="`Code`")]
		public virtual string Code
		{
			get { return this.code; }
			set { this.code = value; }
		}

		[Property(Column="`Start_Date`")]
		public virtual DateTime? StartDate
		{
			get { return this.startDate; }
			set { this.startDate = value; }
		}

		[Property(Column="`End_Date`")]
		public virtual DateTime? EndDate
		{
			get { return this.endDate; }
			set { this.endDate = value; }
		}

		[Property(Column="`Active_TF`")]
		public virtual bool ActiveTF
		{
			get { return this.activeTF; }
			set { this.activeTF = value; }
		}

		[Property(Column="`Redeemed_TF`")]
		public virtual bool RedeemedTF
		{
			get { return this.redeemedTF; }
			set { this.redeemedTF = value; }
		}

		[Property(Column="`Account_Id`")]
		public virtual int? AccountId
		{
			get { return this.accountId; }
			set { this.accountId = value; }
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

		[Property(Column="`Update_User_id`")]
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
                ICriteria c = session.CreateCriteria(typeof(Discount));
                return c;
            }
        }

        public static List<Discount> GetDiscountList(ICriteria criteria)
        {
            return (List<Discount>)criteria.List<Discount>();
        }

		public static Discount GetDiscount(int discountId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<Discount>(discountId);
			}
		}

		public static List<Discount> GetDiscountList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Discount));
				return (List<Discount>)c.List<Discount>();
			}
		}

		public static List<Discount> GetDiscountList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Discount));
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

				return (List<Discount>)c.List<Discount>();
			}
		}

		public static List<Discount> GetDiscountList(string sortExpression)
		{
			return GetDiscountList(sortExpression, -1, -1);
		}

		public static void InsertDiscount(Discount obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateDiscount(Discount obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteDiscount(Discount obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static Discount PopulateDiscount(IDataReader r)
		{
			Discount obj = new Discount();
			obj.DiscountId = (int)r["Discount_Id"];
			obj.DiscountProgramDiscountClassId = (int)r["Discount_Program_Discount_Class_Id"];
			obj.Code = (string)r["Code"];
			obj.StartDate = (r["Start_Date"] == DBNull.Value) ? null : (DateTime?)r["Start_Date"];
			obj.EndDate = (r["End_Date"] == DBNull.Value) ? null : (DateTime?)r["End_Date"];
			obj.ActiveTF = (bool)r["Active_TF"];
			obj.RedeemedTF = (bool)r["Redeemed_TF"];
			obj.AccountId = (r["Account_Id"] == DBNull.Value) ? null : (int?)r["Account_Id"];
			obj.CreateDate = (DateTime)r["Create_Date"];
			obj.CreateUserId = (int)r["Create_User_Id"];
			obj.UpdateDate = (r["Update_Date"] == DBNull.Value) ? null : (DateTime?)r["Update_Date"];
			obj.UpdateUserId = (r["Update_User_id"] == DBNull.Value) ? null : (int?)r["Update_User_id"];
			obj.Deleted = (bool)r["Deleted"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Discount));
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

			Discount castObj = (Discount)obj;
			return (castObj != null && this.discountId == castObj.DiscountId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.discountId.GetHashCode());
		}
		#endregion
	}
}
