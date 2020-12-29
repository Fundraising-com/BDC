using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GA.BDC.Core.Web.UI.UIControls
{
	/// <summary>
	/// Summary description for AddVersionHistory.
	/// </summary>
	public class AddVersionHistory : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtAuthor;
		private System.Windows.Forms.TextBox txtModification;
		private System.Windows.Forms.Button cmdSave;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddVersionHistory()
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtAuthor = new System.Windows.Forms.TextBox();
			this.txtModification = new System.Windows.Forms.TextBox();
			this.cmdSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Author:";
			// 
			// txtAuthor
			// 
			this.txtAuthor.Location = new System.Drawing.Point(56, 8);
			this.txtAuthor.Name = "txtAuthor";
			this.txtAuthor.Size = new System.Drawing.Size(336, 20);
			this.txtAuthor.TabIndex = 1;
			this.txtAuthor.Text = "";
			// 
			// txtModification
			// 
			this.txtModification.Location = new System.Drawing.Point(56, 40);
			this.txtModification.Multiline = true;
			this.txtModification.Name = "txtModification";
			this.txtModification.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtModification.Size = new System.Drawing.Size(336, 72);
			this.txtModification.TabIndex = 2;
			this.txtModification.Text = "";
			// 
			// cmdSave
			// 
			this.cmdSave.Location = new System.Drawing.Point(320, 120);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.TabIndex = 3;
			this.cmdSave.Text = "Save";
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// AddVersionHistory
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(400, 149);
			this.Controls.Add(this.cmdSave);
			this.Controls.Add(this.txtModification);
			this.Controls.Add(this.txtAuthor);
			this.Controls.Add(this.label1);
			this.Name = "AddVersionHistory";
			this.Text = "AddVersionHistory";
			this.Load += new System.EventHandler(this.AddVersionHistory_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void AddVersionHistory_Load(object sender, System.EventArgs e) {
			
		}

		private void cmdSave_Click(object sender, System.EventArgs e) {
			Close();
		}

		#region Attributes
		public string Author {
			set { txtAuthor.Text = value; }
			get { return txtAuthor.Text; }
		}

		public string Modification {
			set { txtModification.Text = value; }
			get { return txtModification.Text; }
		}
		#endregion
	}
}
