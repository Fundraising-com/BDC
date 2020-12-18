//
// 2006-07-05 : Maxime Normand - New class.
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace efundraising.LeadManager
{
	public class LeadManagerService : System.ServiceProcess.ServiceBase
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private TempLeadIntegrator tempLeadIntegrator = null;
		Thread tempLeadIntegratorThread = null;

		public LeadManagerService()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();

		}

		// The main entry point for the process
        //static void Main()
        //{
        //    System.ServiceProcess.ServiceBase[] ServicesToRun;

        //    // More than one user Service may run within the same process. To add
        //    // another service to this process, change the following line to
        //    // create a second service object. For example,
        //    //
        //    //   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new LeadManagerService(), new MySecondUserService()};
        //    //
        //    ServicesToRun = new System.ServiceProcess.ServiceBase[] { new LeadManagerService() };

        //    System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        //}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			this.ServiceName = "LeadManagerService";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			tempLeadIntegrator = new TempLeadIntegrator();
			tempLeadIntegratorThread = new Thread(new ThreadStart(tempLeadIntegrator.Start));
			tempLeadIntegratorThread.Start();
		}
 
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			tempLeadIntegrator.Stop();
			tempLeadIntegrator = null;
		}
		
		protected override void OnPause()
		{
			tempLeadIntegratorThread.Suspend();
		}
		
		protected override void OnContinue()
		{
			tempLeadIntegratorThread.Resume();
		}



	}
}
