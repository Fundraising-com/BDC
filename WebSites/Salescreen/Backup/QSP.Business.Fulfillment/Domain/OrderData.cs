using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

using QSP.Business.Fulfillment;

namespace QSP.Business.Fulfillment.Domain
{

    /// <summary>
    /// Order object wrapper
    /// </summary>
    [DataContract]
    public class OrderData
    {

        #region Private fields

        Order order;
        List<OrderDetailData> orderDetailDataList;
        ShipmentGroup shipmentGroup;
        PostalAddress postalAddressBilling;
        PostalAddress postalAddressShipping;
        PhoneNumber phoneNumber;
        Invoice invoice;
        PaymentInvoice paymentInvoice;
        Payment payment;
        CreditCard creditCard;
        CreditCardAuthorization creditCardAuthorization;
        BankCheck bankCheck;

        #endregion

        #region Public properties

        /// <summary>
        /// Order object
        /// </summary>
        [DataMember]
        public Order Order
        {
            get { return order; }
            set { order = value; }
        }

        /// <summary>
        /// Order detail data list
        /// </summary>
        [DataMember]
        public List<OrderDetailData> OrderDetailDataList
        {
            get { return orderDetailDataList; }
            set { orderDetailDataList = value; }
        }

        /// <summary>
        /// Shipment group object
        /// </summary>
        [DataMember]
        public ShipmentGroup ShipmentGroup
        {
            get { return shipmentGroup; }
            set { shipmentGroup = value; }
        }

        /// <summary>
        /// Postal address billing object
        /// </summary>
        [DataMember]
        public PostalAddress PostalAddressBilling
        {
            get { return postalAddressBilling; }
            set { postalAddressBilling = value; }
        }

        /// <summary>
        /// Postal address shipping object
        /// </summary>
        [DataMember]
        public PostalAddress PostalAddressShipping
        {
            get { return postalAddressShipping; }
            set { postalAddressShipping = value; }
        }

        /// <summary>
        /// Phone number object
        /// </summary>
        [DataMember]
        public PhoneNumber PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        /// <summary>
        /// Invoice object
        /// </summary>
        [DataMember]
        public Invoice Invoice
        {
            get { return invoice; }
            set { invoice = value; }
        }

        /// <summary>
        /// Payment object
        /// </summary>
        [DataMember]
        public Payment Payment
        {
            get { return payment; }
            set { payment = value; }
        }

        /// <summary>
        /// CreditCard object
        /// </summary>
        [DataMember]
        public CreditCard CreditCard
        {
            get { return creditCard; }
            set { creditCard = value; }
        }

        /// <summary>
        /// Payment object
        /// </summary>
        [DataMember]
        public BankCheck BankCheck
        {
            get { return bankCheck; }
            set { bankCheck = value; }
        }
        #endregion

    }
}
