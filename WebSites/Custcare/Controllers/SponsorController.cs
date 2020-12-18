using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GA.BDC.Web.Custcare.Controllers
{
    public class SponsorController : ApiController
    {
        const string ___ACTION_TYPE_REGULAR = "REGULAR";
        const string ___ACTION_TYPE_MOVIE_TICKET_ISSUE = "MOVIE";

        GA.BDC.Web.Custcare.Repository.ICampaignRepository _repo;

        public SponsorController(GA.BDC.Web.Custcare.Repository.ICampaignRepository repo)
        {
            _repo = repo;
        }

        public HttpResponseMessage Get(string param)
        {
            int id;
            Models.Sponsor _sponsor = null;

            if (int.TryParse(param, out id))
            {
                // Load Sponsor info
                _sponsor = _repo.GetSponsorById(id);

                // Load Movie Ticket info
                _sponsor.EarnedPrize = _repo.GetEarnedPrizeByEventParticipationId(_sponsor.EventParticipationId);

                // Load Comments
                _sponsor.Comments = _repo.GetCommentsById(id);
            }

            return Request.CreateResponse(HttpStatusCode.OK, _sponsor);
        }

        public HttpResponseMessage Post(string action, [FromBody]Models.Sponsor sponsor)
        {
            var message = string.Empty;

            bool result = false;

            if (action == ___ACTION_TYPE_REGULAR)
            {
                result = _repo.UpdateSponsor(User.Identity.Name, sponsor, out message);

                if (result)
                {
                    if (_repo.Save())
                    {
                        if (sponsor.EarnedPrize != null)
                            sponsor.EarnedPrize.NewMovieCode = false;

                        // Load Comments
                        sponsor.Comments = _repo.GetCommentsById(sponsor.EventId);
                    }
                    else
                    {
                        message = "Error saving sponsor.";
                    }
                }
            }
            else if (action == ___ACTION_TYPE_MOVIE_TICKET_ISSUE)
            {
                result = _repo.IssueMovieTicket(sponsor.EventParticipationId, out message);

                if (result)
                {
                    if (_repo.Save())
                    {
                        sponsor.EarnedPrize = _repo.GetNewMovieTicket(sponsor.EventParticipationId);
                    }
                    else
                    {
                        message = "Error issuing a new movie ticket.";
                    }
                }
            }
            else
            {
                message = "Unknown action type.";
            }

            if (message == string.Empty)
                return Request.CreateResponse(HttpStatusCode.OK, sponsor);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, message);           
        }
    }
}
