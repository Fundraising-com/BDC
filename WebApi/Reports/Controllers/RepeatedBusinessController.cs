using System;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Helpers;
using System.Web.Http;

namespace GA.BDC.WebApi.Reports.Controllers
{
    [Authorize]
    public class RepeatedBusinessController : ApiController
    {
        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get(string date1Start, string date1End, string date2Start, string date2End, bool showFCs, string countryCode, string regionCode)
        {
	        var a = new DateTime(int.Parse(date1Start.Split('-')[0]), int.Parse(date1Start.Split('-')[1]), int.Parse(date1Start.Split('-')[2]));
	        var b = new DateTime(int.Parse(date1End.Split('-')[0]), int.Parse(date1End.Split('-')[1]), int.Parse(date1End.Split('-')[2]));
	        var c = new DateTime(int.Parse(date2Start.Split('-')[0]), int.Parse(date2Start.Split('-')[1]), int.Parse(date2Start.Split('-')[2]));
	        var d = new DateTime(int.Parse(date2End.Split('-')[0]), int.Parse(date2End.Split('-')[1]), int.Parse(date2End.Split('-')[2]));
	        using (var efundraisingProdUnitOfWork = new UnitOfWork(Shared.Data.Database.EFundraisingProd))
	        {
		        var reportsFactory = efundraisingProdUnitOfWork.CreateRepository<IReportsRepository>();
					 
		        var result = reportsFactory.ExecuteRepeatedBusiness(
			        DateHelper.PrepareStartDate(a),
			        DateHelper.PrepareEndDate(b),
			        DateHelper.PrepareStartDate(c),
			        DateHelper.PrepareEndDate(d),
			        showFCs, countryCode, regionCode);
		        return Ok(result);
	        }
                
        }

       [HttpGet]
       public IHttpActionResult GetDetail(string date1Start, string date1End, string date2Start, string date2End, bool showFCs, string countryCode, string regionCode, bool isDetail)
       {
	       var a = new DateTime(int.Parse(date1Start.Split('-')[0]), int.Parse(date1Start.Split('-')[1]), int.Parse(date1Start.Split('-')[2]));
	       var b = new DateTime(int.Parse(date1End.Split('-')[0]), int.Parse(date1End.Split('-')[1]), int.Parse(date1End.Split('-')[2]));
	       var c = new DateTime(int.Parse(date2Start.Split('-')[0]), int.Parse(date2Start.Split('-')[1]), int.Parse(date2Start.Split('-')[2]));
	       var d = new DateTime(int.Parse(date2End.Split('-')[0]), int.Parse(date2End.Split('-')[1]), int.Parse(date2End.Split('-')[2]));
	       using (var efundraisingProdUnitOfWork = new UnitOfWork(Shared.Data.Database.EFundraisingProd))
	       {
		       var reportsFactory = efundraisingProdUnitOfWork.CreateRepository<IReportsRepository>();
		       var result = reportsFactory.ExecuteRepeatedBusinessDetail(
			       DateHelper.PrepareStartDate(a),
			       DateHelper.PrepareEndDate(b),
			       DateHelper.PrepareStartDate(c),
			       DateHelper.PrepareEndDate(d),
			       showFCs, countryCode, regionCode);
		       return Ok(result);
	       }
      }
    }
}
