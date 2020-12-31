using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;


namespace ADONET.DbUtils
{
	internal class SqlClientSecurity : Genghis.Windows.Forms.WizardPage
	{
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox userText;
    private System.Windows.Forms.TextBox passwordText;
    private System.Windows.Forms.CheckBox integratedSecCheck;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.NumericUpDown lifetimeSpinner;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.NumericUpDown timeoutSpinner;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.NumericUpDown packetSizeSpinner;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.CheckBox persistSecInfoCheck;
		private System.ComponentModel.IContainer components = null;

		public SqlClientSecurity(ConnStringDataSet.SqlClientDataTable sqlClientTable) : base("Connection String Builder", "SQL Server Managed Provider Settings")
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

      // Bind the table to the controls
      userText.DataBindings.Add("Text", sqlClientTable, "User ID");
      passwordText.DataBindings.Add("Text", sqlClientTable, "Pwd");
      integratedSecCheck.DataBindings.Add("Checked", sqlClientTable, "Integrated Security");
      persistSecInfoCheck.DataBindings.Add("Checked", sqlClientTable, "Persist Security Info");;
      lifetimeSpinner.DataBindings.Add("Text", sqlClientTable, "Connection Lifetime");;
      timeoutSpinner.DataBindings.Add("Text", sqlClientTable, "Connection Timeout");;
      packetSizeSpinner.DataBindings.Add("Text", sqlClientTable, "Packet Size");;

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
      this.components = new System.ComponentModel.Container();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.persistSecInfoCheck = new System.Windows.Forms.CheckBox();
      this.integratedSecCheck = new System.Windows.Forms.CheckBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.passwordText = new System.Windows.Forms.TextBox();
      this.userText = new System.Windows.Forms.TextBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.packetSizeSpinner = new System.Windows.Forms.NumericUpDown();
      this.label9 = new System.Windows.Forms.Label();
      this.lifetimeSpinner = new System.Windows.Forms.NumericUpDown();
      this.label10 = new System.Windows.Forms.Label();
      this.timeoutSpinner = new System.Windows.Forms.NumericUpDown();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.groupBox1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.packetSizeSpinner)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.lifetimeSpinner)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.timeoutSpinner)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.persistSecInfoCheck,
                                                                            this.integratedSecCheck,
                                                                            this.label2,
                                                                            this.label1,
                                                                            this.passwordText,
                                                                            this.userText});
      this.groupBox1.Location = new System.Drawing.Point(8, 64);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(384, 112);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Security Settings";
      // 
      // persistSecInfoCheck
      // 
      this.persistSecInfoCheck.Location = new System.Drawing.Point(32, 88);
      this.persistSecInfoCheck.Name = "persistSecInfoCheck";
      this.persistSecInfoCheck.Size = new System.Drawing.Size(224, 16);
      this.persistSecInfoCheck.TabIndex = 5;
      this.persistSecInfoCheck.Text = "Persist Security Information?";
      this.toolTip1.SetToolTip(this.persistSecInfoCheck, @"When set to 'false', security-sensitive information, such as the password, is not returned as part of the connection if the connection is open or has ever been in an open State. Resetting the connection string resets all connection string values including the password.");
      // 
      // integratedSecCheck
      // 
      this.integratedSecCheck.Location = new System.Drawing.Point(32, 72);
      this.integratedSecCheck.Name = "integratedSecCheck";
      this.integratedSecCheck.Size = new System.Drawing.Size(224, 16);
      this.integratedSecCheck.TabIndex = 4;
      this.integratedSecCheck.Text = "Use Integrated Security?";
      this.toolTip1.SetToolTip(this.integratedSecCheck, "Whether the connection is to be a secure connection or not. ");
      this.integratedSecCheck.CheckedChanged += new System.EventHandler(this.integratedSecCheck_CheckedChanged);
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(24, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(72, 16);
      this.label2.TabIndex = 3;
      this.label2.Text = "Password:";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(24, 24);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(72, 16);
      this.label1.TabIndex = 2;
      this.label1.Text = "User Name:";
      // 
      // passwordText
      // 
      this.passwordText.Location = new System.Drawing.Point(96, 48);
      this.passwordText.Name = "passwordText";
      this.passwordText.PasswordChar = '*';
      this.passwordText.Size = new System.Drawing.Size(280, 20);
      this.passwordText.TabIndex = 1;
      this.passwordText.Text = "";
      // 
      // userText
      // 
      this.userText.Location = new System.Drawing.Point(96, 24);
      this.userText.Name = "userText";
      this.userText.Size = new System.Drawing.Size(280, 20);
      this.userText.TabIndex = 0;
      this.userText.Text = "";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.label3,
                                                                            this.packetSizeSpinner,
                                                                            this.label9,
                                                                            this.lifetimeSpinner,
                                                                            this.label10,
                                                                            this.timeoutSpinner});
      this.groupBox3.Location = new System.Drawing.Point(8, 176);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(384, 104);
      this.groupBox3.TabIndex = 12;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Connection Options";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(24, 72);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(112, 16);
      this.label3.TabIndex = 5;
      this.label3.Text = "Packet Size (bytes):";
      // 
      // packetSizeSpinner
      // 
      this.packetSizeSpinner.Location = new System.Drawing.Point(144, 72);
      this.packetSizeSpinner.Maximum = new System.Decimal(new int[] {
                                                                      65535,
                                                                      0,
                                                                      0,
                                                                      0});
      this.packetSizeSpinner.Minimum = new System.Decimal(new int[] {
                                                                      1,
                                                                      0,
                                                                      0,
                                                                      0});
      this.packetSizeSpinner.Name = "packetSizeSpinner";
      this.packetSizeSpinner.Size = new System.Drawing.Size(88, 20);
      this.packetSizeSpinner.TabIndex = 4;
      this.toolTip1.SetToolTip(this.packetSizeSpinner, "Size in bytes of the network packets used to communicate with an instance of SQL " +
        "Server.");
      this.packetSizeSpinner.Value = new System.Decimal(new int[] {
                                                                    8192,
                                                                    0,
                                                                    0,
                                                                    0});
      // 
      // label9
      // 
      this.label9.Location = new System.Drawing.Point(24, 48);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(88, 16);
      this.label9.TabIndex = 3;
      this.label9.Text = "Lifetime (secs):";
      // 
      // lifetimeSpinner
      // 
      this.lifetimeSpinner.Location = new System.Drawing.Point(144, 48);
      this.lifetimeSpinner.Maximum = new System.Decimal(new int[] {
                                                                    600,
                                                                    0,
                                                                    0,
                                                                    0});
      this.lifetimeSpinner.Name = "lifetimeSpinner";
      this.lifetimeSpinner.Size = new System.Drawing.Size(88, 20);
      this.lifetimeSpinner.TabIndex = 1;
      this.toolTip1.SetToolTip(this.lifetimeSpinner, @"When a connection is returned to the pool, its creation time is compared with the current time, and the connection is destroyed if that time span (in seconds) exceeds the value specified by connection lifetime. Useful in clustered configurations to force load balancing between a running server and a server just brought on-line.");
      // 
      // label10
      // 
      this.label10.Location = new System.Drawing.Point(24, 24);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(88, 16);
      this.label10.TabIndex = 1;
      this.label10.Text = "Timeout (secs):";
      // 
      // timeoutSpinner
      // 
      this.timeoutSpinner.Location = new System.Drawing.Point(144, 24);
      this.timeoutSpinner.Maximum = new System.Decimal(new int[] {
                                                                   600,
                                                                   0,
                                                                   0,
                                                                   0});
      this.timeoutSpinner.Name = "timeoutSpinner";
      this.timeoutSpinner.Size = new System.Drawing.Size(88, 20);
      this.timeoutSpinner.TabIndex = 0;
      this.toolTip1.SetToolTip(this.timeoutSpinner, "The length of time (in seconds) to wait for a connection to the server before ter" +
        "minating the attempt and generating an error.");
      this.timeoutSpinner.Value = new System.Decimal(new int[] {
                                                                 15,
                                                                 0,
                                                                 0,
                                                                 0});
      // 
      // SqlClientSecurity
      // 
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.groupBox1,
                                                                  this.groupBox3});
      this.Name = "SqlClientSecurity";
      this.Size = new System.Drawing.Size(400, 288);
      this.groupBox1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.packetSizeSpinner)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.lifetimeSpinner)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.timeoutSpinner)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private void integratedSecCheck_CheckedChanged(object sender, System.EventArgs e)
    {
      userText.Enabled = !integratedSecCheck.Checked;
      passwordText.Enabled = !integratedSecCheck.Checked;
    }

	}
}

