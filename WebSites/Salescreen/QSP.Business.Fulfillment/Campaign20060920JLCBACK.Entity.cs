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
	[Class(Schema="`dbo`", Table="`campaign_2006_09_20_JLC_BACK`")]
	public partial class Campaign20060920JLCBACK
	{
		#region Constants
        public const string Campaign20060920JLCBACKEntity = "Campaign20060920JLCBACK";
		public const string CampaignIdProperty = "CampaignId";
		public const string AccountIdProperty = "AccountId";
		public const string FulfCampaignIdProperty = "FulfCampaignId";
		public const string ProgramTypeIdProperty = "ProgramTypeId";
		public const string WarehouseIdProperty = "WarehouseId";
		public const string CampaignNameProperty = "CampaignName";
		public const string FmIdProperty = "FmId";
		public const string TaxExemptionNumberProperty = "TaxExemptionNumber";
		public const string TaxExemptionExpirationDateProperty = "TaxExemptionExpirationDate";
		public const string StartDateProperty = "StartDate";
		public const string EndDateProperty = "EndDate";
		public const string FiscalYearProperty = "FiscalYear";
		public const string EnrollmentProperty = "Enrollment";
		public const string GoalEstimatedGrossProperty = "GoalEstimatedGross";
		public const string ARORBLProperty = "ARORBL";
		public const string CommentsProperty = "Comments";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		public const string DtsCAccountIdProperty = "DtsCAccountId";
		public const string DtsCCAInstanceProperty = "DtsCCAInstance";
		public const string TradeClassIdProperty = "TradeClassId";
		public const string FormIdProperty = "FormId";
		#endregion

		#region Fields
		protected int campaignId = 0;
		protected int accountId = 0;
		protected int? fulfCampaignId = null;
		protected int programTypeId = 0;
		protected int? warehouseId = null;
		protected string campaignName = "";
		protected string fmId = null;
		protected string taxExemptionNumber = null;
		protected DateTime? taxExemptionExpirationDate = null;
		protected DateTime startDate = DateTime.Now;
		protected DateTime endDate = DateTime.Now;
		protected int fiscalYear = 0;
		protected int enrollment = 0;
		protected decimal? goalEstimatedGross = null;
		protected string aRORBL = null;
		protected string comments = null;
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = null;
		protected int? updateUserId = null;
		protected int? dtsCAccountId = null;
		protected int? dtsCCAInstance = null;
		protected int? tradeClassId = null;
		protected int? formId = null;
		#endregion

		#region Constructors
		public Campaign20060920JLCBACK() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""CampaignId"" column=""`campaign_id`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int CampaignId
		{
			get { return this.campaignId; }
			set { this.campaignId = value; }
		}

		[Property(Column="`account_id`")]
		public virtual int AccountId
		{
			get { return this.accountId; }
			set { this.accountId = value; }
		}

		[Property(Column="`fulf_campaign_id`")]
		public virtual int? FulfCampaignId
		{
			get { return this.fulfCampaignId; }
			set { this.fulfCampaignId = value; }
		}

		[Property(Column="`program_type_id`")]
		public virtual int ProgramTypeId
		{
			get { return this.programTypeId; }
			set { this.programTypeId = value; }
		}

		[Property(Column="`warehouse_id`")]
		public virtual int? WarehouseId
		{
			get { return this.warehouseId; }
			set { this.warehouseId = value; }
		}

		[Property(Column="`campaign_name`")]
		public virtual string CampaignName
		{
			get { return this.campaignName; }
			set { this.campaignName = value; }
		}

		[Property(Column="`fm_id`")]
		public virtual string FmId
		{
			get { return this.fmId; }
			set { this.fmId = value; }
		}

		[Property(Column="`tax_exemption_number`")]
		public virtual string TaxExemptionNumber
		{
			get { return this.taxExemptionNumber; }
			set { this.taxExemptionNumber = value; }
		}

		[Property(Column="`tax_exemption_expiration_date`")]
		public virtual DateTime? TaxExemptionExpirationDate
		{
			get { return this.taxExemptionExpirationDate; }
			set { this.taxExemptionExpirationDate = value; }
		}

		[Property(Column="`start_date`")]
		public virtual DateTime StartDate
		{
			get { return this.startDate; }
			set { this.startDate = value; }
		}

		[Property(Column="`end_date`")]
		public virtual DateTime EndDate
		{
			get { return this.endDate; }
			set { this.endDate = value; }
		}

		[Property(Column="`fiscal_year`")]
		public virtual int FiscalYear
		{
			get { return this.fiscalYear; }
			set { this.fiscalYear = value; }
		}

		[Property(Column="`enrollment`")]
		public virtual int Enrollment
		{
			get { return this.enrollment; }
			set { this.enrollment = value; }
		}

		[Property(Column="`goal_estimated_gross`")]
		public virtual decimal? GoalEstimatedGross
		{
			get { return this.goalEstimatedGross; }
			set { this.goalEstimatedGross = value; }
		}

		[Property(Column="`ARORBL`")]
		public virtual string ARORBL
		{
			get { return this.aRORBL; }
			set { this.aRORBL = value; }
		}

		[Property(Column="`comments`")]
		public virtual string Comments
		{
			get { return this.comments; }
			set { this.comments = value; }
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

		[Property(Column="`dts_CAccountId`")]
		public virtual int? DtsCAccountId
		{
			get { return this.dtsCAccountId; }
			set { this.dtsCAccountId = value; }
		}

		[Property(Column="`dts_CCAInstance`")]
		public virtual int? DtsCCAInstance
		{
			get { return this.dtsCCAInstance; }
			set { this.dtsCCAInstance = value; }
		}

		[Property(Column="`trade_class_id`")]
		public virtual int? TradeClassId
		{
			get { return this.tradeClassId; }
			set { this.tradeClassId = value; }
		}

		[Property(Column="`form_id`")]
		public virtual int? FormId
		{
			get { return this.formId; }
			set { this.formId = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(Campaign20060920JLCBACK));
                return c;
            }
        }

        public static List<Campaign20060920JLCBACK> GetCampaign20060920JLCBACKList(ICriteria criteria)
        {
            return (List<Campaign20060920JLCBACK>)criteria.List<Campaign20060920JLCBACK>();
        }

		public static Campaign20060920JLCBACK GetCampaign20060920JLCBACK(int campaignId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<Campaign20060920JLCBACK>(campaignId);
			}
		}

		public static List<Campaign20060920JLCBACK> GetCampaign20060920JLCBACKList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Campaign20060920JLCBACK));
				return (List<Campaign20060920JLCBACK>)c.List<Campaign20060920JLCBACK>();
			}
		}

		public static List<Campaign20060920JLCBACK> GetCampaign20060920JLCBACKList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Campaign20060920JLCBACK));
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

				return (List<Campaign20060920JLCBACK>)c.List<Campaign20060920JLCBACK>();
			}
		}

		public static List<Campaign20060920JLCBACK> GetCampaign20060920JLCBACKList(string sortExpression)
		{
			return GetCampaign20060920JLCBACKList(sortExpression, -1, -1);
		}

		public static void InsertCampaign20060920JLCBACK(Campaign20060920JLCBACK obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateCampaign20060920JLCBACK(Campaign20060920JLCBACK obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteCampaign20060920JLCBACK(Campaign20060920JLCBACK obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static Campaign20060920JLCBACK PopulateCampaign20060920JLCBACK(IDataReader r)
		{
			Campaign20060920JLCBACK obj = new Campaign20060920JLCBACK();
			obj.CampaignId = (int)r["campaign_id"];
			obj.AccountId = (int)r["account_id"];
			obj.FulfCampaignId = (r["fulf_campaign_id"] == DBNull.Value) ? null : (int?)r["fulf_campaign_id"];
			obj.ProgramTypeId = (int)r["program_type_id"];
			obj.WarehouseId = (r["warehouse_id"] == DBNull.Value) ? null : (int?)r["warehouse_id"];
			obj.CampaignName = (string)r["campaign_name"];
			obj.FmId = (r["fm_id"] == DBNull.Value) ? null : (string)r["fm_id"];
			obj.TaxExemptionNumber = (r["tax_exemption_number"] == DBNull.Value) ? null : (string)r["tax_exemption_number"];
			obj.TaxExemptionExpirationDate = (r["tax_exemption_expiration_date"] == DBNull.Value) ? null : (DateTime?)r["tax_exemption_expiration_date"];
			obj.StartDate = (DateTime)r["start_date"];
			obj.EndDate = (DateTime)r["end_date"];
			obj.FiscalYear = (int)r["fiscal_year"];
			obj.Enrollment = (int)r["enrollment"];
			obj.GoalEstimatedGross = (r["goal_estimated_gross"] == DBNull.Value) ? null : (decimal?)r["goal_estimated_gross"];
			obj.ARORBL = (r["ARORBL"] == DBNull.Value) ? null : (string)r["ARORBL"];
			obj.Comments = (r["comments"] == DBNull.Value) ? null : (string)r["comments"];
			obj.Deleted = (bool)r["deleted"];
			obj.CreateDate = (DateTime)r["create_date"];
			obj.CreateUserId = (int)r["create_user_id"];
			obj.UpdateDate = (r["update_date"] == DBNull.Value) ? null : (DateTime?)r["update_date"];
			obj.UpdateUserId = (r["update_user_id"] == DBNull.Value) ? null : (int?)r["update_user_id"];
			obj.DtsCAccountId = (r["dts_CAccountId"] == DBNull.Value) ? null : (int?)r["dts_CAccountId"];
			obj.DtsCCAInstance = (r["dts_CCAInstance"] == DBNull.Value) ? null : (int?)r["dts_CCAInstance"];
			obj.TradeClassId = (r["trade_class_id"] == DBNull.Value) ? null : (int?)r["trade_class_id"];
			obj.FormId = (r["form_id"] == DBNull.Value) ? null : (int?)r["form_id"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(Campaign20060920JLCBACK));
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

			Campaign20060920JLCBACK castObj = (Campaign20060920JLCBACK)obj;
			return (castObj != null && this.campaignId == castObj.CampaignId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.campaignId.GetHashCode());
		}
		#endregion
	}
}
