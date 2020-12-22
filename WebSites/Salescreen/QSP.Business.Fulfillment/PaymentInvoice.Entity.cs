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
	[Class(Schema="`dbo`", Table="`payment_invoice`")]
	public partial class PaymentInvoice
	{
		#region Constants
        public const string PaymentInvoiceEntity = "PaymentInvoice";
		public const string PaymentInvoiceIdProperty = "PaymentInvoiceId";
		public const string PaymentIdProperty = "PaymentId";
		public const string InvoiceIdProperty = "InvoiceId";
		public const string AmountProperty = "Amount";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		#endregion

		#region Fields
		protected int paymentInvoiceId = 0;
		protected int paymentId = 0;
		protected int invoiceId = 0;
		protected decimal? amount = 0;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = DateTime.Now;
		protected int? updateUserId = null;
		#endregion

		#region Constructors
		public PaymentInvoice() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""PaymentInvoiceId"" column=""`payment_invoice_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int PaymentInvoiceId
		{
			get { return this.paymentInvoiceId; }
			set { this.paymentInvoiceId = value; }
		}

		[Property(Column="`payment_id`")]
		public virtual int PaymentId
		{
			get { return this.paymentId; }
			set { this.paymentId = value; }
		}

		[Property(Column="`invoice_id`")]
		public virtual int InvoiceId
		{
			get { return this.invoiceId; }
			set { this.invoiceId = value; }
		}

		[Property(Column="`amount`")]
		public virtual decimal? Amount
		{
			get { return this.amount; }
			set { this.amount = value; }
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
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(PaymentInvoice));
                return c;
            }
        }

        public static List<PaymentInvoice> GetPaymentInvoiceList(ICriteria criteria)
        {
            return (List<PaymentInvoice>)criteria.List<PaymentInvoice>();
        }

		public static PaymentInvoice GetPaymentInvoice(int paymentInvoiceId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<PaymentInvoice>(paymentInvoiceId);
			}
		}

		public static List<PaymentInvoice> GetPaymentInvoiceList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PaymentInvoice));
				return (List<PaymentInvoice>)c.List<PaymentInvoice>();
			}
		}

		public static List<PaymentInvoice> GetPaymentInvoiceList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(PaymentInvoice));
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

				return (List<PaymentInvoice>)c.List<PaymentInvoice>();
			}
		}

		public static List<PaymentInvoice> GetPaymentInvoiceList(string sortExpression)
		{
			return GetPaymentInvoiceList(sortExpression, -1, -1);
		}

		public static void InsertPaymentInvoice(PaymentInvoice obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdatePaymentInvoice(PaymentInvoice obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeletePaymentInvoice(PaymentInvoice obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static PaymentInvoice PopulatePaymentInvoice(IDataReader r)
		{
			PaymentInvoice obj = new PaymentInvoice();
			obj.PaymentInvoiceId = (int)r["payment_invoice_id"];
			obj.PaymentId = (int)r["payment_id"];
			obj.InvoiceId = (int)r["invoice_id"];
			obj.Amount = (r["amount"] == DBNull.Value) ? null : (int?)r["amount"];
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
				ICriteria c = session.CreateCriteria(typeof(PaymentInvoice));
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

			PaymentInvoice castObj = (PaymentInvoice)obj;
			return (castObj != null && this.paymentInvoiceId == castObj.PaymentInvoiceId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.paymentInvoiceId.GetHashCode());
		}
		#endregion
	}
}
