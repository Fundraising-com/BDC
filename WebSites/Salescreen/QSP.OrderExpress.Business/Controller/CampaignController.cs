using System;
using System.Collections.Generic;
using System.Text;

using NHibernate;
using NHibernate.Criterion;

using BusinessCommunication = QSPForm.Business.Communication;
using BusinessObject = QSP.Business.Fulfillment;
using BusinessValidation = QSPForm.Business.Validation;

namespace QSPForm.Business.Controller
{

    /// <summary>
    /// Handles the business logic regarding campaigns
    /// </summary>
    public class CampaignController
    {

        #region Public variables

        public static int PHONE_NUMBER_TYPE_BILLING = 5;     // Extracted from database QSPFulfillment.phone_number_type
        public static int PHONE_NUMBER_TYPE_SHIPPING = 7;     // Extracted from database QSPFulfillment.phone_number_type

        public static DateTime DefaultCreateDate = DateTime.Now;
        public static int DefaultCreateUserId = 100010;
        public static DateTime DefaultUpdateDate = DateTime.Now;
        public static int DefaultUpdateUserId = 100010;

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="campaign"></param>
        /// <param name="notifications"></param>
        public static void SaveCampaign(QSP.Business.Fulfillment.Domain.CampaignData campaignData, BusinessCommunication.Notifications notifications)
        {
            try
            {

                //if (campaignData.Campaign.CampaignId == 0)
                // Check if a campaign exists for the account and program type
                List<QSP.Business.Fulfillment.Campaign> campaignList = QSP.Business.Fulfillment.Campaign.GetCampaignList(campaignData.Campaign.FiscalYear, campaignData.Campaign.AccountId, campaignData.Campaign.ProgramTypeId);
                if (campaignList.Count == 0)
                {
                    // Check if a campaign exists for the account and program type
                    // List<QSP.Business.Fulfillment.Campaign> campaignList = QSP.Business.Fulfillment.Campaign.GetCampaignList(campaignData.Campaign.FiscalYear, campaignData.Campaign.AccountId, campaignData.Campaign.ProgramTypeId);

                    if (campaignList.Count == 0)
                    {
                        // No existing campaign, so we create one
                        CampaignController.InsertCampaign(campaignData, notifications);
                    }
                    else
                    {
                        // Existing campaign, we update it

                        #region Get existing campaign info

                        QSP.Business.Fulfillment.Domain.CampaignData currentCampaignData = new QSP.Business.Fulfillment.Domain.CampaignData();

                        currentCampaignData.Campaign = campaignList[0];

                        #region Get campaign's billing phone number

                        //List<QSP.Business.Fulfillment.PhoneNumberCampaign> pncList1 = QSP.Business.Fulfillment.PhoneNumberCampaign.GetPhoneNumberCampaignList(campaignData.Campaign.CampaignId, AccountController.PHONE_NUMBER_TYPE_BILLING);

                        //if (pncList1.Count > 0)
                        //{
                        //    if (pncList1[0].PhoneNumberId > 0)
                        //    {
                        //        QSP.Business.Fulfillment.PhoneNumber billingPhoneNumber = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumber(pncList1[0].PhoneNumberId);

                        //        if (billingPhoneNumber != null)
                        //        {
                        //            currentCampaignData.BillingPhoneNumber = billingPhoneNumber.Phone_Number;
                        //        }
                        //    }
                        //}

                        #endregion

                        #region Get account's billing phone number

                        //List<QSP.Business.Fulfillment.PhoneNumberCampaign> pncList2 = QSP.Business.Fulfillment.PhoneNumberCampaign.GetPhoneNumberCampaignList(campaignData.Campaign.CampaignId, AccountController.PHONE_NUMBER_TYPE_SHIPPING);

                        //if (pncList2.Count > 0)
                        //{
                        //    if (pncList2[0].PhoneNumberId > 0)
                        //    {
                        //        QSP.Business.Fulfillment.PhoneNumber shippingPhoneNumber = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumber(pncList2[0].PhoneNumberId);

                        //        if (shippingPhoneNumber != null)
                        //        {
                        //            currentCampaignData.ShippingPhoneNumber = shippingPhoneNumber.Phone_Number;
                        //        }
                        //    }
                        //}

                        #endregion

                        #endregion

                        CampaignController.UpdateCampaign(campaignData, currentCampaignData, notifications);
                    }
                }
                else
                {
                    // Update the campaign

                    #region Get existing campaign info

                    // Check if a campaign exists for the account and program type
                    //    efundraising.EFundraisingCRM.LogSimple.LogInfo("GetCampaignList(campaignData.Campaign.FiscalYear=" + campaignData.Campaign.FiscalYear + ", campaignData.Campaign.AccountId = " + campaignData.Campaign.AccountId + ", campaignData.Campaign.ProgramTypeId = " + campaignData.Campaign.ProgramTypeId);

                    campaignList = QSP.Business.Fulfillment.Campaign.GetCampaignList(campaignData.Campaign.FiscalYear, campaignData.Campaign.AccountId, campaignData.Campaign.ProgramTypeId);

                    QSP.Business.Fulfillment.Domain.CampaignData currentCampaignData = new QSP.Business.Fulfillment.Domain.CampaignData();

                    //  efundraising.EFundraisingCRM.LogSimple.LogInfo("currentcampaign[0] = cid=" + campaignList[0].CampaignId.ToString() + " ,acctid = " + campaignList[0].AccountId.ToString());
                    currentCampaignData.Campaign = campaignList[0];
                    //  efundraising.EFundraisingCRM.LogSimple.LogInfo("currentcampaign result = cid:" + currentCampaignData.Campaign.CampaignId.ToString() + ",acctid:" + currentCampaignData.Campaign.AccountId.ToString());


                    #region Get campaign's billing phone number

                    //List<QSP.Business.Fulfillment.PhoneNumberCampaign> pncList1 = QSP.Business.Fulfillment.PhoneNumberCampaign.GetPhoneNumberCampaignList(campaignData.Campaign.CampaignId, AccountController.PHONE_NUMBER_TYPE_BILLING);

                    //if (pncList1.Count > 0)
                    //{
                    //    if (pncList1[0].PhoneNumberId > 0)
                    //    {
                    //        QSP.Business.Fulfillment.PhoneNumber billingPhoneNumber = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumber(pncList1[0].PhoneNumberId);

                    //        if (billingPhoneNumber != null)
                    //        {
                    //            currentCampaignData.BillingPhoneNumber = billingPhoneNumber.Phone_Number;
                    //        }
                    //    }
                    //}

                    #endregion

                    #region Get account's billing phone number

                    //List<QSP.Business.Fulfillment.PhoneNumberCampaign> pncList2 = QSP.Business.Fulfillment.PhoneNumberCampaign.GetPhoneNumberCampaignList(campaignData.Campaign.CampaignId, AccountController.PHONE_NUMBER_TYPE_SHIPPING);

                    //if (pncList2.Count > 0)
                    //{
                    //    if (pncList2[0].PhoneNumberId > 0)
                    //    {
                    //        QSP.Business.Fulfillment.PhoneNumber shippingPhoneNumber = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumber(pncList2[0].PhoneNumberId);

                    //        if (shippingPhoneNumber != null)
                    //        {
                    //            currentCampaignData.ShippingPhoneNumber = shippingPhoneNumber.Phone_Number;
                    //        }
                    //    }
                    //}

                    #endregion

                    #endregion

                    //SWITCHED CAmopAIGN and CurrentCampaign 
                    CampaignController.UpdateCampaign(campaignData, currentCampaignData, notifications);
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
        /// Validates and saves the campaign in the Order Express system
        /// </summary>
        /// <param name="campaign">The campaign object</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        ///     If the campaign is created successfully, an "Information" notification object will be returned. The dynamic data value will contain the campaign id.
        ///     If the campaign creation fails, an "Error" notification object will be returned. The dynamic data will contain the exception message and the inner exception object if possible.
        /// </param>
        private static void InsertCampaign(QSP.Business.Fulfillment.Domain.CampaignData campaignData, BusinessCommunication.Notifications notifications)
        {
            try
            {
                #region Do data validations

                // Validate campaign data
                QSPForm.Business.Validation.CampaignValidation.ValidateCampaign(campaignData.Campaign, notifications);

                #endregion

                // If successful, save the campaign
                if (notifications.IsSuccessful())
                {
                    #region Try to save the data

                    using (ISession session = BusinessObject.SqlSessionManager.OpenSession())
                    {
                        // Start transaction
                        ITransaction transaction = session.BeginTransaction();

                        try
                        {
                            #region Create billing phone number

                            int billingPhoneNumberId = 0;

                            //List<QSP.Business.Fulfillment.PhoneNumber> billingPhoneNumberList = QSP.Business.Fulfillment.PhoneNumber.FindPhoneNumber(campaignData.BillingPhoneNumber);

                            //if (billingPhoneNumberList.Count > 0)
                            //{
                            //    // We have an existing phone number
                            //    billingPhoneNumberId = billingPhoneNumberList[0].PhoneNumberId;
                            //}
                            //else
                            //{
                            //    // Create new phone number record
                            //    QSP.Business.Fulfillment.PhoneNumber billingPhoneNumber = new QSP.Business.Fulfillment.PhoneNumber();

                            //    //billingPhoneNumber.PhoneNumberId; 
                            //    billingPhoneNumber.Phone_Number = campaignData.BillingPhoneNumber;
                            //    billingPhoneNumber.Deleted = false;
                            //    //billingPhoneNumber.SyncBatchId;
                            //    //billingPhoneNumber.SyncOeOrd;
                            //    billingPhoneNumber.CreateDate = AccountController.DefaultCreateDate;
                            //    billingPhoneNumber.CreateUserId = AccountController.DefaultCreateUserId;
                            //    billingPhoneNumber.UpdateDate = AccountController.DefaultUpdateDate;
                            //    billingPhoneNumber.UpdateUserId = AccountController.DefaultUpdateUserId;

                            //    session.Save(billingPhoneNumber);

                            //    billingPhoneNumberId = billingPhoneNumber.PhoneNumberId;
                            //}

                            #endregion

                            #region Create shipping phone number

                            int shippingPhoneNumberId = 0;

                            //List<QSP.Business.Fulfillment.PhoneNumber> shippingPhoneNumberList = QSP.Business.Fulfillment.PhoneNumber.FindPhoneNumber(campaignData.ShippingPhoneNumber);

                            //if (shippingPhoneNumberList.Count > 0)
                            //{
                            //    // We have an existing phone number
                            //    shippingPhoneNumberId = shippingPhoneNumberList[0].PhoneNumberId;
                            //}
                            //else
                            //{
                            //    // Create new phone number record
                            //    QSP.Business.Fulfillment.PhoneNumber shippingPhoneNumber = new QSP.Business.Fulfillment.PhoneNumber();

                            //    //shippingPhoneNumber.PhoneNumberId; 
                            //    shippingPhoneNumber.Phone_Number = campaignData.ShippingPhoneNumber;
                            //    shippingPhoneNumber.Deleted = false;
                            //    //shippingPhoneNumber.SyncBatchId;
                            //    //shippingPhoneNumber.SyncOeOrd;
                            //    shippingPhoneNumber.CreateDate = AccountController.DefaultCreateDate;
                            //    shippingPhoneNumber.CreateUserId = AccountController.DefaultCreateUserId;
                            //    shippingPhoneNumber.UpdateDate = AccountController.DefaultUpdateDate;
                            //    shippingPhoneNumber.UpdateUserId = AccountController.DefaultUpdateUserId;

                            //    session.Save(shippingPhoneNumber);

                            //    shippingPhoneNumberId = shippingPhoneNumber.PhoneNumberId;
                            //}

                            #endregion

                         /*   campaignData.Campaign.CreateDate = AccountController.DefaultCreateDate;
                            campaignData.Campaign.CreateUserId = AccountController.DefaultCreateUserId;
                            campaignData.Campaign.UpdateDate = AccountController.DefaultUpdateDate;
                            campaignData.Campaign.UpdateUserId = AccountController.DefaultUpdateUserId;
                            */
                            // Save the campaign
                            session.Save(campaignData.Campaign);

                            #region Create campaign's business phone number


                          /*  QSP.Business.Fulfillment.PhoneNumberCampaign pncBilling = new QSP.Business.Fulfillment.PhoneNumberCampaign();

                            //pncBilling.PhoneNumberAccountId;
                            pncBilling.PhoneNumberTypeId = AccountController.PHONE_NUMBER_TYPE_BILLING;
                            pncBilling.PhoneNumberId = billingPhoneNumberId;
                            pncBilling.CampaignId = campaignData.Campaign.CampaignId;
                            pncBilling.Deleted = false;
                            pncBilling.CreateDate = AccountController.DefaultCreateDate;
                            pncBilling.CreateUserId = AccountController.DefaultCreateUserId;
                            pncBilling.UpdateDate = AccountController.DefaultUpdateDate;
                            pncBilling.UpdateUserId = AccountController.DefaultUpdateUserId;

                            session.Save(pncBilling);*/

                            #endregion

                            #region Create campaign's shipping phone number

        /*                    QSP.Business.Fulfillment.PhoneNumberCampaign pncShipping = new QSP.Business.Fulfillment.PhoneNumberCampaign();

                            //pncShipping.PhoneNumberAccountId;
                            pncShipping.PhoneNumberTypeId = AccountController.PHONE_NUMBER_TYPE_SHIPPING;
                            pncShipping.PhoneNumberId = shippingPhoneNumberId;
                            pncShipping.CampaignId = campaignData.Campaign.CampaignId;
                            pncShipping.Deleted = false;
                            pncShipping.CreateDate = AccountController.DefaultCreateDate;
                            pncShipping.CreateUserId = AccountController.DefaultCreateUserId;
                            pncShipping.UpdateDate = AccountController.DefaultUpdateDate;
                            pncShipping.UpdateUserId = AccountController.DefaultUpdateUserId;

                            session.Save(pncShipping);*/

                            #endregion

                            // Commit transaction
                            transaction.Commit();

                            // Create success response
                            List<object> dynamicValues = new List<object>();
                            dynamicValues.Add(campaignData.Campaign.CampaignId);

                            notifications.Add(new BusinessCommunication.Notification("Campaign ID : " + campaignData.Campaign.CampaignId.ToString(), dynamicValues, BusinessCommunication.NotificationType.Information));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="campaign"></param>
        /// <param name="currentCampaign"></param>
        /// <param name="notifications"></param>
        private static void UpdateCampaign(QSP.Business.Fulfillment.Domain.CampaignData campaignData, QSP.Business.Fulfillment.Domain.CampaignData currentCampaignData, BusinessCommunication.Notifications notifications)
        {            
            try
            {
                #region Do data validations

                // Validate campaign data
                QSPForm.Business.Validation.CampaignValidation.ValidateCampaign(campaignData.Campaign, notifications);

                #endregion

                // If successful, save the campaign
                if (notifications.IsSuccessful())
                {
                    #region Try to save the data

                    using (ISession session = BusinessObject.SqlSessionManager.OpenSession())
                    {
                        // Start transaction
                        ITransaction transaction = session.BeginTransaction();

                        try
                        {

                            #region Update campaign

                            // Set values that will not change
                            campaignData.Campaign.AccountId = currentCampaignData.Campaign.AccountId;
                            campaignData.Campaign.ARORBL = currentCampaignData.Campaign.ARORBL;
                            campaignData.Campaign.CampaignId = currentCampaignData.Campaign.CampaignId;
                            //campaignData.Campaign.CampaignName = currentCampaignData.Campaign.CampaignName;
                            //campaignData.Campaign.Comments = currentCampaignData.Campaign.Comments;
                            campaignData.Campaign.CreateDate = currentCampaignData.Campaign.CreateDate;
                            campaignData.Campaign.CreateUserId = currentCampaignData.Campaign.CreateUserId;
                            //campaignData.Campaign.Deleted = currentCampaignData.Campaign.Deleted;
                            campaignData.Campaign.DtsCAccountId = currentCampaignData.Campaign.DtsCAccountId;
                            campaignData.Campaign.DtsCCAInstance = currentCampaignData.Campaign.DtsCCAInstance;
                            //campaignData.Campaign.EndDate = currentCampaignData.Campaign.EndDate;
                            //campaignData.Campaign.Enrollment = currentCampaignData.Campaign.Enrollment;
                            campaignData.Campaign.FiscalYear = currentCampaignData.Campaign.FiscalYear;

                            //campaignData.Campaign.FmId = currentCampaignData.Campaign.FmId;
                           
                            campaignData.Campaign.FormId = currentCampaignData.Campaign.FormId;
                            campaignData.Campaign.FulfCampaignId = currentCampaignData.Campaign.FulfCampaignId;
                            //campaignData.Campaign.GoalEstimatedGross = currentCampaignData.Campaign.GoalEstimatedGross;
                            campaignData.Campaign.ProgramTypeId = currentCampaignData.Campaign.ProgramTypeId;
                            //campaignData.Campaign.StartDate = currentCampaignData.Campaign.StartDate;
                            //campaignData.Campaign.TaxExemptionExpirationDate = currentCampaignData.Campaign.TaxExemptionExpirationDate;
                            //campaignData.Campaign.TaxExemptionNumber = currentCampaignData.Campaign.TaxExemptionNumber;
                            campaignData.Campaign.TradeClassId = currentCampaignData.Campaign.TradeClassId;
                            campaignData.Campaign.UpdateDate = CampaignController.DefaultUpdateDate;
                            campaignData.Campaign.UpdateUserId = CampaignController.DefaultUpdateUserId;
                            campaignData.Campaign.WarehouseId = currentCampaignData.Campaign.WarehouseId;

                            // Save the campaign
                            session.Update(campaignData.Campaign);
                           
                            #endregion

                            #region Update billing phone number

                           /* if (campaignData.BillingPhoneNumber != currentCampaignData.BillingPhoneNumber)
                           {
                                // We have a different billing phone number
                                // Update it

                                #region Create billing phone number

                                int billingPhoneNumberId = 0;

                                List<QSP.Business.Fulfillment.PhoneNumber> billingPhoneNumberList = null;// QSP.Business.Fulfillment.PhoneNumber.FindPhoneNumber(campaignData.BillingPhoneNumber);

                                if (billingPhoneNumberList.Count > 0)
                                {
                                    // We have an existing phone number
                                    billingPhoneNumberId = billingPhoneNumberList[0].PhoneNumberId;
                                }
                                else
                                {
                                    // Create new phone number record
                                    QSP.Business.Fulfillment.PhoneNumber billingPhoneNumber = new QSP.Business.Fulfillment.PhoneNumber();

                                    //billingPhoneNumber.PhoneNumberId; 
                                    billingPhoneNumber.Phone_Number = campaignData.BillingPhoneNumber;
                                    billingPhoneNumber.Deleted = false;
                                    //billingPhoneNumber.SyncBatchId;
                                    //billingPhoneNumber.SyncOeOrd;
                                    billingPhoneNumber.CreateDate = AccountController.DefaultCreateDate;
                                    billingPhoneNumber.CreateUserId = AccountController.DefaultCreateUserId;
                                    billingPhoneNumber.UpdateDate = AccountController.DefaultUpdateDate;
                                    billingPhoneNumber.UpdateUserId = AccountController.DefaultUpdateUserId;

                                    session.Save(billingPhoneNumber);

                                    billingPhoneNumberId = billingPhoneNumber.PhoneNumberId;
                                }

                                #endregion

                                #region Update campaign's billing phone number

                                /*List<QSP.Business.Fulfillment.PhoneNumberCampaign> pncBillingList = QSP.Business.Fulfillment.PhoneNumberCampaign.GetPhoneNumberCampaignList(campaignData.Campaign.CampaignId, CampaignController.PHONE_NUMBER_TYPE_BILLING);

                                if (pncBillingList.Count > 0)
                                {
                                    // Update the record
                                    pncBillingList[0].PhoneNumberId = billingPhoneNumberId;

                                    session.Update(pncBillingList[0]);
                                }
                                else
                                
                                    // Create new record
                                    QSP.Business.Fulfillment.PhoneNumberCampaign pncBilling = new QSP.Business.Fulfillment.PhoneNumberCampaign();

                                    //pncBilling.PhoneNumberAccountId;
                                    pncBilling.PhoneNumberTypeId = AccountController.PHONE_NUMBER_TYPE_BILLING;
                                    pncBilling.PhoneNumberId = billingPhoneNumberId;
                                    pncBilling.CampaignId = campaignData.Campaign.CampaignId;
                                    pncBilling.Deleted = false;
                                    pncBilling.CreateDate = AccountController.DefaultCreateDate;
                                    pncBilling.CreateUserId = AccountController.DefaultCreateUserId;
                                    pncBilling.UpdateDate = AccountController.DefaultUpdateDate;
                                    pncBilling.UpdateUserId = AccountController.DefaultUpdateUserId;

                                    session.Save(pncBilling);
                                //}

                                #endregion
                            }{*/

                            #endregion

                            #region Update shipping phone number

                            if (campaignData.ShippingPhoneNumber != currentCampaignData.ShippingPhoneNumber)
                            {
                                // We have a different shipping phone number
                                // Update it

                                #region Create shipping phone number

                                int billingPhoneNumberId = 0;
                                int shippingPhoneNumberId = 0;
                                billingPhoneNumberId = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumberID(campaignData.BillingPhoneNumber);
                                shippingPhoneNumberId = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumberID(campaignData.ShippingPhoneNumber);


                              /*  List<QSP.Business.Fulfillment.PhoneNumber> shippingPhoneNumberList = QSP.Business.Fulfillment.PhoneNumber.FindPhoneNumber(campaignData.ShippingPhoneNumber);

                                if (shippingPhoneNumberList.Count > 0)
                                {
                                    // We have an existing phone number
                                    shippingPhoneNumberId = shippingPhoneNumberList[0].PhoneNumberId;
                                }
                                else
                                {*/
                                if (shippingPhoneNumberId == 0){
                                    // Create new phone number record
                                    QSP.Business.Fulfillment.PhoneNumber shippingPhoneNumber = new QSP.Business.Fulfillment.PhoneNumber();

                                    //shippingPhoneNumber.PhoneNumberId; 
                                    shippingPhoneNumber.Phone_Number = campaignData.ShippingPhoneNumber;
                                    shippingPhoneNumber.Deleted = false;
                                    //shippingPhoneNumber.SyncBatchId;
                                    //shippingPhoneNumber.SyncOeOrd;
                                    shippingPhoneNumber.CreateDate = AccountController.DefaultCreateDate;
                                    shippingPhoneNumber.CreateUserId = AccountController.DefaultCreateUserId;
                                    shippingPhoneNumber.UpdateDate = AccountController.DefaultUpdateDate;
                                    shippingPhoneNumber.UpdateUserId = AccountController.DefaultUpdateUserId;

                                    session.Save(shippingPhoneNumber);

                                    shippingPhoneNumberId = shippingPhoneNumber.PhoneNumberId;
                                }

                                #endregion

                                #region Update campaign's shipping phone number

                                //MAY CAUSE LOCK!!!
                                List<QSP.Business.Fulfillment.PhoneNumberCampaign> pncShippingList = QSP.Business.Fulfillment.PhoneNumberCampaign.GetPhoneNumberCampaignList(campaignData.Campaign.CampaignId, CampaignController.PHONE_NUMBER_TYPE_SHIPPING);

                                if (pncShippingList.Count > 0)
                                {
                                    // Update the record
                                    pncShippingList[0].PhoneNumberId = shippingPhoneNumberId;

                                    session.Update(pncShippingList[0]);
                                }
                                else
                                {
                                    // Create new record
                                    QSP.Business.Fulfillment.PhoneNumberCampaign pncShipping = new QSP.Business.Fulfillment.PhoneNumberCampaign();

                                    //pncShipping.PhoneNumberAccountId;
                                    pncShipping.PhoneNumberTypeId = AccountController.PHONE_NUMBER_TYPE_SHIPPING;
                                    pncShipping.PhoneNumberId = shippingPhoneNumberId;
                                    pncShipping.CampaignId = campaignData.Campaign.CampaignId;
                                    pncShipping.Deleted = false;
                                    pncShipping.CreateDate = AccountController.DefaultCreateDate;
                                    pncShipping.CreateUserId = AccountController.DefaultCreateUserId;
                                    pncShipping.UpdateDate = AccountController.DefaultUpdateDate;
                                    pncShipping.UpdateUserId = AccountController.DefaultUpdateUserId;

                                    session.Save(pncShipping);
                                }

                                #endregion
                            }

                            #endregion

                            // Commit transaction
                            transaction.Commit();

                            // Create success response
                            List<object> dynamicValues = new List<object>();
                            dynamicValues.Add(campaignData.Campaign.CampaignId);

                            notifications.Add(new BusinessCommunication.Notification("Updated campaign ID : " + campaignData.Campaign.CampaignId.ToString(), dynamicValues, BusinessCommunication.NotificationType.Information));
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

        /// <summary>
        /// Gets the campaign from the Order Express system
        /// </summary>
        /// <param name="campaignId">The Order Express Campaign Id of the data to get</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        ///     If the campaign is retrieved successfully, the notification object will have no contents.
        ///     If the campaign retrieval fails, an "Error" notification object will be returned. The dynamic data will contain the exception message and the inner exception object if possible.
        /// </param>
        /// <returns>The Campaign object retrieved from the Order Express system</returns>
        public static QSP.Business.Fulfillment.Domain.CampaignData GetCampaign(int campaignId, BusinessCommunication.Notifications notifications)
        {
            QSP.Business.Fulfillment.Domain.CampaignData result = new QSP.Business.Fulfillment.Domain.CampaignData();

            try
            {
                using (ISession session = BusinessObject.SqlSessionManager.OpenSession())
                {
                    #region Get Campaign

                    result.Campaign = QSP.Business.Fulfillment.Campaign.GetCampaign(campaignId);

                    #endregion

                    #region Get campaign's billing phone number

                    //List<QSP.Business.Fulfillment.PhoneNumberCampaign> pncList1 = QSP.Business.Fulfillment.PhoneNumberCampaign.GetPhoneNumberCampaignList(campaignId, AccountController.PHONE_NUMBER_TYPE_BILLING);

                    //if (pncList1.Count > 0)
                    //{
                    //    if (pncList1[0].PhoneNumberId > 0)
                    //    {
                    //        QSP.Business.Fulfillment.PhoneNumber billingPhoneNumber = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumber(pncList1[0].PhoneNumberId);

                    //        if (billingPhoneNumber != null)
                    //        {
                    //            result.BillingPhoneNumber = billingPhoneNumber.Phone_Number;
                    //        }
                    //    }
                    //}

                    #endregion

                    #region Get account's billing phone number

                    //List<QSP.Business.Fulfillment.PhoneNumberCampaign> pncList2 = QSP.Business.Fulfillment.PhoneNumberCampaign.GetPhoneNumberCampaignList(campaignId, AccountController.PHONE_NUMBER_TYPE_SHIPPING);

                    //if (pncList2.Count > 0)
                    //{
                    //    if (pncList2[0].PhoneNumberId > 0)
                    //    {
                    //        QSP.Business.Fulfillment.PhoneNumber shippingPhoneNumber = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumber(pncList2[0].PhoneNumberId);

                    //        if (shippingPhoneNumber != null)
                    //        {
                    //            result.ShippingPhoneNumber = shippingPhoneNumber.Phone_Number;
                    //        }
                    //    }
                    //}

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

            return result;
        }

        #endregion



  


    }
}
