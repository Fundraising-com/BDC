using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Windows.Forms;
using System.Configuration;
using efundraising.EFundraisingCRM;
using efundraising.EFundraisingCRM.Fulfillment;
using efundraising.EFundraisingCRM.Linq;


namespace EFRSyncService
{
    public partial class Start : Form
    {
        private EFRSync efrSync;

        public Start()
        {
            InitializeComponent();
            efrSync = new EFRSync();
        }

        // The main entry point for the process
        static void Main(string[] args)
        {
            // Start application

            Application.Run(new Start());
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            efrSync.Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            efrSync.Stop("");
        }
            
          


            


        
    }
}
