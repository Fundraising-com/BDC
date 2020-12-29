using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.Core.ESubsGlobal
{

    public class WebAction
    {
        public const int TYPE_EMAIL_POPUP_REMIND_ME_LATER_EMAILS = 1;
        public const int TYPE_EMAIL_POPUP_NO_THANKS = 2;
        public const int TYPE_EMAIL_POPUP_CLOSE = 3;

        public const int TYPE_DIRECT_MAIL_POPUP_REMIND_ME_LATER_EMAILS = 101;
        public const int TYPE_DIRECT_MAIL_POPUP_NO_THANKS = 102;
        public const int TYPE_DIRECT_MAIL_POPUP_CLOSE = 103;

        private int webActionId = int.MinValue;
        private int eventParticipationId = int.MinValue;
        private int memberHierarchyId = int.MinValue;
        private int type = int.MinValue;
        private String value = null;
        private DateTime createDate = DateTime.MinValue;

        public WebAction()
        {

        }

        #region Methods

        public static List<WebAction> GetWebActions()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetWebActions();
        }

        public static WebAction GetWebActionById(int webActionId)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetWebActionById(webActionId);
        }

        public static WebAction GetWebActionById(int eventParticipationId, int type)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetWebActionByEventParticipationId(eventParticipationId, type);
        }

        public bool Insert()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.InsertWebAction(null, this);
        }

        public bool Update()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.UpdateWebAction(null, this);
        }

        public bool HasTimedOut()
        {
            return CreateDate > DateTime.Now.AddDays(3);
        }

        #endregion

        #region Properties

        public int WebActionId
        {
            get { return webActionId; }
            set { webActionId = value; }
        }

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

        public String Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        #endregion
    }
}
