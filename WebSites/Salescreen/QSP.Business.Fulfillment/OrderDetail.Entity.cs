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
	[Class(Schema="`dbo`", Table="`order_detail`")]
	public partial class OrderDetail
	{
		#region Constants
        public const string OrderDetailEntity = "OrderDetail";
		public const string OrderDetailIdProperty = "OrderDetailId";
		public const string OrderIdProperty = "OrderId";
		public const string CatalogItemDetailIdProperty = "CatalogItemDetailId";
		public const string SourceIdProperty = "SourceId";
		public const string OrderStatusIdProperty = "OrderStatusId";
		public const string StatusReasonIdProperty = "StatusReasonId";
		public const string ShipmentGroupIdProperty = "ShipmentGroupId";
		public const string QuantityProperty = "Quantity";
		public const string AdjustmentQuantityProperty = "AdjustmentQuantity";
		public const string PriceProperty = "Price";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		public const string SyncOeOrdProperty = "SyncOeOrd";
		public const string SyncOecoupProperty = "SyncOecoup";
		public const string SyncOeitemProperty = "SyncOeitem";
		public const string PersonalizationIdProperty = "PersonalizationId";
        public const string RenewalProperty = "Renewal";
        public const string PriceAProperty = "PriceA";
		#endregion

		#region Fields
		protected int orderDetailId = 0;
		protected int orderId = 0;
		protected int catalogItemDetailId = 0;
		protected int? sourceId = null;
		protected int orderStatusId = 0;
		protected int? statusReasonId = null;
		protected int shipmentGroupId = 0;
		protected int quantity = 0;
		protected int adjustmentQuantity = 0;
		protected decimal price = 0;
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = DateTime.Now;
		protected int? updateUserId = null;
		protected int? syncOeOrd = null;
		protected string syncOecoup = null;
		protected int? syncOeitem = null;
		protected int? personalizationId = null;
        protected bool? renewal = null;
        protected decimal? priceA = null;
		#endregion

		#region Constructors
		public OrderDetail() 
		{
		}
		#endregion

		#region Properties

		[RawXml(Content=@"
		<id name=""OrderDetailId"" column=""`order_detail_id`"">
			<generator class=""native"">
			</generator>
		</id>")]
		public virtual int OrderDetailId
		{
			get { return this.orderDetailId; }
			set { this.orderDetailId = value; }
		}

		[Property(Column="`order_id`")]
		public virtual int OrderId
		{
			get { return this.orderId; }
			set { this.orderId = value; }
		}

		[Property(Column="`catalog_item_detail_id`")]
		public virtual int CatalogItemDetailId
		{
			get { return this.catalogItemDetailId; }
			set { this.catalogItemDetailId = value; }
		}

		[Property(Column="`source_id`")]
		public virtual int? SourceId
		{
			get { return this.sourceId; }
			set { this.sourceId = value; }
		}

		[Property(Column="`order_status_id`")]
		public virtual int OrderStatusId
		{
			get { return this.orderStatusId; }
			set { this.orderStatusId = value; }
		}

		[Property(Column="`status_reason_id`")]
		public virtual int? StatusReasonId
		{
			get { return this.statusReasonId; }
			set { this.statusReasonId = value; }
		}

		[Property(Column="`shipment_group_id`")]
		public virtual int ShipmentGroupId
		{
			get { return this.shipmentGroupId; }
			set { this.shipmentGroupId = value; }
		}

		[Property(Column="`quantity`")]
		public virtual int Quantity
		{
			get { return this.quantity; }
			set { this.quantity = value; }
		}

		[Property(Column="`adjustment_quantity`")]
		public virtual int AdjustmentQuantity
		{
			get { return this.adjustmentQuantity; }
			set { this.adjustmentQuantity = value; }
		}

		[Property(Column="`price`")]
		public virtual decimal Price
		{
			get { return this.price; }
			set { this.price = value; }
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
		public virtual DateTime? UpdateDate
		{
			get { return this.updateDate; }
			set { this.updateDate = value; }
		}

		[Property(Column="`update_user_id`")]
		public virtual int? UpdateUserId
		{
			get { return this.updateUserId; }
			set { this.updateUserId = value; }
		}

		[Property(Column="`sync_oe#ord`")]
		public virtual int? SyncOeOrd
		{
			get { return this.syncOeOrd; }
			set { this.syncOeOrd = value; }
		}

		[Property(Column="`sync_oecoup`")]
		public virtual string SyncOecoup
		{
			get { return this.syncOecoup; }
			set { this.syncOecoup = value; }
		}

		[Property(Column="`sync_oeitem`")]
		public virtual int? SyncOeitem
		{
			get { return this.syncOeitem; }
			set { this.syncOeitem = value; }
		}

		[Property(Column="`personalization_id`")]
		public virtual int? PersonalizationId
		{
			get { return this.personalizationId; }
			set { this.personalizationId = value; }
		}

        [Property(Column = "`renewal`")]
        public virtual bool? Renewal
        {
            get { return this.renewal; }
            set { this.renewal = value; }
        }

        [Property(Column = "`pricea`")]
        public virtual decimal? PriceA
        {
            get { return this.priceA; }
            set { this.priceA = value; }
        }
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            ISession session = SqlSessionManager.OpenSession();
            ICriteria c = session.CreateCriteria(typeof(OrderDetail));
            return c;
          
        }

        public static List<OrderDetail> GetOrderDetailList(ICriteria criteria)
        {
            return (List<OrderDetail>)criteria.List<OrderDetail>();
        }

		public static OrderDetail GetOrderDetail(int orderDetailId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<OrderDetail>(orderDetailId);
			}
		}

		public static List<OrderDetail> GetOrderDetailList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(OrderDetail));
				return (List<OrderDetail>)c.List<OrderDetail>();
			}
		}

		public static List<OrderDetail> GetOrderDetailList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(OrderDetail));
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

				return (List<OrderDetail>)c.List<OrderDetail>();
			}
		}

		public static List<OrderDetail> GetOrderDetailList(string sortExpression)
		{
			return GetOrderDetailList(sortExpression, -1, -1);
		}

		public static void InsertOrderDetail(OrderDetail obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateOrderDetail(OrderDetail obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteOrderDetail(OrderDetail obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static OrderDetail PopulateOrderDetail(IDataReader r)
		{
			OrderDetail obj = new OrderDetail();
			obj.OrderDetailId = (int)r["order_detail_id"];
			obj.OrderId = (int)r["order_id"];
			obj.CatalogItemDetailId = (int)r["catalog_item_detail_id"];
			obj.SourceId = (r["source_id"] == DBNull.Value) ? null : (int?)r["source_id"];
			obj.OrderStatusId = (int)r["order_status_id"];
			obj.StatusReasonId = (r["status_reason_id"] == DBNull.Value) ? null : (int?)r["status_reason_id"];
			obj.ShipmentGroupId = (int)r["shipment_group_id"];
			obj.Quantity = (int)r["quantity"];
			obj.AdjustmentQuantity = (int)r["adjustment_quantity"];
			obj.Price = (decimal)r["price"];
			obj.Deleted = (bool)r["deleted"];
			obj.CreateDate = (DateTime)r["create_date"];
			obj.CreateUserId = (int)r["create_user_id"];
			obj.UpdateDate = (r["update_date"] == DBNull.Value) ? null : (DateTime?)r["update_date"];
			obj.UpdateUserId = (r["update_user_id"] == DBNull.Value) ? null : (int?)r["update_user_id"];
			obj.SyncOeOrd = (r["sync_oe#ord"] == DBNull.Value) ? null : (int?)r["sync_oe#ord"];
			obj.SyncOecoup = (r["sync_oecoup"] == DBNull.Value) ? null : (string)r["sync_oecoup"];
			obj.SyncOeitem = (r["sync_oeitem"] == DBNull.Value) ? null : (int?)r["sync_oeitem"];
			obj.PersonalizationId = (r["personalization_id"] == DBNull.Value) ? null : (int?)r["personalization_id"];
            obj.Renewal = (r["renewal"] == DBNull.Value) ? null : (bool?)r["renewal"];
            obj.PriceA = (r["pricea"] == DBNull.Value) ? null : (decimal?)r["pricea"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(OrderDetail));
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

			OrderDetail castObj = (OrderDetail)obj;
			return (castObj != null && this.orderDetailId == castObj.OrderDetailId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.orderDetailId.GetHashCode());
		}
		#endregion
	}
}
