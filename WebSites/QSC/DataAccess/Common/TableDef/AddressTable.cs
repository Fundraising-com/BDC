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
	public class AddressTable: CommonTable
{
public const string TBL_ADDRESS= "Address";
public const string FLD_ADDRESS_ID= "address_id";
public const string FLD_STREET1= "street1";
public const int FLD_STREET1_LENGTH= 50;
public const string FLD_STREET2= "street2";
public const int FLD_STREET2_LENGTH= 50;
public const string FLD_CITY= "city";
public const int FLD_CITY_LENGTH= 50;
public const string FLD_STATEPROVINCE= "stateProvince";
public const int FLD_STATEPROVINCE_LENGTH= 10;
public const string FLD_POSTAL_CODE= "postal_code";
public const int FLD_POSTAL_CODE_LENGTH= 7;
public const string FLD_ZIP4= "zip4";
public const int FLD_ZIP4_LENGTH= 4;
public const string FLD_COUNTRY= "country";
public const int FLD_COUNTRY_LENGTH= 10;
public const string FLD_ADDRESS_TYPE= "address_type";
public const string FLD_ADDRESSLISTID= "AddressListID";
/// <summary>
///     Constructor for CampaignPrizeData.  
///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
/// </summary>
public AddressTable()
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
public AddressTable(SerializationInfo info, StreamingContext context) : base(info, context) 
{
}	
//----------------------------------------------------------------
// Sub BuildDataTable:
//   Creates the following datatable:Address//----------------------------------------------------------------
private void BuildDataTable()
{
	//
	// Create the Address
	//
	this.TableName =TBL_ADDRESS;
	DataColumnCollection columns = this.Columns;
DataColumn Column = columns.Add(FLD_ADDRESS_ID, typeof(Int32));
Column.AllowDBNull = false;
Column.DefaultValue = 0;
columns.Add(FLD_STREET1, typeof(string));
columns.Add(FLD_STREET2, typeof(string));
columns.Add(FLD_CITY, typeof(string));
columns.Add(FLD_STATEPROVINCE, typeof(string));
columns.Add(FLD_POSTAL_CODE, typeof(string));
columns.Add(FLD_ZIP4, typeof(string));
columns.Add(FLD_COUNTRY, typeof(string));
columns.Add(FLD_ADDRESS_TYPE, typeof(Int32));
columns.Add(FLD_ADDRESSLISTID, typeof(Int32));
SetConstraint();
}
private void SetConstraint()
{
this.PrimaryKey = new DataColumn[]{Columns[FLD_ADDRESS_ID]};
}
}
}