using System;

namespace efundraising.efundraisingCore.FormObject {

	public class TimeToCall {
	
		#region private fields

		private string _TimeToCall = string.Empty;
		private string _TimeToCallDesc = string.Empty;

		#endregion

		#region public constructors
		
		public TimeToCall() {
			
		}

		public TimeToCall(string pTimeToCall, string pTimeToCallDesc) {
			//best_time_call, best_time_call_desc
			_TimeToCall = pTimeToCall;
			_TimeToCallDesc = pTimeToCallDesc;
		}

		#endregion

		#region public properties

		public string TimeToCallValue {
			get{ return _TimeToCall; }
		}

		public string TimeToCallDescription {
			get{ return _TimeToCallDesc; }
		}

		#endregion

	}
}
