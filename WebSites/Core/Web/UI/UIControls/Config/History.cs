using System;
using System.Xml;

namespace GA.BDC.Core.Web.UI.UIControls.Config {
	/// <summary>
	/// Summary description for History.
	/// </summary>
	[Serializable]
	public class History {
		private string author;
		private DateTime date;
		private string modification;

		public History() {

		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "Author".ToLower()) {
					author = child.InnerText;
				} else if(child.Name.ToLower() == "DateTime".ToLower()) {
					string sDate = child.InnerText;
					try {
						// todo date util
						// date = new DateTime(
					} catch {}
				} else if(child.Name.ToLower() == "Modification".ToLower()) {
					modification = child.InnerText;
				}
			}
		}

		public string Author {
			set { author = value; }
			get { return author; }
		}

		public DateTime Date {
			set { date = value; }
			get { return date; }
		}

		public string Modification {
			set { modification = value; }
			get { return modification; }
		}
	}
}
