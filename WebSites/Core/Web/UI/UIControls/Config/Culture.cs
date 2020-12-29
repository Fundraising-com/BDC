using System;
using System.Xml;
using System.Collections;

namespace GA.BDC.Core.Web.UI.UIControls.Config {
	[Serializable]
	public class Data {
		private string source;
		private Parameters parameters;

		public Data() {
			parameters = new Parameters();
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "Source".ToLower()) {
					source = child.InnerText;
				} else if(child.Name.ToLower() == "Params".ToLower()) {
					parameters.Load(child);
				}
			}
		}

		public string Source {
			set { source = value; }
			get { return source; }
		}

		public Parameters Parameters {
			set { parameters = value; }
			get { return parameters; }
		}
	}
	/// <summary>
	/// Summary description for Culture.
	/// </summary>
	[Serializable]
	public class Culture {
		private string id;
		private Data data;
		
		public Culture() {
			data = new Data();
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "ID".ToLower()) {
					id = child.InnerText;
				} else if(child.Name.ToLower() == "Data".ToLower()) {
					data = new Data();
					data.Load(child);
				}
			}
		}
		
		public string ID {
			set { id = value; }
			get { return id; }
		}

		public Data Data {
			set { data = value; }
			get { return data; }
		}
	}
}
