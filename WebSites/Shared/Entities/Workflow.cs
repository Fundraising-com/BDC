using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class Workflow
    {
        /// <summary>
        /// Workflow Process
        /// </summary>
        public WorkflowProcess Process { get; set; }
        /// <summary>
        /// Workflow Activity
        /// NOTE: may change to a list
        /// </summary>
        public WorkflowActivity Activity { get; set; }
    }
}
