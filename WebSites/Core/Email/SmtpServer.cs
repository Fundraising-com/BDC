//
// Dec 6, 2004. Stephen Lim - New class. Added ConnectionTimeout property.
// Feb 7, 2005. Stephen Lim - Host defaults to "localhost".
//

using System;

namespace GA.BDC.Core.Email
{
	/// <summary>
	/// Hold SMTP connection information.
	/// </summary>
	/// <example>
	/// Create a new SmtpServer container.
	/// <code>
	/// SmtpServer s = new SmtpServer();
	/// s.Host = "mail.efundraising.com";
	/// s.Port = 25;
	/// </code>
	/// </example>
	public class SmtpServer
	{
		#region Fields
		private string _host = "localhost";
		private string _username = "";
		private string _password = "";
		private int _port = 25;
		private int _connectionTimeout = 30;
		#endregion
		
		#region Constructors
		/// <summary>
		///  Class to hold information on Smtp server.
		/// </summary>
		public SmtpServer()
		{
		}

		public SmtpServer(string smtpServerString)
		{
			smtpServerString = smtpServerString.Trim();

			// Parse smtpServer string
			if (smtpServerString != "") 
			{

				_host = smtpServerString;

				string hostInfo = smtpServerString;

				// Split userinfo@hostinfo
				if (_host.IndexOf("@") > -1) 
				{
					string[] parts = _host.Split("@".ToCharArray(), 2);
					_username = parts[0];
					_host = parts[1];
				}

				// Get username:password
				if (_username.IndexOf(":") > -1)
				{
					string[] parts = _username.Split(":".ToCharArray(), 2);
					_username = parts[0];
					_password = parts[1];
				}

				// Get host:port
				if (_host.IndexOf(":") > -1)
				{
					string[] parts = _host.Split(":".ToCharArray(), 2);

					if (parts[0] == "")
						_host = "localhost";
					else
						_host = parts[0];

					try 
					{
						_port = Convert.ToInt32(parts[1]);
					}
					catch {}
				}
			}
		}

		public SmtpServer(string host, int port) : this("", "", host, port)
		{
		}

		public SmtpServer(string username, string password, string host, int port) : this(username, password, host, port, 25)
		{
		}

		public SmtpServer(string username, string password, string host, int port, int connectionTimeout)
		{
			_username = username;
			_password = password;
			_host = host;
			_port = port;
			_connectionTimeout = connectionTimeout;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Get the string representation of the SmtpServer configuration.
		/// </summary>
		/// <returns>SmtpServer string.</returns>
		public override string ToString()
		{
			return _username + ":" + _password + "@" + 
							_host + ":" + _port;
		}
		#endregion


		#region Properties
		/// <summary>
		/// Get or set the host for this server.
		/// </summary>
		public string Host 
		{
			get 
			{
				return _host;
			}
			set 
			{
				_host = value;
			}
		}

		/// <summary>
		/// Get or set the port number for this server.
		/// </summary>
		public int Port 
		{
			get 
			{
				return _port;
			}
			set 
			{
				_port = value;
			}
		}

		/// <summary>
		/// Get or set the username used to authenticate access to the SMTP server.
		/// </summary>
		public string Username 
		{
			get 
			{
				return _username;
			}
			set 
			{
				_username = value;
			}
		}

		/// <summary>
		/// Get or set the password used to authenticate access to the SMTP server.
		/// </summary>
		public string Password 
		{
			get 
			{
				return _password;
			}
			set 
			{
				_password = value;
			}
		}

		/// <summary>
		/// Get or set the maximum amount of time in seconds to wait before timing out.
		/// </summary>
		public int ConnectionTimeout
		{
			get {return _connectionTimeout;}
			set {_connectionTimeout = value;}
		}
		#endregion

	}
}
