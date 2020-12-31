using System;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Text;
//using FileStore;
//using QSPFulfillment.DataAccess.Common;

namespace QSP.WebControl
{
	/// <summary>
	/// creates excel files from datasets
	/// </summary>
	/// <remarks>
	/// Madina Saitakhmetova
	/// August 2006
	/// 
	/// Updated by Jeff Miles
	/// October 2006
	/// Purpose: To abstract object for reusability
	/// </remarks>
	public class ExcelManager
	{
		protected byte [] fileContent;
		protected XmlDataDocument xdd;
		
		public ExcelManager(DataSet dsSource, string xmlStyleSheet)
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
				xt.Load(System.Configuration.ConfigurationSettings.AppSettings[xmlStyleSheet]);
				writer = new  XmlTextWriter(ms, Encoding.Unicode);
				xt.Transform(xd, null, writer, null);
				fileContent = ms.ToArray();
			}
			catch(Exception ex)
			{
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
