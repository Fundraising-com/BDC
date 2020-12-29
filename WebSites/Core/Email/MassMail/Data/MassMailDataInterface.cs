//
// Dec 1, 2004. Stephen Lim - New class.
// Dec 21, 2004. Stephen Lim - Convert value types to SqlTypes.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.BusinessBase;
using GA.BDC.Core.Data.Sql;
using GA.BDC.Core.Diagnostics;

//using efundraising.EnterpriseComponents;
//using efundraising.Database;

namespace GA.BDC.Core.Email.MassMail.Data
{
	/// <summary>
	/// Easily access various database related tasks with transaction support.
	/// </summary>
	/// <remarks>Instance members are not thread-safe. 
	/// Transaction support is only safe for connections bound to the same physical server.
	/// Nested transaction is not supported.
	/// </remarks>
	public sealed class MassMailDataInterface : DatabaseObject
	{
		#region Constructors
		/// <summary>
		/// Constructor for the class.
		/// </summary>
		public MassMailDataInterface(string connectionString)
		{

			SetConnectionString(connectionString);

		}

      #endregion

      #region Methods

      /// <summary>
      /// Insert email into queue for mailing.
      /// </summary>
      public void AmazonInsertEmailQueue(Email e)
      {
         bool useTransaction = false;
         string storedProcName = "bdc_insert_email_queue";
         SqlInterface si = new SqlInterface(dataProvider, connectionString);

         try
         {
            SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
            paramCol.Add(new SqlDataParameter("@source_id", DbType.Int32, DBValue.ToDBInt32(e.SourceId)));
            paramCol.Add(new SqlDataParameter("@project_id", DbType.Int16, DBValue.ToDBInt16(e.ProjectId)));
            paramCol.Add(new SqlDataParameter("@reply_to_name", DbType.String, DBValue.ToDBString(e.ReplyToName)));
            paramCol.Add(new SqlDataParameter("@reply_to_email", DbType.String, DBValue.ToDBString(e.ReplyToEmail)));
            paramCol.Add(new SqlDataParameter("@to_name", DbType.String, DBValue.ToDBString(e.ToName)));
            paramCol.Add(new SqlDataParameter("@to_email", DbType.String, DBValue.ToDBString(e.ToEmail)));
            paramCol.Add(new SqlDataParameter("@from_email", DbType.String, DBValue.ToDBString(e.FromEmail)));
            paramCol.Add(new SqlDataParameter("@from_name", DbType.String, DBValue.ToDBString(e.FromName)));
            paramCol.Add(new SqlDataParameter("@bounce_email", DbType.String, DBValue.ToDBString(e.ReturnPathEmail)));
            paramCol.Add(new SqlDataParameter("@bounce_name", DbType.String, DBValue.ToDBString(e.ReturnPathName)));
            paramCol.Add(new SqlDataParameter("@cc_email", DbType.String, DBValue.ToDBString(e.CcEmail)));
            paramCol.Add(new SqlDataParameter("@cc_name", DbType.String, DBValue.ToDBString(e.CcName)));
            paramCol.Add(new SqlDataParameter("@bcc_email", DbType.String, DBValue.ToDBString(e.BccEmail)));
            paramCol.Add(new SqlDataParameter("@bcc_name", DbType.String, DBValue.ToDBString(e.BccName)));
            paramCol.Add(new SqlDataParameter("@subject", DbType.String, DBValue.ToDBString(e.Subject)));
            paramCol.Add(new SqlDataParameter("@bodytxt", DbType.String, DBValue.ToDBString(e.TextBody)));
            paramCol.Add(new SqlDataParameter("@bodyhtml", DbType.String, DBValue.ToDBString(e.HtmlBody)));
            paramCol.Add(new SqlDataParameter("@extra_info", DbType.String, DBValue.ToDBString(e.ExtraInfo.GetXmlString())));
            paramCol.Add(new SqlDataParameter("@ret", DbType.Int32, ParameterDirection.ReturnValue));

            si.Open();

            if (useTransaction)
               si.BeginTransaction();

            // Execute query
            si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

            // Throw error is return value is not 0
            if ((int)paramCol["@ret"].Value != 0)
               throw new SqlDataException("Error inserting into database.");

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

	
		/// <summary>
		/// Get the list of emails queued for mailing.
		/// </summary>
		/// <returns>EmailCollection.</returns>
		public EmailCollection GetEmailQueue()
		{
			EmailCollection emails = new EmailCollection();

			bool useTransaction = false;
			string storedProcName = "get_email_queue";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				si.Open();

				if(useTransaction)
					si.BeginTransaction();

		
				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, null);

				// fill our objects
				try 
				{
					foreach (DataRow row in dt.Rows)
					{
						Email email = new Email();
						try 
						{
							email.QueueId = DBValue.ToInt32(row["queue_id"]);
							email.SourceId = DBValue.ToInt32(row["source_id"]);
							email.ProjectId = DBValue.ToInt16(row["project_id"]);
							email.ReplyToName = DBValue.ToString(row["reply_to_name"]);
							email.ReplyToEmail = DBValue.ToString(row["reply_to_email"]);
							email.ToName = DBValue.ToString(row["to_name"]);
							email.ToEmail = DBValue.ToString(row["to_email"]);
							email.FromEmail = DBValue.ToString(row["from_email"]);
							email.FromName = DBValue.ToString(row["from_name"]);
							email.ReturnPathEmail = DBValue.ToString(row["bounce_email"]);
							email.ReturnPathName = DBValue.ToString(row["bounce_name"]);
							email.CcEmail = DBValue.ToString(row["cc_email"]);
							email.CcName = DBValue.ToString(row["cc_name"]);
							email.BccEmail = DBValue.ToString(row["bcc_email"]);
							email.BccName = DBValue.ToString(row["bcc_name"]);
							email.Subject = DBValue.ToString(row["subject"]);
							email.TextBody = DBValue.ToString(row["bodytxt"]);
							email.HtmlBody = DBValue.ToString(row["bodyhtml"]);
							emails.Add(email);
						}
						catch (Exception ex)
						{
							try 
							{
								// Catch and signal error so the collection can be filled
								// for the remaining emails.
								email.SentStatus = EmailQueue.EmailSentStatus.MailError;
								email.Message = ex.Message;
								UpdateStatus(email);
							}
							catch {}
						}
					}
				} 
				catch(System.Exception ex) 
				{
					throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
				}

				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
			return emails;
		}

		/// <summary>
		/// Insert email into queue for mailing.
		/// </summary>
		public void InsertEmailQueue(Email e)
		{
			bool useTransaction = false;
			string storedProcName = "email_queue_insert";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@source_id", DbType.Int32, DBValue.ToDBInt32(e.SourceId)));
				paramCol.Add(new SqlDataParameter("@project_id", DbType.Int16, DBValue.ToDBInt16(e.ProjectId)));
				paramCol.Add(new SqlDataParameter("@reply_to_name", DbType.String, DBValue.ToDBString(e.ReplyToName)));
				paramCol.Add(new SqlDataParameter("@reply_to_email", DbType.String, DBValue.ToDBString(e.ReplyToEmail)));
				paramCol.Add(new SqlDataParameter("@to_name", DbType.String, DBValue.ToDBString(e.ToName)));
				paramCol.Add(new SqlDataParameter("@to_email", DbType.String, DBValue.ToDBString(e.ToEmail)));
				paramCol.Add(new SqlDataParameter("@from_email", DbType.String, DBValue.ToDBString(e.FromEmail)));
				paramCol.Add(new SqlDataParameter("@from_name", DbType.String, DBValue.ToDBString(e.FromName)));
				paramCol.Add(new SqlDataParameter("@bounce_email", DbType.String, DBValue.ToDBString(e.ReturnPathEmail)));
				paramCol.Add(new SqlDataParameter("@bounce_name", DbType.String, DBValue.ToDBString(e.ReturnPathName)));
				paramCol.Add(new SqlDataParameter("@cc_email", DbType.String, DBValue.ToDBString(e.CcEmail)));
				paramCol.Add(new SqlDataParameter("@cc_name", DbType.String, DBValue.ToDBString(e.CcName)));
				paramCol.Add(new SqlDataParameter("@bcc_email", DbType.String, DBValue.ToDBString(e.BccEmail)));
				paramCol.Add(new SqlDataParameter("@bcc_name", DbType.String, DBValue.ToDBString(e.BccName)));
				paramCol.Add(new SqlDataParameter("@subject", DbType.String, DBValue.ToDBString(e.Subject)));
				paramCol.Add(new SqlDataParameter("@bodytxt", DbType.String, DBValue.ToDBString(e.TextBody)));
				paramCol.Add(new SqlDataParameter("@bodyhtml", DbType.String, DBValue.ToDBString(e.HtmlBody)));
				paramCol.Add(new SqlDataParameter("@extra_info", DbType.String, DBValue.ToDBString(e.ExtraInfo.GetXmlString())));
				paramCol.Add(new SqlDataParameter("@ret", DbType.Int32, ParameterDirection.ReturnValue));

				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Execute query
				si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);

				// Throw error is return value is not 0
				if ((int) paramCol["@ret"].Value != 0)
					throw new SqlDataException("Error inserting into database.");

				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
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


		/// <summary>
		/// Set the sent status for the email.
		/// </summary>
		/// <param name="e">Email</param>
		public void UpdateStatus(Email e) 
		{
			bool useTransaction = false;
			string storedProcName = "email_queue_update";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@queue_id", DbType.Int32, DBValue.ToDBInt32(e.QueueId)));
				paramCol.Add(new SqlDataParameter("@sent_status_id", DbType.Int32, DBValue.ToDBInt32((int) e.SentStatus)));
				paramCol.Add(new SqlDataParameter("@error", DbType.String, DBValue.ToDBString(e.Message)));

				si.Open();
				if(useTransaction)
					si.BeginTransaction();

		
				// Fetch and store into database.
				int ret = si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);

				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
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
		#endregion Methods

		#region Methods for Komunikator

		/// <summary>
		/// Get the list of emails queued for mailing.
		/// </summary>
		/// <returns>EmailCollection.</returns>
		public EmailCollection KomunikatorGetEmailQueue()
		{
			EmailCollection komunikatorEmails = new EmailCollection();

			bool useTransaction = false;
			string storedProcName = "esk_get_email_queue";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				si.Open();

				if(useTransaction)
					si.BeginTransaction();

		
				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, null);

				// fill our objects
				try 
				{
					foreach (DataRow row in dt.Rows)
					{
						Email email = new Email();
						try 
						{
							email.QueueId = DBValue.ToInt32(row["queue_id"]);
							email.SourceId = DBValue.ToInt32(row["source_id"]);
							email.ProjectId = DBValue.ToInt16(row["project_id"]);
							email.ReplyToName = DBValue.ToString(row["reply_to_name"]);
							email.ReplyToEmail = DBValue.ToString(row["reply_to_email"]);
							email.ToName = DBValue.ToString(row["to_name"]);
							email.ToEmail = DBValue.ToString(row["to_email"]);
							email.FromEmail = DBValue.ToString(row["from_email"]);
							email.FromName = DBValue.ToString(row["from_name"]);
							email.ReturnPathEmail = DBValue.ToString(row["bounce_email"]);
							email.ReturnPathName = DBValue.ToString(row["bounce_name"]);
							email.CcEmail = DBValue.ToString(row["cc_email"]);
							email.CcName = DBValue.ToString(row["cc_name"]);
							email.BccEmail = DBValue.ToString(row["bcc_email"]);
							email.BccName = DBValue.ToString(row["bcc_name"]);
							email.Subject = DBValue.ToString(row["subject"]);
							email.TextBody = DBValue.ToString(row["bodytxt"]);
							email.HtmlBody = DBValue.ToString(row["bodyhtml"]);
							email.ExtraInfo.LoadFromXmlString(DBValue.ToString(row["extra_info"]));
							email.SentStatus = EmailQueue.EmailSentStatus.Komunik;
							email.Datestamp = new DateTime();
							email.Priority = DBValue.ToInt16(row["priority"]);
                            email.KomunikReturnValueId = (EmailQueue.KomunikReturnValue)DBValue.ToInt16(row["komunik_return_value_id"]);
							//email.KomunikReturnValueId = EmailQueue.KomunikReturnValue.Pending;
							komunikatorEmails.Add(email);
						}
						catch (Exception ex)
						{
							try 
							{
								// Catch and signal error so the collection can be filled
								// for the remaining emails.
								email.KomunikReturnValueId = EmailQueue.KomunikReturnValue.SmtpError; //Unable to fill object
								email.Message = ex.Message;
								KomunikatorUpdateStatus(email);
							}
							catch {}
						}
					}
				} 
				catch(System.Exception ex) 
				{
					throw new SqlDataException("Unable to fill object using " + storedProcName, ex);
				}

				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
					si.Rollback(); 

				// throw exception
				throw;
			} 
			finally 
			{
				// Always close connection.
				si.Close();
			}
			return komunikatorEmails;
		}

		/// <summary>
		/// Insert email into queue for mailing.
		/// </summary>
		public void KomunikatorInsertEmailQueue(Email e)
		{
			bool useTransaction = false;
			string storedProcName = "esk_insert_email_queue";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@source_id", DbType.Int32, DBValue.ToDBInt32(e.SourceId)));
				paramCol.Add(new SqlDataParameter("@project_id", DbType.Int16, DBValue.ToDBInt16(e.ProjectId)));
				paramCol.Add(new SqlDataParameter("@reply_to_name", DbType.String, DBValue.ToDBString(e.ReplyToName)));
				paramCol.Add(new SqlDataParameter("@reply_to_email", DbType.String, DBValue.ToDBString(e.ReplyToEmail)));
				paramCol.Add(new SqlDataParameter("@to_name", DbType.String, DBValue.ToDBString(e.ToName)));
				paramCol.Add(new SqlDataParameter("@to_email", DbType.String, DBValue.ToDBString(e.ToEmail)));
				paramCol.Add(new SqlDataParameter("@from_email", DbType.String, DBValue.ToDBString(e.FromEmail)));
				paramCol.Add(new SqlDataParameter("@from_name", DbType.String, DBValue.ToDBString(e.FromName)));
				paramCol.Add(new SqlDataParameter("@bounce_email", DbType.String, DBValue.ToDBString(e.ReturnPathEmail)));
				paramCol.Add(new SqlDataParameter("@bounce_name", DbType.String, DBValue.ToDBString(e.ReturnPathName)));
				paramCol.Add(new SqlDataParameter("@cc_email", DbType.String, DBValue.ToDBString(e.CcEmail)));
				paramCol.Add(new SqlDataParameter("@cc_name", DbType.String, DBValue.ToDBString(e.CcName)));
				paramCol.Add(new SqlDataParameter("@bcc_email", DbType.String, DBValue.ToDBString(e.BccEmail)));
				paramCol.Add(new SqlDataParameter("@bcc_name", DbType.String, DBValue.ToDBString(e.BccName)));
				paramCol.Add(new SqlDataParameter("@subject", DbType.String, DBValue.ToDBString(e.Subject)));
				paramCol.Add(new SqlDataParameter("@bodytxt", DbType.String, DBValue.ToDBString(e.TextBody)));
				paramCol.Add(new SqlDataParameter("@bodyhtml", DbType.String, DBValue.ToDBString(e.HtmlBody)));
				paramCol.Add(new SqlDataParameter("@extra_info", DbType.String, DBValue.ToDBString(e.ExtraInfo.GetXmlString())));
				paramCol.Add(new SqlDataParameter("@ret", DbType.Int32, ParameterDirection.ReturnValue));

				si.Open();

				if(useTransaction)
					si.BeginTransaction();
		
				// Execute query
				si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);

				// Throw error is return value is not 0
				if ((int) paramCol["@ret"].Value != 0)
					throw new SqlDataException("Error inserting into database.");

				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
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


		/// <summary>
		/// Set the sent status for the email.
		/// </summary>
		/// <param name="e">Email</param>
		public void KomunikatorUpdateStatus(Email e) 
		{
			bool useTransaction = false;
			string storedProcName = "esk_update_email_queue";
			SqlInterface si = new SqlInterface(dataProvider, connectionString);

			try 
			{
				SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
				paramCol.Add(new SqlDataParameter("@queue_id", DbType.Int32, DBValue.ToDBInt32(e.QueueId)));
				paramCol.Add(new SqlDataParameter("@komunik_return_value_id", DbType.Int32, DBValue.ToDBInt32((int)e.KomunikReturnValueId)));
				paramCol.Add(new SqlDataParameter("@error", DbType.String, DBValue.ToDBString(e.Message)));

				si.Open();
				if(useTransaction)
					si.BeginTransaction();

		
				// Fetch and store into database.
				int ret = si.ExecuteNonQuery( storedProcName, CommandType.StoredProcedure, paramCol);

				// Commit our transaction.
				if(useTransaction) 
					si.Commit();
			} 
			catch 
			{
				// Rollback on error.
				if(useTransaction)
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


		
		public string GetEmailContent(string touchIDs, string emailType)
		{
			if (touchIDs == null || touchIDs.Length <1)
				return null;
			string result = string.Empty;
			SqlInterface si = new SqlInterface(dataProvider, connectionString);
			try
			{
				if (touchIDs != null && touchIDs != string.Empty)
				{
					SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
					SqlDataParameter sqlPam = null;
					sqlPam = new SqlDataParameter("@touch_ids", DbType.String, DBValue.ToDBString (touchIDs));
					paramCol.Add(sqlPam);
					si.Open();
					string sqlText = "SELECT bodyhtml FROM EMAIL_QUEUE WHERE PROJECT_ID=2 AND SOURCE_ID =@touch_ids";
					if (emailType.ToLower() == "txt")
						sqlText = "SELECT bodytxt FROM EMAIL_QUEUE WHERE PROJECT_ID=2 AND SOURCE_ID =@touch_ids";

					result = (string)si.ExecuteScalar(sqlText, CommandType.Text, paramCol);
				}
			}
				//			catch (Exception ex)
				//			{
				//				throw new Exception(ex.ToString(), ex);
				//			}
			finally
			{
				si.Close ();
			}
			return result;
		}



		public DataTable GetEmailsFromTouchIDs(string[] touchIDs)
		{
			if (touchIDs == null || touchIDs.Length <1)
				return null;

			DataTable dt = null;
			SqlInterface si = new SqlInterface(dataProvider, connectionString);
			try
			{
				string listTouchID = string.Empty;
				for (int i=0; i < touchIDs.Length; i++)
				{
					listTouchID += ","  + touchIDs[i];
				}

				if (listTouchID != string.Empty)
				{
					listTouchID = string.Format("({0})", listTouchID.Substring(1));
					SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
//					SqlDataParameter sqlPam = null;
//					if (listTouchID == null || DBNull.Equals (listTouchID, DBNull.Value) || listTouchID == string.Empty)
//						sqlPam = new SqlDataParameter("@touch_ids", DbType.String, DBNull.Value);
//					else
//						sqlPam = new SqlDataParameter("@touch_ids", DbType.String, DBValue.ToDBString (listTouchID));
//					paramCol.Add(sqlPam);

					dt = si.ExecuteFetchDataTable(
string.Format( @"SELECT FROM_NAME, FROM_EMAIL, SUBJECT, SOURCE_ID, DATESTAMP
FROM EMAIL_QUEUE
WHERE PROJECT_ID=2
AND SOURCE_ID IN {0}", listTouchID)
				, 
						CommandType.Text, paramCol);
				}
			}
//			catch (Exception ex)
//			{
//				throw new Exception(ex.ToString(), ex);
//			}
			finally
			{
				si.Close ();
			}
			return dt;
		}

        /// <summary>
        /// Insert trace email into Email Monitor Table
        /// </summary>
        public void EmailMonitorInsert(EmailTrace e)
        {
            bool useTransaction = false;
            string storedProcName = "email_monitor_insert";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@source_id", DbType.Int32, DBValue.ToDBInt32(e.SourceID)));
                paramCol.Add(new SqlDataParameter("@subject", DbType.String, DBValue.ToDBString(e.Subject)));
                paramCol.Add(new SqlDataParameter("@monitor_action_id", DbType.Int32, DBValue.ToDBInt32(e.MonitorActionID)));
                paramCol.Add(new SqlDataParameter("@datestamp", DbType.DateTime, DBValue.ToDBDateTime(e.DateStamp)));
                paramCol.Add(new SqlDataParameter("@project_id", DbType.Int16, DBValue.ToDBInt16(e.ProjectID)));
                paramCol.Add(new SqlDataParameter("@completed", DbType.Int16, DBValue.ToDBInt16(e.Completed)));
                paramCol.Add(new SqlDataParameter("@to_email", DbType.String, DBValue.ToDBString(e.ToEmail)));
                paramCol.Add(new SqlDataParameter("@ret", DbType.Int32, ParameterDirection.ReturnValue));
                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

                // Execute query
                si.ExecuteNonQuery(storedProcName, CommandType.StoredProcedure, paramCol);

                // Throw error is return value is not 0
                if ((int)paramCol["@ret"].Value != 0)
                    throw new SqlDataException("Error inserting into database.");

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

        /// <summary>
        /// Insert trace email into Email Monitor Table
        /// </summary>
        public List<EmailTrace> EmailMonitorEmailsToTrace(int completed, DateTime expireDate)
        {
            bool useTransaction = false;
            string storedProcName = "email_monitor_email_to_trace";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@status", DbType.Int32, DBValue.ToDBInt32(completed)));
                paramCol.Add(new SqlDataParameter("@expiry_date", DbType.DateTime, DBValue.ToDateTime(expireDate)));
                si.Open();

                if (useTransaction)
                    si.BeginTransaction();

              		
				// Fetch and store into database.
				DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);
                List<EmailTrace> emailTrace = new List<EmailTrace>();
				foreach (DataRow row in dt.Rows)
				{
                    EmailTrace temp = new EmailTrace();
						
                    temp.EmailMonitorID = DBValue.ToInt32(row["email_monitor_id"]);
                    temp.SourceID = DBValue.ToInt32(row["source_id"]);
                    temp.Subject = DBValue.ToString(row["subject"]);
                    temp.MonitorActionID = DBValue.ToInt16(row["monitor_action_id"]);
                    temp.DateStamp = DBValue.ToDateTime(row["datestamp"]);
                    temp.ProjectID = DBValue.ToInt16(row["project_id"]);
                    temp.Completed = DBValue.ToInt16(row["completed"]);
                    temp.ToEmail = DBValue.ToString(row["to_email"]);

                    emailTrace.Add(temp);
                      
				}
                return emailTrace;
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

        public Email EmailMonitorGetEmailBySourceAndProjectID(Int16 project_id, Int32 source_id)
        {
            Email email = null;

            string storedProcName = "email_monitor_get_email_by_project_id_and_source_id";
            SqlInterface si = new SqlInterface(dataProvider, connectionString);

            try
            {
                SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
                paramCol.Add(new SqlDataParameter("@project_id", DbType.Int16, DBValue.ToDBInt32(project_id)));
                paramCol.Add(new SqlDataParameter("@source_id", DbType.Int32, DBValue.ToDBInt32(source_id)));

                si.Open();

                DataTable dt = si.ExecuteFetchDataTable(storedProcName, CommandType.StoredProcedure, paramCol);
                if (dt != null && dt.Rows.Count > 0)
                {
                    email = new Email();
                    DataRow row = dt.Rows[0];
                    email.QueueId = DBValue.ToInt32(row["queue_id"]);
                    email.SourceId = DBValue.ToInt32(row["source_id"]);
                    email.ProjectId = DBValue.ToInt16(row["project_id"]);
                    email.ReplyToName = DBValue.ToString(row["reply_to_name"]);
                    email.ReplyToEmail = DBValue.ToString(row["reply_to_email"]);
                    email.ToName = DBValue.ToString(row["to_name"]);
                    email.ToEmail = DBValue.ToString(row["to_email"]);
                    email.FromEmail = DBValue.ToString(row["from_email"]);
                    email.FromName = DBValue.ToString(row["from_name"]);
                    email.ReturnPathEmail = DBValue.ToString(row["bounce_email"]);
                    email.ReturnPathName = DBValue.ToString(row["bounce_name"]);
                    email.CcEmail = DBValue.ToString(row["cc_email"]);
                    email.CcName = DBValue.ToString(row["cc_name"]);
                    email.BccEmail = DBValue.ToString(row["bcc_email"]);
                    email.BccName = DBValue.ToString(row["bcc_name"]);
                    email.Subject = DBValue.ToString(row["subject"]);
                    email.TextBody = DBValue.ToString(row["bodytxt"]);
                    email.HtmlBody = DBValue.ToString(row["bodyhtml"]);
                    email.ExtraInfo.LoadFromXmlString(DBValue.ToString(row["extra_info"]));
                    email.SentStatus = EmailQueue.EmailSentStatus.Komunik;
                    email.Datestamp = DBValue.ToDateTime(row["datestamp"]);
                    email.Priority = DBValue.ToInt16(row["priority"]);
                    email.KomunikReturnValueId = (EmailQueue.KomunikReturnValue)DBValue.ToInt16(row["komunik_return_value_id"]);

                }
                return email;
            }
            catch(Exception ex)
            {
                 // throw exception
                throw ex;
            }
            finally
            {
                // Always close connection.
                si.Close();
            }
           
        }
		#endregion
	}
}
