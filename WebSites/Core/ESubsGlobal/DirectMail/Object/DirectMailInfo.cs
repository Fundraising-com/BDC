using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA.BDC.Core.ESubsGlobal.Users;

namespace GA.BDC.Core.ESubsGlobal.DirectMail.Object
{

    public class DirectMailInfo
    {
        public const int STATUS_NEW = 1;
        public const int STATUS_DRAFT = 2;
        public const int STATUS_MODERATED = 3;
        public const int STATUS_PROCESSED_SUCCESSFULLY = 4;
        public const int STATUS_PROCESSED_FAILED = 100;

        private int directMailInfoId = int.MinValue;
        private string message = null;
        private string imageUrl = null;
        private bool moderated = false;
        private int directMailStatus = int.MinValue;
        private DateTime createDate = DateTime.MinValue;
        private string documentPath = null;
        private int eventParticipationId = int.MinValue;
        private int memberHierarchyId = int.MinValue;

        public DirectMailInfo()
        {

        }

        #region Methods

        public static List<DirectMailInfo> GetDirectMailInfos()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailInfo();
        }

        public static List<DirectMailInfo> GetDirectMailInfosReadyToBeProcessed()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailInfosReadyToBeProcessed();
        }

        public static DirectMailInfo GetDirectMailsInfoById(int directMailId)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailInfoById(directMailId);
        }

        public static List<DirectMailInfo> GetDirectMailInfoByEventParticipationId(int eventParticipationId)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailInfoByEventParticipationId(eventParticipationId);
        }

        public List<DirectMail> GetDirectMails()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailsByDirectMailInfoId(this.directMailInfoId);
        }

        public bool Insert()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.InsertDirectMailInfo(null, this);
        }

        public bool Update()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.UpdateDirectMailInfo(null, this);
        }

        public bool Delete()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.DeleteDirectMailInfo(null, this);
        }

        public List<eSubsGlobalUser> GetRecipients()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailRecipients(this.directMailInfoId);
        }


        public void RemoveRecipient(eSubsGlobalUser user)
        { 
        
        }


        public static List<DirectMailInfo> GetDirectMailInfoSent(int eventParticipationId)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDirectMailInfoSent(eventParticipationId);
        }

        public static int GetNumberOfMailsSent(int eventParticipationId)
        {
            List<DirectMailInfo> directMailInfos = GetDirectMailInfoSent(eventParticipationId);
            return directMailInfos.Count;
        }


        #endregion

        #region Properties

        public int DirectMailInfoId
        {
            get { return directMailInfoId; }
            set { directMailInfoId = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        public bool Moderated
        {
            get { return moderated; }
            set { moderated = value; }
        }

        public int DirectMailStatus
        {
            get { return directMailStatus; }
            set { directMailStatus = value; }
        }

        public string DocumentPath
        {
            get { return documentPath; }
            set { documentPath = value; }
        }

        public string strRecipien 
        {
            get 
            {
                string result = "";
                List<eSubsGlobalUser> recipients = GetRecipients();

                foreach (eSubsGlobalUser recipient in recipients)
                {
                    if (result == string.Empty)
                    {
                        result += recipient.CompleteName;
                    }
                    else
                    {
                        result += ", " + recipient.CompleteName;
                    }
                    
                }
                if (result.Length > 200)
                {
                    result = result.Substring(0, 199) + " ...";
                }
                return result;
            }
        }


        public string strCreateDate { get { return createDate.ToString("MM/dd/yy"); } }

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        public int EventParticipationId
        {
            get { return eventParticipationId; }
            set { eventParticipationId = value; }
        }
        
        public int MemberHierarchyId
        {
            get { return memberHierarchyId; }
            set { memberHierarchyId = value; }
        }

        #endregion
    }
}
