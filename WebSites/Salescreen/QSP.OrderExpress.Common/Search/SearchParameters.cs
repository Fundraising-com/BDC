using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QSP.OrderExpress.Common.Enum;

namespace QSP.OrderExpress.Common.Search
{

    [Serializable]
    public abstract class SearchParameters
    {
        public int LoggedUserId { get; set; }
        public UserTypeEnum LoggedUserType { get; set; }


        public bool IsPagingEnabled { get; set; }
        public int ItemsPerPage { get; set; }
        public int RequestedPage { get; set; }

        public string SortField { get; set; }
        public string SearchValue { get; set; }

        public SearchFSMHierarchyOptionEnum SearchFSMOption { get; set; }
    }
}
