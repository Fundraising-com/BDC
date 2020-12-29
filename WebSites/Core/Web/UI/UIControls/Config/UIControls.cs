using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace GA.BDC.Core.Web.UI.UIControls.Config {
	/// <summary>
	/// Summary description for UIControls.
	/// </summary>
	[Serializable, XmlInclude(typeof(UIControl))]
	public class UIControls {
		private ArrayList controlList;

		public UIControls() {
			controlList = new ArrayList();
		}
		
		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "UIControl".ToLower()) {
					UIControl control = new UIControl();
					control.Load(child);
					controlList.Add(control);
				}
			}
		}
		
		public ArrayList ControlList {
			set { controlList = value; }
			get { return controlList; }
		}

	}
}
