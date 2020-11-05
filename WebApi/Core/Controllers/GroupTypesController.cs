using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
    public class GroupTypesController : ApiController
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
            var groupTypeRepository = efundraisingProdUnitOfWork.CreateRepository<IGroupTypeRepository>();
            var entities = groupTypeRepository.GetAll();
            return Ok(entities);
         }
      }
   }
}
