namespace FormPermission
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.btnGenerateFullData = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rbnSourceDenied = new System.Windows.Forms.RadioButton();
            this.rbnSourceAllowed = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.tbxCSVFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnParse = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnToDisk = new System.Windows.Forms.Button();
            this.btnToClipboard = new System.Windows.Forms.Button();
            this.rbnFormIDBoth = new System.Windows.Forms.RadioButton();
            this.btnGenerateSQL = new System.Windows.Forms.Button();
            this.rbnFormIDDenied = new System.Windows.Forms.RadioButton();
            this.rbnFormIDAllowed = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxSQL = new System.Windows.Forms.TextBox();
            this.tbxFormID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxUserID = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNote);
            this.groupBox1.Controls.Add(this.btnGenerateFullData);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dataGridView3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.rbnSourceDenied);
            this.groupBox1.Controls.Add(this.rbnSourceAllowed);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.tbxCSVFile);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.btnParse);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(753, 428);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source zip codes";
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(135, 44);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(186, 13);
            this.lblNote.TabIndex = 15;
            this.lblNote.Text = "First column in CSV must be ZIP Code";
            // 
            // btnGenerateFullData
            // 
            this.btnGenerateFullData.Location = new System.Drawing.Point(632, 394);
            this.btnGenerateFullData.Name = "btnGenerateFullData";
            this.btnGenerateFullData.Size = new System.Drawing.Size(115, 23);
            this.btnGenerateFullData.TabIndex = 14;
            this.btnGenerateFullData.Text = "2 - Generate full data";
            this.btnGenerateFullData.UseVisualStyleBackColor = true;
            this.btnGenerateFullData.Click += new System.EventHandler(this.button5_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(501, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Denied zip codes";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(504, 99);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.Size = new System.Drawing.Size(243, 284);
            this.dataGridView3.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(252, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Allowed zip codes";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Source zip codes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Source zips are";
            // 
            // rbnSourceDenied
            // 
            this.rbnSourceDenied.AutoSize = true;
            this.rbnSourceDenied.Location = new System.Drawing.Point(255, 63);
            this.rbnSourceDenied.Name = "rbnSourceDenied";
            this.rbnSourceDenied.Size = new System.Drawing.Size(59, 17);
            this.rbnSourceDenied.TabIndex = 8;
            this.rbnSourceDenied.TabStop = true;
            this.rbnSourceDenied.Text = "Denied";
            this.rbnSourceDenied.UseVisualStyleBackColor = true;
            // 
            // rbnSourceAllowed
            // 
            this.rbnSourceAllowed.AutoSize = true;
            this.rbnSourceAllowed.Checked = true;
            this.rbnSourceAllowed.Location = new System.Drawing.Point(135, 63);
            this.rbnSourceAllowed.Name = "rbnSourceAllowed";
            this.rbnSourceAllowed.Size = new System.Drawing.Size(62, 17);
            this.rbnSourceAllowed.TabIndex = 7;
            this.rbnSourceAllowed.TabStop = true;
            this.rbnSourceAllowed.Text = "Allowed";
            this.rbnSourceAllowed.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(722, 17);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(25, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbxCSVFile
            // 
            this.tbxCSVFile.Location = new System.Drawing.Point(135, 19);
            this.tbxCSVFile.Name = "tbxCSVFile";
            this.tbxCSVFile.Size = new System.Drawing.Size(581, 20);
            this.tbxCSVFile.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Source file";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(255, 99);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(243, 284);
            this.dataGridView2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(243, 284);
            this.dataGridView1.TabIndex = 2;
            // 
            // btnParse
            // 
            this.btnParse.Location = new System.Drawing.Point(504, 394);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(115, 23);
            this.btnParse.TabIndex = 1;
            this.btnParse.Text = "1 - Parse";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbxUserID);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnToDisk);
            this.groupBox2.Controls.Add(this.btnToClipboard);
            this.groupBox2.Controls.Add(this.rbnFormIDBoth);
            this.groupBox2.Controls.Add(this.btnGenerateSQL);
            this.groupBox2.Controls.Add(this.rbnFormIDDenied);
            this.groupBox2.Controls.Add(this.rbnFormIDAllowed);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbxSQL);
            this.groupBox2.Controls.Add(this.tbxFormID);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(771, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 428);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generate sql";
            // 
            // btnToDisk
            // 
            this.btnToDisk.Location = new System.Drawing.Point(237, 394);
            this.btnToDisk.Name = "btnToDisk";
            this.btnToDisk.Size = new System.Drawing.Size(96, 23);
            this.btnToDisk.TabIndex = 8;
            this.btnToDisk.Text = "To disk";
            this.btnToDisk.UseVisualStyleBackColor = true;
            this.btnToDisk.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnToClipboard
            // 
            this.btnToClipboard.Location = new System.Drawing.Point(122, 394);
            this.btnToClipboard.Name = "btnToClipboard";
            this.btnToClipboard.Size = new System.Drawing.Size(96, 23);
            this.btnToClipboard.TabIndex = 7;
            this.btnToClipboard.Text = "To clipboard";
            this.btnToClipboard.UseVisualStyleBackColor = true;
            this.btnToClipboard.Click += new System.EventHandler(this.button4_Click);
            // 
            // rbnFormIDBoth
            // 
            this.rbnFormIDBoth.AutoSize = true;
            this.rbnFormIDBoth.Checked = true;
            this.rbnFormIDBoth.Location = new System.Drawing.Point(251, 81);
            this.rbnFormIDBoth.Name = "rbnFormIDBoth";
            this.rbnFormIDBoth.Size = new System.Drawing.Size(47, 17);
            this.rbnFormIDBoth.TabIndex = 6;
            this.rbnFormIDBoth.TabStop = true;
            this.rbnFormIDBoth.Text = "Both";
            this.rbnFormIDBoth.UseVisualStyleBackColor = true;
            // 
            // btnGenerateSQL
            // 
            this.btnGenerateSQL.Location = new System.Drawing.Point(9, 394);
            this.btnGenerateSQL.Name = "btnGenerateSQL";
            this.btnGenerateSQL.Size = new System.Drawing.Size(98, 23);
            this.btnGenerateSQL.TabIndex = 2;
            this.btnGenerateSQL.Text = "3 - Generate sql";
            this.btnGenerateSQL.UseVisualStyleBackColor = true;
            this.btnGenerateSQL.Click += new System.EventHandler(this.button2_Click);
            // 
            // rbnFormIDDenied
            // 
            this.rbnFormIDDenied.AutoSize = true;
            this.rbnFormIDDenied.Location = new System.Drawing.Point(171, 81);
            this.rbnFormIDDenied.Name = "rbnFormIDDenied";
            this.rbnFormIDDenied.Size = new System.Drawing.Size(59, 17);
            this.rbnFormIDDenied.TabIndex = 5;
            this.rbnFormIDDenied.Text = "Denied";
            this.rbnFormIDDenied.UseVisualStyleBackColor = true;
            // 
            // rbnFormIDAllowed
            // 
            this.rbnFormIDAllowed.AutoSize = true;
            this.rbnFormIDAllowed.Location = new System.Drawing.Point(88, 81);
            this.rbnFormIDAllowed.Name = "rbnFormIDAllowed";
            this.rbnFormIDAllowed.Size = new System.Drawing.Size(62, 17);
            this.rbnFormIDAllowed.TabIndex = 4;
            this.rbnFormIDAllowed.Text = "Allowed";
            this.rbnFormIDAllowed.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Generate";
            // 
            // tbxSQL
            // 
            this.tbxSQL.Location = new System.Drawing.Point(6, 115);
            this.tbxSQL.Multiline = true;
            this.tbxSQL.Name = "tbxSQL";
            this.tbxSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxSQL.Size = new System.Drawing.Size(328, 263);
            this.tbxSQL.TabIndex = 0;
            // 
            // tbxFormID
            // 
            this.tbxFormID.Location = new System.Drawing.Point(122, 20);
            this.tbxFormID.Name = "tbxFormID";
            this.tbxFormID.Size = new System.Drawing.Size(66, 20);
            this.tbxFormID.TabIndex = 1;
            this.tbxFormID.Text = "141";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Form ID";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "csv";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "CSV files|*.csv|AllFiles|*.*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "User ID";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(194, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "( *Just one ID )";
            // 
            // tbxUserID
            // 
            this.tbxUserID.Location = new System.Drawing.Point(122, 44);
            this.tbxUserID.Name = "tbxUserID";
            this.tbxUserID.Size = new System.Drawing.Size(66, 20);
            this.tbxUserID.TabIndex = 11;
            this.tbxUserID.Text = "101935";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 452);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGenerateSQL;
        private System.Windows.Forms.TextBox tbxFormID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxSQL;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbxCSVFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbnSourceDenied;
        private System.Windows.Forms.RadioButton rbnSourceAllowed;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.RadioButton rbnFormIDDenied;
        private System.Windows.Forms.RadioButton rbnFormIDAllowed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbnFormIDBoth;
        private System.Windows.Forms.Button btnToClipboard;
        private System.Windows.Forms.Button btnGenerateFullData;
        private System.Windows.Forms.Button btnToDisk;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxUserID;
    }
}

