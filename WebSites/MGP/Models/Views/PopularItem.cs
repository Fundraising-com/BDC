using System;
using GA.BDC.Web.MGP.Helpers;
using GA.BDC.Web.MGP.Properties;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class PopularItem
    {
        public int PartnerId { get; set; }
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public string CultureCode { get; set; }
        public int? EntityId { get; set; }
        public int? StorefrontCategoryId { get; set; }
        public int? TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public string ImagePath
        {
            get
            {
                return string.Format("{0}/{1}",
                                     Settings.Default.PopularItemsImageDirectory,
                                     ImageFileName);
            }
        }
        //public string Url
        //{
        //    get
        //    {
        //        if (EntityId.HasValue && StorefrontCategoryId.HasValue)
        //            return string.Format("Group/{0}?eventId={1}&participantId={2}&storefrontcategoryId={3}&entityId={4}",
        //                                 Constants.STORE_REDIRECT_PAGE,
        //                                 EventId,
        //                                 ParticipantId,
        //                                 StorefrontCategoryId,
        //                                 EntityId);
        //        else if (StorefrontCategoryId.HasValue)
        //            return string.Format("Group/{0}?eventId={1}&participantId={2}&storefrontcategoryId={3}",
        //                                 Constants.STORE_REDIRECT_PAGE,
        //                                 EventId,
        //                                 ParticipantId,
        //                                 StorefrontCategoryId);
        //        else
        //            return string.Format("Group/{0}?eventId={1}&participantId={2}",
        //                                 Constants.STORE_REDIRECT_PAGE,
        //                                 EventId,
        //                                 ParticipantId);
        //    }
        //}
    }
}