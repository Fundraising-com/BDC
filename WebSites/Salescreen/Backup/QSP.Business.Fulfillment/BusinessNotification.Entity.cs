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
	[Class(Schema="`dbo`", Table="`business_notification`")]
	public partial class BusinessNotification
	{
		#region Constants
        public const string BusinessNotificationEntity = "BusinessNotification";
		public const string BusinessNotificationIdProperty = "BusinessNotificationId";
		public const string BusinessNotificationNameProperty = "BusinessNotificationName";
		public const string BusinessNotificationTypeIdProperty = "BusinessNotificationTypeId";
		public const string SourceIdProperty = "SourceId";
		public const string BusinessTaskIdProperty = "BusinessTaskId";
		public const string AssignedUserIdProperty = "AssignedUserId";
		public const string EntityIdProperty = "EntityId";
		public const string EntityTypeIdProperty = "EntityTypeId";
		public const string SubjectProperty = "Subject";
		public const string MessageProperty = "Message";
		public const string DescriptionProperty = "Description";
		public const string IsCompleteProperty = "IsComplete";
		public const string CompleteDateProperty = "CompleteDate";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		#endregion

		#region Fields
		protected int businessNotificationId = 0;
		protected string businessNotificationName = "";
		protected int businessNotificationTypeId = 0;
		protected int? sourceId = null;
		protected int? businessTaskId = null;
		protected int assignedUserId = 0;
		protected int? entityId = null;
		protected int? entityTypeId = null;
		protected string subject = "";
		protected string message = "";
		protected string description = null;
		protected bool isComplete = false;
		protected DateTime? completeDate = null;
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime updateDate = DateTime.Now;
		protected int updateUserId = 0;
		#endregion

		#region Constructors
		public BusinessNotification() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""BusinessNotificationId"" column=""`business_notification_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int BusinessNotificationId
		{
			get { return this.businessNotificationId; }
			set { this.businessNotificationId = value; }
		}

		[Property(Column="`business_notification_name`")]
		public virtual string BusinessNotificationName
		{
			get { return this.businessNotificationName; }
			set { this.businessNotificationName = value; }
		}

		[Property(Column="`business_notification_type_id`")]
		public virtual int BusinessNotificationTypeId
		{
			get { return this.businessNotificationTypeId; }
			set { this.businessNotificationTypeId = value; }
		}

		[Property(Column="`source_id`")]
		public virtual int? SourceId
		{
			get { return this.sourceId; }
			set { this.sourceId = value; }
		}

		[Property(Column="`business_task_id`")]
		public virtual int? BusinessTaskId
		{
			get { return this.businessTaskId; }
			set { this.businessTaskId = value; }
		}

		[Property(Column="`assigned_user_id`")]
		public virtual int AssignedUserId
		{
			get { return this.assignedUserId; }
			set { this.assignedUserId = value; }
		}

		[Property(Column="`entity_id`")]
		public virtual int? EntityId
		{
			get { return this.entityId; }
			set { this.entityId = value; }
		}

		[Property(Column="`entity_type_id`")]
		public virtual int? EntityTypeId
		{
			get { return this.entityTypeId; }
			set { this.entityTypeId = value; }
		}

		[Property(Column="`subject`")]
		public virtual string Subject
		{
			get { return this.subject; }
			set { this.subject = value; }
		}

		[Property(Column="`message`")]
		public virtual string Message
		{
			get { return this.message; }
			set { this.message = value; }
		}

		[Property(Column="`description`")]
		public virtual string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		[Property(Column="`is_complete`")]
		public virtual bool IsComplete
		{
			get { return this.isComplete; }
			set { this.isComplete = value; }
		}

		[Property(Column="`complete_date`")]
		public virtual DateTime? CompleteDate
		{
			get { return this.completeDate; }
			set { this.completeDate = value; }
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
		public virtual DateTime UpdateDate
		{
			get { return this.updateDate; }
			set { this.updateDate = value; }
		}

		[Property(Column="`update_user_id`")]
		public virtual int UpdateUserId
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
                ICriteria c = session.CreateCriteria(typeof(BusinessNotification));
                return c;
            }
        }

        public static List<BusinessNotification> GetBusinessNotificationList(ICriteria criteria)
        {
            return (List<BusinessNotification>)criteria.List<BusinessNotification>();
        }

		public static BusinessNotification GetBusinessNotification(int businessNotificationId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<BusinessNotification>(businessNotificationId);
			}
		}

		public static List<BusinessNotification> GetBusinessNotificationList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(BusinessNotification));
				return (List<BusinessNotification>)c.List<BusinessNotification>();
			}
		}

		public static List<BusinessNotification> GetBusinessNotificationList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(BusinessNotification));
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

				return (List<BusinessNotification>)c.List<BusinessNotification>();
			}
		}

		public static List<BusinessNotification> GetBusinessNotificationList(string sortExpression)
		{
			return GetBusinessNotificationList(sortExpression, -1, -1);
		}

		public static void InsertBusinessNotification(BusinessNotification obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateBusinessNotification(BusinessNotification obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteBusinessNotification(BusinessNotification obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static BusinessNotification PopulateBusinessNotification(IDataReader r)
		{
			BusinessNotification obj = new BusinessNotification();
			obj.BusinessNotificationId = (int)r["business_notification_id"];
			obj.BusinessNotificationName = (string)r["business_notification_name"];
			obj.BusinessNotificationTypeId = (int)r["business_notification_type_id"];
			obj.SourceId = (r["source_id"] == DBNull.Value) ? null : (int?)r["source_id"];
			obj.BusinessTaskId = (r["business_task_id"] == DBNull.Value) ? null : (int?)r["business_task_id"];
			obj.AssignedUserId = (int)r["assigned_user_id"];
			obj.EntityId = (r["entity_id"] == DBNull.Value) ? null : (int?)r["entity_id"];
			obj.EntityTypeId = (r["entity_type_id"] == DBNull.Value) ? null : (int?)r["entity_type_id"];
			obj.Subject = (string)r["subject"];
			obj.Message = (string)r["message"];
			obj.Description = (r["description"] == DBNull.Value) ? null : (string)r["description"];
			obj.IsComplete = (bool)r["is_complete"];
			obj.CompleteDate = (r["complete_date"] == DBNull.Value) ? null : (DateTime?)r["complete_date"];
			obj.Deleted = (bool)r["deleted"];
			obj.CreateDate = (DateTime)r["create_date"];
			obj.CreateUserId = (int)r["create_user_id"];
			obj.UpdateDate = (DateTime)r["update_date"];
			obj.UpdateUserId = (int)r["update_user_id"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(BusinessNotification));
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

			BusinessNotification castObj = (BusinessNotification)obj;
			return (castObj != null && this.businessNotificationId == castObj.BusinessNotificationId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.businessNotificationId.GetHashCode());
		}
		#endregion
	}
}
