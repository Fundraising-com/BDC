using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QSP.OrderExpress.Common.Enum;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class BusinessExceptionData
    {
        public int Id { get; set; }
        public EntityTypeEnum EntityType { get; set; }
        public int EntityId { get; set; }
        public int ExceptionId { get; set; }
        public ExceptionTypeEnum ExceptionType { get; set; }
        public string Message { get; set; }
        public decimal? FeesAmount { get; set; }
        public bool? IsApproved { get; set; }
        public int? ApprovedById { get; set; }
        public string ApprovedByFirstName { get; set; }
        public string ApprovedByLastName { get; set; }
        public DateTime CreateDate { get; set; }

        public string ApprovedForDisplay
        {
            get
            {
                string result = "";

                if (this.IsApproved.HasValue)
                {
                    if (this.IsApproved.Value)
                    {
                        result = "Yes";
                    }
                }

                return result;
            }
        }
        public string ApprovedByForDisplay
        {
            get
            {
                return string.Format("{0}, {1}", this.ApprovedByLastName, this.ApprovedByFirstName);
            }
        }
    }
}
