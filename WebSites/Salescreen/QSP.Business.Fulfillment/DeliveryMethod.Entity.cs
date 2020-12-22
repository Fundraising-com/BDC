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
	[Class(Schema="`dbo`", Table="`delivery_method`")]
	public partial class DeliveryMethod
	{
		#region Constants
        public const string DeliveryMethodEntity = "DeliveryMethod";
		public const string DeliveryMethodIdProperty = "DeliveryMethodId";
		public const string DeliveryMethodNameProperty = "DeliveryMethodName";
		#endregion

		#region Fields
		protected int deliveryMethodId = 0;
		protected string deliveryMethodName = "";
		#endregion

		#region Constructors
		public DeliveryMethod() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""DeliveryMethodId"" column=""`delivery_method_id`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int DeliveryMethodId
		{
			get { return this.deliveryMethodId; }
			set { this.deliveryMethodId = value; }
		}

		[Property(Column="`delivery_method_name`")]
		public virtual string DeliveryMethodName
		{
			get { return this.deliveryMethodName; }
			set { this.deliveryMethodName = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(DeliveryMethod));
            return c;
            
        }

		public static DeliveryMethod GetDeliveryMethod(int deliveryMethodId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<DeliveryMethod>(deliveryMethodId);
			}
		}

		public static List<DeliveryMethod> GetDeliveryMethodList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(DeliveryMethod));
				return (List<DeliveryMethod>)c.List<DeliveryMethod>();
			}
		}

		public static List<DeliveryMethod> GetDeliveryMethodList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(DeliveryMethod));
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

				return (List<DeliveryMethod>)c.List<DeliveryMethod>();
			}
		}

		public static List<DeliveryMethod> GetDeliveryMethodList(string sortExpression)
		{
			return GetDeliveryMethodList(sortExpression, -1, -1);
		}

        public static List<DeliveryMethod> GetDeliveryMethodList(ICriteria criteria)
        {
            return (List<DeliveryMethod>)criteria.List<DeliveryMethod>();
        }
        
        public static void InsertDeliveryMethod(DeliveryMethod obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateDeliveryMethod(DeliveryMethod obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteDeliveryMethod(DeliveryMethod obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static DeliveryMethod PopulateDeliveryMethod(IDataReader r)
		{
			DeliveryMethod obj = new DeliveryMethod();
			obj.DeliveryMethodId = (int)r["delivery_method_id"];
			obj.DeliveryMethodName = (string)r["delivery_method_name"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(DeliveryMethod));
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

			DeliveryMethod castObj = (DeliveryMethod)obj;
			return (castObj != null && this.deliveryMethodId == castObj.DeliveryMethodId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.deliveryMethodId.GetHashCode());
		}
		#endregion
	}
}
