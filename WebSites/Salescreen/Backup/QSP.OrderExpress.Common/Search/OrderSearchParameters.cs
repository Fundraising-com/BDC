using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{

    [Serializable]
    public class OrderSearchParameters : SearchParameters
    {
        public OrderSearchFieldEnum SearchField { get; set; }

        public int? SourceId { get; set; }
        public int? FormId { get; set; }
        public int? ProgramTypeId { get; set; }
        public int? OrderTypeId { get; set; }
        public int? StatusCategoryId { get; set; }
        public string SubdivisionCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string FSMId { get; set; }
        public string FSMName { get; set; }
    }

}
