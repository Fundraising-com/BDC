using System;
using System.Collections.Generic;
using System.Linq;

using QSPForm.Common;

using LinqContext = QSP.OrderExpress.Business.Context;
using LinqEntity = QSP.OrderExpress.Business.Entity;
using EntityData = QSP.OrderExpress.Common.Data;

using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Enum;

namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using dataDef = QSPForm.Common.DataDef.EmailEntityTable;
	using dataAccessRef = QSPForm.Data.Email_entity;
	
	/// <summary>
	///     This class contains the business rules that are used for an 
	///     Account.
	/// </summary>

	public class EmailAddressSystem : BusinessSystem
    {
        #region V2 code

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MethodResult SaveToOrganization(int organizationId, string email, EmailTypeEnum emailType, int userId)
        {
            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            return this.SaveToOrganization(organizationId, email, emailType, userId, db);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        /// <param name="oedc"></param>
        /// <returns></returns>
        public MethodResult SaveToOrganization(int organizationId, string email, EmailTypeEnum emailType, int userId, LinqContext.OrderExpressDataContext oedc)
        {
            MethodResult result = new MethodResult();

            try
            {
                List<LinqEntity.EmailOrganization> emailOrganizationList =
                    (
                        from eo in oedc.EmailOrganizations
                        where eo.IsDeleted == false
                            && eo.OrganizationId == organizationId
                            && eo.EmailTypeId == (int)emailType
                        select eo
                    ).ToList();

                if (emailOrganizationList.Count == 0)
                {
                    #region Create new email

                    LinqEntity.Email emailRecord = new LinqEntity.Email();

                    emailRecord.EmailAddress = email;
                    emailRecord.RecipientName = ""; 
                    emailRecord.IsDeleted = 0;
                    emailRecord.CreateDate = DateTime.Now;
                    emailRecord.CreateUserId = userId;
                    emailRecord.UpdateDate = null;
                    emailRecord.UpdateUserId = null;

                    oedc.Emails.InsertOnSubmit(emailRecord);
                    oedc.SubmitChanges();

                    result.ResultItems.Add("EmailId", emailRecord.EmailId);


                    LinqEntity.EmailOrganization emailOrganizationRecord = new LinqEntity.EmailOrganization();

                    emailOrganizationRecord.EmailId = emailRecord.EmailId;
                    emailOrganizationRecord.EmailTypeId = (int)emailType;
                    emailOrganizationRecord.OrganizationId = organizationId;
                    emailOrganizationRecord.IsDeleted = false;
                    emailOrganizationRecord.CreateDate = DateTime.Now;
                    emailOrganizationRecord.CreateUserId = userId;
                    emailOrganizationRecord.UpdateDate = null;
                    emailOrganizationRecord.UpdateUserId = null;

                    oedc.EmailOrganizations.InsertOnSubmit(emailOrganizationRecord);
                    oedc.SubmitChanges();

                    result.ResultItems.Add("EmailOrganizationId", emailOrganizationRecord.EmailOrganizationId);

                    #endregion
                }
                else
                {
                    #region Update phone entries

                    foreach (LinqEntity.EmailOrganization emailOrganizationRecord in emailOrganizationList)
                    {
                        emailOrganizationRecord.Email.EmailAddress = email;
                    }

                    oedc.SubmitChanges();

                    result.ResultItems.Add("PhoneNumberId", emailOrganizationList[0].EmailId);
                    result.ResultItems.Add("PhoneNumberOrganizationId", emailOrganizationList[0].EmailOrganizationId);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MethodResultNotification notification = new MethodResultNotification();

                notification.Message = ex.Message;
                notification.NotificationType = MethodResultNotificationTypeEnum.Error;
                notification.DynamicValues = new Dictionary<string, object>();
                notification.DynamicValues.Add("Exception", ex);

                result.ResultNotifications.Add(notification);
            }

            return result;
        }

        #endregion

        #region Refactored code

        public List<LinqEntity.Email> SelectByAccount(int accountId, QSP.OrderExpress.Common.Enum.EmailTypeEnum emailTypeEnum)
        {
            List<LinqEntity.Email> result = new List<LinqEntity.Email>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from ea in context.EmailAccounts
                        where ea.EmailTypeId == (int)emailTypeEnum
                            && ea.AccountId == accountId
                            && ea.IsDeleted == false
                            && ea.Email.IsDeleted == 0
                        select ea;

            List<LinqEntity.EmailAccount> postalAddressAccountList = query.ToList<LinqEntity.EmailAccount>();
            foreach (LinqEntity.EmailAccount postalAddressAccount in postalAddressAccountList)
            {
                result.Add(postalAddressAccount.Email);
            }

            return result;
        }
        public List<LinqEntity.Email> SelectByOrganization(int organizationId, QSP.OrderExpress.Common.Enum.EmailTypeEnum emailTypeEnum)
        {
            List<LinqEntity.Email> result = new List<LinqEntity.Email>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from ea in context.EmailOrganizations
                        where ea.EmailTypeId == (int)emailTypeEnum
                            && ea.OrganizationId == organizationId
                            && ea.IsDeleted == false
                            && ea.Email.IsDeleted == 0
                        select ea;

            List<LinqEntity.EmailOrganization> postalAddressAccountList = query.ToList<LinqEntity.EmailOrganization>();
            foreach (LinqEntity.EmailOrganization postalAddressAccount in postalAddressAccountList)
            {
                result.Add(postalAddressAccount.Email);
            }

            return result;
        }
        public List<LinqEntity.Email> SelectByCampaign(int campaignId, QSP.OrderExpress.Common.Enum.EmailTypeEnum emailTypeEnum)
        {
            List<LinqEntity.Email> result = new List<LinqEntity.Email>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from ea in context.EmailCampaigns
                        where ea.EmailTypeId == (int)emailTypeEnum
                            && ea.CampaignId == campaignId
                            && ea.IsDeleted == false
                            && ea.Email.IsDeleted == 0
                        select ea;

            List<LinqEntity.EmailCampaign> postalAddressAccountList = query.ToList<LinqEntity.EmailCampaign>();
            foreach (LinqEntity.EmailCampaign postalAddressAccount in postalAddressAccountList)
            {
                result.Add(postalAddressAccount.Email);
            }

            return result;
        }
        public List<LinqEntity.Email> SelectByProgramAgreement(int programAgreementId, QSP.OrderExpress.Common.Enum.EmailTypeEnum emailTypeEnum)
        {
            List<LinqEntity.Email> result = new List<LinqEntity.Email>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from ea in context.EmailProgramAgreements
                        where ea.EmailTypeId == (int)emailTypeEnum
                            && ea.ProgramAgreementId == programAgreementId
                            && ea.IsDeleted == false
                            && ea.Email.IsDeleted == 0
                        select ea;

            List<LinqEntity.EmailProgramAgreement> postalAddressAccountList = query.ToList<LinqEntity.EmailProgramAgreement>();
            foreach (LinqEntity.EmailProgramAgreement postalAddressAccount in postalAddressAccountList)
            {
                result.Add(postalAddressAccount.Email);
            }

            return result;
        }
        public List<LinqEntity.Email> SelectByOrder(int orderId, QSP.OrderExpress.Common.Enum.EmailTypeEnum emailTypeEnum)
        {
            List<LinqEntity.Email> result = new List<LinqEntity.Email>();

            return result;
        }

        #endregion

        #region Old code

        public const String FLD_REASSIGNED = "reassigned";
		dataAccessRef objDataAccess;
		
		public EmailAddressSystem()
		{
			objDataAccess = new dataAccessRef();
		}
		public bool Insert(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Insert(Table, objDataAccess);			
		}

		public bool Update(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Update(Table, objDataAccess);			
		}

		public bool UpdateBatch(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class	
			return this.UpdateBatch(Table, objDataAccess);			
		}

		public bool Delete(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Delete(Table, objDataAccess);			
		}

		//----------------------------------------------------------------
		// Function Validate:
		//   Validates Account row
		// Returns:
		//   true if validation is successful 
		//   false if invalid fields exist 
		// Parameters:
		//   [in]  row: DataRow to be validated
		//   [out] row: Returns row data.  If there are fields
		//              that contain errors they are individually marked.  
		//----------------------------------------------------------------
		protected override bool Validate(DataRow row)
		{
			bool isValid = true;
            
			//Clear all errors
			row.ClearErrors();
			
			if ((row.RowState == DataRowState.Modified) || (row.RowState == DataRowState.Added))
			{
				//Apply Mandatory rules
				isValid = IsValid_RequiredFields(row);
				//Apply Maxlength rules
				isValid &= IsValid_FieldsLength(row);	
				//apply any other rules like unicity, integrity ...
				//Not for now
			}
			//Validation only for Delete Operation
			else if (row.RowState == DataRowState.Deleted)
			{
				isValid = IsValid_Integrity(row);
			}						
            
			return isValid;
		}
        
		//----------------------------------------------------------------
		// Function IsValid_FieldLength:
		//   Validates a specific Account Ownership Table field against his maxlength 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  row: DataRow of DataTable to be validated
		//   [in]  fieldName: field in DataTable to be validated
		//   [in]  maxLen: max length for the field
		//----------------------------------------------------------------
		private bool IsValid_FieldsLength(DataRow row)
		{
			bool isValid = false;
			
			//No string variable to test
			isValid = true;
			
            
			return isValid;
		}


		//----------------------------------------------------------------
		// Function IsValid_RequiredField:
		//   Validates a specific DataTable field as Mandatory 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  row: DataRow from DataTable to be validated
		//----------------------------------------------------------------
		private bool IsValid_RequiredFields(DataRow row)
		{
			bool IsValid = true;
			//Email Address
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_EMAIL_ADDRESS, "Email Address");
			//IsValid &= IsValid_RequiredField(row, dataDef.FLD_RECIPIENT_NAME, "Recipient Name");
			
			if (!IsValid)
			{
				messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
			}
			            
			return IsValid;
		}
        

		private bool IsValid_Unicity(DataRow row)
		{
			
			return true;
		
		}

		private bool IsValid_Integrity(DataRow row)
		{
		
			return true;
		
		}

		public dataDef SelectAll(int EntityTypeID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAll(EntityTypeID);				
			
			return dTbl;
			
		}

		public dataDef SelectOne(int EntityTypeID, int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectOne(EntityTypeID, ID);				
			
			return dTbl;
			
		}
        public dataDef SelectOne(int ID)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = objDataAccess.SelectOne(ID);

            return dTbl;

        }

		public dataDef SelectAllByOrganizationID(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectWentity_idLogic(Common.EntityType.TYPE_ORGANIZATION, ID);				
			
			return dTbl;
			
		}
	
		public dataDef SelectAllByAccountID(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectWentity_idLogic(Common.EntityType.TYPE_ACCOUNT, ID);				
			
			return dTbl;
			
		}

		public dataDef SelectAllByCampaignID(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectWentity_idLogic(Common.EntityType.TYPE_CAMPAIGN, ID);				
			
			return dTbl;
			
		}

		public dataDef SelectAllByWarehouseID(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectWentity_idLogic(Common.EntityType.TYPE_WAREHOUSE, ID);				
			
			return dTbl;
			
		}

		public dataDef SelectAllByEntityID(int EntityTypeID, int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectWentity_idLogic(EntityTypeID, ID);				
			
			return dTbl;
			
		}
	
		public void CopyToEntity(dataDef dTblCopyFromEmail, dataDef dTblCopyToEmail, int UserID, 
			int CopyFromEntityType, int CopyFromEntityID, int CopyFromType,
			int CopyToEntityType, int CopyToEntityID, int CopyToType)
		{
			//This method is used to copy specific email info Between Entity

			DataView dvCopyTo = new DataView(dTblCopyToEmail);
			dvCopyTo.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + CopyToEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + CopyToEntityID
				+ " AND " + dataDef.FLD_TYPE + " = " + CopyToType;
			DataView dvCopyFrom = new DataView(dTblCopyFromEmail);
			dvCopyFrom.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + CopyFromEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + CopyFromEntityID
				+ " AND " + dataDef.FLD_TYPE + " = " + CopyFromType;
					
			if (dvCopyFrom.Count >0)
			{
				//Do the same thing for the Phone Number
				if (dvCopyTo.Count == 0)
				{
					//Copy all the list
					DataRow emailRow = dvCopyFrom[0].Row;					
					DataRow newEmailRow = dTblCopyToEmail.NewRow();
					newEmailRow[dataDef.FLD_ENTITY_ID] = CopyToEntityID.ToString();
					newEmailRow[dataDef.FLD_ENTITY_TYPE_ID] = CopyToEntityType.ToString();	
					newEmailRow[dataDef.FLD_EMAIL_ID] = emailRow[dataDef.FLD_EMAIL_ID];	
					newEmailRow[dataDef.FLD_EMAIL_ADDRESS] = emailRow[dataDef.FLD_EMAIL_ADDRESS];	
					newEmailRow[dataDef.FLD_RECIPIENT_NAME] = emailRow[dataDef.FLD_RECIPIENT_NAME];	
					newEmailRow[dataDef.FLD_TYPE] = CopyToType;
					newEmailRow[dataDef.FLD_CREATE_USER_ID] = UserID;
					dTblCopyToEmail.Rows.Add(newEmailRow);
					
				}
				else
				{
					//Copy all the list to an existing record
					CommonSystem comSys = new CommonSystem();
					DataRow copyToRow = dvCopyTo[0].Row;
					DataRow emailRow = dvCopyFrom[0].Row;
					comSys.UpdateRow(copyToRow, dataDef.FLD_ENTITY_ID, CopyToEntityID.ToString());
					comSys.UpdateRow(copyToRow, dataDef.FLD_ENTITY_TYPE_ID, CopyToEntityType.ToString());
					comSys.UpdateRow(copyToRow, dataDef.FLD_EMAIL_ID, emailRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_EMAIL_ADDRESS, emailRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_RECIPIENT_NAME, emailRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_TYPE, CopyToType.ToString());
					if (copyToRow.RowState == DataRowState.Modified)
						copyToRow[dataDef.FLD_UPDATE_USER_ID] = UserID;
				}
			}
		}	

		public void CopyToEntity(dataDef dTblEmail, int UserID, 
			int CopyFromEntityType, int CopyFromEntityID, int CopyFromType,
			int CopyToEntityType, int CopyToEntityID, int CopyToType)
		{
			//This method is used to copy specific email info Between Entity

			CopyToEntity(dTblEmail, dTblEmail, UserID, 
				CopyFromEntityType, CopyFromEntityID, CopyFromType,
				CopyToEntityType, CopyToEntityID, CopyToType);
		}	

		public void CopyToEntity(dataDef dTblCopyFromEmail, dataDef dTblCopyToEmail, int UserID, 
			int CopyFromEntityType, int CopyFromEntityID,
			int CopyToEntityType, int CopyToEntityID)
		{
			//This method is used to copy specific email info Between Entity

			DataView dvCopyTo = new DataView(dTblCopyToEmail);
			dvCopyTo.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + CopyToEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + CopyToEntityID;
			DataView dvCopyFrom = new DataView(dTblCopyFromEmail);
			dvCopyFrom.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + CopyFromEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + CopyFromEntityID;
					
			if (dvCopyFrom.Count >0)
			{
				//Do the same thing for the Phone Number
				foreach(DataRowView emailRow in dvCopyFrom)
				{
					int typeID =  Convert.ToInt32(emailRow[dataDef.FLD_TYPE]);
					CopyToEntity(dTblCopyFromEmail, dTblCopyToEmail, UserID,
						CopyFromEntityType, CopyFromEntityID, typeID,
						CopyToEntityType, CopyToEntityID, typeID);
				}
				
			}
		}	

		public void CopyToEntity(dataDef dTblEmail, int UserID, 
			int CopyFromEntityType, int CopyFromEntityID,
			int CopyToEntityType, int CopyToEntityID)
		{
			//This method is used to copy specific email info Between Entity

			CopyToEntity(dTblEmail, dTblEmail, UserID, 
				CopyFromEntityType, CopyFromEntityID,
				CopyToEntityType, CopyToEntityID);
		}	

		public DataRow FindRow(dataDef dTblEmail, int FindEntityType, 
			int FindEntityID, int FindEmailType)
		{
			DataRow row;
			//This method is used to copy specific address info Between Entity

			DataView dvFind = new DataView(dTblEmail);
			dvFind.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + FindEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + FindEntityID
				+ " AND " + dataDef.FLD_TYPE + " = " + FindEmailType;
					
			if (dvFind.Count >0)
			{
				//reference the row
				row = dvFind[0].Row;
				return row;
			}
			else
				return null;
			
			
		}

		public DataRow[] FindRows(dataDef dTblEmail, int FindEntityType, 
			int FindEntityID, int FindEmailType)
		{
			DataRow[] arrRow;
			//This method is used to copy specific address info Between Entity

			string sFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + FindEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + FindEntityID
				+ " AND " + dataDef.FLD_TYPE + " = " + FindEmailType;

			arrRow = dTblEmail.Select(sFilter);					
			
			return arrRow;
			
		}

        public DataRow[] FindRows(dataDef dTblEmail, int FindEntityType)
        {
            DataRow[] arrRow;
            //This method is used to copy specific address info Between Entity

            string sFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + FindEntityType;

            arrRow = dTblEmail.Select(sFilter);

            return arrRow;

        }

        #endregion
	}
}
