using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GA.BDC.Web.Custcare.Controllers
{
    public class MembersController : ApiController
    {
        const string ___MEMBERS_ACTION_REGULAR = "REGULAR";
        const string ___MEMBERS_ACTION_MOVIE = "MOVIE";

        GA.BDC.Web.Custcare.Repository.ICampaignRepository _repo;

        public MembersController(GA.BDC.Web.Custcare.Repository.ICampaignRepository repo)
        {
            _repo = repo;
        }

        public HttpResponseMessage Get(string action, string param)
        {
            if (action == ___MEMBERS_ACTION_REGULAR)
            {
                int id;
                List<Models.Members> _participants = null, _supporters = null;

                if (int.TryParse(param, out id))
                {
                    // Load Participants
                    _participants = _repo.GetParticipantsById(id).ToList();

                    // Load Supporters
                    _supporters = _repo.GetSupportersById(id).ToList();
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { _participants, _supporters });
            }
            else if (action == ___MEMBERS_ACTION_MOVIE)
            {
                int id;
                if (int.TryParse(param, out id))
                {
                    // Load Movie Ticket info
                    var prize = _repo.GetEarnedPrizeByEventParticipationId(id);
                    var data = string.Empty;

                    if (prize != null)
                    {
                        data = string.Concat(prize.PrizeItemCode, ";", prize.ExpirationDate, ";", prize.DateIssued);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }                
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Post(int movieTicketEpId, [FromBody]List<Models.Members> members)
        {
            var message = string.Empty;
            var data = string.Empty;

            bool result = false;

            if (movieTicketEpId == default(int))
            {
                result = _repo.UpdateMembers(members, out message);

                if (result)
                {
                    if (_repo.Save())
                    {
                        // No need to do anything.
                    }
                    else
                    {
                        message = "Error saving members.";
                    }
                } 
            }
            else
            {
                result = _repo.IssueMovieTicket(movieTicketEpId, out message);

                if (result)
                {
                    if (_repo.Save())
                    {
                        var prize = _repo.GetNewMovieTicket(movieTicketEpId);

                        if (prize != null)
                        {
                            data = string.Concat(prize.PrizeItemCode, ";", prize.ExpirationDate, ";", prize.DateIssued);
                        }
                    }
                    else
                    {
                        message = "Error issuing a new movie ticket.";
                    }
                }
            }
            

            if (message == string.Empty)
                return Request.CreateResponse(HttpStatusCode.OK, data);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, message);
        }
    }
}
