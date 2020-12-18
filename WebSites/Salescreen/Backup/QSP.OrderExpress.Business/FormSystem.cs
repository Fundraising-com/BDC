namespace QSPForm.Business
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    using dataDef = QSPForm.Common.DataDef.FormTable;
    using dataAccessRef = QSPForm.Data.Form;

    using QSPForm.Common;
    using QSPForm.Common.Entity;
    using QSPForm.Common.DataDef;
    using QSPForm.Data;

    using LinqContext = QSP.OrderExpress.Business.Context;
    using LinqEntity = QSP.OrderExpress.Business.Entity;
    using QSP.OrderExpress.Common.Enum;

    /// <summary>
	///     This class contains the business rules that are used for a 
	///     campaign.
	/// </summary>
	public class FormSystem : BusinessSystem
    {

        #region Version 2 code

        public List<LinqEntity.Form> GetForms()
        {
            List<LinqEntity.Form> result = new List<LinqEntity.Form>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from f in db.Forms
                      where f.IsDeleted == false
                      select f
                      ).ToList();

            return result;
        }
        public List<LinqEntity.Form> GetEnabledForms()
        {
            List<LinqEntity.Form> result = new List<LinqEntity.Form>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from f in db.Forms
                      where f.IsDeleted == false
                        && f.IsEnabled == true
                        && f.IsBaseForm == false
                      select f
                      ).ToList();

            return result;
        }
        public List<LinqEntity.Form> GetEnabledOrderForms()
        {
            List<LinqEntity.Form> result = new List<LinqEntity.Form>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from f in db.Forms
                      where f.IsDeleted == false
                        && f.IsEnabled == true
                        && f.IsBaseForm == false
                        && f.EntityTypeId == (int)EntityTypeEnum.Order
                      select f
                      ).ToList();

            return result;
        }
        public List<LinqEntity.Form> GetEnabledPAForms()
        {
            List<LinqEntity.Form> result = new List<LinqEntity.Form>();

            LinqContext.OrderExpressDataContext db = new LinqContext.OrderExpressDataContext();

            result = (from f in db.Forms
                      where f.IsDeleted == false
                        && f.IsEnabled == true
                        && f.IsBaseForm == false
                        && f.EntityTypeId == (int)EntityTypeEnum.ProgramAgreement
                      select f
                      ).ToList();

            return result;
        }
        public LinqEntity.Form GetForm(int formId)
        {
            LinqEntity.Form result = new LinqEntity.Form();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from f in context.Forms
                        where f.FormId == formId
                        select f;

            result = query.SingleOrDefault();

            return result;
        }

        #endregion

        #region Refactored methods

        public LinqEntity.Form SelectById(int formId)
        {
            LinqEntity.Form result = new LinqEntity.Form();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from f in context.Forms
                        where f.FormId == formId
                        select f;

            result = query.SingleOrDefault();

            return result;
        }
        public List<LinqEntity.Form> SelectByEntityType(EntityTypeEnum entityType)
        {
            List<LinqEntity.Form> result = new List<LinqEntity.Form>();

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            var query = from f in context.Forms
                        where f.IsDeleted == false
                            && f.IsEnabled == true
                            && f.IsBaseForm == false
                            && f.EntityTypeId == (int)entityType
                        orderby f.FormName
                        select f;


            result = query.ToList<LinqEntity.Form>();

            return result;
        }
        public List<LinqEntity.Form> SelectByEntityTypeAndUserPermissions(EntityTypeEnum entityType, int userId, int accountId)
        {
            List<LinqEntity.Form> result = new List<LinqEntity.Form>();

            QSPForm.Business.UserSystem userSystem = new QSPForm.Business.UserSystem();
            QSPForm.Business.PostalAddressSystem postalAddressSystem = new QSPForm.Business.PostalAddressSystem();
            QSPForm.Business.FormPermissionSystem formPermissionSystem = new QSPForm.Business.FormPermissionSystem();
            QSPForm.Business.FormPermissionRegionSystem formPermissionRegionSystem = new QSPForm.Business.FormPermissionRegionSystem();

            List<int> excludedForms = new List<int>();
            List<LinqEntity.Form> formList = this.SelectByEntityType(entityType);
            List<LinqEntity.UserRole> userRoles = userSystem.GetUserRoleListFromUser(userId);
            List<LinqEntity.FormPermission> formPermissionList = formPermissionSystem.SelectByUserRoles(userRoles);
            List<LinqEntity.PostalAddress> postalAddressList = postalAddressSystem.GetAddressByAccount(accountId, PostalAddressTypeEnum.Shipping); //2010-Aug-18 : Changed from billing to shipping, requested by Teresa.

            #region Show only readable/editable forms by role.

            // Form is displayed if entry does not exist. (Allowed by default)
            foreach (LinqEntity.Form form in formList)
            {
                foreach (LinqEntity.FormPermission formPermission in formPermissionList)
                {
                    if (!formPermission.FormId.HasValue || formPermission.FormId == form.FormId)
                    {
                        if (!formPermission.AllowRead || !formPermission.AllowWrite)
                        {
                            if (!excludedForms.Contains(form.FormId))
                            {
                                excludedForms.Add(form.FormId);
                            }
                        }
                        else
                        {
                            if (excludedForms.Contains(form.FormId))
                            {
                                excludedForms.Remove(form.FormId);
                            }
                        }
                    }
                }
            }

            formList = this.ExcludeForms(formList, excludedForms);
            excludedForms.Clear();

            #endregion

            #region Show only readable/editable forms by billing region

            // Form is displayed if entry does not exist. (Allowed by default)
            if (postalAddressList.Count > 0)
            {
                LinqEntity.PostalAddress postalAddress = postalAddressList[0];

                string zipCode = postalAddress.Zip.Trim();
                if (postalAddress.Zip.Trim().Length > 5)
                {
                    zipCode = postalAddress.Zip.Trim().Substring(0, 5);
                }

                foreach (LinqEntity.Form form in formList)
                {
                    List<LinqEntity.FormPermissionRegion> formPermissionRegionList = formPermissionRegionSystem.SelectByZip(form.FormId, zipCode);

                    foreach (LinqEntity.FormPermissionRegion formPermissionRegion in formPermissionRegionList)
                    {
                        if (!formPermissionRegion.FormId.HasValue || formPermissionRegion.FormId.Value == form.FormId)
                        {
                            if (!formPermissionRegion.AllowRead || !formPermissionRegion.AllowWrite)
                            {
                                if (!excludedForms.Contains(form.FormId))
                                {
                                    excludedForms.Add(form.FormId);
                                }
                            }
                            else
                            {
                                if (excludedForms.Contains(form.FormId))
                                {
                                    excludedForms.Remove(form.FormId);
                                }
                            }
                        }
                    }
                }
            }

            formList = this.ExcludeForms(formList, excludedForms);
            excludedForms.Clear();

            #endregion

            result = formList;

            return result;
        }
        private List<LinqEntity.Form> ExcludeForms(List<LinqEntity.Form> formlist, List<int> excludedFormIdList)
        {
            List<LinqEntity.Form> result = new List<LinqEntity.Form>();

            foreach (LinqEntity.Form form in formlist)
            {
                if (!excludedFormIdList.Contains(form.FormId))
                {
                    result.Add(form);
                }
            }

            return result;
        }

        #endregion

        #region Version 1 code

        #region Form System

        dataAccessRef objDataAccess;
		
		public FormSystem()
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
				
				
			}
//			//Validation only for Delete Operation
//			else if (row.RowState == DataRowState.Deleted)
//			{
//				
//				//isValid = IsValid_Integrity(row);
//			}	
            
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
			
			
			isValid &= IsValid_FieldLength(campaignRow, dataDef.FLD_FORM_NAME, "Campaign name", 50);					
			
            
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
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_FORM_CODE, "Form Code");
			IsValid &= IsValid_RequiredField(row, dataDef.FLD_FORM_NAME, "Form Name");			

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
//			dataDef existing = GetCustomerByEmail(row[UserData.FLD_USER_NAME].ToString());
//            
//			if ( null != existingUser )
//			{
//				//
//				// Email is not unique - make sure the email address belongs this customer
//				//
//				if ( userRow[UserData.FLD_PKID].ToString() != existingUser.Tables[UserData.TBL_USERS].Rows[0][UserData.FLD_PKID].ToString() )
//				{
//					//
//					// User PKID does not match, so this would create a duplicate email address
//					//
//					userRow.SetColumnError(UserData.FLD_USER_NAME, "The User Name aready exist"); //email field is not unique
//					userRow.RowError = "The Email already exist";
//                    
//					return false;
//				}
//			}
//			
			return true;
		
		}

		public dataDef SelectAll()
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAll();			
		}

		public dataDef SelectAll(bool IncludeBaseForm)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAll(IncludeBaseForm);			
		}

		public dataDef SelectOne(int ID)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectOne(ID);			
		}

		public dataDef SelectByEntityType(int EntityTypeID)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAllWentity_type_idLogic(EntityTypeID);			
		}

		public dataDef SelectByEntityType(int EntityTypeID, int ProgramTypeID)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAllWentity_type_idLogic(EntityTypeID, ProgramTypeID);			
		}

        public dataDef SelectByEntityType(int EntityTypeID, int ProgramTypeID, int ProgramID)
        {
            //
            // Get the user DataTable from the DataLayer
            //	
            return objDataAccess.SelectAllWentity_type_idLogic(EntityTypeID, ProgramTypeID, ProgramID);
        }

		public dataDef SelectBaseFormByEntityType(int EntityTypeID)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAllBaseFormWentity_type_idLogic(EntityTypeID);			
		}

		public dataDef SelectBaseFormByEntityType(int EntityTypeID, int ProgramTypeID)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAllBaseFormWentity_type_idLogic(EntityTypeID, ProgramTypeID);			
		}

		public dataDef SelectByProgramType(int ProgramTypeID)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAllWprogram_type_idLogic(ProgramTypeID);			
		}

		public dataDef SelectByCampaignID(int CampaignID)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAllWcampaign_idLogic(CampaignID);			
		}

        public dataDef SelectByCampaignID(int CampaignID, int EntityTypeID)
        {
            //
            // Get the user DataTable from the DataLayer
            //	
            return objDataAccess.SelectAllWcampaign_idLogic(CampaignID, EntityTypeID);
        }

		public dataDef SelectAllVersionByFormID(int ID)
		{
			//
			// Get the user DataTable from the DataLayer
			//	
			return objDataAccess.SelectAllVersionWform_idLogic(ID);			
		}
		
		public FormData SelectAllDetail(int ID)
		{			
			return SelectAllDetail(ID, false);
			
		}

		public FormData SelectAllDetail(int ID, bool IncludeAllDerivedElements)
		{			
			//This method fill the All Data needed for an organization
			//into a predefined DataSet
			FormData dts = new FormData();
			dts.Merge(objDataAccess.SelectOne(ID));
			//Business rule
			Business_rule bizRuleDataAccess = new Business_rule();
			dts.Merge(bizRuleDataAccess.SelectAllWform_idLogic(ID));
			//Business Exception
			Business_exception bizExcDataAccess = new Business_exception();
			dts.Merge(bizExcDataAccess.SelectAllWform_idLogic(ID));
			//Business task
			Business_task bizTaskDataAccess = new Business_task();
			dts.Merge(bizTaskDataAccess.SelectAllWform_idLogic(ID));
			//Form Catalog Group
			Form_section frmCatGrpDataAccess = new Form_section();
			dts.Merge(frmCatGrpDataAccess.SelectAllWform_idLogic(ID));
            //Form Delivery Method
            Form_delivery_method frmDelMethDataAccess = new Form_delivery_method();
            dts.Merge(frmDelMethDataAccess.SelectAllWform_idLogic(ID));
            //Form Order Type
            Form_order_type frmOrderTypeDataAccess = new Form_order_type();
            dts.Merge(frmOrderTypeDataAccess.SelectAllWform_idLogic(ID));
            //Form Delivery Method
            Form_profit_rate frmProfitRateDataAccess = new Form_profit_rate();
            dts.Merge(frmProfitRateDataAccess.SelectAllWform_idLogic(ID));

			if (IncludeAllDerivedElements)
			{
				if (dts.Form.Rows.Count >0)
				{
					//For now we assume that we can have only one level
					DataRow dervFormRow = dts.Form.Rows[0];
					if (!dervFormRow.IsNull(dataDef.FLD_PARENT_FORM_ID))
					{
						int drvFormID = Convert.ToInt32(dervFormRow[dataDef.FLD_PARENT_FORM_ID]);
						//Get Biz Rules from this derived Form
						//Business rule
						dts.Merge(bizRuleDataAccess.SelectAllWform_idLogic(drvFormID));
                        //Business task
                        dts.Merge(bizTaskDataAccess.SelectAllWform_idLogic(drvFormID));
					}
                   
				}
			
			}


			return dts;
			
		}

		public FormData InitializeForm(int UserID, int BaseFormID, int EntityTypeID)
		{
			//We prepare the DataSet for all step
			//Add a new Row
			FormData dts = new FormData();
			
			//Create a new Organization  row at start
			FormTable frmTable = dts.Form;
			DataRow row;
			row = frmTable.NewRow();		
			if (BaseFormID > 0)
				row[dataDef.FLD_PARENT_FORM_ID] = BaseFormID;
			if (EntityTypeID > 0)
				row[dataDef.FLD_ENTITY_TYPE_ID] = EntityTypeID;
			
			row[dataDef.FLD_VERSION] = 1;

			row[dataDef.FLD_CREATE_USER_ID] = UserID;

			frmTable.Rows.Add(row);		
		
			//Add a biz rule by default
			BusinessRuleTable ruleTable = dts.BusinessRule;
			DataRow ruleRow;
			ruleRow = ruleTable.NewRow();
			ruleRow[BusinessRuleTable.FLD_NAME] = "New Business Rules";
			ruleRow[BusinessRuleTable.FLD_CREATE_USER_ID] = UserID;
			ruleTable.Rows.Add(ruleRow);
		
			//Add a biz exception by default
			BusinessExceptionTable excTable = dts.BusinessException;
			DataRow excRow;
			excRow = excTable.NewRow();
			excRow[BusinessExceptionTable.FLD_NAME] = "New Business Exception";
			excRow[BusinessExceptionTable.FLD_CREATE_USER_ID] = UserID;
			excTable.Rows.Add(excRow);

			//Add a biz task by default
			BusinessTaskTable bizTaskTable = dts.BusinessTask;
			DataRow bizTaskRow;
			bizTaskRow = bizTaskTable.NewRow();
			bizTaskRow[BusinessTaskTable.FLD_NAME] = "New Business Task";
			bizTaskRow[BusinessTaskTable.FLD_CREATE_USER_ID] = UserID;
			bizTaskTable.Rows.Add(bizTaskRow);


			return dts;
			
		}

		public FormData InitializeNewVersion(FormData dtsOldVersion, int UserID)
		{
			
			//This method fill the All Data needed for a new Form Version
			//into a predefined DataSet			
			FormData dtsNewVersion = new FormData();
			FormTable dTblNewVersion = dtsNewVersion.Form;
			FormTable dTblOldVersion = dtsOldVersion.Form;
			DataRow rowNewVersion = dTblNewVersion.NewRow();
			DataRow rowOldVersion = dTblOldVersion.Rows[0];
			//Fill the Biz Form Header
			int iVersion = 0;
			if (!rowOldVersion.IsNull(FormTable.FLD_VERSION))
				iVersion = Convert.ToInt32(rowOldVersion[FormTable.FLD_VERSION]);
			//Increase version
			iVersion = iVersion + 1;
			//Copy All information of the previous version
			//Form Header
			rowNewVersion[FormTable.FLD_FORM_GROUP_ID] = rowOldVersion[FormTable.FLD_FORM_GROUP_ID];
			rowNewVersion[FormTable.FLD_VERSION] = iVersion;
			rowNewVersion[FormTable.FLD_FORM_CODE] = rowOldVersion[FormTable.FLD_FORM_CODE];			
			rowNewVersion[FormTable.FLD_FORM_NAME] = rowOldVersion[FormTable.FLD_FORM_NAME].ToString() + " - New Version";
			rowNewVersion[FormTable.FLD_DESCRIPTION] = rowOldVersion[FormTable.FLD_DESCRIPTION];
			rowNewVersion[FormTable.FLD_PROGRAM_BASICS_TEXT] = rowOldVersion[FormTable.FLD_PROGRAM_BASICS_TEXT];
			rowNewVersion[FormTable.FLD_ORDER_TERMS_TEXT] = rowOldVersion[FormTable.FLD_ORDER_TERMS_TEXT];
			rowNewVersion[FormTable.FLD_PROGRAM_TYPE_ID] = rowOldVersion[FormTable.FLD_PROGRAM_TYPE_ID];
			rowNewVersion[FormTable.FLD_ENTITY_TYPE_ID] = rowOldVersion[FormTable.FLD_ENTITY_TYPE_ID];
			rowNewVersion[FormTable.FLD_TAX_POSTAL_ADDRESS_TYPE_ID] = rowOldVersion[FormTable.FLD_TAX_POSTAL_ADDRESS_TYPE_ID];
			rowNewVersion[FormTable.FLD_CLOSING_TIMES] = rowOldVersion[FormTable.FLD_CLOSING_TIMES];
			rowNewVersion[FormTable.FLD_IS_PRODUCT_PRICE_UPDATABLE] = rowOldVersion[FormTable.FLD_IS_PRODUCT_PRICE_UPDATABLE];
			rowNewVersion[FormTable.FLD_IMAGE_URL] = rowOldVersion[FormTable.FLD_IMAGE_URL];
			rowNewVersion[FormTable.FLD_IS_BASE_FORM] = rowOldVersion[FormTable.FLD_IS_BASE_FORM];
			rowNewVersion[FormTable.FLD_PARENT_FORM_ID] = rowOldVersion[FormTable.FLD_PARENT_FORM_ID];
			rowNewVersion[FormTable.FLD_ENABLED] = true;		
			rowNewVersion[FormTable.FLD_CREATE_USER_ID] = UserID;

            dTblNewVersion.Rows.Add(rowNewVersion);

			//Copy all business rules to the new form version
			foreach (DataRow rowOldBizRule in dtsOldVersion.BusinessRule)
			{
				if (rowOldBizRule.RowState != DataRowState.Deleted)
				{
					DataRow rowNewBizRule = dtsNewVersion.BusinessRule.NewRow();
					rowNewBizRule[BusinessRuleTable.FLD_FIELD_ID] = rowOldBizRule[BusinessRuleTable.FLD_FIELD_ID];
					rowNewBizRule[BusinessRuleTable.FLD_FIELD_TYPE_ID] = rowOldBizRule[BusinessRuleTable.FLD_FIELD_TYPE_ID];
					rowNewBizRule[BusinessRuleTable.FLD_LOGICAL_OPERATOR_ID] = rowOldBizRule[BusinessRuleTable.FLD_LOGICAL_OPERATOR_ID];
                    rowNewBizRule[BusinessRuleTable.FLD_IS_FORM_PROPERTY] = rowOldBizRule[BusinessRuleTable.FLD_IS_FORM_PROPERTY];
					rowNewBizRule[BusinessRuleTable.FLD_NAME] = rowOldBizRule[BusinessRuleTable.FLD_NAME];
					rowNewBizRule[BusinessRuleTable.FLD_MESSAGE] = rowOldBizRule[BusinessRuleTable.FLD_MESSAGE];
					rowNewBizRule[BusinessRuleTable.FLD_DESCRIPTION] = rowOldBizRule[BusinessRuleTable.FLD_DESCRIPTION];
					rowNewBizRule[BusinessRuleTable.FLD_VALUE_TO_COMPARE] = rowOldBizRule[BusinessRuleTable.FLD_VALUE_TO_COMPARE];
                    rowNewBizRule[BusinessRuleTable.FLD_FORM_SECTION_TYPE_ID] = rowOldBizRule[BusinessRuleTable.FLD_FORM_SECTION_TYPE_ID];
                    rowNewBizRule[BusinessRuleTable.FLD_FORM_SECTION_NUMBER] = rowOldBizRule[BusinessRuleTable.FLD_FORM_SECTION_NUMBER];
					rowNewBizRule[BusinessRuleTable.FLD_CREATE_USER_ID] = UserID;
					dtsNewVersion.BusinessRule.Rows.Add(rowNewBizRule);
				}
			}

			//Copy all business rules to the new form version
			foreach (DataRow rowOldBizExcep in dtsOldVersion.BusinessException)
			{
				if (rowOldBizExcep.RowState != DataRowState.Deleted)
				{
					DataRow rowNewBizExcep = dtsNewVersion.BusinessException.NewRow();
					rowNewBizExcep[BusinessExceptionTable.FLD_NAME] = rowOldBizExcep[BusinessExceptionTable.FLD_NAME];
					rowNewBizExcep[BusinessExceptionTable.FLD_EXCEPTION_TYPE_ID] = rowOldBizExcep[BusinessExceptionTable.FLD_EXCEPTION_TYPE_ID];
					rowNewBizExcep[BusinessExceptionTable.FLD_MESSAGE] = rowOldBizExcep[BusinessExceptionTable.FLD_MESSAGE];			
					rowNewBizExcep[BusinessExceptionTable.FLD_WARNING_MESSAGE] = rowOldBizExcep[BusinessExceptionTable.FLD_WARNING_MESSAGE];
					rowNewBizExcep[BusinessExceptionTable.FLD_EXPRESSION] = rowOldBizExcep[BusinessExceptionTable.FLD_EXPRESSION];
					rowNewBizExcep[BusinessExceptionTable.FLD_FEES_VALUE_EXPRESSION] = rowOldBizExcep[BusinessExceptionTable.FLD_FEES_VALUE_EXPRESSION];
					rowNewBizExcep[BusinessExceptionTable.FLD_APP_ITEM_ID] = rowOldBizExcep[BusinessExceptionTable.FLD_APP_ITEM_ID];
					rowNewBizExcep[BusinessExceptionTable.FLD_ENTITY_TYPE_ID] = rowOldBizExcep[BusinessExceptionTable.FLD_ENTITY_TYPE_ID];
                    rowNewBizExcep[BusinessExceptionTable.FLD_FORM_SECTION_TYPE_ID] = rowOldBizExcep[BusinessExceptionTable.FLD_FORM_SECTION_TYPE_ID];
                    rowNewBizExcep[BusinessExceptionTable.FLD_FORM_SECTION_NUMBER] = rowOldBizExcep[BusinessExceptionTable.FLD_FORM_SECTION_NUMBER];
					
					rowNewBizExcep[BusinessExceptionTable.FLD_CREATE_USER_ID] = UserID;
					dtsNewVersion.BusinessException.Rows.Add(rowNewBizExcep);
				}
			}

			//Copy all business tasks to the new form version
			foreach (DataRow rowOldBizTask in dtsOldVersion.BusinessTask)
			{
				if (rowOldBizTask.RowState != DataRowState.Deleted)
				{
					DataRow rowNewBizTask = dtsNewVersion.BusinessTask.NewRow();
					rowNewBizTask[BusinessTaskTable.FLD_TASK_ID] = rowOldBizTask[BusinessTaskTable.FLD_TASK_ID];
					rowNewBizTask[BusinessTaskTable.FLD_NAME] = rowOldBizTask[BusinessTaskTable.FLD_NAME];
					rowNewBizTask[BusinessTaskTable.FLD_MESSAGE] = rowOldBizTask[BusinessTaskTable.FLD_MESSAGE];			
					rowNewBizTask[BusinessTaskTable.FLD_DESCRIPTION] = rowOldBizTask[BusinessTaskTable.FLD_DESCRIPTION];
					rowNewBizTask[BusinessTaskTable.FLD_EXPRESSION] = rowOldBizTask[BusinessTaskTable.FLD_EXPRESSION];
					rowNewBizTask[BusinessTaskTable.FLD_ASSIGNMENT_TYPE_ID] = rowOldBizTask[BusinessTaskTable.FLD_ASSIGNMENT_TYPE_ID];
					rowNewBizTask[BusinessTaskTable.FLD_ASSIGNED_USER_ID] = rowOldBizTask[BusinessTaskTable.FLD_ASSIGNED_USER_ID];
					rowNewBizTask[BusinessTaskTable.FLD_ASSIGNED_ROLE_ID] = rowOldBizTask[BusinessTaskTable.FLD_ASSIGNED_ROLE_ID];
					rowNewBizTask[BusinessTaskTable.FLD_CREATE_USER_ID] = UserID;
					dtsNewVersion.BusinessTask.Rows.Add(rowNewBizTask);
		
				}
			}

            //Copy all form section to the new form version
            foreach (DataRow rowOldFrmSection in dtsOldVersion.FormSection)
            {
                if (rowOldFrmSection.RowState != DataRowState.Deleted)
                {
                    DataRow rowNewFrmSection = dtsNewVersion.FormSection.NewRow();
                    rowNewFrmSection[FormSectionTable.FLD_CATALOG_ID] = rowOldFrmSection[FormSectionTable.FLD_CATALOG_ID];
                    rowNewFrmSection[FormSectionTable.FLD_CATALOG_ITEM_CATEGORY_ID] = rowOldFrmSection[FormSectionTable.FLD_CATALOG_ITEM_CATEGORY_ID];
                    rowNewFrmSection[FormSectionTable.FLD_CATALOG_ITEM_CATEGORY_NAME] = rowOldFrmSection[FormSectionTable.FLD_CATALOG_ITEM_CATEGORY_NAME];
                    rowNewFrmSection[FormSectionTable.FLD_DESCRIPTION] = rowOldFrmSection[FormSectionTable.FLD_DESCRIPTION];
                    rowNewFrmSection[FormSectionTable.FLD_FORM_SECTION_NUMBER] = rowOldFrmSection[FormSectionTable.FLD_FORM_SECTION_NUMBER];
                    rowNewFrmSection[FormSectionTable.FLD_FORM_SECTION_TITLE] = rowOldFrmSection[FormSectionTable.FLD_FORM_SECTION_TITLE];
                    rowNewFrmSection[FormSectionTable.FLD_FORM_SECTION_TYPE_ID] = rowOldFrmSection[FormSectionTable.FLD_FORM_SECTION_TYPE_ID];
                    rowNewFrmSection[FormSectionTable.FLD_CREATE_USER_ID] = UserID;
                    dtsNewVersion.FormSection.Rows.Add(rowNewFrmSection);

                }
            }

            //Copy all delivery method to the new form version
            foreach (DataRow rowOldFrmDelMeth in dtsOldVersion.FormDeliveryMethod)
            {
                if (rowOldFrmDelMeth.RowState != DataRowState.Deleted)
                {
                    DataRow rowNewFrmDelMeth = dtsNewVersion.FormDeliveryMethod.NewRow();
                    rowNewFrmDelMeth[FormDeliveryMethodTable.FLD_DELIVERY_METHOD_ID] = rowOldFrmDelMeth[FormDeliveryMethodTable.FLD_DELIVERY_METHOD_ID];
                    rowNewFrmDelMeth[FormDeliveryMethodTable.FLD_DELIVERY_METHOD_NAME] = rowOldFrmDelMeth[FormDeliveryMethodTable.FLD_DELIVERY_METHOD_NAME];
                    rowNewFrmDelMeth[FormDeliveryMethodTable.FLD_CREATE_USER_ID] = UserID;
                    dtsNewVersion.FormDeliveryMethod.Rows.Add(rowNewFrmDelMeth);

                }
            }

            //Copy all order type to the new form version
            foreach (DataRow rowOldFrmOrdType in dtsOldVersion.FormOrderType)
            {
                if (rowOldFrmOrdType.RowState != DataRowState.Deleted)
                {
                    DataRow rowNewFrmOrdType = dtsNewVersion.FormOrderType.NewRow();
                    rowNewFrmOrdType[FormOrderTypeTable.FLD_ORDER_TYPE_ID] = rowOldFrmOrdType[FormOrderTypeTable.FLD_ORDER_TYPE_ID];
                    rowNewFrmOrdType[FormOrderTypeTable.FLD_ORDER_TYPE_NAME] = rowOldFrmOrdType[FormOrderTypeTable.FLD_ORDER_TYPE_NAME];
                    rowNewFrmOrdType[FormOrderTypeTable.FLD_CREATE_USER_ID] = UserID;
                    dtsNewVersion.FormOrderType.Rows.Add(rowNewFrmOrdType);

                }
            }

            //Copy all profit rate to the new form version
            foreach (DataRow rowOldFrmProfitRate in dtsOldVersion.FormProfitRate)
            {
                if (rowOldFrmProfitRate.RowState != DataRowState.Deleted)
                {
                    DataRow rowNewFrmProfitRate = dtsNewVersion.FormProfitRate.NewRow();
                    rowNewFrmProfitRate[FormProfitRateTable.FLD_PROFIT_RATE_ID] = rowOldFrmProfitRate[FormProfitRateTable.FLD_PROFIT_RATE_ID];
                    rowNewFrmProfitRate[FormProfitRateTable.FLD_PROFIT_RATE] = rowOldFrmProfitRate[FormProfitRateTable.FLD_PROFIT_RATE];
                    rowNewFrmProfitRate[FormProfitRateTable.FLD_CREATE_USER_ID] = UserID;
                    dtsNewVersion.FormProfitRate.Rows.Add(rowNewFrmProfitRate);

                }
            }

			return dtsNewVersion;
			
		}

        public FormData InitializeFormByModel(FormData dtsModel, int UserID)
        {

            //This method fill the All Data needed for a new Form Version
            //into a predefined DataSet			
            FormData dtsNewForm = new FormData();
            FormTable dTblNewForm = dtsNewForm.Form;
            FormTable dTblModel = dtsModel.Form;
            DataRow rowNewForm = dTblNewForm.NewRow();
            DataRow rowModel = dTblModel.Rows[0];
            //Fill the Biz Form Header
            int iVersion = 1;
            //Copy All information from the Form Model
            //Form Header
            rowNewForm[FormTable.FLD_VERSION] = iVersion;
            rowNewForm[FormTable.FLD_FORM_CODE] = "New";
            rowNewForm[FormTable.FLD_FORM_NAME] = "New Form";
            rowNewForm[FormTable.FLD_DESCRIPTION] = rowModel[FormTable.FLD_DESCRIPTION];
            rowNewForm[FormTable.FLD_PROGRAM_BASICS_TEXT] = rowModel[FormTable.FLD_PROGRAM_BASICS_TEXT];
            rowNewForm[FormTable.FLD_ORDER_TERMS_TEXT] = rowModel[FormTable.FLD_ORDER_TERMS_TEXT];
            rowNewForm[FormTable.FLD_PROGRAM_TYPE_ID] = rowModel[FormTable.FLD_PROGRAM_TYPE_ID];
            rowNewForm[FormTable.FLD_ENTITY_TYPE_ID] = rowModel[FormTable.FLD_ENTITY_TYPE_ID];
            rowNewForm[FormTable.FLD_TAX_POSTAL_ADDRESS_TYPE_ID] = rowModel[FormTable.FLD_TAX_POSTAL_ADDRESS_TYPE_ID];
            rowNewForm[FormTable.FLD_CLOSING_TIMES] = rowModel[FormTable.FLD_CLOSING_TIMES];
            rowNewForm[FormTable.FLD_IS_PRODUCT_PRICE_UPDATABLE] = rowModel[FormTable.FLD_IS_PRODUCT_PRICE_UPDATABLE];
            rowNewForm[FormTable.FLD_IMAGE_URL] = rowModel[FormTable.FLD_IMAGE_URL];
            rowNewForm[FormTable.FLD_IS_BASE_FORM] = rowModel[FormTable.FLD_IS_BASE_FORM];
            rowNewForm[FormTable.FLD_PARENT_FORM_ID] = rowModel[FormTable.FLD_PARENT_FORM_ID];
            rowNewForm[FormTable.FLD_ENABLED] = true;
            rowNewForm[FormTable.FLD_CREATE_USER_ID] = UserID;

            dTblNewForm.Rows.Add(rowNewForm);

            //Copy all business rules to the new form version
            foreach (DataRow rowOldBizRule in dtsModel.BusinessRule)
            {
                if (rowOldBizRule.RowState != DataRowState.Deleted)
                {
                    DataRow rowNewBizRule = dtsNewForm.BusinessRule.NewRow();
                    rowNewBizRule[BusinessRuleTable.FLD_FIELD_ID] = rowOldBizRule[BusinessRuleTable.FLD_FIELD_ID];
                    rowNewBizRule[BusinessRuleTable.FLD_FIELD_TYPE_ID] = rowOldBizRule[BusinessRuleTable.FLD_FIELD_TYPE_ID];
                    rowNewBizRule[BusinessRuleTable.FLD_LOGICAL_OPERATOR_ID] = rowOldBizRule[BusinessRuleTable.FLD_LOGICAL_OPERATOR_ID];
                    rowNewBizRule[BusinessRuleTable.FLD_IS_FORM_PROPERTY] = rowOldBizRule[BusinessRuleTable.FLD_IS_FORM_PROPERTY];
                    rowNewBizRule[BusinessRuleTable.FLD_NAME] = rowOldBizRule[BusinessRuleTable.FLD_NAME];
                    rowNewBizRule[BusinessRuleTable.FLD_MESSAGE] = rowOldBizRule[BusinessRuleTable.FLD_MESSAGE];
                    rowNewBizRule[BusinessRuleTable.FLD_DESCRIPTION] = rowOldBizRule[BusinessRuleTable.FLD_DESCRIPTION];
                    rowNewBizRule[BusinessRuleTable.FLD_VALUE_TO_COMPARE] = rowOldBizRule[BusinessRuleTable.FLD_VALUE_TO_COMPARE];
                    rowNewBizRule[BusinessRuleTable.FLD_FORM_SECTION_TYPE_ID] = rowOldBizRule[BusinessRuleTable.FLD_FORM_SECTION_TYPE_ID];
                    rowNewBizRule[BusinessRuleTable.FLD_FORM_SECTION_NUMBER] = rowOldBizRule[BusinessRuleTable.FLD_FORM_SECTION_NUMBER];
					
                    rowNewBizRule[BusinessRuleTable.FLD_CREATE_USER_ID] = UserID;
                    dtsNewForm.BusinessRule.Rows.Add(rowNewBizRule);
                }
            }

            //Copy all business rules to the new form version
            foreach (DataRow rowOldBizExcep in dtsModel.BusinessException)
            {
                if (rowOldBizExcep.RowState != DataRowState.Deleted)
                {
                    DataRow rowNewBizExcep = dtsNewForm.BusinessException.NewRow();
                    rowNewBizExcep[BusinessExceptionTable.FLD_NAME] = rowOldBizExcep[BusinessExceptionTable.FLD_NAME];
                    rowNewBizExcep[BusinessExceptionTable.FLD_EXCEPTION_TYPE_ID] = rowOldBizExcep[BusinessExceptionTable.FLD_EXCEPTION_TYPE_ID];
                    rowNewBizExcep[BusinessExceptionTable.FLD_MESSAGE] = rowOldBizExcep[BusinessExceptionTable.FLD_MESSAGE];
                    rowNewBizExcep[BusinessExceptionTable.FLD_WARNING_MESSAGE] = rowOldBizExcep[BusinessExceptionTable.FLD_WARNING_MESSAGE];
                    rowNewBizExcep[BusinessExceptionTable.FLD_EXPRESSION] = rowOldBizExcep[BusinessExceptionTable.FLD_EXPRESSION];
                    rowNewBizExcep[BusinessExceptionTable.FLD_FEES_VALUE_EXPRESSION] = rowOldBizExcep[BusinessExceptionTable.FLD_FEES_VALUE_EXPRESSION];
                    rowNewBizExcep[BusinessExceptionTable.FLD_APP_ITEM_ID] = rowOldBizExcep[BusinessExceptionTable.FLD_APP_ITEM_ID];
                    rowNewBizExcep[BusinessExceptionTable.FLD_ENTITY_TYPE_ID] = rowOldBizExcep[BusinessExceptionTable.FLD_ENTITY_TYPE_ID];
                    rowNewBizExcep[BusinessExceptionTable.FLD_FORM_SECTION_TYPE_ID] = rowOldBizExcep[BusinessExceptionTable.FLD_FORM_SECTION_TYPE_ID];
                    rowNewBizExcep[BusinessExceptionTable.FLD_FORM_SECTION_NUMBER] = rowOldBizExcep[BusinessExceptionTable.FLD_FORM_SECTION_NUMBER];
					
                    rowNewBizExcep[BusinessExceptionTable.FLD_CREATE_USER_ID] = UserID;
                    dtsNewForm.BusinessException.Rows.Add(rowNewBizExcep);
                }
            }

            //Copy all business tasks to the new form version
            foreach (DataRow rowOldBizTask in dtsModel.BusinessTask)
            {
                if (rowOldBizTask.RowState != DataRowState.Deleted)
                {
                    DataRow rowNewBizTask = dtsNewForm.BusinessTask.NewRow();
                    rowNewBizTask[BusinessTaskTable.FLD_TASK_ID] = rowOldBizTask[BusinessTaskTable.FLD_TASK_ID];
                    rowNewBizTask[BusinessTaskTable.FLD_NAME] = rowOldBizTask[BusinessTaskTable.FLD_NAME];
                    rowNewBizTask[BusinessTaskTable.FLD_MESSAGE] = rowOldBizTask[BusinessTaskTable.FLD_MESSAGE];
                    rowNewBizTask[BusinessTaskTable.FLD_DESCRIPTION] = rowOldBizTask[BusinessTaskTable.FLD_DESCRIPTION];
                    rowNewBizTask[BusinessTaskTable.FLD_EXPRESSION] = rowOldBizTask[BusinessTaskTable.FLD_EXPRESSION];
                    rowNewBizTask[BusinessTaskTable.FLD_ASSIGNMENT_TYPE_ID] = rowOldBizTask[BusinessTaskTable.FLD_ASSIGNMENT_TYPE_ID];
                    rowNewBizTask[BusinessTaskTable.FLD_ASSIGNED_USER_ID] = rowOldBizTask[BusinessTaskTable.FLD_ASSIGNED_USER_ID];
                    rowNewBizTask[BusinessTaskTable.FLD_ASSIGNED_ROLE_ID] = rowOldBizTask[BusinessTaskTable.FLD_ASSIGNED_ROLE_ID];
                    rowNewBizTask[BusinessTaskTable.FLD_CREATE_USER_ID] = UserID;
                    dtsNewForm.BusinessTask.Rows.Add(rowNewBizTask);

                }
            }

            //Copy all form section to the new form version
            foreach (DataRow rowOldFrmSection in dtsModel.FormSection)
            {
                if (rowOldFrmSection.RowState != DataRowState.Deleted)
                {
                    DataRow rowNewFrmSection = dtsNewForm.FormSection.NewRow();
                    rowNewFrmSection[FormSectionTable.FLD_CATALOG_ID] = rowOldFrmSection[FormSectionTable.FLD_CATALOG_ID];
                    rowNewFrmSection[FormSectionTable.FLD_CATALOG_ITEM_CATEGORY_ID] = rowOldFrmSection[FormSectionTable.FLD_CATALOG_ITEM_CATEGORY_ID];
                    rowNewFrmSection[FormSectionTable.FLD_CATALOG_ITEM_CATEGORY_NAME] = rowOldFrmSection[FormSectionTable.FLD_CATALOG_ITEM_CATEGORY_NAME];
                    rowNewFrmSection[FormSectionTable.FLD_DESCRIPTION] = rowOldFrmSection[FormSectionTable.FLD_DESCRIPTION];
                    rowNewFrmSection[FormSectionTable.FLD_FORM_SECTION_NUMBER] = rowOldFrmSection[FormSectionTable.FLD_FORM_SECTION_NUMBER];
                    rowNewFrmSection[FormSectionTable.FLD_FORM_SECTION_TITLE] = rowOldFrmSection[FormSectionTable.FLD_FORM_SECTION_TITLE];
                    rowNewFrmSection[FormSectionTable.FLD_FORM_SECTION_TYPE_ID] = rowOldFrmSection[FormSectionTable.FLD_FORM_SECTION_TYPE_ID];
                    rowNewFrmSection[FormSectionTable.FLD_CREATE_USER_ID] = UserID;
                    dtsNewForm.FormSection.Rows.Add(rowNewFrmSection);

                }
            }

            //Copy all delivery method to the new form version
            foreach (DataRow rowOldFrmDelMeth in dtsModel.FormDeliveryMethod)
            {
                if (rowOldFrmDelMeth.RowState != DataRowState.Deleted)
                {
                    DataRow rowNewFrmDelMeth = dtsNewForm.FormDeliveryMethod.NewRow();
                    rowNewFrmDelMeth[FormDeliveryMethodTable.FLD_DELIVERY_METHOD_ID] = rowOldFrmDelMeth[FormDeliveryMethodTable.FLD_DELIVERY_METHOD_ID];
                    rowNewFrmDelMeth[FormDeliveryMethodTable.FLD_DELIVERY_METHOD_NAME] = rowOldFrmDelMeth[FormDeliveryMethodTable.FLD_DELIVERY_METHOD_NAME];
                    rowNewFrmDelMeth[FormDeliveryMethodTable.FLD_CREATE_USER_ID] = UserID;
                    dtsNewForm.FormDeliveryMethod.Rows.Add(rowNewFrmDelMeth);

                }
            }

            //Copy all order type to the new form version
            foreach (DataRow rowOldFrmOrdType in dtsModel.FormOrderType)
            {
                if (rowOldFrmOrdType.RowState != DataRowState.Deleted)
                {
                    DataRow rowNewFrmOrdType = dtsNewForm.FormOrderType.NewRow();
                    rowNewFrmOrdType[FormOrderTypeTable.FLD_ORDER_TYPE_ID] = rowOldFrmOrdType[FormOrderTypeTable.FLD_ORDER_TYPE_ID];
                    rowNewFrmOrdType[FormOrderTypeTable.FLD_ORDER_TYPE_NAME] = rowOldFrmOrdType[FormOrderTypeTable.FLD_ORDER_TYPE_NAME];
                    rowNewFrmOrdType[FormOrderTypeTable.FLD_CREATE_USER_ID] = UserID;
                    dtsNewForm.FormOrderType.Rows.Add(rowNewFrmOrdType);

                }
            }

            //Copy all profit rate to the new form version
            foreach (DataRow rowOldFrmProfitRate in dtsModel.FormProfitRate)
            {
                if (rowOldFrmProfitRate.RowState != DataRowState.Deleted)
                {
                    DataRow rowNewFrmProfitRate = dtsNewForm.FormProfitRate.NewRow();
                    rowNewFrmProfitRate[FormProfitRateTable.FLD_PROFIT_RATE_ID] = rowOldFrmProfitRate[FormProfitRateTable.FLD_PROFIT_RATE_ID];
                    rowNewFrmProfitRate[FormProfitRateTable.FLD_PROFIT_RATE] = rowOldFrmProfitRate[FormProfitRateTable.FLD_PROFIT_RATE];
                    rowNewFrmProfitRate[FormProfitRateTable.FLD_CREATE_USER_ID] = UserID;
                    dtsNewForm.FormProfitRate.Rows.Add(rowNewFrmProfitRate);

                }
            }

            return dtsNewForm;

        }

		private bool IsNewVersion_Required(FormData dts, Data.ConnectionProvider connProvider)
		{
			bool IsRequired = false;
			//When the expression used to raised exception are changed
			//We create automatically a new version of the form for archive purposes
			//Business Exception	
			DataRow formRow = dts.Form.Rows[0];
			if (formRow.RowState != DataRowState.Added)
			{
				int iFormID = Convert.ToInt32(formRow[FormTable.FLD_PKID]);
				OrderHeaderTable dTblOrder  = new OrderHeaderTable();
				QSPForm.Data.Order orderDataAccess = new QSPForm.Data.Order(); 
				if (connProvider != null)
				{
					orderDataAccess.MainConnectionProvider = connProvider;
				}
				int iCountOfForm = orderDataAccess.CountAllWform_idLogic(iFormID);

				if (iCountOfForm > 0)
				{

					if (dts.BusinessRule.GetChanges() != null)
					{
						return true;
					}

					if (dts.BusinessException.GetChanges() != null)
					{
						return true;
					}
				}
				else
				{
					return false;
				
				}
			}
			return IsRequired;

		}

		public bool UpdateAllDetail(FormData dts, int UserID)
		{			
			//Variable to handle the operation in One transaction transaction
			String TransactionName = "Form_UpdateAllDetail";						
			Data.ConnectionProvider connProvider = new Data.ConnectionProvider();
			bool IsSuccess = true;
			bool isNewVersion_Required = false;

			try
			{
				//This method fill the All Data needed for an organization
				//into a predefined DataSet	
							
				//Data Object Instanciation
				Data.Form frmDataAccess = new Data.Form();
				Data.Form_group frmGrpDataAccess = new Form_group();
				Data.Business_rule bizRuleDataAccess = new Data.Business_rule();
				Data.Business_exception bizExcDataAccess = new Data.Business_exception();
				Data.Business_task bizTaskDataAccess = new Data.Business_task();
				Data.Form_section frmSectionDataAccess = new Data.Form_section();
                Data.Form_delivery_method frmDelMethDataAccess = new Data.Form_delivery_method();
                Data.Form_order_type frmOrderTypeDataAccess = new Data.Form_order_type();
                Data.Form_profit_rate frmProfitRateDataAccess = new Data.Form_profit_rate();


				// Pass the created ConnectionProvider object to the data-access objects.
				frmDataAccess.MainConnectionProvider = connProvider;
				frmGrpDataAccess.MainConnectionProvider = connProvider;
				bizRuleDataAccess.MainConnectionProvider = connProvider;
				bizExcDataAccess.MainConnectionProvider = connProvider;
				bizTaskDataAccess.MainConnectionProvider = connProvider;
				frmSectionDataAccess.MainConnectionProvider = connProvider;
                frmDelMethDataAccess.MainConnectionProvider = connProvider;
                frmOrderTypeDataAccess.MainConnectionProvider = connProvider;
                frmProfitRateDataAccess.MainConnectionProvider = connProvider;

				connProvider.OpenConnection();
				connProvider.BeginTransaction(TransactionName);
				
				//We have to check if a newer version have been inserted since the first time
				//we read the database.
				DataRow frmRow = dts.Form.Rows[0];
				int iFormID = Convert.ToInt32(frmRow[FormTable.FLD_PKID]);
				if (frmRow.RowState != DataRowState.Added)
				{
					//isNewVersion_Required = IsNewVersion_Required(dts, connProvider);				
					if (isNewVersion_Required)
					{					
						int iVersion = 1; // That start with number one
						if (!frmRow.IsNull(FormTable.FLD_VERSION))
							iVersion = Convert.ToInt32(frmRow[FormTable.FLD_VERSION]);

						FormTable dTblAllVersion = frmDataAccess.SelectAllVersionWform_idLogic(iFormID);
						DataView dvAllVersion = new DataView(dTblAllVersion);
						dvAllVersion.RowFilter = FormTable.FLD_VERSION + " > " + iVersion.ToString();
						//if a new version has been added previously 
						if (dvAllVersion.Count > 0)
						{
							messageManager.HeaderText = "System Error";
							messageManager.ValidationExceptionType = QSPFormExceptionType.RecordIsModified;
							messageManager.SetErrorMessage(messageManager.FormatErrorMessage(QSPFormMessage.CONCURENT_NEW_VERSION,"Form"));

							throw new QSPFormValidationException(messageManager);
						}
						else
						{
							//Create a new version of the form
							dts = InitializeNewVersion(dts, UserID);
							CommonSystem comSys = new CommonSystem();
							bool blnEnabled = false;
							//We have to update the Old Version to enable = false;
							foreach (DataRow rowVersion in dTblAllVersion.Rows)
							{							
								comSys.UpdateRow(rowVersion, FormTable.FLD_ENABLED, blnEnabled.ToString());							
							}
							frmDataAccess.UpdateBatch(dTblAllVersion);
						}
					
					}
					else
					{
						
						//Check for the last update date
						FormTable frmCurrentVersion = frmDataAccess.SelectOne(iFormID);
						if (frmCurrentVersion.Rows.Count > 0)
						{
							DataRow frmCurrentVersionRow = frmCurrentVersion.Rows[0];
							if (Convert.ToDateTime(frmCurrentVersionRow[dataDef.FLD_UPDATE_DATE]) != Convert.ToDateTime(frmRow[dataDef.FLD_UPDATE_DATE, DataRowVersion.Original]))
							{
								messageManager.HeaderText = "System Error";
								messageManager.ValidationExceptionType = QSPFormExceptionType.RecordIsModified;
								messageManager.SetErrorMessage(messageManager.FormatErrorMessage(QSPFormMessage.CONCURENT_RECORD_MODIFIED,"Form"));

								throw new QSPFormValidationException(messageManager);
							}
						}
						else
						{
							messageManager.HeaderText = "System Error";
							messageManager.ValidationExceptionType = QSPFormExceptionType.RecordIsDeleted;
							messageManager.SetErrorMessage(messageManager.FormatErrorMessage(QSPFormMessage.CONCURENT_RECORD_DELETED,"Form"));

							throw new QSPFormValidationException(messageManager);
						}
						
					}
				}
				else
				{
					if (frmRow.IsNull(FormTable.FLD_FORM_GROUP_ID))
					{
						//When it's a completely new, that is required a new Form group
						FormGroupTable dTblfrmGrp = new FormGroupTable();
						DataRow newfrmGrpRow = dTblfrmGrp.NewRow();
						newfrmGrpRow[FormGroupTable.FLD_FORM_GROUP_NAME] = frmRow[FormTable.FLD_FORM_NAME];
						newfrmGrpRow[FormGroupTable.FLD_CREATE_USER_ID] = UserID;
						dTblfrmGrp.Rows.Add(newfrmGrpRow);
						frmGrpDataAccess.Insert(dTblfrmGrp);
						frmRow[FormTable.FLD_FORM_GROUP_ID] = newfrmGrpRow[FormGroupTable.FLD_PKID];
					}
				}
				//This method update the All Data for a business form
				//into a predefined DataSet			
				if (dts.Form.GetChanges() != null)
				{
					frmDataAccess.UpdateBatch(dts.Form);
				}
				PrepareTransactionWithNewID(dts);
				//Business Rule
				if (dts.BusinessRule.GetChanges() != null)
				{
					bizRuleDataAccess.UpdateBatch(dts.BusinessRule);
				}
				//Business Exception
				if (dts.BusinessException.GetChanges() != null)
				{
					bizExcDataAccess.UpdateBatch(dts.BusinessException);
				}
				//Business Task
				if (dts.BusinessTask.GetChanges() != null)
				{
					bizTaskDataAccess.UpdateBatch(dts.BusinessTask);
				}

				//Catalog group
				if (dts.FormSection.GetChanges() != null)
				{
					frmSectionDataAccess.UpdateBatch(dts.FormSection);					
				}

                //Delivery Method
                if (dts.FormDeliveryMethod.GetChanges() != null)
                {
                    frmDelMethDataAccess.UpdateBatch(dts.FormDeliveryMethod);
                }
                //Order Type
                if (dts.FormOrderType.GetChanges() != null)
                {
                    frmOrderTypeDataAccess.UpdateBatch(dts.FormOrderType);
                }
                //Order Type
                if (dts.FormProfitRate.GetChanges() != null)
                {
                    frmProfitRateDataAccess.UpdateBatch(dts.FormProfitRate);
                }

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
				throw ex;
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

		private void PrepareTransactionWithNewID(FormData dts)
		{
			int NewID = Convert.ToInt32(dts.Form.Rows[0][FormTable.FLD_PKID]);
			foreach(DataRow row in dts.BusinessRule.Rows)
			{
				if (row.RowState == DataRowState.Added)
				{
					row[BusinessRuleTable.FLD_FORM_ID] = NewID;
				}
			}
			foreach(DataRow row in dts.BusinessException.Rows)
			{
				if (row.RowState == DataRowState.Added)
				{
					row[BusinessExceptionTable.FLD_FORM_ID] = NewID;
				}
			}
			foreach(DataRow row in dts.BusinessTask.Rows)
			{
				if (row.RowState == DataRowState.Added)
				{
					row[BusinessTaskTable.FLD_FORM_ID] = NewID;
				}
			}
			foreach(DataRow row in dts.FormSection.Rows)
			{
				if (row.RowState == DataRowState.Added)
				{
					row[FormSectionTable.FLD_FORM_ID] = NewID;
				}
			}

            foreach (DataRow row in dts.FormDeliveryMethod.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    row[FormDeliveryMethodTable.FLD_FORM_ID] = NewID;
                }
            }
            foreach (DataRow row in dts.FormOrderType.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    row[FormOrderTypeTable.FLD_FORM_ID] = NewID;
                }
            }
            foreach (DataRow row in dts.FormProfitRate.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    row[FormProfitRateTable.FLD_FORM_ID] = NewID;
                }
            }
			
		}	

		//************************************************************************//
		//																		  //
		//				PERFORM VALIDATION AND TASK FOR ACCOUNT					  //
		//																		  //
		//************************************************************************//

		//BUSINESS VALIDATION (EXCEPTION)
		public bool PerformValidation(AccountData dtsAccount, FormData dtsForm, int UserID, int dataOperation)
		{		
			bool IsValid = true;
			try
			{
				//Account				
				DataRow accRow = dtsAccount.Account.Rows[0];
				if (accRow.RowState == DataRowState.Added)
				{
					if (dtsAccount.AccountException.Rows.Count >0)
						dtsAccount.AccountException.Clear();
				}
				//Evaluate Biz Rule
				QSPForm.Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
				bizSys.EvaluateBizRule(dtsAccount, dtsForm, dataOperation);
				
				QSPForm.Business.BusinessExceptionSystem excSys = new QSPForm.Business.BusinessExceptionSystem();				
				int EntityID = Convert.ToInt32(accRow[AccountTable.FLD_PKID]);
				int EntityTypeID = EntityType.TYPE_ACCOUNT;
				IsValid = excSys.PerformValidation(dtsAccount, dtsForm, UserID, EntityTypeID, EntityID);				
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}

		//BUSINESS TASK
		public bool PerformTask(AccountData dtsAccount, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
		{		
			bool IsValid = true;
			//****************BEGIN PERFORM TASK - ACCOUNT PROCESS				
			DateTime dStart = DateTime.Now;
			TimeSpan tpDuration;
			try
			{
				//****************BEGIN RULE EVALUATION PROCESS
				DateTime dStartProcess = DateTime.Now;
				QSPForm.Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
				bizSys.EvaluateBizRule(dtsAccount, dtsForm, dataOperation);
				Debug.WriteLine("bizSys.EvaluateBizRule: " + DateTime.Now.ToLongTimeString());
				tpDuration = DateTime.Now.Subtract(dStartProcess);
				Debug.WriteLine("bizSys.EvaluateBizRule Duration: " + tpDuration.TotalSeconds.ToString());				
				//****************END RULE EVALUATION PROCESS				
				
				//****************BEGIN PERFORM TASK - ACCOUNT PROCESS
				dStartProcess = DateTime.Now;
				QSPForm.Business.BusinessTaskSystem bizTaskSys = new QSPForm.Business.BusinessTaskSystem();
				int ParamValue = Convert.ToInt32(dtsAccount.Account.Rows[0][AccountTable.FLD_PKID]);
				bizTaskSys.PerformTask(dtsAccount, dtsForm, UserID, ParamValue, EntityType.TYPE_ACCOUNT, connProvider);
				Debug.WriteLine("bizTaskSys.PerformTask: " + DateTime.Now.ToLongTimeString());
				tpDuration = DateTime.Now.Subtract(dStartProcess);
				Debug.WriteLine("bizTaskSys.PerformTask Duration: " + tpDuration.TotalSeconds.ToString());				
				//****************END PERFORM TASK - ACCOUNT PROCESS
				
			}
			catch(Exception ex)
			{
				throw ex;
			}			
			Debug.WriteLine("Perform Task Account: " + DateTime.Now.ToLongTimeString());
			tpDuration = DateTime.Now.Subtract(dStart);
			Debug.WriteLine("Perform Task Account Duration: " + tpDuration.TotalSeconds.ToString());				
			//****************END PERFORM TASK - ACCOUNT PROCESS
	
			return IsValid;
		}

        //************************************************************************//
        //																		  //
        //				PERFORM VALIDATION AND TASK FOR PROGRAM AGREEMENT					  //
        //																		  //
        //************************************************************************//

        //BUSINESS VALIDATION (EXCEPTION)
        public bool PerformValidation(ProgramAgreementData dtsProgramAgreement, FormData dtsForm, int UserID, int dataOperation)
        {
            bool IsValid = true;
            try
            {
                //ProgramAgreement				
                DataRow prgRow = dtsProgramAgreement.ProgramAgreement.Rows[0];
                if (prgRow.RowState == DataRowState.Added)
                {
                    if (dtsProgramAgreement.ProgramAgreementException.Rows.Count > 0)
                        dtsProgramAgreement.ProgramAgreementException.Clear();
                }
                //Evaluate Biz Rule
                QSPForm.Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
                bizSys.EvaluateBizRule(dtsProgramAgreement, dtsForm, dataOperation);

                QSPForm.Business.BusinessExceptionSystem excSys = new QSPForm.Business.BusinessExceptionSystem();
                int EntityID = Convert.ToInt32(prgRow[ProgramAgreementTable.FLD_PKID]);
                int EntityTypeID = EntityType.TYPE_PROGRAM_AGREEMENT;
                IsValid = excSys.PerformValidation(dtsProgramAgreement, dtsForm, UserID, EntityTypeID, EntityID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValid;
        }

        //BUSINESS TASK
        public bool PerformTask(ProgramAgreementData dtsProgramAgreement, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
        {
            bool IsValid = true;
            //****************BEGIN PERFORM TASK - PROGRAM_AGREEMENT PROCESS				
            try
            {
                //****************BEGIN RULE EVALUATION PROCESS
                QSPForm.Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
                bizSys.EvaluateBizRule(dtsProgramAgreement, dtsForm, dataOperation);
                //****************END RULE EVALUATION PROCESS				

                //****************BEGIN PERFORM TASK - PROGRAM_AGREEMENT PROCESS
                QSPForm.Business.BusinessTaskSystem bizTaskSys = new QSPForm.Business.BusinessTaskSystem();
                int ParamValue = Convert.ToInt32(dtsProgramAgreement.ProgramAgreement.Rows[0][ProgramAgreementTable.FLD_PKID]);
                bizTaskSys.PerformTask(dtsProgramAgreement, dtsForm, UserID, ParamValue, EntityType.TYPE_PROGRAM_AGREEMENT, connProvider);
                //****************END PERFORM TASK - PROGRAM_AGREEMENT PROCESS

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //****************END PERFORM TASK - PROGRAM_AGREEMENT PROCESS

            return IsValid;
        }

		//************************************************************************//
		//																		  //
		//		    PERFORM VALIDATION AND TASK FOR CREDIT APPLICATION			  //
		//																		  //
		//************************************************************************//

		//BUSINESS VALIDATION (EXCEPTION)
		public bool PerformValidation(CreditApplicationData dts, FormData dtsForm, int UserID, int dataOperation)
		{		
			bool IsValid = true;
			try
			{
				//Account				
				DataRow crdAppRow = dts.CreditApplication.Rows[0];
				if (crdAppRow.RowState == DataRowState.Added)
				{
					if (dts.CreditException.Rows.Count >0)
						dts.CreditException.Clear();
				}
				//Evaluate Biz Rule
				QSPForm.Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
				bizSys.EvaluateBizRule(dts, dtsForm, dataOperation);
				
				QSPForm.Business.BusinessExceptionSystem excSys = new QSPForm.Business.BusinessExceptionSystem();				
				int EntityID = Convert.ToInt32(crdAppRow[CreditApplicationTable.FLD_PKID]);
				int EntityTypeID = EntityType.TYPE_CREDIT_APPLICATION;
				IsValid = excSys.PerformValidation(dts, dtsForm, UserID, EntityTypeID, EntityID);				
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}

		//BUSINESS TASK
		public bool PerformTask(CreditApplicationData dts, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
		{		
			bool IsValid = true;

			try
			{
				QSPForm.Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
				bizSys.EvaluateBizRule(dts, dtsForm, dataOperation);
				
				QSPForm.Business.BusinessTaskSystem bizTaskSys = new QSPForm.Business.BusinessTaskSystem();
				
				int ParamValue = Convert.ToInt32(dts.CreditApplication.Rows[0][CreditApplicationTable.FLD_PKID]);

				bizTaskSys.PerformTask(dts, dtsForm, UserID, ParamValue, EntityType.TYPE_CREDIT_APPLICATION, connProvider);
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}

		//************************************************************************//
		//																		  //
		//				PERFORM VALIDATION AND TASK FOR ORDER					  //
		//																		  //
		//************************************************************************//

		//BUSINESS VALIDATION (EXCEPTION)
		public bool PerformValidation(OrderData dtsOrder, AccountData dtsAccount, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
		{		
			bool IsValid = true;
			try
			{
				//Order
				DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
				if (ordRow.RowState == DataRowState.Added)
				{
					if (dtsOrder.OrderException.Rows.Count >0)
						dtsOrder.OrderException.Clear();
				}
				bool IsValidOrder = true;

				QSPForm.Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
				bizSys.EvaluateBizRule(dtsOrder, dtsAccount, dtsForm, dataOperation, connProvider);
				
				
				QSPForm.Business.BusinessExceptionSystem excSys = new QSPForm.Business.BusinessExceptionSystem();
				int EntityID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_PKID]);
				int EntityTypeID = EntityType.TYPE_ORDER_BILLING;	
				IsValidOrder = excSys.PerformValidation(dtsOrder, dtsForm, UserID, EntityTypeID, EntityID);

				IsValid = IsValidOrder;
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}

        //BUSINESS MANDATORY VALIDATION (EXCEPTION)
        public bool PerformMandatoryValidation(OrderData dtsOrder, AccountData dtsAccount, FormData dtsForm, int UserID, int dataOperation)
        {
            bool IsValid = true;
            try
            {
                //Order
                DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
                if (ordRow.RowState == DataRowState.Added)
                {
                    if (dtsOrder.OrderException.Rows.Count > 0)
                        dtsOrder.OrderException.Clear();
                }
                bool IsValidOrder = true;

                QSPForm.Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
                bizSys.EvaluateBizRule(dtsOrder, dtsAccount, dtsForm, dataOperation, null);


                QSPForm.Business.BusinessExceptionSystem excSys = new QSPForm.Business.BusinessExceptionSystem();
                int EntityID = Convert.ToInt32(ordRow[OrderHeaderTable.FLD_PKID]);
                int EntityTypeID = EntityType.TYPE_ORDER_BILLING;
                IsValidOrder = excSys.PerformValidation(dtsOrder, dtsForm, UserID, EntityTypeID, EntityID, (int)QSPForm.Common.BusinessExceptionType.Mandatory);

                IsValid = IsValidOrder;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValid;
        }

		//BUSINESS TASK
		public bool PerformTask(OrderData dtsOrder, AccountData dtsAccount, FormData dtsForm, int UserID, int dataOperation, Data.ConnectionProvider connProvider)
		{		
			bool IsValid = true;

			try
			{
				QSPForm.Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
				bizSys.EvaluateBizRule(dtsOrder, dtsAccount, dtsForm, dataOperation, connProvider);
				
				QSPForm.Business.BusinessTaskSystem bizTaskSys = new QSPForm.Business.BusinessTaskSystem();
				int ParamValue = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_PKID]);

				bizTaskSys.PerformTask(dtsOrder, dtsForm, UserID, ParamValue, EntityType.TYPE_ORDER_BILLING, connProvider);
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return IsValid;
		}
		
		public int GetCurrentFormID(int EntityTypeID, int ProgramTypeID)
		{
			int FormID = 0;
			FormSystem frmSys = new FormSystem();
			FormTable dTblForm = new FormTable();
			if (ProgramTypeID > 0)
				dTblForm = frmSys.SelectByEntityType(EntityTypeID, ProgramTypeID);			
			else
				dTblForm = frmSys.SelectByEntityType(EntityTypeID);			
			
			if (dTblForm.Rows.Count > 0)
			{
				DataRow frmRow = dTblForm.Rows[0];
				FormID = Convert.ToInt32(frmRow[FormTable.FLD_PKID]);
			}

			return FormID;
		
		}

		public int GetCurrentFormID(int EntityTypeID)
		{
			return GetCurrentFormID(EntityTypeID, 0);
		
		}

		public int GetCurrentBaseFormID(int EntityTypeID, int ProgramTypeID)
		{
			int FormID = 0;
			FormSystem frmSys = new FormSystem();
			FormTable dTblForm = new FormTable();
			if (ProgramTypeID > 0)
				dTblForm = frmSys.SelectBaseFormByEntityType(EntityTypeID, ProgramTypeID);			
			else
				dTblForm = frmSys.SelectBaseFormByEntityType(EntityTypeID);			
			
			if (dTblForm.Rows.Count > 0)
			{
				DataRow frmRow = dTblForm.Rows[0];
				FormID = Convert.ToInt32(frmRow[FormTable.FLD_PKID]);
			}

			return FormID;
		
		}

		public int GetCurrentBaseFormID(int EntityTypeID)
		{
			return GetCurrentBaseFormID(EntityTypeID, 0);
		
		}

        public FormDeliveryMethodTable SelectAllDeliveryMethodByFormID(int FormID)
        {
            Data.Form_delivery_method frmDelMethDataAccess = new Form_delivery_method();
            return frmDelMethDataAccess.SelectAllWform_idLogic(FormID);

        }

        public FormOrderTypeTable SelectAllOrderTypeByFormID(int FormID)
        {
            Data.Form_order_type frmOrderTypeDataAccess = new Form_order_type();
            return frmOrderTypeDataAccess.SelectAllWform_idLogic(FormID);

        }

        public FormProfitRateTable SelectAllProfitRateByFormID(int FormID)
        {
            Data.Form_profit_rate frmProfitRateDataAccess = new Form_profit_rate();
            return frmProfitRateDataAccess.SelectAllWform_idLogic(FormID);

        }

        public FormSectionTable SelectAllFormSectionByFormID(int FormID, int FormSectionTypeID)
        {
            Data.Form_section frmSectionDataAccess = new Form_section();
            return frmSectionDataAccess.SelectAllWform_idLogic(FormID, FormSectionTypeID);

        }

#endregion

        #region Program agreement methods

        public bool FormRequiresProgramAgreement(int formId)
        {
            bool result = false;

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            #region Get required forms

            var query1 = from frf in context.FormRequiresForms
                         where frf.IsDeleted == false
                             && frf.FormId == formId
                         select frf;

            int requiredFormCount = query1.Count();

            #endregion

            if (requiredFormCount > 0)
            {
                result = true;
            }

            return result;
        }
        public bool FormHasProgramAgreement(int formId, int accountId)
        {
            bool result = false;
            
            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            #region Get required forms

            var query1 =    from    frf in context.FormRequiresForms
                            where   frf.IsDeleted == false
                                &&  frf.FormId == formId
                            select  frf.RequiredFormId;

            List<int> requiredFormIds = query1.ToList<int>();

            #endregion

            #region Get program agreement campaign list

            var query2 =    from    pac in context.ProgramAgreementCampaigns
                             where  pac.Campaign.AccountId == accountId
                                 && pac.Campaign.IsDeleted == false
                                 && pac.IsDeleted == false
                            select  pac;

            List<LinqEntity.ProgramAgreementCampaign> programAgreementCampaignList = query2.ToList<LinqEntity.ProgramAgreementCampaign>();

            #endregion

            #region Check to see if we have at least one of the required forms

            foreach (LinqEntity.ProgramAgreementCampaign programAgreementCampaign in programAgreementCampaignList)
            {
                if (programAgreementCampaign.ProgramAgreement.IsDeleted == false)
                {
                    if (programAgreementCampaign.ProgramAgreement.ProgramAgreementStatus.StatusCategoryId != (int)ProgramAgreementStatusCategoryEnum.Saved
                        && programAgreementCampaign.ProgramAgreement.ProgramAgreementStatus.StatusCategoryId != (int)ProgramAgreementStatusCategoryEnum.Cancelled
                        && programAgreementCampaign.ProgramAgreement.ProgramAgreementStatus.StatusCategoryId != (int)ProgramAgreementStatusCategoryEnum.PendingApproval
                        && programAgreementCampaign.ProgramAgreement.ProgramAgreementStatus.StatusCategoryId != (int)ProgramAgreementStatusCategoryEnum.ErrorReported
                        && programAgreementCampaign.ProgramAgreement.ProgramAgreementStatus.StatusCategoryId != (int)ProgramAgreementStatusCategoryEnum.TestOrderVoid)
                    {
                        if (programAgreementCampaign.ProgramAgreement.FormId.HasValue)
                        {
                            if (requiredFormIds.Contains(programAgreementCampaign.ProgramAgreement.FormId.Value))
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }

            #endregion

            return result;
        }

        #endregion

        #region Form type methods

        /// <summary>
        /// Checks if the specified form is in the Pine Valley form list
        /// </summary>
        /// <param name="formId">The form to check</param>
        /// <returns>True if the formm is in the Pine Valley form list</returns>
        public bool IsPineValleyForm(int formId)
        {
            bool result = false;

            QSPForm.Business.Properties.Settings settings = new QSPForm.Business.Properties.Settings();
            string formListString = settings.PineValleyFormList;
            string[] formArray = formListString.Split(',');

            foreach (string item in formArray)
            {
                if (Convert.ToInt32(item) == formId)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if the specified form is in the Otis form list
        /// </summary>
        /// <param name="formId">The form to check</param>
        /// <returns>True if the formm is in the Otis form list</returns>
        public bool IsOtisForm(int formId)
        {
            bool result = false;

            QSPForm.Business.Properties.Settings settings = new QSPForm.Business.Properties.Settings();
            string formListString = settings.OtisFormList;
            string[] formArray = formListString.Split(',');

            foreach (string item in formArray)
            {
                if (Convert.ToInt32(item) == formId)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if the specified form is in the Hershey form list
        /// </summary>
        /// <param name="formId">The form to check</param>
        /// <returns>True if the form is in the Hershey form list</returns>
        public bool IsHersheyForm(int formId)
        {
            bool result = false;

            QSPForm.Business.Properties.Settings settings = new QSPForm.Business.Properties.Settings();
            string formListString = settings.HersheyFormList;
            string[] formArray = formListString.Split(',');

            foreach (string item in formArray)
            {
                if (Convert.ToInt32(item) == formId)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        #endregion

        #region Week of methods

        public List<WeekOfItem> GetWeekOfOptionList(int formId)
        {
            List<WeekOfItem> result = new List<WeekOfItem>();

            int deliveryDateTypeId = 0;
            DateTime startDate = DateTime.Now;
            DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);

            #region Get the delivery date type from the form id

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            // Get delivery date type
            var query1 = from   fddt in context.FormDeliveryDateTypes
                         where  fddt.FormId == formId
                         select fddt;

            List<LinqEntity.FormDeliveryDateType> formDeliveryDateTypeList = query1.ToList();

            if (formDeliveryDateTypeList.Count > 0)
            {
                deliveryDateTypeId = formDeliveryDateTypeList[0].DeliveryDateTypeId;
            }

            // Get start and end dates
            var query2 = from   f in context.Forms
                         where  f.FormId == formId
                         select f;

            LinqEntity.Form form = query2.SingleOrDefault();

            if (form.StartDate.HasValue)
            {
                startDate = form.StartDate.Value;
            }

            if (form.EndDate.HasValue)
            {
                endDate = form.EndDate.Value;
            }

            #endregion

            if (deliveryDateTypeId == (int)DeliveryDateTypeEnum.ChooseWeekStartingOnSunday)
            {
                //result = this.GetWeekOfOptionList_3(startDate, endDate);
            }
            else if (deliveryDateTypeId == (int)DeliveryDateTypeEnum.ChooseWeekStartingOnMonday)
            {
                //result = this.GetWeekOfOptionList_4(startDate, endDate);
            }
            else if (deliveryDateTypeId == (int)DeliveryDateTypeEnum.ChooseWeekStartingOnSundayOtisLogic)
            {
                result = this.GetWeekOfOptionList_5(startDate, endDate);
            }
            else if (deliveryDateTypeId == (int)DeliveryDateTypeEnum.ChooseWeekStartingOnMondayOtisLogic)
            {
                //result = this.GetWeekOfOptionList_6(startDate, endDate);
            }

            return result;
        }
        public WeekOfItem GetWeekOfItem(int formId, int weekNumber, int year)
        {
            WeekOfItem result = new WeekOfItem();

            int deliveryDateTypeId = 0;

            #region Get the delivery date type from the form id

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            // Get delivery date type
            var query1 = from fddt in context.FormDeliveryDateTypes
                         where fddt.FormId == formId
                         select fddt;

            List<LinqEntity.FormDeliveryDateType> formDeliveryDateTypeList = query1.ToList();

            if (formDeliveryDateTypeList.Count > 0)
            {
                deliveryDateTypeId = formDeliveryDateTypeList[0].DeliveryDateTypeId;
            }

            #endregion

            if (deliveryDateTypeId == (int)DeliveryDateTypeEnum.ChooseWeekStartingOnSunday)
            {
                result = this.GetWeekOfItem_3(weekNumber, year);
            }
            else if (deliveryDateTypeId == (int)DeliveryDateTypeEnum.ChooseWeekStartingOnMonday)
            {
                result = this.GetWeekOfItem_4(weekNumber, year);
            }
            else if (deliveryDateTypeId == (int)DeliveryDateTypeEnum.ChooseWeekStartingOnSundayOtisLogic)
            {
                result = this.GetWeekOfItem_5(weekNumber, year);
            }
            else if (deliveryDateTypeId == (int)DeliveryDateTypeEnum.ChooseWeekStartingOnMondayOtisLogic)
            {
                result = this.GetWeekOfItem_6(weekNumber, year);
            }

            return result;
        }
        public WeekOfItem GetWeekOfItem(int formId, DateTime selectedDate)
        {
            WeekOfItem result = new WeekOfItem();

            int deliveryDateTypeId = 0;

            #region Get the delivery date type from the form id

            LinqContext.OrderExpressDataContext context = new LinqContext.OrderExpressDataContext();

            // Get delivery date type
            var query1 = from fddt in context.FormDeliveryDateTypes
                         where fddt.FormId == formId
                         select fddt;

            List<LinqEntity.FormDeliveryDateType> formDeliveryDateTypeList = query1.ToList();

            if (formDeliveryDateTypeList.Count > 0)
            {
                deliveryDateTypeId = formDeliveryDateTypeList[0].DeliveryDateTypeId;
            }

            #endregion

            if (deliveryDateTypeId == (int)DeliveryDateTypeEnum.ChooseWeekStartingOnSunday)
            {
                result = this.GetWeekOfItem_3(selectedDate);
            }
            else if (deliveryDateTypeId == (int)DeliveryDateTypeEnum.ChooseWeekStartingOnMonday)
            {
                result = this.GetWeekOfItem_4(selectedDate);
            }
            else if (deliveryDateTypeId == (int)DeliveryDateTypeEnum.ChooseWeekStartingOnSundayOtisLogic)
            {
                result = this.GetWeekOfItem_5(selectedDate);
            }
            else if (deliveryDateTypeId == (int)DeliveryDateTypeEnum.ChooseWeekStartingOnMondayOtisLogic)
            {
                result = this.GetWeekOfItem_6(selectedDate);
            }

            return result;
        }

        #region delivery_date_type = 3 (Choose a week starting in Sunday)

        private Dictionary<int, string> GetWeekOfOptionList_3()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            DayOfWeek weekStartDay = DayOfWeek.Sunday;
            int currentYear = DateTime.Now.Year;
            int currentWeek = QSPForm.Common.WeekAndDateUtilities.GetWeekNumber(DateTime.Now, weekStartDay);

            for (int i = currentWeek; i < 54; i++)
            {
                // string description = "Week " + i.ToString() + " from " + QSPForm.Common.WeekAndDateUtilities.GetWeekStart(i, currentYear, weekStartDay).ToShortDateString() + " to " + QSPForm.Common.WeekAndDateUtilities.GetWeekEnd(i, currentYear, weekStartDay).ToShortDateString();
                string description = "Week of " + QSPForm.Common.WeekAndDateUtilities.GetWeekStart(i, currentYear, weekStartDay).ToString("MMMM dd, yyyy");

                result.Add(i, description);
            }

            return result;
        }
        private WeekOfItem GetWeekOfItem_3(int weekNumber, int year)
        {
            WeekOfItem result = new WeekOfItem();

            DayOfWeek weekStartDay = DayOfWeek.Sunday;

            result.WeekNumber = weekNumber;
            result.Year = year;
            result.StartDate = WeekAndDateUtilities.GetWeekStart(weekNumber, year, weekStartDay);
            result.EndDate = WeekAndDateUtilities.GetWeekEnd(weekNumber, year, weekStartDay);

            return result;
        }
        private WeekOfItem GetWeekOfItem_3(DateTime selectedDate)
        {
            WeekOfItem result;

            DayOfWeek weekStartDay = DayOfWeek.Sunday;

            int weekNumber = WeekAndDateUtilities.GetWeekNumber(selectedDate, weekStartDay);

            result = this.GetWeekOfItem_5(weekNumber, selectedDate.Year);

            return result;
        }

        #endregion

        #region delivery_date_type = 4 (Choose a week starting in Monday)

        private Dictionary<int, string> GetWeekOfOptionList_4()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            DayOfWeek weekStartDay = DayOfWeek.Monday;
            int currentYear = DateTime.Now.Year;
            int currentWeek = QSPForm.Common.WeekAndDateUtilities.GetWeekNumber(DateTime.Now, weekStartDay);

            for (int i = currentWeek; i < 54; i++)
            {
                // string description = "Week " + i.ToString() + " from " + QSPForm.Common.WeekAndDateUtilities.GetWeekStart(i, currentYear, weekStartDay).ToShortDateString() + " to " + QSPForm.Common.WeekAndDateUtilities.GetWeekEnd(i, currentYear, weekStartDay).ToShortDateString();
                string description = "Week of " + QSPForm.Common.WeekAndDateUtilities.GetWeekStart(i, currentYear, weekStartDay).ToString("MMMM dd, yyyy");

                result.Add(i, description);
            }

            return result;
        }
        private WeekOfItem GetWeekOfItem_4(int weekNumber, int year)
        {
            WeekOfItem result = new WeekOfItem();

            DayOfWeek weekStartDay = DayOfWeek.Monday;

            result.WeekNumber = weekNumber;
            result.Year = year;
            result.StartDate = WeekAndDateUtilities.GetWeekStart(weekNumber, year, weekStartDay);
            result.EndDate = WeekAndDateUtilities.GetWeekEnd(weekNumber, year, weekStartDay);

            return result;
        }
        private WeekOfItem GetWeekOfItem_4(DateTime selectedDate)
        {
            WeekOfItem result;

            DayOfWeek weekStartDay = DayOfWeek.Monday;

            int weekNumber = WeekAndDateUtilities.GetWeekNumber(selectedDate, weekStartDay);

            result = this.GetWeekOfItem_5(weekNumber, selectedDate.Year);

            return result;
        }

        #endregion

        #region delivery_date_type = 5 (Otis Spring 2009, week starting in Sunday, 3 to 4 weeks cutoff)

        private List<WeekOfItem> GetWeekOfOptionList_5(DateTime startDate, DateTime endDate)
        {
            List<WeekOfItem> result = new List<WeekOfItem>();

            int cutoffWeeks = 0;
            DayOfWeek weekStartDay = DayOfWeek.Sunday;

            #region Check start date

            if (DateTime.Now > startDate)
            {
                int originalStartDateWeekNumber = WeekAndDateUtilities.GetWeekNumber(DateTime.Now, weekStartDay);
                startDate = this.GetWeekOfItem_5(originalStartDateWeekNumber, DateTime.Now.Year).StartDate;
            }

            #endregion

            #region Get cutoff weeks

            DateTime centralTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday && centralTime.Hour > 17)
            {
                // It is friday past 5, we have 4 weeks
                cutoffWeeks = 4;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                // It is saturday, we have 4 weeks
                cutoffWeeks = 4;
            }
            else
            {
                cutoffWeeks = 3;
            }

            #endregion

            int startWeekNumber = WeekAndDateUtilities.GetWeekNumber(startDate, weekStartDay);
            int startWeekNumberWithCutoff = startWeekNumber + cutoffWeeks;
            WeekOfItem startWeekOfItem = this.GetWeekOfItem_5(startWeekNumberWithCutoff, startDate.Year);

            int endWeekNumber = WeekAndDateUtilities.GetWeekNumber(endDate, weekStartDay);
            WeekOfItem endWeekOfItem = this.GetWeekOfItem_5(endWeekNumber, endDate.Year);

            WeekOfItem currentWeekOfItem = startWeekOfItem;
            while (currentWeekOfItem.StartDate <= endWeekOfItem.StartDate)
            {
                // Add new week of item to the result list
                WeekOfItem newItem = new WeekOfItem();

                newItem.StartDate = currentWeekOfItem.StartDate;
                newItem.EndDate = currentWeekOfItem.EndDate;
                newItem.WeekNumber = currentWeekOfItem.WeekNumber;
                newItem.Year = currentWeekOfItem.Year;

                result.Add(newItem);

                // Get next week of item
                currentWeekOfItem.StartDate = currentWeekOfItem.StartDate.AddDays(7);
                currentWeekOfItem.EndDate = currentWeekOfItem.EndDate.AddDays(7);
                currentWeekOfItem.WeekNumber = WeekAndDateUtilities.GetWeekNumber(currentWeekOfItem.StartDate, weekStartDay);
                currentWeekOfItem.Year = currentWeekOfItem.StartDate.Year;
            }

            return result;
        }
        private WeekOfItem GetWeekOfItem_5(int weekNumber, int year)
        {
            WeekOfItem result = new WeekOfItem();

            DayOfWeek weekStartDay = DayOfWeek.Sunday;

            result.WeekNumber = weekNumber;
            result.Year = year;
            result.StartDate = WeekAndDateUtilities.GetWeekStart(weekNumber, year, weekStartDay);
            result.EndDate = WeekAndDateUtilities.GetWeekEnd(weekNumber, year, weekStartDay);

            return result;
        }
        private WeekOfItem GetWeekOfItem_5(DateTime selectedDate)
        {
            WeekOfItem result;

            DayOfWeek weekStartDay = DayOfWeek.Sunday;

            int weekNumber = WeekAndDateUtilities.GetWeekNumber(selectedDate, weekStartDay);

            result = this.GetWeekOfItem_5(weekNumber, selectedDate.Year);

            return result;
        }

        #endregion

        #region delivery_date_type = 6 (Otis Spring 2009, week starting in Monday, 3 to 4 weeks cutoff)

        private Dictionary<int, string> GetWeekOfOptionList_6()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            DayOfWeek weekStartDay = DayOfWeek.Monday;
            int currentYear = DateTime.Now.Year;
            int currentWeek = QSPForm.Common.WeekAndDateUtilities.GetWeekNumber(DateTime.Now, weekStartDay);
            int cutoffWeeks = 0;

            #region Get cutoff weeks

            DateTime centralTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday && centralTime.Hour > 17)
            {
                // It is friday past 5, we have 4 weeks
                cutoffWeeks = 4;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                // It is saturday, we have 4 weeks
                cutoffWeeks = 4;
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                // It is sunday, we have 4 weeks
                cutoffWeeks = 4;
            }
            else
            {
                cutoffWeeks = 3;
            }

            #endregion

            for (int i = (currentWeek + cutoffWeeks); i < 54; i++)
            {
                // string description = "Week " + i.ToString() + " from " + QSPForm.Common.WeekAndDateUtilities.GetWeekStart(i, currentYear, weekStartDay).ToShortDateString() + " to " + QSPForm.Common.WeekAndDateUtilities.GetWeekEnd(i, currentYear, weekStartDay).ToShortDateString();
                string description = "Week of " + QSPForm.Common.WeekAndDateUtilities.GetWeekStart(i, currentYear, weekStartDay).ToString("MMMM dd, yyyy");

                result.Add(i, description);
            }

            return result;
        }
        private WeekOfItem GetWeekOfItem_6(int weekNumber, int year)
        {
            WeekOfItem result = new WeekOfItem();

            DayOfWeek weekStartDay = DayOfWeek.Monday;

            result.WeekNumber = weekNumber;
            result.Year = year;
            result.StartDate = WeekAndDateUtilities.GetWeekStart(weekNumber, year, weekStartDay);
            result.EndDate = WeekAndDateUtilities.GetWeekEnd(weekNumber, year, weekStartDay);

            return result;
        }
        private WeekOfItem GetWeekOfItem_6(DateTime selectedDate)
        {
            WeekOfItem result;

            DayOfWeek weekStartDay = DayOfWeek.Monday;

            int weekNumber = WeekAndDateUtilities.GetWeekNumber(selectedDate, weekStartDay);

            result = this.GetWeekOfItem_5(weekNumber, selectedDate.Year);

            return result;
        }

        #endregion

        #endregion

        #endregion

    }
}
