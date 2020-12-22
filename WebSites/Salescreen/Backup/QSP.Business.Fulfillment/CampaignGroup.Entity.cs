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
	[Class(Schema="`dbo`", Table="`campaign_group`")]
	public partial class CampaignGroup
	{
		#region Constants
        public const string CampaignGroupEntity = "CampaignGroup";
		public const string CampaignGroupIdProperty = "CampaignGroupId";
		public const string CampaignGroupNameProperty = "CampaignGroupName";
		public const string CampaignIdProperty = "CampaignId";
		#endregion

		#region Fields
		protected int campaignGroupId = 0;
		protected string campaignGroupName = "";
		protected int? campaignId = null;
		#endregion

		#region Constructors
		public CampaignGroup() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""CampaignGroupId"" column=""`campaign_group_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int CampaignGroupId
		{
			get { return this.campaignGroupId; }
			set { this.campaignGroupId = value; }
		}

		[Property(Column="`campaign_group_name`")]
		public virtual string CampaignGroupName
		{
			get { return this.campaignGroupName; }
			set { this.campaignGroupName = value; }
		}

		[Property(Column="`campaign_id`")]
		public virtual int? CampaignId
		{
			get { return this.campaignId; }
			set { this.campaignId = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(CampaignGroup));
                return c;
            }
        }

        public static List<CampaignGroup> GetCampaignGroupList(ICriteria criteria)
        {
            return (List<CampaignGroup>)criteria.List<CampaignGroup>();
        }

		public static CampaignGroup GetCampaignGroup(int campaignGroupId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<CampaignGroup>(campaignGroupId);
			}
		}

		public static List<CampaignGroup> GetCampaignGroupList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CampaignGroup));
				return (List<CampaignGroup>)c.List<CampaignGroup>();
			}
		}

		public static List<CampaignGroup> GetCampaignGroupList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CampaignGroup));
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

				return (List<CampaignGroup>)c.List<CampaignGroup>();
			}
		}

		public static List<CampaignGroup> GetCampaignGroupList(string sortExpression)
		{
			return GetCampaignGroupList(sortExpression, -1, -1);
		}

		public static void InsertCampaignGroup(CampaignGroup obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateCampaignGroup(CampaignGroup obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteCampaignGroup(CampaignGroup obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static CampaignGroup PopulateCampaignGroup(IDataReader r)
		{
			CampaignGroup obj = new CampaignGroup();
			obj.CampaignGroupId = (int)r["campaign_group_id"];
			obj.CampaignGroupName = (string)r["campaign_group_name"];
			obj.CampaignId = (r["campaign_id"] == DBNull.Value) ? null : (int?)r["campaign_id"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CampaignGroup));
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

			CampaignGroup castObj = (CampaignGroup)obj;
			return (castObj != null && this.campaignGroupId == castObj.CampaignGroupId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.campaignGroupId.GetHashCode());
		}
		#endregion
	}
}
