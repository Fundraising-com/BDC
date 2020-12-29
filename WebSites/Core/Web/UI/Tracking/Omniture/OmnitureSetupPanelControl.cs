/* Jean-Francois Buist - March 1, 2005
 * This component is used to design multi-language web sites.
 * This is also a form of content management way.
 * This control should always support cultures.
 * 
 */

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;

[assembly: TagPrefix("efundraising.Web.UI.Tracking.Omniture", "OmniturePanel")]

namespace GA.BDC.Core.Web.UI.Tracking.Omniture
{

	/// <summary>
	/// This class is used by the Visual Studio at Design Time.
	/// You can set control properties behavior in the ControlDesigner class.
	/// </summary>
	public class OmniturePanelDesigner : ControlDesigner {

		public OmniturePanelDesigner() {

		}

		// this function makes the IDE shows the option to setup the component, (VS is on the property
		// and on the right click menu.
		public override System.ComponentModel.Design.DesignerVerbCollection Verbs {
			get {
				// add the HTML option in properties
				DesignerVerbCollection dvc = new DesignerVerbCollection();
				dvc.Add( new DesignerVerb("Setup", new EventHandler(this.LaunchOmnitureSetupUI))); 
				return dvc;
			}
		}

		// this function is called when the designer verb is toggled
		private void LaunchOmnitureSetupUI(object sender, EventArgs e) {
			OmnitureSetupForm setupForm =
				new OmnitureSetupForm();
			setupForm.ShowDialog();

			IsDirty = true;
			base.UpdateDesignTimeHtml();
		}
		
		// catch the event when a component change from the properties
		public override void OnComponentChanged(object obj, ComponentChangedEventArgs ce) {
			IsDirty = true;

			base.OnComponentChanged(obj, ce);
			base.UpdateDesignTimeHtml();
		}

		// return the html display we want the UI control to see at design time
		public override string GetDesignTimeHtml() {
			return "<b>Omniture Tracking</b>";
		}
	}

	/// <summary>
	/// Summary description for PagePanelControl.
	/// </summary>
	[DesignerAttribute(typeof(OmniturePanelDesigner), typeof(IDesigner))]
	[DefaultEvent("Click")]
	public class OmnitureSetupPanelControl : System.Web.UI.WebControls.WebControl, INamingContainer {
		protected Literal omnitureContentLiteral = new Literal();

		public OmnitureSetupPanelControl() {
			
		}

		private void OmnitureSetupPanelControl_Load(object sender, EventArgs e) {
			omnitureContentLiteral.Text = "Salut";
		}

		#region Web Form Designer generated code
		protected override void OnInit(EventArgs e) {
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		#region Init
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() { 
			Controls.Add(omnitureContentLiteral);
			this.Load += new EventHandler(OmnitureSetupPanelControl_Load);
		}

		#endregion

		#endregion

		#region Attributes

		#endregion

	}
}
