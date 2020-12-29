/* Jean-Francois Buist
 * Mars 2005
 * 
 * This function is the object that will fill and search from xml
 * file to objects 
 * 
 */

using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace GA.BDC.Core.Web.UI.UIControls.Config {

	/// <summary>
	/// Summary description for PartnerConfigurations.
	/// </summary>
	[Serializable]
	public class UIController {
		private VersionHistory versionHistory;
		private Title title;
		private Header header;
		private UIControls uiControls;
		private string xml;
		private bool runTime = false;
		private ArrayList binaryData = null;	// this is used to hold the binary data when writing
												// instead of writing it row by row
		
		public UIController():this(false) {
			
		}

		public UIController(bool isOnRuntime) {
			runTime = isOnRuntime;
			versionHistory = new VersionHistory();
			title = new Title();
			header = new Header();
			uiControls = new UIControls();
		}

		public void Save(string filename, string baseFilename) {
			SaveDataToDataSource(baseFilename);
			try {
				GA.BDC.Core.Xml.Serialization.Serializer.SaveObjectToXmlFile(filename, this);
			} catch(System.Exception ex) {
				System.Windows.Forms.MessageBox.Show("Unable to save the UI Config file: " + ex.Message);
			}
		}

		#region Obsolete
		/*
		private void Add(string newLine) {
			xml += newLine + "\r\n";
		}

		private void AddPartnersIDDataToXML(string prefix, PartnersID partnersID) {
			Add(prefix + "<PartnersID>");
			foreach(PartnerID _partner in partnersID.PartnerIdList) {
				Add(prefix + "\t<PartnerID>");
				Add(prefix + "\t\t<ID>" + _partner.ID + "</ID>");
				Add(prefix + "\t\t<Cultures>");
				foreach(Culture culture in _partner.Cultures.CultureList) {
					Add(prefix + "\t\t\t<Culture>");
					Add(prefix + "\t\t\t\t<ID>" + culture.ID + "</ID>");
					Add(prefix + "\t\t\t\t<Data>");
					Add(prefix + "\t\t\t\t\t<Source>" + culture.Data.Source + "</Source>");
					Add(prefix + "\t\t\t\t\t<Params>");
					foreach(String s in culture.Data.Parameters.Parameter) {
						Add(prefix + "\t\t\t\t\t\t<Param>" + s + "</Param>");
					}
					Add(prefix + "\t\t\t\t\t</Params>");
					Add(prefix + "\t\t\t\t</Data>");
					Add(prefix + "\t\t\t</Culture>");
				}
				Add(prefix + "\t\t</Cultures>");
				Add(prefix + "\t</PartnerID>");
			}
			Add(prefix + "</PartnersID>");
		}

		public string GenerateXML(string baseFilename) {
			
			SaveDataToDataSource(baseFilename);

			xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n";
			Add("<PageConfig>");
			Add("\t<VersionHistory>");
			foreach(History history in VersionHistory.HistoryList) {
				Add("\t\t<History>");

				Add("\t\t\t<Author>" + history.Author + "</Author>");
				Add("\t\t\t<DateTime>" + history.Date.ToShortDateString() + "</DateTime>");
				Add("\t\t\t<Modification>" + history.Modification + "</Modification>");

				Add("\t\t</History>");
			}
			Add("\t</VersionHistory>");

			Add("\t<Title>");
			AddPartnersIDDataToXML("\t\t", title.PartnersId);
			Add("\t</Title>");

			Add("\t<Header>");
			Add("\t\t<MetaTags>");
			foreach(MetaTag mt in header.MetaTags.MetaTagList) {
				Add("\t\t\t<MetaTag>");
				Add("\t\t\t\t<Name>" + mt.Name + "</Name>");
				AddPartnersIDDataToXML("\t\t\t\t", mt.PartnersIds);
				Add("\t\t\t</MetaTag>");
			}
			Add("\t\t</MetaTags>");
			Add("\t</Header>");

			Add("\t<UIControls>");
			foreach(UIControl uiCon in uiControls.ControlList) {
				Add("\t\t<UIControl>");
				Add("\t\t\t<ID>" + uiCon.ID + "</ID>");
				Add("\t\t\t<Name>" + uiCon.Name + "</Name>");
				Add("\t\t\t<Type>" + uiCon.Type + "</Type>");
				Add("\t\t\t<Params>");
				foreach(string  s in uiCon.Parameters.Parameter) {
					Add("\t\t\t\t<Param>" + s + "</Param>");
				}
				Add("\t\t\t</Params>");
				AddPartnersIDDataToXML("\t\t\t", uiCon.PartnersID);
				Add("\t\t</UIControl>");
			}
			Add("\t</UIControls>");
			Add("</PageConfig>");
			return xml;
		}

		*/
		#endregion

		private void SavePartnerID(PartnersID pIDs, string key, string baseFilename) {

			foreach(PartnerID p in pIDs.PartnerIdList) {
				foreach(Culture c in p.Cultures.CultureList) {
					if(c.Data.Source == "Text File") {
						GA.BDC.Core.Utilities.IO.IniFileHandler iniFile =
							new GA.BDC.Core.Utilities.IO.IniFileHandler(baseFilename + ".ini");
						iniFile.WriteValue(key, p.ID + ":" + c.ID, c.Data.Parameters.Parameter[0].ToString());
						c.Data.Parameters.Parameter[0] = key;
					} else if(c.Data.Source == "Binary File") {
						string[] data = new string[3];
						data[0] = key;
						data[1] = p.ID + ":" + c.ID;
						data[2] = c.Data.Parameters.Parameter[0].ToString();
						binaryData.Add(data);
					}
				}
			}
		}
		
		private void SaveDataToDataSource(string baseFilename) {
			binaryData = new ArrayList();

			SavePartnerID(Title.PartnersId, "Page:Title", baseFilename);
			foreach(MetaTag mt in header.MetaTags.MetaTagList) {
				SavePartnerID(mt.PartnersIds, "MetaTag:" + mt.Name, baseFilename);
			}
			foreach(UIControl c in uiControls.ControlList) {
				SavePartnerID(c.PartnersID, c.Name, baseFilename);
			}

			// save the binary file
			try {
//				if(System.IO.File.Exists(baseFilename + ".bin.bak")) {
//					System.IO.File.Delete(baseFilename + ".bin.bak");
//				}
//
//				if(System.IO.File.Exists(baseFilename + ".bin")) {
//					System.IO.File.Move(baseFilename + ".bin", baseFilename + ".bin.bak");
//				}
				try {
					System.IO.File.Delete(baseFilename + ".xml");
				} catch(System.Exception ex) {
					System.Windows.Forms.MessageBox.Show("Unable to delete file: " + ex.Message);
					return;
				}


				GA.BDC.Core.Xml.Serialization.Serializer.SaveObjectToBinaryFile(baseFilename + ".bin",
					binaryData);
			} catch(System.Exception ex) {
				// todo: notify 
				throw ex;
			}
		}

		public static UIController Load(string filename) {
			UIController controler = null;
			controler = (UIController)GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromXmlFile(filename, typeof(UIController));
			return controler;
		}

		#region Obsolete
		/*
		/// <summary>
		/// Get the full content of the stream object
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		private string GetXmlFromStream(Stream stream) {
			string xml = "";
			StreamReader sr = new StreamReader(stream);
			xml = sr.ReadToEnd();
			sr.Close();
			return xml;
		}

		public void Load(string filename) {
			Load(new System.IO.StreamReader(filename).BaseStream);
		}

		/// <summary>
		/// Load xml configuration
		/// </summary>
		/// <param name="stream"></param>
		public void Load(Stream stream) {
			XmlDocument doc = new XmlDocument();
			string xml = GetXmlFromStream(stream);
			doc.LoadXml(xml);
			foreach(XmlNode node in doc.ChildNodes) {
				if(node.Name.ToLower() == "PageConfig".ToLower()) {
					Load(node);
				}
			}
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "VersionHistory".ToLower()) {
					versionHistory.Load(child);
				} else if(child.Name.ToLower() == "Title".ToLower()) {
					title.Load(child);
				} else if(child.Name.ToLower() == "Header".ToLower()) {
					header.Load(child);
				} else if(child.Name.ToLower() == "UIControls".ToLower()) {
					uiControls.Load(child);
				}
			}
		} */
		#endregion

		private string GetBinaryString(string key, string subKey) {
			foreach(string[] s in binaryData) {
				if(s[0] == key) {
					if(s[1] == subKey) {
						return s[2];
					}
				}
			}
			return "";
		}

		private void ReadPartnerID(PartnersID pIDs, string key, string baseFilename) {
			foreach(PartnerID p in pIDs.PartnerIdList) {
				foreach(Culture c in p.Cultures.CultureList) {
					if(c.Data.Source == "Text File") {
						GA.BDC.Core.Utilities.IO.IniFileHandler iniFile =
							new GA.BDC.Core.Utilities.IO.IniFileHandler(baseFilename + ".ini");
						c.Data.Parameters.Parameter[0] = System.Web.HttpUtility.HtmlDecode(iniFile.ReadValue(key, p.ID + ":" + c.ID));
					} else if(c.Data.Source == "URL") {
						if(runTime) {
							try {
								try {
									c.Data.Parameters.Parameter[0] = GA.BDC.Core.Utilities.Net.URL.GetPageContent(c.Data.Parameters.Parameter[0].ToString());
								} catch(System.Exception ex) {
									throw ex;	// todo notify admins
								}
							} catch(System.Exception ex) {
								throw ex;
								// todo notify admins
							}
						}
					} else if(c.Data.Source == "Binary Text") {
						c.Data.Parameters.Parameter[0] = GetBinaryString(key, p.ID + ":" + c.ID);
					}
				}
			}
		}

		private void SetDynamicTags(PartnersID pIDs, DynTag dTags) {
			foreach(PartnerID p in pIDs.PartnerIdList) {
				foreach(Culture c in p.Cultures.CultureList) {
					string source = c.Data.Parameters.Parameter[0].ToString();
					ArrayList commands = GA.BDC.Core.Parsers.SimpleParser.SimpleTagParser.GetCommandLines(source);
					foreach(string cmd in commands) {
						GA.BDC.Core.Parsers.SimpleParser.SimpleTagParser stp =
							new GA.BDC.Core.Parsers.SimpleParser.SimpleTagParser(cmd);
						if(stp.VariableName == "DynTags") {
							string val = dTags[stp.VariableKey];
							foreach(string function in stp.Functions) {
								switch(function) {
									case "ToUpper()":
										val = val.ToUpper();
										break;
									case "ToLower()":
										val = val.ToLower();
										break;
									case "ToTitleCase()":
										System.Globalization.TextInfo ti =
											System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo;
										val = ti.ToTitleCase(val);
										break;
									case "HtmlEncode()":
										val = System.Web.HttpUtility.HtmlEncode(val);
										break;
									case "HtmlDecode()":
										val = System.Web.HttpUtility.HtmlDecode(val);
										break;
									case "UrlEncode()":
										val = System.Web.HttpUtility.UrlEncode(val);
										break;
									case "UrlDecode()":
										val = System.Web.HttpUtility.UrlDecode(val);
										break;
									case "ToPercent()":
										val = val + "%";
										break;
									case "ToDate().ToShortDateString()":
										// todo: test this
										try {
											DateTime dt = DateTime.Parse(val, System.Threading.Thread.CurrentThread.CurrentCulture);
											val = dt.ToShortDateString();
										} catch {}
										break;
									case "ToCurrency()":
										try {
											// try to convert, otherwise, let the current value
											int v = int.Parse(val);
											val = v.ToString("c", System.Threading.Thread.CurrentThread.CurrentCulture);
										} catch {}
										break;
								}
							}
							source = source.Replace(cmd, val);
							c.Data.Parameters.Parameter[0] = source;
						}
					}					
				}
			}
		}

		public void SetDynamicTags(DynTag dTags) {
			SetDynamicTags(Title.PartnersId, dTags);
			foreach(MetaTag mt in Header.MetaTags.MetaTagList) {
				SetDynamicTags(mt.PartnersIds, dTags);
			}
			foreach(UIControl c in uiControls.ControlList) {
				SetDynamicTags(c.PartnersID, dTags);
			}
		}

		public void ReadDataFromDataSource(string baseFilename) {
			try {
				if(System.IO.File.Exists(baseFilename + ".bin")) {
					// PanelControl.SetPageAttributeToNormal(baseFilename + ".bin");
					binaryData = (ArrayList)GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromBinaryFile(baseFilename + ".bin");
				} else {
					binaryData = new ArrayList();
				}
			} catch(System.Exception ex) {
				// todo: notify 
				throw ex;
			}

			ReadPartnerID(Title.PartnersId, "Page:Title", baseFilename);
			foreach(MetaTag mt in header.MetaTags.MetaTagList) {
				ReadPartnerID(mt.PartnersIds, "MetaTag:" + mt.Name, baseFilename);
			}
			foreach(UIControl c in uiControls.ControlList) {
				ReadPartnerID(c.PartnersID, c.Name, baseFilename);
			}

		}

		public VersionHistory VersionHistory {
			set { versionHistory = value; }
			get { return versionHistory; }
		}

		public Title Title {
			set { title = value; }
			get { return title; }
		}

		public Header Header {
			set { header = value; }
			get { return header; }
		}

		public UIControls UiControls {
			set { uiControls = value; }
			get { return uiControls; }
		}

	}
}

