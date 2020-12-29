// Query.cs

using System;
using System.Collections;
using System.Xml;

namespace GA.BDC.Core.Database {
	public class Query {

		private string id;
		private string name;
		private string description;
		private string querystring;

		public Query() {

		}

		public void LoadQuery(XmlNode node) {
			
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "ID".ToLower()) {
					ID = child.InnerText;
				}else if(child.Name.ToLower() == "Name".ToLower()) {
					Name = child.InnerText;
				}else if(child.Name.ToLower() == "Description".ToLower()) {
					Description = child.InnerText;
				}else if(child.Name.ToLower() == "QueryString".ToLower()) {
					QueryString = child.InnerText;
				}
			}
		}

		public string ID {
			set { id = value; }
			get { return id; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public string QueryString {
			set { querystring = value; }
			get { return querystring; }
		}

	}
}





// -----------------------



