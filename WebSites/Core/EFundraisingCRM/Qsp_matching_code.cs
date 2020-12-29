using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class QspMatchingCode: EFundraisingCRMDataObject {

		private int id;
		private string groupName;
		private string address;
		private string zipCode;
		private string zzzzz;
		private string nnn;
		private string aa99;
		private string zzzzzaa99;
		private string zzzzznnnaa99;


		public QspMatchingCode() : this(int.MinValue) { }
		public QspMatchingCode(int id) : this(id, null) { }
		public QspMatchingCode(int id, string groupName) : this(id, groupName, null) { }
		public QspMatchingCode(int id, string groupName, string address) : this(id, groupName, address, null) { }
		public QspMatchingCode(int id, string groupName, string address, string zipCode) : this(id, groupName, address, zipCode, null) { }
		public QspMatchingCode(int id, string groupName, string address, string zipCode, string zzzzz) : this(id, groupName, address, zipCode, zzzzz, null) { }
		public QspMatchingCode(int id, string groupName, string address, string zipCode, string zzzzz, string nnn) : this(id, groupName, address, zipCode, zzzzz, nnn, null) { }
		public QspMatchingCode(int id, string groupName, string address, string zipCode, string zzzzz, string nnn, string aa99) : this(id, groupName, address, zipCode, zzzzz, nnn, aa99, null) { }
		public QspMatchingCode(int id, string groupName, string address, string zipCode, string zzzzz, string nnn, string aa99, string zzzzzaa99) : this(id, groupName, address, zipCode, zzzzz, nnn, aa99, zzzzzaa99, null) { }
		public QspMatchingCode(int id, string groupName, string address, string zipCode, string zzzzz, string nnn, string aa99, string zzzzzaa99, string zzzzznnnaa99) {
			this.id = id;
			this.groupName = groupName;
			this.address = address;
			this.zipCode = zipCode;
			this.zzzzz = zzzzz;
			this.nnn = nnn;
			this.aa99 = aa99;
			this.zzzzzaa99 = zzzzzaa99;
			this.zzzzznnnaa99 = zzzzznnnaa99;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<QspMatchingCode>\r\n" +
			"	<Id>" + id + "</Id>\r\n" +
			"	<GroupName>" + System.Web.HttpUtility.HtmlEncode(groupName) + "</GroupName>\r\n" +
			"	<Address>" + System.Web.HttpUtility.HtmlEncode(address) + "</Address>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<Zzzzz>" + System.Web.HttpUtility.HtmlEncode(zzzzz) + "</Zzzzz>\r\n" +
			"	<Nnn>" + System.Web.HttpUtility.HtmlEncode(nnn) + "</Nnn>\r\n" +
			"	<Aa99>" + System.Web.HttpUtility.HtmlEncode(aa99) + "</Aa99>\r\n" +
			"	<Zzzzzaa99>" + System.Web.HttpUtility.HtmlEncode(zzzzzaa99) + "</Zzzzzaa99>\r\n" +
			"	<Zzzzznnnaa99>" + System.Web.HttpUtility.HtmlEncode(zzzzznnnaa99) + "</Zzzzznnnaa99>\r\n" +
			"</QspMatchingCode>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("id")) {
					SetXmlValue(ref id, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("groupName")) {
					SetXmlValue(ref groupName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("address")) {
					SetXmlValue(ref address, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zipCode")) {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zzzzz")) {
					SetXmlValue(ref zzzzz, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("nnn")) {
					SetXmlValue(ref nnn, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("aa99")) {
					SetXmlValue(ref aa99, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zzzzzaa99")) {
					SetXmlValue(ref zzzzzaa99, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zzzzznnnaa99")) {
					SetXmlValue(ref zzzzznnnaa99, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static QspMatchingCode[] GetQspMatchingCodes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetQspMatchingCodes();
		}

		public static QspMatchingCode GetQspMatchingCodeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetQspMatchingCodeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertQspMatchingCode(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateQspMatchingCode(this);
		}
		#endregion

		#region Properties
		public int Id {
			set { id = value; }
			get { return id; }
		}

		public string GroupName {
			set { groupName = value; }
			get { return groupName; }
		}

		public string Address {
			set { address = value; }
			get { return address; }
		}

		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public string Zzzzz {
			set { zzzzz = value; }
			get { return zzzzz; }
		}

		public string Nnn {
			set { nnn = value; }
			get { return nnn; }
		}

		public string Aa99 {
			set { aa99 = value; }
			get { return aa99; }
		}

		public string Zzzzzaa99 {
			set { zzzzzaa99 = value; }
			get { return zzzzzaa99; }
		}

		public string Zzzzznnnaa99 {
			set { zzzzznnnaa99 = value; }
			get { return zzzzznnnaa99; }
		}

		#endregion
	}
}
