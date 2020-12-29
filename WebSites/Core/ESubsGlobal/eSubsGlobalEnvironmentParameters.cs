//
// 2005-08-18 - Jean-Francois Buist - New class.
// 

using System;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for eSubsGlobalEnvironmentParameters.
	/// </summary>
	[Serializable]
	public class eSubsGlobalEnvironmentParameters
	{
		#region Fields
		private int _partnerID = int.MinValue;
		private string _partnerGUID = null;
		private string _partnerHost = null;
		private int _groupID = int.MinValue;
		private int _eventID = int.MinValue;
		private int _touchID = int.MinValue;
		private int _eventParticipationID = int.MinValue;
		private Culture _culture = null;
		private int _memberHierarchyID = int.MinValue;
		private string _redirect = null;
		private int _promotionID = int.MinValue;
		private string _externalGroupID;
		private int _facebookID = int.MinValue;
		#endregion

		#region Properties
		public Culture Culture {
			get {return _culture; }
			set { _culture = value; }
		}

		public int PartnerID
		{
			get { return _partnerID; }
			set { _partnerID = value; }
		}

		public string PartnerGUID
		{
			get { return _partnerGUID; }
			set { _partnerGUID = value; }
		}

		public string PartnerHost
		{
			get { return _partnerHost; }
			set { _partnerHost = value; }
		}

		public int GroupID
		{
			get { return _groupID; }
			set { _groupID = value; }
		}

		public int PromotionID
		{
			get { return _promotionID; }
			set { _promotionID = value; }
		}

		public int EventID
		{
			get { return _eventID; }
			set { _eventID = value; }
		}

		public int EventParticipationID {
			get { return _eventParticipationID; }
			set { _eventParticipationID = value; }
		}

		public int TouchID
		{
			get { return _touchID; }
			set { _touchID = value; }
		}

		public int MemberHierarchyID
		{
			get { return _memberHierarchyID; }
			set { _memberHierarchyID = value; }
		}

		public string Redirect {
			get { return _redirect; }
			set { _redirect = value; }
		}
		public string ExternalGroupID
		{
			get {return _externalGroupID;}
			set {_externalGroupID = value;}
		}
		
		public int FacebookID {
			get { return _facebookID; }
			set { _facebookID = value; }
		}
		#endregion

	}
}
