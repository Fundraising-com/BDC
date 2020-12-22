using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;


namespace EFRSyncService
{
    public partial class EFRSyncService : ServiceBase
    {
        Thread efrSyncThread = null;
        private EFRSync efrSync = null;

        public EFRSyncService()
        {
            InitializeComponent();
        }


        // The main entry point for the process
        static void Main()
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;

            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new LeadManagerService(), new MySecondUserService()};
            //
            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new EFRSyncService() };

            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }

        protected override void OnStart(string[] args)
        {
            efrSync = new EFRSync();
            efrSyncThread = new Thread(new ThreadStart(efrSync.Start));
            efrSyncThread.Start();
        }

        protected override void OnStop()
        {
            efrSync.Stop("");
            efrSync = null;
        }

        protected override void OnPause()
        {
            efrSyncThread.Suspend();
        }

        protected override void OnContinue()
        {
            efrSyncThread.Resume();
        }
    }
}
