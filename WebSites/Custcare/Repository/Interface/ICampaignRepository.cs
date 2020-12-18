using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Web.Custcare.Repository
{
    public interface ICampaignRepository
    {
        Models.Event GetEventById(int eventId);
        Models.Event GetEventByLeadId(int leadId);        
        IEnumerable<Models.Event> GetEventsByName(string eventName);
        IEnumerable<Models.Event> GetEventsByEmail(string email);
        IEnumerable<Models.Event> GetEventsBySponsorName(string sponsorName);
        void UpdateEvent(Models.Event evt);
        void DeleteEvent(Models.Event evt);

        Models.Sponsor GetSponsorById(int eventId);
        bool UpdateSponsor(string adminUser, Models.Sponsor sponsor, out string message);

        IEnumerable<Models.User> GetUsersByEmail(string emailAddress);
        IEnumerable<Models.User> GetUsersByName(string memberName);
        void UpdateUser(Models.User user);

        Models.PostalAddress GetShippingAddressById(int eventId);

        Models.Partner GetPartnerById(int partnerId);

        Models.Group GetGroupById(int eventId);
        Models.PaymentInfo GetPaymentInfoById(int eventId);

        IQueryable<Models.AddressState> GetStatesByCountryCode(string countryCode);
        bool InitializePaymentInfoById(int eventId);
        void UpdatePaymentInfo(Models.PaymentInfo pi);
        void UpdateShipping(Models.PostalAddress pa);
        int InsertShipping(Models.PostalAddress pa);
        string GetExternalGroupId(int groupId);
        bool UpdateExternalGroupId(Models.Group grp, int? LinkToEventId, out string message);

        IEnumerable<Models.Members> GetParticipantsById(int eventId);
        IEnumerable<Models.Members> GetSupportersById(int eventId);
        bool UpdateMembers(List<Models.Members> participants, out string message);

        IEnumerable<Models.Order> GetOrdersById(int eventId);

        IQueryable<Models.ParentUser> GetParentUsersById(int eventId);
        void UpdateParentMemberHierarchy(int memberHierarchyId, int newParentMemberHierarchyId);
        bool OrderTransfer(int eventParticipationId, int newParentMemberHierarchyId);

        int? GetGroupIdById(int eventId);

        Models.Prize GetEarnedPrizeByEventParticipationId(int eventParticipationId);        

        bool IssueMovieTicket(int eventParticipationId, out string message);
        Models.Prize GetNewMovieTicket(int eventParticipationId);

        string GetCommentsById(int eventId);

        int? GetEventParticipationId(int eventId);

        Models.Links GetLinksInfoByEventParticipationId(int eventParticipationId);
        bool UpdateLinks(string adminUser, Models.Links links, out string message);

        List<Models.MemberPassword> GetMemberPasswordsByEmail(string email);
        void UpdateUserPassword(int userId, string newPassword);

        bool Save();
    }
}
