using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    public class AuditInformationData
    {
        public int EntityId { get; set; }
        public Enum.EntityTypeEnum EntityType { get; set; }

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
