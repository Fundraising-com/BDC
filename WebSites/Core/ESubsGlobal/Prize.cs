/* Title:	Prize
 * Author:	Jean-Francois Buist
 * Summary:	Partners can have 0, 1 or many prizes.  This information tells the UI what
 *			to display for specific prizes.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// Summary description for Prize.
	/// </summary>
    [Serializable]
	public class Prize : EnvironmentBase {
        private int id = int.MinValue;
        private int parent_id = int.MinValue;
		private string name;
		private string typeName;
        private int programTypeID = int.MinValue;
		private DateTime createDate;

		public Prize() {
            id = int.MinValue;
            parent_id = int.MinValue;
            programTypeID = int.MinValue;
		}

		public Prize(int _id, string _name) {
			id = _id;
			name = _name;
		}

		#region Properties
		public int PrizeID {
			set { id = value; }
			get { return id; }
		}

        public int ParentPrizeID
        {
            set { parent_id = value; }
            get { return parent_id; }
        }

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string TypeName {
			set { typeName = value; }
			get { return typeName; }
		}

		public int ProgramTypeID {
			set { programTypeID = value; }
			get { return programTypeID; }
		}

		public DateTime CreateDate
		{
			set { createDate = value; }
			get { return createDate;}
		}
		#endregion

		#region Data Source Methods
		public static Prize[] GetPrizesByPrizeTypeID(int prizeType) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPrizesByPrizeTypeID(prizeType);
		}

        public static Prize[] GetChildPrizesByPrizeID(int parent_prize_id)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetChildPrizesByPrizeID(parent_prize_id);
        }
		#endregion
	}
}
