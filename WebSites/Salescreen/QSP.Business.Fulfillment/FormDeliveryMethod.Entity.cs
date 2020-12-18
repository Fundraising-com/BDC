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
	[Class(Schema="`dbo`", Table="`form_delivery_method`")]
	public partial class FormDeliveryMethod
	{
		#region Constants
        public const string FormDeliveryMethodEntity = "FormDeliveryMethod";
		public const string FormDeliveryMethodIdProperty = "FormDeliveryMethodId";
		public const string FormIdProperty = "FormId";
		public const string DeliveryMethodIdProperty = "DeliveryMethodId";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		#endregion

		#region Fields
		protected int formDeliveryMethodId = 0;
		protected int formId = 0;
		protected int deliveryMethodId = 0;
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime updateDate = DateTime.Now;
		protected int updateUserId = 0;
		#endregion

		#region Constructors
		public FormDeliveryMethod() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""FormDeliveryMethodId"" column=""`form_delivery_method_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int FormDeliveryMethodId
		{
			get { return this.formDeliveryMethodId; }
			set { this.formDeliveryMethodId = value; }
		}

		[Property(Column="`form_id`")]
		public virtual int FormId
		{
			get { return this.formId; }
			set { this.formId = value; }
		}

		[Property(Column="`delivery_method_id`")]
		public virtual int DeliveryMethodId
		{
			get { return this.deliveryMethodId; }
			set { this.deliveryMethodId = value; }
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
                ICriteria c = session.CreateCriteria(typeof(FormDeliveryMethod));
                return c;
            }
        }

        public static List<FormDeliveryMethod> GetFormDeliveryMethodList(ICriteria criteria)
        {
            return (List<FormDeliveryMethod>)criteria.List<FormDeliveryMethod>();
        }

		public static FormDeliveryMethod GetFormDeliveryMethod(int formDeliveryMethodId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<FormDeliveryMethod>(formDeliveryMethodId);
			}
		}

		public static List<FormDeliveryMethod> GetFormDeliveryMethodList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(FormDeliveryMethod));
				return (List<FormDeliveryMethod>)c.List<FormDeliveryMethod>();
			}
		}

		public static List<FormDeliveryMethod> GetFormDeliveryMethodList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(FormDeliveryMethod));
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

				return (List<FormDeliveryMethod>)c.List<FormDeliveryMethod>();
			}
		}

		public static List<FormDeliveryMethod> GetFormDeliveryMethodList(string sortExpression)
		{
			return GetFormDeliveryMethodList(sortExpression, -1, -1);
		}

		public static void InsertFormDeliveryMethod(FormDeliveryMethod obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateFormDeliveryMethod(FormDeliveryMethod obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteFormDeliveryMethod(FormDeliveryMethod obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static FormDeliveryMethod PopulateFormDeliveryMethod(IDataReader r)
		{
			FormDeliveryMethod obj = new FormDeliveryMethod();
			obj.FormDeliveryMethodId = (int)r["form_delivery_method_id"];
			obj.FormId = (int)r["form_id"];
			obj.DeliveryMethodId = (int)r["delivery_method_id"];
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
				ICriteria c = session.CreateCriteria(typeof(FormDeliveryMethod));
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

			FormDeliveryMethod castObj = (FormDeliveryMethod)obj;
			return (castObj != null && this.formDeliveryMethodId == castObj.FormDeliveryMethodId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.formDeliveryMethodId.GetHashCode());
		}
		#endregion
	}
}
