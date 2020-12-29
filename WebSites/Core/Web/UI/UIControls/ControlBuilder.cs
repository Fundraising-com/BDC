/* Jean-Francois Buist - March 1, 2005
 * This component is used to design multi-language web sites.
 * This is also a form of content management way.
 * This control should always support cultures.
 * 
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GA.BDC.Core.Web.UI.UIControls {
	// components list, UI = User Interface which is the OUTPUT
	//					TK = Tracking Button, which is the INPUT
	public enum COMPONENT_TYPE : byte {
		UI_CONTENT,
		UI_IMAGE,
		TK_BUTTON,
		TK_LINK,
		TK_IMAGE_BUTTON
	}

	/// <summary>
	/// Summary description for ControlBuilder.
	/// </summary>
	public class ControlBuilder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.GroupBox groupBox3;
		private string aspxFilename;
		private string controlName = "";
		private Config.UIControl control = null;
		private Config.UIController uiController = null;
		private System.Windows.Forms.Button cmdSave;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdOpenFile;
		private System.Windows.Forms.TextBox txtASPXFile;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panelContent;
		private System.Windows.Forms.ListBox lstPartners;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button cmdParameters;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panelPreview;
		private System.Windows.Forms.Panel panelExtraParam;
		private COMPONENT_TYPE componentType;

		public ControlBuilder()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmdSave = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdOpenFile = new System.Windows.Forms.Button();
			this.txtASPXFile = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.panelContent = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panelPreview = new System.Windows.Forms.Panel();
			this.lstPartners = new System.Windows.Forms.ListBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.cmdParameters = new System.Windows.Forms.Button();
			this.panel6 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtType = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panelExtraParam = new System.Windows.Forms.Panel();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmdSave);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cmdOpenFile);
			this.groupBox1.Controls.Add(this.txtASPXFile);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 357);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(696, 48);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// cmdSave
			// 
			this.cmdSave.Location = new System.Drawing.Point(496, 16);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.TabIndex = 7;
			this.cmdSave.Text = "Save";
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "ASPX Filename:";
			// 
			// cmdOpenFile
			// 
			this.cmdOpenFile.Location = new System.Drawing.Point(464, 16);
			this.cmdOpenFile.Name = "cmdOpenFile";
			this.cmdOpenFile.Size = new System.Drawing.Size(24, 23);
			this.cmdOpenFile.TabIndex = 5;
			this.cmdOpenFile.Text = "...";
			this.cmdOpenFile.Click += new System.EventHandler(this.cmdOpenFile_Click);
			// 
			// txtASPXFile
			// 
			this.txtASPXFile.Location = new System.Drawing.Point(128, 16);
			this.txtASPXFile.Name = "txtASPXFile";
			this.txtASPXFile.Size = new System.Drawing.Size(320, 20);
			this.txtASPXFile.TabIndex = 4;
			this.txtASPXFile.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.panelContent);
			this.groupBox2.Controls.Add(this.splitter1);
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(696, 357);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			// 
			// panelContent
			// 
			this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelContent.Location = new System.Drawing.Point(155, 16);
			this.panelContent.Name = "panelContent";
			this.panelContent.Size = new System.Drawing.Size(538, 338);
			this.panelContent.TabIndex = 2;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(152, 16);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 338);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.panel1);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox3.Location = new System.Drawing.Point(3, 16);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(149, 338);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(143, 319);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panelPreview);
			this.panel2.Controls.Add(this.panelExtraParam);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(143, 319);
			this.panel2.TabIndex = 3;
			// 
			// panelPreview
			// 
			this.panelPreview.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelPreview.Location = new System.Drawing.Point(0, 0);
			this.panelPreview.Name = "panelPreview";
			this.panelPreview.Size = new System.Drawing.Size(143, 112);
			this.panelPreview.TabIndex = 4;
			// 
			// lstPartners
			// 
			this.lstPartners.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstPartners.Location = new System.Drawing.Point(0, 0);
			this.lstPartners.Name = "lstPartners";
			this.lstPartners.Size = new System.Drawing.Size(143, 43);
			this.lstPartners.TabIndex = 3;
			// 
			// panel4
			// 
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(143, 48);
			this.panel4.TabIndex = 1;
			// 
			// cmdParameters
			// 
			this.cmdParameters.Location = new System.Drawing.Point(0, 0);
			this.cmdParameters.Name = "cmdParameters";
			this.cmdParameters.Size = new System.Drawing.Size(128, 24);
			this.cmdParameters.TabIndex = 3;
			this.cmdParameters.Text = "Parameters...";
			// 
			// panel6
			// 
			this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel6.Location = new System.Drawing.Point(0, 136);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(143, 24);
			this.panel6.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 5;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(72, 56);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(64, 20);
			this.txtName.TabIndex = 3;
			this.txtName.Text = "";
			// 
			// txtType
			// 
			this.txtType.Location = new System.Drawing.Point(72, 80);
			this.txtType.Name = "txtType";
			this.txtType.Size = new System.Drawing.Size(64, 20);
			this.txtType.TabIndex = 7;
			this.txtType.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 4;
			// 
			// panel5
			// 
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(0, 0);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(143, 160);
			this.panel5.TabIndex = 2;
			// 
			// panelExtraParam
			// 
			this.panelExtraParam.BackColor = System.Drawing.SystemColors.Control;
			this.panelExtraParam.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExtraParam.Location = new System.Drawing.Point(0, 0);
			this.panelExtraParam.Name = "panelExtraParam";
			this.panelExtraParam.Size = new System.Drawing.Size(143, 319);
			this.panelExtraParam.TabIndex = 0;
			// 
			// ControlBuilder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(696, 405);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "ControlBuilder";
			this.Text = "ControlBuilder";
			this.Load += new System.EventHandler(this.ControlBuilder_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		// every control provides a preview that tells the user what he is doing
		public void LoadPreview() {
			string previewContent = "";
			switch(componentType) {
				case COMPONENT_TYPE.UI_CONTENT:
					foreach(UIControls.Config.UIControl c in uiController.UiControls.ControlList) {
						if(c.ID == controlName) {
							// we got the right component from the config file
							UIControls.Config.PartnerID p = (UIControls.Config.PartnerID)c.PartnersID.PartnerIdList[0];
							UIControls.Config.Culture cc = (UIControls.Config.Culture)p.Cultures.CultureList[0];
							previewContent = cc.Data.Parameters.Parameter[0].ToString();
						}
					}
					Preview.Content content = new Preview.Content(previewContent);
					panelPreview.Controls.Clear();
					panelPreview.Controls.Add(content);
					content.Dock = DockStyle.Fill;
					break;
				case COMPONENT_TYPE.TK_IMAGE_BUTTON:
					foreach(UIControls.Config.UIControl c in uiController.UiControls.ControlList) {
						if(c.ID == controlName) {
							// we got the right component from the config file
							UIControls.Config.PartnerID p = (UIControls.Config.PartnerID)c.PartnersID.PartnerIdList[0];
							UIControls.Config.Culture cc = (UIControls.Config.Culture)p.Cultures.CultureList[0];
							previewContent = cc.Data.Parameters.Parameter[0].ToString();
						}
					}
					Preview.Image image = new Preview.Image(System.IO.Path.GetDirectoryName(aspxFilename) + "\\" + previewContent.Replace("/", "\\"));
					panelPreview.Controls.Clear();
					panelPreview.Controls.Add(image);
					image.Dock = DockStyle.Fill;
					break;
				case COMPONENT_TYPE.TK_BUTTON:
					foreach(UIControls.Config.UIControl c in uiController.UiControls.ControlList) {
						if(c.ID == controlName) {
							// we got the right component from the config file
							UIControls.Config.PartnerID p = (UIControls.Config.PartnerID)c.PartnersID.PartnerIdList[0];
							UIControls.Config.Culture cc = (UIControls.Config.Culture)p.Cultures.CultureList[0];
							previewContent = cc.Data.Parameters.Parameter[0].ToString();
						}
					}
					Preview.Button con = new Preview.Button(previewContent);
					panelPreview.Controls.Clear();
					panelPreview.Controls.Add(con);
					con.Dock = DockStyle.Fill;
					break;
				case COMPONENT_TYPE.TK_LINK:
					foreach(UIControls.Config.UIControl c in uiController.UiControls.ControlList) {
						if(c.ID == controlName) {
							// we got the right component from the config file
							UIControls.Config.PartnerID p = (UIControls.Config.PartnerID)c.PartnersID.PartnerIdList[0];
							UIControls.Config.Culture cc = (UIControls.Config.Culture)p.Cultures.CultureList[0];
							previewContent = cc.Data.Parameters.Parameter[0].ToString();
						}
					}
					Preview.Link link = new Preview.Link(previewContent);
					panelPreview.Controls.Clear();
					panelPreview.Controls.Add(link);
					link.Dock = DockStyle.Fill;
					break;
			}
		}

		// load the partners setup user control to manipulate the controls data
		private void LoadPartnersIDForm() {
			GA.BDC.Core.Web.UI.UIControls.Controls.Partners p =
				new GA.BDC.Core.Web.UI.UIControls.Controls.Partners(this);
			Config.PartnersID pa = control.PartnersID;
			p.SetPartnersID(ref pa);
			panelContent.Controls.Clear();
			panelContent.Controls.Add(p);
			p.Dock = DockStyle.Fill;
		}

		private Config.PartnerID GetDefaultPartner() {
			Config.PartnerID p = new Config.PartnerID();
			p.ID = "Default";

			BaseConfig.GlobalizerConfigs gcs = new BaseConfig.GlobalizerConfigs();
			gcs.LoadXML();

			BaseConfig.GlobalizerConfig gc = gcs.GetCurrentWorkingConfig();
			foreach(BaseConfig.SupportedCulture sc in gc.SupportedCultures.SupportedCultureList) {
				Config.Culture c = new Config.Culture();
				c.ID = sc.Name;
				c.Data.Source = "Binary File";
				switch(componentType) {
					case COMPONENT_TYPE.UI_CONTENT:
						c.Data.Parameters.Parameter.Add("");
						break;
					case COMPONENT_TYPE.TK_IMAGE_BUTTON:
						c.Data.Parameters.Parameter.Add("[IMAGEURL]");
						break;
					case COMPONENT_TYPE.TK_BUTTON:
						c.Data.Parameters.Parameter.Add("Button");
						break;
					case COMPONENT_TYPE.TK_LINK:
						c.Data.Parameters.Parameter.Add("Link");
						break;
				}
				p.Cultures.CultureList.Add(c);
			}
			return p;
		}

		// load event, it build itself by using ui design control parameters
		private void ControlBuilder_Load(object sender, System.EventArgs e) {
			
			// display the page config filename
			txtASPXFile.Text = aspxFilename;
			if(aspxFilename != "") {
				// the page config exists, then we load it
				uiController = new GA.BDC.Core.Web.UI.UIControls.Config.UIController();
				uiController = (Config.UIController) GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromXmlFile(
					aspxFilename + ".xml", typeof(Config.UIController));
				// uiController.Load(aspxFilename + ".xml");
				try {
					uiController.ReadDataFromDataSource(aspxFilename);
				} catch(System.Exception ex) {
					System.Windows.Forms.MessageBox.Show("Unable to fill data: " + ex.Message);
					return;
				}

				// set the current working ui control by using the control name
				// provided by the design time control
				if(uiController.UiControls.ControlList != null) {
					foreach(Config.UIControl c in uiController.UiControls.ControlList) {
						if(c.Name == controlName) {
							control = c;
							break;
						}
					}
				} else {
					System.Windows.Forms.MessageBox.Show("Internal Error");
					return;
				}

				if(control == null) {
					Config.UIControl c = new Config.UIControl();
					c.ID = controlName;
					c.Name = controlName;
					switch(componentType) {
						case COMPONENT_TYPE.TK_BUTTON:
						case COMPONENT_TYPE.TK_IMAGE_BUTTON:
						case COMPONENT_TYPE.TK_LINK:
							c.Type = "ButtonPanelControl";
							break;
						case COMPONENT_TYPE.UI_CONTENT:
						case COMPONENT_TYPE.UI_IMAGE:
							c.Type = "ContentPanelControl";
							break;
						default:
							System.Windows.Forms.MessageBox.Show("Internal Error");
							return;
					}
					c.Parameters.Parameter.Add("");
					c.PartnersID.PartnerIdList.Add(GetDefaultPartner());
					uiController.UiControls.ControlList.Add(c);
					control = c;
				}
			} else {
				System.Windows.Forms.MessageBox.Show("Unable to load config file");
				return;
			}

			LoadPreview();
			LoadPartnersIDForm();

			if((System.IO.File.GetAttributes(aspxFilename + ".xml")
				& System.IO.FileAttributes.ReadOnly) ==
				System.IO.FileAttributes.ReadOnly) {
				System.Windows.Forms.MessageBox.Show("Your files are read only, checkout the config\r\nand data files before any change.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		// set the name of the control we are working on design time
		public void SetControlName(string s) {
			controlName = s;
		}

		// set the component type described as an enumeration list
		public void SetComponentType(COMPONENT_TYPE compType) {
			componentType = compType;
		}

		// when the user want to open a file for a specified control
		private void cmdOpenFile_Click(object sender, System.EventArgs e) {
			System.Windows.Forms.OpenFileDialog ofd = new
				System.Windows.Forms.OpenFileDialog();
			if(ofd.ShowDialog() == DialogResult.OK) {
				txtASPXFile.Text = ofd.FileName;
				ASPXFilename = ofd.FileName;
				uiController = new GA.BDC.Core.Web.UI.UIControls.Config.UIController();
				// uiController.Load(txtASPXFile.Text + ".xml");
				uiController = (Config.UIController) GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromXmlFile(
					txtASPXFile.Text + ".xml", typeof(Config.UIController));
			}
		}

		// everything is done?  we save it.
		private void cmdSave_Click(object sender, System.EventArgs e) {
			try {
				try {
					System.IO.File.Delete(ASPXFilename + ".xml");
				} catch(System.Exception ex) {
					System.Windows.Forms.MessageBox.Show("Unable to delete file: " + ex.Message);
					return;
				}


				uiController.Save(ASPXFilename + ".xml", ASPXFilename);
				Close();
			} catch(System.Exception ex) {
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#region Attributes
		public string ASPXFilename {
			set { aspxFilename = value; }
			get { return aspxFilename; }
		}
		#endregion
	}
}
