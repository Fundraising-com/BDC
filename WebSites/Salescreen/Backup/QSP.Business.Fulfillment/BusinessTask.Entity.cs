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
	[Class(Schema="`dbo`", Table="`business_task`")]
	public partial class BusinessTask
	{
		#region Constants
        public const string BusinessTaskEntity = "BusinessTask";
		public const string BusinessTaskIdProperty = "BusinessTaskId";
		public const string FormIdProperty = "FormId";
		public const string TaskIdProperty = "TaskId";
		public const string BusinessTaskNameProperty = "BusinessTaskName";
		public const string TaskExpressionProperty = "TaskExpression";
		public const string DescriptionProperty = "Description";
		public const string MessageProperty = "Message";
		public const string AssignmentTypeIdProperty = "AssignmentTypeId";
		public const string AssignedUserIdProperty = "AssignedUserId";
		public const string AssignedRoleIdProperty = "AssignedRoleId";
		public const string ParentBusinessTaskIdProperty = "ParentBusinessTaskId";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		#endregion

		#region Fields
		protected int businessTaskId = 0;
		protected int formId = 0;
		protected int taskId = 0;
		protected string businessTaskName = "";
		protected string taskExpression = "";
		protected string description = null;
		protected string message = null;
		protected int? assignmentTypeId = 1;
		protected int? assignedUserId = null;
		protected int? assignedRoleId = null;
		protected int? parentBusinessTaskId = null;
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = DateTime.Now;
		protected int? updateUserId = null;
		#endregion

		#region Constructors
		public BusinessTask() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""BusinessTaskId"" column=""`business_task_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int BusinessTaskId
		{
			get { return this.businessTaskId; }
			set { this.businessTaskId = value; }
		}

		[Property(Column="`form_id`")]
		public virtual int FormId
		{
			get { return this.formId; }
			set { this.formId = value; }
		}

		[Property(Column="`task_id`")]
		public virtual int TaskId
		{
			get { return this.taskId; }
			set { this.taskId = value; }
		}

		[Property(Column="`business_task_name`")]
		public virtual string BusinessTaskName
		{
			get { return this.businessTaskName; }
			set { this.businessTaskName = value; }
		}

		[Property(Column="`task_expression`")]
		public virtual string TaskExpression
		{
			get { return this.taskExpression; }
			set { this.taskExpression = value; }
		}

		[Property(Column="`description`")]
		public virtual string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		[Property(Column="`message`")]
		public virtual string Message
		{
			get { return this.message; }
			set { this.message = value; }
		}

		[Property(Column="`assignment_type_id`")]
		public virtual int? AssignmentTypeId
		{
			get { return this.assignmentTypeId; }
			set { this.assignmentTypeId = value; }
		}

		[Property(Column="`assigned_user_id`")]
		public virtual int? AssignedUserId
		{
			get { return this.assignedUserId; }
			set { this.assignedUserId = value; }
		}

		[Property(Column="`assigned_role_id`")]
		public virtual int? AssignedRoleId
		{
			get { return this.assignedRoleId; }
			set { this.assignedRoleId = value; }
		}

		[Property(Column="`parent_business_task_id`")]
		public virtual int? ParentBusinessTaskId
		{
			get { return this.parentBusinessTaskId; }
			set { this.parentBusinessTaskId = value; }
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
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(BusinessTask));
                return c;
            }
        }

        public static List<BusinessTask> GetBusinessTaskList(ICriteria criteria)
        {
            return (List<BusinessTask>)criteria.List<BusinessTask>();
        }

		public static BusinessTask GetBusinessTask(int businessTaskId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<BusinessTask>(businessTaskId);
			}
		}

		public static List<BusinessTask> GetBusinessTaskList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(BusinessTask));
				return (List<BusinessTask>)c.List<BusinessTask>();
			}
		}

		public static List<BusinessTask> GetBusinessTaskList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(BusinessTask));
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

				return (List<BusinessTask>)c.List<BusinessTask>();
			}
		}

		public static List<BusinessTask> GetBusinessTaskList(string sortExpression)
		{
			return GetBusinessTaskList(sortExpression, -1, -1);
		}

		public static void InsertBusinessTask(BusinessTask obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateBusinessTask(BusinessTask obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteBusinessTask(BusinessTask obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static BusinessTask PopulateBusinessTask(IDataReader r)
		{
			BusinessTask obj = new BusinessTask();
			obj.BusinessTaskId = (int)r["business_task_id"];
			obj.FormId = (int)r["form_id"];
			obj.TaskId = (int)r["task_id"];
			obj.BusinessTaskName = (string)r["business_task_name"];
			obj.TaskExpression = (string)r["task_expression"];
			obj.Description = (r["description"] == DBNull.Value) ? null : (string)r["description"];
			obj.Message = (r["message"] == DBNull.Value) ? null : (string)r["message"];
			obj.AssignmentTypeId = (r["assignment_type_id"] == DBNull.Value) ? null : (int?)r["assignment_type_id"];
			obj.AssignedUserId = (r["assigned_user_id"] == DBNull.Value) ? null : (int?)r["assigned_user_id"];
			obj.AssignedRoleId = (r["assigned_role_id"] == DBNull.Value) ? null : (int?)r["assigned_role_id"];
			obj.ParentBusinessTaskId = (r["parent_business_task_id"] == DBNull.Value) ? null : (int?)r["parent_business_task_id"];
			obj.Deleted = (bool)r["deleted"];
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
				ICriteria c = session.CreateCriteria(typeof(BusinessTask));
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

			BusinessTask castObj = (BusinessTask)obj;
			return (castObj != null && this.businessTaskId == castObj.BusinessTaskId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.businessTaskId.GetHashCode());
		}
		#endregion
	}
}
