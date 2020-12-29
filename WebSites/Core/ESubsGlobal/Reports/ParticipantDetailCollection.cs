using System;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.Reports {
	/// <summary>
	/// Summary description for ParticipantDetailCollection.
	/// </summary>
    [Serializable]
	public class ParticipantDetailCollection {
		private ArrayList participantDetails;

		public ParticipantDetailCollection() {
			participantDetails = new ArrayList();
		}

		public void GetParticipantDetails(int eventID) {
			// fill the array of participant details
		}
	}
}
