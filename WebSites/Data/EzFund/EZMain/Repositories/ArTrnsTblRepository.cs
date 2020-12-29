using GA.BDC.Shared.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.EZMain.Tables;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using Dapper.Contrib.Extensions;
using Dapper;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    class ArTrnsTblRepository : IArTrnsTblRepository
    {
        private readonly DataProvider _dataProvider;

        public ArTrnsTblRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }


        public void Delete(ArTrnsTbl model)
        {
            throw new NotImplementedException();
        }

        public IList<ArTrnsTbl> GetAll()
        {
            throw new NotImplementedException();
        }

        public ArTrnsTbl GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertPayment(ArTrnsTbl model)
        {
            var paymentToBePersisted = ArTrnsTblMapper.Dehydrate(model);
            _dataProvider.Database.Connection.Insert(paymentToBePersisted, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
        }

        public int Save(ArTrnsTbl model)
        {
            throw new NotImplementedException();
        }

        public void Update(ArTrnsTbl model)
        {
            throw new NotImplementedException();
        }

        
    }      
}
