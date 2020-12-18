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
	[Class(Schema="`dbo`", Table="`epi_eds_batch`")]
	public partial class EpiEdsBatch
	{
		#region Constants
        public const string EpiEdsBatchEntity = "EpiEdsBatch";
		public const string EpiEdsBatchIdProperty = "EpiEdsBatchId";
		public const string EdsBatchIdProperty = "EdsBatchId";
		public const string CreateDateProperty = "CreateDate";
		#endregion

		#region Fields
		protected int epiEdsBatchId = 0;
		protected int edsBatchId = 0;
		protected DateTime? createDate = DateTime.Now;
		#endregion

		#region Constructors
		public EpiEdsBatch() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""EpiEdsBatchId"" column=""`epi_eds_batch_id`"">
			<generator class=""native"">
			</generator>
		</id>")]

		public virtual int EpiEdsBatchId
		{
			get { return this.epiEdsBatchId; }
			set { this.epiEdsBatchId = value; }
		}

		[Property(Column="`eds_batch_id`")]
		public virtual int EdsBatchId
		{
			get { return this.edsBatchId; }
			set { this.edsBatchId = value; }
		}

		[Property(Column="`create_date`")]
		public virtual DateTime? CreateDate
		{
			get { return this.createDate; }
			set { this.createDate = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(EpiEdsBatch));
                return c;
            }
        }

        public static List<EpiEdsBatch> GetEpiEdsBatchList(ICriteria criteria)
        {
            return (List<EpiEdsBatch>)criteria.List<EpiEdsBatch>();
        }

		public static EpiEdsBatch GetEpiEdsBatch(int epiEdsBatchId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<EpiEdsBatch>(epiEdsBatchId);
			}
		}

		public static List<EpiEdsBatch> GetEpiEdsBatchList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(EpiEdsBatch));
				return (List<EpiEdsBatch>)c.List<EpiEdsBatch>();
			}
		}

		public static List<EpiEdsBatch> GetEpiEdsBatchList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(EpiEdsBatch));
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

				return (List<EpiEdsBatch>)c.List<EpiEdsBatch>();
			}
		}

		public static List<EpiEdsBatch> GetEpiEdsBatchList(string sortExpression)
		{
			return GetEpiEdsBatchList(sortExpression, -1, -1);
		}

		public static void InsertEpiEdsBatch(EpiEdsBatch obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateEpiEdsBatch(EpiEdsBatch obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteEpiEdsBatch(EpiEdsBatch obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static EpiEdsBatch PopulateEpiEdsBatch(IDataReader r)
		{
			EpiEdsBatch obj = new EpiEdsBatch();
			obj.EpiEdsBatchId = (int)r["epi_eds_batch_id"];
			obj.EdsBatchId = (int)r["eds_batch_id"];
			obj.CreateDate = (r["create_date"] == DBNull.Value) ? null : (DateTime?)r["create_date"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(EpiEdsBatch));
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

			EpiEdsBatch castObj = (EpiEdsBatch)obj;
			return (castObj != null && this.epiEdsBatchId == castObj.EpiEdsBatchId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.epiEdsBatchId.GetHashCode());
		}
		#endregion
	}
}
