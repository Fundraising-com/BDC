using System.Collections.Generic;
using GA.BDC.Shared.Entities;
using System;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IArTrnsTblRepository : IRepository<ArTrnsTbl>
   {

        void InsertPayment(ArTrnsTbl sale);
    }
}
