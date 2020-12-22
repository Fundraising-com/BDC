using System;
using System.IO;
using System.Xml;
using System.Collections;
using GA.BDC.Core.efundraisingCore;
using GA.BDC.Core.Utilities;

namespace GA.BDC.WEB.ScratchcardWeb.Components.Server.EmailTemplate {

	public sealed class EmailTemplate {
		
		private string _CultureName;
		private string _PartnerID;
		private string _MailDesignPath;
		private string _Subject;

		private ArrayList _TagList;
		private ArrayList _BindingList;
		private ArrayList _ActionList;
		private ArrayList _SourceObject;

		public EmailTemplate() {
			this._TagList = new ArrayList();
			this._BindingList = new ArrayList();
			this._ActionList = new ArrayList();
			this._SourceObject = new ArrayList();
		}

		public string PartnerID {
			get{ return this._PartnerID; }
			set{ this._PartnerID = value; }
		}

		public string MailDesignPath {
			get{ return this._MailDesignPath; }
			set{ this._MailDesignPath = value; }
		}

		public ArrayList TagList {
			get{ return this._TagList; }
			set{ this._TagList = value; }
		}

		public ArrayList BindingList {
			get{ return this._BindingList; }
			set{ this._BindingList = value; }
		}

		public ArrayList ActionList {
			get{ return this._ActionList; }
			set{ this._ActionList = value; }
		}

		public ArrayList SourceObject {
			get{ return this._SourceObject; }
			set{ this._SourceObject = value; }
		}

		public string Subject {
			get{ return this._Subject; }
			set{ this._Subject = value; }
		}

		public string CultureName {
			get{ return this._CultureName; }
			set{ this._CultureName = value; }
		}
	}

	public sealed class EmailTemplateBuilder {

		internal sealed class XPATH {
            public const string __NAME = "[NAME]";
			public const string __CULTURE = "[CULTURENAME]";
			public const string __XPATH_TEMPLATE = "//Templates[@Name='[NAME]']/Culture[@Name='[CULTURENAME]']/Template";
			public const string __XPATH_CULTURE = "//Templates[@Name='[NAME]']/Culture[@Name='[CULTURENAME]']";
			public const string __XPATH_PARTNER_TEMPLATE = "Template[@Name='[NAME]']";
		}

		private string _BasePath = "";

		private XmlDocument _XmlDoc;
	
		public EmailTemplateBuilder(XmlDocument pXmlDoc, string pBasePath) {
			this._XmlDoc = pXmlDoc;
			this._BasePath = pBasePath;
		}

		public bool GetPartnerHasEmailTemplate(XmlNode pNode, int pPartnerID) {
			if(pNode.SelectSingleNode("Template[@PartnerID='" + pPartnerID.ToString() + "']") == null)
				return false;
			return true;
		}

		public EmailTemplate GetEmailTemplateObject(string pCultureCode, string pEmailName, int pPartnerID) {
			EmailTemplate oTmp = new EmailTemplate();
			this.ParseEmailTemplateData(ref oTmp, pCultureCode, pEmailName, pPartnerID);
			return oTmp;
		}

		private void ParseEmailTemplateData(ref EmailTemplate pEmailTemplate, string pCultureCode, string pEmailName, int pPartnerID) {
			System.Xml.XmlDocument oDoc = this._XmlDoc;// new System.Xml.XmlDocument();
			try {
				//oDoc.Load(this.TemplateConfigFile);
				XmlNode oXCult = oDoc.DocumentElement.SelectSingleNode(XPATH.__XPATH_CULTURE.Replace(XPATH.__NAME, pEmailName).Replace(XPATH.__CULTURE, pCultureCode));
				if(oXCult != null) {
					pEmailTemplate.CultureName = oXCult.Attributes["Name"].InnerText;
					pEmailTemplate.Subject = oXCult.Attributes["Subject"].InnerText;
					if(!this.GetPartnerHasEmailTemplate(oXCult, pPartnerID))
						pPartnerID = 0;
					XmlNode oXTemp = oXCult.SelectSingleNode("Template[@PartnerID='" + pPartnerID.ToString() + "']");
					if(oXTemp != null) {
						pEmailTemplate.MailDesignPath = this._BasePath + @"\" + oXTemp.Attributes["MailDesignPath"].InnerText;
						//pEmailTemplate.MailDesignPath = @"\" + oXTemp.Attributes["MailDesignPath"].InnerText;
						pEmailTemplate.PartnerID = oXTemp.Attributes["PartnerID"].InnerText;
						XmlNodeList oXTags = oXTemp.FirstChild.ChildNodes;
						for(int i=0;i<oXTags.Count;i++) { 
							pEmailTemplate.TagList.Add(oXTags.Item(i).Attributes["Name"].InnerText);
							pEmailTemplate.BindingList.Add(oXTags.Item(i).Attributes["BindingField"].InnerText);
							pEmailTemplate.ActionList.Add(oXTags.Item(i).Attributes["Action"].InnerText);
							pEmailTemplate.SourceObject.Add(oXTags.Item(i).Attributes["SourceObject"].InnerText);
						}
					}
				}				
			} catch(System.Xml.XmlException ex) {
				throw ex;
			} catch {
				throw;
			}
		}
	}

	public class EmailPreview {
		private string _Subject;
		private string _Body;

		public EmailPreview(){}

		public EmailPreview(string pSubject, string pBody) {
			this._Subject = pSubject;
			this._Body = pBody;
		}

		public string Subject {
			get{ return this._Subject; }
			set{ this._Subject = value; }
		}
		
		public string Body {
			get{ return this._Body; }
			set{ this._Body = value; }
		}

		public static EmailPreview CreatePreview(GA.BDC.Core.efundraisingCore.eFundEnv pEnvironment, string pTemplateName, string pFilename) {
			XmlDocument oXml = new XmlDocument();
			if(System.Web.HttpContext.Current.Server != null)
				pFilename = System.Web.HttpContext.Current.Server.MapPath(pFilename);
			oXml.Load(pFilename);
			return EmailBodyBuilder.GetEmailBodyTemplate(pEnvironment, pTemplateName, oXml);
		}
	}

	public sealed class EmailBodyBuilder 
	{
		
		public static EmailPreview GetEmailBodyTemplate(GA.BDC.Core.efundraisingCore.eFundEnv pEFundEnv, string pTemplateName, XmlDocument pXmlDoc) 
		{
			EmailPreview oPrev = new EmailPreview();

			EmailTemplateBuilder oBuilder = new EmailTemplateBuilder(pXmlDoc, System.Web.HttpContext.Current.Request.PhysicalApplicationPath);
			EmailTemplate oEmailTemp = oBuilder.GetEmailTemplateObject(pEFundEnv.CultureName, pTemplateName, pEFundEnv.PartnerInfo.PartnerID);
			oPrev.Subject = oEmailTemp.Subject;
			Promotion promo = Promotion.GetPromotion(pEFundEnv.PromotionID);

			System.IO.StreamReader oFile = new StreamReader(oEmailTemp.MailDesignPath);
			string oMailBody = oFile.ReadToEnd();
			oFile.Close();
			for(int i=0;i<oEmailTemp.TagList.Count;i++) 
			{
				if(oEmailTemp.BindingList[i].ToString() != "") 
				{
					switch(oEmailTemp.ActionList[i].ToString().ToLower()) 
					{
						case "replace":
							if(oEmailTemp.SourceObject[i].ToString() == "eFundEnv.PartnerInfo")
								oMailBody = oMailBody.Replace(oEmailTemp.TagList[i].ToString(),
                                   GA.BDC.Core.Utilities.Reflection.Reflect.GetProperty(oEmailTemp.BindingList[i].ToString(), pEFundEnv.PartnerInfo));
							else if(oEmailTemp.SourceObject[i].ToString() == "Lead")
								oMailBody = oMailBody.Replace(oEmailTemp.TagList[i].ToString(), 
									GA.BDC.Core.Utilities.Reflection.Reflect.GetProperty(oEmailTemp.BindingList[i].ToString(), pEFundEnv.LeadObject));
							else if(oEmailTemp.SourceObject[i].ToString() == "eFundEnv")
								oMailBody = oMailBody.Replace(oEmailTemp.TagList[i].ToString(),
                                    GA.BDC.Core.Utilities.Reflection.Reflect.GetProperty(oEmailTemp.BindingList[i].ToString(), pEFundEnv));
							else if(oEmailTemp.SourceObject[i].ToString() == "Promotion")
								oMailBody = oMailBody.Replace(oEmailTemp.TagList[i].ToString(),
                                    GA.BDC.Core.Utilities.Reflection.Reflect.GetProperty(oEmailTemp.BindingList[i].ToString(), promo));
							break;
						case "assign":
							if(oEmailTemp.SourceObject[i].ToString() == "eFundEnv.PartnerInfo")
								oMailBody = oMailBody.Replace(oEmailTemp.TagList[i].ToString() + "=", oEmailTemp.TagList[i].ToString() + "=" +
                                    GA.BDC.Core.Utilities.Reflection.Reflect.GetProperty(oEmailTemp.BindingList[i].ToString(), pEFundEnv.PartnerInfo));
							else if(oEmailTemp.SourceObject[i].ToString() == "Lead")
								oMailBody = oMailBody.Replace(oEmailTemp.TagList[i].ToString() + "=", oEmailTemp.TagList[i].ToString() + "=" +
                                    GA.BDC.Core.Utilities.Reflection.Reflect.GetProperty(oEmailTemp.BindingList[i].ToString(), pEFundEnv.LeadObject));
							else if(oEmailTemp.SourceObject[i].ToString() == "eFundEnv")
								oMailBody = oMailBody.Replace(oEmailTemp.TagList[i].ToString() + "=", oEmailTemp.TagList[i].ToString() + "=" +
                                    GA.BDC.Core.Utilities.Reflection.Reflect.GetProperty(oEmailTemp.BindingList[i].ToString(), pEFundEnv));
							else if(oEmailTemp.SourceObject[i].ToString() == "Promotion")
								oMailBody = oMailBody.Replace(oEmailTemp.TagList[i].ToString() + "=", oEmailTemp.TagList[i].ToString() + "=" +
                                    GA.BDC.Core.Utilities.Reflection.Reflect.GetProperty(oEmailTemp.BindingList[i].ToString(), promo));
							break;
					}		
				}
			}
			oPrev.Body = oMailBody;
			return oPrev;
		}
	}
}
