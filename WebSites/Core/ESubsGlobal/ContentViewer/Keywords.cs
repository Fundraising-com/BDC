using System;
using System.Xml;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.ContentViewer {

	public class Keywords {

		private ArrayList _keywords;

		public Keywords() {
			_keywords = new ArrayList();
		}

		#region Object Manipulation

		// add a new Keyword
		public void AddKeyword(Keyword keyword) {
			_keywords.Add(keyword);
		}

		// remove a Keyword
		public void RemoveKeyword(Keyword keyword) {
			_keywords.Remove(keyword);
		}

		// check if this specified Keyword exists
		public bool Exists(Keyword keyword) {
			return _keywords.Contains(keyword);
		}

		// clear the list of Keywords
		public void Clear() {
			_keywords.Clear();
		}

		// get all Keywords
		public Keyword[] GetAllKeywords() {
			Keyword[] keyword = new Keyword[_keywords.Count];
			for(int i=0;i<_keywords.Count;i++) {
				keyword[i] = (Keyword)_keywords[i];
			}
			return keyword;
		}

		// get one Keyword by Level
		public Keyword GetKeywordByLevel(int level) {
			for(int i=0;i<_keywords.Count;i++) {
				Keyword keyword = (Keyword)_keywords[i];
				if(keyword.Level == level) {
					return keyword;
				}
			}
			return null;
		}

		// get all Keyword by Level
		public Keyword[] GetKeywordsByLevel(int level) {
			ArrayList list = new ArrayList();
			for(int i=0;i<_keywords.Count;i++) {
				Keyword keyword = (Keyword)_keywords[i];
				if(keyword.Level == level) {
					list.Add(keyword);
				}
			}
			if(list.Count < 1) return null;
			Keyword[] keywords = new Keyword[list.Count];
			for(int i=0;i<list.Count;i++) {
				keywords[i] = (Keyword)list[i];
			}
			return keywords;
		}

		// get one Keyword by Key
		public Keyword GetKeywordByKey(string key) {
			for(int i=0;i<_keywords.Count;i++) {
				Keyword keyword = (Keyword)_keywords[i];
				if(keyword.Key == key) {
					return keyword;
				}
			}
			return null;
		}

		// get all Keyword by Key
		public Keyword[] GetKeywordsByKey(string key) {
			ArrayList list = new ArrayList();
			for(int i=0;i<_keywords.Count;i++) {
				Keyword keyword = (Keyword)_keywords[i];
				if(keyword.Key == key) {
					list.Add(keyword);
				}
			}
			if(list.Count < 1) return null;
			Keyword[] keywords = new Keyword[list.Count];
			for(int i=0;i<list.Count;i++) {
				keywords[i] = (Keyword)list[i];
			}
			return keywords;
		}

		#endregion

		#region XML Methods

		#region Save XML
		private string IdentXML(string xml) {
			string newXML = "";
			string[] lines = xml.Split('\r');
			foreach(string strXml in lines) {
				if(strXml.Trim() == "")
					break;
				newXML += "\t" + strXml.Replace("\n", "") + "\r\n";
			}
			return newXML;
		}

		public virtual string GenerateXML() {
			string rstr = "<Keywords>\r\n";
			foreach(Keyword keyword in _keywords) {
				rstr += IdentXML(keyword.GenerateXML());
			}
			rstr += "</Keywords>\r\n";
			return rstr;
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "keyword") {
					Keyword keyword = new Keyword();
					keyword.Load(node);
					_keywords.Add(keyword);
				}
			}
		}
		// load from an xml string 
		public virtual void LoadXml(string xml) {
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from an xml document object
		public virtual void Load(System.Xml.XmlDocument doc) {
			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a stream
		public virtual void Load(System.IO.Stream inStream) {
			XmlDocument doc = new XmlDocument();
			doc.Load(inStream);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a text reader
		public virtual void Load(System.IO.TextReader txtReader) {
			XmlDocument doc = new XmlDocument();
			doc.Load(txtReader);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from an xml reader
		public virtual void Load(System.Xml.XmlReader reader) {
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a xml filename
		public virtual void Load(string filename) {
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		#endregion

		#endregion

		#region Properties
		public ArrayList Keyword {
			set { _keywords = value; }
			get { return _keywords; }
		}
		#endregion
	}
}
