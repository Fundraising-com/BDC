using System;
using System.Collections;

namespace GA.BDC.Core.Diagnostics.Eavesdrop
{
	/// <summary>
	/// Summary description for EavesdropAction.
	/// </summary>
	public class EavesdropAction {
		private string _name;
		private ArrayList _status;

		public EavesdropAction() {
			_status = new ArrayList();
		}

		public void AddAction(string action) {
			for(int i=0;i<_status.Count;i++) {
				object[] objs = (object[]) _status[i];
				if((string)objs[0] == action) {
					int increment = (int)objs[1];
					object[] newObjs = new object[] { action, ++increment };

					_status.RemoveAt(i);	// remove the current object
					_status.Add(newObjs);
					return;
				}
			}
			object[] newOjb = new object[] { action, 1 };
			_status.Add(newOjb);
		}

		#region Properties
		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		public ArrayList Status {
			get { return _status; }
			set { _status = value; }
		}
		#endregion

	}
}
