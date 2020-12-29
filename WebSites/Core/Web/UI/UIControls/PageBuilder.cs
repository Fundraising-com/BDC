using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using GA.BDC.Core.Web.UI.UIControls.Config;

namespace GA.BDC.Core.Web.UI.UIControls
{
	/// <summary>
	/// Summary description for PageBuilder.
	/// </summary>
	public class PageBuilder : System.Windows.Forms.Form {
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TreeView tvPageAttributes;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.TextBox txtASPXFile;
		private System.Windows.Forms.Button cmdOpenFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdSave;
		private string aspxFilename;
		private GA.BDC.Core.Web.UI.UIControls.Config.UIController uiController;
		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.ContextMenu popupMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.Button cmdChangeProjectSettings;
		private System.Windows.Forms.MenuItem menuItem7;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PageBuilder()
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
			this.cmdChangeProjectSettings = new System.Windows.Forms.Button();
			this.cmdSave = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdOpenFile = new System.Windows.Forms.Button();
			this.txtASPXFile = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.tvPageAttributes = new System.Windows.Forms.TreeView();
			this.popupMenu = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmdChangeProjectSettings);
			this.groupBox1.Controls.Add(this.cmdSave);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cmdOpenFile);
			this.groupBox1.Controls.Add(this.txtASPXFile);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 358);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(776, 48);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// cmdChangeProjectSettings
			// 
			this.cmdChangeProjectSettings.Location = new System.Drawing.Point(576, 16);
			this.cmdChangeProjectSettings.Name = "cmdChangeProjectSettings";
			this.cmdChangeProjectSettings.Size = new System.Drawing.Size(136, 23);
			this.cmdChangeProjectSettings.TabIndex = 4;
			this.cmdChangeProjectSettings.Text = "Change Project Settings";
			this.cmdChangeProjectSettings.Click += new System.EventHandler(this.cmdChangeProjectSettings_Click);
			// 
			// cmdSave
			// 
			this.cmdSave.Location = new System.Drawing.Point(488, 16);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.TabIndex = 3;
			this.cmdSave.Text = "Save";
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "ASPX Filename:";
			// 
			// cmdOpenFile
			// 
			this.cmdOpenFile.Location = new System.Drawing.Point(456, 16);
			this.cmdOpenFile.Name = "cmdOpenFile";
			this.cmdOpenFile.Size = new System.Drawing.Size(24, 23);
			this.cmdOpenFile.TabIndex = 1;
			this.cmdOpenFile.Text = "...";
			this.cmdOpenFile.Click += new System.EventHandler(this.cmdOpenFile_Click);
			// 
			// txtASPXFile
			// 
			this.txtASPXFile.Location = new System.Drawing.Point(120, 16);
			this.txtASPXFile.Name = "txtASPXFile";
			this.txtASPXFile.Size = new System.Drawing.Size(320, 20);
			this.txtASPXFile.TabIndex = 0;
			this.txtASPXFile.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.mainPanel);
			this.groupBox2.Controls.Add(this.splitter1);
			this.groupBox2.Controls.Add(this.tvPageAttributes);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(776, 358);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			// 
			// mainPanel
			// 
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(155, 16);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(618, 339);
			this.mainPanel.TabIndex = 2;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(152, 16);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 339);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// tvPageAttributes
			// 
			this.tvPageAttributes.ContextMenu = this.popupMenu;
			this.tvPageAttributes.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvPageAttributes.ImageIndex = -1;
			this.tvPageAttributes.Location = new System.Drawing.Point(3, 16);
			this.tvPageAttributes.Name = "tvPageAttributes";
			this.tvPageAttributes.SelectedImageIndex = -1;
			this.tvPageAttributes.Size = new System.Drawing.Size(149, 339);
			this.tvPageAttributes.TabIndex = 0;
			this.tvPageAttributes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPageAttributes_AfterSelect);
			// 
			// popupMenu
			// 
			this.popupMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem4,
																					  this.menuItem7,
																					  this.menuItem5,
																					  this.menuItem6});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Add Version";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Add Partner";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Add Culture";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "Add Meta Tag";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 5;
			this.menuItem5.Text = "Delete";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 6;
			this.menuItem6.Text = "Update";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 4;
			this.menuItem7.Text = "Add Hidden Control";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// PageBuilder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(776, 406);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "PageBuilder";
			this.Text = "PageBuilder";
			this.Load += new System.EventHandler(this.PageBuilder_Load);
			this.Closed += new System.EventHandler(this.PageBuilder_Closed);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void PageBuilder_Load(object sender, System.EventArgs e) {
			// mainPanel is the area where we want to display user controls
			mainPanel.Controls.Clear();

			// this is the splash screen
			GA.BDC.Core.Web.UI.UIControls.Controls.GlobalizerSplash gs = 
				new GA.BDC.Core.Web.UI.UIControls.Controls.GlobalizerSplash();
			mainPanel.Controls.Add(gs);
			gs.Dock = DockStyle.Fill;

			// ASPX Filename has been passed by the designer control
			txtASPXFile.Text = aspxFilename;

			if(aspxFilename != "" && System.IO.File.Exists(aspxFilename + ".xml")) {
				// the file exists, so we load it's config
				uiController = new GA.BDC.Core.Web.UI.UIControls.Config.UIController();
				// uiController.Load(aspxFilename + ".xml");
				uiController = (Config.UIController) GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromXmlFile(
					aspxFilename + ".xml", typeof(Config.UIController));
				uiController.ReadDataFromDataSource(aspxFilename);
				FillUIControler(true);

				if((System.IO.File.GetAttributes(aspxFilename + ".xml")
					& System.IO.FileAttributes.ReadOnly) ==
					System.IO.FileAttributes.ReadOnly) {
					System.Windows.Forms.MessageBox.Show("Your files are read only, checkout the config\r\nand data files before any change.", "Check Out", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			} else { 
				// a new config file will be created, version history is ask
				AddVersionHistory avh = new AddVersionHistory();
				avh.ShowDialog(this);
				History h = new History();
				h.Author = avh.Author;
				h.Date = DateTime.Now;
				h.Modification = avh.Modification;

				// adding version history
				uiController = new GA.BDC.Core.Web.UI.UIControls.Config.UIController();
				uiController.VersionHistory.HistoryList.Add(h);

				// adding a page title
				uiController.Title.PartnersId.PartnerIdList.Add(GetDefaultPartner());
				FillUIControler();

				/*
				// detects that it is the first time the user uses this component,
				// so we ask for the base file
				if(DialogResult.Yes == MessageBox.Show(this, "You are about to create a page configuration,\r\n" +
					"Select the Web Page (.aspx/.ascx) you want to configure!",
					"Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)) {
					cmdOpenFile_Click(sender, e);
				}*/
			}			
		}

		// creates a node (title) and all the params are child to this node
		private TreeNode CreateTreeNode(string title, params string[] list) {
			TreeNode node = new TreeNode(title);
			for(int i=0;i<list.Length;i++) {
				TreeNode child = new TreeNode(list[i].ToString());
				node.Nodes.Add(child);
			}
			return node;
		}

		// fill version history treeview section
		private void FillHistory() {
			TreeNode nodeH = new TreeNode("Version History");
			int count = 1;
			foreach(History h in uiController.VersionHistory.HistoryList) {
				nodeH.Nodes.Add(CreateTreeNode("Version: " + count.ToString(), h.Author, h.Date.ToShortDateString(), h.Modification));
				count++;
			}
			tvPageAttributes.Nodes.Add(nodeH);
		}

		// fill partner treeview section
		private TreeNode FillPartnerID(PartnersID partnersID) {
			TreeNode nodeP = new TreeNode("Partners ID");
			foreach(PartnerID p in partnersID.PartnerIdList) {
				TreeNode nodeSubPartner = new TreeNode("Partner: " + p.ID);
				foreach(Culture c in p.Cultures.CultureList) {
					nodeSubPartner.Nodes.Add(CreateTreeNode("Culture: " + c.ID, c.Data.Source, c.Data.Parameters.Parameter[0].ToString()));
				}
				nodeP.Nodes.Add(nodeSubPartner);
			}
			return nodeP;
		}

		// fill title treeview section
		private void FillTitle() {
			TreeNode nodePageTitle = new TreeNode("Page Title");
			nodePageTitle.Nodes.Add(FillPartnerID(uiController.Title.PartnersId));
			tvPageAttributes.Nodes.Add(nodePageTitle);
		}

		// fill header/meta tags treeview section
		private void FillHeader() {
			TreeNode nodePageHeader = new TreeNode("Page Header");

			TreeNode node = new TreeNode("Meta Tags");
			foreach(MetaTag mt in uiController.Header.MetaTags.MetaTagList) {
				TreeNode subNode = new TreeNode("Meta Tag: " + mt.Name);
				subNode.Nodes.Add(FillPartnerID(mt.PartnersIds));
				node.Nodes.Add(subNode);
			}

			nodePageHeader.Nodes.Add(node);
			tvPageAttributes.Nodes.Add(nodePageHeader);
		}
		
		// fill all controls of the page (read only)
		private void FillUIControls() {
			TreeNode nodeUIControl = new TreeNode("UI Controls");

			foreach(UIControl uic in uiController.UiControls.ControlList) {
				TreeNode node = new TreeNode("UIControl: " + uic.Name);
				node.Nodes.Add(new TreeNode(uic.Type));
				node.Nodes.Add(FillPartnerID(uic.PartnersID));
				nodeUIControl.Nodes.Add(node);
			}

			tvPageAttributes.Nodes.Add(nodeUIControl);
		}

		// fill controls and page config whitout refreshing
		public void FillUIControler() {
			FillUIControler(false);
		}

		// fille controls and page config and clear the tree view first
		public void FillUIControler(bool erase) {
			if(erase) {
				tvPageAttributes.Nodes.Clear();
			}
			tvPageAttributes.BeginUpdate();
			FillHistory(); 
			FillTitle();
			FillHeader();
			FillUIControls();
			tvPageAttributes.EndUpdate();
		}

		// open the base file, and load it if configuration file exists
		private void cmdOpenFile_Click(object sender, System.EventArgs e) {
			System.Windows.Forms.OpenFileDialog ofd = new
				System.Windows.Forms.OpenFileDialog();
			if(ofd.ShowDialog() == DialogResult.OK) {
				txtASPXFile.Text = ofd.FileName;
				ASPXFilename = ofd.FileName;
				uiController = new GA.BDC.Core.Web.UI.UIControls.Config.UIController();
				if(System.IO.File.Exists(txtASPXFile.Text + ".xml")) {
					// the config file exists, then it loads it
					// uiController.Load(txtASPXFile.Text + ".xml");
					uiController = (Config.UIController) GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromXmlFile(
						txtASPXFile.Text + ".xml", typeof(Config.UIController));
					FillUIControler();
				} else {
					// a new config file will be created, version history is ask
					AddVersionHistory avh = new AddVersionHistory();
					avh.ShowDialog(this);
					History h = new History();
					h.Author = avh.Author;
					h.Date = DateTime.Now;
					h.Modification = avh.Modification;

					// adding version history
					uiController.VersionHistory.HistoryList.Add(h);

					// adding a page title
					uiController.Title.PartnersId.PartnerIdList.Add(GetDefaultPartner());
					FillUIControler();
				}
			}
		}

		// Get the default partner id (used to display data on design time)
		private PartnerID GetDefaultPartner() {
			PartnerID p = new PartnerID();
			p.ID = "Default";

			BaseConfig.GlobalizerConfigs gcs = new BaseConfig.GlobalizerConfigs();
			gcs.LoadXML();

			BaseConfig.GlobalizerConfig gc = gcs.GetCurrentWorkingConfig();
			foreach(BaseConfig.SupportedCulture sc in gc.SupportedCultures.SupportedCultureList) {
				Culture c = new Culture();
				c.ID = sc.Name;
				c.Data.Source = "Binary File";
				c.Data.Parameters.Parameter.Add("");
				p.Cultures.CultureList.Add(c);
			}
			return p;
		}

		// Get the root of the tree view
		private string GetRootNodeText(TreeNode node) {
			while(node.Parent != null) {
				node = node.Parent;
			}
			return node.Text;
		}

		// show the split user control
		private void ShowSplit(string message) {
			GA.BDC.Core.Web.UI.UIControls.Controls.Split split =
				new GA.BDC.Core.Web.UI.UIControls.Controls.Split();
			split.Message = message;
			mainPanel.Controls.Clear();
			mainPanel.Controls.Add(split);
			split.Dock = DockStyle.Fill;
		}

		// show the partner user control
		private void ShowPartners(PartnersID _partnersID) {
			GA.BDC.Core.Web.UI.UIControls.Controls.Partners partners =
				new GA.BDC.Core.Web.UI.UIControls.Controls.Partners(this);
			// by setting the partners id, the control Controls.Partners will
			// arrange by itself the data
			partners.SetPartnersID(ref _partnersID);
			mainPanel.Controls.Clear();
			mainPanel.Controls.Add(partners);
			partners.Dock = DockStyle.Fill;
		}

		// arise when a selection have been made on the tree view
		private void tvPageAttributes_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			string root = GetRootNodeText(tvPageAttributes.SelectedNode);
			switch(root) {
				case "Version History":
					ShowSplit("Page Configuration Version History");
					break;
				case "Page Title":
					ShowPartners(uiController.Title.PartnersId);
					break;
				case "Page Header":
					string metaTagID = GetID(tvPageAttributes.SelectedNode, "Meta Tag: ");
					foreach(MetaTag m in uiController.Header.MetaTags.MetaTagList) {
						if(m.Name == metaTagID) {
							ShowPartners(m.PartnersIds);
							break;
						}
					}
					break;
				case "UI Controls":
					string controlID = GetID(tvPageAttributes.SelectedNode, "UIControl: ");
					foreach(UIControl uic in uiController.UiControls.ControlList) {
						if(controlID == uic.ID) {
							ShowPartners(uic.PartnersID);
							break;
						}
					}
					break;
			}
		}

		// save the configuration file
		private void cmdSave_Click(object sender, System.EventArgs e) {
			try {

//				// delete the backup file if exists
//				if(System.IO.File.Exists(ASPXFilename + ".xml.bak")) {
//					System.IO.File.Delete(ASPXFilename + ".xml.bak");
//				}
//				// backup the current configuration
//				if(System.IO.File.Exists(ASPXFilename + ".xml")) {
//					System.IO.File.Move(ASPXFilename + ".xml", ASPXFilename + ".xml.bak");
//				}

				try {
					System.IO.File.Delete(ASPXFilename + ".xml");
				} catch(System.Exception ex) {
					System.Windows.Forms.MessageBox.Show("Unable to delete file: " + ex.Message);
					return;
				}


				// uicontrol generates the xml string
				// Utils.SaveFile(ASPXFilename + ".xml", uiController.GenerateXML(ASPXFilename));
				uiController.Save(ASPXFilename + ".xml", ASPXFilename);
				// GA.BDC.Core.Xml.Serialization.Serializer.SaveObjectToXmlFile(ASPXFilename + ".xml", uiController);
				//uiController = (Config.UIController) GA.BDC.Core.Xml.Serialization.Serializer.GetObjectFromXmlFile(
				//	ASPXFilename + ".xml", typeof(Config.UIController));
				// GA.BDC.Core.Utilities.IO.FileHandler.CreateNewFile(ASPXFilename + ".xml", uiController.GenerateXML(ASPXFilename));
				Close();
			} catch(System.Exception ex) {
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// go through the tree view nodes and try to find the node that is titled like prefix
		// it goes up, which means, from the leaves to the root
		private TreeNode GetNamedNode(TreeNode node, string prefix) {
			while(!node.Text.StartsWith(prefix)) {
				if(node.Parent == null) {
					return null;
				}
				node = node.Parent;
			}
			return node;
		}

		// some node title are "Meta Tag 1", so we find Meta Tag and replace if by nothing,
		// so we get 1.
		private string GetID(TreeNode node, string prefix) {
			TreeNode n = GetNamedNode(node, prefix);
			if(n != null) {
				return GetNamedNode(node, prefix).Text.Replace(prefix, "");
			} else {
				return "";
			}
		}

		// Delete popup from tree view
		private void menuItem5_Click(object sender, System.EventArgs e) {
			if(tvPageAttributes.SelectedNode != null) {
				string root = GetRootNodeText(tvPageAttributes.SelectedNode);
				switch(root) {
					case "Version History":
						string versionID = GetID(tvPageAttributes.SelectedNode, "Version: ");
						// version id is automatically incremented by the software, not attached
						// to the configuration file.  so version 1 will be version[0] of the array.
						if(versionID != "") {
							uiController.VersionHistory.HistoryList.RemoveAt(int.Parse(versionID) - 1);
							tvPageAttributes.Nodes.Clear();
							FillUIControler();
						} else {
							MessageBox.Show(this, "You can't delete all versions");
						}
						break;
					case "Page Title":
						if(tvPageAttributes.SelectedNode.Text.StartsWith("Partners ID")) {
							// override the title.partnerid if partnerid is selected
							uiController.Title.PartnersId = new PartnersID();
							FillUIControler(true);
						} else {
							string partnerID = GetID(tvPageAttributes.SelectedNode, "Partner: ");
							string cultureID = GetID(tvPageAttributes.SelectedNode, "Culture: ");
							
							foreach(PartnerID p in uiController.Title.PartnersId.PartnerIdList) {
								if(p.ID == partnerID) {
									if(cultureID != "") {
										foreach(Culture c in p.Cultures.CultureList) {
											if(cultureID == c.ID) {
												// find the to delete culture and remove it
												p.Cultures.CultureList.Remove(c);
												FillUIControler(true);
												break;
											}
										}
									} else {
										// find the to delete partner and remove it
										uiController.Title.PartnersId.PartnerIdList.Remove(p);
										FillUIControler(true);
										break;
									}
								}
							}
						}
						break;
					case "Page Header":
						if(tvPageAttributes.SelectedNode.Text.StartsWith("Partners ID")) {
							// can't delete the whole partner id
							FillUIControler(true);
						} else if(tvPageAttributes.SelectedNode.Text.StartsWith("Meta Tag: ")) {
							string metaTagID = GetID(tvPageAttributes.SelectedNode, "Meta Tag: ");
							// Meta Tag: 1 ==> MetaTags.MetaTagList[0];
							MetaTag t = (MetaTag)uiController.Header.MetaTags.MetaTagList[int.Parse(metaTagID)-1];
							if(t.Name == metaTagID) {
								uiController.Header.MetaTags.MetaTagList.Remove(t);
								FillUIControler(true);
							}
						} else if(tvPageAttributes.SelectedNode.Text.StartsWith("Partner: ")) {
							string metaTagID = GetID(tvPageAttributes.SelectedNode, "Meta Tag: ");
							string partnerID = GetID(tvPageAttributes.SelectedNode, "Partner: ");

							for(int i=0;i<uiController.Header.MetaTags.MetaTagList.Count;i++) {
								MetaTag t = (MetaTag)uiController.Header.MetaTags.MetaTagList[i];
								if(t.Name == metaTagID) {
									for(int j=0;j<t.PartnersIds.PartnerIdList.Count;j++) {
										PartnerID pid = (PartnerID)t.PartnersIds.PartnerIdList[j];
										if(pid.ID == partnerID) {
											t.PartnersIds.PartnerIdList.Remove(pid);
											FillUIControler(true);
										}
									}
								}
							}
						} else if(tvPageAttributes.SelectedNode.Text.StartsWith("Culture: ")) {
							string metaTagID = GetID(tvPageAttributes.SelectedNode, "Meta Tag: ");
							string partnerID = GetID(tvPageAttributes.SelectedNode, "Partner: ");
							string culture = GetID(tvPageAttributes.SelectedNode, "Culture: ");

							for(int i=0;i<uiController.Header.MetaTags.MetaTagList.Count;i++) {
								MetaTag t = (MetaTag)uiController.Header.MetaTags.MetaTagList[i];
								if(t.Name == metaTagID) {
									for(int j=0;j<t.PartnersIds.PartnerIdList.Count;j++) {
										PartnerID pid = (PartnerID)t.PartnersIds.PartnerIdList[j];
										if(pid.ID == partnerID) {
											for(int k=0;k<pid.Cultures.CultureList.Count;k++) {
												Culture c = (Culture)pid.Cultures.CultureList[k];
												if(c.ID == culture) {
													pid.Cultures.CultureList.Remove(c);
													FillUIControler(true);
													break;
												}
											}
										}
									}
								}
							}
						}
						break;
					case "UI Controls":
						// read only, can't delete
						break;
				}
			}
		}

		// add version history
		private void menuItem1_Click(object sender, System.EventArgs e) {
			History h = new History();
			AddVersionHistory vh = new AddVersionHistory();
			vh.ShowDialog(this);
			h.Author = vh.Author;
			h.Date = DateTime.Now;
			h.Modification = vh.Modification;
			uiController.VersionHistory.HistoryList.Add(h);
			tvPageAttributes.Nodes.Clear();
			FillUIControler();
		}

		// Add partner id
		private void menuItem2_Click(object sender, System.EventArgs e) {
			if(tvPageAttributes.SelectedNode != null) {
				string root = GetRootNodeText(tvPageAttributes.SelectedNode);
				switch(root) {
					case "Version History":
						MessageBox.Show(this, "You can't add a partner id into version history");
						break;
					case "Page Title":
						GA.BDC.Core.Web.UI.UIControls.Config.PartnerID p = 
							new GA.BDC.Core.Web.UI.UIControls.Config.PartnerID();
						InputBox inputBox = new InputBox();
						inputBox.LblMessage.Text = "Insert new partner id";
						inputBox.ShowDialog(this);
						if(inputBox.Ok) {
							p.ID = inputBox.TxtInput.Text;
							BaseConfig.GlobalizerConfigs gcs = new BaseConfig.GlobalizerConfigs();
							gcs.LoadXML();
							BaseConfig.GlobalizerConfig gc = gcs.GetCurrentWorkingConfig();
							foreach(BaseConfig.SupportedCulture sc in gc.SupportedCultures.SupportedCultureList) {
								Culture c = new Culture();
								c.ID = sc.Name;
								c.Data.Source = "Binary File";
								c.Data.Parameters.Parameter.Add("");
								p.Cultures.CultureList.Add(c);
							}
							uiController.Title.PartnersId.PartnerIdList.Add(p);
							FillUIControler(true);
						}
						break;
					case "Page Header":
						string metaTagID = GetID(tvPageAttributes.SelectedNode, "Meta Tag: ");
						bool found = false;
						foreach(MetaTag m in uiController.Header.MetaTags.MetaTagList) {
							if(m.Name == metaTagID) {
								GA.BDC.Core.Web.UI.UIControls.Config.PartnerID pr =
									new GA.BDC.Core.Web.UI.UIControls.Config.PartnerID();
								inputBox = new InputBox();
								inputBox.LblMessage.Text = "Insert new partner id";
								inputBox.ShowDialog(this);
								if(inputBox.Ok) {
									pr.ID = inputBox.TxtInput.Text;
									BaseConfig.GlobalizerConfigs gcs = new BaseConfig.GlobalizerConfigs();
									gcs.LoadXML();
									BaseConfig.GlobalizerConfig gc = gcs.GetCurrentWorkingConfig();
									foreach(BaseConfig.SupportedCulture sc in gc.SupportedCultures.SupportedCultureList) {
										Culture c = new Culture();
										c.ID = sc.Name;
										c.Data.Source = "Binary File";
										c.Data.Parameters.Parameter.Add("");
										pr.Cultures.CultureList.Add(c);
									}
									m.PartnersIds.PartnerIdList.Add(pr);
									FillUIControler(true);
								}
								found = true;
								break;
							}
						}
						if(!found) {
							MessageBox.Show(this, "You must select a meta tag first!");
						}
						break;
					case "UI Controls":
						break;
				}
			}
		}

		// add culture
		private void menuItem3_Click(object sender, System.EventArgs e) {
			if(tvPageAttributes.SelectedNode != null) {
				string root = GetRootNodeText(tvPageAttributes.SelectedNode);
				switch(root) {
					case "Version History":
						MessageBox.Show(this, "You can't add a culture into version history");
						break;
					case "Page Title":
						string partnerID = GetID(tvPageAttributes.SelectedNode, "Partner: ");
						bool found = false;
						foreach(GA.BDC.Core.Web.UI.UIControls.Config.PartnerID p in uiController.Title.PartnersId.PartnerIdList) {
							if(p.ID == partnerID) {
								GA.BDC.Core.Web.UI.UIControls.Config.Culture c = 
									new GA.BDC.Core.Web.UI.UIControls.Config.Culture();
								InputBox inputBox = new InputBox();
								inputBox.LblMessage.Text = "Insert new culture code";
								inputBox.ShowDialog(this);
								if(inputBox.Ok) {
									c.ID = inputBox.TxtInput.Text;
									c.Data.Source = "Binary File";
									c.Data.Parameters.Parameter.Add("");
									p.Cultures.CultureList.Add(c);
									FillUIControler(true);
								}
								found = true;
							}
						}
						if(!found) {
							MessageBox.Show("You must select a partner");
						}
						break;
					case "Page Header":
						string metaTagID = GetID(tvPageAttributes.SelectedNode, "Meta Tag: ");
						partnerID = GetID(tvPageAttributes.SelectedNode, "Partner: ");

						found = false;
						foreach(GA.BDC.Core.Web.UI.UIControls.Config.MetaTag m in uiController.Header.MetaTags.MetaTagList) {
							if(m.Name == metaTagID) {
								foreach(GA.BDC.Core.Web.UI.UIControls.Config.PartnerID p in m.PartnersIds.PartnerIdList) {
									if(p.ID == partnerID) {
										GA.BDC.Core.Web.UI.UIControls.Config.Culture c = 
											new GA.BDC.Core.Web.UI.UIControls.Config.Culture();
										InputBox inputBox = new InputBox();
										inputBox.LblMessage.Text = "Insert new culture code";
										inputBox.ShowDialog(this);
										if(inputBox.Ok) {
											c.ID = inputBox.TxtInput.Text;
											c.Data.Source = "Binary File";
											c.Data.Parameters.Parameter.Add("");
											p.Cultures.CultureList.Add(c);
											tvPageAttributes.Nodes.Clear();
											FillUIControler();
										}
										found = true;
									}
								}
								if(!found) {
									MessageBox.Show("You must select a partner");
								}
							}
						}
						break;
					case "UI Controls":
						break;
				}
			}
		}

		// add meta tag
		private void menuItem4_Click(object sender, System.EventArgs e) {
			if(tvPageAttributes.SelectedNode != null) {
				string root = GetRootNodeText(tvPageAttributes.SelectedNode);
				switch(root) {
					case "Version History":
						MessageBox.Show(this, "You can't add a meta tag into version history");
						break;
					case "Page Title":
						MessageBox.Show(this, "You can't add a meta tag into the page title");
						break;
					case "Page Header":
						MetaTag newMetaTag = new MetaTag();

						InputBox inputBox = new InputBox();
						inputBox.LblMessage.Text = "Insert Meta Tag Name";
						inputBox.ShowDialog(this);
						if(inputBox.Ok) {
							newMetaTag.Name = inputBox.TxtInput.Text;
							uiController.Header.MetaTags.MetaTagList.Add(newMetaTag);
							FillUIControler(true);
						}
						break;
					case "UI Controls":
						MessageBox.Show(this, "You can't add a meta tag into UI Controls");
						break;
				}
			}
		}

		private void menuItem6_Click(object sender, System.EventArgs e) {
			FillUIControler(true);
		}

		private void PageBuilder_Closed(object sender, System.EventArgs e) {
			Application.Exit();
		}

		private void groupBox1_Enter(object sender, System.EventArgs e) {
		
		}

		private void cmdChangeProjectSettings_Click(object sender, System.EventArgs e) {
			// loads the configuration for the globalization project settings
			// check for more info -> c:\GlobalizerConfig\GlobalizerConfig.xml
			ChangeProjectSettings cpss = new ChangeProjectSettings();
			cpss.ShowDialog(this);
		}

		private void menuItem7_Click(object sender, System.EventArgs e) {
			// insert a new ui control just to hold data (eg. error messages, etc..)

			// get the control name
			string controlName = "";
			InputBox ib = new InputBox();
			ib.LblMessage.Text = "Insert key name (Must be unique)";
			ib.ShowDialog(this);
			if(ib.Ok) {
				controlName = ib.TxtInput.Text;
			} else {
				return;
			}

			UIControl uic = new UIControl();
			uic.ID = controlName;
			uic.Name = controlName;
			uic.Type = "Hidden";
			BaseConfig.GlobalizerConfigs gcs = 
				new BaseConfig.GlobalizerConfigs();
			gcs.LoadXML();
			BaseConfig.GlobalizerConfig gc = gcs.GetCurrentWorkingConfig();

			PartnerID pid = new PartnerID();
			pid.ID = "Default";
			foreach(BaseConfig.SupportedCulture sc in gc.SupportedCultures.SupportedCultureList) {
				Config.Culture c = new Config.Culture();
				c.ID = sc.Name;
				c.Data.Source = "Binary File";
				c.Data.Parameters.Parameter.Add("");
				pid.Cultures.CultureList.Add(c);
			}
			uic.PartnersID.PartnerIdList.Add(pid);
			uiController.UiControls.ControlList.Add(uic);
			FillUIControler(true);
		}

		#region Attributes
		public string ASPXFilename {
			set { aspxFilename = value; }
			get { return aspxFilename; }
		}
		#endregion
	}
}
