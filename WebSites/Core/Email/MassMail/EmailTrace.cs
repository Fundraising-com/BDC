using System;
using System.Collections.Generic;
using System.Text;
using GA.BDC.Core.Email.MassMail.Data;

namespace GA.BDC.Core.Email.MassMail
{
    public class EmailTrace
    {
        #region Private Fields

        private int emailMonitorID;
        private int sourceID;
        private string subject;
        private int monitorActionID;
        private DateTime datestamp;
        private int projectID;
        private int completed;
        private string toEmail;

        #endregion

        #region Constructors

        public EmailTrace() { }

        #endregion

        #region Properties

        public int EmailMonitorID
        {
            get { return emailMonitorID; }
            set { emailMonitorID = value; }
        }

        public int SourceID
        {
            get { return sourceID; }
            set { sourceID = value; }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public int MonitorActionID
        {
            get { return monitorActionID; }
            set { monitorActionID = value; }
        }

        public DateTime DateStamp
        {
            get { return datestamp; }
            set { datestamp = value; }
        }

        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

        public int Completed
        {
            get { return completed; }
            set { completed = value; }
        }

        public string ToEmail
        {
            get { return toEmail; }
            set { toEmail = value; }
        }

        #endregion

        #region Public Methods

        public static void InsertEmailMonitor(string stringConnection, EmailTrace e)
        {
           MassMailDataInterface mDI = new MassMailDataInterface(stringConnection);
            mDI.EmailMonitorInsert(e);
        }

        public static List<EmailTrace> EmailMonitorEmailToTrace(string stringConnection, int completion, DateTime expire)
        {
            MassMailDataInterface mDI = new MassMailDataInterface(stringConnection);
            return  mDI.EmailMonitorEmailsToTrace(completion, expire);
        }

        public static Email GetEmailBySourceIDAndSourceID(string stringConnection, Int32 sourceID, Int16 projectID)
        {
            MassMailDataInterface mDI = new MassMailDataInterface( stringConnection);
            return mDI.EmailMonitorGetEmailBySourceAndProjectID(projectID, sourceID);
        }

        #endregion
    }
}
