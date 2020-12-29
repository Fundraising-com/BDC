using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class WorkflowActivity
    {
        /// <summary>
        /// Activity Id
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// Process Id
        /// </summary>
        public int ProcessId { get; set; }
        /// <summary>
        /// Activity Template Id
        /// </summary>
        public int ActivityTemplateId { get; set; }
        /// <summary>
        /// Priority Code
        /// </summary>
        public int PriorityCode { get; set; }
        /// <summary>
        /// Status Code
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// Status Date
        /// </summary>
        public DateTime StatusDate { get; set; }
        /// <summary>
        /// Creation Date
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Actor Group Code
        /// </summary>
        public string ActorGroupCode { get; set; }
        /// <summary>
        /// Actor Code
        /// </summary>
        public string ActorCode { get; set; }
        /// <summary>
        /// Completion Flag
        /// </summary>
        public bool CompletionFlag { get; set; }
        /// <summary>
        /// SPCL Sort Code Flag
        /// </summary>
        public string SPCLSortCode { get; set; }
        /// <summary>
        /// Last Modification Date
        /// </summary>
        public DateTime LastModificationDate { get; set; }
        /// <summary>
        /// Last Modification Person Code
        /// </summary>
        public string LastModificationPersonCode { get; set; }

    }
}
