using System;
using GA.BDC.Web.MGP.Properties;
namespace GA.BDC.Web.MGP.Models.Views
{
    public class PersonalizationImage
    {
        public int ImageId { get; set; }
        public int PersonalizationId { get; set; }
        public bool IsCoverAlbum { get; set; }
        public string ImageURL { get; set; }
        public string ImageText { get; set; }
        public bool IsGalleryImage
        {
            get
            {
                return ImageURL.StartsWith(Settings.Default.PersonalizationImageDirectory, StringComparison.InvariantCultureIgnoreCase);
            }
        }
    }
}