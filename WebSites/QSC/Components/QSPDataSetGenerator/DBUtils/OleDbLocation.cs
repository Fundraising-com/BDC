using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;

namespace ADONET.DbUtils
{
	internal class OleDbLocation : Genghis.Windows.Forms.WizardPage
	{
		private System.ComponentModel.IContainer components = null;

		public OleDbLocation()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
      Label lbl = new Label();
      lbl.Text = "OleDb";
      lbl.Location = new Point(20,20);
      lbl.Size = new Size(100,100);
      this.Controls.Add(lbl);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

