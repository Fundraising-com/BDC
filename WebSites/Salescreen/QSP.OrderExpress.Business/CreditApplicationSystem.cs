using System;
using QSPForm.Common;

namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using dataDef = QSPForm.Common.DataDef.CreditApplicationTable;
	using dataAccessRef = QSPForm.Data.Credit_application;
	
	/// <summary>
	///     This class contains the business rules that are used for an 
	///     CreditApplication.
	/// </summary>

	public class CreditApplicationSystem : BusinessSystem
	{
		
		dataAccessRef objDataAccess;
		
		public CreditApplicationSystem()
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
		//   Validates CreditApplication row
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
		//   Validates a specific CreditApplication Ownership Table field against his maxlength 
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
			//CreditApplication
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_ACCOUNT_ID, "Account");
			//CreditApplication Number
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_CREDIT_LIMIT, "Credit Limit");
			
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

		public dataDef SelectAll()
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAll();				
			
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
	
		public dataDef SelectAllByAccountID(int AccountID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWaccount_id(AccountID);				
			
			return dTbl;			
		}

		public dataDef SelectAllByCustomerID(int CustomerID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWcustomer_idLogic(CustomerID);				
			
			return dTbl;			
		}

		public CreditApplicationData SelectAllDetail(int ID)
		{			
			
			return objDataAccess.SelectAllDetail(ID);
			
		}

		public CreditApplicationData SelectAllDetailByAccountID(int AccountID)
		{			
			
			return objDataAccess.SelectAllDetailByAccountID(AccountID);
			
		}

		public CreditApplicationData InitializeCreditApplication(int AccountID, int UserID)
		{			
			//This method fill the All Data needed for a Credit Application
			//into a predefined DataSet
			CreditApplicationData dts = new CreditApplicationData();
			//We get the info about the account first
			AccountSystem accSys = new AccountSystem();
			dts.Merge(accSys.SelectOne(AccountID));
			
			//Creating the New Row with Default information
			DataRow newRow = dts.CreditApplication.NewRow();
			DataRow accRow = dts.Account.Rows[0];
			newRow[dataDef.FLD_ACCOUNT_ID] = accRow[AccountTable.FLD_PKID];
			//Get the form_id
			int FormID = 0;
			FormSystem frmSys = new FormSystem();
			FormID = frmSys.GetCurrentFormID(EntityType.TYPE_CREDIT_APPLICATION);
			if (FormID > 0)
			{
				newRow[dataDef.FLD_FORM_ID] = FormID;
			}
			newRow[dataDef.FLD_CREATE_USER_ID] = UserID;
			
			dts.CreditApplication.Rows.Add(newRow);
			int creditAppID = Convert.ToInt32(dts.CreditApplication.Rows[0][dataDef.FLD_PKID]);

			PostalAddressSystem addSys = new PostalAddressSystem();
			dts.Merge(addSys.SelectAllByAccountID(AccountID));			
			
			//We copy the Billing Information to the Credit application
			addSys.CopyToEntity(dts.PostalAddress, UserID, 
					EntityType.TYPE_ACCOUNT, AccountID, PostalAddressType.TYPE_BILLING,
					EntityType.TYPE_CREDIT_APPLICATION, creditAppID, PostalAddressType.TYPE_BILLING);
			DataRow addrRow = addSys.FindRow(dts.PostalAddress, EntityType.TYPE_CREDIT_APPLICATION, creditAppID, PostalAddressType.TYPE_BILLING);
			if (addrRow != null)
			{
				string sOfficerName = "";
				if (addrRow[PostalAddressEntityTable.FLD_FIRST_NAME].ToString().Trim().Length >0)
				{
					sOfficerName = addrRow[PostalAddressEntityTable.FLD_FIRST_NAME].ToString().Trim();
				}				
				if (addrRow[PostalAddressEntityTable.FLD_LAST_NAME].ToString().Trim().Length >0)
				{
					if (sOfficerName.Length > 0)
						sOfficerName = sOfficerName + " ";

					sOfficerName = sOfficerName + addrRow[PostalAddressEntityTable.FLD_LAST_NAME].ToString().Trim();
				}
				if (sOfficerName.Length > 0)
					newRow[dataDef.FLD_OFFICER_NAME] = sOfficerName;
			}
			
			//We copy the Billing Information to the Credit application
			PhoneNumberSystem phoneSys = new PhoneNumberSystem();
			dts.Merge(phoneSys.SelectAllByAccountID(AccountID));			
			
			phoneSys.CopyToEntity(dts.PhoneNumber, UserID, 
				EntityType.TYPE_ACCOUNT, AccountID, PhoneNumberType.TYPE_BILLING_PHONE,
				EntityType.TYPE_CREDIT_APPLICATION, creditAppID, PhoneNumberType.TYPE_BILLING_PHONE);
			
			return dts;
			
		}

		public bool  InsertAllDetail(CreditApplicationData dts, int UserID)
		{			
			String TransactionName = "CreditApp_InsertAllDetail";						
			Data.ConnectionProvider connProvider = new Data.ConnectionProvider();

			bool IsSuccess = true;
			try
			{
				int creditAppID = 0;
				int creditCardID = 0;
				DataRow rowAddress;
				DataRow rowPhone;

				DataRow crdAppRow;
				crdAppRow = dts.CreditApplication.Rows[0];
				creditAppID = Convert.ToInt32(crdAppRow[dataDef.FLD_PKID]);
		
				Data.Credit_application crdAppDataAccess = new Data.Credit_application();
				Data.Credit_card crdDataAccess = new Data.Credit_card();
				Data.Postal_address_entity addDataAccess = new Data.Postal_address_entity();
				Data.Phone_number_entity phoneDataAccess = new Data.Phone_number_entity();

				crdAppDataAccess.MainConnectionProvider = connProvider;
				crdDataAccess.MainConnectionProvider = connProvider;
				addDataAccess.MainConnectionProvider = connProvider;
				phoneDataAccess.MainConnectionProvider = connProvider;
					
				connProvider.OpenConnection();
				connProvider.BeginTransaction(TransactionName);

				//Postal Address
				if (dts.PostalAddress.GetChanges() != null)
				{	
					addDataAccess.UpdateBatch(dts.PostalAddress);				
				}
				
				//Phone Number
				if (dts.PhoneNumber.GetChanges() != null)
				{
					phoneDataAccess.UpdateBatch(dts.PhoneNumber);			
				}
				
				//-------------------------------------/////
				//Rematch with Credit Application 
				//-------------------------------------/////
				PostalAddressSystem addSys = new PostalAddressSystem();
				PhoneNumberSystem phoneSys = new PhoneNumberSystem();

				rowAddress = addSys.FindRow(dts.PostalAddress,
											EntityType.TYPE_CREDIT_APPLICATION,
											creditAppID,
											PostalAddressType.TYPE_BILLING);
				if (rowAddress != null)
				{
					crdAppRow[dataDef.FLD_POSTAL_ADDRESS_ID] = rowAddress[PostalAddressEntityTable.FLD_PKID];					
				}	
				
				//Phone Number
				rowPhone = phoneSys.FindRow(dts.PhoneNumber,
									EntityType.TYPE_CREDIT_APPLICATION,
									creditAppID,
									PhoneNumberType.TYPE_BILLING_PHONE);
				if (rowPhone != null)
				{
					crdAppRow[dataDef.FLD_PHONE_NUMBER_ID] = rowPhone[PhoneNumberEntityTable.FLD_PKID];	
				}	
						
				//Home Phone Number
				rowPhone = phoneSys.FindRow(dts.PhoneNumber,
											EntityType.TYPE_CREDIT_APPLICATION,
											creditAppID,
											PhoneNumberType.TYPE_HOME_PHONE_NUMBER);
				if (rowPhone != null)
				{
					crdAppRow[dataDef.FLD_HOME_PHONE_NUMBER_ID] = rowPhone[PhoneNumberEntityTable.FLD_PKID];	
				}

				//Credit Card
				if (dts.CreditCard.GetChanges() != null)
				{
					DataRow ccRow = dts.CreditCard.Rows[0];
					creditCardID = Convert.ToInt32(ccRow[CreditCardTable.FLD_PKID]);
					//-------------------------------------/////
					//Rematch with Credit Card 
					//-------------------------------------/////
					rowAddress = addSys.FindRow(dts.PostalAddress,
												EntityType.TYPE_CREDIT_CARD,
												creditCardID,
												PostalAddressType.TYPE_BILLING);
					if (rowAddress != null)
					{
						ccRow[CreditCardTable.FLD_POSTAL_ADDRESS_ID] = rowAddress[PostalAddressEntityTable.FLD_PKID];					
					}
		
					CreditCardSystem ccSys = new CreditCardSystem();
					crdDataAccess.UpdateBatch(dts.CreditCard);
					

					creditCardID = Convert.ToInt32(ccRow[CreditCardTable.FLD_PKID]);
					crdAppRow[dataDef.FLD_CREDIT_CARD_ID] = creditCardID;
				}

				//This method fill the All Data needed for a credit application
				//into a predefined DataSet	
				
				crdAppDataAccess.UpdateBatch(dts.CreditApplication);				
				
				SetDocumentRequirement(dts, UserID, connProvider);

				Refresh(dts, UserID, DataOperation.INSERT, connProvider);	
			
			
				//Commit transaction 
				connProvider.CommitTransaction();
				IsSuccess = true;
				
			}
			catch (Exception ex)
			{
				if (connProvider != null)
				{
					if (connProvider.DBConnection.State != ConnectionState.Closed)
						connProvider.RollbackTransaction(TransactionName);
				}
				IsSuccess = false;
			}
			finally
			{
				if (connProvider != null)
				{
					if (connProvider.DBConnection.State != ConnectionState.Closed)
						connProvider.CloseConnection(false);
				}
			}			

			return IsSuccess;
		
		}

		public bool  UpdateAllDetail(CreditApplicationData dts, int UserID)
		{			
			String TransactionName = "CreditApp_UpdateAllDetail";						
			Data.ConnectionProvider connProvider = new Data.ConnectionProvider();

			bool IsSuccess = true;
			try
			{
				int creditAppID = 0;
				int creditCardID = 0;
				DataRow rowAddress;
				DataRow rowPhone;
				CommonSystem comSys = new CommonSystem();

				DataRow crdAppRow;
				crdAppRow = dts.CreditApplication.Rows[0];
				creditAppID = Convert.ToInt32(crdAppRow[dataDef.FLD_PKID]);

				Data.Credit_application crdAppDataAccess = new Data.Credit_application();
				Data.Credit_card crdDataAccess = new Data.Credit_card();
				Data.Postal_address_entity addDataAccess = new Data.Postal_address_entity();
				Data.Phone_number_entity phoneDataAccess = new Data.Phone_number_entity();

				crdAppDataAccess.MainConnectionProvider = connProvider;
				crdDataAccess.MainConnectionProvider = connProvider;
				addDataAccess.MainConnectionProvider = connProvider;
				phoneDataAccess.MainConnectionProvider = connProvider;
					
				connProvider.OpenConnection();
				connProvider.BeginTransaction(TransactionName);
					
				//Postal Address
				if (dts.PostalAddress.GetChanges() != null)
				{	
					addDataAccess.UpdateBatch(dts.PostalAddress);					
				}
				
				//Phone Number
				if (dts.PhoneNumber.GetChanges() != null)
				{
					phoneDataAccess.UpdateBatch(dts.PhoneNumber);					
				}
				
				//-------------------------------------/////
				//Rematch with Credit Application 
				//-------------------------------------/////
				PostalAddressSystem addSys = new PostalAddressSystem();
				PhoneNumberSystem phoneSys = new PhoneNumberSystem();

				rowAddress = addSys.FindRow(dts.PostalAddress,
					EntityType.TYPE_CREDIT_APPLICATION,
					creditAppID,
					PostalAddressType.TYPE_BILLING);
				if (rowAddress != null)
				{
					int AddressID = Convert.ToInt32(rowAddress[PostalAddressEntityTable.FLD_PKID]);
					comSys.UpdateRow(crdAppRow, dataDef.FLD_POSTAL_ADDRESS_ID, AddressID.ToString());							
				}	
				
				//Phone Number
				rowPhone = phoneSys.FindRow(dts.PhoneNumber,
					EntityType.TYPE_CREDIT_APPLICATION,
					creditAppID,
					PhoneNumberType.TYPE_BILLING_PHONE);
				if (rowPhone != null)
				{
					int PhoneID = Convert.ToInt32(rowPhone[PhoneNumberEntityTable.FLD_PKID]);
					comSys.UpdateRow(crdAppRow, dataDef.FLD_PHONE_NUMBER_ID, PhoneID.ToString());				
				}	
						
				//Home Phone Number
				rowPhone = phoneSys.FindRow(dts.PhoneNumber,
					EntityType.TYPE_CREDIT_APPLICATION,
					creditAppID,
					PhoneNumberType.TYPE_HOME_PHONE_NUMBER);
				if (rowPhone != null)
				{
					int PhoneID = Convert.ToInt32(rowPhone[PhoneNumberEntityTable.FLD_PKID]);
					comSys.UpdateRow(crdAppRow, dataDef.FLD_HOME_PHONE_NUMBER_ID, PhoneID.ToString());				
				}

				//Credit Card
				if (dts.CreditCard.Rows.Count > 0)
				{
					DataRow ccRow = dts.CreditCard.Rows[0];
					creditCardID = Convert.ToInt32(ccRow[CreditCardTable.FLD_PKID]);
					//-------------------------------------/////
					//Rematch with Credit Card 
					//-------------------------------------/////
					rowAddress = addSys.FindRow(dts.PostalAddress,
						EntityType.TYPE_CREDIT_CARD,
						creditCardID,
						PostalAddressType.TYPE_BILLING);
					if (rowAddress != null)
					{
						int AddressID = Convert.ToInt32(rowAddress[PostalAddressEntityTable.FLD_PKID]);
						comSys.UpdateRow(ccRow, CreditCardTable.FLD_POSTAL_ADDRESS_ID, AddressID.ToString());								
					}

					if (dts.CreditCard.GetChanges() != null)
					{
						CreditCardSystem ccSys = new CreditCardSystem();
						crdDataAccess.UpdateBatch(dts.CreditCard);
					}
					creditCardID = Convert.ToInt32(ccRow[CreditCardTable.FLD_PKID]);
					comSys.UpdateRow(crdAppRow,dataDef.FLD_CREDIT_CARD_ID, creditCardID.ToString());
				}

				//This method fill the All Data needed for a credit application
				//into a predefined DataSet	
				if (dts.CreditApplication.GetChanges() != null)
				{
					crdAppDataAccess.UpdateBatch(dts.CreditApplication);					
				}

				SetDocumentRequirement(dts, UserID,	connProvider);

				//Business Validation
				Refresh(dts, UserID, DataOperation.UPDATE, connProvider);
			
				//Commit transaction 
				connProvider.CommitTransaction();
				IsSuccess = true;
				
			}
			catch (Exception ex)
			{
				if (connProvider != null)
				{
					if (connProvider.DBConnection.State != ConnectionState.Closed)
						connProvider.RollbackTransaction(TransactionName);
				}
				IsSuccess = false;
			}
			finally
			{
				if (connProvider != null)
				{
					if (connProvider.DBConnection.State != ConnectionState.Closed)
						connProvider.CloseConnection(false);
				}
			}			

			return IsSuccess;
		
		}

		//************************************************************************//
		//				  REFRESH --BUSINESS EXCEPTION AND TASK 				  //
		//************************************************************************//
		internal bool Refresh(int creditAppID, int UserID)
		{
			bool IsValid = true;

			try
			{	
				CreditApplicationData dts = SelectAllDetail(creditAppID);
				IsValid = Refresh(dts, UserID, DataOperation.UPDATE, null);
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}
		internal bool Refresh(int creditAppID, int UserID, Data.ConnectionProvider connProvider)
		{
			bool IsValid = true;

			try
			{	
				Data.Credit_application crAppDataAccess = new Credit_application();
				if (connProvider != null)
					crAppDataAccess.MainConnectionProvider = connProvider;
				CreditApplicationData dts = crAppDataAccess.SelectAllDetail(creditAppID);
				IsValid = Refresh(dts, UserID, DataOperation.UPDATE, connProvider);
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}


		private bool Refresh(CreditApplicationData dts, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
		{
			bool IsValid = true;

			try
			{	
				FormData dtsForm = new FormData();
				FormSystem frmSys = new FormSystem();
				DataRow crdAppRow = dts.CreditApplication.Rows[0];
				int FormID = Convert.ToInt32(crdAppRow[dataDef.FLD_FORM_ID]);
				dtsForm = frmSys.SelectAllDetail(FormID, true);

				bool HasChanged = false;
				HasChanged = RefreshValidation(dts, dtsForm, UserID, dataOperation, connProvider);
				RefreshTask(dts, dtsForm, UserID, dataOperation, connProvider);
				//If a change happen --> cascade the change to the order
				if (HasChanged)
				{
					int AccountID = Convert.ToInt32(crdAppRow[dataDef.FLD_ACCOUNT_ID]);
					AccountSystem accSys = new AccountSystem();
					IsValid = accSys.Refresh(AccountID, UserID, connProvider);
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}

		//************************************************************************//
		//			  VALIDATION -- BUSINESS EXCEPTION -- ACCOUNT				  //
		//************************************************************************//
		public bool PerformValidation(CreditApplicationData dts, int UserID, int dataOperation)
		{		
			bool IsValid = true;

			try
			{
				DataRow crdAppRow = dts.CreditApplication.Rows[0];
				int FormID = Convert.ToInt32(crdAppRow[CreditApplicationTable.FLD_FORM_ID]); //Hardcoded
				QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
				FormData dtsForm = formSys.SelectAllDetail(FormID, true);

				IsValid = PerformValidation(dts, dtsForm, UserID, dataOperation);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}

		private bool PerformValidation(CreditApplicationData dts, FormData dtsForm, int UserID, int dataOperation)
		{		
			bool IsValid = true;

			try
			{
				QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
				IsValid = formSys.PerformValidation(dts, dtsForm, UserID, dataOperation);						
				
				DataRow crdAppRow = dts.CreditApplication.Rows[0];				
				crdAppRow[dataDef.FLD_IS_VALIDATION_PERFORMED] = true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}


		private bool RefreshValidation(CreditApplicationData dts, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
		{		
			bool IsValid = true;

			try
			{					
				DataRow crdAppRow = dts.CreditApplication.Rows[0];
				bool IsValidationPerformed = false;
				if (!crdAppRow.IsNull(dataDef.FLD_IS_VALIDATION_PERFORMED))
					IsValidationPerformed = Convert.ToBoolean(crdAppRow[dataDef.FLD_IS_VALIDATION_PERFORMED]);
				//In a Refresh we only performed the validation if it's not already done
				//Otherwise we shoud use the Perform Method directly
				if (!IsValidationPerformed)
					PerformValidation(dts, dtsForm, UserID, dataOperation);

				if(dts.CreditException.GetChanges() != null)
				{
					Data.Entity_exception excDataAccess = new Data.Entity_exception();
					if (connProvider != null)
						excDataAccess.MainConnectionProvider = connProvider;
					excDataAccess.UpdateBatch(dts.CreditException);					
				}
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}

		//************************************************************************//
		//					BUSINESS TASK -- ACCOUNT							  //
		//************************************************************************//
		private bool PerformTask(CreditApplicationData dts, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
		{		
			bool IsSuccess = true;

			try
			{	
				QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
				IsSuccess = formSys.PerformTask(dts, dtsForm, UserID, dataOperation, connProvider);
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsSuccess;
		}

		private bool RefreshTask(CreditApplicationData dts, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
		{		
			bool IsSuccess = false;
			try
			{	
				//1 - Perform Account Task First												
				IsSuccess = PerformTask(dts, dtsForm, UserID, dataOperation, connProvider);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsSuccess;
		}

		private void SetDocumentRequirement(CreditApplicationData dts, int UserID, Data.ConnectionProvider connProvider)
		{
			DataRow accRow = dts.Account.Rows[0];
			int AccountID = Convert.ToInt32(accRow[AccountTable.FLD_PKID]);
			DataRow crdRow = dts.CreditApplication.Rows[0];
			int CreditAppID = Convert.ToInt32(crdRow[dataDef.FLD_PKID]);
			DocumentEntityTable crdDoc = dts.CreditDocument;
			EntityExceptionSystem excSys = new EntityExceptionSystem();
			EntityExceptionTable accExc = excSys.SelectAllByAccountID(AccountID);

			bool IsCreditAppFormRequired = false;
			//Apply Validation on the account Level First
			DataView dvException = new DataView(accExc);
			string sFilter = EntityExceptionTable.FLD_EXCEPTION_TYPE_ID + " = " + Convert.ToInt32(BusinessExceptionType.CreditApplication).ToString();
				
			dvException.RowFilter =	sFilter;
			IsCreditAppFormRequired = (dvException.Count != 0);
			
			dvException = null;

			//If a Tax Exemption Form is required we have to add a row 
			//in the DocumentEntityTable
			if (IsCreditAppFormRequired)
			{
				if (crdDoc.Rows.Count == 0)
				{
					DataRow newRow = crdDoc.NewRow();
					newRow[DocumentEntityTable.FLD_ENTITY_ID] = CreditAppID;
					newRow[DocumentEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_CREDIT_APPLICATION;
					newRow[DocumentEntityTable.FLD_DOCUMENT_NAME] = "Credit Application Form: Account # " + AccountID.ToString();
					newRow[DocumentEntityTable.FLD_DOCUMENT_TYPE_ID] = DocumentType.CREDIT_APPLICATION;
					newRow[DocumentEntityTable.FLD_CREATE_USER_ID] = UserID;
					crdDoc.Rows.Add(newRow);

					Data.Document_entity docDataAccess = new Data.Document_entity();
					if (connProvider != null)
						docDataAccess.MainConnectionProvider = connProvider;
					docDataAccess.Insert(crdDoc);
				}				
			}
			else
			{				
				if (crdDoc.Rows.Count > 0)
				{
					//Nothing For Now
//					DataRow docRow = crdDoc.Rows[0];
//					docRow[DocumentEntityTable.FLD_UPDATE_USER_ID] = UserID;
//					docRow.Delete();
//					DocumentEntitySystem docSys = new DocumentEntitySystem();
//					docSys.Delete(crdDoc);

				}
			}
			
		}
		
	}
}
