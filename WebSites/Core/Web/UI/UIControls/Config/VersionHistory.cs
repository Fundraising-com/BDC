using System;
using System.Xml;
using System.Collections;
using System.Xml.Serialization;

namespace GA.BDC.Core.Web.UI.UIControls.Config {
	/// <summary>
	/// Summary description for VersionHistory.
	/// </summary>
	[Serializable, XmlInclude(typeof(History))]
	public class VersionHistory	{
		private ArrayList historyList;

		public VersionHistory() {
			historyList = new ArrayList();
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "History".ToLower()) {
					History history = new History();
					history.Load(child);
					historyList.Add(history);
				}
			}
		}

		public ArrayList HistoryList {
			set { historyList = value; }
			get { return historyList; }
		}
	}
}
