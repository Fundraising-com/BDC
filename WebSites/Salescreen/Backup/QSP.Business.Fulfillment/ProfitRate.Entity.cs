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
using NHibernate.Expression;
using NHibernate.Mapping.Attributes;

namespace QSP.Business.Fulfillment
{
    [Serializable]
    [Class(Schema = "`dbo`", Table = "`profit_rate`")]
    public partial class ProfitRate
    {
        #region Constants
        public const string ProfitRateEntity = "ProfitRate";
        public const string CommissionRateProperty = "CommissionRate";
        public const string ProfitRateIdProperty = "ProfitRateId";
        public const string ProfitRateAmountProperty = "ProfitRateAmount";
        #endregion

        #region Fields
        protected double commissionRate = 0;
        protected int profitRateId = 0;
        protected double profitRateAmount = 0;
        #endregion

        #region Constructors
        public ProfitRate()
        {
        }
        #endregion

        #region Properties
        [RawXml(Content = @"
		<id name=""ProfitRateId"" column=""`profit_rate_id`"">
			<generator class=""assigned"">
			</generator>
		</id>")]

        [Property(Column = "`commission_rate`")]
        public virtual double CommissionRate
        {
            get { return this.commissionRate; }
            set { this.commissionRate = value; }
        }

        public virtual int ProfitRateId
        {
            get { return this.profitRateId; }
            set { this.profitRateId = value; }
        }

        [Property(Column = "`profit_rate`")]
        public virtual double ProfitRateAmount
        {
            get { return this.profitRateAmount; }
            set { this.profitRateAmount = value; }
        }
        #endregion

        #region Methods
        public static ProfitRate GetProfitRate(int profitRateId)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                return session.Get<ProfitRate>(profitRateId);
            }
        }

        public static List<ProfitRate> GetProfitRateList(ICriteria criteria)
        {
            return (List<ProfitRate>)criteria.List<ProfitRate>();
        }

        public static List<ProfitRate> GetProfitRateList()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(ProfitRate));
                return (List<ProfitRate>)c.List<ProfitRate>();
            }
        }

        public static List<ProfitRate> GetProfitRateList(string sortExpression, int maximumRows, int startRowIndex)
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(ProfitRate));
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

                return (List<ProfitRate>)c.List<ProfitRate>();
            }
        }

        public static List<ProfitRate> GetProfitRateList(string sortExpression)
        {
            return GetProfitRateList(sortExpression, -1, -1);
        }

        public static void InsertProfitRate(ProfitRate obj)
        {
            if (obj != null)
                obj.Insert();
        }

        public static void UpdateProfitRate(ProfitRate obj)
        {
            if (obj != null)
                obj.Update();
        }

        public static void DeleteProfitRate(ProfitRate obj)
        {
            if (obj != null)
                obj.Delete();
        }

        protected static ProfitRate PopulateProfitRate(IDataReader r)
        {
            ProfitRate obj = new ProfitRate();
            obj.CommissionRate = (double)r["commission_rate"];
            obj.ProfitRateId = (int)r["profit_rate_id"];
            obj.ProfitRateAmount = (double)r["profit_rate"];

            return obj;
        }

        public static int GetCount()
        {
            using (ISession session = SqlSessionManager.OpenSession())
            {
                ICriteria c = session.CreateCriteria(typeof(ProfitRate));
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

            ProfitRate castObj = (ProfitRate)obj;
            return (castObj != null && this.profitRateId == castObj.ProfitRateId);
        }

        /// <summary>
        /// Local implementation of GetHashCode based on unique value members
        /// </summary>
        public override int GetHashCode()
        {
            return 29 * (1 + this.profitRateId.GetHashCode());
        }
        #endregion
    }
}
