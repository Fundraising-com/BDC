using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class ControlType: eFundraisingStoreDataObject {

		private int controlTypeId;
		private string assemblyName;
		private string nameSpace;
		private string className;
		private string displayAttribute;
		private string bindingName;
		private string eventHandlerName;
		private short autoPostBack;
		private DateTime datestamp;


		public ControlType() : this(int.MinValue) { }
		public ControlType(int controlTypeId) : this(controlTypeId, null) { }
		public ControlType(int controlTypeId, string assemblyName) : this(controlTypeId, assemblyName, null) { }
		public ControlType(int controlTypeId, string assemblyName, string nameSpace) : this(controlTypeId, assemblyName, nameSpace, null) { }
		public ControlType(int controlTypeId, string assemblyName, string nameSpace, string className) : this(controlTypeId, assemblyName, nameSpace, className, null) { }
		public ControlType(int controlTypeId, string assemblyName, string nameSpace, string className, string displayAttribute) : this(controlTypeId, assemblyName, nameSpace, className, displayAttribute, null) { }
		public ControlType(int controlTypeId, string assemblyName, string nameSpace, string className, string displayAttribute, string bindingName) : this(controlTypeId, assemblyName, nameSpace, className, displayAttribute, bindingName, null) { }
		public ControlType(int controlTypeId, string assemblyName, string nameSpace, string className, string displayAttribute, string bindingName, string eventHandlerName) : this(controlTypeId, assemblyName, nameSpace, className, displayAttribute, bindingName, eventHandlerName, short.MinValue) { }
		public ControlType(int controlTypeId, string assemblyName, string nameSpace, string className, string displayAttribute, string bindingName, string eventHandlerName, short autoPostBack) : this(controlTypeId, assemblyName, nameSpace, className, displayAttribute, bindingName, eventHandlerName, autoPostBack, DateTime.MinValue) { }
		public ControlType(int controlTypeId, string assemblyName, string nameSpace, string className, string displayAttribute, string bindingName, string eventHandlerName, short autoPostBack, DateTime datestamp) {
			this.controlTypeId = controlTypeId;
			this.assemblyName = assemblyName;
			this.nameSpace = nameSpace;
			this.className = className;
			this.displayAttribute = displayAttribute;
			this.bindingName = bindingName;
			this.eventHandlerName = eventHandlerName;
			this.autoPostBack = autoPostBack;
			this.datestamp = datestamp;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ControlType>\r\n" +
			"	<ControlTypeId>" + controlTypeId + "</ControlTypeId>\r\n" +
			"	<AssemblyName>" + System.Web.HttpUtility.HtmlEncode(assemblyName) + "</AssemblyName>\r\n" +
			"	<Namespace>" + System.Web.HttpUtility.HtmlEncode(nameSpace) + "</Namespace>\r\n" +
			"	<ClassName>" + System.Web.HttpUtility.HtmlEncode(className) + "</ClassName>\r\n" +
			"	<DisplayAttribute>" + System.Web.HttpUtility.HtmlEncode(displayAttribute) + "</DisplayAttribute>\r\n" +
			"	<BindingName>" + System.Web.HttpUtility.HtmlEncode(bindingName) + "</BindingName>\r\n" +
			"	<EventHandlerName>" + System.Web.HttpUtility.HtmlEncode(eventHandlerName) + "</EventHandlerName>\r\n" +
			"	<AutoPostBack>" + autoPostBack + "</AutoPostBack>\r\n" +
			"	<Datestamp>" + datestamp + "</Datestamp>\r\n" +
			"</ControlType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "controlTypeId") {
					SetXmlValue(ref controlTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "assemblyName") {
					SetXmlValue(ref assemblyName, node.InnerText);
				}
				if(node.Name.ToLower() == "namespace") {
					SetXmlValue(ref nameSpace, node.InnerText);
				}
				if(node.Name.ToLower() == "className") {
					SetXmlValue(ref className, node.InnerText);
				}
				if(node.Name.ToLower() == "displayAttribute") {
					SetXmlValue(ref displayAttribute, node.InnerText);
				}
				if(node.Name.ToLower() == "bindingName") {
					SetXmlValue(ref bindingName, node.InnerText);
				}
				if(node.Name.ToLower() == "eventHandlerName") {
					SetXmlValue(ref eventHandlerName, node.InnerText);
				}
				if(node.Name.ToLower() == "autoPostBack") {
					SetXmlValue(ref autoPostBack, node.InnerText);
				}
				if(node.Name.ToLower() == "datestamp") {
					SetXmlValue(ref datestamp, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ControlType[] GetControlTypes() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetControlTypes();
		}

		public static ControlType GetControlTypeByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetControlTypeByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertControlType(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateControlType(this);
		}
		#endregion

		#region Properties
		public int ControlTypeId {
			set { controlTypeId = value; }
			get { return controlTypeId; }
		}

		public string AssemblyName {
			set { assemblyName = value; }
			get { return assemblyName; }
		}

		public string Namespace {
			set { nameSpace = value; }
			get { return nameSpace; }
		}

		public string ClassName {
			set { className = value; }
			get { return className; }
		}

		public string DisplayAttribute {
			set { displayAttribute = value; }
			get { return displayAttribute; }
		}

		public string BindingName {
			set { bindingName = value; }
			get { return bindingName; }
		}

		public string EventHandlerName {
			set { eventHandlerName = value; }
			get { return eventHandlerName; }
		}

		public short AutoPostBack {
			set { autoPostBack = value; }
			get { return autoPostBack; }
		}

		public DateTime Datestamp {
			set { datestamp = value; }
			get { return datestamp; }
		}

		#endregion
	}
}
