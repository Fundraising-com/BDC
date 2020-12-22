using System;
using QSPForm.Common;

namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using dataDef = QSPForm.Common.DataDef.WarehouseTable;
	using dataAccessRef = QSPForm.Data.Warehouse;
	
	/// <summary>
	///     This class contains the business rules that are used for an 
	///     Warehouse.
	/// </summary>

	public class WarehouseSystem : BusinessSystem
	{
		
		dataAccessRef objDataAccess;
		
		public WarehouseSystem()
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
		//   Validates Warehouse row
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
				if (isValid)
					isValid  = IsValid_Unicity(row);
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
		//   Validates a specific Warehouse Ownership Table field against his maxlength 
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
			//Warehouse
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_NAME, "Warehouse Name");
			
			if (!IsValid)
			{
				messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
			}
			            
			return IsValid;
		}
        

		private bool IsValid_Unicity(DataRow row)
		{
			
			// 
			// Ensure that Warehouse Name does not already exist in the database.
			// Call a Method from the Data Layer

			string warehouseName = "";
			warehouseName = row[dataDef.FLD_NAME].ToString();
			int warehouseID = Convert.ToInt32(row[dataDef.FLD_PKID]);

			DataTable existingWarehouse = objDataAccess.SelectAllWwarehouse_nameLogic(warehouseName);
			if(existingWarehouse.Rows.Count > 0)
			{
				DataRow existingRow = existingWarehouse.Rows[0];
				if(existingRow[dataDef.FLD_PKID].ToString() != warehouseID.ToString())
				{
					row.SetColumnError(dataDef.FLD_NAME, "This Warehouse name already exists, please choose a new one.");
					messageManager.ValidationExceptionType = QSPFormExceptionType.Unicity;
					return false;
				}
			}
			
			return true;
		}

		private bool IsValid_Integrity(DataRow row)
		{
		
			return true;
		
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

        public dataDef SelectOneByFulfID(int FulfID)
        {
            dataDef dTbl;
            //
            // Get the user DataTable from the DataLayer
            //
            return objDataAccess.SelectOneByFulfID(FulfID);
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

		public WarehouseData InitializeWarehouse(int UserID)
		{
			//We prepare the DataSet for all step
			//Add a new Row
			WarehouseData dts = new WarehouseData();
			
			//Create a new Organization  row at start
			WarehouseTable wareTable = dts.Warehouse;
			DataRow row;
			row = wareTable.NewRow();		
			row[WarehouseTable.FLD_NAME] = "New Warehouse";
			row[WarehouseTable.FLD_WAREHOUSE_STATUS_ID] = WarehouseStatus.IN_PROCESS;			
			row[WarehouseTable.FLD_CREATE_USER_ID] = UserID;
			wareTable.Rows.Add(row);

			return dts;
			
		}

		public WarehouseData SelectAllDetail(int ID)
		{			
			//This method fill the All Data needed for an warehouse
			//into a predefined DataSet
			WarehouseData dts = new WarehouseData();
			dts.Merge(objDataAccess.SelectOne(ID));	
			//Postal Address
			PostalAddressSystem addSys = new PostalAddressSystem();
			dts.Merge(addSys.SelectAllByWarehouseID(ID));
			//Phone Number
			PhoneNumberSystem phoneSys = new PhoneNumberSystem();
			dts.Merge(phoneSys.SelectAllByWarehouseID(ID));			
			//Email Addess
			EmailAddressSystem emailSys = new EmailAddressSystem();
			dts.Merge(emailSys.SelectAllByWarehouseID(ID));

			//Business Calendar
			Data.Warehouse_business_calendar wareDataAcc = new Data.Warehouse_business_calendar();
			dts.Merge(wareDataAcc.SelectAllWwarehouse_idLogic(ID));

			return dts;
			
		}

		public WarehouseData SelectAllDetailByFulfWarehouseID(int FulfWarehouseID)
		{			
			//This method fill the All Data needed for an warehouse
			//into a predefined DataSet
			WarehouseData dts = new WarehouseData();
			dts.Merge(objDataAccess.SelectAllWfulf_warehouse_idLogic(FulfWarehouseID));	
			if (dts.Warehouse.Rows.Count > 0)
			{
				DataRow wareRow = dts.Warehouse.Rows[0];
				int ID = 0;
				ID = Convert.ToInt32(wareRow[dataDef.FLD_PKID]);
				//Postal Address
				PostalAddressSystem addSys = new PostalAddressSystem();
				dts.Merge(addSys.SelectAllByWarehouseID(ID));
				//Phone Number
				PhoneNumberSystem phoneSys = new PhoneNumberSystem();
				dts.Merge(phoneSys.SelectAllByWarehouseID(ID));			
				//Email Addess
				EmailAddressSystem emailSys = new EmailAddressSystem();
				dts.Merge(emailSys.SelectAllByWarehouseID(ID));
				//Business Calendar
				Data.Warehouse_business_calendar wareDataAcc = new Data.Warehouse_business_calendar();
				dts.Merge(wareDataAcc.SelectAllWwarehouse_idLogic(ID));

			}
			return dts;
			
		}

		public bool  UpdateAllDetail(WarehouseData dts)
		{			
			bool IsSuccess = true;
			//This method fill the All Data needed for an warehouse
			//into a predefined DataSet			
			//Postal Address
			if (dts.PostalAddress.GetChanges() != null)
			{
				PostalAddressSystem addSys = new PostalAddressSystem();
				IsSuccess = addSys.UpdateBatch(dts.PostalAddress);
				if (!IsSuccess)
					return IsSuccess;
			}
			//Phone Number
			if (dts.PhoneNumber.GetChanges() != null)
			{
				PhoneNumberSystem phoneSys = new PhoneNumberSystem();
				IsSuccess = phoneSys.UpdateBatch(dts.PhoneNumber);
				if (!IsSuccess)
					return IsSuccess;
			}
			//Email Address
			if (dts.EmailAddress.GetChanges() != null)
			{
				EmailAddressSystem emailSys = new EmailAddressSystem();
				IsSuccess = emailSys.UpdateBatch(dts.EmailAddress);
			}

			if (dts.BusinessCalendar.GetChanges() != null)
			{
				Data.Warehouse_business_calendar wareDataAcc = new Data.Warehouse_business_calendar();
				wareDataAcc.UpdateBatch(dts.BusinessCalendar);
			}
			
			//--------------------------------------//
			//Rematch with Warehouse -- 
			//-------------------------------------//
			ReassignContactInformation(dts.Warehouse, dts.PostalAddress, dts.PhoneNumber, dts.EmailAddress);

			//Proceed to the insert of the table Warehouse
			IsSuccess = UpdateBatch(dts.Warehouse);
			
			return IsSuccess;
			
		}

		public bool  InsertAllDetail(WarehouseData dts)
		{			
			bool IsSuccess = true;
			//This method fill the All Data needed for an warehouse
			//into a predefined DataSet			
			//Postal Address
			if (dts.PostalAddress.GetChanges() != null)
			{
				PostalAddressSystem addSys = new PostalAddressSystem();
				IsSuccess = addSys.UpdateBatch(dts.PostalAddress);
				if (!IsSuccess)
					return IsSuccess;
			}
			//Phone Number
			if (dts.PhoneNumber.GetChanges() != null)
			{
				PhoneNumberSystem phoneSys = new PhoneNumberSystem();
				IsSuccess = phoneSys.UpdateBatch(dts.PhoneNumber);
				if (!IsSuccess)
					return IsSuccess;
			}
			//Email Address
			if (dts.EmailAddress.GetChanges() != null)
			{
				EmailAddressSystem emailSys = new EmailAddressSystem();
				IsSuccess = emailSys.UpdateBatch(dts.EmailAddress);
			}
			
			//--------------------------------------//
			//Rematch with Warehouse -- 
			//-------------------------------------//
			ReassignContactInformation(dts.Warehouse, dts.PostalAddress, dts.PhoneNumber, dts.EmailAddress);

			//Proceed to the insert of the table Warehouse
			IsSuccess = UpdateBatch(dts.Warehouse);	

			//for the Warehouse Calendar, we need to get the new ID before
			int WarehouseID = Convert.ToInt32(dts.Warehouse.Rows[0][dataDef.FLD_PKID]);
			foreach (DataRow calRow in dts.BusinessCalendar)
			{
				if (calRow.RowState == DataRowState.Added || calRow.RowState == DataRowState.Modified)
				{
					calRow[WarehouseBusinessCalendarTable.FLD_WAREHOUSE_ID] = WarehouseID;
				}
			}
			if (dts.BusinessCalendar.GetChanges() != null)
			{
				Data.Warehouse_business_calendar wareDataAcc = new Data.Warehouse_business_calendar();
				wareDataAcc.UpdateBatch(dts.BusinessCalendar);
			}
						
			return IsSuccess;
			
		}

		private void ReassignContactInformation(WarehouseTable dTblWarehouse, 
			PostalAddressEntityTable dTblAddress,
			PhoneNumberEntityTable dTblPhone,
			EmailEntityTable dTblEmail)
		{
			DataRow wareRow = dTblWarehouse.Rows[0];
			int WarehouseID = Convert.ToInt32(wareRow[dataDef.FLD_PKID]);
			DataRow rowToFind;
			
			//----------------------------------------------//
			//   Rematch with Warehouse -- Postal Address
			//----------------------------------------------//

			//Billing Address
			PostalAddressSystem addrSys = new PostalAddressSystem();
			
			rowToFind = addrSys.FindRow(dTblAddress, EntityType.TYPE_WAREHOUSE, 
				WarehouseID, PostalAddressType.TYPE_BILLING);
			if (rowToFind != null)
			{
				wareRow[dataDef.FLD_POSTAL_ADDRESS_ID] = rowToFind[PostalAddressEntityTable.FLD_PKID];					
			}

			PhoneNumberSystem phoneSys = new PhoneNumberSystem();			
			//General Phone Number
			rowToFind = phoneSys.FindRow(dTblPhone, EntityType.TYPE_WAREHOUSE, 
				WarehouseID, PhoneNumberType.TYPE_PHONE_NUMBER);
			if (rowToFind != null)
			{
				wareRow[dataDef.FLD_PHONE_NUMBER_ID] = rowToFind[PhoneNumberEntityTable.FLD_PKID];					
			}

			//General Fax Number 
			rowToFind = phoneSys.FindRow(dTblPhone, EntityType.TYPE_WAREHOUSE, 
				WarehouseID, PhoneNumberType.TYPE_FAX_NUMBER);
			if (rowToFind != null)
			{
				wareRow[dataDef.FLD_FAX_NUMBER_ID] = rowToFind[PhoneNumberEntityTable.FLD_PKID];					
			}

			//Receiving Phone Number 
			rowToFind = phoneSys.FindRow(dTblPhone, EntityType.TYPE_WAREHOUSE, 
				WarehouseID, PhoneNumberType.TYPE_RECEIVING_PHONE_NUMBER);
			if (rowToFind != null)
			{
				wareRow[dataDef.FLD_FAX_NUMBER_ID] = rowToFind[PhoneNumberEntityTable.FLD_PKID];					
			}

			EmailAddressSystem emailSys = new EmailAddressSystem();
			
			//Corporative Email Address
			rowToFind = emailSys.FindRow(dTblEmail, EntityType.TYPE_WAREHOUSE, 
				WarehouseID, EmailType.TYPE_CORPORATIVE);
			if (rowToFind != null)
			{
				wareRow[dataDef.FLD_EMAIL_ID] = rowToFind[EmailEntityTable.FLD_PKID];					
			}
		}

	

		public dataDef SelectAll_Search(int SearchType, String Criteria, string SubdivisionCode)
		{			
			dataDef dTbl;
			
			//
			// Get the user DataTable from the DataLayer
			//				
			dTbl = objDataAccess.SelectAll_Search(SearchType, Criteria, SubdivisionCode);				
			
			return dTbl;			
		}

        public dataDef SelectAll_Search(int SearchType, String Criteria, string SubdivisionCode, bool PickUp)
        {
            dataDef dTbl;

            //
            // Get the user DataTable from the DataLayer
            //				
            dTbl = objDataAccess.SelectAll_Search(SearchType, Criteria, SubdivisionCode, PickUp);

            return dTbl;
        }

		public dataDef SelectAllByWarehouseName(String warehouseName)
		{
			//
			// Get the user DataTable from the DataLayer
			//
					
			return objDataAccess.SelectAllWwarehouse_nameLogic(warehouseName);						
			
		}

		public dataDef SelectDefaultWarehouseByZipCode(string Zip)
		{
			//This method is used to synchronize the data of the AS400
			//With the data of our DB on SQL Server
			dataDef dTblWarehouse =  new WarehouseTable();
            DataRepository.IVMWHSP syncDataAccess = new DataRepository.IVMWHSP();
			IVMWHSPTable dTblWarehouse_AS400 = syncDataAccess.SelectDefaultWarehouseByZipCode(Zip);
			if (dTblWarehouse_AS400.Rows.Count > 0)
			{
				int fulfWarehouseID = Convert.ToInt32(dTblWarehouse_AS400.Rows[0][IVMWHSPTable.FLD_PKID]);
				dTblWarehouse = objDataAccess.SelectAllWfulf_warehouse_idLogic(fulfWarehouseID);				
			}

			return dTblWarehouse;
		}

		public Common.DataDef.FINGDSTable SelectProductInventory_ByFulfWarehouseId(int FulfWarehouseID)
		{
			Common.DataDef.FINGDSTable dTblWarehouse =  new Common.DataDef.FINGDSTable();
			DataRepository.IVMWHSP syncDataAccess = new DataRepository.IVMWHSP();
			return syncDataAccess.SelectWarehouseProductInventory(FulfWarehouseID);
		}

		public bool Synchronize()
		{
			//This method is used to synchronize the data of the AS400
			//With the data of our DB on SQL Server
			bool IsSuccess = false;
			CommonSystem comSys = new CommonSystem();
			DataRepository.IVMWHSP syncDataAccess = new DataRepository.IVMWHSP();
			IVMWHSPTable dTblWarehouse_AS400 = syncDataAccess.SelectAll();
			
			foreach (DataRow syncRow in dTblWarehouse_AS400.Rows)
			{
				//Retreive the ID
				int FulfWarehouseID = Convert.ToInt32(syncRow[IVMWHSPTable.FLD_PKID]);
				WarehouseData dts = SelectAllDetailByFulfWarehouseID(FulfWarehouseID);
				
				//Fill the Vendor table before, there is not a lot of row
				Data.Vendor venDataAccess = new Data.Vendor();
				VendorTable dTblVendor = venDataAccess.SelectAll();
				DataView dvVendor = new DataView(dTblVendor);
				dvVendor.Sort = VendorTable.FLD_FULF_VENDOR_ID;

				if (dts.Warehouse.Rows.Count == 0)
				{				
					//Add the new Warehouse				
					dts = InitializeWarehouse(0);
				}


				//Treatment of the difference on the Warehouse Table
				DataRow wareRow = dts.Warehouse.Rows[0];					
				int WareID = Convert.ToInt32(wareRow[dataDef.FLD_PKID]);
				//Warehouse name
				comSys.UpdateRow(wareRow, dataDef.FLD_NAME , syncRow[IVMWHSPTable.FLD_NAME].ToString());
				//Vendor ID
				string vendorID = "";
				if (!syncRow.IsNull(IVMWHSPTable.FLD_VENDOR_ID))
				{
					string fulf_vendor_id = syncRow[IVMWHSPTable.FLD_VENDOR_ID].ToString();
					//Find the Vendor ID
					
					int iIndex = dvVendor.Find(fulf_vendor_id);
					if (iIndex != -1)
					{
						vendorID = dvVendor[iIndex][VendorTable.FLD_PKID].ToString();							
					}
				}
				comSys.UpdateRow(wareRow, dataDef.FLD_VENDOR_ID , vendorID);
				
				//Status
				string statusCode = "O"; //For Open
				if (!syncRow.IsNull(IVMWHSPTable.FLD_STATUS))
				{
					statusCode = syncRow[IVMWHSPTable.FLD_STATUS].ToString();
				}

				if (statusCode == "O")
				{
					wareRow[dataDef.FLD_WAREHOUSE_STATUS_ID] = WarehouseStatus.PROCESSED;
				}
				else
				{
					wareRow[dataDef.FLD_WAREHOUSE_STATUS_ID] = WarehouseStatus.PROCESSED_CLOSED;
				}
				
				//Postal Address
				PostalAddressSystem addSys = new PostalAddressSystem();
				DataView DVAddress = new DataView(dts.PostalAddress);
				DataRow addressRow;
				//Contact Name AATN
				//We have to split the field in two;
				string ContactName = syncRow[IVMWHSPTable.FLD_ADDRESS_ATTN].ToString();
				ContactName = ContactName.Replace("ATTN","").Trim();
				string[] arrName = ContactName.Split(new char[] {' '}, 2);
				string firstName = "";
				string lastName = "";
				if (arrName.Length > 0)
				{
					firstName = arrName[0];
					if (arrName.Length > 1)
					{
						lastName = arrName[1];
					}
				}
				//State
				string SubdivisionCode = "";
				if (syncRow[IVMWHSPTable.FLD_ADDRESS_STATE].ToString().Length >0)
					SubdivisionCode = "US-" + syncRow[IVMWHSPTable.FLD_ADDRESS_STATE].ToString();

				//Zip Code
				string ZipCode = "";
				if (syncRow[IVMWHSPTable.FLD_ADDRESS_ZIP].ToString().Length >0)
					ZipCode = syncRow[IVMWHSPTable.FLD_ADDRESS_ZIP].ToString();
				if (syncRow[IVMWHSPTable.FLD_ADDRESS_ZIP4].ToString().Length >0)
					ZipCode = ZipCode + "-" + syncRow[IVMWHSPTable.FLD_ADDRESS_ZIP4].ToString();

				addressRow = addSys.FindRow(dts.PostalAddress,
					Common.EntityType.TYPE_WAREHOUSE,
					WareID,
					Common.PostalAddressType.TYPE_BILLING);

				if (addressRow != null)
				{
					//'Table Mapping  
					//verification of all value before replacement                    
					//entity (Account_ID, Order_ID, Organization_ID, etc...)
					comSys.UpdateRow(addressRow, PostalAddressEntityTable.FLD_ENTITY_ID, WareID.ToString());
					//entity type (Account, Order, Organization, etc...)
					comSys.UpdateRow(addressRow, PostalAddressEntityTable.FLD_ENTITY_TYPE_ID, EntityType.TYPE_WAREHOUSE.ToString());
					//Address type (Billing, Shipping)
					comSys.UpdateRow(addressRow, PostalAddressEntityTable.FLD_TYPE, PostalAddressType.TYPE_BILLING.ToString());
					//Company Name
					comSys.UpdateRow(addressRow, PostalAddressEntityTable.FLD_NAME, syncRow[IVMWHSPTable.FLD_COMPANY_NAME].ToString());
					//Contact First Name
					comSys.UpdateRow(addressRow, PostalAddressEntityTable.FLD_FIRST_NAME, firstName);
					//Contact Last Name
					comSys.UpdateRow(addressRow, PostalAddressEntityTable.FLD_LAST_NAME, lastName);
					//Address Line 1
					comSys.UpdateRow(addressRow, PostalAddressEntityTable.FLD_ADDRESS1, syncRow[IVMWHSPTable.FLD_ADDRESS_LINE_1].ToString());
					//Address Line 2
					comSys.UpdateRow(addressRow, PostalAddressEntityTable.FLD_ADDRESS2, syncRow[IVMWHSPTable.FLD_ADDRESS_LINE_2].ToString());
					//City
					comSys.UpdateRow(addressRow, PostalAddressEntityTable.FLD_CITY, syncRow[IVMWHSPTable.FLD_ADDRESS_CITY].ToString());
					//State
					comSys.UpdateRow(addressRow, PostalAddressEntityTable.FLD_SUBDIVISION_CODE, SubdivisionCode);
					//Zip Code
					comSys.UpdateRow(addressRow, PostalAddressEntityTable.FLD_ZIP, ZipCode);						
				
					if (addressRow.RowState == DataRowState.Added)
						addressRow[PostalAddressEntityTable.FLD_CREATE_USER_ID] = 0;
					else
						comSys.UpdateRow(addressRow, PostalAddressEntityTable.FLD_UPDATE_USER_ID, "0");
				}
				else
				{
					DataRow newRow = dts.PostalAddress.NewRow();
					newRow[PostalAddressEntityTable.FLD_ENTITY_ID] = WareID.ToString();
					//entity type (Account, Order, Organization, etc...)
					newRow[PostalAddressEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_WAREHOUSE.ToString();
					//Address type (Billing, Shipping)
					newRow[PostalAddressEntityTable.FLD_TYPE] = PostalAddressType.TYPE_BILLING.ToString();
					//Company Name
					newRow[PostalAddressEntityTable.FLD_NAME] = syncRow[IVMWHSPTable.FLD_COMPANY_NAME].ToString();
					//Contact Name
					newRow[PostalAddressEntityTable.FLD_FIRST_NAME] = firstName;
					//Contact Name
					newRow[PostalAddressEntityTable.FLD_LAST_NAME] = lastName;
					//Address Line 1
					newRow[PostalAddressEntityTable.FLD_ADDRESS1] = syncRow[IVMWHSPTable.FLD_ADDRESS_LINE_1].ToString();
					//Address Line 2
					newRow[PostalAddressEntityTable.FLD_ADDRESS2] = syncRow[IVMWHSPTable.FLD_ADDRESS_LINE_2].ToString();
					//City
					newRow[PostalAddressEntityTable.FLD_CITY] = syncRow[IVMWHSPTable.FLD_ADDRESS_CITY].ToString();
					//State
					newRow[PostalAddressEntityTable.FLD_SUBDIVISION_CODE] = SubdivisionCode;
					//Zip Code
					newRow[PostalAddressEntityTable.FLD_ZIP] = ZipCode;					

					newRow[PostalAddressEntityTable.FLD_CREATE_USER_ID] = 0;
					dts.PostalAddress.Rows.Add(newRow);											
				}
		

				PhoneNumberEntityTable dTblPhoneNumber = dts.PhoneNumber;	
				PhoneNumberSystem phoneSys = new PhoneNumberSystem();
				string synch_phoneNumber = "";
				//-----------------------------------------
				// Warehouse Phone Number
				//-----------------------------------------
				DataRow rowPhone = phoneSys.FindRow(dts.PhoneNumber,
					Common.EntityType.TYPE_WAREHOUSE,
					WareID,
					Common.PhoneNumberType.TYPE_PHONE_NUMBER);
				
				if ((syncRow[IVMWHSPTable.FLD_PHONE_NUMBER].ToString().Trim() != "0") &&
					(syncRow[IVMWHSPTable.FLD_PHONE_NUMBER].ToString().Trim().Length > 0))
				{
					synch_phoneNumber = syncRow[IVMWHSPTable.FLD_PHONE_NUMBER].ToString().Trim();
					synch_phoneNumber = comSys.FormatPhoneNumber(synch_phoneNumber);
				}
				else
					synch_phoneNumber = "";

				if (rowPhone != null)
				{
					if (synch_phoneNumber.Length > 0)
					{
						comSys.UpdateRow(rowPhone, PhoneNumberEntityTable.FLD_PHONE_NUMBER, synch_phoneNumber);
					}
					else
					{	
						rowPhone.Delete();
					}
				}
				else
				{
					if (synch_phoneNumber.Length > 0)
					{
						DataRow newRow = dTblPhoneNumber.NewRow();
						newRow[PhoneNumberEntityTable.FLD_TYPE] = PhoneNumberType.TYPE_PHONE_NUMBER; //Corporate
						newRow[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = synch_phoneNumber;
						newRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = WareID;
						newRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_WAREHOUSE;
						newRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = 0;
						dTblPhoneNumber.Rows.Add(newRow);
					}				
				
				}
				//--------------------------------------		
				// Warehouse Fax Number
				//--------------------------------------
				if ((syncRow[IVMWHSPTable.FLD_FAX].ToString().Trim() != "0") &&
					(syncRow[IVMWHSPTable.FLD_FAX].ToString().Trim().Length > 0))
				{
					synch_phoneNumber = syncRow[IVMWHSPTable.FLD_FAX].ToString().Trim();
					synch_phoneNumber = comSys.FormatPhoneNumber(synch_phoneNumber);
				}
				else
					synch_phoneNumber = "";
				
				rowPhone = phoneSys.FindRow(dts.PhoneNumber,
					Common.EntityType.TYPE_WAREHOUSE,
					WareID,
					Common.PhoneNumberType.TYPE_FAX_NUMBER);			
				if (rowPhone != null)
				{
					if (synch_phoneNumber.Length > 0)
					{
						comSys.UpdateRow(rowPhone, PhoneNumberEntityTable.FLD_PHONE_NUMBER, synch_phoneNumber);
					}
					else
					{	
						rowPhone.Delete();
					}
				}
				else
				{
					if (synch_phoneNumber.Length > 0)
					{
						DataRow newRow = dTblPhoneNumber.NewRow();
						newRow[PhoneNumberEntityTable.FLD_TYPE] = PhoneNumberType.TYPE_FAX_NUMBER; //Corporate
						newRow[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = synch_phoneNumber;
						newRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = WareID;
						newRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_WAREHOUSE;
						newRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = 0;
						dTblPhoneNumber.Rows.Add(newRow);
					}				
				
				}

				//--------------------------------------		
				// Warehouse Receiving Phone Number
				//--------------------------------------
				if ((syncRow[IVMWHSPTable.FLD_RECEIVING_PHONE_NUMBER].ToString().Trim() != "0") &&
				    (syncRow[IVMWHSPTable.FLD_RECEIVING_PHONE_NUMBER].ToString().Trim().Length > 0))
				{
					synch_phoneNumber = syncRow[IVMWHSPTable.FLD_RECEIVING_PHONE_NUMBER].ToString().Trim();
					synch_phoneNumber = comSys.FormatPhoneNumber(synch_phoneNumber);
					if (syncRow[IVMWHSPTable.FLD_RECEIVING_PHONE_EXTENSION].ToString().Length > 0)
						synch_phoneNumber = synch_phoneNumber + " #" + syncRow[IVMWHSPTable.FLD_RECEIVING_PHONE_NUMBER].ToString();
				}
				else
					synch_phoneNumber = "";
				
				rowPhone = phoneSys.FindRow(dts.PhoneNumber,
											Common.EntityType.TYPE_WAREHOUSE,
											WareID,
											Common.PhoneNumberType.TYPE_RECEIVING_PHONE_NUMBER);			
				if (rowPhone != null)
				{
					if (synch_phoneNumber.Length > 0)
					{
						comSys.UpdateRow(rowPhone, PhoneNumberEntityTable.FLD_PHONE_NUMBER, synch_phoneNumber);
					}
					else
					{	
						rowPhone.Delete();
					}
				}
				else
				{
					if (synch_phoneNumber.Length > 0)
					{
						DataRow newRow = dTblPhoneNumber.NewRow();
						newRow[PhoneNumberEntityTable.FLD_TYPE] = PhoneNumberType.TYPE_RECEIVING_PHONE_NUMBER; //Corporate
						newRow[PhoneNumberEntityTable.FLD_PHONE_NUMBER] = synch_phoneNumber;
						newRow[PhoneNumberEntityTable.FLD_ENTITY_ID] = WareID;
						newRow[PhoneNumberEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_WAREHOUSE;
						newRow[PhoneNumberEntityTable.FLD_CREATE_USER_ID] = 0;
						dTblPhoneNumber.Rows.Add(newRow);
					}				
				
				}

			
				

			
			}

			return IsSuccess;

		}
		

	}
}
