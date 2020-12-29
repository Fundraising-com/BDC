//
// 2005-05-15 - Stephen Lim - New class.
//
using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace GA.BDC.Core.Diagnostics
{
	/// <summary>
	/// Summary description for ApplicationEnvironment.
	/// </summary>
	public class ApplicationEnvironment
	{
		#region Fields
		private string _machineName = "";
		private string _processId = "";
		private string _processName = "";
		private string _threadId = "";
		private string _windowsId = "";
		private string _appDomain = "";
		private string _productName = "";
		#endregion

		#region Constructors
		private ApplicationEnvironment()
		{
			
		}
		#endregion

		#region Methods
		public static ApplicationEnvironment NewApplicationEnvironment()
		{
			ApplicationEnvironment appEnv = new ApplicationEnvironment();

			
			try 
			{
				appEnv.MachineName = Environment.MachineName;
			}
			catch {}					

			try 
			{
				appEnv.ProcessId = Process.GetCurrentProcess().Id.ToString();
			}
			catch {}
						
			try 
			{
				// NOTE: Do not use Process.GetCurrentProcess().ProcessName as it is
				// extremely slow (10x slower). Use AppDomain.CurrentDomain.FriendlyName
				// instead.
				appEnv.ProcessName = System.AppDomain.CurrentDomain.FriendlyName;
			}
			catch {}

			try 
			{
				appEnv.ThreadId = Thread.CurrentPrincipal.Identity.Name;
			}
			catch {}

			try 
			{
				appEnv.WindowsId = WindowsIdentity.GetCurrent().Name;
			}
			catch {}

			try 
			{
				appEnv.AppDomain = System.AppDomain.CurrentDomain.FriendlyName;
			}
			catch {}

			try 
			{
				appEnv.ProductName = Application.ProductName + " " + Application.ProductVersion;
			}
			catch {}

			return appEnv;
		}
		#endregion

		#region Properties
		public string MachineName
		{
			get { return _machineName; }
			set { _machineName = value; }
		}

		public string ProcessId
		{
			get { return _processId; }
			set { _processId = value; }
		}

		public string ProcessName
		{
			get { return _processName; }
			set { _processName = value; }
		}

		public string ThreadId
		{
			get { return _threadId; }
			set { _threadId = value; }
		}

		public string WindowsId
		{
			get { return _windowsId; }
			set { _windowsId = value; }
		}

		public string AppDomain
		{
			get { return _appDomain; }
			set { _appDomain = value; }
		}

		public string ProductName
		{
			get { return _productName; }
			set { _productName = value; }
		}
		#endregion
	}
}
