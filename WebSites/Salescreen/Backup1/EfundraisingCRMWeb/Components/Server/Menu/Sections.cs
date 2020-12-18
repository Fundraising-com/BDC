using System;
using System.Xml;
using System.Collections;

namespace EFundraisingCRMWeb.Components.Server.Menu {
	public class Sections {

		private ArrayList _sections;

		public Sections() {
			_sections = new ArrayList();
		}

		#region Object Manipulation

		// add a new Section
		public void AddSection(Section section) {
			_sections.Add(section);
		}

		// remove a Section
		public void RemoveSection(Section section) {
			_sections.Remove(section);
		}

		// check if this specified Section exists
		public bool Exists(Section section) {
			return _sections.Contains(section);
		}

		// clear the list of Sections
		public void Clear() {
			_sections.Clear();
		}

		// get all Sections
		public Section[] GetAllSections() {
			Section[] section = new Section[_sections.Count];
			for(int i=0;i<_sections.Count;i++) {
				section[i] = (Section)_sections[i];
			}
			return section;
		}

		// get one Section by ID
		public Section GetSectionByID(string id) {
			for(int i=0;i<_sections.Count;i++) {
				Section section = (Section)_sections[i];
				if(section.ID == id) {
					return section;
				}
			}
			return null;
		}

		// get all Section by ID
		public Section[] GetSectionsByID(string id) {
			ArrayList list = new ArrayList();
			for(int i=0;i<_sections.Count;i++) {
				Section section = (Section)_sections[i];
				if(section.ID == id) {
					list.Add(section);
				}
			}
			if(list.Count < 1) return null;
			Section[] sections = new Section[list.Count];
			for(int i=0;i<list.Count;i++) {
				sections[i] = (Section)list[i];
			}
			return sections;
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
			string rstr = "<Sections>\r\n";
			foreach(Section section in _sections) {
				rstr += IdentXML(section.GenerateXML());
			}
			rstr += "</Sections>\r\n";
			return rstr;
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "section") {
					Section section = new Section();
					section.Load(node);
					_sections.Add(section);
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
		public ArrayList Section {
			set { _sections = value; }
			get { return _sections; }
		}
		#endregion
	}
}
