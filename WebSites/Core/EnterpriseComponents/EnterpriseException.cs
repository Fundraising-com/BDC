using System;
using System.Reflection;
using System.Threading;
using System.Collections;

namespace GA.BDC.Core.EnterpriseComponents
{
	public struct MethodParameter
	{
		public object parameterName;
		public object parameterValue;
	}

	/// <summary>
	/// Summary description for EnterpriseException.
	/// </summary>
	public class EnterpriseException : ApplicationException
	{
		// exception data to be collected
		private DateTime currentDate;
		private string machineName;
		private string exceptionSource;
		private string exceptionType;
		private string exceptionMessage;
		private string stackTrace;
		private string callStack;
		private string applicationDomainName;
		private string assemblyName;
		private string threadId;
		private string threadUser;
		private string parameterData;

		private string xmlException;
		private Assembly assemblyObject;

		// default constructor
		public EnterpriseException()
		{
		}

		// Constructor accepting a single string message
		public EnterpriseException (string message) : base(message)
		{
		}
   
		// Constructor accepting a string message and an 
		// inner exception which will be wrapped by this 
		// custom exception class
		public EnterpriseException(string message, 
			Exception inner) : base(message, inner)
		{
//			assemblyObject = Assembly.GetExecutingAssembly();

			// collect info
			currentDate = DateTime.Now;
			machineName = Environment.MachineName;
			exceptionSource = inner.Source;
			exceptionType = inner.GetType().FullName;
			exceptionMessage = message;
			stackTrace = inner.StackTrace;
			callStack = Environment.StackTrace;
			applicationDomainName = AppDomain.CurrentDomain.FriendlyName;
			assemblyName = ""; //assemblyObject.FullName;
//			threadId = AppDomain.GetCurrentThreadId().ToString();
//			threadUser = Thread.CurrentPrincipal.Identity.Name;

			xmlException = "<xml>" +
				"<Exception>" +
				"<Type>" + exceptionType +"</Type>" + 
				"<Date>" + currentDate +"</Date>" + 
				"<MachineName>" + machineName +"</MachineName>" + 
				"<Source>" + exceptionSource +"</Source>" + 
				"<Message>" + exceptionMessage +"</Message>" + 
				"<StackTrace>" + stackTrace +"</StackTrace>" + 
				"<CallStack>" + callStack +"</CallStack>" + 
				"<ApplicationDomainName>" + applicationDomainName +"</ApplicationDomainName>" + 
				"<AssemblyInfo>" +
				"<AssemblyName>" + assemblyName +"</AssemblyName>" + 
				"</AssemblyInfo>" +
//				"<ThreadInfo>" +
//				"<ThreadId>" + threadId +"</ThreadId>" + 
//				"<ThreadUser>" + threadUser +"</ThreadUser>" + 
//				"</ThreadInfo>" +
				"</Exception>" +
				"</xml>";

			LoggingSystem.LogError(xmlException);
		}

		/*
		public EnterpriseException(string message, 
			Exception inner, MethodBase mBase, DataParameters[] parameters)
			: base(message, inner)
		{
//			assemblyObject = Assembly.GetExecutingAssembly();
//
//			// collect info
//			currentDate = DateTime.Now;
//			machineName = Environment.MachineName;
//			exceptionSource = inner.Source;
//			exceptionType = inner.GetType().FullName;
//			exceptionMessage = message;
//			stackTrace = inner.StackTrace;
//			callStack = Environment.StackTrace;
//			applicationDomainName = AppDomain.CurrentDomain.FriendlyName;
//			assemblyName = assemblyObject.FullName;
//			threadId = AppDomain.GetCurrentThreadId().ToString();
//			threadUser = Thread.CurrentPrincipal.Identity.Name;
//
//			ParameterInfo[] methodBaseParams = mBase.GetParameters();
//
//			foreach(DataParameters parameter in parameters)
//			{
//				parameterData += "<Params><Name>" + parameter.ParameterName + "</Name>" +
//					"<Value>" + parameter.Value + "</Value></Params>";
//			}
//
//			xmlException = "<xml>" +
//				"<Exception>" +
//				"<Type>" + exceptionType +"</Type>" + 
//				"<Date>" + currentDate +"</Date>" + 
//				"<Method>" +
//				"<Name>" + mBase.Name + "</Name>" +
//				parameterData +
//				"</Method>" +
//				"<MachineName>" + machineName +"</MachineName>" + 
//				"<Source>" + exceptionSource +"</Source>" + 
//				"<Message>" + exceptionMessage +"</Message>" + 
//				"<StackTrace>" + stackTrace +"</StackTrace>" + 
//				"<CallStack>" + callStack +"</CallStack>" + 
//				"<ApplicationDomainName>" + applicationDomainName +"</ApplicationDomainName>" + 
//				"<AssemblyInfo>" +
//				"<AssemblyName>" + assemblyName +"</AssemblyName>" + 
//				"</AssemblyInfo>" +
//				"<ThreadInfo>" +
//				"<ThreadId>" + threadId +"</ThreadId>" + 
//				"<ThreadUser>" + threadUser +"</ThreadUser>" + 
//				"</ThreadInfo>" +
//				"</Exception>" +
//				"</xml>";
//
//			LoggingSystem.LogError("eSubs", xmlException);
		}*/

	}
}
