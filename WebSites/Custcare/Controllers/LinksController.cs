using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;

namespace GA.BDC.Web.Custcare.Controllers
{
    public class LinksController : ApiController
    {
        GA.BDC.Web.Custcare.Repository.ICampaignRepository _repo;

        public LinksController(GA.BDC.Web.Custcare.Repository.ICampaignRepository repo)
        {
            _repo = repo;
        }

        public HttpResponseMessage Get(string param)
        {
            int id;
            Models.Links _link = null;

            if (int.TryParse(param, out id))
            {
                // Get event_participation_id of sponsor
                var _ep = _repo.GetEventParticipationId(id);

                // Load Links info
                if (_ep != null)
                {
                    _link = _repo.GetLinksInfoByEventParticipationId(_ep.Value);

                    if (_link != null)
                    {
                        var domain = string.Empty;
                        if (Request.RequestUri.Authority.Contains("test"))
                            domain = ConfigurationManager.AppSettings["TestMGPDomain"];
                        else
                            domain = ConfigurationManager.AppSettings["ProdMGPDomain"];

                        _link.MGPDomain = string.Concat("http://", domain);
                        _link.WelcomePage = string.Concat("http://", domain, "/", ConfigurationManager.AppSettings["WelcomePagePath"]);
                        _link.CampaignManagerHomePage = string.Concat("http://", domain, "/", 
                                                                      ConfigurationManager.AppSettings["CampaignManagerHomePagePath"], "?participantId=",
                                                                      _ep.Value);
                    }
                }

                // Load Comments
                _link.Comments = _repo.GetCommentsById(id);
            }

            return Request.CreateResponse(HttpStatusCode.OK, _link);
        }

        public HttpResponseMessage Post([FromBody]Models.Links links)
        {
            var message = string.Empty;

            bool result = _repo.UpdateLinks(User.Identity.Name, links, out message);

            if (result)
            {
                if (_repo.Save())
                {
                    // Load Comments
                    links.Comments = _repo.GetCommentsById(links.EventId);
                }
                else
                {
                    message = "Error saving links.";
                }
            } 

            if (message == string.Empty)
                return Request.CreateResponse(HttpStatusCode.OK, links);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, message);
        }
    }
}
