using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal 
{

	public class Program 
	{

		private int programId;
		private string programName;
		private DateTime createDate;


		public Program() : this(int.MinValue) { }
		public Program(int programId) : this(programId, null) { }
		public Program(int programId, string programName) : this(programId, programName, DateTime.MinValue) { }
		public Program(int programId, string programName, DateTime createDate) 
		{
			this.programId = programId;
			this.programName = programName;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		private string IdentXML(string xml) 
		{
			string newXML = "";
			string[] lines = xml.Split('\r');
			foreach(string strXml in lines) 
			{
				if(strXml.Trim() == "")
					break;
				newXML += "\t" + strXml.Replace("\n", "") + "\r\n";
			}
			return newXML;
		}

		public virtual string GenerateXML() 
		{
			return "<Program>\r\n" +
				"	<ProgramId>" + programId + "</ProgramId>\r\n" +
				"	<ProgramName>" + System.Web.HttpUtility.HtmlEncode(programName) + "</ProgramName>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</Program>\r\n";
		}
		#endregion

		#region Set Xml Values
		private void SetXmlValue(ref int obj, string val) 
		{
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
		}

		private void SetXmlValue(ref string obj, string val) 
		{
			if(val == "") { obj = null; return; }
			obj = System.Web.HttpUtility.HtmlDecode(val);
		}
		
		private void SetXmlValue(ref bool obj, string val) 
		{
			if(val == "") { obj = false; return; }
			obj = (val.ToLower() == "true");
		}

		private void SetXmlValue(ref Decimal obj, string val) 
		{
			if(val == "") { obj = Decimal.MinValue; return; }
			obj = Decimal.Parse(val);
		}

		private void SetXmlValue(ref DateTime obj, string val) 
		{
			if(val == "") { obj = DateTime.MinValue; return; }
			obj = DateTime.Parse(val);
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) 
		{
			foreach(XmlNode node in childNodes) 
			{
				if(node.Name.ToLower() == "programId") 
				{
					SetXmlValue(ref programId, node.InnerText);
				}
				if(node.Name.ToLower() == "programName") 
				{
					SetXmlValue(ref programName, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") 
				{
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		// load from an xml string 
		public virtual void LoadXml(string xml) 
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
			}
		}

		// load from an xml document object
		public virtual void Load(System.Xml.XmlDocument doc) 
		{
			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
			}
		}

		// load from a stream
		public virtual void Load(System.IO.Stream inStream) 
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(inStream);

			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
			}
		}

		// load from a text reader
		public virtual void Load(System.IO.TextReader txtReader) 
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(txtReader);

			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
			}
		}

		// load from an xml reader
		public virtual void Load(System.Xml.XmlReader reader) 
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);

			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
			}
		}

		// load from a xml filename
		public virtual void Load(string filename) 
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
			}
		}

		#endregion

		#endregion

		#region Data Source Methods
		public static Program[] GetPrograms() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPrograms();
		}

		public static Program GetProgramByID(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetProgramByID(id);
		}

		public int Insert() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertProgram(this);
		}

		public int Update() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdateProgram(this);
		}
		#endregion

		#region Properties
		public int ProgramId 
		{
			set { programId = value; }
			get { return programId; }
		}

		public string ProgramName 
		{
			set { programName = value; }
			get { return programName; }
		}

		public DateTime CreateDate 
		{
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
