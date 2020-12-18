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
	[Class(Schema="`dbo`", Table="`fulfillment_house`")]
	public partial class FulfillmentHouse
	{
		#region Constants
        public const string FulfillmentHouseEntity = "FulfillmentHouse";
		public const string FulfillmentHouseIdProperty = "FulfillmentHouseId";
		public const string FulfillmentHouseNameProperty = "FulfillmentHouseName";
		#endregion

		#region Fields
		protected int fulfillmentHouseId = 0;
		protected string fulfillmentHouseName = "";
		#endregion

		#region Constructors
		public FulfillmentHouse() 
		{
		}
		#endregion

		#region Properties
		[RawXml(Content=@"
		<id name=""FulfillmentHouseId"" column=""`fulfillment_house_id`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

		public virtual int FulfillmentHouseId
		{
			get { return this.fulfillmentHouseId; }
			set { this.fulfillmentHouseId = value; }
		}

		[Property(Column="`fulfillment_house_name`")]
		public virtual string FulfillmentHouseName
		{
			get { return this.fulfillmentHouseName; }
			set { this.fulfillmentHouseName = value; }
		}
		#endregion

		#region Methods

        public static ICriteria CreateCriteria()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(FulfillmentHouse));
                return c;
            }
        }

        public static List<FulfillmentHouse> GetFulfillmentHouseList(ICriteria criteria)
        {
            return (List<FulfillmentHouse>)criteria.List<FulfillmentHouse>();
        }

		public static FulfillmentHouse GetFulfillmentHouse(int fulfillmentHouseId)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				return session.Get<FulfillmentHouse>(fulfillmentHouseId);
			}
		}

		public static List<FulfillmentHouse> GetFulfillmentHouseList()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(FulfillmentHouse));
				return (List<FulfillmentHouse>)c.List<FulfillmentHouse>();
			}
		}

		public static List<FulfillmentHouse> GetFulfillmentHouseList(string sortExpression, int maximumRows, int startRowIndex)
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(FulfillmentHouse));
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

				return (List<FulfillmentHouse>)c.List<FulfillmentHouse>();
			}
		}

		public static List<FulfillmentHouse> GetFulfillmentHouseList(string sortExpression)
		{
			return GetFulfillmentHouseList(sortExpression, -1, -1);
		}

		public static void InsertFulfillmentHouse(FulfillmentHouse obj)
		{
			if (obj != null)
				obj.Insert();
		}

		public static void UpdateFulfillmentHouse(FulfillmentHouse obj)
		{
			if (obj != null)
				obj.Update();
		}

		public static void DeleteFulfillmentHouse(FulfillmentHouse obj)
		{
			if (obj != null)
				obj.Delete();
		}

        protected static FulfillmentHouse PopulateFulfillmentHouse(IDataReader r)
		{
			FulfillmentHouse obj = new FulfillmentHouse();
			obj.FulfillmentHouseId = (int)r["fulfillment_house_id"];
			obj.FulfillmentHouseName = (string)r["fulfillment_house_name"];

			return obj;
		}

		public static int GetCount()
		{
			using (ISession session = SqlSessionManager.OpenSession())
			{
				ICriteria c = session.CreateCriteria(typeof(FulfillmentHouse));
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

			FulfillmentHouse castObj = (FulfillmentHouse)obj;
			return (castObj != null && this.fulfillmentHouseId == castObj.FulfillmentHouseId);
		}

		/// <summary>
		/// Local implementation of GetHashCode based on unique value members
		/// </summary>
		public override int GetHashCode()
		{
			return 29 * (1 + this.fulfillmentHouseId.GetHashCode());
		}
		#endregion
	}
}
