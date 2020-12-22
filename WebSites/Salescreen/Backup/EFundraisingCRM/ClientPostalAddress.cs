using System;
using System.Xml;
using System.Text;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for ClientPostalAddress.
	/// </summary>
	public class ClientPostalAddress: Core.BusinessBase
	{
		private string clientSequenceCode;
		private int clientId;
		private int postalAddressId;
		private DateTime createDate;
		private bool active;
		private string clientAddressType;

		public string ClientSequenceCode
		{
			get {return clientSequenceCode;}
			set { clientSequenceCode = value;}
		}

		public string ClientAddressType 
		{
			get { return clientAddressType;}
			set { clientAddressType =value ;}
		}

		public ClientPostalAddress() : this(int.MinValue) { }
		public ClientPostalAddress(int ClientId) : this(ClientId, int.MinValue) { }
		public ClientPostalAddress(int ClientId, int postalAddressId) : this(ClientId, postalAddressId, DateTime.MinValue) { }
		public ClientPostalAddress(int ClientId, int postalAddressId, DateTime createDate): this(ClientId, postalAddressId,  createDate,  true) 
		{
		}
		public ClientPostalAddress(int ClientId, int postalAddressId, DateTime createDate, bool bActive) : this(ClientId, postalAddressId,  createDate, string.Empty, "st") 
		{
		}
		public ClientPostalAddress(int ClientId, int postalAddressId, DateTime createDate, string ClSeqCode, string ClAddressType):this(ClientId, postalAddressId,  createDate,ClSeqCode, ClAddressType,  true) 
		{
		}

		
		public ClientPostalAddress(int ClientId, int postalAddressId, DateTime createDate, string ClSeqCode, string ClAddressType, bool bActive) 
		{
			this.clientId = ClientId;
			this.postalAddressId = postalAddressId;
			this.createDate = createDate;
			this.Active = bActive;
			this.clientSequenceCode = ClSeqCode;
			this.clientAddressType = ClAddressType;
		}

		
		public bool IsDifferent(ClientPostalAddress theClientAddress)
		{	
			bool result = (this.GenerateXML() != theClientAddress.GenerateXML());
			return result;
		}


		#region XML Methods

		#region Save XML

		public virtual string GenerateXML() 
		{
			StringBuilder strBuilder = new StringBuilder();
			strBuilder.AppendFormat("<{0}>\r\n", this.GetType().Name);
			System.Reflection.PropertyInfo[] pInfo = this.GetType().GetProperties();
			for (int i=0; i< pInfo.Length; i++)
			{
				strBuilder.AppendFormat("<{0}>{1}</{0}>\r\n", pInfo[i].Name, pInfo[i].GetValue(this, new object[] {}).ToString());
			}
			strBuilder.AppendFormat("</{0}>\r\n", this.GetType().Name);
			return strBuilder.ToString();

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
			
			System.Reflection.PropertyInfo[] pInfo = this.GetType().GetProperties();
			foreach(XmlNode node in childNodes) 
			{
				for (int i = 0; i< pInfo.Length; i++)
				{
					if (string.Compare(node.Name, pInfo[i].Name, true) == 0)
					{						
						switch (pInfo[i].PropertyType.ToString())
						{
							case "System.Boolean":
								pInfo[i].SetValue(this, System.Boolean.Parse(node.InnerText), null);
								break;
							case "System.Int32":
								pInfo[i].SetValue(this, System.Int32.Parse(node.InnerText), null);
								break;
							case "System.Double":
								pInfo[i].SetValue(this, System.Double.Parse(node.InnerText), null);
								break;
							case "System.DateTime":
								pInfo[i].SetValue(this, System.DateTime.Parse(node.InnerText), null);
								break;
							case "System.Decimal":
								pInfo[i].SetValue(this, System.Double.Parse(node.InnerText), null);
								break;
							case "System.Single":
								pInfo[i].SetValue(this, System.Decimal.Parse(node.InnerText), null);
								break;
							case "System.Byte":
								pInfo[i].SetValue(this, System.Byte.Parse(node.InnerText), null);
								break;
							case "System.UInt64":
								pInfo[i].SetValue(this, System.UInt64.Parse(node.InnerText), null);
								break;
							case "System.UInt32":
								pInfo[i].SetValue(this, System.UInt32.Parse(node.InnerText), null);
								break;
							case "System.UInt16":
								pInfo[i].SetValue(this, System.UInt16.Parse(node.InnerText), null);
								break;
							case "System.Int16":
								pInfo[i].SetValue(this, System.Int16.Parse(node.InnerText), null);
								break;
							case "System.Int64":
								pInfo[i].SetValue(this, System.Int64.Parse(node.InnerText), null);
								break;
							case "System.Char":
								pInfo[i].SetValue(this, System.Char.Parse(node.InnerText), null);
								break;
							case "System.String":
								pInfo[i].SetValue(this, node.InnerText, null);
								break;

						}
						break;
					}
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
		public static ClientPostalAddress[] GetClientPostalAddressByClientId(int clientId) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientPostalAddressByClientId(clientId);
		}
		public static ClientPostalAddress GetClientActivePostalAddressByClientId(int clientId) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetActiveClientPostalAddressByClientId(clientId);
		}


		public int Insert() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertClientPostalAddress(this);
		}

		public int Update() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			ClientPostalAddress theActiveClientPostalAddress = dbo.GetActiveClientPostalAddressByClientId(this.ClientId);
			if (theActiveClientPostalAddress != null && this.IsDifferent(theActiveClientPostalAddress))
				return dbo.InsertClientPostalAddress(this);
			else
				return -1;
		}

		#endregion

		#region Properties
		public int ClientId
		{
			set { clientId = value; }
			get { return clientId; }
		}

		public int PostalAddressId 
		{
			set { postalAddressId = value; }
			get { return postalAddressId; }
		}

		public DateTime CreateDate 
		{
			set { createDate = value; }
			get { return createDate; }
		}

		public bool Active
		{
			set { active = value;}
			get { return active;}
		}

		#endregion
	}

}
