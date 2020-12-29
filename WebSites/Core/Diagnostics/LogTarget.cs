//
// 2005-05-15 - Stephen Lim - New class.
//
using System;

namespace GA.BDC.Core.Diagnostics
{
	/// <summary>
	/// LogTarget.
	/// </summary>
	public class LogTarget
	{
		#region Fields
		protected string _logSource = "";
		protected LogType _logType = LogType.EventLog;
		#endregion

		#region Constructors
		public LogTarget()
		{

		}

		public LogTarget(string type, string source)
		{
			switch (type.Trim().ToLower())
			{	
				case "email":
					_logType = LogType.Email;
					break;
				case "eventlog":
					_logType = LogType.EventLog;
					break;
				case "file":
					_logType = LogType.File;
					break;
			}	

			_logSource = source;
		}

		public LogTarget(LogType type, string source)
		{
			_logType = type;
			_logSource = source;
		}
		#endregion

		#region Properties

		public LogType LogType 
		{
			get {return _logType;}
			set {_logType = value;}
		}

		public string LogSource 
		{
			get {return _logSource;}
			set {_logSource = value;}
		}
		#endregion
	}
}
