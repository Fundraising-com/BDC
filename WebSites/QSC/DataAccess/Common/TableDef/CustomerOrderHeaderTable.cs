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
	public class CustomerOrderHeaderTable: CommonTable
{
public const string TBL_CUSTOMERORDERHEADER= "CustomerOrderHeader";
public const string FLD_INSTANCE= "Instance";
public const string FLD_NEXTDETAILTRANSID= "NextDetailTransID";
public const string FLD_ACCOUNTID= "AccountID";
public const string FLD_CUSTOMERBILLTOINSTANCE= "CustomerBillToInstance";
public const string FLD_STUDENTINSTANCE= "StudentInstance";
public const string FLD_STATUSINSTANCE= "StatusInstance";
public const string FLD_FIRSTSTATUSINSTANCE= "FirstStatusInstance";
public const string FLD_TOTALPROCESSINGFEE= "TotalProcessingFee";
public const string FLD_TOTALPROCESSINGFEEA= "TotalProcessingFeeA";
public const string FLD_PROCESSINGFEETAX= "ProcessingFeeTax";
public const string FLD_PROCESSINGFEETAXA= "ProcessingFeeTaxA";
public const string FLD_PROCESSINGFEETRANSID= "ProcessingFeeTransID";
public const string FLD_ORDERBATCHDATE= "OrderBatchDate";
public const string FLD_ORDERBATCHID= "OrderBatchID";
public const string FLD_ORDERBATCHSEQUENCE= "OrderBatchSequence";
public const string FLD_CREATIONDATE= "CreationDate";
public const string FLD_LASTSENTINVOICEDATE= "LastSentInvoiceDate";
public const string FLD_NUMBERINVOICESSENT= "NumberInvoicesSent";
public const string FLD_FORCEINVOICE= "ForceInvoice";
public const string FLD_DELFLAG= "DelFlag";
public const string FLD_CHANGEUSERID= "ChangeUserID";
public const int FLD_CHANGEUSERID_LENGTH= 4;
public const string FLD_CHANGEDATE= "ChangeDate";
public const string FLD_TYPE= "Type";
public const string FLD_PAYMENTMETHODINSTANCE= "PaymentMethodInstance";
public const string FLD_CAMPAIGNID= "CampaignID";
/// <summary>
///     Constructor for CampaignPrizeData.  
///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
/// </summary>
public CustomerOrderHeaderTable()
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
public CustomerOrderHeaderTable(SerializationInfo info, StreamingContext context) : base(info, context) 
{
}	
//----------------------------------------------------------------
// Sub BuildDataTable:
//   Creates the following datatable:CustomerOrderHeader//----------------------------------------------------------------
private void BuildDataTable()
{
	//
	// Create the CustomerOrderHeader
	//
	this.TableName =TBL_CUSTOMERORDERHEADER;
	DataColumnCollection columns = this.Columns;
DataColumn Column = columns.Add(FLD_INSTANCE, typeof(Int32));
Column.AllowDBNull = false;
Column.DefaultValue = 0;
columns.Add(FLD_NEXTDETAILTRANSID, typeof(Int32));
columns.Add(FLD_ACCOUNTID, typeof(Int32));
columns.Add(FLD_CUSTOMERBILLTOINSTANCE, typeof(Int32));
columns.Add(FLD_STUDENTINSTANCE, typeof(Int32));
columns.Add(FLD_STATUSINSTANCE, typeof(Int32));
columns.Add(FLD_FIRSTSTATUSINSTANCE, typeof(Int32));
columns.Add(FLD_TOTALPROCESSINGFEE, typeof(double));
columns.Add(FLD_TOTALPROCESSINGFEEA, typeof(double));
columns.Add(FLD_PROCESSINGFEETAX, typeof(double));
columns.Add(FLD_PROCESSINGFEETAXA, typeof(double));
columns.Add(FLD_PROCESSINGFEETRANSID, typeof(Int32));
columns.Add(FLD_ORDERBATCHDATE, typeof(DateTime));
columns.Add(FLD_ORDERBATCHID, typeof(Int32));
columns.Add(FLD_ORDERBATCHSEQUENCE, typeof(Int32));
columns.Add(FLD_CREATIONDATE, typeof(DateTime));
columns.Add(FLD_LASTSENTINVOICEDATE, typeof(DateTime));
columns.Add(FLD_NUMBERINVOICESSENT, typeof(Int32));
columns.Add(FLD_FORCEINVOICE, typeof(bool));
columns.Add(FLD_DELFLAG, typeof(bool));
columns.Add(FLD_CHANGEUSERID, typeof(string));
columns.Add(FLD_CHANGEDATE, typeof(DateTime));
columns.Add(FLD_TYPE, typeof(Int32));
columns.Add(FLD_PAYMENTMETHODINSTANCE, typeof(Int32));
columns.Add(FLD_CAMPAIGNID, typeof(Int32));
SetConstraint();
}
private void SetConstraint()
{
this.PrimaryKey = new DataColumn[]{Columns[FLD_INSTANCE]};
}
}
}