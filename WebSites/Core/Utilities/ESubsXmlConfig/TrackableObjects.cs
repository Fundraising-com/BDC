//
//	February 3, 2005	-	Louis Turmel	Project Added
//	March	17, 2005	-	Louis Turmel	Code Comments
//

using System;
using System.Xml;

namespace GA.BDC.Core.Utilities.ESubsXmlConfig
{

	/// <summary>
	/// class struct for storing all informations of Tracking object
	/// </summary>
	public struct TrackableButtonList {
		public string CodeName;
		public string Text;
		public string ObjectType;
		public string NextPageUrl;
		public string ImageUrl;
		public string ContactListType;
		public bool Visible;
	}

	/// <summary>
	/// class containing some static methods and functions to get the List of TrackableButton
	/// </summary>
	public class TrackableObjects {
	
		#region public static methods

		/// <summary>
		/// static function returning the list of trackable button from specified Group Objects Name
		/// </summary>
		/// <param name="pFilename"></param>
		/// <param name="pGroupObjectName"></param>
		/// <returns></returns>
		public static TrackableButtonList[] GetTrackableButtonConfig(string pFilename, string pGroupName, string pGroupObjectName) {
			TrackableButtonList[] oTrackableObject = null;
			XmlDocument oDoc = new XmlDocument();
			try {
				oDoc.Load(pFilename);				
				// Get all node of the specified Object Name
				string oFind = Variables.TrackableVars.__XPath_GetGroupType.Replace(
					Variables.TrackableVars.__NodeName, pGroupName).Replace(Variables.TrackableVars.__RequestAttribute, pGroupObjectName);
				XmlNodeList oList = oDoc.SelectNodes(oFind);
				oTrackableObject = new TrackableButtonList[oList.Count];
				for(byte i=0;i<oList.Count;i++) {
					oTrackableObject[i].CodeName = oList.Item(i).Attributes["CodeName"].Value;
					oTrackableObject[i].ImageUrl = oList.Item(i).Attributes["ImageUrl"].Value;
					oTrackableObject[i].NextPageUrl = oList.Item(i).Attributes["NextPageUrl"].Value.Replace("~","&");
					oTrackableObject[i].ObjectType = oList.Item(i).Attributes["ObjectType"].Value;
					oTrackableObject[i].Visible = Convert.ToBoolean(oList.Item(i).Attributes["Visible"].Value);
					oTrackableObject[i].Text = oList.Item(i).Attributes["Text"].Value;
				}        
			} catch(System.Xml.XmlException ex) {
				if(pGroupObjectName.Length <= 0)
					throw new Exception("The parameter pGroupObjectName should contain valid GroupObjectName",ex);
				else
					throw ex;
			} catch(System.IO.IOException ex) {
				if(pFilename.Length <= 0)
					throw new Exception("The parameter pFilename should contain valid filename",ex);
			} catch(Exception ex) {
				throw ex;
			} finally {

			}
			return oTrackableObject;
		}

		#endregion
	}
}

