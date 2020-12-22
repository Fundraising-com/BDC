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
	[Class(Schema="`dbo`", Table="`CM_Roles`")]
	public partial class CMRoles
	{
		#region Constants
        public const string CMRolesEntity = "CMRoles";
		public const string RoleIDProperty = "RoleID";
		public const string NameProperty = "Name";
		public const string DescriptionProperty = "Description";
		#endregion

		#region Fields
		protected int roleID = 0;
		protected string name = "";
		protected string description = null;
		#endregion

		#region Constructors
		public CMRoles() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""RoleID"" column=""`Role_ID`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int RoleID
		{
			get { return this.roleID; }
			set { this.roleID = value; }
		}

		[Property(Column="`Name`")]
		public virtual string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		[Property(Column="`Description`")]
		public virtual string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(CMRoles));
                return c;
            }
        }

        public static List<CMRoles> GetCMRolesList(ICriteria criteria)
        {
            return (List<CMRoles>)criteria.List<CMRoles>();
        }

		public static CMRoles GetCMRoles(int roleID)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<CMRoles>(roleID);
			}
		}

		public static List<CMRoles> GetCMRolesList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CMRoles));
				return (List<CMRoles>)c.List<CMRoles>();
			}
		}

		public static List<CMRoles> GetCMRolesList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CMRoles));
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

				return (List<CMRoles>)c.List<CMRoles>();
			}
		}

		public static List<CMRoles> GetCMRolesList(string sortExpression)
		{
			return GetCMRolesList(sortExpression, -1, -1);
		}

		public static void InsertCMRoles(CMRoles obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateCMRoles(CMRoles obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteCMRoles(CMRoles obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static CMRoles PopulateCMRoles(IDataReader r)
		{
			CMRoles obj = new CMRoles();
			obj.RoleID = (int)r["Role_ID"];
			obj.Name = (string)r["Name"];
			obj.Description = (r["Description"] == DBNull.Value) ? null : (string)r["Description"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CMRoles));
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

			CMRoles castObj = (CMRoles)obj;
			return (castObj != null && this.roleID == castObj.RoleID);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.roleID.GetHashCode());
		}
		#endregion
	}
}
