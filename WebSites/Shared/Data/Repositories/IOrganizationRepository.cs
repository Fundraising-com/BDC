using GA.BDC.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Data.Repositories
{
    public interface IOrganizationRepository : IRepository<EzFundOrganization>
    {
			IList<int> GetOrganizationsByClientData(Client client);
			IList<int> GetOrganizationsByClientDataNew(Client client);
			int CreateNewOrganization(EzFundSale model);
    }
}
