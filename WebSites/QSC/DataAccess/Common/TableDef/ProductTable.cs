using System;
using System.Data;
using System.Runtime.Serialization;
namespace QSPFulfillment.DataAccess.Common.TableDef
		  {
/// <summary>
///     A custom serializable dataset containing user information.
///     <remarks>
///         This class is used to define the shape of CampaignPrizeData.
///     </remarks>
///     <remarks>
///         The serializale constructor allows objects of type CampaignPrizeData to be remoted.
///     </remarks>
/// </summary>
[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class ProductTable: CommonTable
{
public const string TBL_PRODUCT= "Product";
public const string FLD_PRODUCT_INSTANCE= "Product_Instance";
public const string FLD_PRODUCT_CODE= "Product_Code";
public const int FLD_PRODUCT_CODE_LENGTH= 20;
public const string FLD_PRODUCT_YEAR= "Product_Year";
public const string FLD_PRODUCT_SEASON= "Product_Season";
public const int FLD_PRODUCT_SEASON_LENGTH= 1;
public const string FLD_ALPHA_CODE= "Alpha_Code";
public const int FLD_ALPHA_CODE_LENGTH= 4;
public const string FLD_PRODUCT_NAME= "Product_Name";
public const int FLD_PRODUCT_NAME_LENGTH= 55;
public const string FLD_PRODUCT_SORT_NAME= "Product_Sort_Name";
public const int FLD_PRODUCT_SORT_NAME_LENGTH= 55;
public const string FLD_PUB_NBR= "Pub_Nbr";
public const string FLD_AGES= "Ages";
public const int FLD_AGES_LENGTH= 20;
public const string FLD_INTERNET= "Internet";
public const int FLD_INTERNET_LENGTH= 3;
public const string FLD_ISSUE_RCVD_DT= "Issue_Rcvd_Dt";
public const string FLD_COVERRECEIVED= "CoverReceived";
public const int FLD_COVERRECEIVED_LENGTH= 3;
public const string FLD_HIGHLIGHTCOVER= "HighlightCover";
public const string FLD_FEATURING= "Featuring";
public const string FLD_STATUS= "Status";
public const int FLD_STATUS_LENGTH= 15;
public const string FLD_COMMENT= "Comment";
public const int FLD_COMMENT_LENGTH= 200;
public const string FLD_COMMENTDATE= "CommentDate";
public const string FLD_CATEGORY_CODE= "Category_Code";
public const string FLD_FULFILL_HOUSE_NBR= "Fulfill_House_Nbr";
public const int FLD_FULFILL_HOUSE_NBR_LENGTH= 3;
public const string FLD_MAIL_DT= "Mail_Dt";
public const int FLD_MAIL_DT_LENGTH= 30;
public const string FLD_AUTH_FORM_RTRN_DT= "Auth_Form_Rtrn_Dt";
public const string FLD_ISSUEDATEUSED= "IssueDateUsed";
public const int FLD_ISSUEDATEUSED_LENGTH= 30;
public const string FLD_LOGGED_BY= "Logged_By";
public const int FLD_LOGGED_BY_LENGTH= 15;
public const string FLD_LOG_DT= "Log_Dt";
public const string FLD_LANG= "Lang";
public const int FLD_LANG_LENGTH= 10;
public const string FLD_PRODUCTLINE= "ProductLine";
public const string FLD_DAYSLEADTIME= "DaysLeadTime";
public const string FLD_VENDORNUMBER= "VendorNumber";
public const int FLD_VENDORNUMBER_LENGTH= 30;
public const string FLD_VENDORSITENAME= "VendorSiteName";
public const int FLD_VENDORSITENAME_LENGTH= 15;
public const string FLD_PAYGROUPLOOKUPCODE= "PayGroupLookUpCode";
public const int FLD_PAYGROUPLOOKUPCODE_LENGTH= 25;
public const string FLD_TERMSNAME= "TermsName";
public const int FLD_TERMSNAME_LENGTH= 50;
public const string FLD_CURRENCY= "Currency";
public const string FLD_COUNTRYCODE= "CountryCode";
public const int FLD_COUNTRYCODE_LENGTH= 10;
public const string FLD_TYPE= "Type";
public const string FLD_UNITOFMEASURE= "UnitOfMeasure";
public const int FLD_UNITOFMEASURE_LENGTH= 10;
public const string FLD_UOMCONVFACTOR= "UOMConvFactor";
public const string FLD_UNITWEIGHT= "UnitWeight";
public const string FLD_UNITCOST= "UnitCost";
public const string FLD_ORACLECODE= "OracleCode";
public const int FLD_ORACLECODE_LENGTH= 50;
public const string FLD_PRIZE_LEVEL= "Prize_Level";
public const int FLD_PRIZE_LEVEL_LENGTH= 10;
public const string FLD_PRIZE_LEVEL_QTY_REQUIRED= "Prize_Level_Qty_Required";
/// <summary>
///     Constructor for CampaignPrizeData.  
///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
/// </summary>
public ProductTable()
{
	//
	// Create the tables in the dataset
	//
	BuildDataTable();
}
/// <summary>
///     Constructor to support serialization.
///     <remarks>Constructor that supports serialization.</remarks> 
///     <param name="info">The SerializationInfo object to read from.</param>
///     <param name="context">Information on who is calling this method.</param>
/// </summary>
public ProductTable(SerializationInfo info, StreamingContext context) : base(info, context) 
{
}	
//----------------------------------------------------------------
// Sub BuildDataTable:
//   Creates the following datatable:Product//----------------------------------------------------------------
private void BuildDataTable()
{
	//
	// Create the Product
	//
	this.TableName =TBL_PRODUCT;
	DataColumnCollection columns = this.Columns;
DataColumn Column = columns.Add(FLD_PRODUCT_INSTANCE, typeof(Int32));
Column.AllowDBNull = false;
Column.DefaultValue = 0;
columns.Add(FLD_PRODUCT_CODE, typeof(string));
columns.Add(FLD_PRODUCT_YEAR, typeof(Int32));
columns.Add(FLD_PRODUCT_SEASON, typeof(string));
columns.Add(FLD_ALPHA_CODE, typeof(string));
columns.Add(FLD_PRODUCT_NAME, typeof(string));
columns.Add(FLD_PRODUCT_SORT_NAME, typeof(string));
columns.Add(FLD_PUB_NBR, typeof(Int32));
columns.Add(FLD_AGES, typeof(string));
columns.Add(FLD_INTERNET, typeof(string));
columns.Add(FLD_ISSUE_RCVD_DT, typeof(DateTime));
columns.Add(FLD_COVERRECEIVED, typeof(string));
columns.Add(FLD_HIGHLIGHTCOVER, typeof(Int32));
columns.Add(FLD_FEATURING, typeof(Int32));
columns.Add(FLD_STATUS, typeof(string));
columns.Add(FLD_COMMENT, typeof(string));
columns.Add(FLD_COMMENTDATE, typeof(DateTime));
columns.Add(FLD_CATEGORY_CODE, typeof(Int32));
columns.Add(FLD_FULFILL_HOUSE_NBR, typeof(string));
columns.Add(FLD_MAIL_DT, typeof(string));
columns.Add(FLD_AUTH_FORM_RTRN_DT, typeof(DateTime));
columns.Add(FLD_ISSUEDATEUSED, typeof(string));
columns.Add(FLD_LOGGED_BY, typeof(string));
columns.Add(FLD_LOG_DT, typeof(DateTime));
columns.Add(FLD_LANG, typeof(string));
columns.Add(FLD_PRODUCTLINE, typeof(Int32));
columns.Add(FLD_DAYSLEADTIME, typeof(Int32));
columns.Add(FLD_VENDORNUMBER, typeof(string));
columns.Add(FLD_VENDORSITENAME, typeof(string));
columns.Add(FLD_PAYGROUPLOOKUPCODE, typeof(string));
columns.Add(FLD_TERMSNAME, typeof(string));
columns.Add(FLD_CURRENCY, typeof(Int32));
columns.Add(FLD_COUNTRYCODE, typeof(string));
columns.Add(FLD_TYPE, typeof(Int32));
columns.Add(FLD_UNITOFMEASURE, typeof(string));
columns.Add(FLD_UOMCONVFACTOR, typeof(Int32));
columns.Add(FLD_UNITWEIGHT, typeof(Decimal));
columns.Add(FLD_UNITCOST, typeof(Decimal));
columns.Add(FLD_ORACLECODE, typeof(string));
columns.Add(FLD_PRIZE_LEVEL, typeof(string));
columns.Add(FLD_PRIZE_LEVEL_QTY_REQUIRED, typeof(Int32));
SetConstraint();
}
private void SetConstraint()
{
this.PrimaryKey = new DataColumn[]{Columns[FLD_PRODUCT_INSTANCE]};
}
}
}