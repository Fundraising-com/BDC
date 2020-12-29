/* Title:	ESubs Global Database Donation partial
 * Author:	Jiro Hidaka
 * Summary:	Data access layer object to retreive Donation data in the EFRECommerce database.
 * 
 * Create Date:	September 3, 2011
 * 
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Data;
using GA.BDC.Core.BusinessBase;
using GA.BDC.Core.Data.Sql;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.ESubsGlobal;
using GA.BDC.Core.ESubsGlobal.Donation;

namespace GA.BDC.Core.ESubsGlobal.DataAccess
{
    public partial class ESubsGlobalDatabase : GA.BDC.Core.Data.Sql.DatabaseObject
    {
        #region Orders Info
        private Comments LoadComment(DataRow row)
        {
            Comments comment = new Comments();
            comment.EventParticipationID = DBValue.ToInt32(row["event_participation_id"]);
            comment.EventID = DBValue.ToInt32(row["event_id"]);
            comment.MemberHierarchyID = DBValue.ToInt32(row["member_hierarchy_id"]);
            comment.DonorName = DBValue.ToString(row["donor_name"]);
            comment.DonationAmount = DBValue.ToDecimal(row["donation_amount"]);
            comment.DonorComments = DBValue.ToString(row["donor_comments"]);
            return comment;
        }
 
        public List<Comments> GetDonorCommentsByPartnerID(Int32 partner_id)
        {
            List<Comments> comments = new List<Comments>();

            bool useTransaction = false;
            string storedProcName = "es_get_donor_comments_by_partner_id";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@partner_id", DbType.Int32, DBValue.ToDBInt32(partner_id)));
                si.Open();
                if (useTransaction)
                    si.BeginTransaction();

                // Fetch and store into database.
                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                // fill our objects
                try
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        comments.Add(LoadComment(row));
                    }
                }
                catch (System.Exception ex)
                {
                    throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                }

                // Commit our transaction.
                if (useTransaction)
                    si.Commit();
            }
            catch
            {
                // Rollback on error.
                if (useTransaction)
                    si.Rollback();

                // throw exception
                throw;
            }
            finally
            {
                // Always close connection.
                si.Close();
            }
            return comments;
        }
        #endregion
    }
}
