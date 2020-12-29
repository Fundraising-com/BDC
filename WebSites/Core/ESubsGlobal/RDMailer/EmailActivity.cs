using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.RDMailer
{
    public enum Project
    {
        ESubsTouch = 2,
        ESubsTellAFriend = 3,
        ESubsReporting = 4,
        PHPSendMail = 6,
        MajorDomo = 7
    }

    public enum KomunikAction
    {
        MessageEnvoye =	1,
        MessageOuvert =	2,
        HardBounce = 4,
        ClickDeLien = 5,
        Unsubscribe6 = 6,
        SoftBounce = 8,
        ReplyFaitAuCourriel = 15,
        UnableToProcess	= 101,
        InQueue	= 102,
        Delivered = 103,
        Rescheduled = 104,
        PermanentBounce = 105,
        TemporaryBounce = 106,
        SpamFilteringBounce = 107,
        Open = 108,
        Clicks = 109,
        Reply = 110,
        WebviewRequest = 111,
        Subscription =112, 
        UpdateProfile = 113, 
        UnsubscriptionPageRequest = 114,
        Unsubscribe115 = 115,
        ResubscriptionPageRequest = 116,
        Resubscribe = 117,
        ClickOnViralLink = 118,
        ViralFormSubmitted = 119,
        ContactsReferred = 120,
        WebpageDelivered = 121,
        UnknownAtThisMoment = 127
    }

    public class EmailActivity : EnvironmentBase
    {
        private int _emailActivityId;
        private int _touchId;
        private int _projectId;
        private int _emailTemplateId;
        private DateTime _emailActivityDate;
        private int _actionId;
        private string _actionDesc;
        private int _batchId;
        private DateTime _createDate;

        public EmailActivity() : this(int.MinValue) { }
        public EmailActivity(int emailActivityId) : this(emailActivityId, int.MinValue) { }
        public EmailActivity(int emailActivityId, int touchId) : this(emailActivityId, touchId, int.MinValue) { }
        public EmailActivity(int emailActivityId, int touchId, int projectId) : this(emailActivityId, touchId, projectId, int.MinValue) { }
        public EmailActivity(int emailActivityId, int touchId, int projectId, int emailTemplateId) : this(emailActivityId, touchId, projectId, emailTemplateId, DateTime.MinValue) { }
        public EmailActivity(int emailActivityId, int touchId, int projectId, int emailTemplateId, DateTime emailActivityDate) : this(emailActivityId, touchId, projectId, emailTemplateId, emailActivityDate, int.MinValue) { }
        public EmailActivity(int emailActivityId, int touchId, int projectId, int emailTemplateId, DateTime emailActivityDate, int actionId) : this(emailActivityId, touchId, projectId, emailTemplateId, emailActivityDate, actionId, null) { }
        public EmailActivity(int emailActivityId, int touchId, int projectId, int emailTemplateId, DateTime emailActivityDate, int actionId, string actionDesc) : this(emailActivityId, touchId, projectId, emailTemplateId, emailActivityDate, actionId, actionDesc, int.MinValue) { }
        public EmailActivity(int emailActivityId, int touchId, int projectId, int emailTemplateId, DateTime emailActivityDate, int actionId, string actionDesc, int batchId) : this(emailActivityId, touchId, projectId, emailTemplateId, emailActivityDate, actionId, actionDesc, batchId, DateTime.MinValue) { }
        public EmailActivity(int emailActivityId, int touchId, int projectId, int emailTemplateId, DateTime emailActivityDate, int actionId, string actionDesc, int batchId, DateTime createDate)
        {
            _emailActivityId = emailActivityId;
            _touchId = touchId;
            _projectId = projectId;
            _emailTemplateId = emailTemplateId;
            _emailActivityDate = emailActivityDate;
            _actionId = actionId;
            _actionDesc = actionDesc;
            _batchId = batchId;
            _createDate = createDate;
        }

        #region XML Methods

        #region Save XML
        private string IdentXML(string xml)
        {
            string newXML = "";
            string[] lines = xml.Split('\r');
            foreach (string strXml in lines)
            {
                if (strXml.Trim() == "")
                    break;
                newXML += "\t" + strXml.Replace("\n", "") + "\r\n";
            }
            return newXML;
        }

        public virtual string GenerateXML()
        {
            return "<EmailActivity>\r\n" +
            "	<EmailActivityId>" + _emailActivityId + "</EmailActivityId>\r\n" +
            "	<TouchId>" + _touchId + "</TouchId>\r\n" +
            "	<ProjectId>" + _projectId + "</ProjectId>\r\n" +
            "	<EmailTemplateId>" + _emailTemplateId + "</EmailTemplateId>\r\n" +
            "	<EmailActivityDate>" + _emailActivityDate + "</EmailActivityDate>\r\n" +
            "	<ActionId>" + _actionId + "</ActionId>\r\n" +
            "	<ActionDesc>" + _actionDesc + "</ActionDesc>\r\n" +
            "	<BatchId>" + _batchId + "</BatchId>\r\n" +
            "	<CreateDate>" + _createDate + "</CreateDate>\r\n" +
            "</EmailActivity>\r\n";
        }
        #endregion

        #region Set Xml Values
        private void SetXmlValue(ref int obj, string val)
        {
            if (val == "") { obj = int.MinValue; return; }
            obj = int.Parse(val);
        }

        private void SetXmlValue(ref string obj, string val)
        {
            if (val == "") { obj = null; return; }
            obj = val;
        }

        private void SetXmlValue(ref bool obj, string val)
        {
            if (val == "") { obj = false; return; }
            obj = (val.ToLower() == "true");
        }

        private void SetXmlValue(ref Decimal obj, string val)
        {
            if (val == "") { obj = Decimal.MinValue; return; }
            obj = Decimal.Parse(val);
        }

        private void SetXmlValue(ref DateTime obj, string val)
        {
            if (val == "") { obj = DateTime.MinValue; return; }
            obj = DateTime.Parse(val);
        }

        #endregion

        #region Load Methods
        // loop through the xml and set the properties and child classes
        public virtual void Load(XmlNode childNodes)
        {
            foreach (XmlNode node in childNodes)
            {
                if (node.Name.ToLower() == "emailactivityid")
                {
                    SetXmlValue(ref _emailActivityId, node.InnerText);
                }
                if (node.Name.ToLower() == "touchid")
                {
                    SetXmlValue(ref _touchId, node.InnerText);
                }
                if (node.Name.ToLower() == "projectid")
                {
                    SetXmlValue(ref _projectId, node.InnerText);
                }
                if (node.Name.ToLower() == "emailtemplateid")
                {
                    SetXmlValue(ref _emailTemplateId, node.InnerText);
                }
                if (node.Name.ToLower() == "emailactivitydate")
                {
                    SetXmlValue(ref _emailActivityDate, node.InnerText);
                }
                if (node.Name.ToLower() == "actionid")
                {
                    SetXmlValue(ref _actionId, node.InnerText);
                }
                if (node.Name.ToLower() == "actiondesc")
                {
                    SetXmlValue(ref _actionDesc, node.InnerText);
                }
                if (node.Name.ToLower() == "batchid")
                {
                    SetXmlValue(ref _batchId, node.InnerText);
                }
                if (node.Name.ToLower() == "createdate")
                {
                    SetXmlValue(ref _createDate, node.InnerText);
                }
            }
        }
        // load from an xml string 
        public virtual void LoadXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from an xml document object
        public virtual void Load(System.Xml.XmlDocument doc)
        {
            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from a stream
        public virtual void Load(System.IO.Stream inStream)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(inStream);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from a text reader
        public virtual void Load(System.IO.TextReader txtReader)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(txtReader);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from an xml reader
        public virtual void Load(System.Xml.XmlReader reader)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        // load from a xml filename
        public virtual void Load(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            foreach (XmlNode node in doc.ChildNodes)
            {
                Load(node);
            }
        }

        #endregion

        #endregion

        #region Data Access Methods

        public static List<EmailActivity> GetEmailActivityByTouchID(int touch_id, int project_id, int action_id)
        {
            DataAccess.RDMailerDatabase dbo = new DataAccess.RDMailerDatabase();
            return dbo.GetEmailActivityByTouchID(touch_id, project_id, action_id);
        }

        public static List<EmailActivity> GetEmailActivityByTouchInfoID(int touch_info_id, int project_id, int action_id)
        {
            DataAccess.RDMailerDatabase dbo = new DataAccess.RDMailerDatabase();
            return dbo.GetEmailActivityByTouchInfoID(touch_info_id, project_id, action_id);
        }

        #endregion

        #region Properties
        public int EmailActivityId
        {
            set { _emailActivityId = value; }
            get { return _emailActivityId; }
        }

        public int TouchId
        {
            set { _touchId = value; }
            get { return _touchId; }
        }

        public int ProjectId
        {
            set { _projectId = value; }
            get { return _projectId; }
        }

        public int EmailTemplateId
        {
            set { _emailTemplateId = value; }
            get { return _emailTemplateId; }
        }

        public DateTime EmailActivityDate
        {
            set { _emailActivityDate = value; }
            get { return _emailActivityDate; }
        }

        public int ActionId
        {
            set { _actionId = value; }
            get { return _actionId; }
        }

        public string ActionDesc
        {
            set { _actionDesc = value; }
            get { return _actionDesc; }
        }

        public int BatchId
        {
            set { _batchId = value; }
            get { return _batchId; }
        }

        public DateTime CreateDate
        {
            set { _createDate = value; }
            get { return _createDate; }
        }

        #endregion
    }
}
