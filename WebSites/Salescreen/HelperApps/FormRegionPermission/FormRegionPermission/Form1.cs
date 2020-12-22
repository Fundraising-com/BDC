using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormRegionPermission
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
            this.comboBox1.SelectedIndex = 0;

            this.label3.Text = "";
            this.label5.Text = "";
            this.label7.Text = "";
            this.label8.Text = "";
            this.progressBar2.Visible = false;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetConnectionString();
            this.LoadSourceForms();
            this.LoadTargetForms();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadTargetForms();
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadTargetForms();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            this.button2.Enabled = true;

            this.startDate = DateTime.Now;
            this.totalItems = 0;
            this.processedItems = 0;
            this.currentStep = "";
            this.DisplayProgress();
            this.timer1.Enabled = true;
            this.textBox2.Text = "";
            this.progressBar2.Visible = true;

            WorkerParameter parameter = new WorkerParameter();
            parameter.SourceFormId = Convert.ToInt32(((ComboItem)this.comboBox2.SelectedItem).Id);
            parameter.TargetFormId = Convert.ToInt32(((ComboItem)this.comboBox3.SelectedItem).Id);
            parameter.ConnectionString = this.connectionString;

            backgroundWorker1.RunWorkerAsync(parameter);
        }
        private void button2_Click(object sender, EventArgs e)
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

            var totalQuery = from fpr in context.form_permission_regions
                             where fpr.form_id == parameter.SourceFormId
                             select fpr;

            count = totalQuery.Count();

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

            int result = context.ExecuteCommand("delete from form_permission_region where form_id = " + parameter.TargetFormId.ToString() + "");

            #endregion

            #region Add new data

            int itemsCreatedCount = 0;

            var sourceQuery = from fpr in context.form_permission_regions
                              where fpr.form_id == parameter.SourceFormId
                              select fpr;

            foreach (Data.Entity.form_permission_region sourceItem in sourceQuery)
            {
                #region Create new record

                Data.Entity.form_permission_region newItem = new Data.Entity.form_permission_region();

                newItem.form_id = parameter.TargetFormId;
                newItem.zip = sourceItem.zip;
                newItem.allow_read = sourceItem.allow_read;
                newItem.allow_write = sourceItem.allow_write;
                newItem.description = sourceItem.description;
                newItem.create_date = sourceItem.create_date;
                newItem.create_user_id = sourceItem.create_user_id;
                newItem.update_date = sourceItem.update_date;
                newItem.update_user_id = sourceItem.update_user_id;

                context.form_permission_regions.InsertOnSubmit(newItem);
                context.SubmitChanges();

                itemsCreatedCount++;

                #endregion

                #region Check for cancellation

                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                #endregion

                #region Send progress update

                WorkerProgress wp3 = new WorkerProgress();
                wp3.Message = String.Format("Adding item for zip code: '{0}'", newItem.zip);
                wp3.ProcessedItems = itemsCreatedCount;
                wp3.TotalItems = count;

                this.backgroundWorker1.ReportProgress(0, wp3);

                #endregion
            }

            #endregion
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            WorkerProgress wp = (WorkerProgress)e.UserState;

            this.totalItems = wp.TotalItems;
            this.processedItems = wp.ProcessedItems;
            this.currentStep = wp.Message;

            this.DisplayProgress();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.button1.Enabled = true;
            this.button2.Enabled = false;
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
            //this.DisplayProgress();
        }

        private void LoadSourceForms()
        {
            Data.QspFulfillmentDataContext context = new Data.QspFulfillmentDataContext();
            context.Connection.ConnectionString = this.connectionString;

            var distinctFormIds = (from fpr in context.form_permission_regions
                                   select fpr.form_id).Distinct();

            var forms = from f in context.forms
                        where distinctFormIds.Contains(f.form_id)
                        orderby f.form_id
                        select f;

            List<Data.Entity.form> formList = forms.ToList();

            this.comboBox2.Items.Clear();
            foreach (Data.Entity.form form in formList)
            {
                ComboItem item = new ComboItem();
                item.Id = form.form_id;
                item.Description = form.form_id.ToString() + " - " + form.form_name;

                this.comboBox2.Items.Add(item);
            }
        }
        private void LoadTargetForms()
        {
            bool removeDisabledForms = this.checkBox1.Checked;
            bool removeDeletedForms = this.checkBox1.Checked;

            Data.QspFulfillmentDataContext context = new Data.QspFulfillmentDataContext();
            context.Connection.ConnectionString = this.connectionString;

            var formQuery = from f in context.forms
                            select f;

            if (removeDisabledForms)
            {
                formQuery = from f in formQuery
                            where f.enabled == true
                            select f;
            }

            if (removeDeletedForms)
            {
                formQuery = from f in formQuery
                            where f.deleted == false
                            select f;
            }

            formQuery = from f in formQuery
                        orderby f.form_id
                        select f;

            List<Data.Entity.form> formList = formQuery.ToList();

            this.comboBox3.Items.Clear();
            foreach (Data.Entity.form form in formList)
            {
                ComboItem item = new ComboItem();
                item.Id = form.form_id;
                item.Description = form.form_id.ToString() + " - " + form.form_name;

                this.comboBox3.Items.Add(item);
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

            // Log
            this.textBox2.Text = this.currentStep + System.Environment.NewLine + this.textBox2.Text;
            this.textBox2.Refresh();
        }
        private void SetConnectionString()
        {
            this.connectionString = (string)this.comboBox1.SelectedItem;
        }

    }
}
