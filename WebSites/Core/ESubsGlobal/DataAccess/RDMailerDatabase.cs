using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GA.BDC.Core.ESubsGlobal.RDMailer;
using GA.BDC.Core.BusinessBase;
using GA.BDC.Core.Data.Sql;

namespace GA.BDC.Core.ESubsGlobal.DataAccess
{
    public class RDMailerDatabase : ESubsGlobalDatabase
    {
        public RDMailerDatabase() { }

        #region Email Activity

        public List<EmailActivity> GetEmailActivityByTouchID(int touch_id, int project_id, int action_id)
        {
            return GetEmailActivityByTouchID(touch_id, project_id, action_id, null);
        }
        private List<EmailActivity> GetEmailActivityByTouchID(int touch_id, int project_id, int action_id, SqlInterface si)
        {
            List<EmailActivity> emailActivity = new List<EmailActivity>();

            string storedProcName = "es_get_email_activity_by_touch_id";

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
                paramCol.Add(new SqlDataParameter("@touch_id ", DbType.Int32, DBValue.ToDBInt32(touch_id)));
                paramCol.Add(new SqlDataParameter("@project_id ", DbType.Int32, DBValue.ToDBInt32(project_id)));
                paramCol.Add(new SqlDataParameter("@action_id ", DbType.Int32, DBValue.ToDBInt32(action_id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            emailActivity.Add(LoadEmailActivity(row));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return emailActivity;
        }

        public List<EmailActivity> GetEmailActivityByTouchInfoID(int touch_info_id, int project_id, int action_id)
        {
            return GetEmailActivityByTouchInfoID(touch_info_id, project_id, action_id, null);
        }
        private List<EmailActivity> GetEmailActivityByTouchInfoID(int touch_info_id, int project_id, int action_id, SqlInterface si)
        {
            List<EmailActivity> emailActivity = new List<EmailActivity>();

            string storedProcName = "es_get_email_activity_by_touch_info_id";

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
                paramCol.Add(new SqlDataParameter("@touch_info_id ", DbType.Int32, DBValue.ToDBInt32(touch_info_id)));
                paramCol.Add(new SqlDataParameter("@project_id ", DbType.Int32, DBValue.ToDBInt32(project_id)));
                paramCol.Add(new SqlDataParameter("@action_id ", DbType.Int32, DBValue.ToDBInt32(action_id)));

                if (newConnection)
                {
                    // open the connection
                    si.Open();
                }

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // fill our objects
                    try
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            emailActivity.Add(LoadEmailActivity(row));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
            }
            finally
            {
                if (newConnection)
                {
                    // Always close connection.
                    si.Close();
                }
            }
            return emailActivity;
        }

        private EmailActivity LoadEmailActivity(DataRow row)
        {
            EmailActivity ea = new EmailActivity();

            // Store database values into our business object
            ea.EmailActivityId = DBValue.ToInt32(row["email_activity_id"]);
            ea.TouchId = DBValue.ToInt32(row["touch_id"]);
            ea.ProjectId = DBValue.ToInt32(row["project_id"]);
            ea.EmailTemplateId = DBValue.ToInt32(row["email_template_id"]);
            ea.EmailActivityDate = DBValue.ToDateTime(row["email_activity_date"]);
            ea.ActionId = DBValue.ToInt32(row["action_id"]);
            ea.ActionDesc = DBValue.ToString(row["action_desc"]);
            ea.BatchId = DBValue.ToInt32(row["batch_id"]);
            ea.CreateDate = DBValue.ToDateTime(row["create_date"]);

            return ea;
        }

        #endregion
    }
}
