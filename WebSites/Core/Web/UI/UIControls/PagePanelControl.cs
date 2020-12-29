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

[assembly: TagPrefix("GA.BDC.Core.Web.UI.UIControls", "PagePanel")]
namespace GA.BDC.Core.Web.UI.UIControls {

	/// <summary>
	/// This class is used by the Visual Studio at Design Time.
	/// You can set control properties behavior in the ControlDesigner class.
	/// </summary>
	public class PagePanelDesigner : ControlDesigner {
		private string aspxFileName;
		private PageBuilder builder = null;

		public PagePanelDesigner() {
			aspxFileName = "";

			EnvDTE.DTE myDTE;
			myDTE = (EnvDTE.DTE)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.8.0"); // Get an instance of the currently running Visual Studio .NET IDE.

			aspxFileName = myDTE.ActiveDocument.FullName + ".xml";
		}

		public override void OnSetParent() {
			// this places the text when the control is attached to it's component at
			// design time.
			PagePanelControl tpc = (PagePanelControl) Component;	// get the component we draw
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
			builder = new PageBuilder();
			builder.ASPXFilename = aspxFileName;
			builder.ShowDialog();
			IsDirty = true;					// tell the IDE know the value has changed

			PagePanelControl tpc = (PagePanelControl)Component;		// get the component we draw
			aspxFileName = builder.ASPXFilename;
			tpc.ASPXFileName = aspxFileName;	// set the filename to the design-time component

			// this refreshes the component at design-time
			// the VS IDE doesn't understand by itself if there is a change
			PropertyDescriptor properties;
			properties = TypeDescriptor.GetProperties(typeof(PagePanelControl))["ASPXFileName"]; 
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
					//GA.BDC.Core.Utilities.IO.FileHandler.CreateNewFile(
					//	aspxFileName + ".xml", uiController.GenerateXML(aspxFileName + ".xml"));
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
			return "<b>Globalizer Page Configuration</b>";
		}
	}

	/// <summary>
	/// Summary description for PagePanelControl.
	/// </summary>
	[DesignerAttribute(typeof(PagePanelDesigner), typeof(IDesigner))]
	public class PagePanelControl : PanelControl {
		private System.Web.UI.LiteralControl panel = new System.Web.UI.LiteralControl("literalControl");
		private string aspxFileName = "";
		// load the configuration
		private UIControls.Config.UIController uiControls = 
			new UIControls.Config.UIController(true);


		// sets the text
		public PagePanelControl() {
			panel.Text = "<b>Globalizer Page Configuration</b>";
			Controls.Add(panel);

			bool designtime = true;
			EnvDTE.DTE myDTE = null;
			try {
				// if it can't get the IDE running, it means it's on a running web
				myDTE = (EnvDTE.DTE)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.7.1"); // Get an instance of the currently running Visual Studio .NET IDE.
			} catch { designtime = false; }

			if(ASPXFileName == "" && designtime) {
				if(myDTE != null) {
                    aspxFileName = myDTE.ActiveDocument.FullName;
				}
			}
		}

		#region Run-Time Methods
		// it loops through the meta contents and build it
		private string BuildMetaContents(UIControls.Config.MetaTags mtags, string partnerID, string cultureName, string partnerType) {
			string metaTags = "";
			
			foreach(UIControls.Config.MetaTag m in mtags.MetaTagList) {
				metaTags += "<meta name=\"" + m.Name + "\" content=\"" + GetData(m.PartnersIds, partnerID, cultureName, partnerType) + "\">\r\n";
			}
			return metaTags;
		}

		// this method has to be called on the load event of a page or user control, since
		// user controls init() seems to be called before the Page init(), then we need
		// page information to process this component.
		public void Refresh(DynTag dTags, UserControl uc) {
			panel.Visible = false;
			
			// load the configuration
			//UIControls.Config.UIController uiControls = new UIControls.Config.UIController(true);
			// uiControls.Load(aspxFileName + ".xml");
            uiControls = (Config.UIController)GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromXmlFile(
                aspxFileName + ".xml", typeof(Config.UIController));
           
			// uiControls.ReadDataFromDataSource(aspxFileName);

            try {
                uiControls.SetDynamicTags(dTags);
            } catch(System.Exception ex) {
                throw new Exception("Unable to set DynTags", ex);
            }

			// get the culture
            string cultureName = BaseConfig.GlobalizerConfigs.GetDesignDefaultCulture();
            if(Page.Session[GlobalizerBasePage.CurrentCultureSessionKey] != null) {
                cultureName = Page.Session[GlobalizerBasePage.CurrentCultureSessionKey].ToString();
            }

            // get the partner id
            string partnerID = BaseConfig.GlobalizerConfigs.GetDesignDefaultPartnerID();
            if(Page.Session[GlobalizerBasePage.CurrentPartnerIDSessionKey] != null) {
                partnerID = Page.Session[GlobalizerBasePage.CurrentPartnerIDSessionKey].ToString();
            }

            string partnerType = "";
            if(Page.Session[GlobalizerBasePage.CurrentPartnerTypeSessionKey] != null) {
                partnerType = Page.Session[GlobalizerBasePage.CurrentPartnerTypeSessionKey].ToString();
            }

            if(uc == null) {
                // set page title, these sessions variables will be set to the page
                Page.Session[GlobalizerBasePage.PageTitleSessionKey] = GetData(uiControls.Title.PartnersId, partnerID, cultureName, partnerType);
                // set meta tags
                Page.Session[GlobalizerBasePage.MetaContentSessionKey] = BuildMetaContents(uiControls.Header.MetaTags, partnerID, cultureName, partnerType);
            }

            // set ui controls
            foreach (UIControls.Config.UIControl uiControl in uiControls.UiControls.ControlList)
            {
                try
                {
                    string type = uiControl.Type;
                    string name = uiControl.Name;
                    string data = GetData(uiControl.PartnersID, partnerID, cultureName, partnerType);
                    data = data.Replace("_efund_", dTags["TemplatePath"]);
                    data = data.Replace("_classic_", dTags["Theme"]);

                    if (type == "Label")
                    {
                        Label label = (Label)Page.FindControl(name);
                        if (label != null)
                        {
                            label.Text = data;
                        }
                    }
                    else if (type == "Button")
                    {
                        Button button = (Button)Page.FindControl(name);
                        if (button != null)
                        {
                            button.Text = data;
                        }
                    }
                    else if (type == "ContentPanelControl")
                    {
                        if (uc != null)
                        {
                            ContentPanelControl cpc = (ContentPanelControl)uc.FindControl(name);
                            while (cpc == null)
                            {
                                if (uc == null)
                                {
                                    throw new Exception("Unable to retreive the Globalizer Control:(1) " + name);
                                }
                                if (uc.Parent == null)
                                {
                                    throw new Exception("Unable to retreive the Globalizer Control:(2) " + name);
                                }
                                uc = (UserControl)uc.Parent;
                                if (uc == null)
                                    break;
                                cpc = (ContentPanelControl)uc.FindControl(name);
                            }
                            if (cpc != null)
                            {
                                cpc.Text = data;
                            }
                        }
                        else
                        {
                            ContentPanelControl cpc = (ContentPanelControl)Page.FindControl(name);
                            if (cpc != null)
                            {
                                cpc.Text = data;
                            }
                        }
                    }
                    else if (type == "ButtonPanelControl")
                    {
                        if (uc != null)
                        {
                            ButtonPanelControl bpc = (ButtonPanelControl)uc.FindControl(name);
                            while (bpc == null)
                            {
                                if (uc == null)
                                {
                                    throw new Exception("Unable to retreive the Globalizer Control:(1) " + name);
                                }
                                if (uc.Parent == null)
                                {
                                    throw new Exception("Unable to retreive the Globalizer Control:(2) " + name);
                                }
                                uc = (UserControl)uc.Parent;
                                if (uc == null)
                                    break;
                                bpc = (ButtonPanelControl)uc.FindControl(name);
                            }
                            if (bpc != null)
                            {
                                bpc.Text = data;
                            }
                        }
                        else
                        {
                            ButtonPanelControl bpc = (ButtonPanelControl)Page.FindControl(name);
                            if (bpc != null)
                            {
                                bpc.Text = data;
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    throw new Exception("Unable to refresh object " + uiControl.ID, ex);
                }
            }
		}

        public string GetValue(DynTag dTags, string key)
        {
            // uiControls.Load(aspxFileName + ".xml");
            uiControls = (Config.UIController)GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromXmlFile(
                aspxFileName + ".xml", typeof(Config.UIController));

            uiControls.ReadDataFromDataSource(aspxFileName);
            uiControls.SetDynamicTags(dTags);

            // get the culture
            string cultureName = BaseConfig.GlobalizerConfigs.GetDesignDefaultCulture();
            if (Page.Session[GlobalizerBasePage.CurrentCultureSessionKey] != null)
            {
                cultureName = Page.Session[GlobalizerBasePage.CurrentCultureSessionKey].ToString();
            }

            // get the partner id
            string partnerID = BaseConfig.GlobalizerConfigs.GetDesignDefaultPartnerID();
            if (Page.Session[GlobalizerBasePage.CurrentPartnerIDSessionKey] != null)
            {
                partnerID = Page.Session[GlobalizerBasePage.CurrentPartnerIDSessionKey].ToString();
            }

            string partnerType = "";
            if (Page.Session[GlobalizerBasePage.CurrentPartnerTypeSessionKey] != null)
            {
                partnerType = Page.Session[GlobalizerBasePage.CurrentPartnerTypeSessionKey].ToString();
            }

            // set ui controls
            foreach (UIControls.Config.UIControl uiControl in uiControls.UiControls.ControlList)
            {
                string type = uiControl.Type;
                string name = uiControl.Name;
                string data = GetData(uiControl.PartnersID, partnerID, cultureName, partnerType);

                if (type == "Hidden" && name.ToLower() == key.ToLower())
                {
                    return data;
                }
            }
            return "";
        }

		#endregion

		#region I wish to be able to make this happen
//		protected override void OnInit(EventArgs e) {
//			Refresh(e);
//		}
		#endregion

		#region Public Attributes

		[Browsable(true),
		Category("Settings"),
		Description("Set or get the page configuration file.")]
		public string ASPXFileName {
			get { return aspxFileName; }
			set { aspxFileName = value; }
		}

		#endregion
	}
}
