using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GA.BDC.Web.Custcare.Controllers
{
    public class OrdersController : ApiController
    {
        GA.BDC.Web.Custcare.Repository.ICampaignRepository _repo;

        public OrdersController(GA.BDC.Web.Custcare.Repository.ICampaignRepository repo)
        {
            _repo = repo;
        }

        public HttpResponseMessage Get(string param)
        {
            int id;
            Models.Orders _orders = null;

            if (int.TryParse(param, out id))
            {
                _orders = new Models.Orders();

                // Load Sales Info
                var sales_info = _repo.GetOrdersById(id);

                if (sales_info != null)
                {
                    _orders.SalesInfo = sales_info.ToList();
                }

                // Load Participant/Sponsor users
                var parent_users = _repo.GetParentUsersById(id);

                if (parent_users != null)
                {
                    _orders.ParentUsers = parent_users.ToList();
                }

                // Setup Profit Statement URL
                var groupId = _repo.GetGroupIdById(id);
                if (groupId != null)
                {
                    _orders.ProfitStatementUrl = string.Concat("GetSAPProfitCheckReport?groupId=", groupId);
                }                
            }

            return Request.CreateResponse(HttpStatusCode.OK, _orders);
        }

        public HttpResponseMessage Post(int eventParticipationId, int parentMemberHierarchyId)
        {
            var result = _repo.OrderTransfer(eventParticipationId, parentMemberHierarchyId);

            if (result)
            {
                if (_repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
