namespace QSPForm.Business
{

    using System;
    using System.Data;
    using System.Linq;
    using QSPForm.Common.DataDef;
    using QSPForm.Data;
    using dataDef = QSPForm.Common.DataDef.BusinessRuleTable;
    using dataAccessRef = QSPForm.Data.Business_rule;
    using QSPForm.Common;
    using System.Collections.Generic;

    /// <summary>
    ///     This class contains the business rules that are used for a 
    ///     campaign.
    /// </summary>
    public class BusinessRuleSystem : BusinessSystem
    {
        dataAccessRef objDataAccess;

        public BusinessRuleSystem()
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


            isValid &= IsValid_FieldLength(campaignRow, dataDef.FLD_NAME, "Business Rule Name", 50);


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
            IsValid &= IsValid_RequiredField(row, dataDef.FLD_NAME, "Business Rule Name");

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
            // Get the DataTable from the DataLayer
            //
            return objDataAccess.SelectAll();
        }

        public dataDef SelectOne(int ID)
        {
            //
            // Get the DataTable from the DataLayer
            //
            return objDataAccess.SelectOne(ID);
        }


        public dataDef SelectAllByFormID(int FormID)
        {
            //
            // Get the DataTable from the DataLayer
            //
            return SelectAllByFormID(FormID, false);
        }

        public dataDef SelectAllByFormID(int FormID, bool IncludeAllDerivedElements)
        {
            //
            // Get the DataTable from the DataLayer
            //			
            return objDataAccess.SelectAllWform_idLogic(FormID, IncludeAllDerivedElements);
        }

        public dataDef SelectAllByBusinessFieldID(int BizFieldID)
        {
            //
            // Get the DataTable from the DataLayer
            //
            return objDataAccess.SelectAllWfield_idLogic(BizFieldID);
        }

        public dataDef SelectAllByBusinessFieldID(int BizFieldID, int FormID)
        {
            //
            // Get the DataTable from the DataLayer
            //
            return objDataAccess.SelectAllWfield_idLogic(BizFieldID, FormID);
        }

        public dataDef SelectAllByBusinessFieldID(int BizFieldID, int FormID, int FormSectionTypeID)
        {
            //
            // Get the DataTable from the DataLayer
            //
            return objDataAccess.SelectAllWfield_idLogic(BizFieldID, FormID, FormSectionTypeID);
        }

        public dataDef SelectAllByBusinessFieldID(int BizFieldID, int FormID, int FormSectionTypeID, int FormSectionNumber)
        {
            //
            // Get the DataTable from the DataLayer
            //
            return objDataAccess.SelectAllWfield_idLogic(BizFieldID, FormID, FormSectionTypeID, FormSectionNumber);
        }

        public dataDef SelectAllByBusinessFieldName(string BizFieldName, int FormID)
        {
            //
            // Get the DataTable from the DataLayer
            //
            return objDataAccess.SelectAllWfield_nameLogic(BizFieldName, FormID);
        }

        public dataDef SelectAllByBusinessRuleName(string BizRuleName, int FormID)
        {
            //
            // Get the DataTable from the DataLayer
            //
            return objDataAccess.SelectAllWbusiness_rule_nameLogic(BizRuleName, FormID);
        }

        public int GetMinNbDayLeadTime(int FormID)
        {
            int toReturn = 0;
            string BizRuleName = QSPForm.Common.FormPropertyName.MIN_NB_DAY_LEAD_TIME;
            try
            {
                //
                // Get the DataTable from the DataLayer
                //
                dataDef dTbl = objDataAccess.SelectAllWfield_nameLogic(BizRuleName, FormID);
                if (dTbl.Rows.Count > 0)
                {
                    DataRow row = dTbl.Rows[0];
                    toReturn = Convert.ToInt32(row[dataDef.FLD_VALUE_TO_COMPARE]);
                }
            }
            catch (Exception ex)
            {
                toReturn = 0;
            }

            return toReturn;
        }

        public int SetMinNbDayLeadTime(OrderData orderData, int FormID, int FormSectionTypeID)
        {
            OrderDetailTable dTblOrderDetail = orderData.OrderDetail;
            int toReturn = 0;
            int FieldID = QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME;
            int additionalLeadTime = 0;

            try
            {
                QSPForm.Business.FormSystem formSystem = new QSPForm.Business.FormSystem();
                FormTable formTable = formSystem.SelectOne(FormID);

                //Calculate leadtime for orders going to CHR Warehouses
                int warehouseTypeID = 0;
                if (formTable.Rows[0][FormTable.FLD_WAREHOUSE_TYPE_ID] != System.DBNull.Value)
                {
                    warehouseTypeID = Convert.ToInt32(formTable.Rows[0][FormTable.FLD_WAREHOUSE_TYPE_ID].ToString());
                }

                if (warehouseTypeID == QSPForm.Common.WarehouseType.CHRobinson)
                {
                    #region CHR calculation

                    PostalAddressEntityTable postalAddressTable = (PostalAddressEntityTable)orderData.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY];
                    DataView dvPostalAddress = new DataView(postalAddressTable);
                    dvPostalAddress.RowFilter = PostalAddressEntityTable.FLD_TYPE + " = " + PostalAddressType.TYPE_SHIPPING.ToString();
                    if (dvPostalAddress.Count > 0)
                    {
                        int leadTime = 0;

                        DataRow row = dvPostalAddress[0].Row;
                        string zipCode = row[PostalAddressEntityTable.FLD_ZIP].ToString();

                        ShipmentGroupTable shipmentGroupTable = (ShipmentGroupTable)orderData.Tables[ShipmentGroupTable.TBL_SHIPMENT_GROUP];
                        DataRow sgtRow = shipmentGroupTable.Rows[0];
                        int deliveryMethod = Convert.ToInt32(sgtRow[21]);

                        leadTime = GetLeadTime_CHR_BusinessDaysConsideringHolidays(zipCode, deliveryMethod);

                        foreach (DataRow orderDetailRow in dTblOrderDetail)
                        {
                            orderDetailRow[OrderDetailTable.FLD_NB_DAY_LEAD_TIME] = leadTime;
                        }

                        toReturn = leadTime;
                    }

                    #endregion
                }

                else
                {
                    #region Normal calculation

                    //
                    // Get the DataTable from the DataLayer
                    //
                    dataDef dTbl = objDataAccess.SelectAllWfield_idLogic(FieldID, FormID, QSPForm.Common.FormSectionType.STANDARD_PRODUCT);
                    DataView dv = new DataView(dTbl);
                    int minDLT = 0;
                    int minDLT_Section_1 = 0;
                    int minDLT_Section_2 = 0;
                    int minDLT_Section_3 = 0;
                    dv.RowFilter = "ISNULL(" + dataDef.FLD_FORM_SECTION_NUMBER + ",0) = 0";
                    if (dv.Count > 0)
                    {
                        DataRow row = dv[0].Row;
                        minDLT = Convert.ToInt32(row[dataDef.FLD_VALUE_TO_COMPARE]);
                    }
                    dv.RowFilter = "ISNULL(" + dataDef.FLD_FORM_SECTION_NUMBER + ",0) = 1";
                    if (dv.Count > 0)
                    {
                        DataRow row = dv[0].Row;
                        minDLT_Section_1 = Convert.ToInt32(row[dataDef.FLD_VALUE_TO_COMPARE]);
                    }
                    dv.RowFilter = "ISNULL(" + dataDef.FLD_FORM_SECTION_NUMBER + ",0) = 2";
                    if (dv.Count > 0)
                    {
                        DataRow row = dv[0].Row;
                        minDLT_Section_2 = Convert.ToInt32(row[dataDef.FLD_VALUE_TO_COMPARE]);
                    }
                    dv.RowFilter = "ISNULL(" + dataDef.FLD_FORM_SECTION_NUMBER + ",0) = 3";
                    if (dv.Count > 0)
                    {
                        DataRow row = dv[0].Row;
                        minDLT_Section_3 = Convert.ToInt32(row[dataDef.FLD_VALUE_TO_COMPARE]);
                    }
                    //Populate from Section 1
                    if (minDLT_Section_1 == 0)
                        minDLT_Section_1 = minDLT;
                    DataView dvOrderDetail = new DataView(dTblOrderDetail);
                    dvOrderDetail.RowFilter = "ISNULL(" + OrderDetailTable.FLD_FORM_SECTION_TYPE_ID + ",1) = " + FormSectionType.STANDARD_PRODUCT +
                        " AND ISNULL(" + OrderDetailTable.FLD_FORM_SECTION_NUMBER + ",1) = 1";
                    foreach (DataRowView drvw in dvOrderDetail)
                    {
                        drvw[OrderDetailTable.FLD_NB_DAY_LEAD_TIME] = minDLT_Section_1 + additionalLeadTime;
                    }
                    //Populate from Section 2
                    if (minDLT_Section_2 == 0)
                        minDLT_Section_2 = minDLT;
                    dvOrderDetail = new DataView(dTblOrderDetail);
                    dvOrderDetail.RowFilter = "ISNULL(" + OrderDetailTable.FLD_FORM_SECTION_TYPE_ID + ",1) = " + FormSectionType.STANDARD_PRODUCT +
                        " AND " + OrderDetailTable.FLD_FORM_SECTION_NUMBER + " = 2";
                    foreach (DataRowView drvw in dvOrderDetail)
                    {
                        drvw[OrderDetailTable.FLD_NB_DAY_LEAD_TIME] = minDLT_Section_2 + additionalLeadTime;
                    }

                    //Populate from Section 3
                    if (minDLT_Section_3 == 0)
                        minDLT_Section_3 = minDLT;
                    dvOrderDetail = new DataView(dTblOrderDetail);
                    dvOrderDetail.RowFilter = "ISNULL(" + OrderDetailTable.FLD_FORM_SECTION_TYPE_ID + ",1) = " + FormSectionType.STANDARD_PRODUCT +
                        " AND " + OrderDetailTable.FLD_FORM_SECTION_NUMBER + " = 3";
                    foreach (DataRowView drvw in dvOrderDetail)
                    {
                        drvw[OrderDetailTable.FLD_NB_DAY_LEAD_TIME] = minDLT_Section_3 + additionalLeadTime;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                toReturn = 0;
            }

            return toReturn;
        }

        public int GetMinNbDayLeadTime_Supply(int FormID)
        {
            int toReturn = 0;
            int BizRuleID = QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME;
            try
            {
                //
                // Get the DataTable from the DataLayer
                //
                dataDef dTbl = objDataAccess.SelectAllWfield_idLogic(BizRuleID, FormID, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT);
                string sValue = dTbl.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 0);
                if (sValue.Length > 0)
                {
                    toReturn = Convert.ToInt32(sValue);
                }
            }
            catch (Exception ex)
            {
                toReturn = 0;
            }

            return toReturn;
        }


        public int GetLeadTime_CHR_BusinessDays(string zipCode, int deliveryMethod)
        {
            int leadTimeInBusinessDays = 0;

            if (deliveryMethod == 2)
            {
                // Pickup in warehouse
                leadTimeInBusinessDays = 1;
            }
            else
            {
                // Common carrier
                QSP.Business.Fulfillment.ZipPrefix zipCodePrefixObject = QSP.Business.Fulfillment.ZipPrefix.GetZipCodePrefixByZipCode(zipCode);

                List<QSP.Business.Fulfillment.WarehouseZipPrefixLeadtime> leadTimeObjectList = QSP.Business.Fulfillment.WarehouseZipPrefixLeadtime.GetWarehouseZipPrefixLeadtimeFromZipPrefixId(zipCodePrefixObject.ZipPrefixID);

                if (leadTimeObjectList.Count == 0)
                {
                }
                else if (leadTimeObjectList.Count == 1)
                {
                    leadTimeInBusinessDays = leadTimeObjectList[0].Leadtime ?? 0;
                }
                else
                {
                    QSP.Business.Fulfillment.WarehouseZipPrefixLeadtime warehouseZipPrefixLeadtimeObject =
                        (from lt in leadTimeObjectList
                         where lt.Zip.Trim() == zipCode.Trim()
                         select lt).Single<QSP.Business.Fulfillment.WarehouseZipPrefixLeadtime>();

                    leadTimeInBusinessDays = warehouseZipPrefixLeadtimeObject.Leadtime ?? 0;
                }
            }

            return leadTimeInBusinessDays;
        }
        public int GetLeadTime_CHR_BusinessDaysConsideringHolidays(string zipCode, int deliveryMethod)
        {
            int leadTimeInNaturalDays = 0;
            DateTime startDate = DateTime.Now;

            if (deliveryMethod == 2)
            {
                // Pickup in warehouse
                leadTimeInNaturalDays = 1;
            }
            else
            {
                // Common carrier
                int leadTimeInBusinessDays = GetLeadTime_CHR_BusinessDays(zipCode, deliveryMethod);

                leadTimeInNaturalDays = QSP.Business.Fulfillment.BusinessCalendar.GetBusinessDaysConsideringHolidays(leadTimeInBusinessDays, startDate);

                // If it is over 2 PM, add one more day
                if (startDate.Hour > 14)
                {
                    leadTimeInNaturalDays++;
                }

                // Add one more day for order picking time
                leadTimeInNaturalDays++;
            }

            return leadTimeInNaturalDays;
        }
        public int GetLeadTime_CHR_NaturalDays(string zipCode, int deliveryMethod)
        {
            int leadTimeInNaturalDays = 0;
            DateTime startDate = DateTime.Now;

            if (deliveryMethod == 1)
            {
                // Pickup in warehouse
                leadTimeInNaturalDays = 1;
            }
            else
            {
                // Common carrier
                int leadTimeInBusinessDays = GetLeadTime_CHR_BusinessDays(zipCode, deliveryMethod);

                leadTimeInNaturalDays = QSP.Business.Fulfillment.BusinessCalendar.GetNaturalDaysFromBusinessDays(leadTimeInBusinessDays, startDate);

                // If it is over 2 PM, add one more day
                if (startDate.Hour > 14)
                {
                    leadTimeInNaturalDays++;
                }

                // Add one more day for order picking time
                leadTimeInNaturalDays++;
            }

            return leadTimeInNaturalDays;
        }




        public int GetMinTotalQuantity(int FormID, int FormSectionTypeID)
        {
            return GetMinTotalQuantity(FormID, FormSectionTypeID, 0);
        }

        public int GetMinTotalQuantity(int FormID, int FormSectionTypeID, int FormSectionNumber)
        {
            int toReturn = 0;
            int FldID = QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY;
            try
            {
                //
                // Get the DataTable from the DataLayer
                //
                dataDef dTbl = objDataAccess.SelectAllWfield_idLogic(FldID, FormID, FormSectionTypeID, FormSectionNumber);
                string sValue = dTbl.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, FormSectionTypeID, FormSectionNumber);
                if (sValue.Length > 0)
                {
                    toReturn = Convert.ToInt32(sValue);
                }
            }
            catch (Exception ex)
            {
                toReturn = 0;
            }

            return toReturn;
        }

        public int GetMinLineItemQuantity(int FormID, int FormSectionTypeID)
        {

            return GetMinLineItemQuantity(FormID, FormSectionTypeID, 0);

        }
        public int GetMinLineItemQuantity(int FormID, int FormSectionTypeID, int FormSectionNumber)
        {
            int toReturn = 0;
            int FldID = QSPForm.Common.FormProperty.MIN_LINE_ITEM_QUANTITY;

            try
            {
                //
                // Get the DataTable from the DataLayer
                //
                dataDef dTbl = new dataDef();
                if (FormSectionNumber == 0)
                {
                    dTbl = objDataAccess.SelectAllWfield_idLogic(FldID, FormID, FormSectionTypeID);
                }
                else
                {
                    dTbl = objDataAccess.SelectAllWfield_idLogic(FldID, FormID, FormSectionTypeID, FormSectionNumber);
                }

                string sValue = dTbl.GetFormPropertyValueString(FldID, FormSectionTypeID, FormSectionNumber);
                if (sValue.Length > 0)
                {
                    toReturn = Convert.ToInt32(sValue);
                }
            }
            catch (Exception ex)
            {
                toReturn = 0;
            }

            return toReturn;
        }

        public decimal GetMinTotalAmount(int FormID, int FormSectionTypeID)
        {
            decimal toReturn = 0;
            int FieldID = QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT;
            try
            {
                //
                // Get the DataTable from the DataLayer
                //
                dataDef dTbl = objDataAccess.SelectAllWfield_idLogic(FieldID, FormID, FormSectionTypeID);
                if (dTbl.Rows.Count > 0)
                {
                    DataRow row = dTbl.Rows[0];
                    toReturn = Convert.ToDecimal(row[dataDef.FLD_VALUE_TO_COMPARE]);
                }
            }
            catch (Exception ex)
            {
                toReturn = 0;
            }

            return toReturn;
        }

        public int GetMaxTotalAmount(int FormID, int FormSectionTypeID)
        {
            int toReturn = 0;
            int BizRuleID = QSPForm.Common.FormProperty.MAX_TOTAL_AMOUNT;
            try
            {
                //
                // Get the DataTable from the DataLayer
                //
                dataDef dTbl = objDataAccess.SelectAllWfield_idLogic(BizRuleID, FormID, FormSectionTypeID);
                if (dTbl.Rows.Count > 0)
                {
                    DataRow row = dTbl.Rows[0];
                    toReturn = Convert.ToInt32(row[dataDef.FLD_VALUE_TO_COMPARE]);
                }
            }
            catch (Exception ex)
            {
                toReturn = 0;
            }

            return toReturn;
        }

        public int GetAccoutHistoryInterval_NbDay(dataDef dTbl)
        {
            int toReturn = 0;
            int FieldID = QSPForm.Common.FormProperty.ACCOUNT_HISTORY_INTERVAL_NB_DAY;
            try
            {
                //
                // Get the DataTable from the DataLayer
                //
                string sValue = dTbl.GetFormPropertyValueString(FieldID, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0);
                if (sValue.Length > 0)
                {
                    toReturn = Convert.ToInt32(sValue);
                }
            }
            catch
            {
                toReturn = 0;
            }

            return toReturn;
        }

        public string GetCommonCarrierName(int FormID)
        {
            string toReturn = "";
            int FieldID = QSPForm.Common.FormProperty.COMMON_CARRIER_NAME;
            try
            {
                //
                // Get the DataTable from the DataLayer
                //
                dataDef dTbl = objDataAccess.SelectAllWfield_idLogic(FieldID, FormID);
                string sValue = dTbl.GetFormPropertyValueString(FieldID, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0);
                if (sValue.Trim().Length > 0)
                {
                    toReturn = sValue;
                }
            }
            catch (Exception ex)
            {
                toReturn = "";
            }

            return toReturn;
        }


        public void AddColumnValidation(DataRow valRow, string ColumnName, string ValueToCompare, int FieldType)
        {
            bool IsValid = true;
            //Field Type
            //	1	String 
            //	2	Integer 
            //	3	Double 
            //	4	Date 
            //	5	Currency 
            //DataRow valRow = dTbl.Rows[0];

            switch (FieldType)
            {
                case 1: // String
                    {
                        valRow.Table.Columns.Add(ColumnName, typeof(System.String));
                        valRow[ColumnName] = ValueToCompare;
                        break;
                    }
                case 2: //Integer
                    {
                        valRow.Table.Columns.Add(ColumnName, typeof(System.Int32));
                        valRow[ColumnName] = Convert.ToInt32(ValueToCompare);
                        break;
                    }
                case 3: //Double
                    {
                        valRow.Table.Columns.Add(ColumnName, typeof(System.Decimal));
                        valRow[ColumnName] = Convert.ToDecimal(ValueToCompare);
                        break;
                    }
                case 4: //Date
                    {
                        valRow.Table.Columns.Add(ColumnName, typeof(System.DateTime));

                        DateTime val1 = DateTime.Today;
                        if (ValueToCompare.ToUpper() != "TODAY")
                        {
                            val1 = Convert.ToDateTime(ValueToCompare);
                        }
                        valRow[ColumnName] = val1;
                        break;
                    }
                case 5: //Currency
                    {
                        valRow.Table.Columns.Add(ColumnName, typeof(System.Decimal));
                        valRow[ColumnName] = Convert.ToDecimal(ValueToCompare);
                        break;
                    }
                case 6: //Boolean
                    {
                        valRow.Table.Columns.Add(ColumnName, typeof(System.Boolean));
                        valRow[ColumnName] = Convert.ToBoolean(ValueToCompare);
                        break;
                    }
            }

        }

        public void SetColumnFormProperty(DataRow valRow, string ColumnName, string ValueToCompare, int FieldType)
        {
            bool IsValid = true;
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
                        valRow[ColumnName] = ValueToCompare;
                        break;
                    }
                case 2: //Integer
                    {
                        valRow[ColumnName] = Convert.ToInt32(ValueToCompare);
                        break;
                    }
                case 3: //Double
                    {
                        valRow[ColumnName] = Convert.ToDecimal(ValueToCompare);
                        break;
                    }
                case 4: //Date
                    {
                        DateTime val1 = DateTime.Today;
                        if (ValueToCompare.ToUpper() != "TODAY")
                        {
                            val1 = Convert.ToDateTime(ValueToCompare);
                        }
                        valRow[ColumnName] = val1;
                        break;
                    }
                case 5: //Currency
                    {
                        valRow[ColumnName] = Convert.ToDecimal(ValueToCompare);
                        break;
                    }
                case 6: //Boolean
                    {
                        valRow[ColumnName] = Convert.ToBoolean(ValueToCompare);
                        break;
                    }
            }

        }


        public bool BizRuleValidate(string Value, int Operator, string ValueToCompare, int FieldType)
        {
            bool IsValid = true;
            //Logical Operator
            //	1	Equal 
            //	2	NotEqual 
            //	3	GreaterThan 
            //	4	GreaterThanEqual 
            //	5	LessThan 
            //	6	LessThanEqual

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
                        string val = Value;
                        string val1 = ValueToCompare;
                        IsValid = BizRuleValidateString(val, Operator, val1);
                        break;
                    }
                case 2: //Integer
                    {
                        int val = Convert.ToInt32(Value);
                        int val1 = Convert.ToInt32(ValueToCompare);
                        IsValid = BizRuleValidateInteger(val, Operator, val1);
                        break;
                    }
                case 3: //Double
                    {
                        decimal val = Convert.ToDecimal(Value);
                        decimal val1 = Convert.ToDecimal(ValueToCompare);
                        IsValid = BizRuleValidateDecimal(val, Operator, val1);
                        break;
                    }
                case 4: //Date
                    {
                        DateTime val = Convert.ToDateTime(Value);
                        DateTime val1 = DateTime.Today;
                        if (ValueToCompare.ToUpper() != "TODAY")
                        {
                            val1 = Convert.ToDateTime(ValueToCompare);
                        }
                        IsValid = BizRuleValidateDate(val, Operator, val1);
                        break;
                    }
                case 5: //Currency
                    {
                        decimal val = Convert.ToDecimal(Value);
                        decimal val1 = Convert.ToDecimal(ValueToCompare);
                        IsValid = BizRuleValidateDecimal(val, Operator, val1);
                        break;
                    }
                case 6: //Boolean
                    {
                        bool val = Convert.ToBoolean(Value);
                        bool val1 = Convert.ToBoolean(ValueToCompare);
                        IsValid = BizRuleValidateBoolean(val, Operator, val1);
                        break;
                    }
            }
            return IsValid;

        }

        private bool BizRuleValidateInteger(int Value, int Operator, int ValueToCompare)
        {
            bool IsValid = true;
            //Logical Operator
            //	1	Equal 
            //	2	NotEqual 
            //	3	GreaterThan 
            //	4	GreaterThanEqual 
            //	5	LessThan 
            //	6	LessThanEqual

            switch (Operator)
            {
                case 1:
                    {
                        IsValid = (Value == ValueToCompare);
                        break;
                    }
                case 2:
                    {
                        IsValid = (Value != ValueToCompare);
                        break;
                    }
                case 3:
                    {
                        IsValid = (Value > ValueToCompare);
                        break;
                    }
                case 4:
                    {
                        IsValid = (Value >= ValueToCompare);
                        break;
                    }
                case 5:
                    {
                        IsValid = (Value < ValueToCompare);
                        break;
                    }
                case 6:
                    {
                        IsValid = (Value <= ValueToCompare);
                        break;
                    }
            }
            return IsValid;

        }

        private bool BizRuleValidateString(string Value, int Operator, string ValueToCompare)
        {
            bool IsValid = true;
            //Logical Operator
            //	1	Equal 
            //	2	NotEqual 
            //	3	GreaterThan 
            //	4	GreaterThanEqual 
            //	5	LessThan 
            //	6	LessThanEqual
            Value = Value.Trim().ToUpper();
            ValueToCompare = ValueToCompare.Trim().ToUpper();
            switch (Operator)
            {
                case 1:
                    {
                        IsValid = (Value == ValueToCompare);
                        break;
                    }
                case 2:
                    {
                        IsValid = (Value != ValueToCompare);
                        break;
                    }
                default:
                    {
                        IsValid = (Value == ValueToCompare);
                        break;
                    }
            }
            return IsValid;

        }

        private bool BizRuleValidateDate(DateTime Value, int Operator, DateTime ValueToCompare)
        {
            bool IsValid = true;
            //Logical Operator
            //	1	Equal 
            //	2	NotEqual 
            //	3	GreaterThan 
            //	4	GreaterThanEqual 
            //	5	LessThan 
            //	6	LessThanEqual

            switch (Operator)
            {
                case 1:
                    {
                        IsValid = (Value == ValueToCompare);
                        break;
                    }
                case 2:
                    {
                        IsValid = (Value != ValueToCompare);
                        break;
                    }
                case 3:
                    {
                        IsValid = (Value > ValueToCompare);
                        break;
                    }
                case 4:
                    {
                        IsValid = (Value >= ValueToCompare);
                        break;
                    }
                case 5:
                    {
                        IsValid = (Value < ValueToCompare);
                        break;
                    }
                case 6:
                    {
                        IsValid = (Value <= ValueToCompare);
                        break;
                    }
            }
            return IsValid;

        }

        private bool BizRuleValidateDecimal(Decimal Value, int Operator, Decimal ValueToCompare)
        {
            bool IsValid = true;
            //Logical Operator
            //	1	Equal 
            //	2	NotEqual 
            //	3	GreaterThan 
            //	4	GreaterThanEqual 
            //	5	LessThan 
            //	6	LessThanEqual

            switch (Operator)
            {
                case 1:
                    {
                        IsValid = (Value == ValueToCompare);
                        break;
                    }
                case 2:
                    {
                        IsValid = (Value != ValueToCompare);
                        break;
                    }
                case 3:
                    {
                        IsValid = (Value > ValueToCompare);
                        break;
                    }
                case 4:
                    {
                        IsValid = (Value >= ValueToCompare);
                        break;
                    }
                case 5:
                    {
                        IsValid = (Value < ValueToCompare);
                        break;
                    }
                case 6:
                    {
                        IsValid = (Value <= ValueToCompare);
                        break;
                    }
            }
            return IsValid;

        }

        private bool BizRuleValidateBoolean(Boolean Value, int Operator, Boolean ValueToCompare)
        {
            bool IsValid = true;
            //Logical Operator
            //	1	Equal 
            //	2	NotEqual 
            //	3	GreaterThan 
            //	4	GreaterThanEqual 
            //	5	LessThan 
            //	6	LessThanEqual

            switch (Operator)
            {
                case 1:
                    {
                        IsValid = (Value == ValueToCompare);
                        break;
                    }
                case 2:
                    {
                        IsValid = (Value != ValueToCompare);
                        break;
                    }
                default:
                    {
                        IsValid = (Value == ValueToCompare);
                        break;
                    }

            }
            return IsValid;

        }

        public bool EvaluateBizRule(AccountData dtsAccount, FormData dtsForm, int dataOperation)
        {
            bool IsValid = true;

            try
            {
                QSPForm.Business.BusinessFieldSystem bizFldSys = new QSPForm.Business.BusinessFieldSystem();
                bizFldSys.SetValidationData(dtsAccount, dataOperation);
                IsValid = EvaluateBizRule(dtsAccount.AccountValidation, dtsForm.BusinessRule);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValid;
        }

        public bool EvaluateBizRule(ProgramAgreementData dtsProgramAgreement, FormData dtsForm, int dataOperation)
        {
            bool IsValid = true;

            try
            {
                QSPForm.Business.BusinessFieldSystem bizFldSys = new QSPForm.Business.BusinessFieldSystem();
                bizFldSys.SetValidationData(dtsProgramAgreement, dataOperation);
                IsValid = EvaluateBizRule(dtsProgramAgreement.ProgramAgreementValidation, dtsForm.BusinessRule);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValid;
        }

        public bool EvaluateBizRule(CreditApplicationData dts, FormData dtsForm, int dataOperation)
        {
            bool IsValid = true;

            try
            {
                QSPForm.Business.BusinessFieldSystem bizFldSys = new QSPForm.Business.BusinessFieldSystem();
                bizFldSys.SetValidationData(dts, dataOperation);
                IsValid = EvaluateBizRule(dts.CreditValidation, dtsForm.BusinessRule);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValid;
        }

        public bool EvaluateBizRule(OrderData dtsOrder, AccountData dtsAccount, FormData dtsForm, int dataOperation, Data.ConnectionProvider connProvider)
        {
            bool IsValid = true;

            try
            {
                QSPForm.Business.BusinessFieldSystem bizFldSys = new QSPForm.Business.BusinessFieldSystem();
                bizFldSys.SetValidationData(dtsOrder, dtsAccount, dtsForm, dataOperation, connProvider);

                IsValid = EvaluateBizRule(dtsOrder.OrderValidation, dtsForm.BusinessRule);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValid;
        }

        private bool EvaluateBizRule(ValidationTable dTblVal, BusinessRuleTable dtblBizRule)
        {
            bool IsValid = true;

            try
            {
                foreach (DataRow valRow in dTblVal.Rows)
                {
                    int FormSectionTypeID = 0;
                    int FormSectionNumber = 0;
                    if (!valRow.IsNull(ValidationTable.FLD_FORM_SECTION_TYPE_ID))
                        FormSectionTypeID = Convert.ToInt32(valRow[ValidationTable.FLD_FORM_SECTION_TYPE_ID]);
                    if (!valRow.IsNull(ValidationTable.FLD_FORM_SECTION_NUMBER))
                        FormSectionNumber = Convert.ToInt32(valRow[ValidationTable.FLD_FORM_SECTION_NUMBER]);

                    DataView dv = new DataView(dtblBizRule);
                    string sFilter = "";
                    sFilter = "(ISNULL(" + BusinessRuleTable.FLD_FORM_SECTION_TYPE_ID + ",0) = " + FormSectionTypeID.ToString();
                    sFilter += " AND ISNULL(" + BusinessRuleTable.FLD_FORM_SECTION_NUMBER + ",0) = " + FormSectionNumber.ToString();
                    sFilter += ") OR (ISNULL(" + BusinessRuleTable.FLD_FORM_SECTION_TYPE_ID + ",0) = 0)";
                    dv.RowFilter = sFilter;

                    if (dv.Count > 0)
                    {
                        //Rule
                        foreach (DataRowView vRow in dv)
                        {
                            DataRow row = vRow.Row;
                            string labelRule = row[BusinessRuleTable.FLD_NAME].ToString();
                            string colName = row[BusinessRuleTable.FLD_FIELD_NAME].ToString();
                            string msg = row[BusinessRuleTable.FLD_MESSAGE].ToString();
                            int fieldType = Convert.ToInt32(row[BusinessRuleTable.FLD_FIELD_TYPE_ID]);
                            string sValueToCompare = row[BusinessRuleTable.FLD_VALUE_TO_COMPARE].ToString();
                            int Operator = Convert.ToInt32(row[BusinessRuleTable.FLD_LOGICAL_OPERATOR_ID]);
                            bool isFormProperty = Convert.ToBoolean(row[BusinessRuleTable.FLD_IS_FORM_PROPERTY]);

                            if (dTblVal.Columns.IndexOf(colName) == -1)
                            {
                                AddColumnValidation(valRow, colName, sValueToCompare, fieldType);
                            }
                            else
                            {

                                IsValid = true;
                            }
                            if (isFormProperty)
                            {
                                SetColumnFormProperty(valRow, colName, sValueToCompare, fieldType);
                            }
                            else
                            {
                                string sValue = valRow[colName].ToString();

                                IsValid = BizRuleValidate(sValue, Operator, sValueToCompare, fieldType);
                                row[BusinessRuleTable.FLD_IS_VALID] = IsValid;
                            }

                        }

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsValid;
        }

        public bool EvaluateBooleanExpression(DataSet dts, FormData dtsForm, ref string sExpression, int FormSectionTypeID, int FormSectionNumber)
        {
            bool bValue = false;

            try
            {
                ValidationTable dTblVal = (ValidationTable)dts.Tables[ValidationTable.TBL_VALIDATION];
                DataRow valRow = dTblVal.Rows[0];
                if (dts is OrderData)
                {
                    valRow = dTblVal.GetValidationRow(FormSectionTypeID, FormSectionNumber);
                }

                dataDef dtblBizRule = dtsForm.BusinessRule;

                //Do a check to see if the Supply has to be evaluated
                //We do a bypass when there is no supply items
                if (FormSectionTypeID >= QSPForm.Common.FormSectionType.SUPPLY_PRODUCT)
                {
                    int totalsupply = 0;
                    if (!valRow.IsNull(ValidationTable.FLD_TOTAL_QUANTITY))
                    {
                        totalsupply = Convert.ToInt32(valRow[ValidationTable.FLD_TOTAL_QUANTITY]);
                    }
                    if (totalsupply == 0)
                    {
                        return false;
                    }
                }

                DataView dv = new DataView(dtblBizRule);
                dv.RowFilter = "ISNULL(" + BusinessRuleTable.FLD_IS_FORM_PROPERTY + ",0) = 0";
                foreach (DataRowView vrowRule in dv)
                {
                    DataRow rowRule = vrowRule.Row;
                    string labelRule = rowRule[BusinessRuleTable.FLD_NAME].ToString();
                    string field_name = rowRule[BusinessRuleTable.FLD_FIELD_NAME].ToString();
                    int fieldType = Convert.ToInt32(rowRule[BusinessRuleTable.FLD_FIELD_TYPE_ID]);
                    int OperatorID = Convert.ToInt32(rowRule[BusinessRuleTable.FLD_LOGICAL_OPERATOR_ID]);
                    string sOperator = GetOperator(OperatorID);
                    string sValueToCompare = rowRule[BusinessRuleTable.FLD_VALUE_TO_COMPARE].ToString();
                    //Handle the field type like when the data is a string we have to put ' ...
                    string sLitteralRule = FormatBizRuleExpression(field_name, sOperator, sValueToCompare, fieldType);//"(" + field_name + " " + sOperator + " " + sValueToCompare + ")";
                    sExpression = sExpression.Replace(labelRule, sLitteralRule);
                }
                //Set the expression to evaluate if it's valid
                DataColumn col = dTblVal.Columns[ValidationTable.FLD_IS_VALID];
                if (sExpression.Length > 0)
                {
                    col.Expression = "NOT (" + sExpression + ")";
                    bValue = !Convert.ToBoolean(valRow[ValidationTable.FLD_IS_VALID]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bValue;
        }

        private string FormatBizRuleExpression(string sFieldName, string sOperator, string sValue, int FieldType)
        {
            string formatExpression = "";

            switch (FieldType)
            {
                case 1: // String
                    {
                        string val = sValue.Trim();
                        //Check if it's already enclosed between apostrophe
                        sValue = sValue.Replace("'", "");
                        //Remove the apostrophe from string and Add them after
                        sValue = "'" + sValue;
                        sValue = sValue + "'";
                        formatExpression = "(" + sFieldName + " " + sOperator + " " + sValue + ")";
                        break;
                    }
                case 2: //Integer
                    {
                        int val = Convert.ToInt32(sValue);
                        formatExpression = "(" + sFieldName + " " + sOperator + " " + val.ToString() + ")";
                        break;
                    }
                case 3: //Double
                    {
                        decimal val = Convert.ToDecimal(sValue);
                        formatExpression = "(" + sFieldName + " " + sOperator + " " + val.ToString() + ")";
                        break;
                    }
                case 4: //Date
                    {
                        sValue = sValue.Replace("#", "");
                        //Remove the pawn sign from string and Add them after
                        DateTime val = DateTime.Today;
                        if (sValue.ToUpper() != "TODAY")
                        {
                            val = Convert.ToDateTime(sValue);
                        }
                        sValue = "#" + val.ToShortDateString() + "#";
                        formatExpression = "(" + sFieldName + " " + sOperator + " " + sValue + ")";
                        break;
                    }
                case 5: //Currency
                    {
                        decimal val = Convert.ToDecimal(sValue);
                        formatExpression = "(" + sFieldName + " " + sOperator + " " + val.ToString() + ")";
                        break;
                    }
                case 6: //Boolean
                    {
                        bool val = Convert.ToBoolean(sValue);
                        formatExpression = "(" + sFieldName + " " + sOperator + " " + val.ToString() + ")";
                        break;
                    }
            }
            return formatExpression;
        }

        //Use to calculate the Fees amount
        public decimal EvaluateDecimalExpression(DataSet dts, FormData dtsForm, ref string sBizFeesExpression, int FormSectionTypeID, int FormSectionNumber)
        {
            decimal Value = 0;

            try
            {
                ValidationTable dTblVal = (ValidationTable)dts.Tables[ValidationTable.TBL_VALIDATION];
                DataRow valRow = dTblVal.Rows[0];
                if (dts is OrderData)
                {
                    valRow = dTblVal.GetValidationRow(FormSectionTypeID, FormSectionNumber);
                }

                dataDef dtblBizRule = dtsForm.BusinessRule;

                bool IsValidRule = false;

                foreach (DataRow rowRule in dtblBizRule.Rows)
                {
                    string labelRule = rowRule[BusinessRuleTable.FLD_NAME].ToString();
                    string field_name = rowRule[BusinessRuleTable.FLD_FIELD_NAME].ToString();
                    int OperatorID = Convert.ToInt32(rowRule[BusinessRuleTable.FLD_LOGICAL_OPERATOR_ID]);
                    string sOperator = GetOperator(OperatorID);
                    string sValueToCompare = rowRule[BusinessRuleTable.FLD_VALUE_TO_COMPARE].ToString();
                    IsValidRule = Convert.ToBoolean(rowRule[BusinessRuleTable.FLD_IS_VALID]);
                    //TODO Handle the field type like when the data is a string we have to put ' ...
                    string sLitteralRule = "(" + field_name + " " + sOperator + " " + sValueToCompare + ")";
                    sBizFeesExpression = sBizFeesExpression.Replace(labelRule, sLitteralRule);
                }
                //Set the expression to evaluate if it's valid
                DataColumn col;
                if (sBizFeesExpression.Length > 0)
                {
                    col = dTblVal.Columns[ValidationTable.FLD_FEES_VALUE_AMOUNT];
                    col.Expression = sBizFeesExpression;
                    //Copy the result of the Expression of the Fees Amount
                    Value = Convert.ToDecimal(valRow[ValidationTable.FLD_FEES_VALUE_AMOUNT]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Value;
        }



        private string GetOperator(int Operator)
        {
            //Logical Operator
            //	1	Equal 
            //	2	NotEqual 
            //	3	GreaterThan 
            //	4	GreaterThanEqual 
            //	5	LessThan 
            //	6	LessThanEqual
            string sOperator = "";

            switch (Operator)
            {
                case 1:
                    {
                        sOperator = "=";
                        break;
                    }
                case 2:
                    {
                        sOperator = "<>";
                        break;
                    }
                case 3:
                    {
                        sOperator = ">";
                        break;
                    }
                case 4:
                    {
                        sOperator = ">=";
                        break;
                    }
                case 5:
                    {
                        sOperator = "<";
                        break;
                    }
                case 6:
                    {
                        sOperator = "<=";
                        break;
                    }
            }
            return sOperator;

        }



    }
}
