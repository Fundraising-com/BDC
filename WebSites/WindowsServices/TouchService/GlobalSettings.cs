//
// 2004-11-30 - Stephen Lim - New class.
// 2005-02-24 - Stephen Lim - Add support for TouchThrottleObjects
//

using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.ServiceProcess;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.Diagnostics;
using GA.BDC.Core.Email.MassMail;


namespace GA.BDC.Core.TouchService
{
	/// <summary>
	/// Store global settings.
	/// </summary>
	/// <remarks>
	/// Static and instance members are not thread-safe. You should use the Lock and Unlock methods
	/// to synchronize access for thread-safety. You must pay careful attention to release the lock
	/// to allow others to access the class.
	/// </remarks>
	/// <example>
	/// The example below shows how to use the GlobalSettings object to store
	/// settings.
	/// <code>
	/// try {
	///		GlobalSettings.Lock();
	///		GlobalSettings.Enabled = false;
	///	}
	///	finally {
	///		GlobalSettings.Unlock();
	///	}
	/// </code>
	/// </example>
	public sealed class GlobalSettings
	{
		#region Enums
		enum LogLevel 
		{
			
		}
		#endregion

		#region Fields
		private static readonly object syncRoot = new object();
		private static bool enabled;
		private static bool haltOnError;
		private static int logLevel;
		private static long queueInterval;
		private static Hashtable throttleObjects;
		private static EmailQueue.MailService mailService;
        private static short daysDue;
		#endregion
	
		#region Constructors
		/// <summary>
		/// Static constructor to initialize static variables.
		/// </summary>
		static GlobalSettings()
		{
			// Default values
			enabled = true;
			haltOnError = true;
			logLevel = 1;
			queueInterval = 10000;
			throttleObjects = new Hashtable();
		}
		#endregion

		#region Methods
		/// <summary>
		/// Read values from XML configuration file.
		/// </summary>
		public static void LoadSettingsFromConfig() 
		{
			// Read halt on error flag from config
			try 
			{
				string val = ApplicationSettings.GetConfig()["Touch.HaltOnError"].ToLower();

				if (val == "true") 
					GlobalSettings.HaltOnError = true;
				else if (val == "false")
					GlobalSettings.HaltOnError = false;
				else 
				{
					Logger.LogError("TouchHaltOnError configuration is invalid.");
				}
			}
			catch {}

			// Read interval from config
			try 
			{
				long val = Convert.ToInt64(ApplicationSettings.GetConfig()["Touch.QueueInterval"]);
				
				if (val >= 0)
					GlobalSettings.QueueInterval = val;
				else 
					Logger.LogError("TouchQueueInterval is ignored because it is less than zero.");
			}
			catch {}
			

			// Read interval from config
			try 
			{
				int val = Convert.ToInt32(ApplicationSettings.GetConfig()["Touch.QueueInterval"]);
				if (val >= 0)
					GlobalSettings.QueueInterval = val;
				else 
					Logger.LogError("TouchQueueInterval configuration is ignored because it is less than zero.");
			}
			catch {}


			// Read optional throttle objects from config
			try 
			{
				for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("Touch.ThrottleObject"); i++)
				{
					if (ApplicationSettings.GetConfig()["Touch.ThrottleObject", i, "object"] != "")
					{
						// Increment value by 1 because counter should start from 1 and not from zero.
						throttleObjects.Add(ApplicationSettings.GetConfig()["Touch.ThrottleObject", i, "object"], 
							ApplicationSettings.GetConfig()["Touch.ThrottleObject", i, "factor"] + 1);
					}
				}
			}
			catch {}

			//KO: Read MailService option from config
			try 
			{
				string val = ApplicationSettings.GetConfig()["Touch.MailService"].ToLower();

				if (val == "komunikator") 
					GlobalSettings.MailService = EmailQueue.MailService.Komunikator;
				else if (val == "massmailer")
					GlobalSettings.MailService = EmailQueue.MailService.MassMailer;
				else 
				{
					Logger.LogError("TouchMailService configuration is invalid.");
				}
			}
			catch {}
            try
            {
                GlobalSettings.DaysDue = Convert.ToInt16(ApplicationSettings.GetConfig()["Touch.DueDays"]);
                if (GlobalSettings.DaysDue < 0)
                {
                    Logger.LogError("TouchMailService for DueDays configuration is have invalid data: ".ToString() + GlobalSettings.DaysDue.ToString());
                }
            }
            catch(Exception ex)
            {
                Logger.LogError("TouchMailService for DueDays configuration is invalid.".ToString(), ex);
            }
                
        
		}

		/// <summary>
		///  Hold lock.
		/// </summary>
		public static void Lock()
		{
			Monitor.Enter(syncRoot);
		}

		/// <summary>
		/// Release lock.
		/// </summary>
		public static void Unlock() 
		{
			Monitor.Exit(syncRoot);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Get or set the flag to start or stop server.
		/// </summary>
		public static bool Enabled 
		{
			get {return enabled;}
			set {enabled = value;}
		}

		/// <summary>
		/// Get or set the flag to stop server on error.
		/// </summary>
		public static bool HaltOnError 
		{
			get {return haltOnError;}
			set {haltOnError = value;}
		}

		/// <summary>
		/// Get or set the amount of time in milliseconds 
		/// to throttle each thread worker.
		/// </summary>
		public static long QueueInterval 
		{
			get {return queueInterval;}
			set {queueInterval = value;}
		}

		/// <summary>
		/// Get or set the objects to throttle.
		/// </summary>
		public static Hashtable ThrottleObjects 
		{
			get {return throttleObjects;}
			set {throttleObjects = value;}
		}

		/// <summary>
		/// Get or set the mailService.
		/// </summary>
		public static EmailQueue.MailService MailService 
		{
			get {return mailService;}
			set {mailService = value;}
		}
        public static short DaysDue
        {
            get { return daysDue; }
            set { daysDue = value;}
        }
		#endregion
	}
}
