using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

namespace QSP.CommonObjects
{
	/// <summary>
	/// Summary description for ConnFactoryWizardDialog.
	/// </summary>
	public class ConnFactoryWizardDialog : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label6;
    public System.Windows.Forms.TextBox editNamespace;
    private System.Windows.Forms.Label label5;
    public System.Windows.Forms.TextBox editClassName;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.RadioButton radOracle;
    private System.Windows.Forms.RadioButton radODBC;
    private System.Windows.Forms.RadioButton radSQLServer;
    private System.Windows.Forms.RadioButton radOLEDB;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button FinishButton;
    private System.Windows.Forms.Label label7;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ConnFactoryWizardDialog(string className)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
#if DEBUG
      editNamespace.Text = "TestNamespace";
#endif
      editClassName.Text = className;

      Assembly oracle = Assembly.LoadWithPartialName("System.Data.OracleClient");
      Assembly odbc = Assembly.LoadWithPartialName("Microsoft.Data.Odbc");
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
      this.label6 = new System.Windows.Forms.Label();
      this.editNamespace = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.editClassName = new System.Windows.Forms.TextBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.radOracle = new System.Windows.Forms.RadioButton();
      this.radODBC = new System.Windows.Forms.RadioButton();
      this.radSQLServer = new System.Windows.Forms.RadioButton();
      this.radOLEDB = new System.Windows.Forms.RadioButton();
      this.cancelButton = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.FinishButton = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.White;
      this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                         this.label2,
                                                                         this.label1});
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(464, 64);
      this.panel1.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(8, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(280, 24);
      this.label2.TabIndex = 1;
      this.label2.Text = "Creates a class that hands out ADO.NET Connections";
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.label1.Location = new System.Drawing.Point(8, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(280, 23);
      this.label1.TabIndex = 0;
      this.label1.Text = "ADO Guy\'s Connection Factory Wizard";
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(8, 96);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(72, 16);
      this.label6.TabIndex = 17;
      this.label6.Text = "Namespace:";
      // 
      // editNamespace
      // 
      this.editNamespace.Location = new System.Drawing.Point(88, 96);
      this.editNamespace.Name = "editNamespace";
      this.editNamespace.Size = new System.Drawing.Size(368, 20);
      this.editNamespace.TabIndex = 14;
      this.editNamespace.Text = "";
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(8, 72);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(72, 16);
      this.label5.TabIndex = 16;
      this.label5.Text = "Class Name:";
      // 
      // editClassName
      // 
      this.editClassName.Location = new System.Drawing.Point(88, 72);
      this.editClassName.Name = "editClassName";
      this.editClassName.Size = new System.Drawing.Size(368, 20);
      this.editClassName.TabIndex = 13;
      this.editClassName.Text = "";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.radOracle,
                                                                            this.radODBC,
                                                                            this.radSQLServer,
                                                                            this.radOLEDB});
      this.groupBox3.Location = new System.Drawing.Point(88, 120);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(128, 88);
      this.groupBox3.TabIndex = 15;
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
      // cancelButton
      // 
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(304, 232);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.TabIndex = 20;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Location = new System.Drawing.Point(4, 216);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(452, 8);
      this.groupBox1.TabIndex = 18;
      this.groupBox1.TabStop = false;
      // 
      // FinishButton
      // 
      this.FinishButton.Location = new System.Drawing.Point(384, 232);
      this.FinishButton.Name = "FinishButton";
      this.FinishButton.TabIndex = 19;
      this.FinishButton.Text = "Finish";
      this.FinishButton.Click += new System.EventHandler(this.FinishButton_Click);
      // 
      // label7
      // 
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.label7.Location = new System.Drawing.Point(224, 128);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(240, 80);
      this.label7.TabIndex = 21;
      this.label7.Text = "Managed Providers may be disabled if the wizard could not find the assembly.  Ple" +
        "ase see Microsoft\'s site to download the ODBC and Oracle Managed Providers if ne" +
        "eded.";
      // 
      // ConnFactoryWizardDialog
      // 
      this.AcceptButton = this.FinishButton;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(464, 262);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.label7,
                                                                  this.cancelButton,
                                                                  this.groupBox1,
                                                                  this.FinishButton,
                                                                  this.label6,
                                                                  this.editNamespace,
                                                                  this.label5,
                                                                  this.editClassName,
                                                                  this.groupBox3,
                                                                  this.panel1});
      this.Name = "ConnFactoryWizardDialog";
      this.Text = "Connection Factory Wizard";
      this.Load += new System.EventHandler(this.ConnFactoryWizardDialog_Load);
      this.panel1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void ConnFactoryWizardDialog_Load(object sender, System.EventArgs e)
    {
    
    }

    private void FinishButton_Click(object sender, System.EventArgs e)
    {
      if (ValidateWindow())
      {
        this.DialogResult = DialogResult.OK;
        Close();
      }
    }

    private bool ValidateWindow()
    {
      if ((editClassName.Text.Length > 0) && (editNamespace.Text.Length > 0))
      {
        return true;
      }
      else
      {
        MessageBox.Show("You must fill in all fields");
        return false;
      }
    }

    private void cancelButton_Click(object sender, System.EventArgs e)
    {
      Close();
    }


	}
}
