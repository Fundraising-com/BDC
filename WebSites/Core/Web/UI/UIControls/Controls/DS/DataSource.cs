using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GA.BDC.Core.Web.UI.UIControls.Controls.DS
{
	/// <summary>
	/// Summary description for DataSource.
	/// </summary>
	public class DataSource : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox txtDataParam;
		private GA.BDC.Core.Web.UI.UIControls.Config.Data data;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.CheckBox checkBox6;
		private GA.BDC.Core.Web.UI.UIControls.Controls.Partners parent;
		private System.Windows.Forms.LinkLabel linkURL;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataSource(GA.BDC.Core.Web.UI.UIControls.Controls.Partners _parent) {
			parent = _parent;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.linkURL = new System.Windows.Forms.LinkLabel();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.txtDataParam = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.lblMessage = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.linkURL);
			this.groupBox1.Controls.Add(this.checkBox6);
			this.groupBox1.Controls.Add(this.checkBox5);
			this.groupBox1.Controls.Add(this.checkBox4);
			this.groupBox1.Controls.Add(this.checkBox3);
			this.groupBox1.Controls.Add(this.checkBox2);
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(96, 232);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// linkURL
			// 
			this.linkURL.Location = new System.Drawing.Point(16, 160);
			this.linkURL.Name = "linkURL";
			this.linkURL.Size = new System.Drawing.Size(72, 16);
			this.linkURL.TabIndex = 11;
			this.linkURL.TabStop = true;
			this.linkURL.Text = "Relative Path";
			this.linkURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkURL_LinkClicked);
			// 
			// checkBox6
			// 
			this.checkBox6.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox6.Location = new System.Drawing.Point(16, 136);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(72, 24);
			this.checkBox6.TabIndex = 10;
			this.checkBox6.Text = "URL";
			this.checkBox6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
			// 
			// checkBox5
			// 
			this.checkBox5.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox5.Location = new System.Drawing.Point(16, 112);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(72, 24);
			this.checkBox5.TabIndex = 9;
			this.checkBox5.Text = "Database";
			this.checkBox5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
			// 
			// checkBox4
			// 
			this.checkBox4.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox4.Location = new System.Drawing.Point(16, 88);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(72, 24);
			this.checkBox4.TabIndex = 8;
			this.checkBox4.Text = "Resource";
			this.checkBox4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
			// 
			// checkBox3
			// 
			this.checkBox3.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox3.Location = new System.Drawing.Point(16, 64);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(72, 24);
			this.checkBox3.TabIndex = 7;
			this.checkBox3.Text = "Binary File";
			this.checkBox3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
			// 
			// checkBox2
			// 
			this.checkBox2.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox2.Location = new System.Drawing.Point(16, 40);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(72, 24);
			this.checkBox2.TabIndex = 6;
			this.checkBox2.Text = "Text File";
			this.checkBox2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// checkBox1
			// 
			this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox1.Location = new System.Drawing.Point(16, 16);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(72, 24);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "Inner";
			this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.groupBox4);
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(96, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(368, 232);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.txtDataParam);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox4.Location = new System.Drawing.Point(3, 48);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(362, 181);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			// 
			// txtDataParam
			// 
			this.txtDataParam.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDataParam.Location = new System.Drawing.Point(3, 16);
			this.txtDataParam.Name = "txtDataParam";
			this.txtDataParam.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtDataParam.Size = new System.Drawing.Size(356, 20);
			this.txtDataParam.TabIndex = 1;
			this.txtDataParam.Text = "";
			this.txtDataParam.Visible = false;
			this.txtDataParam.TextChanged += new System.EventHandler(this.txtDataParam_TextChanged);
			this.txtDataParam.Leave += new System.EventHandler(this.txtDataParam_Leave);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.lblMessage);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox3.Location = new System.Drawing.Point(3, 16);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(362, 32);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			// 
			// lblMessage
			// 
			this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblMessage.Location = new System.Drawing.Point(3, 16);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(356, 13);
			this.lblMessage.TabIndex = 1;
			this.lblMessage.Text = "Select a Data Source";
			// 
			// DataSource
			// 
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "DataSource";
			this.Size = new System.Drawing.Size(464, 232);
			this.Load += new System.EventHandler(this.DataSource_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdInner_Click(object sender, System.EventArgs e) {
			lblMessage.Text = "Insert your one line text here.";
			txtDataParam.Multiline = false;
			txtDataParam.Visible = true;
		}

		private void cmdFile_Click(object sender, System.EventArgs e) {
			lblMessage.Text = "Insert your text here.";
			txtDataParam.Multiline = true;
			txtDataParam.Visible = true;
		}

		private void cmdBin_Click(object sender, System.EventArgs e) {
			lblMessage.Text = "Insert your text here.";
			txtDataParam.Multiline = true;
			txtDataParam.Visible = true;
		}

		private void cmdResource_Click(object sender, System.EventArgs e) {
			lblMessage.Text = "Insert resource key.";
			txtDataParam.Multiline = false;
			txtDataParam.Visible = true;
		}

		private void cmdDatabase_Click(object sender, System.EventArgs e) {
			lblMessage.Text = "Insert select query here.";
			txtDataParam.Multiline = false;
			txtDataParam.Visible = true;
		}

		private void cmdURL_Click(object sender, System.EventArgs e) {
			lblMessage.Text = "Insert URL.";
			txtDataParam.Multiline = false;
			txtDataParam.Visible = true;
		}

		private void ShowData() {
			switch(data.Source) {
				case "Inner Text":
					txtDataParam.Text = data.Parameters.Parameter[0].ToString();
					txtDataParam.Visible = true;
					UnselectAllButton(checkBox1);
					txtDataParam.Multiline = false;
					txtDataParam.Visible = true;
					break;
				case "Text File":
					txtDataParam.Text = data.Parameters.Parameter[0].ToString().Replace("<br>", "\r\n");
					txtDataParam.Visible = true;
					UnselectAllButton(checkBox2);
					txtDataParam.Multiline = true;
					txtDataParam.Visible = true;
					break;
				case "Binary File":
					txtDataParam.Text = data.Parameters.Parameter[0].ToString().Replace("<br>", "\r\n");
					txtDataParam.Visible = true;
					UnselectAllButton(checkBox3);
					txtDataParam.Multiline = true;
					txtDataParam.Visible = true;
					break;
				case "Database":
					txtDataParam.Text = data.Parameters.Parameter[0].ToString();
					txtDataParam.Visible = true;
					UnselectAllButton(checkBox4);
					txtDataParam.Multiline = false;
					txtDataParam.Visible = true;
					break;
				case "Resource":
					txtDataParam.Text = data.Parameters.Parameter[0].ToString();
					txtDataParam.Visible = true;
					UnselectAllButton(checkBox5);
					txtDataParam.Multiline = false;
					txtDataParam.Visible = true;
					break;
				case "URL":
					txtDataParam.Text = data.Parameters.Parameter[0].ToString();
					txtDataParam.Visible = true;
					UnselectAllButton(checkBox6);
					txtDataParam.Multiline = false;
					txtDataParam.Visible = true;
					break;
			}
		}

		private void UnselectAllButton(System.Windows.Forms.CheckBox chk) {
			if(chk != checkBox1) {
				checkBox1.CheckState = CheckState.Unchecked;
				checkBox1.BackColor = Color.Transparent;
			}
			if(chk != checkBox2) {
				checkBox2.CheckState = CheckState.Unchecked;
				checkBox2.BackColor = Color.Transparent;
			}
			if(chk != checkBox3) {
				checkBox3.CheckState = CheckState.Unchecked;
				checkBox3.BackColor = Color.Transparent;
			}
			if(chk != checkBox4) {
				checkBox4.CheckState = CheckState.Unchecked;
				checkBox4.BackColor = Color.Transparent;
			}
			if(chk != checkBox5) {
				checkBox5.CheckState = CheckState.Unchecked;
				checkBox5.BackColor = Color.Transparent;
			}
			if(chk != checkBox6) {
				checkBox6.CheckState = CheckState.Unchecked;
				checkBox6.BackColor = Color.Transparent;
			}
			chk.BackColor = Color.LightGray;
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e) {
			UnselectAllButton(checkBox1);
			data.Source = "Inner Text";
			cmdInner_Click(sender, e);
		}

		private void checkBox2_CheckedChanged(object sender, System.EventArgs e) {
			UnselectAllButton(checkBox2);
			data.Source = "Text File";
			cmdFile_Click(sender, e);
		}

		private void checkBox3_CheckedChanged(object sender, System.EventArgs e) {
			UnselectAllButton(checkBox3);
			data.Source = "Binary File";
			cmdBin_Click(sender, e);
		}

		private void checkBox4_CheckedChanged(object sender, System.EventArgs e) {
			UnselectAllButton(checkBox4);
			data.Source = "Resource";
			cmdResource_Click(sender, e);
		}

		private void checkBox5_CheckedChanged(object sender, System.EventArgs e) {
			UnselectAllButton(checkBox5);
			data.Source = "Database";
			cmdDatabase_Click(sender, e);
		}

		private void checkBox6_CheckedChanged(object sender, System.EventArgs e) {
			UnselectAllButton(checkBox6);
			data.Source = "URL";
			cmdURL_Click(sender, e);
		}

		private void txtDataParam_TextChanged(object sender, System.EventArgs e) {
			// data.Parameters.Parameter[0] = System.Web.HttpUtility.HtmlEncode(txtDataParam.Text.Replace("\r\n", "<br>"));
			data.Parameters.Parameter[0] = txtDataParam.Text.Replace("\r\n", "<br>");
		}

		private void txtDataParam_Leave(object sender, System.EventArgs e) {

		}

		private void DataSource_Load(object sender, System.EventArgs e) {
			
		}

		private void linkURL_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
			if(DialogResult.OK == ofd.ShowDialog(this)) {
				txtDataParam.Text = ofd.FileName.Replace(BaseConfig.GlobalizerConfigs.GetBasePath(), "").Replace("\\", "/");
				
			}
		}

		#region Attributes
		public GA.BDC.Core.Web.UI.UIControls.Config.Data Data {
			set { data = value; ShowData(); }
			get { return data; }
		}
		#endregion
	}
}
