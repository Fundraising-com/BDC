using System;
using System.Collections.Generic;
using System.Text;
using BusinessCommunication = QSPForm.Business.Communication;
using BusinessObject = QSP.Business.Fulfillment;
using QSPForm.Common;
using QSPForm.Common.DataDef;

namespace QSPForm.Business.Validation
{
    /// <summary>
    /// Handles the validation logic regarding orders
    /// </summary>
    public class OrderValidation
    {
        #region Methods

        /// <summary>
        /// Validates the order information
        /// </summary>
        /// <param name="order">The order object to validate</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        /// </param>
        public static void ValidateOrder(BusinessObject.Domain.OrderData orderData, BusinessCommunication.Notifications notifications)
        {
            ValidateOrderHeader(orderData.Order, notifications);
            ValidateOrderDetails(orderData.OrderDetailDataList, notifications);
            ValidateShipmentGroup(orderData.ShipmentGroup, notifications);
            ValidatePostalAddress(orderData.PostalAddressBilling, notifications);
            ValidatePostalAddress(orderData.PostalAddressShipping, notifications);
            ValidatePhoneNumber(orderData.PhoneNumber, notifications);
            ValidateInvoice(orderData.Invoice, notifications);
            ValidatePayment(orderData.Payment, notifications);
            ValidateCreditCard(orderData.CreditCard, notifications);
            ValidateBankCheck(orderData.BankCheck, notifications);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="notifications"></param>
        private static void ValidateOrderHeader(BusinessObject.Order order, BusinessCommunication.Notifications notifications)
        {
            if (order.FormId == null)
            {
                notifications.Add(new BusinessCommunication.Notification("Order's form ID missing", null, BusinessCommunication.NotificationType.Error));
            }

            if (order.CampaignId <= 0)
            {
                notifications.Add(new BusinessCommunication.Notification("Order's campaign ID is incorrect", null, BusinessCommunication.NotificationType.Error));
            }
        }
        
        /// <summary>
        /// Validates the order detail information
        /// </summary>
        /// <param name="orderDetails">A list of order detail objects to validate</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        /// </param>
        private static void ValidateOrderDetails(List<BusinessObject.Domain.OrderDetailData> orderDetailDataList, BusinessCommunication.Notifications notifications)
        {
            List<int> sgList = new List<int>();
            foreach (BusinessObject.Domain.OrderDetailData orderDetailData in orderDetailDataList)
            {
                if (!sgList.Contains(orderDetailData.OrderDetail.ShipmentGroupId))
                {
                    sgList.Add(orderDetailData.OrderDetail.ShipmentGroupId);
                }
            }

            if (sgList.Count != 1)
            {
                notifications.Add(new BusinessCommunication.Notification("Error: Multiple shipping addresses have been used", null, BusinessCommunication.NotificationType.Error));
            }

            foreach (BusinessObject.Domain.OrderDetailData orderDetailData in orderDetailDataList)
            {
                ValidateOrderDetail(orderDetailData, notifications);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetailData"></param>
        /// <param name="notifications"></param>
        private static void ValidateOrderDetail(BusinessObject.Domain.OrderDetailData orderDetailData, BusinessCommunication.Notifications notifications)
        {
            if (orderDetailData.OrderDetail.Quantity <= 0)
            {
                notifications.Add(new BusinessCommunication.Notification("Product " + orderDetailData.OrderDetail.CatalogItemDetailId + " must be equal or higher than 0", null, BusinessCommunication.NotificationType.Error));
            }

            if (orderDetailData.OrderDetail.Quantity > 0 && orderDetailData.OrderDetail.AdjustmentQuantity >= orderDetailData.OrderDetail.Quantity)
            {
                notifications.Add(new BusinessCommunication.Notification("Product " + orderDetailData.OrderDetail.CatalogItemDetailId + " must have a quantity higher or equal to adjustement", null, BusinessCommunication.NotificationType.Error));
            }

            ValidateOrderDetailTaxes(orderDetailData.OrderDetailTaxList, notifications);
        }

        /// <summary>
        /// Validates the order detail tax information
        /// </summary>
        /// <param name="orderDetailTaxes">A list of order detail tax objects to validate</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        /// </param>
        private static void ValidateOrderDetailTaxes(List<BusinessObject.OrderDetailTax> orderDetailTaxes, BusinessCommunication.Notifications notifications)
        {

        }

        /// <summary>
        /// Validates the shipment group information
        /// </summary>
        /// <param name="shipmentGroup">The shipment group object to validate</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsValid property determines if the operation was a success or not.
        /// </param>
        private static void ValidateShipmentGroup(BusinessObject.ShipmentGroup shipmentGroup, BusinessCommunication.Notifications notifications)
        {

            if (shipmentGroup.RequestedDeliveryDate == null || shipmentGroup.RequestedDeliveryDate <= DateTime.Today)
            {
                notifications.Add(new BusinessCommunication.Notification("Requested delivery date must be later than today", null, BusinessCommunication.NotificationType.Error));
            }

            if (shipmentGroup.DeliveryMethodId == null)
            {
                notifications.Add(new BusinessCommunication.Notification("You need to specify a delivery method", null, BusinessCommunication.NotificationType.Error));
            }
            else
            {
                if (shipmentGroup.DeliveryMethodId == DeliveryMethod.PICK_UP_AT_WAREHOUSE)
                {
                    if (shipmentGroup.DeliveryWarehouseId == null || shipmentGroup.DeliveryWarehouseId <= 0)
                    {
                        notifications.Add(new BusinessCommunication.Notification("You need to specify the warehouse ID in order to select a pick up at warehouse", null, BusinessCommunication.NotificationType.Error));
                    }
                }
            }
        }

        /// <summary>
        /// Validates the postal address information
        /// </summary>
        /// <param name="postalAddress">The postal address object to validate</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsValid property determines if the operation was a success or not.
        /// </param>
        private static void ValidatePostalAddress(BusinessObject.PostalAddress postalAddress, BusinessCommunication.Notifications notifications)
        {

            if (postalAddress.Address1 == null)
            {
                notifications.Add(new BusinessCommunication.Notification("Address: Street 1 should not be empty", null, BusinessCommunication.NotificationType.Error));
            }
            else
            {
                if (postalAddress.Address1.Trim().Length == 0)
                {
                    notifications.Add(new BusinessCommunication.Notification("Address: Street 1 should not be empty", null, BusinessCommunication.NotificationType.Error));
                }
            }

            if (postalAddress.County == null)
            {
                notifications.Add(new BusinessCommunication.Notification("Address: County should not be empty", null, BusinessCommunication.NotificationType.Error));
            }
            else
            {
                if (postalAddress.County.Trim().Length == 0)
                {
                    notifications.Add(new BusinessCommunication.Notification("Address: County should not be empty", null, BusinessCommunication.NotificationType.Error));
                }
            }

            if (postalAddress.Zip == null)
            {
                notifications.Add(new BusinessCommunication.Notification("Address: Zip should not be empty", null, BusinessCommunication.NotificationType.Error));
            }
            else
            {
                if (postalAddress.Zip.Trim().Length == 0)
                {
                    notifications.Add(new BusinessCommunication.Notification("Address: Zip should not be empty", null, BusinessCommunication.NotificationType.Error));
                }
            }

            if (postalAddress.SubdivisionCode == null)
            {
                notifications.Add(new BusinessCommunication.Notification("Address: State should not be empty", null, BusinessCommunication.NotificationType.Error));
            }
            else
            {
                if (postalAddress.SubdivisionCode.Trim().Length == 0)
                {
                    notifications.Add(new BusinessCommunication.Notification("Address: State should not be empty", null, BusinessCommunication.NotificationType.Error));
                }
            }

            if (postalAddress.Name == null)
            {
                notifications.Add(new BusinessCommunication.Notification("Address: Name should not be empty", null, BusinessCommunication.NotificationType.Error));
            }
            else
            {
                if (postalAddress.Name.Trim().Length == 0)
                {
                    notifications.Add(new BusinessCommunication.Notification("Address: Name should not be empty", null, BusinessCommunication.NotificationType.Error));
                }
            }
        }

        /// <summary>
        /// Validates the phone number information
        /// </summary>
        /// <param name="phoneNumber">The phone number object to validate</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsValid property determines if the operation was a success or not.
        /// </param>
        private static void ValidatePhoneNumber(BusinessObject.PhoneNumber phoneNumber, BusinessCommunication.Notifications notifications)
        {
            if (phoneNumber.Phone_Number == null)
            {
                notifications.Add(new BusinessCommunication.Notification("Shipment Phone Number: A phone number must be provided", null, BusinessCommunication.NotificationType.Error));
            }
            else
            {
                if (phoneNumber.Phone_Number.Trim().Length == 0)
                {
                    notifications.Add(new BusinessCommunication.Notification("Shipment Phone Number: A phone number must be provided", null, BusinessCommunication.NotificationType.Error));
                }

                if (phoneNumber.Phone_Number.Trim().Length > 10)
                {
                    notifications.Add(new BusinessCommunication.Notification("Shipment Phone Number: A phone number must be less than or equal to 10 digits", null, BusinessCommunication.NotificationType.Error));
                }
            }
        }

        /// <summary>
        /// Validates the invoice information
        /// </summary>
        /// <param name="invoice">The invoice object to validate</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        /// </param>
        private static void ValidateInvoice(BusinessObject.Invoice invoice, BusinessCommunication.Notifications notifications)
        {
        }

        /// <summary>
        /// Validates the payment information
        /// </summary>
        /// <param name="payment">The payment object to validate</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        /// </param>
        private static void ValidatePayment(BusinessObject.Payment payment, BusinessCommunication.Notifications notifications)
        {
        }

        /// <summary>
        /// Validates the CreditCard information
        /// </summary>
        /// <param name="creditCard">The CreditCard object to validate</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        /// </param>
        private static void ValidateCreditCard(BusinessObject.CreditCard creditCard, BusinessCommunication.Notifications notifications)
        {
        }

        /// <summary>
        /// Validates the BankCheck information
        /// </summary>
        /// <param name="bankCheck">The BankCheck object to validate</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        /// </param>
        private static void ValidateBankCheck(BusinessObject.BankCheck bankCheck, BusinessCommunication.Notifications notifications)
        {
        }

        #endregion
    }

}
