//
//	March 17, 2005	-	Louis Turmel	-	Code Comments
//	April 28, 2005	-	Louis Turmel	-	Implement Culture Feature for XML Parser Instruction
//

using System;
using System.Xml;

namespace GA.BDC.Core.Utilities.eSubsXmlConfig
{

	/// <summary>
	/// Instruction container
	/// </summary>
	public struct InstructionsContainer {
		public string Name;
		public string Type;
		public string FileType;
		public string DisplayText;
		public Step[] Steps;
	}
	
	/// <summary>
	/// Step container
	/// </summary>
	public struct Step {
		public byte StepNo;
		public string Text;

		/// <summary>
		/// Get the Display Text Attribute
		/// </summary>
		public string DisplayText {
			get{ return this.Text; }
		}
	}

	/// <summary>
	/// class containing somes static function to retreive the import your address book instructions
	/// </summary>
	public class ImportInstructions {
		
		#region private const

		private const string __RequestTypeAttribute = "[FileType]";
		private const string __RequestFileTypeAttribute = "[Type]";
		private const string __RequestNameAttribute = "[Name]";
		private const string __RequestCultureNameAttribute = "[CultureName]";
		private const string __RequestDisplayTextAttribute = "[DisplayText]";

		/// <summary>
		/// XML XPath Query
		/// </summary>
		public const string __XPath_Instructions = "//ImportInstructions[@CultureName='[CultureName]']/Instructions[@Name='[Name]' and @Type='[Type]' and @FileType='[FileType]']";
	
		#endregion

		/// <summary>
		/// static function return all step for a specific contact list type
		/// </summary>
		/// <param name="pFilename"></param>
		/// <param name="pSoft"></param>
		/// <param name="pType"></param>
		/// <param name="pExt"></param>
		/// <param name="pCultureName"></param>
		/// <returns></returns>
		public static InstructionsContainer GetImportInstructions(string pFilename,string pSoft, string pType, string pExt, string pCultureName) {
			InstructionsContainer oInstruction = new InstructionsContainer();
			XmlDocument oDoc = new XmlDocument();
			try {
				oDoc.Load(pFilename);
				string oXPathQuery = __XPath_Instructions.Replace(__RequestFileTypeAttribute,pExt).Replace(__RequestTypeAttribute, pType).Replace(__RequestNameAttribute,pSoft).Replace(__RequestCultureNameAttribute, pCultureName);
				XmlNode oInstructionNode = oDoc.SelectSingleNode(oXPathQuery);
				if(oInstructionNode == null)
					throw new XmlException("The current XML XPath Query cannot return valid resultset");
				Step[] oStep = new Step[oInstructionNode.ChildNodes.Count];
				for(byte i=0;i<oInstructionNode.ChildNodes.Count;i++) {
					oStep[i] = new Step();
					oStep[i].StepNo = byte.Parse(oInstructionNode.ChildNodes[i].Attributes["No"].Value);
					oStep[i].Text = oInstructionNode.ChildNodes[i].InnerText;
				}				
				oInstruction.Steps = oStep;
				oInstruction.Name = oInstructionNode.Attributes["Name"].Value;
				oInstruction.Type = oInstructionNode.Attributes["Type"].Value;
				oInstruction.FileType = oInstructionNode.Attributes["FileType"].Value;
				oInstruction.DisplayText = oInstructionNode.Attributes["DisplayText"].Value;
			} catch(System.Xml.XmlException ex) {
				 if(pType.Length <= 0)
					throw new Exception("The parameter pType should contain valid Type Name",ex);
				else if(pExt.Length <= 0)
					throw new Exception("The parameter pExt should contain valid Extention name",ex);
				else
					throw ex;
			} catch(System.IO.IOException ex) {
				if(pFilename.Length <= 0)
					throw new Exception("The parameter pFilename should contain valid filename",ex);
				else 
					throw ex;
			} catch(Exception ex) {
				throw ex;
			} finally {
			
			}
			return oInstruction;
		}
	}
}
