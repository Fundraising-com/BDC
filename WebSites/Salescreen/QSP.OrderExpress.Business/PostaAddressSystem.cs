using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

using QSPForm.Common;
using QSPForm.Common.DataDef;

using dataDef = QSPForm.Common.DataDef.PostalAddressEntityTable;
using dataAccessRef = QSPForm.Data.Postal_address_entity;

using LinqContext = QSP.OrderExpress.Business.Context;
using LinqEntity = QSP.OrderExpress.Business.Entity;
using EntityData = QSP.OrderExpress.Common.Data;

using QSP.OrderExpress.Common.Comunication;
using QSP.OrderExpress.Common.Enum;

namespace QSPForm.Business
{
	public class PostalAddressSystem : BusinessSystem
    {
        #region V2 code

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="addressData"></param>
        /// <param name="addressType"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MethodResult SaveToOrganization(int organizationId, EntityData.AddressData addressData, PostalAddressTypeEnum addressType, int userId)
        {
            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            return this.SaveToOrganization(organizationId, addressData, addressType, userId, db);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="addressData"></param>
        /// <param name="addressType"></param>
        /// <param name="userId"></param>
        /// <param name="oedc"></param>
        /// <returns></returns>
        public MethodResult SaveToOrganization(int organizationId, EntityData.AddressData addressData, PostalAddressTypeEnum addressType, int userId, LinqContext.OrderExpressDataContext oedc)
        {
            MethodResult result = new MethodResult();

            try
            {
                List<LinqEntity.PostalAddressOrganization> postalAddressOrganizationList =
                    (
                        from pao in oedc.PostalAddressOrganizations
                        where pao.IsDeleted == false
                            && pao.OrganizationId == organizationId
                            && pao.PostalAddressTypeId == (int)addressType
                        select pao
                    ).ToList();

                if (postalAddressOrganizationList.Count == 0)
                {
                    #region Create new address

                    LinqEntity.PostalAddress postalAddressRecord = new LinqEntity.PostalAddress();

                    postalAddressRecord.Name = addressData.Name;
                    postalAddressRecord.FirstName = addressData.FirstName;
                    postalAddressRecord.LastName = addressData.LastName;
                    postalAddressRecord.Address1 = addressData.Address1;
                    postalAddressRecord.Address2 = addressData.Address1;
                    postalAddressRecord.City = addressData.City;
                    postalAddressRecord.County = addressData.County;
                    postalAddressRecord.SubdivisionCode = addressData.Subdivision.Code;
                    postalAddressRecord.Zip = addressData.Zip;
                    postalAddressRecord.Zip4 = addressData.Zip4;
                    postalAddressRecord.IsResidentialArea = addressData.IsResidentialArea;
                    postalAddressRecord.IsDeleted = 0;
                    postalAddressRecord.CreateDate = DateTime.Now;
                    postalAddressRecord.CreateUserId = userId;
                    postalAddressRecord.UpdateDate = null;
                    postalAddressRecord.UpdateUserId = null;

                    oedc.PostalAddresses.InsertOnSubmit(postalAddressRecord);
                    oedc.SubmitChanges();
                    
                    result.ResultItems.Add("PostalAddressId", postalAddressRecord.PostalAddressId);


                    LinqEntity.PostalAddressOrganization postalAddressOrganizationRecord = new LinqEntity.PostalAddressOrganization();

                    postalAddressOrganizationRecord.OrganizationId = organizationId;
                    postalAddressOrganizationRecord.PostalAddressId = postalAddressRecord.PostalAddressId;
                    postalAddressOrganizationRecord.PostalAddressTypeId = (int)addressType;
                    postalAddressOrganizationRecord.IsDeleted = false;
                    postalAddressOrganizationRecord.CreateDate = DateTime.Now;
                    postalAddressOrganizationRecord.CreateUserId = userId;
                    postalAddressOrganizationRecord.UpdateDate = null;
                    postalAddressOrganizationRecord.UpdateUserId = null;

                    oedc.PostalAddressOrganizations.InsertOnSubmit(postalAddressOrganizationRecord);
                    oedc.SubmitChanges();

                    result.ResultItems.Add("PostalAddressOrganizationId", postalAddressOrganizationRecord.PostalAddressOrganizationId);

                    #endregion
                }
                else
                {
                    #region Update phone entries

                    foreach (LinqEntity.PostalAddressOrganization postalAddressOrganizationRecord in postalAddressOrganizationList)
                    {
                        postalAddressOrganizationRecord.PostalAddress.Address1 = addressData.Address1;
                        postalAddressOrganizationRecord.PostalAddress.Address2 = addressData.Address2;
                        postalAddressOrganizationRecord.PostalAddress.City = addressData.City;
                        postalAddressOrganizationRecord.PostalAddress.County = addressData.County;
                        postalAddressOrganizationRecord.PostalAddress.FirstName = addressData.FirstName;
                        postalAddressOrganizationRecord.PostalAddress.IsResidentialArea = addressData.IsResidentialArea;
                        postalAddressOrganizationRecord.PostalAddress.LastName = addressData.LastName;
                        postalAddressOrganizationRecord.PostalAddress.Name = addressData.Name;
                        postalAddressOrganizationRecord.PostalAddress.SubdivisionCode = addressData.Subdivision.Code;
                        postalAddressOrganizationRecord.PostalAddress.UpdateDate = DateTime.Now;
                        postalAddressOrganizationRecord.PostalAddress.UpdateUserId = userId;
                        postalAddressOrganizationRecord.PostalAddress.Zip = addressData.Zip;
                        postalAddressOrganizationRecord.PostalAddress.Zip4 = addressData.Zip4;
                    }

                    oedc.SubmitChanges();

                    result.ResultItems.Add("PhoneNumberId", postalAddressOrganizationList[0].PostalAddressId);
                    result.ResultItems.Add("PhoneNumberOrganizationId", postalAddressOrganizationList[0].PostalAddressOrganizationId);

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

        public List<LinqEntity.PostalAddress> GetAddressByAccount(int accountId, PostalAddressTypeEnum postalAddressType)
        {
            List<LinqEntity.PostalAddress> result = new List<LinqEntity.PostalAddress>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from paa in context.PostalAddressAccounts
                        where paa.PostalAddressTypeId == (int)postalAddressType
                            && paa.AccountId == accountId
                            && paa.IsDeleted == false
                            && paa.PostalAddress.IsDeleted == 0
                        select paa;

            List<LinqEntity.PostalAddressAccount> postalAddressAccountList = query.ToList<LinqEntity.PostalAddressAccount>();
            foreach (LinqEntity.PostalAddressAccount postalAddressAccount in postalAddressAccountList)
            {
                result.Add(postalAddressAccount.PostalAddress);
            }

            return result;
        }
        public List<LinqEntity.PostalAddress> GetAddressByOrganization(int organizationId, PostalAddressTypeEnum postalAddressType)
        {
            List<LinqEntity.PostalAddress> result = new List<LinqEntity.PostalAddress>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from pao in context.PostalAddressOrganizations
                        where pao.PostalAddressTypeId == (int)postalAddressType
                            && pao.OrganizationId == organizationId
                            && pao.IsDeleted == false
                            && pao.PostalAddress.IsDeleted == 0
                        select pao;

            List<LinqEntity.PostalAddressOrganization> postalAddressAccountList = query.ToList<LinqEntity.PostalAddressOrganization>();
            foreach (LinqEntity.PostalAddressOrganization postalAddressAccount in postalAddressAccountList)
            {
                result.Add(postalAddressAccount.PostalAddress);
            }

            return result;
        }
        public List<LinqEntity.PostalAddress> GetAddressByCampaign(int campaignId, PostalAddressTypeEnum postalAddressType)
        {
            List<LinqEntity.PostalAddress> result = new List<LinqEntity.PostalAddress>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from pac in context.PostalAddressCampaigns
                        where pac.PostalAddressTypeId == (int)postalAddressType
                            && pac.CampaignId == campaignId
                            && pac.IsDeleted == false
                            && pac.PostalAddress.IsDeleted == 0
                        select pac;

            List<LinqEntity.PostalAddressCampaign> postalAddressAccountList = query.ToList<LinqEntity.PostalAddressCampaign>();
            foreach (LinqEntity.PostalAddressCampaign postalAddressAccount in postalAddressAccountList)
            {
                result.Add(postalAddressAccount.PostalAddress);
            }

            return result;
        }
        public List<LinqEntity.PostalAddress> GetAddressByProgramAgreement(int programAgreementId, PostalAddressTypeEnum postalAddressType)
        {
            List<LinqEntity.PostalAddress> result = new List<LinqEntity.PostalAddress>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from papa in context.PostalAddressProgramAgreements
                        where papa.PostalAddressTypeId == (int)postalAddressType
                            && papa.ProgramAgreementId == programAgreementId
                            && papa.IsDeleted == false
                            && papa.PostalAddress.IsDeleted == 0
                        select papa;

            List<LinqEntity.PostalAddressProgramAgreement> postalAddressAccountList = query.ToList<LinqEntity.PostalAddressProgramAgreement>();
            foreach (LinqEntity.PostalAddressProgramAgreement postalAddressAccount in postalAddressAccountList)
            {
                result.Add(postalAddressAccount.PostalAddress);
            }

            return result;
        }
        public List<LinqEntity.PostalAddress> GetAddressByOrder(int orderId, PostalAddressTypeEnum postalAddressType)
        {
            List<LinqEntity.PostalAddress> result = new List<LinqEntity.PostalAddress>();

            return result;
        }

        #endregion

        #region Old code

        dataAccessRef addrDataAccess;
		public const String FLD_REASSIGNED = "reassigned";
		
		public PostalAddressSystem()
		{
			addrDataAccess = new dataAccessRef();
		}
		public bool Insert(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Insert(Table, addrDataAccess);			
		}

		public bool Update(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Update(Table, addrDataAccess);			
		}

		public bool UpdateBatch(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.UpdateBatch(Table, addrDataAccess);			
		}

		public bool Delete(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Delete(Table, addrDataAccess);			
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
			//Contact First Name
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_FIRST_NAME, "Contact First Name");
			//Contact First Name
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_LAST_NAME, "Contact Last Name");
			//Address Line 1
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_ADDRESS1, "Address");
			//City
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_CITY, "City");
			//Zip
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_ZIP, "Zip");
			
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
			dTbl = addrDataAccess.SelectAll(EntityTypeID);				
			
			return dTbl;
			
		}

		public dataDef SelectOne(int EntityTypeID, int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = addrDataAccess.SelectOne(EntityTypeID, ID);				
			
			return dTbl;
			
		}
        public dataDef SelectOne(int ID)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            dTbl = addrDataAccess.SelectOne(ID);

            return dTbl;

        }

		public dataDef SelectAllByOrganizationID(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = addrDataAccess.SelectWentity_idLogic(Common.EntityType.TYPE_ORGANIZATION, ID);				
			
			return dTbl;
			
		}
	
		public dataDef SelectAllByAccountID(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = addrDataAccess.SelectWentity_idLogic(Common.EntityType.TYPE_ACCOUNT, ID);				
			
			return dTbl;
			
		}

		public dataDef SelectAllByCampaignID(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = addrDataAccess.SelectWentity_idLogic(Common.EntityType.TYPE_CAMPAIGN, ID);				
			
			return dTbl;
			
		}

		public dataDef SelectAllBillingAddressByOrderID(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = addrDataAccess.SelectWentity_idLogic(Common.EntityType.TYPE_ORDER_BILLING, ID);				
			
			return dTbl;
			
		}

		
		public dataDef SelectAllShippingAddressByOrderID(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = addrDataAccess.SelectWentity_idLogic(Common.EntityType.TYPE_ORDER_SHIPPING, ID);				
			
			return dTbl;
			
		}

		public dataDef SelectAllByWarehouseID(int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = addrDataAccess.SelectWentity_idLogic(Common.EntityType.TYPE_WAREHOUSE, ID);				
			
			return dTbl;
			
		}


		public dataDef SelectAllByEntityID(int EntityTypeID, int ID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = addrDataAccess.SelectWentity_idLogic(EntityTypeID, ID);				
			
			return dTbl;
			
		}
	
		public void CopyToEntity(dataDef dTblAddress, int UserID, 
								int CopyFromEntityType, int CopyFromEntityID, int CopyFromPostalAddressType,
								int CopyToEntityType, int CopyToEntityID, int CopyToPostalAddressType)
		{
			//This method is used to copy specific address info Between Entity
			CopyToEntity(dTblAddress, dTblAddress, UserID, 
						CopyFromEntityType, CopyFromEntityID, CopyFromPostalAddressType,
						CopyToEntityType, CopyToEntityID, CopyToPostalAddressType);
			
		}	

		public void CopyToEntity(dataDef dTblCopyFromAddress, dataDef dTblCopyToAddress, int UserID, 
			int CopyFromEntityType, int CopyFromEntityID, int CopyFromPostalAddressType,
			int CopyToEntityType, int CopyToEntityID, int CopyToPostalAddressType)
		{
			//This method is used to copy specific address info Between Entity

			DataView dvCopyTo = new DataView(dTblCopyToAddress);
			dvCopyTo.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + CopyToEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + CopyToEntityID
				+ " AND " + dataDef.FLD_TYPE + " = " + CopyToPostalAddressType;
			DataView dvCopyFrom = new DataView(dTblCopyFromAddress);
			dvCopyFrom.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + CopyFromEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + CopyFromEntityID
				+ " AND " + dataDef.FLD_TYPE + " = " + CopyFromPostalAddressType;
					
			if (dvCopyFrom.Count >0)
			{
				if (dvCopyTo.Count == 0)
				{
					//Copy all the list to a new record
					DataRow newRow = dTblCopyToAddress.NewRow();
					DataRow addrRow = dvCopyFrom[0].Row;
					newRow[dataDef.FLD_ENTITY_ID] = CopyToEntityID;
					newRow[dataDef.FLD_ENTITY_TYPE_ID] = CopyToEntityType;		
					newRow[dataDef.FLD_ADDRESS_ID] = addrRow[dataDef.FLD_ADDRESS_ID];
					newRow[dataDef.FLD_NAME] = addrRow[dataDef.FLD_NAME];
					newRow[dataDef.FLD_FIRST_NAME] = addrRow[dataDef.FLD_FIRST_NAME];
					newRow[dataDef.FLD_LAST_NAME] = addrRow[dataDef.FLD_LAST_NAME];
					newRow[dataDef.FLD_ADDRESS1] = addrRow[dataDef.FLD_ADDRESS1];
					newRow[dataDef.FLD_ADDRESS2] = addrRow[dataDef.FLD_ADDRESS2];
					newRow[dataDef.FLD_CITY] = addrRow[dataDef.FLD_CITY];
					newRow[dataDef.FLD_TYPE] = CopyToPostalAddressType;
					newRow[dataDef.FLD_COUNTY] = addrRow[dataDef.FLD_COUNTY];
					newRow[dataDef.FLD_COUNTRY_CODE] = addrRow[dataDef.FLD_COUNTRY_CODE];
					newRow[dataDef.FLD_SUBDIVISION_CODE] = addrRow[dataDef.FLD_SUBDIVISION_CODE];
					newRow[dataDef.FLD_ZIP] = addrRow[dataDef.FLD_ZIP];	
					newRow[dataDef.FLD_RESIDENTIAL_AREA] = addrRow[dataDef.FLD_RESIDENTIAL_AREA];								
					newRow[dataDef.FLD_CREATE_USER_ID] = UserID;
					dTblCopyToAddress.Rows.Add(newRow);
					
				}
				else
				{
					//Copy all the list to an existing record
					CommonSystem comSys = new CommonSystem();
					DataRow copyToRow = dvCopyTo[0].Row;
					DataRow addrRow = dvCopyFrom[0].Row;
					comSys.UpdateRow(copyToRow, dataDef.FLD_ENTITY_ID, CopyToEntityID.ToString());
					comSys.UpdateRow(copyToRow, dataDef.FLD_ENTITY_TYPE_ID, CopyToEntityType.ToString());
					comSys.UpdateRow(copyToRow, dataDef.FLD_ADDRESS_ID, addrRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_NAME, addrRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_FIRST_NAME, addrRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_LAST_NAME, addrRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_ADDRESS1, addrRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_ADDRESS2, addrRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_CITY, addrRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_TYPE, CopyToPostalAddressType.ToString());
					comSys.UpdateRow(copyToRow, dataDef.FLD_COUNTY, addrRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_COUNTRY_CODE, addrRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_SUBDIVISION_CODE, addrRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_ZIP, addrRow);
					comSys.UpdateRow(copyToRow, dataDef.FLD_RESIDENTIAL_AREA, addrRow);
					if (copyToRow.RowState == DataRowState.Modified)
						copyToRow[dataDef.FLD_UPDATE_USER_ID] = UserID;
							
					
				}
			}

			
		}	

		public void CopyToEntity(dataDef dTblCopyFromAddress, dataDef dTblCopyToAddress, int UserID, 
			int CopyFromEntityType, int CopyFromEntityID,
			int CopyToEntityType, int CopyToEntityID)
		{
			//This method is used to copy specific address info Between Entity

			DataView dvCopyTo = new DataView(dTblCopyToAddress);
			dvCopyTo.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + CopyToEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + CopyToEntityID;
			DataView dvCopyFrom = new DataView(dTblCopyFromAddress);
			dvCopyFrom.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + CopyFromEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + CopyFromEntityID;
					
			if (dvCopyFrom.Count >0)
			{
				//Copy all the list
				foreach(DataRowView addrRow in dvCopyFrom)
				{
					int typeID =  Convert.ToInt32(addrRow[dataDef.FLD_TYPE]);
					CopyToEntity(dTblCopyFromAddress, dTblCopyToAddress, UserID,
								CopyFromEntityType,CopyFromEntityID, typeID,
								CopyToEntityType, CopyToEntityID, typeID);
				}

			}
			
		}	

		public void CopyToEntity(dataDef dTblAddress, int UserID, 
			int CopyFromEntityType, int CopyFromEntityID,
			int CopyToEntityType, int CopyToEntityID)
		{
			//This method is used to copy specific address info Between Entity

			CopyToEntity(dTblAddress, dTblAddress, UserID, 
				CopyFromEntityType, CopyFromEntityID,
				CopyToEntityType, CopyToEntityID);
			
		}	


		public DataRow FindRow(dataDef dTblAddress,	int FindEntityType, 
				int FindEntityID, int FindPostalAddressType)
		{
			DataRow row;
			//This method is used to copy specific address info Between Entity

			DataView dvAddress = new DataView(dTblAddress);
			dvAddress.RowFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + FindEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + FindEntityID
				+ " AND " + dataDef.FLD_TYPE + " = " + FindPostalAddressType;
					
			if (dvAddress.Count >0)
			{
				//reference the row
				row = dvAddress[0].Row;
				return row;
			}
			else
			 return null;		
			
		}

		public DataRow[] FindRows(dataDef dTblAddress,	int FindEntityType, 
			int FindEntityID, int FindPostalAddressType)
		{
			DataRow[] arrRow;
			//This method is used to copy specific address info Between Entity

			string sFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + FindEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + FindEntityID
				+ " AND " + dataDef.FLD_TYPE + " = " + FindPostalAddressType;

			arrRow = dTblAddress.Select(sFilter);					
			
			return arrRow;
			
		}
        public DataRow[] FindRows(dataDef dTblAddress, int FindEntityType)
        {
            DataRow[] arrRow;
            //This method is used to copy specific address info Between Entity

            string sFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + FindEntityType;

            arrRow = dTblAddress.Select(sFilter);

            return arrRow;

        }

        #endregion
    }
}
