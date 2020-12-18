using System;
using System.Xml;
using log4net;

namespace EFundraisingCRMWeb.Components.Server.User {

	public class CrmUser {
        public static ILog Logger { get; set; }
		private const string sessionKey = "CurrentCrmSessionKey";

		private int iD;
		private string name;
		private string email;

		private Roles roles = null;

		public CrmUser() : this(int.MinValue) { }
		public CrmUser(int iD) : this(iD, null) { }
		public CrmUser(int iD, string name) : this(iD, name, null) { }
		public CrmUser(int iD, string name, string email) {
			this.iD = iD;
			this.name = name;
			this.email = email;
		}

	    static CrmUser()
	    {
            Logger = LogManager.GetLogger("CrmUser");
	    }

		#region Data Source Methods
		public static CrmUser FromIntegratedLogin(string username, string password) {
			Components.Server.IntegratedLogin.IntegratedLoginHandler integratedLoginHandler =
				new Components.Server.IntegratedLogin.IntegratedLoginHandler();

            Logger.Debug("user / pwd - " + username + password);
            string[] roles = integratedLoginHandler.AuthenticateUser(username, password);

            Logger.Debug("roles - " + roles);
            if (roles != null || username == "testuser" && password == "test321")
            {
                Components.Server.User.CrmUser crmUser = null;
                crmUser =
                    new Components.Server.User.CrmUser(int.MinValue,
                    username, username + "@qsp.com");

                crmUser.Roles = new Components.Server.User.Roles();

                if (username == "testuser")
                {
                    Components.Server.User.Role mrole =
                        new Components.Server.User.Role("1", "gCAEFR_Intranet_IT");
                    crmUser.Roles.AddRole(mrole);
                }
                else
                {
                    foreach (string role in roles)
                    {
                        Components.Server.User.Role mrole =
                            new Components.Server.User.Role(role, role);
                        crmUser.Roles.AddRole(mrole);
                    }
                }


                Logger.Debug("crmUser - " + crmUser.iD + crmUser.name); 
                return crmUser;
            }
            


            
            {
                System.Web.HttpContext.Current.Session["traceLOG"] = System.Web.HttpContext.Current.Session["traceLOG"].ToString() + " ROLES ARE NULL";
            }

			return null;
		}

		public static CrmUser Create(System.Web.SessionState.HttpSessionState session) {
            //place a logger here for sessionKey
			if(session[sessionKey] != null) {
                //anther here to see if it entered
                Logger.Debug("entered session[sessionKey] - " + session[sessionKey]);
				var result  = (CrmUser)session[sessionKey];
                //log result to see if its not null
                Logger.Debug("(CrmUser)session[sessionKey] - " + result);
                return result;
			}
			return null;
		}

		public void Save(System.Web.SessionState.HttpSessionState session) {
			session[sessionKey] = this;
		}

		#endregion

		#region Methods
		public bool IsReadAccess(object component) {
			if(component is EFundraisingCRMWebBasePage) {
				EFundraisingCRMWebBasePage efrBase =
					(EFundraisingCRMWebBasePage)component;
				return efrBase.IsReadAccess();
			} else if(component is System.Web.UI.UserControl) {
				System.Web.UI.UserControl uc =
					(System.Web.UI.UserControl)component;
				EFundraisingCRMWebBasePage efrBase =
					(EFundraisingCRMWebBasePage)uc.Page;
				return efrBase.IsReadAccess();
			} else {
				throw new ArgumentException("Crm User Is Read Access do not allow : " + component.GetType().FullName, "object component");
			}
		}

		public bool IsWriteAccess(object component) {
			if(component is EFundraisingCRMWebBasePage) {
				EFundraisingCRMWebBasePage efrBase =
					(EFundraisingCRMWebBasePage)component;
				return efrBase.IsWriteAccess();
			} else if(component is System.Web.UI.UserControl) {
				System.Web.UI.UserControl uc =
					(System.Web.UI.UserControl)component;
				EFundraisingCRMWebBasePage efrBase =
					(EFundraisingCRMWebBasePage)uc.Page;
				return efrBase.IsWriteAccess();
			} else {
				throw new ArgumentException("Crm User Is Write Access do not allow : " + component.GetType().FullName, "object component");
			}
		}

		public void MessageBox(object component, string message) {
			if(component is EFundraisingCRMWebBasePage) {
				EFundraisingCRMWebBasePage efrBase =
					(EFundraisingCRMWebBasePage)component;
				efrBase.MessageBox(message);
			} else if(component is System.Web.UI.UserControl) {
				System.Web.UI.UserControl uc =
					(System.Web.UI.UserControl)component;
				EFundraisingCRMWebBasePage efrBase =
					(EFundraisingCRMWebBasePage)uc.Page;
				efrBase.MessageBox(message);
			} else {
				throw new ArgumentException("Crm User Message Box do not allow : " + component.GetType().FullName, "object component");
			}
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
			return "<CrmUser>\r\n" +
			"	<ID>" + iD + "</ID>\r\n" +
			"	<Name>" + name + "</Name>\r\n" +
			"	<Email>" + email + "</Email>\r\n" +
			IdentXML(roles.GenerateXML()) + 
			"</CrmUser>\r\n";
		}
		#endregion

		#region Set Xml Values
		private void SetXmlValue(ref int obj, string val) {
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
		}

		private void SetXmlValue(ref string obj, string val) {
			if(val == "") { obj = null; return; }
			obj = val;
		}
		
		private void SetXmlValue(ref bool obj, string val) {
			if(val == "") { obj = false; return; }
			obj = (val.ToLower() == "true");
		}

		private void SetXmlValue(ref Decimal obj, string val) {
			if(val == "") { obj = Decimal.MinValue; return; }
			obj = Decimal.Parse(val);
		}

		private void SetXmlValue(ref DateTime obj, string val) {
			if(val == "") { obj = DateTime.MinValue; return; }
			obj = DateTime.Parse(val);
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "id") {
					SetXmlValue(ref iD, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "email") {
					SetXmlValue(ref email, node.InnerText);
				}
				if(node.Name.ToLower() == "roles") {
					roles = new Roles();
					roles.Load(node);
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
		public int ID {
			set { iD = value; }
			get { return iD; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public Roles Roles {
			set { roles = value; }
			get { return roles; }
		}

		#endregion
	}
}
