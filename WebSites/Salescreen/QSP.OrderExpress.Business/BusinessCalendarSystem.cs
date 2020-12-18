namespace QSPForm.Business
{

    using System;
    using System.Data;
    using QSPForm.Common.DataDef;
    using QSPForm.Data;
    using dataDef = QSPForm.Common.DataDef.BusinessCalendarTable;
    using dataAccessRef = QSPForm.Data.Business_calendar;
    using QSPForm.Common;

    /// <summary>
    ///     This class contains the business rules that are used for a 
    ///     campaign.
    /// </summary>
    public class BusinessCalendarSystem : BusinessSystem
    {
        dataAccessRef objDataAccess;

        public BusinessCalendarSystem()
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
            IsValid &= IsValid_RequiredField(row, dataDef.FLD_PKID, "Business Date");

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

        public dataDef SelectOne(DateTime BizDate)
        {
            //
            // Get the user DataTable from the DataLayer
            //	
            return objDataAccess.SelectOne(BizDate);
        }

        public dataDef SelectAll_Search(DateTime StartDate, DateTime EndDate, DateTime OrderDate)
        {
            dataDef dTbl;

            //
            // Get the user DataTable from the DataLayer
            //				
            dTbl = objDataAccess.SelectAll_Search(StartDate, EndDate, OrderDate);

            return dTbl;
        }

        public dataDef SelectAll_Search(DateTime StartDate, DateTime EndDate, DateTime OrderDate, int WarehouseID)
        {
            dataDef dTbl;

            //
            // Get the user DataTable from the DataLayer
            //				
            dTbl = objDataAccess.SelectAll_Search(StartDate, EndDate, OrderDate, WarehouseID);
                                     

            return dTbl;
        }

        /// <summary>
        /// This method is for Spring Shutdown
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="OrderDate"></param>
        /// <param name="shutdownStartDate"></param>
        /// <param name="shutdownEndDate"></param>
        /// <param name="warehouseID"></param>
        /// <param name="formID"></param>
        /// <returns></returns>
        public dataDef SelectAll_Search(DateTime StartDate, DateTime EndDate, DateTime OrderDate, DateTime shutdownStartDate, DateTime shutdownEndDate, int warehouseID, int formID)
        {
            dataDef dTbl;

            //
            // Get the user DataTable from the DataLayer
            //				
            dTbl = objDataAccess.SelectAll_Search(StartDate, EndDate, OrderDate, shutdownStartDate, shutdownEndDate, warehouseID, formID);

            return dTbl;
        }

        public dataDef SelectAll_Search(DateTime StartDate, DateTime EndDate, DateTime OrderDate, string[] shutdownStartDate, string[] shutdownEndDate, int warehouseID, int formID, int ShutDownFormID)
        {
            dataDef dTbl;

            //
            // Get the user DataTable from the DataLayer
            //				
            dTbl = objDataAccess.SelectAll_Search(StartDate, EndDate, OrderDate, warehouseID);

            DataView dv = new DataView(dTbl);

            if (formID == ShutDownFormID)
            {

                dv.Sort = dataDef.FLD_NB_DAY_LEAD_TIME;

                foreach (DataRow detailRow in dTbl.Rows)
                {
                    for (int i = 0; i < shutdownEndDate.Length; i++)
                    {
                        if (Convert.ToDateTime(detailRow[BusinessCalendarTable.FLD_PKID].ToString()) >= Convert.ToDateTime(shutdownStartDate[i]) && Convert.ToDateTime(detailRow[BusinessCalendarTable.FLD_PKID].ToString()) <= Convert.ToDateTime(shutdownEndDate[i]))
                        {
                            if (detailRow[BusinessCalendarTable.FLD_NB_DAY_LEAD_TIME].ToString() != "-1")
                            {
                                detailRow[BusinessCalendarTable.FLD_NB_DAY_LEAD_TIME] = -1;
                            }
                        }
                    }
                }
            }

            dv.Sort = dataDef.FLD_PKID;
            int counter = 0;
            if (OrderDate < StartDate)
            {
                counter = GetNbDayLeadTime(OrderDate, StartDate);
            }
            foreach (DataRow detailRow in dTbl.Rows)
            {
                if (detailRow[BusinessCalendarTable.FLD_NB_DAY_LEAD_TIME].ToString() != "-1")
                {
                    detailRow[BusinessCalendarTable.FLD_NB_DAY_LEAD_TIME] = counter;
                    counter++;
                }
            }

            dTbl = (dataDef)dv.Table;


            return dTbl;
        }


        public void InsertCountOfBusinessday(dataDef dTbl)
        {
            //			DataView dvBizDates = new DataView(dTbl);
            //			int iCount = 0;
            //			for (int iCount = 0 ; iCount < dvBizDates.Count; iCount++)
            //			{
            //			
            //			
            //			}


        }

        public DateTime GetNextBusinessDay(DateTime date, int duration)
        {
            //Get the FY of the Date given as param
            return objDataAccess.GetNextBusinessDay(date, duration);
        }

        public int GetNbDayLeadTime(DateTime StartDate, DateTime EndDate)
        {
            //Get the FY of the Date given as param
            return objDataAccess.GetNbDayLeadTime(StartDate, EndDate);
        }

        public int GetNbDayLeadTime(DateTime StartDate, DateTime EndDate,string[] shutdownStartDate, string[] shutdownEndDate, int formId, int shutdownFormId)
        {
            int counter = 0;
            int shutdownCounter = 0;

            counter = objDataAccess.GetNbDayLeadTime(StartDate, EndDate);
            if (formId == shutdownFormId)
            {
                 for (int i = 0; i < shutdownEndDate.Length; i++)
                 {
                     if(StartDate < Convert.ToDateTime(shutdownStartDate[i]) && EndDate > Convert.ToDateTime(shutdownEndDate[i]))
                         shutdownCounter += objDataAccess.GetNbDayLeadTime(Convert.ToDateTime(shutdownStartDate[i]),Convert.ToDateTime(shutdownEndDate[i]));
                 }
            }

            counter = counter - shutdownCounter;
            return counter;
        }

        public int GetFiscalYear()
        {
            //Get the current FY
            DateTime date = DateTime.Today;
            return objDataAccess.GetFiscalYear(date);
        }

        public DateTime GetFirstDateOfFiscalYear()
        {
            //Get the current FY
            int FiscalYear = GetFiscalYear();
            return GetFirstDateOfFiscalYear(FiscalYear);
        }

        public DateTime GetLastDateOfFiscalYear()
        {
            //Get the current FY
            int FiscalYear = GetFiscalYear();
            return GetLastDateOfFiscalYear(FiscalYear);
        }

        public DateTime GetFirstDateOfFiscalYear(int FiscalYear)
        {
            //Get the current FY
            DateTime FirstDateOfFY = new DateTime((FiscalYear - 1), 7, 1);
            return FirstDateOfFY;
        }

        public DateTime GetLastDateOfFiscalYear(int FiscalYear)
        {
            //Get the current FY
            DateTime LastDateOfFY = new DateTime((FiscalYear), 6, 30);
            return LastDateOfFY;
        }

        public int GetFiscalYear(DateTime date)
        {
            //Get the FY of the Date given as param
            return objDataAccess.GetFiscalYear(date);
        }

    }
}
