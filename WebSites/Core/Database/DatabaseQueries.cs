// DatabaseQueries.cs

using System;
using System.Collections;
using System.Xml;

namespace GA.BDC.Core.Database {
	public class DatabaseQueries {

		private ArrayList querylist = new ArrayList();


		public DatabaseQueries() {

		}

		public void LoadXML(string filename) {
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			foreach(XmlNode node in doc.ChildNodes) {
				this.LoadDatabaseQueries(node);
			}
		}

		public Query GetQueryByID(string childName) {
			foreach(Query query in querylist) {
				if(query.ID.ToLower() == childName.ToLower()) {
					return query;
				}
			}
			return null;
		}


		public Query GetQueryByName(string childName) {
			foreach(Query query in querylist) {
				if(query.Name.ToLower() == childName.ToLower()) {
					return query;
				}
			}
			return null;
		}


		public void LoadDatabaseQueries(XmlNode node) {
			
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "Query".ToLower()) {
					Query query = new Query();
					query.LoadQuery(child);
					AddQuery(query);
				}
			}
		}

		public void AddQuery(Query query) {
			QueryList.Add(query);
		}

		public ArrayList QueryList {
			set { querylist = value; }
			get { return querylist; }
		}

	}
}





// -----------------------



