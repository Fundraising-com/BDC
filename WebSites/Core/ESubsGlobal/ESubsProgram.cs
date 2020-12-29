/* Title:	Program
 * Author:	Jean-Francois Buist
 * Summary:	A partner can sign-up for eSubs, Traditional, or other projects.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;

namespace GA.BDC.Core.ESubsGlobal {
	/// <summary>
	/// Summary description for Program.
	/// </summary>
    [Serializable]
	public class ESubsProgram : EnvironmentBase {
        private int id = int.MinValue;
		private string name;
		private string val;
		private string url;

		public ESubsProgram() {

		}

		public ESubsProgram(int _id, string _name, string _val) {
			id = _id;
			name = _name;
			val = _val;
		}

		#region Properties
		public int ID {
			set { id = value; }
			get { return id; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string Value {
			set { val = value; }
			get { return val; }
		}

		public string URL {
			set { url = value; }
			get { return url; }
		}

		#endregion
	}
}
