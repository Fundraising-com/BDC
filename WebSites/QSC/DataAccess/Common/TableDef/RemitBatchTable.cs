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
	public class RemitBatchTable: CommonTable
	{
		public const string TBL_REMITBATCH= "RemitBatch";
		public const string FLD_ID= "ID";
		public const string FLD_DATE= "Date";
		public const string FLD_COUNTRYCODE= "CountryCode";
		public const int FLD_COUNTRYCODE_LENGTH= 2;
		public const string FLD_STATUS= "Status";
		public const string FLD_FILENAME= "Filename";
		public const int FLD_FILENAME_LENGTH= 200;
		public const string FLD_FULFILLMENTHOUSENBR= "FulfillmentHouseNbr";
		public const int FLD_FULFILLMENTHOUSENBR_LENGTH= 3;
		public const string FLD_TOTALBASEPRICE= "TotalBasePrice";
		public const string FLD_TOTALUNITS= "TotalUnits";
		public const string FLD_TOTALCHADD= "TotalCHADD";
		public const string FLD_TOTALCANCELLED= "TotalCancelled";
		public const string FLD_DATECHANGED= "DateChanged";
		public const string FLD_USERIDCHANGED= "UserIDChanged";
		public const int FLD_USERIDCHANGED_LENGTH= 4;
		public const string FLD_TOTALCATALOGPRICE= "TotalCatalogPrice";
		public const string FLD_TOTALITEMPRICE= "TotalItemPrice";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public RemitBatchTable()
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
		public RemitBatchTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:RemitBatch//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the RemitBatch
			//
			this.TableName =TBL_REMITBATCH;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_ID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_DATE, typeof(DateTime));
			columns.Add(FLD_COUNTRYCODE, typeof(string));
			columns.Add(FLD_STATUS, typeof(Int32));
			columns.Add(FLD_FILENAME, typeof(string));
			columns.Add(FLD_FULFILLMENTHOUSENBR, typeof(string));
			columns.Add(FLD_TOTALBASEPRICE, typeof(Decimal));
			columns.Add(FLD_TOTALUNITS, typeof(Int32));
			columns.Add(FLD_TOTALCHADD, typeof(Int32));
			columns.Add(FLD_TOTALCANCELLED, typeof(Int32));
			columns.Add(FLD_DATECHANGED, typeof(DateTime));
			columns.Add(FLD_USERIDCHANGED, typeof(string));
			columns.Add(FLD_TOTALCATALOGPRICE, typeof(Decimal));
			columns.Add(FLD_TOTALITEMPRICE, typeof(Decimal));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_ID]};
		}
	}
}