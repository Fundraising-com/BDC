using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;

namespace GA.BDC.Data.EzFund.EZMain.Mappers
{
    public static class WorkflowMapper
    {
        public static wkfl_proc_tbl DehydrateProcess(WorkflowProcess process)
        {
            var result = new wkfl_proc_tbl
            {
                PROC_TPLT_ID = process.ProcessTemplateId,
                PARENT_PROC_ID = process.ParentProcessId,
                ORG_ID = process.OrganizationId,
                CPGN_ID = process.CampaignId,
                ORDR_ID = process.OrderId,
                STEP_NBR = process.StepNumber,
                MSTONE_CDE = process.MilestoneCode,
                STAT_CDE = process.StatusCode,
                STAT_DTE = process.StatusDate,
                CREA_ACTOR_CDE = process.CreatorCode,
                CREA_DTE = process.CreationDate,
                CMPL_FLG = process.CompletionFlag,
                MARK_FOR_HIST_FLG = process.MarkForHistoryFlag,
                LAST_MODF_DTE = DateTime.Now,
                PARAM_TXT = process.ParameterText
            };
            return result;
        }
        public static wkfl_acty_tbl DehydrateActivity(WorkflowActivity activity)
        {
            var result = new wkfl_acty_tbl
            {
                PROC_ID = activity.ProcessId,
                ACTY_TPLT_ID = activity.ActivityTemplateId,
                PRIO_CDE = activity.PriorityCode,
                STAT_CDE = activity.StatusCode,
                STAT_DTE = activity.StatusDate,
                CREA_DTE = activity.CreationDate,
                STRT_DTE = activity.StartDate,
                ACTOR_GRP_CDE = activity.ActorGroupCode,
                ACTOR_CDE = activity.ActorCode,
                CMPL_FLG = activity.CompletionFlag,
                SPCL_SORT_CDE = activity.SPCLSortCode,
                LAST_MODF_DTE = activity.LastModificationDate,
                LAST_MODF_PRSN_CDE = activity.LastModificationPersonCode
            };
            return result;
        }

        public static WorkflowProcess HydrateProcess(wkfl_proc_tbl process)
        {
            return process == null? null: new WorkflowProcess
            {
                ProcessId = process.PROC_ID,
                ProcessTemplateId = process.PROC_TPLT_ID,
                ParentProcessId = process.PARENT_PROC_ID,
                OrganizationId = process.ORG_ID??0,
                CampaignId= process.CPGN_ID??0,
                OrderId= process.ORDR_ID??0,
                StepNumber= process.STEP_NBR??0,
                MilestoneCode = process.MSTONE_CDE??0,
                StatusCode= process.STAT_CDE,
                StatusDate= process.STAT_DTE,
                CreatorCode= process.CREA_ACTOR_CDE,
                CreationDate= process.CREA_DTE,
                CompletionFlag= process.CMPL_FLG??false,
                MarkForHistoryFlag= process.MARK_FOR_HIST_FLG??false,
                LastModificationDate = process.LAST_MODF_DTE,
                ParameterText= process.PARAM_TXT
            };
        }
    }
}
