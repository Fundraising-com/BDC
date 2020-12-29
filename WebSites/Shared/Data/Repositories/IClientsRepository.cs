using System.Collections.Generic;
using GA.BDC.Shared.Entities;
using System;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IClientsRepository : IRepository<Client>
   {
        Client GetByIdAndSequenceCode(int clientId, string sequenceCodes);

        Client GetShippingAddressById(int clientid);

        //Client GetByIdSalesScreen(int clientId);
    }
}
