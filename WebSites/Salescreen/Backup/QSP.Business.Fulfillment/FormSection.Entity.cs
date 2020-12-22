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
	[Class(Schema="`dbo`", Table="`form_section`")]
	public partial class FormSection
	{
		#region Constants
        public const string FormSectionEntity = "FormSection";
		public const string FormSectionIdProperty = "FormSectionId";
		public const string FormIdProperty = "FormId";
		public const string FormSectionTypeIdProperty = "FormSectionTypeId";
		public const string FormSectionNumberProperty = "FormSectionNumber";
		public const string FormSectionTitleProperty = "FormSectionTitle";
		public const string DescriptionProperty = "Description";
		public const string CatalogItemCategoryIdProperty = "CatalogItemCategoryId";
		public const string DeletedProperty = "Deleted";
		public const string CreateDateProperty = "CreateDate";
		public const string CreateUserIdProperty = "CreateUserId";
		public const string UpdateDateProperty = "UpdateDate";
		public const string UpdateUserIdProperty = "UpdateUserId";
		#endregion

		#region Fields
		protected int formSectionId = 0;
		protected int formId = 0;
		protected int formSectionTypeId = 0;
		protected int? formSectionNumber = null;
		protected string formSectionTitle = null;
		protected string description = null;
		protected int catalogItemCategoryId = 0;
		protected bool deleted = false;
		protected DateTime createDate = DateTime.Now;
		protected int createUserId = 0;
		protected DateTime? updateDate = DateTime.Now;
		protected int? updateUserId = null;
		#endregion

		#region Constructors
		public FormSection() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""FormSectionId"" column=""`form_section_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int FormSectionId
		{
			get { return this.formSectionId; }
			set { this.formSectionId = value; }
		}

		[Property(Column="`form_id`")]
		public virtual int FormId
		{
			get { return this.formId; }
			set { this.formId = value; }
		}

		[Property(Column="`form_section_type_id`")]
		public virtual int FormSectionTypeId
		{
			get { return this.formSectionTypeId; }
			set { this.formSectionTypeId = value; }
		}

		[Property(Column="`form_section_number`")]
		public virtual int? FormSectionNumber
		{
			get { return this.formSectionNumber; }
			set { this.formSectionNumber = value; }
		}

		[Property(Column="`form_section_title`")]
		public virtual string FormSectionTitle
		{
			get { return this.formSectionTitle; }
			set { this.formSectionTitle = value; }
		}

		[Property(Column="`description`")]
		public virtual string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		[Property(Column="`catalog_item_category_id`")]
		public virtual int CatalogItemCategoryId
		{
			get { return this.catalogItemCategoryId; }
			set { this.catalogItemCategoryId = value; }
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
                ICriteria c = session.CreateCriteria(typeof(FormSection));
                return c;
            }
        }

        public static List<FormSection> GetFormSectionList(ICriteria criteria)
        {
            return (List<FormSection>)criteria.List<FormSection>();
        }

		public static FormSection GetFormSection(int formSectionId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<FormSection>(formSectionId);
			}
		}

		public static List<FormSection> GetFormSectionList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(FormSection));
				return (List<FormSection>)c.List<FormSection>();
			}
		}

		public static List<FormSection> GetFormSectionList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(FormSection));
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

				return (List<FormSection>)c.List<FormSection>();
			}
		}

		public static List<FormSection> GetFormSectionList(string sortExpression)
		{
			return GetFormSectionList(sortExpression, -1, -1);
		}

		public static void InsertFormSection(FormSection obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateFormSection(FormSection obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteFormSection(FormSection obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static FormSection PopulateFormSection(IDataReader r)
		{
			FormSection obj = new FormSection();
			obj.FormSectionId = (int)r["form_section_id"];
			obj.FormId = (int)r["form_id"];
			obj.FormSectionTypeId = (int)r["form_section_type_id"];
			obj.FormSectionNumber = (r["form_section_number"] == DBNull.Value) ? null : (int?)r["form_section_number"];
			obj.FormSectionTitle = (r["form_section_title"] == DBNull.Value) ? null : (string)r["form_section_title"];
			obj.Description = (r["description"] == DBNull.Value) ? null : (string)r["description"];
			obj.CatalogItemCategoryId = (int)r["catalog_item_category_id"];
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
				ICriteria c = session.CreateCriteria(typeof(FormSection));
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

			FormSection castObj = (FormSection)obj;
			return (castObj != null && this.formSectionId == castObj.FormSectionId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.formSectionId.GetHashCode());
		}
		#endregion
	}
}
