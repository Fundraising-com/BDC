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
	[Class(Schema="`dbo`", Table="`QSPForm_payment_assignment_type`")]
	public partial class QSPFormPaymentAssignmentType
	{
		#region Constants
        public const string QSPFormPaymentAssignmentTypeEntity = "QSPFormPaymentAssignmentType";
		public const string PaymentAssignmentTypeIdProperty = "PaymentAssignmentTypeId";
		public const string FulfPaymentAssignmentTypeNameProperty = "FulfPaymentAssignmentTypeName";
		#endregion

		#region Fields
		protected int paymentAssignmentTypeId = 0;
		protected string fulfPaymentAssignmentTypeName = "";
		#endregion

		#region Constructors
		public QSPFormPaymentAssignmentType() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""PaymentAssignmentTypeId"" column=""`payment_assignment_type_id`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int PaymentAssignmentTypeId
		{
			get { return this.paymentAssignmentTypeId; }
			set { this.paymentAssignmentTypeId = value; }
		}

		[Property(Column="`fulf_payment_assignment_type_name`")]
		public virtual string FulfPaymentAssignmentTypeName
		{
			get { return this.fulfPaymentAssignmentTypeName; }
			set { this.fulfPaymentAssignmentTypeName = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(QSPFormPaymentAssignmentType));
                return c;
            }
        }

        public static List<QSPFormPaymentAssignmentType> GetQSPFormPaymentAssignmentTypeList(ICriteria criteria)
        {
            return (List<QSPFormPaymentAssignmentType>)criteria.List<QSPFormPaymentAssignmentType>();
        }

		public static QSPFormPaymentAssignmentType GetQSPFormPaymentAssignmentType(int paymentAssignmentTypeId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<QSPFormPaymentAssignmentType>(paymentAssignmentTypeId);
			}
		}

		public static List<QSPFormPaymentAssignmentType> GetQSPFormPaymentAssignmentTypeList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QSPFormPaymentAssignmentType));
				return (List<QSPFormPaymentAssignmentType>)c.List<QSPFormPaymentAssignmentType>();
			}
		}

		public static List<QSPFormPaymentAssignmentType> GetQSPFormPaymentAssignmentTypeList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QSPFormPaymentAssignmentType));
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

				return (List<QSPFormPaymentAssignmentType>)c.List<QSPFormPaymentAssignmentType>();
			}
		}

		public static List<QSPFormPaymentAssignmentType> GetQSPFormPaymentAssignmentTypeList(string sortExpression)
		{
			return GetQSPFormPaymentAssignmentTypeList(sortExpression, -1, -1);
		}

		public static void InsertQSPFormPaymentAssignmentType(QSPFormPaymentAssignmentType obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateQSPFormPaymentAssignmentType(QSPFormPaymentAssignmentType obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteQSPFormPaymentAssignmentType(QSPFormPaymentAssignmentType obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static QSPFormPaymentAssignmentType PopulateQSPFormPaymentAssignmentType(IDataReader r)
		{
			QSPFormPaymentAssignmentType obj = new QSPFormPaymentAssignmentType();
			obj.PaymentAssignmentTypeId = (int)r["payment_assignment_type_id"];
			obj.FulfPaymentAssignmentTypeName = (string)r["fulf_payment_assignment_type_name"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QSPFormPaymentAssignmentType));
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

			QSPFormPaymentAssignmentType castObj = (QSPFormPaymentAssignmentType)obj;
			return (castObj != null && this.paymentAssignmentTypeId == castObj.PaymentAssignmentTypeId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.paymentAssignmentTypeId.GetHashCode());
		}
		#endregion
	}
}
