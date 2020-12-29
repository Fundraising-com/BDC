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
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;

[assembly: TagPrefix("GA.BDC.Core.Web.UI.UIControls", "ContentPanel")]
namespace GA.BDC.Core.Web.UI.UIControls {

	/// <summary>
	/// This class is used by the Visual Studio at Design Time.
	/// You can set control properties behavior in the ControlDesigner class.
	/// </summary>
	public class ContentPanelDesigner : ControlDesigner {
		private string aspxFileName;
		private ControlBuilder builder = null;

		public ContentPanelDesigner() {
			aspxFileName = "";
		}

		public override void OnSetParent() {
			// this places the text when the control is attached to it's component at
			// design time.
			ContentPanelControl tpc = (ContentPanelControl) Component;	// get the component we draw
			aspxFileName = tpc.ASPXFileName;	// set the aspx filename

			base.OnSetParent ();
		}

		// this function makes the IDE shows the option to setup the component, (VS is on the property
		// and on the right click menu.
		public override System.ComponentModel.Design.DesignerVerbCollection Verbs {
			get {
				// add the HTML option in properties
				DesignerVerbCollection dvc = new DesignerVerbCollection();          
				dvc.Add( new DesignerVerb("Setup", new EventHandler(this.LaunchTextBuilder))); 
				return dvc;
			}
		}

		// this function is called when the designer verb is toggled
		private void LaunchTextBuilder(object sender, EventArgs e) {
			// open the html builder form
			builder = new ControlBuilder();

			ContentPanelControl tpc = (ContentPanelControl)Component;	// get the component we draw

			if(!PanelControl.HasWebForm(tpc.Page)) {
				UserControl uc = (UserControl)tpc.Parent;
				for(int i=0;i<uc.Controls.Count;i++) {
					if(uc.Controls[i] is WebControl) {
						WebControl wc = (WebControl)uc.Controls[i];
						if(wc is PagePanelControl) {
							PagePanelControl ppc = (PagePanelControl)wc;
							this.aspxFileName = ppc.ASPXFileName;
							break;
						}
					}
				}
			} else {
				for(int i=0;i<tpc.Page.Controls.Count;i++) {
					if(tpc.Page.Controls[i] is WebControl) {
						WebControl c = (WebControl)tpc.Page.Controls[i];
						if(c is PagePanelControl) {
							PagePanelControl ppc = (PagePanelControl)c;
							this.aspxFileName = ppc.ASPXFileName;
							break;
						}
					}
				}
			}

			if(this.aspxFileName == "") {
				System.Windows.Forms.MessageBox.Show("You must have a Page Configuration Control in order to add UI Controls!", "Error Creating Control", 
					System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
				return;
			}

			builder.SetControlName(tpc.ID);
			builder.SetComponentType(COMPONENT_TYPE.UI_CONTENT);
			builder.ASPXFilename = aspxFileName;
			builder.ShowDialog();
			IsDirty = true;					// tell the IDE know the value has changed

			aspxFileName = builder.ASPXFilename;
			tpc.ASPXFileName = aspxFileName;	// set the filename to the design-time component

			// this refreshes the component at design-time
			// the VS IDE doesn't understand by itself if there is a change
			PropertyDescriptor properties;
			properties = TypeDescriptor.GetProperties(typeof(ContentPanelControl))["ASPXFileName"]; 
			this.RaiseComponentChanging(properties);
			this.RaiseComponentChanged(properties, "", aspxFileName);

			// update the UI
			base.UpdateDesignTimeHtml();
		}
		
		// catch the event when a component change from the properties
		public override void OnComponentChanged(object obj, ComponentChangedEventArgs ce) {
			System.Web.UI.Design.TextControlDesigner textControlDesigner =
				new System.Web.UI.Design.TextControlDesigner();
			
			if(ce.Member.Name.ToString() == "ID") {
				// this means, the user has changed the id of the button
				string oldName = ce.OldValue.ToString();
				string newName = ce.NewValue.ToString();

				UIControls.Config.UIController uiController = 
					new UIControls.Config.UIController(true);
				// uiController.Load(aspxFileName + ".xml");
				uiController = (UIControls.Config.UIController)GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromXmlFile(aspxFileName + ".xml", typeof(UIControls.Config.UIController));

				foreach(UIControls.Config.UIControl c in uiController.UiControls.ControlList) {
					if(c.Name == oldName) {
						c.ID = newName;
						c.Name = newName;
					}
				}

				try {
					try {
						System.IO.File.Delete(aspxFileName + ".xml");
					} catch(System.Exception ex) {
						System.Windows.Forms.MessageBox.Show("Unable to delete file: " + ex.Message);
						return;
					}


					uiController.Save(aspxFileName + ".xml", aspxFileName);
				} catch(System.Exception ex) {
					System.Windows.Forms.MessageBox.Show("Unable to save config file: " + ex.Message);
				}
			}

			IsDirty = true;

			base.OnComponentChanged(obj, ce);
			base.UpdateDesignTimeHtml();
		}

		// return the html display we want the UI control to see at design time
		public override string GetDesignTimeHtml() {
			if(aspxFileName != "") {
				// get the component we draw
				ContentPanelControl tpc = (ContentPanelControl)Component;	

				UIControls.Config.UIController uiController = 
					new UIControls.Config.UIController(true);
				uiController = (UIControls.Config.UIController)
					GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromXmlFile(aspxFileName + ".xml",
					typeof(UIControls.Config.UIController));
				// uiController.Load(aspxFileName + ".xml");
				uiController.ReadDataFromDataSource(aspxFileName);

				foreach(UIControls.Config.UIControl c in uiController.UiControls.ControlList) {
					if(c.ID == tpc.ID) {
						// we got the right component from the config file
						foreach(UIControls.Config.PartnerID p in c.PartnersID.PartnerIdList) {
							if(p.ID == BaseConfig.GlobalizerConfigs.GetDesignDefaultPartnerID()) {
								foreach(UIControls.Config.Culture cc in p.Cultures.CultureList) {
									if(cc.ID == BaseConfig.GlobalizerConfigs.GetDesignDefaultCulture()) {
										return cc.Data.Parameters.Parameter[0].ToString();
									}
								}
							}
						}
					}
				}
				return "<b>ERROR ON CONTROL!</b>";
			} else {
				return "<b>Content Panel</b>";
			}
		}
	}

	/// <summary>
	/// Summary description for PagePanelControl.
	/// </summary>
	[DesignerAttribute(typeof(ContentPanelDesigner), typeof(IDesigner))]
	public class ContentPanelControl : PanelControl {
		private System.Web.UI.WebControls.Literal panel = new System.Web.UI.WebControls.Literal();
		private System.Web.UI.WebControls.LinkButton editLinkButton = new System.Web.UI.WebControls.LinkButton();
		private System.Web.UI.WebControls.LinkButton saveLinkButton = new System.Web.UI.WebControls.LinkButton();
		private System.Web.UI.WebControls.TextBox editTextBox = new System.Web.UI.WebControls.TextBox();
		private string aspxFileName = "";

		// sets the text
		public ContentPanelControl() {
			panel.Text = "<b>Content Panel</b>";
			Controls.Add(panel);

			saveLinkButton.ID = "SaveLinkButton";
			saveLinkButton.Text = "[Save]";
			saveLinkButton.ToolTip = ID;
			saveLinkButton.Click += new EventHandler(saveLinkButton_Click);
			saveLinkButton.Visible = false;

			editLinkButton.ID = "EditLinkButton";
			editLinkButton.Text = "[Edit]";
			editLinkButton.ToolTip = ID;
			editLinkButton.Click += new EventHandler(editLinkButton_Click);
			editLinkButton.Visible = false;

			editTextBox.ID = "EditTextBox";
			editTextBox.Columns = 30;
			editTextBox.Rows = 5;
			editTextBox.TextMode = TextBoxMode.MultiLine;
			editTextBox.Visible = false;

			Controls.Add(editLinkButton);
			Controls.Add(editTextBox);
			Controls.Add(saveLinkButton);
		}

		#region Web Form Designer generated code
		protected override void OnInit(EventArgs e) {
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		// this user control is loaded at run-time
		private void LoadContentPanelControl(object sender, System.EventArgs e) {
			InitPanelControl();

			if(revealYourself) {
				panel.Text = @"<table bgcolor=YellowGreen><tr><td>" +
					panel.Text + @"</td></tr></table>";
			}

			if(!Page.IsPostBack) {
				editTextBox.Text = panel.Text;
			}

			editLinkButton.Visible = editControls;

		}

		#region Init
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() { 
			this.Load += new System.EventHandler(LoadContentPanelControl);
		}

		#endregion
		#endregion

		public string Text {
			set { panel.Text = value; }
			get { return panel.Text; }
		}

		#region Public Attributes

		[Browsable(true),
		Category("Settings"),
		Description("Set or get the page configuration file.")]
		public string ASPXFileName {
			get { return aspxFileName; }
			set { aspxFileName = value; }
		}

		#endregion

		private void Set(string val, string key) {
			Page.Session[key] = val;
		}

		private string Get(string val, string key) {
			if(Page.Session[key] != null) {
				return (string)Page.Session[key];
			}
			return "";
		}

		private void editLinkButton_Click(object sender, EventArgs e) {
			panel.Visible = false;
			editLinkButton.Visible = false;
			saveLinkButton.Visible = true;
			editTextBox.Visible = true;
		}

		private void saveLinkButton_Click(object sender, EventArgs e) {
			string newData = System.Web.HttpUtility.HtmlEncode(editTextBox.Text);

			string filename = aspxFileName.ToLower().Replace(GetCurrentBaseProjectPath(), GetCurrentProductionBaseProjectPath());

			Config.UIController uiControls = (Config.UIController) GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromXmlFile(
				filename + ".xml", typeof(Config.UIController));

			// set ui controls
			foreach(UIControls.Config.UIControl uiControl in uiControls.UiControls.ControlList) {
				try {
					string type = uiControl.Type;
					string name = uiControl.Name;

					if(name == ID) {
						for(int i=0;i<uiControl.PartnersID.PartnerIdList.Count;i++) {
							UIControls.Config.PartnerID partner = (UIControls.Config.PartnerID)uiControl.PartnersID.PartnerIdList[i];
							for(int j=0;j<partner.Cultures.CultureList.Count;i++) {
								UIControls.Config.Culture culture = (UIControls.Config.Culture)partner.Cultures.CultureList[j];
								if(culture.Data.Parameters.Parameter[0].ToString() != newData) {
									culture.Data.Parameters.Parameter[0] = newData;
									break;
								} else {
									// no changes noticable
									return;
								}
							}
							break;
						}
					}

				} catch(System.Exception ex) {
					throw new Exception("Unable to save object " + uiControl.ID, ex);
				}
			}

			System.IO.File.Delete(filename + ".xml");
			uiControls.Save(filename + ".xml", filename);

			string email = "File has changed: " + aspxFileName + "\r\n";
			email += "Page: http://" + Page.Request.Url.Host + Page.Request.FilePath + ".xml";
			GA.BDC.Core.Diagnostics.Logger.LogInfo(email, null);

			panel.Visible = true;
			editLinkButton.Visible = true;
			saveLinkButton.Visible = false;
			editTextBox.Visible = false;
			panel.Text = System.Web.HttpUtility.HtmlDecode(newData);
		}

		// get current production base project
		public string GetCurrentProductionBaseProjectPath() {
			return GetCurrentProductionBaseProjectPath(Page.Session);
		}

		public string GetCurrentProductionBaseProjectPath(System.Web.SessionState.HttpSessionState session) {
			if(session[GlobalizerBasePage.CurrentProductionBaseProjectPath] != null) {
				return session[GlobalizerBasePage.CurrentProductionBaseProjectPath].ToString().ToLower();
			}
			return "";
		}

		// get current base project path
		public string GetCurrentBaseProjectPath() {
			return GetCurrentBaseProjectPath(Page.Session);
		}

		public string GetCurrentBaseProjectPath(System.Web.SessionState.HttpSessionState session) {
			if(session[GlobalizerBasePage.CurrentBaseProjectPath] != null) {
				return session[GlobalizerBasePage.CurrentBaseProjectPath].ToString().ToLower();
			}
			return "";
		}
	}
}
