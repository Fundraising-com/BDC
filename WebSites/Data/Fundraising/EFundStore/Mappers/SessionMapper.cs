using System.Collections.Generic;
using GA.BDC.Data.Fundraising.EFundStore.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.EFundStore.Mappers
{
    public static class SessionMapper
    {
        public static Session Hydrate(session session, IEnumerable<session_item> sessionItems)
        {
            var result = new Session
            {
                AnonymousId = session.anonymous_id,
                Id = session.id,
                Created = session.created_on                
            };
            foreach (var sessionItem in sessionItems)
            {
                result.Properties.Add(sessionItem.name, sessionItem.value);
            }
            return result;
        }

        public static session Dehydrate(Session session)
        {
            var result = new session
            {
                id = session.Id,
                anonymous_id = session.AnonymousId,
                created_on = session.Created
            };
            return result;
        }

        public static session_item DehydrateItem(KeyValuePair<string, string> property)
        {
            var result = new session_item
            {
                name = property.Key,
                value = property.Value
            };
            return result;
        }
    }
}
