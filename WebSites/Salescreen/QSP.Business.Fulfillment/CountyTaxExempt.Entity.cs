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
	[Class(Schema="`dbo`", Table="`county_tax_exempt`")]
	public partial class CountyTaxExempt
	{
		#region Constants
        public const string CountyTaxExemptEntity = "CountyTaxExempt";
		public const string CountyTaxExemptIdProperty = "CountyTaxExemptId";
		public const string CountyNameProperty = "CountyName";
		public const string SubdivisionCodeProperty = "SubdivisionCode";
		#endregion

		#region Fields
		protected int countyTaxExemptId = 0;
		protected string countyName = "";
		protected int subdivisionCode = 0;
		#endregion

		#region Constructors
		public CountyTaxExempt() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""CountyTaxExemptId"" column=""`county_tax_exempt_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int CountyTaxExemptId
		{
			get { return this.countyTaxExemptId; }
			set { this.countyTaxExemptId = value; }
		}

		[Property(Column="`county_name`")]
		public virtual string CountyName
		{
			get { return this.countyName; }
			set { this.countyName = value; }
		}

		[Property(Column="`subdivision_code`")]
		public virtual int SubdivisionCode
		{
			get { return this.subdivisionCode; }
			set { this.subdivisionCode = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(CountyTaxExempt));
                return c;
            }
        }

        public static List<CountyTaxExempt> GetCountyTaxExemptList(ICriteria criteria)
        {
            return (List<CountyTaxExempt>)criteria.List<CountyTaxExempt>();
        }

		public static CountyTaxExempt GetCountyTaxExempt(int countyTaxExemptId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<CountyTaxExempt>(countyTaxExemptId);
			}
		}

		public static List<CountyTaxExempt> GetCountyTaxExemptList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CountyTaxExempt));
				return (List<CountyTaxExempt>)c.List<CountyTaxExempt>();
			}
		}

		public static List<CountyTaxExempt> GetCountyTaxExemptList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CountyTaxExempt));
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

				return (List<CountyTaxExempt>)c.List<CountyTaxExempt>();
			}
		}

		public static List<CountyTaxExempt> GetCountyTaxExemptList(string sortExpression)
		{
			return GetCountyTaxExemptList(sortExpression, -1, -1);
		}

		public static void InsertCountyTaxExempt(CountyTaxExempt obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateCountyTaxExempt(CountyTaxExempt obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteCountyTaxExempt(CountyTaxExempt obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static CountyTaxExempt PopulateCountyTaxExempt(IDataReader r)
		{
			CountyTaxExempt obj = new CountyTaxExempt();
			obj.CountyTaxExemptId = (int)r["county_tax_exempt_id"];
			obj.CountyName = (string)r["county_name"];
			obj.SubdivisionCode = (int)r["subdivision_code"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(CountyTaxExempt));
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

			CountyTaxExempt castObj = (CountyTaxExempt)obj;
			return (castObj != null && this.countyTaxExemptId == castObj.CountyTaxExemptId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.countyTaxExemptId.GetHashCode());
		}
		#endregion
	}
}
