using System;

namespace GA.BDC.Core.WebTracking {
	/// <summary>
	/// Summary description for VisitorTrack.
	/// </summary>
    [Serializable]
   public class VisitorTrack : GA.BDC.Core.BusinessBase.BusinessBase
   {
		private int visitorLogID;
		private int trackableObject;
		private string trackingCode;
		private string trackDynamic;
		private DateTime createDate;

		private VisitorLog _visitorLog;

		public VisitorTrack(VisitorLog vl) {
			if(vl != null) {
				visitorLogID = vl.VisitorLogID;
				_visitorLog = vl;
			} else {
				visitorLogID = int.MinValue;
			}
		}

		private void IncrementEvent() {
			_visitorLog.Increment++;
		}

		private void IncrementClick() {
			_visitorLog.TrackNo++;
		}

		public void InsertIntoDatabase(string _trackingCode) {
			if(visitorLogID == int.MinValue) {
				return;
			}

			if(_trackingCode == null || _trackingCode == "") {
				return;	
			}

			// set this object's trackingCode variable
			trackingCode = _trackingCode;

			DataAccess.WebTrackingDatabase dbo = new DataAccess.WebTrackingDatabase();

			TrackableObject _trackableObject = 
				WebTracking.TrackableObject.GetTrackableObject(_trackingCode);

			if(_trackableObject == null) {
				return;
			}

			trackableObject = _trackableObject.TrackableObjectTypeID;

			if(trackableObject > 0) {	// means that this is a button
				IncrementClick();
			}

			IncrementEvent();

			dbo.InsertVisitorTrack(visitorLogID, _trackableObject.TrackableObjectID, _visitorLog.Increment, _visitorLog.TrackNo, trackDynamic);
		}

		public int VisitorLogID {
			set { visitorLogID = value; }
			get { return visitorLogID; }
		}

		public int TrackableObject {
			set { trackableObject = value; }
			get { return trackableObject; }
		}

		public string TrackingCode {
			set { trackingCode = value; }
			get { return trackingCode; }
		}

		public string TrackDynamic {
			set { trackDynamic = value; }
			get { return trackDynamic; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

	}
}
