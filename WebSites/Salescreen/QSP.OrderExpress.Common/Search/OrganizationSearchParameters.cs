using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{

    [Serializable]
    public class OrganizationSearchParameters : SearchParameters
    {
        public OrganizationSearchFieldEnum SearchField { get; set; }

        public int? OrganizationTypeId { get; set; }
        public string SubdivisionCode { get; set; }
        public string FSMId { get; set; }
        public string FSMName { get; set; }
    }

}
