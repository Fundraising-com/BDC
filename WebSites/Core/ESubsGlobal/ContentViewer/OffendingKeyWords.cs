using System;
using System.Xml;
using System.Collections;

namespace GA.BDC.Core.ESubsGlobal.ContentViewer {

	public class OffendingKeyWords {

		private ArrayList _offendingKeyWords;

		public OffendingKeyWords() {
			_offendingKeyWords = new ArrayList();
		}

		#region Object Manipulation

		// add a new OffendingKeyWord
		public void AddOffendingKeyWord(OffendingKeyWord offendingKeyWord) {
			_offendingKeyWords.Add(offendingKeyWord);
		}

		// remove a OffendingKeyWord
		public void RemoveOffendingKeyWord(OffendingKeyWord offendingKeyWord) {
			_offendingKeyWords.Remove(offendingKeyWord);
		}

		// check if this specified OffendingKeyWord exists
		public bool Exists(OffendingKeyWord offendingKeyWord) {
			return _offendingKeyWords.Contains(offendingKeyWord);
		}

		// clear the list of OffendingKeyWords
		public void Clear() {
			_offendingKeyWords.Clear();
		}

		// get all OffendingKeyWords
		public OffendingKeyWord[] GetAllOffendingKeyWords() {
			OffendingKeyWord[] offendingKeyWord = new OffendingKeyWord[_offendingKeyWords.Count];
			for(int i=0;i<_offendingKeyWords.Count;i++) {
				offendingKeyWord[i] = (OffendingKeyWord)_offendingKeyWords[i];
			}
			return offendingKeyWord;
		}

		// get one OffendingKeyWord by Message
		public OffendingKeyWord GetOffendingKeyWordByMessage(string message) {
			for(int i=0;i<_offendingKeyWords.Count;i++) {
				OffendingKeyWord offendingKeyWord = (OffendingKeyWord)_offendingKeyWords[i];
				if(offendingKeyWord.Message == message) {
					return offendingKeyWord;
				}
			}
			return null;
		}

		// get all OffendingKeyWord by Message
		public OffendingKeyWord[] GetOffendingKeyWordsByMessage(string message) {
			ArrayList list = new ArrayList();
			for(int i=0;i<_offendingKeyWords.Count;i++) {
				OffendingKeyWord offendingKeyWord = (OffendingKeyWord)_offendingKeyWords[i];
				if(offendingKeyWord.Message == message) {
					list.Add(offendingKeyWord);
				}
			}
			if(list.Count < 1) return null;
			OffendingKeyWord[] offendingKeyWords = new OffendingKeyWord[list.Count];
			for(int i=0;i<list.Count;i++) {
				offendingKeyWords[i] = (OffendingKeyWord)list[i];
			}
			return offendingKeyWords;
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
			string rstr = "<OffendingKeyWords>\r\n";
			foreach(OffendingKeyWord offendingKeyWord in _offendingKeyWords) {
				rstr += IdentXML(offendingKeyWord.GenerateXML());
			}
			rstr += "</OffendingKeyWords>\r\n";
			return rstr;
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "offendingkeyword") {
					OffendingKeyWord offendingKeyWord = new OffendingKeyWord();
					offendingKeyWord.Load(node);
					_offendingKeyWords.Add(offendingKeyWord);
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
		public ArrayList OffendingKeyWord {
			set { _offendingKeyWords = value; }
			get { return _offendingKeyWords; }
		}
		#endregion
	}
}
