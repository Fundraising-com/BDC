using System;

namespace GA.BDC.Core.WebTracking {
	/// <summary>
	/// Summary description for TrackableObject.
	/// </summary>
    [Serializable]
   public class TrackableObject : GA.BDC.Core.BusinessBase.BusinessBase
   {
		private int trackableObjectID = int.MinValue;
		private int trackableObjectTypeID = int.MinValue;
		private string trackableObjectDesc = "";
		private string trackingCode = "";

		public TrackableObject() {

		}

		public void InsertIntoDatabase() {
			DataAccess.WebTrackingDatabase dbo = new DataAccess.WebTrackingDatabase();
			dbo.InsertWebTrackingObject(TrackableObjectTypeID, trackableObjectDesc, trackingCode, ref trackableObjectID);
		}

		public static int GetClickCount(string _trackingCode) {
			DataAccess.WebTrackingDatabase dbo = new DataAccess.WebTrackingDatabase();
			return dbo.GetClickCount(_trackingCode);
		}

		public static TrackableObject GetTrackableObject(string _trackingCode) {
			DataAccess.WebTrackingDatabase dbo = new DataAccess.WebTrackingDatabase();
			TrackableObject trackableObject = dbo.GetTrackableObject(_trackingCode);
			if(trackableObject == null) {
				return null;
			}
			trackableObject.trackingCode = _trackingCode;

			return trackableObject;
		}

		#region Properties

		public int TrackableObjectID {
			set { trackableObjectID = value; }
			get { return trackableObjectID; }
		}

		public int TrackableObjectTypeID {
			set { trackableObjectTypeID = value; }
			get { return trackableObjectTypeID; }
		}

		public string TrackableObjectDesc {
			set { trackableObjectDesc = value; }
			get { return trackableObjectDesc; }
		}

		public string TrackingCode {
			set { trackingCode = value; }
			get { return trackingCode; }
		}

		#endregion
	}
}
