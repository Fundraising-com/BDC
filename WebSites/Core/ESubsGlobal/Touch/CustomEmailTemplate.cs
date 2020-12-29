using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GA.BDC.Core.ESubsGlobal.Users;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Touch
{
    [Serializable]
    public class MemberType
    {
        public int memberTypeId { get; set; }
        public string memberTypeName { get; set; }
        public string emailDescription { get; set; }        
    }

    [Serializable]
    public class EmailBatch
    {
        public EmailBatch()
        {
            strTo = "";
            m_listTouchInfo = new List<touch_info>();
            m_listCustomEmailTemplate = new List<CustomEmailTemplate>();
            m_masterListUser = new List<UnknownUser>();
            m_listTouch = new List<Touch>();
        }

        public MemberType memberType {get; set;}
        public DateTime createDate { get; set; }
        public List<UnknownUser> m_masterListUser;
        public List<touch_info> m_listTouchInfo { get; set; }        
        public List<Touch> m_listTouch { get; set; }
        public List<CustomEmailTemplate> m_listCustomEmailTemplate { get; set; }


        public int intervalEmail
        {
            get
            {
                int result = 0;

                if (m_listTouchInfo.Count() >= 2)
                {
                    TimeSpan tspan = m_listTouchInfo[0].launch_date - m_listTouchInfo[1].launch_date;
                    result = Math.Abs(Convert.ToInt32(tspan.TotalDays));

                }
                return result;
            }
        }

        //public string Subject { get; set; }
        public string strToStrip 
        {
            get 
            {
                if (strTo.Length > 60)
                {
                    return strTo.Substring(0, 57) + "...";
                }
                if (strTo == string.Empty)
                {
                    strTo = " &nbsp;";
                }
                return strTo;
            }            
        }

        public string strTo { get; set; }
        public int touche_info_id { get; set; }
        public string strCreateDate { get { return createDate.ToString("MM/dd/yy"); } }
    }

    [Serializable]
    public class EmailContactManager
    {
        private List<EmailGroup> m_listEmail;
        List<UnknownUser> m_masterListUser;
        List<Touch> m_listTouch = new List<Touch>();
        List<touch_info> m_listTouchInfo = new List<touch_info>();
        List<CustomEmailTemplate> m_listCustomEmail = new List<CustomEmailTemplate>();
        List<EmailBatch> m_bach = new List<EmailBatch>();

        public int DraftCount
        {
            get { return m_bach.Count(); }
        }

        public void RemoveContactFromEveryWhere(string email)
        {
            //supose to have only one but just in case
            List<UnknownUser> listUU = m_masterListUser.Where(p => p.Email == email).ToList<UnknownUser>();

            foreach( UnknownUser uu in listUU )
            {
                EventParticipation ep = EventParticipation.GetEventParticipationByMemberHierarchyID(uu.HierarchyID);
                if (ep != null)
                {
                    int deleteByUser = (int)TouchProcessedStatus.DeleteByUser;

                    List<Touch> listTouch = m_listTouch.Where(p => p.event_participation_id == ep.EventParticipationID && p.processed == (int)TouchProcessedStatus.New).ToList<Touch>();
                    foreach (Touch ti in listTouch)
                    {
                        ti.processed = deleteByUser;
                        ti.UpdateInDatabase();
                    }
                }
            }
        }

        static public List<BusinessRule> GetBusinessRule
        {
            get
            {
                if (HttpContext.Current.Cache["BusinessRule"] == null)
                {
                    EmailTemplateList[] emailTemplalelist = EmailTemplateList.GetList();

                    ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                    List<BusinessRule> list = dbo.GetBusinessRuleAll(null);

                    foreach (BusinessRule br in list)
                    {
                        foreach (EmailTemplateList etl in emailTemplalelist)
                        {
                            if (br.email_template_id == etl.TemplateId)
                            {
                                br.emailTemplate = etl;
                                break;
                            }
                        }
                    }

                    HttpContext.Current.Cache["BusinessRule"] = list;
                }

                return (List<BusinessRule>)HttpContext.Current.Cache["BusinessRule"];
            }
        }

        public EmailContactManager()
        {
            eSubsGlobalUser user = eSubsGlobalUser.Create();
            m_listEmail = new List<EmailGroup>();
            m_masterListUser = eSubsGlobalUser.GetLightWeightContactAll(user.HierarchyID);
        }

        public List<Touch> GetTouchByHID(int hid)
        {
            return m_listTouch.Where(p => p.member_hierarchy_id == hid).ToList<Touch>();
        }

        public touch_info GetTouchInfoById(int id)
        {
            return m_listTouchInfo.FirstOrDefault(p => p.touch_info_id == id);
        }

        public EmailGroup GetEmail(int touch_info_id, int processStatus)
        {
            return m_listEmail.FirstOrDefault(p => p.isTouch_info.touch_info_id == touch_info_id && p.Processed == (TouchProcessedStatus)processStatus);
        }

        public void DeleteDraft(int touch_info_id)
        {
            EmailBatch eb = GetDraft(touch_info_id);

            foreach (Touch to in eb.m_listTouch)
            {
                to.processed = (int)TouchProcessedStatus.DeleteByUser;
                to.UpdateInDatabase();
            }
            m_bach.Remove(eb);
        }

        public EmailBatch GetDraft(int touch_info_id)
        {
            return m_bach.FirstOrDefault(p => p.touche_info_id == touch_info_id);
        }

        public void CreateNewEmailForBounceHierarchyId(int member_hierarchy_id)
        {
            UnknownUser uuser = m_masterListUser.FirstOrDefault(p => p.HierarchyID == member_hierarchy_id);
            if (uuser != null)
            {
                EventParticipation ep = EventParticipation.GetEventParticipationByMemberHierarchyID(uuser.HierarchyID);

                if (ep == null)
                    return;

                List<Touch> list =  m_listTouch.Where(p => p.event_participation_id == ep.EventParticipationID).ToList<Touch>();

                if (list != null && list.Count > 0)
                {
                    foreach (Touch t in list)
                    {
                        touch_info ti = m_listTouchInfo.FirstOrDefault(p => p.touch_info_id == t.touch_info_id);
                        if (ti != null)
                        {
                            touch_info newti = new touch_info();

                            newti.visitor_log_id = ti.visitor_log_id;
                            newti.business_rule_id = ti.business_rule_id;

                            TimeSpan span = DateTime.Today - ti.launch_date;
                            newti.launch_date = DateTime.Today.AddDays(span.Days);
                            newti.create_date = DateTime.Now;

                            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                            dbo.InsertOnlyTouchInfo(newti);

                            t.touch_info_id = newti.touch_info_id;
                            t.UpdateInDatabase();

                            CustomEmailTemplate cet = m_listCustomEmail.FirstOrDefault(p => p.touch_info_id == ti.touch_info_id);
                            if (cet != null)
                            {
                                cet.touch_info_id = newti.touch_info_id;
                                dbo.UpdateCustomEmailTemplate(cet, null);
                            }
                            else
                            {
                                GA.BDC.Core.Diagnostics.Logger.LogWarn("'custom_email_template' entry doesn't exist for touch_info_id = " + ti.touch_info_id);
                            }

                            // 1st ATTEMP
                            // 2nd ATTEMP
                            // 3rd ATTEMP
                        }
                        else
                        {
                            GA.BDC.Core.Diagnostics.Logger.LogWarn("'touch_info' entry doesn't exist for touch_id = " + ti.touch_info_id);
                        }
                    }                
                }             
            }
        }

        public List<EmailGroup> GetEmailGroup(params TouchProcessedStatus[] status)
        {
            List<EmailGroup> eg = new List<EmailGroup>();
            foreach (TouchProcessedStatus tps in status)
            {
                var r = from em in m_listEmail where em.Processed == tps select em;
                eg.AddRange(r.ToList<EmailGroup>());
            }
            return eg;
        }

        public List<EmailGroup> SendEmail()
        {
            var r = from em in m_listEmail where em.Processed == TouchProcessedStatus.Sent select em;
            return r.ToList<EmailGroup>();
        }

        public List<EmailGroup> PendingEmail()
        {
            var r = from em in m_listEmail where em.Processed == TouchProcessedStatus.New select em;
            return r.ToList<EmailGroup>();
        }

        public List<EmailBatch> DraftEmail()
        {
            return m_bach;
        }

        public void addEmail(Touch touch , touch_info ti, CustomEmailTemplate cet, MemberType memberType, int member_hierarchy_id)
        {
            m_listTouch.Add(touch);
            m_listTouchInfo.Add(ti);
            m_listCustomEmail.Add(cet);

            //HANDLE DRAFT BATCH
            if ((TouchProcessedStatus)touch.processed == TouchProcessedStatus.Draft)
            {
                EmailBatch currentBatch = null;

                foreach (EmailBatch eBatch in m_bach)
                {
                    TimeSpan tpan = eBatch.createDate - touch.create_date;
                    if (Math.Abs(tpan.TotalSeconds) < 61)
                    {
                        if (eBatch.memberType.memberTypeId == memberType.memberTypeId)
                        {
                            currentBatch = eBatch;
                        }
                    }
                }

                if (currentBatch == null)
                {
                    currentBatch = new EmailBatch();
                    m_bach.Add(currentBatch);
                    currentBatch.createDate = touch.create_date;
                    //The first Touch_info id find will be use as a ID unique to retrieve easy the group of email.
                    currentBatch.touche_info_id = ti.touch_info_id;
                    currentBatch.memberType = memberType;                    
                }

                UnknownUser uu = m_masterListUser != null ? m_masterListUser.FirstOrDefault(p => p.HierarchyID == member_hierarchy_id) : null;
               
                if( uu != null)
                {
                    if (currentBatch.m_masterListUser.FirstOrDefault(p => p.HierarchyID == uu.HierarchyID) == null)
                    {
                        currentBatch.m_masterListUser.Add(uu);
                        if (currentBatch.strTo == "")
                        {
                            currentBatch.strTo = uu.Email;
                        }
                        else
                        {
                            currentBatch.strTo += ", " + uu.Email;
                        }
                    }
                }
                
                if (currentBatch.m_listTouchInfo.FirstOrDefault(p => p.touch_info_id == ti.touch_info_id) == null)
                {
                    currentBatch.m_listTouchInfo.Add(ti);
                }
                if (currentBatch.m_listCustomEmailTemplate.FirstOrDefault(p => p.custom_email_template_id == cet.custom_email_template_id) == null)
                {
                    currentBatch.m_listCustomEmailTemplate.Add(cet);
                }
                if (currentBatch.m_listTouch.FirstOrDefault(p => p.touch_id == touch.touch_id) == null)
                {
                    currentBatch.m_listTouch.Add(touch);
                }
            }
            else
            {
                /// ALL OTHER CASE
                EmailGroup eb = m_listEmail.FirstOrDefault(p => p.touche_info_id == ti.touch_info_id && p.Processed == (TouchProcessedStatus)touch.processed);

                if (eb == null)
                {
                    eb = new EmailGroup();
                    eb.isTouch_info = ti;
                    m_listEmail.Add(eb);
                    eb.memberType = memberType;
                    BusinessRule br = EmailContactManager.GetBusinessRule.FirstOrDefault(p => p.business_rule_id == ti.business_rule_id);
                    if (br != null && br.emailTemplate != null)
                    {
                        eb.TypeEmail = br.emailTemplate.TemplateName;
                        if (eb.TypeEmail.Contains("Personnal note from"))
                        {
                            eb.TypeEmail = "Personal Note";
                        }
                    }
                    eb.MemberType = memberType;
                }

                eb.add(cet, m_masterListUser, member_hierarchy_id, touch);
            }
        }
    }

    [Serializable]
    public class EmailGroup
    {
        public const int maxStripShortChar = 30;
        public const int maxStripChar = 40;
        public const int maxStripLongChar = 100;
        private List<CustomEmailTemplate> m_customEmailTemplate;
        private touch_info m_touch_info;
        private List<UnknownUser> m_listUser;
        private List<Touch> m_Listtouch;

        public MemberType memberType { get; set; }

        TouchProcessedStatus m_processed = TouchProcessedStatus.NoSpecified;
        
        private string m_StrToUser;

        public EmailGroup()
        {
            m_customEmailTemplate= new List<CustomEmailTemplate>();
            m_touch_info = new touch_info();
            m_listUser = new List<UnknownUser>();
            m_Listtouch = new List<Touch>();
 
            m_StrToUser = "";
        }       

        public void Resend()
        {
            TouchInfo touchInfo = TouchInfo.LoadByTouchInfoId(this.touche_info_id);
            touchInfo.TouchInfoID = int.MinValue;
            touchInfo.LaunchDate = DateTime.Now;
            touchInfo.RuleID = touchInfo.BusinessRuleId;
            touchInfo.InsertDatabase();

            foreach (Touch t in m_Listtouch)
            {
                if (t.processed == (int)TouchProcessedStatus.Sent)
                {
                    Touch touch = Touch.LoadByTouchId(t.touch_id);
                    touch.touch_id = int.MinValue;
                    touch.processed = (int)TouchProcessedStatus.New;
                    touch.touch_info_id = touchInfo.TouchInfoID;
                    touch.InsertCloneInDatabase();

                    /*t.processed = (int)newprocesses;
                    t.*/
                    //t.UpdateInDatabase();
                }
            }
        }        

        public void ChangeProcess(TouchProcessedStatus newprocesses)
        {
            foreach (Touch t in m_Listtouch)
            {
                t.processed = (int)newprocesses;
                t.UpdateInDatabase();
            }
            m_processed = newprocesses;
        }

        public int GetTouchIDByEventParticipationID(int event_participation_id)
        {
            Touch t = m_Listtouch.Find(p => p.event_participation_id == event_participation_id);
            if (t != null)
                return t.touch_id;
            else
                return int.MinValue;
        }

        public void add(CustomEmailTemplate cet, List<UnknownUser> m_masterListUser, int member_hierarchy_id, Touch touch)
        {
           m_customEmailTemplate.Add(cet);
           m_Listtouch.Add(touch);

           if (m_processed == TouchProcessedStatus.NoSpecified)
           {
               m_processed = (TouchProcessedStatus)touch.processed;
           }

           UnknownUser user =  m_masterListUser.FirstOrDefault(p => p.HierarchyID == member_hierarchy_id);
           string temp = string.Empty;
           if (user != null)
           {
               m_listUser.Add(user);

               // it the email not the name right ?
               /*
               if (!string.IsNullOrEmpty(user.CompleteName))
                   temp = user.CompleteName;
               else
                   temp = user.Email;
               */
               temp = user.Email;
               if (m_StrToUser == string.Empty)
               {
                   m_StrToUser = temp;
               }
               else
               {
                   m_StrToUser += ", " + temp;
               }
           }
        }

        public string To { get { return m_StrToUser.Trim(); } }
        public string ToStrip
        {
            get
            {
                if (m_StrToUser.Length > maxStripShortChar)
                {
                    return m_StrToUser.Substring(0, maxStripShortChar - 1) + "...";
                }
                return m_StrToUser;
            }
        }

        public string subject { set { m_customEmailTemplate[0].subject = value; } get { return m_customEmailTemplate[0].subject; } }
        public string subjectStrip
        {
            get
            {
                if (m_customEmailTemplate[0].subject.Length > maxStripChar)
                {
                    return m_customEmailTemplate[0].subject.Substring(0, maxStripChar - 1) + "...";
                }

                return m_customEmailTemplate[0].subject;
            }
        }

        public string message { get { return m_customEmailTemplate[0].body_html; } }

        public touch_info isTouch_info
        {
            set { m_touch_info = value; }
            get { return m_touch_info; }
        }

        public TouchProcessedStatus Processed
        {
            set {  m_processed = value; }
            get { return m_processed; }
        }

        public string strLaunchDate { get { return m_touch_info.launch_date.ToString("MM/dd/yy"); } }
        public DateTime launchDate { get {return  m_touch_info.launch_date; }}   
        public int touche_info_id  { get { return m_touch_info.touch_info_id; }}
        public string TypeEmail { get; set; }
        public List<UnknownUser> EmailRecipients { get { return m_listUser; } }
        public int UserCount { get { return (m_listUser != null && m_listUser.Count > 0) ? m_listUser.Count : 0; } }
        public int RegisteredUserCount { get { return (m_listUser != null && m_listUser.Count > 0) ? m_listUser.Where(x => x.UserID != int.MinValue).Count() : 0; } }
        public MemberType MemberType { get; set; }
    }

    [Serializable]
    public class touch_info {

		private int _touch_info_id;
		private int _business_rule_id;
		private int _visitor_log_id;
		private DateTime _launch_date;
		private DateTime _create_date;
        private int _reminder_interval_day;

		public touch_info() : this(int.MinValue) { }
		public touch_info(int touch_info_id) : this(touch_info_id, int.MinValue) { }
		public touch_info(int touch_info_id, int business_rule_id) : this(touch_info_id, business_rule_id, int.MinValue) { }
		public touch_info(int touch_info_id, int business_rule_id, int visitor_log_id) : this(touch_info_id, business_rule_id, visitor_log_id, DateTime.MinValue) { }
		public touch_info(int touch_info_id, int business_rule_id, int visitor_log_id, DateTime launch_date) : this(touch_info_id, business_rule_id, visitor_log_id, launch_date, DateTime.MinValue) { }
		public touch_info(int touch_info_id, int business_rule_id, int visitor_log_id, DateTime launch_date, DateTime create_date) {
			_touch_info_id = touch_info_id;
			_business_rule_id = business_rule_id;
			_visitor_log_id = visitor_log_id;
			_launch_date = launch_date;
			_create_date = create_date;
		}

		#region Properties
		public int touch_info_id {
			set { _touch_info_id = value; }
			get { return _touch_info_id; }
		}

		public int business_rule_id {
			set { _business_rule_id = value; }
			get { return _business_rule_id; }
		}

		public int visitor_log_id {
			set { _visitor_log_id = value; }
			get { return _visitor_log_id; }
		}

		public DateTime launch_date {
			set { _launch_date = value; }
			get { return _launch_date; }
		}

		public DateTime create_date {
			set { _create_date = value; }
			get { return _create_date; }
		}

        public int reminder_interval_day
        {
            set { _reminder_interval_day = value; }
            get { return _reminder_interval_day; }
		}
        

		#endregion
	}

    [Serializable]
    public class CustomEmailTemplate
    {
        private int _touch_info_id;
        private int _custom_email_template_id;
        private string _subject;
        private string _body_txt;
        private string _body_html;
        private DateTime _create_date;

        public CustomEmailTemplate() : this(0) { }
        public CustomEmailTemplate(int touch_info_id) : this(touch_info_id, 0) { }
        public CustomEmailTemplate(int touch_info_id, int custom_email_template_id) : this(touch_info_id, custom_email_template_id, null) { }
        public CustomEmailTemplate(int touch_info_id, int custom_email_template_id, string subject) : this(touch_info_id, custom_email_template_id, subject, null) { }
        public CustomEmailTemplate(int touch_info_id, int custom_email_template_id, string subject, string body_txt) : this(touch_info_id, custom_email_template_id, subject, body_txt, null) { }
        public CustomEmailTemplate(int touch_info_id, int custom_email_template_id, string subject, string body_txt, string body_html) : this(touch_info_id, custom_email_template_id, subject, body_txt, body_html, DateTime.Now) { }
        public CustomEmailTemplate(int touch_info_id, int custom_email_template_id, string subject, string body_txt, string body_html, DateTime create_date)
        {
            _touch_info_id = touch_info_id;
            _custom_email_template_id = custom_email_template_id;
            _subject = subject;
            _body_txt = body_txt;
            _body_html = body_html;
            _create_date = create_date;
        }

        #region Properties
        public int touch_info_id
        {
            set { _touch_info_id = value; }
            get { return _touch_info_id; }
        }

        public int custom_email_template_id
        {
            set { _custom_email_template_id = value; }
            get { return _custom_email_template_id; }
        }

        public string subject
        {
            set { _subject = value; }
            get { return _subject; }
        }

        public string body_txt
        {
            set { _body_txt = value; }
            get { return _body_txt; }
        }

        public string body_html
        {
            set { _body_html = value; }
            get { return _body_html; }
        }

        public DateTime create_date
        {
            set { _create_date = value; }
            get { return _create_date; }
        }

        #endregion
    }
}
