using System;
using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
    public interface IProspectRepository : IRepository<ProsNmadTbl>
    {

        ProsNmadTbl GetProspectById(int externalId);
       
    }
}
