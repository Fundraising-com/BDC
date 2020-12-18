using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

using QSP.Business.Fulfillment;

namespace QSP.Business.Fulfillment.Domain
{

    /// <summary>
    /// Campaign object wrapper
    /// </summary>
    [DataContract]
    public class CampaignData
    {

        #region Private fields

        Campaign campaign;
        string billingPhoneNumber;
        string shippingPhoneNumber;

        #endregion

        #region Public properties

        /// <summary>
        /// Campaign object
        /// </summary>
        [DataMember]
        public Campaign Campaign
        {
            get { return campaign; }
            set { campaign = value; }
        }

        /// <summary>
        /// BillingPhoneNumber object
        /// </summary>
        [DataMember]
        public string BillingPhoneNumber
        {
            get { return billingPhoneNumber; }
            set { billingPhoneNumber = value; }
        }

        /// <summary>
        /// ShippingPhoneNumber object
        /// </summary>
        [DataMember]
        public string ShippingPhoneNumber
        {
            get { return shippingPhoneNumber; }
            set { shippingPhoneNumber = value; }
        }

        #endregion

    }
}
