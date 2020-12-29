using System;
using SWCorporate.AppServices.Shared;
using SWCorporate.SystemEx.Console;
using System.Diagnostics;
using SWCorporate.SystemEx;


namespace GA.BDC.Console.TaskExecutor
{
   /// <summary>
   ///   GA.BDC.Console.TaskExecutor
   /// </summary>
   internal sealed class Program : ProgramBase<Program, TaskFlags>
   {
      
      #region private static int Main(string[] taskArgs)

      /// <summary>
      ///   The main entry point of the application.
      /// </summary>
      /// <param name="taskArgs">commandline arguments to pass on to the caller.</param>
      /// <returns>an integer which represents an exit code.  Any value greater than 0 represents an error.</returns>
      private static int Main(string[] taskArgs)
      {
            try
            {

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                var exitCode = Execute(taskArgs);
                return exitCode;
            }
            catch (Exception exception)
            {
                Trace.TraceError(exception.Message);
                SWCorporate.SystemEx.InstrumentationProvider.SendExceptionNotification(exception, null);
                throw;
            }

        }

      #endregion

      #region default constructor

      public Program()
          : base()
      {
      }

      #endregion

      #region public override/implementation of abstract SWCorporate.SystemEx.Console.ProgramBase<P,T> methods

      /// <summary>
      ///   This routine will return the command line usage information specific to this program.
      /// </summary>
      /// <returns>A string expalining how to use the program.</returns>
      public override string GetAppUsage()
      {
         var taskNames = Enum.GetNames(typeof(TaskFlags));
         var usage = "AVAILABLE TASKS:\r\n\t" + string.Join("\r\n\t", taskNames);
         return usage;
      }

      /// <summary>
      ///   Allows us to handle any exceptions after the base class exception handling.
      /// </summary>
      /// <param name="consoleProgram">Represents the program in which the exception was thrown.</param>
      /// <param name="exception">Reperesents the exception that was thrown.</param>
      public override void HandleException(Program consoleProgram, Exception exception)
      {
         SWCorporate.SystemEx.InstrumentationProvider.SendExceptionNotification(exception, null);
      }

      #endregion
   }
}
