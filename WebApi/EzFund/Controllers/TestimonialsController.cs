using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GA.BDC.Data.EzFund.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.EzFund.Controllers
{
    public class TestimonialsController : ApiController
    {
        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var testimonialRepository = EzmainUnitOfWork.CreateRepository<ITestimonialsRepository>();
                var testimonials = testimonialRepository.GetAll();
                return Ok(testimonials);
            }
        }

        [HttpGet]
        public IList<Testimonial> GetAllTestimonials(int maxResults, bool isRandom)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var testimonialRepository = EzmainUnitOfWork.CreateRepository<ITestimonialsRepository>();
                var testimonials = testimonialRepository.GetAll();
                if (isRandom)
                {
                    var random = new Random(DateTime.Now.Millisecond);
                    testimonials = testimonials.OrderBy(p => random.Next(0, 1000)).ToList();
                }
                testimonials = testimonials.Take(maxResults).ToList();
                return testimonials;
            }
        }

    }


}



