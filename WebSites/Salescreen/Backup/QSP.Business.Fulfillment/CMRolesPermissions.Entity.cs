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
	[Class(Schema="`dbo`", Table="`CM_Roles_Permissions`")]
	public partial class CMRolesPermissions
	{
		#region Constants
        public const string CMRolesPermissionsEntity = "CMRolesPermissions";
		public const string RolePermissionIDProperty = "RolePermissionID";
		public const string RoleIDProperty = "RoleID";
		public const string EntityTypeIDProperty = "EntityTypeID";
		public const string RightViewProperty = "RightView";
		public const string RightInsertProperty = "RightInsert";
		public const string RightUpdateProperty = "RightUpdate";
		public const string RightDeleteProperty = "RightDelete";
		#endregion

		#region Fields
		protected int rolePermissionID = 0;
		protected int? roleID = null;
		protected int? entityTypeID = null;
		protected bool? rightView = null;
		protected bool? rightInsert = null;
		protected bool? rightUpdate = null;
		protected bool? rightDelete = null;
		#endregion

		#region Constructors
		public CMRolesPermissions() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""RolePermissionID"" column=""`Role_Permission_ID`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int RolePermissionID
		{
			get { return this.rolePermissionID; }
			set { this.rolePermissionID = value; }
		}

		[Property(Column="`Role_ID`")]
		public virtual int? RoleID
		{
			get { return this.roleID; }
			set { this.roleID = value; }
		}

		[Property(Column="`EntityTypeID`")]
		public virtual int? EntityTypeID
		{
			get { return this.entityTypeID; }
			set { this.entityTypeID = value; }
		}

		[Property(Column="`Right_View`")]
		public virtual bool? RightView
		{
			get { return this.rightView; }
			set { this.rightView = value; }
		}

		[Property(Column="`Right_Insert`")]
		public virtual bool? RightInsert
		{
			get { return this.rightInsert; }
			set { this.rightInsert = value; }
		}

		[Property(Column="`Right_Update`")]
		public virtual bool? RightUpdate
		{
			get { return this.rightUpdate; }
			set { this.rightUpdate = value; }
		}

		[Property(Column="`Right_Delete`")]
		public virtual bool? RightDelete
		{
			get { return this.rightDelete; }
			set { this.rightDelete = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(CMRolesPermissions));
                return c;
            }
        }

        public static List<CMRolesPermissions> GetCMRolesPermissionsList(ICriteria criteria)
        {
            return (List<CMRolesPermissions>)criteria.List<CMRolesPermissions>();
        }

		public static CMRolesPermissions GetCMRolesPermissions(int rolePermissionID)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<CMRolesPermissions>(rolePermissionID);
			}
		}

		public static List<CMRolesPermissions> GetCMRolesPermissionsList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CMRolesPermissions));
				return (List<CMRolesPermissions>)c.List<CMRolesPermissions>();
			}
		}

		public static List<CMRolesPermissions> GetCMRolesPermissionsList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CMRolesPermissions));
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

				return (List<CMRolesPermissions>)c.List<CMRolesPermissions>();
			}
		}

		public static List<CMRolesPermissions> GetCMRolesPermissionsList(string sortExpression)
		{
			return GetCMRolesPermissionsList(sortExpression, -1, -1);
		}

		public static void InsertCMRolesPermissions(CMRolesPermissions obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateCMRolesPermissions(CMRolesPermissions obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteCMRolesPermissions(CMRolesPermissions obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static CMRolesPermissions PopulateCMRolesPermissions(IDataReader r)
		{
			CMRolesPermissions obj = new CMRolesPermissions();
			obj.RolePermissionID = (int)r["Role_Permission_ID"];
			obj.RoleID = (r["Role_ID"] == DBNull.Value) ? null : (int?)r["Role_ID"];
			obj.EntityTypeID = (r["EntityTypeID"] == DBNull.Value) ? null : (int?)r["EntityTypeID"];
			obj.RightView = (r["Right_View"] == DBNull.Value) ? null : (bool?)r["Right_View"];
			obj.RightInsert = (r["Right_Insert"] == DBNull.Value) ? null : (bool?)r["Right_Insert"];
			obj.RightUpdate = (r["Right_Update"] == DBNull.Value) ? null : (bool?)r["Right_Update"];
			obj.RightDelete = (r["Right_Delete"] == DBNull.Value) ? null : (bool?)r["Right_Delete"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CMRolesPermissions));
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

			CMRolesPermissions castObj = (CMRolesPermissions)obj;
			return (castObj != null && this.rolePermissionID == castObj.RolePermissionID);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.rolePermissionID.GetHashCode());
		}
		#endregion
	}
}
