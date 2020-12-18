﻿////////////////////////////////////////////////////////////////////////////////////////////
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
	[Class(Schema = "`dbo`", Table = "`form_permission_region`")]
	public partial class FormPermissionRegion
	{
		#region Constants
		public const string FormPermissionRegionEntity = "FormPermissionRegion";
		public const string FormPermissionRegionIdProperty = "FormPermissionRegionId";
		public const string FormIdProperty = "FormId";
		public const string ZipProperty = "Zip";
		public const string DescriptionProperty = "Description";
		public const string AllowReadProperty = "AllowRead";
		public const string AllowWriteProperty = "AllowWrite";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		#endregion

		#region Fields
		protected int formPermissionRegionId = 0;
		protected int? formId = null;
		protected string zip = null;
		protected string description = null;
		protected bool allowRead = true;
		protected bool allowWrite = true;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime updateDate = DateTime.Now;
		protected int updateUserId = 0;
		#endregion

		#region Constructors
		public FormPermissionRegion()
		{
		}
		#endregion

		#region Properties
		[RawXml(Content = @"
		<id name=""FormPermissionRegionId"" column=""`form_permission_region_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int FormPermissionRegionId
		{
			get { return this.formPermissionRegionId; }
			set { this.formPermissionRegionId = value; }
		}

		[Property(Column = "`form_id`")]
		public virtual int? FormId
		{
			get { return this.formId; }
			set { this.formId = value; }
		}

		[Property(Column = "`zip`")]
		public virtual string Zip
		{
			get { return this.zip; }
			set { this.zip = value; }
		}

		[Property(Column = "`description`")]
		public virtual string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		[Property(Column = "`allow_read`")]
		public virtual bool AllowRead
		{
			get { return this.allowRead; }
			set { this.allowRead = value; }
		}

		[Property(Column = "`allow_write`")]
		public virtual bool AllowWrite
		{
			get { return this.allowWrite; }
			set { this.allowWrite = value; }
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
		public virtual DateTime UpdateDate
		{
			get { return this.updateDate; }
			set { this.updateDate = value; }
		}

		[Property(Column = "`update_user_id`")]
		public virtual int UpdateUserId
		{
			get { return this.updateUserId; }
			set { this.updateUserId = value; }
		}
		#endregion

		#region Methods
		public static FormPermissionRegion GetFormPermissionRegion(int formPermissionRegionId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<FormPermissionRegion>(formPermissionRegionId);
			}
		}

		public static List<FormPermissionRegion> GetFormPermissionRegionList(ICriteria criteria)
		{
			return (List<FormPermissionRegion>)criteria.List<FormPermissionRegion>();
		}

		public static List<FormPermissionRegion> GetFormPermissionRegionList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(FormPermissionRegion));
				return (List<FormPermissionRegion>)c.List<FormPermissionRegion>();
			}
		}

		public static List<FormPermissionRegion> GetFormPermissionRegionList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(FormPermissionRegion));
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

				return (List<FormPermissionRegion>)c.List<FormPermissionRegion>();
			}
		}

		public static List<FormPermissionRegion> GetFormPermissionRegionList(string sortExpression)
		{
			return GetFormPermissionRegionList(sortExpression, -1, -1);
		}

		public static void InsertFormPermissionRegion(FormPermissionRegion obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateFormPermissionRegion(FormPermissionRegion obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteFormPermissionRegion(FormPermissionRegion obj)
		{
			if (obj != null)
				obj.Delete();
		}

		protected static FormPermissionRegion PopulateFormPermissionRegion(IDataReader r)
		{
			FormPermissionRegion obj = new FormPermissionRegion();
			obj.FormPermissionRegionId = (int)r["form_permission_region_id"];
			obj.FormId = (r["form_id"] == DBNull.Value) ? null : (int?)r["form_id"];
			obj.Zip = (r["zip"] == DBNull.Value) ? null : (string)r["zip"];
			obj.Description = (r["description"] == DBNull.Value) ? null : (string)r["description"];
			obj.AllowRead = (bool)r["allow_read"];
			obj.AllowWrite = (bool)r["allow_write"];
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
				ICriteria c = session.CreateCriteria(typeof(FormPermissionRegion));
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

			FormPermissionRegion castObj = (FormPermissionRegion)obj;
			return (castObj != null && this.formPermissionRegionId == castObj.FormPermissionRegionId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.formPermissionRegionId.GetHashCode());
		}
		#endregion
	}
}
