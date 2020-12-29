using GA.BDC.Shared.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Data;
using GA.BDC.Data.EzFund.EZMain.Tables;
using Dapper;
using Dapper.Contrib.Extensions;


namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    class LeadsRepository : ILeadRepository
    {
        private readonly DataProvider _dataProvider;

        public LeadsRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void CreateLeadActivity(Lead lead, LeadActivityType activityType, bool isCompleted, string comments)
        {
            throw new NotImplementedException();
        }

        public void CreateLeadComment(Lead lead, string comments)
        {
            throw new NotImplementedException();
        }

        public void CreateLeadVisit(Lead lead)
        {
            throw new NotImplementedException();
        }

        public void CreateUnassigmentLog(Lead lead)
        {
            throw new NotImplementedException();
        }

        public void Delete(Lead model)
        {
            throw new NotImplementedException();
        }

        public IList<Lead> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Lead> GetAllByDateRange(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<Lead> GetAllByEmail(int id, string email)
        {
            throw new NotImplementedException();
        }

        public IList<Lead> GetAllByPhone(int id, string phone)
        {
            throw new NotImplementedException();
        }

        //public ProsNmadTbl GetProsById(int id)
        //{
        //    const string sql = @"SELECT TOP 1 * FROM PROS_NMAD_TBL (NOLOCK) WHERE SEQ_NBR = @id";
        //    var row = _dataProvider.Database.Connection.QueryFirst<pros_nmad_tbl>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
        //    return LeadMapper.Hydrate(row);
        //}

        //public Lead GetProspectById(int externalId)
        //{
        //    const string sql = @"SELECT TOP 1 * FROM PROS_NMAD_TBL (NOLOCK) WHERE SEQ_NBR = @id";
        //    var row = _dataProvider.Database.Connection.QueryFirst<pros_nmad_tbl>(sql, new { externalId }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
        //    return LeadMapper.Hydrate(row);
        //}

        public int Save(Lead lead)
        {
            var row = LeadMapper.Dehydrate(lead);
            var leadId = _dataProvider.Database.Connection.Insert(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            //Disabled for now  - new kit form
            //foreach (var item in lead.SelectedProducts)
            //{
            //    var rowProduct = LeadMapper.DehydrateProduct(item);
            //    rowProduct.SEQ_NBR = (int)leadId; 
            //    _dataProvider.Database.Connection.Insert(rowProduct, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            //}
            return (int)leadId;
        }

        public void Update(Lead model)
        {

            var toBeUpdated = _dataProvider.leads.Find(model.Id);
            toBeUpdated.CTCT_NME = model.FirstName;
            toBeUpdated.PH_1_NBR = model.Phone;
            toBeUpdated.EML_TXT = model.Email;
            toBeUpdated.CMNT_TXT = toBeUpdated.CMNT_TXT + " - " + model.Comments;
            //toBeUpdated.CMNT_TXT = toBeUpdated.CMNT_TXT + " - Group Type: " + model.GroupType;
            toBeUpdated.ORG_NME = model.Group;
            toBeUpdated.SLS_STRT_TXT = model.StartRange;
            toBeUpdated.RFRL_CDE = model.ReferralCode;
            toBeUpdated.GRP_TYPE = model.GrpType;
            toBeUpdated.ORG_MEMB_QTY_TXT = model.OrgMembQtyTxt;
            toBeUpdated.ADDR_1_TXT = model.Address != null ? model.Address.Address1 : string.Empty;
            toBeUpdated.CITY_NME = model.Address != null ? model.Address.City : string.Empty;
            toBeUpdated.ST_CDE = model.StCde;
            toBeUpdated.ZIP_CDE = model.Address != null ? model.Address.PostCode : string.Empty;




            _dataProvider.SaveChanges();

            // throw new NotImplementedException();
        }

        public Lead GetById(int id)
        {
            var leadFound = _dataProvider.Database.Connection.Query<lead>("SELECT TOP 1 * FROM PROS_NMAD_TBL (NOLOCK) WHERE SEQ_NBR = @id", new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).First();
            var result = LeadMapper.Hydrate(leadFound);
            return result;
        }


        //Lead IRepository<Lead>.GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}


        public IList<Lead> GetByEmail(string email)
        {

            throw new NotImplementedException();
        }


    }
}
