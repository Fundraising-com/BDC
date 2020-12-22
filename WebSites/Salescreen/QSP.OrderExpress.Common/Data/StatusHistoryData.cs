using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{

    [Serializable]
    public class StatusHistoryData
    {

        #region Properties

        public int QSPId { get; set; }
        public int? StatusId { get; set; }
        public int? StatusCategoryId { get; set; }
        public string StatusColorCode { get; set; }
        public string StatusShortDescription { get; set; }
        public string Reason { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }

        #endregion

        #region Display Properties

        public string DisplayColorCode
        {
            get
            {
                if (string.IsNullOrEmpty(StatusColorCode))
                {
                    return "White";
                }
                else
                {
                    return StatusColorCode;
                }
            }
        }
        public string DisplayCreatorName
        {
            get
            {
                if (string.IsNullOrEmpty(CreatorFirstName) && string.IsNullOrEmpty(CreatorLastName))
                {
                    return "System";
                }
                else
                {
                    return string.Format("{0} {1}", CreatorLastName, CreatorFirstName);
                }
            }
        }
        public string DisplayCreateDate
        {
            get
            {
                return string.Format("{0} {1}", CreateDate.ToShortDateString(), CreateDate.ToShortTimeString());
            }
        }

        #endregion

    }
}
