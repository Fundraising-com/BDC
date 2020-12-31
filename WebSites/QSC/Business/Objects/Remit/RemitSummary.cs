using System;
using QSPFulfillment.DataAccess.Common;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Text;
using System.Data;
using DAL;
using dataAccessRef = DAL.RemitSummaryData;

namespace Business.Objects
{
	/// <summary>
	/// Returns remit summary 
	/// </summary>
	/// <remarks>
	/// Madina Saitakhmetova
	/// August 2006
	/// </remarks>
	public class RemitSummary : BusinessSystem
	{
		protected const string TABLE_NAME = "RemitSummary";
		protected const string REMIT_SUMMARY_HEADER = "<h4 style=\"font-family:arial;\">Remit Summary</h4>";
		protected const string REMIT_SUMMARY_FOOTER = "<br><br><br>";
		private const string XML_STYLESHEET = "RemitAutomation_HTML_Stylesheet";

        protected dataAccessRef dataAccess;
		protected DataSet dsRemitSummary;
		protected string _htmlTable = "";
		protected XmlDataDocument xdd;

		#region Properties

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		internal override DataSet baseDataSet
		{
			get
			{
				if(dsRemitSummary == null)
				{
					dsRemitSummary = new DataSet();
				}

				return this.dsRemitSummary;
			}
		}

		public override string DefaultTableName
		{
			get
			{
				return TABLE_NAME;
			}
		}

		public string htmlTable
		{
			get
			{
				if(_htmlTable.Length == 0)
				{
					XmlWriter writer = null;

					try
					{
						//fill html table with data from dataset
						dsRemitSummary.EnforceConstraints = false;

						xdd = new XmlDataDocument(dsRemitSummary);

						//Create an XML declaration. 
						XmlDeclaration xmldecl;
						xmldecl = xdd.CreateXmlDeclaration("1.0",null,null);

						//Add the new node to the document.
						XmlElement root = xdd.DocumentElement;
						xdd.InsertBefore(xmldecl, root);

						MemoryStream ms = new MemoryStream();			

						XslTransform  xt = new XslTransform();
						xt.Load(System.Configuration.ConfigurationSettings.AppSettings[XML_STYLESHEET]);
						writer = new  XmlTextWriter(ms, Encoding.ASCII);
						xt.Transform(xdd, null, writer, null);

						BinaryReader br = new BinaryReader(ms);

						ASCIIEncoding ascii = new ASCIIEncoding();

						_htmlTable = REMIT_SUMMARY_HEADER + ascii.GetString(ms.ToArray()) + REMIT_SUMMARY_FOOTER;
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

				return _htmlTable;
			}
		}
	
		#endregion

		public RemitSummary(int runID)
		{
			dataAccess = new dataAccessRef();
 
			try
			{
				dataAccess.SelectAll(this.baseDataSet, runID, this.DefaultTableName);
			}
			catch(Exception ex)
			{				
				ApplicationError.ManageError(ex);
				throw ex;
			}
		}
	}
}
