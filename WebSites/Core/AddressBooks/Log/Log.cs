using System;
using System.Xml;

namespace GA.BDC.Core.AddressBooks.Log {

	public class Log {
		
		private XmlDocument _xmlDoc;
		private XmlNode _WorkingErrorNode;
		private string _ErrorFilename;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pCampaignID"></param>
		/// <param name="pErrorFilename"></param>
		public Log(int pCampaignID, string pErrorFilename) {
			this._ErrorFilename = pErrorFilename;
			this._xmlDoc = new XmlDocument();
			try {
				this._xmlDoc.Load(this._ErrorFilename);
			}
			catch(Exception ex) {
				this.CreateMasterRoot();
				this.CreateErrorRoot(pCampaignID);
			}
			try {
				this._WorkingErrorNode = this._xmlDoc.DocumentElement.SelectSingleNode("//Error[@CampaignID=" + pCampaignID.ToString() + "]");
			}
			catch(Exception ex) {
				this.CreateErrorRoot(pCampaignID);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pEmail"></param>
		public void NewContactError(string pEmail) {
			XmlElement oContactError = this._xmlDoc.CreateElement("Contact");
			oContactError.Attributes.Append(this._xmlDoc.CreateAttribute("FirstName"));
			oContactError.Attributes.Append(this._xmlDoc.CreateAttribute("LastName"));
			oContactError.Attributes.Append(this._xmlDoc.CreateAttribute("Email"));
			oContactError.Attributes["Email"].InnerText = pEmail;
			this._WorkingErrorNode.AppendChild(oContactError);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pCampaignID"></param>
		/// <returns></returns>
		private void CreateErrorRoot(int pCampaignID) {
			XmlElement oErrorNode = this._xmlDoc.CreateElement("Error");
			oErrorNode.Attributes.Append(this._xmlDoc.CreateAttribute("CampaignID"));
			oErrorNode.Attributes["CampaignID"].InnerText = pCampaignID.ToString();
			this._xmlDoc.DocumentElement.AppendChild(oErrorNode);
		}

		/// <summary>
		/// 
		/// </summary>
		private void CreateMasterRoot() {
			XmlElement oMaster = this._xmlDoc.CreateElement("AddressBookErrors");
			this._xmlDoc.AppendChild(oMaster);
		}

		/// <summary>
		/// 
		/// </summary>
		public void Save() {
			this._xmlDoc.DocumentElement.AppendChild(this._WorkingErrorNode);
			this._xmlDoc.Save(this._ErrorFilename);
		}	
	}
}
