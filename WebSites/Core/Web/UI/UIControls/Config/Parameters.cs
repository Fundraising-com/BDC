using System;
using System.Xml;
using System.Collections;

namespace GA.BDC.Core.Web.UI.UIControls.Config {
	/// <summary>
	/// Summary description for Parameters.
	/// </summary>
	[Serializable]
	public class Parameters {
		private ArrayList parameters;

		public Parameters() {
			parameters = new ArrayList();
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "Param".ToLower()) {
					parameters.Add(child.InnerText);
				}
			}
		}

		public ArrayList Parameter {
			set { parameters = value; }
			get { return parameters; }
		}
	}
}
