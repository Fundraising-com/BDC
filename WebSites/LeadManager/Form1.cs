using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace efundraising.LeadManager
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private TempLeadIntegrator tempLeadIntegrator;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();
			tempLeadIntegrator = new TempLeadIntegrator();
		}
		
        //// The main entry point for the process
        static void Main(string[] args)
       {
            //Start application
            Application.Run(new Form1());
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(56, 48);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "Start";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(168, 48);
			this.button2.Name = "button2";
			this.button2.TabIndex = 1;
			this.button2.Text = "Stop";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(336, 117);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			tempLeadIntegrator.Stop();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			tempLeadIntegrator.Start();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			tempLeadIntegrator.Stop();
		}
	}
}
