using System;
using System.Xml;
using System.Collections;

namespace EFundraisingCRMWeb.Components.Server.User {

	public class Roles {

		private ArrayList roles;

		public Roles() {
			roles = new ArrayList();
		}

		#region Object Manipulation

		// add a new Role
		public void AddRole(Role role) {
			roles.Add(role);
		}

		// remove a Role
		public void RemoveRole(Role role) {
			roles.Remove(role);
		}

		// check if this specified Role exists
		public bool Exists(Role role) {
			return roles.Contains(role);
		}

		// clear the list of Roles
		public void Clear() {
			roles.Clear();
		}

		// get all Roles
		public Role[] GetAllRoles() {
			Role[] role = new Role[roles.Count];
			for(int i=0;i<roles.Count;i++) {
				role[i] = (Role)roles[i];
			}
			return role;
		}

		// get one Role by ID
		public Role GetRoleByID(string id) {
			for(int i=0;i<roles.Count;i++) {
				Role role = (Role)roles[i];
				if(role.ID == id) {
					return role;
				}
			}
			return null;
		}

		// get all Role by ID
		public Role[] GetRolesByID(string id) {
			ArrayList list = new ArrayList();
			for(int i=0;i<roles.Count;i++) {
				Role role = (Role)roles[i];
				if(role.ID == id) {
					list.Add(role);
				}
			}
			if(list.Count < 1) return null;
			Role[] _roles = new Role[list.Count];
			for(int i=0;i<list.Count;i++) {
				_roles[i] = (Role)list[i];
			}
			return _roles;
		}

		// get one Role by Name
		public Role GetRoleByName(string name) {
			for(int i=0;i<roles.Count;i++) {
				Role role = (Role)roles[i];
				if(role.Name == name) {
					return role;
				}
			}
			return null;
		}

		// get all Role by Name
		public Role[] GetRolesByName(string name) {
			ArrayList list = new ArrayList();
			for(int i=0;i<roles.Count;i++) {
				Role role = (Role)roles[i];
				if(role.Name == name) {
					list.Add(role);
				}
			}
			if(list.Count < 1) return null;
			Role[] _roles = new Role[list.Count];
			for(int i=0;i<list.Count;i++) {
				_roles[i] = (Role)list[i];
			}
			return _roles;
		}

		#endregion

		#region XML Methods

		#region Save XML
		private string IdentXML(string xml) {
			string newXML = "";
			string[] lines = xml.Split('\r');
			foreach(string strXml in lines) {
				if(strXml.Trim() == "")
					break;
				newXML += "\t" + strXml.Replace("\n", "") + "\r\n";
			}
			return newXML;
		}

		public virtual string GenerateXML() {
			string rstr = "<Roles>\r\n";
			foreach(Role role in roles) {
				rstr += IdentXML(role.GenerateXML());
			}
			rstr += "</Roles>\r\n";
			return rstr;
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "role") {
					Role role = new Role();
					role.Load(node);
					roles.Add(role);
				}
			}
		}
		// load from an xml string 
		public virtual void LoadXml(string xml) {
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from an xml document object
		public virtual void Load(System.Xml.XmlDocument doc) {
			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a stream
		public virtual void Load(System.IO.Stream inStream) {
			XmlDocument doc = new XmlDocument();
			doc.Load(inStream);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a text reader
		public virtual void Load(System.IO.TextReader txtReader) {
			XmlDocument doc = new XmlDocument();
			doc.Load(txtReader);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from an xml reader
		public virtual void Load(System.Xml.XmlReader reader) {
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a xml filename
		public virtual void Load(string filename) {
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		#endregion

		#endregion

		#region Properties
		public ArrayList Role {
			set { roles = value; }
			get { return roles; }
		}
		#endregion

		#region Static

		public static Roles CreateRoles(string roles) // roles in format gCAEFR_Intranet_Reporting|gCAEFR_Intranet_IT
		{
			if (roles == null || roles.Trim() == string.Empty)
				return null;
			string[] theRoles = roles.Split('|');
			Roles myRoles = null;
			if (theRoles.Length > 0)
			{
				myRoles = new Roles();
				for (int i=0; i< theRoles.Length; i++)
				{
					if (theRoles[i].Trim() != string.Empty)
					{
						Role r = new Role(i.ToString(), theRoles[i]);
						myRoles.AddRole(r);
					}
				}
			}
			return myRoles;
		}
		#endregion
	}
}
