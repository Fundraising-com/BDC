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
	public class CAccountTable: CommonTable
{
public const string TBL_CACCOUNT= "CAccount";
public const string FLD_ID= "id";
public const string FLD_NAME= "name";
public const string FLD_COUNTRY= "country";
public const string FLD_LANG= "lang";
public const string FLD_CACCOUNTCODECLASS= "caccountcodeclass";
public const string FLD_CACCOUNTCODEGROUP= "caccountcodegroup";
public const string FLD_PHONELISTID= "phonelistid";
public const string FLD_ADDRESSLISTID= "addresslistid";
public const string FLD_ADDRESS1= "address1";
public const string FLD_ADDRESS2= "address2";
public const string FLD_CITY= "city";
public const string FLD_STATE= "state";
public const string FLD_ZIP= "zip";
public const string FLD_ZIP4= "zip4";
public const string FLD_COUNTY= "county";
public const string FLD_STATUSID= "statusid";
public const string FLD_ENROLLMENT= "enrollment";
public const string FLD_COMMENT= "comment";
public const string FLD_EMAIL= "email";
public const string FLD_ISPRIVATEORG= "isprivateorg";
public const string FLD_ISADULTGROUP= "isadultgroup";
public const string FLD_PARENTID= "parentid";
public const string FLD_SALESREGIONID= "salesregionid";
public const string FLD_STATEMENTPRINTCYCLEID= "statementprintcycleid";
public const string FLD_STATEMENTPRINTSLOT= "statementprintslot";
public const string FLD_DATECREATEDTOSSTHIS= "datecreatedtossthis";
public const string FLD_DATEUPDATED= "dateupdated";
public const string FLD_USERIDMODIFIED= "useridmodified";
public const string FLD_VENDORNUMBER= "vendornumber";
public const string FLD_VENDORSITENAME= "vendorsitename";
public const string FLD_VENDORPAYGROUP= "vendorpaygroup";
public const string FLD_ORIGINALADDRESS1= "originaladdress1";
public const string FLD_ORIGINALADDRESS2= "originaladdress2";
public const string FLD_ORIGINALCITY= "originalcity";
public const string FLD_ORIGINALSTATE= "originalstate";
public const string FLD_ORIGINALZIP= "originalzip";
public const string FLD_ORIGINALZIP4= "originalzip4";
public const string FLD_SHIPTOADDRESS1= "shiptoaddress1";
public const string FLD_SHIPTOADDRESS2= "shiptoaddress2";
public const string FLD_SHIPTOCITY= "shiptocity";
public const string FLD_SHIPTOSTATE= "shiptostate";
public const string FLD_SHIPTOZIP= "shiptozip";
public const string FLD_SHIPTOZIP4= "shiptozip4";
public const string FLD_SPONSOR= "sponsor";
public const string FLD_FMID= "fmid";
/// <summary>
///     Constructor for CampaignPrizeData.  
///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
/// </summary>
public CAccountTable()
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
public CAccountTable(SerializationInfo info, StreamingContext context) : base(info, context) 
{
}	
//----------------------------------------------------------------
// Sub BuildDataTable:
//   Creates the following datatable:CAccount//----------------------------------------------------------------
private void BuildDataTable()
{
	//
	// Create the CAccount
	//
	this.TableName =TBL_CACCOUNT;
	DataColumnCollection columns = this.Columns;
DataColumn Column = columns.Add(FLD_ID, typeof(Int32));
Column.AllowDBNull = false;
Column.DefaultValue = 0;
columns.Add(FLD_NAME, typeof(string));
columns.Add(FLD_COUNTRY, typeof(string));
columns.Add(FLD_LANG, typeof(string));
columns.Add(FLD_CACCOUNTCODECLASS, typeof(string));
columns.Add(FLD_CACCOUNTCODEGROUP, typeof(string));
columns.Add(FLD_PHONELISTID, typeof(string));
columns.Add(FLD_ADDRESSLISTID, typeof(string));
columns.Add(FLD_ADDRESS1, typeof(string));
columns.Add(FLD_ADDRESS2, typeof(string));
columns.Add(FLD_CITY, typeof(string));
columns.Add(FLD_STATE, typeof(string));
columns.Add(FLD_ZIP, typeof(string));
columns.Add(FLD_ZIP4, typeof(string));
columns.Add(FLD_COUNTY, typeof(string));
columns.Add(FLD_STATUSID, typeof(string));
columns.Add(FLD_ENROLLMENT, typeof(string));
columns.Add(FLD_COMMENT, typeof(string));
columns.Add(FLD_EMAIL, typeof(string));
columns.Add(FLD_ISPRIVATEORG, typeof(string));
columns.Add(FLD_ISADULTGROUP, typeof(string));
columns.Add(FLD_PARENTID, typeof(string));
columns.Add(FLD_SALESREGIONID, typeof(string));
columns.Add(FLD_STATEMENTPRINTCYCLEID, typeof(string));
columns.Add(FLD_STATEMENTPRINTSLOT, typeof(string));
columns.Add(FLD_DATECREATEDTOSSTHIS, typeof(string));
columns.Add(FLD_DATEUPDATED, typeof(string));
columns.Add(FLD_USERIDMODIFIED, typeof(string));
columns.Add(FLD_VENDORNUMBER, typeof(string));
columns.Add(FLD_VENDORSITENAME, typeof(string));
columns.Add(FLD_VENDORPAYGROUP, typeof(string));
columns.Add(FLD_ORIGINALADDRESS1, typeof(string));
columns.Add(FLD_ORIGINALADDRESS2, typeof(string));
columns.Add(FLD_ORIGINALCITY, typeof(string));
columns.Add(FLD_ORIGINALSTATE, typeof(string));
columns.Add(FLD_ORIGINALZIP, typeof(string));
columns.Add(FLD_ORIGINALZIP4, typeof(string));
columns.Add(FLD_SHIPTOADDRESS1, typeof(string));
columns.Add(FLD_SHIPTOADDRESS2, typeof(string));
columns.Add(FLD_SHIPTOCITY, typeof(string));
columns.Add(FLD_SHIPTOSTATE, typeof(string));
columns.Add(FLD_SHIPTOZIP, typeof(string));
columns.Add(FLD_SHIPTOZIP4, typeof(string));
columns.Add(FLD_SPONSOR, typeof(string));
columns.Add(FLD_FMID, typeof(string));

SetConstraint();
}
private void SetConstraint()
{
this.PrimaryKey = new DataColumn[]{Columns[FLD_ID]};
}
}
}