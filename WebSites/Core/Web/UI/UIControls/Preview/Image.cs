using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GA.BDC.Core.Web.UI.UIControls.Preview
{
	/// <summary>
	/// Summary description for Image.
	/// </summary>
	public class Image : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.PictureBox picture;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private string previewContent;

		public Image(string _previewContent)
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
			this.picture = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// picture
			// 
			this.picture.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picture.Location = new System.Drawing.Point(0, 0);
			this.picture.Name = "picture";
			this.picture.Size = new System.Drawing.Size(150, 150);
			this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picture.TabIndex = 0;
			this.picture.TabStop = false;
			// 
			// Image
			// 
			this.Controls.Add(this.picture);
			this.Name = "Image";
			this.Load += new System.EventHandler(this.Image_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Image_Load(object sender, System.EventArgs e) {
			if(previewContent != "") {
				try {
					picture.Image = Bitmap.FromFile(previewContent);
				} catch(System.Exception ex) {
					MessageBox.Show(this, "Unable to preview the image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
