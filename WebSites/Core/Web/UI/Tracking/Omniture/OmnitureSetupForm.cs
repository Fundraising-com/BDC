using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GA.BDC.Core.Web.UI.Tracking.Omniture
{
	/// <summary>
	/// Summary description for OmintureSetupForm.
	/// </summary>
	public class OmnitureSetupForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox OmnitureAccountTextBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public OmnitureSetupForm()
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
			this.OmnitureAccountTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// OmnitureAccountTextBox
			// 
			this.OmnitureAccountTextBox.Location = new System.Drawing.Point(176, 16);
			this.OmnitureAccountTextBox.Name = "OmnitureAccountTextBox";
			this.OmnitureAccountTextBox.Size = new System.Drawing.Size(160, 20);
			this.OmnitureAccountTextBox.TabIndex = 0;
			this.OmnitureAccountTextBox.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Omniture Account Name:";
			// 
			// OmintureSetupForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 429);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.OmnitureAccountTextBox);
			this.Name = "OmintureSetupForm";
			this.Text = "OmintureSetupForm";
			this.ResumeLayout(false);

		}
		#endregion

	}
}
