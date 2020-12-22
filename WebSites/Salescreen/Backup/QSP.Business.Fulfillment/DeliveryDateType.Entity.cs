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
    [Class(Schema = "`dbo`", Table = "`delivery_date_type`")]
    public partial class DeliveryDateType
    {
        #region Constants
        public const string DeliveryDateTypeEntity = "DeliveryDateType";
        public const string DeliveryDateTypeIdProperty = "DeliveryDateTypeId";
		public const string NameProperty = "Name";
		public const string DescriptionProperty = "Description";
		public const string IsDateUsedProperty = "IsDateUsed";
        public const string IsTimeUsedProperty = "IsTimeUsed";
        public const string IsOptionUsedProperty = "IsOptionUsed";
		#endregion

		#region Fields
        protected int deliveryDateTypeId = 0;
		protected string name = null;
		protected string description = null;
		protected bool isDateUsed = false;
        protected bool isTimeUsed = false;
        protected bool isOptionUsed = false;
		#endregion

		#region Constructors
        public DeliveryDateType() 
		{
		}
		#endregion

		#region Properties

		[RawXml(Content= @"
		<id name=""DeliveryDateTypeId"" column=""`delivery_date_type_id`"">
			<generator class=""native"">
			</generator>
		</id>")]
        public virtual int DeliveryDateTypeId
		{
            get { return this.deliveryDateTypeId; }
            set { this.deliveryDateTypeId = value; }
		}

		[Property(Column="`name`")]
		public virtual string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		[Property(Column="`description`")]
		public virtual string Description
		{
            get { return this.description; }
            set { this.description = value; }
		}

        [Property(Column="`is_date_used`")]
        public virtual bool IsDateUsed
		{
            get { return this.isDateUsed; }
            set { this.isDateUsed = value; }
		}

        [Property(Column = "`is_time_used`")]
        public virtual bool IsTimeUsed
        {
            get { return this.isTimeUsed; }
            set { this.isTimeUsed = value; }
        }

        [Property(Column="`is_option_used`")]
        public virtual bool IsOptionUsed
        {
            get { return this.isOptionUsed; }
            set { this.isOptionUsed = value; }
        }
        
        #endregion

		#region Methods
        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(DeliveryDateType));
                return c;
            }
        }

        public static List<DeliveryDateType> GetDeliveryDateTypeList(ICriteria criteria)
        {
            return (List<DeliveryDateType>)criteria.List<DeliveryDateType>();
        }

        public static DeliveryDateType GetDeliveryDateType(int deliveryDateTypeId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
                return session.Get<DeliveryDateType>(deliveryDateTypeId);
			}
		}

        public static List<DeliveryDateType> GetDeliveryDateTypeList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
                ICriteria c = session.CreateCriteria(typeof(DeliveryDateType));
                return (List<DeliveryDateType>)c.List<DeliveryDateType>();
			}
		}

        public static List<DeliveryDateType> GetDeliveryDateTypeList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
                ICriteria c = session.CreateCriteria(typeof(DeliveryDateType));
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

                return (List<DeliveryDateType>)c.List<DeliveryDateType>();
			}
		}

        public static List<DeliveryDateType> GetDeliveryDateTypeList(string sortExpression)
		{
            return GetDeliveryDateTypeList(sortExpression, -1, -1);
		}

        public static void InsertDeliveryDateType(DeliveryDateType obj)
		{
			if (obj != null)
				obj.Insert();
		}

        public static void UpdateDeliveryDateType(DeliveryDateType obj)
		{
			if (obj != null)
				obj.Update();
		}

        public static void DeleteDeliveryDateType(DeliveryDateType obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static DeliveryDateType PopulateDeliveryDateType(IDataReader r)
		{
            DeliveryDateType obj = new DeliveryDateType();
            obj.DeliveryDateTypeId = (int)r["delivery_date_type_id"];
            obj.Name = (r["name"] == DBNull.Value) ? null : (string)r["name"];
            obj.Description = (r["description"] == DBNull.Value) ? null : (string)r["description"];
            obj.IsDateUsed = (bool)r["is_date_used"];
            obj.IsTimeUsed = (bool)r["is_time_used"];
            obj.IsOptionUsed = (bool)r["is_option_used"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
                ICriteria c = session.CreateCriteria(typeof(DeliveryDateType));
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

            DeliveryDateType castObj = (DeliveryDateType)obj;
            return (castObj != null && this.deliveryDateTypeId == castObj.DeliveryDateTypeId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
            return 29 * (1 + this.deliveryDateTypeId.GetHashCode());
		}
		#endregion
    }
}
