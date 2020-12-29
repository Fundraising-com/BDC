using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using GA.BDC.Core.Web.UI.UIControls.BaseConfig;

namespace GA.BDC.Core.Web.UI.UIControls
{
	/// <summary>
	/// Summary description for ChangeProjectSettings.
	/// </summary>
	public class ChangeProjectSettings : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListView tvProjects;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button cmdSave;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboCurrentProject;
		private System.Windows.Forms.ComboBox cboDebugMode;
		private System.Windows.Forms.ComboBox cboDefaultCulture;
		private System.Windows.Forms.TextBox txtDefaultPartner;
		private System.Windows.Forms.Button cmdOpenFile;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtID;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ChangeProjectSettings()
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
			this.tvProjects = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cmdSave = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.cboCurrentProject = new System.Windows.Forms.ComboBox();
			this.cboDebugMode = new System.Windows.Forms.ComboBox();
			this.cboDefaultCulture = new System.Windows.Forms.ComboBox();
			this.txtDefaultPartner = new System.Windows.Forms.TextBox();
			this.cmdOpenFile = new System.Windows.Forms.Button();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtID = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tvProjects
			// 
			this.tvProjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.columnHeader1,
																						 this.columnHeader2,
																						 this.columnHeader3});
			this.tvProjects.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvProjects.LabelWrap = false;
			this.tvProjects.Location = new System.Drawing.Point(0, 0);
			this.tvProjects.MultiSelect = false;
			this.tvProjects.Name = "tvProjects";
			this.tvProjects.Size = new System.Drawing.Size(192, 269);
			this.tvProjects.TabIndex = 0;
			this.tvProjects.View = System.Windows.Forms.View.List;
			this.tvProjects.SelectedIndexChanged += new System.EventHandler(this.tvProjects_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "WP";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Filename";
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(192, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 269);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cmdSave);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.cboCurrentProject);
			this.panel1.Controls.Add(this.cboDebugMode);
			this.panel1.Controls.Add(this.cboDefaultCulture);
			this.panel1.Controls.Add(this.txtDefaultPartner);
			this.panel1.Controls.Add(this.cmdOpenFile);
			this.panel1.Controls.Add(this.txtFileName);
			this.panel1.Controls.Add(this.txtName);
			this.panel1.Controls.Add(this.txtID);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(195, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(341, 269);
			this.panel1.TabIndex = 2;
			// 
			// cmdSave
			// 
			this.cmdSave.Location = new System.Drawing.Point(224, 232);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.TabIndex = 53;
			this.cmdSave.Text = "Save";
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(280, 23);
			this.label2.TabIndex = 52;
			this.label2.Text = "Projects Configuration";
			// 
			// cboCurrentProject
			// 
			this.cboCurrentProject.Location = new System.Drawing.Point(192, 104);
			this.cboCurrentProject.Name = "cboCurrentProject";
			this.cboCurrentProject.Size = new System.Drawing.Size(104, 21);
			this.cboCurrentProject.TabIndex = 51;
			// 
			// cboDebugMode
			// 
			this.cboDebugMode.Location = new System.Drawing.Point(192, 152);
			this.cboDebugMode.Name = "cboDebugMode";
			this.cboDebugMode.Size = new System.Drawing.Size(104, 21);
			this.cboDebugMode.TabIndex = 50;
			// 
			// cboDefaultCulture
			// 
			this.cboDefaultCulture.Location = new System.Drawing.Point(192, 200);
			this.cboDefaultCulture.Name = "cboDefaultCulture";
			this.cboDefaultCulture.Size = new System.Drawing.Size(104, 21);
			this.cboDefaultCulture.TabIndex = 49;
			// 
			// txtDefaultPartner
			// 
			this.txtDefaultPartner.Location = new System.Drawing.Point(192, 176);
			this.txtDefaultPartner.Name = "txtDefaultPartner";
			this.txtDefaultPartner.Size = new System.Drawing.Size(104, 20);
			this.txtDefaultPartner.TabIndex = 48;
			this.txtDefaultPartner.Text = "";
			// 
			// cmdOpenFile
			// 
			this.cmdOpenFile.Location = new System.Drawing.Point(304, 128);
			this.cmdOpenFile.Name = "cmdOpenFile";
			this.cmdOpenFile.Size = new System.Drawing.Size(24, 23);
			this.cmdOpenFile.TabIndex = 47;
			this.cmdOpenFile.Text = "...";
			this.cmdOpenFile.Click += new System.EventHandler(this.cmdOpenFile_Click);
			// 
			// txtFileName
			// 
			this.txtFileName.Location = new System.Drawing.Point(192, 128);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(104, 20);
			this.txtFileName.TabIndex = 46;
			this.txtFileName.Text = "";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(192, 80);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(104, 20);
			this.txtName.TabIndex = 45;
			this.txtName.Text = "";
			// 
			// txtID
			// 
			this.txtID.Location = new System.Drawing.Point(192, 56);
			this.txtID.Name = "txtID";
			this.txtID.Size = new System.Drawing.Size(104, 20);
			this.txtID.TabIndex = 44;
			this.txtID.Text = "";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(16, 80);
			this.label12.Name = "label12";
			this.label12.TabIndex = 43;
			this.label12.Text = "Name";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(16, 104);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(136, 23);
			this.label11.TabIndex = 42;
			this.label11.Text = "Current Working Project:";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(16, 128);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(128, 23);
			this.label10.TabIndex = 41;
			this.label10.Text = "Base Project File Name:";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 152);
			this.label9.Name = "label9";
			this.label9.TabIndex = 40;
			this.label9.Text = "Debug Mode:";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 176);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(168, 23);
			this.label8.TabIndex = 39;
			this.label8.Text = " Design-time default partner id:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 200);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(144, 23);
			this.label7.TabIndex = 38;
			this.label7.Text = "Design-time default culture:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 56);
			this.label1.Name = "label1";
			this.label1.TabIndex = 37;
			this.label1.Text = "ID";
			// 
			// ChangeProjectSettings
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(536, 269);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.tvProjects);
			this.Name = "ChangeProjectSettings";
			this.Text = "ChangeProjectSettings";
			this.Load += new System.EventHandler(this.ChangeProjectSettings_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FillProject(string projectName) {
			GlobalizerConfigs gcs = new GlobalizerConfigs();
			gcs.LoadXML();
			foreach(GlobalizerConfig gc in gcs.GlobalizerConfigList) {
				if(gc.Name == projectName) {
					txtID.Text = gc.ID;
					txtName.Text = gc.Name;
					cboCurrentProject.Items.Clear();
					cboCurrentProject.Items.Add("True");
					cboCurrentProject.Items.Add("False");
					cboCurrentProject.Text = gc.CurrentWorkingProject;
					txtFileName.Text = gc.BaseProjectFileName;
					cboDebugMode.Items.Clear();
					cboDebugMode.Items.Add("True");
					cboDebugMode.Items.Add("False");
					cboDebugMode.Text = gc.Debug;
					txtDefaultPartner.Text = gc.DesignDefaultPartnerID;
					cboDefaultCulture.Items.Clear();
					foreach(BaseConfig.SupportedCulture sc in gc.SupportedCultures.SupportedCultureList) {
						cboDefaultCulture.Items.Add(sc.Name);
					}
					cboDefaultCulture.Text = gc.DesignDefaultCulture;
				}
			}
		}

		private void ChangeProjectSettings_Load(object sender, System.EventArgs e) {
			GlobalizerConfigs gcs = new GlobalizerConfigs();
			gcs.LoadXML();
			foreach(GlobalizerConfig gc in gcs.GlobalizerConfigList) {
				string[] s = new string[3];
				s[0] = gc.Name;
				s[1] = gc.CurrentWorkingProject;
				s[2] = gc.BaseProjectFileName;
				tvProjects.Items.Add(new ListViewItem(s));				
			}
		}

		private void tvProjects_SelectedIndexChanged(object sender, System.EventArgs e) {
			FillProject(tvProjects.SelectedItems[0].Text);
		}

		private void cmdSave_Click(object sender, System.EventArgs e) {
			GlobalizerConfigs gcs = new GlobalizerConfigs();
			gcs.LoadXML();
			bool setFalse = false;
			foreach(GlobalizerConfig gc in gcs.GlobalizerConfigList) {
				if(gc.Name == tvProjects.SelectedItems[0].Text) {
					gc.ID = txtID.Text;
					gc.Name = txtName.Text;
					gc.CurrentWorkingProject = cboCurrentProject.Text;
					if(cboCurrentProject.Text.ToLower() == "true") {
						setFalse = true;
					}
					gc.BaseProjectFileName = txtFileName.Text;
					gc.Debug = cboDebugMode.Text;
					gc.DesignDefaultPartnerID = txtDefaultPartner.Text;
					gc.DesignDefaultCulture = cboDefaultCulture.Text;
				}
			}
			
			foreach(GlobalizerConfig gc in gcs.GlobalizerConfigList) {
				if(gc.Name != tvProjects.SelectedItems[0].Text) {
					gc.CurrentWorkingProject = "False";
				}
			}

			if(!gcs.Save()) {
				MessageBox.Show(this, "Error! Unable to save the file!");
				return;
			} else {
				Close();
			}
		}

		private void cmdOpenFile_Click(object sender, System.EventArgs e) {
			MessageBox.Show(this, "Select the project file.", "Select File", MessageBoxButtons.OK,
				MessageBoxIcon.Information);
			OpenFileDialog ofd = new OpenFileDialog();
			if(DialogResult.OK == ofd.ShowDialog(this)) {
				txtFileName.Text = System.IO.Path.GetDirectoryName(ofd.FileName) + "\\";
			}
		}
	}
}
