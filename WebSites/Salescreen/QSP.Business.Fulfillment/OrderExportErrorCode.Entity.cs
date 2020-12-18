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
	[Class(Schema="`dbo`", Table="`Order_Export_Error_Code`")]
	public partial class OrderExportErrorCode
	{
		#region Constants
        public const string OrderExportErrorCodeEntity = "OrderExportErrorCode";
		public const string ExportErrorCodeProperty = "ExportErrorCode";
		public const string ExportErrorDescriptionProperty = "ExportErrorDescription";
		#endregion

		#region Fields
		protected int exportErrorCode = 0;
		protected string exportErrorDescription = null;
		#endregion

		#region Constructors
		public OrderExportErrorCode() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""ExportErrorCode"" column=""`export_error_code`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int ExportErrorCode
		{
			get { return this.exportErrorCode; }
			set { this.exportErrorCode = value; }
		}

		[Property(Column="`export_error_description`")]
		public virtual string ExportErrorDescription
		{
			get { return this.exportErrorDescription; }
			set { this.exportErrorDescription = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(OrderExportErrorCode));
                return c;
            }
        }

        public static List<OrderExportErrorCode> GetOrderExportErrorCodeList(ICriteria criteria)
        {
            return (List<OrderExportErrorCode>)criteria.List<OrderExportErrorCode>();
        }

		public static OrderExportErrorCode GetOrderExportErrorCode(int exportErrorCode)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<OrderExportErrorCode>(exportErrorCode);
			}
		}

		public static List<OrderExportErrorCode> GetOrderExportErrorCodeList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(OrderExportErrorCode));
				return (List<OrderExportErrorCode>)c.List<OrderExportErrorCode>();
			}
		}

		public static List<OrderExportErrorCode> GetOrderExportErrorCodeList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(OrderExportErrorCode));
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

				return (List<OrderExportErrorCode>)c.List<OrderExportErrorCode>();
			}
		}

		public static List<OrderExportErrorCode> GetOrderExportErrorCodeList(string sortExpression)
		{
			return GetOrderExportErrorCodeList(sortExpression, -1, -1);
		}

		public static void InsertOrderExportErrorCode(OrderExportErrorCode obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateOrderExportErrorCode(OrderExportErrorCode obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteOrderExportErrorCode(OrderExportErrorCode obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static OrderExportErrorCode PopulateOrderExportErrorCode(IDataReader r)
		{
			OrderExportErrorCode obj = new OrderExportErrorCode();
			obj.ExportErrorCode = (int)r["export_error_code"];
			obj.ExportErrorDescription = (r["export_error_description"] == DBNull.Value) ? null : (string)r["export_error_description"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(OrderExportErrorCode));
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

			OrderExportErrorCode castObj = (OrderExportErrorCode)obj;
			return (castObj != null && this.exportErrorCode == castObj.ExportErrorCode);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.exportErrorCode.GetHashCode());
		}
		#endregion
	}
}
