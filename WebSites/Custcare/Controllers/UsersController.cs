using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GA.BDC.Web.Custcare.Controllers
{
    public class UsersController : ApiController
    {
        GA.BDC.Web.Custcare.Repository.ICampaignRepository _repo;

        public UsersController(GA.BDC.Web.Custcare.Repository.ICampaignRepository repo)
        {
            _repo = repo;
        }
    }
}
