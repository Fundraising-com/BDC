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
    [Class(Schema = "`dbo`", Table = "`order_payment`")]
    public partial class OrderPayment
    {
        #region Constants
        public const string OrderPaymentEntity = "OrderPayment";
        public const string OrderPaymentIdProperty = "OrderPaymentId";
        public const string OrderPaymentMethodIdProperty = "OrderPaymentMethodId";
        public const string OrderPaymentCollectionStatusIdProperty = "OrderPaymentCollectionStatusId";
        public const string OrderIdProperty = "OrderId";
        public const string NumberProperty = "Number";
        public const string EntryDateProperty = "EntryDate";
        public const string CashableDateProperty = "CashableDate";
        public const string AmountProperty = "Amount";
        public const string ComissionPaidProperty = "ComissionPaid";
        public const string ForeignOrderIdProperty = "ForeignOrderId";
        public const string CreditCardIdProperty = "CreditCardId";
        public const string CreateDateProperty = "CreateDate";
        public const string CreateUserIdProperty = "CreateUserId";
        public const string UpdateDateProperty = "UpdateDate";
        public const string UpdateUserIdProperty = "UpdateUserId";
        #endregion

        #region Fields
        protected int orderPaymentId = 0;
        protected int orderPaymentMethodId = 0;
        protected int? orderPaymentCollectionStatusId = null;
        protected int orderId = 0;
        protected int number = 0;
        protected DateTime entryDate = DateTime.Now;
        protected DateTime cashableDate = DateTime.Now;
        protected decimal amount = 0;
        protected bool comissionPaid = false;
        protected int? foreignOrderId = null;
        protected int? creditCardId = null;
        protected DateTime createDate = DateTime.Now;
        protected int createUserId = 0;
        protected DateTime? updateDate = null;
        protected int? updateUserId = null;
        #endregion

        #region Constructors
        public OrderPayment()
        {
        }
        #endregion

        #region Properties
        [RawXml(Content = @"
		<id name=""OrderPaymentId"" column=""`order_payment_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

        public virtual int OrderPaymentId
        {
            get { return this.orderPaymentId; }
            set { this.orderPaymentId = value; }
        }

        [Property(Column = "`order_payment_method_id`")]
        public virtual int OrderPaymentMethodId
        {
            get { return this.orderPaymentMethodId; }
            set { this.orderPaymentMethodId = value; }
        }

        [Property(Column = "`order_payment_collection_status_id`")]
        public virtual int? OrderPaymentCollectionStatusId
        {
            get { return this.orderPaymentCollectionStatusId; }
            set { this.orderPaymentCollectionStatusId = value; }
        }

        [Property(Column = "`order_id`")]
        public virtual int OrderId
        {
            get { return this.orderId; }
            set { this.orderId = value; }
        }

        [Property(Column = "`number`")]
        public virtual int Number
        {
            get { return this.number; }
            set { this.number = value; }
        }

        [Property(Column = "`entry_date`")]
        public virtual DateTime EntryDate
        {
            get { return this.entryDate; }
            set { this.entryDate = value; }
        }

        [Property(Column = "`cashable_date`")]
        public virtual DateTime CashableDate
        {
            get { return this.cashableDate; }
            set { this.cashableDate = value; }
        }

        [Property(Column = "`amount`")]
        public virtual decimal Amount
        {
            get { return this.amount; }
            set { this.amount = value; }
        }

        [Property(Column = "`comission_paid`")]
        public virtual bool ComissionPaid
        {
            get { return this.comissionPaid; }
            set { this.comissionPaid = value; }
        }

        [Property(Column = "`foreign_order_id`")]
        public virtual int? ForeignOrderId
        {
            get { return this.foreignOrderId; }
            set { this.foreignOrderId = value; }
        }

        [Property(Column = "`credit_card_id`")]
        public virtual int? CreditCardId
        {
            get { return this.creditCardId; }
            set { this.creditCardId = value; }
        }

        [Property(Column = "`create_date`")]
        public virtual DateTime CreateDate
        {
            get { return this.createDate; }
            set { this.createDate = value; }
        }

        [Property(Column = "`create_user_id`")]
        public virtual int CreateUserId
        {
            get { return this.createUserId; }
            set { this.createUserId = value; }
        }

        [Property(Column = "`update_date`")]
        public virtual DateTime? UpdateDate
        {
            get { return this.updateDate; }
            set { this.updateDate = value; }
        }

        [Property(Column = "`update_user_id`")]
        public virtual int? UpdateUserId
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
                ICriteria c = session.CreateCriteria(typeof(OrderPayment));
                return c;
            }
        }

        public static List<OrderPayment> GetOrderPaymentList(ICriteria criteria)
        {
            return (List<OrderPayment>)criteria.List<OrderPayment>();
        }

        public static OrderPayment GetOrderPayment(int orderPaymentId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                return session.Get<OrderPayment>(orderPaymentId);
            }
        }

        public static List<OrderPayment> GetOrderPaymentList()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(OrderPayment));
                return (List<OrderPayment>)c.List<OrderPayment>();
            }
        }

        public static List<OrderPayment> GetOrderPaymentList(string sortExpression, int maximumRows, int startRowIndex)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(OrderPayment));
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

                return (List<OrderPayment>)c.List<OrderPayment>();
            }
        }

        public static List<OrderPayment> GetOrderPaymentList(string sortExpression)
        {
            return GetOrderPaymentList(sortExpression, -1, -1);
        }

        public static void InsertOrderPayment(OrderPayment obj)
        {
            if (obj != null)
                obj.Insert();
        }

        public static void UpdateOrderPayment(OrderPayment obj)
        {
            if (obj != null)
                obj.Update();
        }

        public static void DeleteOrderPayment(OrderPayment obj)
        {
            if (obj != null)
                obj.Delete();
        }

        protected static OrderPayment PopulateOrderPayment(IDataReader r)
        {
            OrderPayment obj = new OrderPayment();
            obj.OrderPaymentId = (int)r["order_payment_id"];
            obj.OrderPaymentMethodId = (int)r["order_payment_method_id"];
            obj.OrderPaymentCollectionStatusId = (r["order_payment_collection_status_id"] == DBNull.Value) ? null : (int?)r["order_payment_collection_status_id"];
            obj.OrderId = (int)r["order_id"];
            obj.Number = (int)r["number"];
            obj.EntryDate = (DateTime)r["entry_date"];
            obj.CashableDate = (DateTime)r["cashable_date"];
            obj.Amount = (decimal)r["amount"];
            obj.ComissionPaid = (bool)r["comission_paid"];
            obj.ForeignOrderId = (r["foreign_order_id"] == DBNull.Value) ? null : (int?)r["foreign_order_id"];
            obj.CreditCardId = (r["credit_card_id"] == DBNull.Value) ? null : (int?)r["credit_card_id"];
            obj.CreateDate = (DateTime)r["create_date"];
            obj.CreateUserId = (int)r["create_user_id"];
            obj.UpdateDate = (r["update_date"] == DBNull.Value) ? null : (DateTime?)r["update_date"];
            obj.UpdateUserId = (r["update_user_id"] == DBNull.Value) ? null : (int?)r["update_user_id"];

            return obj;
        }

        public static int GetCount()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(OrderPayment));
                c.SetProjection(Projections.RowCount());
                return (int)c.UniqueResult();
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

            OrderPayment castObj = (OrderPayment)obj;
            return (castObj != null && this.orderPaymentId == castObj.OrderPaymentId);
        }

        /// <summary>
        /// Local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode()
        {
            return 29 * (1 + this.orderPaymentId.GetHashCode());
        }
        #endregion
    }
}
