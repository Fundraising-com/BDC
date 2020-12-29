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
using GA.BDC.Core.EnterpriseComponents;

[assembly: TagPrefix("GA.BDC.Core.Web.UI.UIControls", "ButtonPanel")]
public delegate void TrackingButtonEventHandler(object sender, System.EventArgs e);

namespace GA.BDC.Core.Web.UI.UIControls {

	public enum BUTTON_TYPE {
		BUTTON,
		LINK,
		IMAGE
	}

	/// <summary>
	/// This class is used by the Visual Studio at Design Time.
	/// You can set control properties behavior in the ControlDesigner class.
	/// </summary>
	public class ButtonPanelDesigner : ControlDesigner {
		private string aspxFileName;
		private ControlBuilder builder = null;
		private BUTTON_TYPE buttonType;

		public ButtonPanelDesigner() {
			aspxFileName = "";
		}

		public override void OnSetParent() {
			// this places the text when the control is attached to it's component at
			// design time.
			ButtonPanelControl tpc = (ButtonPanelControl) Component;	// get the component we draw
			aspxFileName = tpc.ASPXFileName;	// set the aspx filename
			buttonType = tpc.ButtonType;

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
			ButtonPanelControl tpc = (ButtonPanelControl)Component;	// get the component we draw
			builder.SetControlName(tpc.ID);

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
			
			switch(buttonType) {
				case BUTTON_TYPE.BUTTON:
					builder.SetComponentType(COMPONENT_TYPE.TK_BUTTON);
					break;
				case BUTTON_TYPE.LINK:
					builder.SetComponentType(COMPONENT_TYPE.TK_LINK);
					break;
				case BUTTON_TYPE.IMAGE:
					builder.SetComponentType(COMPONENT_TYPE.TK_IMAGE_BUTTON);
					break;
			}
			builder.ASPXFilename = aspxFileName;
			builder.ShowDialog();
			IsDirty = true;					// tell the IDE know the value has changed

			aspxFileName = builder.ASPXFilename;
			tpc.ASPXFileName = aspxFileName;	// set the filename to the design-time component

			// this refreshes the component at design-time
			// the VS IDE doesn't understand by itself if there is a change
			PropertyDescriptor properties;
			properties = TypeDescriptor.GetProperties(typeof(ButtonPanelControl))["ASPXFileName"]; 
			this.RaiseComponentChanging(properties);
			this.RaiseComponentChanged(properties, "", aspxFileName);

			// update the UI
			base.UpdateDesignTimeHtml();
		}
		
		// catch the event when a component change from the properties
		public override void OnComponentChanged(object obj, ComponentChangedEventArgs ce) {
			if(ce.Member.Name.ToString() == "ButtonType") {
				if(ce.NewValue.ToString() == "LINK") {
					buttonType = BUTTON_TYPE.LINK;
				} else if(ce.NewValue.ToString() == "BUTTON") {
					buttonType = BUTTON_TYPE.BUTTON;
				} else if(ce.NewValue.ToString() == "IMAGE") {
					buttonType = BUTTON_TYPE.IMAGE;
				}
			} else if(ce.Member.Name.ToString() == "ImageAlign") {
				if(buttonType != BUTTON_TYPE.IMAGE) {
					System.Windows.Forms.MessageBox.Show("Image aligment is only available with the image button!", "Warning", 
						System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
					return;
				}
			} else if(ce.Member.Name.ToString() == "ID") {
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
//					if(System.IO.File.Exists(aspxFileName + ".xml.bak")) {
//						System.IO.File.Delete(aspxFileName + ".xml.bak");
//					}
//					if(System.IO.File.Exists(aspxFileName + ".xml")) {
//						System.IO.File.Move(aspxFileName + ".xml", aspxFileName + ".xml.bak");
//					}

					try {
						System.IO.File.Delete(aspxFileName + ".xml");
					} catch(System.Exception ex) {
						System.Windows.Forms.MessageBox.Show("Unable to delete file: " + ex.Message);
						return;
					}

					uiController.Save(aspxFileName + ".xml", aspxFileName);
					// Utils.SaveFile(aspxFileName + ".xml", uiController.GenerateXML(aspxFileName));
					//efundraising.Utilities.IO.FileHandler.CreateNewFile(
					//	aspxFileName + ".xml", uiController.GenerateXML(aspxFileName + ".xml"));
				} catch(System.Exception ex) {
					System.Windows.Forms.MessageBox.Show("Unable to save config file: " + ex.Message);
				}
			}
			
			IsDirty = true;

			base.OnComponentChanged(obj, ce);
			base.UpdateDesignTimeHtml();
		}

		private string GetData() {
			if(aspxFileName != "") {
				// get the component we draw
				ButtonPanelControl tpc = (ButtonPanelControl)Component;	

				UIControls.Config.UIController uiController = 
					new UIControls.Config.UIController(true);

				// PanelControl.SetPageAttributeToNormal(aspxFileName + ".xml");
				
				uiController = (UIControls.Config.UIController)
					GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromXmlFile(aspxFileName + ".xml", typeof(UIControls.Config.UIController));
				uiController.ReadDataFromDataSource(aspxFileName);
				foreach(UIControls.Config.UIControl c in uiController.UiControls.ControlList) {
					if(c.ID == tpc.ID) {
						// we got the right component from the config file
						foreach(UIControls.Config.PartnerID p in c.PartnersID.PartnerIdList) {
							if(p.ID == BaseConfig.GlobalizerConfigs.GetDesignDefaultPartnerID()) {
								foreach(UIControls.Config.Culture cc in p.Cultures.CultureList) {
									if(cc.ID == BaseConfig.GlobalizerConfigs.GetDesignDefaultCulture()) {
										// return System.Web.HttpUtility.HtmlEncode(cc.Data.Parameters.Parameter[0].ToString());
										return cc.Data.Parameters.Parameter[0].ToString();
									}
								}
							}
						}
					}
				}
				return "<b>ERROR ON CONTROL!</b>";
			} else {
				return "Button";
			}
		}

		// return the html display we want the UI control to see at design time
		public override string GetDesignTimeHtml() {
			string r = "";
			switch(buttonType) {
				case BUTTON_TYPE.BUTTON:
					r = "<input type='submit' value='" + GetData() + "'>";
					break;
				case BUTTON_TYPE.LINK:
					r = "<a href='javascript:void();'>" + GetData() + "</a>";
					break;
				case BUTTON_TYPE.IMAGE:
					r = "<img src='" + GetData() + "'>";
					break;
			}
			return r;
		}
	}

	/// <summary>
	/// Summary description for PagePanelControl.
	/// </summary>
	[DesignerAttribute(typeof(ButtonPanelDesigner), typeof(IDesigner))]
	[DefaultEvent("Click")]
	public class ButtonPanelControl : PanelControl {
		// private System.Web.UI.WebControls.WebControl button = null;
		private System.Web.UI.WebControls.Button button = null;
		private System.Web.UI.WebControls.LinkButton link = null;
		private System.Web.UI.WebControls.ImageButton image = null;

		private string text;
		private string aspxFileName = "";

		private bool addClicks = false;
		private bool showErrors = false;
		private bool showClicks = false;

		private bool isDefault;

		// tracking
		private string codeName;
		private string commandArgument;
		private string alt;

		// what is the button
		private BUTTON_TYPE buttonType;

		// More attributes
		private System.Web.UI.WebControls.ImageAlign imageAlign;
		private bool causesValidation;

		[Category("Action")]
		[Description("Fires when the Button is clicked.")]
		public event TrackingButtonEventHandler Click;

		// sets the text
		public ButtonPanelControl() {

		}

		#region Property setted by the Globalizer Page Config
		public string Text {
			set { text = value;	}
			get { return text; }
		}
		#endregion

		private int GetTrackableObjectTypeID(string s) {
			if(s.ToLower().StartsWith("img")) {
				return 2;
			} else if(s.ToLower().StartsWith("lnk")) {
				return 1;
			} else if(s.ToLower().StartsWith("btn")) {
				return 5;
			}
			return -1;
		}

		private string GetErrorMessage() {
			if(CodeName == null) {
				return "Code Name is Undefined!";	
			} else if(CodeName == "") {
				return "Code Name is Undefined!";
			}

			bool codeNameDoNotExistsInDatabase = IsConfigErrors();

			if(codeNameDoNotExistsInDatabase) {
				WebTracking.TrackableObject trackableObject =
					new WebTracking.TrackableObject();
				trackableObject.TrackableObjectTypeID = GetTrackableObjectTypeID(CodeName);
				if(trackableObject.TrackableObjectTypeID != -1) {
					trackableObject.TrackingCode = CodeName;
					trackableObject.TrackableObjectDesc = 
						System.IO.Path.GetFileName(Page.Request.Url.AbsoluteUri);
					try {
						trackableObject.InsertIntoDatabase();
						codeNameDoNotExistsInDatabase = IsConfigErrors();
					} catch(System.Exception ex) {  
						return "Exception thrown: " + CodeName + " " + 
							trackableObject.TrackableObjectDesc + " " + ex.Message;
					}
				}
			}

			if(codeNameDoNotExistsInDatabase) {
				return "The code name is not entered in the database!";
			}
			return "";
		}

		// this user control is loaded at run-time
		private void LoadButtonPanelControl(object sender, System.EventArgs e) {
			// set the values like AddClicks, ShowErrors or Show Buttons Clicks
			SetTrackingProperties();
			InitPanelControl();

			string toolTip = "";

			if(showClicks || showErrors) {
				toolTip += "Code Name: " + CodeName + "\r\n";
			}

			// show number of clicks per partner
			int clickCount = -1;
			if(showClicks) {
				clickCount = GetClicksCount();
				toolTip += "Number of clicks: " + clickCount.ToString() + "\r\n";
			}

			// check if this control is well configured
			string errorMessage = "";
			if(showErrors) {
				errorMessage = GetErrorMessage();
				if(errorMessage != "") {
					toolTip += "Error Message: " + errorMessage;
				} else {
					toolTip += "Error Message: No Errors";
				}
			}

			switch(buttonType) {
				case BUTTON_TYPE.BUTTON:
					button = new System.Web.UI.WebControls.Button();
					button.Text = text;
					button.Click += new System.EventHandler(OnClick);
					button.ToolTip = toolTip;
					button.CausesValidation = causesValidation;
					WebControl b = (WebControl)button;
					SetCommontProperties(ref b);
					if(showClicks) {
						button.Text += "[" + clickCount + "]";
					}
					if(showErrors) {
						if(errorMessage != "") {
							button.BackColor = Color.Red;
							button.Text += "[X]";
						}
					}
					Controls.Add(button);
					break;
				case BUTTON_TYPE.LINK:
					link = new System.Web.UI.WebControls.LinkButton();
					link.Text = text;
					link.ToolTip = toolTip;
					link.CausesValidation = causesValidation;
					link.Click += new System.EventHandler(OnClick);
					WebControl l = (WebControl)link;
					SetCommontProperties(ref l);
					if(showClicks) {
						link.Text += "[" + clickCount + "]";
					}
					if(showErrors) {
						if(errorMessage != "") {
							link.BackColor = Color.Red;
							link.Text += "[X]";
						}
					}
					Controls.Add(link);
					break;
				case BUTTON_TYPE.IMAGE:
					image = new System.Web.UI.WebControls.ImageButton();
					image.ImageUrl = text;
					image.ImageAlign = ImageAlign;
					image.AlternateText = alt;
					image.ToolTip = toolTip;
					image.CausesValidation = causesValidation;
					image.Click += new System.Web.UI.ImageClickEventHandler(OnClick);
					WebControl i = (WebControl)image;
					SetCommontProperties(ref i);
					if(showClicks) {
						image.BorderWidth = 1;
						image.BorderStyle = BorderStyle.Solid;
						image.BorderColor = Color.Tan;
						Literal litMessage = new Literal();
						litMessage.Text = "[" + clickCount + "]<br>";
						Controls.Add(litMessage);
					}
					if(showErrors) {
						if(errorMessage != "") {
							image.BorderWidth = 2;
							image.BorderStyle = BorderStyle.Solid;
							image.BorderColor = Color.Red;
						}
					}
					Controls.Add(image);
					break;
			}

			if(revealYourself) {
				switch(buttonType) {
					case BUTTON_TYPE.BUTTON:
						button.BackColor = Color.YellowGreen;
						break;
					case BUTTON_TYPE.LINK:
						link.BackColor = Color.YellowGreen;
						break;
					case BUTTON_TYPE.IMAGE:
						image.BorderStyle = BorderStyle.Double;
						image.BorderWidth = Unit.Pixel(2);
						image.BorderColor = Color.YellowGreen;
						break;
				}
			}

			if(!Page.IsPostBack) {
				Click += new TrackingButtonEventHandler(OnClick);
			}
		}

		// get the properties of the web project settings
		// like 'show errors, show clicks, log clicks'
		protected void SetTrackingProperties() {
			// get the connection string
			string trackingButtonConnectionString = "";
			if(Page.Session["TrackingButton.ConnectionString"] != null) {
				trackingButtonConnectionString = Page.Session["TrackingButton.ConnectionString"].ToString();
			} else { return; }

			// save the clicks
			addClicks = false;
			if(Page.Session["TrackingButton.SaveClicks"] != null) {
				if(Page.Session["TrackingButton.SaveClicks"].ToString().ToLower() == "true") {
					addClicks = true;
				}
			}

			// do not use the tracking?  we leave, if you want to see the clicks or
			// errors, set TrackingButton.SaveClicks to true
			if(!addClicks) { 
				return; 
			}

			// get the page id (set by the web page that is using this control
			int pageID = -1;
			if(Page.Session["TrackingButton.PageID"] != null) {
				pageID = int.Parse(Page.Session["TrackingButton.PageID"].ToString());
			} else { return; }

			// this control will show the clicks?
			showClicks = false;
			if(Page.Session["TrackingButton.ShowClicks"] != null) {
				if(Page.Session["TrackingButton.ShowClicks"].ToString().ToLower() == "true") {
					showClicks = true;
				}
			}

			// this control shows the errors?
			showErrors = false;
			if(Page.Session["TrackingButton.ConfigErrors"] != null) {
				if(Page.Session["TrackingButton.ConfigErrors"].ToString().ToLower() == "true") {
					showErrors = true;
				}
			}
		}

		#region Database Calls

		private void InsertVisitorLogClick() {
			if(CodeName != null && CodeName != "") {
				WebTracking.VisitorLog visitorLog = WebTracking.VisitorLog.Create(Page.Session);
				if(visitorLog.VisitorLogID > 0) {
					WebTracking.VisitorTrack visitorTrack = 
						new WebTracking.VisitorTrack(visitorLog);
					visitorTrack.InsertIntoDatabase(CodeName);
				}
			}
		}

		#region Old
		private void TBDInsertVisitorLogClick() {

			int pageID = -1;
			int visitorLogID = -1;
			int webProjectID = 1;
			string connectionString = "";
			
			try {
				System.Web.SessionState.HttpSessionState session = this.Page.Session;

				if(session["TrackingButton.PageID"] != null) {
					pageID = int.Parse(session["TrackingButton.PageID"].ToString());
				}

				if(session["TrackingButton.VisitorLogID"] != null) {
					visitorLogID = int.Parse(session["TrackingButton.VisitorLogID"].ToString());
				}

				if(session["TrackingButton.WebProjectID"] != null) {
					webProjectID = int.Parse(session["TrackingButton.WebProjectID"].ToString());
				}

				if(session["TrackingButton.ConnectionString"] != null) {
					connectionString = session["TrackingButton.ConnectionString"].ToString();
				}
			} catch { return; }

			DataParameters[] parameters = new DataParameters[5];

			parameters[0] = new DataParameters();
			parameters[0].ParameterName = "@intVisitorsLogID";
			parameters[0].DataType = DbType.Int32;
			parameters[0].ParamDirection = ParameterDirection.Input;
			parameters[0].Value = visitorLogID;

			parameters[1] = new DataParameters();
			parameters[1].ParameterName = "@intWebprojectID";
			parameters[1].DataType = DbType.Int16;
			parameters[1].ParamDirection = ParameterDirection.Input;
			parameters[1].Value = webProjectID;

			parameters[2] = new DataParameters();
			parameters[2].ParameterName = "@intCurrentWebpageID";
			parameters[2].DataType = DbType.Int32;
			parameters[2].ParamDirection = ParameterDirection.Input;
			parameters[2].Value = pageID;

			parameters[3] = new DataParameters();
			parameters[3].ParameterName = "@strWebpageUrl";
			parameters[3].DataType = DbType.String;
			parameters[3].ParamDirection = ParameterDirection.Input;
			parameters[3].Value = DBNull.Value;

			parameters[4] = new DataParameters();
			parameters[4].ParameterName = "@strTrackingCode";
			parameters[4].DataType = DbType.String;
			parameters[4].ParamDirection = ParameterDirection.Input;
			parameters[4].Value = CodeName;

			DatabaseInterface dbi = new DatabaseInterface();


			try {
				dbi.PredefinedConnectionString = connectionString;
				dbi.ExecuteScalar("track_visitors_clicks", parameters);
			} catch {}
		}
		#endregion

		public int GetClicksCount() {
			if(CodeName == null || CodeName == "") {
				return int.MinValue;
			}
			return WebTracking.TrackableObject.GetClickCount(CodeName);
		}


		private bool IsConfigErrors() {
			if(CodeName == null && CodeName == "") {
				return true;
			}

			WebTracking.TrackableObject trackableObject =
				WebTracking.TrackableObject.GetTrackableObject(CodeName);
			if(trackableObject != null) {
				return false;	// no errors
			}
			return true;	// no entry in db
		}

		#endregion

		private void SaveClick() {
			if(addClicks) {
				if(Page.IsPostBack) {
					InsertVisitorLogClick();
				}
			}
		}

		// click event of the image button
		// todo: add area map to this control
		private void OnClick(object sender, System.Web.UI.ImageClickEventArgs e) {
			OnClick(sender, (System.EventArgs)e);
		}

		// click event of all controls, it raises the click event on the page
		// in design-time
		public virtual void OnClick(object sender, System.EventArgs e) {
			if(Click != null) {
				SaveClick();
				Click(this, e);
			}
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
			this.Load += new System.EventHandler(LoadButtonPanelControl);
		}

		#endregion
		#endregion

		#region Public Attributes

		[Browsable(true),
		Category("Settings"),
		Description("Default button")]
		public bool IsDefault {
			set { isDefault = value; }
			get { return isDefault; }
		}

		[Browsable(true),
		Category("Settings"),
		Description("Set or get the page configuration file.")]
		public string ASPXFileName {
			get { return aspxFileName; }
			set { aspxFileName = value; }
		}

		[Browsable(true),
		Category("Settings"),
		Description("Set or get the button type.")]
		public BUTTON_TYPE ButtonType {
			set { buttonType = value; }
			get { return buttonType; }
		}

		[Browsable(true),
		Category("Tracking"),
		Description("Set/Get the click name")]
		public string CodeName {
			set { codeName = value; }
			get { return codeName; }
		}

		[Browsable(true),
		Category("Tracking"),
		Description("Set/Get the alternate image tag")]
		public string Alt {
			set { alt = value; }
			get { return alt; }
		}

		[Browsable(true),
		Category("Layout"),
		Description("Aligment of the control")]
		public System.Web.UI.WebControls.ImageAlign ImageAlign {
			set { imageAlign = value; }
			get { return imageAlign; }
		}

		[Browsable(true),
		Category("Layout"),
		Description("Store serialized data")]
		public string CommandArgument {
			set { commandArgument = value; }
			get { return commandArgument; }
		}

		[Browsable(true),
		Category("Behavior"),
		Description("Affected by validations controls")]
		public bool CausesValidation {
			set { causesValidation = value; }
			get { return causesValidation; }
		}
		#endregion
	}
}
