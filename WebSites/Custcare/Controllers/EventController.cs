using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GA.BDC.Web.Custcare.Controllers
{
    public class EventController : ApiController
    {
        GA.BDC.Web.Custcare.Repository.ICampaignRepository _repo;

        public EventController(GA.BDC.Web.Custcare.Repository.ICampaignRepository repo)
        {
            _repo = repo;
        }

        public HttpResponseMessage Get(Models.EventSearch type, string param)
        {
            int id;
            var _events = new List<Models.Event>();
            switch (type)
            {
                case GA.BDC.Web.Custcare.Models.EventSearch.ByEventId:
                    if (int.TryParse(param, out id))
                    {
                        var data = _repo.GetEventById(id);

                        if (data != null)
                            _events.Add(data);
                    }
                    break;
                case GA.BDC.Web.Custcare.Models.EventSearch.ByLeadId:
                    if (int.TryParse(param, out id))
                    {
                        var data = _repo.GetEventByLeadId(id);

                        if (data != null)
                            _events.Add(data);
                    }
                    break;
                case GA.BDC.Web.Custcare.Models.EventSearch.ByEventName:
                    if (!string.IsNullOrEmpty(param))
                    {
                        var data = _repo.GetEventsByName(param);

                        if (data != null)
                            _events.AddRange(data.ToList());
                    }
                    break;
                case GA.BDC.Web.Custcare.Models.EventSearch.ByEmail:
                    if (!string.IsNullOrEmpty(param))
                    {
                        var data = _repo.GetEventsByEmail(param);

                        if (data != null)
                            _events.AddRange(data.ToList());
                    }
                    break;
                case GA.BDC.Web.Custcare.Models.EventSearch.BySponsorName:
                    if (!string.IsNullOrEmpty(param))
                    {
                        var data = _repo.GetEventsBySponsorName(param);

                        if (data != null)
                            _events.AddRange(data.ToList());
                    }
                    break;
                default:
                    break;
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, _events.Any() ? _events : null);
        }

        public HttpResponseMessage Post([FromBody]Models.Event evt)
        {
            if (evt.Active && evt.EndDate != null)
                evt.EndDate = null;
            else if (!evt.Active && evt.EndDate == null)
                evt.EndDate = DateTime.Now;

            _repo.UpdateEvent(evt);

            if (_repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.OK, evt);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
