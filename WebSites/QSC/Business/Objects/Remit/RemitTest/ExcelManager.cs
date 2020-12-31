using System;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Text;
using FileStore;
using QSPFulfillment.DataAccess.Common;

namespace Business.Objects.RemitTests
{
	/// <summary>
	/// creates excel files from datasets
	/// </summary>
	/// <remarks>
	/// Madina Saitakhmetova
	/// August 2006
	/// </remarks>
	public class ExcelManager
	{
        private const string XML_STYLESHEET = "RemitAutomation_Excel_Stylesheet";

		protected byte [] fileContent;
		protected XmlDataDocument xdd;
		
		public ExcelManager(DataSet dsSource)
		{	
			XmlWriter writer = null;

			try
			{
				dsSource.EnforceConstraints = false;

				xdd = new XmlDataDocument(dsSource);

				XmlDocument xd = (XmlDocument) xdd;

				//Create an XML declaration. 
				XmlDeclaration xmldecl;
				xmldecl = xd.CreateXmlDeclaration("1.0",null,null);

				//Add the new node to the document.
				XmlElement root = xd.DocumentElement;
				xd.InsertBefore(xmldecl, root);

				MemoryStream ms = new MemoryStream();			

				XslTransform  xt = new XslTransform();
				xt.Load(System.Configuration.ConfigurationSettings.AppSettings[XML_STYLESHEET]);
				writer = new  XmlTextWriter(ms, Encoding.ASCII);
				xt.Transform(xd, null, writer, null);
				fileContent = ms.ToArray();
			}
			catch(Exception ex)
			{
				ApplicationError.ManageError(ex);
				throw ex;
			}
			finally
			{
				writer.Close();
			}
		}

		public byte[] ExcelFile
		{
			get
			{
				return this.fileContent;
			}
		}
	}
}
