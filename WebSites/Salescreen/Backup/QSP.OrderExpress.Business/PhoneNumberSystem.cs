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
	using dataDef = QSPForm.Common.DataDef.PhoneNumberEntityTable;
	using dataAccessRef = QSPForm.Data.Phone_number_entity;
	
	/// <summary>
	///     This class contains the business rules that are used for an 
	///     Account.
	/// </summary>

	public class PhoneNumberSystem : BusinessSystem
    {
        #region V2 code

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="phoneNumberType"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MethodResult SaveToOrganization(int organizationId, string phoneNumber, PhoneNumberTypeEnum phoneNumberType, int userId)
        {
            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            return this.SaveToOrganization(organizationId, phoneNumber, phoneNumberType, userId, db);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="phoneNumberType"></param>
        /// <param name="userId"></param>
        /// <param name="oedc"></param>
        /// <returns></returns>
        public MethodResult SaveToOrganization(int organizationId, string phoneNumber, PhoneNumberTypeEnum phoneNumberType, int userId, LinqContext.OrderExpressDataContext oedc)
        {
            MethodResult result = new MethodResult();

            try
            {
                List<LinqEntity.PhoneNumberOrganization> phoneNumberOrganizationList =
                    (
                        from pao in oedc.PhoneNumberOrganizations
                        where pao.IsDeleted == false
                            && pao.OrganizationId == organizationId
                            && pao.PhoneNumberTypeId == (int)phoneNumberType
                        select pao
                    ).ToList();

                if (phoneNumberOrganizationList.Count == 0)
                {
                    #region Create new phone number

                    LinqEntity.PhoneNumber phoneNumberRecord = new LinqEntity.PhoneNumber();

                    phoneNumberRecord.Number = phoneNumber;
                    phoneNumberRecord.IsDeleted = false;
                    phoneNumberRecord.CreateDate = DateTime.Now;
                    phoneNumberRecord.CreateUserId = userId;
                    phoneNumberRecord.UpdateDate = null;
                    phoneNumberRecord.UpdateUserId = null;

                    oedc.PhoneNumbers.InsertOnSubmit(phoneNumberRecord);
                    oedc.SubmitChanges();

                    result.ResultItems.Add("PhoneNumberId", phoneNumberRecord.PhoneNumberId);


                    LinqEntity.PhoneNumberOrganization phoneNumberOrganizationRecord = new LinqEntity.PhoneNumberOrganization();

                    phoneNumberOrganizationRecord.OrganizationId = organizationId;
                    phoneNumberOrganizationRecord.PhoneNumberId = phoneNumberRecord.PhoneNumberId;
                    phoneNumberOrganizationRecord.PhoneNumberTypeId = (int)phoneNumberType;
                    phoneNumberOrganizationRecord.IsDeleted = false;
                    phoneNumberOrganizationRecord.CreateDate = DateTime.Now;
                    phoneNumberOrganizationRecord.CreateUserId = userId;
                    phoneNumberOrganizationRecord.UpdateDate = null;
                    phoneNumberOrganizationRecord.UpdateUserId = null;

                    oedc.PhoneNumberOrganizations.InsertOnSubmit(phoneNumberOrganizationRecord);
                    oedc.SubmitChanges();

                    result.ResultItems.Add("PhoneNumberOrganizationId", phoneNumberOrganizationRecord.PhoneNumberOrganizationId);

                    #endregion
                }
                else
                {
                    #region Update phone entries

                    foreach (LinqEntity.PhoneNumberOrganization phoneNumberOrganizationRecord in phoneNumberOrganizationList)
                    {
                        phoneNumberOrganizationRecord.PhoneNumber.Number = phoneNumber;
                    }

                    oedc.SubmitChanges();

                    result.ResultItems.Add("PhoneNumberId", phoneNumberOrganizationList[0].PhoneNumberId);
                    result.ResultItems.Add("PhoneNumberOrganizationId", phoneNumberOrganizationList[0].PhoneNumberOrganizationId);

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

        public List<LinqEntity.PhoneNumber> SelectByAccount(int accountId, PhoneNumberTypeEnum phoneNumberTypeEnum)
        {
            List<LinqEntity.PhoneNumber> result = new List<LinqEntity.PhoneNumber>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from pna in context.PhoneNumberAccounts
                        where pna.PhoneNumberTypeId == (int)phoneNumberTypeEnum
                            && pna.AccountId == accountId
                            && pna.IsDeleted == false
                            && pna.PhoneNumber.IsDeleted == false
                        select pna;

            List<LinqEntity.PhoneNumberAccount> postalAddressAccountList = query.ToList<LinqEntity.PhoneNumberAccount>();
            foreach (LinqEntity.PhoneNumberAccount postalAddressAccount in postalAddressAccountList)
            {
                result.Add(postalAddressAccount.PhoneNumber);
            }

            return result;
        }
        public List<LinqEntity.PhoneNumber> SelectByOrganization(int organizationId, PhoneNumberTypeEnum phoneNumberTypeEnum)
        {
            List<LinqEntity.PhoneNumber> result = new List<LinqEntity.PhoneNumber>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from pna in context.PhoneNumberOrganizations
                        where pna.PhoneNumberTypeId == (int)phoneNumberTypeEnum
                            && pna.OrganizationId == organizationId
                            && pna.IsDeleted == false
                            && pna.PhoneNumber.IsDeleted == false
                        select pna;

            List<LinqEntity.PhoneNumberOrganization> postalAddressAccountList = query.ToList<LinqEntity.PhoneNumberOrganization>();
            foreach (LinqEntity.PhoneNumberOrganization postalAddressAccount in postalAddressAccountList)
            {
                result.Add(postalAddressAccount.PhoneNumber);
            }

            return result;
        }
        public List<LinqEntity.PhoneNumber> SelectByCampaign(int campaignId, PhoneNumberTypeEnum phoneNumberTypeEnum)
        {
            List<LinqEntity.PhoneNumber> result = new List<LinqEntity.PhoneNumber>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from pna in context.PhoneNumberCampaigns
                        where pna.PhoneNumberTypeId == (int)phoneNumberTypeEnum
                            && pna.CampaignId == campaignId
                            && pna.IsDeleted == false
                            && pna.PhoneNumber.IsDeleted == false
                        select pna;

            List<LinqEntity.PhoneNumberCampaign> postalAddressAccountList = query.ToList<LinqEntity.PhoneNumberCampaign>();
            foreach (LinqEntity.PhoneNumberCampaign postalAddressAccount in postalAddressAccountList)
            {
                result.Add(postalAddressAccount.PhoneNumber);
            }

            return result;
        }
        public List<LinqEntity.PhoneNumber> SelectByProgramAgreement(int programAgreementId, PhoneNumberTypeEnum phoneNumberTypeEnum)
        {
            List<LinqEntity.PhoneNumber> result = new List<LinqEntity.PhoneNumber>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from pna in context.PhoneNumberProgramAgreements
                        where pna.PhoneNumberTypeId == (int)phoneNumberTypeEnum
                            && pna.ProgramAgreementId == programAgreementId
                            && pna.IsDeleted == false
                            && pna.PhoneNumber.IsDeleted == false
                        select pna;

            List<LinqEntity.PhoneNumberProgramAgreement> postalAddressAccountList = query.ToList<LinqEntity.PhoneNumberProgramAgreement>();
            foreach (LinqEntity.PhoneNumberProgramAgreement postalAddressAccount in postalAddressAccountList)
            {
                result.Add(postalAddressAccount.PhoneNumber);
            }

            return result;
        }
        public List<LinqEntity.PostalAddress> SelectByOrder(int orderId, PostalAddressTypeEnum postalAddressType)
        {
            List<LinqEntity.PostalAddress> result = new List<LinqEntity.PostalAddress>();

            return result;
        }

        #endregion

        #region Old code

        dataAccessRef objDataAccess;
		
		public PhoneNumberSystem()
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
			//Account
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_PHONE_NUMBER, "Phone Number");
			
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
	
		
		public void CopyToEntity(dataDef dTblCopyFromPhone, dataDef dTblCopyToPhone, int UserID, 
				int CopyFromEntityType, int CopyFromEntityID, int CopyFromType,
				int CopyToEntityType, int CopyToEntityID, int CopyToType)
		{
			
			//This method is used to copy specific phone number info Between Entity

			DataView dvCopyTo = new DataView(dTblCopyToPhone);
			dvCopyTo.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + CopyToEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + CopyToEntityID
				+ " AND " + dataDef.FLD_TYPE + " = " + CopyToType;
			DataView dvCopyFrom = new DataView(dTblCopyFromPhone);
			dvCopyFrom.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + CopyFromEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + CopyFromEntityID
				+ " AND " + dataDef.FLD_TYPE + " = " + CopyFromType;
				
			if (dvCopyFrom.Count >0)
			{
				//Do the same thing for the Phone Number
				if (dvCopyTo.Count == 0)
				{
					//Copy all the list
					DataRow phoneRow = dvCopyFrom[0].Row;					
					DataRow newPhoneRow = dTblCopyToPhone.NewRow();
					newPhoneRow[dataDef.FLD_ENTITY_ID] = CopyToEntityID.ToString();
					newPhoneRow[dataDef.FLD_ENTITY_TYPE_ID] = CopyToEntityType;	
					newPhoneRow[dataDef.FLD_PHONE_NUMBER_ID] = phoneRow[dataDef.FLD_PHONE_NUMBER_ID];
					newPhoneRow[dataDef.FLD_PHONE_NUMBER] = phoneRow[dataDef.FLD_PHONE_NUMBER];								
					newPhoneRow[dataDef.FLD_TYPE] = CopyToType;
					newPhoneRow[dataDef.FLD_CREATE_USER_ID] = UserID;
					dTblCopyToPhone.Rows.Add(newPhoneRow);
				
				}
				else
				{
					//Copy all the list to an existing record
					CommonSystem comSys = new CommonSystem();
					DataRow copyToRow = dvCopyTo[0].Row;
					DataRow phoneRow = dvCopyFrom[0].Row;
					comSys.UpdateRow(copyToRow, dataDef.FLD_ENTITY_ID, CopyToEntityID.ToString());
					comSys.UpdateRow(copyToRow, dataDef.FLD_ENTITY_TYPE_ID, CopyToEntityType.ToString());
					comSys.UpdateRow(copyToRow, dataDef.FLD_PHONE_NUMBER_ID, phoneRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_PHONE_NUMBER, phoneRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_TYPE, CopyToType.ToString());
					if (copyToRow.RowState == DataRowState.Modified)
						copyToRow[dataDef.FLD_UPDATE_USER_ID] = UserID;
				}
			}
		
		}	

		public void CopyToEntity(dataDef dTblPhone, int UserID, 
			int CopyFromEntityType, int CopyFromEntityID, int CopyFromType,
			int CopyToEntityType, int CopyToEntityID, int CopyToType)
		{
			//This method is used to copy specific phone number info Between Entity

			CopyToEntity(dTblPhone, dTblPhone, UserID, 
				CopyFromEntityType, CopyFromEntityID, CopyFromType,
				CopyToEntityType, CopyToEntityID, CopyToType);
			
		}	



		public void CopyToEntity(dataDef dTblCopyFromPhone, dataDef dTblCopyToPhone, int UserID, 
			int CopyFromEntityType, int CopyFromEntityID,
			int CopyToEntityType, int CopyToEntityID)
		{
			//This method is used to copy specific phone number info Between Entity

			DataView dvCopyTo = new DataView(dTblCopyToPhone);
			dvCopyTo.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + CopyToEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + CopyToEntityID;
			DataView dvCopyFrom = new DataView(dTblCopyFromPhone);
			dvCopyFrom.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + CopyFromEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + CopyFromEntityID;
					
			if (dvCopyFrom.Count >0)
			{
				//Do the same thing for the Phone Number
				foreach(DataRowView phoneRow in dvCopyFrom)
				{
					int typeID =  Convert.ToInt32(phoneRow[dataDef.FLD_TYPE]);
					CopyToEntity(dTblCopyFromPhone, dTblCopyToPhone, UserID,CopyFromEntityType,CopyFromEntityID, typeID,
						CopyToEntityType, CopyToEntityID, typeID);
				}
				
			}
			
		}	

		public void CopyToEntity(dataDef dTblPhone, int UserID, 
			int CopyFromEntityType, int CopyFromEntityID,
			int CopyToEntityType, int CopyToEntityID)
		{
			//This method is used to copy specific phone number info Between Entity

			CopyToEntity(dTblPhone, dTblPhone, UserID, 
				CopyFromEntityType, CopyFromEntityID,
				CopyToEntityType, CopyToEntityID);
			
		}	

		public DataRow FindRow(dataDef dTblPhone, int FindEntityType, 
			int FindEntityID, int FindPhoneType)
		{
			DataRow row;
			//This method is used to copy specific address info Between Entity

			DataView dvFind = new DataView(dTblPhone);
			dvFind.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + FindEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + FindEntityID
				+ " AND " + dataDef.FLD_TYPE + " = " + FindPhoneType;
					
			if (dvFind.Count >0)
			{
				//reference the row
				row = dvFind[0].Row;
				return row;
			}
			else
				return null;
			
			
		}

		public DataRow[] FindRows(dataDef dTblPhone, int FindEntityType, 
			int FindEntityID, int FindPhoneType)
		{
			DataRow[] arrRow;
			//This method is used to copy specific address info Between Entity

			string sFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + FindEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + FindEntityID
				+ " AND " + dataDef.FLD_TYPE + " = " + FindPhoneType;

			arrRow = dTblPhone.Select(sFilter);					
			
			return arrRow;
			
		}

        public DataRow[] FindRows(dataDef dTblPhone, int FindEntityType)
        {
            DataRow[] arrRow;
            //This method is used to copy specific address info Between Entity

            string sFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + FindEntityType;

            arrRow = dTblPhone.Select(sFilter);

            return arrRow;

        }

        #endregion
	}
}
