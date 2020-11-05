using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
    public class OrganizationTypeController : ApiController
    {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpGet]
      public IHttpActionResult GetAll()
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var organizationTypeRepository = efundraisingProdUnitOfWork.CreateRepository<IOrganizationTypeRepository>();
            var entities = organizationTypeRepository.GetAll();
            return Ok(entities);
         }
      }
   }
}
