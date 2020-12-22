using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

namespace FormPermission
{
    public partial class Form1 : Form
    {
        private List<string> zipCodesSource = new List<string>();
        private List<string> zipCodesAllowed = new List<string>();
        private List<string> zipCodesDenied = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;

            this.zipCodesSource = new List<string>();
            this.zipCodesAllowed = new List<string>();
            this.zipCodesDenied = new List<string>();

            #region Read values from CSV

            using (StreamReader sr = new StreamReader(this.textBox1.Text))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    this.zipCodesSource.Add(line);
                }
            }

            #endregion

            #region Generate allowed / denied zip code lists

            if (this.radioButton1.Checked)
            {
                // Source zip codes are allowed
                this.CopySourceToAllowed();
                this.GenerateDeniedFromAllowed();
            }
            else if (this.radioButton2.Checked)
            {
                // Source zip codes are denied
                //this.CopySourceToDenied();
                //this.GenerateAllowedFromDenied();
            }

            #endregion

            this.label7.Text = String.Format("Allowed zip codes ({0})", this.zipCodesAllowed.Count);
            this.label8.Text = String.Format("Denied zip codes ({0})", this.zipCodesDenied.Count);

            this.dataGridView1.DataSource = this.zipCodesSource;
            this.dataGridView2.DataSource = this.zipCodesAllowed;
            this.dataGridView3.DataSource = this.zipCodesDenied;

            this.button1.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.button2.Enabled = false;

            StringBuilder sb = new StringBuilder();

            string isAllowed = "";
            string formId = this.textBox2.Text.Trim();
            string userId = "101689";
            bool generateAllowed = this.radioButton3.Checked;
            bool generateDenied = this.radioButton4.Checked;
            bool generateBoth = this.radioButton5.Checked;

            if (generateAllowed || generateBoth)
            {
                sb.AppendLine("-- Allowed zip codes --");
                sb.AppendLine("-- ----------------- --");

                isAllowed = "1";
                foreach (string zip in this.zipCodesAllowed)
                {
                    this.AddInsertToQueryBatch(sb, formId, zip, "", isAllowed, userId);
                }

                sb.AppendLine("-- ----------------- --");
            }
            if (generateDenied || generateBoth)
            {
                sb.AppendLine("-- Denied zip codes --");
                sb.AppendLine("-- ---------------- --");

                isAllowed = "0";
                foreach (string zip in this.zipCodesDenied)
                {
                    this.AddInsertToQueryBatch(sb, formId, zip, "", isAllowed, userId);
                }

                sb.AppendLine("-- ----------------- --");
            }

            this.textBox3.Text = sb.ToString();

            this.button2.Enabled = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.button3.Enabled = false;

            DialogResult result = this.openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBox1.Text = this.openFileDialog1.FileName;
            }

            this.button3.Enabled = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.button4.Enabled = false;

            this.textBox3.SelectAll();
            this.textBox3.Copy();

            this.button4.Enabled = true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //// Generate data for all zip codes

            //this.button5.Enabled = false;

            //List<string> fullAllowedZipList = new List<string>();
            //List<string> fullDeniedZipList = new List<string>();
            //List<ZipCodeItem> fullZipList = new List<ZipCodeItem>();

            //#region Generate full allowed zip list

            //foreach (string zip in this.zipCodeAllowedString)
            //{
            //    if (zip.Length == 3)
            //    {
            //        for (int i = 0; i < 100; i++)
            //        {
            //            string newZip = zip + i.ToString("00");

            //            fullAllowedZipList.Add(newZip);
            //        }
            //    }
            //    else if (zip.Length == 4)
            //    {
            //        for (int i = 0; i < 10; i++)
            //        {
            //            string newZip = zip + i.ToString("0");

            //            fullAllowedZipList.Add(newZip);
            //        }
            //    }
            //    else if (zip.Length == 5)
            //    {
            //        fullAllowedZipList.Add(zip);
            //    }
            //}

            //#endregion

            //#region Generate full denied zip list

            //foreach (string zip in this.zipCodeDeniedString)
            //{
            //    if (zip.Length == 3)
            //    {
            //        for (int i = 0; i < 100; i++)
            //        {
            //            string newZip = zip + i.ToString("00");

            //            fullDeniedZipList.Add(newZip);
            //        }
            //    }
            //    else if (zip.Length == 4)
            //    {
            //        for (int i = 0; i < 10; i++)
            //        {
            //            string newZip = zip + i.ToString("0");

            //            fullDeniedZipList.Add(newZip);
            //        }
            //    }
            //    else if (zip.Length == 5)
            //    {
            //        fullDeniedZipList.Add(zip);
            //    }
            //}

            //#endregion

            //#region Generate full zip list

            //for (int i = 0; i < 100000; i++)
            //{
            //    string zip = i.ToString("00000");

            //    ZipCodeItem zipCodeItem = new ZipCodeItem();

            //    zipCodeItem.ZipCode = zip;
            //    zipCodeItem.Description = "";
            //    zipCodeItem.IsAllowed = fullAllowedZipList.Contains(zip);
            //    zipCodeItem.IsDenied = fullDeniedZipList.Contains(zip);

            //    fullZipList.Add(zipCodeItem);
            //}

            //#endregion

            //List<ZipCodeItem> nonFlagged =
            //    (from fzl in fullZipList
            //     where fzl.IsAllowed == false
            //        && fzl.IsDenied == false
            //     select fzl
            //     ).ToList();

            //List<ZipCodeItem> doubleFlagged =
            //    (from fzl in fullZipList
            //     where fzl.IsAllowed == true
            //        && fzl.IsDenied == true
            //     select fzl
            //     ).ToList();

            //List<ZipCodeItem> allowed =
            //    (from fzl in fullZipList
            //     where fzl.IsAllowed == true
            //        && fzl.IsDenied == false
            //     select fzl
            //     ).ToList();

            //List<ZipCodeItem> denied =
            //    (from fzl in fullZipList
            //     where fzl.IsAllowed == false
            //        && fzl.IsDenied == true
            //     select fzl
            //     ).ToList();


            //this.zipCodesAllowed = allowed;
            //this.zipCodesDenied = nonFlagged;


            //int nonFlaggedCount = nonFlagged.Count;
            //int doubleFlaggedCount = doubleFlagged.Count;
            //int allowedCount = allowed.Count;
            //int deniedCount = denied.Count;

            //this.button5.Enabled = true;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.button6.Enabled = false;

            StringBuilder sb = new StringBuilder();
            string targetFolder = @"d:\Documents and Settings\jmartinez0033\Desktop\temp";
            int maxLinesPerFile = 10000;
            int currentLine = 1;
            int currentFile = 1;
            string fileName;
            string fileContents;
            TextWriter tw;

            using (StringReader reader = new StringReader(this.textBox3.Text))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    sb.AppendLine(line);

                    if (currentLine >= maxLinesPerFile)
                    {
                        fileName = targetFolder + "\\file-" + currentFile.ToString() + ".sql";
                        fileContents = "use qspfulfillment" + System.Environment.NewLine + sb.ToString();

                        tw = new StreamWriter(fileName);
                        tw.Write(fileContents);
                        tw.Close();

                        sb = new StringBuilder();
                        currentLine = 1;
                        currentFile++;
                    }

                    currentLine++;
                }
            }


            fileName = targetFolder + "\\file-" + currentFile.ToString() + ".sql";
            fileContents = "use qspfulfillment" + System.Environment.NewLine + sb.ToString();

            tw = new StreamWriter(fileName);
            tw.Write(fileContents);
            tw.Close();

            sb = new StringBuilder();
            currentLine = 1;
            currentFile++;


            this.button6.Enabled = true;
        }

        private void CopySourceToAllowed()
        {
            this.zipCodesAllowed =
                (from zp in this.zipCodesSource
                 select zp
                 ).ToList();
        }
        private void CopySourceToDenied()
        {
            this.zipCodesDenied =
                (from zp in this.zipCodesSource
                 select zp
                 ).ToList();
        }
        private void GenerateDeniedFromAllowed()
        {
            for (int i = 0; i < 100000; i++)
            {
                string zipCode = i.ToString("00000");

                if (this.zipCodesAllowed.Contains(zipCode))
                {
                    // its allowed
                }
                else
                {
                    // its denied
                    // add to the list
                    this.zipCodesDenied.Add(zipCode);
                }
            }
        }
        private void GenerateAllowedFromDenied()
        {

        }
        private bool Should3DigitZipGoToDeniedList(string zip3digits)
        {
            bool result = true;

            //if (zipCodeAllowedString.Contains(zip3digits))
            //{
            //    // 3 digit zip is in the allowed list
            //    result = false;
            //}
            //else
            //{
            //    // 3 digit zip is NOT in the allowed list

            //    #region Look in 5 digit zips

            //    for (int i = 0; i < 10; i++)
            //    {
            //        string zip4digits = zip3digits + i.ToString();

            //        result = this.Should4DigitZipGoToDeniedList(zip4digits);

            //        if (!result)
            //        {
            //            break;
            //        }
            //    }

            //    #endregion
            //}

            return result;
        }
        private bool Should4DigitZipGoToDeniedList(string zip4digits)
        {
            bool result = true;

            //if (zipCodeAllowedString.Contains(zip4digits))
            //{
            //    // 4 digit zip is in the allowed list
            //    result = false;
            //}
            //else
            //{
            //    // 4 digit zip is NOT in the allowed list

            //    #region Look in 5 digit zips

            //    for (int i = 0; i < 10; i++)
            //    {
            //        string zip5digits = zip4digits + i.ToString();

            //        result = this.Should5DigitZipGoToDeniedList(zip5digits);

            //        if (!result)
            //        {
            //            break;
            //        }
            //    }

            //    #endregion
            //}

            return result;
        }
        private bool Should5DigitZipGoToDeniedList(string zip5digits)
        {
            bool result = true;

            //if (zipCodeAllowedString.Contains(zip5digits))
            //{
            //    result = false;
            //}

            return result;
        }

        private void AddInsertToQueryBatch(StringBuilder sb, string formId, string zipCode, string zipCodeDescription, string isAllowed, string userId)
        {
            sb.AppendLine(
                String.Format(
                    "INSERT INTO [QSPFulfillment].[dbo].[form_permission_region] ([form_id] ,[zip] ,[description] ,[allow_read] ,[allow_write] ,[create_date] ,[create_user_id] ,[update_date] ,[update_user_id]) VALUES ({0} ,'{1}' ,'{2}' ,{3} ,{3} ,getdate() ,{4} ,getdate() ,{4})", 
                    formId, 
                    zipCode, 
                    zipCodeDescription, 
                    isAllowed, 
                    userId)
                );
        }




    }
}
