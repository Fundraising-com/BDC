using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Data;
using efundraising.ESubsGlobal.DataAccess;

namespace efundraising.ESubsGlobal
{
    public class PersonalizationInfo
    {

        private int _eventid;
        private string _eventname;
        private string _groupurl;
        private string _partlast;
        private string _partfirst;
        private string _emailaddress;
        private string _pwd;
        private int _approverstatid;
        private string _approvername;
        private DateTime _approverdate;
        private DateTime _imgcreatedate;
        private int _leadid;
        private int _eventpartid;
        private string _imgappstatusdesc;
        private string _memberurl;
       

        public PersonalizationInfo() : this(int.MinValue){}
        public PersonalizationInfo(int eventid) : this(eventid, null) { }
        public PersonalizationInfo(int eventid, string eventname) : this(eventid, eventname, null) { }
        public PersonalizationInfo(int eventid, string eventname, string groupurl) : this(eventid, eventname, groupurl, null) { }
        public PersonalizationInfo(int eventid, string eventname, string groupurl, string partlast) : this(eventid, eventname, groupurl, partlast, null) { }
        public PersonalizationInfo(int eventid, string eventname, string groupurl, string partlast, string partfirst) : this(eventid, eventname, groupurl, partlast, partfirst, null) { }
        public PersonalizationInfo(int eventid, string eventname, string groupurl, string partlast, string partfirst, string emailaddress) : this(eventid, eventname, groupurl, partlast, partfirst, emailaddress, null) { }
        public PersonalizationInfo(int eventid, string eventname, string groupurl, string partlast, string partfirst, string emailaddress, string pwd) : this(eventid, eventname, groupurl, partlast, partfirst, emailaddress, pwd, int.MinValue) { }
        public PersonalizationInfo(int eventid, string eventname, string groupurl, string partlast, string partfirst, string emailaddress, string pwd, int approverstatid) : this(eventid, eventname, groupurl, partlast, partfirst, emailaddress, pwd, approverstatid, null) { }
        public PersonalizationInfo(int eventid, string eventname, string groupurl, string partlast, string partfirst, string emailaddress, string pwd, int approverstatid, string approvername) : this(eventid, eventname, groupurl, partlast, partfirst, emailaddress, pwd, approverstatid, approvername, DateTime.MinValue) { }
        public PersonalizationInfo(int eventid, string eventname, string groupurl, string partlast, string partfirst, string emailaddress, string pwd, int approverstatid, string approvername, DateTime approverdate) : this(eventid, eventname, groupurl, partlast, partfirst, emailaddress, pwd, approverstatid, approvername, approverdate, DateTime.MinValue) { }

        public PersonalizationInfo(int eventid, string eventname, string groupurl, string partlast, string partfirst, string emailaddress, string pwd, int approverstatid, string approvername, DateTime approverdate, DateTime imgcreatedate) : this(eventid, eventname, groupurl, partlast, partfirst, emailaddress, pwd, approverstatid, approvername, approverdate, imgcreatedate, int.MinValue) { }
        
        public PersonalizationInfo(int eventid, string eventname, string groupurl, string partlast, string partfirst, string emailaddress, string pwd, int approverstatid, string approvername, DateTime approverdate, DateTime imgcreatedate, int leadid) : this(eventid, eventname, groupurl, partlast, partfirst, emailaddress, pwd, approverstatid, approvername, approverdate, imgcreatedate, leadid, int.MinValue) { }
        
        public PersonalizationInfo(int eventid, string eventname, string groupurl, string partlast, string partfirst, string emailaddress, string pwd, int approverstatid, string approvername, DateTime approverdate, DateTime imgcreatedate, int leadid, int eventpartid) : this(eventid, eventname, groupurl, partlast, partfirst, emailaddress, pwd, approverstatid, approvername, approverdate,imgcreatedate, leadid, eventpartid, null) { }
        public PersonalizationInfo(int eventid, string eventname, string groupurl, string partlast, string partfirst, string emailaddress, string pwd, int approverstatid, string approvername, DateTime approverdate, DateTime imgcreatedate, int leadid, int eventpartid, string imgappstatusdesc) : this(eventid, eventname, groupurl, partlast, partfirst, emailaddress, pwd, approverstatid, approvername, approverdate, imgcreatedate, leadid, eventpartid, imgappstatusdesc, null) { }
        public PersonalizationInfo(int eventid, string eventname, string groupurl, string partlast, string partfirst, string emailaddress, string pwd, int approverstatid, string approvername, DateTime approverdate, DateTime imgcreatedate, int leadid, int eventpartid, string imgappstatusdesc, string memberurl)
        { 
            _eventid = eventid;
            _eventname = eventname;
            _groupurl = groupurl;
            _partlast = partlast;
            _partfirst = partfirst;
            _emailaddress = emailaddress;
            _pwd = pwd;
            _approverstatid = approverstatid;
            _approvername = approvername;
            _approverdate = approverdate;
            _imgcreatedate = imgcreatedate;
            _leadid = leadid;
            _eventpartid = eventpartid;
            _imgappstatusdesc = imgappstatusdesc;
            _memberurl = memberurl;
        }
        #region get/sets

        public string MemberUrl
        {
            set { _memberurl = value; }
            get { return _memberurl; }
        }

        public string ImgAppStatusDesc
        {
            set { _imgappstatusdesc = value; }
            get { return _imgappstatusdesc; }
        }
        
        public int LeadId
        {
            set { _leadid = value; }
            get { return _leadid; }
        }

        public int EventPartId
        {
            set { _eventpartid = value; }
            get { return _eventpartid; }
        }
        public int ApproverStatId
        {
            set { _approverstatid = value; }
            get { return _approverstatid; }
        }
        public string Approvername
        {
            set { _approvername = value; }
            get { return _approvername; }
        }
        
        public DateTime ApproverDate
        {
            set { _approverdate = value; }
            get { return _approverdate; }
        }
        
        public DateTime ImgCreateDate
        {
            set { _imgcreatedate = value; }
            get { return _imgcreatedate; }
        }

        public string Pwd
        {
            set { _pwd = value; }
            get { return _pwd; }
        }
        
        public string EmailAddress
        {
            set { _emailaddress = value; }
            get { return _emailaddress; }
        }

        public string PartFirst
        {
            set { _partfirst = value; }
            get { return _partfirst; }
        }
        
        public string PartLast
        {
            set { _partlast = value; }
            get { return _partlast; }
        }
        
        public int EventID
        {
            set { _eventid = value; }
            get { return _eventid; }
        }

        public string EventName
        {
            set { _eventname = value; }
            get { return _eventname; }
        }

        public string GroupUrl
        {
            set { _groupurl = value; }
            get { return _groupurl; }
        }
        #endregion

      /*  public static PersonalizationInfo GetPersonalizationInfo(int image_id)
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                return dbo.GetPersonalizationInfo(image_id);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex);
            }
        }
       */



    }
}
