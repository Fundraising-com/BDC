using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GA.BDC.Web.Custcare.Controllers
{
    public class ToolsController : ApiController
    {
        const string ___TOOLS_TYPE_PASSWORD = "PASSWORD";

        GA.BDC.Web.Custcare.Repository.ICampaignRepository _repo;

        public ToolsController(GA.BDC.Web.Custcare.Repository.ICampaignRepository repo)
        {
            _repo = repo;
        }

        public HttpResponseMessage Get(string type, string param)
        {
            List<Models.MemberPassword> memberPassword = null;

            if (type == ___TOOLS_TYPE_PASSWORD)
            {
                memberPassword = _repo.GetMemberPasswordsByEmail(param);
            }

            return Request.CreateResponse(HttpStatusCode.OK, memberPassword);
        }

        public HttpResponseMessage Post(string type, [FromBody]Models.MemberPassword memberPassword)
        {
            var message = string.Empty;

            if (type == ___TOOLS_TYPE_PASSWORD)
            {
                _repo.UpdateUserPassword(memberPassword.UserId, memberPassword.Password);

                if (_repo.Save())
                {
                    // No need to do anything.
                }
                else
                {
                    message = "Error saving password.";
                }
            }
            else
            {
                message = "Unknown type.";
            }

            if (message == string.Empty)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, message);           
        }
    }
}
