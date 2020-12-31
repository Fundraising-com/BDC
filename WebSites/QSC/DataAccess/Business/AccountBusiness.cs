namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.AccountTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.AccountData;
	using QSPFulfillment.DataAccess.Common;
	public enum AddressType {ShipTo,BillTo,CustomerRefund};

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class AccountBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		
		public AccountBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public AccountBusiness(bool AsMessageManager):base(AsMessageManager)
		{
		
		}
		public bool Delete(tableRef Table)
		{
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.Delete(Table,dataAccess);
		}
		public bool Insert(tableRef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Insert(Table,dataAccess);	
		}
		public bool UpdateBatch(tableRef Table)
		{			
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.UpdateBatch(Table,dataAccess);
		}
		public bool Update(tableRef Table)
		{
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.Update(Table, dataAccess);
		}
		public void SelectAll(DataTable Table)
		{
			try
			{
				dataAccess.SelectAll(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectFieldManager(DataTable Table,int CampaingID)
		{
			try
			{
				dataAccess.SelectFieldManager(Table,CampaingID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectAllFulfillmentHouse(DataTable Table)
		{
			try
			{
				dataAccess.SelectAllFulfillmentHouse(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectAllIncludInIncident(DataTable Table)
		{
			try
			{
				dataAccess.SelectAllIncludInIncident(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectAllPublisher(DataTable Table,int FulfillmentHouse)
		{
			try
			{
				dataAccess.SelectAllPublisher(Table,FulfillmentHouse);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllFieldManager(DataTable Table) 
		{
			try 
			{
				dataAccess.SelectAllFieldManager(Table);
			} 
			catch (Exception ex) 
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllProgramFundraising(DataTable Table) 
		{
			try 
			{
				dataAccess.SelectAllProgram(Table, 36001);
			} 
			catch (Exception ex) 
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllProgramIncentives(DataTable Table) 
		{
			try 
			{
				dataAccess.SelectAllProgram(Table, 36002);
				dataAccess.SelectAllProgram(Table, 36003);
			} 
			catch (Exception ex) 
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllCatalogCode(DataTable Table) 
		{
			try 
			{
				dataAccess.SelectAllCatalogCode(Table);
			} 
			catch (Exception ex) 
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		
		//----------------------------------------------------------------
		// Function Validate:
		//   Validates 
		// Returns:
		//   true if validation is successful 
		//   false if invalid fields exist 
		// Parameters:
		//   [in]  row: to be validated
		//   [out] row: If there are fields
		//              that contain errors they are individually marked.  
		//----------------------------------------------------------------
		protected override bool Validate(DataRow Row)
		{
			bool isValid = true;
			//Clear all errors
			Row.ClearErrors();
			if ((Row.RowState == DataRowState.Added) || (Row.RowState == DataRowState.Modified))
			{
				isValid = IsValid_RequiredFields(Row);
				isValid &= IsValids_FieldLength(Row);
			}
			return isValid;
		}
		//----------------------------------------------------------------
		// Function IsValid_RequiredField:
		//   Validates a specific tableRef field as Mandatory 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  Row: Row to be validated
		//----------------------------------------------------------------
		private bool IsValid_RequiredFields(DataRow Row)
		{
			bool IsValid = true;
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_CAMPAIGNSTART,"CampaignStart");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_CAMPAIGNEND,"CampaignEnd");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_ISNATIONAL,"IsNational");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_NUMBEROFCLASSROOMS,"NumberOfClassrooms");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_NUMBEROFSTUDENTS,"NumberOfStudents");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_COMMISSION,"Commission");
			if (!IsValid)
			{
				messageManager.ValidationExceptionType = ExceptionType.RequiredFields;
			}
			return IsValid;
		}
		//----------------------------------------------------------------
		// Function IsValid_FieldLength:
		//   Validates a specific tableRef field against his maxlength 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  Row: Row to be validated
		//   [in]  fieldName: field in to be validated
		//   [in]  maxLen: max length for the field
		//----------------------------------------------------------------
		private bool IsValids_FieldLength(DataRow Row)
		{
			bool isValid = true;
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_NAME,"", tableRef.FLD_NAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ADDRESS1,"", tableRef.FLD_ADDRESS1_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ADDRESS2,"", tableRef.FLD_ADDRESS2_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CITY,"", tableRef.FLD_CITY_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_STATE,"", tableRef.FLD_STATE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ZIP,"", tableRef.FLD_ZIP_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ZIPPLUSFOUR,"", tableRef.FLD_ZIPPLUSFOUR_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ATTNLINE,"", tableRef.FLD_ATTNLINE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_FIELDMANAGERNO,"", tableRef.FLD_FIELDMANAGERNO_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_FIELDMANAGERREGION,"", tableRef.FLD_FIELDMANAGERREGION_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COUNTY,"", tableRef.FLD_COUNTY_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COUNTYCODE,"", tableRef.FLD_COUNTYCODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_SCHOOLTYPE,"", tableRef.FLD_SCHOOLTYPE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PUBLICCATHOLIC,"", tableRef.FLD_PUBLICCATHOLIC_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_TAXEXEMPTNUMBER,"", tableRef.FLD_TAXEXEMPTNUMBER_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_UNITTYPE,"", tableRef.FLD_UNITTYPE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_NATIONALDISTRICT,"", tableRef.FLD_NATIONALDISTRICT_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_NATIONALFIELDMANAGER,"", tableRef.FLD_NATIONALFIELDMANAGER_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_SCHOOLDISTRICTNAME,"", tableRef.FLD_SCHOOLDISTRICTNAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_SHIPTOACCTORFM,"", tableRef.FLD_SHIPTOACCTORFM_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_AMFMIND,"", tableRef.FLD_AMFMIND_LENGTH);
			return isValid;
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}