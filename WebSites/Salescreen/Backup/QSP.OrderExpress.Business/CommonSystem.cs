using System;
using System.Data;
using System.IO;
using QSPForm.Common.DataDef;
using System.Text.RegularExpressions;

namespace QSPForm.Business
{
	/// <summary>
	/// Summary description for CommonSystem.
	/// </summary>
	public class CommonSystem  : BusinessSystem
	{
		QSPForm.Data.Common comDataAccess;
		public CommonSystem()
		{
			comDataAccess = new QSPForm.Data.Common();
		}


		public DataTable SelectAllUSState()
		{
			//
			// Get the DataTable from the DataLayer
			//
										
			return  comDataAccess.SelectAllUSState();
			
		}

		public bool SendEmailNotification(string strFrom, string strTo, string strSujet, string strBody)
		{
			return comDataAccess.SendEmailNotification(strFrom, strTo, strSujet, strBody);
		}


		public decimal GetTaxRate(DataSet dts, int OrgType, int FormID, int EntityTypeID)
		{
			//Get the Tax Rate
			decimal taxRate = 0;
			TaxInfoTable dTblTaxInfo = GetTaxInfoTable(dts, OrgType, FormID, EntityTypeID);

			//We need to summarize the tax rate cause it's split in different level (county, city, State)
//			if (!dTblTaxInfo.IsTaxExempted)
//			{
				foreach (DataRow row in dTblTaxInfo.Rows)
				{
					if (!row.IsNull(TaxInfoTable.FLD_RATE))
						taxRate = taxRate + Convert.ToDecimal(row[TaxInfoTable.FLD_RATE]);
				}
//			}	
			return taxRate;

		}

        public decimal GetTaxRate(DataSet dts, int FormID, int EntityTypeID)
        {
            //Get the Tax Rate
            decimal taxRate = 0;
            TAXFLETable dTblTAXFLEInfo = GetTAXFLEInfo(dts, FormID, EntityTypeID);

            //We need to summarize the tax rate cause it's split in different level (county, city, State)
            //			if (!dTblTaxInfo.IsTaxExempted)
            //			{
            if (dTblTAXFLEInfo.Rows.Count > 0)
            {
                DataRow row = dTblTAXFLEInfo.Rows[0];
                if (!row.IsNull(TAXFLETable.FLD_FDSTTX)) //State Rate 
                    taxRate = taxRate + Convert.ToDecimal(row[TAXFLETable.FLD_FDSTTX]);
                if (!row.IsNull(TAXFLETable.FLD_FDCOTX)) //County Rate 
                    taxRate = taxRate + Convert.ToDecimal(row[TAXFLETable.FLD_FDCOTX]);
                if (!row.IsNull(TAXFLETable.FLD_FDCITX)) //City Rate 
                    taxRate = taxRate + Convert.ToDecimal(row[TAXFLETable.FLD_FDCITX]);
            }
            //			}	
            return taxRate;

        }

		
		public TaxInfoTable GetTaxInfoTable(DataSet dts, int OrgType, int FormID, int EntityTypeID)
		{
			string ZipCode = "";
			string CityName = "";
			string CountyName = "";
			string SubdivisionCode = "";		
			int postAddType = Common.PostalAddressType.TYPE_BILLING;
			int programType = 0;
			int ProductTypeID = 0;
            int entityTypeID = EntityTypeID;
            
			//Form Info -- For the Postal Address Type to used
			FormSystem formSys = new FormSystem();
			FormTable dTblForm = formSys.SelectOne(FormID);
			if (dTblForm.Rows.Count >0)
			{
				postAddType = Convert.ToInt32(dTblForm.Rows[0][FormTable.FLD_TAX_POSTAL_ADDRESS_TYPE_ID]);
                if ((EntityTypeID == Common.EntityType.TYPE_ORDER_BILLING) && (postAddType == Common.PostalAddressType.TYPE_SHIPPING))
			    {
                    //We have to re calculate the Entity type when it,s order
                    entityTypeID = Common.EntityType.TYPE_ORDER_SHIPPING;
                }
				programType = Convert.ToInt32(dTblForm.Rows[0][FormTable.FLD_PROGRAM_TYPE_ID]);
			}
			DataTable dTblProductType;
			QSPForm.Data.Common comDataAccess = new QSPForm.Data.Common();
			dTblProductType = comDataAccess.SelectAllProductTypeByProgramType(programType);
			if (dTblProductType.Rows.Count > 0)
			{
				ProductTypeID = Convert.ToInt32(dTblProductType.Rows[0][0]);
			}

			OrganizationTable dtOrganization = (OrganizationTable)dts.Tables[OrganizationTable.TBL_ORGANIZATION];
			PostalAddressEntityTable dtAddress = (PostalAddressEntityTable)dts.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY];
			
			DataView DVAddress = new DataView(dtAddress);			
			DVAddress.RowFilter = PostalAddressEntityTable.FLD_TYPE + " = " + postAddType.ToString() + " AND " +
                PostalAddressEntityTable.FLD_ENTITY_TYPE_ID + " = " + entityTypeID.ToString();
			if (DVAddress.Count > 0)
			{
				DataRow row = DVAddress[0].Row;
				//'Table Mapping                      			
					
				CityName = row[PostalAddressEntityTable.FLD_CITY].ToString();
				CountyName = row[PostalAddressEntityTable.FLD_COUNTY].ToString();
				SubdivisionCode = row[PostalAddressEntityTable.FLD_SUBDIVISION_CODE].ToString();
                ZipCode = row[PostalAddressEntityTable.FLD_ZIP].ToString();
                if (ZipCode.Length >= 5)
                    ZipCode = ZipCode.Substring(0, 5);	
                else
                    ZipCode = ZipCode.PadLeft(5, '0');
				
			}			
			return GetTaxInfoTable(ProductTypeID, OrgType, SubdivisionCode, CityName, CountyName, ZipCode);
			
		}

        public bool IsTaxExempted(DataSet dts, int OrgType, int FormID, int EntityTypeID)
        {
            string ZipCode = "";
            string CityName = "";
            string CountyName = "";
            string SubdivisionCode = "";
            int postAddType = Common.PostalAddressType.TYPE_BILLING;
            int programType = 0;
            int ProductTypeID = 0;
            int entityTypeID = EntityTypeID;
            bool isTaxExempted = false;

            //Form Info -- For the Postal Address Type to used
            FormSystem formSys = new FormSystem();
            FormTable dTblForm = formSys.SelectOne(FormID);
            if (dTblForm.Rows.Count > 0)
            {
                postAddType = Convert.ToInt32(dTblForm.Rows[0][FormTable.FLD_TAX_POSTAL_ADDRESS_TYPE_ID]);
                if ((EntityTypeID == Common.EntityType.TYPE_ORDER_BILLING) && (postAddType == Common.PostalAddressType.TYPE_SHIPPING))
                {
                    //We have to re calculate the Entity type when it,s order
                    entityTypeID = Common.EntityType.TYPE_ORDER_SHIPPING;
                }
                programType = Convert.ToInt32(dTblForm.Rows[0][FormTable.FLD_PROGRAM_TYPE_ID]);
            }
            DataTable dTblProductType;
            QSPForm.Data.Common comDataAccess = new QSPForm.Data.Common();
            dTblProductType = comDataAccess.SelectAllProductTypeByProgramType(programType);
            if (dTblProductType.Rows.Count > 0)
            {
                ProductTypeID = Convert.ToInt32(dTblProductType.Rows[0][0]);
            }

            OrganizationTable dtOrganization = (OrganizationTable)dts.Tables[OrganizationTable.TBL_ORGANIZATION];
            PostalAddressEntityTable dtAddress = (PostalAddressEntityTable)dts.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY];

            DataView DVAddress = new DataView(dtAddress);
            DVAddress.RowFilter = PostalAddressEntityTable.FLD_TYPE + " = " + postAddType.ToString() + " AND " +
                PostalAddressEntityTable.FLD_ENTITY_TYPE_ID + " = " + entityTypeID.ToString();
            if (DVAddress.Count > 0)
            {
                DataRow row = DVAddress[0].Row;
                //'Table Mapping                      			

                CityName = row[PostalAddressEntityTable.FLD_CITY].ToString();
                CountyName = row[PostalAddressEntityTable.FLD_COUNTY].ToString();
                SubdivisionCode = row[PostalAddressEntityTable.FLD_SUBDIVISION_CODE].ToString();
                ZipCode = row[PostalAddressEntityTable.FLD_ZIP].ToString();
                if (ZipCode.Length >= 5)
                    ZipCode = ZipCode.Substring(0, 5);
                else
                    ZipCode = ZipCode.PadLeft(5, '0');

            }

            // Ben - 05/07/2007
            // GA Issue - Only state in that situation.
            if (SubdivisionCode != "US-GA")
            {
                QSPForm.Data.Tax_calculation_method datTax = new QSPForm.Data.Tax_calculation_method();
                Common.DataDef.TaxCalculationMethodTable dTblTaxCalcMeth = new TaxCalculationMethodTable();
                dTblTaxCalcMeth = datTax.SelectAll_Search(SubdivisionCode, ProductTypeID, OrgType);
                if (dTblTaxCalcMeth.Rows.Count > 0)
                {
                    DataRow row = dTblTaxCalcMeth.Rows[0];
                    if (!row.IsNull(TaxCalculationMethodTable.FLD_TAX_EXEMPTABLE))
                        isTaxExempted = Convert.ToBoolean(row[TaxCalculationMethodTable.FLD_TAX_EXEMPTABLE]);
                }
            }

            return isTaxExempted;

        }

        public TAXFLETable GetTAXFLEInfo(DataSet dts, int FormID, int EntityTypeID)
        {
            string CityName = "";
            string CountyName = "";
            string SubdivisionCode = "";
            int postAddType = Common.PostalAddressType.TYPE_BILLING;
            int entityTypeID = EntityTypeID;

            //Form Info -- For the Postal Address Type to used
            FormSystem formSys = new FormSystem();
            FormTable dTblForm = formSys.SelectOne(FormID);
            if (dTblForm.Rows.Count > 0)
            {
                postAddType = Convert.ToInt32(dTblForm.Rows[0][FormTable.FLD_TAX_POSTAL_ADDRESS_TYPE_ID]);
                if ((EntityTypeID == Common.EntityType.TYPE_ORDER_BILLING) && (postAddType == Common.PostalAddressType.TYPE_SHIPPING))
                {
                    //We have to re calculate the Entity type when it,s order
                    entityTypeID = Common.EntityType.TYPE_ORDER_SHIPPING;
                }
            }           

            PostalAddressEntityTable dtAddress = (PostalAddressEntityTable)dts.Tables[PostalAddressEntityTable.TBL_POSTAL_ADDRESS_ENTITY];

            DataView DVAddress = new DataView(dtAddress);
            DVAddress.RowFilter = PostalAddressEntityTable.FLD_TYPE + " = " + postAddType.ToString() + " AND " +
                PostalAddressEntityTable.FLD_ENTITY_TYPE_ID + " = " + entityTypeID.ToString();
            if (DVAddress.Count > 0)
            {
                DataRow row = DVAddress[0].Row;
                //'Table Mapping                      			

                CityName = row[PostalAddressEntityTable.FLD_CITY].ToString().Trim();
                CountyName = row[PostalAddressEntityTable.FLD_COUNTY].ToString().Trim();
                SubdivisionCode = row[PostalAddressEntityTable.FLD_SUBDIVISION_CODE].ToString().Trim();               
            }
            string StateCode = SubdivisionCode.Replace("US-", "");
            
            if (CountyName.Trim().Length >0)
                return GetTAXFLEInfo(StateCode, CityName, CountyName);
            else
                return GetTAXFLEInfo(StateCode, CityName);
        }

		public TaxInfoTable GetTaxInfoTable(int ProductTypeID, int OrganizationTypeID, String SubdivisionCode, String CityName, String CountyName, String ZipCode)
		{
			//Get the Tax Info Table
			QSPForm.Data.Common comDataAccess = new QSPForm.Data.Common();
			return comDataAccess.GetTaxInfoTable(ProductTypeID, OrganizationTypeID, SubdivisionCode, CityName, CountyName, ZipCode);

		}

        public TAXFLETable GetTAXFLEInfo(String StateCode, String CityName, String CountyName)
        {
            //Get the Tax Info Table
            QSPForm.DataRepository.TAXFLE taxDataAccess = new QSPForm.DataRepository.TAXFLE();
            return taxDataAccess.SelectByAddress(StateCode, CityName, CountyName);

        }
        public TAXFLETable GetTAXFLEInfo(String StateCode, String CityName)
        {
            //Get the Tax Info Table
            QSPForm.DataRepository.TAXFLE taxDataAccess = new QSPForm.DataRepository.TAXFLE();
            return taxDataAccess.SelectByAddress(StateCode, CityName, "");

        }

        public TAXFLETable GetTAXFLEInfo(int FulfAccountID)
        {
            //Get the Tax Info Table
            QSPForm.DataRepository.TAXFLE taxDataAccess = new QSPForm.DataRepository.TAXFLE();
            return taxDataAccess.SelectByARCUST(FulfAccountID);

        }

		public decimal GetTaxRate(int Zip, String CityName, String StateCode)
		{
			QSPForm.Data.Common comDataAccess = new QSPForm.Data.Common();
			return comDataAccess.GetTaxRate(Zip, CityName, StateCode);
		}

		public DataTable SelectAllFMID()
		{
			//
			// Get the DataTable from the DataLayer
			//
				
			return  comDataAccess.SelectAllFMID();
			
		}

		

		

		public DataTable SelectAllOrganizationType()
		{
			//
			// Get the DataTable from the DataLayer
			//
				
			return  comDataAccess.SelectAllOrganizationType();			
			
		}

		public DataTable SelectAllOrganizationLevel()
		{
			//
			// Get the DataTable from the DataLayer
			//
				
			return  comDataAccess.SelectAllOrganizationLevel();			
			
		}

		public DataTable SelectAllTradeClass()
		{
			//
			// Get the DataTable from the DataLayer
			//
				
			return  comDataAccess.SelectAllTradeClass();			
			
		}


		public DataTable SelectAllAccountType()
		{
			//
			// Get the DataTable from the DataLayer
			//
				
			return  comDataAccess.SelectAllAccountType();			
			
		}

		public DataTable SelectAllCampaignType()
		{
			//
			// Get the DataTable from the DataLayer
			//
				
			return  comDataAccess.SelectAllCampaignType();			
			
		}

		public DataTable SelectAllPostalAddressType()
		{
			//
			// Get the DataTable from the DataLayer
			//
			return  comDataAccess.SelectAllPostalAddressType();			
			
		}

		public DataTable SelectAllPhoneNumberType()
		{
			//
			// Get the DataTable from the DataLayer
			//
			return  comDataAccess.SelectAllPhoneNumberType();			
			
		}

		public DataTable SelectAllEmailType()
		{
			//
			// Get the DataTable from the DataLayer
			//
			return  comDataAccess.SelectAllEmailType();			
			
		}

		public DataTable SelectAllProgramType()
		{
			//
			// Get the DataTable from the DataLayer
			//
			return  comDataAccess.SelectAllProgramType();			
			
		}

        public DataTable SelectAllProgramTypeByEntityTypeID(int EntityTypeID)
		{
			//
			// Get the DataTable from the DataLayer
			//
            return comDataAccess.SelectAllProgramTypeByEntityTypeID(EntityTypeID);			
			
		}

        

        public DataTable SelectAllPaymentAssignmentType()
        {
            //
            // Get the DataTable from the DataLayer
            //
            return comDataAccess.SelectAllPaymentAssignmentType();

        }

        public DataTable SelectAllProgramAgreementCatalogs(int formId, DateTime programAgreementDate)
        {
            return comDataAccess.SelectAllProgramAgreementCatalogs(formId, programAgreementDate);
        }

		public DataTable SelectAllSource()
		{
			//
			// Get the DataTable from the DataLayer
			//
			return comDataAccess.SelectAllSource();			
			
		}

		public DataTable SelectAllAccountStatus()
		{	
			return comDataAccess.SelectAllAccountStatus();	
		}

		public DataTable SelectAllOrderStatus()
		{	
			return comDataAccess.SelectAllOrderStatus();	
		}

		public DataTable SelectOneOrderStatus(int OrderStatusID)
		{	
			return comDataAccess.SelectOneOrderStatus(OrderStatusID);	
		}

		public string GetOrderStatusName(int OrderStatusID)
		{	
			string toReturn = "";
			DataTable dTbl = comDataAccess.SelectOneOrderStatus(OrderStatusID);	
			if (dTbl.Rows.Count >0)
			{
				toReturn = dTbl.Rows[0][1].ToString();
			}

			return toReturn;
		}

		public DataTable SelectAllOrderType()
		{	
			return comDataAccess.SelectAllOrderType();	
		}

		public DataTable SelectAllStatusCategory()
		{	
			return comDataAccess.SelectAllStatusCategory();	
		}

		public DataTable SelectAllStatusCategory(int EntityTypeID)
		{	
			return comDataAccess.SelectAllStatusCategory(EntityTypeID);	
		}		
		
		public DataTable SelectAllStatusReason()
		{			
			return comDataAccess.SelectAllStatusReason();	

		}
		
		public DataTable SelectAllBusinessFieldType()
		{			
			return comDataAccess.SelectAllBusinessFieldType();	

		}
		public DataTable SelectAllLogicalOperator()
		{			
			return comDataAccess.SelectAllLogicalOperator();	

		}
        public DataTable SelectAllFormSectionType()
        {
            return comDataAccess.SelectAllFormSectionType();

        }
		public DataTable SelectAllExceptionType()
		{			
			return comDataAccess.SelectAllExceptionType();	

		}
		public DataTable SelectAllProductType()
		{			
			return comDataAccess.SelectAllProductType();	

		}
		public DataTable SelectAllEntityType()
		{			
			return comDataAccess.SelectAllEntityType();	
		}

		public DataTable SelectAllTaskType()
		{			
			return comDataAccess.SelectAllTaskType();	
		}

        public DataTable SelectAllDeliveryMethod()
        {
            return comDataAccess.SelectAllDeliveryMethod();
        }

        public DataTable SelectAllProfitRate()
        {
            return comDataAccess.SelectAllProfitRate();
        }

		public DataTable SelectAllDocumentType()
		{			
			return comDataAccess.SelectAllDocumentType();	
		}
		
		public DataTable SelectAllBusinessDivision()
		{
			return comDataAccess.SelectAllBusinessDivision();
		}

		public DataTable SelectAllBusinessNotificationType()
		{
			return comDataAccess.SelectAllBusinessNotificationType();
		}

		
		public DataTable SelectAllTags(string procName,string paramName)
		{
			return comDataAccess.SelectAllTags(procName,paramName);
		}

		public DataTable SelectAllRegion()
		{
			return comDataAccess.SelectAllRegion();
		}

		public DataTable SelectAllImageCategory()
		{
			return comDataAccess.SelectAllImageCategory();
		}
	

		public bool UpdateRow(DataRow row, string colFieldName, string sValue)
		{
			bool IsChanged = false;

			sValue = sValue.Trim();
			
			if (row.Table.Columns[colFieldName].DataType == System.Type.GetType("System.String"))
			{
				string OldValue = "";
				if (row[colFieldName] != DBNull.Value)
				{
					string NewValue = sValue;
					OldValue = row[colFieldName].ToString().Trim();
					if (OldValue != NewValue)
					{
						row[colFieldName] = NewValue;
						IsChanged = true;
					}
					
				}		
				else
				{
					//When the value is already Null
					if (sValue.Length > 0)
					{
						string NewValue = sValue;
						row[colFieldName] = NewValue;
						IsChanged = true;
					}
				}
			}
			else if (row.Table.Columns[colFieldName].DataType == System.Type.GetType("System.Int32"))
			{
				int OldValue = 0;
					
				//entity (Account, Order, Organization, etc...)
				if (row[colFieldName] != DBNull.Value)
				{
					if (sValue.Length > 0)
					{
						int NewValue = Convert.ToInt32(sValue);
						OldValue = Convert.ToInt32(row[colFieldName]);
						if (OldValue != NewValue)
						{
							row[colFieldName] = NewValue;
							IsChanged = true;
						}
					}
					else
					{
						row[colFieldName] = DBNull.Value;
						IsChanged = true;
					}
				}		
				else
				{
					//When the value is already Null
					if (sValue.Length > 0)
					{
						int NewValue = Convert.ToInt32(sValue);
						row[colFieldName] = NewValue;
						IsChanged = true;
					}
				}
			}
			else if (row.Table.Columns[colFieldName].DataType == System.Type.GetType("System.Int64"))
			{
				long OldValue = 0;
					
				//entity (Account, Order, Organization, etc...)
				if (row[colFieldName] != DBNull.Value)
				{
					if (sValue.Length > 0)
					{
						long NewValue = Convert.ToInt64(sValue);
						OldValue = Convert.ToInt64(row[colFieldName]);
						if (OldValue != NewValue)
						{
							row[colFieldName] = NewValue;
							IsChanged = true;
						}
					}
					else
					{
						row[colFieldName] = DBNull.Value;
						IsChanged = true;
					}
				}		
				else
				{
					//When the value is already Null
					if (sValue.Length > 0)
					{
						long NewValue = Convert.ToInt64(sValue);
						row[colFieldName] = NewValue;
						IsChanged = true;
					}
				}
			}
            else if (row.Table.Columns[colFieldName].DataType == System.Type.GetType("System.Single"))
            {
                float OldValue = 0;

                //entity (Account, Order, Organization, etc...)
                if (row[colFieldName] != DBNull.Value)
                {
                    if (sValue.Length > 0)
                    {
                        float NewValue = Convert.ToSingle(sValue);
                        OldValue = Convert.ToSingle(row[colFieldName]);
                        if (OldValue != NewValue)
                        {
                            row[colFieldName] = NewValue;
                            IsChanged = true;
                        }
                    }
                    else
                    {
                        row[colFieldName] = DBNull.Value;
                        IsChanged = true;
                    }
                }
                else
                {
                    //When the value is already Null
                    if (sValue.Length > 0)
                    {
                        float NewValue = Convert.ToSingle(sValue);
                        row[colFieldName] = NewValue;
                        IsChanged = true;
                    }
                }
            }
			else if (row.Table.Columns[colFieldName].DataType == System.Type.GetType("System.Decimal"))
			{
				decimal OldValue = 0;
					
				//entity (Account, Order, Organization, etc...)
				if (row[colFieldName] != DBNull.Value)
				{
					if (sValue.Length > 0)
					{
						decimal NewValue = Convert.ToDecimal(sValue);
						OldValue = Convert.ToDecimal(row[colFieldName]);
						if (OldValue != NewValue)
						{
							row[colFieldName] = NewValue;
							IsChanged = true;
						}
					}
					else
					{
						row[colFieldName] = DBNull.Value;
						IsChanged = true;
					}
				}		
				else
				{
					//When the value is already Null
					if (sValue.Length > 0)
					{
						decimal NewValue = Convert.ToDecimal(sValue);
						row[colFieldName] = NewValue;
						IsChanged = true;
					}
				}
			}	
			else if (row.Table.Columns[colFieldName].DataType == System.Type.GetType("System.Boolean"))
			{
				bool OldValue = false;
					
				//entity (Account, Order, Organization, etc...)
				if (row[colFieldName] != DBNull.Value)
				{
					if (sValue.Length > 0)
					{
						bool NewValue = Convert.ToBoolean(sValue);
						OldValue = Convert.ToBoolean(row[colFieldName]);
						if (OldValue != NewValue)
						{
							row[colFieldName] = NewValue;
							IsChanged = true;
						}
					}
					else
					{
						row[colFieldName] = DBNull.Value;
						IsChanged = true;
					}
				}		
				else
				{
					//When the value is already Null
					if (sValue.Length > 0)
					{
						bool NewValue = Convert.ToBoolean(sValue);
						row[colFieldName] = NewValue;
						IsChanged = true;
					}
				}
			}

			else if (row.Table.Columns[colFieldName].DataType == System.Type.GetType("System.DateTime"))
			{
				DateTime OldValue = DateTime.MinValue;
					
				//entity (Account, Order, Organization, etc...)
				if (row[colFieldName] != DBNull.Value)
				{
					if (sValue.Length > 0)
					{
						DateTime NewValue = Convert.ToDateTime(sValue);
						OldValue = Convert.ToDateTime(row[colFieldName]);
						if (OldValue != NewValue)
						{
							row[colFieldName] = NewValue;
							IsChanged = true;
						}
					}
					else
					{
						row[colFieldName] = DBNull.Value;
						IsChanged = true;
					}
				}		
				else
				{
					//When the value is already Null
					if (sValue.Length > 0)
					{
						DateTime NewValue = Convert.ToDateTime(sValue);
						row[colFieldName] = NewValue;
						IsChanged = true;
					}
				}
			}
			
			return IsChanged;
		
		}

		public bool UpdateRow(DataRow row, string colFieldName, DataRow rowToCopy)
		{
			string strValue = "";
			if (rowToCopy[colFieldName] != DBNull.Value)
			{
				strValue = rowToCopy[colFieldName].ToString();
			}
			return UpdateRow(row, colFieldName, strValue);
		}


		public string FormatPhoneNumber(string phoneNumber)
		{
			string regex_format = @"\d{3}-\d{3}-\d{4}";
			string regex_format_digit = @"\d{10}";
			string fix_phoneNumber = phoneNumber; //Init with the phoneNumber
			bool isValid = false;
			//
			// Check field format
			//
			isValid = (new Regex(regex_format)).IsMatch(phoneNumber);            
            
			if (!isValid)
			{
				isValid = (new Regex(regex_format_digit)).IsMatch(phoneNumber);        
				if (isValid)
				{	
					fix_phoneNumber = string.Format("{0}-{1}-{2}",
						phoneNumber.Substring(0, 3),
						phoneNumber.Substring(3, 3),
						phoneNumber.Substring(6));					
				}
			}
			
			return fix_phoneNumber;
		
		}

        public string FormatZipCode(string zipCode)
        {
            string regex_format_short = @"\d{5}";
            string regex_format_short_alt1 = @"\d{4}";
            string regex_format_long = @"\d{5}-\d{4}"; ;
            string regex_format_long_alt1 = @"\d{4}-\d{4}";
            string regex_format_long_alt2 = @"\d{5}-\d{3}";
            string regex_format_long_alt3 = @"\d{4}-\d{3}";
            string fix_zipCode = zipCode; //Init with the zipCode
            bool isValid = false;
            //
            // Check field format
            //
            if (zipCode.Trim().Length <= 5)
            {
                isValid = (new Regex(regex_format_short)).IsMatch(zipCode);
                if (!isValid)
                {
                    isValid = (new Regex(regex_format_short_alt1)).IsMatch(zipCode);
                    if (isValid)
                    {
                        fix_zipCode = zipCode.PadLeft(5, '0');
                    }
                    
                }
            }
            else
            {
                isValid = (new Regex(regex_format_long)).IsMatch(zipCode);
                if (!isValid)
                {
                    isValid = (new Regex(regex_format_long_alt1)).IsMatch(zipCode);
                    if (isValid)
                    {
                        fix_zipCode = string.Format("{0}-{1}",
                        zipCode.Substring(0, 4).PadLeft(5, '0'),
                        zipCode.Substring(5));
                    }
                    else
                    {
                        isValid = (new Regex(regex_format_long_alt2)).IsMatch(zipCode);
                        if (isValid)
                        {
                            fix_zipCode = string.Format("{0}-{1}",
                            zipCode.Substring(0, 5),
                            zipCode.Substring(6).PadLeft(4, '0'));
                        }
                        else
                        {
                            isValid = (new Regex(regex_format_long_alt3)).IsMatch(zipCode);
                            if (isValid)
                            {
                                fix_zipCode = string.Format("{0}-{1}",
                                zipCode.Substring(0, 4).PadLeft(5, '0'),
                                zipCode.Substring(5).PadLeft(4, '0'));
                            }


                        }
                    
                    
                    }

                }
            }

            return fix_zipCode;

        }

		public int GetNbOfMonth(DateTime dateA, DateTime dateB)
		{
			int diff = 0;
			diff += (dateB.Year- dateA.Year) * 12;
			int diff2 = 0;
			diff2 = dateB.Month - dateA.Month;
			diff += diff2;
			
			if (diff < -1) 
				diff = diff * -1;
			//Adjust for days
			if (dateB.Day < dateA.Day)
				diff -= 1;
				
			return diff;
		}

		public int GetNbOfDay(DateTime dateA, DateTime dateB)
		{
			int diff = 0;
			System.TimeSpan diff1 = dateB.Subtract(dateA);
			diff = diff1.Days;		
				
			return diff;
		}

		public void EmptyPromoLogoTempFolder(string sID, string path)
		{
			DirectoryInfo directory = new DirectoryInfo(path); 
			foreach(FileInfo f in directory.GetFiles(sID+"-*.*") ) 
			{ 
				f.Delete();
			} 
		}

        public bool InsertDuplicateAccountOverride(int newAccountId, int potentialDuplicateAccountId, int createUserId)
        {
            return comDataAccess.InsertDuplicateAccountOverride(newAccountId, potentialDuplicateAccountId, createUserId);
        }

        public bool UpdateTerritoryBYFMID(string fromfmid, string tofmid, string salestofmid, DateTime effectiveDate, string reason, int icreateuserId)
        {
            return comDataAccess.UpdateTerritoryBYFMID(fromfmid, tofmid, salestofmid, effectiveDate, reason, icreateuserId);
        }

        public DataTable SelectAllProfitRateByProgramType(int ProgramTypeID)
        {
            return comDataAccess.SelectAllProfitRateByProgramType(ProgramTypeID);
        }
        public DataTable SelectAllProfitRateByForm(int FormID)
        {
            return comDataAccess.SelectAllProfitRateByFormID(FormID);
        }

        public bool HasProceessedOrder(int CampaignID, int FormID)
        {
            return ((DataTable)comDataAccess.SelectOneProcessedOrder(CampaignID, FormID)).Rows.Count > 0;
        }

        public bool HasOrder(int CampaignID)
        {
            return ((DataTable)comDataAccess.SelectOneOrder(CampaignID)).Rows.Count > 0;
        }

        public bool HasOrder(int CampaignID, int FormID)
        {
            return ((DataTable)comDataAccess.SelectOneOrder(CampaignID,FormID)).Rows.Count > 0;
        }



        

	}
}
