using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GA.BDC.Web.Custcare.Repository
{
    public class CampaignRepository : ICampaignRepository
    {        
        GA.BDC.Data.MGP.esubs_global_v2.Models.DataProvider _eSubsCtx;
        GA.BDC.Data.MGP.esubs_global_v2.LINQ.DataProviderDataContext _eSubsLinqCtx;
        GA.BDC.Data.MGP.EFRCommon.Models.DataProvider _efrCommonCtx;

        public CampaignRepository(GA.BDC.Data.MGP.esubs_global_v2.Models.DataProvider eSubsCtx,
                                  GA.BDC.Data.MGP.esubs_global_v2.LINQ.DataProviderDataContext eSubsLinqCtx,
                                  GA.BDC.Data.MGP.EFRCommon.Models.DataProvider efrCommonCtx)
        {
            _eSubsCtx = eSubsCtx;
            _eSubsLinqCtx = eSubsLinqCtx;
            _efrCommonCtx = efrCommonCtx;
        }

        #region ICampaignRepository Members

        public Models.Event GetEventById(int eventId)
        {
            var _evt = _eSubsCtx.events.SingleOrDefault(e => e.event_id == eventId);

            if (_evt == null)
            {
                return null;
            }
            else
            {
                return EventMapper(_evt);
            }            
        }

        public Models.Event GetEventByLeadId(int leadId)
        {
            var _lead = _eSubsLinqCtx.cc_search_by_lead_id(leadId).FirstOrDefault();

            if (_lead == null)
            {
                return null;
            }
            else
            {
                var evtId = _lead.event_id;

                var _evt = _eSubsCtx.events.Single(e => e.event_id == evtId);

                return EventMapper(_evt);
            }
        }

        public IEnumerable<Models.Event> GetEventsByName(string eventName)
        {
            var _evts = _eSubsCtx.events.Where(e => e.event_name.Contains(eventName));

            if (_evts == null)
            {
                return null;
            }
            else
            {
                return _evts.ToList().Select(e => EventMapper(e));
            }
        }

        public IEnumerable<Models.Event> GetEventsByEmail(string email)
        {
            var _email = _eSubsLinqCtx.cc_search_by_email(email);

            if (_email == null)
            {
                return null;
            }
            else
            {
                var _evts = new List<Data.MGP.esubs_global_v2.Models._event>();

                foreach (var em in _email)
                {
                    _evts.Add(_eSubsCtx.events.Single(e => e.event_id == em.event_id));
                }

                return _evts.ToList().Select(e => EventMapper(e));
            }
        }

        public IEnumerable<Models.Event> GetEventsBySponsorName(string sponsorName)
        {
            var _sponsors = _eSubsLinqCtx.cc_search_by_member_name(sponsorName);

            if (_sponsors == null)
            {
                return null;
            }
            else
            {
                var _evts = new List<Data.MGP.esubs_global_v2.Models._event>();

                foreach (var em in _sponsors)
                {
                    _evts.Add(_eSubsCtx.events.Single(e => e.event_id == em.event_id));
                }

                return _evts.ToList().Select(e => EventMapper(e));
            }
        }

        public void UpdateEvent(Models.Event evt)
        {
            try
            {
                var _event = _eSubsCtx.events.Single(e => e.event_id == evt.EventId);

                _event.event_type_id = evt.EventTypeId;
                _event.culture_code = evt.CultureCode;
                _event.event_name = evt.EventName;
                _event.start_date = evt.StartDate;
                _event.end_date = evt.EndDate;
                _event.active = evt.Active;
                _event.comments = evt.Comments;
                _event.create_date = evt.CreateDate;
                _event.redirect = evt.Redirect;
                _event.displayable = evt.Displayable;
                _event.want_sales_rep_call = evt.WantSalesRepCall;
                _event.group_type_id = evt.GroupTypeId;
                _event.processing_fee = evt.ProcessingFee;
                _event.profit_calculated = evt.ProfitCalculated;
                _event.event_status_id = evt.EventStatusId;
                _event.profit_group_id = evt.ProfitGroupId;
                _event.donation = evt.Donation;
                _event.date_of_event = evt.DateOfEvent;
                _event.discount_site = evt.DiscountSite;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEvent(Models.Event evt)
        {
            throw new NotImplementedException();
        }

        public Models.Sponsor GetSponsorById(int eventId)
        {
            var _sp = _eSubsLinqCtx.cc_get_sponsor_info(eventId);

            if (_sp == null)
            {
                return null;
            }
            else
            {
                return _sp.Select(s => SponsorMapper(s)).First();
            }
        }

        public bool UpdateSponsor(string adminUser, Models.Sponsor sponsor, out string message)
        {
            try
            {
                message = string.Empty;

                // Update first and last name
                var _user = (from ep in _eSubsCtx.event_participation
                             from mh in _eSubsCtx.member_hierarchy
                             from m in _eSubsCtx.members
                             from u in _eSubsCtx.users
                             where ep.event_participation_id == sponsor.EventParticipationId
                                && ep.member_hierarchy_id == mh.member_hierarchy_id
                                && mh.member_id == m.member_id
                                && m.user_id == u.user_id
                             select u).FirstOrDefault();

                _user.first_name = sponsor.FirstName;
                _user.last_name = sponsor.LastName;

                // Update email and password
                _user.password = sponsor.Password;
                if (!_user.email_address.Equals(sponsor.EmailAddress, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (_eSubsCtx.users.Any(u => u.email_address.Equals(sponsor.EmailAddress, StringComparison.InvariantCultureIgnoreCase) &&
                                                 u.partner_id == _user.partner_id))
                    {
                        message = "Username/Email already exists.";
                        return false;
                    }
                    else
                    {
                        _user.email_address = sponsor.EmailAddress;
                    }
                }

                // Update unsubscribe
                _eSubsLinqCtx.cc_unsubscribe(sponsor.EventParticipationId, sponsor.Unsubscribed == 1);

                // Update comments
                var comment = _eSubsCtx.custcare_comments.FirstOrDefault(c => c.event_id == sponsor.EventId);

                if (comment == null)
                {
                    _eSubsCtx.custcare_comments.Add(new Data.MGP.esubs_global_v2.Models.custcare_comments
                    {
                        event_id = sponsor.EventId,
                        comments = "Sponsor information was changed by " + adminUser + " on " + DateTime.Now,
                        create_date = DateTime.Now
                    });
                }
                else
                {
                    comment.comments += "<br/><br/>Sponsor information was changed by " + adminUser + " on " + DateTime.Now;
                }

                // Update telephone
                bool doSave = false;
                Data.MGP.esubs_global_v2.Models.phone_number phone = null;
                if (sponsor.PhoneNumber != null)
                {
                    if (sponsor.PhoneNumber == string.Empty)
                        sponsor.PhoneNumber = "000-000-0000";

                    var _telephone = (from pi in _eSubsCtx.payment_info
                                      from ph in _eSubsCtx.phone_number
                                      where pi.event_id == sponsor.EventId && pi.active
                                         && pi.phone_number_id == ph.phone_number_id
                                      select ph).SingleOrDefault();

                    if (_telephone == null)
                    {
                        doSave = true;

                        phone = new Data.MGP.esubs_global_v2.Models.phone_number
                        {
                            phone_number1 = sponsor.PhoneNumber,
                            create_date = System.DateTime.Now
                        };

                        _eSubsCtx.phone_number.Add(phone);
                    }
                    else
                    {
                        _telephone.phone_number1 = sponsor.PhoneNumber;
                    }
                }

                // Do an advance Save operation to get the phone_number_id
                if (doSave)
                {
                    var result = Save();

                    if (!result)
                    {
                        message = "Error saving sponsor!";
                        return false;
                    }
                    else
                    {
                        var _pi = _eSubsCtx.payment_info.SingleOrDefault(pi => pi.event_id == sponsor.EventId && pi.active);

                        if (_pi != null)
                        {
                            _pi.phone_number_id = phone.phone_number_id;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Models.User> GetUsersByEmail(string emailAddress)
        {
            var _users = _eSubsCtx.users.Where(u => u.email_address.IndexOf(emailAddress, StringComparison.OrdinalIgnoreCase) >= 0);

            if (_users == null)
            {
                return null;
            }
            else
            {
                return _users.ToList().Select(u => UserMapper(u));
            }
        }

        public IEnumerable<Models.User> GetUsersByName(string memberName)
        {
            var _users = _eSubsCtx.users.Where(u => u.first_name.IndexOf(memberName, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                    u.last_name.IndexOf(memberName, StringComparison.OrdinalIgnoreCase) >= 0);

            if (_users == null)
            {
                return null;
            }
            else
            {
                return _users.ToList().Select(u => UserMapper(u));
            }
        }

        public void UpdateUser(Models.User user)
        {
            throw new NotImplementedException();
        }

        public Models.PostalAddress GetShippingAddressById(int eventId)
        {
            var _address = (from pi in _eSubsCtx.payment_info
                            from pa in _eSubsCtx.postal_address
                            where pi.event_id == eventId && pi.active
                               && pi.postal_address_id == pa.postal_address_id
                            select pa).SingleOrDefault();

            if (_address == null)
            {
                return null;
            }
            else
            {
                return AddressMapper(_address);
            }
        }

        public Models.Partner GetPartnerById(int partnerId)
        {
            var _partner = _efrCommonCtx.partners.SingleOrDefault(p => p.partner_id == partnerId);

            if (_partner == null)
            {
                return null;
            }
            else
            {
                return PartnerMapper(_partner);
            }
        }

        public Models.Group GetGroupById(int eventId)
        {
            var _group = (from e in _eSubsCtx.events
                          from eg in _eSubsCtx.event_group
                          from g in _eSubsCtx.groups
                          where e.event_id == eventId
                             && e.event_id == eg.event_id
                             && eg.group_id == g.group_id
                          select g).SingleOrDefault();

            if (_group == null)
            {
                return null;
            }
            else
            {
                return GroupMapper(_group);
            }            
        }

        public Models.PaymentInfo GetPaymentInfoById(int eventId)
        {
            var _pa = (from pi in _eSubsCtx.payment_info
                       where pi.event_id == eventId && pi.active
                       select pi).SingleOrDefault();

            if (_pa == null)
            {
                return null;
            }
            else
            {
                return PaymentInfoMapper(_pa);
            }
        }

        public IQueryable<Models.AddressState> GetStatesByCountryCode(string countryCode)
        {
            var _states = (from c in _eSubsCtx.countries
                           from s in _eSubsCtx.subdivisions
                           where c.country_code == s.country_code
                              && c.country_code == countryCode
                           select new Models.AddressState
                           {
                               CountryCode = countryCode,
                               StateName = s.subdivision_name_1,
                               SubdivisionCode = s.subdivision_code
                           });

            if (_states == null)
            {
                return null;
            }
            else
            {
                return _states;
            }  
        }

        public bool InitializePaymentInfoById(int eventId)
        {
            var result = _eSubsLinqCtx.es_init_payment_info(eventId);

            if (result < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void UpdatePaymentInfo(Models.PaymentInfo pi)
        {
            try
            {
                var _pi = _eSubsCtx.payment_info.Single(p => p.payment_info_id == pi.PaymentInfoId);

                _pi.event_id = pi.EventId;
                _pi.group_id = pi.GroupId;
                _pi.postal_address_id = pi.PostalAddressId;
                _pi.phone_number_id = pi.PhoneNumberId;
                _pi.payment_name = pi.PaymentName;
                _pi.on_behalf_of_name = pi.OnBehalfOfName;
                _pi.ship_to_name = pi.ShipToName;
                _pi.ssn = pi.SSN;
                _pi.active = pi.Active;
                _pi.create_date = pi.CreateDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateShipping(Models.PostalAddress pa)
        {
            try
            {
                var _pa = _eSubsCtx.postal_address.Single(a => a.postal_address_id == pa.PostalAddressId);

                _pa.address_1 = pa.Address1;
                _pa.address_2 = pa.Address2;
                _pa.city = pa.City;
                _pa.country_code = pa.CountryCode;
                _pa.subdivision_code = pa.SubdivisionCode;
                _pa.zip_code = pa.ZipCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertShipping(Models.PostalAddress pa)
        {
            try
            {
                var _pa = _eSubsCtx.postal_address.Add(new Data.MGP.esubs_global_v2.Models.postal_address
                {
                    address_1 = pa.Address1,
                    address_2 = pa.Address2,
                    city = pa.City,
                    country_code = pa.CountryCode,
                    subdivision_code = pa.SubdivisionCode,
                    zip_code = pa.ZipCode,
                    create_date = DateTime.Now
                });

                var result = Save();

                if (!result)
                {
                    return -1;
                }
                else
                {
                    return _pa.postal_address_id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetExternalGroupId(int groupId)
        {
            var _grp = _eSubsCtx.groups.Where(g => g.group_id == groupId).SingleOrDefault();

            if (_grp == null)
            {
                return null;
            }
            else
            {
                return _grp.external_group_id;
            }
        }

        public bool UpdateExternalGroupId(Models.Group grp, int? LinkToEventId, out string message)
        {
            try
            {
                message = string.Empty;

                var _grpTo = _eSubsCtx.groups.Single(g => g.group_id == grp.GroupId);

                if (LinkToEventId != null)
                {
                    var _grpFrom = (from e in _eSubsCtx.events
                                    from eg in _eSubsCtx.event_group
                                    from g in _eSubsCtx.groups
                                    where e.event_id == LinkToEventId.Value
                                       && e.event_id == eg.event_id
                                       && eg.group_id == g.group_id
                                    select g).SingleOrDefault();

                    if (_grpFrom == null)
                    {
                        message = "Invalid link to event id = " + LinkToEventId.Value;
                        return false;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(_grpFrom.external_group_id))
                        {
                            message = "Not a valid XFactor event (i.e. eTeamz, LLU etc).";
                            return false;
                        }
                        else
                        {
                            _grpTo.comments = "Linked ext. grpId from group id = " + _grpFrom.group_id;
                            _grpFrom.comments = "Removed ext. grpId = '" + _grpFrom.external_group_id + "' and linked to group id = " + _grpTo.group_id;
                            _grpTo.external_group_id = _grpFrom.external_group_id;
                            _grpFrom.external_group_id = null;                            
                        }
                    }
                }
                else
                {
                    _grpTo.external_group_id = grp.ExternalGroupId;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Models.Members> GetParticipantsById(int eventId)
        {
            var _participants = _eSubsLinqCtx.cc_get_participant_stats(eventId);

            if (_participants == null)
            {
                return null;
            }
            else
            {
                return _participants.Select(o => ParticipantsMapper(o));
            }
        }

        public IEnumerable<Models.Members> GetSupportersById(int eventId)
        {
            var _supporters = _eSubsLinqCtx.cc_get_supporters_invited(eventId, null);

            if (_supporters == null)
            {
                return null;
            }
            else
            {
                return _supporters.Select(o => SupportersMapper(o));
            }
        }

        public bool UpdateMembers(List<Models.Members> members, out string message)
        {
            try
            {
                message = string.Empty;

                // Update member info
                foreach (var member in members)
                {
                    var _member = (from ep in _eSubsCtx.event_participation
                                 from mh in _eSubsCtx.member_hierarchy
                                 from m in _eSubsCtx.members
                                 where ep.event_participation_id == member.EventParticipationId
                                    && ep.member_hierarchy_id == mh.member_hierarchy_id
                                    && mh.member_id == m.member_id
                                 select m).SingleOrDefault();

                    var _memberHierarchy = _member != null ? _member.member_hierarchy.First() : null;

                    if (_member != null)
                    {
                        if (_member.deleted != member.Deleted)
                        {
                            _member.deleted = member.Deleted;
                        }

                        if (_member.unsubscribe != member.Unsubscribed)
                        {
                            _member.unsubscribe = member.Unsubscribed;

                            if (_member.opt_status_id != 2 && member.Unsubscribed)
                            {
                                _member.opt_status_id = 2;
                            }
                            else if (_member.opt_status_id != 1 && !member.Unsubscribed)
                            {
                                _member.opt_status_id = 1;
                            }
                        }

                        if (_member.user_id != null)
                        {
                            var _user = _eSubsCtx.users.Single(u => u.user_id == _member.user_id);

                            if (_user.unsubscribe == null)
                            {
                                if (member.Unsubscribed)
                                {
                                    _user.unsubscribe = true;
                                }
                            }
                            else if (_user.unsubscribe != member.Unsubscribed)
                            {
                                _user.unsubscribe = member.Unsubscribed;
                            }

                            if (_user.opt_status_id && member.Unsubscribed)
                            {
                                _user.opt_status_id = false;
                                _user.unsubscribe_date = DateTime.Now;
                            }
                            else if (!_user.opt_status_id && !member.Unsubscribed)
                            {
                                _user.opt_status_id = true;
                                _user.unsubscribe_date = null;
                            }
                        }
                    }

                    if (_memberHierarchy != null)
                    {
                        if(_memberHierarchy.unsubscribe != member.Unsubscribed)
                        {
                            _memberHierarchy.unsubscribe = member.Unsubscribed;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Models.Order> GetOrdersById(int eventId)
        {
            var _orders = _eSubsLinqCtx.cc_get_orders_for_campaign(eventId);

            if (_orders == null)
            {
                return null;
            }
            else
            {
                return _orders.Select(o => OrdersMapper(o));
            }
        }

        public IQueryable<Models.ParentUser> GetParentUsersById(int eventId)
        {
            var _pa = (from m in _eSubsCtx.members
                       from u in _eSubsCtx.users
                       from mh in _eSubsCtx.member_hierarchy
                       from ep in _eSubsCtx.event_participation
                       where m.user_id == u.user_id
                          && m.member_id == mh.member_id
                          && mh.member_hierarchy_id == ep.member_hierarchy_id
                          && ep.event_id == eventId
                          && mh.active
                          && !m.deleted
                       orderby u.first_name, u.last_name
                       select new Models.ParentUser
                       {
                           UserId = u.user_id,
                           CultureCode = u.culture_code,
                           FirstName = u.first_name,
                           LastName = u.last_name,
                           EmailAddress = u.email_address,
                           Username = u.username,
                           Password = u.password,
                           PartnerId = u.partner_id,
                           CreateDate = u.create_date,
                           MemberId = u.member_id,
                           CoppaMonth = u.coppa_month,
                           CoppaYear = u.coppa_year,
                           AgreeTermServices = u.agree_term_services,
                           Unsubscribe = u.unsubscribe,
                           UnsubscribeDate = u.unsubscribe_date,
                           IsFirstLogin = u.is_first_login,
                           MemberHierarchyId = mh.member_hierarchy_id,
                           EventParticipationId = ep.event_participation_id
                       });

            if (_pa == null)
            {
                return null;
            }
            else
            {
                return _pa;
            }
        }

        public void UpdateParentMemberHierarchy(int memberHierarchyId, int newParentMemberHierarchyId)
        {
            var _mh = _eSubsCtx.member_hierarchy.Single(mh => mh.member_hierarchy_id == memberHierarchyId);

            _mh.parent_member_hierarchy_id = newParentMemberHierarchyId;
        }

        public bool OrderTransfer(int eventParticipationId, int newParentMemberHierarchyId)
        {
            var result = _eSubsLinqCtx.cc_order_transfer(eventParticipationId, newParentMemberHierarchyId);

            if (result < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int? GetGroupIdById(int eventId)
        {
            var _grp = _eSubsCtx.event_group.FirstOrDefault(eg => eg.event_id == eventId);

            if (_grp == null)
            {
                return null;
            }
            else
            {
                return _grp.group_id;
            }
        }

        public Models.Prize GetEarnedPrizeByEventParticipationId(int eventParticipationId)
        {
            var _pr = (from ep in _eSubsCtx.earned_prize
                       from pi in _eSubsCtx.prize_item
                       where ep.prize_item_id == pi.prize_item_id
                          && ep.event_participation_id == eventParticipationId
                       orderby pi.create_date descending
                       select new Models.Prize
                       {
                           PrizeId = pi.prize_id,
                           EventParticipationId = ep.event_participation_id,
                           PrizeItemCode = pi.prize_item_code,
                           ExpirationDate = pi.expiration_date,
                           DateIssued = pi.create_date
                       }).FirstOrDefault();

            if (_pr == null)
            {
                return null;
            }
            else
            {
                return _pr;
            }
        }
        
        public bool IssueMovieTicket(int eventParticipationId, out string message)
        {
            try
            {
                message = string.Empty;

                // Issue a new movie ticket (Prize ID = 6)
                var result = _eSubsLinqCtx.cc_issue_movie_ticket(eventParticipationId);

                var code = result.First().Column1;

                if (!code.StartsWith("[") && !code.EndsWith("]"))
                {
                    return true;
                }
                else
                {
                    message = code.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Models.Prize GetNewMovieTicket(int eventParticipationId)
        {
            var _prize = _eSubsCtx.earned_prize.Where(ep => ep.event_participation_id == eventParticipationId && ep.prize_item.prize_id == 6)
                                               .OrderByDescending(ep => ep.create_date)
                                               .FirstOrDefault();

            if (_prize == null)
            {
                return null;
            }
            else
            {
                return new Models.Prize
                {
                    PrizeId = _prize.prize_item.prize_id,
                    EventParticipationId = _prize.event_participation_id,
                    PrizeItemCode = _prize.prize_item.prize_item_code,
                    ExpirationDate = _prize.prize_item.expiration_date,
                    DateIssued = _prize.create_date,
                    NewMovieCode = true
                };
            }
        }

        public string GetCommentsById(int eventId)
        {
            var _cmt = _eSubsCtx.custcare_comments.FirstOrDefault(cc => cc.event_id == eventId);

            if (_cmt == null)
            {
                return null;
            }
            else
            {
                return _cmt.comments;
            }
        }

        public Models.Links GetLinksInfoByEventParticipationId(int eventParticipationId)
        {
            var _link = _eSubsLinqCtx.cc_get_links_info(eventParticipationId);

            if (_link == null)
            {
                return null;
            }
            else
            {
                return _link.Select(o => LinkMapper(o)).First();
            }
        }

        public int? GetEventParticipationId(int eventId)
        {
            var _ep = _eSubsCtx.event_participation.FirstOrDefault(ep => ep.event_id == eventId && ep.participation_channel_id == 3);

            if (_ep == null)
            {
                return null;
            }
            else
            {
                return _ep.event_participation_id;
            }
        }

        public bool UpdateLinks(string adminUser, Models.Links links, out string message)
        {
            try
            {
                message = string.Empty;

                // Update comments
                var comment = _eSubsCtx.custcare_comments.FirstOrDefault(c => c.event_id == links.EventId);

                if (comment == null)
                {
                    _eSubsCtx.custcare_comments.Add(new Data.MGP.esubs_global_v2.Models.custcare_comments
                    {
                        event_id = links.EventId,
                        comments = "By " + adminUser + " on " + DateTime.Now + "<br/>" + links.NewComments,
                        create_date = DateTime.Now
                    });
                }
                else
                {
                    comment.comments += "<br/><br/>By " + adminUser + " on " + DateTime.Now + ":<br/>" + links.NewComments;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Models.MemberPassword> GetMemberPasswordsByEmail(string email)
        {
            var _mp = _eSubsLinqCtx.cc_get_member_by_email(email);

            if (_mp == null)
            {
                return null;
            }
            else
            {
                var result = _mp.Select(s => MemberPasswordMapper(s)).ToList();

                // Group by multiple columns to remove duplicates
                result = result.GroupBy(x => new { x.UserId, x.PartnerId, x.PartnerName, x.Email, x.Password, x.Name })
                               .Select(y => new Models.MemberPassword
                                            {
                                                UserId = y.Key.UserId,
                                                PartnerId = y.Key.PartnerId,
                                                PartnerName = y.Key.PartnerName,
                                                Email = y.Key.Email,
                                                Password = y.Key.Password,
                                                Name = y.Key.Name                                                
                                            }).ToList();

                return result;
            }
        }

        public void UpdateUserPassword(int userId, string newPassword)
        {
            try
            {
                var _user = _eSubsCtx.users.Single(u => u.user_id == userId);

                _user.password = newPassword;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Save()
        {
            try
            {
                var result = _eSubsCtx.SaveChanges();

                return result >= 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Private Members

        private Models.Event EventMapper(GA.BDC.Data.MGP.esubs_global_v2.Models._event evt)
        {
            return new Models.Event
            {
                EventId = evt.event_id,
                EventTypeId = evt.event_type_id,
                CultureCode = evt.culture_code,
                EventName = evt.event_name,
                StartDate = evt.start_date,
                EndDate = evt.end_date,
                Active = evt.active,
                Comments = evt.comments,
                CreateDate = evt.create_date,
                Redirect = evt.redirect,
                Displayable = evt.displayable,
                WantSalesRepCall = evt.want_sales_rep_call,
                GroupTypeId = evt.group_type_id,
                ProcessingFee = evt.processing_fee,
                ProfitCalculated = evt.profit_calculated,
                EventStatusId = evt.event_status_id,
                ProfitGroupId = evt.profit_group_id,
                Donation = evt.donation,
                DateOfEvent = evt.date_of_event,
                DiscountSite = evt.discount_site
            };
        }

        private Models.Sponsor SponsorMapper(GA.BDC.Data.MGP.esubs_global_v2.LINQ.cc_get_sponsor_infoResult sp)
        {
            return new Models.Sponsor
            {
                PartnerName = sp.partner_name,
                GroupName = sp.group_name,
                EventActive = sp.active,
                EventCreateDate = sp.create_date,
                EventEndDate = sp.end_date,
                EventName = sp.event_name,
                EventId = sp.event_id,
                PhoneNumber = sp.phone_number,
                EmailAddress = sp.email_address,
                Password = sp.password,
                Name = sp.name,
                FirstName = sp.first_name,
                LastName = sp.last_name,
                EventParticipationId = sp.event_participation_id,
                Unsubscribed = sp.unsubscribed,
                GroupId = sp.group_id,
                MovieTicket = sp.movie_ticket
            };
        }

        private Models.User UserMapper(GA.BDC.Data.MGP.esubs_global_v2.Models.user user)
        {
            return new Models.User
            {
                UserId = user.user_id,
                CultureCode = user.culture_code,
                FirstName = user.first_name,
                LastName = user.last_name,
                EmailAddress = user.email_address,
                Username = user.username,
                Password = user.password,
                PartnerId = user.partner_id,
                CreateDate = user.create_date,
                MemberId = user.member_id,
                CoppaMonth = user.coppa_month,
                CoppaYear = user.coppa_year,
                AgreeTermServices = user.agree_term_services,
                Unsubscribe = user.unsubscribe,
                UnsubscribeDate = user.unsubscribe_date,
                OptStatusId = user.opt_status_id,
                IsFirstLogin = user.is_first_login
            };
        }

        private Models.PostalAddress AddressMapper(GA.BDC.Data.MGP.esubs_global_v2.Models.postal_address pa)
        {
            return new Models.PostalAddress
            {
                PostalAddressId = pa.postal_address_id,
                Address1 = pa.address_1 ?? string.Empty,
                Address2 = pa.address_2 ?? string.Empty,
                City = pa.city ?? string.Empty,
                ZipCode = pa.zip_code ?? string.Empty,
                CountryCode = pa.country_code ?? string.Empty,
                SubdivisionCode = pa.subdivision_code ?? string.Empty
            };
        }

        private Models.Partner PartnerMapper(GA.BDC.Data.MGP.EFRCommon.Models.partner p)
        {
            return new Models.Partner
            {
                PartnerId = p.partner_id,
                PartnerName = p.partner_name,
                PartnerTypeId = p.partner_type_id,
                PartnerTypeName = p.partner_type.partner_type_name,
                Guid = p.guid,
                HasCollectionSite = p.has_collection_site,
                IsActive = p.is_active,
                CreateDate = p.create_date
            };
        }

        private Models.Group GroupMapper(GA.BDC.Data.MGP.esubs_global_v2.Models.group grp)
        {
            return new Models.Group
            {
                GroupId = grp.group_id,
                GroupName = grp.group_name,
                PartnerId = grp.partner_id,
                SponsorId = grp.sponsor_id,
                LeadId = grp.lead_id,
                ExternalGroupId = grp.external_group_id,
                ExpectedMemberShip = grp.expected_membership,
                CreateDate = grp.create_date
            };
        }

        private Models.PaymentInfo PaymentInfoMapper(GA.BDC.Data.MGP.esubs_global_v2.Models.payment_info pa)
        {
            return new Models.PaymentInfo
            {
                PaymentInfoId = pa.payment_info_id,
                EventId = pa.event_id,
                GroupId = pa.group_id,
                PostalAddressId = pa.postal_address_id,
                PhoneNumberId = pa.phone_number_id,
                PaymentName = pa.payment_name,
                OnBehalfOfName = pa.on_behalf_of_name,
                ShipToName = pa.ship_to_name,
                SSN = pa.ssn,
                Active = pa.active,
                CreateDate = pa.create_date
            };
        }

        private Models.Members ParticipantsMapper(GA.BDC.Data.MGP.esubs_global_v2.LINQ.cc_get_participant_statsResult part)
        {
            return new Models.Members
            {
                Name = part.participant_name,
                EmailsSent = part.email_sent,
                NumOfSubs = part.nb_subs,
                AmountSold = part.amount_sold,
                Profit = part.profit,
                EventParticipationId = part.participant_id,
                EmailAddress = part.email_address,
                Password = part.password,
                BouncedCount = part.bounced_count,
                CreateDate = part.create_date,
                MovieTicket = part.movie_ticket,
                Active = part.active,
                Unsubscribed = part.unsubscribed,
                Deleted = part.deleted,
                MemberHierarchyId = part.member_hierarchy_id
            };
        }

        private Models.Members SupportersMapper(GA.BDC.Data.MGP.esubs_global_v2.LINQ.cc_get_supporters_invitedResult supp)
        {
            return new Models.Members
            {
                Name = supp.supporter_name,
                EventParticipationId = supp.supporter_id,
                NumOfSubs = supp.nb_subs,
                AmountSold = supp.amount,
                Profit = supp.profit != null ? supp.profit.Value : default(double),
                Bounced = supp.bounced,
                Unsubscribed = supp.unsubscribed,
                OrderDate = supp.orderdate,
                CreateDate = supp.create_date,                
                EmailAddress = supp.email_address,
                MemberHierarchyId = supp.member_hierarchy_id,
                ParentMemberHierarchyId = supp.parent_member_hierarchy_id
            };
        }

        private Models.Order OrdersMapper(GA.BDC.Data.MGP.esubs_global_v2.LINQ.cc_get_orders_for_campaignResult ord)
        {
            return new Models.Order
            {
                Amount = ord.amount,
                CatalogItemTitle = ord.CatalogItemTitle,
                CustomerName = ord.customer_Name,
                EdsId = ord.eds_id,
                MemberHierarchyId = ord.member_hierarchy_id,
                MemberName = ord.member_name,
                NbSubs = ord.nb_subs,
                OrderDate = ord.orderDate,
                OrderID = ord.OrderID,
                ParentMemberHierarchy_id = ord.parent_member_hierarchy_id,
                ParentName = ord.parent_name,
                UserType = ord.user_type,
                EventParticipationId = ord.event_participation_id
            };
        }

        private Models.Links LinkMapper(GA.BDC.Data.MGP.esubs_global_v2.LINQ.cc_get_links_infoResult link)
        {
            return new Models.Links
            {
                EventId = link.event_id,
                MemberHierarchyId = link.member_hierarchy_id,
                Redirect = link.redirect
            };
        }

        private Models.MemberPassword MemberPasswordMapper(GA.BDC.Data.MGP.esubs_global_v2.LINQ.cc_get_member_by_emailResult mp)
        {
            return new Models.MemberPassword
            {
                UserId = mp.user_id,
                PartnerId = mp.partner_id.Value,
                PartnerName = mp.partner_name,
                Email = mp.email_address,
                Name = (mp.first_name + " " + mp.last_name).Trim(),
                Password = mp.password
            };
        }

        #endregion
    }
}