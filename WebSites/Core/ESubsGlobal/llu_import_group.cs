using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal {

	public class Llu_group_import {

		private int external_group_id;
		private string group_name;
		private string sponsor_name;
		private string address;
		private string city;
		private string state;
		private string zip;
		private string password;
		private string email;
		private string country;
		private string phone;
		private string member_count;


		public Llu_group_import() : this(int.MinValue) { }
		public Llu_group_import(int external_group_id) : this(external_group_id, null) { }
		public Llu_group_import(int external_group_id, string group_name) : this(external_group_id, group_name, null) { }
		public Llu_group_import(int external_group_id, string group_name, string sponsor_name) : this(external_group_id, group_name, sponsor_name, null) { }
		public Llu_group_import(int external_group_id, string group_name, string sponsor_name, string address) : this(external_group_id, group_name, sponsor_name, address, null) { }
		public Llu_group_import(int external_group_id, string group_name, string sponsor_name, string address, string city) : this(external_group_id, group_name, sponsor_name, address, city, null) { }
		public Llu_group_import(int external_group_id, string group_name, string sponsor_name, string address, string city, string state) : this(external_group_id, group_name, sponsor_name, address, city, state, null) { }
		public Llu_group_import(int external_group_id, string group_name, string sponsor_name, string address, string city, string state, string zip) : this(external_group_id, group_name, sponsor_name, address, city, state, zip, null) { }
		public Llu_group_import(int external_group_id, string group_name, string sponsor_name, string address, string city, string state, string zip, string password) : this(external_group_id, group_name, sponsor_name, address, city, state, zip, password, null) { }
		public Llu_group_import(int external_group_id, string group_name, string sponsor_name, string address, string city, string state, string zip, string password, string email) : this(external_group_id, group_name, sponsor_name, address, city, state, zip, password, email, null) { }
		public Llu_group_import(int external_group_id, string group_name, string sponsor_name, string address, string city, string state, string zip, string password, string email, string country) : this(external_group_id, group_name, sponsor_name, address, city, state, zip, password, email, country, null) { }
		public Llu_group_import(int external_group_id, string group_name, string sponsor_name, string address, string city, string state, string zip, string password, string email, string country, string phone) : this(external_group_id, group_name, sponsor_name, address, city, state, zip, password, email, country, phone, null) { }
		public Llu_group_import(int external_group_id, string group_name, string sponsor_name, string address, string city, string state, string zip, string password, string email, string country, string phone, string member_count) {
			this.external_group_id = external_group_id;
			this.group_name = group_name;
			this.sponsor_name = sponsor_name;
			this.address = address;
			this.city = city;
			this.state = state;
			this.zip = zip;
			this.password = password;
			this.email = email;
			this.country = country;
			this.phone = phone;
			this.member_count = member_count;
		}

		/*
		#region Data Source Methods
		public static Llu_group_import[] GetLlu_group_imports() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetLlu_group_imports();
		}
		#endregion*/

		#region Properties
		public int External_group_id {
			set { external_group_id = value; }
			get { return external_group_id; }
		}

		public string Group_name {
			set { group_name = value; }
			get { return group_name; }
		}

		public string Sponsor_name {
			set { sponsor_name = value; }
			get { return sponsor_name; }
		}

		public string Address {
			set { address = value; }
			get { return address; }
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

		public string Password {
			set { password = value; }
			get { return password; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public string Country {
			set { country = value; }
			get { return country; }
		}

		public string Phone {
			set { phone = value; }
			get { return phone; }
		}

		public string Member_count {
			set { member_count = value; }
			get { return member_count; }
		}

		#endregion
	}
}

