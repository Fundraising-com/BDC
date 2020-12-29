//
// Dec 2, 2004. Stephen Lim - New class.
//

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;


namespace GA.BDC.Core.TouchService
{
	/// <summary>
	/// Summary description for TestForm.
	/// </summary>
	public class TestForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
		private TouchWorker touchWorker;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox MessageTextBox;
		private System.Windows.Forms.TextBox FromRuleIDTextBox;
		private System.Windows.Forms.TextBox ToRuleIdTextBox;
		private System.Windows.Forms.Label to;
		private System.Windows.Forms.Button StartButton;
		private System.Windows.Forms.Label label2;
		private Thread touchWorkerThread;


		public TestForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}

		static void Main()
		{
			Application.Run(new TestForm());
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
			this.MessageTextBox = new System.Windows.Forms.TextBox();
			this.FromRuleIDTextBox = new System.Windows.Forms.TextBox();
			this.ToRuleIdTextBox = new System.Windows.Forms.TextBox();
			this.to = new System.Windows.Forms.Label();
			this.StartButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Touch Running...";
			// 
			// MessageTextBox
			// 
			this.MessageTextBox.Location = new System.Drawing.Point(8, 48);
			this.MessageTextBox.Multiline = true;
			this.MessageTextBox.Name = "MessageTextBox";
			this.MessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.MessageTextBox.Size = new System.Drawing.Size(648, 424);
			this.MessageTextBox.TabIndex = 1;
			this.MessageTextBox.Text = "";
			// 
			// FromRuleIDTextBox
			// 
			this.FromRuleIDTextBox.Location = new System.Drawing.Point(240, 16);
			this.FromRuleIDTextBox.Name = "FromRuleIDTextBox";
			this.FromRuleIDTextBox.TabIndex = 2;
			this.FromRuleIDTextBox.Text = "0";
			// 
			// ToRuleIdTextBox
			// 
			this.ToRuleIdTextBox.Location = new System.Drawing.Point(384, 16);
			this.ToRuleIdTextBox.Name = "ToRuleIdTextBox";
			this.ToRuleIdTextBox.TabIndex = 3;
			this.ToRuleIdTextBox.Text = "10000";
			// 
			// to
			// 
			this.to.Location = new System.Drawing.Point(352, 16);
			this.to.Name = "to";
			this.to.Size = new System.Drawing.Size(24, 23);
			this.to.TabIndex = 4;
			this.to.Text = "to";
			// 
			// StartButton
			// 
			this.StartButton.Location = new System.Drawing.Point(528, 16);
			this.StartButton.Name = "StartButton";
			this.StartButton.TabIndex = 5;
			this.StartButton.Text = "Start";
			this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(192, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Rule ID:";
			// 
			// TestForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(674, 479);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.StartButton);
			this.Controls.Add(this.to);
			this.Controls.Add(this.ToRuleIdTextBox);
			this.Controls.Add(this.FromRuleIDTextBox);
			this.Controls.Add(this.MessageTextBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "TestForm";
			this.Text = "TouchService";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.TestForm_Closing);
			this.Load += new System.EventHandler(this.TestForm_Load);
			this.ResumeLayout(false);

		}
		#endregion


		private void TestForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try 
			{
				// Shutdown queue worker thread
				if (touchWorkerThread.ThreadState != System.Threading.ThreadState.Unstarted) 
				{
					touchWorkerThread.Abort();
					touchWorkerThread.Join(3000);
				}
			}
			catch {}
		}

		private void TestForm_Load(object sender, System.EventArgs e)
		{
		
		}

		private void StartButton_Click(object sender, System.EventArgs e) {
			touchWorker = new TouchWorker();
			try {
				touchWorker.OnMessage += new Message(touchWorker_OnMessage);
				touchWorker.FromRuleID = int.Parse(FromRuleIDTextBox.Text);
				touchWorker.ToRuleID = int.Parse(ToRuleIdTextBox.Text);
				touchWorkerThread = new Thread(new ThreadStart(touchWorker.Start));
				touchWorkerThread.Start();
			}
			catch {}
		}

		private void touchWorker_OnMessage(string message) {
			MessageTextBox.Text += message + "\r\n";
		}
	}
}
