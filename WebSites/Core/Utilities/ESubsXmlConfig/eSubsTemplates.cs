//
//	April 29, 2005	-	Louis Turmel	-	Class Implementation added
//

using System;
using System.Xml;

namespace GA.BDC.Core.Utilities.eSubsXmlConfig
{

	/// <summary>
	/// class Structure containing informations about an Templated Partner
	/// </summary>
	/// <remarks>The information are based from Template Xml File</remarks>
	public struct TemplateInfo {
		
		#region private members

		private int _PartnerID;
		private string _PartnerName;
		private bool _HaveCM;

		#endregion

		#region public constructor

		/// <summary>
		/// class struct constructor
		/// </summary>
		/// <param name="pPartnerID">Partner ID Number</param>
		/// <param name="pPartnerName">Partner Name</param>
		/// <param name="pHaveCM">Access at an Campaign Manager Pages</param>
		public TemplateInfo(int pPartnerID, string pPartnerName, bool pHaveCM) {
			this._PartnerID = pPartnerID;
			this._PartnerName = pPartnerName;
			this._HaveCM = pHaveCM;
		}

		#endregion

		#region public Attributes

		/// <summary>
		/// Get - Set the PartnerID
		/// </summary>
		public int PartnerID {
			get{ return this._PartnerID; }
			set{ this._PartnerID = value; }
		}

		/// <summary>
		/// Get - Set the PartnerName
		/// </summary>
	 	public string PartnerName {
			get{ return this._PartnerName; }
			set{ this._PartnerName = value; }
		}

		/// <summary>
		/// Get - Set if the current Partner have an Campaign Manager
		/// </summary>
		public bool HaveCampaignManager {
			get{ return this._HaveCM; }
			set{ this._HaveCM = value; }
		}

		#endregion
	}

	/// <summary>
	/// Class given access to somes static methods and function
	/// </summary>
	public sealed class eSubsTemplates {

		#region private members

		public const string __PARTNER_ID = "[PartnerID]";
		public const string __SINGLE_TEMPLATE = "//eSubsPages/Templates/Template[@partnerID='[PartnerID]']";
		
		#endregion

		#region public class constructors
		
		/// <summary>
		/// default class constructor
		/// </summary>
		public eSubsTemplates() {
			
		}

		#endregion

		#region public static functions

		/// <summary>
		/// Static function to get if a specific PartnerID have access
		/// to CampaignManager Section
		/// </summary>
		/// <param name="pFilename">File name where the Template XML file is located</param>
		/// <param name="pPartnerID">Partner ID Number</param>
		/// <returns></returns>
		public static bool HaveCampaignManager(string pFilename, int pPartnerID) {
			bool oHaveCM = false;			
			#region ArgumentNullException & ArgumentException blocks validation

			if(pFilename == null)
				throw new ArgumentNullException("pFilename", "Null parameter value is not accepted");
			if(pFilename.Length == 0)
				throw new ArgumentException("The parameter not accept an Empty string as value","pFilename");
			
			#endregion
			XmlDocument oXml = new XmlDocument();
			try {
				oXml.Load(pFilename);
				XmlNode oTmp = oXml.DocumentElement.SelectSingleNode(__SINGLE_TEMPLATE.Replace(__PARTNER_ID, pPartnerID.ToString()));
				if(oTmp != null) {
					oHaveCM = bool.Parse(oTmp.Attributes["HaveCM"].InnerText);
				}
			} catch(Exception ex) {
				throw ex;
			}
			return oHaveCM;
		}

		#endregion
	}
}
