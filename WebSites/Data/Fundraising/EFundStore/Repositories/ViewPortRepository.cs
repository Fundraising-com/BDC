using System.Collections.Generic;
using System.Linq;
using GA.BDC.Data.Fundraising.EFundStore.Mappers;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Repositories
{
   public class ViewPortRepository : IViewPortRepository
   {
      private readonly DataProvider _dataProvider;

      public ViewPortRepository(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

      public ViewPort GetById(int id)
      {
         throw new System.NotImplementedException();
      }

      public IList<ViewPort> GetAll()
      {
         var viewPorts = _dataProvider.view_ports.ToList();
         return viewPorts.Select(ViewPortMapper.Hydrate).ToList();
      }

      public int Save(ViewPort model)
      {
         throw new System.NotImplementedException();
      }

      public void Update(ViewPort model)
      {
         throw new System.NotImplementedException();
      }

      public void Delete(ViewPort model)
      {
         throw new System.NotImplementedException();
      }
   }
}
