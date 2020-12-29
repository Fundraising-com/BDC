using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class WorkflowProcess
    {
        /// <summary>
        /// Process Id
        /// </summary>
        public int ProcessId { get; set; }
        /// <summary>
        /// Process Template Id
        /// </summary>
        public int ProcessTemplateId { get; set; }
        /// <summary>
        /// Parent Process Id
        /// </summary>
        public int? ParentProcessId { get; set; }
        /// <summary>
        /// Organization Id
        /// </summary>
        public int OrganizationId { get; set; }
        /// <summary>
        /// Campaign Id
        /// </summary>
        public int CampaignId { get; set; }
        /// <summary>
        /// Order Id
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Step Number
        /// </summary>
        public int StepNumber { get; set; }
        /// <summary>
        /// Milestone Code
        /// </summary>
        public int MilestoneCode { get; set; }
        /// <summary>
        /// Status Code
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// Status Date
        /// </summary>
        public DateTime? StatusDate { get; set; }
        /// <summary>
        /// Creator Code
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Creation Date
        /// </summary>
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// Completion Flag
        /// </summary>
        public bool CompletionFlag { get; set; }
        /// <summary>
        /// Mark for History Flag
        /// </summary>
        public bool MarkForHistoryFlag { get; set; }
        /// <summary>
        /// Last Modification Date
        /// </summary>
        public DateTime? LastModificationDate { get; set; }
        /// <summary>
        /// Parameters Text
        /// </summary>
        public string ParameterText { get; set; }
    }
}
