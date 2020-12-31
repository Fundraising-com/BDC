using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace QSP.CommonObjects
{
	/// <summary>
	/// Summary description for StoredProcDialog.
	/// </summary>
	public class StoredProcDialog : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ListBox StoredProcListBox;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button FinishButton;
    private string resultingStoredProc;
    private DataTable storedProcTable;
    private string connString;

    public string ResultingStoredProcName
    {
      get { return resultingStoredProc; }
    }

		public StoredProcDialog(string connectionString)
		{
      connString = connectionString;  
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
      this.StoredProcListBox = new System.Windows.Forms.ListBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.cancelButton = new System.Windows.Forms.Button();
      this.FinishButton = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // StoredProcListBox
      // 
      this.StoredProcListBox.Location = new System.Drawing.Point(8, 16);
      this.StoredProcListBox.Name = "StoredProcListBox";
      this.StoredProcListBox.Size = new System.Drawing.Size(216, 199);
      this.StoredProcListBox.Sorted = true;
      this.StoredProcListBox.TabIndex = 0;
      this.StoredProcListBox.DoubleClick += new System.EventHandler(this.StoredProcListBox_DoubleClick);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.StoredProcListBox);
      this.groupBox1.Location = new System.Drawing.Point(8, 8);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(232, 224);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Select Stored Procedure";
      // 
      // cancelButton
      // 
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(80, 240);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.TabIndex = 10;
      this.cancelButton.Text = "Cancel";
      // 
      // FinishButton
      // 
      this.FinishButton.Location = new System.Drawing.Point(160, 240);
      this.FinishButton.Name = "FinishButton";
      this.FinishButton.TabIndex = 9;
      this.FinishButton.Text = "OK";
      this.FinishButton.Click += new System.EventHandler(this.FinishButton_Click);
      // 
      // StoredProcDialog
      // 
      this.AcceptButton = this.FinishButton;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(248, 270);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.FinishButton);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "StoredProcDialog";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Stored Procedure Selection";
      this.Load += new System.EventHandler(this.StoredProcDialog_Load);
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void FinishButton_Click(object sender, System.EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      DataRowView selected = StoredProcListBox.SelectedValue as DataRowView;;
      this.resultingStoredProc = selected.Row["PROCEDURE_NAME"] as string;
      Close();
    }

    private void StoredProcDialog_Load(object sender, System.EventArgs e)
    {

      OleDbConnection conn = new OleDbConnection(connString);
      conn.Open();
      storedProcTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Procedures, null);
      conn.Close();

      if (storedProcTable != null)
      {
        StoredProcListBox.DataSource = storedProcTable;
#if DEBUG
//        string show = "";
//        foreach (DataColumn col in storedProcTable.Columns)
//        {
//          show += col.ColumnName + "\r\n";
//        }
//        MessageBox.Show(show);
#endif
        StoredProcListBox.DisplayMember = "PROCEDURE_NAME";
      }
    }

    private void StoredProcListBox_DoubleClick(object sender, System.EventArgs e)
    {
      FinishButton_Click(null, null);
    }
	}
}
