using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GA.BDC.Core.Web.UI.UIControls.Controls
{
	/// <summary>
	/// Summary description for GlobalizerSplash.
	/// </summary>
	public class GlobalizerSplash : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.PictureBox picSplash;
		private System.Windows.Forms.Label label1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GlobalizerSplash()
		{
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
			this.picSplash = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// picSplash
			// 
			this.picSplash.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picSplash.Location = new System.Drawing.Point(0, 104);
			this.picSplash.Name = "picSplash";
			this.picSplash.Size = new System.Drawing.Size(520, 272);
			this.picSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picSplash.TabIndex = 0;
			this.picSplash.TabStop = false;
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(520, 104);
			this.label1.TabIndex = 0;
			this.label1.Text = "Globalizer Page Setup";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// GlobalizerSplash
			// 
			this.Controls.Add(this.picSplash);
			this.Controls.Add(this.label1);
			this.Name = "GlobalizerSplash";
			this.Size = new System.Drawing.Size(520, 376);
			this.Load += new System.EventHandler(this.GlobalizerSplash_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void GlobalizerSplash_Load(object sender, System.EventArgs e) {
			/*
			System.Net.WebClient wc = new System.Net.WebClient();
			try {
				System.Drawing.Image bmp = Bitmap.FromStream(new System.Net.WebClient().OpenRead("http://efundraising.com/Globalization.jpeg"));
				picSplash.Image = bmp;
			} catch(Exception ex) {
				MessageBox.Show(this, ex.Message);
			}*/
		}
	}
}
