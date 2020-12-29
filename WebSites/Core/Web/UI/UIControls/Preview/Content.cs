using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GA.BDC.Core.Web.UI.UIControls.Preview
{
	/// <summary>
	/// Summary description for Content.
	/// </summary>
	public class Content : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TextBox textBox1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private string previewContent;

		public Content(string _previewContent)
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(150, 150);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			// 
			// Content
			// 
			this.Controls.Add(this.textBox1);
			this.Name = "Content";
			this.Load += new System.EventHandler(this.Content_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Content_Load(object sender, System.EventArgs e) {
			if(previewContent == "") {
				textBox1.Text = "Magnus es, domine, et <a href=\"laudabilis.aspx\">laudabilis</a> valde: magna virtus tua, et sapientiae tuae non est numerus. et laudare te vult homo, aliqua portio creaturae tuae, et homo circumferens mortalitem suam, circumferens testimonium peccati sui et testimonium, quia superbis resistis: et tamen laudare te vult homo, aliqua portio creaturae tuae.tu excitas\r\n";
			} else {
				textBox1.Text = previewContent;
			}
		}
	}
}
