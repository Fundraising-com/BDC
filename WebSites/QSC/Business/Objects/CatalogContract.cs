using System;
using System.Data;
using QSP.WebControl;
using FileStore;
using dataAccessRef = DAL.CatalogContractData;
using DAL;

namespace Business.Objects
{
	/// <summary>
	/// Handles returning Catalog Contract Spreadsheet
	/// </summary>
	/// <remarks>
	/// Jeff Miles
	/// April 2007
	/// </remarks>
	
	public enum CatalogContractSeasonalColumns
	{
		MagazineCover_Filename,
		Ad_Filename,
		Ad_PageSize,
		Internet_Approval,
		Contract_ABCCode,
		Ad_Cost,
		Ad_Cost_Currency,
		QSP_Exclusive,
		Oracle_Code,
		Catalog_PageNumber,
		GST_Registration_Number,
		HST_Registration_Number,
		PST_Registration_Number,
		Remit_Code,
		Magazine_Title,
		Product_Type,
		Publisher_Name,
		Publisher_Address1,
		Publisher_Address2,
		Publisher_City,
		Publisher_State,
		Publisher_Zip,
		Publisher_Zip_Four,
		Publisher_Country,
		PublisherContact_FirstName,
		PublisherContact_LastName,
		PublisherContact_Title,
		PublisherContact_Phone,
		PublisherContact_Fax,
		PublisherContact_Email,
		Fulfillment_House_Name,
		Fulfillment_House_Address1,
		Fulfillment_House_Address2,
		Fulfillment_House_City,
		Fulfillment_House_State,
		Fulfillment_House_Zip,
		Fulfillment_House_Zip_Four,
		Fulfillment_House_Country,
		QSPAgencyCode,
		Fulfillment_House_Contact_FirstName,
		Fulfillment_House_Contact_LastName,
		Fulfillment_House_Contact_Title,
		Fulfillment_House_Contact_Email,
		Fulfillment_House_Contact_WorkPhone,
		Fulfillment_House_Contact_Fax,
		EffortKey,
		Listing_Copy_Text,
		Category_Code,
		Listing_Level,
		PlacementLevel,
		Catalog_Spacing,
		Product_Language,
		Vendor_Number,
		Vendor_Site_Name,
		Paygroup_Lookup_Code,
		Remit_Rate_Percent,
		Nbr_Of_Issues_Per_Year,
		Nbr_Of_Issues,
		QSPCA_Listing_Copy_Text,
		NewsStand_Price_Yr_Price,
		NewsStand_Price_Single_Yr_GST_Price,
		NewsStand_Price_Single_Yr_HST_Price,
		Conversion_Rate_Percent,
		NewsStand_Price_Yr_GST_Price,
		NewsStand_Price_Yr_HST_Price,
		US_Price,
		Canadian_Base_Price,
		GST_You_Pay_Price,
		HST_You_Pay_Price,
		Cat_GST_Price,
		Cat_HST_Price,
		Cat_GST_Percent,
		Cat_HST_Percent,
		Cat_Difference_Percent,
		BasePriceWithoutPostage_Price,
		PostageRemitRate_Percent,
		PostageAmount_Price,
		BaseRemitRate_Percent,
		Contract_EffectiveDate,
		Contract_EndDate,
		Contract_DateSubmitted
	}

	public enum CatalogContractNonPrinterColumns
	{
		Internet_Approval,
		Contract_ABCCode,
		QSP_Premium,
		QSP_Exclusive,
		Premium_Indicator,
		Premium_Code,
		Premium_Copy,
		Ad_Cost,
		Ad_Cost_Currency,
		Oracle_Code,
		Product_Language,
		Vendor_Number,
		Vendor_Site_Name,
		Paygroup_Lookup_Code,
		GST_Registration_Number,
		HST_Registration_Number,
		PST_Registration_Number,
		Remit_Code,
		Publisher_Name,
		Publisher_Address1,
		Publisher_Address2,
		Publisher_City,
		Publisher_State,
		Publisher_Zip,
		Publisher_Zip_Four,
		Publisher_Country,
		PublisherContact_FirstName,
		PublisherContact_LastName,
		PublisherContact_Phone,
		PublisherContact_Email,
		Fulfillment_House_Name,
		Fulfillment_House_Address1,
		Fulfillment_House_Address2,
		Fulfillment_House_City,
		Fulfillment_House_State,
		Fulfillment_House_Zip,
		Fulfillment_House_Zip_Four,
		Fulfillment_House_Country,
		QSPAgencyCode,
		Fulfillment_House_Contact_FirstName,
		Fulfillment_House_Contact_LastName,
		Fulfillment_House_Contact_Title,
		Fulfillment_House_Contact_Email,
		Fulfillment_House_Contact_WorkPhone,
		Fulfillment_House_Contact_Fax,
		EffortKey,
		QSPCA_Listing_Copy_Text,
		Conversion_Rate_Percent,
		US_Price,
		Product_Comment,
		Comment,
		Contract_Comment
	}


	public class CatalogContract: BusinessSystem
	{
		DataSet dtResultSet;
		dataAccessRef dataAccess;
		ExcelManager excelMgr;

		protected const string XML_STYLESHEET = "CatalogContract_Excel_Stylesheet";
		protected const string TABLE_NAME = "CatalogContract";
		protected const string CHANGE_COLUMN = "Changes";
		protected const string CONTRACTFORMRECEIVED_COLUMN = "ContractFormReceived";

		#region Properties

		protected override DBTableOperation DataAccessReference
		{
			get
			{
				return dataAccess;
			}
		}

		internal override DataSet baseDataSet
		{
			get
			{
				if(dtResultSet == null)
				{
					dtResultSet = new DataSet();
				}

				return this.dtResultSet;
			}
		}

		public override string DefaultTableName
		{
			get
			{
				return TABLE_NAME;
			}
		}
	
		#endregion

		public CatalogContract()
		{
			dtResultSet = new DataSet();
			dataAccess = new dataAccessRef();
		}

		public byte[] GetCatalogContract(int CatalogID, int CatalogIDLastSeason, string ReportType)
		{
			//Get all data
			FillCatalogContract(CatalogID, CatalogIDLastSeason);

			FilterCatalogContract(ReportType);

			//Flag if data changed from last season
			MarkSeasonalDifferences();

			//Remove Last Season's Columns
			RemoveLastSeasonColumns();
			
			//put dtResultSet in excel object, and format (highlights, etc) document with Stylesheet
			excelMgr = new ExcelManager(dtResultSet, XML_STYLESHEET);

			return excelMgr.ExcelFile;
		}

		private void FillCatalogContract(int CatalogID, int CatalogIDLastSeason)
		{
			try
			{
				dataAccess.SelectAll(dtResultSet, DefaultTableName, CatalogID, CatalogIDLastSeason);
			}
			catch(Exception ex)
			{
				ManageError(ex);
			}
		}

		private void MarkSeasonalDifferences()
		{
			Enum catalogContractSeasonalColumns = new CatalogContractSeasonalColumns();

			foreach (DataRow r in dtResultSet.Tables[0].Rows)
			{
				if (r[CONTRACTFORMRECEIVED_COLUMN].ToString() == "N/A")
                    r[CHANGE_COLUMN] = "N/A";

				foreach (String column in System.Enum.GetNames(catalogContractSeasonalColumns.GetType()))
				{
					if (dtResultSet.Tables[0].Columns.Contains(column) && dtResultSet.Tables[0].Columns.Contains(column+"_LastSeason"))
					{
						if (r[column].ToString() != r[column+"_LastSeason"].ToString())
						{
							if (r[CONTRACTFORMRECEIVED_COLUMN].ToString() != "N/A")
							{
								r[CHANGE_COLUMN] = "Yes";
								r[column] = "#highlight#" + r[column];
							}
						}
					}
				}
			}
		}

		private void RemoveLastSeasonColumns()
		{
			Enum catalogContractSeasonalColumns = new CatalogContractSeasonalColumns();

			foreach (String column in System.Enum.GetNames(catalogContractSeasonalColumns.GetType()))
			{
				if (dtResultSet.Tables[0].Columns.Contains(column+"_LastSeason"))
					dtResultSet.Tables[0].Columns.Remove(column+"_LastSeason");
			}

		}

		private void FilterCatalogContract(string ReportType)
		{
			if (ReportType == "Printer")
			{
				Enum catalogContractNonPrinterColumns = new CatalogContractNonPrinterColumns();
				
				foreach (String nonPrinterColumn in System.Enum.GetNames(catalogContractNonPrinterColumns.GetType()))
				{
					dtResultSet.Tables[0].Columns.Remove(nonPrinterColumn);
					if (dtResultSet.Tables[0].Columns.Contains(nonPrinterColumn+"_LastSeason"))
						dtResultSet.Tables[0].Columns.Remove(nonPrinterColumn+"_LastSeason");
				}
			}
		}
	}
}
