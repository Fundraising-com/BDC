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
    class SellingKitLeadsRepository : ISellingKitLeadRepository
    {
        private readonly DataProvider _dataProvider;

        public SellingKitLeadsRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public int Save(SellingKitLead sellingkitlead)
        {
            var row = SellingKitLeadMapper.Dehydrate(sellingkitlead);
            var sellingkitleadId = _dataProvider.Database.Connection.Insert(row, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);

            return (int)sellingkitleadId;
        }


                       
        public IList<SellingKitLead> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(SellingKitLead model)
        {
            throw new NotImplementedException();
        }

        public void Delete(SellingKitLead model)
        {
            throw new NotImplementedException();
        }

        public SellingKitLead GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
