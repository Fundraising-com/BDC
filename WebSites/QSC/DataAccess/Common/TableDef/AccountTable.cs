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
	public class AccountTable: CommonTable
{
public const string TBL_ACCOUNT= "Account";
public const string FLD_ID= "ID";
public const string FLD_NAME= "Name";
public const int FLD_NAME_LENGTH= 30;
public const string FLD_ADDRESS1= "Address1";
public const int FLD_ADDRESS1_LENGTH= 25;
public const string FLD_ADDRESS2= "Address2";
public const int FLD_ADDRESS2_LENGTH= 25;
public const string FLD_CITY= "City";
public const int FLD_CITY_LENGTH= 15;
public const string FLD_STATE= "State";
public const int FLD_STATE_LENGTH= 2;
public const string FLD_ZIP= "Zip";
public const int FLD_ZIP_LENGTH= 6;
public const string FLD_ZIPPLUSFOUR= "ZipPlusFour";
public const int FLD_ZIPPLUSFOUR_LENGTH= 4;
public const string FLD_ATTNLINE= "AttnLine";
public const int FLD_ATTNLINE_LENGTH= 25;
public const string FLD_FIELDMANAGERNO= "FieldManagerNo";
public const int FLD_FIELDMANAGERNO_LENGTH= 4;
public const string FLD_FIELDMANAGERREGION= "FieldManagerRegion";
public const int FLD_FIELDMANAGERREGION_LENGTH= 2;
public const string FLD_COUNTY= "County";
public const int FLD_COUNTY_LENGTH= 15;
public const string FLD_COUNTYCODE= "CountyCode";
public const int FLD_COUNTYCODE_LENGTH= 3;
public const string FLD_SCHOOLTYPE= "SchoolType";
public const int FLD_SCHOOLTYPE_LENGTH= 2;
public const string FLD_PUBLICCATHOLIC= "PublicCatholic";
public const int FLD_PUBLICCATHOLIC_LENGTH= 2;
public const string FLD_TAXEXEMPTNUMBER= "TaxExemptNumber";
public const int FLD_TAXEXEMPTNUMBER_LENGTH= 10;
public const string FLD_CAMPAIGNSTART= "CampaignStart";
public const string FLD_CAMPAIGNEND= "CampaignEnd";
public const string FLD_ISNATIONAL= "IsNational";
public const string FLD_UNITTYPE= "UnitType";
public const int FLD_UNITTYPE_LENGTH= 1;
public const string FLD_NATIONALDISTRICT= "NationalDistrict";
public const int FLD_NATIONALDISTRICT_LENGTH= 2;
public const string FLD_NATIONALFIELDMANAGER= "NationalFieldManager";
public const int FLD_NATIONALFIELDMANAGER_LENGTH= 4;
public const string FLD_SCHOOLDISTRICTNAME= "SchoolDistrictName";
public const int FLD_SCHOOLDISTRICTNAME_LENGTH= 30;
public const string FLD_NUMBEROFCLASSROOMS= "NumberOfClassrooms";
public const string FLD_NUMBEROFSTUDENTS= "NumberOfStudents";
public const string FLD_SHIPTOACCTORFM= "ShipToAcctOrFM";
public const int FLD_SHIPTOACCTORFM_LENGTH= 1;
public const string FLD_AMFMIND= "AMFMInd";
public const int FLD_AMFMIND_LENGTH= 1;
public const string FLD_COMMISSION= "Commission";
/// <summary>
///     Constructor for CampaignPrizeData.  
///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
/// </summary>
public AccountTable()
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
public AccountTable(SerializationInfo info, StreamingContext context) : base(info, context) 
{
}	
//----------------------------------------------------------------
// Sub BuildDataTable:
//   Creates the following datatable:Account//----------------------------------------------------------------
private void BuildDataTable()
{
	//
	// Create the Account
	//
	this.TableName =TBL_ACCOUNT;
	DataColumnCollection columns = this.Columns;
DataColumn Column = columns.Add(FLD_ID, typeof(Int32));
Column.AllowDBNull = false;
Column.DefaultValue = 0;
columns.Add(FLD_NAME, typeof(string));
columns.Add(FLD_ADDRESS1, typeof(string));
columns.Add(FLD_ADDRESS2, typeof(string));
columns.Add(FLD_CITY, typeof(string));
columns.Add(FLD_STATE, typeof(string));
columns.Add(FLD_ZIP, typeof(string));
columns.Add(FLD_ZIPPLUSFOUR, typeof(string));
columns.Add(FLD_ATTNLINE, typeof(string));
columns.Add(FLD_FIELDMANAGERNO, typeof(string));
columns.Add(FLD_FIELDMANAGERREGION, typeof(string));
columns.Add(FLD_COUNTY, typeof(string));
columns.Add(FLD_COUNTYCODE, typeof(string));
columns.Add(FLD_SCHOOLTYPE, typeof(string));
columns.Add(FLD_PUBLICCATHOLIC, typeof(string));
columns.Add(FLD_TAXEXEMPTNUMBER, typeof(string));
columns.Add(FLD_CAMPAIGNSTART, typeof(DateTime));
columns.Add(FLD_CAMPAIGNEND, typeof(DateTime));
columns.Add(FLD_ISNATIONAL, typeof(bool));
columns.Add(FLD_UNITTYPE, typeof(string));
columns.Add(FLD_NATIONALDISTRICT, typeof(string));
columns.Add(FLD_NATIONALFIELDMANAGER, typeof(string));
columns.Add(FLD_SCHOOLDISTRICTNAME, typeof(string));
columns.Add(FLD_NUMBEROFCLASSROOMS, typeof(Int32));
columns.Add(FLD_NUMBEROFSTUDENTS, typeof(Int32));
columns.Add(FLD_SHIPTOACCTORFM, typeof(string));
columns.Add(FLD_AMFMIND, typeof(string));
columns.Add(FLD_COMMISSION, typeof(double));
SetConstraint();
}
private void SetConstraint()
{
this.PrimaryKey = new DataColumn[]{Columns[FLD_ID]};
}
}
}