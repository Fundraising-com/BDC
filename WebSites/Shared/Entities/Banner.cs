using System;
using System.Collections.Generic;

namespace GA.BDC.Shared.Entities
{
    public class Banner
    {
        public Banner()
        {
            ViewPorts = new List<ViewPort>();
        }
        public int Id { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string AlternativeText { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public string BootStrapClasses { get; set; }
        public int SortOrder { get; set; }
        public IList<ViewPort> ViewPorts { get; set; }
    }
}
