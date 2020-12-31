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
	public class PAYMENTTable: CommonTable
{
public const string TBL_PAYMENT= "PAYMENT";
public const string FLD_PAYMENT_ID= "PAYMENT_ID";
public const string FLD_ACCOUNT_ID= "ACCOUNT_ID";
public const string FLD_ACCOUNT_TYPE_ID= "ACCOUNT_TYPE_ID";
public const string FLD_PAYMENT_METHOD_ID= "PAYMENT_METHOD_ID";
public const string FLD_PAYMENT_EFFECTIVE_DATE= "PAYMENT_EFFECTIVE_DATE";
public const string FLD_CHEQUE_NUMBER= "CHEQUE_NUMBER";
public const int FLD_CHEQUE_NUMBER_LENGTH= 50;
public const string FLD_CHEQUE_DATE= "CHEQUE_DATE";
public const string FLD_CHEQUE_PAYER= "CHEQUE_PAYER";
public const int FLD_CHEQUE_PAYER_LENGTH= 50;
public const string FLD_CREDIT_CARD_OWNER= "CREDIT_CARD_OWNER";
public const int FLD_CREDIT_CARD_OWNER_LENGTH= 80;
public const string FLD_CREDIT_CARD_AUTHORIZATION= "CREDIT_CARD_AUTHORIZATION";
public const int FLD_CREDIT_CARD_AUTHORIZATION_LENGTH= 20;
public const string FLD_PAYMENT_AMOUNT= "PAYMENT_AMOUNT";
public const string FLD_NOTE_TO_PRINT= "NOTE_TO_PRINT";
public const int FLD_NOTE_TO_PRINT_LENGTH= 100;
public const string FLD_DATETIME_CREATED= "DATETIME_CREATED";
public const string FLD_DATETIME_MODIFIED= "DATETIME_MODIFIED";
public const string FLD_LAST_UPDATED_BY= "LAST_UPDATED_BY";
public const int FLD_LAST_UPDATED_BY_LENGTH= 30;
public const string FLD_ORDER_ID= "ORDER_ID";
public const string FLD_COUNTRY_CODE= "COUNTRY_CODE";
public const int FLD_COUNTRY_CODE_LENGTH= 10;
public const string FLD_CAMPAIGN_ID= "CAMPAIGN_ID";
/// <summary>
///     Constructor for CampaignPrizeData.  
///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
/// </summary>
public PAYMENTTable()
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
public PAYMENTTable(SerializationInfo info, StreamingContext context) : base(info, context) 
{
}	
//----------------------------------------------------------------
// Sub BuildDataTable:
//   Creates the following datatable:PAYMENT//----------------------------------------------------------------
private void BuildDataTable()
{
	//
	// Create the PAYMENT
	//
	this.TableName =TBL_PAYMENT;
	DataColumnCollection columns = this.Columns;
DataColumn Column = columns.Add(FLD_PAYMENT_ID, typeof(Int32));
Column.AllowDBNull = false;
Column.DefaultValue = 0;
columns.Add(FLD_ACCOUNT_ID, typeof(Int32));
columns.Add(FLD_ACCOUNT_TYPE_ID, typeof(Int32));
columns.Add(FLD_PAYMENT_METHOD_ID, typeof(Int32));
columns.Add(FLD_PAYMENT_EFFECTIVE_DATE, typeof(DateTime));
columns.Add(FLD_CHEQUE_NUMBER, typeof(string));
columns.Add(FLD_CHEQUE_DATE, typeof(DateTime));
columns.Add(FLD_CHEQUE_PAYER, typeof(string));
columns.Add(FLD_CREDIT_CARD_OWNER, typeof(string));
columns.Add(FLD_CREDIT_CARD_AUTHORIZATION, typeof(string));
columns.Add(FLD_PAYMENT_AMOUNT, typeof(Decimal));
columns.Add(FLD_NOTE_TO_PRINT, typeof(string));
columns.Add(FLD_DATETIME_CREATED, typeof(DateTime));
columns.Add(FLD_DATETIME_MODIFIED, typeof(DateTime));
columns.Add(FLD_LAST_UPDATED_BY, typeof(string));
columns.Add(FLD_ORDER_ID, typeof(Int32));
columns.Add(FLD_COUNTRY_CODE, typeof(string));
columns.Add(FLD_CAMPAIGN_ID, typeof(Int32));
SetConstraint();
}
private void SetConstraint()
{
this.PrimaryKey = new DataColumn[]{Columns[FLD_PAYMENT_ID]};
}
}
}