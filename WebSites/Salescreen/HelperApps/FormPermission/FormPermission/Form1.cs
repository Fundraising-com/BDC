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
        private List<ZipCodeItem> zipCodesSource = new List<ZipCodeItem>();
        private List<ZipCodeItem> zipCodesAllowed = new List<ZipCodeItem>();
        private List<ZipCodeItem> zipCodesDenied = new List<ZipCodeItem>();
        private List<string> zipCodeAllowedString = new List<string>();
        private List<string> zipCodeDeniedString = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.btnParse.Enabled = false;

            this.zipCodesSource = new List<ZipCodeItem>();
            this.zipCodesAllowed = new List<ZipCodeItem>();
            this.zipCodesDenied = new List<ZipCodeItem>();

            #region Read values from CSV

            // The comma separated values have two values
            // First = State / description
            // Second = zip code
            // Zip codes can ve 1 - 5 digits
            // Zip codes will then be expanded to 5 digits (1234 will yield 12340, 12341, 12342 ... )

            using (StreamReader sr = new StreamReader(this.tbxCSVFile.Text))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] rowArray = line.Split(',');

                    ZipCodeItem newZipCodeItem = new ZipCodeItem();
                    newZipCodeItem.Description = rowArray[1].Trim();
                    newZipCodeItem.ZipCode = rowArray[0].Trim();

                    this.zipCodesSource.Add(newZipCodeItem);
                }
            }

            #endregion

            #region Generate allowed / denied zip code lists

            if (this.rbnSourceAllowed.Checked)
            {
                // Source zip codes are allowed
                this.CopySourceToAllowed();
                this.GenerateDeniedFromAllowed();
            }
            else if (this.rbnSourceDenied.Checked)
            {
                // Source zip codes are denied
                this.CopySourceToDenied();
                this.GenerateAllowedFromDenied();
            }

            #endregion

            this.label7.Text = String.Format("Allowed zip codes ({0})", this.zipCodesAllowed.Count);
            this.label8.Text = String.Format("Denied zip codes ({0})", this.zipCodesDenied.Count);

            this.dataGridView1.DataSource = this.zipCodesSource;
            this.dataGridView2.DataSource = this.zipCodesAllowed;
            this.dataGridView3.DataSource = this.zipCodesDenied;

            this.btnParse.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.btnGenerateSQL.Enabled = false;
            this.tbxSQL.Text = string.Empty;
            StringBuilder sb = new StringBuilder();

            string isAllowed = "";
            string formId = this.tbxFormID.Text.Trim();
            string userId = tbxUserID.Text.Trim();
            bool generateAllowed = this.rbnFormIDAllowed.Checked;
            bool generateDenied = this.rbnFormIDDenied.Checked;
            bool generateBoth = this.rbnFormIDBoth.Checked;

            if (generateAllowed || generateBoth)
            {
                sb.AppendLine("-- Allowed zip codes --");
                sb.AppendLine("-- ----------------- --");

                isAllowed = "1";
                foreach (ZipCodeItem zip in this.zipCodesAllowed)
                {
                    zip.Description = zip.Description.Replace("'", "");
                    zip.Description = "";

                    this.AddInsertToQueryBatch(sb, formId, zip.ZipCode, zip.Description, isAllowed, userId);
                }

                sb.AppendLine("-- ----------------- --");
            }
            if (generateDenied || generateBoth)
            {
                sb.AppendLine("-- Denied zip codes --");
                sb.AppendLine("-- ---------------- --");

                isAllowed = "0";
                foreach (ZipCodeItem zip in this.zipCodesDenied)
                {
                    this.AddInsertToQueryBatch(sb, formId, zip.ZipCode, zip.Description, isAllowed, userId);
                }

                sb.AppendLine("-- ----------------- --");
            }

            this.tbxSQL.Text = sb.ToString();

            this.btnGenerateSQL.Enabled = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.button3.Enabled = false;

            DialogResult result = this.openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.tbxCSVFile.Text = this.openFileDialog1.FileName;
            }

            this.button3.Enabled = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.btnToClipboard.Enabled = false;

            this.tbxSQL.SelectAll();
            this.tbxSQL.Copy();

            this.btnToClipboard.Enabled = true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            List<ZipCodeItem> allowed = null, denied = null, nonFlagged = null, doubleFlagged = null;

            // Generate data for all zip codes

            this.btnGenerateFullData.Enabled = false;

            List<string> fullAllowedZipList = new List<string>();
            List<string> fullDeniedZipList = new List<string>();
            List<ZipCodeItem> fullZipList = new List<ZipCodeItem>(100000);

            #region Old Code for Generate allowed, denied zip

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
            //        //fullDeniedZipList.Add(zip);
            //    }
            //}

            //#endregion

            #endregion

            //If the CSV file is allow list
            if (rbnSourceAllowed.Checked)
            {
                #region Generate full allowed zip list

                //starts from row 2, row 1 being the header
                for (int j = 1; j < zipCodeAllowedString.Count; j++)
                {
                    string zip = this.zipCodeAllowedString[j];
                    if (zip.Length == 3)
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            string newZip = zip + i.ToString("00");

                            fullAllowedZipList.Add(newZip);
                        }
                    }
                    else if (zip.Length == 4)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            string newZip = zip + i.ToString("0");

                            fullAllowedZipList.Add(newZip);
                        }
                    }
                    else if (zip.Length == 5)
                    {
                        fullAllowedZipList.Add(zip);
                    }
                }

                #endregion

                #region Generate full zip list

                for (int i = 0; i < 100000; i++)
                {
                    string zip = i.ToString("00000");

                    ZipCodeItem zipCodeItem = new ZipCodeItem();

                    zipCodeItem.ZipCode = zip;
                    zipCodeItem.Description = "";
                    zipCodeItem.IsAllowed = fullAllowedZipList.Contains(zip);
                    zipCodeItem.IsDenied = false;

                    fullZipList.Add(zipCodeItem);
                }

                #endregion

                allowed = (from fzl in fullZipList
                           where fzl.IsAllowed == true
                              && fzl.IsDenied == false
                           select fzl
                              ).ToList();

                denied = (from fzl in fullZipList
                          where fzl.IsAllowed == false
                             && fzl.IsDenied == false
                          select fzl
                     ).ToList();
            }

            //If the CSV file is deny list
            if (rbnSourceDenied.Checked)
            {
                #region Generate full denied zip list

                for (int j = 1; j < zipCodeDeniedString.Count; j++)
                {
                    string zip = this.zipCodeDeniedString[j];
                    if (zip.Length == 3)
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            string newZip = zip + i.ToString("00");

                            fullDeniedZipList.Add(newZip);
                        }
                    }
                    else if (zip.Length == 4)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            string newZip = zip + i.ToString("0");

                            fullDeniedZipList.Add(newZip);
                        }
                    }
                    else if (zip.Length == 5)
                    {
                        fullDeniedZipList.Add(zip);
                    }
                }

                #endregion

                #region Generate full zip list

                for (int i = 0; i < 100000; i++)
                {
                    string zip = i.ToString("00000");

                    ZipCodeItem zipCodeItem = new ZipCodeItem();

                    zipCodeItem.ZipCode = zip;
                    zipCodeItem.Description = "";
                    zipCodeItem.IsAllowed = false;
                    zipCodeItem.IsDenied = fullDeniedZipList.Contains(zip);

                    fullZipList.Add(zipCodeItem);
                }

                #endregion

                denied = (from fzl in fullZipList
                          where fzl.IsAllowed == false
                             && fzl.IsDenied == true
                          select fzl
                               ).ToList();

                allowed = (from fzl in fullZipList
                           where fzl.IsAllowed == false
                              && fzl.IsDenied == false
                           select fzl
                     ).ToList();
            }

            nonFlagged =
               (from fzl in fullZipList
                where fzl.IsAllowed == false
                   && fzl.IsDenied == false
                select fzl
                ).ToList();

            doubleFlagged =
                (from fzl in fullZipList
                 where fzl.IsAllowed == true
                    && fzl.IsDenied == true
                 select fzl
                 ).ToList();




            this.zipCodesAllowed = allowed;
            this.zipCodesDenied = denied;


            //int nonFlaggedCount = nonFlagged.Count;
            //int doubleFlaggedCount = doubleFlagged.Count;
            //int allowedCount = allowed.Count;
            //int deniedCount = denied.Count;

            this.btnGenerateFullData.Enabled = true;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.btnToDisk.Enabled = false;

            StringBuilder sb = new StringBuilder();
            string targetFolder = @"c:\temp";
            if (!Directory.Exists(@"c:\temp"))
                Directory.CreateDirectory(@"c:\temp");
            int maxLinesPerFile = 10000;
            int currentLine = 1;
            int currentFile = 1;
            string fileName;
            string fileContents;
            TextWriter tw;

            using (StringReader reader = new StringReader(this.tbxSQL.Text))
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


            this.btnToDisk.Enabled = true;
        }

        private void CopySourceToAllowed()
        {
            //foreach (ZipCodeItem currentZipCodeItem in this.zipCodesSourceExpanded)
            //{
            //    ZipCodeItem newZipCodeItem = new ZipCodeItem();
            //    newZipCodeItem.Description = currentZipCodeItem.Description;
            //    newZipCodeItem.ZipCode = currentZipCodeItem.ZipCode;

            //    this.zipCodesAllowed.Add(newZipCodeItem);
            //}

            this.zipCodesAllowed =
                (from zp in this.zipCodesSource
                 select zp
                 ).ToList();

            this.zipCodeAllowedString =
                (from zp in this.zipCodesAllowed
                 select zp.ZipCode
                 ).ToList();
        }
        private void CopySourceToDenied()
        {
            //foreach (ZipCodeItem currentZipCodeItem in this.zipCodesSourceExpanded)
            //{
            //    ZipCodeItem newZipCodeItem = new ZipCodeItem();
            //    newZipCodeItem.Description = currentZipCodeItem.Description;
            //    newZipCodeItem.ZipCode = currentZipCodeItem.ZipCode;

            //    this.zipCodesDenied.Add(newZipCodeItem);
            //}

            this.zipCodesDenied =
                (from zp in this.zipCodesSource
                 select zp
                 ).ToList();

            this.zipCodeDeniedString =
                (from zp in this.zipCodesDenied
                 select zp.ZipCode
                 ).ToList();
        }
        private void GenerateDeniedFromAllowed()
        {
            // This might be buggy

            List<ZipCodeItem> result = new List<ZipCodeItem>();

            for (int i = 0; i < 1000; i++)
            {
                string zip3digits = i.ToString("000");

                if (this.Should3DigitZipGoToDeniedList(zip3digits))
                {
                    ZipCodeItem newZipCodeItem = new ZipCodeItem();

                    newZipCodeItem.Description = "";
                    newZipCodeItem.ZipCode = zip3digits;

                    this.zipCodesDenied.Add(newZipCodeItem);
                }
                else
                {
                    #region Check 4 digit zip codes

                    for (int j = 0; j < 10; j++)
                    {
                        string zip4digits = zip3digits + j.ToString();

                        if (this.Should4DigitZipGoToDeniedList(zip4digits))
                        {
                            ZipCodeItem newZipCodeItem = new ZipCodeItem();

                            newZipCodeItem.Description = "";
                            newZipCodeItem.ZipCode = zip4digits;

                            this.zipCodesDenied.Add(newZipCodeItem);
                        }
                        else
                        {
                            #region Check 5 digit zip codes

                            for (int k = 0; k < 10; k++)
                            {
                                string zip5digits = zip4digits + k.ToString();

                                if (this.Should5DigitZipGoToDeniedList(zip5digits))
                                {
                                    ZipCodeItem newZipCodeItem = new ZipCodeItem();

                                    newZipCodeItem.Description = "";
                                    newZipCodeItem.ZipCode = zip5digits;

                                    this.zipCodesDenied.Add(newZipCodeItem);
                                }
                            }

                            #endregion
                        }
                    }

                    #endregion
                }
            }
        }
        private void GenerateAllowedFromDenied()
        {
            for (int i = 0; i < 100000; i++)
            {
                int count = (from zp in this.zipCodesDenied
                             where zp.ZipCode == i.ToString("00000")
                             select zp
                                ).Count();

                if (count > 0)
                {
                    // Zip is in the denied list
                    // Do nothing
                }
                else
                {
                    // Zip is not in denied list
                    // Add to allowed
                    ZipCodeItem newZipCodeItem = new ZipCodeItem();
                    newZipCodeItem.Description = "";
                    newZipCodeItem.ZipCode = i.ToString("00000");

                    this.zipCodesAllowed.Add(newZipCodeItem);
                }
            }
        }
        private bool Should3DigitZipGoToDeniedList(string zip3digits)
        {
            bool result = true;

            if (zipCodeAllowedString.Contains(zip3digits))
            {
                // 3 digit zip is in the allowed list
                result = false;
            }
            else
            {
                // 3 digit zip is NOT in the allowed list

                #region Look in 5 digit zips

                for (int i = 0; i < 10; i++)
                {
                    string zip4digits = zip3digits + i.ToString();

                    result = this.Should4DigitZipGoToDeniedList(zip4digits);

                    if (!result)
                    {
                        break;
                    }
                }

                #endregion
            }

            return result;
        }
        private bool Should4DigitZipGoToDeniedList(string zip4digits)
        {
            bool result = true;

            if (zipCodeAllowedString.Contains(zip4digits))
            {
                // 4 digit zip is in the allowed list
                result = false;
            }
            else
            {
                // 4 digit zip is NOT in the allowed list

                #region Look in 5 digit zips

                for (int i = 0; i < 10; i++)
                {
                    string zip5digits = zip4digits + i.ToString();

                    result = this.Should5DigitZipGoToDeniedList(zip5digits);

                    if (!result)
                    {
                        break;
                    }
                }

                #endregion
            }

            return result;
        }
        private bool Should5DigitZipGoToDeniedList(string zip5digits)
        {
            bool result = true;

            if (zipCodeAllowedString.Contains(zip5digits))
            {
                result = false;
            }

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
