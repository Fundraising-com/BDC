using System;
using System.Xml;
using System.Collections;

namespace EFundraisingCRMWeb.Components.Server.Menu {
	public class Roles {

		private ArrayList _roles;

		public Roles() {
			_roles = new ArrayList();
		}

		#region Method
		public bool IsAllowed(Components.Server.User.CrmUser user) {
//			foreach(Components.Server.Menu.Role role in _roles) {
//				foreach(Components.Server.User.Role userRole in user.Roles.Role) {
//					if(role.ID.ToLower() == userRole.ID.ToLower()) {
//						return true;
//					}
//				}
//			}
//			return false;
			return true;
		}

		public bool IsRead(Components.Server.User.CrmUser user) {
			foreach(Components.Server.Menu.Role role in _roles) {
				foreach(Components.Server.User.Role userRole in user.Roles.Role) {
					if(role.ID.ToLower() == userRole.ID.ToLower() && role.Read.ToLower() == "true") {
						return true;
					}
				}
			}
			return false;
		}

		public bool IsWrite(Components.Server.User.CrmUser user) {
			foreach(Components.Server.Menu.Role role in _roles) {
				foreach(Components.Server.User.Role userRole in user.Roles.Role) {
					if(role.ID.ToLower() == userRole.ID.ToLower() && role.Write.ToLower() == "true") {
						return true;
					}
				}
			}
			return false;
		}

		#endregion

		#region Object Manipulation

		// add a new Role
		public void AddRole(Role role) {
			_roles.Add(role);
		}

		// remove a Role
		public void RemoveRole(Role role) {
			_roles.Remove(role);
		}

		// check if this specified Role exists
		public bool Exists(Role role) {
			return _roles.Contains(role);
		}

		// clear the list of Roles
		public void Clear() {
			_roles.Clear();
		}

		// get all Roles
		public Role[] GetAllRoles() {
			Role[] role = new Role[_roles.Count];
			for(int i=0;i<_roles.Count;i++) {
				role[i] = (Role)_roles[i];
			}
			return role;
		}

		// get one Role by ID
		public Role GetRoleByID(string id) {
			for(int i=0;i<_roles.Count;i++) {
				Role role = (Role)_roles[i];
				if(role.ID == id) {
					return role;
				}
			}
			return null;
		}

		// get all Role by ID
		public Role[] GetRolesByID(string id) {
			ArrayList list = new ArrayList();
			for(int i=0;i<_roles.Count;i++) {
				Role role = (Role)_roles[i];
				if(role.ID == id) {
					list.Add(role);
				}
			}
			if(list.Count < 1) return null;
			Role[] roles = new Role[list.Count];
			for(int i=0;i<list.Count;i++) {
				roles[i] = (Role)list[i];
			}
			return roles;
		}

		// get one Role by Read
		public Role GetRoleByRead(string read) {
			for(int i=0;i<_roles.Count;i++) {
				Role role = (Role)_roles[i];
				if(role.Read == read) {
					return role;
				}
			}
			return null;
		}

		// get all Role by Read
		public Role[] GetRolesByRead(string read) {
			ArrayList list = new ArrayList();
			for(int i=0;i<_roles.Count;i++) {
				Role role = (Role)_roles[i];
				if(role.Read == read) {
					list.Add(role);
				}
			}
			if(list.Count < 1) return null;
			Role[] roles = new Role[list.Count];
			for(int i=0;i<list.Count;i++) {
				roles[i] = (Role)list[i];
			}
			return roles;
		}

		// get one Role by Write
		public Role GetRoleByWrite(string write) {
			for(int i=0;i<_roles.Count;i++) {
				Role role = (Role)_roles[i];
				if(role.Write == write) {
					return role;
				}
			}
			return null;
		}

		// get all Role by Write
		public Role[] GetRolesByWrite(string write) {
			ArrayList list = new ArrayList();
			for(int i=0;i<_roles.Count;i++) {
				Role role = (Role)_roles[i];
				if(role.Write == write) {
					list.Add(role);
				}
			}
			if(list.Count < 1) return null;
			Role[] roles = new Role[list.Count];
			for(int i=0;i<list.Count;i++) {
				roles[i] = (Role)list[i];
			}
			return roles;
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
			foreach(Role role in _roles) {
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
					_roles.Add(role);
                    System.Diagnostics.Debug.Print(role.ID.ToString());
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
			set { _roles = value; }
			get { return _roles; }
		}
		#endregion
	}
}
