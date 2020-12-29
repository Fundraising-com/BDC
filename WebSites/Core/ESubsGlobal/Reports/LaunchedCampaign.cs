using System;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for ParticipantDetail.
	/// </summary>
	public class LaunchedCampaign {
        private int _eventID = int.MinValue;
		private string _eventName;
		private DateTime _launchDate;
		private bool _active;


		public LaunchedCampaign() {

		}

		#region Properties
		public int EventID {
			set { _eventID = value; }
			get { return _eventID; }
		}

		public string EventName {
			set { _eventName = value; }
			get { return _eventName; }
		}

		public DateTime LaunchDate {
			set { _launchDate = value; }
			get { return _launchDate; }
		}

		public bool Active {
			set { _active = value; }
			get { return _active; }
		}
		#endregion
	}
}
