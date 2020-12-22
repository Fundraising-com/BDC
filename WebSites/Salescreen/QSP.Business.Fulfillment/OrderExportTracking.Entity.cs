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
	[Class(Schema="`dbo`", Table="`Order_Export_Tracking`")]
	public partial class OrderExportTracking
	{
		#region Constants
        public const string OrderExportTrackingEntity = "OrderExportTracking";
		public const string EdsOrderIdProperty = "EdsOrderId";
		public const string ExportDateProperty = "ExportDate";
		public const string ExportDestinationProperty = "ExportDestination";
		public const string ExportOrderStatusIdProperty = "ExportOrderStatusId";
		public const string ExportErrorProperty = "ExportError";
		public const string ExportOrderTypeProperty = "ExportOrderType";
		public const string ExportStatusProperty = "ExportStatus";
		public const string ConfFileNameProperty = "ConfFileName";
		#endregion

		#region Fields
		protected int edsOrderId = 0;
		protected DateTime? exportDate = null;
		protected string exportDestination = "";
		protected int? exportOrderStatusId = null;
		protected int? exportError = null;
		protected string exportOrderType = "";
		protected int? exportStatus = null;
		protected string confFileName = null;
		#endregion

		#region Constructors
		public OrderExportTracking() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<composite-id>
			<key-property name=""EdsOrderId"" column=""`eds_order_id`"" />
			<key-property name=""ExportOrderType"" column=""`export_order_type`"" />
		</composite-id>")]

		public virtual int EdsOrderId
		{
			get { return this.edsOrderId; }
			set { this.edsOrderId = value; }
		}

		[Property(Column="`export_date`")]
		public virtual DateTime? ExportDate
		{
			get { return this.exportDate; }
			set { this.exportDate = value; }
		}

		[Property(Column="`export_destination`")]
		public virtual string ExportDestination
		{
			get { return this.exportDestination; }
			set { this.exportDestination = value; }
		}

		[Property(Column="`export_order_status_id`")]
		public virtual int? ExportOrderStatusId
		{
			get { return this.exportOrderStatusId; }
			set { this.exportOrderStatusId = value; }
		}

		[Property(Column="`export_error`")]
		public virtual int? ExportError
		{
			get { return this.exportError; }
			set { this.exportError = value; }
		}

		public virtual string ExportOrderType
		{
			get { return this.exportOrderType; }
			set { this.exportOrderType = value; }
		}

		[Property(Column="`export_status`")]
		public virtual int? ExportStatus
		{
			get { return this.exportStatus; }
			set { this.exportStatus = value; }
		}

		[Property(Column="`conf_file_name`")]
		public virtual string ConfFileName
		{
			get { return this.confFileName; }
			set { this.confFileName = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(OrderExportTracking));
                return c;
            }
        }

        public static List<OrderExportTracking> GetOrderExportTrackingList(ICriteria criteria)
        {
            return (List<OrderExportTracking>)criteria.List<OrderExportTracking>();
        }

		public static OrderExportTracking GetOrderExportTracking(int edsOrderId, string exportOrderType)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(OrderExportTracking));
				c.Add(Expression.Eq(EdsOrderIdProperty, edsOrderId));
				c.Add(Expression.Eq(ExportOrderTypeProperty, exportOrderType));
				return c.UniqueResult<OrderExportTracking>();
			}
		}

		public static List<OrderExportTracking> GetOrderExportTrackingList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(OrderExportTracking));
				return (List<OrderExportTracking>)c.List<OrderExportTracking>();
			}
		}

		public static List<OrderExportTracking> GetOrderExportTrackingList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(OrderExportTracking));
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

				return (List<OrderExportTracking>)c.List<OrderExportTracking>();
			}
		}

		public static List<OrderExportTracking> GetOrderExportTrackingList(string sortExpression)
		{
			return GetOrderExportTrackingList(sortExpression, -1, -1);
		}

		public static void InsertOrderExportTracking(OrderExportTracking obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateOrderExportTracking(OrderExportTracking obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteOrderExportTracking(OrderExportTracking obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static OrderExportTracking PopulateOrderExportTracking(IDataReader r)
		{
			OrderExportTracking obj = new OrderExportTracking();
			obj.EdsOrderId = (int)r["eds_order_id"];
			obj.ExportDate = (r["export_date"] == DBNull.Value) ? null : (DateTime?)r["export_date"];
			obj.ExportDestination = (string)r["export_destination"];
			obj.ExportOrderStatusId = (r["export_order_status_id"] == DBNull.Value) ? null : (int?)r["export_order_status_id"];
			obj.ExportError = (r["export_error"] == DBNull.Value) ? null : (int?)r["export_error"];
			obj.ExportOrderType = (string)r["export_order_type"];
			obj.ExportStatus = (r["export_status"] == DBNull.Value) ? null : (int?)r["export_status"];
			obj.ConfFileName = (r["conf_file_name"] == DBNull.Value) ? null : (string)r["conf_file_name"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(OrderExportTracking));
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

			OrderExportTracking castObj = (OrderExportTracking)obj;
			return (castObj != null && this.edsOrderId == castObj.EdsOrderId && this.exportOrderType == castObj.ExportOrderType);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.edsOrderId.GetHashCode() + this.exportOrderType.GetHashCode());
		}
		#endregion
	}
}
