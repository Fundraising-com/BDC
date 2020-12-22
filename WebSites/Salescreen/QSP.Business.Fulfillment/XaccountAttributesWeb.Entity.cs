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
	[Class(Schema="`dbo`", Table="`Xaccount_attributes_web`")]
	public partial class XaccountAttributesWeb
	{
		#region Constants
        public const string XaccountAttributesWebEntity = "XaccountAttributesWeb";
		public const string FulfAccountIdProperty = "FulfAccountId";
		public const string DisplayNameProperty = "DisplayName";
		public const string EnabledProperty = "Enabled";
		#endregion

		#region Fields
		protected int fulfAccountId = 0;
		protected string displayName = "";
		protected bool enabled = false;
		#endregion

		#region Constructors
		public XaccountAttributesWeb() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""FulfAccountId"" column=""`fulf_account_id`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int FulfAccountId
		{
			get { return this.fulfAccountId; }
			set { this.fulfAccountId = value; }
		}

		[Property(Column="`display_name`")]
		public virtual string DisplayName
		{
			get { return this.displayName; }
			set { this.displayName = value; }
		}

		[Property(Column="`enabled`")]
		public virtual bool Enabled
		{
			get { return this.enabled; }
			set { this.enabled = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(XaccountAttributesWeb));
                return c;
            }
        }

        public static List<XaccountAttributesWeb> GetXaccountAttributesWebList(ICriteria criteria)
        {
            return (List<XaccountAttributesWeb>)criteria.List<XaccountAttributesWeb>();
        }

		public static XaccountAttributesWeb GetXaccountAttributesWeb(int fulfAccountId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<XaccountAttributesWeb>(fulfAccountId);
			}
		}

		public static List<XaccountAttributesWeb> GetXaccountAttributesWebList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(XaccountAttributesWeb));
				return (List<XaccountAttributesWeb>)c.List<XaccountAttributesWeb>();
			}
		}

		public static List<XaccountAttributesWeb> GetXaccountAttributesWebList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(XaccountAttributesWeb));
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

				return (List<XaccountAttributesWeb>)c.List<XaccountAttributesWeb>();
			}
		}

		public static List<XaccountAttributesWeb> GetXaccountAttributesWebList(string sortExpression)
		{
			return GetXaccountAttributesWebList(sortExpression, -1, -1);
		}

		public static void InsertXaccountAttributesWeb(XaccountAttributesWeb obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateXaccountAttributesWeb(XaccountAttributesWeb obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteXaccountAttributesWeb(XaccountAttributesWeb obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static XaccountAttributesWeb PopulateXaccountAttributesWeb(IDataReader r)
		{
			XaccountAttributesWeb obj = new XaccountAttributesWeb();
			obj.FulfAccountId = (int)r["fulf_account_id"];
			obj.DisplayName = (string)r["display_name"];
			obj.Enabled = (bool)r["enabled"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(XaccountAttributesWeb));
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

			XaccountAttributesWeb castObj = (XaccountAttributesWeb)obj;
			return (castObj != null && this.fulfAccountId == castObj.FulfAccountId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.fulfAccountId.GetHashCode());
		}
		#endregion
	}
}
