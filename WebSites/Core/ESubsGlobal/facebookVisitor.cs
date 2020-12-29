using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal
{
    public class FacebookVisitor : EnvironmentBase
    {
        private int _event_facebook_visitor_id = int.MinValue;
        private int _personalization_id = int.MinValue;        

        private string _facebook_id;
        private string _facebook_image_url;
        private string _facebook_firstname;
        private string _facebook_lastname;
        private DateTime _update_date;
        private DateTime _create_date;
        private byte _deleted;

        public void InsertIntoDatabase()
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                dbo.InsertEventFaceBookVisitor(_personalization_id, _facebook_id, _facebook_image_url, _facebook_firstname, _facebook_lastname, ref _event_facebook_visitor_id);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex, this);
            }
        }

        public void UpdateIntoDatabase()
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                dbo.UpdateEventFaceBookVisitor(this);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex, this);
            }
        }

        public static List<FacebookVisitor> GetFacebookVisitor(Int32 personalization_id)
        {
            ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
            return dbo.GetEventFaceBookVisitor(personalization_id);
        }

        #region Properties
        
        public int EventFacebookVisitorID
        {
            set { _event_facebook_visitor_id = value; }
            get { return _event_facebook_visitor_id; }
        }

        public string FacebookID
        {
            set { _facebook_id  = value; }
            get { return _facebook_id; }
        }

        public int PersonalizationID
        {
            set { _personalization_id = value; }
            get { return _personalization_id; }
        }        

        public string FacebookImageUrl
        {
            set { _facebook_image_url = value; }
            get { return _facebook_image_url; }
        }

        public string FacebookFirstName
        {
            set { this._facebook_firstname = value; }
            get { return _facebook_firstname; }
        }

        public string FacebookLastName
        {
            set { this._facebook_lastname= value; }
            get { return this._facebook_lastname; }
        }

        public byte Deleted
        {
            set { this._deleted = value; }
            get { return this._deleted; }
        }

        #endregion

    }
}
