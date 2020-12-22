using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeliveryServiceDataImporter
{
    public partial class Form1 : Form
    {
        private DateTime startDate;
        private int totalItems = 0;
        private int processedItems = 0;
        private string currentStep = "";
        private string connectionString = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox2.SelectedIndex = 0;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadFormList();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetConnectionString();
            this.LoadFormList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filename = this.openFileDialog1.FileName;
                this.textBox1.Text = filename;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.button2.Enabled = false;
            this.button3.Enabled = true;

            this.startDate = DateTime.Now;
            this.totalItems = 0;
            this.processedItems = 0;
            this.currentStep = "";
            this.DisplayProgress();
            this.timer1.Enabled = true;

            WorkerParameter parameter = new WorkerParameter();
            parameter.FormId = Convert.ToInt32(((ComboItem)this.comboBox1.SelectedItem).Id);
            parameter.FileName = this.textBox1.Text;
            parameter.ConnectionString = this.connectionString;

            backgroundWorker1.RunWorkerAsync(parameter);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerParameter parameter = (WorkerParameter)e.Argument;

            Data.QspFulfillmentDataContext context = new Data.QspFulfillmentDataContext();
            context.Connection.ConnectionString = parameter.ConnectionString;

            #region Count new items

            #region Check for cancellation

            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            #endregion

            #region Send progress update

            WorkerProgress wp1 = new WorkerProgress();
            wp1.Message = "Getting item count";
            wp1.ProcessedItems = 0;
            wp1.TotalItems = 0;

            this.backgroundWorker1.ReportProgress(0, wp1);

            #endregion

            int count = 0;
            string inputCount = null;
            StreamReader readerCount = File.OpenText(parameter.FileName);

            while ((inputCount = readerCount.ReadLine()) != null)
            {
                count++;
            }

            readerCount.Close();

            #endregion

            #region Delete existing data

            #region Check for cancellation

            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            #endregion

            #region Send progress update

            WorkerProgress wp2 = new WorkerProgress();
            wp2.Message = "Deleting existing data";
            wp2.ProcessedItems = 0;
            wp2.TotalItems = 0;

            this.backgroundWorker1.ReportProgress(0, wp2);

            #endregion

            int result = context.ExecuteCommand("delete from delivery_service where form_id = " + parameter.FormId.ToString() + "");

            #endregion

            #region Add new data

            string input = null;
            int itemsCreatedCount = 0;
            int processedCount = 0;
            StreamReader reader = File.OpenText(parameter.FileName);

            while ((input = reader.ReadLine()) != null)
            {
                #region Parse text

                string[] strinvValues = input.Split(',');
                string string1 = "";
                string string2 = "";
                string string3 = "";

                string1 = strinvValues[0];
                string2 = strinvValues[1];
                if (strinvValues.Length > 2)
                {
                    string3 = strinvValues[2];
                }

                #endregion

                #region Prepare data

                string zipCode = "";
                int deliveryServiceTypeId = 0;
                int transitDays = 0;

                zipCode = Convert.ToInt32(string1).ToString("00000");
                if (string2 == "Standard Service Area")
                {
                    deliveryServiceTypeId = 1;
                }
                else if (string2 == "Stretch Service Area")
                {
                    deliveryServiceTypeId = 2;
                }
                else
                {
                    deliveryServiceTypeId = 3;
                }

                if (string3.Trim().Length > 0)
                {
                    transitDays = Convert.ToInt32(string3);
                }

                #endregion

                #region Create new record

                Data.Entity.delivery_service newItem = new Data.Entity.delivery_service();

                newItem.form_id = parameter.FormId;
                newItem.zip_code = zipCode;
                newItem.delivery_service_type_id = deliveryServiceTypeId;
                newItem.transit_days = transitDays;

                context.delivery_services.InsertOnSubmit(newItem);

                itemsCreatedCount++;

                //context.SubmitChanges();
                //System.Threading.Thread.Sleep(10);

                #endregion

                #region Save new records

                if (itemsCreatedCount == 50)
                {
                    context.SubmitChanges();
                    itemsCreatedCount = 0;
                }

                #endregion

                processedCount++;

                #region Check for cancellation

                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                #endregion

                #region Send progress update

                WorkerProgress wp3 = new WorkerProgress();
                wp3.Message = String.Format("Adding item for zip code: '{0}'", newItem.zip_code);
                wp3.ProcessedItems = processedCount;
                wp3.TotalItems = count;

                this.backgroundWorker1.ReportProgress(0, wp3);

                #endregion
            }

            context.SubmitChanges();

            reader.Close();

            #endregion
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            WorkerProgress wp = (WorkerProgress)e.UserState;

            this.totalItems = wp.TotalItems;
            this.processedItems = wp.ProcessedItems;
            this.currentStep = wp.Message;

            //this.DisplayProgress();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.button2.Enabled = true;
            this.button3.Enabled = false;
            this.DisplayProgress();
            this.timer1.Enabled = false;

            if (e.Cancelled)
            {
                this.label3.Text = "Cancelled!";
            }
            else
            {
                this.label3.Text = "Done";
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.DisplayProgress();
        }

        private void LoadFormList()
        {
            bool loadOnlyEnabledForms = this.checkBox1.Checked;

            Data.QspFulfillmentDataContext context = new Data.QspFulfillmentDataContext();
            context.Connection.ConnectionString = this.connectionString;

            var formQuery = from f in context.forms
                            where f.deleted == false
                            select f;

            if (loadOnlyEnabledForms)
            {
                formQuery = from f in formQuery
                            where f.enabled == true
                            select f;
            }

            List<Data.Entity.form> formList = formQuery.ToList();

            this.comboBox1.Items.Clear();
            foreach (Data.Entity.form form in formList)
            {
                ComboItem item = new ComboItem();
                item.Id = form.form_id;
                item.Description = form.form_id.ToString() + " - " + form.form_name;
                
                this.comboBox1.Items.Add(item);
            }
        }
        private void DisplayProgress()
        {
            // Elapsed time
            long elapsedTicks = DateTime.Now.Ticks - this.startDate.Ticks;
            TimeSpan elapsedTime = new TimeSpan(elapsedTicks);
            this.label8.Text = String.Format("Elapsed: {0} days, {1} hours, {2} minutes, {3} seconds", elapsedTime.Days, elapsedTime.Hours.ToString(), elapsedTime.Minutes.ToString(), elapsedTime.Seconds.ToString());
            this.label8.Refresh();

            // Remaining time
            if (this.totalItems == 0 || this.processedItems == 0)
            {
                this.label7.Text = "";
                this.label7.Refresh();
            }
            else
            {
                long ticksPerItem = elapsedTicks / this.processedItems;
                int remainingItems = this.totalItems - this.processedItems;
                long remainingTicks = ticksPerItem * remainingItems;
                TimeSpan remainingTime = new TimeSpan(remainingTicks);

                this.label7.Text = String.Format("Remaining: {0} days, {1} hours, {2} minutes, {3} seconds", remainingTime.Days, remainingTime.Hours.ToString(), remainingTime.Minutes.ToString(), remainingTime.Seconds.ToString());
                this.label7.Refresh();
            }

            // Current step
            this.label3.Text = this.currentStep;
            this.label3.Refresh();

            // Progress percentage
            if (this.totalItems == 0)
            {
                this.label5.Text = "";
                this.label5.Refresh();
            }
            else
            {
                double percentage = (Convert.ToDouble(this.processedItems) / Convert.ToDouble(this.totalItems)) * 100;

                this.label5.Text = String.Format("{0} / {1} = {2} %", this.processedItems.ToString(), this.totalItems.ToString(), percentage.ToString("0.0000"));
                this.label5.Refresh();
            }

            // Progress bar
            this.progressBar2.Maximum = this.totalItems;
            this.progressBar2.Minimum = 0;
            this.progressBar2.Value = this.processedItems;
            this.progressBar2.Refresh();
        }
        private void SetConnectionString()
        {
            this.connectionString = (string)this.comboBox2.SelectedItem;
        }

    }
}
