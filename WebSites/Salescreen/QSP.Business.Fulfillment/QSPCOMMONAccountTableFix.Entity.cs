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
	[Class(Schema="`dbo`", Table="`QSPCOMMON_account_table_fix`")]
	public partial class QSPCOMMONAccountTableFix
	{
		#region Constants
        public const string QSPCOMMONAccountTableFixEntity = "QSPCOMMONAccountTableFix";
		public const string AccountIdProperty = "AccountId";
		public const string AccountNameProperty = "AccountName";
		public const string FmIdProperty = "FmId";
		public const string FulfAccountIdProperty = "FulfAccountId";
		public const string DtsFlagpoleInstanceProperty = "DtsFlagpoleInstance";
		public const string DtsCAccountIdProperty = "DtsCAccountId";
		public const string OrganizationIdProperty = "OrganizationId";
		public const string CacctIdProperty = "CacctId";
		public const string CacctNameProperty = "CacctName";
		public const string CacctFMIDProperty = "CacctFMID";
		public const string CacctProgramTypeProperty = "CacctProgramType";
		public const string CacctFlagpoleInstanceProperty = "CacctFlagpoleInstance";
		public const string QspcommonOrganizationIdProperty = "QspcommonOrganizationId";
		public const string CreateDateProperty = "CreateDate";
		#endregion

		#region Fields
		protected int accountId = 0;
		protected string accountName = null;
		protected string fmId = null;
		protected int? fulfAccountId = null;
		protected int? dtsFlagpoleInstance = null;
		protected int? dtsCAccountId = null;
		protected int? organizationId = null;
		protected int? cacctId = null;
		protected string cacctName = null;
		protected string cacctFMID = null;
		protected byte? cacctProgramType = null;
		protected int? cacctFlagpoleInstance = null;
		protected int? qspcommonOrganizationId = null;
		protected DateTime createDate = DateTime.Now;
		#endregion

		#region Constructors
		public QSPCOMMONAccountTableFix() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<composite-id>
			<key-property name=""AccountId"" column=""`account_id`"" />
			<key-property name=""CreateDate"" column=""`create_date`"" />
		</composite-id>")]

		public virtual int AccountId
		{
			get { return this.accountId; }
			set { this.accountId = value; }
		}

		[Property(Column="`account_name`")]
		public virtual string AccountName
		{
			get { return this.accountName; }
			set { this.accountName = value; }
		}

		[Property(Column="`fm_id`")]
		public virtual string FmId
		{
			get { return this.fmId; }
			set { this.fmId = value; }
		}

		[Property(Column="`fulf_account_id`")]
		public virtual int? FulfAccountId
		{
			get { return this.fulfAccountId; }
			set { this.fulfAccountId = value; }
		}

		[Property(Column="`dts_FlagpoleInstance`")]
		public virtual int? DtsFlagpoleInstance
		{
			get { return this.dtsFlagpoleInstance; }
			set { this.dtsFlagpoleInstance = value; }
		}

		[Property(Column="`dts_CAccountId`")]
		public virtual int? DtsCAccountId
		{
			get { return this.dtsCAccountId; }
			set { this.dtsCAccountId = value; }
		}

		[Property(Column="`organization_id`")]
		public virtual int? OrganizationId
		{
			get { return this.organizationId; }
			set { this.organizationId = value; }
		}

		[Property(Column="`cacct_Id`")]
		public virtual int? CacctId
		{
			get { return this.cacctId; }
			set { this.cacctId = value; }
		}

		[Property(Column="`cacct_Name`")]
		public virtual string CacctName
		{
			get { return this.cacctName; }
			set { this.cacctName = value; }
		}

		[Property(Column="`cacct_FMID`")]
		public virtual string CacctFMID
		{
			get { return this.cacctFMID; }
			set { this.cacctFMID = value; }
		}

		[Property(Column="`cacct_ProgramType`")]
		public virtual byte? CacctProgramType
		{
			get { return this.cacctProgramType; }
			set { this.cacctProgramType = value; }
		}

		[Property(Column="`cacct_FlagpoleInstance`")]
		public virtual int? CacctFlagpoleInstance
		{
			get { return this.cacctFlagpoleInstance; }
			set { this.cacctFlagpoleInstance = value; }
		}

		[Property(Column="`qspcommon_organization_id`")]
		public virtual int? QspcommonOrganizationId
		{
			get { return this.qspcommonOrganizationId; }
			set { this.qspcommonOrganizationId = value; }
		}

		public virtual DateTime CreateDate
		{
			get { return this.createDate; }
			set { this.createDate = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(QSPCOMMONAccountTableFix));
                return c;
            }
        }

        public static List<QSPCOMMONAccountTableFix> GetQSPCOMMONAccountTableFixList(ICriteria criteria)
        {
            return (List<QSPCOMMONAccountTableFix>)criteria.List<QSPCOMMONAccountTableFix>();
        }

		public static QSPCOMMONAccountTableFix GetQSPCOMMONAccountTableFix(int accountId, DateTime createDate)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QSPCOMMONAccountTableFix));
				c.Add(Expression.Eq(AccountIdProperty, accountId));
				c.Add(Expression.Eq(CreateDateProperty, createDate));
				return c.UniqueResult<QSPCOMMONAccountTableFix>();
			}
		}

		public static List<QSPCOMMONAccountTableFix> GetQSPCOMMONAccountTableFixList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QSPCOMMONAccountTableFix));
				return (List<QSPCOMMONAccountTableFix>)c.List<QSPCOMMONAccountTableFix>();
			}
		}

		public static List<QSPCOMMONAccountTableFix> GetQSPCOMMONAccountTableFixList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QSPCOMMONAccountTableFix));
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

				return (List<QSPCOMMONAccountTableFix>)c.List<QSPCOMMONAccountTableFix>();
			}
		}

		public static List<QSPCOMMONAccountTableFix> GetQSPCOMMONAccountTableFixList(string sortExpression)
		{
			return GetQSPCOMMONAccountTableFixList(sortExpression, -1, -1);
		}

		public static void InsertQSPCOMMONAccountTableFix(QSPCOMMONAccountTableFix obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateQSPCOMMONAccountTableFix(QSPCOMMONAccountTableFix obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteQSPCOMMONAccountTableFix(QSPCOMMONAccountTableFix obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static QSPCOMMONAccountTableFix PopulateQSPCOMMONAccountTableFix(IDataReader r)
		{
			QSPCOMMONAccountTableFix obj = new QSPCOMMONAccountTableFix();
			obj.AccountId = (int)r["account_id"];
			obj.AccountName = (r["account_name"] == DBNull.Value) ? null : (string)r["account_name"];
			obj.FmId = (r["fm_id"] == DBNull.Value) ? null : (string)r["fm_id"];
			obj.FulfAccountId = (r["fulf_account_id"] == DBNull.Value) ? null : (int?)r["fulf_account_id"];
			obj.DtsFlagpoleInstance = (r["dts_FlagpoleInstance"] == DBNull.Value) ? null : (int?)r["dts_FlagpoleInstance"];
			obj.DtsCAccountId = (r["dts_CAccountId"] == DBNull.Value) ? null : (int?)r["dts_CAccountId"];
			obj.OrganizationId = (r["organization_id"] == DBNull.Value) ? null : (int?)r["organization_id"];
			obj.CacctId = (r["cacct_Id"] == DBNull.Value) ? null : (int?)r["cacct_Id"];
			obj.CacctName = (r["cacct_Name"] == DBNull.Value) ? null : (string)r["cacct_Name"];
			obj.CacctFMID = (r["cacct_FMID"] == DBNull.Value) ? null : (string)r["cacct_FMID"];
			obj.CacctProgramType = (r["cacct_ProgramType"] == DBNull.Value) ? null : (byte?)r["cacct_ProgramType"];
			obj.CacctFlagpoleInstance = (r["cacct_FlagpoleInstance"] == DBNull.Value) ? null : (int?)r["cacct_FlagpoleInstance"];
			obj.QspcommonOrganizationId = (r["qspcommon_organization_id"] == DBNull.Value) ? null : (int?)r["qspcommon_organization_id"];
			obj.CreateDate = (DateTime)r["create_date"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(QSPCOMMONAccountTableFix));
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

			QSPCOMMONAccountTableFix castObj = (QSPCOMMONAccountTableFix)obj;
			return (castObj != null && this.accountId == castObj.AccountId && this.createDate == castObj.CreateDate);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.accountId.GetHashCode() + this.createDate.GetHashCode());
		}
		#endregion
	}
}
