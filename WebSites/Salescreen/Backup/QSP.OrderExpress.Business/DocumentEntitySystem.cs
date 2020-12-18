using System;
using QSPForm.Common;

namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Common;
	using dataDef = QSPForm.Common.DataDef.DocumentEntityTable;
	using dataAccessRef = QSPForm.Data.Document_entity;
	
	/// <summary>
	///     This class contains the business rules that are used for an 
	///     Entity.
	/// </summary>

	public class DocumentEntitySystem : BusinessSystem
	{
		
		dataAccessRef objDataAccess;
		
		public DocumentEntitySystem()
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
			bool IsSuccess = false;
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			IsSuccess = this.Update(Table, objDataAccess);	
			
			int EntityID = Convert.ToInt32(Table.Rows[0][dataDef.FLD_ENTITY_ID]);
			int EntityTypeID = Convert.ToInt32(Table.Rows[0][dataDef.FLD_ENTITY_TYPE_ID]);
			int UserID = Convert.ToInt32(Table.Rows[0][dataDef.FLD_UPDATE_USER_ID]);
			
			if(EntityTypeID == Common.EntityType.TYPE_ACCOUNT)
			{
				AccountSystem accSys = new AccountSystem();
				accSys.Refresh(EntityID, UserID);							
			}
			else if(EntityTypeID == Common.EntityType.TYPE_CREDIT_APPLICATION)
			{
				CreditApplicationSystem crdSys = new CreditApplicationSystem();
				crdSys.Refresh(EntityID, UserID);
			}
			return IsSuccess;
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
		//   Validates Entity row
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
		//   Validates a specific Entity Ownership Table field against his maxlength 
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
			//Entity
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_ENTITY_ID, "Entity");
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_ENTITY_TYPE_ID, "Entity Type");
			
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

		public dataDef SelectOne(int ID,int EntityTypeID)
		{			
			dataDef dTbl;			
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectOne(ID,EntityTypeID);				
			
			return dTbl;
			
		}
	
		public dataDef SelectAllByEntityTypeID(int EntityTypeID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWentity_type_idLogic(EntityTypeID);				
			
			return dTbl;			
		}

		public dataDef SelectAllByEntityID(int EntityID, int EntityTypeID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWentity_idLogic(EntityID, EntityTypeID);				
			
			return dTbl;			
		}

		public dataDef SelectAllByAccountID(int AccountID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWentity_idLogic(AccountID, EntityType.TYPE_ACCOUNT);				
			
			return dTbl;			
		}
		
		public dataDef SelectAllByCreditApplicationID(int CreditAppID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWentity_idLogic(CreditAppID, EntityType.TYPE_CREDIT_APPLICATION);				
			
			return dTbl;			
		}

		public dataDef SelectAllByOrderID(int OrderID)
		{			
			dataDef dTbl;
			//
			// Get the user DataTable from the DataLayer
			//
			dTbl = objDataAccess.SelectAllWentity_idLogic(OrderID, EntityType.TYPE_ORDER_BILLING);				
			
			return dTbl;			
		}


		public dataDef SelectAll_Search(int SearchType, String Criteria, int EntityTypeID, int EntityID, int DocumentTypeID, bool Approved)
		{			
			dataDef dTbl;
			
			//
			// Get the user DataTable from the DataLayer
			//				
			dTbl = objDataAccess.SelectAll_Search(SearchType, Criteria, EntityTypeID, EntityID, DocumentTypeID, Approved);				
			
			return dTbl;			
		}	

		public dataDef SelectAll_Search(int SearchType, String Criteria, int EntityTypeID, int EntityID, int DocumentTypeID)
		{			
			dataDef dTbl;
			
			//
			// Get the user DataTable from the DataLayer
			//				
			dTbl = objDataAccess.SelectAll_Search(SearchType, Criteria, EntityTypeID, EntityID, DocumentTypeID);				
			
			return dTbl;			
		}

		public DataRow FindRow(dataDef dTblDocument, int FindEntityType, 
			int FindEntityID)
		{
			return FindRow(dTblDocument, FindEntityType, FindEntityID, 0);		
			
		}

		public DataRow FindRow(dataDef dTblDocument, int FindEntityType, 
			int FindEntityID, int FindDocumentType)
		{
			DataRow row;
			//This method is used to find specific document info Between Entity

			DataView dvDocument = new DataView(dTblDocument);
			string sFilter = "";
			sFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + FindEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + FindEntityID;
			if (FindDocumentType > 0)
				sFilter = sFilter + " AND " + dataDef.FLD_DOCUMENT_TYPE_ID + " = " + FindDocumentType;
			
			dvDocument.RowFilter = sFilter;

			if (dvDocument.Count >0)
			{
				//reference the row
				row = dvDocument[0].Row;
				return row;
			}
			else
				return null;		
			
		}

		public DataRow[] FindRows(dataDef dTblDocument, int FindEntityType, 
			int FindEntityID)
		{
			return FindRows(dTblDocument, FindEntityType, FindEntityID, 0);		
			
		}

		public DataRow[] FindRows(dataDef dTblDocument, int FindEntityType, 
			int FindEntityID, int FindDocumentType)
		{
			DataRow[] arrRow;
			//This method is used to find specific document info Between Entity

			string sFilter = "";
			sFilter = dataDef.FLD_ENTITY_TYPE_ID + " = " + FindEntityType
				+ " AND " + dataDef.FLD_ENTITY_ID + " = " + FindEntityID;
			if (FindDocumentType > 0)
				sFilter = sFilter + " AND " + dataDef.FLD_DOCUMENT_TYPE_ID + " = " + FindDocumentType;
			

			arrRow = dTblDocument.Select(sFilter);					
			
			return arrRow;
			
		}


		public void SetDocumentRequirement(DataSet dts, int UserID, int BizExceptionTypeID, int DocumentTypeID)
		{
//			AccountTable dTblAccount = (AccountTable) dts.Tables[AccountTable.TBL_ACCOUNT];
//			DocumentEntityTable dTblDocument = (DocumentEntityTable) dts.Tables[DocumentEntityTable.TBL_DOCUMENT_ENTITY];
//			EntityExceptionTable dTblException = (EntityExceptionTable) dts.Tables[EntityExceptionTable.TBL_ENTITY_EXCEPTION];
//			
//			if ((dTblDocument != null) && (dTblException != null))
//			{
//
//				bool IsRequired = false;
//				//Apply Validation on the account Level First
//				DataView dvException = new DataView(dTblException);
//				string sFilter = EntityExceptionTable.FLD_EXCEPTION_TYPE_ID + " = " + BizExceptionTypeID.ToString();
//				
//				dvException.RowFilter =	sFilter;
//				IsRequired = (dvException.Count != 0);
//			
//				dvException = null;
//
//				//If a Tax Exemption Form is required we have to add a row 
//				//in the DocumentEntityTable
//				if (IsRequired)
//				{
//					if (dtsAccount.AccountDocument.Rows.Count == 0)
//					{
//						DataRow newRow = dTblDocument.NewRow();
//						//Get the Account ID for document name
//						int AccID = 0;
//						if (dTblAccount != null)
//							AccID = Convert.ToInt32(dTblAccount.Rows[0][dataDef.FLD_PKID]);
//
//						newRow[DocumentEntityTable.FLD_ENTITY_ID] = AccID;
//						newRow[DocumentEntityTable.FLD_ENTITY_TYPE_ID] = EntityType.TYPE_ACCOUNT;
//						newRow[DocumentEntityTable.FLD_DOCUMENT_NAME] = "Tax Exemption Form: Account # " + AccID.ToString();
//						newRow[DocumentEntityTable.FLD_DOCUMENT_TYPE_ID] = DocumentType.TAX_EXEMPTION;
//						newRow[DocumentEntityTable.FLD_CREATE_USER_ID] = UserID;
//						dTblDocument.Rows.Add(newRow);
//
//						DocumentEntitySystem docSys = new DocumentEntitySystem();
//						docSys.Insert(dTblDocument);
//					}				
//				}
//				else
//				{				
//					//				if (dtsAccount.AccountDocument.Rows.Count == 0)
//					//				{
//					//					DataRow docRow = dtsAccount.AccountDocument.Rows[0];
//					//				}
//				}
//			}
			
		}

	}
}
