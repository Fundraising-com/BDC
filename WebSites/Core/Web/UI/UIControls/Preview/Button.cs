using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GA.BDC.Core.Web.UI.UIControls.Preview
{
	/// <summary>
	/// Summary description for Button.
	/// </summary>
	public class Button : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Button cmdButton;
		private string previewContent;
		private System.Windows.Forms.GroupBox groupBox1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Button(string _previewContent)
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
			this.cmdButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdButton
			// 
			this.cmdButton.Location = new System.Drawing.Point(16, 48);
			this.cmdButton.Name = "cmdButton";
			this.cmdButton.Size = new System.Drawing.Size(104, 23);
			this.cmdButton.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmdButton);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(136, 120);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// Button
			// 
			this.Controls.Add(this.groupBox1);
			this.Name = "Button";
			this.Size = new System.Drawing.Size(136, 120);
			this.Load += new System.EventHandler(this.Button_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void Button_Load(object sender, System.EventArgs e) {
			if(previewContent != "") {
				cmdButton.Text = previewContent;
			}
		}
	}
}
