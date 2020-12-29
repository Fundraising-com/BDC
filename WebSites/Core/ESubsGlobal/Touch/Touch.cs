using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GA.BDC.Core.ESubsGlobal.DataAccess;

namespace GA.BDC.Core.ESubsGlobal.Touch
{
    [Serializable]
    public class Touch : EnvironmentBase
    {
        private int _touch_id;
        private int _event_participation_id;
        private int _member_hierarchy_id;
        private int _touch_info_id;
        private int _processed;
        private DateTime _create_date;
        private string _msrepl_tran_version;


        public Touch() : this(int.MinValue) { }
        public Touch(int touch_id) : this(touch_id, int.MinValue) { }
        public Touch(int touch_id, int event_participation_id) : this(touch_id, event_participation_id, int.MinValue) { }
        public Touch(int touch_id, int event_participation_id, int member_hierarchy_id) : this(touch_id, event_participation_id, member_hierarchy_id, int.MinValue) { }
        public Touch(int touch_id, int event_participation_id, int member_hierarchy_id, int touch_info_id) : this(touch_id, event_participation_id, member_hierarchy_id, touch_info_id, int.MinValue) { }
        public Touch(int touch_id, int event_participation_id, int member_hierarchy_id, int touch_info_id, int processed) : this(touch_id, event_participation_id, member_hierarchy_id, touch_info_id, processed, DateTime.Today) { }

        public Touch(int touch_id, int event_participation_id, int member_hierarchy_id, int touch_info_id, int processed, DateTime create_date) : this(touch_id, event_participation_id, member_hierarchy_id, touch_info_id, processed, create_date, "") { }
        public Touch(int touch_id, int event_participation_id, int member_hierarchy_id, int touch_info_id, int processed, DateTime create_date, string msrepl_tran_version)
        {
            _touch_id = touch_id;
            _event_participation_id = event_participation_id;
            _member_hierarchy_id = member_hierarchy_id;
            _touch_info_id = touch_info_id;
            _processed = processed;
            _create_date = create_date;
            _msrepl_tran_version = msrepl_tran_version;
        }

        public void UpdateInDatabase()
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                dbo.UpdateTouch(this);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex, this);
            }        
        }

        public void InsertInDatabase()
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                dbo.InsertOnlyTouch(this);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex, this);
            }
        }

        public void InsertCloneInDatabase()
        {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                dbo.InsertCloneTouch(this);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex, this);
            }
        }

        public static Touch LoadByTouchId(int touchId) {
            try
            {
                ESubsGlobalDatabase dbo = new ESubsGlobalDatabase();
                return dbo.LoadByTouchId(touchId);
            }
            catch (Exception ex)
            {
                throw new ESubsGlobalException(ex.Message, ex);
            }

        }

        #region Properties
        public int touch_id
        {
            set { _touch_id = value; }
            get { return _touch_id; }
        }

        public int event_participation_id
        {
            set { _event_participation_id = value; }
            get { return _event_participation_id; }
        }

        public int member_hierarchy_id
        {
            set { _member_hierarchy_id = value; }
            get { return _member_hierarchy_id; }
        }

        public int touch_info_id
        {
            set { _touch_info_id = value; }
            get { return _touch_info_id; }
        }

        public int processed
        {
            set { _processed = value; }
            get { return _processed; }
        }

        public DateTime create_date
        {
            set { _create_date = value; }
            get { return _create_date; }
        }

        public string msrepl_tran_version
        {
            set { _msrepl_tran_version = value; }
            get { return _msrepl_tran_version; }
        }




        #endregion
    }
}
