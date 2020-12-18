using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

using QSP.Business.Fulfillment;

namespace QSP.Business.Fulfillment.Domain
{

    /// <summary>
    /// Order detail object wrapper
    /// </summary>
    [DataContract]
    public class OrderDetailData
    {

        #region Private fields

        OrderDetail orderDetail;
        List<Commission> commissionList;
        List<OrderDetailTax> orderDetailTaxList;

        #endregion

        #region Public properties

        /// <summary>
        /// Order detail object
        /// </summary>
        [DataMember]
        public OrderDetail OrderDetail
        {
            get { return orderDetail; }
            set { orderDetail = value; }
        }

        /// <summary>
        /// Commission values for the order detail
        /// </summary>
        [DataMember]
        public List<Commission> CommissionList
        {
            get { return commissionList; }
            set { commissionList = value; }
        }

        /// <summary>
        /// Order detail tax list
        /// </summary>
        [DataMember]
        public List<OrderDetailTax> OrderDetailTaxList
        {
            get { return orderDetailTaxList; }
            set { orderDetailTaxList = value; }
        }

        #endregion

    }
}
