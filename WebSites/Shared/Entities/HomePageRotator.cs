using System;
using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
    public class HomePageRotator
    {
       
        public int Id { get; set; }
        public string Url { get; set; }
        public string CategoryUrl { get; set; }
        public string Image { get; set; }
        public string AlternativeText { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public bool IsProduct { get; set; }
        public int SortOrder { get; set; }
        public int PartnerId { get; set; }
    }
}

