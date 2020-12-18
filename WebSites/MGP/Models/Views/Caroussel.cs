using GA.BDC.Web.MGP.Helpers.Extensions;
namespace GA.BDC.Web.MGP.Models.Views
{
    public class Caroussel
    {
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public string ImageUrl { get; set; }
        public string AnchorUrl { get; set; }
        public string AlternativeText { get; set; }
        public bool IsCoverAlbum { get; set; }
        public bool RedirectToStore { get; set; }
    }
}