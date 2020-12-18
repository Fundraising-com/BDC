namespace QSPForm.Business
{
	
	using System;
	using System.Data;
	using QSPForm.Common.DataDef;
	using QSPForm.Data;
	using dataDef = QSPForm.Common.DataDef.BusinessFieldTable;
	using dataAccessRef = QSPForm.Data.Business_field;
	using QSPForm.Common;
	
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     campaign.
	/// </summary>
	public class BusinessFieldSystem : BusinessSystem  
	{
		dataAccessRef objDataAccess;
		
		public BusinessFieldSystem()
		{
			objDataAccess = new dataAccessRef();
		}
		/// <summary>
		///     Validates and inserts a new Campaign
		///     <remarks>   
		///         Returns user data.  If there are fields that contain errors 
		///         that contain errors they are individually marked.  
		///     </remarks>   
		///     <param name="customer">dataDef to be inserted.</param>
		///     <retvalue>true if successful; otherwise, false.</retvalue>
		/// </summary>
		public bool Insert(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Insert(Table, new dataAccessRef());			
		}

		public bool Update(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Update(Table, new dataAccessRef());			
		}

		public bool UpdateBatch(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.UpdateBatch(Table, new dataAccessRef());			
		}

		public bool Delete(dataDef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Delete(Table, new dataAccessRef());			
		}

		//----------------------------------------------------------------
		// Function Validate:
		//   Validates campaign
		// Returns:
		//   true if validation is successful 
		//   false if invalid fields exist 
		// Parameters:
		//   [in]  row: CampaignTable to be validated
		//   [out] row: Returns Campaign data.  If there are fields
		//              that contain errors they are individually marked.  
		//----------------------------------------------------------------
		protected override bool Validate(DataRow row)
		{
			bool isValid = true;
            
			//Clear all errors
			row.ClearErrors();
			if ((row.RowState == DataRowState.Added) || (row.RowState == DataRowState.Modified))
			{
				//Apply Mandatory rules
				isValid = IsValid_RequiredFields(row);
				//Apply Maxlength rules
				isValid &= IsValid_FieldsLength(row);			
				//apply any other rules like unicity, integrity ...
				if (isValid)
					isValid = IsValid_Unicity(row);
				
			}
			else if (row.RowState == DataRowState.Deleted)
			{
				//Validation only for Delete Operation				
				isValid = IsValid_Integrity(row);
			}	
            
			return isValid;
		}
		
		//----------------------------------------------------------------
		// Function IsValid_FieldsLength:
		//   Validates a specific dataDef field against his maxlength 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  UserRow: Row of campaign from Campaign_TABLE to be validated
		//   [in]  fieldName: field in campaignData to be validated
		//   [in]  maxLen: max length for the field
		//----------------------------------------------------------------
		private bool IsValid_FieldsLength(DataRow campaignRow)
		{
			bool isValid = true;
			
			
			isValid &= IsValid_FieldLength(campaignRow, dataDef.FLD_FIELD_NAME, "Field name", 50);					
			
            
			return isValid;
		}


		//----------------------------------------------------------------
		// Function IsValid_RequiredField:
		//   Validates a specific dataDef field as Mandatory 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  UserRow: Row of campaign from Campaign_TABLE to be validated
		//----------------------------------------------------------------
		private bool IsValid_RequiredFields(DataRow row)
		{
			bool IsValid = true;

			//Campaign Name
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_FIELD_NAME, "Field name");		

			if (!IsValid)
			{
				messageManager.ValidationExceptionType = QSPFormExceptionType.RequiredFields;
			}
            
			return IsValid;
		}
        
		

		private bool IsValid_Unicity(DataRow row)
		{
			
			bool isValid = false;
			//
			// Ensure that User Name does not already exist in the database.
			//
			string fieldName = row[dataDef.FLD_FIELD_NAME].ToString().Trim();
			if (fieldName.Length >0)
			{
				dataDef dTblBizField = SelectAllByBizFieldName(fieldName);
            
				if (dTblBizField.Rows.Count >0)
				{
					DataRow duplRow = dTblBizField.Rows[0];
					//
					// Field is not unique - make sure the Field is not the same record
					//
					if ( row[dataDef.FLD_PKID].ToString() != duplRow[dataDef.FLD_PKID].ToString())
					{
						//
						// Field PKID does not match, so this would create a duplicate field
						//
						row.SetColumnError(dataDef.FLD_FIELD_NAME, messageManager.FormatErrorMessage(QSPFormMessage.VALMSG_UNICITY,new string[] {"The Field Name (" + fieldName  + ")","system"}));
						messageManager.ValidationExceptionType = QSPFormExceptionType.Unicity;
						return false;

					}
				}
			}
			
			return true;
		
		}

		private bool IsValid_Integrity(DataRow row)
		{
			
			bool isValid = false;
			//
			// Ensure that this Business Field is no longer user in the Business Rules of Forms.
			//
			BusinessRuleSystem bizSys = new BusinessRuleSystem();
			
			int BizFieldID = Convert.ToInt32(row[dataDef.FLD_PKID, DataRowVersion.Original]);
			BusinessRuleTable bizTable = bizSys.SelectAllByBusinessFieldID(BizFieldID);
			if (bizTable.Rows.Count != 0)
			{
				row.RejectChanges();
				string msg = messageManager.FormatErrorMessage(QSPFormMessage.VALMSG_INTEGRITY, new String[] {"Field (" + row[dataDef.FLD_FIELD_NAME].ToString() + ")","business rule"});
				row.SetColumnError(dataDef.FLD_PKID, msg); 
				messageManager.ValidationExceptionType = QSPFormExceptionType.Integrity;
				
				return false;

			}
					
			
			
			
			return true;
		
		}

		public dataDef SelectAll()
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAll();			
		}

		public dataDef SelectOne(int ID)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectOne(ID);			
		}

		public dataDef SelectAllByBizFieldName(string BizFieldName)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAllWfield_nameLogic(BizFieldName);			
		}

		public dataDef SelectAllByEntityTypeID(int EntityTypeID)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAllWentity_type_idLogic(EntityTypeID);			
		}

		public dataDef SelectAllByFormID(int FormID)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAllWform_idLogic(FormID);			
		}
		
        private void SetValidationField(ValidationTable dTblVal, int EntityTypeID)
        {
            dataAccessRef dal = new dataAccessRef();
            BusinessFieldTable dTblFields = new BusinessFieldTable();
            dTblFields = dal.SelectAllWentity_type_idLogic(EntityTypeID);
            foreach (DataRow fldRow in dTblFields)
            {
                string colName = fldRow[BusinessFieldTable.FLD_FIELD_NAME].ToString();
                int FieldType = Convert.ToInt32(fldRow[BusinessFieldTable.FLD_FIELD_TYPE_ID]);
                if (colName.Length > 0)
                {
                    if (!dTblVal.Columns.Contains(colName))
                    {
                        //Field Type
                        //	1	String 
                        //	2	Integer 
                        //	3	Double 
                        //	4	Date 
                        //	5	Currency 

                        switch (FieldType)
                        {
                            case 1: // String
                                {
                                    dTblVal.Columns.Add(colName, typeof(System.String));
                                    break;
                                }
                            case 2: //Integer
                                {
                                    dTblVal.Columns.Add(colName, typeof(System.Int32));
                                    break;
                                }
                            case 3: //Double
                                {
                                    dTblVal.Columns.Add(colName, typeof(System.Decimal));
                                    break;
                                }
                            case 4: //Date
                                {
                                    dTblVal.Columns.Add(colName, typeof(System.DateTime));
                                    break;
                                }
                            case 5: //Currency
                                {
                                    dTblVal.Columns.Add(colName, typeof(System.Decimal));
                                    break;
                                }
                            case 6: //Boolean
                                {
                                    dTblVal.Columns.Add(colName, typeof(System.Boolean));
                                    break;
                                }
                        }


                    }

                }
            }
        
        }


		public void SetValidationData(AccountData dtsAccount, int dataOperation)
		{	
			//IsNewAccount
			bool IsNewAccount = false;
			int AccountID = 0;
			int OrgTypeID = 0;
			DataRow accRow = dtsAccount.Account.Rows[0];
			DataRow campRow = dtsAccount.Campaign.Rows[0];
			DataRow orgRow = dtsAccount.Organization.Rows[0];
			AccountID = Convert.ToInt32(accRow[AccountTable.FLD_PKID]);
            ValidationTable accVal = dtsAccount.AccountValidation;

            //Refresh the DataTable Column Definition if there is a new field defined
            SetValidationField(accVal, EntityType.TYPE_ACCOUNT);

			//Account Record Status
			if (dataOperation == DataOperation.INSERT)
				IsNewAccount = true;
			
			//IsApproved
			bool IsApproved = false;
			int creditAppTypeID = 0;
			if (AccountID > 0)
			{
				CreditApplicationTable dTblCrdApp = new CreditApplicationTable();
				dTblCrdApp = dtsAccount.CreditApplication;
				if (dTblCrdApp.Rows.Count >0)
				{
					DataRow crdRow = dTblCrdApp.Rows[0];
					//Account Status
					if (!crdRow.IsNull(CreditApplicationTable.FLD_APPROVED))
					{
						IsApproved = Convert.ToBoolean(crdRow[CreditApplicationTable.FLD_APPROVED]);
					}
					if (!crdRow.IsNull(CreditApplicationTable.FLD_TYPE_ID))
					{
						creditAppTypeID = Convert.ToInt32(crdRow[CreditApplicationTable.FLD_TYPE_ID]);
					}

				}
			}

			//Organization type
			if (dtsAccount.AccountX.Rows.Count > 0)
			{	
				DataRow accxRow = dtsAccount.AccountX.Rows[0];
				if (!accxRow.IsNull(AccountXTable.FLD_ORG_TYPE_ID))
					OrgTypeID = Convert.ToInt32(accxRow[AccountXTable.FLD_ORG_TYPE_ID]);
			}
			else
			{
				if (!orgRow.IsNull(OrganizationTable.FLD_ORG_TYPE_ID))
					OrgTypeID = Convert.ToInt32(orgRow[OrganizationTable.FLD_ORG_TYPE_ID]);
			}

			//Nb Inactive Month and
			int NbInactiveMonth = 0;

			DateTime CurrentDate = DateTime.Now;
			DateTime LastOrderDate = DateTime.MinValue;
			if (!accRow.IsNull(AccountTable.FLD_LAST_ORDER_DATE))
			{
				LastOrderDate = Convert.ToDateTime(accRow[AccountTable.FLD_LAST_ORDER_DATE]);
			}			
			CommonSystem comSys = new CommonSystem();
			NbInactiveMonth = comSys.GetNbOfMonth(LastOrderDate, CurrentDate);

			
			if (accVal.Rows.Count ==0)
			{				
				accVal.Rows.Add(accVal.NewRow());							
			}
			DataRow accValRow  = accVal.Rows[0];
			accValRow[ValidationTable.FLD_IS_NEW_ACCOUNT] = IsNewAccount;
            accValRow[ValidationTable.FLD_IS_FIRST_TIME_PROCESS] = false;
			accValRow[ValidationTable.FLD_ORG_TYPE_ID] = OrgTypeID;
            accValRow[ValidationTable.FLD_ACCOUNT_FM_ID] = accRow[AccountTable.FLD_FM_ID];
			accValRow[ValidationTable.FLD_ACCOUNT_STATUS_ID] = accRow[AccountTable.FLD_ACCOUNT_STATUS_ID];
			accValRow[ValidationTable.FLD_PROGRAM_TYPE_ID] = campRow[CampaignTable.FLD_PROG_TYPE_ID];
			accValRow[ValidationTable.FLD_IS_CREDIT_APP_APPROVED] = IsApproved;	
			accValRow[ValidationTable.FLD_CREDIT_APPLICATION_TYPE_ID] = creditAppTypeID;	
			accValRow[ValidationTable.FLD_NB_INACTIVE_MONTH] = NbInactiveMonth;	
			//Tax Exemption information
			accValRow[ValidationTable.FLD_IS_TAX_EXEMPTION_NO_ENTERED] = (accRow[AccountTable.FLD_TAX_EXEMPTION_NO].ToString().Trim().Length > 0);
			AccountSystem accSys = new AccountSystem();			
			accValRow[ValidationTable.FLD_IS_TAX_EXEMPTED] = accSys.IsTaxExempted(dtsAccount);			
			//Tax Exemption Document
			DocumentEntitySystem docSys = new DocumentEntitySystem();
			DataRow docRow = docSys.FindRow(dtsAccount.AccountDocument, EntityType.TYPE_ACCOUNT, AccountID, DocumentType.TAX_EXEMPTION);
			if (docRow != null)
			{
				bool received = false;
				if (!docRow.IsNull(DocumentEntityTable.FLD_APPROVED))
					received = Convert.ToBoolean(docRow[DocumentEntityTable.FLD_APPROVED]);
				accValRow[ValidationTable.FLD_IS_TAX_EXEMPTION_FORM_RECEIVED] = received;
			}

		}

        public void SetValidationData(ProgramAgreementData dtsProgramAgreement, int dataOperation)
        {
            //IsNewProgramAgreement
            bool IsNewProgramAgreement = false;
            int ProgramAgreementID = 0;
            int OrgTypeID = 0;
            DataRow prgRow = dtsProgramAgreement.ProgramAgreement.Rows[0];
            DataRow campRow = dtsProgramAgreement.Campaign.Rows[0];
            DataRow prgCampRow = dtsProgramAgreement.ProgramAgreementCampaign.Rows[0];
            ProgramAgreementID = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_PKID]);
            ValidationTable prgVal = dtsProgramAgreement.ProgramAgreementValidation;

            //Refresh the DataTable Column Definition if there is a new field defined
            SetValidationField(prgVal, EntityType.TYPE_PROGRAM_AGREEMENT);

            //ProgramAgreement Record Status
            if (dataOperation == DataOperation.INSERT)
                IsNewProgramAgreement = true;

            //IsApproved
            bool IsApproved = false;
            //int creditAppTypeID = 0;
            //if (ProgramAgreementID > 0)
            //{
            //    CreditApplicationTable dTblCrdApp = new CreditApplicationTable();
            //    dTblCrdApp = dtsProgramAgreement.CreditApplication;
            //    if (dTblCrdApp.Rows.Count > 0)
            //    {
            //        DataRow crdRow = dTblCrdApp.Rows[0];
            //        //ProgramAgreement Status
            //        if (!crdRow.IsNull(CreditApplicationTable.FLD_APPROVED))
            //        {
            //            IsApproved = Convert.ToBoolean(crdRow[CreditApplicationTable.FLD_APPROVED]);
            //        }
            //        if (!crdRow.IsNull(CreditApplicationTable.FLD_TYPE_ID))
            //        {
            //            creditAppTypeID = Convert.ToInt32(crdRow[CreditApplicationTable.FLD_TYPE_ID]);
            //        }

            //    }
            //}

            

            if (prgVal.Rows.Count == 0)
            {
                prgVal.Rows.Add(prgVal.NewRow());
            }
            DataRow prgValRow = prgVal.Rows[0];
            prgValRow[ValidationTable.FLD_IS_NEW_PROGRAM_AGREEMENT] = IsNewProgramAgreement;
            prgValRow[ValidationTable.FLD_ACCOUNT_FM_ID] = campRow[CampaignTable.FLD_FM_ID];
            prgValRow[ValidationTable.FLD_PROGRAM_AGREEMENT_STATUS_ID] = prgRow[ProgramAgreementTable.FLD_PROGRAM_AGREEMENT_STATUS_ID];
            prgValRow[ValidationTable.FLD_PROGRAM_TYPE_ID] = campRow[CampaignTable.FLD_PROG_TYPE_ID];
            prgValRow[ValidationTable.FLD_IS_CREDIT_APP_APPROVED] = IsApproved;
            //prgValRow[ValidationTable.FLD_CREDIT_APPLICATION_TYPE_ID] = creditAppTypeID;
            

        }

		public void SetValidationData(CreditApplicationData dts, int dataOperation)
		{	
			//IsNewAccount
			bool IsNewAccount = false;
			DataRow accRow = dts.Account.Rows[0];			
			//Account Status
			IsNewAccount = false; //should be an update 

			bool IsNewCredit = false;
			//Data Operation
			if (dataOperation == DataOperation.INSERT)
				IsNewCredit = true;

			//IsApproved
			bool IsApproved = false;
			DataRow crdRow = dts.CreditApplication.Rows[0];
			int creditAppID = Convert.ToInt32(crdRow[CreditApplicationTable.FLD_PKID]);

			//Account Status
			if (!crdRow.IsNull(CreditApplicationTable.FLD_APPROVED))
			{
				IsApproved = Convert.ToBoolean(crdRow[CreditApplicationTable.FLD_APPROVED]);
			}
			int creditAppTypeID = 0;
			if (!crdRow.IsNull(CreditApplicationTable.FLD_TYPE_ID))
			{
				creditAppTypeID = Convert.ToInt32(crdRow[CreditApplicationTable.FLD_TYPE_ID]);
			}
			ValidationTable dTblVal = dts.CreditValidation;

            //Refresh the DataTable Column Definition if there is a new field defined
            SetValidationField(dTblVal, EntityType.TYPE_CREDIT_APPLICATION);

			if (dTblVal.Rows.Count ==0)
			{				
				dTblVal.Rows.Add(dTblVal.NewRow());							
			}
			DataRow crdValRow  = dTblVal.Rows[0];
			crdValRow[ValidationTable.FLD_IS_NEW_ACCOUNT] = IsNewAccount;		
			crdValRow[ValidationTable.FLD_IS_NEW_CREDIT_APP] = IsNewCredit;	
			crdValRow[ValidationTable.FLD_IS_CREDIT_APP_APPROVED] = IsApproved;	
			crdValRow[ValidationTable.FLD_CREDIT_APPLICATION_TYPE_ID] = creditAppTypeID;
			crdValRow[ValidationTable.FLD_ACCOUNT_STATUS_ID] = accRow[AccountTable.FLD_ACCOUNT_STATUS_ID];
			//crdValRow[ValidationTable.FLD_PROGRAM_TYPE_ID] = campRow[CampaignTable.FLD_PROG_TYPE_ID];
			
			//Credit Application Document
			DocumentEntitySystem docSys = new DocumentEntitySystem();
			DataRow docRow = docSys.FindRow(dts.CreditDocument, EntityType.TYPE_CREDIT_APPLICATION, creditAppID, DocumentType.CREDIT_APPLICATION);
			if (docRow != null)
			{
				bool received = false;
				if (!docRow.IsNull(DocumentEntityTable.FLD_APPROVED))
					received = Convert.ToBoolean(docRow[DocumentEntityTable.FLD_APPROVED]);
				crdValRow[ValidationTable.FLD_IS_CREDIT_APPLICATION_FORM_RECEIVED] = received;
			}
			
		
		}

        public void SetValidationData(OrderData dtsOrder, AccountData dtsAccount, FormData dtsForm, int dataOperation, Data.ConnectionProvider connProvider)
		{
			//Nb Day Lead Time
			ShipmentGroupTable dtblShipmentGroup = dtsOrder.ShipmentGroup;
			OrderHeaderTable dtblOrderHeader = dtsOrder.OrderHeader;
            FormSectionTable dtblFormSection = dtsForm.FormSection;
			DateTime deliveryDate = DateTime.Today;
			DateTime orderDate = DateTime.Today;
			DateTime supply_deliveryDate = DateTime.Today;
			String sCatalogItemCodeList = "";

			int NbDayLeadTime = 3;
			int SupplyNbDayLeadTime = 3;
			int deliveryMethodID = 1;
			bool IsNewOrder = false;
			
			BusinessCalendarSystem calSys = new BusinessCalendarSystem();	

			//Order Status
			if (dataOperation == DataOperation.INSERT)
				IsNewOrder = true;

			if (dtblOrderHeader.Rows.Count >0)
			{
				DataRow ordRow  = dtblOrderHeader.Rows[0];
				if (!ordRow.IsNull(OrderHeaderTable.FLD_ORDER_DATE))
					orderDate = Convert.ToDateTime(ordRow[OrderHeaderTable.FLD_ORDER_DATE]);
			}
            //Get The Account History 
            //we cannot set the value before cause its based on a value of the business rules
            Decimal accHistInterval_totalAmount = 0;
            Decimal accHistInterval_totalAdjAmount = 0;
            Decimal accHistInterval_maxTotalAmount = 0;
            int NbDay = 0;
            string sValue = dtsForm.BusinessRule.GetFormPropertyValueString(FormProperty.ACCOUNT_HISTORY_INTERVAL_NB_DAY);
            if (sValue.Length > 0)
                NbDay = Convert.ToInt32(sValue);

            int AccountID = Convert.ToInt32(dtsAccount.Account.Rows[0][AccountTable.FLD_PKID]);
            int OrderID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);
            Data.Account accDataAccess = new Data.Account();
            if (connProvider != null)
                accDataAccess.MainConnectionProvider = connProvider;

            if (NbDay > 0)
            {
                DataTable tblAccount = accDataAccess.SummarizeWaccount_idLogic_Winterval(AccountID, NbDay, OrderID);
                if (tblAccount.Rows.Count > 0)
                {
                    DataRow row = tblAccount.Rows[0];
                    accHistInterval_totalAmount = Convert.ToDecimal(row["total_amount"]);
                    accHistInterval_totalAdjAmount = Convert.ToDecimal(row["total_adj_amount"]);
                    accHistInterval_maxTotalAmount = Convert.ToDecimal(row["max_total_amount"]);

                }
            }
            
			if (dtblShipmentGroup.Rows.Count >0)
			{
				//Delivery Date and Nb Day Lead Time
				DataRow shipRow  = dtblShipmentGroup.Rows[0];
				if (!shipRow.IsNull(ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE))
				{				
					deliveryDate = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_REQUESTED_DELIVERY_DATE]);
					NbDayLeadTime = calSys.GetNbDayLeadTime(orderDate, deliveryDate);
					
				}
				//Supply Delivery Date and Supply Nb Day Lead Time
				if (!shipRow.IsNull(ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE))
				{				
					supply_deliveryDate = Convert.ToDateTime(shipRow[ShipmentGroupTable.FLD_SUPPLY_REQUESTED_DELIVERY_DATE]);
					SupplyNbDayLeadTime = calSys.GetNbDayLeadTime(orderDate, supply_deliveryDate);
					
				}
				//Delivery Method 
				if (!shipRow.IsNull(ShipmentGroupTable.FLD_DELIVERY_METHOD_ID))
				{
					deliveryMethodID = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_DELIVERY_METHOD_ID]);
				}
			}
            ////Tax Exemption

            //OrderSystem ordSys = new OrderSystem();
            //decimal taxRate = ordSys.CalculateTax(dtsOrder, dtsAccount, 0);
		
			//IsNewAccount
			bool IsNewAccount = false;
            bool IsFirstTimeProcess = false;			
			DataRow ordrow = dtsOrder.OrderHeader.Rows[0];
			DataRow accRow = dtsAccount.Account.Rows[0];
			DataRow campRow = dtsAccount.Campaign.Rows[0];
			DataRow orgRow = dtsAccount.Organization.Rows[0];
			//Account Status
			IsNewAccount = false; //Should never be new		
            //Organization type
            int OrgTypeID = 0;
            if (dtsAccount.AccountX.Rows.Count > 0)
            {
                DataRow accxRow = dtsAccount.AccountX.Rows[0];
                if (!accxRow.IsNull(AccountXTable.FLD_ORG_TYPE_ID))
                    OrgTypeID = Convert.ToInt32(accxRow[AccountXTable.FLD_ORG_TYPE_ID]);
            }
            else
            {
                if (!orgRow.IsNull(OrganizationTable.FLD_ORG_TYPE_ID))
                    OrgTypeID = Convert.ToInt32(orgRow[OrganizationTable.FLD_ORG_TYPE_ID]);
            }
			ValidationTable ordVal = dtsOrder.OrderValidation;
            //Refresh the DataTable Column Definition if there is a new field defined
            SetValidationField(ordVal, EntityType.TYPE_ORDER_BILLING);
            ordVal.Rows.Clear();
            //Determine the Number of section to create
            int NbSection = 0;
            //if (dtsForm.FormSection.IsContainFormSectionType(
            //The first Standard Product is mandatory

            //get Cookie dough product quantities
            QSPForm.Business.ProductSystem prodSys = new QSPForm.Business.ProductSystem();
            //check for product type cookies, poduct_type_id = 6
            QSPForm.Common.DataDef.ProductTable products = prodSys.SelectAllByProductType(6);

            for (int iIndex = 0; iIndex <= 3; iIndex++)
            {
                DataRow ordValRow = ordVal.GetValidationRow(QSPForm.Common.FormSectionType.STANDARD_PRODUCT, iIndex);
                if (ordValRow == null)
                {
                    DataRow newValRow = ordVal.NewRow();
                    newValRow[ValidationTable.FLD_FORM_SECTION_TYPE_ID] = QSPForm.Common.FormSectionType.STANDARD_PRODUCT;
                    if (iIndex > 0)
                    {
                        newValRow[ValidationTable.FLD_FORM_SECTION_NUMBER] = iIndex;
                    }
                    ordVal.Rows.Add(newValRow);
                    ordValRow = newValRow;
                }
                ordValRow[ValidationTable.FLD_NB_DAY_LEAD_TIME] = NbDayLeadTime;
                ordValRow[ValidationTable.FLD_DELIVERY_DATE] = deliveryDate;
                ordValRow[ValidationTable.FLD_IS_NEW_ACCOUNT] = IsNewAccount;
                ordValRow[ValidationTable.FLD_IS_NEW_ORDER] = IsNewOrder;
                ordValRow[ValidationTable.FLD_IS_FIRST_TIME_PROCESS] = IsFirstTimeProcess;
                ordValRow[ValidationTable.FLD_DELIVERY_METHOD_ID] = deliveryMethodID;
                ordValRow[ValidationTable.FLD_ORDER_TYPE_ID] = ordrow[OrderHeaderTable.FLD_ORDER_TYPE_ID];
                ordValRow[ValidationTable.FLD_ORDER_STATUS_ID] = ordrow[OrderHeaderTable.FLD_ORDER_STATUS_ID];
                ordValRow[ValidationTable.FLD_ACCOUNT_STATUS_ID] = accRow[AccountTable.FLD_ACCOUNT_STATUS_ID];
                ordValRow[ValidationTable.FLD_ORDER_FM_ID] = ordrow[OrderHeaderTable.FLD_FM_ID];
                ordValRow[ValidationTable.FLD_ACCOUNT_FM_ID] = accRow[AccountTable.FLD_FM_ID];
                ordValRow[ValidationTable.FLD_PROGRAM_TYPE_ID] = campRow[CampaignTable.FLD_PROG_TYPE_ID];
                ordValRow[ValidationTable.FLD_ORG_TYPE_ID] = OrgTypeID;
                ordValRow[ValidationTable.FLD_ACCOUNT_HISTORY_INTERVAL_NB_DAY] = NbDay;
                ordValRow[ValidationTable.FLD_ACCOUNT_HISTORY_INTERVAL_TOTAL_AMOUNT] = accHistInterval_totalAdjAmount;
                ordValRow[ValidationTable.FLD_ACCOUNT_HISTORY_INTERVAL_MAX_TOTAL_AMOUNT] = accHistInterval_maxTotalAmount;
			
                ordValRow[ValidationTable.FLD_CATALOG_ITEM_CODE] = dtsOrder.OrderDetail.CatalogItemCodeList;
                ordValRow[ValidationTable.FLD_CATALOG_ITEM_NB_DAY_LEAD_TIME] = dtsOrder.OrderDetail.MaxNbDayLeadTime;

                //Order Total and Other things
                ordValRow[ValidationTable.FLD_TOTAL_QUANTITY] = dtsOrder.OrderDetail.GetTotalQuantity(QSPForm.Common.FormSectionType.STANDARD_PRODUCT, iIndex);//TotalQty;	
                ordValRow[ValidationTable.FLD_TOTAL_CD_QUANTITY] = dtsOrder.OrderDetail.GetTotalCDQuantity(QSPForm.Common.FormSectionType.STANDARD_PRODUCT, iIndex, products);//TotalCDQty;	
                ordValRow[ValidationTable.FLD_TOTAL_AMOUNT] = dtsOrder.OrderDetail.GetTotalAmount(QSPForm.Common.FormSectionType.STANDARD_PRODUCT, iIndex);//TotalAmount;
                //MAX Minimum Nb Day Lead-Time
                ordValRow[ValidationTable.FLD_ORDER_MAX_MIN_NB_DAY_LEAD_TIME] = dtsOrder.OrderDetail.GetMaxNbDayLeadTime(QSPForm.Common.FormSectionType.STANDARD_PRODUCT);

            
            }

            //The Other Product Sections
            for (int iIndex = 0; iIndex <= 3; iIndex++)
            {
                DataRow ordValRow = ordVal.GetValidationRow(QSPForm.Common.FormSectionType.OTHER_PRODUCT, iIndex);
                if (ordValRow == null)
                {
                    DataRow newValRow = ordVal.NewRow();
                    newValRow[ValidationTable.FLD_FORM_SECTION_TYPE_ID] = QSPForm.Common.FormSectionType.OTHER_PRODUCT;
                    if (iIndex > 0)
                    {
                        newValRow[ValidationTable.FLD_FORM_SECTION_NUMBER] = iIndex;
                    }
                    ordVal.Rows.Add(newValRow);
                    ordValRow = newValRow;
                }
                ordValRow[ValidationTable.FLD_NB_DAY_LEAD_TIME] = NbDayLeadTime;
                ordValRow[ValidationTable.FLD_DELIVERY_DATE] = deliveryDate;
                ordValRow[ValidationTable.FLD_IS_NEW_ACCOUNT] = IsNewAccount;
                ordValRow[ValidationTable.FLD_IS_NEW_ORDER] = IsNewOrder;
                ordValRow[ValidationTable.FLD_DELIVERY_METHOD_ID] = deliveryMethodID;
                ordValRow[ValidationTable.FLD_ORDER_TYPE_ID] = ordrow[OrderHeaderTable.FLD_ORDER_TYPE_ID];
                ordValRow[ValidationTable.FLD_ORDER_STATUS_ID] = ordrow[OrderHeaderTable.FLD_ORDER_STATUS_ID];
                ordValRow[ValidationTable.FLD_ACCOUNT_STATUS_ID] = accRow[AccountTable.FLD_ACCOUNT_STATUS_ID];
                ordValRow[ValidationTable.FLD_ORDER_FM_ID] = ordrow[OrderHeaderTable.FLD_FM_ID];
                ordValRow[ValidationTable.FLD_ACCOUNT_FM_ID] = accRow[AccountTable.FLD_FM_ID];
                ordValRow[ValidationTable.FLD_PROGRAM_TYPE_ID] = campRow[CampaignTable.FLD_PROG_TYPE_ID];
                ordValRow[ValidationTable.FLD_ORG_TYPE_ID] = OrgTypeID;

                ordValRow[ValidationTable.FLD_CATALOG_ITEM_CODE] = dtsOrder.OrderDetail.CatalogItemCodeList;
                ordValRow[ValidationTable.FLD_CATALOG_ITEM_NB_DAY_LEAD_TIME] = dtsOrder.OrderDetail.MaxNbDayLeadTime;

                //Order Total and Other things
                ordValRow[ValidationTable.FLD_TOTAL_QUANTITY] = dtsOrder.OrderDetail.GetTotalQuantity(QSPForm.Common.FormSectionType.OTHER_PRODUCT, iIndex);//TotalQty;	
                ordValRow[ValidationTable.FLD_TOTAL_CD_QUANTITY] = dtsOrder.OrderDetail.GetTotalCDQuantity(QSPForm.Common.FormSectionType.STANDARD_PRODUCT, iIndex, products);//TotalCDQty;	
                ordValRow[ValidationTable.FLD_TOTAL_AMOUNT] = dtsOrder.OrderDetail.GetTotalAmount(QSPForm.Common.FormSectionType.OTHER_PRODUCT, iIndex);//TotalAmount;
                //MAX Minimum Nb Day Lead-Time
                ordValRow[ValidationTable.FLD_ORDER_MAX_MIN_NB_DAY_LEAD_TIME] = dtsOrder.OrderDetail.GetMaxNbDayLeadTime(QSPForm.Common.FormSectionType.STANDARD_PRODUCT);


            }

            //The Supply Product Sections
            for (int iIndex = 0; iIndex <= 3; iIndex++)
            {
                DataRow ordValRow = ordVal.GetValidationRow(QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, iIndex);
                if (ordValRow == null)
                {
                    DataRow newValRow = ordVal.NewRow();
                    newValRow[ValidationTable.FLD_FORM_SECTION_TYPE_ID] = QSPForm.Common.FormSectionType.SUPPLY_PRODUCT;
                    if (iIndex > 0)
                    {
                        newValRow[ValidationTable.FLD_FORM_SECTION_NUMBER] = iIndex;
                    }
                    ordVal.Rows.Add(newValRow);
                    ordValRow = newValRow;
                }
                ordValRow[ValidationTable.FLD_NB_DAY_LEAD_TIME] = SupplyNbDayLeadTime;
                ordValRow[ValidationTable.FLD_DELIVERY_DATE] = supply_deliveryDate;
                ordValRow[ValidationTable.FLD_IS_NEW_ACCOUNT] = IsNewAccount;
                ordValRow[ValidationTable.FLD_IS_NEW_ORDER] = IsNewOrder;
                ordValRow[ValidationTable.FLD_DELIVERY_METHOD_ID] = deliveryMethodID;
                ordValRow[ValidationTable.FLD_ORDER_TYPE_ID] = ordrow[OrderHeaderTable.FLD_ORDER_TYPE_ID];
                ordValRow[ValidationTable.FLD_ORDER_STATUS_ID] = ordrow[OrderHeaderTable.FLD_ORDER_STATUS_ID];
                ordValRow[ValidationTable.FLD_ACCOUNT_STATUS_ID] = accRow[AccountTable.FLD_ACCOUNT_STATUS_ID];
                ordValRow[ValidationTable.FLD_ORDER_FM_ID] = ordrow[OrderHeaderTable.FLD_FM_ID];
                ordValRow[ValidationTable.FLD_ACCOUNT_FM_ID] = accRow[AccountTable.FLD_FM_ID];
                ordValRow[ValidationTable.FLD_PROGRAM_TYPE_ID] = campRow[CampaignTable.FLD_PROG_TYPE_ID];
                ordValRow[ValidationTable.FLD_ORG_TYPE_ID] = OrgTypeID;

                //ordValRow[ValidationTable.FLD_CATALOG_ITEM_CODE] = dtsOrder.OrderDetail.CatalogItemCodeList;
                //ordValRow[ValidationTable.FLD_CATALOG_ITEM_NB_DAY_LEAD_TIME] = dtsOrder.OrderDetail.MaxNbDayLeadTime;

                //ordValRow[ValidationTable.FLD_IS_TAX_EXEMPTED] = (taxRate == 0);

                //Order Total and Other things
                ordValRow[ValidationTable.FLD_TOTAL_QUANTITY] = dtsOrder.OrderSupply.GetTotalQuantity(QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, iIndex);//TotalQty;	
                ordValRow[ValidationTable.FLD_TOTAL_CD_QUANTITY] = dtsOrder.OrderDetail.GetTotalCDQuantity(QSPForm.Common.FormSectionType.STANDARD_PRODUCT, iIndex, products);//TotalCDQty;	
                ordValRow[ValidationTable.FLD_TOTAL_AMOUNT] = dtsOrder.OrderSupply.GetTotalAmount(QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, iIndex);//TotalAmount;
                //MAX Minimum Nb Day Lead-Time
                //ordValRow[ValidationTable.FLD_ORDER_MAX_MIN_NB_DAY_LEAD_TIME] = dtsOrder.OrderDetail.GetMaxNbDayLeadTime(QSPForm.Common.FormSectionType.STANDARD_PRODUCT);


            }

            ordVal.AcceptChanges();	
		
		}


		


	}
}
