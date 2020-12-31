using System;
using System.Data;
using System.Runtime.Serialization;
namespace QSPFulfillment.DataAccess.Common.TableDef
		  {
[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class CampaignTable: CommonTable
{
public const string TBL_CAMPAIGN= "Campaign";
public const string FLD_ID= "ID";
public const string FLD_STATUS= "Status";
public const string FLD_RENEWAL= "Renewal";
public const string FLD_COUNTRY = "Country";
public const int FLD_COUNTRY_LENGTH = 50;
public const string FLD_FMID= "FMID";
public const string FLD_DATECHANGED = "DateChanged";
public const string FLD_LANG = "Lang";
public const string FLD_ENDDATE = "EndDate";
public const string FLD_STARTDATE = "StartDate";
public const string FLD_INCENTIVESBILLTOID= "IncentivesBillToID";
public const string FLD_BILLTOACCOUNTID = "BillToAccountID";
public const string FLD_SHIPTOCAMPAIGNCONTACTID = "ShipToCampaignContactID";
public const string FLD_SHIPTOACCOUNTID = "ShipToAccountID";
public const string FLD_ESTIMATEDGROSS = "EstimatedGross";
public const string FLD_NUMBEROFPARTICIPANTS = "NumberOfParticipants";
public const string FLD_NUMBEROFCLASSROOOMS = "NumberOfClassroooms";
public const string FLD_NUMBEROFSTAFF = "NumberOfStaff";
public const string FLD_BILLTOCAMPAIGNCONTACTID = "BillToCampaignContactID";
public const string FLD_SUPPLIESCAMPAIGNCONTACTID = "SuppliesCampaignContactID";
public const string FLD_SUPPLIESSHIPTOCAMPAIGNCONTACTID = "SuppliesShipToCampaignContactID";
public const string FLD_SUPPLIESDELIVERYDATE = "SuppliesDeliveryDate";
public const string FLD_SPECIALINSTRUCTIONS = "SpecialInstructions";
public const string FLD_ISSTAFFORDER = "IsStaffOrder";
public const string FLD_STAFFORDERDISCOUNT = "StaffOrderDiscount";
public const string FLD_ISTESTCAMPAIGN = "IsTestCampaign";
public const string FLD_DATEMODIFIED = "DateModified";
public const string FLD_USERIDMODIFIED = "UserIDModified";
public const string FLD_ISPAYLATER = "IsPayLater";
public const string FLD_INCENTIVESDISTRIBUTIONID = "IncentivesDistributionID";
public const string FLD_FSREQUIRED = "FSRequired";
public const string FLD_FSEXTRAREQUIRED = "FSExtraRequired";
public const string FLD_FSORDERRECCREATED = "FSOrderRecCreated";
public const string FLD_APPROVEDSTATUSDATE = "ApprovedStatusDate";
public const string FLD_MAGNETSTATEMENTDATE = "MagnetStatementDate";
public const string FLD_REWARDSPROGRAMCUMULATIVE = "RewardsProgramCumulative";
public const string FLD_REWARDSPROGRAMCHART = "RewardsProgramChart";
public const string FLD_REWARDSPROGRAMDRAW = "RewardsProgramDraw";
public const string FLD_CONTACTNAME = "ContactName";
public const string FLD_PHONELISTID = "PhoneListID";
public const string FLD_SUPPLIESADDRESSID = "SuppliesAddressID";
public const string FLD_DATESUBMITTED = "DateSubmitted";
public const string FLD_COOKIEDOUGHDELIVERYDATE = "CookieDoughDeliveryDate";

/// <summary>
///     Constructor for CampaignPrizeData.  
///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
/// </summary>
public CampaignTable()
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
public CampaignTable(SerializationInfo info, StreamingContext context) : base(info, context) 
{
}	
//----------------------------------------------------------------
// Sub BuildDataTable:
//   Creates the following datatable:Campaign//----------------------------------------------------------------
private void BuildDataTable()
{
	//
	// Create the Campaign
	//
this.TableName =TBL_CAMPAIGN;
DataColumnCollection columns = this.Columns;
DataColumn Column = columns.Add(FLD_ID, typeof(Int32));
Column.AllowDBNull = false;
Column.DefaultValue = 0;

columns.Add(FLD_STATUS, typeof(Int32));
columns.Add(FLD_RENEWAL, typeof(Int32));
columns.Add(FLD_COUNTRY, typeof(string));
columns.Add(FLD_FMID, typeof(string));
columns.Add(FLD_DATECHANGED, typeof(string));
columns.Add(FLD_LANG, typeof(string));
columns.Add(FLD_ENDDATE, typeof(DateTime));
columns.Add(FLD_STARTDATE, typeof(DateTime));
columns.Add(FLD_INCENTIVESBILLTOID, typeof(Int32));
columns.Add(FLD_BILLTOACCOUNTID, typeof(Int32));
columns.Add(FLD_SHIPTOCAMPAIGNCONTACTID, typeof(Int32));
columns.Add(FLD_SHIPTOACCOUNTID, typeof(Int32));
columns.Add(FLD_ESTIMATEDGROSS, typeof(string));
columns.Add(FLD_NUMBEROFPARTICIPANTS, typeof(Int32));
columns.Add(FLD_NUMBEROFCLASSROOOMS, typeof(Int32));
columns.Add(FLD_NUMBEROFSTAFF, typeof(Int32));
columns.Add(FLD_BILLTOCAMPAIGNCONTACTID, typeof(Int32));
columns.Add(FLD_SUPPLIESCAMPAIGNCONTACTID, typeof(Int32));
columns.Add(FLD_SUPPLIESSHIPTOCAMPAIGNCONTACTID, typeof(Int32));
columns.Add(FLD_SUPPLIESDELIVERYDATE, typeof(DateTime));
columns.Add(FLD_SPECIALINSTRUCTIONS, typeof(string));
columns.Add(FLD_ISSTAFFORDER, typeof(Int32));
columns.Add(FLD_STAFFORDERDISCOUNT, typeof(string));
columns.Add(FLD_ISTESTCAMPAIGN, typeof(Int32));
columns.Add(FLD_DATEMODIFIED, typeof(DateTime));
columns.Add(FLD_USERIDMODIFIED, typeof(string));
columns.Add(FLD_ISPAYLATER , typeof(Int32));
columns.Add(FLD_INCENTIVESDISTRIBUTIONID, typeof(Int32));
columns.Add(FLD_FSREQUIRED, typeof(Int32));
columns.Add(FLD_FSEXTRAREQUIRED, typeof(Int32));
columns.Add(FLD_FSORDERRECCREATED, typeof(Int32));
columns.Add(FLD_APPROVEDSTATUSDATE, typeof(DateTime));
columns.Add(FLD_MAGNETSTATEMENTDATE, typeof(DateTime));
columns.Add(FLD_REWARDSPROGRAMCUMULATIVE, typeof(Int32));
columns.Add(FLD_REWARDSPROGRAMCHART, typeof(Int32));
columns.Add(FLD_REWARDSPROGRAMDRAW, typeof(Int32));
columns.Add(FLD_CONTACTNAME, typeof(string));
columns.Add(FLD_PHONELISTID, typeof(Int32));
columns.Add(FLD_SUPPLIESADDRESSID, typeof(Int32));
columns.Add(FLD_DATESUBMITTED, typeof(DateTime));
columns.Add(FLD_COOKIEDOUGHDELIVERYDATE, typeof(DateTime));
SetConstraint();
}
private void SetConstraint()
{
this.PrimaryKey = new DataColumn[]{Columns[FLD_ID]};
}
}
}