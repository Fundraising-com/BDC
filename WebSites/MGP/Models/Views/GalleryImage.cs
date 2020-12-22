using System;
using GA.BDC.Web.MGP.Properties;

namespace GA.BDC.Web.MGP.Models.Views
{
    public class GalleryImage
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string DirectoryName { get; set; }
        public string FileName { get; set; }            
        public int PartnerId { get; set; }
        public string CultureCode { get; set; }
        public string FullFilePath
        {
            get  
            {
                return string.Format("{0}/{1}/{2}",
                                     Settings.Default.PersonalizationImageGalleryDirectory,
                                     DirectoryName,
                                     FileName);
            }            
        }  
    }
}