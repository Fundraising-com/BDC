using System.Collections.Generic;
using System.Linq;
using Dapper;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
    public class BannersRepository : IBannersRepository
    {
        private readonly DataProvider _dataProvider;

        public BannersRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        public IList<Banner> GetByPartner(int type, int partnerId)
        {
           var ids = _dataProvider.Database.Connection.Query<int>("SELECT id FROM banner B (NOLOCK) WHERE B.type = @type AND B.partner_id = @partnerId AND B.is_active = 1;",
              new {type, partnerId}, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            
            if (!ids.Any())
            {
               ids = _dataProvider.Database.Connection.Query<int>("SELECT id FROM banner B (NOLOCK) WHERE B.type = @type AND B.partner_id = 0 AND B.is_active = 1;",
             new { type }, _dataProvider.Database.CurrentTransaction.UnderlyingTransaction).ToList();
            }
           return ids.Select(GetById).ToList();
        }

        public Banner GetById(int id)
        {
           const  string sql = @"SELECT id, created_on,[image],[url],[partner_id],[type],[alternative_text],[is_active],[sort_order] FROM banner B (NOLOCK) WHERE B.id = @id;
SELECT VP.id, VP.created_on, VP.name, VP.bootstrap_class FROM view_port VP (NOLOCK) JOIN banner_view_port BVP (NOLOCK) ON VP.id = BVP.view_port_id WHERE BVP.banner_id = @id;";
           var multi = _dataProvider.Database.Connection.QueryMultiple(sql, new {id},
              _dataProvider.Database.CurrentTransaction.UnderlyingTransaction);
           var banner = multi.Read<banner>().First();
           var viewPorts = multi.Read<view_port>();
            var result = BannerMapper.Hydrate(banner);
            foreach (var viewPort in viewPorts.Select(ViewPortMapper.Hydrate))
            {
               result.ViewPorts.Add(viewPort);
               result.BootStrapClasses += viewPort.BootstrapClass + " ";
            }

            return result;
        }

       public IList<Banner> GetAll()
       {
          throw new System.NotImplementedException();
       }

       public int Save(Banner model)
       {
          throw new System.NotImplementedException();
       }

       public void Update(Banner model)
       {
          throw new System.NotImplementedException();
       }

       public void Delete(Banner model)
       {
          throw new System.NotImplementedException();
       }
    }
}
