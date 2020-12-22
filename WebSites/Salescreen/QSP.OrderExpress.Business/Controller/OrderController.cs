using System;
using System.Collections.Generic;
using System.Text;

using NHibernate;
using NHibernate.Criterion;

using BusinessCommunication = QSPForm.Business.Communication;
using BusinessObject = QSP.Business.Fulfillment;
using BusinessValidation = QSPForm.Business.Validation;

using QSP.OrderExpress.Common.Enum;
using System.Configuration;

namespace QSPForm.Business.Controller
{

    /// <summary>
    /// Handles the business logic regarding orders
    /// </summary>
    public class OrderController
    {

        #region EFR related methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="catalogItemID"></param>
        /// <param name="catalogItemCategoryID"></param>
        /// <param name="profitRate"></param>
        /// <param name="country_code"></param>
        /// <param name="paidUpFront"></param>
        /// <returns></returns>
        public static decimal GetCommissionRate(int catalogItemID, int catalogItemCategoryID, decimal profitRate, string country_code, bool paidUpFront)
        {
            int scratchcardID = 0;
            int wfc = 0;
            int mm = 0;
            int hershey = 0;
            int hebert = 0;
            decimal commissionRate = 0;

            //check sc id //check if we have a sc  //set upfront false id not sc
            ICriteria criteria = BusinessObject.CatalogItemCategory.CreateCriteria();
            criteria.Add(Expression.Eq(BusinessObject.CatalogItemCategory.CatalogItemCategoryIdProperty, catalogItemCategoryID));
            List<BusinessObject.CatalogItemCategory> catalogItemCategories = BusinessObject.CatalogItemCategory.GetCatalogItemCategoryList(criteria);
            foreach (BusinessObject.CatalogItemCategory category in catalogItemCategories)
            {
                switch (category.CatalogItemCategoryName)
                {
                    case "Scratchcard":
                        scratchcardID = category.CatalogItemCategoryId;
                        break;
                    case "Stock WFC":
                        wfc = category.CatalogItemCategoryId;
                        break;
                    case "M&M/Mars":
                        mm = category.CatalogItemCategoryId;
                        break;
                    case "Hershey":
                        hershey = category.CatalogItemCategoryId;
                        break;
                    case "Hebert/Lamontagne":
                        hebert = category.CatalogItemCategoryId;
                        break;
                }

            }


            if (catalogItemCategoryID != scratchcardID)
            {
                //if not sc, no upfront applies
                paidUpFront = false;
            }



            //try a request with item id only
            criteria = BusinessObject.CommissionRate.CreateCriteria();

            //check if product is WFC straight/variety, hebert, hershey, m&m
            //profit rate only applies to them
            if (catalogItemCategoryID == wfc || catalogItemCategoryID == mm || catalogItemCategoryID == hershey || catalogItemCategoryID == hebert)
            {
                criteria.Add(Expression.Eq(BusinessObject.CommissionRate.ProfitRateValueProperty, profitRate));
            }
            criteria.Add(Expression.Eq(BusinessObject.CommissionRate.IsPaidUpfrontProperty, paidUpFront));
            criteria.Add(Expression.Eq(BusinessObject.CommissionRate.CountryCodeProperty, country_code));
            criteria.Add(Expression.Eq(BusinessObject.CommissionRate.CatalogItemIdProperty, catalogItemID));

            List<BusinessObject.CommissionRate> commissionRates = BusinessObject.CommissionRate.GetCommissionRateList(criteria);
            if (commissionRates != null && commissionRates.Count > 0)
            {
                commissionRate = commissionRates[0].CommissionRateValue;
            }
            else //try with item category only
            {

                //try a request with item id only
                criteria = BusinessObject.CommissionRate.CreateCriteria();

                //check if product is WFC straight/variety, hebert, hershey, m&m
                //profit rate only applies to them
                if (catalogItemCategoryID == wfc || catalogItemCategoryID == mm || catalogItemCategoryID == hershey || catalogItemCategoryID == hebert)
                {
                    criteria.Add(Expression.Eq(BusinessObject.CommissionRate.ProfitRateValueProperty, profitRate));
                }
                criteria.Add(Expression.Eq(BusinessObject.CommissionRate.IsPaidUpfrontProperty, paidUpFront));
                criteria.Add(Expression.Eq(BusinessObject.CommissionRate.CountryCodeProperty, country_code));
                int? a = new int?();
                a = 0;
                criteria.Add(Expression.Eq(BusinessObject.CommissionRate.CatalogItemIdProperty, a));
                criteria.Add(Expression.Eq(BusinessObject.CommissionRate.CatalogItemCategoryIdProperty, catalogItemCategoryID));

                commissionRates = BusinessObject.CommissionRate.GetCommissionRateList(criteria);
                if (commissionRates != null && commissionRates.Count > 0)
                {
                    commissionRate = commissionRates[0].CommissionRateValue;
                }
                else
                {
                    commissionRate = -1;
                }
            }

            return commissionRate;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId">The Order Express Order Id of the order to get</param>
        /// <returns></returns>
        public static BusinessObject.Domain.OrderData GetOrder(int orderId, BusinessCommunication.Notifications notifications)
        {
            BusinessObject.Domain.OrderData result = null;

            //List<object> dynamicValues = new List<object>();
            //notifications.Add(new BusinessCommunication.Notification("Method not implemented yet", dynamicValues, BusinessCommunication.NotificationType.Error));

            #region Get order header

            result = new QSP.Business.Fulfillment.Domain.OrderData();
            result.Order = QSP.Business.Fulfillment.Order.GetOrder(orderId);

            #endregion

            #region Get order details

            result.OrderDetailDataList = new List<QSP.Business.Fulfillment.Domain.OrderDetailData>();

            List<QSP.Business.Fulfillment.OrderDetail> odl = QSP.Business.Fulfillment.OrderDetail.GetOrderDetailListFromOrder(orderId);
            foreach (QSP.Business.Fulfillment.OrderDetail orderDetailItem in odl)
            {
                QSP.Business.Fulfillment.Domain.OrderDetailData odd = new QSP.Business.Fulfillment.Domain.OrderDetailData();

                odd.OrderDetail = orderDetailItem;
                odd.CommissionList = QSP.Business.Fulfillment.Commission.GetCommissionListFromOrderDetail(odd.OrderDetail.OrderDetailId);
                odd.OrderDetailTaxList = QSP.Business.Fulfillment.OrderDetailTax.GetOrderDetailTaxListFromOrderDetail(odd.OrderDetail.OrderDetailId);

                result.OrderDetailDataList.Add(odd);
            }

            #endregion

            #region Get shipment group

            if (result.OrderDetailDataList.Count > 0)
            {
                int shipmentGroupId = result.OrderDetailDataList[0].OrderDetail.ShipmentGroupId;

                result.ShipmentGroup = QSP.Business.Fulfillment.ShipmentGroup.GetShipmentGroup(shipmentGroupId);
            }

            #endregion

            #region Get phone number

            //if (result.ShipmentGroup != null)
            //{
            //    if (result.ShipmentGroup.ShippingPhoneNumberId != null)
            //    {
            //        result.PhoneNumber = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumber((int)result.ShipmentGroup.ShippingPhoneNumberId);
            //    }
            //}

            #endregion

            #region Get shipping address

            if (result.ShipmentGroup != null)
            {
                if (result.ShipmentGroup.ShippingPostalAddressId != null)
                {
                    result.PostalAddressShipping = QSP.Business.Fulfillment.PostalAddress.GetPostalAddress((int)result.ShipmentGroup.ShippingPostalAddressId);
                }
            }

            #endregion

            #region Get billing address

            result.PostalAddressBilling = QSP.Business.Fulfillment.PostalAddress.GetPostalAddress(result.Order.BillingPostalAddressId);

            #endregion

            //result.Invoice;
            //result.Payment;
            //result.BankCheck;
            //result.CreditCard;

            return result;
        }

        /// <summary>
        /// Validates and creates the order in the Order Express system
        /// </summary>
        /// <param name="orderData">The complete order data object.</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        ///     If the order is created successfully, an "Information" notification object will be returned. The dynamic data value will contain the order id.
        ///     If the order creation fails, an "Error" notification object will be returned. The dynamic data will contain the exception message and the inner exception object if possible.
        /// </param>
        public static void SaveOrder(BusinessObject.Domain.OrderData orderData, BusinessCommunication.Notifications notifications)
        {
            try
            {


                if (orderData.Order.OrderId == 0)
                {
                    OrderController.InsertOrder(orderData, notifications);
                }

                else
                {
                    BusinessObject.Domain.OrderData currentOrderData = OrderController.GetOrder(orderData.Order.OrderId, notifications);

                    // Get ids for update
                    orderData = OrderController.FillIds(orderData, currentOrderData, notifications);

                    // Update the order
                    OrderController.UpdateOrder(orderData, currentOrderData, notifications);

                }
            }
            catch (Exception ex)
            {
                // Create failure response
                List<object> dynamicValues = new List<object>();
                dynamicValues.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    dynamicValues.Add(ex.InnerException.ToString());
                }

                notifications.Add(new BusinessCommunication.Notification("System Error : " + ex.Message + "; Stack Trace : " + ex.StackTrace, dynamicValues, BusinessCommunication.NotificationType.Error));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderData"></param>
        /// <param name="notifications"></param>
        private static void InsertOrder(BusinessObject.Domain.OrderData orderData, BusinessCommunication.Notifications notifications)
        {
            try
            {
                #region Do data validation

                QSPForm.Business.Validation.OrderValidation.ValidateOrder(orderData, notifications);

                #endregion

                // If successful, save the order
                if (notifications.IsSuccessful())
                {
                    #region Try to save the data

                    using (ISession session = BusinessObject.SqlSessionManager.OpenSession())
                    {
                        // Start transaction
                        ITransaction transaction = session.BeginTransaction();

                        try
                        {
                            #region Independent objects

                            // Save phone number
                            if (orderData.PhoneNumber.PhoneNumberId == 0)
                            {
                                // Create
                                session.Save(orderData.PhoneNumber);
                            }
                            else
                            {
                                // Update
                                session.Update(orderData.PhoneNumber);
                            }

                            // Save shipping postal address
                            if (orderData.PostalAddressShipping.PostalAddressId == 0)
                            {
                                // Create
                                session.Save(orderData.PostalAddressShipping);
                            }
                            else
                            {
                                // Update
                                session.Update(orderData.PostalAddressShipping);
                            }

                            // Save billing postal address
                            if (orderData.PostalAddressBilling.PostalAddressId == 0)
                            {
                                // Create
                                session.Save(orderData.PostalAddressBilling);
                            }
                            else
                            {
                                // Update
                                session.Update(orderData.PostalAddressBilling);
                            }

                            #endregion

                            #region Set the campaign's address

                            List<BusinessObject.PostalAddressCampaign> PACList1 = BusinessObject.PostalAddressCampaign.GetAddressesByTypeAndCampaign(BusinessObject.PostalAddressType.Billing, orderData.Order.CampaignId);
                            List<BusinessObject.PostalAddressCampaign> PACList2 = BusinessObject.PostalAddressCampaign.GetAddressesByTypeAndCampaign(BusinessObject.PostalAddressType.Shipping, orderData.Order.CampaignId);


                            if (PACList2.Count == 0)
                            {
                                // If we have no address, we add it
                                BusinessObject.PostalAddressCampaign pacShipping = new QSP.Business.Fulfillment.PostalAddressCampaign();
                                //pacShipping.PostalAddressCampaignId;
                                pacShipping.PostalAddressTypeId = BusinessObject.PostalAddressType.Shipping;
                                pacShipping.CampaignId = orderData.Order.CampaignId;
                                pacShipping.PostalAddressId = orderData.PostalAddressShipping.PostalAddressId;
                                pacShipping.Deleted = false;
                                pacShipping.CreateDate = DateTime.Now;
                                pacShipping.CreateUserId = -1;
                                pacShipping.UpdateDate = DateTime.Now;
                                pacShipping.UpdateUserId = -1;
                                session.Save(pacShipping);
                            }


                            //List<BusinessObject.PostalAddressCampaign> PACList1 = BusinessObject.PostalAddressCampaign.GetAddressesByTypeAndCampaign(BusinessObject.PostalAddressType.Billing, orderData.Order.CampaignId);

                            if (PACList1.Count == 0)
                            {
                                // If we have no address, we add it
                                BusinessObject.PostalAddressCampaign pacBilling = new QSP.Business.Fulfillment.PostalAddressCampaign();
                                //pacBilling.PostalAddressCampaignId;
                                pacBilling.PostalAddressTypeId = BusinessObject.PostalAddressType.Billing;
                                pacBilling.CampaignId = orderData.Order.CampaignId;
                                pacBilling.PostalAddressId = orderData.PostalAddressBilling.PostalAddressId;
                                pacBilling.Deleted = false;
                                pacBilling.CreateDate = DateTime.Now;
                                pacBilling.CreateUserId = -1;
                                pacBilling.UpdateDate = DateTime.Now;
                                pacBilling.UpdateUserId = -1;
                                session.Save(pacBilling);
                            }

                            #endregion

                            #region Set the account's address

                            // Get campaign record (to get the account id)
                            QSP.Business.Fulfillment.Campaign campaign = QSP.Business.Fulfillment.Campaign.GetCampaign(orderData.Order.CampaignId);

                            List<BusinessObject.PostalAddressAccount> PAAList1 = BusinessObject.PostalAddressAccount.GetAddressesByTypeAndAccount(BusinessObject.PostalAddressType.Billing, campaign.AccountId);
                            List<BusinessObject.PostalAddressAccount> PAAList2 = BusinessObject.PostalAddressAccount.GetAddressesByTypeAndAccount(BusinessObject.PostalAddressType.Shipping, campaign.AccountId);

                            if (PAAList2.Count == 0)
                            {
                                // If we have no address, we add it
                                BusinessObject.PostalAddressAccount paaShipping = new QSP.Business.Fulfillment.PostalAddressAccount();
                                //paaShipping.PostalAddressCampaignId;
                                paaShipping.PostalAddressTypeId = BusinessObject.PostalAddressType.Shipping;
                                paaShipping.AccountId = campaign.AccountId;
                                paaShipping.PostalAddressId = orderData.PostalAddressShipping.PostalAddressId;
                                paaShipping.Deleted = false;
                                paaShipping.CreateDate = DateTime.Now;
                                paaShipping.CreateUserId = -1;
                                paaShipping.UpdateDate = DateTime.Now;
                                paaShipping.UpdateUserId = -1;
                                session.Save(paaShipping);
                            }

                            //List<BusinessObject.PostalAddressAccount> PAAList1 = BusinessObject.PostalAddressAccount.GetAddressesByTypeAndAccount(BusinessObject.PostalAddressType.Billing, campaign.AccountId);

                            if (PAAList1.Count == 0)
                            {
                                // If we have no address, we add it
                                BusinessObject.PostalAddressAccount paaBilling = new QSP.Business.Fulfillment.PostalAddressAccount();
                                //paaBilling.PostalAddressCampaignId;
                                paaBilling.PostalAddressTypeId = BusinessObject.PostalAddressType.Billing;
                                paaBilling.AccountId = campaign.AccountId;
                                paaBilling.PostalAddressId = orderData.PostalAddressBilling.PostalAddressId;
                                paaBilling.Deleted = false;
                                paaBilling.CreateDate = DateTime.Now;
                                paaBilling.CreateUserId = -1;
                                paaBilling.UpdateDate = DateTime.Now;
                                paaBilling.UpdateUserId = -1;
                                session.Save(paaBilling);
                            }

                            #endregion

                            #region Shipment group

                            // Set foreign ids to shipment group
                            orderData.ShipmentGroup.ShippingPostalAddressId = orderData.PostalAddressShipping.PostalAddressId;
                            orderData.ShipmentGroup.ShippingPhoneNumberId = orderData.PhoneNumber.PhoneNumberId;

                            // Save the shipment group 
                            session.Save(orderData.ShipmentGroup);

                            #endregion

                            #region Order

                            // Set foreign ids to order
                            orderData.Order.BillingPostalAddressId = orderData.PostalAddressBilling.PostalAddressId;

                            // Save the order
                            session.Save(orderData.Order);

                            #endregion

                            #region Order details, commission and taxes

                            // Iterate each order detail data item
                            foreach (BusinessObject.Domain.OrderDetailData orderDetailDataItem in orderData.OrderDetailDataList)
                            {
                                // Set foreign ids to order detail
                                orderDetailDataItem.OrderDetail.OrderId = orderData.Order.OrderId;
                                orderDetailDataItem.OrderDetail.ShipmentGroupId = orderData.ShipmentGroup.ShipmentGroupId;

                                // Save the order detail data
                                session.Save(orderDetailDataItem.OrderDetail);


                                // Iterate each order detail commission item
                                foreach (BusinessObject.Commission orderDetailCommissionItem in orderDetailDataItem.CommissionList)
                                {
                                    // Set foreign ids to order detail tax
                                    orderDetailCommissionItem.OrderDetailId = orderDetailDataItem.OrderDetail.OrderDetailId;

                                    session.Save(orderDetailCommissionItem);
                                }


                                // Iterate each order detail tax item
                                foreach (BusinessObject.OrderDetailTax orderDetailTaxItem in orderDetailDataItem.OrderDetailTaxList)
                                {
                                    // Set foreign ids to order detail tax
                                    orderDetailTaxItem.OrderDetailId = orderDetailDataItem.OrderDetail.OrderDetailId;

                                    session.Save(orderDetailTaxItem);
                                }
                            }

                            #endregion

                            #region Financial Information

                            // Save the Invoice
                            if (orderData.Payment != null)
                            {
                                session.Save(orderData.Payment);
                            }

                            //orderData.Payment.

                            //if (orderData.Invoice != null)
                            //{
                            //    // Save the Invoice
                            //    orderData.Invoice.OrderId = orderData.Order.OrderId;
                            //    session.Save(orderData.Invoice);
                            //}

                            //if (orderData.CreditCard != null)
                            //{
                            //    // Save the Credit Card
                            //    session.Save(orderData.CreditCard);
                            //}

                            //if (orderData.BankCheck != null)
                            //{
                            //    // Save the Cheque Info
                            //    session.Save(orderData.BankCheck);
                            //}

                            //if (orderData.Payment != null)
                            //{
                            //    // Save the Payment
                            //    if (orderData.CreditCard != null)
                            //    {
                            //        orderData.Payment.CreditCardId = orderData.CreditCard.CreditCardId;
                            //    }

                            //    if (orderData.BankCheck != null)
                            //    {
                            //        orderData.Payment.BankCheckId = orderData.BankCheck.BankCheckId;

                            //    }

                            //    session.Save(orderData.Payment);
                            //}

                            //if (isNewRecord && orderData.Payment != null && orderData.Invoice != null)
                            //{
                            //    // Add invoice payment record
                            //    BusinessObject.PaymentInvoice pi = new QSP.Business.Fulfillment.PaymentInvoice();
                            //    //pi.PaymentInvoiceId;
                            //    pi.Amount = orderData.Payment.Amount;
                            //    pi.PaymentId = orderData.Payment.PaymentId;
                            //    pi.InvoiceId = orderData.Invoice.InvoiceId;
                            //    pi.CreateDate = DateTime.Now;
                            //    pi.CreateUserId = -1;
                            //    pi.UpdateDate = DateTime.Now;
                            //    pi.UpdateUserId = -1;

                            //    session.Save(pi);
                            //}

                            //// Save the Payment
                            //if (orderData.Payment != null)
                            //{
                            //   session.Save(orderData.Payment);
                            //}

                            //// Save the Credit Card
                            //if (orderData.CreditCard != null)
                            //{
                            //    session.Save(orderData.CreditCard);
                            //}

                            //// Save the Cheque Info
                            //if (orderData.BankCheck != null)
                            //{
                            //    session.Save(orderData.BankCheck);
                            //}

                            #endregion

                            // Commit transaction
                            transaction.Commit();

                            // Create success response
                            List<object> dynamicValues = new List<object>();
                            dynamicValues.Add(orderData.Order.OrderId);

                            notifications.Add(new BusinessCommunication.Notification("Order ID : " + orderData.Order.OrderId.ToString(), dynamicValues, BusinessCommunication.NotificationType.Information));
                        }
                        catch (Exception ex)
                        {
                            // Rollback transaction
                            transaction.Rollback();

                            // Create failure response
                            List<object> dynamicValues = new List<object>();
                            dynamicValues.Add(ex.Message);
                            if (ex.InnerException != null)
                            {
                                dynamicValues.Add(ex.InnerException.ToString());
                            }

                            notifications.Add(new BusinessCommunication.Notification("System Error : " + ex.Message + "; Stack Trace : " + ex.StackTrace, dynamicValues, BusinessCommunication.NotificationType.Error));
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                // Create failure response
                List<object> dynamicValues = new List<object>();
                dynamicValues.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    dynamicValues.Add(ex.InnerException.ToString());
                }

                notifications.Add(new BusinessCommunication.Notification("System Error : " + ex.Message + "; Stack Trace : " + ex.StackTrace, dynamicValues, BusinessCommunication.NotificationType.Error));
            }
        }

        #region Update methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderData"></param>
        /// <param name="currentOrderData"></param>
        /// <param name="notifications"></param>
        /// <returns></returns>
        public static BusinessObject.Domain.OrderData FillIds(BusinessObject.Domain.OrderData orderData, BusinessObject.Domain.OrderData currentOrderData, BusinessCommunication.Notifications notifications)
        {
            if (notifications.IsSuccessful())
            {
                try
                {
                    #region Billing postal address

                    // Assign record id
                    orderData.PostalAddressBilling.PostalAddressId = currentOrderData.Order.BillingPostalAddressId;
                    orderData.Order.BillingPostalAddressId = currentOrderData.Order.BillingPostalAddressId;

                    // Set data that will not change

                    //orderData.PostalAddressBilling.Address1;
                    //orderData.PostalAddressBilling.Address2;
                    //orderData.PostalAddressBilling.City;
                    //orderData.PostalAddressBilling.County;
                    orderData.PostalAddressBilling.CreateDate = currentOrderData.PostalAddressBilling.CreateDate;
                    orderData.PostalAddressBilling.CreateUserId = currentOrderData.PostalAddressBilling.CreateUserId;
                    orderData.PostalAddressBilling.Deleted = currentOrderData.PostalAddressBilling.Deleted;
                    orderData.PostalAddressBilling.DtsCAccountId = currentOrderData.PostalAddressBilling.DtsCAccountId;
                    orderData.PostalAddressBilling.DtsCCAInstance = currentOrderData.PostalAddressBilling.DtsCCAInstance;
                    orderData.PostalAddressBilling.DtsFlagpoleInstance = currentOrderData.PostalAddressBilling.DtsFlagpoleInstance;
                    //orderData.PostalAddressBilling.FirstName;
                    //orderData.PostalAddressBilling.LastName;
                    //orderData.PostalAddressBilling.Name;
                    orderData.PostalAddressBilling.PostalAddressId = currentOrderData.PostalAddressBilling.PostalAddressId;
                    //orderData.PostalAddressBilling.ResidentialArea;
                    //orderData.PostalAddressBilling.SubdivisionCode;
                    orderData.PostalAddressBilling.SyncBatchId = currentOrderData.PostalAddressBilling.SyncBatchId;
                    orderData.PostalAddressBilling.SyncOeOrd = currentOrderData.PostalAddressBilling.SyncOeOrd;
                    orderData.PostalAddressBilling.UpdateDate = DateTime.Now;
                    //orderData.PostalAddressBilling.UpdateUserId;
                    //orderData.PostalAddressBilling.Zip;
                    //orderData.PostalAddressBilling.Zip4;

                    #endregion

                    #region Billing postal address

                    if (currentOrderData.ShipmentGroup != null)
                    {
                        if (currentOrderData.ShipmentGroup.ShippingPostalAddressId != null)
                        {
                            // Assign record id
                            orderData.PostalAddressShipping.PostalAddressId = (int)currentOrderData.ShipmentGroup.ShippingPostalAddressId;

                            // Set data that will not change
                            //orderData.PostalAddressShipping.Address1;
                            //orderData.PostalAddressShipping.Address2;
                            //orderData.PostalAddressShipping.City;
                            //orderData.PostalAddressShipping.County;
                            orderData.PostalAddressShipping.CreateDate = currentOrderData.PostalAddressShipping.CreateDate;
                            orderData.PostalAddressShipping.CreateUserId = currentOrderData.PostalAddressShipping.CreateUserId;
                            orderData.PostalAddressShipping.Deleted = currentOrderData.PostalAddressShipping.Deleted;
                            orderData.PostalAddressShipping.DtsCAccountId = currentOrderData.PostalAddressShipping.DtsCAccountId;
                            orderData.PostalAddressShipping.DtsCCAInstance = currentOrderData.PostalAddressShipping.DtsCCAInstance;
                            orderData.PostalAddressShipping.DtsFlagpoleInstance = currentOrderData.PostalAddressShipping.DtsFlagpoleInstance;
                            //orderData.PostalAddressShipping.FirstName;
                            //orderData.PostalAddressShipping.LastName;
                            //orderData.PostalAddressShipping.Name;
                            orderData.PostalAddressShipping.PostalAddressId = currentOrderData.PostalAddressShipping.PostalAddressId;
                            //orderData.PostalAddressShipping.ResidentialArea;
                            //orderData.PostalAddressShipping.SubdivisionCode;
                            orderData.PostalAddressShipping.SyncBatchId = currentOrderData.PostalAddressShipping.SyncBatchId;
                            orderData.PostalAddressShipping.SyncOeOrd = currentOrderData.PostalAddressShipping.SyncOeOrd;
                            orderData.PostalAddressShipping.UpdateDate = DateTime.Now;
                            //orderData.PostalAddressShipping.UpdateUserId;
                            //orderData.PostalAddressShipping.Zip;
                            //orderData.PostalAddressShipping.Zip4;
                        }
                    }

                    #endregion

                    #region Billing phone number

                    /*    if (currentOrderData.ShipmentGroup != null)
                    {
                        if (currentOrderData.ShipmentGroup.ShippingPhoneNumberId != null)
                        {
                            // Assign record id
                            orderData.PhoneNumber.PhoneNumberId = (int)currentOrderData.ShipmentGroup.ShippingPhoneNumberId;

                            // Set data that will not change
                            orderData.PhoneNumber.CreateDate = currentOrderData.PhoneNumber.CreateDate;
                            orderData.PhoneNumber.CreateUserId = currentOrderData.PhoneNumber.CreateUserId;
                            orderData.PhoneNumber.Deleted = currentOrderData.PhoneNumber.Deleted;
                            //orderData.PhoneNumber.Phone_Number;
                            //orderData.PhoneNumber.PhoneNumberId;
                            orderData.PhoneNumber.SyncBatchId = currentOrderData.PhoneNumber.SyncBatchId;
                            orderData.PhoneNumber.SyncOeOrd = currentOrderData.PhoneNumber.SyncOeOrd;
                            orderData.PhoneNumber.UpdateDate = DateTime.Now;
                            orderData.PhoneNumber.UpdateUserId = currentOrderData.PhoneNumber.UpdateUserId;
                        }
                    }
                    */
                    #endregion

                    #region Shipment group

                    if (currentOrderData.ShipmentGroup != null)
                    {
                        orderData.ShipmentGroup.CreateDate = currentOrderData.ShipmentGroup.CreateDate;
                        orderData.ShipmentGroup.CreateUserId = currentOrderData.ShipmentGroup.CreateUserId;
                        orderData.ShipmentGroup.Deleted = currentOrderData.ShipmentGroup.Deleted;
                        //orderData.ShipmentGroup.DeliveryMethodId;
                        //orderData.ShipmentGroup.DeliveryNoLaterThan;
                        //orderData.ShipmentGroup.DeliveryWarehouseId;
                        //orderData.ShipmentGroup.ExpeditedFreightChargePaymentAssignmentTypeId;
                        //orderData.ShipmentGroup.RequestedDeliveryDate;
                        //orderData.ShipmentGroup.ShipmentDate;
                        orderData.ShipmentGroup.ShipmentGroupId = currentOrderData.ShipmentGroup.ShipmentGroupId;
                        //orderData.ShipmentGroup.ShipmentSupplyTo;
                        //orderData.ShipmentGroup.ShippingCharges;
                        orderData.ShipmentGroup.ShippingEmailId = currentOrderData.ShipmentGroup.ShippingEmailId;
                        //orderData.ShipmentGroup.ShippingExpeditedCharges;
                        //orderData.ShipmentGroup.ShippingExpeditedChargesCustomerId;
                        //orderData.ShipmentGroup.ShippingExpeditedFreightCharges;
                        orderData.ShipmentGroup.ShippingFaxNumberId = currentOrderData.ShipmentGroup.ShippingFaxNumberId;
                        orderData.ShipmentGroup.ShippingPhoneNumberId = currentOrderData.ShipmentGroup.ShippingPhoneNumberId;
                        orderData.ShipmentGroup.ShippingPostalAddressId = currentOrderData.ShipmentGroup.ShippingPostalAddressId;
                        orderData.ShipmentGroup.SyncBatchId = currentOrderData.ShipmentGroup.SyncBatchId;
                        orderData.ShipmentGroup.SyncOeOrd = currentOrderData.ShipmentGroup.SyncOeOrd;
                        orderData.ShipmentGroup.UpdateDate = DateTime.Now;
                        orderData.ShipmentGroup.UpdateUserId = currentOrderData.ShipmentGroup.UpdateUserId;
                    }

                    #endregion

                    #region Order

                    orderData.Order.OrderId = currentOrderData.Order.OrderId;

                    //orderData.Order.AdjustmentAmount = currentOrderData.Order.AdjustmentAmount;
                    orderData.Order.BillingEmailId = currentOrderData.Order.BillingEmailId;
                    orderData.Order.BillingFaxNumberId = currentOrderData.Order.BillingFaxNumberId;
                    orderData.Order.BillingPhoneNumberId = currentOrderData.Order.BillingPhoneNumberId;
                    orderData.Order.BillingPostalAddressId = currentOrderData.Order.BillingPostalAddressId;
                    orderData.Order.CampaignId = currentOrderData.Order.CampaignId;
                    //orderData.Order.Comments = currentOrderData.Order.Comments;
                    orderData.Order.CreateDate = currentOrderData.Order.CreateDate;
                    orderData.Order.CreateUserId = currentOrderData.Order.CreateUserId;
                    orderData.Order.CustomerId = currentOrderData.Order.CustomerId;
                    orderData.Order.CustomerPoNumber = currentOrderData.Order.CustomerPoNumber;
                    orderData.Order.Deleted = currentOrderData.Order.Deleted;
                    // orderData.Order.FmId = currentOrderData.Order.FmId;
                    orderData.Order.FormId = currentOrderData.Order.FormId;
                    orderData.Order.FulfOrderId = currentOrderData.Order.FulfOrderId;
                    orderData.Order.InstallmentPlanId = currentOrderData.Order.InstallmentPlanId;
                    orderData.Order.OLORD = currentOrderData.Order.OLORD;
                    orderData.Order.OrderDate = currentOrderData.Order.OrderDate;
                    orderData.Order.OrderGroupId = currentOrderData.Order.OrderGroupId;
                    //orderData.Order.OrderStatusId = currentOrderData.Order.OrderStatusId;
                    orderData.Order.OrderTypeId = currentOrderData.Order.OrderTypeId;
                    orderData.Order.ParticipantId = currentOrderData.Order.ParticipantId;
                    //orderData.Order.ProfitRate = currentOrderData.Order.ProfitRate;
                    orderData.Order.SourceId = currentOrderData.Order.SourceId;
                    orderData.Order.StatusReasonId = currentOrderData.Order.StatusReasonId;
                    orderData.Order.UpdateDate = DateTime.Now;
                    orderData.Order.UpdateUserId = currentOrderData.Order.UpdateUserId;

                    #endregion

                    #region Order detail

                    foreach (BusinessObject.Domain.OrderDetailData orderDetailData in orderData.OrderDetailDataList)
                    {
                        #region Load data from existing record

                        foreach (BusinessObject.Domain.OrderDetailData currentOrderDetailData in currentOrderData.OrderDetailDataList)
                        {
                            if (orderDetailData.OrderDetail.CatalogItemDetailId == currentOrderDetailData.OrderDetail.CatalogItemDetailId)
                            {
                                orderDetailData.OrderDetail.OrderDetailId = currentOrderDetailData.OrderDetail.OrderDetailId;

                                //orderDetailData.OrderDetail.AdjustmentQuantity = currentOrderDetailData.OrderDetail.AdjustmentQuantity;
                                orderDetailData.OrderDetail.CatalogItemDetailId = currentOrderDetailData.OrderDetail.CatalogItemDetailId;
                                orderDetailData.OrderDetail.CreateDate = currentOrderDetailData.OrderDetail.CreateDate;
                                orderDetailData.OrderDetail.CreateUserId = currentOrderDetailData.OrderDetail.CreateUserId;
                                //orderDetailData.OrderDetail.Deleted = currentOrderDetailData.OrderDetail.Deleted;
                                orderDetailData.OrderDetail.OrderId = currentOrderDetailData.OrderDetail.OrderId;
                                // orderDetailData.OrderDetail.OrderStatusId = currentOrderDetailData.OrderDetail.OrderStatusId;
                                orderDetailData.OrderDetail.PersonalizationId = currentOrderDetailData.OrderDetail.PersonalizationId;
                                //orderDetailData.OrderDetail.Price = currentOrderDetailData.OrderDetail.Price;
                                //orderDetailData.OrderDetail.PriceA = currentOrderDetailData.OrderDetail.PriceA;
                                //orderDetailData.OrderDetail.Quantity = currentOrderDetailData.OrderDetail.Quantity;
                                //orderDetailData.OrderDetail.Renewal = currentOrderDetailData.OrderDetail.Renewal;
                                orderDetailData.OrderDetail.ShipmentGroupId = currentOrderDetailData.OrderDetail.ShipmentGroupId;
                                //orderDetailData.OrderDetail.SourceId = currentOrderDetailData.OrderDetail.SourceId;
                                //orderDetailData.OrderDetail.StatusReasonId = currentOrderDetailData.OrderDetail.StatusReasonId;
                                orderDetailData.OrderDetail.SyncOecoup = currentOrderDetailData.OrderDetail.SyncOecoup;
                                orderDetailData.OrderDetail.SyncOeitem = currentOrderDetailData.OrderDetail.SyncOeitem;
                                orderDetailData.OrderDetail.SyncOeOrd = currentOrderDetailData.OrderDetail.SyncOeOrd;
                                orderDetailData.OrderDetail.UpdateDate = DateTime.Now;
                                //orderDetailData.OrderDetail.UpdateUserId = currentOrderDetailData.OrderDetail.UpdateUserId;

                                foreach (BusinessObject.OrderDetailTax tax in orderDetailData.OrderDetailTaxList)
                                {
                                    tax.OrderDetailId = currentOrderDetailData.OrderDetail.OrderDetailId;
                                }

                                foreach (BusinessObject.Commission comission in orderDetailData.CommissionList)
                                {
                                    comission.OrderDetailId = currentOrderDetailData.OrderDetail.OrderDetailId;
                                }

                                break;
                            }
                        }

                        #endregion

                        #region Load data for new record

                        if (orderDetailData.OrderDetail.OrderDetailId == 0)
                        {
                            // If we still have no order detail id, it will be a new record
                            // assign order id and shipment group
                            foreach (BusinessObject.Domain.OrderDetailData currentOrderDetailData in currentOrderData.OrderDetailDataList)
                            {
                                if (currentOrderDetailData.OrderDetail.OrderDetailId != 0)
                                {
                                    // We have an existing record, we can get the infor from here
                                    orderDetailData.OrderDetail.OrderId = currentOrderDetailData.OrderDetail.OrderId;
                                    orderDetailData.OrderDetail.ShipmentGroupId = currentOrderDetailData.OrderDetail.ShipmentGroupId;

                                    break;
                                }
                            }
                        }

                        #endregion
                    }

                    #endregion

                    #region Order detail tax

                    foreach (BusinessObject.Domain.OrderDetailData orderDetailData in orderData.OrderDetailDataList)
                    {
                        foreach (BusinessObject.Domain.OrderDetailData currentOrderDetailData in currentOrderData.OrderDetailDataList)
                        {
                            //UPDATE DONE on 8/4/2010, might need to be done somewhere else
                            //need to verify the actual item to continue
                            if (orderDetailData.OrderDetail.CatalogItemDetailId == currentOrderDetailData.OrderDetail.CatalogItemDetailId)
                            {
                                foreach (BusinessObject.OrderDetailTax orderDetailTax in orderDetailData.OrderDetailTaxList)
                                {
                                    foreach (BusinessObject.OrderDetailTax currentOrderDetailTax in currentOrderDetailData.OrderDetailTaxList)
                                    {
                                        if (orderDetailTax.TaxTypeId == currentOrderDetailTax.TaxTypeId)
                                        {
                                            orderDetailTax.OrderDetailTaxId = currentOrderDetailTax.OrderDetailTaxId;

                                            orderDetailTax.CreateDate = currentOrderDetailTax.CreateDate;
                                            orderDetailTax.CreateUserId = currentOrderDetailTax.CreateUserId;
                                            orderDetailTax.Deleted = currentOrderDetailTax.Deleted;
                                            orderDetailTax.OrderDetailId = currentOrderDetailTax.OrderDetailId;
                                            orderDetailTax.OrderDetailTaxId = currentOrderDetailTax.OrderDetailTaxId;
                                            //orderDetailTax.TaxAmount = currentOrderDetailTax.TaxAmount;
                                            //orderDetailTax.TaxCalculationMethodId = currentOrderDetailTax.TaxCalculationMethodId;
                                            //orderDetailTax.TaxRate = currentOrderDetailTax.TaxRate;
                                            //orderDetailTax.TaxTypeId = currentOrderDetailTax.TaxTypeId;
                                            orderDetailTax.UpdateDate = DateTime.Now;
                                            //orderDetailTax.UpdateUserId = currentOrderDetailTax.UpdateUserId;

                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region Order detail commission

                    foreach (BusinessObject.Domain.OrderDetailData orderDetailData in orderData.OrderDetailDataList)
                    {
                        foreach (BusinessObject.Domain.OrderDetailData currentOrderDetailData in currentOrderData.OrderDetailDataList)
                        {
                            if (orderDetailData.OrderDetail.OrderDetailId == currentOrderDetailData.OrderDetail.OrderDetailId)
                            {

                                foreach (BusinessObject.Commission orderCommission in orderDetailData.CommissionList)
                                {
                                    foreach (BusinessObject.Commission currentOrderCommission in currentOrderDetailData.CommissionList)
                                    {
                                        if (currentOrderCommission.CommissionId == 0)
                                        {
                                            // Do nothing
                                        }
                                        else
                                        {
                                            if (orderCommission.OrderDetailId == orderDetailData.OrderDetail.OrderDetailId && orderCommission.CommissionTypeId == currentOrderCommission.CommissionTypeId && orderCommission.FmId == currentOrderCommission.FmId)
                                            {
                                                orderCommission.CommissionId = currentOrderCommission.CommissionId;

                                                //orderCommission.CommissionRate = currentOrderCommission.CommissionRate;
                                                //orderCommission.CommissionTypeId = currentOrderCommission.CommissionTypeId;
                                                orderCommission.CreateDate = currentOrderCommission.CreateDate;
                                                orderCommission.CreateUserId = currentOrderCommission.CreateUserId;
                                                orderCommission.FmId = currentOrderCommission.FmId;
                                                //orderCommission.OrderDetailId = currentOrderCommission.OrderDetailId;
                                                orderCommission.UpdateDate = DateTime.Now;
                                                //orderCommission.UpdateUserId = currentOrderCommission.UpdateUserId;

                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                }
                catch (Exception ex)
                {
                    #region Create failure response

                    List<object> dynamicValues = new List<object>();
                    dynamicValues.Add(ex.Message);
                    if (ex.InnerException != null)
                    {
                        dynamicValues.Add(ex.InnerException.ToString());
                    }

                    notifications.Add(new BusinessCommunication.Notification("System Error : " + ex.Message + "; Stack Trace : " + ex.StackTrace, dynamicValues, BusinessCommunication.NotificationType.Error));

                    #endregion
                }
            }

            return orderData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderStatusId"></param>
        /// <param name="orderId"></param>
        /// <param name="notifications"></param>
        public static void UpdateStatus(int orderStatusId, int orderId, BusinessCommunication.Notifications notifications)
        {
            try
            {
                QSP.Business.Fulfillment.Order order = QSP.Business.Fulfillment.Order.GetOrder(orderId);
                List<QSP.Business.Fulfillment.OrderDetail> orderDetailList = QSP.Business.Fulfillment.OrderDetail.GetOrderDetailListFromOrder(order.OrderId);

                using (ISession session = BusinessObject.SqlSessionManager.OpenSession())
                {
                    // Start transaction
                    ITransaction transaction = session.BeginTransaction();

                    try
                    {
                        #region Update order

                        order.OrderStatusId = orderStatusId;
                        order.UpdateDate = DateTime.Now;
                        //order.UpdateUserId = 101721;

                        session.Update(order);

                        #endregion

                        #region Update order items

                        foreach (QSP.Business.Fulfillment.OrderDetail orderDetailItem in orderDetailList)
                        {
                            orderDetailItem.OrderStatusId = orderStatusId;
                            orderDetailItem.UpdateDate = DateTime.Now;
                            //orderDetailItem.UpdateUserId = 101721;

                            session.Update(orderDetailItem);
                        }

                        #endregion

                        // Commit transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        #region Failure

                        // Rollback transaction
                        transaction.Rollback();

                        // Create failure response
                        List<object> dynamicValues = new List<object>();
                        dynamicValues.Add(ex.Message);
                        if (ex.InnerException != null)
                        {
                            dynamicValues.Add(ex.InnerException.ToString());
                        }

                        notifications.Add(new BusinessCommunication.Notification("System Error : " + ex.Message + "; Stack Trace : " + ex.StackTrace, dynamicValues, BusinessCommunication.NotificationType.Error));

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                #region Create failure response

                List<object> dynamicValues = new List<object>();
                dynamicValues.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    dynamicValues.Add(ex.InnerException.ToString());
                }

                notifications.Add(new BusinessCommunication.Notification("System Error : " + ex.Message + "; Stack Trace : " + ex.StackTrace, dynamicValues, BusinessCommunication.NotificationType.Error));

                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderData"></param>
        /// <param name="currentOrderData"></param>
        /// <param name="notifications"></param>
        private static void UpdateOrder(BusinessObject.Domain.OrderData orderData, BusinessObject.Domain.OrderData currentOrderData, BusinessCommunication.Notifications notifications)
        {
            try
            {

                #region Do data validation

                QSPForm.Business.Validation.OrderValidation.ValidateOrder(orderData, notifications);

                #endregion

                // If successful, save the order
                if (notifications.IsSuccessful())
                {
                    #region Try to save the data

                    using (ISession session = BusinessObject.SqlSessionManager.OpenSession())
                    {
                        // Start transaction
                        ITransaction transaction = session.BeginTransaction();

                        try
                        {
                            session.Update(orderData.PhoneNumber);
                            session.Update(orderData.PostalAddressShipping);
                            session.Update(orderData.PostalAddressBilling);
                            session.Update(orderData.ShipmentGroup);
                            session.Update(orderData.Order);

                            OrderController.UpdateOrderDetails(orderData.OrderDetailDataList, currentOrderData.OrderDetailDataList, notifications, session);

                            // Commit transaction
                            transaction.Commit();

                            // Create success response
                            List<object> dynamicValues = new List<object>();

                            notifications.Add(new BusinessCommunication.Notification("Order ID : " + orderData.Order.OrderId.ToString() + " updated", dynamicValues, BusinessCommunication.NotificationType.Information));
                        }
                        catch (Exception ex)
                        {
                            #region Failure

                            // Rollback transaction
                            transaction.Rollback();

                            // Create failure response
                            List<object> dynamicValues = new List<object>();
                            dynamicValues.Add(ex.Message);
                            if (ex.InnerException != null)
                            {
                                dynamicValues.Add(ex.InnerException.ToString());
                            }

                            notifications.Add(new BusinessCommunication.Notification("System Error : " + ex.Message + "; Stack Trace : " + ex.StackTrace, dynamicValues, BusinessCommunication.NotificationType.Error));

                            #endregion
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                #region Create failure response

                List<object> dynamicValues = new List<object>();
                dynamicValues.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    dynamicValues.Add(ex.InnerException.ToString());
                }

                notifications.Add(new BusinessCommunication.Notification("System Error : " + ex.Message + "; Stack Trace : " + ex.StackTrace, dynamicValues, BusinessCommunication.NotificationType.Error));

                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetailData"></param>
        /// <param name="currentOrderDetailData"></param>
        /// <param name="notifications"></param>
        /// <param name="session"></param>
        private static void UpdateOrderDetails(List<BusinessObject.Domain.OrderDetailData> orderDetailDataList, List<BusinessObject.Domain.OrderDetailData> currentOrderDetailDataList, BusinessCommunication.Notifications notifications, ISession session)
        {
            #region look for order details to delete

            // Look for details in the currentOrderData object that are not in the orderDate object

            foreach (BusinessObject.Domain.OrderDetailData currentOrderDetailData in currentOrderDetailDataList)
            {
                bool deleteCurrentOrderDetailRecord = true;

                foreach (BusinessObject.Domain.OrderDetailData orderDetailData in orderDetailDataList)
                {
                    if (currentOrderDetailData.OrderDetail.OrderDetailId == orderDetailData.OrderDetail.OrderDetailId)
                    {
                        // We found the order detail record, we do not delete
                        deleteCurrentOrderDetailRecord = false;
                        break;
                    }
                }

                if (deleteCurrentOrderDetailRecord)
                {
                    #region delete the record

                    // Delete the order detail
                    currentOrderDetailData.OrderDetail.Deleted = true;
                    session.Update(currentOrderDetailData.OrderDetail);

                    // Delete order detail tax
                    foreach (BusinessObject.OrderDetailTax currentOrderDetailTax in currentOrderDetailData.OrderDetailTaxList)
                    {
                        currentOrderDetailTax.Deleted = true;
                        session.Update(currentOrderDetailTax);
                    }

                    // Delete commissions
                    foreach (BusinessObject.Commission currentCommission in currentOrderDetailData.CommissionList)
                    {
                        currentCommission.Delete();
                    }

                    #endregion
                }
            }

            #endregion

            #region look for order details to create

            // Look for details in the orderDate object that are not in the currentOrderData object

            foreach (BusinessObject.Domain.OrderDetailData orderDetailData in orderDetailDataList)
            {
                if (orderDetailData.OrderDetail.OrderDetailId == 0)
                {
                    // the order detail has no id, this means it is a new record

                    // Set foreign ids to order detail

                    // Region look for order id and shipment group

                    //orderDetailData.OrderDetail.OrderId = orderData.Order.OrderId;
                    //orderDetailData.OrderDetail.ShipmentGroupId = orderData.ShipmentGroup.ShipmentGroupId;

                    // Save the order detail data
                    session.Save(orderDetailData.OrderDetail);


                    // Iterate each order detail commission item
                    foreach (BusinessObject.Commission orderDetailCommission in orderDetailData.CommissionList)
                    {
                        // Set foreign ids to order detail tax
                        orderDetailCommission.OrderDetailId = orderDetailData.OrderDetail.OrderDetailId;

                        session.Save(orderDetailCommission);
                    }


                    // Iterate each order detail tax item
                    foreach (BusinessObject.OrderDetailTax orderDetailTax in orderDetailData.OrderDetailTaxList)
                    {
                        // Set foreign ids to order detail tax
                        orderDetailTax.OrderDetailId = orderDetailData.OrderDetail.OrderDetailId;

                        session.Save(orderDetailTax);
                    }
                }
            }

            #endregion

            #region look for order details to update

            // Look for details in the orderDate object that are also in the currentOrderData object

            foreach (BusinessObject.Domain.OrderDetailData orderDetailData in orderDetailDataList)
            {
                foreach (BusinessObject.Domain.OrderDetailData currentOrderDetailData in currentOrderDetailDataList)
                {
                    if (orderDetailData.OrderDetail.OrderDetailId == currentOrderDetailData.OrderDetail.OrderDetailId)
                    {
                        //orderDetailData.OrderDetail.OrderId = currentOrderDetailData.OrderDetail.OrderId;
                        //orderDetailData.OrderDetail.ShipmentGroupId = currentOrderDetailData.OrderDetail.ShipmentGroupId;

                        session.Update(orderDetailData.OrderDetail);
                        OrderController.UpdateOrderDetailTaxes(orderDetailData.OrderDetailTaxList, currentOrderDetailData.OrderDetailTaxList, notifications, session);
                        OrderController.UpdateOrderDetailCommissions(orderDetailData.CommissionList, currentOrderDetailData.CommissionList, notifications, session);

                        break;
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetailCommissionList"></param>
        /// <param name="currentOrderDetailCommissionList"></param>
        /// <param name="notifications"></param>
        /// <param name="session"></param>
        private static void UpdateOrderDetailCommissions(List<BusinessObject.Commission> orderDetailCommissionList, List<BusinessObject.Commission> currentOrderDetailCommissionList, BusinessCommunication.Notifications notifications, ISession session)
        {
            #region look for order detail commissions to delete

            foreach (BusinessObject.Commission currentOrderDetailCommission in currentOrderDetailCommissionList)
            {
                bool deleteCurrentOrderDetailCommissionRecord = true;

                foreach (BusinessObject.Commission orderDetailCommission in orderDetailCommissionList)
                {
                    if (currentOrderDetailCommission.CommissionId == orderDetailCommission.CommissionId)
                    {
                        // We found the order detail record, we do not delete
                        deleteCurrentOrderDetailCommissionRecord = false;
                        break;
                    }
                }

                if (deleteCurrentOrderDetailCommissionRecord)
                {
                    #region delete the record

                    // Delete the order detail tax
                    currentOrderDetailCommission.Delete();

                    #endregion
                }
            }

            #endregion

            #region look for order detail commissions to create

            foreach (BusinessObject.Commission orderDetailCommission in orderDetailCommissionList)
            {
                if (orderDetailCommission.CommissionId == 0)
                {
                    // the order detail tax has no id, this means it is a new record
                    //orderDetailCommission.OrderDetailId = orderDetailData.OrderDetail.OrderDetailId;
                    session.Save(orderDetailCommission);
                }
            }

            #endregion

            #region look for order detail commissions to update

            foreach (BusinessObject.Commission orderDetailCommission in orderDetailCommissionList)
            {
                foreach (BusinessObject.Commission currentOrderDetailCommission in currentOrderDetailCommissionList)
                {
                    if (orderDetailCommission.CommissionId == currentOrderDetailCommission.CommissionId)
                    {
                        orderDetailCommission.OrderDetailId = currentOrderDetailCommission.OrderDetailId;
                        session.Update(orderDetailCommission);
                        break;
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetailTaxList"></param>
        /// <param name="currentOrderDetailTaxList"></param>
        /// <param name="notifications"></param>
        /// <param name="session"></param>
        private static void UpdateOrderDetailTaxes(List<BusinessObject.OrderDetailTax> orderDetailTaxList, List<BusinessObject.OrderDetailTax> currentOrderDetailTaxList, BusinessCommunication.Notifications notifications, ISession session)
        {
            #region look for order detail taxes to delete

            foreach (BusinessObject.OrderDetailTax currentOrderDetailTax in currentOrderDetailTaxList)
            {
                bool deleteCurrentOrderDetailTaxRecord = true;

                foreach (BusinessObject.OrderDetailTax orderDetailTax in orderDetailTaxList)
                {
                    if (currentOrderDetailTax.OrderDetailTaxId == orderDetailTax.OrderDetailTaxId)
                    {
                        // We found the order detail record, we do not delete
                        deleteCurrentOrderDetailTaxRecord = false;
                        break;
                    }
                }

                if (deleteCurrentOrderDetailTaxRecord)
                {
                    #region delete the record

                    // Delete the order detail tax
                    currentOrderDetailTax.Deleted = true;
                    session.Update(currentOrderDetailTax);

                    #endregion
                }
            }

            #endregion

            #region look for order detail taxes to create

            foreach (BusinessObject.OrderDetailTax orderDetailTax in orderDetailTaxList)
            {
                if (orderDetailTax.OrderDetailTaxId == 0)
                {
                    // the order detail tax has no id, this means it is a new record
                    //orderDetailTax.OrderDetailId = orderDetailData.OrderDetail.OrderDetailId;
                    session.Save(orderDetailTax);
                }
            }

            #endregion

            #region look for order detail taxes to update

            foreach (BusinessObject.OrderDetailTax orderDetailTax in orderDetailTaxList)
            {
                foreach (BusinessObject.OrderDetailTax currentOrderDetailTax in currentOrderDetailTaxList)
                {
                    if (orderDetailTax.OrderDetailTaxId == currentOrderDetailTax.OrderDetailTaxId)
                    {
                        //orderDetailTax.OrderDetailId = currentOrderDetailTax.OrderDetailId;
                        session.Update(orderDetailTax);
                        break;
                    }
                }
            }

            #endregion
        }

        #endregion

        #endregion

        #region Charges management

        /// <summary>
        /// Gets a list of existing charges for the specified order
        /// </summary>
        /// <param name="orderId">The id of the order to get the carges from</param>
        /// <param name="notifications"></param>
        /// <returns>A list of charges that where loaded</returns>
        public List<QSP.Business.Fulfillment.OrderCharge> GetCharges(int orderId, BusinessCommunication.Notifications notifications)
        {
            List<QSP.Business.Fulfillment.OrderCharge> charges = new List<QSP.Business.Fulfillment.OrderCharge>();
            ICriteria criteria = BusinessObject.OrderCharge.CreateCriteria();
            charges = QSP.Business.Fulfillment.OrderCharge.GetOrderChargeList(criteria);

            return charges;
        }

        /// <summary>
        /// Creates and saves a list of applicable charges to the specified order
        /// </summary>
        /// <param name="orderId">The id of the order to generate the charges to</param>
        /// <param name="notifications"></param>
        public void GenerateCharges(int orderId, BusinessCommunication.Notifications notifications)
        {
            #region Delete existing charges before recreating them with updated info

            this.RemoveCharges(orderId, notifications);

            #endregion

            #region Determine which form is being used

            bool isOtis = false;
            bool isPineValley = false;

            QSPForm.Business.FormSystem formSystem = new FormSystem();
            QSP.Business.Fulfillment.Order order = QSP.Business.Fulfillment.Order.GetOrder(orderId);

            if (order.FormId != null)
            {
                isOtis = formSystem.IsOtisForm(Convert.ToInt32(order.FormId));
                isPineValley = formSystem.IsPineValleyForm(Convert.ToInt32(order.FormId));
            }

            #endregion

            if (isOtis)
            {
                this.OtisGenerateCharges(order, notifications);
            }
            else if (isPineValley)
            {
                this.PineValleyGenerateCharges(order, notifications);
            }
        }

        /// <summary>
        /// Adds the order charge to the database
        /// </summary>
        /// <param name="orderCharge">The order charge to be added</param>
        /// <param name="notifications"></param>
        public void AddCharge(int orderId, QSP.Business.Fulfillment.OrderCharge orderCharge, BusinessCommunication.Notifications notifications)
        {
            List<QSP.Business.Fulfillment.OrderCharge> charges = GetCharges(orderId, notifications);
            charges.Add(orderCharge);
            SaveCharges(charges, notifications);
        }

        /// <summary>
        /// Update the order charge in the database
        /// </summary>
        /// <param name="orderCharge">The order charge to be updated</param>
        /// <param name="notifications"></param>
        public void UpdateCharge(QSP.Business.Fulfillment.OrderCharge orderCharge, BusinessCommunication.Notifications notifications)
        {
            List<QSP.Business.Fulfillment.OrderCharge> charges = GetCharges(orderCharge.OrderId, notifications);
            charges.Find(delegate(QSP.Business.Fulfillment.OrderCharge oc) { return oc.OrderChargeId == orderCharge.OrderChargeId; });
            SaveCharges(charges, notifications);
        }

        /// <summary>
        /// Removes the order charge from the database
        /// </summary>
        /// <param name="orderCharge">The order charge to be removed</param>
        /// <param name="notifications"></param>
        public void RemoveCharge(QSP.Business.Fulfillment.OrderCharge orderCharge, BusinessCommunication.Notifications notifications)
        {
            orderCharge.Delete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="notifications"></param>
        public void RemoveCharges(int orderId, BusinessCommunication.Notifications notifications)
        {
            List<QSP.Business.Fulfillment.OrderCharge> orderChargeList = QSP.Business.Fulfillment.OrderCharge.GetOrderChargeListFromOrder(orderId);

            foreach (QSP.Business.Fulfillment.OrderCharge orderCharge in orderChargeList)
            {
                QSP.Business.Fulfillment.OrderCharge.DeleteOrderCharge(orderCharge);
            }
        }

        /// <summary>
        /// Saves the list of charges supplied to the database
        /// </summary>
        /// <param name="chargeList">The list of charges to be saved</param>
        /// <param name="notifications"></param>
        private void SaveCharges(List<QSP.Business.Fulfillment.OrderCharge> chargeList, BusinessCommunication.Notifications notifications)
        {
            foreach (QSP.Business.Fulfillment.OrderCharge oc in chargeList)
            {
                oc.Save();
            }
        }

        #endregion

        #region Otis Spunkmeyer Spring 2009 specific logic

        /// <summary>
        /// Generates a list of applicable charges to the specified order.
        /// </summary>
        /// <remarks>
        /// Business logic specific to OTis Spunkmeyer bulk order forms
        /// </remarks>
        /// <param name="orderId">The id of the order to generate the charges to</param>
        /// <param name="notifications"></param>
        private void OtisGenerateCharges(QSP.Business.Fulfillment.Order order, BusinessCommunication.Notifications notifications)
        {
            bool isDsd = false;
            bool isContinuation = false;
            bool isOrderPickup = false;
            string deliveryZipCode = "";
            int orderTotalNumberOfCases = 0;
            int formId = 0;
            decimal orderTotalAmount = 0;
            List<QSP.Business.Fulfillment.OrderCharge> chargeList = new List<QSP.Business.Fulfillment.OrderCharge>();

            #region Get info from order

            List<QSP.Business.Fulfillment.OrderDetail> odList = QSP.Business.Fulfillment.OrderDetail.GetOrderDetailListFromOrder(order.OrderId);
            if (odList.Count > 0)
            {
                int shipmentGroupId = odList[0].ShipmentGroupId;
                QSP.Business.Fulfillment.ShipmentGroup sg = QSP.Business.Fulfillment.ShipmentGroup.GetShipmentGroup(shipmentGroupId);
                QSP.Business.Fulfillment.PostalAddress deliveryAddress = QSP.Business.Fulfillment.PostalAddress.GetPostalAddress((int)sg.ShippingPostalAddressId);
                deliveryZipCode = deliveryAddress.Zip;

                if (sg.DeliveryMethodId == 2)
                {
                    isOrderPickup = true;
                }
                else
                {
                    isOrderPickup = false;
                }
            }

            foreach (QSP.Business.Fulfillment.OrderDetail od in odList)
            {
                orderTotalNumberOfCases += (od.Quantity + od.AdjustmentQuantity);
                orderTotalAmount += ((od.Quantity + od.AdjustmentQuantity) * od.Price);
            }

            isDsd = this.CheckIfInArea(deliveryZipCode, order.FormId.Value);
            isContinuation = this.OtisCheckIfContinuation(order, isDsd);

            #endregion

            #region Get charges

            if (!isContinuation && !isOrderPickup)
            {
                //2010-08-12 : Teresa asked to remove these, as we no longer use DSD & LiftGate charges
                //this.OtisCreateChargeDsdOrNonDsd(isContinuation, isDsd, orderTotalNumberOfCases, chargeList, notifications);
                //this.OtisCreateChargeLiftgate(isDsd, orderTotalNumberOfCases, chargeList, notifications);

                //Fuel Charges are used
                this.OtisCreateChargeFuelCharge(orderTotalAmount, chargeList, notifications);
                //Less than minimum charge (moved from Business exception, as this is surcharge)
                if (order.FormId.HasValue)
                    formId = order.FormId.Value;
                this.OtisLessThanMinimumCharge(odList, chargeList, formId);
            }

            #endregion

            #region Save charges

            // Set order id in charges
            foreach (QSP.Business.Fulfillment.OrderCharge oc in chargeList)
            {
                oc.OrderId = order.OrderId;
            }

            // Save charges to database
            this.SaveCharges(chargeList, notifications);

            // Save continuation status
            this.SaveContinuationFlag(order.OrderId, isContinuation);

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool OtisCheckIfContinuation(QSP.Business.Fulfillment.Order order, bool isDSD)
        {
            bool result = false;

            if (isDSD)
            {
                if (order.FormId != null)
                {
                    #region Check if we have previous continuations

                    bool previousContinuationExist = false;

                    // Get a list of orders from the same campaign and form
                    List<QSP.Business.Fulfillment.Order> orderList = QSP.Business.Fulfillment.Order.GetOrderListFromCampaignAndForm(order.CampaignId, Convert.ToInt32(order.FormId));
                    //orderList.Remove(order);

                    foreach (QSP.Business.Fulfillment.Order existingOrder in orderList)
                    {
                        if (existingOrder.IsContinuation)
                        {
                            previousContinuationExist = true;
                            break;
                        }
                    }

                    #endregion

                    if (!previousContinuationExist)
                    {
                        if (orderList.Count > 0)
                        {
                            #region Check if new order is before last order's delivery date

                            bool isBeforeLastDeliveryDate = false;

                            orderList.Sort(CompareOrdersByReverseDate);
                            DateTime lastDeliveryDate = GetLastRequestedDeliveryDate(orderList[0].OrderId);
                            //DateTime newDeliveryDate = GetLastRequestedDeliveryDate(order.OrderId);

                            #endregion

                            if (lastDeliveryDate > DateTime.Now)
                            {
                                result = true;
                            }
                            else
                            {
                                #region Check if last delivery is within 10 days

                                TimeSpan span = lastDeliveryDate.Subtract(DateTime.Now);

                                #endregion

                                if (span.Days < 10)
                                {
                                    result = true;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private DateTime GetLastRequestedDeliveryDate(int orderId)
        {
            DateTime result = DateTime.MinValue;

            List<QSP.Business.Fulfillment.OrderDetail> orderDetailList = QSP.Business.Fulfillment.OrderDetail.GetOrderDetailListFromOrder(orderId);

            if (orderDetailList.Count > 0)
            {
                // Use the shipment_group_id for the order detail record to look up requested delivery date
                QSP.Business.Fulfillment.ShipmentGroup shipmentGroup = QSP.Business.Fulfillment.ShipmentGroup.GetShipmentGroup(orderDetailList[0].ShipmentGroupId);

                result = (DateTime)shipmentGroup.RequestedDeliveryDate; ;
            }
            else
            {
                result = DateTime.MinValue;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderA"></param>
        /// <param name="orderB"></param>
        /// <returns></returns>
        private static int CompareOrdersByReverseDate(BusinessObject.Order orderA, BusinessObject.Order orderB)
        {
            return orderB.OrderDate.CompareTo(orderA.OrderDate);
        }

        /// <summary>
        /// Creates a DSD or Non DSD charge if applicable
        /// </summary>
        /// <param name="isContinuation">Specified if the order is a continuation or not</param>
        /// <param name="isDsd">Specifies if the order is a DSD order or not</param>
        /// <param name="numberOfCases">The number of cases ordered</param>
        /// <param name="chargeList">The list to add the charge to</param>
        /// <param name="notifications"></param>
        private void OtisCreateChargeDsdOrNonDsd(bool isContinuation, bool isDsd, int numberOfCases, List<QSP.Business.Fulfillment.OrderCharge> chargeList, BusinessCommunication.Notifications notifications)
        {
            // The logic is as follows:
            // 
            // New Orders with 50 or more cases
            //     DSD Delivery (In Area)  = $0
            //     Non-DSD / Conway Delivery (Out Area) = $0
            // 
            // New Orders less than 50 cases
            //     DSD Delivery (In Area)  = $125
            //     Non-DSD / Conway Delivery (Out Area) = $250
            // 
            // We need to create a table with zipcode and a flag to see if it is dsd or non dsd
            //
            // There is No Continuation Order for orders in Non-DSD area.   All orders will be treated as New Orders.   All shipping and handling charges apply.
            // Only 1 continuation order allowed per season in DSD area for free of charge.
            // Continuation orders in DSD area has no minimum case quantity limits


            bool addCharge = false;
            decimal chargeAmount = 0;

            #region Generate charge

            if (isContinuation)
            {
                #region Continuation order

                if (numberOfCases < 50)
                {
                    if (!isDsd)
                    {
                        addCharge = true;
                        chargeAmount = 250;
                    }
                }

                #endregion
            }
            else
            {
                #region New order

                if (numberOfCases < 50)
                {
                    if (isDsd)
                    {
                        addCharge = true;
                        chargeAmount = 125;
                    }
                    else
                    {
                        addCharge = true;
                        chargeAmount = 250;
                    }
                }

                #endregion
            }

            #endregion

            if (addCharge)
            {
                #region Add charge to list

                QSP.Business.Fulfillment.OrderCharge oc = new QSP.Business.Fulfillment.OrderCharge();

                oc.OrderId = 0;

                if (isDsd)
                {
                    oc.ChargeId = (int)ChargeTypeEnum.DSDDelivery;
                }
                else
                {
                    oc.ChargeId = (int)ChargeTypeEnum.NonDSDDelivery;
                }

                oc.ChargeToId = "A";

                oc.Amount = chargeAmount;
                oc.CreateDate = DateTime.Now;
                oc.CreateUserId = 100010;
                // oc.OrderChargeId;
                // oc.AccountId;
                // oc.EstimatedAmount;
                // oc.Comment;
                // oc.UpdateDate;
                // oc.UpdateUserId;

                chargeList.Add(oc);

                #endregion
            }

        }

        /// <summary>
        /// Creates a lift gate charge if applicable
        /// </summary>
        /// <param name="isDsd">Specifies if the order is a DSD order or not</param>
        /// <param name="numberOfCases">The number of cases ordered</param>
        /// <param name="chargeList">The list to add the charge to</param>
        /// <param name="notifications"></param>
        private void OtisCreateChargeLiftgate(bool isDsd, int numberOfCases, List<QSP.Business.Fulfillment.OrderCharge> chargeList, BusinessCommunication.Notifications notifications)
        {
            // Non-DSD orders over 100 cases  -  there is a Lift Gate charge of $100

            bool addCharge = false;
            decimal chargeAmount = 0;

            #region Generate charge

            if (!isDsd && numberOfCases > 100)
            {
                addCharge = true;
                chargeAmount = 100;
            }

            #endregion

            if (addCharge)
            {
                #region Add charge to list

                QSP.Business.Fulfillment.OrderCharge oc = new QSP.Business.Fulfillment.OrderCharge();

                oc.OrderId = 0;
                oc.ChargeId = (int)ChargeTypeEnum.LiftGate;
                oc.ChargeToId = "A";
                oc.Amount = chargeAmount;
                oc.CreateDate = DateTime.Now;
                oc.CreateUserId = 100010;
                // oc.OrderChargeId;
                // oc.AccountId;
                // oc.EstimatedAmount;
                // oc.Comment;
                // oc.UpdateDate;
                // oc.UpdateUserId;

                chargeList.Add(oc);

                #endregion
            }
        }

        /// <summary>
        /// Creates a fuel charge if applicable
        /// </summary>
        /// <param name="orderTotalAmount">The total amount of the  order</param>
        /// <param name="chargeList">The list to add the charge to</param>
        /// <param name="notifications"></param>
        private void OtisCreateChargeFuelCharge(decimal orderTotalAmount, List<QSP.Business.Fulfillment.OrderCharge> chargeList, BusinessCommunication.Notifications notifications)
        {
            // Any order under $2000 – There will be a Fuel Charge of $50

            bool addCharge = false;
            decimal chargeAmount = 0;

            #region Generate charge

            if (orderTotalAmount < 2000)
            {
                addCharge = true;
                chargeAmount = 50;
            }

            #endregion

            if (addCharge)
            {
                #region Add charge to list

                QSP.Business.Fulfillment.OrderCharge oc = new QSP.Business.Fulfillment.OrderCharge();

                oc.OrderId = 0;
                oc.ChargeId = (int)ChargeTypeEnum.Fuel;
                oc.ChargeToId = "A";
                oc.Amount = chargeAmount;
                oc.CreateDate = DateTime.Now;
                oc.CreateUserId = 100010;
                // oc.OrderChargeId;
                // oc.AccountId;
                // oc.EstimatedAmount;
                // oc.Comment;
                // oc.UpdateDate;
                // oc.UpdateUserId;

                chargeList.Add(oc);

                #endregion
            }
        }

        /// <summary>
        /// Creates a Less than minimum charge if applicable
        /// </summary>
        /// <param name="odList">OrderDetail List from Order to check CD</param>
        /// <param name="chargeList">The list to add the charge to</param>
        /// <param name="notifications"></param>
        private void OtisLessThanMinimumCharge(List<QSP.Business.Fulfillment.OrderDetail> odList, List<QSP.Business.Fulfillment.OrderCharge> chargeList, int formId)
        {
            // Any order has less than 120 units

            bool addCharge = false;
            int quantity = 0;
            List<int> cdMinCountFormId = new List<int>();
            //get Cookie dough product quantities
            QSPForm.Business.ProductSystem prodSys = new QSPForm.Business.ProductSystem();
            //check for product type cookie dough, poduct_type_id = 6
            QSPForm.Common.DataDef.ProductTable products = prodSys.SelectAllByProductType(6);
            QSPForm.Data.CatalogItemDetail catDetailDataAccess = new QSPForm.Data.CatalogItemDetail();

            //get the Combo Form Ids
            try
            {
                //cd min count order form id
                foreach (string strFormId in ConfigurationManager.AppSettings["CD_MinCount_form"].ToString().Split(new char[] { ',' }))
                {
                    cdMinCountFormId.Add(Convert.ToInt32(strFormId.Trim()));
                }
            }
            catch
            {

            }

            #region Generate charge

            foreach (QSP.Business.Fulfillment.OrderDetail od in odList)
            {

                if (cdMinCountFormId.Contains(formId))
                {
                    //only for Otis combo forms

                    //get Catalogdetail record
                    QSPForm.Common.DataDef.CatalogItemDetailTable dTblCatDetail = catDetailDataAccess.SelectOne(od.CatalogItemDetailId);
                    //use the Catalogdetail_code to search in product table
                    if (products.Select(QSPForm.Common.DataDef.ProductTable.FLD_CODE + "=" + dTblCatDetail.Rows[0][QSPForm.Common.DataDef.CatalogItemDetailTable.FLD_CODE].ToString()).Length > 0)
                    {
                        quantity = quantity + od.Quantity;
                    }
                }
                else
                {
                    //normal case for any forms
                    quantity = quantity + od.Quantity;
                }

            }

            //if quantity
            if (quantity < 120)
            {
                addCharge = true;
            }

            #endregion

            if (addCharge)
            {
                #region Add charge to list

                QSP.Business.Fulfillment.OrderCharge oc = new QSP.Business.Fulfillment.OrderCharge();

                oc.OrderId = 0;
                oc.ChargeId = (int)ChargeTypeEnum.MinimumOrder;
                oc.ChargeToId = "A";
                oc.Amount = 100;
                oc.CreateDate = DateTime.Now;
                oc.CreateUserId = 100010;
                // oc.OrderChargeId;
                // oc.AccountId;
                // oc.EstimatedAmount;
                // oc.Comment;
                // oc.UpdateDate;
                // oc.UpdateUserId;

                chargeList.Add(oc);

                #endregion
            }
        }


        /// <summary>
        /// Calculates the lead time for the Otis order
        /// Default value of 15 business days applies
        /// </summary>
        /// <param name="notifications"></param>
        /// <returns>The calculated lead time</returns>
        private int OtisCalculateLeadTime(BusinessCommunication.Notifications notifications)
        {
            // Default lead time is 15 days
            int result = 15;

            return result;
        }

        #endregion

        #region Pine Valley Spring 2009 specific logic

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="notifications"></param>
        private void PineValleyGenerateCharges(QSP.Business.Fulfillment.Order order, BusinessCommunication.Notifications notifications)
        {
            bool isInArea = false;
            string deliveryZipCode = "";
            int orderTotalNumberOfCases = 0;
            decimal orderTotalAmount = 0;
            List<QSP.Business.Fulfillment.OrderCharge> chargeList = new List<QSP.Business.Fulfillment.OrderCharge>();

            //Has PA stuff already been accounted for?

            #region Get info from order

            List<QSP.Business.Fulfillment.OrderDetail> odList = QSP.Business.Fulfillment.OrderDetail.GetOrderDetailListFromOrder(order.OrderId);
            if (odList.Count > 0)
            {
                int shipmentGroupId = odList[0].ShipmentGroupId;
                QSP.Business.Fulfillment.ShipmentGroup sg = QSP.Business.Fulfillment.ShipmentGroup.GetShipmentGroup(shipmentGroupId);
                QSP.Business.Fulfillment.PostalAddress deliveryAddress = QSP.Business.Fulfillment.PostalAddress.GetPostalAddress((int)sg.ShippingPostalAddressId);

                deliveryZipCode = deliveryAddress.Zip;
            }

            foreach (QSP.Business.Fulfillment.OrderDetail od in odList)
            {
                orderTotalNumberOfCases += (od.Quantity + od.AdjustmentQuantity);
                orderTotalAmount += ((od.Quantity + od.AdjustmentQuantity) * od.Price);
            }

            isInArea = this.CheckIfInArea(deliveryZipCode, order.FormId.Value);

            #endregion

            #region Get charges

            this.PineValleyCreateChargeRemoteDelivery(isInArea, deliveryZipCode, chargeList, notifications);
            this.PineValleyCreateChargeMinimumOrder(orderTotalNumberOfCases, chargeList, notifications);
            this.PineValleyCreateChargeFuelCharge(orderTotalAmount, chargeList, notifications);

            #endregion

            #region Save charges

            // Set order id in charges
            foreach (QSP.Business.Fulfillment.OrderCharge oc in chargeList)
            {
                oc.OrderId = order.OrderId;
            }

            // Save charges to database
            this.SaveCharges(chargeList, notifications);

            #endregion
        }

        /// <summary>
        /// Returns whether specified zip code is in direct shipping area for current order form.
        /// </summary>
        /// <param name="zipCode">The zip code to look for shipment info.</param>
        /// <param name="formId">The order form being shipped.</param>
        /// <returns>True if direct shipping (DSD), false if remote shipping (NON-DSD).</returns>
        private bool CheckIfInArea(string zipCode, int formId)
        {
            bool result = false;

            // Trim the 2nd part of zip codes: 87108-2207
            if (zipCode.Length > 5)
            {
                zipCode = zipCode.Substring(0, 5);
            }

            List<QSP.Business.Fulfillment.DeliveryService> dsList = QSP.Business.Fulfillment.DeliveryService.GetDeliveryServiceListFromZipCode(zipCode, formId);

            foreach (QSP.Business.Fulfillment.DeliveryService ds in dsList)
            {
                // 1 is local delivery; 2 is remote delivery; 3 is no service
                if (dsList[0].DeliveryServiceTypeId == 1)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Creates a minimum order charge if aplicable
        /// </summary>
        /// <param name="numberOfCases">The number of cases ordered</param>
        /// <param name="chargeList">The list to add the charge to</param>
        /// <param name="notifications"></param>
        private void PineValleyCreateChargeMinimumOrder(int numberOfCases, List<QSP.Business.Fulfillment.OrderCharge> chargeList, BusinessCommunication.Notifications notifications)
        {
            // New Orders less than 66 cases
            // “Minimum Order” Fee/Surcharge  = $145

            bool addCharge = false;
            decimal chargeAmount = 0;

            #region Generate charge

            if (numberOfCases < 66)
            {
                addCharge = true;
                chargeAmount = 145;
            }

            #endregion

            if (addCharge)
            {
                #region Add charge to list

                QSP.Business.Fulfillment.OrderCharge oc = new QSP.Business.Fulfillment.OrderCharge();

                oc.OrderId = 0;
                oc.ChargeId = (int)ChargeTypeEnum.MinimumOrder;
                oc.ChargeToId = "A";
                oc.Amount = chargeAmount;
                oc.CreateDate = DateTime.Now;
                oc.CreateUserId = 100010;
                // oc.OrderChargeId;
                // oc.AccountId;
                // oc.EstimatedAmount;
                // oc.Comment;
                // oc.UpdateDate;
                // oc.UpdateUserId;

                chargeList.Add(oc);

                #endregion
            }
        }

        /// <summary>
        /// Creates a remote delivery charge if aplicable
        /// </summary>
        /// <param name="isInArea">Specifies if the order is in area or not</param>
        /// <param name="deliveryZipCode">The delivery zip code for the order</param>
        /// <param name="chargeList">The list to add the charge to</param>
        /// <param name="notifications"></param>
        private void PineValleyCreateChargeRemoteDelivery(bool isInArea, string deliveryZipCode, List<QSP.Business.Fulfillment.OrderCharge> chargeList, BusinessCommunication.Notifications notifications)
        {
            // New Orders less than 66 cases
            // “Remote Delivery”  (Out of Area) Fee/Surcharge  is about $200 

            bool addCharge = false;
            decimal chargeAmount = 0;

            #region Generate charge

            if (!isInArea)
            {
                addCharge = true;
                chargeAmount = 200;
            }

            #endregion

            if (addCharge)
            {
                #region Add charge to list

                QSP.Business.Fulfillment.OrderCharge oc = new QSP.Business.Fulfillment.OrderCharge();

                oc.OrderId = 0;
                oc.ChargeId = (int)ChargeTypeEnum.RemoteDelivery;
                oc.ChargeToId = "A";
                oc.Amount = chargeAmount;
                oc.CreateDate = DateTime.Now;
                oc.CreateUserId = 100010;
                // oc.OrderChargeId;
                // oc.AccountId;
                // oc.EstimatedAmount;
                // oc.Comment;
                // oc.UpdateDate;
                // oc.UpdateUserId;

                chargeList.Add(oc);

                #endregion
            }
        }

        /// <summary>
        /// Creates a fuel charge if aplicable
        /// </summary>
        /// <param name="orderTotalAmount">The total amount of the  order</param>
        /// <param name="chargeList">The list to add the charge to</param>
        /// <param name="notifications"></param>
        private void PineValleyCreateChargeFuelCharge(decimal orderTotalAmount, List<QSP.Business.Fulfillment.OrderCharge> chargeList, BusinessCommunication.Notifications notifications)
        {
            // Any order under $2000 – There will be a Fuel Charge of $50

            bool addCharge = false;
            decimal chargeAmount = 0;

            #region Generate charge

            if (orderTotalAmount < 2000)
            {
                addCharge = true;
                chargeAmount = 50;
            }

            #endregion

            if (addCharge)
            {
                #region Add charge to list

                QSP.Business.Fulfillment.OrderCharge oc = new QSP.Business.Fulfillment.OrderCharge();

                oc.OrderId = 0;
                oc.ChargeId = (int)ChargeTypeEnum.Fuel;
                oc.ChargeToId = "A";
                oc.Amount = chargeAmount;
                oc.CreateDate = DateTime.Now;
                oc.CreateUserId = 100010;
                // oc.OrderChargeId;
                // oc.AccountId;
                // oc.EstimatedAmount;
                // oc.Comment;
                // oc.UpdateDate;
                // oc.UpdateUserId;

                chargeList.Add(oc);

                #endregion
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifications"></param>
        private int PineValleyCalculateLeadTime(BusinessCommunication.Notifications notifications)
        {
            // Default lead time is 15 days
            int result = 15;

            return result;
        }

        #endregion

        #region Other methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatusId"></param>
        /// <param name="notifications"></param>
        public void UpdateOrderStatus(int orderId, int orderStatusId, BusinessCommunication.Notifications notifications)
        {
            QSP.Business.Fulfillment.Order order = QSP.Business.Fulfillment.Order.GetOrder(orderId);

            order.OrderStatusId = orderStatusId;

            QSP.Business.Fulfillment.Order.UpdateOrder(order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="isContinuation"></param>
        private void SaveContinuationFlag(int orderId, bool isContinuation)
        {
            QSP.Business.Fulfillment.Order order = QSP.Business.Fulfillment.Order.GetOrder(orderId);

            order.IsContinuation = isContinuation;

            QSP.Business.Fulfillment.Order.UpdateOrder(order);
        }

        #endregion

    }
}

