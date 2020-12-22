using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class TempDmUsaHockeyInline1: EFundraisingCRMDataObject {

		private int id;
		private string compagnie;
		private string contact;
		private string address1;
		private string address2;
		private string city;
		private string state;
		private string zip;
		private string phone;
		private string ext;


		public TempDmUsaHockeyInline1() : this(int.MinValue) { }
		public TempDmUsaHockeyInline1(int id) : this(id, null) { }
		public TempDmUsaHockeyInline1(int id, string compagnie) : this(id, compagnie, null) { }
		public TempDmUsaHockeyInline1(int id, string compagnie, string contact) : this(id, compagnie, contact, null) { }
		public TempDmUsaHockeyInline1(int id, string compagnie, string contact, string address1) : this(id, compagnie, contact, address1, null) { }
		public TempDmUsaHockeyInline1(int id, string compagnie, string contact, string address1, string address2) : this(id, compagnie, contact, address1, address2, null) { }
		public TempDmUsaHockeyInline1(int id, string compagnie, string contact, string address1, string address2, string city) : this(id, compagnie, contact, address1, address2, city, null) { }
		public TempDmUsaHockeyInline1(int id, string compagnie, string contact, string address1, string address2, string city, string state) : this(id, compagnie, contact, address1, address2, city, state, null) { }
		public TempDmUsaHockeyInline1(int id, string compagnie, string contact, string address1, string address2, string city, string state, string zip) : this(id, compagnie, contact, address1, address2, city, state, zip, null) { }
		public TempDmUsaHockeyInline1(int id, string compagnie, string contact, string address1, string address2, string city, string state, string zip, string phone) : this(id, compagnie, contact, address1, address2, city, state, zip, phone, null) { }
		public TempDmUsaHockeyInline1(int id, string compagnie, string contact, string address1, string address2, string city, string state, string zip, string phone, string ext) {
			this.id = id;
			this.compagnie = compagnie;
			this.contact = contact;
			this.address1 = address1;
			this.address2 = address2;
			this.city = city;
			this.state = state;
			this.zip = zip;
			this.phone = phone;
			this.ext = ext;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TempDmUsaHockeyInline1>\r\n" +
			"	<Id>" + id + "</Id>\r\n" +
			"	<Compagnie>" + System.Web.HttpUtility.HtmlEncode(compagnie) + "</Compagnie>\r\n" +
			"	<Contact>" + System.Web.HttpUtility.HtmlEncode(contact) + "</Contact>\r\n" +
			"	<Address1>" + System.Web.HttpUtility.HtmlEncode(address1) + "</Address1>\r\n" +
			"	<Address2>" + System.Web.HttpUtility.HtmlEncode(address2) + "</Address2>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<State>" + System.Web.HttpUtility.HtmlEncode(state) + "</State>\r\n" +
			"	<Zip>" + System.Web.HttpUtility.HtmlEncode(zip) + "</Zip>\r\n" +
			"	<Phone>" + System.Web.HttpUtility.HtmlEncode(phone) + "</Phone>\r\n" +
			"	<Ext>" + System.Web.HttpUtility.HtmlEncode(ext) + "</Ext>\r\n" +
			"</TempDmUsaHockeyInline1>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("id")) {
					SetXmlValue(ref id, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("compagnie")) {
					SetXmlValue(ref compagnie, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("contact")) {
					SetXmlValue(ref contact, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("address1")) {
					SetXmlValue(ref address1, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("address2")) {
					SetXmlValue(ref address2, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("city")) {
					SetXmlValue(ref city, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("state")) {
					SetXmlValue(ref state, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zip")) {
					SetXmlValue(ref zip, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phone")) {
					SetXmlValue(ref phone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ext")) {
					SetXmlValue(ref ext, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static TempDmUsaHockeyInline1[] GetTempDmUsaHockeyInline1s() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTempDmUsaHockeyInline1s();
		}

		public static TempDmUsaHockeyInline1 GetTempDmUsaHockeyInline1ByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTempDmUsaHockeyInline1ByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTempDmUsaHockeyInline1(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTempDmUsaHockeyInline1(this);
		}
		#endregion

		#region Properties
		public int Id {
			set { id = value; }
			get { return id; }
		}

		public string Compagnie {
			set { compagnie = value; }
			get { return compagnie; }
		}

		public string Contact {
			set { contact = value; }
			get { return contact; }
		}

		public string Address1 {
			set { address1 = value; }
			get { return address1; }
		}

		public string Address2 {
			set { address2 = value; }
			get { return address2; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string State {
			set { state = value; }
			get { return state; }
		}

		public string Zip {
			set { zip = value; }
			get { return zip; }
		}

		public string Phone {
			set { phone = value; }
			get { return phone; }
		}

		public string Ext {
			set { ext = value; }
			get { return ext; }
		}

		#endregion
	}
}
