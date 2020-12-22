using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using PostalAddressFix.Logic;

namespace PostalAddressFix.Console
{
    class Program
    {
        static BackgroundWorker worker;
        static StringBuilder log = new StringBuilder();
        static int count = 0;
        static DateTime startDate;
        static DateTime endDate;

        static void Main(string[] args)
        {
            startDate = DateTime.Now;

            LogText("Postal Address Fix");
            LogText("");
            LogText("Fixes postal address zip codes in the qspfulfillment database");
            LogText("-------------------------------------------------------------");
            LogText("");
            LogText("Start @ " + startDate.ToShortDateString() + " " + startDate.ToLongTimeString());
            LogText("");


            PostalAddressFix.Console.Properties.Settings appSettings = new PostalAddressFix.Console.Properties.Settings();

            FixWorkSettings workSettings = new FixWorkSettings();
            workSettings.ConnectionString = appSettings.ConnectionString;
            workSettings.Page = null;
            workSettings.PageSize = 0;
            workSettings.UpdateData = true;
            workSettings.DelayInMiliseconds = 0;


            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync(workSettings);
            
            System.Console.ReadKey();
        }

        static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.IsBackground = true;
            FixWorkLogic Fixer = new FixWorkLogic();
            Fixer.FixZipCodes(worker, (FixWorkSettings)e.Argument);
        }
        static void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                FixWorkState State = (FixWorkState)e.UserState;

                if (State.OriginalZip != State.NewZip)
                {
                    LogText(string.Format("PostalAddressId = {0}, OriginalZip = '{1}', NewZip = '{2}'", State.PostalAddressId, State.OriginalZip, State.NewZip));
                    count++;
                }
                
            }
        }
        static void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            endDate = DateTime.Now;
            TimeSpan ts = new TimeSpan(endDate.Ticks - startDate.Ticks);

            LogText("");
            LogText("End @ " + endDate.ToShortDateString() + " " + endDate.ToLongTimeString());
            LogText("");
            LogText("Elapsed time: " + ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds);
            LogText(count.ToString() + " zip codes updated");

            WriteLogToDisk();

            Environment.Exit(0);
        }

        static void LogText(string text)
        {
            System.Console.WriteLine(text);
            log.AppendLine(text);
        }
        static void WriteLogToDisk()
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
    }
}
