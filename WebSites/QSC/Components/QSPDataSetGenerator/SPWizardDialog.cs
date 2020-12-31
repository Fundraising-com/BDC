// SPWizardDialog.cs: Shawn Wildermuth [swildermuth@adoguy.com]
#region Copyright © 2002 Shawn Wildermuth
/* This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from the
 * use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software in a
 *    product, an acknowledgment in the product documentation is requested, as
 *    shown here:
 * 
 *    Portions copyright © 2002 Shawn Wildermuth (http://www.adoguy.com/).
 * 
 * 2. No substantial portion of this source code may be redistributed without
 *    the express written permission of the copyright holders, where
 *    "substantial" is defined as enough code to be recognizably from this code.
 */
#endregion

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using Interop.OLEDB32;
using ADONET.DbUtils;

namespace QSP.CommonObjects
{
  [ComVisible(false)]
  public enum ManagedProviderType
  {
    Invalid,
    OLEDB,
    ODBC,
    SQLServer,
    Oracle
  }

  /// <summary>
	/// Summary description for SPWizardWnd.
	/// </summary>
	[ComVisible(false)]
	public class SPWizardDialog : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button FinishButton;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    public System.Windows.Forms.TextBox editConnString;
    public System.Windows.Forms.TextBox editStoredProc;
    public System.Windows.Forms.TextBox editClassName;
    public System.Windows.Forms.TextBox editNamespace;
    private System.Windows.Forms.Button ConnStrButton;
    private System.Windows.Forms.RadioButton radOLEDB;
    private System.Windows.Forms.RadioButton radSQLServer;
    private System.Windows.Forms.RadioButton radODBC;
    private System.Windows.Forms.RadioButton radOracle;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button StoredProcButton;

    public ManagedProviderType Provider
    {
      get
      {
        if (radOLEDB.Checked) return ManagedProviderType.OLEDB;
        if (radODBC.Checked) return ManagedProviderType.ODBC;
        if (radSQLServer.Checked) return ManagedProviderType.SQLServer;
        if (radOracle.Checked) return ManagedProviderType.Oracle;
        return ManagedProviderType.Invalid;
      }
    }

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SPWizardDialog(string className)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
#if DEBUG
      editStoredProc.Text = "spAllTypes";
      editNamespace.Text = "Pubs";
      editConnString.Text = "Provider=SQLOLEDB;Server=localhost;Database=Pubs;UID=sa;PWD=vernyay;";
#endif
      editClassName.Text = className;

      Assembly oracle = Assembly.LoadWithPartialName("System.Data.OracleClient");
      Assembly odbc = Assembly.LoadWithPartialName("Microsoft.Data.Odbc");
      Assembly data = Assembly.LoadWithPartialName("System.Data");
      if (oracle == null) radOracle.Enabled = false;
      if (odbc == null) radODBC.Enabled = false;
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.editConnString = new System.Windows.Forms.TextBox();
      this.FinishButton = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.radOracle = new System.Windows.Forms.RadioButton();
      this.radODBC = new System.Windows.Forms.RadioButton();
      this.radSQLServer = new System.Windows.Forms.RadioButton();
      this.radOLEDB = new System.Windows.Forms.RadioButton();
      this.label4 = new System.Windows.Forms.Label();
      this.editStoredProc = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.editClassName = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.editNamespace = new System.Windows.Forms.TextBox();
      this.ConnStrButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.StoredProcButton = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.White;
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(512, 64);
      this.panel1.TabIndex = 0;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(8, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(376, 24);
      this.label2.TabIndex = 1;
      this.label2.Text = "Creates a class that wraps a Stored Procedure, including the Command and Paramete" +
        "rs, but does not attach a connection...";
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.label1.Location = new System.Drawing.Point(8, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(392, 23);
      this.label1.TabIndex = 0;
      this.label1.Text = "ADO Guy\'s Stored Procedure Wrapper";
      // 
      // editConnString
      // 
      this.editConnString.Location = new System.Drawing.Point(112, 72);
      this.editConnString.Name = "editConnString";
      this.editConnString.Size = new System.Drawing.Size(360, 20);
      this.editConnString.TabIndex = 0;
      this.editConnString.Text = "";
      // 
      // FinishButton
      // 
      this.FinishButton.Location = new System.Drawing.Point(424, 272);
      this.FinishButton.Name = "FinishButton";
      this.FinishButton.TabIndex = 7;
      this.FinishButton.Text = "Finish";
      this.FinishButton.Click += new System.EventHandler(this.FinishButton_Click);
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(8, 72);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(104, 16);
      this.label3.TabIndex = 3;
      this.label3.Text = "Connection String:";
      // 
      // groupBox1
      // 
      this.groupBox1.Location = new System.Drawing.Point(0, 256);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(504, 8);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.radOracle);
      this.groupBox3.Controls.Add(this.radODBC);
      this.groupBox3.Controls.Add(this.radSQLServer);
      this.groupBox3.Controls.Add(this.radOLEDB);
      this.groupBox3.Location = new System.Drawing.Point(112, 168);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(128, 88);
      this.groupBox3.TabIndex = 6;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Managed Provider";
      // 
      // radOracle
      // 
      this.radOracle.Location = new System.Drawing.Point(8, 64);
      this.radOracle.Name = "radOracle";
      this.radOracle.Size = new System.Drawing.Size(104, 16);
      this.radOracle.TabIndex = 3;
      this.radOracle.Text = "Oracle";
      // 
      // radODBC
      // 
      this.radODBC.Location = new System.Drawing.Point(8, 48);
      this.radODBC.Name = "radODBC";
      this.radODBC.Size = new System.Drawing.Size(104, 16);
      this.radODBC.TabIndex = 2;
      this.radODBC.Text = "ODBC";
      // 
      // radSQLServer
      // 
      this.radSQLServer.Location = new System.Drawing.Point(8, 32);
      this.radSQLServer.Name = "radSQLServer";
      this.radSQLServer.Size = new System.Drawing.Size(104, 16);
      this.radSQLServer.TabIndex = 1;
      this.radSQLServer.Text = "SQL Server";
      // 
      // radOLEDB
      // 
      this.radOLEDB.Checked = true;
      this.radOLEDB.Location = new System.Drawing.Point(8, 16);
      this.radOLEDB.Name = "radOLEDB";
      this.radOLEDB.Size = new System.Drawing.Size(104, 16);
      this.radOLEDB.TabIndex = 0;
      this.radOLEDB.TabStop = true;
      this.radOLEDB.Text = "OLE/DB";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(8, 96);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(104, 16);
      this.label4.TabIndex = 8;
      this.label4.Text = "Stored Procedure:";
      // 
      // editStoredProc
      // 
      this.editStoredProc.Location = new System.Drawing.Point(112, 96);
      this.editStoredProc.Name = "editStoredProc";
      this.editStoredProc.Size = new System.Drawing.Size(360, 20);
      this.editStoredProc.TabIndex = 2;
      this.editStoredProc.Text = "";
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(8, 120);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(104, 16);
      this.label5.TabIndex = 10;
      this.label5.Text = "Class Name:";
      // 
      // editClassName
      // 
      this.editClassName.Location = new System.Drawing.Point(112, 120);
      this.editClassName.Name = "editClassName";
      this.editClassName.Size = new System.Drawing.Size(384, 20);
      this.editClassName.TabIndex = 4;
      this.editClassName.Text = "";
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(8, 144);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(104, 16);
      this.label6.TabIndex = 12;
      this.label6.Text = "Namespace:";
      // 
      // editNamespace
      // 
      this.editNamespace.Location = new System.Drawing.Point(112, 144);
      this.editNamespace.Name = "editNamespace";
      this.editNamespace.Size = new System.Drawing.Size(384, 20);
      this.editNamespace.TabIndex = 5;
      this.editNamespace.Text = "";
      // 
      // ConnStrButton
      // 
      this.ConnStrButton.Location = new System.Drawing.Point(472, 72);
      this.ConnStrButton.Name = "ConnStrButton";
      this.ConnStrButton.Size = new System.Drawing.Size(24, 20);
      this.ConnStrButton.TabIndex = 1;
      this.ConnStrButton.Text = "...";
      this.ConnStrButton.Click += new System.EventHandler(this.ConnStrButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(344, 272);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.TabIndex = 8;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
      // 
      // label7
      // 
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.label7.Location = new System.Drawing.Point(248, 176);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(248, 80);
      this.label7.TabIndex = 14;
      this.label7.Text = "Managed Providers may be disabled if the wizard could not find the assembly.  Ple" +
        "ase see Microsoft\'s site to download the ODBC and Oracle Managed Providers if ne" +
        "eded.";
      this.label7.Click += new System.EventHandler(this.label7_Click);
      // 
      // StoredProcButton
      // 
      this.StoredProcButton.Location = new System.Drawing.Point(472, 96);
      this.StoredProcButton.Name = "StoredProcButton";
      this.StoredProcButton.Size = new System.Drawing.Size(24, 20);
      this.StoredProcButton.TabIndex = 3;
      this.StoredProcButton.Text = "...";
      this.StoredProcButton.Click += new System.EventHandler(this.StoredProcButton_Click);
      // 
      // SPWizardDialog
      // 
      this.AcceptButton = this.FinishButton;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(506, 302);
      this.ControlBox = false;
      this.Controls.Add(this.StoredProcButton);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.ConnStrButton);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.editNamespace);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.editClassName);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.editStoredProc);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.FinishButton);
      this.Controls.Add(this.editConnString);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SPWizardDialog";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Stored Procedure Wrapper Wizard (Page 1 of 1)";
      this.Load += new System.EventHandler(this.SPWizardDialog_Load);
      this.panel1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void ConnStrButton_Click(object sender, System.EventArgs e)
    {
      this.editConnString.Text = ConnectionStrings.GetOleDbConnectionString();
    }

    private void FinishButton_Click(object sender, System.EventArgs e)
    {
      if (ValidateWindow())
      {
        this.DialogResult = DialogResult.OK;
        Close();
      }
    }

    private void CancelButton_Click(object sender, System.EventArgs e)
    {
      Close();
    }

    private bool ValidateWindow()
    {
      if ((editClassName.Text.Length > 0) && (editNamespace.Text.Length > 0) && (editConnString.Text.Length > 0) && (this.editStoredProc.Text.Length > 0))
      {
        return true;
      }
      else
      {
        MessageBox.Show("You must fill in all fields");
        return false;
      }
    }

    private void label7_Click(object sender, System.EventArgs e)
    {
    
    }

    private void SPWizardDialog_Load(object sender, System.EventArgs e)
    {
    
    }

    private void StoredProcButton_Click(object sender, System.EventArgs e)
    {
      if (editConnString.Text.Length == 0) return;
      
      StoredProcDialog dlg = new StoredProcDialog(editConnString.Text);

      DialogResult res = dlg.ShowDialog();
      if (res == DialogResult.OK)
      {
        this.editStoredProc.Text = dlg.ResultingStoredProcName;
      }
    }

	}
}
