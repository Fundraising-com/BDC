using System;

namespace GA.BDC.Core.efundraisingCore.Geography {

	public class State {
	
		#region private fields

		private string _StateCode = string.Empty;
		private string _StateName = string.Empty;

		#endregion

		#region public constructors

		public State() {
			
		}

		public State(string pStateCode, string pStateName) {
			_StateCode = pStateCode;
			_StateName = pStateName;
		}	

		#endregion

		#region public properties

		public string StateCode {
			get{ return this._StateCode; }
		}

		public string StateName {
			get{ return this._StateName; }
		}

		#endregion
	}
}
