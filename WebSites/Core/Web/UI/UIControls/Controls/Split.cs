using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GA.BDC.Core.Web.UI.UIControls.Controls
{
	/// <summary>
	/// Summary description for Split.
	/// </summary>
	public class Split : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label lblMessage;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Split()
		{
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

		public string Message {
			set { lblMessage.Text = value; }
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblMessage = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(328, 150);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "Globalizer";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Split
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lblMessage});
			this.Name = "Split";
			this.Size = new System.Drawing.Size(328, 150);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
