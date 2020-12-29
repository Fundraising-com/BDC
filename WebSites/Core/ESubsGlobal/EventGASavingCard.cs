using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA.BDC.Core.ESubsGlobal
{
    /// <summary>
    /// Summary description for EventGASavingCard.
    /// </summary>
    [Serializable]
    public class EventGASavingCard : EnvironmentBase
    {

        #region Fields
        private int _id = int.MinValue;
        private int _eventID = int.MinValue;
        #endregion


        #region Methods
        public static EventGASavingCard Get(int eventId)
        {
            var dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetEventGASavingCardByEventId(eventId);
        }


        #endregion

        #region Properties
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }

        public int EventID
        {
            set { _eventID = value; }
            get { return _eventID; }
        }

        #endregion
    }
}
