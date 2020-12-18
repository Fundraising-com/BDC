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
    [Class(Schema = "`dbo`", Table = "`form_delivery_date_type`")]
    public partial class FormDeliveryDateType
    {
        #region Constants
        public const string FormDeliveryDateTypeEntity = "FormDeliveryDateType";
        public const string FormDeliveryDateTypeIdProperty = "FormDeliveryDateTypeId";
		public const string FormIdProperty = "FormId";
		public const string DeliveryDateTypeIdProperty = "DeliveryDateTypeId";
		#endregion

		#region Fields
        protected int formDeliveryDateTypeId = 0;
        protected int formId = 0;
        protected int deliveryDateTypeId = 0;
		#endregion

		#region Constructors
        public FormDeliveryDateType() 
		{
		}
		#endregion

		#region Properties

		[RawXml(Content= @"
		<id name=""FormDeliveryDateTypeId"" column=""`form_delivery_date_type_id`"">
			<generator class=""native"">
			</generator>
		</id>")]
        public virtual int FormDeliveryDateTypeId
		{
            get { return this.formDeliveryDateTypeId; }
            set { this.formDeliveryDateTypeId = value; }
		}

        [Property(Column = "`form_id`")]
        public virtual int FormId
        {
            get { return this.formId; }
            set { this.formId = value; }
        }

        [Property(Column = "`delivery_date_type_id`")]
        public virtual int DeliveryDateTypeId
        {
            get { return this.deliveryDateTypeId; }
            set { this.deliveryDateTypeId = value; }
        }
        
        #endregion

		#region Methods
        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(FormDeliveryDateType));
                return c;
            }
        }

        public static List<FormDeliveryDateType> GetFormDeliveryDateTypeList(ICriteria criteria)
        {
            return (List<FormDeliveryDateType>)criteria.List<FormDeliveryDateType>();
        }

        public static FormDeliveryDateType GetFormDeliveryDateType(int formDeliveryDateTypeId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
                return session.Get<FormDeliveryDateType>(formDeliveryDateTypeId);
			}
		}

        public static List<FormDeliveryDateType> GetFormDeliveryDateTypeList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
                ICriteria c = session.CreateCriteria(typeof(FormDeliveryDateType));
                return (List<FormDeliveryDateType>)c.List<FormDeliveryDateType>();
			}
		}

        public static List<FormDeliveryDateType> GetFormDeliveryDateTypeList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
                ICriteria c = session.CreateCriteria(typeof(FormDeliveryDateType));
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

                return (List<FormDeliveryDateType>)c.List<FormDeliveryDateType>();
			}
		}

        public static List<FormDeliveryDateType> GetFormDeliveryDateTypeList(string sortExpression)
		{
            return GetFormDeliveryDateTypeList(sortExpression, -1, -1);
		}

        public static void InsertFormDeliveryDateType(FormDeliveryDateType obj)
		{
			if (obj != null)
				obj.Insert();
		}

        public static void UpdateFormDeliveryDateType(FormDeliveryDateType obj)
		{
			if (obj != null)
				obj.Update();
		}

        public static void DeleteFormDeliveryDateType(FormDeliveryDateType obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static FormDeliveryDateType PopulateFormDeliveryDateType(IDataReader r)
		{
            FormDeliveryDateType obj = new FormDeliveryDateType();
            obj.FormDeliveryDateTypeId = (int)r["form_delivery_date_type_id"];
            obj.FormId = (int)r["form_id"];
            obj.DeliveryDateTypeId = (int)r["delivery_date_type_id"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
                ICriteria c = session.CreateCriteria(typeof(FormDeliveryDateType));
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

            FormDeliveryDateType castObj = (FormDeliveryDateType)obj;
            return (castObj != null && this.formDeliveryDateTypeId == castObj.FormDeliveryDateTypeId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
            return 29 * (1 + this.formDeliveryDateTypeId.GetHashCode());
		}
		#endregion
    }
}

