using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GA.BDC.Core.Web.UI.UIControls
{
	/// <summary>
	/// Summary description for InputBox.
	/// </summary>
	public class InputBox : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.TextBox txtInput;
		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.Button cmdCancel;
		private bool ok = false;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InputBox()
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
			this.lblMessage = new System.Windows.Forms.Label();
			this.txtInput = new System.Windows.Forms.TextBox();
			this.cmdOk = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Location = new System.Drawing.Point(8, 16);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(320, 32);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "label1";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtInput
			// 
			this.txtInput.Location = new System.Drawing.Point(8, 56);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(192, 20);
			this.txtInput.TabIndex = 1;
			this.txtInput.Text = "";
			// 
			// cmdOk
			// 
			this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOk.Location = new System.Drawing.Point(208, 56);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(56, 23);
			this.cmdOk.TabIndex = 2;
			this.cmdOk.Text = "Ok";
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(272, 56);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(56, 23);
			this.cmdCancel.TabIndex = 3;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.button2_Click);
			// 
			// InputBox
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(336, 94);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cmdCancel,
																		  this.cmdOk,
																		  this.txtInput,
																		  this.lblMessage});
			this.Name = "InputBox";
			this.Text = "InputBox";
			this.ResumeLayout(false);

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void cmdOk_Click(object sender, System.EventArgs e) {
			ok = true;
			Close();
		}

		#region Attributes
		public bool Ok {
			set { ok = value; }
			get { return ok; }
		}

		public System.Windows.Forms.Label LblMessage {
			set { lblMessage = value; }
			get { return lblMessage; }
		}

		public System.Windows.Forms.TextBox TxtInput {
			set { txtInput = value; }
			get { return txtInput; }
		}
		#endregion
	}
}
