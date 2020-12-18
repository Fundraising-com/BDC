using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{

    [Serializable]
    public class AccountSearchParameters : SearchParameters
    {
        public AccountSearchFieldEnum SearchField { get; set; }

        public int? ProgramTypeId { get; set; }
        public int? StatusCategoryId { get; set; }
        public string SubdivisionCode { get; set; }
        public string FSMId { get; set; }
        public string FSMName { get; set; }
    }

}
