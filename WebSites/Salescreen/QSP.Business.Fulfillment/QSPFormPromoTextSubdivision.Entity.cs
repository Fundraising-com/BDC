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
using NHibernate.Criterion;
using NHibernate.Mapping.Attributes;

namespace QSP.Business.Fulfillment
{
	[Serializable]
	[Class(Schema="`dbo`", Table="`QSPForm_promo_text_subdivision`")]
	public partial class QSPFormPromoTextSubdivision
	{
		#region Constants
        public const string QSPFormPromoTextSubdivisionEntity = "QSPFormPromoTextSubdivision";
		public const string PromoTextSubdivisionIdProperty = "PromoTextSubdivisionId";
		public const string PromoTextIdProperty = "PromoTextId";
		public const string SubdivisionCodeProperty = "SubdivisionCode";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		#endregion

		#region Fields
		protected int promoTextSubdivisionId = 0;
		protected int promoTextId = 0;
		protected string subdivisionCode = "";
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime updateDate = DateTime.Now;
		protected int updateUserId = 0;
		#endregion

		#region Constructors
		public QSPFormPromoTextSubdivision() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""PromoTextSubdivisionId"" column=""`promo_text_subdivision_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int PromoTextSubdivisionId
		{
			get { return this.promoTextSubdivisionId; }
			set { this.promoTextSubdivisionId = value; }
		}

		[Property(Column="`promo_text_id`")]
		public virtual int PromoTextId
		{
			get { return this.promoTextId; }
			set { this.promoTextId = value; }
		}

		[Property(Column="`subdivision_code`")]
		public virtual string SubdivisionCode
		{
			get { return this.subdivisionCode; }
			set { this.subdivisionCode = value; }
		}

		[Property(Column="`deleted`")]
		public virtual bool Deleted
		{
			get { return this.deleted; }
			set { this.deleted = value; }
		}

		[Property(Column="`create_date`")]
		public virtual DateTime CreateDate
		{
			get { return this.createDate; }
			set { this.createDate = value; }
		}

		[Property(Column="`create_user_id`")]
		public virtual int CreateUserId
		{
			get { return this.createUserId; }
			set { this.createUserId = value; }
		}

		[Property(Column="`update_date`")]
		public virtual DateTime UpdateDate
		{
			get { return this.updateDate; }
			set { this.updateDate = value; }
		}

		[Property(Column="`update_user_id`")]
		public virtual int UpdateUserId
		{
			get { return this.updateUserId; }
			set { this.updateUserId = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(QSPFormPromoTextSubdivision));
                return c;
            }
        }

        public static List<QSPFormPromoTextSubdivision> GetQSPFormPromoTextSubdivisionList(ICriteria criteria)
        {
            return (List<QSPFormPromoTextSubdivision>)criteria.List<QSPFormPromoTextSubdivision>();
        }

		public static QSPFormPromoTextSubdivision GetQSPFormPromoTextSubdivision(int promoTextSubdivisionId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<QSPFormPromoTextSubdivision>(promoTextSubdivisionId);
			}
		}

		public static List<QSPFormPromoTextSubdivision> GetQSPFormPromoTextSubdivisionList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QSPFormPromoTextSubdivision));
				return (List<QSPFormPromoTextSubdivision>)c.List<QSPFormPromoTextSubdivision>();
			}
		}

		public static List<QSPFormPromoTextSubdivision> GetQSPFormPromoTextSubdivisionList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QSPFormPromoTextSubdivision));
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
							c.AddOrder(NHibernate.Criterion.Order.Desc(expressions[i]));
						else
							c.AddOrder(NHibernate.Criterion.Order.Asc(expressions[i]));
					}
				}

				// Set offset and limit
				if (startRowIndex >= 0)
					c.SetFirstResult(startRowIndex);

				if (maximumRows >= 0)
					c.SetMaxResults(maximumRows);

				return (List<QSPFormPromoTextSubdivision>)c.List<QSPFormPromoTextSubdivision>();
			}
		}

		public static List<QSPFormPromoTextSubdivision> GetQSPFormPromoTextSubdivisionList(string sortExpression)
		{
			return GetQSPFormPromoTextSubdivisionList(sortExpression, -1, -1);
		}

		public static void InsertQSPFormPromoTextSubdivision(QSPFormPromoTextSubdivision obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateQSPFormPromoTextSubdivision(QSPFormPromoTextSubdivision obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteQSPFormPromoTextSubdivision(QSPFormPromoTextSubdivision obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static QSPFormPromoTextSubdivision PopulateQSPFormPromoTextSubdivision(IDataReader r)
		{
			QSPFormPromoTextSubdivision obj = new QSPFormPromoTextSubdivision();
			obj.PromoTextSubdivisionId = (int)r["promo_text_subdivision_id"];
			obj.PromoTextId = (int)r["promo_text_id"];
			obj.SubdivisionCode = (string)r["subdivision_code"];
			obj.Deleted = (bool)r["deleted"];
			obj.CreateDate = (DateTime)r["create_date"];
			obj.CreateUserId = (int)r["create_user_id"];
			obj.UpdateDate = (DateTime)r["update_date"];
			obj.UpdateUserId = (int)r["update_user_id"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QSPFormPromoTextSubdivision));
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

			QSPFormPromoTextSubdivision castObj = (QSPFormPromoTextSubdivision)obj;
			return (castObj != null && this.promoTextSubdivisionId == castObj.PromoTextSubdivisionId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.promoTextSubdivisionId.GetHashCode());
		}
		#endregion
	}
}
