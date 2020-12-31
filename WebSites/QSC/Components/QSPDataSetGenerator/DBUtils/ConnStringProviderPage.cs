using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Genghis.Windows.Forms;
using System.Windows.Forms;

namespace ADONET.DbUtils
{
	internal class ConnStringProviderPage : Genghis.Windows.Forms.WizardPage
	{
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton oracleButton;
    private System.Windows.Forms.RadioButton odbcButton;
    private System.Windows.Forms.RadioButton oledbButton;
    private System.Windows.Forms.RadioButton sqlButton;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox addFactoryCheck;
    private System.Windows.Forms.CheckBox copyClipboardCheck;
    private System.Windows.Forms.CheckBox pasteConnectionCheck;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.ComponentModel.IContainer components = null;
    private ManagedProviderType mpType;

		public ConnStringProviderPage(ManagedProviderType type) : base("Connection String Builder", "Allows you to construct a connection string for Managed Providers.")
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

      // Cache the selected type
      mpType = type;
		}

    protected void EnableProviderButtons(ManagedProviderType type)
    {
      // Enable/Disable Managed Providers
			this.sqlButton.Enabled    = (type & ManagedProviderType.SqlClient) == ManagedProviderType.SqlClient;
      this.oledbButton.Enabled  = (type & ManagedProviderType.OleDb) == ManagedProviderType.OleDb;
      this.oracleButton.Enabled = (type & ManagedProviderType.Oracle) == ManagedProviderType.Oracle;
      this.odbcButton.Enabled   = (type & ManagedProviderType.Odbc) == ManagedProviderType.Odbc;

      // Select the first enabled one
      if (sqlButton.Enabled) sqlButton.Checked = true;
      else if (oledbButton.Enabled) oledbButton.Checked = true;
      else if (oracleButton.Enabled) oracleButton.Checked = true;
      else if (odbcButton.Enabled) odbcButton.Checked = true;
      else
      {
        throw new Exception("No Managed Providers Enabled");
      }

      // Cause the page to be setup correctly
      SetCorrectPage();
    }

    protected void SetCorrectPage()
    {
      WizardSheet sht = ParentForm as WizardSheet;
      if (sqlButton.Checked) sht.CurrentGroup = "SqlClient";
      if (oledbButton.Checked) sht.CurrentGroup = "OleDb";
      if (odbcButton.Checked) sht.CurrentGroup = "Odbc";
      if (oracleButton.Checked) sht.CurrentGroup = "Oracle";
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

    private void ProviderClicked(object src, EventArgs args)
    {
      SetCorrectPage();
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
      this.oracleButton = new System.Windows.Forms.RadioButton();
      this.odbcButton = new System.Windows.Forms.RadioButton();
      this.oledbButton = new System.Windows.Forms.RadioButton();
      this.sqlButton = new System.Windows.Forms.RadioButton();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.pasteConnectionCheck = new System.Windows.Forms.CheckBox();
      this.copyClipboardCheck = new System.Windows.Forms.CheckBox();
      this.addFactoryCheck = new System.Windows.Forms.CheckBox();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.oracleButton,
                                                                            this.odbcButton,
                                                                            this.oledbButton,
                                                                            this.sqlButton});
      this.groupBox1.Location = new System.Drawing.Point(8, 152);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(384, 112);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Pick a Managed Provider:";
      // 
      // oracleButton
      // 
      this.oracleButton.Location = new System.Drawing.Point(32, 72);
      this.oracleButton.Name = "oracleButton";
      this.oracleButton.Size = new System.Drawing.Size(232, 16);
      this.oracleButton.TabIndex = 3;
      this.oracleButton.Text = "Oracle Managed Provider (OracleClient)";
      this.oracleButton.Click += new System.EventHandler(this.ProviderClicked);
      // 
      // odbcButton
      // 
      this.odbcButton.Location = new System.Drawing.Point(32, 56);
      this.odbcButton.Name = "odbcButton";
      this.odbcButton.Size = new System.Drawing.Size(232, 16);
      this.odbcButton.TabIndex = 2;
      this.odbcButton.Text = "ODBC Managed Provider (Odbc)";
      this.odbcButton.Click += new System.EventHandler(this.ProviderClicked);
      // 
      // oledbButton
      // 
      this.oledbButton.Location = new System.Drawing.Point(32, 40);
      this.oledbButton.Name = "oledbButton";
      this.oledbButton.Size = new System.Drawing.Size(232, 16);
      this.oledbButton.TabIndex = 1;
      this.oledbButton.Text = "OLE/DB Managed Provider (OleDb)";
      this.oledbButton.Click += new System.EventHandler(this.ProviderClicked);
      // 
      // sqlButton
      // 
      this.sqlButton.Checked = true;
      this.sqlButton.Location = new System.Drawing.Point(32, 24);
      this.sqlButton.Name = "sqlButton";
      this.sqlButton.Size = new System.Drawing.Size(240, 16);
      this.sqlButton.TabIndex = 0;
      this.sqlButton.TabStop = true;
      this.sqlButton.Text = "SQL Server Managed Provider (SqlClient)";
      this.sqlButton.Click += new System.EventHandler(this.ProviderClicked);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.pasteConnectionCheck,
                                                                            this.copyClipboardCheck,
                                                                            this.addFactoryCheck});
      this.groupBox2.Location = new System.Drawing.Point(8, 64);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(384, 80);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Build what?";
      // 
      // pasteConnectionCheck
      // 
      this.pasteConnectionCheck.Location = new System.Drawing.Point(32, 56);
      this.pasteConnectionCheck.Name = "pasteConnectionCheck";
      this.pasteConnectionCheck.Size = new System.Drawing.Size(328, 16);
      this.pasteConnectionCheck.TabIndex = 2;
      this.pasteConnectionCheck.Text = "Paste a new Connection Object at the current cursor position";
      this.toolTip1.SetToolTip(this.pasteConnectionCheck, "Adds a new Connection Object at the cursor position of an open document.");
      this.pasteConnectionCheck.CheckedChanged += new System.EventHandler(this.pasteConnectionCheck_CheckedChanged);
      // 
      // copyClipboardCheck
      // 
      this.copyClipboardCheck.Location = new System.Drawing.Point(32, 40);
      this.copyClipboardCheck.Name = "copyClipboardCheck";
      this.copyClipboardCheck.Size = new System.Drawing.Size(296, 16);
      this.copyClipboardCheck.TabIndex = 1;
      this.copyClipboardCheck.Text = "Copy the Connection String to the Clipboard";
      this.toolTip1.SetToolTip(this.copyClipboardCheck, "Copies the connection string to the clipboard.");
      this.copyClipboardCheck.CheckedChanged += new System.EventHandler(this.copyClipboardCheck_CheckedChanged);
      // 
      // addFactoryCheck
      // 
      this.addFactoryCheck.Checked = true;
      this.addFactoryCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.addFactoryCheck.Location = new System.Drawing.Point(32, 24);
      this.addFactoryCheck.Name = "addFactoryCheck";
      this.addFactoryCheck.Size = new System.Drawing.Size(296, 16);
      this.addFactoryCheck.TabIndex = 0;
      this.addFactoryCheck.Text = "Add a Connection Factory to the Current Project";
      this.toolTip1.SetToolTip(this.addFactoryCheck, "Adds a class to the current project that supports creating new connections withou" +
        "t exposing the connection properties.");
      this.addFactoryCheck.CheckedChanged += new System.EventHandler(this.addFactoryCheck_CheckedChanged);
      // 
      // ConnStringProviderPage
      // 
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.groupBox2,
                                                                  this.groupBox1});
      this.Name = "ConnStringProviderPage";
      this.Load += new System.EventHandler(this.ConnStringProviderPage_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void addFactoryCheck_CheckedChanged(object sender, System.EventArgs e)
    {
      if (!addFactoryCheck.Checked && !copyClipboardCheck.Checked && !pasteConnectionCheck.Checked)
      {
        addFactoryCheck.Checked = true;
      }
    }

    private void copyClipboardCheck_CheckedChanged(object sender, System.EventArgs e)
    {
      addFactoryCheck_CheckedChanged(sender, e);
    }

    private void pasteConnectionCheck_CheckedChanged(object sender, System.EventArgs e)
    {
      addFactoryCheck_CheckedChanged(sender, e);
    }

    private void ConnStringProviderPage_Load(object sender, System.EventArgs e)
    {
      EnableProviderButtons(mpType);
    }
	}
}

