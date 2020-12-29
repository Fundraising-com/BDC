using System;
using log4net;
using SWCorporate.SystemEx.Console;

using GA.BDC.Console.PAPFeedback.TaskExecutor.Tasks;

namespace GA.BDC.Console.PAPFeedback.TaskExecutor
{ /// <summary>
    ///   GA.BDC.Console.TaskExecutor
    /// </summary>
    internal sealed class Program : ProgramBase<Program, TaskFlags>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
        #region private static int Main(string[] taskArgs)

        /// <summary>
        ///   The main entry point of the application.
        /// </summary>
        /// <param name="taskArgs">commandline arguments to pass on to the caller.</param>
        /// <returns>an integer which represents an exit code.  Any value greater than 0 represents an error.</returns>
        private static int Main(string[] taskArgs)
        {
            Log.Debug("Start running the application.");
            Log.DebugFormat("Parameters received: {0}", string.Join(",", taskArgs));
            var exitCode = Execute(taskArgs);
            return exitCode;
        }

      #endregion


      #region public override/implementation of abstract SWCorporate.SystemEx.Console.ProgramBase<P,T> methods

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
            Log.Fatal(exception);
        }

        #endregion
    }
}
