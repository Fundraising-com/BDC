using System;
using System.Collections.Generic;
using System.Text;

using NHibernate;
using NHibernate.Expression;

using BusinessCommunication = QSPForm.Business.Communication;
using BusinessObject = QSP.Business.Fulfillment;
using BusinessValidation = QSPForm.Business.Validation;

namespace QSPForm.Business.Controller
{

    /// <summary>
    /// Handles the business logic regarding orders
    /// </summary>
    public class AccountController
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
        /// Checks if an account exists in order express for the client id and sequence code provided by efr.
        /// If an account exists, the account id is returned.
        /// If no account exists, the information is validated and an account is created. The new account id is returned.
        /// </summary>
        /// <param name="account">The account object</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        ///     If the method succeeded, an "Information" notification object will be returned. The first dynamic data value will contain the account id, the second dynamic data value will contain the organization id.
        ///     If the method failed, an "Error" notification object will be returned. The dynamic data will contain a list of error messages collected.
        /// </param>
        /// <param name="clientId">The client id provided by EFR</param>
        /// <param name="sequenceCode">The sequence code provided by EFR</param>
        public static void SaveAccount(BusinessObject.Domain.AccountData accountData, BusinessCommunication.Notifications notifications, int clientId, string sequenceCode, int programTypeId)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                BusinessCommunication.Notifications getAccountNotifications = new QSPForm.Business.Communication.Notifications();

                if (accountData.Account.AccountId > 0)
                {
                     // Create success response
                     List<object> dynamicValues = new List<object>();
                     dynamicValues.Add(accountData.Account.AccountId);
                     getAccountNotifications.Add(new BusinessCommunication.Notification("Account ID : " +  accountData.Account.AccountId.ToString(), dynamicValues, BusinessCommunication.NotificationType.Information));
    
                }
                else
                {
                    AccountController.SearchAccountFromEfrClient(clientId, sequenceCode, programTypeId, accountData.Account.FmId, getAccountNotifications);
                }
                

                if (getAccountNotifications.IsSuccessful())
                {
                    if (getAccountNotifications.Count > 0)
                    {
                        int accountId = 0;
                        QSPForm.Business.Communication.Notification getAccountNotification = getAccountNotifications[0];

                        if (getAccountNotification.DynamicValues.Count > 0)
                        {
                            #region Success, existing account

                            accountId = Convert.ToInt32(getAccountNotification.DynamicValues[0]);
                            
                            accountData.Account.AccountId = accountId;

                            // Create success response
                            List<object> dynamicValues = new List<object>();
                            dynamicValues.Add(accountId);
                            dynamicValues.Add(accountData.Account.OrganizationId);

                            notifications.Add(new BusinessCommunication.Notification("Account ID : " + accountId.ToString(), dynamicValues, BusinessCommunication.NotificationType.Information));

                            #endregion
                        }
                        else
                        {
                            #region no account id, we need a new account

                            // We have no account id value in our results
                            // We need a new account

                            using (ISession session = BusinessObject.SqlSessionManager.OpenSession())
                            {    
                                
                                int billingPhoneNumberId = 0;
                                int shippingPhoneNumberId = 0;
                                billingPhoneNumberId = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumberID(accountData.BillingPhoneNumber);
                                shippingPhoneNumberId = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumberID(accountData.ShippingPhoneNumber);


                                // Start transaction
                                ITransaction transaction = session.BeginTransaction();

                                try
                                {
                                    #region Create billing phone number
                                              
                                                               
                                    /*List<QSP.Business.Fulfillment.PhoneNumber> billingPhoneNumberList = QSP.Business.Fulfillment.PhoneNumber.FindPhoneNumber(accountData.BillingPhoneNumber);
                                    if (billingPhoneNumberList.Count > 0)
                                    {
                                        // We have an existing phone number
                                        billingPhoneNumberId = billingPhoneNumberList[0].PhoneNumberId;
                                    }
                                    else
                                    {*/

                              
                                   if (billingPhoneNumberId == 0) {
                                        // Create new phone number record
                                        QSP.Business.Fulfillment.PhoneNumber billingPhoneNumber = new QSP.Business.Fulfillment.PhoneNumber();

                                        //billingPhoneNumber.PhoneNumberId; 
                                        billingPhoneNumber.Phone_Number = accountData.BillingPhoneNumber;
                                        billingPhoneNumber.Deleted = false;
                                        //billingPhoneNumber.SyncBatchId;
                                        //billingPhoneNumber.SyncOeOrd;
                                   /*     billingPhoneNumber.CreateDate = AccountController.DefaultCreateDate;
                                        billingPhoneNumber.CreateUserId = AccountController.DefaultCreateUserId;
                                        billingPhoneNumber.UpdateDate = AccountController.DefaultUpdateDate;
                                        billingPhoneNumber.UpdateUserId = AccountController.DefaultUpdateUserId;
*/
                                        session.Save(billingPhoneNumber);

                                        billingPhoneNumberId = billingPhoneNumber.PhoneNumberId;
                                    }

                                    #endregion

                                    #region Create shipping phone number

                          

                                /*    List<QSP.Business.Fulfillment.PhoneNumber> shippingPhoneNumberList = QSP.Business.Fulfillment.PhoneNumber.FindPhoneNumber(accountData.ShippingPhoneNumber);

                                    if (shippingPhoneNumberList.Count > 0)
                                    {
                                        // We have an existing phone number
                                        shippingPhoneNumberId = shippingPhoneNumberList[0].PhoneNumberId;
                                    }
                                    else
                                    {*/ 
                                    
                            
                                   if (shippingPhoneNumberId == 0) {
                               
                                        // Create new phone number record
                                        QSP.Business.Fulfillment.PhoneNumber shippingPhoneNumber = new QSP.Business.Fulfillment.PhoneNumber();

                                        //shippingPhoneNumber.PhoneNumberId; 
                                        shippingPhoneNumber.Phone_Number = accountData.ShippingPhoneNumber;
                                        shippingPhoneNumber.Deleted = false;
                                        //shippingPhoneNumber.SyncBatchId;
                                        //shippingPhoneNumber.SyncOeOrd;
                                   /*     shippingPhoneNumber.CreateDate = AccountController.DefaultCreateDate;
                                        shippingPhoneNumber.CreateUserId = AccountController.DefaultCreateUserId;
                                        shippingPhoneNumber.UpdateDate = AccountController.DefaultUpdateDate;
                                        shippingPhoneNumber.UpdateUserId = AccountController.DefaultUpdateUserId;
*/
                                        session.Save(shippingPhoneNumber);

                                        shippingPhoneNumberId = shippingPhoneNumber.PhoneNumberId;
                                    }

                                    #endregion


                                    #region Create organization record

                                    BusinessObject.Organization organization = new BusinessObject.Organization();
                                    organization.BusinessDivisionId = 4;    // 1 = US, 2 = Canada, 3 = EPI, 4 = EFR
                                    organization.OrganizationTypeId = accountData.OrganizationTypeId;       // 1 = Public, 2 = Catholic, 3 = Christian, 4 = Other, 5 = Campus
                                    organization.OrganizationLevelId = accountData.OrganizationLevelId;     // 1 = Elementary, 2 = Middle, 3 = High, 4 = Other, 5 = Baseball, 6 = AYSO, 7 = Moose 
                                    organization.FmId = accountData.Account.FmId;
                                    organization.OrganizationName = accountData.Account.AccountName;
                                    organization.Comments = accountData.Account.Comments;
                                    organization.Deleted = false;
                              /*      organization.CreateDate = AccountController.DefaultCreateDate;
                                    organization.CreateUserId = AccountController.DefaultCreateUserId;
                                    organization.UpdateDate = AccountController.DefaultUpdateDate;
                                    organization.UpdateUserId = AccountController.DefaultUpdateUserId;*/
                                    //organization.OrganizationId;
                                    //organization.OrganizationStatusId;
                                    //organization.ARNMBL;
                                    //organization.MDRPID; 
                                    //organization.DtsCAccountID;
                                    //organization.DtsFlagpoleInstance;                                        
                                    //organization.TaxExemptionExpirationDate;
                                    //organization.TaxExemptionNumber;

                                    // Save the organization
                                    session.Save(organization);

                                    #endregion

                                    #region Create organization billing phone number

                                    QSP.Business.Fulfillment.PhoneNumberOrganization pnoBilling = new QSP.Business.Fulfillment.PhoneNumberOrganization();

                                    //pnoBilling.PhoneNumberOrganizationId;
                                    pnoBilling.PhoneNumberTypeId = AccountController.PHONE_NUMBER_TYPE_BILLING; 
                                    pnoBilling.PhoneNumberId = billingPhoneNumberId;
                                    pnoBilling.OrganizationId = organization.OrganizationId; 
                                    pnoBilling.Deleted = false;
                                    pnoBilling.CreateDate = AccountController.DefaultCreateDate;
                                    pnoBilling.CreateUserId = AccountController.DefaultCreateUserId;
                                    pnoBilling.UpdateDate = AccountController.DefaultUpdateDate;
                                    pnoBilling.UpdateUserId = AccountController.DefaultUpdateUserId;

                                    session.Save(pnoBilling);

                                    #endregion

                                    #region Create organization shipping phone number

                                    QSP.Business.Fulfillment.PhoneNumberOrganization pnoShipping = new QSP.Business.Fulfillment.PhoneNumberOrganization();

                                    //pnoShipping.PhoneNumberOrganizationId;
                                    pnoShipping.PhoneNumberTypeId = AccountController.PHONE_NUMBER_TYPE_SHIPPING;
                                    pnoShipping.PhoneNumberId = shippingPhoneNumberId;
                                    pnoShipping.OrganizationId = organization.OrganizationId;
                                    pnoShipping.Deleted = false;
                                    pnoShipping.CreateDate = AccountController.DefaultCreateDate;
                                    pnoShipping.CreateUserId = AccountController.DefaultCreateUserId;
                                    pnoShipping.UpdateDate = AccountController.DefaultUpdateDate;
                                    pnoShipping.UpdateUserId = AccountController.DefaultUpdateUserId;

                                    session.Save(pnoShipping);

                                    #endregion


                                    #region Assign org id to account record

                                    accountData.Account.OrganizationId = organization.OrganizationId;

                                    #endregion

                                    #region Do data validations

                                    // Validate account data
                                    BusinessCommunication.Notifications newAccountNotifications = new QSPForm.Business.Communication.Notifications();
                                    QSPForm.Business.Validation.AccountValidation.ValidateAccount(accountData.Account, newAccountNotifications);

                                    #endregion

                                    if (newAccountNotifications.IsSuccessful())
                                    {
                                 /*       accountData.Account.CreateDate = AccountController.DefaultCreateDate;
                                        accountData.Account.CreateUserId = AccountController.DefaultCreateUserId;
                                        accountData.Account.UpdateDate = AccountController.DefaultUpdateDate;
                                        accountData.Account.UpdateUserId = AccountController.DefaultUpdateUserId;
*/
                                        session.Save(accountData.Account);

                                        #region Create business phone number

                                        QSP.Business.Fulfillment.PhoneNumberAccount pnaBilling = new QSP.Business.Fulfillment.PhoneNumberAccount();

                                        //pnaBilling.PhoneNumberAccountId;
                                        pnaBilling.PhoneNumberTypeId = AccountController.PHONE_NUMBER_TYPE_BILLING;
                                        pnaBilling.PhoneNumberId = billingPhoneNumberId;
                                        pnaBilling.AccountId = accountData.Account.AccountId;
                                        pnaBilling.Deleted = false;
                                        pnaBilling.CreateDate = AccountController.DefaultCreateDate;
                                        pnaBilling.CreateUserId = AccountController.DefaultCreateUserId;
                                        pnaBilling.UpdateDate = AccountController.DefaultUpdateDate;
                                        pnaBilling.UpdateUserId = AccountController.DefaultUpdateUserId;

                                        session.Save(pnaBilling);

                                        #endregion

                                        #region Create shipping phone number

                                        QSP.Business.Fulfillment.PhoneNumberAccount pnaShipping = new QSP.Business.Fulfillment.PhoneNumberAccount();

                                        //pnaShipping.PhoneNumberAccountId;
                                        pnaShipping.PhoneNumberTypeId = AccountController.PHONE_NUMBER_TYPE_SHIPPING;
                                        pnaShipping.PhoneNumberId = shippingPhoneNumberId;
                                        pnaShipping.AccountId = accountData.Account.AccountId;
                                        pnaShipping.Deleted = false;
                                        pnaShipping.CreateDate = AccountController.DefaultCreateDate;
                                        pnaShipping.CreateUserId = AccountController.DefaultCreateUserId;
                                        pnaShipping.UpdateDate = AccountController.DefaultUpdateDate;
                                        pnaShipping.UpdateUserId = AccountController.DefaultUpdateUserId;

                                        session.Save(pnaShipping);

                                        #endregion

                                        #region Create client to account record

                                        BusinessObject.ClientAccount clientAccount = new BusinessObject.ClientAccount();
                                        clientAccount.AccountId = accountData.Account.AccountId;
                                        clientAccount.ClientId = clientId;
                                        clientAccount.SequenceCode = sequenceCode;
                                        clientAccount.ProgramTypeId = programTypeId;

                                        // Save client account record
                                        session.Save(clientAccount);

                                        #endregion

                                        // Commit transaction
                                        transaction.Commit();

                                        // Create success response
                                        List<object> dynamicValues = new List<object>();
                                        dynamicValues.Add(accountData.Account.AccountId);
                                        dynamicValues.Add(organization.OrganizationId);

                                        notifications.Add(new BusinessCommunication.Notification("Account ID : " + accountData.Account.AccountId.ToString(), dynamicValues, BusinessCommunication.NotificationType.Information));
                                    }
                                    else
                                    {
                                        // Rollback transaction
                                        transaction.Rollback();

                                        #region Validation failed

                                        // New account counld not be created
                                        // We cannot proceed
                                        // Copy internal notifications to the notification parameter

                                        List<object> dynamicValues = new List<object>();
                                        notifications.Add(new BusinessCommunication.Notification("Validation of new account failed, could not create new account", dynamicValues, BusinessCommunication.NotificationType.Error));

                                        notifications.InsertNotifications(newAccountNotifications);

                                        #endregion
                                    }
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
                    else
                    {
                        #region Failure

                        // No notifications, we have no errors, or account ids
                        // We cannot proceed
                        // Copy internal notificatoin to the notification parameter

                        List<object> dynamicValues = new List<object>();
                        notifications.Add(new BusinessCommunication.Notification("Could not get any notification", dynamicValues, BusinessCommunication.NotificationType.Error));

                        notifications.InsertNotifications(getAccountNotifications);

                        #endregion
                    }
                }
                else
                {
                    #region Failure

                    // Could not get an account
                    // We cannot proceed
                    // copy internal notification to the notification parameter

                    List<object> dynamicValues = new List<object>();
                    notifications.Add(new BusinessCommunication.Notification("Errors checking accounts in order express", dynamicValues, BusinessCommunication.NotificationType.Error));

                    notifications.InsertNotifications(getAccountNotifications);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                #region Exception handling

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

        /// <summary>
        /// Searches for an existing account in order express given the client id and sequence code from EFR.
        /// </summary>
        /// <param name="clientId">The client id provided by EFR</param>
        /// <param name="sequenceCode">The sequence code provided by EFR</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        ///     If the method succeeded, an "Information" notification object will be returned. The first dynamic data value will contain the account id.
        ///     If the method failed, an "Error" notification object will be returned. The dynamic data will contain a list of error messages collected.
        /// </param>
        public static void SearchAccountFromEfrClient(int clientId, string sequenceCode, int programTypeId, string fmId, BusinessCommunication.Notifications notifications)
        {
            try
            {
                List<BusinessObject.ClientAccount> clientAccountList = BusinessObject.ClientAccount.GetClientAccountListByClientIdSequenceCodeAndProgramType(clientId, sequenceCode, programTypeId);

                if (clientAccountList != null)
                {
                    if (clientAccountList.Count > 0)
                    {
                        // Check for fm_id
                        int accountId = 0;
                        foreach (BusinessObject.ClientAccount item in clientAccountList)
                        {
                            BusinessObject.Account itemAccount = BusinessObject.Account.GetAccount(item.AccountId);

                            if (itemAccount != null)
                            {
                                if (itemAccount.FmId == fmId)
                                {
                                    accountId = itemAccount.AccountId;
                                    break;
                                }
                            }
                        }

                        if (accountId > 0)
                        {
                            // Create success response
                            List<object> dynamicValues = new List<object>();
                            dynamicValues.Add(accountId);

                            notifications.Add(new BusinessCommunication.Notification("Account ID : " + accountId.ToString(), dynamicValues, BusinessCommunication.NotificationType.Information));
                        }
                        else
                        {
                            // Create failure response
                            List<object> dynamicValues = new List<object>();
                            notifications.Add(new BusinessCommunication.Notification("No records found with client id = '" + clientId.ToString() + "' and sequence code = '" + sequenceCode + "'", dynamicValues, BusinessCommunication.NotificationType.Information));
                        }
                    }
                    else
                    {
                        // Create failure response
                        List<object> dynamicValues = new List<object>();
                        notifications.Add(new BusinessCommunication.Notification("No records found with client id = '" + clientId.ToString() + "' and sequence code = '" + sequenceCode + "'", dynamicValues, BusinessCommunication.NotificationType.Information));
                    }
                }
                else
                {
                    // Create failure response
                    List<object> dynamicValues = new List<object>();
                    notifications.Add(new BusinessCommunication.Notification("No records found with client id = '" + clientId.ToString() + "' and sequence code = '" + sequenceCode + "'", dynamicValues, BusinessCommunication.NotificationType.Information));
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
        /// Gets the account from the Order Express system
        /// </summary>
        /// <param name="accountId">The Order Express Account Id of the data to get</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        ///     If the account is retrieved successfully, the notification object will have no contents.
        ///     If the account retrieval fails, an "Error" notification object will be returned. The dynamic data will contain the exception message and the inner exception object if possible.
        /// </param>
        /// <returns>The Account object retrieved from the Order Express system</returns>
        public static QSP.Business.Fulfillment.Domain.AccountData GetAccount(int accountId, BusinessCommunication.Notifications notifications)
        {
            QSP.Business.Fulfillment.Domain.AccountData result = new QSP.Business.Fulfillment.Domain.AccountData();

            try
            {
                using (ISession session = BusinessObject.SqlSessionManager.OpenSession())
                {
                    #region Get account

                    result.Account = QSP.Business.Fulfillment.Account.GetAccount(accountId);
                    
                    #endregion

                    #region Get account's billing phone number

                    //List<QSP.Business.Fulfillment.PhoneNumberAccount> pnaList1 = QSP.Business.Fulfillment.PhoneNumberAccount.GetPhoneNumberAccountList(accountId, AccountController.PHONE_NUMBER_TYPE_BILLING);

                    //if (pnaList1.Count > 0)
                    //{
                    //    if (pnaList1[0].PhoneNumberId > 0)
                    //    {
                    //        QSP.Business.Fulfillment.PhoneNumber billingPhoneNumber = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumber(pnaList1[0].PhoneNumberId);

                    //        if (billingPhoneNumber != null)
                    //        {
                    //            result.BillingPhoneNumber = billingPhoneNumber.Phone_Number;
                    //        }
                    //    }
                    //}

                    #endregion

                    #region Get account's billing phone number

                    //List<QSP.Business.Fulfillment.PhoneNumberAccount> pnaList2 = QSP.Business.Fulfillment.PhoneNumberAccount.GetPhoneNumberAccountList(accountId, AccountController.PHONE_NUMBER_TYPE_SHIPPING);

                    //if (pnaList2.Count > 0)
                    //{
                    //    if (pnaList2[0].PhoneNumberId > 0)
                    //    {
                    //        QSP.Business.Fulfillment.PhoneNumber shippingPhoneNumber = QSP.Business.Fulfillment.PhoneNumber.GetPhoneNumber(pnaList2[0].PhoneNumberId);

                    //        if (shippingPhoneNumber != null)
                    //        {
                    //            result.ShippingPhoneNumber = shippingPhoneNumber.Phone_Number;
                    //        }
                    //    }
                    //}

                    #endregion

                    #region Get account's organization info

                    if (result.Account.OrganizationId > 0)
                    {
                        QSP.Business.Fulfillment.Organization org = QSP.Business.Fulfillment.Organization.GetOrganization(result.Account.OrganizationId);

                        result.OrganizationLevelId = org.OrganizationLevelId ?? 0;
                        result.OrganizationTypeId = org.OrganizationTypeId;
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

            return result;
        }

        #endregion

        #region Old code

        /// <summary>
        /// Validates and creates the account in the Order Express system
        /// </summary>
        /// <param name="account">The account object</param>
        /// <param name="notifications">
        ///     Notification object where the results of the method are returned.
        ///     The IsSuccessful property determines if the operation was a success or not.
        ///     If the method succeeded, an "Information" notification object will be returned. The first dynamic data value will contain the account id.
        ///     If the method failed, an "Error" notification object will be returned. The dynamic data will contain a list of error messages collected.
        /// </param>
        public static void SaveAccount_DEPRECATED(BusinessObject.Account account, BusinessCommunication.Notifications notifications)
        {
            try
            {
                #region Do data validations

                // Validate account data
                QSPForm.Business.Validation.AccountValidation.ValidateAccount(account, notifications);

                #endregion

                // If successful, save the account
                if (notifications.IsSuccessful())
                {
                    #region Try to save the data

                    using (ISession session = BusinessObject.SqlSessionManager.OpenSession())
                    {
                        // Start transaction
                        ITransaction transaction = session.BeginTransaction();

                        try
                        {
                            // Save the account
                            session.Save(account);

                            // Commit transaction
                            transaction.Commit();

                            // Create success response
                            List<object> dynamicValues = new List<object>();
                            dynamicValues.Add(account.AccountId);

                            notifications.Add(new BusinessCommunication.Notification("Account ID : " + account.AccountId.ToString(), dynamicValues, BusinessCommunication.NotificationType.Information));
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

        #endregion

    }
}
