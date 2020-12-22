using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{

    [Serializable]
    public class DocumentSearchParameters : SearchParameters
    {
        public DocumentSearchFieldEnum SearchField { get; set; }

        public int? DocumentTypeId { get; set; }
        public int? DocumentStatusId { get; set; }
    }
}
