/* Title:	Participation channel
 * Author:	Jean-Francois Buist
 * Summary:	This tells how an Event Participant has been created.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// Summary description for ParticipantChannel.
	/// </summary>
    [Serializable]
	public class ParticipationChannel : EnvironmentBase {

		#region Fields
		private int _participationChannelID = int.MinValue;
		private string _name = null;
		#endregion

		#region Constructors
		public ParticipationChannel() 
		{
	
		}
		#endregion

		public static ParticipationChannel Create(int participationChannelID) 
		{
			ParticipationChannel p = new ParticipationChannel();
			p.ParticipationChannelID = 1;
			return p;
		}

		public static ParticipationChannel InvitedBySponsor {
			get { 
				ParticipationChannel p = new ParticipationChannel();
				p.ParticipationChannelID = 1;
				p.Name = "Invited by Sponsor";
				return p;
			}
		}

		public static ParticipationChannel InvitedByParticipant {
			get { 
				ParticipationChannel p = new ParticipationChannel();
				p.ParticipationChannelID = 1;
				p.Name = "Invited by Participant";
				return p;
			}
		}

		public static ParticipationChannel SponsorCreated {
			get {
				ParticipationChannel p = new ParticipationChannel();
				p.ParticipationChannelID = 3;
				p.Name = "Sponsor Creation";
				return p;
			}
		}

		public static ParticipationChannel FindMyGroup {
			get { 
				ParticipationChannel p = new ParticipationChannel();
				p.ParticipationChannelID = 2;
				p.Name = "Find my Group";
				return p;
			}
		}

		#region Properties
		public int ParticipationChannelID {
			set { _participationChannelID = value; }
			get { return _participationChannelID; }
		}

		public string Name {
			set { _name = value; }
			get { return _name; }
		}
		#endregion
	}
}
