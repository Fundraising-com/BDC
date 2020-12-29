/* Title:	ESubs Global Database touch partial
 * Author:	jason warren
 * Summary:	Data access layer object to retreive/update/insert values in the database.
 * 
 * Create Date:	August 1, 2011
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using GA.BDC.Core.BusinessBase;
using GA.BDC.Core.Data.Sql;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.ESubsGlobal;
using GA.BDC.Core.ESubsGlobal.Common;
using GA.BDC.Core.ESubsGlobal.Touch;
using GA.BDC.Core.ESubsGlobal.Users;
using GA.BDC.Core.ESubsGlobal.Payment;
using GA.BDC.Core.ESubsGlobal.Stats;
using GA.BDC.Core.ESubsGlobal.FlagPole;
using GA.BDC.Core.ESubsGlobal.Promo;
using System.Configuration;

using System.Xml;


namespace GA.BDC.Core.ESubsGlobal.DataAccess
{
    public partial class ESubsGlobalDatabase : GA.BDC.Core.Data.Sql.DatabaseObject
    {
        public ParticipantTotalAmount LoadParticipantTotalAmount(DataRow row)
        {
            ParticipantTotalAmount pta = new ParticipantTotalAmount();
            pta.EventParticipationId = DBValue.ToInt32(row["event_participation_id"]);
            pta.ParticipantName = DBValue.ToString(row["participant_name"]);
            pta.Items = DBValue.ToInt32(row["items"]);
            pta.TotalAmount = DBValue.ToDecimal(row["total_amount"]);
            pta.TotalSupporters = DBValue.ToInt32(row["total_supporters"]);
            pta.TotalDonationAmount = DBValue.ToDecimal(row["total_donation_amount"]);
            pta.TotalDonors = DBValue.ToInt32(row["total_donors"]);
            if (row.Table.Columns.Contains("total_profit"))
                pta.TotalProfit = (decimal)DBValue.ToDouble(row["total_profit"]);

            return pta;
        }

        public List<ParticipantTotalAmount> GetTop3ParticipantTotalAmountByParnerID(Int32 partnerID)
        {
            List<ParticipantTotalAmount> list = new List<ParticipantTotalAmount>();

            bool useTransaction = false;
            string storedProcName = "es_get_top3_participant_total_amount_by_partner_id";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@partner_id", DbType.Int32, DBValue.ToDBInt32(partnerID)));
                si.Open();
                if (useTransaction)
                    si.BeginTransaction();

                // Fetch and store into database.
                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                // fill our objects
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(LoadParticipantTotalAmount(dr));
                        }
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
            return list;
        }

        public List<FindEvent> FindSomething(string eventName, string countryCode, string subDivisionCode, int partnerID,string searchType)
        {
            List<FindEvent> lst = new List<FindEvent>();

            bool useTransaction = false;
            string storedProcName = "es_find_something";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@keyword", DbType.String, DBValue.ToDBString(eventName)));
                if (!string.IsNullOrEmpty(searchType))
                {
                    paramCol.Add(new SqlDataParameter("@searchType", DbType.String, DBValue.ToDBString(searchType)));
                }

                paramCol.Add(new SqlDataParameter("@country_code", DbType.String, DBValue.ToDBString(countryCode)));
                paramCol.Add(new SqlDataParameter("@subdivision_code", DbType.String, DBValue.ToDBString(subDivisionCode)));
                paramCol.Add(new SqlDataParameter("@partner_id", DbType.Int32, DBValue.ToDBInt32((partnerID == 0 ? int.MinValue : partnerID))));

                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                // Fetch and store into database.
                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                // fill our objects
                try
                {
                    lst = new List<FindEvent>(dt.Rows.Count);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Event ev = LoadEvent(dt.Rows[i]);                        

                        subDivisionCode = DBValue.ToString(dt.Rows[i]["subdivision_code"]);
                        string address = DBValue.ToString(dt.Rows[i]["address"]);
                        string city = DBValue.ToString(dt.Rows[i]["city"]);

                        // UPDATE July 22, 2011:
                        //   Created a new table 'event_total_amount' to store amount raised per event
                        decimal totalAmount = 0M, totalDonationAmount = 0M, totalProfit = 0M;
                        int totalSupporters = 0, totalDonars = 0;
                        if (dt.Rows[i].Table.Columns.Contains("total_amount"))
                            totalAmount = DBValue.ToDecimal(dt.Rows[i]["total_amount"]);
                        if (dt.Rows[i].Table.Columns.Contains("total_supporters"))
                            totalSupporters = DBValue.ToInt32(dt.Rows[i]["total_supporters"]);
                        if (dt.Rows[i].Table.Columns.Contains("total_donation_amount"))
                            totalDonationAmount = DBValue.ToDecimal(dt.Rows[i]["total_donation_amount"]);
                        if (dt.Rows[i].Table.Columns.Contains("total_donors"))
                            totalDonars = DBValue.ToInt32(dt.Rows[i]["total_donors"]);
                        if (dt.Rows[i].Table.Columns.Contains("total_profit"))
                            totalProfit = (decimal)DBValue.ToDouble(dt.Rows[i]["total_profit"]);

                        FindEvent fev = new FindEvent(null, subDivisionCode, address, city, totalAmount, totalSupporters, totalDonationAmount, totalDonars);
                        fev.Name = DBValue.ToString(dt.Rows[i]["name"]);
                        fev.TotalProfit = totalProfit;

                        fev.event_participation_id = dt.Rows[i]["event_participation_id"].ToString();
                        fev.event_id = dt.Rows[i]["event_id"].ToString();

                        lst.Add(fev);                      
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

            return lst;
        }

        #region MemberType
        private MemberType LoadMemberType(DataRow row)
        {
            MemberType m = new MemberType();

            // Store database values into our business object
            m.memberTypeId = DBValue.ToInt32(row["member_type_id"]);
            m.memberTypeName = DBValue.ToString(row["member_type_name"]);
            m.emailDescription = DBValue.ToString(row["email_description"]);
            
            // return the filled object
            return m;
        }

        #endregion

        #region Custom Email template
        private CustomEmailTemplate LoadCustomEmailTemplate(DataRow row)
        {
            CustomEmailTemplate cet = new CustomEmailTemplate();

            // Store database values into our business object
            cet.custom_email_template_id = DBValue.ToInt32(row["custom_email_template_id"]);
            cet.touch_info_id = DBValue.ToInt32(row["touch_info_id"]);
            cet.subject = DBValue.ToString(row["subject"]);
            cet.body_html = DBValue.ToString(row["body_html"]);
            cet.body_txt = DBValue.ToString(row["body_txt"]);
            cet.create_date = DBValue.ToDateTime(row["create_date"]);

            // return the filled object
            return cet;
        }

        public int UpdateCustomEmailTemplate(CustomEmailTemplate cet, SqlInterface si)
        {
            int result = int.MinValue;

            string storedProcName = "es_update_custom_email_template";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@custom_email_template_id", DbType.Int32, DBValue.ToDBInt32(cet.custom_email_template_id)));
                paramCol.Add(new SqlDataParameter("@touch_info_id", DbType.Int32, DBValue.ToDBInt32(cet.touch_info_id)));
                paramCol.Add(new SqlDataParameter("@subject", DbType.String, DBValue.ToDBString(cet.subject)));
                paramCol.Add(new SqlDataParameter("@body_txt", DbType.String, DBValue.ToDBString(cet.body_txt)));
                paramCol.Add(new SqlDataParameter("@body_html", DbType.String, DBValue.ToDBString(cet.body_html)));
                paramCol.Add(new SqlDataParameter("@create_date", DbType.DateTime, DBValue.ToDBDateTime(cet.create_date)));
            
                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                result = si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

                if (result > 0)
                {
                    return result;
                }
                else
                {
                    throw new SqlDataException("Error updating database calling " + storedProcName);
                }

            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return result;
        }

        private touch_info LoadTouch_info(DataRow row)
        {
            touch_info ti = new touch_info();

            // Store database values into our business object
            ti.touch_info_id = DBValue.ToInt32(row["touch_info_id"]);
            ti.launch_date = DBValue.ToDateTime(row["launch_date"]);
            ti.business_rule_id = DBValue.ToInt32(row["business_rule_id"]);
            ti.visitor_log_id = DBValue.ToInt32(row["visitor_log_id"]);
            ti.create_date = DBValue.ToDateTime(row["create_date"]);
            ti.reminder_interval_day = DBValue.ToInt32(row["reminder_interval_day"]);

            // return the filled object
            return ti;
        }

        public void UpdateTouchInfo(touch_info ti)
        {
            bool useTransaction = false;
            string storedProcName = "es_update_only_touch_info";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@visitor_log_id", DbType.Int32, DBValue.ToDBInt32(ti.visitor_log_id)));
                
                if (ti.launch_date == DateTime.MinValue)
                {
                    paramCol.Add(new SqlDataParameter("@launch_date", DbType.DateTime, DBValue.ToDBDateTime(DateTime.Now)));
                }
                else
                {
                    paramCol.Add(new SqlDataParameter("@launch_date", DbType.DateTime, DBValue.ToDBDateTime(ti.launch_date)));
                }

                paramCol.Add(new SqlDataParameter("@business_rule_id", DbType.Int32, DBValue.ToDBInt32(ti.business_rule_id)));
                paramCol.Add(new SqlDataParameter("@reminder_interval_day", DbType.Int32, DBValue.ToDBInt32(ti.reminder_interval_day)));

                paramCol.Add(new SqlDataParameter("@touch_info_id", DbType.Int32, DBValue.ToDBInt32(ti.touch_info_id)));

                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                // Fetch and store into database.
                si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);



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
        }

        public EmailContactManager GetEmail(int parent_member_hierarchy_id, int event_id, SqlInterface si)
        {
            EmailContactManager emb = new EmailContactManager();

            string storedProcName = "es_get_custom_Email_and_touch_by_parent_member_hierarchy_id";

            // if the SqlInterface is passed as argument it means that 
            // this call should be applied to an already open connection
            // and the method which call this method is using transaction
            bool newConnection = true;
            if (si == null)
            {
                si = new SqlInterface(dataProvider, connectionString);
            }
            else
            {
                newConnection = false;
            }

            try
            {
                // declare stored procedure parameters
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }
                paramCol.Add(new SqlDataParameter("@parent_member_hierarchy_id", DbType.Int32, DBValue.ToDBInt32(parent_member_hierarchy_id)));
                if (event_id != int.MinValue)
                    paramCol.Add(new SqlDataParameter("@event_id", DbType.Int32, DBValue.ToDBInt32(event_id)));

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);
         
                if (dt != null)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        // fill our objects
                        try
                        {
                            DataRow row = dt.Rows[i];

                            emb.addEmail(LoadTouch(row), LoadTouch_info(row), LoadCustomEmailTemplate(row), LoadMemberType(row) , Convert.ToInt32(row["member_hierarchy_id"]));

                        }
                        catch (Exception ex)
                        {
                            throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                        }
                    }
                }
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return emb;
        }

        #endregion

        #region Email Template Tag
        private Tag LoadTag(DataRow row)
        {
            Tag tag = new Tag();
            tag.TagID = DBValue.ToInt32(row["tag_id"]);
            tag.StartTagName = DBValue.ToString(row["start_tag_name"]);
            tag.EndTagName = DBValue.ToString(row["end_tag_name"]);
            tag.Description = DBValue.ToString(row["description"]);
            return tag;
        }

        public List<Tag> GetTags()
        {
            List<Tag> tag = new List<Tag>();

            bool useTransaction = false;
            string storedProcName = "es_get_tag";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                si.Open();
                if (useTransaction)
                    si.BeginTransaction();

                // Fetch and store into database.
                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, null);

                if (dt.Rows.Count < 1)
                {
                    throw new SqlDataException("No records on " + storedProcName);
                }

                // fill our objects
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tag.Add(LoadTag(dt.Rows[i]));
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
            return tag;
        }

        public Tag GetTagByID(Int32 tag_id)
        {
            Tag tag = new Tag();

            bool useTransaction = false;
            string storedProcName = "es_get_tag_by_id";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@tag_id", DbType.Int32, DBValue.ToDBInt32(tag_id)));
                si.Open();
                if (useTransaction)
                    si.BeginTransaction();

                // Fetch and store into database.
                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt.Rows.Count < 1)
                {
                    throw new SqlDataException("No records on " + storedProcName);
                }

                // fill our objects
                try
                {
                    tag = LoadTag(dt.Rows[0]);
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
            return tag;
        }

        private EmailTemplateTag LoadEmailTemplateTag(DataRow row)
        {
            EmailTemplateTag ett = new EmailTemplateTag();
            ett.EmailTemplateID = DBValue.ToInt32(row["email_template_id"]);
            ett.ProductOfferID = DBValue.ToInt32(row["product_offer_id"]);
            ett.TagID = DBValue.ToInt32(row["tag_id"]);
            return ett;
        }

        public List<EmailTemplateTag> GetEmailTemplateTags(Int32 email_template_id, Int32 product_offer_id)
        {
            List<EmailTemplateTag> etts = new List<EmailTemplateTag>();

            bool useTransaction = false;
            string storedProcName = "es_get_email_template_tag";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@email_template_id", DbType.Int32, DBValue.ToDBInt32(email_template_id)));
                paramCol.Add(new SqlDataParameter("@product_offer_id", DbType.Int32, DBValue.ToDBInt32(product_offer_id)));
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
                        etts.Add(LoadEmailTemplateTag(row));
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
            return etts;
        }
        #endregion
    }
}
