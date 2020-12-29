using System;
using System.Collections;

namespace GA.BDC.Core.Web.UI.UIControls {
	/// <summary>
	/// Dynamic tags are used to make the content of the Globalizer Dynamic.
	/// </summary>
	[Serializable()]
	public class DynTag {
		private Hashtable dynData;

		public DynTag() {
			dynData = new Hashtable();
		}

		public void Remove(string param) {
			dynData.Remove(param);
		}

		public string this[string param] {
			get {
				if(dynData[param] != null) {
					return dynData[param].ToString();
				} else {
					return "";
				}
			}
			set {
				dynData[param] = value;
			}
		}
	}
}
