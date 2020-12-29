using System;
using System.Diagnostics;
using SWCorporate.SystemEx;
using SWCorporate.SystemEx.Console;

using GA.BDC.Console.TaskExecutor.BusinessLogic;

namespace GA.BDC.Console.TaskExecutor.Tasks
{
    internal sealed class SendEZPaymentsTask : ITask<TaskFlags>
    {

        void ITask<TaskFlags>.Execute(TaskFlags taskFlags, params string[] taskArgs)
        {
            var className = this.GetType().Name;
            Trace.TraceInformation("Start: " + className);
            try
            {
                EZIDocPublisher.PublishEZPaymentsIDoc();
            }

            catch (Exception exception)
            {
                Trace.TraceError(exception.Message);
                InstrumentationProvider.SendExceptionNotification(exception, null);
                throw;
            }
            Trace.TraceInformation("End: " + className);
        }

    }
}
