using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Search
{

    [Serializable]
    public class DocumentSearchItem
    {

        #region Properties

        public int QSPDocumentId { get; set; }
        public string Name { get; set; }
        public Enum.DocumentTypeEnum Type { get; set; }
        public string TypeName { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedById { get; set; }
        public string ApprovedByFirstName { get; set; }
        public string ApprovedByLastName { get; set; }
        public string ExemptionNumber { get; set; }
        public DateTime? ExemptionStartDate { get; set; }
        public DateTime? ExemptionEndDate { get; set; }

        public int QSPAccountId { get; set; }
        public int? EDSAccountId { get; set; }
        public string AccountName { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }

        #endregion

        #region Display Properties

        public bool IsReceived
        {
            get
            {
                if (ReceivedDate.HasValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string DisplayReceivedDate
        {
            get
            {
                if (ReceivedDate.HasValue)
                {
                    return string.Format("{0} {1}", ReceivedDate.Value.ToShortDateString(), ReceivedDate.Value.ToShortTimeString());
                }
                else
                {
                    return String.Empty;
                }
            }
        }
        public string DisplayApprovedByName
        {
            get
            {
                if (string.IsNullOrEmpty(ApprovedByFirstName) && string.IsNullOrEmpty(ApprovedByLastName))
                {
                    return String.Empty;
                }
                else
                {
                    return string.Format("{0} {1}", ApprovedByLastName, ApprovedByFirstName);
                }
            }
        }
        public string DisplayApprovedDate
        {
            get
            {
                if (ApprovedDate.HasValue)
                {
                    return string.Format("{0} {1}", ApprovedDate.Value.ToShortDateString(), ApprovedDate.Value.ToShortTimeString());
                }
                else
                {
                    return String.Empty;
                }
            }
        }
        public string DisplayExemptionStartDate
        {
            get
            {
                if (ExemptionStartDate.HasValue)
                {
                    return ExemptionStartDate.Value.ToShortDateString();
                }
                else
                {
                    return String.Empty;
                }
            }
        }
        public string DisplayExemptionEndDate
        {
            get
            {
                if (ExemptionEndDate.HasValue)
                {
                    return ExemptionEndDate.Value.ToShortDateString();
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        #endregion

    }
}
