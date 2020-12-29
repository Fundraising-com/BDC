using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace GA.BDC.Core.Web.UI.UIControls.Config {
	/// <summary>
	/// Summary description for Cultures.
	/// </summary>
	[Serializable, XmlInclude(typeof(Culture))]
	public class Cultures {
		private ArrayList cultureList;

		public Cultures() {
			cultureList = new ArrayList();
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "Culture".ToLower()) {
					Culture culture = new Culture();
					culture.Load(child);
					cultureList.Add(culture);
				}
			}
		}

		public ArrayList CultureList {
			set { cultureList = value; }
			get { return cultureList; }
		}
	}
}
