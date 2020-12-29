using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GA.BDC.Core.Web.UI.UIControls.Preview
{
	/// <summary>
	/// Summary description for Link.
	/// </summary>
	public class Link : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.LinkLabel linkLabel1;
		private string previewContent;
		private System.Windows.Forms.GroupBox groupBox1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Link(string _previewContent)
		{
			previewContent = _previewContent;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(16, 64);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(128, 23);
			this.linkLabel1.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.linkLabel1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(150, 150);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// Link
			// 
			this.Controls.Add(this.groupBox1);
			this.Name = "Link";
			this.Load += new System.EventHandler(this.Link_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void Link_Load(object sender, System.EventArgs e) {
			if(previewContent != "") {
				linkLabel1.Text = previewContent;
			}
		}
	}
}
