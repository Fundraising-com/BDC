using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Xml.Linq;
using BusinessServices;

namespace ActivityLibrary
{

    public abstract class ProcessFileBaseActivity : CodeActivity
    {
       
        public InArgument<string> fileName { get; set; }

        protected  abstract ProcessBatchNodeActivity CreateAndInitializeBatchNodeActivity();

        protected override void Execute(CodeActivityContext context)
        {
            string text = context.GetValue(this.fileName);
            ProcessBatchNodeActivity aBNA = null;


            XDocument xDoc = XDocument.Load(@text);
            var batch = from r in xDoc.Descendants("BATCH")
                        select r;
            foreach (var f in batch)
            {
                aBNA = CreateAndInitializeBatchNodeActivity();

                IDictionary<String, Object> wfresults = WorkflowInvoker.Invoke(aBNA,
                    new Dictionary<String, Object>
                      {
                          {"_batchNode", f}
                          ,{"_ke3Filename", text}
                        
                      }
                 );
            }
        }
    }
}
