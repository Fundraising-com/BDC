using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

using QSP.Business.Fulfillment;

namespace QSP.Business.Fulfillment.Domain
{

    /// <summary>
    /// Account object wrapper
    /// </summary>
    [DataContract]
    public class AccountData
    {

        #region Private fields

        Account account;
        string billingPhoneNumber;
        string shippingPhoneNumber;

        int organizationTypeId = 0;
        int organizationLevelId = 0;

        #endregion

        #region Public properties

        /// <summary>
        /// Account object
        /// </summary>
        [DataMember]
        public Account Account
        {
            get { return account; }
            set { account = value; }
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

        /// <summary>
        /// OrganizationTypeId object
        /// </summary>
        [DataMember]
        public int OrganizationTypeId
        {
            get { return organizationTypeId; }
            set { organizationTypeId = value; }
        }

        /// <summary>
        /// OrganizationLevelId object
        /// </summary>
        [DataMember]
        public int OrganizationLevelId
        {
            get { return organizationLevelId; }
            set { organizationLevelId = value; }
        }

        #endregion

    }
}
