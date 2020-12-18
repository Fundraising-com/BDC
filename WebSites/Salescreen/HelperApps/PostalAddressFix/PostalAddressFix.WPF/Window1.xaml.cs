using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Threading;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using PostalAddressFix.Logic;

namespace PostalAddressFix.WPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
//Data Source=qspdevdb1.qsp;Initial Catalog=QSPFulfillment;Integrated Security=True
//Data Source=qsptestdb1.qsp;Initial Catalog=QSPFulfillment;Integrated Security=True
//Data Source=qspstagdb1.qsp;Initial Catalog=QSPFulfillment;Integrated Security=True
//Data Source=qspproddb1.qsp;Initial Catalog=QSPFulfillment;Integrated Security=True

        private BackgroundWorker worker;
        private StringBuilder log = new StringBuilder();
        private int count = 0;
        private DateTime startDate;
        private DateTime endDate;

        public Window1()
        {
            InitializeComponent();
        }
        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            this.txtOutput.Text = "";
            this.txtBlockProgress.Text = "Loading...";
            this.btnExecute.IsEnabled = false;
            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = 1;
            this.progressBar.Value = 0;

            startDate = DateTime.Now;

            LogReset();

            LogText("Postal Address Fix");
            LogText("");
            LogText("Fixes postal address zip codes in the qspfulfillment database");
            LogText("-------------------------------------------------------------");
            LogText("");
            LogText("Start @ " + startDate.ToShortDateString() + " " + startDate.ToLongTimeString());
            LogText("");

            this.txtOutput.Text = log.ToString();

            FixWorkSettings workSettings = new FixWorkSettings();
            workSettings.ConnectionString = this.txtConnectionString.Text;
            if (Convert.ToBoolean(this.boolUsePaging.IsChecked))
            {
                workSettings.Page = Convert.ToInt32(this.txtPage.Text);
                workSettings.PageSize = Convert.ToInt32(this.txtItemsPerPage.Text);
            }
            else
            {
                workSettings.Page = null;
                workSettings.PageSize = 0;
            }
            workSettings.UpdateData = Convert.ToBoolean(this.boolUpdateData.IsChecked);
            workSettings.DelayInMiliseconds = Convert.ToInt32(this.txtDelay.Text);
            

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync(workSettings);

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.IsBackground = true;
            FixWorkLogic Fixer = new FixWorkLogic();
            Fixer.FixZipCodes(worker, (FixWorkSettings)e.Argument);
        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                FixWorkState State = (FixWorkState)e.UserState;

                if (State.NewZip != null)
                {
                    if (!State.IsZipValid && State.NewZip.Length > 0)
                    {
                        LogText(string.Format("PostalAddressId = {0}, OriginalZip = '{1}', NewZip = '{2}'", State.PostalAddressId, State.OriginalZip, State.NewZip));
                        count++;
                    }
                }

                if (State.Total > 0)
                {
                    string percentage = ((Convert.ToDouble(State.Count) / Convert.ToDouble(State.Total)) * 100).ToString("0.00");
                    this.txtBlockProgress.Text = String.Format("{0} / {1} = {2}%", State.Count, State.Total, percentage);
                    this.txtBlockUpdates.Text = count.ToString();
                    this.progressBar.Minimum = 0;
                    this.progressBar.Maximum = State.Total;
                    this.progressBar.Value = State.Count;
                }
                
            }
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            endDate = DateTime.Now;
            TimeSpan ts = new TimeSpan(endDate.Ticks - startDate.Ticks);

            LogText("");
            LogText("End @ " + endDate.ToShortDateString() + " " + endDate.ToLongTimeString());
            LogText("");
            LogText("Elapsed time: " + ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds);
            LogText(count.ToString() + " zip codes updated");

            this.txtOutput.Text = log.ToString();

            LogSaveToDisk();

            this.btnExecute.IsEnabled = true;
        }

        private void LogReset()
        {
            log = new StringBuilder();
        }
        private void LogText(string text)
        {
            log.AppendLine(text);
        }
        private void LogSaveToDisk()
        {
            try
            {
                string fileName = string.Format("log-{0}-{1}-{2}-{3}.txt", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), DateTime.Now.Ticks.ToString());
                string path = Environment.CurrentDirectory;

                TextWriter tw = new StreamWriter(path + "\\" + fileName);
                tw.Write(log.ToString());
                tw.Close();
            }
            catch (Exception ex)
            {
                // Ignore error
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PostalAddressFix.WPF.Properties.Settings settings = new PostalAddressFix.WPF.Properties.Settings();
            this.txtConnectionString.Text = settings.ConnectionString; 
        }

    }
}
