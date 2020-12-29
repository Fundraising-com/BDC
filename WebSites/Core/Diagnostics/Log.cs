using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace GA.BDC.Core.Diagnostics
{
	/// <summary>
	/// Log.
	/// </summary>
	public class Log
	{
		#region Fields
		protected LogLevel _level = LogLevel.Info;
		protected DateTime _gmtDate = DateTime.UtcNow;
		protected DateTime _localDate = DateTime.Now;
		protected string _message = "";
		protected string _source = "";
		protected Exception _ex = null;
		protected ApplicationEnvironment _appEnv = null;
		protected WebEnvironment _webEnv = null;
		#endregion

		#region Constructors
		public Log() : this(DateTime.UtcNow, "", "", null)
		{
		}

		public Log(Exception ex) : this(DateTime.UtcNow, "", "", ex)
		{
		}

		public Log(string source, Exception ex) : this(DateTime.UtcNow, "", source, ex)
		{
		}

		public Log(string msg) : this(DateTime.UtcNow, msg, "", null)
		{
		}

		public Log(string msg, string source) : this(DateTime.UtcNow, msg, source, null)
		{
		}

		public Log(string msg, string source, Exception ex) : this(DateTime.UtcNow, msg, source, ex)
		{
		}

		public Log(DateTime gmtDate, string msg, string source, Exception ex): this(LogLevel.Info, gmtDate, msg, source, ex)
		{

		}

		public Log(LogLevel level, DateTime gmtDate, string msg, string source, Exception ex)
		{
			_level = level;
			_gmtDate = gmtDate;
			_localDate = gmtDate.ToLocalTime();
			_message = msg;
			_ex = ex;

			_appEnv = ApplicationEnvironment.NewApplicationEnvironment();
			try {
				_webEnv = WebEnvironment.NewWebEnvironment();
			} catch { }

			if (source != null && source != "")
				_source = source;
			else
			{
				try 
				{
					if (Application.AppDomain != null && Application.AppDomain != "")
						_source = Application.AppDomain;
				}
				catch {}

				try 
				{
					HttpContext context = HttpContext.Current;
					if (context != null)
						_source = context.Request.Url.Host.ToLower() + context.Request.ApplicationPath.TrimEnd("/".ToCharArray()).ToLower();
				}
				catch {}
			}			
		}
		#endregion

		#region Properties

		public LogLevel Level
		{
			get { return _level; }
			set { _level = value; }
		}

		public DateTime GmtDate
		{
			get { return _gmtDate; }
			set { _gmtDate = value; }
		}

		public DateTime LocalDate
		{
			get { return _localDate; }
			set { _localDate = value; }
		}

		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}

		public string Source
		{
			get { return _source; }
			set { _source = value; }
		}

		public Exception Exception
		{
			get { return _ex; }
			set { _ex = value; }
		}

		public ApplicationEnvironment Application
		{
			get { return _appEnv; }
			set { _appEnv = value; }
		}

		public WebEnvironment Web
		{
			get { return _webEnv; }
			set { _webEnv = value; }
		}

		#endregion

		#region Methods
		/// <summary>
		/// Write Xml element.
		/// </summary>
		/// <param name="wr">XmlWriter</param>
		/// <param name="name">Element name.</param>
		/// <param name="val">Element value.</param>
		protected void WriteElementString(XmlTextWriter wr, string name, string val)
		{
			try 
			{
				if (val != null)
				{
					wr.WriteStartElement(name);
					if (wr.Formatting == Formatting.Indented)
						wr.WriteRaw(XmlEncode(val, true));
					else
						wr.WriteRaw(XmlEncode(val, false));
					wr.WriteEndElement();
				}
			}
			catch {}
		}

		/// <summary>
		/// Write Xml attribute.
		/// </summary>
		/// <param name="wr">XmlWriter</param>
		/// <param name="name">Attribute name.</param>
		/// <param name="val">Attribute value.</param>
		protected void WriteAttributeString(XmlTextWriter wr, string name, string val)
		{
			try 
			{
				if (val != null)
				{
					wr.WriteStartAttribute(name, "");
					if (wr.Formatting == Formatting.Indented)
						wr.WriteRaw(XmlEncode(val, true));
					else
						wr.WriteRaw(XmlEncode(val, false));
					wr.WriteEndAttribute();
				}
			}
			catch {}
		}

		/// <summary>
		/// Encode Xml. This method supports encoding newlines.
		/// </summary>
		/// <param name="val">String to encode.</param>
		/// <returns>Encoded string.</returns>
		protected string XmlEncode(string val, bool indented)
		{
			if (val != null)
				val = HttpUtility.HtmlEncode(val);
			
			if (! indented)
				val = val.Replace("\r", "&#xd;").Replace("\n", "&#xa;");

			return val;
		}

		/// <summary>
		/// Convert log to Xml string.
		/// </summary>
		/// <param name="indented">Xml string should be indented and use multiline.</param>
		/// <returns>Xml string.</returns>
		public string ToXmlString(bool indented)
		{
			// Cannot XmlSerialize an Exception object in .NET 1.1 because
			// of declarative security permission.
			// See http://support.microsoft.com/default.aspx?scid=kb%3BEN-US%3BQ326971
			// The restriction should be lifted in .NET 2.0.
			// To workaround the problem, we serialize to XML manually.
			MemoryStream stream = new MemoryStream();
			XmlTextWriter wr = new XmlTextWriter(stream, new System.Text.UTF8Encoding());

			// Set formatting
			if (indented)
				wr.Formatting = Formatting.Indented;

			// Start log
			switch (_level)
			{
				case LogLevel.Error:
					wr.WriteStartElement("Error");
					break;
				case LogLevel.Info:
					wr.WriteStartElement("Info");
					break;
				case LogLevel.Warn:
					wr.WriteStartElement("Warn");
					break;
				case LogLevel.Debug:
					wr.WriteStartElement("Debug");
					break;
			}
			WriteAttributeString(wr, "GmtDate", _gmtDate.ToString());
			WriteAttributeString(wr, "LocalDate", _localDate.ToString());
			WriteAttributeString(wr, "Message", _message);
			WriteAttributeString(wr, "Source", _source);

			// Write Exception
			if (_ex != null)
			{
				wr.WriteStartElement("Exception");
				WriteAttributeString(wr, "Source", _ex.Source);
				if (_ex.TargetSite != null)
					WriteAttributeString(wr, "TargetSite", _ex.TargetSite.ToString());
				WriteAttributeString(wr, "Message", _ex.Message);
				WriteAttributeString(wr, "StackTrace", _ex.ToString());
				WriteAttributeString(wr, "HelpLink", _ex.HelpLink);
				wr.WriteEndElement();
			}

			// Write ApplicationEnvironment
			if (_appEnv != null)
			{
				wr.WriteStartElement("Application");
				WriteAttributeString(wr, "MachineName", _appEnv.MachineName);
				WriteAttributeString(wr, "ProcessId", _appEnv.ProcessId);
				WriteAttributeString(wr, "ProcessName", _appEnv.ProcessName);
				WriteAttributeString(wr, "ThreadId", _appEnv.ThreadId);
				WriteAttributeString(wr, "WindowsId", _appEnv.WindowsId);
				WriteAttributeString(wr, "AppDomain", _appEnv.AppDomain);
				WriteAttributeString(wr, "ProductName", _appEnv.ProductName);
				wr.WriteEndElement();
			}
			
			// Write WebEnvironment
			if (_webEnv != null)
			{
				wr.WriteStartElement("Web");
				WriteAttributeString(wr, "Url", _webEnv.Url);
				WriteAttributeString(wr, "RemoteIp", _webEnv.RemoteIp);
				WriteAttributeString(wr, "HttpMethod", _webEnv.HttpMethod);
				WriteAttributeString(wr, "NewSession", _webEnv.NewSession.ToString());
				WriteAttributeString(wr, "Query", _webEnv.Query);
				wr.WriteStartElement("Headers");
				foreach (string key in _webEnv.Headers.Keys)
				{
					string xmlKey = Regex.Replace(key, @"\W", "_", RegexOptions.None);
					xmlKey = Regex.Replace(xmlKey, @"^(\d)", "_$1", RegexOptions.None);

					if (key != "VsDebuggerCausalityData")
						WriteAttributeString(wr, xmlKey, _webEnv.Headers[key]);
				}
				wr.WriteEndElement();
				wr.WriteStartElement("Form");
				foreach (string key in _webEnv.Form.Keys)
				{
					string xmlKey = Regex.Replace(key, @"\W", "_", RegexOptions.None);
					xmlKey = Regex.Replace(xmlKey, @"^(\d)", "_$1", RegexOptions.None);

					if (key != "__VIEWSTATE" &&
						key != "__EVENTTARGET" &&
						key != "__EVENTARGUMENT")
					{
						WriteAttributeString(wr, xmlKey, _webEnv.Form[key]);
					}
				}
				wr.WriteEndElement();
				wr.WriteEndElement();
			}

			// End log
			wr.WriteEndElement();
			wr.Close();

			// Return XML string
			return System.Text.Encoding.UTF8.GetString(stream.GetBuffer());
		}
		#endregion
	}

}
