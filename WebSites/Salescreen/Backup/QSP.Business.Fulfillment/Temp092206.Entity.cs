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
	[Class(Schema="`dbo`", Table="`Temp092206`")]
	public partial class Temp092206
	{
		#region Constants
        public const string Temp092206Entity = "Temp092206";
		public const string OrderIdProperty = "OrderId";
		#endregion

		#region Fields
		protected int orderId = 0;
		#endregion

		#region Constructors
		public Temp092206() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""OrderId"" column=""`order_id`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int OrderId
		{
			get { return this.orderId; }
			set { this.orderId = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Temp092206));
                return c;
            }
        }

        public static List<Temp092206> GetTemp092206List(ICriteria criteria)
        {
            return (List<Temp092206>)criteria.List<Temp092206>();
        }

		public static Temp092206 GetTemp092206(int orderId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<Temp092206>(orderId);
			}
		}

		public static List<Temp092206> GetTemp092206List()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Temp092206));
				return (List<Temp092206>)c.List<Temp092206>();
			}
		}

		public static List<Temp092206> GetTemp092206List(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Temp092206));
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

				return (List<Temp092206>)c.List<Temp092206>();
			}
		}

		public static List<Temp092206> GetTemp092206List(string sortExpression)
		{
			return GetTemp092206List(sortExpression, -1, -1);
		}

		public static void InsertTemp092206(Temp092206 obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateTemp092206(Temp092206 obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteTemp092206(Temp092206 obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static Temp092206 PopulateTemp092206(IDataReader r)
		{
			Temp092206 obj = new Temp092206();
			obj.OrderId = (int)r["order_id"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Temp092206));
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

			Temp092206 castObj = (Temp092206)obj;
			return (castObj != null && this.orderId == castObj.OrderId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.orderId.GetHashCode());
		}
		#endregion
	}
}
