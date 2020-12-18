using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class CatalogData
    {
        public int Id { get; set; }
        public int CatalogGroupId { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Culture { get; set; }
        public bool? IsPriced { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserFirstName { get; set; }
        public string CreateUserLastName { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public string UpdateUserFirstName { get; set; }
        public string UpdateUserLastName { get; set; }
    }
}
