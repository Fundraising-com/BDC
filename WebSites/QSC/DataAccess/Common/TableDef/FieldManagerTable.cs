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
	public class FieldManagerTable: CommonTable
{
public const string TBL_FIELDMANAGER= "fieldManager";
public const string FLD_FMID= "FMID";
public const string FLD_STATUS= "status";
public const string FLD_COUNTRY= "country";
public const string	FLD_PHONE_LIST_ID= "phoneListID";
public const string	FLD_ADDRESS_LIST_ID= "addressListID";
public const string FLD_FIRST_NAME= "firstName";
public const string FLD_LAST_NAME= "lastName";
public const string FLD_MIDDLE_INITIAL= "middleInitial";
public const string FLD_EMAIL= "email";
public const string FLD_DMID= "DMID";
public const string FLD_USERIDMODIFIED= "userIDModified";
public const string FLD_DATEMODIFIED= "dateModified";
public const string FLD_COMMENT= "comment";
public const string FLD_DMINDICATOR= "DMIndicator";
public const string FLD_LANG= "lang";
public const string FLD_DELETEDTF= "deletedTF";

/// <summary>
///     Constructor for CampaignPrizeData.  
///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
/// </summary>
public FieldManagerTable()
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
public FieldManagerTable(SerializationInfo info, StreamingContext context) : base(info, context) 
{
}	
//----------------------------------------------------------------
// Sub BuildDataTable:
//   Creates the following datatable:FieldManager//----------------------------------------------------------------
private void BuildDataTable()
{
	//
	// Create the FieldManager
	//
	this.TableName =TBL_FIELDMANAGER;
	DataColumnCollection columns = this.Columns;
	DataColumn Column = columns.Add(FLD_FMID, typeof(string));
	Column.AllowDBNull = false;
	Column.DefaultValue = 0;
	columns.Add(FLD_STATUS, typeof(string));
	columns.Add(FLD_COUNTRY, typeof(string));
	columns.Add(FLD_PHONE_LIST_ID, typeof(Int32));
	columns.Add(FLD_ADDRESS_LIST_ID, typeof(Int32));
	columns.Add(FLD_FIRST_NAME, typeof(string));
	columns.Add(FLD_LAST_NAME, typeof(string));
	columns.Add(FLD_MIDDLE_INITIAL, typeof(string));
	columns.Add(FLD_EMAIL, typeof(string));
	columns.Add(FLD_DMID, typeof(string));
	columns.Add(FLD_USERIDMODIFIED, typeof(string));
	columns.Add(FLD_DATEMODIFIED, typeof(string));
	columns.Add(FLD_COMMENT, typeof(string));
	columns.Add(FLD_DMINDICATOR, typeof(string));
	columns.Add(FLD_LANG, typeof(string));
	columns.Add(FLD_DELETEDTF, typeof(string));
	SetConstraint();
}
private void SetConstraint()
{
this.PrimaryKey = new DataColumn[]{Columns[FLD_FMID]};
}
}
}