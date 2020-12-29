using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using GA.BDC.Data.EzFund.EZMain.Mappers;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Data;
using GA.BDC.Data.EzFund.EZMain.Tables;

namespace GA.BDC.Data.EzFund.EZMain.Repositories
{
    public class TestimonialsRepository : ITestimonialsRepository
    {
        private readonly DataProvider _dataProvider;
        private readonly EZMain.Tables.DataProvider _ezmainProdDataProvider;

        public TestimonialsRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _ezmainProdDataProvider = new EZMain.Tables.DataProvider();
            _ezmainProdDataProvider.Database.BeginTransaction();
        }

        public IList<Testimonial> GetAll()
        {

            const string sql = "SELECT TESTML_ID FROM SITE_TESTML_TBL (NOLOCK)";
            var ids = _dataProvider.Database.Connection.Query<int>(sql,
                 null, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            return ids.Select(GetById).ToList();

           
        }

        public Testimonial GetById(int id)
        {
            const string sql = "SELECT * FROM site_testml_tbl (NOLOCK) WHERE TESTML_ID = @id";
            var row = _dataProvider.Database.Connection.QueryFirst<site_testml_tbl>(sql, new { id }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
            return TestimonialMapper.Hydrate(row);
        }

        void IRepository<Testimonial>.Delete(Testimonial model)
        {
            throw new NotImplementedException();
        }

       
        int IRepository<Testimonial>.Save(Testimonial model)
        {
            throw new NotImplementedException();
        }

        void IRepository<Testimonial>.Update(Testimonial model)
        {
            throw new NotImplementedException();
        }
    }
}
