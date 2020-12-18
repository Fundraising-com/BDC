using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSPForm.Common.Entity
{
    public class WeekOfItem
    {
        public int Year { get; set; }
        public int WeekNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description
        {
            get
            {
                string result = String.Format("Week of: {0}", this.StartDate.ToString("MMMM dd, yyyy"));
                return result;
            }
        }
    }
}
