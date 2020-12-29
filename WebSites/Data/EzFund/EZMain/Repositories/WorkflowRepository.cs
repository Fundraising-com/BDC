using GA.BDC.Shared.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using GA.BDC.Data.EzFund.EZMain.Tables;
using Dapper.Contrib.Extensions;
using Dapper;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    class WorkflowRepository : IWorkflowRepository
    {
        private readonly DataProvider _dataProvider;

        public WorkflowRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Delete(Workflow model)
        {
            throw new NotImplementedException();
        }

        public IList<Workflow> GetAll()
        {
            throw new NotImplementedException();
        }

        public Workflow GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int SaveWorkflowProcess(WorkflowProcess process)
        {
            var processEntity = WorkflowMapper.DehydrateProcess(process);
            var processId = _dataProvider.Database.Connection.Insert(processEntity, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return (int)processId;

        }
        public int SaveWorkflowActivity(WorkflowActivity activity) {
            var activityEntity = WorkflowMapper.DehydrateActivity(activity);
            var activityId = _dataProvider.Database.Connection.Insert(activityEntity, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return (int)activityId;
        }

        public int Save(Workflow model)
        {
            throw new NotImplementedException();
        }

        public void Update(Workflow model)
        {
            throw new NotImplementedException();
        }

        public WorkflowProcess GetLatestByOrgId(int orgId)
        {
            const string sqlOrganization = "SELECT TOP 1 * FROM WKFL_PROC_TBL (NOLOCK) WHERE ORG_ID = @orgId";
            var processRow = _dataProvider.Database.Connection.QueryFirstOrDefault<wkfl_proc_tbl>(sqlOrganization,
                  new { orgId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return WorkflowMapper.HydrateProcess(processRow);
        }
    }
}
