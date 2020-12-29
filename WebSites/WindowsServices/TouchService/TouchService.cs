//
// Nov 30, 2004. Stephen Lim - New class.
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace GA.BDC.Core.TouchService
{
	public class TouchService : System.ServiceProcess.ServiceBase
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private TouchWorker touchWorker;
		private Thread touchWorkerThread;

		public TouchService()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();
		}

		// The main entry point for the process
	/*	static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
	
			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
			//
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new TouchService() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}
      */
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// TouchService
			// 
			this.CanPauseAndContinue = true;
			this.ServiceName = System.Windows.Forms.Application.ProductName;
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
			// Start Touch service
			touchWorker = new TouchWorker();
			try 
			{
				touchWorkerThread = new Thread(new ThreadStart(touchWorker.Start));
				touchWorkerThread.Start();
			}
			catch {}
		}
 
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			try 
			{
				// Shutdown queue worker thread
				if (touchWorkerThread.ThreadState != System.Threading.ThreadState.Unstarted) 
				{
					touchWorkerThread.Abort();
					touchWorkerThread.Join(3000);
				}
			}
			catch {}
		}

		/// <summary>
		/// Pause this service.
		/// </summary>
		protected override void OnPause()
		{
			GlobalSettings.Enabled = false;
		}

		/// <summary>
		/// Resume this service.
		/// </summary>
		protected override void OnContinue()
		{
			GlobalSettings.Enabled = true;
		}

		/// <summary>
		/// Stop current service.
		/// </summary>
		public static void Stop() 
		{
			try 
			{
				ServiceController[] serviceControllers = ServiceController.GetServices();
				foreach (ServiceController sc in serviceControllers) 
				{
					if (sc.ServiceName == System.Windows.Forms.Application.ProductName) 
					{
						sc.Stop();
						break;
					}
				}
			}
			catch {}
		}
	}
}
