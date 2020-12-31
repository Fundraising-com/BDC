using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.DirectoryServices;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;
using Genghis.Windows.Forms;

namespace ADONET.DbUtils
{
	internal class SqlClientLocation : Genghis.Windows.Forms.WizardPage
	{
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox serverCombo;
    private System.Windows.Forms.ComboBox databaseCombo;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox dbFileText;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button dbFileBtn;
    private System.Windows.Forms.OpenFileDialog attachDbFileDialog;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox netLibCombo;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.NumericUpDown maxPoolSpinner;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.NumericUpDown minPoolSpinner;
    private System.Windows.Forms.CheckBox resetConnCheck;
    private System.Windows.Forms.CheckBox enlistCheckbox;
    private System.Windows.Forms.CheckBox connPoolingCheck;
		private System.ComponentModel.IContainer components = null;

		public SqlClientLocation(ConnStringDataSet.SqlClientDataTable sqlClientTable) : base("Connection String Builder", "SQL Server Managed Provider Settings")
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

      // Bind the table to the controls
      serverCombo.DataBindings.Add("Text", sqlClientTable, "Server");
      databaseCombo.DataBindings.Add("Text", sqlClientTable, "Database");
      dbFileText.DataBindings.Add("Text", sqlClientTable, "AttachDBFilename");
      netLibCombo.DataBindings.Add("Text", sqlClientTable, "Network Library");
      maxPoolSpinner.DataBindings.Add("Text", sqlClientTable, "Max Pool Size");
      minPoolSpinner.DataBindings.Add("Text", sqlClientTable, "Min Pool Size");
      resetConnCheck.DataBindings.Add("Checked", sqlClientTable, "Connection Reset");
      enlistCheckbox.DataBindings.Add("Checked", sqlClientTable, "Enlist");
      connPoolingCheck.DataBindings.Add("Checked", sqlClientTable, "Pooling");
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
      this.label1 = new System.Windows.Forms.Label();
      this.serverCombo = new System.Windows.Forms.ComboBox();
      this.databaseCombo = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.dbFileText = new System.Windows.Forms.TextBox();
      this.dbFileBtn = new System.Windows.Forms.Button();
      this.netLibCombo = new System.Windows.Forms.ComboBox();
      this.resetConnCheck = new System.Windows.Forms.CheckBox();
      this.enlistCheckbox = new System.Windows.Forms.CheckBox();
      this.connPoolingCheck = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.attachDbFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label6 = new System.Windows.Forms.Label();
      this.maxPoolSpinner = new System.Windows.Forms.NumericUpDown();
      this.label5 = new System.Windows.Forms.Label();
      this.minPoolSpinner = new System.Windows.Forms.NumericUpDown();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.maxPoolSpinner)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.minPoolSpinner)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(32, 24);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 16);
      this.label1.TabIndex = 1;
      this.label1.Text = "Server:";
      // 
      // serverCombo
      // 
      this.serverCombo.Location = new System.Drawing.Point(120, 24);
      this.serverCombo.Name = "serverCombo";
      this.serverCombo.Size = new System.Drawing.Size(256, 21);
      this.serverCombo.TabIndex = 0;
      this.toolTip1.SetToolTip(this.serverCombo, "The location or network address of the server.");
      this.serverCombo.DropDown += new System.EventHandler(this.serverCombo_DropDown);
      this.serverCombo.TextChanged += new System.EventHandler(this.serverCombo_TextChanged);
      // 
      // databaseCombo
      // 
      this.databaseCombo.Location = new System.Drawing.Point(120, 48);
      this.databaseCombo.Name = "databaseCombo";
      this.databaseCombo.Size = new System.Drawing.Size(256, 21);
      this.databaseCombo.TabIndex = 1;
      this.toolTip1.SetToolTip(this.databaseCombo, "The name of the Database to connect to on the Server.");
      this.databaseCombo.DropDown += new System.EventHandler(this.databaseCombo_DropDown);
      this.databaseCombo.TextChanged += new System.EventHandler(this.databaseCombo_TextChanged);
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(32, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(64, 16);
      this.label2.TabIndex = 3;
      this.label2.Text = "Database:";
      // 
      // dbFileText
      // 
      this.dbFileText.Location = new System.Drawing.Point(120, 72);
      this.dbFileText.Name = "dbFileText";
      this.dbFileText.Size = new System.Drawing.Size(232, 20);
      this.dbFileText.TabIndex = 2;
      this.dbFileText.Text = "";
      this.toolTip1.SetToolTip(this.dbFileText, "The name of the primary file, including the full path name, of an attachable data" +
        "base. ");
      // 
      // dbFileBtn
      // 
      this.dbFileBtn.Location = new System.Drawing.Point(352, 72);
      this.dbFileBtn.Name = "dbFileBtn";
      this.dbFileBtn.Size = new System.Drawing.Size(24, 20);
      this.dbFileBtn.TabIndex = 6;
      this.dbFileBtn.Text = "...";
      this.toolTip1.SetToolTip(this.dbFileBtn, "Pick the DB File to attach to.");
      this.dbFileBtn.Click += new System.EventHandler(this.dbFileBtn_Click);
      // 
      // netLibCombo
      // 
      this.netLibCombo.Items.AddRange(new object[] {
                                                     "TCP/IP",
                                                     "Named Pipes",
                                                     "Multiprotocol",
                                                     "Apple Talk",
                                                     "VIA",
                                                     "Shared Memory",
                                                     "IPX/SPX"});
      this.netLibCombo.Location = new System.Drawing.Point(120, 96);
      this.netLibCombo.Name = "netLibCombo";
      this.netLibCombo.Size = new System.Drawing.Size(256, 21);
      this.netLibCombo.TabIndex = 3;
      this.netLibCombo.Text = "TCP/IP";
      this.toolTip1.SetToolTip(this.netLibCombo, "The Network Library to access the SQL Server with.");
      // 
      // resetConnCheck
      // 
      this.resetConnCheck.Checked = true;
      this.resetConnCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.resetConnCheck.Location = new System.Drawing.Point(32, 48);
      this.resetConnCheck.Name = "resetConnCheck";
      this.resetConnCheck.Size = new System.Drawing.Size(168, 16);
      this.resetConnCheck.TabIndex = 4;
      this.resetConnCheck.Text = "Reset Connection in Pool";
      this.toolTip1.SetToolTip(this.resetConnCheck, @"Determines whether the database connection is reset when being removed from the pool. Setting to 'false' avoids making an additional server round-trip when obtaining a connection, but the programmer must be aware that the connection state is not being reset.");
      // 
      // enlistCheckbox
      // 
      this.enlistCheckbox.Checked = true;
      this.enlistCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.enlistCheckbox.Location = new System.Drawing.Point(32, 32);
      this.enlistCheckbox.Name = "enlistCheckbox";
      this.enlistCheckbox.Size = new System.Drawing.Size(168, 16);
      this.enlistCheckbox.TabIndex = 3;
      this.enlistCheckbox.Text = "Enlist Pooled Connection";
      this.toolTip1.SetToolTip(this.enlistCheckbox, "When true, the pooler automatically enlists the connection in the creation thread" +
        "\'s current transaction context.");
      // 
      // connPoolingCheck
      // 
      this.connPoolingCheck.Checked = true;
      this.connPoolingCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.connPoolingCheck.Location = new System.Drawing.Point(32, 16);
      this.connPoolingCheck.Name = "connPoolingCheck";
      this.connPoolingCheck.Size = new System.Drawing.Size(168, 16);
      this.connPoolingCheck.TabIndex = 2;
      this.connPoolingCheck.Text = "Support Connection Pooling";
      this.toolTip1.SetToolTip(this.connPoolingCheck, "When true, the SQLConnection object is drawn from the appropriate pool, or if nec" +
        "essary, is created and added to the appropriate pool.");
      this.connPoolingCheck.CheckedChanged += new System.EventHandler(this.connPoolingCheck_CheckedChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.netLibCombo,
                                                                            this.label4,
                                                                            this.dbFileBtn,
                                                                            this.dbFileText,
                                                                            this.label3,
                                                                            this.label1,
                                                                            this.serverCombo,
                                                                            this.databaseCombo,
                                                                            this.label2});
      this.groupBox1.Location = new System.Drawing.Point(8, 64);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(384, 136);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Location";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(32, 96);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(88, 16);
      this.label4.TabIndex = 7;
      this.label4.Tag = "";
      this.label4.Text = "Network Library:";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(32, 72);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(64, 16);
      this.label3.TabIndex = 5;
      this.label3.Text = "DB File:";
      // 
      // attachDbFileDialog
      // 
      this.attachDbFileDialog.DefaultExt = "dbf";
      this.attachDbFileDialog.Filter = "Database Files|*.dbf|All files|*.*";
      this.attachDbFileDialog.Title = "Attach DB File:";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.label6,
                                                                            this.maxPoolSpinner,
                                                                            this.label5,
                                                                            this.minPoolSpinner,
                                                                            this.resetConnCheck,
                                                                            this.enlistCheckbox,
                                                                            this.connPoolingCheck});
      this.groupBox2.Location = new System.Drawing.Point(8, 200);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(384, 80);
      this.groupBox2.TabIndex = 6;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Connection Pool Settings";
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(200, 48);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(88, 16);
      this.label6.TabIndex = 10;
      this.label6.Text = "Max Pool Size:";
      // 
      // maxPoolSpinner
      // 
      this.maxPoolSpinner.Location = new System.Drawing.Point(288, 48);
      this.maxPoolSpinner.Name = "maxPoolSpinner";
      this.maxPoolSpinner.Size = new System.Drawing.Size(88, 20);
      this.maxPoolSpinner.TabIndex = 6;
      this.maxPoolSpinner.Value = new System.Decimal(new int[] {
                                                                 100,
                                                                 0,
                                                                 0,
                                                                 0});
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(200, 24);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(88, 16);
      this.label5.TabIndex = 8;
      this.label5.Text = "Min Pool Size:";
      // 
      // minPoolSpinner
      // 
      this.minPoolSpinner.Location = new System.Drawing.Point(288, 24);
      this.minPoolSpinner.Name = "minPoolSpinner";
      this.minPoolSpinner.Size = new System.Drawing.Size(88, 20);
      this.minPoolSpinner.TabIndex = 5;
      // 
      // SqlClientLocation
      // 
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.groupBox1,
                                                                  this.groupBox2});
      this.Name = "SqlClientLocation";
      this.Size = new System.Drawing.Size(400, 288);
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.maxPoolSpinner)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.minPoolSpinner)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private bool serverComboDroppedYet = false;
    private void serverCombo_DropDown(object sender, System.EventArgs e)
    {
      if (!serverComboDroppedYet)
      {
        using (CursorChanger changer = new CursorChanger(Cursors.WaitCursor))
        {
          serverComboDroppedYet = true;
          RegistryKey lastConnectKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\MSSQLServer\Client\SuperSocketNetLib\LastConnect", false);
          serverCombo.Items.AddRange(lastConnectKey.GetValueNames());
          lastConnectKey.Close();
        }
      }
    }

    private bool databaseComboDropped = false;
    private void databaseCombo_DropDown(object sender, System.EventArgs e)
    {
      if (!databaseComboDropped)
      {
        using (CursorChanger changer = new CursorChanger(Cursors.WaitCursor))
        {
          databaseComboDropped = true;
          try
          {
            using (SqlConnection conn = new SqlConnection(string.Format("Server={0};Database=master;Integrated Security=true", serverCombo.Text)))
            {
              conn.Open();
              SqlCommand cmd = conn.CreateCommand();
              cmd.CommandText = "exec sp_databases";
              SqlDataReader rdr = cmd.ExecuteReader();
              while (rdr.Read())
              {
                databaseCombo.Items.Add(rdr.GetString(0));
              }
            }
          }
          catch 
          {
            // Nothing to do
          }
        }
      }
    }

    private void dbFileBtn_Click(object sender, System.EventArgs e)
    {
      if (DialogResult.OK == attachDbFileDialog.ShowDialog())
      {
        dbFileText.Text = attachDbFileDialog.FileName;
      }
    }

    private void groupBox2_Enter(object sender, System.EventArgs e)
    {
    
    }

    private void connPoolingCheck_CheckedChanged(object sender, System.EventArgs e)
    {
      minPoolSpinner.Enabled = connPoolingCheck.Checked;     
      maxPoolSpinner.Enabled = connPoolingCheck.Checked;
      enlistCheckbox.Enabled = connPoolingCheck.Checked;
      resetConnCheck.Enabled = connPoolingCheck.Checked;
      if (!connPoolingCheck.Checked)
      {
        enlistCheckbox.Checked = false;
        resetConnCheck.Checked = false;
      }
    }

    private void databaseCombo_TextChanged(object sender, System.EventArgs e)
    {
    
    }

    private void serverCombo_TextChanged(object sender, System.EventArgs e)
    {
      databaseComboDropped = false;
    }

	}
}

