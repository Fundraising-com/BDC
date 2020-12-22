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
	[Class(Schema="`dbo`", Table="`qxtrak_aux`")]
	public partial class QxtrakAux
	{
		#region Constants
        public const string QxtrakAuxEntity = "QxtrakAux";
		public const string QxtrakAuxIdProperty = "QxtrakAuxId";
		public const string OrderIdProperty = "OrderId";
		public const string QxtrakIdProperty = "QxtrakId";
		#endregion

		#region Fields
		protected int qxtrakAuxId = 0;
		protected int orderId = 0;
		protected string qxtrakId = "";
		#endregion

		#region Constructors
		public QxtrakAux() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""QxtrakAuxId"" column=""`qxtrak_aux_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int QxtrakAuxId
		{
			get { return this.qxtrakAuxId; }
			set { this.qxtrakAuxId = value; }
		}

		[Property(Column="`order_id`")]
		public virtual int OrderId
		{
			get { return this.orderId; }
			set { this.orderId = value; }
		}

		[Property(Column="`qxtrak_id`")]
		public virtual string QxtrakId
		{
			get { return this.qxtrakId; }
			set { this.qxtrakId = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(QxtrakAux));
                return c;
            }
        }

        public static List<QxtrakAux> GetQxtrakAuxList(ICriteria criteria)
        {
            return (List<QxtrakAux>)criteria.List<QxtrakAux>();
        }

		public static QxtrakAux GetQxtrakAux(int qxtrakAuxId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<QxtrakAux>(qxtrakAuxId);
			}
		}

		public static List<QxtrakAux> GetQxtrakAuxList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QxtrakAux));
				return (List<QxtrakAux>)c.List<QxtrakAux>();
			}
		}

		public static List<QxtrakAux> GetQxtrakAuxList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QxtrakAux));
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

				return (List<QxtrakAux>)c.List<QxtrakAux>();
			}
		}

		public static List<QxtrakAux> GetQxtrakAuxList(string sortExpression)
		{
			return GetQxtrakAuxList(sortExpression, -1, -1);
		}

		public static void InsertQxtrakAux(QxtrakAux obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateQxtrakAux(QxtrakAux obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteQxtrakAux(QxtrakAux obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static QxtrakAux PopulateQxtrakAux(IDataReader r)
		{
			QxtrakAux obj = new QxtrakAux();
			obj.QxtrakAuxId = (int)r["qxtrak_aux_id"];
			obj.OrderId = (int)r["order_id"];
			obj.QxtrakId = (string)r["qxtrak_id"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QxtrakAux));
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

			QxtrakAux castObj = (QxtrakAux)obj;
			return (castObj != null && this.qxtrakAuxId == castObj.QxtrakAuxId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.qxtrakAuxId.GetHashCode());
		}
		#endregion
	}
}
