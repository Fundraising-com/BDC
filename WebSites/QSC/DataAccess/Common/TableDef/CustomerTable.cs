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
	public class CustomerTable: CommonTable
{
public const string TBL_CUSTOMER= "Customer";
public const string FLD_INSTANCE= "Instance";
public const string FLD_STATUSINSTANCE= "StatusInstance";
public const string FLD_LASTNAME= "LastName";
public const int FLD_LASTNAME_LENGTH= 40;
public const string FLD_FIRSTNAME= "FirstName";
public const int FLD_FIRSTNAME_LENGTH= 40;
public const string FLD_ADDRESS1= "Address1";
public const int FLD_ADDRESS1_LENGTH= 50;
public const string FLD_ADDRESS2= "Address2";
public const int FLD_ADDRESS2_LENGTH= 50;
public const string FLD_CITY= "City";
public const int FLD_CITY_LENGTH= 50;
public const string FLD_COUNTY= "County";
public const int FLD_COUNTY_LENGTH= 31;
public const string FLD_STATE= "State";
public const int FLD_STATE_LENGTH= 10;
public const string FLD_ZIP= "Zip";
public const int FLD_ZIP_LENGTH= 10;
public const string FLD_ZIPPLUSFOUR= "ZipPlusFour";
public const int FLD_ZIPPLUSFOUR_LENGTH= 4;
public const string FLD_OVERRIDEADDRESS= "OverrideAddress";
public const string FLD_CHANGEUSERID= "ChangeUserID";
public const int FLD_CHANGEUSERID_LENGTH= 4;
public const string FLD_CHANGEDATE= "ChangeDate";
public const string FLD_EMAIL= "Email";
public const int FLD_EMAIL_LENGTH= 50;
public const string FLD_PHONE= "Phone";
public const int FLD_PHONE_LENGTH= 25;
public const string FLD_TYPE= "Type";
/// <summary>
///     Constructor for CampaignPrizeData.  
///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
/// </summary>
public CustomerTable()
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
public CustomerTable(SerializationInfo info, StreamingContext context) : base(info, context) 
{
}	
//----------------------------------------------------------------
// Sub BuildDataTable:
//   Creates the following datatable:Customer//----------------------------------------------------------------
private void BuildDataTable()
{
	//
	// Create the Customer
	//
	this.TableName =TBL_CUSTOMER;
	DataColumnCollection columns = this.Columns;
DataColumn Column = columns.Add(FLD_INSTANCE, typeof(Int32));
Column.AllowDBNull = false;
Column.DefaultValue = 0;
columns.Add(FLD_STATUSINSTANCE, typeof(Int32));
columns.Add(FLD_LASTNAME, typeof(string));
columns.Add(FLD_FIRSTNAME, typeof(string));
columns.Add(FLD_ADDRESS1, typeof(string));
columns.Add(FLD_ADDRESS2, typeof(string));
columns.Add(FLD_CITY, typeof(string));
columns.Add(FLD_COUNTY, typeof(string));
columns.Add(FLD_STATE, typeof(string));
columns.Add(FLD_ZIP, typeof(string));
columns.Add(FLD_ZIPPLUSFOUR, typeof(string));
columns.Add(FLD_OVERRIDEADDRESS, typeof(bool));
columns.Add(FLD_CHANGEUSERID, typeof(string));
columns.Add(FLD_CHANGEDATE, typeof(DateTime));
columns.Add(FLD_EMAIL, typeof(string));
columns.Add(FLD_PHONE, typeof(string));
columns.Add(FLD_TYPE, typeof(Int32));
SetConstraint();
}
private void SetConstraint()
{
this.PrimaryKey = new DataColumn[]{Columns[FLD_INSTANCE]};
}
}
}